namespace MySoftware.GUI
{
    partial class MainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolLogin = new System.Windows.Forms.ToolStripDropDownButton();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(884, 561);
            this.dockPanel.TabIndex = 0;
            // 
            // toolLogin
            // 
            this.toolLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolLogin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.toolLogin.Image = ((System.Drawing.Image)(resources.GetObject("toolLogin.Image")));
            this.toolLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLogin.Name = "toolLogin";
            this.toolLogin.Size = new System.Drawing.Size(29, 23);
            this.toolLogin.Text = "Login";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.loginToolStripMenuItem.Text = "Login";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.logoutToolStripMenuItem.Text = "Logout";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(51, 23);
            this.toolStripButton1.Text = "Teach";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(75, 23);
            this.toolStripButton2.Text = "Operator";
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLogin,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(884, 26);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dockPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripDropDownButton toolLogin;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip;
    }
}