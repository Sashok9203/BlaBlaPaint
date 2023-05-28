namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox = new PictureBox();
            sizeNumericUpDown = new NumericUpDown();
            label1 = new Label();
            toolStrip = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            fileOpenToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            SaveAsToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            undoToolStripButton = new ToolStripButton();
            redoToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            colorLabel = new ToolStripLabel();
            colorToolStripButton = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            fillToolStripButton = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripDropDownButton = new ToolStripDropDownButton();
            toolStripLabel1 = new ToolStripLabel();
            toolStripComboBox = new ToolStripComboBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            toolImageList = new ImageList(components);
            eraseToolStripButton = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sizeNumericUpDown).BeginInit();
            toolStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox.BackColor = Color.Gainsboro;
            pictureBox.Location = new Point(12, 59);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(802, 535);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.SizeChanged += pictureBox_SizeChanged;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // sizeNumericUpDown
            // 
            sizeNumericUpDown.DecimalPlaces = 1;
            sizeNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            sizeNumericUpDown.Location = new Point(276, 73);
            sizeNumericUpDown.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            sizeNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sizeNumericUpDown.Name = "sizeNumericUpDown";
            sizeNumericUpDown.Size = new Size(80, 23);
            sizeNumericUpDown.TabIndex = 2;
            sizeNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            sizeNumericUpDown.ValueChanged += sizeNumericUpDown_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(214, 77);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 3;
            label1.Text = "Pen Size :";
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(25, 25);
            toolStrip.Items.AddRange(new ToolStripItem[] { newToolStripButton, fileOpenToolStripButton, saveToolStripButton, SaveAsToolStripButton, toolStripSeparator1, undoToolStripButton, redoToolStripButton, toolStripSeparator2, colorLabel, colorToolStripButton, toolStripSeparator4, fillToolStripButton, toolStripSeparator5, eraseToolStripButton, toolStripDropDownButton, toolStripLabel1, toolStripComboBox });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(826, 32);
            toolStrip.TabIndex = 4;
            toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = Properties.Resources.Newfile_page_document_empty_6315;
            newToolStripButton.ImageTransparentColor = Color.Magenta;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(29, 29);
            newToolStripButton.Text = "NewImage";
            newToolStripButton.Click += newToolStripButton_Click;
            // 
            // fileOpenToolStripButton
            // 
            fileOpenToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            fileOpenToolStripButton.Image = Properties.Resources.open_file256_25211;
            fileOpenToolStripButton.ImageTransparentColor = Color.Magenta;
            fileOpenToolStripButton.Name = "fileOpenToolStripButton";
            fileOpenToolStripButton.Size = new Size(29, 29);
            fileOpenToolStripButton.Text = "Open File";
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = Properties.Resources.save_file_disk_open_searsh_loading_clipboard_1513;
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(29, 29);
            saveToolStripButton.Text = "Save";
            // 
            // SaveAsToolStripButton
            // 
            SaveAsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SaveAsToolStripButton.Image = Properties.Resources.diskette_save_saveas_1514;
            SaveAsToolStripButton.ImageTransparentColor = Color.Magenta;
            SaveAsToolStripButton.Name = "SaveAsToolStripButton";
            SaveAsToolStripButton.Size = new Size(29, 29);
            SaveAsToolStripButton.Text = "Save As";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 32);
            // 
            // undoToolStripButton
            // 
            undoToolStripButton.AutoToolTip = false;
            undoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            undoToolStripButton.Enabled = false;
            undoToolStripButton.Image = Properties.Resources.undoarrow_undo_1534;
            undoToolStripButton.ImageTransparentColor = Color.Magenta;
            undoToolStripButton.Name = "undoToolStripButton";
            undoToolStripButton.Size = new Size(29, 29);
            undoToolStripButton.Text = "Undo";
            undoToolStripButton.TextImageRelation = TextImageRelation.TextBeforeImage;
            undoToolStripButton.Click += undoToolStripButton_Click;
            // 
            // redoToolStripButton
            // 
            redoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            redoToolStripButton.Enabled = false;
            redoToolStripButton.Image = Properties.Resources.redoarrow_rehace_1547;
            redoToolStripButton.ImageTransparentColor = Color.Magenta;
            redoToolStripButton.Name = "redoToolStripButton";
            redoToolStripButton.Size = new Size(29, 29);
            redoToolStripButton.Text = "Redo";
            redoToolStripButton.Click += redoToolStripButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 32);
            // 
            // colorLabel
            // 
            colorLabel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new Size(0, 29);
            colorLabel.Text = "        ";
            // 
            // colorToolStripButton
            // 
            colorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            colorToolStripButton.Image = Properties.Resources.color_1222;
            colorToolStripButton.ImageTransparentColor = Color.Magenta;
            colorToolStripButton.Name = "colorToolStripButton";
            colorToolStripButton.Size = new Size(29, 29);
            colorToolStripButton.Text = "Color";
            colorToolStripButton.Click += colorButton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 32);
            // 
            // fillToolStripButton
            // 
            fillToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            fillToolStripButton.Image = Properties.Resources.preferences_color_801;
            fillToolStripButton.ImageTransparentColor = Color.Magenta;
            fillToolStripButton.Name = "fillToolStripButton";
            fillToolStripButton.Size = new Size(29, 29);
            fillToolStripButton.Text = "Back Color";
            fillToolStripButton.Click += fillToolStripButton_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 32);
            // 
            // toolStripDropDownButton
            // 
            toolStripDropDownButton.ForeColor = Color.Black;
            toolStripDropDownButton.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton.Name = "toolStripDropDownButton";
            toolStripDropDownButton.Size = new Size(42, 29);
            toolStripDropDownButton.Text = "Tool";
            toolStripDropDownButton.DropDownItemClicked += toolStripDropDownButton1_DropDownItemClicked;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.ForeColor = Color.Black;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(57, 29);
            toolStripLabel1.Text = "  Tool size";
            // 
            // toolStripComboBox
            // 
            toolStripComboBox.FlatStyle = FlatStyle.Standard;
            toolStripComboBox.Name = "toolStripComboBox";
            toolStripComboBox.Size = new Size(75, 32);
            toolStripComboBox.Text = "1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(826, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // toolImageList
            // 
            toolImageList.ColorDepth = ColorDepth.Depth32Bit;
            toolImageList.ImageStream = (ImageListStreamer)resources.GetObject("toolImageList.ImageStream");
            toolImageList.TransparentColor = Color.Transparent;
            toolImageList.Images.SetKeyName(0, "edit_clear_all_icon_181104.png");
            toolImageList.Images.SetKeyName(1, "straightline_83780.png");
            toolImageList.Images.SetKeyName(2, "curve_96294.png");
            toolImageList.Images.SetKeyName(3, "chart_line_variant_icon_136800.png");
            toolImageList.Images.SetKeyName(4, "rectangle_icon_144162.png");
            toolImageList.Images.SetKeyName(5, "rectangle_icon_131506.png");
            toolImageList.Images.SetKeyName(6, "circle_80174.png");
            toolImageList.Images.SetKeyName(7, "circle_80914.png");
            toolImageList.Images.SetKeyName(8, "ellipse_adobe_illustrator_tool_circle_icon_189052.png");
            toolImageList.Images.SetKeyName(9, "ellipse_icon_138652.png");
            toolImageList.Images.SetKeyName(10, "software-shape-polygon_97830.png");
            toolImageList.Images.SetKeyName(11, "polygon_icon_215372.png");
            // 
            // eraseToolStripButton
            // 
            eraseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            eraseToolStripButton.Image = Properties.Resources.clear256_24830;
            eraseToolStripButton.ImageTransparentColor = Color.Magenta;
            eraseToolStripButton.Name = "eraseToolStripButton";
            eraseToolStripButton.Size = new Size(29, 29);
            eraseToolStripButton.Text = "Clear All";
            eraseToolStripButton.Click += eraseToolStripButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(826, 675);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            Controls.Add(sizeNumericUpDown);
            Controls.Add(pictureBox);
            ForeColor = Color.White;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(300, 300);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)sizeNumericUpDown).EndInit();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private NumericUpDown sizeNumericUpDown;
        private Label label1;
        private ToolStrip toolStrip;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripButton newToolStripButton;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripButton undoToolStripButton;
        private ToolStripButton redoToolStripButton;
        private ToolStripButton fileOpenToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripButton SaveAsToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton toolStripDropDownButton;
        private ToolStripComboBox toolStripComboBox;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton colorToolStripButton;
        private ToolStripButton fillToolStripButton;
        private ToolStripLabel colorLabel;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ImageList toolImageList;
        private ToolStripButton eraseToolStripButton;
    }
}