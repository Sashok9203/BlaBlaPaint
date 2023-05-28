using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class Ellipse : IDrawable
    {
        public Pen Pen { get; set; }
        public Point Start { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Filed { get; set; }

        public Ellipse(Pen pen)
        {
            Height = 0;
            Width = 0;
            Start = new();
            Pen = new(pen.Color, pen.Width);
        }


        public void Draw(Graphics g)
        {
            if (Filed) g.FillEllipse(Pen.Brush, Start.X, Start.Y, Width, Height);
            else g.DrawEllipse(Pen, Start.X, Start.Y, Width, Height);
        }
    }
}
