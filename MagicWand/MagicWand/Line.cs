using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicWand
{
    class Line
    {
        private List<Point> points;
        public int Count
        {
            get
            {
                return points.Count;
            }
        }

        public Line()
        {
            points = new List<Point>();
        }

        public Line(Point p)
        {
            points = new List<Point>();
            points.Add(p);
        }

        public Line(int x,int y)
        {
            points = new List<Point>();
            Add(x,y);
        }

        public void Add(int x,int y)
        {
            points.Add(new Point(x, y));
        }

        public void Add(Point p)
        {
            points.Add(p);
        }

        public void Clear()
        {
            points.Clear();
        }

        public bool Contains(Point p)
        {
            return points.Contains(p);
        }

        public bool Contains(int x,int y)
        {
            return points.Contains(new Point(x, y));
        }

        public Point ElementAt(int index)
        {
            return points.ElementAt(index);
        }

        public Point Last()
        {
            return points.Last();
        }

        public override string ToString()
        {
            string output= "";

            foreach (Point curr in points)
            {
                output += "[" + curr.X + ";" + curr.Y + "]";
            }
            return output;
        }
    }
}
