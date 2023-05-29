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
        Eraser,
        Line,
        FreeLine,
        MultiLine,
        Rectangle,
        FillRectangle,
        Circle,
        FillCircle,
        Ellipse,
        FillEllipse,
        Polygon,
        FillPolygon
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
        private readonly Pen drawPen, erasePen;
        private List<IDrawable> shapes;
        private ShapeType curentShapeType;
        private Point tmp;

        private Line? line = null;
        private Eraser? eraser = null;
        private MultiLineShape? multiLShape = null;
        private MultiRectangleShape? multiRShape = null;


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
            drawPen = new(curentColor, 1);
            int index = 0;
            toolStripDropDownButton.Image = toolImageList.Images[1]; ;
            foreach (string name in Enum.GetNames<ShapeType>().ToArray())
            {
                toolStripDropDownButton.DropDownItems.Add(name);
                toolStripDropDownButton.DropDownItems[index].DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                toolStripDropDownButton.DropDownItems[index].Tag = (ShapeType)index;
                toolStripDropDownButton.DropDownItems[index].Image = toolImageList.Images[index++];
            }
            for (int i = 1; i <= 100; i++) widhtComboBox.Items.Add(i);
            curentShapeType = ShapeType.Line;
            colorLabelbitmap = new Bitmap(5, 5);
            clbg = Graphics.FromImage(colorLabelbitmap);
            colorLabel.Image = colorLabelbitmap;
            curentBackColor = Color.White;
            erasePen = new Pen(curentBackColor, 1);
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(curentBackColor);
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
                        if (line == null) line = new(drawPen, new Point(e.X, e.Y), new Point(e.X, e.Y));
                        else
                        {
                            line.End = tmp;
                            line.Draw(gr);
                            addShape(line);
                            line = null;
                        }
                        break;

                    case ShapeType.Polygon:
                    case ShapeType.FillPolygon:
                    case ShapeType.MultiLine:
                    case ShapeType.FreeLine:
                        multiLShape ??= new(drawPen);
                        multiLShape.AddPoint(new Point(e.X, e.Y));
                        multiLShape.Type = curentShapeType == ShapeType.Polygon || curentShapeType == ShapeType.FillPolygon ?
                                     ShapeType.MultiLine : curentShapeType;
                        break;

                    case ShapeType.Circle:
                    case ShapeType.FillCircle:
                    case ShapeType.Ellipse:
                    case ShapeType.FillEllipse:
                    case ShapeType.Rectangle:
                    case ShapeType.FillRectangle:
                        if (multiRShape == null)
                        {
                            multiRShape = new(drawPen, curentShapeType);
                            tmp = new Point(e.X, e.Y);
                            multiRShape.Filed = curentShapeType == ShapeType.FillRectangle;
                        }
                        else
                        {
                            addShape(multiRShape);
                            multiRShape = null;
                        }
                        break;

                    case ShapeType.Eraser:
                        eraser ??= new(erasePen);
                        eraser.AddPoint(new Point(e.X, e.Y));

                        break;

                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                switch (curentShapeType)
                {
                    case ShapeType.Polygon:
                    case ShapeType.FillPolygon:
                    case ShapeType.MultiLine:
                        if (multiLShape != null && multiLShape.PointCount != 1)
                        {
                            if (curentShapeType == ShapeType.Polygon || curentShapeType == ShapeType.FillPolygon)
                                multiLShape.Type = curentShapeType;
                            addShape(multiLShape);
                        }
                        multiLShape = null;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
                        break;

                    case ShapeType.Circle:
                    case ShapeType.FillCircle:
                    case ShapeType.Ellipse:
                    case ShapeType.FillEllipse:
                    case ShapeType.Rectangle:
                    case ShapeType.FillRectangle:
                        multiRShape = null;
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
                        if (mouseDragUpdeqtsCount % 5 == 0)
                        {
                            multiLShape?.AddPoint(new Point(e.X, e.Y));
                            multiLShape?.Draw(gr);
                            pictureBox.Image = bitmap;
                        }
                        break;
                    case ShapeType.Eraser:
                        eraser?.AddPoint(new Point(e.X, e.Y));
                        eraser?.Draw(gr);
                        pictureBox.Image = bitmap;
                        break;
                }

            }

            switch (curentShapeType)
            {
                case ShapeType.Polygon:
                case ShapeType.FillPolygon:
                case ShapeType.MultiLine:
                    if (multiLShape != null && multiLShape.PointCount != 0)
                    {

                        if (mouseDragUpdeqtsCount % 5 == 0)
                        {
                            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                            multiLShape.Draw(gr);
                            tmp = new(e.X, e.Y);
                            gr.DrawLine(multiLShape.Pen, multiLShape.LastPoint, tmp);
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
                    if (multiRShape != null)
                    {
                        multiRShape.Width = Math.Abs(tmp.X - e.X);
                        multiRShape.Height = Math.Abs(tmp.Y - e.Y);
                        Point normalize = new()
                        {
                            X = tmp.X < e.X ? tmp.X : e.X,
                            Y = tmp.Y < e.Y ? tmp.Y : e.Y,
                        };
                        multiRShape.Start = normalize;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        multiRShape.Draw(gr);
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
                        multiLShape?.AddPoint(new Point(e.X, e.Y));
                        addShape(multiLShape);
                        multiLShape.Draw(gr);
                        pictureBox.Image = bitmap;
                        multiLShape = null;
                        break;
                    case ShapeType.Eraser:
                        eraser?.AddPoint(new Point(e.X, e.Y));
                        addShape(eraser);
                        eraser.Draw(gr);
                        pictureBox.Image = bitmap;
                        eraser = null;
                        break;

                }
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                drawPen.Color = cd.Color;
                curentColor = cd.Color;
                labelColorChange();
            }
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
                erasePen.Color = cd.Color;
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

        private void Clear()
        {
            gr.Clear(curentBackColor);
            shapes.Clear();
            undoPointer = -1;
            pictureBox.Image = bitmap;
        }

        private void eraseToolStripButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void widhtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawPen.Width = widhtComboBox.SelectedIndex;
            erasePen.Width = drawPen.Width;
        }
    }
}