namespace KryptonTreeViewExamples
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.kryptonTreeView = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSystem = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.checkSparkle = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.check2010Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.check2007Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonAppend = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonInsert = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonRemove = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCheckSet = new ComponentFactory.Krypton.Toolkit.KryptonCheckSet(this.components);
            this.groupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(500, 513);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.propertyGrid);
            this.groupBox.Location = new System.Drawing.Point(285, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(290, 495);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Properties for Selected KryptonTreeView";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this.kryptonTreeView;
            this.propertyGrid.Size = new System.Drawing.Size(278, 470);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // kryptonTreeView
            // 
            this.kryptonTreeView.HideSelection = false;
            this.kryptonTreeView.HotTracking = true;
            this.kryptonTreeView.ImageIndex = 3;
            this.kryptonTreeView.ImageList = this.imageList;
            this.kryptonTreeView.Location = new System.Drawing.Point(13, 31);
            this.kryptonTreeView.Name = "kryptonTreeView";
            this.kryptonTreeView.SelectedImageIndex = 3;
            this.kryptonTreeView.Size = new System.Drawing.Size(255, 285);
            this.kryptonTreeView.TabIndex = 6;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "flag_bahamas.png");
            this.imageList.Images.SetKeyName(1, "flag_china.png");
            this.imageList.Images.SetKeyName(2, "flag_ecuador.png");
            this.imageList.Images.SetKeyName(3, "flag_england.png");
            this.imageList.Images.SetKeyName(4, "flag_france.png");
            this.imageList.Images.SetKeyName(5, "flag_greece.png");
            this.imageList.Images.SetKeyName(6, "flag_netherlands.png");
            this.imageList.Images.SetKeyName(7, "flag_poland.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkSystem);
            this.groupBox1.Controls.Add(this.checkSparkle);
            this.groupBox1.Controls.Add(this.check2010Blue);
            this.groupBox1.Controls.Add(this.check2007Blue);
            this.groupBox1.Controls.Add(this.buttonAppend);
            this.groupBox1.Controls.Add(this.buttonInsert);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Location = new System.Drawing.Point(13, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 168);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // checkSystem
            // 
            this.checkSystem.AutoSize = true;
            this.checkSystem.Location = new System.Drawing.Point(125, 126);
            this.checkSystem.Name = "checkSystem";
            this.checkSystem.Size = new System.Drawing.Size(113, 25);
            this.checkSystem.TabIndex = 7;
            this.checkSystem.Values.Text = "System";
            // 
            // checkSparkle
            // 
            this.checkSparkle.AutoSize = true;
            this.checkSparkle.Location = new System.Drawing.Point(125, 93);
            this.checkSparkle.Name = "checkSparkle";
            this.checkSparkle.Size = new System.Drawing.Size(113, 25);
            this.checkSparkle.TabIndex = 6;
            this.checkSparkle.Values.Text = "Sparkle - Blue";
            // 
            // check2010Blue
            // 
            this.check2010Blue.AutoSize = true;
            this.check2010Blue.Checked = true;
            this.check2010Blue.Location = new System.Drawing.Point(125, 31);
            this.check2010Blue.Name = "check2010Blue";
            this.check2010Blue.Size = new System.Drawing.Size(113, 25);
            this.check2010Blue.TabIndex = 5;
            this.check2010Blue.Values.Text = "Office 2010 - Blue";
            // 
            // check2007Blue
            // 
            this.check2007Blue.AutoSize = true;
            this.check2007Blue.Location = new System.Drawing.Point(125, 62);
            this.check2007Blue.Name = "check2007Blue";
            this.check2007Blue.Size = new System.Drawing.Size(113, 25);
            this.check2007Blue.TabIndex = 4;
            this.check2007Blue.Values.Text = "Office 2007 - Blue";
            // 
            // buttonAppend
            // 
            this.buttonAppend.AutoSize = true;
            this.buttonAppend.Location = new System.Drawing.Point(19, 30);
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(90, 25);
            this.buttonAppend.TabIndex = 0;
            this.buttonAppend.Values.Text = "Append";
            this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.AutoSize = true;
            this.buttonInsert.Location = new System.Drawing.Point(19, 62);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(90, 25);
            this.buttonInsert.TabIndex = 1;
            this.buttonInsert.Values.Text = "Insert";
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(19, 126);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(90, 25);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Values.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.AutoSize = true;
            this.buttonRemove.Location = new System.Drawing.Point(19, 94);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(90, 25);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Values.Text = "Remove";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // kryptonCheckSet
            // 
            this.kryptonCheckSet.CheckButtons.Add(this.checkSystem);
            this.kryptonCheckSet.CheckButtons.Add(this.checkSparkle);
            this.kryptonCheckSet.CheckButtons.Add(this.check2010Blue);
            this.kryptonCheckSet.CheckButtons.Add(this.check2007Blue);
            this.kryptonCheckSet.CheckedButton = this.check2010Blue;
            this.kryptonCheckSet.CheckedButtonChanged += new System.EventHandler(this.kryptonCheckSet_CheckedButtonChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 548);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.kryptonTreeView);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonTreeView Examples";
            this.groupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView kryptonTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton checkSystem;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton checkSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton check2010Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton check2007Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonAppend;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonInsert;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonRemove;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckSet kryptonCheckSet;
        private System.Windows.Forms.ImageList imageList;
    }
}

