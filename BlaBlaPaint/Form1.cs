using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using WinFormsApp2.Properties;
using WinFormsApp2.Shapes;

namespace WinFormsApp2
{

    public partial class Form1 : Form
    {

        public enum Shapes
        {
            Line,
            FreeLine,
            Bezier,
            Rectangle,
            FillRectangle,
            Circle,
            FillCircle
        }
        private Graphics gr;
        private readonly Graphics clbg;
        private Bitmap bitmap;
        private Bitmap colorLabelbitmap;
        private Color curentColor, curentBackColor;
        private readonly Pen pen;
        private bool mDown = false;
        private List<IDrawable> shapes;
        private Shapes curentShape;
        private Line line;

        public Form1()
        {
            InitializeComponent();
            shapes = new();
            curentColor = Color.Black;
            pen = new(curentColor, 1);
            int index = 0;
            toolStripDropDownButton.Image = toolImageList.Images[index]; ;
            foreach (string name in Enum.GetNames<Shapes>().ToArray())
            {
                toolStripDropDownButton.DropDownItems.Add(name);
                toolStripDropDownButton.DropDownItems[index].DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                toolStripDropDownButton.DropDownItems[index].Tag = (Shapes)index;
                toolStripDropDownButton.DropDownItems[index].Image = toolImageList.Images[index++];
            }

            curentShape = Shapes.Line;
            colorLabelbitmap = new Bitmap(5, 5);
            clbg = Graphics.FromImage(colorLabelbitmap);
            colorLabel.Image = colorLabelbitmap;
            curentBackColor = pictureBox.BackColor;

            bitmapUpdate();
            labelColorChange();

        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (curentShape)
            {
                case Shapes.Line: line = new(pen,new Point(e.X,e.Y), new Point(e.X, e.Y)); break;
            }
           
            mDown = true;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (mDown)
            {
                bitmapUpdate(false);
                switch (curentShape)
                {
                    case Shapes.Line:
                        line.End = new Point(e.X, e.Y);
                        line.Draw(gr); 
                        break;
                }

            }
        }



        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            switch (curentShape)
            {
                case Shapes.Line:
                    line.End = new Point(e.X, e.Y);
                    line.Draw(gr);
                    shapes.Add(line);
                    break;
            }
            mDown = false;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pen.Color = cd.Color;
                curentColor = cd.Color;
                labelColorChange();
            }
        }

        private void sizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

            pen.Width = (float)sizeNumericUpDown.Value;

        }



        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripDropDownButton.Image = e.ClickedItem.Image;
            curentShape = (Shapes)e.ClickedItem.Tag;
        }

        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            bitmapUpdate();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new();
            if (cd.ShowDialog() == DialogResult.OK && curentBackColor != cd.Color)
            {
                curentBackColor = cd.Color;
                bitmapUpdate(false);
            }
        }

        private void labelColorChange()
        {
            clbg.Clear(curentColor);
            colorLabel.Invalidate();
        }

        private void reDraw()
        {
            foreach (var item in shapes)
                item.Draw(gr);
        }

        private void bitmapUpdate(bool newBitmap = true)
        {
            if (newBitmap)
            {
                bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                gr = Graphics.FromImage(bitmap);
            }
            gr.Clear(curentBackColor);
            reDraw();
            pictureBox.Image = bitmap;
        }
    }
}