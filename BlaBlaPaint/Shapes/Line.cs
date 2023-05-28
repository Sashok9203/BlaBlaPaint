using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class Line : IDrawable
    {
        public Point Start { get; set; }
        public Point End{ get; set; }
        public Pen Pen { get; set; }
        public Line(Pen pen,Point start, Point end)
        {
            Start = start;
            End = end;
            Pen = new ( pen.Color,pen.Width);
        }

        public void Draw(Graphics g)
        {
           g.DrawLine(Pen, Start, End);
        }
    }
}
