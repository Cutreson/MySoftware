
namespace MySoftware.Class.View
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
            this.pbWindow = new Cyotek.Windows.Forms.ImageBox();
            this.SuspendLayout();
            // 
            // pbWindow
            // 
            this.pbWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.pbWindow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pbWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWindow.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.pbWindow.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.pbWindow.Location = new System.Drawing.Point(0, 0);
            this.pbWindow.Name = "pbWindow";
            this.pbWindow.Size = new System.Drawing.Size(400, 400);
            this.pbWindow.TabIndex = 1;
            this.pbWindow.Text = "Image Box";
            this.pbWindow.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbWindow.TextBackColor = System.Drawing.Color.White;
            this.pbWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseMove);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbWindow);
            this.Name = "Display";
            this.Size = new System.Drawing.Size(400, 400);
            this.ResumeLayout(false);

        }

        #endregion

        public Cyotek.Windows.Forms.ImageBox pbWindow;
    }
}
