using System.ComponentModel;
using System.Globalization;
using System.Net;

namespace YuukiPS_Launcher
{
    public partial class Download : Form
    {
        private string set_download;
        private string set_folder;
        private WebClient dl;

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
                dl = new WebClient();
                dl.DownloadFileCompleted += DLDone;
                dl.DownloadProgressChanged += DLProgress;
                try
                {
                    Console.WriteLine("Start Download: " + set_download);
                    dl.DownloadFileAsync(new Uri(set_download), set_folder);
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

        private void DLProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;
            DLBar.Value = int.Parse(Math.Truncate(percentage).ToString(CultureInfo.InvariantCulture));

            GetNumDownload.Text = $@"Update {Tool.SizeSuffix(e.BytesReceived)} of {Tool.SizeSuffix(e.TotalBytesToReceive)}";
        }

        private void DLDone(object? sender, AsyncCompletedEventArgs e)
        {
            btDownload.Enabled = true;
            GetNumDownload.Text = "Done";
            // TODO: check vaild file
            DialogResult = DialogResult.OK;
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
