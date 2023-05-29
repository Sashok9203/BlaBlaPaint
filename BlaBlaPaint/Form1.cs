using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
        FillPolygon,
        Image
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

        private string? currentLoadedImagePath = null;
        private Graphics currentGraphics;
        private readonly Graphics colorLableGraphics;
        private Bitmap bitmap;
        private Bitmap colorLabelbitmap;
        private Image currentImageTool;
        private Color currentColor, currentBackColor;
        private readonly Pen drawPen, erasePen;
        private List<IDrawable> shapes;
        private ShapeType currentShapeType;
        private Point tmp;

        private Line? line = null;
        private Eraser? eraser = null;
        private MultiLineShape? multiLShape = null;
        private MultiRectangleShape? multiRShape = null;
        private Picture? picture = null;

        private int uPointer = -1, mouseDragUpdeqtsCount,updateSkip = 5;
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
        private bool saved;
        private bool Saved
        {
            get => saved;
            set
            {
                saved = value;
                saveToolStripButton.Enabled = !saved;
            }
        }

        public Form1()
        {
            InitializeComponent();
            currentImageTool = Resources.defaultImage;
            shapes = new();
            currentColor = Color.Black;
            drawPen = new(currentColor, 1);
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
            currentShapeType = ShapeType.Line;
            colorLabelbitmap = new Bitmap(5, 5);
            colorLableGraphics = Graphics.FromImage(colorLabelbitmap);
            colorLabel.Image = colorLabelbitmap;
            currentBackColor = Color.White;
            erasePen = new Pen(currentBackColor, 1);
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            currentGraphics = Graphics.FromImage(bitmap);
            currentGraphics.Clear(currentBackColor);
            pictureBox.Image = bitmap;
            labelColorChange();
            Saved = true;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (currentShapeType)
                {
                    case ShapeType.Line:
                        if (line == null) line = new(drawPen, new Point(e.X, e.Y), new Point(e.X, e.Y));
                        else
                        {
                            line.End = tmp;
                            line.Draw(currentGraphics);
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
                        multiLShape.Type = currentShapeType == ShapeType.Polygon || currentShapeType == ShapeType.FillPolygon ?
                                     ShapeType.MultiLine : currentShapeType;
                        break;

                    case ShapeType.Circle:
                    case ShapeType.FillCircle:
                    case ShapeType.Ellipse:
                    case ShapeType.FillEllipse:
                    case ShapeType.Rectangle:
                    case ShapeType.FillRectangle:
                        if (multiRShape == null)
                        {
                            multiRShape = new(drawPen, currentShapeType);
                            tmp = new Point(e.X, e.Y);
                            multiRShape.Filed = currentShapeType == ShapeType.FillRectangle;
                        }
                        else
                        {
                            addShape(multiRShape);
                            multiRShape = null;
                        }
                        break;

                    case ShapeType.Image:
                        if (picture == null)
                        {
                            picture = new(currentImageTool);
                            tmp = new Point(e.X, e.Y);
                        }
                        else
                        {
                            addShape(picture);
                            picture = null;
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
                switch (currentShapeType)
                {
                    case ShapeType.Polygon:
                    case ShapeType.FillPolygon:
                    case ShapeType.MultiLine:
                        if (multiLShape != null && multiLShape.PointCount != 1)
                        {
                            if (currentShapeType == ShapeType.Polygon || currentShapeType == ShapeType.FillPolygon)
                                multiLShape.Type = currentShapeType;
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

                    case ShapeType.Image:
                        picture = null;
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
                switch (currentShapeType)
                {
                    case ShapeType.FreeLine:
                        multiLShape?.AddPoint(new Point(e.X, e.Y));
                        multiLShape?.Draw(currentGraphics);
                        pictureBox.Image = bitmap;
                        break;
                    case ShapeType.Eraser:
                        eraser?.AddPoint(new Point(e.X, e.Y));
                        eraser?.Draw(currentGraphics);
                        pictureBox.Image = bitmap;
                        break;
                }
            }

            switch (currentShapeType)
            {
                case ShapeType.Polygon:
                case ShapeType.FillPolygon:
                case ShapeType.MultiLine:
                    if (multiLShape != null && multiLShape.PointCount != 0)
                    {
                        if (mouseDragUpdeqtsCount % 5 == 0)
                        {
                            bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
                            multiLShape.Draw(currentGraphics);
                            tmp = new(e.X, e.Y);
                            currentGraphics.DrawLine(multiLShape.Pen, multiLShape.LastPoint, tmp);
                        }
                    }
                    break;

                case ShapeType.Line:
                    if (line != null)
                    {
                        tmp = new(e.X, e.Y);
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        currentGraphics.DrawLine(line.Pen, line.Start, tmp);
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
                        multiRShape.Draw(currentGraphics);
                    }
                    break;
                case ShapeType.Image:
                    if (picture != null)
                    {
                        picture.Size = new Size(Math.Abs(tmp.X - e.X), Math.Abs(tmp.Y - e.Y));
                        Point normalize = new()
                        {
                            X = tmp.X < e.X ? tmp.X : e.X,
                            Y = tmp.Y < e.Y ? tmp.Y : e.Y,
                        };
                        picture.StartPosition = normalize;
                        bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Skip);
                        picture.Draw(currentGraphics);
                    }
                    break;

            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (currentShapeType)
                {
                    case ShapeType.FreeLine:
                        multiLShape?.AddPoint(new Point(e.X, e.Y));
                        addShape(multiLShape);
                        multiLShape.Draw(currentGraphics);
                        pictureBox.Image = bitmap;
                        multiLShape = null;
                        break;
                    case ShapeType.Eraser:
                        eraser?.AddPoint(new Point(e.X, e.Y));
                        addShape(eraser);
                        eraser.Draw(currentGraphics);
                        pictureBox.Image = bitmap;
                        eraser = null;
                        break;

                }
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (currentShapeType == ShapeType.Image)
            {
                Image? image = getImage().image;
                if (image != null)
                {
                    currentImageTool = image;
                    colorLabel.Image = currentImageTool;
                }
            }
            else
            {
                ColorDialog cd = new();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    drawPen.Color = cd.Color;
                    currentColor = cd.Color;
                    labelColorChange();
                }
            }
        }

        private void toolStripDropDownButtonClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripDropDownButton.Image = e.ClickedItem.Image;
            currentShapeType = (ShapeType)e.ClickedItem.Tag;
            if (currentShapeType == ShapeType.Image) colorLabel.Image = currentImageTool;
            else colorLabel.Image = colorLabelbitmap;
        }

        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            bitmapUpdate();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                if (MessageBox.Show("Want to save this image?", "Save?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    currentLoadedImagePath ??= getFileNameToImageSave();
                    if (currentLoadedImagePath != null) saveImage(currentLoadedImagePath);
                }
            }
            Clear();
            currentLoadedImagePath = null;
        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new();
            if (cd.ShowDialog() == DialogResult.OK && currentBackColor != cd.Color)
            {
                erasePen.Color = cd.Color;
                currentBackColor = cd.Color;
                bitmapUpdate(BitmapUpdate.Redraw, UpdateTime.Now);
            }
        }

        private void labelColorChange()
        {
            colorLableGraphics.Clear(currentColor);
            colorLabel.Invalidate();
        }

        private void reDraw()
        {
            for (int i = 0; i <= undoPointer; i++)
                shapes[i].Draw(currentGraphics);
        }

        private void bitmapUpdate(BitmapUpdate update = BitmapUpdate.NewBitmap, UpdateTime upTime = UpdateTime.Now)
        {
            if (upTime != UpdateTime.Now && mouseDragUpdeqtsCount % updateSkip != 0) return;

            if (update == BitmapUpdate.NewBitmap)
            {
                bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                currentGraphics = Graphics.FromImage(bitmap);
            }
            currentGraphics.Clear(currentBackColor);
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
            Saved = false;
        }

        private void Clear()
        {
            currentGraphics.Clear(currentBackColor);
            shapes.Clear();
            undoPointer = -1;
            pictureBox.Image = bitmap;
        }

        private void eraseToolStripButton_Click(object sender, EventArgs e) => Clear();
        
        private void widhtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawPen.Width = widhtComboBox.SelectedIndex;
            erasePen.Width = drawPen.Width;
        }

        private void fileOpenToolStripButton_Click(object sender, EventArgs e)
        {
            if (!saved) MessageBox.Show("save");
            (Image?, string?) val = getImage();
            if (val.Item1 != null)
            {
                Picture picture = new(new Bitmap(val.Item1))
                {
                    Size = pictureBox.Size,
                    StartPosition = new()
                };
                addShape(picture);
                picture.Draw(currentGraphics);
                pictureBox.Image = bitmap;
                currentLoadedImagePath = val.Item2;
                val.Item1.Dispose();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            currentLoadedImagePath ??= getFileNameToImageSave();
            if (currentLoadedImagePath == null) return;
            saveImage(currentLoadedImagePath);
            Saved = true;
        }

        private void SaveAsToolStripButton_Click(object sender, EventArgs e)
        {
            string? path = getFileNameToImageSave();
            if (path == null) return;
            saveImage(path);
            Saved = true;
        }

        private (Image? image, string? path) getImage()
        {

            Image? image = null;
            string? path = null;
            OpenFileDialog fd = new();
            fd.Filter = "JPG files(*.jpg)|*.jpg|JPEG files(*.jpeg)|*.jpeg|BMP files(*.bmp)|*.bmp|PNG files(*.png)|*.png";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(fd.FileName);
                path = fd.FileName;
            }
            return (image, path);
        }

        private string? getFileNameToImageSave()
        {

            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "JPG files(*.jpg)|*.jpg|JPEG files(*.jpeg)|*.jpeg|BMP files(*.bmp)|*.bmp|PNG files(*.png)|*.png";
            if (sfg.ShowDialog() == DialogResult.OK)
                return sfg.FileName;
            return null;
        }

        private void saveImage(string path)
        {
            string exp = Path.GetExtension(path)[1..].ToLower();
            switch (exp)
            {
                case "jpeg":
                case "jpg":
                    bitmap.Save(path, ImageFormat.Jpeg);
                    break;
                case "png":
                    bitmap.Save(path, ImageFormat.Png);
                    break;
                case "bmp":
                    bitmap.Save(path, ImageFormat.Bmp);
                    break;
            }
        }
    }
}