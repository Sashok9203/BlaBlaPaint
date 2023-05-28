using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using WinFormsApp2.Properties;
using WinFormsApp2.Shapes;
using static WinFormsApp2.Form1;

namespace WinFormsApp2
{
    public enum ShapeType
    {
        Line,
        FreeLine,
        MultiLine,
        Rectangle,
        FillRectangle,
        Circle,
        FillCircle,
        Ellipse,
        FillEllipse
    }

    public partial class Form1 : Form
    {
        public enum BitmapUpdate
        {
            Redraw,
            NewBitmap
        }
        public enum UpdateTime
        {
            Now,
            Skip
        }
        
        private Graphics gr;
        private readonly Graphics clbg;
        private Bitmap bitmap;
        private Bitmap colorLabelbitmap;
        private Color curentColor, curentBackColor;
        private readonly Pen pen;
        private List<IDrawable> shapes;
        private ShapeType curentShapeType;
        private Point tmp;

        private Line? line = null;
        private FreeLine? fline = null;
        private MultiShape? multiShape = null;
        

        private int uPointer = -1, mouseDragUpdeqtsCount;
        private int undoPointer
        {
            get => uPointer;
            set
            {
                uPointer = value;
                redoToolStripButton.Enabled = uPointer < shapes.Count - 1;
                undoToolStripButton.Enabled = uPointer >= 0;
            }
        }
        public Form1()
        {
            InitializeComponent();
            shapes = new();
            curentColor = Color.Black;
            pen = new(curentColor, 1);
            int index = 0;
            toolStripDropDownButton.Image = toolImageList.Images[index]; ;
            foreach (string name in Enum.GetNames<ShapeType>().ToArray())
            {
                toolStripDropDownButton.DropDownItems.Add(name);
                toolStripDropDownButton.DropDownItems[index].DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                toolStripDropDownButton.DropDownItems[index].Tag = (ShapeType)index;
                toolStripDropDownButton.DropDownItems[index].Image = toolImageList.Images[index++];
            }

            curentShapeType = ShapeType.Line;
            colorLabelbitmap = new Bitmap(5, 5);
            clbg = Graphics.FromImage(colorLabelbitmap);
            colorLabel.Image = colorLabelbitmap;
            curentBackColor = pictureBox.BackColor;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(curentBackColor);
            reDraw();
            pictureBox.Image = bitmap;
            labelColorChange();

        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (curentShapeType)
                {
                    case ShapeType.Line:
                        if (line == null)  line = new(pen, new Point(e.X, e.Y), new Point(e.X, e.Y));
                        else
                        {
                            line.End = tmp;
                            line.Draw(gr);
                            addShape(line);
                            line = null;
                        }
                        break;

                    case ShapeType.MultiLine:
                    case ShapeType.FreeLine:
                        fline ??= new(pen);
                        fline.AddPoint(new Point(e.X, e.Y));
                        fline.Free = curentShapeType == ShapeType.FreeLine;
                        break;

                    case ShapeType.Circle:
                    case ShapeType.FillCircle:
                    case ShapeType.Ellipse:
                    case ShapeType.FillEllipse:
                    case ShapeType.Rectangle:
                    case ShapeType.FillRectangle:
                        if (multiShape == null)
                        {
                            multiShape =  new(pen, curentShapeType);
                            tmp = new Point(e.X, e.Y);
                            multiShape.Filed = curentShapeType == ShapeType.FillRectangle;
                        }
                        else
                        {
                            addShape(multiShape);
                            multiShape = null;
                        }
                        break;

                   
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                switch (curentShapeType)
                {
                    case ShapeType.MultiLine:
                        if (fline != null && fline.PointCount != 1)
                        {
                            addShape(fline);
                            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
                        }
                        fline = null;
                        break;

                    case ShapeType.Circle:
                    case ShapeType.FillCircle:
                    case ShapeType.Ellipse:
                    case ShapeType.FillEllipse:
                    case ShapeType.Rectangle:
                    case ShapeType.FillRectangle:
                        multiShape = null;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
                        break;

                  
                    case ShapeType.Line:
                        line = null;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
                        break;

                }
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mouseDragUpdeqtsCount++;
            if (e.Button == MouseButtons.Left)
            {

                switch (curentShapeType)
                {
                    case ShapeType.FreeLine:
                        if (mouseDragUpdeqtsCount % 5 == 0) fline?.AddPoint(new Point(e.X, e.Y));
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        fline?.Draw(gr);
                        break;

                }

            }

            switch (curentShapeType)
            {

                case ShapeType.MultiLine:
                    if (fline != null && fline.PointCount != 0)
                    {

                        if (mouseDragUpdeqtsCount % 5 == 0)
                        {
                            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                            fline.Draw(gr);
                            tmp = new(e.X, e.Y);
                            gr.DrawLine(fline.Pen, fline.LastPoint, tmp);
                        }

                    }
                    break;
                case ShapeType.Line:
                    if (line != null)
                    {
                        tmp = new(e.X, e.Y);
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        gr.DrawLine(line.Pen, line.Start, tmp);
                    }
                    break;

                case ShapeType.Circle:
                case ShapeType.FillCircle:
                case ShapeType.Rectangle:
                case ShapeType.FillRectangle:
                case ShapeType.Ellipse:
                case ShapeType.FillEllipse:
                    if (multiShape != null)
                    {
                        multiShape.Width = Math.Abs(tmp.X - e.X);
                        multiShape.Height = Math.Abs(tmp.Y - e.Y);
                        Point normalize = new()
                        {
                            X = tmp.X < e.X ? tmp.X : e.X,
                            Y = tmp.Y < e.Y ? tmp.Y : e.Y,
                        };
                        multiShape.Start = normalize;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        multiShape.Draw(gr);
                    }
                    break;

               

            }
        }


        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (curentShapeType)
                {
                    case ShapeType.FreeLine:
                        fline.AddPoint(new Point(e.X, e.Y));
                        addShape(fline);
                        fline = null;
                        break;
                }
            }
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
            curentShapeType = (ShapeType)e.ClickedItem.Tag;
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
                bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
            }
        }

        private void labelColorChange()
        {
            clbg.Clear(curentColor);
            colorLabel.Invalidate();
        }

        private void reDraw()
        {
            for (int i = 0; i <= undoPointer; i++)
                shapes[i].Draw(gr);
        }

        private void bitmapUpdate(BitmapUpdate update = BitmapUpdate.NewBitmap, UpdateTime upTime = UpdateTime.Now)
        {
            if (upTime != UpdateTime.Now && mouseDragUpdeqtsCount % 5 != 0) return;

            if (update == BitmapUpdate.NewBitmap)
            {
                bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                gr = Graphics.FromImage(bitmap);
            }
            gr.Clear(curentBackColor);
            reDraw();
            pictureBox.Image = bitmap;

        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            undoPointer--;
            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            undoPointer++;
            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
        }

        private void addShape(IDrawable shape)
        {
            if (undoPointer != shapes.Count - 1)
                shapes.RemoveRange(undoPointer + 1, shapes.Count - undoPointer - 1);
            shapes.Add(shape);
            undoPointer = shapes.Count - 1;
        }



    }
}