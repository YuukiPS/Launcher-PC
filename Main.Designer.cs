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
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Yuuki", "tes.yuuki.me", "N/A", "N/A", "N/A" }, -1);
            btStart = new Button();
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
            GetTypeGame = new ComboBox();
            btStartOfficialServer = new Button();
            grProfile = new GroupBox();
            GetProfileServer = new ComboBox();
            btload = new Button();
            btsave = new Button();
            label8 = new Label();
            grExtra = new GroupBox();
            Extra_AkebiGC = new CheckBox();
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
            tabPage3 = new TabPage();
            Is_ServerList_Autocheck = new CheckBox();
            ServerList = new ListView();
            ServerList_GetName = new ColumnHeader();
            ServerList_GetHost = new ColumnHeader();
            ServerList_GetOnline = new ColumnHeader();
            ServerList_GetVersion = new ColumnHeader();
            ServerList_GetPing = new ColumnHeader();
            btReloadServer = new Button();
            label3 = new Label();
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
            tabPage8 = new TabPage();
            tabControl1 = new TabControl();
            tabPage6 = new TabPage();
            DEV_MA_bt_Decrypt = new Button();
            DEV_MA_Set_Key2_Patch = new TextBox();
            DEV_MA_Set_Key2_NoPatch = new TextBox();
            label13 = new Label();
            label12 = new Label();
            DEV_MA_Set_Key1_Patch = new TextBox();
            label11 = new Label();
            DEV_MA_Set_Key1_NoPatch = new TextBox();
            label10 = new Label();
            DEV_MA_bt_Selectfile = new Button();
            label9 = new Label();
            DEV_MA_get_file = new TextBox();
            DEV_MA_bt_Patch = new Button();
            tabPage7 = new TabPage();
            DEV_UA_Set_Key2_Patch = new TextBox();
            label20 = new Label();
            DEV_UA_Set_Key1_NoPatch = new TextBox();
            label21 = new Label();
            DEV_UA_bt_Selectfile = new Button();
            label22 = new Label();
            DEV_UA_get_file = new TextBox();
            DEV_UA_bt_Patch = new Button();
            Set_Version = new Label();
            linkDiscord = new LinkLabel();
            linkGithub = new LinkLabel();
            linkWeb = new LinkLabel();
            CekUpdateTT = new System.Windows.Forms.Timer(components);
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
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            TabConfig.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage9.SuspendLayout();
            tabPage8.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            SuspendLayout();
            // 
            // btStart
            // 
            btStart.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            btStart.Location = new Point(6, 61);
            btStart.Name = "btStart";
            btStart.Size = new Size(105, 38);
            btStart.TabIndex = 0;
            btStart.Text = "Launch";
            btStart.UseVisualStyleBackColor = true;
            btStart.Click += btStart_Click;
            // 
            // GetServerHost
            // 
            GetServerHost.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetServerHost.Location = new Point(6, 20);
            GetServerHost.Name = "GetServerHost";
            GetServerHost.Size = new Size(253, 35);
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
            TabMain.Controls.Add(tabPage3);
            TabMain.Controls.Add(tabPage4);
            TabMain.Controls.Add(tabPage8);
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
            grConfigGameLite.Location = new Point(8, 126);
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
            groupBox8.Controls.Add(GetServerHost);
            groupBox8.Controls.Add(btStart);
            groupBox8.Controls.Add(GetTypeGame);
            groupBox8.Controls.Add(btStartOfficialServer);
            groupBox8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox8.Location = new Point(8, 6);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(420, 114);
            groupBox8.TabIndex = 18;
            groupBox8.TabStop = false;
            groupBox8.Text = "Connect to server";
            // 
            // GetTypeGame
            // 
            GetTypeGame.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            GetTypeGame.FormattingEnabled = true;
            GetTypeGame.Location = new Point(117, 61);
            GetTypeGame.Name = "GetTypeGame";
            GetTypeGame.Size = new Size(290, 38);
            GetTypeGame.TabIndex = 14;
            GetTypeGame.SelectedIndexChanged += GetTypeGame_SelectedIndexChanged;
            // 
            // btStartOfficialServer
            // 
            btStartOfficialServer.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btStartOfficialServer.Location = new Point(265, 20);
            btStartOfficialServer.Name = "btStartOfficialServer";
            btStartOfficialServer.Size = new Size(142, 35);
            btStartOfficialServer.TabIndex = 13;
            btStartOfficialServer.Text = "Official Server";
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
            grProfile.Location = new Point(8, 191);
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
            grExtra.Controls.Add(Extra_AkebiGC);
            grExtra.Location = new Point(277, 302);
            grExtra.Name = "grExtra";
            grExtra.Size = new Size(151, 70);
            grExtra.TabIndex = 12;
            grExtra.TabStop = false;
            grExtra.Text = "Extra";
            // 
            // Extra_AkebiGC
            // 
            Extra_AkebiGC.AutoSize = true;
            Extra_AkebiGC.Location = new Point(6, 22);
            Extra_AkebiGC.Name = "Extra_AkebiGC";
            Extra_AkebiGC.Size = new Size(77, 19);
            Extra_AkebiGC.TabIndex = 0;
            Extra_AkebiGC.Text = "Akebi-GC";
            Extra_AkebiGC.UseVisualStyleBackColor = true;
            // 
            // grProxy
            // 
            grProxy.Controls.Add(GetProxyPort);
            grProxy.Controls.Add(stIsRunProxy);
            grProxy.Controls.Add(CheckProxyEnable);
            grProxy.Controls.Add(label2);
            grProxy.Location = new Point(8, 305);
            grProxy.Name = "grProxy";
            grProxy.Size = new Size(263, 67);
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
            // tabPage3
            // 
            tabPage3.Controls.Add(Is_ServerList_Autocheck);
            tabPage3.Controls.Add(ServerList);
            tabPage3.Controls.Add(btReloadServer);
            tabPage3.Controls.Add(label3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(654, 383);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Command";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // Is_ServerList_Autocheck
            // 
            Is_ServerList_Autocheck.AutoSize = true;
            Is_ServerList_Autocheck.Checked = true;
            Is_ServerList_Autocheck.CheckState = CheckState.Checked;
            Is_ServerList_Autocheck.Location = new Point(425, 54);
            Is_ServerList_Autocheck.Name = "Is_ServerList_Autocheck";
            Is_ServerList_Autocheck.Size = new Size(91, 19);
            Is_ServerList_Autocheck.TabIndex = 13;
            Is_ServerList_Autocheck.Text = "Auto Reload";
            Is_ServerList_Autocheck.UseVisualStyleBackColor = true;
            // 
            // ServerList
            // 
            ServerList.AllowDrop = true;
            ServerList.Columns.AddRange(new ColumnHeader[] { ServerList_GetName, ServerList_GetHost, ServerList_GetOnline, ServerList_GetVersion, ServerList_GetPing });
            ServerList.FullRowSelect = true;
            ServerList.GridLines = true;
            ServerList.Items.AddRange(new ListViewItem[] { listViewItem1 });
            ServerList.Location = new Point(8, 15);
            ServerList.MultiSelect = false;
            ServerList.Name = "ServerList";
            ServerList.ShowGroups = false;
            ServerList.Size = new Size(401, 239);
            ServerList.TabIndex = 8;
            ServerList.UseCompatibleStateImageBehavior = false;
            ServerList.View = View.Details;
            ServerList.MouseDoubleClick += ServerList_MouseDoubleClick;
            // 
            // ServerList_GetName
            // 
            ServerList_GetName.Text = "Name";
            ServerList_GetName.Width = 90;
            // 
            // ServerList_GetHost
            // 
            ServerList_GetHost.Text = "Host";
            ServerList_GetHost.Width = 120;
            // 
            // ServerList_GetOnline
            // 
            ServerList_GetOnline.Text = "Online";
            // 
            // ServerList_GetVersion
            // 
            ServerList_GetVersion.Text = "Version";
            // 
            // ServerList_GetPing
            // 
            ServerList_GetPing.Text = "Ping";
            // 
            // btReloadServer
            // 
            btReloadServer.Location = new Point(415, 15);
            btReloadServer.Name = "btReloadServer";
            btReloadServer.Size = new Size(75, 23);
            btReloadServer.TabIndex = 10;
            btReloadServer.Text = "Reload";
            btReloadServer.UseVisualStyleBackColor = true;
            btReloadServer.Click += btReloadServer_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(415, 91);
            label3.Name = "label3";
            label3.Size = new Size(102, 30);
            label3.TabIndex = 9;
            label3.Text = "Server list";
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
            // tabPage8
            // 
            tabPage8.Controls.Add(tabControl1);
            tabPage8.Location = new Point(4, 24);
            tabPage8.Name = "tabPage8";
            tabPage8.Size = new Size(654, 383);
            tabPage8.TabIndex = 4;
            tabPage8.Text = "Development Tool";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(654, 383);
            tabControl1.TabIndex = 0;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(DEV_MA_bt_Decrypt);
            tabPage6.Controls.Add(DEV_MA_Set_Key2_Patch);
            tabPage6.Controls.Add(DEV_MA_Set_Key2_NoPatch);
            tabPage6.Controls.Add(label13);
            tabPage6.Controls.Add(label12);
            tabPage6.Controls.Add(DEV_MA_Set_Key1_Patch);
            tabPage6.Controls.Add(label11);
            tabPage6.Controls.Add(DEV_MA_Set_Key1_NoPatch);
            tabPage6.Controls.Add(label10);
            tabPage6.Controls.Add(DEV_MA_bt_Selectfile);
            tabPage6.Controls.Add(label9);
            tabPage6.Controls.Add(DEV_MA_get_file);
            tabPage6.Controls.Add(DEV_MA_bt_Patch);
            tabPage6.Location = new Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(646, 355);
            tabPage6.TabIndex = 0;
            tabPage6.Text = "Metadata";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // DEV_MA_bt_Decrypt
            // 
            DEV_MA_bt_Decrypt.Location = new Point(579, 7);
            DEV_MA_bt_Decrypt.Name = "DEV_MA_bt_Decrypt";
            DEV_MA_bt_Decrypt.Size = new Size(59, 23);
            DEV_MA_bt_Decrypt.TabIndex = 12;
            DEV_MA_bt_Decrypt.Text = "Decrypt";
            DEV_MA_bt_Decrypt.UseVisualStyleBackColor = true;
            DEV_MA_bt_Decrypt.Click += DEV_MA_bt_Decrypt_Click;
            // 
            // DEV_MA_Set_Key2_Patch
            // 
            DEV_MA_Set_Key2_Patch.Location = new Point(329, 160);
            DEV_MA_Set_Key2_Patch.Multiline = true;
            DEV_MA_Set_Key2_Patch.Name = "DEV_MA_Set_Key2_Patch";
            DEV_MA_Set_Key2_Patch.Size = new Size(300, 77);
            DEV_MA_Set_Key2_Patch.TabIndex = 11;
            // 
            // DEV_MA_Set_Key2_NoPatch
            // 
            DEV_MA_Set_Key2_NoPatch.Location = new Point(17, 160);
            DEV_MA_Set_Key2_NoPatch.Multiline = true;
            DEV_MA_Set_Key2_NoPatch.Name = "DEV_MA_Set_Key2_NoPatch";
            DEV_MA_Set_Key2_NoPatch.Size = new Size(306, 77);
            DEV_MA_Set_Key2_NoPatch.TabIndex = 10;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(329, 136);
            label13.Name = "label13";
            label13.Size = new Size(95, 21);
            label13.TabIndex = 9;
            label13.Text = "Key2 (Patch)";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(17, 136);
            label12.Name = "label12";
            label12.Size = new Size(114, 21);
            label12.TabIndex = 8;
            label12.Text = "Key2 (Original)";
            // 
            // DEV_MA_Set_Key1_Patch
            // 
            DEV_MA_Set_Key1_Patch.Location = new Point(329, 56);
            DEV_MA_Set_Key1_Patch.Multiline = true;
            DEV_MA_Set_Key1_Patch.Name = "DEV_MA_Set_Key1_Patch";
            DEV_MA_Set_Key1_Patch.Size = new Size(300, 77);
            DEV_MA_Set_Key1_Patch.TabIndex = 7;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(329, 32);
            label11.Name = "label11";
            label11.Size = new Size(95, 21);
            label11.TabIndex = 6;
            label11.Text = "Key1 (Patch)";
            // 
            // DEV_MA_Set_Key1_NoPatch
            // 
            DEV_MA_Set_Key1_NoPatch.Location = new Point(17, 56);
            DEV_MA_Set_Key1_NoPatch.Multiline = true;
            DEV_MA_Set_Key1_NoPatch.Name = "DEV_MA_Set_Key1_NoPatch";
            DEV_MA_Set_Key1_NoPatch.Size = new Size(306, 77);
            DEV_MA_Set_Key1_NoPatch.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(17, 32);
            label10.Name = "label10";
            label10.Size = new Size(114, 21);
            label10.TabIndex = 4;
            label10.Text = "Key1 (Original)";
            // 
            // DEV_MA_bt_Selectfile
            // 
            DEV_MA_bt_Selectfile.Location = new Point(438, 7);
            DEV_MA_bt_Selectfile.Name = "DEV_MA_bt_Selectfile";
            DEV_MA_bt_Selectfile.Size = new Size(70, 23);
            DEV_MA_bt_Selectfile.TabIndex = 3;
            DEV_MA_bt_Selectfile.Text = "Select File";
            DEV_MA_bt_Selectfile.UseVisualStyleBackColor = true;
            DEV_MA_bt_Selectfile.Click += DEV_UA_bt_Selectfile_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(6, 6);
            label9.Name = "label9";
            label9.Size = new Size(37, 21);
            label9.TabIndex = 2;
            label9.Text = "File:";
            // 
            // DEV_MA_get_file
            // 
            DEV_MA_get_file.Location = new Point(40, 6);
            DEV_MA_get_file.Name = "DEV_MA_get_file";
            DEV_MA_get_file.Size = new Size(392, 23);
            DEV_MA_get_file.TabIndex = 1;
            // 
            // DEV_MA_bt_Patch
            // 
            DEV_MA_bt_Patch.Location = new Point(514, 7);
            DEV_MA_bt_Patch.Name = "DEV_MA_bt_Patch";
            DEV_MA_bt_Patch.Size = new Size(59, 23);
            DEV_MA_bt_Patch.TabIndex = 0;
            DEV_MA_bt_Patch.Text = "Patch";
            DEV_MA_bt_Patch.UseVisualStyleBackColor = true;
            DEV_MA_bt_Patch.Click += DEV_MA_bt_Patch_Click;
            // 
            // tabPage7
            // 
            tabPage7.Controls.Add(DEV_UA_Set_Key2_Patch);
            tabPage7.Controls.Add(label20);
            tabPage7.Controls.Add(DEV_UA_Set_Key1_NoPatch);
            tabPage7.Controls.Add(label21);
            tabPage7.Controls.Add(DEV_UA_bt_Selectfile);
            tabPage7.Controls.Add(label22);
            tabPage7.Controls.Add(DEV_UA_get_file);
            tabPage7.Controls.Add(DEV_UA_bt_Patch);
            tabPage7.Location = new Point(4, 24);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(646, 355);
            tabPage7.TabIndex = 1;
            tabPage7.Text = "UserAssembly";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // DEV_UA_Set_Key2_Patch
            // 
            DEV_UA_Set_Key2_Patch.Location = new Point(329, 53);
            DEV_UA_Set_Key2_Patch.Multiline = true;
            DEV_UA_Set_Key2_Patch.Name = "DEV_UA_Set_Key2_Patch";
            DEV_UA_Set_Key2_Patch.Size = new Size(302, 194);
            DEV_UA_Set_Key2_Patch.TabIndex = 19;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(329, 29);
            label20.Name = "label20";
            label20.Size = new Size(95, 21);
            label20.TabIndex = 18;
            label20.Text = "Key2 (Patch)";
            // 
            // DEV_UA_Set_Key1_NoPatch
            // 
            DEV_UA_Set_Key1_NoPatch.Location = new Point(17, 53);
            DEV_UA_Set_Key1_NoPatch.Multiline = true;
            DEV_UA_Set_Key1_NoPatch.Name = "DEV_UA_Set_Key1_NoPatch";
            DEV_UA_Set_Key1_NoPatch.Size = new Size(306, 194);
            DEV_UA_Set_Key1_NoPatch.TabIndex = 17;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label21.Location = new Point(17, 29);
            label21.Name = "label21";
            label21.Size = new Size(114, 21);
            label21.TabIndex = 16;
            label21.Text = "Key1 (Original)";
            // 
            // DEV_UA_bt_Selectfile
            // 
            DEV_UA_bt_Selectfile.Location = new Point(498, 7);
            DEV_UA_bt_Selectfile.Name = "DEV_UA_bt_Selectfile";
            DEV_UA_bt_Selectfile.Size = new Size(75, 23);
            DEV_UA_bt_Selectfile.TabIndex = 15;
            DEV_UA_bt_Selectfile.Text = "Select File";
            DEV_UA_bt_Selectfile.UseVisualStyleBackColor = true;
            DEV_UA_bt_Selectfile.Click += DEV_UA_bt_Selectfile_Click_1;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label22.Location = new Point(0, 6);
            label22.Name = "label22";
            label22.Size = new Size(37, 21);
            label22.TabIndex = 14;
            label22.Text = "File:";
            // 
            // DEV_UA_get_file
            // 
            DEV_UA_get_file.Location = new Point(40, 6);
            DEV_UA_get_file.Name = "DEV_UA_get_file";
            DEV_UA_get_file.Size = new Size(457, 23);
            DEV_UA_get_file.TabIndex = 13;
            // 
            // DEV_UA_bt_Patch
            // 
            DEV_UA_bt_Patch.Location = new Point(579, 7);
            DEV_UA_bt_Patch.Name = "DEV_UA_bt_Patch";
            DEV_UA_bt_Patch.Size = new Size(59, 23);
            DEV_UA_bt_Patch.TabIndex = 12;
            DEV_UA_bt_Patch.Text = "Patch";
            DEV_UA_bt_Patch.UseVisualStyleBackColor = true;
            DEV_UA_bt_Patch.Click += DEV_UA_bt_Patch_Click_1;
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
            // CekUpdateTT
            // 
            CekUpdateTT.Enabled = true;
            CekUpdateTT.Interval = 5000;
            CekUpdateTT.Tick += CekUpdateTT_Tick;
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
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            TabConfig.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            tabPage8.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStart;
        private TextBox GetServerHost;
        private Label label2;
        private TextBox GetProxyPort;
        private TabControl TabMain;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button Server_Start;
        private TabPage tabPage3;
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
        private TabPage tabPage8;
        private System.Windows.Forms.Timer CekUpdateTT;
        private GroupBox grExtra;
        private CheckBox Extra_AkebiGC;
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
        private TabControl tabControl1;
        private TabPage tabPage6;
        private Button DEV_MA_bt_Selectfile;
        private Label label9;
        private TextBox DEV_MA_get_file;
        private Button DEV_MA_bt_Patch;
        private TabPage tabPage7;
        private Label label10;
        private TextBox DEV_MA_Set_Key1_NoPatch;
        private TextBox DEV_MA_Set_Key1_Patch;
        private Label label11;
        private TextBox DEV_MA_Set_Key2_Patch;
        private TextBox DEV_MA_Set_Key2_NoPatch;
        private Label label13;
        private Label label12;
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
        private TextBox DEV_UA_Set_Key2_Patch;
        private Label label20;
        private TextBox DEV_UA_Set_Key1_NoPatch;
        private Label label21;
        private Button DEV_UA_bt_Selectfile;
        private Label label22;
        private TextBox DEV_UA_get_file;
        private Button DEV_UA_bt_Patch;
        private Button DEV_MA_bt_Decrypt;
        private CheckBox Is_ServerList_Autocheck;
        public ListView ServerList;
        private ColumnHeader ServerList_GetName;
        private ColumnHeader ServerList_GetHost;
        private ColumnHeader ServerList_GetOnline;
        private ColumnHeader ServerList_GetVersion;
        private ColumnHeader ServerList_GetPing;
        private Button btReloadServer;
        private Label label3;
        private Button btStartOfficialServer;
        private ComboBox GetTypeGame;
        private GroupBox grProfile;
        private ComboBox GetProfileServer;
        private Button btload;
        private GroupBox groupBox8;
        private GroupBox grConfigGameLite;
    }
}