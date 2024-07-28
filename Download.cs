using Downloader;
using System.ComponentModel;
using YuukiPS_Launcher.Utils;
using YuukiPS_Launcher.Yuuki;

namespace YuukiPS_Launcher
{
    public partial class Download : Form
    {
        private readonly string setDownload = "";
        private readonly string setFolder = "";
        private DownloadService? dl = null;

        public Download(string urlDownload = "", string folderDownload = "")
        {
            setDownload = urlDownload;
            setFolder = folderDownload;

            InitializeComponent();
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(setDownload))
            {
                Logger.Error("Download", "Download failed: No URL provided");
                MessageBox.Show("Download failed because no URL was found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(setFolder)))
            {
                Logger.Error("Download", $"Download failed: Folder not found or inaccessible - {setFolder}");
                MessageBox.Show("Can't save file because the destination folder can't be found or accessed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(setFolder))
            {
                Logger.Info("Download", $"File old found {setFolder} remove for redownload?");
                File.Delete(setFolder);
            }

            btDownload.Enabled = false;

            GetNameDownload.Text = setDownload;

            if (dl == null)
            {
                var downloadOpt = new DownloadConfiguration()
                {
                    ChunkCount = 8, // file parts to download, default value is 1
                    //OnTheFlyDownload = true, // caching in-memory or not? default values is true
                    ParallelDownload = true // download parts of file as parallel or not. Default value is false
                };
                dl = new DownloadService(downloadOpt);
                dl.DownloadStarted += Dl_DownloadStarted;
                dl.DownloadFileCompleted += Dl_DownloadFileCompleted;
                dl.DownloadProgressChanged += Dl_DownloadProgressChanged;
                dl.ChunkDownloadProgressChanged += Dl_ChunkDownloadProgressChanged;
                try
                {
                    dl.DownloadFileTaskAsync(setDownload, setFolder);
                }
                catch (Exception ek)
                {
                    MessageBox.Show($"Failed downloading Data: " + ek.Message, "Oh Snap!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show($"Error Download", "Oh Snap!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void Dl_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            btDownload.Invoke(delegate
            {
                btDownload.Enabled = true;
            });
            GetNumDownload.Invoke(delegate
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
            if (dl == null || dl.IsCancelled) return;
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
            else
            {
                DLBar.Invoke((Action)delegate
                {
                    DLBar.Value = (int)e.ProgressPercentage;
                });
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
            Logger.Info("Download", $"Starting download - URL: {setDownload}, Destination: {setFolder}");
        }

        private async void BTCancel_Click(object sender, EventArgs e)
        {
            if (dl != null)
            {
                // dl.CancelAsync();
                // dl.Dispose();
                try
                {
                    btCancel.Enabled = false;
                    btDownload.Enabled = false;
                    GetNumDownload.Text = "Canceling...";

                    dl.CancelAsync();
                    await Task.Delay(1000); // give some time to cancel

                    dl.Dispose();
                    dl = null;

                    GetNumDownload.Text = "Canceled";
                    DLBar.Value = 0;
                }
                catch (Exception ex)
                {
                    Logger.Error("Download", $"Error during canceling download: {ex.Message}");
                    MessageBox.Show($"Error during canceling download: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btCancel.Enabled = true;
                    btDownload.Enabled = true;
                }
            }
            DialogResult = DialogResult.Cancel;
        }

        private void Download_Load(object sender, EventArgs e)
        {
            btDownload.PerformClick();
        }
    }
}
