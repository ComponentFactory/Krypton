namespace KryptonGalleryExamples
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.kryptonGallery1 = new ComponentFactory.Krypton.Ribbon.KryptonGallery();
            this.kryptonGalleryRange1 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange2 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange3 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.imageListMedium = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxImages = new System.Windows.Forms.GroupBox();
            this.radioLargeList = new System.Windows.Forms.RadioButton();
            this.radioMediumList = new System.Windows.Forms.RadioButton();
            this.radioSmallList = new System.Windows.Forms.RadioButton();
            this.groupBoxSize = new System.Windows.Forms.GroupBox();
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBoxSections = new System.Windows.Forms.GroupBox();
            this.checkBoxAddCustomItems = new System.Windows.Forms.CheckBox();
            this.checkBoxGroupImages = new System.Windows.Forms.CheckBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.groupBoxImages.SuspendLayout();
            this.groupBoxSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
            this.groupBoxSections.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonGallery1
            // 
            this.kryptonGallery1.AutoSize = true;
            this.kryptonGallery1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.kryptonGallery1.DropButtonRanges.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange[] {
            this.kryptonGalleryRange1,
            this.kryptonGalleryRange2,
            this.kryptonGalleryRange3});
            this.kryptonGallery1.ImageList = this.imageListMedium;
            this.kryptonGallery1.Location = new System.Drawing.Point(178, 20);
            this.kryptonGallery1.Name = "kryptonGallery1";
            this.kryptonGallery1.PreferredItemSize = new System.Drawing.Size(4, 1);
            this.kryptonGallery1.Size = new System.Drawing.Size(174, 46);
            this.kryptonGallery1.TabIndex = 4;
            this.kryptonGallery1.GalleryDropMenu += new System.EventHandler<ComponentFactory.Krypton.Ribbon.GalleryDropMenuEventArgs>(this.kryptonGallery1_GalleryDropMenu);
            // 
            // kryptonGalleryRange1
            // 
            this.kryptonGalleryRange1.Heading = "Stars";
            this.kryptonGalleryRange1.ImageIndexEnd = 4;
            this.kryptonGalleryRange1.ImageIndexStart = 0;
            // 
            // kryptonGalleryRange2
            // 
            this.kryptonGalleryRange2.Heading = "Arrows";
            this.kryptonGalleryRange2.ImageIndexEnd = 12;
            this.kryptonGalleryRange2.ImageIndexStart = 5;
            // 
            // kryptonGalleryRange3
            // 
            this.kryptonGalleryRange3.Heading = "Users";
            this.kryptonGalleryRange3.ImageIndexEnd = 15;
            this.kryptonGalleryRange3.ImageIndexStart = 13;
            // 
            // imageListMedium
            // 
            this.imageListMedium.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMedium.ImageStream")));
            this.imageListMedium.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMedium.Images.SetKeyName(0, "star_yellow.png");
            this.imageListMedium.Images.SetKeyName(1, "star_blue.png");
            this.imageListMedium.Images.SetKeyName(2, "star_green.png");
            this.imageListMedium.Images.SetKeyName(3, "star_grey.png");
            this.imageListMedium.Images.SetKeyName(4, "star_red.png");
            this.imageListMedium.Images.SetKeyName(5, "arrow_up_green.png");
            this.imageListMedium.Images.SetKeyName(6, "arrow_down_green.png");
            this.imageListMedium.Images.SetKeyName(7, "arrow_left_green.png");
            this.imageListMedium.Images.SetKeyName(8, "arrow_right_green.png");
            this.imageListMedium.Images.SetKeyName(9, "arrow_up_blue.png");
            this.imageListMedium.Images.SetKeyName(10, "arrow_down_blue.png");
            this.imageListMedium.Images.SetKeyName(11, "arrow_left_blue.png");
            this.imageListMedium.Images.SetKeyName(12, "arrow_right_blue.png");
            this.imageListMedium.Images.SetKeyName(13, "user3.png");
            this.imageListMedium.Images.SetKeyName(14, "user1.png");
            this.imageListMedium.Images.SetKeyName(15, "user2.png");
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "star_yellow.png");
            this.imageListSmall.Images.SetKeyName(1, "star_blue.png");
            this.imageListSmall.Images.SetKeyName(2, "star_green.png");
            this.imageListSmall.Images.SetKeyName(3, "star_grey.png");
            this.imageListSmall.Images.SetKeyName(4, "star_red.png");
            this.imageListSmall.Images.SetKeyName(5, "arrow_up_green.png");
            this.imageListSmall.Images.SetKeyName(6, "arrow_down_green.png");
            this.imageListSmall.Images.SetKeyName(7, "arrow_left_green.png");
            this.imageListSmall.Images.SetKeyName(8, "arrow_right_green.png");
            this.imageListSmall.Images.SetKeyName(9, "arrow_up_blue.png");
            this.imageListSmall.Images.SetKeyName(10, "arrow_down_blue.png");
            this.imageListSmall.Images.SetKeyName(11, "arrow_left_blue.png");
            this.imageListSmall.Images.SetKeyName(12, "arrow_right_blue.png");
            this.imageListSmall.Images.SetKeyName(13, "user3.png");
            this.imageListSmall.Images.SetKeyName(14, "user1.png");
            this.imageListSmall.Images.SetKeyName(15, "user2.png");
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "star_yellow.png");
            this.imageListLarge.Images.SetKeyName(1, "star_blue.png");
            this.imageListLarge.Images.SetKeyName(2, "star_green.png");
            this.imageListLarge.Images.SetKeyName(3, "star_grey.png");
            this.imageListLarge.Images.SetKeyName(4, "star_red.png");
            this.imageListLarge.Images.SetKeyName(5, "arrow_up_green.png");
            this.imageListLarge.Images.SetKeyName(6, "arrow_down_green.png");
            this.imageListLarge.Images.SetKeyName(7, "arrow_left_green.png");
            this.imageListLarge.Images.SetKeyName(8, "arrow_right_green.png");
            this.imageListLarge.Images.SetKeyName(9, "arrow_up_blue.png");
            this.imageListLarge.Images.SetKeyName(10, "arrow_down_blue.png");
            this.imageListLarge.Images.SetKeyName(11, "arrow_left_blue.png");
            this.imageListLarge.Images.SetKeyName(12, "arrow_right_blue.png");
            this.imageListLarge.Images.SetKeyName(13, "user3.png");
            this.imageListLarge.Images.SetKeyName(14, "user1.png");
            this.imageListLarge.Images.SetKeyName(15, "user2.png");
            // 
            // groupBoxImages
            // 
            this.groupBoxImages.Controls.Add(this.radioLargeList);
            this.groupBoxImages.Controls.Add(this.radioMediumList);
            this.groupBoxImages.Controls.Add(this.radioSmallList);
            this.groupBoxImages.Location = new System.Drawing.Point(13, 13);
            this.groupBoxImages.Name = "groupBoxImages";
            this.groupBoxImages.Size = new System.Drawing.Size(149, 114);
            this.groupBoxImages.TabIndex = 0;
            this.groupBoxImages.TabStop = false;
            this.groupBoxImages.Text = "Gallery Images";
            // 
            // radioLargeList
            // 
            this.radioLargeList.AutoSize = true;
            this.radioLargeList.Location = new System.Drawing.Point(15, 72);
            this.radioLargeList.Name = "radioLargeList";
            this.radioLargeList.Size = new System.Drawing.Size(104, 17);
            this.radioLargeList.TabIndex = 2;
            this.radioLargeList.Text = "Large Image List";
            this.radioLargeList.UseVisualStyleBackColor = true;
            this.radioLargeList.CheckedChanged += new System.EventHandler(this.radioLargeList_CheckedChanged);
            // 
            // radioMediumList
            // 
            this.radioMediumList.AutoSize = true;
            this.radioMediumList.Checked = true;
            this.radioMediumList.Location = new System.Drawing.Point(15, 49);
            this.radioMediumList.Name = "radioMediumList";
            this.radioMediumList.Size = new System.Drawing.Size(113, 17);
            this.radioMediumList.TabIndex = 1;
            this.radioMediumList.TabStop = true;
            this.radioMediumList.Text = "Medium Image List";
            this.radioMediumList.UseVisualStyleBackColor = true;
            this.radioMediumList.CheckedChanged += new System.EventHandler(this.radioMediumList_CheckedChanged);
            // 
            // radioSmallList
            // 
            this.radioSmallList.AutoSize = true;
            this.radioSmallList.Location = new System.Drawing.Point(15, 26);
            this.radioSmallList.Name = "radioSmallList";
            this.radioSmallList.Size = new System.Drawing.Size(101, 17);
            this.radioSmallList.TabIndex = 0;
            this.radioSmallList.Text = "Small Image List";
            this.radioSmallList.UseVisualStyleBackColor = true;
            this.radioSmallList.CheckedChanged += new System.EventHandler(this.radioSmallList_CheckedChanged);
            // 
            // groupBoxSize
            // 
            this.groupBoxSize.Controls.Add(this.numericHeight);
            this.groupBoxSize.Controls.Add(this.label2);
            this.groupBoxSize.Controls.Add(this.label1);
            this.groupBoxSize.Controls.Add(this.numericWidth);
            this.groupBoxSize.Location = new System.Drawing.Point(13, 134);
            this.groupBoxSize.Name = "groupBoxSize";
            this.groupBoxSize.Size = new System.Drawing.Size(149, 89);
            this.groupBoxSize.TabIndex = 1;
            this.groupBoxSize.TabStop = false;
            this.groupBoxSize.Text = "Gallery Size";
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(91, 53);
            this.numericHeight.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(42, 21);
            this.numericHeight.TabIndex = 3;
            this.numericHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.ValueChanged += new System.EventHandler(this.numericHeight_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height (Items)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width (Items)";
            // 
            // numericWidth
            // 
            this.numericWidth.Location = new System.Drawing.Point(91, 26);
            this.numericWidth.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(42, 21);
            this.numericWidth.TabIndex = 1;
            this.numericWidth.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericWidth.ValueChanged += new System.EventHandler(this.numericWidth_ValueChanged);
            // 
            // groupBoxSections
            // 
            this.groupBoxSections.Controls.Add(this.checkBoxAddCustomItems);
            this.groupBoxSections.Controls.Add(this.checkBoxGroupImages);
            this.groupBoxSections.Location = new System.Drawing.Point(13, 230);
            this.groupBoxSections.Name = "groupBoxSections";
            this.groupBoxSections.Size = new System.Drawing.Size(149, 91);
            this.groupBoxSections.TabIndex = 2;
            this.groupBoxSections.TabStop = false;
            this.groupBoxSections.Text = "Gallery Drop Menu";
            // 
            // checkBoxAddCustomItems
            // 
            this.checkBoxAddCustomItems.AutoSize = true;
            this.checkBoxAddCustomItems.Location = new System.Drawing.Point(15, 54);
            this.checkBoxAddCustomItems.Name = "checkBoxAddCustomItems";
            this.checkBoxAddCustomItems.Size = new System.Drawing.Size(114, 17);
            this.checkBoxAddCustomItems.TabIndex = 1;
            this.checkBoxAddCustomItems.Text = "Add Custom Items";
            this.checkBoxAddCustomItems.UseVisualStyleBackColor = true;
            // 
            // checkBoxGroupImages
            // 
            this.checkBoxGroupImages.AutoSize = true;
            this.checkBoxGroupImages.Checked = true;
            this.checkBoxGroupImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGroupImages.Location = new System.Drawing.Point(15, 31);
            this.checkBoxGroupImages.Name = "checkBoxGroupImages";
            this.checkBoxGroupImages.Size = new System.Drawing.Size(93, 17);
            this.checkBoxGroupImages.TabIndex = 0;
            this.checkBoxGroupImages.Text = "Group Images";
            this.checkBoxGroupImages.UseVisualStyleBackColor = true;
            this.checkBoxGroupImages.CheckedChanged += new System.EventHandler(this.checkBoxGroupImages_CheckedChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(13, 327);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(149, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 366);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxSections);
            this.Controls.Add(this.groupBoxSize);
            this.Controls.Add(this.groupBoxImages);
            this.Controls.Add(this.kryptonGallery1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Krypton Gallery Examples";
            this.groupBoxImages.ResumeLayout(false);
            this.groupBoxImages.PerformLayout();
            this.groupBoxSize.ResumeLayout(false);
            this.groupBoxSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
            this.groupBoxSections.ResumeLayout(false);
            this.groupBoxSections.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonGallery kryptonGallery1;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ImageList imageListMedium;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.GroupBox groupBoxImages;
        private System.Windows.Forms.RadioButton radioLargeList;
        private System.Windows.Forms.RadioButton radioMediumList;
        private System.Windows.Forms.RadioButton radioSmallList;
        private System.Windows.Forms.GroupBox groupBoxSize;
        private System.Windows.Forms.NumericUpDown numericHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericWidth;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange1;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange2;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange3;
        private System.Windows.Forms.GroupBox groupBoxSections;
        private System.Windows.Forms.CheckBox checkBoxAddCustomItems;
        private System.Windows.Forms.CheckBox checkBoxGroupImages;
        private System.Windows.Forms.Button buttonClose;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

