using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicWand
{
    public partial class Form1 : Form
    {
        Bitmap Mask;
        Bitmap Bmp;
        //List<Point> markStack;

        List<Line> markStack;/*Ligne contient une liste de points composant une ligne 
        à l'horizontal et verticale*/
        List<Point> alreadyDone;

        public Form1()
        {
            InitializeComponent();

            markStack = new List<Line>();
            alreadyDone = new List<Point>();

            Bmp = new Bitmap(pictureBox1.Image);

            ScaleImage(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);

            int mWidth = Bmp.Width;
            int mHeight = Bmp.Height;

            Mask = new Bitmap(mWidth, mHeight);

            Console.WriteLine(Bmp.Width);
            Color color = Bmp.GetPixel(3, 1);

            //bool found = false;//le pixel est pas de la bonne couleur

            //GetBorder(color);
            //GetExtremity(1,color);

            Point p = new Point(3, 1);
            GetZone(p, color);

            pictureBox2.Size = new Size(260, 240);

            Console.WriteLine("width:" + Mask.Width);
            //ZoomPixel(Mask, pictureBox2);

            ScaleImage(Mask, pictureBox2.Width, pictureBox2.Height);
        }

        static public void ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            PointF Zoom = new PointF(20, 20);

            using (Matrix transform = Graphics.FromImage(image).Transform)
            {
                Graphics.FromImage(image).InterpolationMode = InterpolationMode.NearestNeighbor;
                //e.Graphics.ResetTransform();

                if (Zoom.X != 1.0 || Zoom.Y != 1.0)
                    transform.Scale(Zoom.X, Zoom.Y, MatrixOrder.Append);

                //if (ImageDisplayLocation.X != 0 || ImageDisplayLocation.Y != 0) //Convert translation back to display pixel unit.
                //    transform.Translate(ImageDisplayLocation.X / Zoom.X, ImageDisplayLocation.Y / Zoom.Y);

                Graphics.FromImage(image).Transform = transform;
            }
        }

        private void ZoomPixel(Bitmap bmp,PictureBox pic)
        {
            float scaleX = (float)pic.Width / (float)bmp.Width;
            float scaleY = (float)pic.Height / (float)bmp.Height;

            float scale = Math.Min(scaleX, scaleY);
            // Make a bitmap of the right size.
            int wid = (int)(bmp.Width * scale);
            int hgt = (int)(bmp.Height * scale);
            Bitmap bm = new Bitmap(wid, hgt);

            pic.Width = wid;
            pic.Height = hgt;

            // Draw the image onto the new bitmap.
            using (Graphics gr = Graphics.FromImage(bm))
            {
                // No smoothing.
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                Point[] dest =
                {
                    new Point(0, 0),
                    new Point(wid, 0),
                    new Point(0, hgt),
                };
                Rectangle source = new Rectangle(
                    0, 0,
                    bmp.Width,
                    bmp.Height);
                gr.DrawImage(bmp,
                    dest, source, GraphicsUnit.Pixel);
            }

            // Display the result.
            pic.Image = bm;
        }

        private void GetZone(Point p, Color color)
        {
            bool found = true;

            int numbPoints = 1;
            int x = p.X;
            int y = p.Y;

            GetLine(p, color);

            int j = 0;
            while (j < markStack.Count)
            {
                int i = 0;
                found = true;

                numbPoints = markStack[j].Count;
                while (i < numbPoints)
                {
                    if (markStack[j].Count > 0)
                    {
                        x = markStack[j].ElementAt(i).X;
                        y = markStack[j].ElementAt(i).Y;

                        if (y > 0 && Bmp.GetPixel(x, y - 1) == color)
                        {
                            GetLine(new Point(x, y - 1), color);
                        }
                    }
                    i++;
                }
                i = 0;
                x = markStack[j].ElementAt(i).X;
                y = markStack[j].ElementAt(i).Y;

                if (y > 0 && Bmp.GetPixel(x, y - 1) == color)
                {
                    GetLine(new Point(x, y - 1), color);
                }
                markStack.RemoveAt(j);
                Console.WriteLine("COUNT: " + markStack.Count);
            }
        }

        private void GetLine(Point p, Color color)
        {
            markStack.Add(new Line(p));
            Mask.SetPixel(p.X, p.Y, Color.Black);
            alreadyDone.Add(p);

            ParcoursLine(-1, 0, p, color);
            ParcoursLine(1, 0, p, color);
        }

        private void ParcoursLine(int side, int lineID, Point p, Color color)
        //side = -1 pour gauche et 1 pour droite
        {
            bool foundOther = false;
            bool testCondition = false;
            int x = p.X;

            while (foundOther == false)
            {
                if (side == -1)
                {
                    x--;
                    testCondition = x >= 0;
                }
                else if (side == 1)
                {
                    x++;
                    testCondition = x < Bmp.Width;
                }
                Console.WriteLine(x);
                if (testCondition)
                {
                    if (alreadyDone.Contains(new Point(x, p.Y)))
                    {
                        foundOther = true;
                    }
                    else if (Bmp.GetPixel(x, p.Y) == color)
                    {
                        Mask.SetPixel(x, p.Y, Color.Black);
                        markStack[lineID].Add(x, p.Y);
                        alreadyDone.Add(new Point(x, p.Y));
                        //foundOther = true;
                    }
                    else
                    {
                        alreadyDone.Add(new Point(x, p.Y));
                        foundOther = true;
                    }
                }
                else
                {
                    foundOther = true;
                }
            }
        }

        /*private void GetBorder(Color color)
        {
            //Initialisation
            Point p;

            /*
            markStack.Add(new Line(p));//initialize le point de départ de la ligne horizontale
            markStack.Add(new Line(p));//initialize le point de départ de la ligne verticale

            GetExtremity(0,0, color);*/

        /*  int nbPoints = 1;//nombre de points dans la ligne
          int i = 1;//index d'un point dans une ligne
          int j = 0;//index d'une ligne

          while(j<markStack.Count || markStack.Count ==0)
          {
              i = 1;

              if(j==0)
              {
                  p = new Point(7, 7);
                  i = 0;

                  markStack.Add(new Line(p));//initialize le point de départ de la ligne horizontale
                  markStack.Add(new Line(p));//initialize le point de départ de la ligne verticale

                  GetExtremity(j, i, color);

                  i = 1;

                  nbPoints = markStack[j].Count;
              }
              else
              {    
                  nbPoints = markStack[j].Count;

                  if (nbPoints > 1)
                  {
                      p = markStack[j].ElementAt(1);

                      markStack.Add(new Line(p));//initialize le point de départ de la ligne horizontale
                      markStack.Add(new Line(p));//initialize le point de départ de la ligne verticale

                      GetExtremity(j, i, color);
                  }
                  else
                  {
                     // j = markStack.Count;
                      i = nbPoints;
                  }
              }

              while (i < nbPoints && nbPoints>1)
              {
                  p = markStack[j].ElementAt(i);

                  markStack.Add(new Line(p));//initialize le point de départ de la ligne verticale

                  GetExtremity(j, i, color);

                  i++;
              }
              j++;
          }

          /*p = markStack[1].ElementAt(1);

          markStack.Add(new Line(p));//initialize le point de départ de la ligne horizontale
          markStack.Add(new Line(p));//initialize le point de départ de la ligne verticale

          GetExtremity(1, 1, color);
      }*/

        /*private void GetExtremity(int idLine,int id,Color color)
        {
            int idHorizon = idLine;
            int idVertical = markStack.Count -1;

            Point p = markStack.Last().ElementAt(0);

            Console.WriteLine(p);
            int pX = p.X;
            int pY = p.Y;

            GetLeftBorder(idHorizon, ref pX, pY,color);

            pX = p.X;
            GetRightBorder(idHorizon, ref pX, pY,color);

            Console.WriteLine("Pendant"+ markStack[idHorizon].ToString());

            pX = p.X;
            pY = p.Y;
            GetUpBorder(idVertical, pX, ref pY,color);

            pY = p.Y;
            GetDownBorder(idVertical,pX, ref pY,color);

            Console.WriteLine("Vertical" + markStack[idVertical].ToString());

            pY = p.Y;
        }*/

        /*private bool GetLeftBorder(int lineID,ref int x, int y,Color color)
        {
            x--;
            Console.WriteLine(x);
            if (x >= 0)
            {
                if (markStack[lineID].Contains(x, y))
                    return false;
                if (Bmp.GetPixel(x, y) != color)
                {
                    Mask.SetPixel(markStack[lineID].Last().X,
                        markStack[lineID].Last().Y, Color.Black);
                    return true;
                }
                else
                {
                    markStack[lineID].Add(x, y);
                }
            }
            else
            {
                return false;
            }
            if (GetLeftBorder(lineID, ref x, y,color))
            {
                return true;
            }
            return false;
        }*/

        /*private bool GetRightBorder(int lineID,ref int x, int y, Color color)
        {
            x++;
            Console.WriteLine(x);
            if (x < Bmp.Width)
            {
                if (markStack[lineID].Contains(x, y))
                    return false;
                if (Bmp.GetPixel(x, y) != color)
                {
                    Mask.SetPixel(markStack[lineID].Last().X,
                        markStack[lineID].Last().Y, Color.Black);
                    return true;
                }
                else
                {
                    markStack[lineID].Add(x, y);
                }
            }
            else
            {
                return false;
            }
            if (GetRightBorder(lineID,ref x, y, color))
            {
                return true;
            }
            return false;
        }*/

        /*private bool GetUpBorder(int lineID,int x, ref int y,Color color)
        {
            y--;
            Console.WriteLine(y);
            if (y >= 0)
            {
                if (markStack[lineID].Contains(x, y))
                    return false;
                if (Bmp.GetPixel(x, y) != color)
                {
                    Mask.SetPixel(markStack[lineID].Last().X,
                        markStack[lineID].Last().Y, Color.Black);
                    return true;
                }
                else
                {
                    markStack[lineID].Add(x, y);
                }
            }
            else
            {
                return false;
            }
            if (GetUpBorder(lineID,x, ref y, color))
            {
                return true;
            }
            return false;
        }*/

        /*private bool GetDownBorder(int lineID,int x, ref int y,Color color)
        {
            y++;
            Console.WriteLine(y);
            Console.WriteLine(Bmp.Height);
            if (y < Bmp.Height)
            {
                if (markStack[lineID].Contains(x, y))
                    return false;
                if (Bmp.GetPixel(x, y) != color)
                {
                    Mask.SetPixel(markStack[lineID].Last().X,
                        markStack[lineID].Last().Y, Color.Black);
                    return true;
                }
                else
                {
                    markStack[lineID].Add(x, y);
                }
            }
            else
            {
                return false;
            }
            if (GetDownBorder(lineID,x, ref y,color))
            {
                return true;
            }
            return false;
        }*/
    }
}
