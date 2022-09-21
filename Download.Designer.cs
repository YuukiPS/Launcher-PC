namespace YuukiPS_Launcher
{
    partial class Download
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DLBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.GetNameDownload = new System.Windows.Forms.Label();
            this.GetNumDownload = new System.Windows.Forms.Label();
            this.btDownload = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DLBar
            // 
            this.DLBar.Location = new System.Drawing.Point(12, 12);
            this.DLBar.Name = "DLBar";
            this.DLBar.Size = new System.Drawing.Size(532, 28);
            this.DLBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Download: ";
            // 
            // GetNameDownload
            // 
            this.GetNameDownload.AutoSize = true;
            this.GetNameDownload.Location = new System.Drawing.Point(75, 43);
            this.GetNameDownload.Name = "GetNameDownload";
            this.GetNameDownload.Size = new System.Drawing.Size(19, 15);
            this.GetNameDownload.TabIndex = 2;
            this.GetNameDownload.Text = "....";
            // 
            // GetNumDownload
            // 
            this.GetNumDownload.AutoSize = true;
            this.GetNumDownload.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetNumDownload.Location = new System.Drawing.Point(12, 87);
            this.GetNumDownload.Name = "GetNumDownload";
            this.GetNumDownload.Size = new System.Drawing.Size(16, 20);
            this.GetNumDownload.TabIndex = 3;
            this.GetNumDownload.Text = "?";
            // 
            // btDownload
            // 
            this.btDownload.Location = new System.Drawing.Point(12, 61);
            this.btDownload.Name = "btDownload";
            this.btDownload.Size = new System.Drawing.Size(75, 23);
            this.btDownload.TabIndex = 4;
            this.btDownload.Text = "Start";
            this.btDownload.UseVisualStyleBackColor = true;
            this.btDownload.Click += new System.EventHandler(this.btDownload_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(93, 61);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 111);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btDownload);
            this.Controls.Add(this.GetNumDownload);
            this.Controls.Add(this.GetNameDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DLBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Download";
            this.Text = "Download";
            this.Load += new System.EventHandler(this.Download_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar DLBar;
        private Label label1;
        private Label GetNameDownload;
        private Label GetNumDownload;
        private Button btDownload;
        private Button btCancel;
    }
}