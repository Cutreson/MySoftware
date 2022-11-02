
namespace MySoftware
{
    partial class FrmSettingCamera
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
            this.btnSnap = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.tabControlImage = new System.Windows.Forms.TabControl();
            this.tbAcqusition = new System.Windows.Forms.TabPage();
            this.cboCamType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoSetDefaultSettings = new System.Windows.Forms.CheckBox();
            this.RTCTriggerDelayRaw = new System.Windows.Forms.TextBox();
            this.RTCGainRaw = new System.Windows.Forms.TextBox();
            this.txtNumberBuffer = new System.Windows.Forms.TextBox();
            this.RTCExposureTimeRaw = new System.Windows.Forms.TextBox();
            this.btnLoadCameraDefaultSettings = new System.Windows.Forms.Button();
            this.btnSaveCameraDefaultSettings = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.cboCamIP = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCamName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGrabMode = new System.Windows.Forms.ComboBox();
            this.RTCTriggerActivation = new System.Windows.Forms.ComboBox();
            this.RTCTriggerSource = new System.Windows.Forms.ComboBox();
            this.RTCTriggerMode = new System.Windows.Forms.ComboBox();
            this.RTCAcquisitionMode = new System.Windows.Forms.ComboBox();
            this.RTCPixelFormat = new System.Windows.Forms.ComboBox();
            this.RTCExposureMode = new System.Windows.Forms.ComboBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tbSettingsImage = new System.Windows.Forms.TabPage();
            this.cboImageType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolderImage = new System.Windows.Forms.TextBox();
            this.ckbIsSaveImage = new System.Windows.Forms.CheckBox();
            this.btnLoadImageTest = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnLoadImages = new System.Windows.Forms.Button();
            this.lstImages = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hWindow = new MySoftware.Class.ClassView.Display();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.openImageFiles = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControlImage.SuspendLayout();
            this.tbAcqusition.SuspendLayout();
            this.tbSettingsImage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSnap
            // 
            this.btnSnap.Location = new System.Drawing.Point(1, 1);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(40, 23);
            this.btnSnap.TabIndex = 7;
            this.btnSnap.Text = "Snap";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnSaveSettings);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnLive);
            this.panel1.Controls.Add(this.btnSnap);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 26);
            this.panel1.TabIndex = 8;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(720, 1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 71;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveSettings.Location = new System.Drawing.Point(800, 1);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(100, 23);
            this.btnSaveSettings.TabIndex = 13;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(905, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(47, 1);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(40, 23);
            this.btnLive.TabIndex = 8;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // tabControlImage
            // 
            this.tabControlImage.Controls.Add(this.tbAcqusition);
            this.tabControlImage.Controls.Add(this.tbSettingsImage);
            this.tabControlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlImage.Location = new System.Drawing.Point(3, 3);
            this.tabControlImage.Name = "tabControlImage";
            this.tabControlImage.SelectedIndex = 0;
            this.tabControlImage.Size = new System.Drawing.Size(541, 491);
            this.tabControlImage.TabIndex = 9;
            // 
            // tbAcqusition
            // 
            this.tbAcqusition.Controls.Add(this.cboCamType);
            this.tbAcqusition.Controls.Add(this.label1);
            this.tbAcqusition.Controls.Add(this.chkAutoSetDefaultSettings);
            this.tbAcqusition.Controls.Add(this.RTCTriggerDelayRaw);
            this.tbAcqusition.Controls.Add(this.RTCGainRaw);
            this.tbAcqusition.Controls.Add(this.txtNumberBuffer);
            this.tbAcqusition.Controls.Add(this.RTCExposureTimeRaw);
            this.tbAcqusition.Controls.Add(this.btnLoadCameraDefaultSettings);
            this.tbAcqusition.Controls.Add(this.btnSaveCameraDefaultSettings);
            this.tbAcqusition.Controls.Add(this.btnConnect);
            this.tbAcqusition.Controls.Add(this.btnDetect);
            this.tbAcqusition.Controls.Add(this.cboCamIP);
            this.tbAcqusition.Controls.Add(this.label15);
            this.tbAcqusition.Controls.Add(this.label12);
            this.tbAcqusition.Controls.Add(this.label11);
            this.tbAcqusition.Controls.Add(this.label10);
            this.tbAcqusition.Controls.Add(this.label9);
            this.tbAcqusition.Controls.Add(this.label7);
            this.tbAcqusition.Controls.Add(this.label5);
            this.tbAcqusition.Controls.Add(this.label8);
            this.tbAcqusition.Controls.Add(this.label6);
            this.tbAcqusition.Controls.Add(this.label14);
            this.tbAcqusition.Controls.Add(this.label13);
            this.tbAcqusition.Controls.Add(this.label16);
            this.tbAcqusition.Controls.Add(this.label4);
            this.tbAcqusition.Controls.Add(this.label3);
            this.tbAcqusition.Controls.Add(this.cboCamName);
            this.tbAcqusition.Controls.Add(this.label2);
            this.tbAcqusition.Controls.Add(this.cbGrabMode);
            this.tbAcqusition.Controls.Add(this.RTCTriggerActivation);
            this.tbAcqusition.Controls.Add(this.RTCTriggerSource);
            this.tbAcqusition.Controls.Add(this.RTCTriggerMode);
            this.tbAcqusition.Controls.Add(this.RTCAcquisitionMode);
            this.tbAcqusition.Controls.Add(this.RTCPixelFormat);
            this.tbAcqusition.Controls.Add(this.RTCExposureMode);
            this.tbAcqusition.Controls.Add(this.btnDisconnect);
            this.tbAcqusition.Location = new System.Drawing.Point(4, 22);
            this.tbAcqusition.Name = "tbAcqusition";
            this.tbAcqusition.Padding = new System.Windows.Forms.Padding(3);
            this.tbAcqusition.Size = new System.Drawing.Size(533, 465);
            this.tbAcqusition.TabIndex = 0;
            this.tbAcqusition.Text = "Acqusition";
            this.tbAcqusition.UseVisualStyleBackColor = true;
            this.tbAcqusition.Click += new System.EventHandler(this.tbAcqusition_Click);
            // 
            // cboCamType
            // 
            this.cboCamType.FormattingEnabled = true;
            this.cboCamType.Items.AddRange(new object[] {
            "GigE_Basler",
            "GigE_Hikvision",
            "Image"});
            this.cboCamType.Location = new System.Drawing.Point(110, 19);
            this.cboCamType.Name = "cboCamType";
            this.cboCamType.Size = new System.Drawing.Size(287, 21);
            this.cboCamType.TabIndex = 70;
            this.cboCamType.SelectedIndexChanged += new System.EventHandler(this.cboCamType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Cam Type";
            // 
            // chkAutoSetDefaultSettings
            // 
            this.chkAutoSetDefaultSettings.AutoSize = true;
            this.chkAutoSetDefaultSettings.Location = new System.Drawing.Point(17, 333);
            this.chkAutoSetDefaultSettings.Name = "chkAutoSetDefaultSettings";
            this.chkAutoSetDefaultSettings.Size = new System.Drawing.Size(220, 17);
            this.chkAutoSetDefaultSettings.TabIndex = 61;
            this.chkAutoSetDefaultSettings.Text = "Auto Set Default Settings When Connect";
            this.chkAutoSetDefaultSettings.UseVisualStyleBackColor = true;
            // 
            // RTCTriggerDelayRaw
            // 
            this.RTCTriggerDelayRaw.Location = new System.Drawing.Point(110, 192);
            this.RTCTriggerDelayRaw.Name = "RTCTriggerDelayRaw";
            this.RTCTriggerDelayRaw.Size = new System.Drawing.Size(105, 20);
            this.RTCTriggerDelayRaw.TabIndex = 50;
            // 
            // RTCGainRaw
            // 
            this.RTCGainRaw.Location = new System.Drawing.Point(110, 165);
            this.RTCGainRaw.Name = "RTCGainRaw";
            this.RTCGainRaw.Size = new System.Drawing.Size(105, 20);
            this.RTCGainRaw.TabIndex = 46;
            // 
            // txtNumberBuffer
            // 
            this.txtNumberBuffer.Location = new System.Drawing.Point(371, 387);
            this.txtNumberBuffer.Name = "txtNumberBuffer";
            this.txtNumberBuffer.Size = new System.Drawing.Size(125, 20);
            this.txtNumberBuffer.TabIndex = 66;
            // 
            // RTCExposureTimeRaw
            // 
            this.RTCExposureTimeRaw.Location = new System.Drawing.Point(110, 138);
            this.RTCExposureTimeRaw.Name = "RTCExposureTimeRaw";
            this.RTCExposureTimeRaw.Size = new System.Drawing.Size(105, 20);
            this.RTCExposureTimeRaw.TabIndex = 42;
            // 
            // btnLoadCameraDefaultSettings
            // 
            this.btnLoadCameraDefaultSettings.Enabled = false;
            this.btnLoadCameraDefaultSettings.Location = new System.Drawing.Point(157, 290);
            this.btnLoadCameraDefaultSettings.Name = "btnLoadCameraDefaultSettings";
            this.btnLoadCameraDefaultSettings.Size = new System.Drawing.Size(172, 23);
            this.btnLoadCameraDefaultSettings.TabIndex = 60;
            this.btnLoadCameraDefaultSettings.Text = "Load Default Settings";
            this.btnLoadCameraDefaultSettings.UseVisualStyleBackColor = true;
            // 
            // btnSaveCameraDefaultSettings
            // 
            this.btnSaveCameraDefaultSettings.Enabled = false;
            this.btnSaveCameraDefaultSettings.Location = new System.Drawing.Point(17, 290);
            this.btnSaveCameraDefaultSettings.Name = "btnSaveCameraDefaultSettings";
            this.btnSaveCameraDefaultSettings.Size = new System.Drawing.Size(110, 23);
            this.btnSaveCameraDefaultSettings.TabIndex = 59;
            this.btnSaveCameraDefaultSettings.Text = "Save To Default";
            this.btnSaveCameraDefaultSettings.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(421, 76);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 39;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(421, 45);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(75, 23);
            this.btnDetect.TabIndex = 36;
            this.btnDetect.Text = "Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // cboCamIP
            // 
            this.cboCamIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboCamIP.FormattingEnabled = true;
            this.cboCamIP.Location = new System.Drawing.Point(110, 77);
            this.cboCamIP.Name = "cboCamIP";
            this.cboCamIP.Size = new System.Drawing.Size(287, 21);
            this.cboCamIP.TabIndex = 38;
            this.cboCamIP.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 387);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 63;
            this.label15.Text = "Mode";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 248);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Trigger Activation";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(262, 226);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 55;
            this.label11.Text = "Trigger Source";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 221);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Trigger Mode";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(262, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Acquisition Mode";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(262, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Pixel Format";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Exposure Mode";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Trigger Delay";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Gain";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(14, 361);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Grab Settings:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Camera Settings:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(254, 387);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 65;
            this.label16.Text = "Number Buffer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Exposure Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "IP";
            // 
            // cboCamName
            // 
            this.cboCamName.FormattingEnabled = true;
            this.cboCamName.Location = new System.Drawing.Point(110, 46);
            this.cboCamName.Name = "cboCamName";
            this.cboCamName.Size = new System.Drawing.Size(287, 21);
            this.cboCamName.TabIndex = 35;
            this.cboCamName.SelectedIndexChanged += new System.EventHandler(this.cboInCamNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Cam Name";
            // 
            // cbGrabMode
            // 
            this.cbGrabMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrabMode.FormattingEnabled = true;
            this.cbGrabMode.Items.AddRange(new object[] {
            "Sync",
            "ASync"});
            this.cbGrabMode.Location = new System.Drawing.Point(110, 384);
            this.cbGrabMode.Name = "cbGrabMode";
            this.cbGrabMode.Size = new System.Drawing.Size(105, 21);
            this.cbGrabMode.TabIndex = 64;
            // 
            // RTCTriggerActivation
            // 
            this.RTCTriggerActivation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCTriggerActivation.FormattingEnabled = true;
            this.RTCTriggerActivation.Location = new System.Drawing.Point(110, 245);
            this.RTCTriggerActivation.Name = "RTCTriggerActivation";
            this.RTCTriggerActivation.Size = new System.Drawing.Size(105, 21);
            this.RTCTriggerActivation.TabIndex = 58;
            // 
            // RTCTriggerSource
            // 
            this.RTCTriggerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCTriggerSource.FormattingEnabled = true;
            this.RTCTriggerSource.Location = new System.Drawing.Point(371, 224);
            this.RTCTriggerSource.Name = "RTCTriggerSource";
            this.RTCTriggerSource.Size = new System.Drawing.Size(125, 21);
            this.RTCTriggerSource.TabIndex = 56;
            // 
            // RTCTriggerMode
            // 
            this.RTCTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCTriggerMode.FormattingEnabled = true;
            this.RTCTriggerMode.Location = new System.Drawing.Point(110, 218);
            this.RTCTriggerMode.Name = "RTCTriggerMode";
            this.RTCTriggerMode.Size = new System.Drawing.Size(105, 21);
            this.RTCTriggerMode.TabIndex = 54;
            // 
            // RTCAcquisitionMode
            // 
            this.RTCAcquisitionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCAcquisitionMode.FormattingEnabled = true;
            this.RTCAcquisitionMode.Location = new System.Drawing.Point(371, 195);
            this.RTCAcquisitionMode.Name = "RTCAcquisitionMode";
            this.RTCAcquisitionMode.Size = new System.Drawing.Size(125, 21);
            this.RTCAcquisitionMode.TabIndex = 52;
            // 
            // RTCPixelFormat
            // 
            this.RTCPixelFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCPixelFormat.FormattingEnabled = true;
            this.RTCPixelFormat.Location = new System.Drawing.Point(371, 166);
            this.RTCPixelFormat.Name = "RTCPixelFormat";
            this.RTCPixelFormat.Size = new System.Drawing.Size(125, 21);
            this.RTCPixelFormat.TabIndex = 48;
            // 
            // RTCExposureMode
            // 
            this.RTCExposureMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RTCExposureMode.FormattingEnabled = true;
            this.RTCExposureMode.Location = new System.Drawing.Point(371, 139);
            this.RTCExposureMode.Name = "RTCExposureMode";
            this.RTCExposureMode.Size = new System.Drawing.Size(125, 21);
            this.RTCExposureMode.TabIndex = 44;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(421, 76);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 67;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // tbSettingsImage
            // 
            this.tbSettingsImage.Controls.Add(this.cboImageType);
            this.tbSettingsImage.Controls.Add(this.label17);
            this.tbSettingsImage.Controls.Add(this.btnSelectFolder);
            this.tbSettingsImage.Controls.Add(this.txtFolderImage);
            this.tbSettingsImage.Controls.Add(this.ckbIsSaveImage);
            this.tbSettingsImage.Controls.Add(this.btnLoadImageTest);
            this.tbSettingsImage.Controls.Add(this.btnMoveUp);
            this.tbSettingsImage.Controls.Add(this.btnMoveDown);
            this.tbSettingsImage.Controls.Add(this.btnRemove);
            this.tbSettingsImage.Controls.Add(this.btnClearAll);
            this.tbSettingsImage.Controls.Add(this.btnLoadImages);
            this.tbSettingsImage.Controls.Add(this.lstImages);
            this.tbSettingsImage.Location = new System.Drawing.Point(4, 22);
            this.tbSettingsImage.Name = "tbSettingsImage";
            this.tbSettingsImage.Padding = new System.Windows.Forms.Padding(3);
            this.tbSettingsImage.Size = new System.Drawing.Size(533, 465);
            this.tbSettingsImage.TabIndex = 1;
            this.tbSettingsImage.Text = "Settings";
            this.tbSettingsImage.UseVisualStyleBackColor = true;
            // 
            // cboImageType
            // 
            this.cboImageType.FormattingEnabled = true;
            this.cboImageType.Items.AddRange(new object[] {
            "bmp",
            "jpg",
            "png"});
            this.cboImageType.Location = new System.Drawing.Point(470, 41);
            this.cboImageType.Name = "cboImageType";
            this.cboImageType.Size = new System.Drawing.Size(52, 21);
            this.cboImageType.TabIndex = 20;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(399, 45);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "ImageType";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(359, 40);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(28, 23);
            this.btnSelectFolder.TabIndex = 18;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolderImage
            // 
            this.txtFolderImage.Location = new System.Drawing.Point(122, 41);
            this.txtFolderImage.Name = "txtFolderImage";
            this.txtFolderImage.Size = new System.Drawing.Size(235, 20);
            this.txtFolderImage.TabIndex = 17;
            // 
            // ckbIsSaveImage
            // 
            this.ckbIsSaveImage.AutoSize = true;
            this.ckbIsSaveImage.Location = new System.Drawing.Point(10, 43);
            this.ckbIsSaveImage.Name = "ckbIsSaveImage";
            this.ckbIsSaveImage.Size = new System.Drawing.Size(83, 17);
            this.ckbIsSaveImage.TabIndex = 16;
            this.ckbIsSaveImage.Text = "Save Image";
            this.ckbIsSaveImage.UseVisualStyleBackColor = true;
            // 
            // btnLoadImageTest
            // 
            this.btnLoadImageTest.Location = new System.Drawing.Point(96, 6);
            this.btnLoadImageTest.Name = "btnLoadImageTest";
            this.btnLoadImageTest.Size = new System.Drawing.Size(87, 23);
            this.btnLoadImageTest.TabIndex = 11;
            this.btnLoadImageTest.Text = "Load Images Test";
            this.btnLoadImageTest.UseVisualStyleBackColor = true;
            this.btnLoadImageTest.Click += new System.EventHandler(this.btnLoadImageTest_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(251, 6);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(60, 23);
            this.btnMoveUp.TabIndex = 7;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(317, 6);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(78, 23);
            this.btnMoveDown.TabIndex = 8;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(401, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(60, 23);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(467, 6);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(60, 23);
            this.btnClearAll.TabIndex = 10;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnLoadImages
            // 
            this.btnLoadImages.Location = new System.Drawing.Point(3, 6);
            this.btnLoadImages.Name = "btnLoadImages";
            this.btnLoadImages.Size = new System.Drawing.Size(87, 23);
            this.btnLoadImages.TabIndex = 6;
            this.btnLoadImages.Text = "Load Images";
            this.btnLoadImages.UseVisualStyleBackColor = true;
            this.btnLoadImages.Click += new System.EventHandler(this.btnLoadImages_Click);
            // 
            // lstImages
            // 
            this.lstImages.FormattingEnabled = true;
            this.lstImages.Location = new System.Drawing.Point(3, 74);
            this.lstImages.Name = "lstImages";
            this.lstImages.Size = new System.Drawing.Size(524, 394);
            this.lstImages.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.98772F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.01228F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(977, 497);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.hWindow);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(550, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 491);
            this.panel2.TabIndex = 10;
            // 
            // hWindow
            // 
            this.hWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindow.IsEnableDrawing = false;
            this.hWindow.Location = new System.Drawing.Point(0, 21);
            this.hWindow.Name = "hWindow";
            this.hWindow.Size = new System.Drawing.Size(424, 470);
            this.hWindow.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Acqusition",
            "Live"});
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(424, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // openImageFiles
            // 
            this.openImageFiles.Filter = "Choose Image Files (*.bmp;*.jpg;*.png;*.tif)|*.bmp;*.jpg*.png;*.tif";
            this.openImageFiles.Multiselect = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(985, 549);
            this.panel3.TabIndex = 11;
            // 
            // FrmSettingCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(985, 549);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(1001, 588);
            this.MinimumSize = new System.Drawing.Size(1001, 588);
            this.Name = "FrmSettingCamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting Camera";
            this.Load += new System.EventHandler(this.FrmSettingCamera_Load);
            this.panel1.ResumeLayout(false);
            this.tabControlImage.ResumeLayout(false);
            this.tbAcqusition.ResumeLayout(false);
            this.tbAcqusition.PerformLayout();
            this.tbSettingsImage.ResumeLayout(false);
            this.tbSettingsImage.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSnap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.TabControl tabControlImage;
        private System.Windows.Forms.TabPage tbAcqusition;
        private System.Windows.Forms.TabPage tbSettingsImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkAutoSetDefaultSettings;
        private System.Windows.Forms.TextBox RTCTriggerDelayRaw;
        private System.Windows.Forms.TextBox RTCGainRaw;
        private System.Windows.Forms.TextBox txtNumberBuffer;
        private System.Windows.Forms.TextBox RTCExposureTimeRaw;
        private System.Windows.Forms.Button btnLoadCameraDefaultSettings;
        private System.Windows.Forms.Button btnSaveCameraDefaultSettings;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.ComboBox cboCamIP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCamName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbGrabMode;
        private System.Windows.Forms.ComboBox RTCTriggerActivation;
        private System.Windows.Forms.ComboBox RTCTriggerSource;
        private System.Windows.Forms.ComboBox RTCTriggerMode;
        private System.Windows.Forms.ComboBox RTCAcquisitionMode;
        private System.Windows.Forms.ComboBox RTCPixelFormat;
        private System.Windows.Forms.ComboBox RTCExposureMode;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.ComboBox cboCamType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnLoadImages;
        private System.Windows.Forms.ListBox lstImages;
        private System.Windows.Forms.OpenFileDialog openImageFiles;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLoadImageTest;
        private System.Windows.Forms.ComboBox cboImageType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.TextBox txtFolderImage;
        private System.Windows.Forms.CheckBox ckbIsSaveImage;
        private Class.ClassView.Display hWindow;
    }
}