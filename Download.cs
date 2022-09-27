using Downloader;
using System.ComponentModel;
using YuukiPS_Launcher.Yuuki;

namespace YuukiPS_Launcher
{
    public partial class Download : Form
    {
        private string set_download;
        private string set_folder;
        private DownloadService dl;

        public Download(string url_download, string folder_download)
        {
            set_download = url_download;
            set_folder = folder_download;

            InitializeComponent();
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(set_download))
            {
                MessageBox.Show("Download failed because no url was found");
                return;
            }

            if (Directory.Exists(set_folder))
            {
                MessageBox.Show("Can't save file because folder can't be found or can't be accessed");
                return;
            }


            btDownload.Enabled = false;

            GetNameDownload.Text = set_download;

            if (dl == null)
            {
                var downloadOpt = new DownloadConfiguration()
                {
                    ChunkCount = 8, // file parts to download, default value is 1
                    OnTheFlyDownload = true, // caching in-memory or not? default values is true
                    ParallelDownload = true // download parts of file as parallel or not. Default value is false
                };
                dl = new DownloadService(downloadOpt);
                dl.DownloadStarted += Dl_DownloadStarted;
                dl.DownloadFileCompleted += Dl_DownloadFileCompleted;
                dl.DownloadProgressChanged += Dl_DownloadProgressChanged;
                dl.ChunkDownloadProgressChanged += Dl_ChunkDownloadProgressChanged;
                try
                {
                    dl.DownloadFileTaskAsync(set_download, set_folder);
                }
                catch (Exception ek)
                {
                    MessageBox.Show($"Failed downloading Data", "Oh Snap!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show($"Error Download", "Oh Snap!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void Dl_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            btDownload.Invoke((Action)delegate
            {
                btDownload.Enabled = true;
            });
            GetNumDownload.Invoke((Action)delegate
            {
                GetNumDownload.Text = "Done";
            });
            DialogResult = DialogResult.OK;
        }

        private void Dl_ChunkDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            //Console.WriteLine($@"Update {e.ReceivedBytes} of {e.TotalBytesToReceive}");
        }

        private void Dl_DownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            double nonZeroSpeed = e.BytesPerSecondSpeed + 0.0001;
            int estimateTime = (int)((e.TotalBytesToReceive - e.ReceivedBytesSize) / nonZeroSpeed);
            bool isMinutes = estimateTime >= 60;
            string timeLeftUnit = "seconds";

            if (isMinutes)
            {
                timeLeftUnit = "minutes";
                estimateTime /= 60;
            }

            if (estimateTime < 0)
            {
                estimateTime = 0;
                timeLeftUnit = "unknown";
            }

            string bytesReceived = Tool.CalcMemoryMensurableUnit(e.ReceivedBytesSize);
            string totalBytesToReceive = Tool.CalcMemoryMensurableUnit(e.TotalBytesToReceive);

            GetNumDownload.Invoke((Action)delegate
            {
                GetNumDownload.Text = $"{bytesReceived} of {totalBytesToReceive} | {estimateTime} {timeLeftUnit} left | Speed: {Tool.CalcMemoryMensurableUnit(e.BytesPerSecondSpeed)}/s";
            });
        }

        private void Dl_DownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            Console.WriteLine("Start Download: " + e.FileName);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (dl != null)
            {
                dl.CancelAsync();
                dl.Dispose();
            }
            DialogResult = DialogResult.Cancel;
        }

        private void Download_Load(object sender, EventArgs e)
        {
            btDownload.PerformClick();
        }
    }
}
