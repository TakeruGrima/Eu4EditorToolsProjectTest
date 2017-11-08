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

        Point depart;

        public Form1()
        {
            InitializeComponent();

            markStack = new List<Line>();
            alreadyDone = new List<Point>();

            Bmp = new Bitmap(pixelPictureBox1.Image);

            pixelPictureBox1.Invalidate();

            int mWidth = Bmp.Width;
            int mHeight = Bmp.Height;

            Mask = new Bitmap(mWidth, mHeight);

            Console.WriteLine(Bmp.Width);
            Color color = Bmp.GetPixel(3, 1);

            Point p = new Point(3, 1);
            depart = new Point();
            //GetZone(p, color);

            FloodFill(Bmp, p, color, Color.Red);



            pixelPictureBox2.Size = new Size(260, 240);
            pixelPictureBox2.Image = Mask;
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

        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }

        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(pt);
            while (q.Count > 0)
            {
                Point n = q.Dequeue();
                if (!ColorMatch(bmp.GetPixel(n.X, n.Y), targetColor))
                    continue;
                Point w = n, e = new Point(n.X + 1, n.Y);
                while ((w.X >= 0) && ColorMatch(bmp.GetPixel(w.X, w.Y), targetColor))
                {
                    if ((w.X - 1 >= 0 && !ColorMatch(bmp.GetPixel(w.X - 1, w.Y), targetColor))
                        || (w.X -1 == -1 || w.Y -1 == -1 || w.Y +1 == bmp.Height))
                    {
                        Mask.SetPixel(w.X, w.Y, replacementColor);
                        Console.WriteLine(w + ": " + Mask.GetPixel(w.X,w.Y));
                    }
                    bmp.SetPixel(w.X, w.Y, replacementColor);
                    depart.X = w.X;
                    depart.Y = w.Y;
                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X <= bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    if ((e.X + 1 < bmp.Width -1 && !ColorMatch(bmp.GetPixel(w.X + 1, w.Y), targetColor))
                        || (e.X + 1 == bmp.Width || e.Y - 1 == -1 || e.Y +1 == bmp.Height))
                    {
                        Mask.SetPixel(e.X, e.Y, replacementColor);
                        Console.WriteLine(e + ": " + Mask.GetPixel(e.X, e.Y));
                    }
                    bmp.SetPixel(e.X, e.Y, replacementColor);
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
                Console.WriteLine("count: " + q.Count);
            }
            Console.WriteLine("FIN");
        }
    }
}
