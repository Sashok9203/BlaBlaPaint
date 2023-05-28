using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class MultiLineShape : IDrawable
    {
        private List<Point> points;

        public Pen Pen { get; set; }

        public ShapeType Type { get; set; }

        public int PointCount => points.Count;

        public Point LastPoint => points[^1] ;

        public MultiLineShape(Pen pen, Point[]? p = null)
        {
            points = p == null ? new() : new List<Point>(p);
            Pen = new (pen.Color,pen.Width);
        }

        public void AddPoint(Point point) => points.Add(point);

        public void Draw(Graphics g)
        {
            if (points.Count >= 2)
            {
                switch (Type)
                {
                    case ShapeType.MultiLine:
                        g.DrawLines(Pen, points.ToArray());
                        break;
                    case ShapeType.FreeLine:
                        g.DrawCurve(Pen, points.ToArray());
                        break;
                    case ShapeType.Polygon:
                        g.DrawPolygon(Pen, points.ToArray());
                        break;
                    case ShapeType.FillPolygon:
                        g.FillPolygon(Pen.Brush, points.ToArray());
                        break;
                }
                
            }
        }
        
    }
}
