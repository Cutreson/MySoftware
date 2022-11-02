using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware.Class.ClassView
{
    public partial class Display : UserControl
    {
        private Random random = new Random();
        private Point point;
        private Point pointOffset;

        private Rectangle rect;
        List<Rectangle>  Rects;
        public Rectangle GetRectangle()
        {
            int ratioX = 1, ratioY = 1 ;
            int widthImg = 1, heightImg= 1;
            if (pbWindow.Image !=null)
            {
                widthImg = pbWindow.Image.Width;
                heightImg = pbWindow.Image.Height;
                ratioY = pbWindow.Image.Width / pbWindow.Width;
                ratioX = pbWindow.Image.Height / pbWindow.Height;
            }
            //return new Rectangle(rect.X* ratioX, rect.Y *ratioY, rect.Width * ratioY, rect.Height * ratioY);

            bool bSelect = false;
            bool bBackground = true;
            Bitmap temp = null;
            double scale = 1;
            int offsetX = 0;
            int offsetY = 0;

            if (bBackground)
            {
                scale = (double)heightImg / pbWindow.ClientRectangle.Height;
                double s2 = (double)widthImg / pbWindow.ClientRectangle.Width;
                if (s2 > scale)
                {
                    scale = s2;
                    int picH = (int)(heightImg / scale);
                    offsetY = (pbWindow.Height - picH) / 2;
                }
                else
                {
                    int picW = (int)(widthImg / scale);
                    offsetX = (pbWindow.Width - picW) / 2;
                }

            }
            Rectangle roiSelect = new Rectangle((int)((rect.X - offsetX) * scale), (int)((rect.Y - offsetY) * scale), (int)(rect.Width * scale), (int)(rect.Height * scale));

            return roiSelect;
        }
        public Rectangle GetRectangle_OnPictureBox(Rectangle rect)
        {
            int ratioX = 1, ratioY = 1;
            int widthImg = 1, heightImg = 1;
            if (pbWindow.Image != null)
            {
                widthImg = pbWindow.Image.Width;
                heightImg = pbWindow.Image.Height;
                ratioY = pbWindow.Image.Width / pbWindow.ClientSize.Width;
                ratioX = pbWindow.Image.Height / pbWindow.ClientSize.Height;
            }
            //return new Rectangle(rect.X* ratioX, rect.Y *ratioY, rect.Width * ratioY, rect.Height * ratioY);

            bool bSelect = false;
            bool bBackground = true;
            Bitmap temp = null;
            double scale = 1;
            int offsetX = 0;
            int offsetY = 0;

            if (bBackground)
            {
                scale = (double)heightImg / pbWindow.ClientRectangle.Height;
                double s2 = (double)widthImg / pbWindow.ClientRectangle.Width;

                if (s2 > scale)
                {
                    scale = s2;
                    int picH = (int)(heightImg);
                    offsetY =  (int)(pbWindow.ClientSize.Height * (scale) - picH) / 2;
                }
                else
                {
                    int picW = (int)(widthImg);
                    //offsetX = (pbWindow.Width * scale - picW) / 2;
                }

            }
            Rectangle roiSelect = new Rectangle((int)((rect.X + offsetX) / scale), (int)((rect.Y + offsetY) / scale), (int)(rect.Width / scale), (int)(rect.Height / scale));

            return roiSelect;
        }
        public Display()
        {
            InitializeComponent();
        }
        private bool isEnableDrawing;
        public bool IsEnableDrawing
        {
            get
            {
                return isEnableDrawing;
            }
            set
            {
                isEnableDrawing = value;
            }
        }

        private void pbWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEnableDrawing)
            {
                //set the bottom right one
                if (e.Button == MouseButtons.Left)
                {
                    rect = new Rectangle(point.X, point.Y, e.X - point.X, e.Y - point.Y);
                    this.pbWindow.Invalidate();
                }

                if (e.Button == MouseButtons.Right && rect.Width > 0 && rect.Height > 0)
                {
                    //move the rectangle
                    if (rect.Contains(e.Location))
                    {
                        rect.Location = new Point(e.X - pointOffset.X, e.Y - pointOffset.Y);

                        this.pbWindow.Invalidate();
                    }
                }
            }
        }

        private void pbWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (isEnableDrawing)
            {
                if (e.Button == MouseButtons.Left)
                    point = e.Location;

                //set inner offset for moving
                if (e.Button == MouseButtons.Right && rect.Width > 0 && rect.Height > 0)
                {
                    if (rect.Contains(e.Location))
                        pointOffset = new Point(e.X - rect.X, e.Y - rect.Y);
                }
            }
            //set the upper left point of our selection rectangle
            
        }

        private void pbWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (isEnableDrawing)
            {
                if (e.Button == MouseButtons.Left)
                {
                    rect = new Rectangle(point.X, point.Y, e.X - point.X, e.Y - point.Y);
                    //Rects.Add(rect);
                    GenerateBmp();
                }

                if (e.Button == MouseButtons.Right && rect.Width > 0 && rect.Height > 0)
                {
                    if (rect.Contains(e.Location))
                    {
                        rect.Location = new Point(e.X - pointOffset.X, e.Y - pointOffset.Y);

                        //redisplay selection image with new values
                        GenerateBmp();
                    }
                }
            }
        }

        private void pbWindow_Paint(object sender, PaintEventArgs e)
        {
            if (isEnableDrawing)
            {
                if (rect.Width > 0 && rect.Height > 0)
                {
                    //fill
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(95, 255, 255, 255)))
                        e.Graphics.FillRectangle(sb, rect);
                    //draw
                    e.Graphics.DrawRectangle(Pens.Blue, rect);
                }
            }
            
            //if (Rects == null)
            //{
            //    Rects = new List<Rectangle>();
            //}
            //int rectCount = Rects.Count;
            //if (rectCount > 0)
            //{
            //    for (int i = 0; i < rectCount; i++)
            //    {
            //        Rectangle newRect = new Rectangle();
            //        newRect = Rects[i];
            //        if (newRect.Width > 0 && newRect.Height > 0)
            //        {
            //            //fill
            //            using (SolidBrush sb = new SolidBrush(Color.FromArgb(95, 255, 255, 255)))
            //                e.Graphics.FillRectangle(sb, newRect);
            //            //draw
            //            e.Graphics.DrawRectangle(Pens.Blue, newRect);
            //        }
            //    }
            //}
        }
        Graphics Graphic;
        private bool IsDrawOnly;

        public void DrawGraphics(Rectangle rect)
        {
            Pen pen = new Pen(Color.Red);
            SolidBrush sbb = new SolidBrush(Color.FromArgb(95, 255, 100, 255));
            if (rect==null)
            {
                new Rectangle(0, 0, 500, 600);
            }

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (formGraphics == null)
                    {
                        formGraphics = pbWindow.CreateGraphics();
                    }
                    formGraphics.FillRectangle(sbb, rect);
                    formGraphics.DrawRectangle(pen, rect);


                    //myBrush.Dispose();
                    //formGraphics.Dispose();

                });
            }
            else
            {
                if (formGraphics == null)
                {
                    formGraphics = pbWindow.CreateGraphics();
                }

                formGraphics.FillRectangle(sbb, rect);
                formGraphics.DrawRectangle(pen, rect);
                //myBrush.Dispose();
                //formGraphics.Dispose();
            }
            
        }
        public void drawZigZag(Panel p, int direction) // 1 = right, -1 = left
        {
            Graphics g = p.CreateGraphics();

            g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)), p.DisplayRectangle);

            Point[] points = new Point[4];

            points[0] = new Point(0, 0);
            points[1] = new Point(0, p.Height);
            points[2] = new Point(p.Width, p.Height);
            points[3] = new Point(p.Width, 0);

            Brush brush = new SolidBrush(Color.DarkGreen);

            g.FillPolygon(brush, points);
        }
        private void GenerateBmp()
        {
            //check, if we have a valid rectangle
            if (isEnableDrawing)
            {
                if (rect.Width > 0 && rect.Height > 0)
                {
                    //create a new Bitmap with the size of the selection-rectangle
                    Bitmap bmp = new Bitmap(rect.Width, rect.Height);

                    //draw the selectex part of the original image
                    using (Graphics g = Graphics.FromImage(bmp))
                        g.DrawImage(this.pbWindow.Image, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);

                    //grab the old image of the picturebox
                    //Image bOld = this.pictureBox2.Image;

                    ////assign our new one
                    //this.pictureBox2.Image = bmp;

                    //dispose the old one, if not null (previously no image assigned to that control)
                    //if (bOld != null)
                    //    bOld.Dispose();
                }
            }
            
        }
        private Bitmap SetUpPictures(PictureBox pb)
        {
            //create a bitmap to display
            Bitmap bmp1 = new Bitmap(pb.ClientSize.Width, pb.ClientSize.Height);

            //get the graphics-context
            using (Graphics g = Graphics.FromImage(bmp1))
            {
                //get a random, opaque, color
                Color c = Color.FromArgb(255, random.Next(256), random.Next(256), random.Next(256));
                g.Clear(c);

                //better smoothinmode for round shapes
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //draw ten shapes to the bitmap
                for (int i = 0; i < 10; i++)
                {
                    //loaction and size rectangle
                    Rectangle r = new Rectangle(random.Next(pb.ClientSize.Width / 2), random.Next(pb.ClientSize.Height / 2),
                        random.Next(pb.ClientSize.Width / 2), random.Next(pb.ClientSize.Height / 2));

                    //random color
                    Color c2 = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));

                    //one color brush
                    using (SolidBrush sb = new SolidBrush(c2))
                    {
                        //check, if i is odd or even and decide on that to draw rectangles or ellipses
                        if ((i & 0x01) == 1)
                            g.FillEllipse(sb, r);
                        else
                            g.FillRectangle(sb, r);
                    }
                }
            }

            //return our artwork
            return bmp1;
        }

        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        System.Drawing.Graphics formGraphics;

        
    }
}
