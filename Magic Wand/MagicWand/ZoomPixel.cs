using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicWand
{
    class ZoomPixel
    {
        public Bitmap bmp;
        Size size;
        int zoom;
        int limit;

        public ZoomPixel(Bitmap bmp)
        {
            this.bmp = bmp;
            size = bmp.Size;
            zoom = 40;
            limit = 128;

            
        }
    }
}
