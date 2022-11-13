
namespace MySoftware
{
    partial class FrmCamSetting
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
            this.cboCamType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.cboCamIP = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCamName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnSnap = new System.Windows.Forms.Button();
            this.hWindow = new MySoftware.Class.View.Display();
            this.SuspendLayout();
            // 
            // cboCamType
            // 
            this.cboCamType.FormattingEnabled = true;
            this.cboCamType.Items.AddRange(new object[] {
            "GigE_Basler",
            "GigE_Hikvision",
            "Image"});
            this.cboCamType.Location = new System.Drawing.Point(125, 45);
            this.cboCamType.Name = "cboCamType";
            this.cboCamType.Size = new System.Drawing.Size(287, 21);
            this.cboCamType.TabIndex = 79;
            this.cboCamType.SelectedIndexChanged += new System.EventHandler(this.cboCamType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Cam Type";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(436, 102);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 76;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(436, 71);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(75, 23);
            this.btnDetect.TabIndex = 73;
            this.btnDetect.Text = "Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // cboCamIP
            // 
            this.cboCamIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboCamIP.FormattingEnabled = true;
            this.cboCamIP.Location = new System.Drawing.Point(125, 103);
            this.cboCamIP.Name = "cboCamIP";
            this.cboCamIP.Size = new System.Drawing.Size(287, 21);
            this.cboCamIP.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "IP";
            // 
            // cboCamName
            // 
            this.cboCamName.FormattingEnabled = true;
            this.cboCamName.Location = new System.Drawing.Point(125, 72);
            this.cboCamName.Name = "cboCamName";
            this.cboCamName.Size = new System.Drawing.Size(287, 21);
            this.cboCamName.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Cam Name";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(436, 102);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 77;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(296, 205);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(40, 23);
            this.btnLive.TabIndex = 81;
            this.btnLive.Text = "Live";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnSnap
            // 
            this.btnSnap.Location = new System.Drawing.Point(250, 205);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(40, 23);
            this.btnSnap.TabIndex = 80;
            this.btnSnap.Text = "Snap";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // hWindow
            // 
            this.hWindow.Location = new System.Drawing.Point(533, 12);
            this.hWindow.Name = "hWindow";
            this.hWindow.Size = new System.Drawing.Size(487, 492);
            this.hWindow.TabIndex = 0;
            // 
            // FrmCamSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 516);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnSnap);
            this.Controls.Add(this.cboCamType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.cboCamIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboCamName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.hWindow);
            this.Name = "FrmCamSetting";
            this.Text = "FrmCamSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Class.View.Display hWindow;
        private System.Windows.Forms.ComboBox cboCamType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.ComboBox cboCamIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCamName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnSnap;
    }
}