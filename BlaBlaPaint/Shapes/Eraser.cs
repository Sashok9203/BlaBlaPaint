using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class Eraser : IDrawable
    {
        private List<Point> points;
        private float widht;
        public Pen Pen { get; set; }
        public int PointCount => points.Count;
        public Point LastPoint => points.Last();
        public Eraser(Pen pen, Point[]? p = null)
        {
            points = p == null ? new() : new List<Point>(p);
            Pen = pen;
            widht = pen.Width;
        }

        public void AddPoint(Point point)
        {
            point.X -= (int)widht/2;
            point.Y -= (int)widht/2;
            points.Add(point);
        }

        public void Draw(Graphics g)
        {
            float tmpWidht = Pen.Width;
            Pen.Width = widht;
            foreach (var item in points)
                g.FillEllipse(Pen.Brush, item.X, item.Y, widht, widht);
            Pen.Width = tmpWidht;
        }
    }
}
