
namespace MySoftware.Camera
{
    partial class CameraLive
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraLive));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOneShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonContinuousShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.panel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.deviceListView = new System.Windows.Forms.ListView();
            this.comboBoxTestImage = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.comboBoxPixelFormat = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.sliderHeight = new PylonC.NETSupportLibrary.SliderUserControl();
            this.sliderWidth = new PylonC.NETSupportLibrary.SliderUserControl();
            this.sliderExposureTime = new PylonC.NETSupportLibrary.SliderUserControl();
            this.sliderGain = new PylonC.NETSupportLibrary.SliderUserControl();
            this.updateDeviceListTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOneShot,
            this.toolStripButtonContinuousShot,
            this.toolStripButtonStop});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(284, 39);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonOneShot
            // 
            this.toolStripButtonOneShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOneShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOneShot.Image")));
            this.toolStripButtonOneShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOneShot.Name = "toolStripButtonOneShot";
            this.toolStripButtonOneShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonOneShot.Text = "One Shot";
            this.toolStripButtonOneShot.ToolTipText = "One Shot";
            this.toolStripButtonOneShot.Click += new System.EventHandler(this.toolStripButtonOneShot_Click);
            // 
            // toolStripButtonContinuousShot
            // 
            this.toolStripButtonContinuousShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonContinuousShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonContinuousShot.Image")));
            this.toolStripButtonContinuousShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContinuousShot.Name = "toolStripButtonContinuousShot";
            this.toolStripButtonContinuousShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonContinuousShot.Text = "Continuous Shot";
            this.toolStripButtonContinuousShot.ToolTipText = "Continuous Shot";
            this.toolStripButtonContinuousShot.Click += new System.EventHandler(this.toolStripButtonContinuousShot_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonStop.Text = "Stop Grab";
            this.toolStripButtonStop.ToolTipText = "Stop Grab";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.splitContainer1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 39);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(284, 522);
            this.panel.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.deviceListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxTestImage);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxPixelFormat);
            this.splitContainer1.Panel2.Controls.Add(this.sliderHeight);
            this.splitContainer1.Panel2.Controls.Add(this.sliderWidth);
            this.splitContainer1.Panel2.Controls.Add(this.sliderExposureTime);
            this.splitContainer1.Panel2.Controls.Add(this.sliderGain);
            this.splitContainer1.Size = new System.Drawing.Size(284, 522);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.TabIndex = 13;
            // 
            // deviceListView
            // 
            this.deviceListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceListView.HideSelection = false;
            this.deviceListView.Location = new System.Drawing.Point(0, 0);
            this.deviceListView.MultiSelect = false;
            this.deviceListView.Name = "deviceListView";
            this.deviceListView.ShowItemToolTips = true;
            this.deviceListView.Size = new System.Drawing.Size(284, 145);
            this.deviceListView.TabIndex = 1;
            this.deviceListView.UseCompatibleStateImageBehavior = false;
            this.deviceListView.View = System.Windows.Forms.View.Tile;
            this.deviceListView.SelectedIndexChanged += new System.EventHandler(this.deviceListView_SelectedIndexChanged);
            // 
            // comboBoxTestImage
            // 
            this.comboBoxTestImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTestImage.Location = new System.Drawing.Point(22, 8);
            this.comboBoxTestImage.MinimumSize = new System.Drawing.Size(150, 39);
            this.comboBoxTestImage.Name = "comboBoxTestImage";
            this.comboBoxTestImage.NodeName = "TestImageSelector";
            this.comboBoxTestImage.Size = new System.Drawing.Size(199, 46);
            this.comboBoxTestImage.TabIndex = 12;
            // 
            // comboBoxPixelFormat
            // 
            this.comboBoxPixelFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPixelFormat.Location = new System.Drawing.Point(22, 61);
            this.comboBoxPixelFormat.MinimumSize = new System.Drawing.Size(150, 39);
            this.comboBoxPixelFormat.Name = "comboBoxPixelFormat";
            this.comboBoxPixelFormat.NodeName = "PixelFormat";
            this.comboBoxPixelFormat.Size = new System.Drawing.Size(199, 46);
            this.comboBoxPixelFormat.TabIndex = 11;
            // 
            // sliderHeight
            // 
            this.sliderHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderHeight.Location = new System.Drawing.Point(10, 280);
            this.sliderHeight.MaximumSize = new System.Drawing.Size(875, 50);
            this.sliderHeight.MinimumSize = new System.Drawing.Size(225, 50);
            this.sliderHeight.Name = "sliderHeight";
            this.sliderHeight.NodeName = "Height";
            this.sliderHeight.Size = new System.Drawing.Size(230, 50);
            this.sliderHeight.TabIndex = 10;
            // 
            // sliderWidth
            // 
            this.sliderWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderWidth.Location = new System.Drawing.Point(10, 224);
            this.sliderWidth.MaximumSize = new System.Drawing.Size(875, 50);
            this.sliderWidth.MinimumSize = new System.Drawing.Size(225, 50);
            this.sliderWidth.Name = "sliderWidth";
            this.sliderWidth.NodeName = "Width";
            this.sliderWidth.Size = new System.Drawing.Size(230, 50);
            this.sliderWidth.TabIndex = 9;
            // 
            // sliderExposureTime
            // 
            this.sliderExposureTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderExposureTime.Location = new System.Drawing.Point(10, 168);
            this.sliderExposureTime.MaximumSize = new System.Drawing.Size(875, 50);
            this.sliderExposureTime.MinimumSize = new System.Drawing.Size(225, 50);
            this.sliderExposureTime.Name = "sliderExposureTime";
            this.sliderExposureTime.NodeName = "ExposureTimeRaw";
            this.sliderExposureTime.Size = new System.Drawing.Size(230, 50);
            this.sliderExposureTime.TabIndex = 8;
            // 
            // sliderGain
            // 
            this.sliderGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderGain.Location = new System.Drawing.Point(10, 112);
            this.sliderGain.MaximumSize = new System.Drawing.Size(875, 50);
            this.sliderGain.MinimumSize = new System.Drawing.Size(225, 50);
            this.sliderGain.Name = "sliderGain";
            this.sliderGain.NodeName = "GainRaw";
            this.sliderGain.Size = new System.Drawing.Size(230, 50);
            this.sliderGain.TabIndex = 7;
            // 
            // updateDeviceListTimer
            // 
            this.updateDeviceListTimer.Enabled = true;
            this.updateDeviceListTimer.Interval = 5000;
            // 
            // CameraLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.toolStrip);
            this.Name = "CameraLive";
            this.TabText = "Camera Live";
            this.Text = "Camera Live";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonOneShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonContinuousShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView deviceListView;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxTestImage;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxPixelFormat;
        private PylonC.NETSupportLibrary.SliderUserControl sliderHeight;
        private PylonC.NETSupportLibrary.SliderUserControl sliderWidth;
        private PylonC.NETSupportLibrary.SliderUserControl sliderExposureTime;
        private PylonC.NETSupportLibrary.SliderUserControl sliderGain;
        private System.Windows.Forms.Timer updateDeviceListTimer;
    }
}