using System;
using System.Drawing;

namespace MySoftware.Camera
{
    public class ImageReadyEventArgs : EventArgs
    {
        public Image ImgSrc
        {
            get;
            set;
        }
        public ImageReadyEventArgs(Image img)
        {
            if (ImgSrc != null) ImgSrc.Dispose();
            ImgSrc = img;
        }
    }
}
