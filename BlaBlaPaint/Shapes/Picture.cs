using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Shapes
{
    internal class Picture : IDrawable
    {
        private Image image;
        public Size Size { get; set; }
        public Point StartPosition { get; set; }

        public Picture(Image image)
        {
            Size = new();
            StartPosition = new();
            this.image = image;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, StartPosition.X, StartPosition.Y, Size.Width, Size.Height);
        }
    }
}
