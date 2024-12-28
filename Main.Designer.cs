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
            components = new System.ComponentModel.Container();
            btStartNormal = new Button();
            GetServerHost = new TextBox();
            label2 = new Label();
            GetProxyPort = new TextBox();
            grLog = new GroupBox();
            EnableShowLog = new CheckBox();
            EnableSendLog = new CheckBox();
            grConfigGameLite = new GroupBox();
            Set_LA_Select = new Button();
            Set_LA_GameFolder = new TextBox();
            label5 = new Label();
            stIsRunProxy = new Label();
            groupBox8 = new GroupBox();
            btStartYuukiServer = new Button();
            GetTypeGame = new ComboBox();
            btStartOfficialServer = new Button();
            Enable_WipeLoginCache = new CheckBox();
            grProfile = new GroupBox();
            GetProfileServer = new ComboBox();
            btload = new Button();
            btsave = new Button();
            label8 = new Label();
            grExtra = new GroupBox();
            ExtraCheat = new CheckBox();
            Enable_RPC = new CheckBox();
            grProxy = new GroupBox();
            CheckProxyEnable = new CheckBox();
            groupBox3 = new GroupBox();
            Get_LA_Version = new Label();
            Get_LA_MD5 = new Label();
            Get_LA_CH = new Label();
            Get_LA_REL = new Label();
            SetVersion = new Label();
            Server_Config_OpenFolder = new Button();
            linkDiscord = new LinkLabel();
            linkGithub = new LinkLabel();
            linkWeb = new LinkLabel();
            CheckGameRun = new System.Windows.Forms.Timer(components);
            CheckProxyRun = new System.Windows.Forms.Timer(components);
            grLog.SuspendLayout();
            grConfigGameLite.SuspendLayout();
            groupBox8.SuspendLayout();
            grProfile.SuspendLayout();
            grExtra.SuspendLayout();
            grProxy.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // btStartNormal
            // 
            btStartNormal.BackColor = Color.FromArgb(52, 152, 219);
            btStartNormal.Cursor = Cursors.Hand;
            btStartNormal.FlatAppearance.BorderSize = 0;
            btStartNormal.FlatStyle = FlatStyle.Flat;
            btStartNormal.Font = new Font("Segoe UI", 12F);
            btStartNormal.ForeColor = Color.White;
            btStartNormal.Location = new Point(10, 64);
            btStartNormal.Name = "btStartNormal";
            btStartNormal.Size = new Size(123, 36);
            btStartNormal.TabIndex = 0;
            btStartNormal.Text = "Launch";
            btStartNormal.UseVisualStyleBackColor = false;
            btStartNormal.Click += BTStartNormal_Click;
            // 
            // GetServerHost
            // 
            GetServerHost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GetServerHost.BackColor = Color.FromArgb(240, 240, 240);
            GetServerHost.BorderStyle = BorderStyle.FixedSingle;
            GetServerHost.Font = new Font("Segoe UI", 12F);
            GetServerHost.Location = new Point(10, 28);
            GetServerHost.Name = "GetServerHost";
            GetServerHost.PlaceholderText = "https://ps.yuuki.me";
            GetServerHost.ScrollBars = ScrollBars.Horizontal;
            GetServerHost.Size = new Size(210, 29);
            GetServerHost.TabIndex = 2;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.FromArgb(60, 60, 60);
            label2.Location = new Point(250, 22);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 4;
            label2.Text = "Port:";
            // 
            // GetProxyPort
            // 
            GetProxyPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GetProxyPort.BackColor = Color.FromArgb(240, 240, 240);
            GetProxyPort.BorderStyle = BorderStyle.None;
            GetProxyPort.Font = new Font("Segoe UI", 11F);
            GetProxyPort.Location = new Point(295, 22);
            GetProxyPort.Name = "GetProxyPort";
            GetProxyPort.Size = new Size(55, 20);
            GetProxyPort.TabIndex = 5;
            GetProxyPort.Text = "2242";
            // 
            // grLog
            // 
            grLog.Anchor = AnchorStyles.None;
            grLog.Controls.Add(EnableShowLog);
            grLog.Controls.Add(EnableSendLog);
            grLog.Location = new Point(427, 143);
            grLog.Name = "grLog";
            grLog.Size = new Size(133, 51);
            grLog.TabIndex = 20;
            grLog.TabStop = false;
            grLog.Text = "Log";
            // 
            // EnableShowLog
            // 
            EnableShowLog.AutoSize = true;
            EnableShowLog.Dock = DockStyle.Right;
            EnableShowLog.Location = new Point(75, 19);
            EnableShowLog.Name = "EnableShowLog";
            EnableShowLog.Size = new Size(55, 29);
            EnableShowLog.TabIndex = 4;
            EnableShowLog.Text = "Show";
            EnableShowLog.UseVisualStyleBackColor = true;
            // 
            // EnableSendLog
            // 
            EnableSendLog.AutoSize = true;
            EnableSendLog.Checked = true;
            EnableSendLog.CheckState = CheckState.Checked;
            EnableSendLog.Cursor = Cursors.Hand;
            EnableSendLog.Dock = DockStyle.Left;
            EnableSendLog.ForeColor = Color.FromArgb(60, 60, 60);
            EnableSendLog.Location = new Point(3, 19);
            EnableSendLog.Name = "EnableSendLog";
            EnableSendLog.Size = new Size(52, 29);
            EnableSendLog.TabIndex = 3;
            EnableSendLog.Text = "Send";
            EnableSendLog.UseVisualStyleBackColor = true;
            // 
            // grConfigGameLite
            // 
            grConfigGameLite.Anchor = AnchorStyles.None;
            grConfigGameLite.Controls.Add(Set_LA_Select);
            grConfigGameLite.Controls.Add(Set_LA_GameFolder);
            grConfigGameLite.Controls.Add(label5);
            grConfigGameLite.Font = new Font("Segoe UI", 11F);
            grConfigGameLite.ForeColor = Color.FromArgb(60, 60, 60);
            grConfigGameLite.Location = new Point(4, 199);
            grConfigGameLite.MaximumSize = new Size(672, 128);
            grConfigGameLite.Name = "grConfigGameLite";
            grConfigGameLite.Size = new Size(417, 60);
            grConfigGameLite.TabIndex = 19;
            grConfigGameLite.TabStop = false;
            grConfigGameLite.Text = "Game Config";
            // 
            // Set_LA_Select
            // 
            Set_LA_Select.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Set_LA_Select.BackColor = Color.FromArgb(46, 204, 113);
            Set_LA_Select.Cursor = Cursors.Hand;
            Set_LA_Select.FlatAppearance.BorderSize = 0;
            Set_LA_Select.FlatStyle = FlatStyle.Flat;
            Set_LA_Select.ForeColor = Color.White;
            Set_LA_Select.Location = new Point(336, 18);
            Set_LA_Select.Name = "Set_LA_Select";
            Set_LA_Select.Size = new Size(75, 33);
            Set_LA_Select.TabIndex = 9;
            Set_LA_Select.Text = "Choose";
            Set_LA_Select.UseVisualStyleBackColor = false;
            Set_LA_Select.Click += Set_LA_Select_Click;
            // 
            // Set_LA_GameFolder
            // 
            Set_LA_GameFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Set_LA_GameFolder.BackColor = Color.FromArgb(240, 240, 240);
            Set_LA_GameFolder.BorderStyle = BorderStyle.FixedSingle;
            Set_LA_GameFolder.Location = new Point(113, 22);
            Set_LA_GameFolder.Name = "Set_LA_GameFolder";
            Set_LA_GameFolder.ReadOnly = true;
            Set_LA_GameFolder.Size = new Size(209, 27);
            Set_LA_GameFolder.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.ForeColor = Color.FromArgb(60, 60, 60);
            label5.Location = new Point(10, 24);
            label5.Name = "label5";
            label5.Size = new Size(97, 20);
            label5.TabIndex = 0;
            label5.Text = "Game Folder:";
            // 
            // stIsRunProxy
            // 
            stIsRunProxy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            stIsRunProxy.AutoSize = true;
            stIsRunProxy.Font = new Font("Segoe UI", 11F);
            stIsRunProxy.ForeColor = Color.FromArgb(60, 60, 60);
            stIsRunProxy.Location = new Point(3, 52);
            stIsRunProxy.Name = "stIsRunProxy";
            stIsRunProxy.Size = new Size(81, 20);
            stIsRunProxy.TabIndex = 8;
            stIsRunProxy.Text = "Status: OFF";
            stIsRunProxy.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox8
            // 
            groupBox8.Anchor = AnchorStyles.None;
            groupBox8.BackColor = Color.Transparent;
            groupBox8.Controls.Add(btStartYuukiServer);
            groupBox8.Controls.Add(GetServerHost);
            groupBox8.Controls.Add(btStartNormal);
            groupBox8.Controls.Add(GetTypeGame);
            groupBox8.Controls.Add(btStartOfficialServer);
            groupBox8.Font = new Font("Segoe UI", 12F);
            groupBox8.ForeColor = Color.FromArgb(60, 60, 60);
            groupBox8.Location = new Point(4, 2);
            groupBox8.MaximumSize = new Size(672, 128);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(417, 110);
            groupBox8.TabIndex = 18;
            groupBox8.TabStop = false;
            groupBox8.Text = "Connect to server";
            // 
            // btStartYuukiServer
            // 
            btStartYuukiServer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btStartYuukiServer.AutoSize = true;
            btStartYuukiServer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btStartYuukiServer.BackColor = Color.FromArgb(52, 152, 219);
            btStartYuukiServer.Cursor = Cursors.Hand;
            btStartYuukiServer.FlatAppearance.BorderSize = 0;
            btStartYuukiServer.FlatStyle = FlatStyle.Flat;
            btStartYuukiServer.Font = new Font("Segoe UI", 12F);
            btStartYuukiServer.ForeColor = Color.White;
            btStartYuukiServer.Location = new Point(334, 26);
            btStartYuukiServer.Name = "btStartYuukiServer";
            btStartYuukiServer.Size = new Size(77, 31);
            btStartYuukiServer.TabIndex = 20;
            btStartYuukiServer.Text = "YuukiPS";
            btStartYuukiServer.UseVisualStyleBackColor = false;
            btStartYuukiServer.Click += BTStartYuukiServer_Click;
            // 
            // GetTypeGame
            // 
            GetTypeGame.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GetTypeGame.Font = new Font("Segoe UI", 12F);
            GetTypeGame.FormattingEnabled = true;
            GetTypeGame.ImeMode = ImeMode.NoControl;
            GetTypeGame.Location = new Point(151, 69);
            GetTypeGame.Name = "GetTypeGame";
            GetTypeGame.Size = new Size(260, 29);
            GetTypeGame.TabIndex = 14;
            GetTypeGame.SelectedIndexChanged += GetTypeGame_SelectedIndexChanged;
            // 
            // btStartOfficialServer
            // 
            btStartOfficialServer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btStartOfficialServer.AutoSize = true;
            btStartOfficialServer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btStartOfficialServer.BackColor = Color.FromArgb(46, 204, 113);
            btStartOfficialServer.Cursor = Cursors.Hand;
            btStartOfficialServer.FlatAppearance.BorderSize = 0;
            btStartOfficialServer.FlatStyle = FlatStyle.Flat;
            btStartOfficialServer.Font = new Font("Segoe UI", 12F);
            btStartOfficialServer.ForeColor = Color.White;
            btStartOfficialServer.Location = new Point(259, 26);
            btStartOfficialServer.Name = "btStartOfficialServer";
            btStartOfficialServer.Size = new Size(69, 31);
            btStartOfficialServer.TabIndex = 13;
            btStartOfficialServer.Text = "Official";
            btStartOfficialServer.UseVisualStyleBackColor = false;
            btStartOfficialServer.Click += BTStartOfficialServer_Click;
            // 
            // Enable_WipeLoginCache
            // 
            Enable_WipeLoginCache.AutoSize = true;
            Enable_WipeLoginCache.Cursor = Cursors.Hand;
            Enable_WipeLoginCache.ForeColor = Color.FromArgb(60, 60, 60);
            Enable_WipeLoginCache.Location = new Point(70, 47);
            Enable_WipeLoginCache.Name = "Enable_WipeLoginCache";
            Enable_WipeLoginCache.Size = new Size(104, 24);
            Enable_WipeLoginCache.TabIndex = 20;
            Enable_WipeLoginCache.Text = "Wipe Login";
            Enable_WipeLoginCache.UseVisualStyleBackColor = true;
            // 
            // grProfile
            // 
            grProfile.Anchor = AnchorStyles.None;
            grProfile.BackColor = Color.FromArgb(245, 245, 245);
            grProfile.Controls.Add(GetProfileServer);
            grProfile.Controls.Add(btload);
            grProfile.Controls.Add(btsave);
            grProfile.Controls.Add(label8);
            grProfile.Font = new Font("Segoe UI", 11F);
            grProfile.ForeColor = Color.FromArgb(60, 60, 60);
            grProfile.Location = new Point(4, 108);
            grProfile.MaximumSize = new Size(672, 128);
            grProfile.Name = "grProfile";
            grProfile.Size = new Size(417, 96);
            grProfile.TabIndex = 17;
            grProfile.TabStop = false;
            grProfile.Text = "Profile";
            // 
            // GetProfileServer
            // 
            GetProfileServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GetProfileServer.Font = new Font("Segoe UI", 12F);
            GetProfileServer.FormattingEnabled = true;
            GetProfileServer.Location = new Point(14, 33);
            GetProfileServer.Name = "GetProfileServer";
            GetProfileServer.Size = new Size(194, 29);
            GetProfileServer.TabIndex = 15;
            GetProfileServer.SelectedIndexChanged += GetProfileServer_SelectedIndexChanged;
            // 
            // btload
            // 
            btload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btload.BackColor = Color.FromArgb(52, 152, 219);
            btload.Cursor = Cursors.Hand;
            btload.FlatAppearance.BorderSize = 0;
            btload.FlatStyle = FlatStyle.Flat;
            btload.Font = new Font("Segoe UI", 12F);
            btload.ForeColor = Color.White;
            btload.Location = new Point(336, 28);
            btload.Name = "btload";
            btload.Size = new Size(75, 34);
            btload.TabIndex = 16;
            btload.Text = "Load";
            btload.UseVisualStyleBackColor = false;
            btload.Click += BTLoadClick;
            // 
            // btsave
            // 
            btsave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btsave.BackColor = Color.FromArgb(46, 204, 113);
            btsave.Cursor = Cursors.Hand;
            btsave.FlatAppearance.BorderSize = 0;
            btsave.FlatStyle = FlatStyle.Flat;
            btsave.Font = new Font("Segoe UI", 12F);
            btsave.ForeColor = Color.White;
            btsave.Location = new Point(250, 28);
            btsave.Name = "btsave";
            btsave.Size = new Size(80, 34);
            btsave.TabIndex = 2;
            btsave.Text = "Save";
            btsave.UseVisualStyleBackColor = false;
            btsave.Click += SetLASaveClick;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.ForeColor = Color.FromArgb(60, 60, 60);
            label8.Location = new Point(146, 69);
            label8.Name = "label8";
            label8.Size = new Size(269, 19);
            label8.TabIndex = 13;
            label8.Text = "Click 'Save' to store current profile settings";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grExtra
            // 
            grExtra.Anchor = AnchorStyles.None;
            grExtra.BackColor = SystemColors.ButtonFace;
            grExtra.Controls.Add(Enable_WipeLoginCache);
            grExtra.Controls.Add(ExtraCheat);
            grExtra.Controls.Add(Enable_RPC);
            grExtra.Font = new Font("Segoe UI", 11F);
            grExtra.ForeColor = Color.FromArgb(60, 60, 60);
            grExtra.Location = new Point(4, 260);
            grExtra.Name = "grExtra";
            grExtra.Size = new Size(217, 76);
            grExtra.TabIndex = 12;
            grExtra.TabStop = false;
            grExtra.Text = "Extra";
            // 
            // ExtraCheat
            // 
            ExtraCheat.AutoSize = true;
            ExtraCheat.Cursor = Cursors.Hand;
            ExtraCheat.Dock = DockStyle.Top;
            ExtraCheat.ForeColor = Color.FromArgb(60, 60, 60);
            ExtraCheat.Location = new Point(3, 47);
            ExtraCheat.Name = "ExtraCheat";
            ExtraCheat.Size = new Size(211, 24);
            ExtraCheat.TabIndex = 0;
            ExtraCheat.Text = "Cheat";
            ExtraCheat.UseVisualStyleBackColor = true;
            // 
            // Enable_RPC
            // 
            Enable_RPC.AutoSize = true;
            Enable_RPC.Cursor = Cursors.Hand;
            Enable_RPC.Dock = DockStyle.Top;
            Enable_RPC.ForeColor = Color.FromArgb(60, 60, 60);
            Enable_RPC.Location = new Point(3, 23);
            Enable_RPC.Name = "Enable_RPC";
            Enable_RPC.Size = new Size(211, 24);
            Enable_RPC.TabIndex = 1;
            Enable_RPC.Text = "Rich Presence (Discord)";
            Enable_RPC.UseVisualStyleBackColor = true;
            Enable_RPC.CheckedChanged += ExtraEnableRPCCheckedChanged;
            // 
            // grProxy
            // 
            grProxy.Anchor = AnchorStyles.None;
            grProxy.BackColor = Color.FromArgb(240, 240, 240);
            grProxy.Controls.Add(stIsRunProxy);
            grProxy.Controls.Add(GetProxyPort);
            grProxy.Controls.Add(CheckProxyEnable);
            grProxy.Controls.Add(label2);
            grProxy.Font = new Font("Segoe UI", 10F);
            grProxy.ForeColor = Color.FromArgb(60, 60, 60);
            grProxy.Location = new Point(224, 260);
            grProxy.Name = "grProxy";
            grProxy.Size = new Size(197, 81);
            grProxy.TabIndex = 11;
            grProxy.TabStop = false;
            grProxy.Text = "Proxy";
            // 
            // CheckProxyEnable
            // 
            CheckProxyEnable.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CheckProxyEnable.AutoSize = true;
            CheckProxyEnable.Checked = true;
            CheckProxyEnable.CheckState = CheckState.Checked;
            CheckProxyEnable.Cursor = Cursors.Hand;
            CheckProxyEnable.ForeColor = Color.FromArgb(60, 60, 60);
            CheckProxyEnable.Location = new Point(3, 26);
            CheckProxyEnable.Name = "CheckProxyEnable";
            CheckProxyEnable.Size = new Size(68, 23);
            CheckProxyEnable.TabIndex = 7;
            CheckProxyEnable.Text = "Enable";
            CheckProxyEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.None;
            groupBox3.BackColor = Color.FromArgb(240, 240, 240);
            groupBox3.Controls.Add(Get_LA_Version);
            groupBox3.Controls.Add(Get_LA_MD5);
            groupBox3.Controls.Add(Get_LA_CH);
            groupBox3.Controls.Add(Get_LA_REL);
            groupBox3.Font = new Font("Segoe UI", 10F);
            groupBox3.ForeColor = Color.FromArgb(60, 60, 60);
            groupBox3.Location = new Point(427, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(1, 1, 10, 10);
            groupBox3.Size = new Size(211, 135);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Game Info";
            // 
            // Get_LA_Version
            // 
            Get_LA_Version.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Get_LA_Version.AutoSize = true;
            Get_LA_Version.Font = new Font("Segoe UI", 10F);
            Get_LA_Version.ForeColor = Color.FromArgb(60, 60, 60);
            Get_LA_Version.Location = new Point(8, 24);
            Get_LA_Version.Name = "Get_LA_Version";
            Get_LA_Version.Size = new Size(120, 19);
            Get_LA_Version.TabIndex = 3;
            Get_LA_Version.Text = "Version: Unknown";
            Get_LA_Version.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Get_LA_MD5
            // 
            Get_LA_MD5.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Get_LA_MD5.AutoSize = true;
            Get_LA_MD5.Cursor = Cursors.Hand;
            Get_LA_MD5.Font = new Font("Segoe UI", 10F);
            Get_LA_MD5.ForeColor = Color.FromArgb(60, 60, 60);
            Get_LA_MD5.Location = new Point(8, 104);
            Get_LA_MD5.Name = "Get_LA_MD5";
            Get_LA_MD5.Size = new Size(106, 19);
            Get_LA_MD5.TabIndex = 7;
            Get_LA_MD5.Text = "MD5: Unknown";
            Get_LA_MD5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Get_LA_CH
            // 
            Get_LA_CH.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Get_LA_CH.AutoSize = true;
            Get_LA_CH.Font = new Font("Segoe UI", 10F);
            Get_LA_CH.ForeColor = Color.FromArgb(60, 60, 60);
            Get_LA_CH.Location = new Point(8, 49);
            Get_LA_CH.Name = "Get_LA_CH";
            Get_LA_CH.Size = new Size(125, 19);
            Get_LA_CH.TabIndex = 4;
            Get_LA_CH.Text = "Channel: Unknown";
            Get_LA_CH.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Get_LA_REL
            // 
            Get_LA_REL.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Get_LA_REL.AutoSize = true;
            Get_LA_REL.Font = new Font("Segoe UI", 11F);
            Get_LA_REL.ForeColor = Color.FromArgb(50, 50, 50);
            Get_LA_REL.Location = new Point(8, 74);
            Get_LA_REL.Name = "Get_LA_REL";
            Get_LA_REL.Size = new Size(128, 20);
            Get_LA_REL.TabIndex = 5;
            Get_LA_REL.Text = "Release: Unknown";
            Get_LA_REL.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SetVersion
            // 
            SetVersion.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SetVersion.Location = new Point(3, 288);
            SetVersion.Name = "SetVersion";
            SetVersion.Size = new Size(221, 15);
            SetVersion.TabIndex = 12;
            SetVersion.Text = "Version: 0.0.0";
            // 
            // Server_Config_OpenFolder
            // 
            Server_Config_OpenFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Server_Config_OpenFolder.BackColor = Color.FromArgb(52, 152, 219);
            Server_Config_OpenFolder.Cursor = Cursors.Hand;
            Server_Config_OpenFolder.Enabled = false;
            Server_Config_OpenFolder.FlatStyle = FlatStyle.Flat;
            Server_Config_OpenFolder.Font = new Font("Segoe UI", 10F);
            Server_Config_OpenFolder.ForeColor = Color.White;
            Server_Config_OpenFolder.Location = new Point(10, 141);
            Server_Config_OpenFolder.Name = "Server_Config_OpenFolder";
            Server_Config_OpenFolder.Size = new Size(202, 35);
            Server_Config_OpenFolder.TabIndex = 1;
            Server_Config_OpenFolder.Text = "Open Server Folder";
            Server_Config_OpenFolder.UseVisualStyleBackColor = false;
            // 
            // linkDiscord
            // 
            linkDiscord.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkDiscord.AutoSize = true;
            linkDiscord.Location = new Point(93, 356);
            linkDiscord.Name = "linkDiscord";
            linkDiscord.Size = new Size(47, 15);
            linkDiscord.TabIndex = 13;
            linkDiscord.TabStop = true;
            linkDiscord.Text = "Discord";
            linkDiscord.LinkClicked += LinkDiscordLinkClicked;
            // 
            // linkGithub
            // 
            linkGithub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkGithub.AutoSize = true;
            linkGithub.Location = new Point(7, 356);
            linkGithub.Margin = new Padding(0, 0, 5, 0);
            linkGithub.Name = "linkGithub";
            linkGithub.Size = new Size(43, 15);
            linkGithub.TabIndex = 14;
            linkGithub.TabStop = true;
            linkGithub.Text = "Github";
            linkGithub.LinkClicked += LinkGithubLinkClicked;
            // 
            // linkWeb
            // 
            linkWeb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkWeb.Location = new Point(55, 356);
            linkWeb.Margin = new Padding(0, 0, 5, 0);
            linkWeb.Name = "linkWeb";
            linkWeb.Size = new Size(31, 15);
            linkWeb.TabIndex = 15;
            linkWeb.TabStop = true;
            linkWeb.Text = "Web";
            linkWeb.LinkClicked += LinkWebLinkClicked;
            // 
            // CheckGameRun
            // 
            CheckGameRun.Enabled = true;
            CheckGameRun.Interval = 1000;
            CheckGameRun.Tick += CheckGameRun_Tick;
            // 
            // CheckProxyRun
            // 
            CheckProxyRun.Enabled = true;
            CheckProxyRun.Interval = 1000;
            CheckProxyRun.Tick += CheckProxyRunTick;
            // 
            // Main
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(644, 380);
            Controls.Add(grLog);
            Controls.Add(linkWeb);
            Controls.Add(grConfigGameLite);
            Controls.Add(linkGithub);
            Controls.Add(linkDiscord);
            Controls.Add(groupBox8);
            Controls.Add(grProfile);
            Controls.Add(grExtra);
            Controls.Add(groupBox3);
            Controls.Add(grProxy);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(660, 419);
            MinimumSize = new Size(660, 419);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YuukiPS Launcher";
            FormClosing += MainFormClosing;
            Load += Main_Load;
            grLog.ResumeLayout(false);
            grLog.PerformLayout();
            grConfigGameLite.ResumeLayout(false);
            grConfigGameLite.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            grProfile.ResumeLayout(false);
            grProfile.PerformLayout();
            grExtra.ResumeLayout(false);
            grExtra.PerformLayout();
            grProxy.ResumeLayout(false);
            grProxy.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStartNormal;
        private TextBox GetServerHost;
        private Label label2;
        private TextBox GetProxyPort;
        private GroupBox grProxy;
        private CheckBox CheckProxyEnable;
        private Label SetVersion;
        private Label stIsRunProxy;
        private LinkLabel linkDiscord;
        private LinkLabel linkGithub;
        private LinkLabel linkWeb;
        private GroupBox grExtra;
        private CheckBox ExtraCheat;
        private CheckBox Enable_RPC;
        private TextBox Set_LA_GameFolder;
        private Label label5;
        private Button btsave;
        private GroupBox groupBox3;
        private Label Get_LA_MD5;
        private Label Get_LA_REL;
        private Label Get_LA_CH;
        private Label Get_LA_Version;
        private Button Set_LA_Select;
        private System.Windows.Forms.Timer CheckGameRun;
        private System.Windows.Forms.Timer CheckProxyRun;
        private Label label8;
        private Button Server_Config_OpenFolder;
        private Button btStartOfficialServer;
        private ComboBox GetTypeGame;
        private GroupBox grProfile;
        private ComboBox GetProfileServer;
        private Button btload;
        private GroupBox groupBox8;
        private GroupBox grConfigGameLite;
        private Button btStartYuukiServer;
        private CheckBox Enable_WipeLoginCache;
        private GroupBox grLog;
        private CheckBox EnableShowLog;
        private CheckBox EnableSendLog;
    }
}