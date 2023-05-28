using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class MultiRectangleShape : IDrawable
    {
        private int height;
        private int width;

        public ShapeType Type { get; set; }
        public Pen Pen { get; set; }
        public Point Start { get; set; }
        public int Height
        {
            get
            {
                if (Type == ShapeType.Circle || Type == ShapeType.FillCircle) return Math.Max(height, width);
                return height;
            }
            set => height = value;
        }
        public int Width
        {
            get
            {
                if (Type == ShapeType.Circle || Type == ShapeType.FillCircle) return Math.Max(height, width);
                return width;
            }
            set => width = value;
        }
        public bool Filed { get; set; }
        public MultiRectangleShape(Pen pen, ShapeType type)
        {
            Height = 0;
            Width = 0;
            Start = new();
            Pen = new(pen.Color, pen.Width);
            Type = type;

        }
        public void Draw(Graphics g)
        {
            switch (Type)
            {
                case ShapeType.Rectangle: 
                    g.DrawRectangle(Pen, Start.X, Start.Y, Width, Height);
                    break;
                case ShapeType.FillRectangle:
                    g.FillRectangle(Pen.Brush, Start.X, Start.Y, Width, Height);
                    break;
                case ShapeType.FillCircle:
                case ShapeType.FillEllipse:
                   g.FillEllipse(Pen.Brush, Start.X, Start.Y, Width, Height);
                    break;
                case ShapeType.Circle:
                case ShapeType.Ellipse:
                    g.DrawEllipse(Pen, Start.X, Start.Y, Width, Height);
                    break;
               
            }
            
        }
    }
}
