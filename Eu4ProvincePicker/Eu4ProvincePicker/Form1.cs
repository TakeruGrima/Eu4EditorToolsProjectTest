using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ProvincePicker
{
    public partial class Form1 : Form
    {
        Dictionary<Color, List<Line>> provLines;

        public Form1()
        {
            InitializeComponent();
        }

        public void GetBorder()
        {
            //we search pixel lines in a province
            provLines = new Dictionary<Color, List<Line>>();

            Bitmap bmp = new Bitmap(panelPicture1.Image);
            Bitmap mask = new Bitmap(panelPicture1.Image);

            int width = bmp.Width;
            int height = bmp.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    //store the first point
                    Line l = new Line(x, y);

                    // Go until it's not the same color or it hits the edge of the image
                    while (x < width && c == bmp.GetPixel(x, y))
                    {
                        x++;
                    }

                    //store the final point
                    l.End = new Point(x, y);

                    if (!provLines.ContainsKey(c))
                    {
                        List<Line> lines = new List<Line>();
                        lines.Add(l);
                        provLines.Add(c, lines);
                    }

                    if (c != Color.Black && x < width && mask.GetPixel(x, y) != Color.Black)
                    { // it's PTI, so don't bother with a border
                        mask.SetPixel(x, y, Color.Black);
                    }

                    x--;
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color c = bmp.GetPixel(x, y);

                    do
                    {
                        y++;
                    } while (y < height && c == bmp.GetPixel(x,y));

                    if (c != Color.Black && y < height && mask.GetPixel(x, y) != Color.Black)
                    { // it's PTI, so don't bother with a border
                        mask.SetPixel(x, y, Color.Black);
                    }
                }
            }

            panelPicture1.Image = mask;
            panelPicture1.PerformLayout();
        }

        private void FindBox_TextChanged(object sender, EventArgs e)
        {
           if(FindBox.Text== "Roma")
            {
                //16,140,192
                if(provLines.ContainsKey(Color.FromArgb(16,140,192)))
                {
                    Point p = provLines[Color.FromArgb(16, 140, 192)][0].Begin;

                    panelPicture1.GoToArea(p);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetBorder();
        }
    }

    class Pixel
    {
        public int X;
        public int Y;
        public Color Color;

        public Pixel(int x, int y, Bitmap bmp)
        {
            X = x;
            Y = y;

            Color = bmp.GetPixel(x, y);
        }
    }

    class Line
    {
        public Point Begin;
        public Point End;

        public Line(Point begin)
        {
            Begin = begin;
        }

        public Line(int x,int y)
        {
            Begin = new Point(x, y);
        }
    }
}
