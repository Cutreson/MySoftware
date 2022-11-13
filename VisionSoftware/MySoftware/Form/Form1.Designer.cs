namespace MySoftware
{
    partial class Form1
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
            this.contextMenuMainView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuMainView.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuMainView
            // 
            this.contextMenuMainView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.grabImageToolStripMenuItem});
            this.contextMenuMainView.Name = "contextMenuMainView";
            this.contextMenuMainView.Size = new System.Drawing.Size(140, 70);
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openImageToolStripMenuItem.Text = "Open Image";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // grabImageToolStripMenuItem
            // 
            this.grabImageToolStripMenuItem.Name = "grabImageToolStripMenuItem";
            this.grabImageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.grabImageToolStripMenuItem.Text = "Grab Image";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(335, 450);
            this.CloseButton = false;
            this.ContextMenuStrip = this.contextMenuMainView;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.TabText = "Form1";
            this.Text = "Form1";
            this.contextMenuMainView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuMainView;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grabImageToolStripMenuItem;
    }
}