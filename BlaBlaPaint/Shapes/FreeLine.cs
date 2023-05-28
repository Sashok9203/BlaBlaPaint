using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class FreeLine : IDrawable
    {
        private List<Point> points;

        public Pen Pen { get; set; }

        public bool Free { get; set; }

        public int PointCount => points.Count;



        public Point LastPoint => points[^1] ;

        public FreeLine(Pen pen, Point[]? p = null)
        {
            points = p == null ? new() : new List<Point>(p);
            Pen = new (pen.Color,pen.Width);
        }

        public void AddPoint(Point point) => points.Add(point);

        public void Draw(Graphics g)
        {
            if (points.Count >= 2)
            {
                if (Free) g.DrawCurve(Pen, points.ToArray());
                else g.DrawLines(Pen, points.ToArray());
            }
        }
        
    }
}
