
namespace MySoftware.Class.ClassView
{
    partial class Display
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbWindow = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWindow
            // 
            this.pbWindow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWindow.Location = new System.Drawing.Point(0, 0);
            this.pbWindow.Name = "pbWindow";
            this.pbWindow.Size = new System.Drawing.Size(582, 406);
            this.pbWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWindow.TabIndex = 0;
            this.pbWindow.TabStop = false;
            this.pbWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.pbWindow_Paint);
            this.pbWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbWindow_MouseDown);
            this.pbWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbWindow_MouseMove);
            this.pbWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbWindow_MouseUp);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbWindow);
            this.Name = "Display";
            this.Size = new System.Drawing.Size(582, 406);
            ((System.ComponentModel.ISupportInitialize)(this.pbWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbWindow;
    }
}
