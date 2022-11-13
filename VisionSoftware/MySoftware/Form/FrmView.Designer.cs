
namespace MySoftware
{
    partial class FrmView
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
            this.hWindow = new MySoftware.Class.View.Display();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindow
            // 
            this.hWindow.ContextMenuStrip = this.contextMenu;
            this.hWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindow.Location = new System.Drawing.Point(0, 0);
            this.hWindow.Margin = new System.Windows.Forms.Padding(4);
            this.hWindow.Name = "hWindow";
            this.hWindow.Size = new System.Drawing.Size(584, 561);
            this.hWindow.TabIndex = 1;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.grabImageToolStripMenuItem,
            this.liveToolStripMenuItem});
            this.contextMenu.Name = "contextMenuMainView";
            this.contextMenu.Size = new System.Drawing.Size(181, 114);
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // grabImageToolStripMenuItem
            // 
            this.grabImageToolStripMenuItem.Name = "grabImageToolStripMenuItem";
            this.grabImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.grabImageToolStripMenuItem.Text = "Grab Image";
            this.grabImageToolStripMenuItem.Click += new System.EventHandler(this.grabImageToolStripMenuItem_Click);
            // 
            // liveToolStripMenuItem
            // 
            this.liveToolStripMenuItem.Name = "liveToolStripMenuItem";
            this.liveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.liveToolStripMenuItem.Text = "Live";
            this.liveToolStripMenuItem.Click += new System.EventHandler(this.liveToolStripMenuItem_Click);
            // 
            // FrmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.hWindow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmView";
            this.TabText = "Main View";
            this.Text = "Main View";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Class.View.Display hWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grabImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveToolStripMenuItem;
    }
}