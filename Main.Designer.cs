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
            TabMain = new TabControl();
            tabPage1 = new TabPage();
            grConfigGameLite = new GroupBox();
            Set_LA_Select = new Button();
            Set_LA_GameFolder = new TextBox();
            label5 = new Label();
            groupBox8 = new GroupBox();
            btStartYuukiServer = new Button();
            GetTypeGame = new ComboBox();
            btStartOfficialServer = new Button();
            grProfile = new GroupBox();
            GetProfileServer = new ComboBox();
            btload = new Button();
            btsave = new Button();
            label8 = new Label();
            grExtra = new GroupBox();
            Extra_Cheat = new CheckBox();
            Enable_RPC = new CheckBox();
            grProxy = new GroupBox();
            stIsRunProxy = new Label();
            CheckProxyEnable = new CheckBox();
            groupBox3 = new GroupBox();
            Get_LA_Version = new Label();
            Get_LA_MD5 = new Label();
            Get_LA_CH = new Label();
            Get_LA_Metode = new Label();
            Get_LA_REL = new Label();
            tabPage2 = new TabPage();
            tabControl2 = new TabControl();
            tabPage10 = new TabPage();
            groupBox4 = new GroupBox();
            Server_DL_DB = new Button();
            Server_DL_JAVA = new Button();
            groupBox6 = new GroupBox();
            Server_DL_RES = new Button();
            comboBox2 = new ComboBox();
            Server_Start = new Button();
            groupBox5 = new GroupBox();
            Server_DL_GC = new Button();
            comboBox1 = new ComboBox();
            Server_Config_OpenFolder = new Button();
            tabPage11 = new TabPage();
            textBox4 = new TextBox();
            label17 = new Label();
            textBox3 = new TextBox();
            label16 = new Label();
            button1 = new Button();
            textBox2 = new TextBox();
            label15 = new Label();
            textBox1 = new TextBox();
            label14 = new Label();
            tabPage4 = new TabPage();
            TabConfig = new TabControl();
            tabPage5 = new TabPage();
            Set_UA_Folder = new TextBox();
            Set_Metadata_Folder = new TextBox();
            label6 = new Label();
            label4 = new Label();
            Set_LA_GameFile = new TextBox();
            label7 = new Label();
            tabPage9 = new TabPage();
            Config_Discord_Enable = new CheckBox();
            tabPage3 = new TabPage();
            progressBar1 = new ProgressBar();
            label9 = new Label();
            label3 = new Label();
            txt_statusUpd = new Label();
            btnstartUpdate = new Button();
            groupBox1 = new GroupBox();
            btchooseupdate = new Button();
            tbx_update = new TextBox();
            label1 = new Label();
            Set_Version = new Label();
            linkDiscord = new LinkLabel();
            linkGithub = new LinkLabel();
            linkWeb = new LinkLabel();
            CheckGameRun = new System.Windows.Forms.Timer(components);
            CheckProxyRun = new System.Windows.Forms.Timer(components);
            TabMain.SuspendLayout();
            tabPage1.SuspendLayout();
            grConfigGameLite.SuspendLayout();
            groupBox8.SuspendLayout();
            grProfile.SuspendLayout();
            grExtra.SuspendLayout();
            grProxy.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage10.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            tabPage11.SuspendLayout();
            tabPage4.SuspendLayout();
            TabConfig.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage9.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btStartNormal
            // 
            btStartNormal.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            btStartNormal.Location = new Point(6, 61);
            btStartNormal.Name = "btStartNormal";
            btStartNormal.Size = new Size(105, 38);
            btStartNormal.TabIndex = 0;
            btStartNormal.Text = "Launch";
            btStartNormal.UseVisualStyleBackColor = true;
            btStartNormal.Click += btStartNormal_Click;
            // 
            // GetServerHost
            // 
            GetServerHost.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetServerHost.Location = new Point(6, 20);
            GetServerHost.Name = "GetServerHost";
            GetServerHost.Size = new Size(232, 35);
            GetServerHost.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(0, 35);
            label2.Name = "label2";
            label2.Size = new Size(41, 21);
            label2.TabIndex = 4;
            label2.Text = "Port:";
            // 
            // GetProxyPort
            // 
            GetProxyPort.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetProxyPort.Location = new Point(37, 35);
            GetProxyPort.Name = "GetProxyPort";
            GetProxyPort.Size = new Size(51, 25);
            GetProxyPort.TabIndex = 5;
            GetProxyPort.Text = "2242";
            // 
            // TabMain
            // 
            TabMain.Controls.Add(tabPage1);
            TabMain.Controls.Add(tabPage2);
            TabMain.Controls.Add(tabPage4);
            TabMain.Controls.Add(tabPage3);
            TabMain.Dock = DockStyle.Top;
            TabMain.Location = new Point(0, 0);
            TabMain.Name = "TabMain";
            TabMain.SelectedIndex = 0;
            TabMain.Size = new Size(662, 411);
            TabMain.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(grConfigGameLite);
            tabPage1.Controls.Add(groupBox8);
            tabPage1.Controls.Add(grProfile);
            tabPage1.Controls.Add(grExtra);
            tabPage1.Controls.Add(grProxy);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(654, 383);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Connect";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // grConfigGameLite
            // 
            grConfigGameLite.Controls.Add(Set_LA_Select);
            grConfigGameLite.Controls.Add(Set_LA_GameFolder);
            grConfigGameLite.Controls.Add(label5);
            grConfigGameLite.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            grConfigGameLite.Location = new Point(8, 111);
            grConfigGameLite.Name = "grConfigGameLite";
            grConfigGameLite.Size = new Size(420, 59);
            grConfigGameLite.TabIndex = 19;
            grConfigGameLite.TabStop = false;
            grConfigGameLite.Text = "Game Config";
            // 
            // Set_LA_Select
            // 
            Set_LA_Select.Location = new Point(331, 16);
            Set_LA_Select.Name = "Set_LA_Select";
            Set_LA_Select.Size = new Size(76, 30);
            Set_LA_Select.TabIndex = 9;
            Set_LA_Select.Text = "Choose";
            Set_LA_Select.UseVisualStyleBackColor = true;
            Set_LA_Select.Click += Set_LA_Select_Click;
            // 
            // Set_LA_GameFolder
            // 
            Set_LA_GameFolder.Location = new Point(117, 17);
            Set_LA_GameFolder.Name = "Set_LA_GameFolder";
            Set_LA_GameFolder.ReadOnly = true;
            Set_LA_GameFolder.Size = new Size(208, 29);
            Set_LA_GameFolder.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(9, 25);
            label5.Name = "label5";
            label5.Size = new Size(102, 21);
            label5.TabIndex = 0;
            label5.Text = "Game Folder:";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btStartYuukiServer);
            groupBox8.Controls.Add(GetServerHost);
            groupBox8.Controls.Add(btStartNormal);
            groupBox8.Controls.Add(GetTypeGame);
            groupBox8.Controls.Add(btStartOfficialServer);
            groupBox8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox8.Location = new Point(8, 6);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(420, 106);
            groupBox8.TabIndex = 18;
            groupBox8.TabStop = false;
            groupBox8.Text = "Connect to server";
            // 
            // btStartYuukiServer
            // 
            btStartYuukiServer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btStartYuukiServer.Location = new Point(326, 20);
            btStartYuukiServer.Name = "btStartYuukiServer";
            btStartYuukiServer.Size = new Size(91, 35);
            btStartYuukiServer.TabIndex = 20;
            btStartYuukiServer.Text = "YuukiPS";
            btStartYuukiServer.UseVisualStyleBackColor = true;
            btStartYuukiServer.Click += btStartYuukiServer_Click;
            // 
            // GetTypeGame
            // 
            GetTypeGame.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetTypeGame.FormattingEnabled = true;
            GetTypeGame.Location = new Point(117, 61);
            GetTypeGame.Name = "GetTypeGame";
            GetTypeGame.Size = new Size(300, 38);
            GetTypeGame.TabIndex = 14;
            GetTypeGame.SelectedIndexChanged += GetTypeGame_SelectedIndexChanged;
            // 
            // btStartOfficialServer
            // 
            btStartOfficialServer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btStartOfficialServer.Location = new Point(244, 20);
            btStartOfficialServer.Name = "btStartOfficialServer";
            btStartOfficialServer.Size = new Size(81, 35);
            btStartOfficialServer.TabIndex = 13;
            btStartOfficialServer.Text = "Official";
            btStartOfficialServer.UseVisualStyleBackColor = true;
            btStartOfficialServer.Click += btStartOfficialServer_Click;
            // 
            // grProfile
            // 
            grProfile.Controls.Add(GetProfileServer);
            grProfile.Controls.Add(btload);
            grProfile.Controls.Add(btsave);
            grProfile.Controls.Add(label8);
            grProfile.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            grProfile.Location = new Point(6, 176);
            grProfile.Name = "grProfile";
            grProfile.Size = new Size(420, 108);
            grProfile.TabIndex = 17;
            grProfile.TabStop = false;
            grProfile.Text = "Profile";
            // 
            // GetProfileServer
            // 
            GetProfileServer.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetProfileServer.FormattingEnabled = true;
            GetProfileServer.Location = new Point(6, 28);
            GetProfileServer.Name = "GetProfileServer";
            GetProfileServer.Size = new Size(234, 38);
            GetProfileServer.TabIndex = 15;
            GetProfileServer.SelectedIndexChanged += GetProfileServer_SelectedIndexChanged;
            // 
            // btload
            // 
            btload.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btload.Location = new Point(332, 28);
            btload.Name = "btload";
            btload.Size = new Size(75, 38);
            btload.TabIndex = 16;
            btload.Text = "Load";
            btload.UseVisualStyleBackColor = true;
            btload.Click += btload_Click;
            // 
            // btsave
            // 
            btsave.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btsave.Location = new Point(246, 28);
            btsave.Name = "btsave";
            btsave.Size = new Size(80, 38);
            btsave.TabIndex = 2;
            btsave.Text = "Save";
            btsave.UseVisualStyleBackColor = true;
            btsave.Click += Set_LA_Save_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(176, 78);
            label8.Name = "label8";
            label8.Size = new Size(231, 20);
            label8.TabIndex = 13;
            label8.Text = "Click Save to save current settings";
            // 
            // grExtra
            // 
            grExtra.Controls.Add(Extra_Cheat);
            grExtra.Controls.Add(Enable_RPC);
            grExtra.Location = new Point(431, 264);
            grExtra.Name = "grExtra";
            grExtra.Size = new Size(217, 65);
            grExtra.TabIndex = 12;
            grExtra.TabStop = false;
            grExtra.Text = "Extra";
            // 
            // Extra_Cheat
            // 
            Extra_Cheat.AutoSize = true;
            Extra_Cheat.Location = new Point(7, 22);
            Extra_Cheat.Name = "Extra_Cheat";
            Extra_Cheat.Size = new Size(57, 19);
            Extra_Cheat.TabIndex = 0;
            Extra_Cheat.Text = "Cheat";
            Extra_Cheat.UseVisualStyleBackColor = true;
            Extra_Cheat.CheckedChanged += Extra_Cheat_CheckedChanged;
            // 
            // Enable_RPC
            // 
            Enable_RPC.AutoSize = true;
            Enable_RPC.Location = new Point(7, 41);
            Enable_RPC.Name = "Enable_RPC";
            Enable_RPC.Size = new Size(150, 19);
            Enable_RPC.TabIndex = 1;
            Enable_RPC.Text = "Rich Presence (Discord)";
            Enable_RPC.UseVisualStyleBackColor = true;
            Enable_RPC.CheckedChanged += Extra_Enable_RPC_CheckedChanged;
            // 
            // grProxy
            // 
            grProxy.Controls.Add(GetProxyPort);
            grProxy.Controls.Add(stIsRunProxy);
            grProxy.Controls.Add(CheckProxyEnable);
            grProxy.Controls.Add(label2);
            grProxy.Location = new Point(431, 191);
            grProxy.Name = "grProxy";
            grProxy.Size = new Size(215, 67);
            grProxy.TabIndex = 11;
            grProxy.TabStop = false;
            grProxy.Text = "Proxy";
            // 
            // stIsRunProxy
            // 
            stIsRunProxy.AutoSize = true;
            stIsRunProxy.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            stIsRunProxy.Location = new Point(92, 14);
            stIsRunProxy.Name = "stIsRunProxy";
            stIsRunProxy.Size = new Size(87, 21);
            stIsRunProxy.TabIndex = 8;
            stIsRunProxy.Text = "Status: OFF";
            // 
            // CheckProxyEnable
            // 
            CheckProxyEnable.AutoSize = true;
            CheckProxyEnable.Checked = true;
            CheckProxyEnable.CheckState = CheckState.Checked;
            CheckProxyEnable.Location = new Point(6, 18);
            CheckProxyEnable.Name = "CheckProxyEnable";
            CheckProxyEnable.Size = new Size(61, 19);
            CheckProxyEnable.TabIndex = 7;
            CheckProxyEnable.Text = "Enable";
            CheckProxyEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Get_LA_Version);
            groupBox3.Controls.Add(Get_LA_MD5);
            groupBox3.Controls.Add(Get_LA_CH);
            groupBox3.Controls.Add(Get_LA_Metode);
            groupBox3.Controls.Add(Get_LA_REL);
            groupBox3.Location = new Point(431, 8);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(217, 177);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Game";
            // 
            // Get_LA_Version
            // 
            Get_LA_Version.AutoSize = true;
            Get_LA_Version.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Get_LA_Version.Location = new Point(6, 19);
            Get_LA_Version.Name = "Get_LA_Version";
            Get_LA_Version.Size = new Size(136, 21);
            Get_LA_Version.TabIndex = 3;
            Get_LA_Version.Text = "Version: Unknown";
            // 
            // Get_LA_MD5
            // 
            Get_LA_MD5.AutoSize = true;
            Get_LA_MD5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Get_LA_MD5.Location = new Point(7, 145);
            Get_LA_MD5.Name = "Get_LA_MD5";
            Get_LA_MD5.Size = new Size(118, 21);
            Get_LA_MD5.TabIndex = 7;
            Get_LA_MD5.Text = "MD5: Unknown";
            // 
            // Get_LA_CH
            // 
            Get_LA_CH.AutoSize = true;
            Get_LA_CH.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Get_LA_CH.Location = new Point(6, 50);
            Get_LA_CH.Name = "Get_LA_CH";
            Get_LA_CH.Size = new Size(141, 21);
            Get_LA_CH.TabIndex = 4;
            Get_LA_CH.Text = "Channel: Unknown";
            // 
            // Get_LA_Metode
            // 
            Get_LA_Metode.AutoSize = true;
            Get_LA_Metode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Get_LA_Metode.Location = new Point(6, 114);
            Get_LA_Metode.Name = "Get_LA_Metode";
            Get_LA_Metode.Size = new Size(141, 21);
            Get_LA_Metode.TabIndex = 6;
            Get_LA_Metode.Text = "Metode:  Unknown";
            // 
            // Get_LA_REL
            // 
            Get_LA_REL.AutoSize = true;
            Get_LA_REL.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Get_LA_REL.Location = new Point(6, 83);
            Get_LA_REL.Name = "Get_LA_REL";
            Get_LA_REL.Size = new Size(137, 21);
            Get_LA_REL.TabIndex = 5;
            Get_LA_REL.Text = "Release: Unknown";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tabControl2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(654, 383);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Server";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage10);
            tabControl2.Controls.Add(tabPage11);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(3, 3);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(648, 377);
            tabControl2.TabIndex = 8;
            // 
            // tabPage10
            // 
            tabPage10.Controls.Add(groupBox4);
            tabPage10.Controls.Add(groupBox6);
            tabPage10.Controls.Add(Server_Start);
            tabPage10.Controls.Add(groupBox5);
            tabPage10.Controls.Add(Server_Config_OpenFolder);
            tabPage10.Location = new Point(4, 24);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new Padding(3);
            tabPage10.Size = new Size(640, 349);
            tabPage10.TabIndex = 0;
            tabPage10.Text = "Home";
            tabPage10.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(Server_DL_DB);
            groupBox4.Controls.Add(Server_DL_JAVA);
            groupBox4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(6, 6);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(155, 100);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Download Package";
            // 
            // Server_DL_DB
            // 
            Server_DL_DB.Location = new Point(6, 63);
            Server_DL_DB.Name = "Server_DL_DB";
            Server_DL_DB.Size = new Size(143, 29);
            Server_DL_DB.TabIndex = 1;
            Server_DL_DB.Text = "MongoDB";
            Server_DL_DB.UseVisualStyleBackColor = true;
            Server_DL_DB.Click += Server_DL_DB_Click;
            // 
            // Server_DL_JAVA
            // 
            Server_DL_JAVA.Location = new Point(6, 28);
            Server_DL_JAVA.Name = "Server_DL_JAVA";
            Server_DL_JAVA.Size = new Size(143, 29);
            Server_DL_JAVA.TabIndex = 0;
            Server_DL_JAVA.Text = "Java";
            Server_DL_JAVA.UseVisualStyleBackColor = true;
            Server_DL_JAVA.Click += Server_DL_JAVA_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(Server_DL_RES);
            groupBox6.Controls.Add(comboBox2);
            groupBox6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox6.Location = new Point(367, 6);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(200, 100);
            groupBox6.TabIndex = 7;
            groupBox6.TabStop = false;
            groupBox6.Text = "Version Resources:";
            // 
            // Server_DL_RES
            // 
            Server_DL_RES.Location = new Point(6, 56);
            Server_DL_RES.Name = "Server_DL_RES";
            Server_DL_RES.Size = new Size(188, 38);
            Server_DL_RES.TabIndex = 8;
            Server_DL_RES.Text = "Download";
            Server_DL_RES.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Yuuki Gitlab 3.1", "Yuuki Gitlab 3.0", "Yuuki Gitlab 2.8", "Yuuki Gitlab 2.7", "Yuuki Gitlab 2.6" });
            comboBox2.Location = new Point(6, 23);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(188, 33);
            comboBox2.TabIndex = 3;
            comboBox2.Text = "Yuuki Gitlab 3.1";
            // 
            // Server_Start
            // 
            Server_Start.Location = new Point(107, 218);
            Server_Start.Name = "Server_Start";
            Server_Start.Size = new Size(93, 23);
            Server_Start.TabIndex = 0;
            Server_Start.Text = "Start";
            Server_Start.UseVisualStyleBackColor = true;
            Server_Start.Click += Server_Start_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(Server_DL_GC);
            groupBox5.Controls.Add(comboBox1);
            groupBox5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(167, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 100);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Version Grasscutter:";
            // 
            // Server_DL_GC
            // 
            Server_DL_GC.Location = new Point(6, 57);
            Server_DL_GC.Name = "Server_DL_GC";
            Server_DL_GC.Size = new Size(188, 38);
            Server_DL_GC.TabIndex = 3;
            Server_DL_GC.Text = "Download";
            Server_DL_GC.UseVisualStyleBackColor = true;
            Server_DL_GC.Click += Server_DL_GC_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "DockerGS 2.6", "DockerGS 2.7", "DockerGS 2.8", "DockerGS 3.0", "DockerGS 3.1" });
            comboBox1.Location = new Point(6, 23);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(188, 33);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "DockerGC 3.1";
            // 
            // Server_Config_OpenFolder
            // 
            Server_Config_OpenFolder.Location = new Point(12, 218);
            Server_Config_OpenFolder.Name = "Server_Config_OpenFolder";
            Server_Config_OpenFolder.Size = new Size(89, 23);
            Server_Config_OpenFolder.TabIndex = 1;
            Server_Config_OpenFolder.Text = "Folder Server";
            Server_Config_OpenFolder.UseVisualStyleBackColor = true;
            Server_Config_OpenFolder.Click += Server_Config_OpenFolder_Click;
            // 
            // tabPage11
            // 
            tabPage11.Controls.Add(textBox4);
            tabPage11.Controls.Add(label17);
            tabPage11.Controls.Add(textBox3);
            tabPage11.Controls.Add(label16);
            tabPage11.Controls.Add(button1);
            tabPage11.Controls.Add(textBox2);
            tabPage11.Controls.Add(label15);
            tabPage11.Controls.Add(textBox1);
            tabPage11.Controls.Add(label14);
            tabPage11.Location = new Point(4, 24);
            tabPage11.Name = "tabPage11";
            tabPage11.Padding = new Padding(3);
            tabPage11.Size = new Size(640, 349);
            tabPage11.TabIndex = 1;
            tabPage11.Text = "Custom";
            tabPage11.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(6, 192);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(626, 23);
            textBox4.TabIndex = 8;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(6, 165);
            label17.Name = "label17";
            label17.Size = new Size(206, 25);
            label17.TabIndex = 7;
            label17.Text = "Folder MongoDB (BIN):";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(6, 139);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(626, 23);
            textBox3.TabIndex = 6;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(6, 111);
            label16.Name = "label16";
            label16.Size = new Size(157, 25);
            label16.TabIndex = 5;
            label16.Text = "Folder Java (BIN):";
            // 
            // button1
            // 
            button1.Location = new Point(557, 221);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(6, 85);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(626, 23);
            textBox2.TabIndex = 3;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(6, 57);
            label15.Name = "label15";
            label15.Size = new Size(258, 25);
            label15.TabIndex = 2;
            label15.Text = "Grasscutter Resources Folder:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(6, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(626, 23);
            textBox1.TabIndex = 1;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(3, 3);
            label14.Name = "label14";
            label14.Size = new Size(169, 25);
            label14.TabIndex = 0;
            label14.Text = "Grasscutter Folder:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(TabConfig);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(654, 383);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Config";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // TabConfig
            // 
            TabConfig.Controls.Add(tabPage5);
            TabConfig.Controls.Add(tabPage9);
            TabConfig.Dock = DockStyle.Fill;
            TabConfig.Location = new Point(0, 0);
            TabConfig.Name = "TabConfig";
            TabConfig.SelectedIndex = 0;
            TabConfig.Size = new Size(654, 383);
            TabConfig.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(Set_UA_Folder);
            tabPage5.Controls.Add(Set_Metadata_Folder);
            tabPage5.Controls.Add(label6);
            tabPage5.Controls.Add(label4);
            tabPage5.Controls.Add(Set_LA_GameFile);
            tabPage5.Controls.Add(label7);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(646, 355);
            tabPage5.TabIndex = 0;
            tabPage5.Text = "Launcher";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // Set_UA_Folder
            // 
            Set_UA_Folder.Location = new Point(195, 89);
            Set_UA_Folder.Name = "Set_UA_Folder";
            Set_UA_Folder.ReadOnly = true;
            Set_UA_Folder.Size = new Size(219, 23);
            Set_UA_Folder.TabIndex = 1;
            // 
            // Set_Metadata_Folder
            // 
            Set_Metadata_Folder.Location = new Point(10, 89);
            Set_Metadata_Folder.Name = "Set_Metadata_Folder";
            Set_Metadata_Folder.ReadOnly = true;
            Set_Metadata_Folder.Size = new Size(179, 23);
            Set_Metadata_Folder.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(195, 62);
            label6.Name = "label6";
            label6.Size = new Size(160, 21);
            label6.TabIndex = 0;
            label6.Text = "UserAssembly Folder:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(10, 62);
            label4.Name = "label4";
            label4.Size = new Size(126, 21);
            label4.TabIndex = 3;
            label4.Text = "Metadata Folder:";
            // 
            // Set_LA_GameFile
            // 
            Set_LA_GameFile.Location = new Point(10, 36);
            Set_LA_GameFile.Name = "Set_LA_GameFile";
            Set_LA_GameFile.ReadOnly = true;
            Set_LA_GameFile.Size = new Size(405, 23);
            Set_LA_GameFile.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(6, 12);
            label7.Name = "label7";
            label7.Size = new Size(86, 21);
            label7.TabIndex = 10;
            label7.Text = "Game files:";
            // 
            // tabPage9
            // 
            tabPage9.Controls.Add(Config_Discord_Enable);
            tabPage9.Location = new Point(4, 24);
            tabPage9.Name = "tabPage9";
            tabPage9.Size = new Size(646, 355);
            tabPage9.TabIndex = 1;
            tabPage9.Text = "Discord";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // Config_Discord_Enable
            // 
            Config_Discord_Enable.AutoSize = true;
            Config_Discord_Enable.Checked = true;
            Config_Discord_Enable.CheckState = CheckState.Checked;
            Config_Discord_Enable.Location = new Point(14, 13);
            Config_Discord_Enable.Name = "Config_Discord_Enable";
            Config_Discord_Enable.Size = new Size(142, 19);
            Config_Discord_Enable.TabIndex = 0;
            Config_Discord_Enable.Text = "Discord Rich Presence";
            Config_Discord_Enable.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(progressBar1);
            tabPage3.Controls.Add(label9);
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(txt_statusUpd);
            tabPage3.Controls.Add(btnstartUpdate);
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(654, 383);
            tabPage3.TabIndex = 4;
            tabPage3.Text = "Updater";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(17, 199);
            progressBar1.Maximum = 101;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(600, 34);
            progressBar1.TabIndex = 25;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(92, 336);
            label9.Name = "label9";
            label9.Size = new Size(465, 32);
            label9.TabIndex = 24;
            label9.Text = "Set again the game folder upon using this";
            label9.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(56, 253);
            label3.Name = "label3";
            label3.Size = new Size(555, 64);
            label3.TabIndex = 23;
            label3.Text = "Note: Do NOT use this to update GI from a version\r\nlower than 3.5.0 (or 3.5.0) to 3.6.0+";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // txt_statusUpd
            // 
            txt_statusUpd.AutoSize = true;
            txt_statusUpd.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            txt_statusUpd.Location = new Point(6, 80);
            txt_statusUpd.Name = "txt_statusUpd";
            txt_statusUpd.Size = new Size(176, 32);
            txt_statusUpd.TabIndex = 22;
            txt_statusUpd.Text = "Status: Waiting.";
            // 
            // btnstartUpdate
            // 
            btnstartUpdate.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            btnstartUpdate.Location = new Point(459, 16);
            btnstartUpdate.Name = "btnstartUpdate";
            btnstartUpdate.Size = new Size(158, 49);
            btnstartUpdate.TabIndex = 21;
            btnstartUpdate.Text = "Start";
            btnstartUpdate.UseVisualStyleBackColor = true;
            btnstartUpdate.Click += btnstartUpdate_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btchooseupdate);
            groupBox1.Controls.Add(tbx_update);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(8, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(420, 59);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Update Config";
            // 
            // btchooseupdate
            // 
            btchooseupdate.Location = new Point(331, 16);
            btchooseupdate.Name = "btchooseupdate";
            btchooseupdate.Size = new Size(76, 30);
            btchooseupdate.TabIndex = 9;
            btchooseupdate.Text = "Choose";
            btchooseupdate.UseVisualStyleBackColor = true;
            btchooseupdate.Click += btchooseupdate_Click;
            // 
            // tbx_update
            // 
            tbx_update.Location = new Point(117, 17);
            tbx_update.Name = "tbx_update";
            tbx_update.ReadOnly = true;
            tbx_update.Size = new Size(208, 29);
            tbx_update.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(9, 25);
            label1.Name = "label1";
            label1.Size = new Size(97, 21);
            label1.TabIndex = 0;
            label1.Text = "Update Path:";
            // 
            // Set_Version
            // 
            Set_Version.AutoSize = true;
            Set_Version.Dock = DockStyle.Left;
            Set_Version.Location = new Point(0, 411);
            Set_Version.Name = "Set_Version";
            Set_Version.Size = new Size(75, 15);
            Set_Version.TabIndex = 12;
            Set_Version.Text = "Version: 0.0.0";
            // 
            // linkDiscord
            // 
            linkDiscord.AutoSize = true;
            linkDiscord.Dock = DockStyle.Right;
            linkDiscord.Location = new Point(615, 411);
            linkDiscord.Name = "linkDiscord";
            linkDiscord.Size = new Size(47, 15);
            linkDiscord.TabIndex = 13;
            linkDiscord.TabStop = true;
            linkDiscord.Text = "Discord";
            linkDiscord.LinkClicked += linkDiscord_LinkClicked;
            // 
            // linkGithub
            // 
            linkGithub.AutoSize = true;
            linkGithub.Dock = DockStyle.Right;
            linkGithub.Location = new Point(572, 411);
            linkGithub.Name = "linkGithub";
            linkGithub.Size = new Size(43, 15);
            linkGithub.TabIndex = 14;
            linkGithub.TabStop = true;
            linkGithub.Text = "Github";
            linkGithub.LinkClicked += linkGithub_LinkClicked;
            // 
            // linkWeb
            // 
            linkWeb.AutoSize = true;
            linkWeb.Dock = DockStyle.Right;
            linkWeb.Location = new Point(541, 411);
            linkWeb.Name = "linkWeb";
            linkWeb.Size = new Size(31, 15);
            linkWeb.TabIndex = 15;
            linkWeb.TabStop = true;
            linkWeb.Text = "Web";
            linkWeb.LinkClicked += linkWeb_LinkClicked;
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
            CheckProxyRun.Tick += CheckProxyRun_Tick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 431);
            Controls.Add(linkWeb);
            Controls.Add(linkGithub);
            Controls.Add(linkDiscord);
            Controls.Add(Set_Version);
            Controls.Add(TabMain);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Main";
            Text = "YuukiPS Launcher";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            TabMain.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
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
            tabPage2.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage10.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            tabPage11.ResumeLayout(false);
            tabPage11.PerformLayout();
            tabPage4.ResumeLayout(false);
            TabConfig.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStartNormal;
        private TextBox GetServerHost;
        private Label label2;
        private TextBox GetProxyPort;
        private TabControl TabMain;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button Server_Start;
        private TabPage tabPage4;
        private GroupBox grProxy;
        private CheckBox CheckProxyEnable;
        private Label Set_Version;
        private Label stIsRunProxy;
        private LinkLabel linkDiscord;
        private LinkLabel linkGithub;
        private LinkLabel linkWeb;
        private TabControl TabConfig;
        private TabPage tabPage5;
        private TextBox Set_Metadata_Folder;
        private Label label4;
        private GroupBox grExtra;
        private CheckBox Extra_Cheat;
        private CheckBox Enable_RPC;
        private TextBox Set_LA_GameFolder;
        private Label label5;
        private Button btsave;
        private GroupBox groupBox3;
        private Label Get_LA_MD5;
        private Label Get_LA_Metode;
        private Label Get_LA_REL;
        private Label Get_LA_CH;
        private Label Get_LA_Version;
        private Button Set_LA_Select;
        private TextBox Set_UA_Folder;
        private Label label6;
        private TextBox Set_LA_GameFile;
        private Label label7;
        private System.Windows.Forms.Timer CheckGameRun;
        private System.Windows.Forms.Timer CheckProxyRun;
        private Label label8;
        private TabPage tabPage9;
        private CheckBox Config_Discord_Enable;
        private Button Server_Config_OpenFolder;
        private ComboBox comboBox1;
        private GroupBox groupBox4;
        private Button Server_DL_JAVA;
        private Button Server_DL_DB;
        private GroupBox groupBox5;
        private Button Server_DL_GC;
        private GroupBox groupBox6;
        private Button Server_DL_RES;
        private ComboBox comboBox2;
        private TabControl tabControl2;
        private TabPage tabPage10;
        private TabPage tabPage11;
        private TextBox textBox4;
        private Label label17;
        private TextBox textBox3;
        private Label label16;
        private Button button1;
        private TextBox textBox2;
        private Label label15;
        private TextBox textBox1;
        private Label label14;
        private Button btStartOfficialServer;
        private ComboBox GetTypeGame;
        private GroupBox grProfile;
        private ComboBox GetProfileServer;
        private Button btload;
        private GroupBox groupBox8;
        private GroupBox grConfigGameLite;
        private Button btStartYuukiServer;
        private TabPage tabPage3;
        private GroupBox groupBox1;
        private Button btchooseupdate;
        private TextBox tbx_update;
        private Label label1;
        private Button btnstartUpdate;
        private Label txt_statusUpd;
        private Label label3;
        private Label label9;
        private ProgressBar progressBar1;
    }
}