namespace RibbonGallery
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
            this.kryptonRibbon = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonRibbonTab1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.kryptonRibbonGroupButton1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupButton3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroup2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.galleryNormal = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.galleryRanges = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery();
            this.kryptonGalleryRange5 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange6 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange7 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.galleryCustom = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery();
            this.kryptonGalleryRange1 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange2 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange3 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonGalleryRange4 = new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange();
            this.kryptonFillPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonLabelExplain = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabelTitle = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonFillPanel)).BeginInit();
            this.kryptonFillPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonRibbon
            // 
            this.kryptonRibbon.HideRibbonSize = new System.Drawing.Size(100, 100);
            this.kryptonRibbon.Name = "kryptonRibbon";
            this.kryptonRibbon.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1});
            this.kryptonRibbon.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.kryptonRibbon.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.kryptonRibbonTab1});
            this.kryptonRibbon.SelectedContext = null;
            this.kryptonRibbon.SelectedTab = this.kryptonRibbonTab1;
            this.kryptonRibbon.Size = new System.Drawing.Size(769, 114);
            this.kryptonRibbon.TabIndex = 0;
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonContextMenuItem1.Image")));
            this.kryptonContextMenuItem1.Text = "E&xit";
            this.kryptonContextMenuItem1.Click += new System.EventHandler(this.kryptonContextMenuItem1_Click);
            // 
            // kryptonRibbonTab1
            // 
            this.kryptonRibbonTab1.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup1,
            this.kryptonRibbonGroup2});
            // 
            // kryptonRibbonGroup1
            // 
            this.kryptonRibbonGroup1.AllowCollapsed = false;
            this.kryptonRibbonGroup1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroup1.Image")));
            this.kryptonRibbonGroup1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1});
            this.kryptonRibbonGroup1.KeyTipGroup = "P";
            this.kryptonRibbonGroup1.TextLine1 = "Palettes";
            // 
            // kryptonRibbonGroupTriple1
            // 
            this.kryptonRibbonGroupTriple1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton1,
            this.kryptonRibbonGroupButton2,
            this.kryptonRibbonGroupButton3});
            this.kryptonRibbonGroupTriple1.MaximumSize = ComponentFactory.Krypton.Ribbon.GroupItemSize.Medium;
            this.kryptonRibbonGroupTriple1.MinimumSize = ComponentFactory.Krypton.Ribbon.GroupItemSize.Medium;
            // 
            // kryptonRibbonGroupButton1
            // 
            this.kryptonRibbonGroupButton1.ButtonType = ComponentFactory.Krypton.Ribbon.GroupButtonType.Check;
            this.kryptonRibbonGroupButton1.Checked = true;
            this.kryptonRibbonGroupButton1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButton1.ImageSmall")));
            this.kryptonRibbonGroupButton1.TextLine1 = "Office 2010";
            this.kryptonRibbonGroupButton1.TextLine2 = "Blue";
            this.kryptonRibbonGroupButton1.Click += new System.EventHandler(this.kryptonRibbonGroupButton1_Click);
            // 
            // kryptonRibbonGroupButton2
            // 
            this.kryptonRibbonGroupButton2.ButtonType = ComponentFactory.Krypton.Ribbon.GroupButtonType.Check;
            this.kryptonRibbonGroupButton2.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButton2.ImageSmall")));
            this.kryptonRibbonGroupButton2.TextLine1 = "Office 2007";
            this.kryptonRibbonGroupButton2.TextLine2 = "Silver";
            this.kryptonRibbonGroupButton2.Click += new System.EventHandler(this.kryptonRibbonGroupButton2_Click);
            // 
            // kryptonRibbonGroupButton3
            // 
            this.kryptonRibbonGroupButton3.ButtonType = ComponentFactory.Krypton.Ribbon.GroupButtonType.Check;
            this.kryptonRibbonGroupButton3.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroupButton3.ImageSmall")));
            this.kryptonRibbonGroupButton3.TextLine1 = "Sparkle Orange";
            this.kryptonRibbonGroupButton3.Click += new System.EventHandler(this.kryptonRibbonGroupButton3_Click);
            // 
            // kryptonRibbonGroup2
            // 
            this.kryptonRibbonGroup2.Image = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroup2.Image")));
            this.kryptonRibbonGroup2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.galleryNormal,
            this.galleryRanges,
            this.galleryCustom});
            this.kryptonRibbonGroup2.TextLine1 = "Galleries";
            // 
            // galleryNormal
            // 
            this.galleryNormal.DropButtonItemWidth = 6;
            this.galleryNormal.ImageLarge = ((System.Drawing.Image)(resources.GetObject("galleryNormal.ImageLarge")));
            this.galleryNormal.ImageList = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "flag_australia.png");
            this.imageList1.Images.SetKeyName(1, "flag_bahamas.png");
            this.imageList1.Images.SetKeyName(2, "flag_belgium.png");
            this.imageList1.Images.SetKeyName(3, "flag_brazil.png");
            this.imageList1.Images.SetKeyName(4, "flag_canada.png");
            this.imageList1.Images.SetKeyName(5, "flag_england.png");
            this.imageList1.Images.SetKeyName(6, "flag_germany.png");
            this.imageList1.Images.SetKeyName(7, "flag_jamaica.png");
            this.imageList1.Images.SetKeyName(8, "flag_norway.png");
            this.imageList1.Images.SetKeyName(9, "flag_scotland.png");
            this.imageList1.Images.SetKeyName(10, "flag_south_africa.png");
            this.imageList1.Images.SetKeyName(11, "flag_spain.png");
            this.imageList1.Images.SetKeyName(12, "flag_switzerland.png");
            this.imageList1.Images.SetKeyName(13, "flag_united_kingdom.png");
            this.imageList1.Images.SetKeyName(14, "flag_usa.png");
            this.imageList1.Images.SetKeyName(15, "flag_wales.png");
            // 
            // galleryRanges
            // 
            this.galleryRanges.DropButtonItemWidth = 6;
            this.galleryRanges.DropButtonRanges.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonGalleryRange[] {
            this.kryptonGalleryRange5,
            this.kryptonGalleryRange6,
            this.kryptonGalleryRange7});
            this.galleryRanges.ImageLarge = ((System.Drawing.Image)(resources.GetObject("galleryRanges.ImageLarge")));
            this.galleryRanges.ImageList = this.imageList1;
            this.galleryRanges.KeyTip = "Y";
            // 
            // kryptonGalleryRange5
            // 
            this.kryptonGalleryRange5.Heading = "Users";
            this.kryptonGalleryRange5.ImageIndexEnd = 5;
            this.kryptonGalleryRange5.ImageIndexStart = 0;
            // 
            // kryptonGalleryRange6
            // 
            this.kryptonGalleryRange6.Heading = "Managers";
            this.kryptonGalleryRange6.ImageIndexEnd = 11;
            this.kryptonGalleryRange6.ImageIndexStart = 6;
            // 
            // kryptonGalleryRange7
            // 
            this.kryptonGalleryRange7.Heading = "Others";
            this.kryptonGalleryRange7.ImageIndexEnd = 15;
            this.kryptonGalleryRange7.ImageIndexStart = 12;
            // 
            // galleryCustom
            // 
            this.galleryCustom.DropButtonItemWidth = 6;
            this.galleryCustom.ImageLarge = ((System.Drawing.Image)(resources.GetObject("galleryCustom.ImageLarge")));
            this.galleryCustom.ImageList = this.imageList1;
            this.galleryCustom.KeyTip = "Z";
            this.galleryCustom.GalleryDropMenu += new System.EventHandler<ComponentFactory.Krypton.Ribbon.GalleryDropMenuEventArgs>(this.galleryCustom_GalleryDropMenu);
            // 
            // kryptonGalleryRange1
            // 
            this.kryptonGalleryRange1.Heading = "Group 1";
            this.kryptonGalleryRange1.ImageIndexEnd = 4;
            this.kryptonGalleryRange1.ImageIndexStart = 0;
            // 
            // kryptonGalleryRange2
            // 
            this.kryptonGalleryRange2.Heading = "Group 2";
            this.kryptonGalleryRange2.ImageIndexEnd = 9;
            this.kryptonGalleryRange2.ImageIndexStart = 5;
            // 
            // kryptonGalleryRange3
            // 
            this.kryptonGalleryRange3.Heading = "Group 3";
            this.kryptonGalleryRange3.ImageIndexEnd = 14;
            this.kryptonGalleryRange3.ImageIndexStart = 10;
            // 
            // kryptonGalleryRange4
            // 
            this.kryptonGalleryRange4.Heading = "Group 4";
            this.kryptonGalleryRange4.ImageIndexEnd = 20;
            this.kryptonGalleryRange4.ImageIndexStart = 15;
            // 
            // kryptonFillPanel
            // 
            this.kryptonFillPanel.Controls.Add(this.kryptonPanel1);
            this.kryptonFillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonFillPanel.Location = new System.Drawing.Point(0, 114);
            this.kryptonFillPanel.Name = "kryptonFillPanel";
            this.kryptonFillPanel.Size = new System.Drawing.Size(769, 174);
            this.kryptonFillPanel.TabIndex = 1;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabelExplain);
            this.kryptonPanel1.Controls.Add(this.kryptonLabelTitle);
            this.kryptonPanel1.Location = new System.Drawing.Point(16, 16);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(335, 101);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabelExplain
            // 
            this.kryptonLabelExplain.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabelExplain.Location = new System.Drawing.Point(8, 36);
            this.kryptonLabelExplain.Name = "kryptonLabelExplain";
            this.kryptonLabelExplain.Size = new System.Drawing.Size(312, 49);
            this.kryptonLabelExplain.TabIndex = 1;
            this.kryptonLabelExplain.Values.Text = "The Left gallery shows a standard drop down of all images.\r\nThe Middle gallery pr" +
                "ovides grouping in the drop down.\r\nThe Right gallery customizes the drop down me" +
                "nu.\r\n";
            // 
            // kryptonLabelTitle
            // 
            this.kryptonLabelTitle.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitlePanel;
            this.kryptonLabelTitle.Location = new System.Drawing.Point(8, 8);
            this.kryptonLabelTitle.Name = "kryptonLabelTitle";
            this.kryptonLabelTitle.Size = new System.Drawing.Size(84, 28);
            this.kryptonLabelTitle.TabIndex = 0;
            this.kryptonLabelTitle.Values.Text = "Galleries";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 288);
            this.Controls.Add(this.kryptonFillPanel);
            this.Controls.Add(this.kryptonRibbon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(381, 322);
            this.Name = "Form1";
            this.Text = "Ribbon Gallery";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonFillPanel)).EndInit();
            this.kryptonFillPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonRibbon kryptonRibbon;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonFillPanel;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton3;
        private System.Windows.Forms.ImageList imageList1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup2;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange1;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange2;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange3;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange4;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery galleryNormal;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery galleryRanges;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange5;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange6;
        private ComponentFactory.Krypton.Ribbon.KryptonGalleryRange kryptonGalleryRange7;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGallery galleryCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabelExplain;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabelTitle;
    }
}

