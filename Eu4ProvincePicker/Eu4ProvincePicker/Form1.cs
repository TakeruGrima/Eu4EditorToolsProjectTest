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
        Bitmap Mask;
        Bitmap Bmp;

        Bitmap map;

        Color color;

        List<Color> colors;

        public Form1()
        {
            InitializeComponent();

            string filePath = @"C:\Users\polo\Documents\Paradox Interactive\Europa Universalis IV\mod\extendedtimeline\map\definition.csv";
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0\";";
            OleDbConnection connection = new OleDbConnection(connectionString);
            string cmdText = "SELECT * FROM [definition$]";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            List<string> ids = new List<string>();
            colors = new List<Color>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader[5].ToString() == "x")
                    {
                        ids.Add(reader[0].ToString());

                        colors.Add(Color.FromArgb(Int32.Parse(reader[1].ToString()),
                            Int32.Parse(reader[2].ToString()),
                            Int32.Parse(reader[3].ToString())));
                    }
                }
            }

            //panel1.SetDisplayRectLocation(0, AutoScrollPosition.Y - item.BoundingRect.Bottom + ClientRectangle.Bottom);
            //panel1.AdjustFormScrollbars(true);

            /*if (bmp.GetPixel(3001, 2048 - 1361) == color)
            {
                int x = 3001 - pictureBox1.Width / 2; 
                int y = 2048 - 1361 - pictureBox1.Height / 2;
                Rectangle rect = new Rectangle(x, y, pictureBox1.Width, pictureBox1.Height);

            }*/
        }

        private void panelPicture1_Click(object sender, EventArgs e)
        {
            panelPicture1.GoToArea(3001, 2048 - 1361);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (FindBox.Text == "Roma")
            {
                Bmp = new Bitmap(panelPicture1.Image);

                map = new Bitmap(panelPicture1.Image);

                int mWidth = Bmp.Width;
                int mHeight = Bmp.Height;

                Mask = new Bitmap(panelPicture1.Image);

                //panelPicture1.GoToArea(254, 2048 - 1841);

                //254.000 1841.000 247.000 1840.000 385.000 1917.000 239.000 1834.000 386.000 1917.000 242.000 1840.000
                /*FloodFill(Bmp,new Point(254,2048 - 1841), color, Color.AliceBlue);
                FloodFill(Bmp, new Point(266,213), color, Color.AliceBlue);
                FloodFill(Bmp, new Point(385, 2048 - 1917), color, Color.AliceBlue);
                FloodFill(Bmp, new Point(239, 2048 - 1834), color, Color.AliceBlue);
                FloodFill(Bmp, new Point(286, 2048 - 1917), color, Color.AliceBlue);
                FloodFill(Bmp, new Point(242, 2048 - 1840), color, Color.AliceBlue);*/

                Point p = new Point(0, 0);

                while(p.Y < 100)
                {
                    while (p.X < Bmp.Width)
                    {
                        color = Bmp.GetPixel(p.X, p.Y);
                        if (colors.Contains(color) && color.B < 200)
                        {
                            FloodFill(Bmp, p, color, Color.AliceBlue);
                        }
                        p.X++;
                    }
                    p.Y++;
                    p.X = 0;
                }

                //color = Bmp.GetPixel(p.X, p.Y);
                //FloodFill(Bmp, p, color, Color.AliceBlue);
                panelPicture1.Image = Mask;

                panelPicture1.Invalidate();
            }
            /*else
            {
                map = new Bitmap(panelPicture1.Image);

                panelPicture1.Image = map;
            }*/
        }

        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }

        private void BorderDraw(Bitmap bmp, Point pt, Color color, List<Point> points)
        {
            foreach (Point p in points)
            {
                if (p.X >= 0 && p.X + 1 <= bmp.Width && p.Y >= 0 && p.Y + 1 <= bmp.Height)
                {
                    if (p.X < bmp.Width - 1 && !ColorMatch(bmp.GetPixel(p.X + 1, p.Y), color))
                        Mask.SetPixel(p.X + 1, p.Y, Color.Black);
                    if (p.X > 0 && !ColorMatch(bmp.GetPixel(p.X - 1, p.Y), color))
                        Mask.SetPixel(p.X - 1, p.Y, Color.Black);
                    if (p.Y < bmp.Height - 1 && !ColorMatch(bmp.GetPixel(p.X, p.Y + 1), color))
                        Mask.SetPixel(p.X, p.Y + 1, Color.Black);
                    if (p.Y > 0 && !ColorMatch(bmp.GetPixel(p.X, p.Y - 1), color))
                        Mask.SetPixel(p.X, p.Y - 1, Color.Black);
                }
            }
        }

        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            List<Point> points = new List<Point>();//stocke les points coloré
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
                    bmp.SetPixel(w.X, w.Y, replacementColor);
                    points.Add(new Point(w.X, w.Y));
                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X <= bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    bmp.SetPixel(e.X, e.Y, replacementColor);
                    points.Add(new Point(e.X, e.Y));
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
            }
            BorderDraw(bmp, pt, replacementColor, points);
        }
    }
}
