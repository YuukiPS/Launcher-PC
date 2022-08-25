namespace YuukiPS_Launcher
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GetHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetPort = new System.Windows.Forms.TextBox();
            this.UseHTTPS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btStart.Location = new System.Drawing.Point(21, 109);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(115, 47);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Launch";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Address:";
            // 
            // GetHost
            // 
            this.GetHost.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetHost.Location = new System.Drawing.Point(21, 60);
            this.GetHost.Name = "GetHost";
            this.GetHost.Size = new System.Drawing.Size(281, 43);
            this.GetHost.TabIndex = 2;
            this.GetHost.Text = "eu.genshin.ps.yuuki.me";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(21, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port Proxy:";
            // 
            // GetPort
            // 
            this.GetPort.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPort.Location = new System.Drawing.Point(122, 159);
            this.GetPort.Name = "GetPort";
            this.GetPort.Size = new System.Drawing.Size(53, 35);
            this.GetPort.TabIndex = 5;
            this.GetPort.Text = "8184";
            // 
            // UseHTTPS
            // 
            this.UseHTTPS.AutoSize = true;
            this.UseHTTPS.Checked = true;
            this.UseHTTPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseHTTPS.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UseHTTPS.Location = new System.Drawing.Point(234, 157);
            this.UseHTTPS.Name = "UseHTTPS";
            this.UseHTTPS.Size = new System.Drawing.Size(92, 34);
            this.UseHTTPS.TabIndex = 6;
            this.UseHTTPS.Text = "HTTPS";
            this.UseHTTPS.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 198);
            this.Controls.Add(this.UseHTTPS);
            this.Controls.Add(this.GetPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GetHost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "YuukiPS Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btStart;
        private Label label1;
        private TextBox GetHost;
        private Label label2;
        private TextBox GetPort;
        private CheckBox UseHTTPS;
    }
}