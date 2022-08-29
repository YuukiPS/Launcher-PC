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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Yuuki",
            "tes.yuuki.me",
            "900",
            "6.9"}, -1);
            this.btStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GetHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetPort = new System.Windows.Forms.TextBox();
            this.CheckProxyUseHTTPS = new System.Windows.Forms.CheckBox();
            this.TabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stIsRunProxy = new System.Windows.Forms.Label();
            this.CheckProxyEnable = new System.Windows.Forms.CheckBox();
            this.ServerList = new System.Windows.Forms.ListView();
            this.ServerList_GetName = new System.Windows.Forms.ColumnHeader();
            this.ServerList_GetHost = new System.Windows.Forms.ColumnHeader();
            this.ServerList_GetOnline = new System.Windows.Forms.ColumnHeader();
            this.ServerList_GetVersion = new System.Windows.Forms.ColumnHeader();
            this.btReloadServer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btStartServer = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.TabConfig = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btSetInputMetadata = new System.Windows.Forms.Button();
            this.SetInputMetadata = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.Set_Version = new System.Windows.Forms.Label();
            this.linkDiscord = new System.Windows.Forms.LinkLabel();
            this.linkGithub = new System.Windows.Forms.LinkLabel();
            this.linkWeb = new System.Windows.Forms.LinkLabel();
            this.TabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.TabConfig.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btStart.Location = new System.Drawing.Point(6, 80);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(228, 47);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Launch";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Address:";
            // 
            // GetHost
            // 
            this.GetHost.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetHost.Location = new System.Drawing.Point(6, 39);
            this.GetHost.Name = "GetHost";
            this.GetHost.Size = new System.Drawing.Size(228, 35);
            this.GetHost.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(82, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // GetPort
            // 
            this.GetPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetPort.Location = new System.Drawing.Point(121, 12);
            this.GetPort.Name = "GetPort";
            this.GetPort.Size = new System.Drawing.Size(51, 25);
            this.GetPort.TabIndex = 5;
            this.GetPort.Text = "2242";
            // 
            // CheckProxyUseHTTPS
            // 
            this.CheckProxyUseHTTPS.AutoSize = true;
            this.CheckProxyUseHTTPS.Checked = true;
            this.CheckProxyUseHTTPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckProxyUseHTTPS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckProxyUseHTTPS.Location = new System.Drawing.Point(16, 19);
            this.CheckProxyUseHTTPS.Name = "CheckProxyUseHTTPS";
            this.CheckProxyUseHTTPS.Size = new System.Drawing.Size(60, 19);
            this.CheckProxyUseHTTPS.TabIndex = 6;
            this.CheckProxyUseHTTPS.Text = "HTTPS";
            this.CheckProxyUseHTTPS.UseVisualStyleBackColor = true;
            // 
            // TabMain
            // 
            this.TabMain.Controls.Add(this.tabPage1);
            this.TabMain.Controls.Add(this.tabPage2);
            this.TabMain.Controls.Add(this.tabPage3);
            this.TabMain.Controls.Add(this.tabPage4);
            this.TabMain.Controls.Add(this.tabPage8);
            this.TabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabMain.Location = new System.Drawing.Point(0, 0);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedIndex = 0;
            this.TabMain.Size = new System.Drawing.Size(579, 309);
            this.TabMain.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.ServerList);
            this.tabPage1.Controls.Add(this.btReloadServer);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.GetHost);
            this.tabPage1.Controls.Add(this.btStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connect";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stIsRunProxy);
            this.groupBox1.Controls.Add(this.CheckProxyEnable);
            this.groupBox1.Controls.Add(this.CheckProxyUseHTTPS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.GetPort);
            this.groupBox1.Location = new System.Drawing.Point(6, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 67);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxy";
            // 
            // stIsRunProxy
            // 
            this.stIsRunProxy.AutoSize = true;
            this.stIsRunProxy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.stIsRunProxy.Location = new System.Drawing.Point(83, 40);
            this.stIsRunProxy.Name = "stIsRunProxy";
            this.stIsRunProxy.Size = new System.Drawing.Size(87, 21);
            this.stIsRunProxy.TabIndex = 8;
            this.stIsRunProxy.Text = "Status: OFF";
            // 
            // CheckProxyEnable
            // 
            this.CheckProxyEnable.AutoSize = true;
            this.CheckProxyEnable.Checked = true;
            this.CheckProxyEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckProxyEnable.Location = new System.Drawing.Point(16, 40);
            this.CheckProxyEnable.Name = "CheckProxyEnable";
            this.CheckProxyEnable.Size = new System.Drawing.Size(61, 19);
            this.CheckProxyEnable.TabIndex = 7;
            this.CheckProxyEnable.Text = "Enable";
            this.CheckProxyEnable.UseVisualStyleBackColor = true;
            // 
            // ServerList
            // 
            this.ServerList.AllowDrop = true;
            this.ServerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ServerList_GetName,
            this.ServerList_GetHost,
            this.ServerList_GetOnline,
            this.ServerList_GetVersion});
            this.ServerList.FullRowSelect = true;
            this.ServerList.GridLines = true;
            this.ServerList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.ServerList.Location = new System.Drawing.Point(243, 36);
            this.ServerList.MultiSelect = false;
            this.ServerList.Name = "ServerList";
            this.ServerList.ShowGroups = false;
            this.ServerList.Size = new System.Drawing.Size(322, 239);
            this.ServerList.TabIndex = 8;
            this.ServerList.UseCompatibleStateImageBehavior = false;
            this.ServerList.View = System.Windows.Forms.View.Details;
            this.ServerList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ServerList_MouseDoubleClick);
            // 
            // ServerList_GetName
            // 
            this.ServerList_GetName.Text = "Name";
            // 
            // ServerList_GetHost
            // 
            this.ServerList_GetHost.Text = "Host";
            this.ServerList_GetHost.Width = 120;
            // 
            // ServerList_GetOnline
            // 
            this.ServerList_GetOnline.Text = "Online";
            // 
            // ServerList_GetVersion
            // 
            this.ServerList_GetVersion.Text = "Version";
            // 
            // btReloadServer
            // 
            this.btReloadServer.Location = new System.Drawing.Point(490, 11);
            this.btReloadServer.Name = "btReloadServer";
            this.btReloadServer.Size = new System.Drawing.Size(75, 23);
            this.btReloadServer.TabIndex = 10;
            this.btReloadServer.Text = "Reload";
            this.btReloadServer.UseVisualStyleBackColor = true;
            this.btReloadServer.Click += new System.EventHandler(this.btReloadServer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(243, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Server list";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btStartServer);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Server";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btStartServer
            // 
            this.btStartServer.Location = new System.Drawing.Point(6, 6);
            this.btStartServer.Name = "btStartServer";
            this.btStartServer.Size = new System.Drawing.Size(75, 23);
            this.btStartServer.TabIndex = 0;
            this.btStartServer.Text = "Start";
            this.btStartServer.UseVisualStyleBackColor = true;
            this.btStartServer.Click += new System.EventHandler(this.btStartServer_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(571, 281);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Command";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.TabConfig);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(571, 281);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Config";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // TabConfig
            // 
            this.TabConfig.Controls.Add(this.tabPage5);
            this.TabConfig.Controls.Add(this.tabPage6);
            this.TabConfig.Controls.Add(this.tabPage7);
            this.TabConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabConfig.Location = new System.Drawing.Point(0, 0);
            this.TabConfig.Name = "TabConfig";
            this.TabConfig.SelectedIndex = 0;
            this.TabConfig.Size = new System.Drawing.Size(571, 281);
            this.TabConfig.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(563, 253);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Launcher";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btSetInputMetadata);
            this.tabPage6.Controls.Add(this.SetInputMetadata);
            this.tabPage6.Controls.Add(this.label4);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(563, 253);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Metadata";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btSetInputMetadata
            // 
            this.btSetInputMetadata.Location = new System.Drawing.Point(8, 60);
            this.btSetInputMetadata.Name = "btSetInputMetadata";
            this.btSetInputMetadata.Size = new System.Drawing.Size(75, 23);
            this.btSetInputMetadata.TabIndex = 5;
            this.btSetInputMetadata.Text = "Select file";
            this.btSetInputMetadata.UseVisualStyleBackColor = true;
            // 
            // SetInputMetadata
            // 
            this.SetInputMetadata.Location = new System.Drawing.Point(8, 31);
            this.SetInputMetadata.Name = "SetInputMetadata";
            this.SetInputMetadata.Size = new System.Drawing.Size(546, 23);
            this.SetInputMetadata.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Input file:";
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(563, 253);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "UserAssembly";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 24);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(571, 281);
            this.tabPage8.TabIndex = 4;
            this.tabPage8.Text = "Account";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // Set_Version
            // 
            this.Set_Version.AutoSize = true;
            this.Set_Version.Dock = System.Windows.Forms.DockStyle.Left;
            this.Set_Version.Location = new System.Drawing.Point(0, 309);
            this.Set_Version.Name = "Set_Version";
            this.Set_Version.Size = new System.Drawing.Size(75, 15);
            this.Set_Version.TabIndex = 12;
            this.Set_Version.Text = "Version: 0.0.0";
            // 
            // linkDiscord
            // 
            this.linkDiscord.AutoSize = true;
            this.linkDiscord.Location = new System.Drawing.Point(528, 308);
            this.linkDiscord.Name = "linkDiscord";
            this.linkDiscord.Size = new System.Drawing.Size(47, 15);
            this.linkDiscord.TabIndex = 13;
            this.linkDiscord.TabStop = true;
            this.linkDiscord.Text = "Discord";
            this.linkDiscord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDiscord_LinkClicked);
            // 
            // linkGithub
            // 
            this.linkGithub.AutoSize = true;
            this.linkGithub.Location = new System.Drawing.Point(479, 308);
            this.linkGithub.Name = "linkGithub";
            this.linkGithub.Size = new System.Drawing.Size(43, 15);
            this.linkGithub.TabIndex = 14;
            this.linkGithub.TabStop = true;
            this.linkGithub.Text = "Github";
            this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
            // 
            // linkWeb
            // 
            this.linkWeb.AutoSize = true;
            this.linkWeb.Location = new System.Drawing.Point(442, 309);
            this.linkWeb.Name = "linkWeb";
            this.linkWeb.Size = new System.Drawing.Size(31, 15);
            this.linkWeb.TabIndex = 15;
            this.linkWeb.TabStop = true;
            this.linkWeb.Text = "Web";
            this.linkWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkWeb_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 328);
            this.Controls.Add(this.linkWeb);
            this.Controls.Add(this.linkGithub);
            this.Controls.Add(this.linkDiscord);
            this.Controls.Add(this.Set_Version);
            this.Controls.Add(this.TabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "YuukiPS Launcher";
            this.Load += new System.EventHandler(this.Main_Load);
            this.TabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.TabConfig.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btStart;
        private Label label1;
        private TextBox GetHost;
        private Label label2;
        private TextBox GetPort;
        private CheckBox CheckProxyUseHTTPS;
        private TabControl TabMain;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btStartServer;
        private Label label3;
        private ColumnHeader ServerList_GetName;
        private ColumnHeader ServerList_GetHost;
        private ColumnHeader ServerList_GetOnline;
        private ColumnHeader ServerList_GetVersion;
        private TabPage tabPage3;
        private TabPage tabPage4;
        public ListView ServerList;
        private Button btReloadServer;
        private GroupBox groupBox1;
        private CheckBox CheckProxyEnable;
        private Label Set_Version;
        private Label stIsRunProxy;
        private LinkLabel linkDiscord;
        private LinkLabel linkGithub;
        private LinkLabel linkWeb;
        private TabControl TabConfig;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private Button btSetInputMetadata;
        private TextBox SetInputMetadata;
        private Label label4;
        private TabPage tabPage7;
        private TabPage tabPage8;
    }
}