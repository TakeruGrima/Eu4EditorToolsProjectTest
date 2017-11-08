using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ProvincePicker
{
    public partial class PixelPictureBox : PictureBox
    {
        public PointF Zoom = new PointF(1F,1F);
        private InterpolationMode interpolationMode = InterpolationMode.NearestNeighbor;

        /// <summary>
        /// Paint the image
        /// </summary>
        /// <param name="e">The paint event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsDisposed)
                return;

            if (Image != null)
            {
                if (e.Graphics.InterpolationMode != interpolationMode)
                    e.Graphics.InterpolationMode = interpolationMode;

                using (Matrix transform = e.Graphics.Transform)
                {
                    //e.Graphics.ResetTransform();

                    if (Zoom.X != 1.0 || Zoom.Y != 1.0)
                        transform.Scale(Zoom.X, Zoom.Y, MatrixOrder.Append);

                    //if (ImageDisplayLocation.X != 0 || ImageDisplayLocation.Y != 0) //Convert translation back to display pixel unit.
                    //    transform.Translate(ImageDisplayLocation.X / Zoom.X, ImageDisplayLocation.Y / Zoom.Y);


                    e.Graphics.PixelOffsetMode = PixelOffsetMode.Half; //Added
                    e.Graphics.Transform = transform;
                }
            }

            base.OnPaint(e);

            //If you want to draw something over the control in control coordinate, you must first reset the transformation! :D
            //e.Graphics.ResetTransform();
            //Draw your stuff
        }
    }
}
