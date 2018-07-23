namespace NavigatorPlayground
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
            this.kryptonNavigator1 = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.groupBoxProperties = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxPages = new System.Windows.Forms.GroupBox();
            this.kryptonButtonEnable = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonRemove = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.groupBoxProperties.SuspendLayout();
            this.groupBoxPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonNavigator1.Location = new System.Drawing.Point(18, 31);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.kryptonNavigator1.SelectedIndex = 0;
            this.kryptonNavigator1.Size = new System.Drawing.Size(297, 355);
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            this.kryptonNavigator1.SelectedPageChanged += new System.EventHandler(this.kryptonNavigator1_SelectedPageChanged);
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageLarge")));
            this.kryptonPage1.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageMedium")));
            this.kryptonPage1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageSmall")));
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(295, 329);
            this.kryptonPage1.Text = "Page 1";
            this.kryptonPage1.TextDescription = "Page 1 Description";
            this.kryptonPage1.TextTitle = "Page 1 Title";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "DA289FB3CA334928DA289FB3CA334928";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageLarge")));
            this.kryptonPage2.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageMedium")));
            this.kryptonPage2.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageSmall")));
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(295, 329);
            this.kryptonPage2.Text = "Page 2";
            this.kryptonPage2.TextDescription = "Page 2 Description";
            this.kryptonPage2.TextTitle = "Page 2 Title";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "FAFF770F50A44D94FAFF770F50A44D94";
            // 
            // groupBoxProperties
            // 
            this.groupBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProperties.Controls.Add(this.propertyGrid1);
            this.groupBoxProperties.Location = new System.Drawing.Point(328, 12);
            this.groupBoxProperties.Name = "groupBoxProperties";
            this.groupBoxProperties.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxProperties.Size = new System.Drawing.Size(312, 464);
            this.groupBoxProperties.TabIndex = 2;
            this.groupBoxProperties.TabStop = false;
            this.groupBoxProperties.Text = "Properties for KryptonNavigator";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(5, 19);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(302, 440);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(565, 485);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxPages
            // 
            this.groupBoxPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPages.Controls.Add(this.kryptonButtonEnable);
            this.groupBoxPages.Controls.Add(this.kryptonButtonClear);
            this.groupBoxPages.Controls.Add(this.kryptonButtonRemove);
            this.groupBoxPages.Controls.Add(this.kryptonButtonAdd);
            this.groupBoxPages.Location = new System.Drawing.Point(13, 407);
            this.groupBoxPages.Name = "groupBoxPages";
            this.groupBoxPages.Size = new System.Drawing.Size(309, 69);
            this.groupBoxPages.TabIndex = 1;
            this.groupBoxPages.TabStop = false;
            this.groupBoxPages.Text = "Pages";
            // 
            // kryptonButtonEnable
            // 
            this.kryptonButtonEnable.Location = new System.Drawing.Point(234, 26);
            this.kryptonButtonEnable.Name = "kryptonButtonEnable";
            this.kryptonButtonEnable.Size = new System.Drawing.Size(65, 29);
            this.kryptonButtonEnable.TabIndex = 3;
            this.kryptonButtonEnable.Values.Text = "Enable";
            this.kryptonButtonEnable.Click += new System.EventHandler(this.kryptonButtonEnable_Click);
            // 
            // kryptonButtonClear
            // 
            this.kryptonButtonClear.Location = new System.Drawing.Point(159, 26);
            this.kryptonButtonClear.Name = "kryptonButtonClear";
            this.kryptonButtonClear.Size = new System.Drawing.Size(65, 29);
            this.kryptonButtonClear.TabIndex = 2;
            this.kryptonButtonClear.Values.Text = "Clear";
            this.kryptonButtonClear.Click += new System.EventHandler(this.kryptonButtonClear_Click);
            // 
            // kryptonButtonRemove
            // 
            this.kryptonButtonRemove.Location = new System.Drawing.Point(84, 26);
            this.kryptonButtonRemove.Name = "kryptonButtonRemove";
            this.kryptonButtonRemove.Size = new System.Drawing.Size(65, 29);
            this.kryptonButtonRemove.TabIndex = 1;
            this.kryptonButtonRemove.Values.Text = "Remove";
            this.kryptonButtonRemove.Click += new System.EventHandler(this.kryptonButtonRemove_Click);
            // 
            // kryptonButtonAdd
            // 
            this.kryptonButtonAdd.Location = new System.Drawing.Point(9, 26);
            this.kryptonButtonAdd.Name = "kryptonButtonAdd";
            this.kryptonButtonAdd.Size = new System.Drawing.Size(65, 29);
            this.kryptonButtonAdd.TabIndex = 0;
            this.kryptonButtonAdd.Values.Text = "Add";
            this.kryptonButtonAdd.Click += new System.EventHandler(this.kryptonButtonAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "airmail_closed.png");
            this.imageList1.Images.SetKeyName(1, "brickwall.png");
            this.imageList1.Images.SetKeyName(2, "calculator.png");
            this.imageList1.Images.SetKeyName(3, "clients.png");
            this.imageList1.Images.SetKeyName(4, "fire.png");
            this.imageList1.Images.SetKeyName(5, "newspaper.png");
            this.imageList1.Images.SetKeyName(6, "robber.png");
            this.imageList1.Images.SetKeyName(7, "virus.png");
            this.imageList1.Images.SetKeyName(8, "web.png");
            this.imageList1.Images.SetKeyName(9, "worm.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 516);
            this.Controls.Add(this.groupBoxPages);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxProperties);
            this.Controls.Add(this.kryptonNavigator1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(530, 326);
            this.Name = "Form1";
            this.Text = "Navigator Playground";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.groupBoxProperties.ResumeLayout(false);
            this.groupBoxPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private System.Windows.Forms.GroupBox groupBoxProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxPages;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonRemove;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonAdd;
        private System.Windows.Forms.ImageList imageList1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonEnable;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

