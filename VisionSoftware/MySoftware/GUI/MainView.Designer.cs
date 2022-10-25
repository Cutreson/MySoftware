
namespace MySoftware.GUI
{
    partial class MainView
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
            this.proPictureBox1 = new ProPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.proPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // proPictureBox1
            // 
            this.proPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proPictureBox1.ErrorImage = global::MySoftware.Properties.Resources.picture;
            this.proPictureBox1.Image = global::MySoftware.Properties.Resources.picture;
            this.proPictureBox1.InitialImage = global::MySoftware.Properties.Resources.picture;
            this.proPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.proPictureBox1.Name = "proPictureBox1";
            this.proPictureBox1.Size = new System.Drawing.Size(600, 600);
            this.proPictureBox1.TabIndex = 0;
            this.proPictureBox1.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.proPictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainView";
            this.TabText = "MainView";
            this.Text = "MainView";
            ((System.ComponentModel.ISupportInitialize)(this.proPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProPictureBox proPictureBox1;
    }
}