namespace OrientationPlusAlignment
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
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage4 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.groupBoxBarOrientation = new System.Windows.Forms.GroupBox();
            this.radioOrientationRight = new System.Windows.Forms.RadioButton();
            this.radioOrientationLeft = new System.Windows.Forms.RadioButton();
            this.radioOrientationBottom = new System.Windows.Forms.RadioButton();
            this.radioOrientationTop = new System.Windows.Forms.RadioButton();
            this.groupBoxItemOrientation = new System.Windows.Forms.GroupBox();
            this.radioItemFixedRight = new System.Windows.Forms.RadioButton();
            this.radioItemFixedLeft = new System.Windows.Forms.RadioButton();
            this.radioItemFixedBottom = new System.Windows.Forms.RadioButton();
            this.radioItemFixedTop = new System.Windows.Forms.RadioButton();
            this.radioItemAuto = new System.Windows.Forms.RadioButton();
            this.groupBoxItemAlignment = new System.Windows.Forms.GroupBox();
            this.radioItemFar = new System.Windows.Forms.RadioButton();
            this.radioItemCenter = new System.Windows.Forms.RadioButton();
            this.radioItemNear = new System.Windows.Forms.RadioButton();
            this.groupBoxItemSizing = new System.Windows.Forms.GroupBox();
            this.radioSizingSameWidthHeight = new System.Windows.Forms.RadioButton();
            this.radioSizingSameWidth = new System.Windows.Forms.RadioButton();
            this.radioSizingSameHeight = new System.Windows.Forms.RadioButton();
            this.radioSizingIndividual = new System.Windows.Forms.RadioButton();
            this.groupBoxItemSizes = new System.Windows.Forms.GroupBox();
            this.numericUpDownBarFirstItemInset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMaxSizeComma = new System.Windows.Forms.Label();
            this.labelMinSizeComma = new System.Windows.Forms.Label();
            this.numericUpDownMaxItemSizeY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinItemSizeY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBarMinHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMaxItemSizeX = new System.Windows.Forms.NumericUpDown();
            this.labelMaxItemSize = new System.Windows.Forms.Label();
            this.labelMinItemSize = new System.Windows.Forms.Label();
            this.numericUpDownMinItemSizeX = new System.Windows.Forms.NumericUpDown();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioModeRibbonTabs = new System.Windows.Forms.RadioButton();
            this.radioModesCheckButton = new System.Windows.Forms.RadioButton();
            this.radioModeTabs = new System.Windows.Forms.RadioButton();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            this.kryptonNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).BeginInit();
            this.groupBoxBarOrientation.SuspendLayout();
            this.groupBoxItemOrientation.SuspendLayout();
            this.groupBoxItemAlignment.SuspendLayout();
            this.groupBoxItemSizing.SuspendLayout();
            this.groupBoxItemSizes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarFirstItemInset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxItemSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinItemSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarMinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxItemSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinItemSizeX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonNavigator1.Bar.TabBorderStyle = ComponentFactory.Krypton.Toolkit.TabBorderStyle.RoundedEqualMedium;
            this.kryptonNavigator1.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator1.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator1.Location = new System.Drawing.Point(166, 18);
            this.kryptonNavigator1.Name = "kryptonNavigator1";
            this.kryptonNavigator1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2,
            this.kryptonPage3,
            this.kryptonPage4});
            this.kryptonNavigator1.SelectedIndex = 3;
            this.kryptonNavigator1.Size = new System.Drawing.Size(383, 338);
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonNavigator1.StateCommon.CheckButton.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonNavigator1.TabIndex = 0;
            this.kryptonNavigator1.Text = "kryptonNavigator1";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageLarge")));
            this.kryptonPage1.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageMedium")));
            this.kryptonPage1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageSmall")));
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(381, 298);
            this.kryptonPage1.Text = "One";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "D9FC21A91AC9495DD9FC21A91AC9495D";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageLarge")));
            this.kryptonPage2.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageMedium")));
            this.kryptonPage2.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageSmall")));
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(381, 298);
            this.kryptonPage2.Text = "Second Page";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "3FE5A65E0C4647C33FE5A65E0C4647C3";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageLarge")));
            this.kryptonPage3.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageMedium")));
            this.kryptonPage3.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageSmall")));
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(381, 298);
            this.kryptonPage3.Text = "3";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "20FA2E1F5BF246EC20FA2E1F5BF246EC";
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageLarge")));
            this.kryptonPage4.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageMedium")));
            this.kryptonPage4.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageSmall")));
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Size = new System.Drawing.Size(381, 298);
            this.kryptonPage4.Text = "Omega";
            this.kryptonPage4.ToolTipTitle = "Page ToolTip";
            this.kryptonPage4.UniqueName = "C6345925E3CD40F0C6345925E3CD40F0";
            // 
            // groupBoxBarOrientation
            // 
            this.groupBoxBarOrientation.Controls.Add(this.radioOrientationRight);
            this.groupBoxBarOrientation.Controls.Add(this.radioOrientationLeft);
            this.groupBoxBarOrientation.Controls.Add(this.radioOrientationBottom);
            this.groupBoxBarOrientation.Controls.Add(this.radioOrientationTop);
            this.groupBoxBarOrientation.Location = new System.Drawing.Point(12, 114);
            this.groupBoxBarOrientation.Name = "groupBoxBarOrientation";
            this.groupBoxBarOrientation.Size = new System.Drawing.Size(147, 120);
            this.groupBoxBarOrientation.TabIndex = 2;
            this.groupBoxBarOrientation.TabStop = false;
            this.groupBoxBarOrientation.Text = "Bar Orientation";
            // 
            // radioOrientationRight
            // 
            this.radioOrientationRight.AutoSize = true;
            this.radioOrientationRight.Location = new System.Drawing.Point(18, 93);
            this.radioOrientationRight.Name = "radioOrientationRight";
            this.radioOrientationRight.Size = new System.Drawing.Size(50, 17);
            this.radioOrientationRight.TabIndex = 3;
            this.radioOrientationRight.TabStop = true;
            this.radioOrientationRight.Text = "Right";
            this.radioOrientationRight.UseVisualStyleBackColor = true;
            this.radioOrientationRight.CheckedChanged += new System.EventHandler(this.radioOrientationRight_CheckedChanged);
            // 
            // radioOrientationLeft
            // 
            this.radioOrientationLeft.AutoSize = true;
            this.radioOrientationLeft.Location = new System.Drawing.Point(18, 70);
            this.radioOrientationLeft.Name = "radioOrientationLeft";
            this.radioOrientationLeft.Size = new System.Drawing.Size(44, 17);
            this.radioOrientationLeft.TabIndex = 2;
            this.radioOrientationLeft.TabStop = true;
            this.radioOrientationLeft.Text = "Left";
            this.radioOrientationLeft.UseVisualStyleBackColor = true;
            this.radioOrientationLeft.CheckedChanged += new System.EventHandler(this.radioOrientationLeft_CheckedChanged);
            // 
            // radioOrientationBottom
            // 
            this.radioOrientationBottom.AutoSize = true;
            this.radioOrientationBottom.Location = new System.Drawing.Point(18, 47);
            this.radioOrientationBottom.Name = "radioOrientationBottom";
            this.radioOrientationBottom.Size = new System.Drawing.Size(59, 17);
            this.radioOrientationBottom.TabIndex = 1;
            this.radioOrientationBottom.TabStop = true;
            this.radioOrientationBottom.Text = "Bottom";
            this.radioOrientationBottom.UseVisualStyleBackColor = true;
            this.radioOrientationBottom.CheckedChanged += new System.EventHandler(this.radioOrientationBottom_CheckedChanged);
            // 
            // radioOrientationTop
            // 
            this.radioOrientationTop.AutoSize = true;
            this.radioOrientationTop.Location = new System.Drawing.Point(18, 24);
            this.radioOrientationTop.Name = "radioOrientationTop";
            this.radioOrientationTop.Size = new System.Drawing.Size(43, 17);
            this.radioOrientationTop.TabIndex = 0;
            this.radioOrientationTop.TabStop = true;
            this.radioOrientationTop.Text = "Top";
            this.radioOrientationTop.UseVisualStyleBackColor = true;
            this.radioOrientationTop.CheckedChanged += new System.EventHandler(this.radioOrientationTop_CheckedChanged);
            // 
            // groupBoxItemOrientation
            // 
            this.groupBoxItemOrientation.Controls.Add(this.radioItemFixedRight);
            this.groupBoxItemOrientation.Controls.Add(this.radioItemFixedLeft);
            this.groupBoxItemOrientation.Controls.Add(this.radioItemFixedBottom);
            this.groupBoxItemOrientation.Controls.Add(this.radioItemFixedTop);
            this.groupBoxItemOrientation.Controls.Add(this.radioItemAuto);
            this.groupBoxItemOrientation.Location = new System.Drawing.Point(12, 240);
            this.groupBoxItemOrientation.Name = "groupBoxItemOrientation";
            this.groupBoxItemOrientation.Size = new System.Drawing.Size(147, 142);
            this.groupBoxItemOrientation.TabIndex = 3;
            this.groupBoxItemOrientation.TabStop = false;
            this.groupBoxItemOrientation.Text = "Item Orientation";
            // 
            // radioItemFixedRight
            // 
            this.radioItemFixedRight.AutoSize = true;
            this.radioItemFixedRight.Location = new System.Drawing.Point(18, 116);
            this.radioItemFixedRight.Name = "radioItemFixedRight";
            this.radioItemFixedRight.Size = new System.Drawing.Size(79, 17);
            this.radioItemFixedRight.TabIndex = 4;
            this.radioItemFixedRight.TabStop = true;
            this.radioItemFixedRight.Text = "Fixed Right";
            this.radioItemFixedRight.UseVisualStyleBackColor = true;
            this.radioItemFixedRight.CheckedChanged += new System.EventHandler(this.radioItemFixedRight_CheckedChanged);
            // 
            // radioItemFixedLeft
            // 
            this.radioItemFixedLeft.AutoSize = true;
            this.radioItemFixedLeft.Location = new System.Drawing.Point(18, 93);
            this.radioItemFixedLeft.Name = "radioItemFixedLeft";
            this.radioItemFixedLeft.Size = new System.Drawing.Size(73, 17);
            this.radioItemFixedLeft.TabIndex = 3;
            this.radioItemFixedLeft.TabStop = true;
            this.radioItemFixedLeft.Text = "Fixed Left";
            this.radioItemFixedLeft.UseVisualStyleBackColor = true;
            this.radioItemFixedLeft.CheckedChanged += new System.EventHandler(this.radioItemFixedLeft_CheckedChanged);
            // 
            // radioItemFixedBottom
            // 
            this.radioItemFixedBottom.AutoSize = true;
            this.radioItemFixedBottom.Location = new System.Drawing.Point(18, 70);
            this.radioItemFixedBottom.Name = "radioItemFixedBottom";
            this.radioItemFixedBottom.Size = new System.Drawing.Size(88, 17);
            this.radioItemFixedBottom.TabIndex = 2;
            this.radioItemFixedBottom.TabStop = true;
            this.radioItemFixedBottom.Text = "Fixed Bottom";
            this.radioItemFixedBottom.UseVisualStyleBackColor = true;
            this.radioItemFixedBottom.CheckedChanged += new System.EventHandler(this.radioItemFixedBottom_CheckedChanged);
            // 
            // radioItemFixedTop
            // 
            this.radioItemFixedTop.AutoSize = true;
            this.radioItemFixedTop.Location = new System.Drawing.Point(18, 47);
            this.radioItemFixedTop.Name = "radioItemFixedTop";
            this.radioItemFixedTop.Size = new System.Drawing.Size(72, 17);
            this.radioItemFixedTop.TabIndex = 1;
            this.radioItemFixedTop.TabStop = true;
            this.radioItemFixedTop.Text = "Fixed Top";
            this.radioItemFixedTop.UseVisualStyleBackColor = true;
            this.radioItemFixedTop.CheckedChanged += new System.EventHandler(this.radioItemFixedTop_CheckedChanged);
            // 
            // radioItemAuto
            // 
            this.radioItemAuto.AutoSize = true;
            this.radioItemAuto.Location = new System.Drawing.Point(18, 24);
            this.radioItemAuto.Name = "radioItemAuto";
            this.radioItemAuto.Size = new System.Drawing.Size(48, 17);
            this.radioItemAuto.TabIndex = 0;
            this.radioItemAuto.TabStop = true;
            this.radioItemAuto.Text = "Auto";
            this.radioItemAuto.UseVisualStyleBackColor = true;
            this.radioItemAuto.CheckedChanged += new System.EventHandler(this.radioItemAuto_CheckedChanged);
            // 
            // groupBoxItemAlignment
            // 
            this.groupBoxItemAlignment.Controls.Add(this.radioItemFar);
            this.groupBoxItemAlignment.Controls.Add(this.radioItemCenter);
            this.groupBoxItemAlignment.Controls.Add(this.radioItemNear);
            this.groupBoxItemAlignment.Location = new System.Drawing.Point(13, 387);
            this.groupBoxItemAlignment.Name = "groupBoxItemAlignment";
            this.groupBoxItemAlignment.Size = new System.Drawing.Size(147, 96);
            this.groupBoxItemAlignment.TabIndex = 4;
            this.groupBoxItemAlignment.TabStop = false;
            this.groupBoxItemAlignment.Text = "Item Alignment";
            // 
            // radioItemFar
            // 
            this.radioItemFar.AutoSize = true;
            this.radioItemFar.Location = new System.Drawing.Point(18, 70);
            this.radioItemFar.Name = "radioItemFar";
            this.radioItemFar.Size = new System.Drawing.Size(41, 17);
            this.radioItemFar.TabIndex = 2;
            this.radioItemFar.TabStop = true;
            this.radioItemFar.Text = "Far";
            this.radioItemFar.UseVisualStyleBackColor = true;
            this.radioItemFar.CheckedChanged += new System.EventHandler(this.radioItemFar_CheckedChanged);
            // 
            // radioItemCenter
            // 
            this.radioItemCenter.AutoSize = true;
            this.radioItemCenter.Location = new System.Drawing.Point(18, 47);
            this.radioItemCenter.Name = "radioItemCenter";
            this.radioItemCenter.Size = new System.Drawing.Size(58, 17);
            this.radioItemCenter.TabIndex = 1;
            this.radioItemCenter.TabStop = true;
            this.radioItemCenter.Text = "Center";
            this.radioItemCenter.UseVisualStyleBackColor = true;
            this.radioItemCenter.CheckedChanged += new System.EventHandler(this.radioItemCenter_CheckedChanged);
            // 
            // radioItemNear
            // 
            this.radioItemNear.AutoSize = true;
            this.radioItemNear.Location = new System.Drawing.Point(18, 24);
            this.radioItemNear.Name = "radioItemNear";
            this.radioItemNear.Size = new System.Drawing.Size(48, 17);
            this.radioItemNear.TabIndex = 0;
            this.radioItemNear.TabStop = true;
            this.radioItemNear.Text = "Near";
            this.radioItemNear.UseVisualStyleBackColor = true;
            this.radioItemNear.CheckedChanged += new System.EventHandler(this.radioItemNear_CheckedChanged);
            // 
            // groupBoxItemSizing
            // 
            this.groupBoxItemSizing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxItemSizing.Controls.Add(this.radioSizingSameWidthHeight);
            this.groupBoxItemSizing.Controls.Add(this.radioSizingSameWidth);
            this.groupBoxItemSizing.Controls.Add(this.radioSizingSameHeight);
            this.groupBoxItemSizing.Controls.Add(this.radioSizingIndividual);
            this.groupBoxItemSizing.Location = new System.Drawing.Point(166, 363);
            this.groupBoxItemSizing.Name = "groupBoxItemSizing";
            this.groupBoxItemSizing.Size = new System.Drawing.Size(167, 120);
            this.groupBoxItemSizing.TabIndex = 5;
            this.groupBoxItemSizing.TabStop = false;
            this.groupBoxItemSizing.Text = "Item Sizing";
            // 
            // radioSizingSameWidthHeight
            // 
            this.radioSizingSameWidthHeight.AutoSize = true;
            this.radioSizingSameWidthHeight.Location = new System.Drawing.Point(18, 93);
            this.radioSizingSameWidthHeight.Name = "radioSizingSameWidthHeight";
            this.radioSizingSameWidthHeight.Size = new System.Drawing.Size(140, 17);
            this.radioSizingSameWidthHeight.TabIndex = 3;
            this.radioSizingSameWidthHeight.TabStop = true;
            this.radioSizingSameWidthHeight.Text = "All Same Width & Height";
            this.radioSizingSameWidthHeight.UseMnemonic = false;
            this.radioSizingSameWidthHeight.UseVisualStyleBackColor = true;
            this.radioSizingSameWidthHeight.CheckedChanged += new System.EventHandler(this.radioSizingSameWidthHeight_CheckedChanged);
            // 
            // radioSizingSameWidth
            // 
            this.radioSizingSameWidth.AutoSize = true;
            this.radioSizingSameWidth.Location = new System.Drawing.Point(18, 70);
            this.radioSizingSameWidth.Name = "radioSizingSameWidth";
            this.radioSizingSameWidth.Size = new System.Drawing.Size(96, 17);
            this.radioSizingSameWidth.TabIndex = 2;
            this.radioSizingSameWidth.TabStop = true;
            this.radioSizingSameWidth.Text = "All Same Width";
            this.radioSizingSameWidth.UseVisualStyleBackColor = true;
            this.radioSizingSameWidth.CheckedChanged += new System.EventHandler(this.radioSizingSameWidth_CheckedChanged);
            // 
            // radioSizingSameHeight
            // 
            this.radioSizingSameHeight.AutoSize = true;
            this.radioSizingSameHeight.Location = new System.Drawing.Point(18, 47);
            this.radioSizingSameHeight.Name = "radioSizingSameHeight";
            this.radioSizingSameHeight.Size = new System.Drawing.Size(99, 17);
            this.radioSizingSameHeight.TabIndex = 1;
            this.radioSizingSameHeight.TabStop = true;
            this.radioSizingSameHeight.Text = "All Same Height";
            this.radioSizingSameHeight.UseVisualStyleBackColor = true;
            this.radioSizingSameHeight.CheckedChanged += new System.EventHandler(this.radioSizingSameHeight_CheckedChanged);
            // 
            // radioSizingIndividual
            // 
            this.radioSizingIndividual.AutoSize = true;
            this.radioSizingIndividual.Location = new System.Drawing.Point(18, 24);
            this.radioSizingIndividual.Name = "radioSizingIndividual";
            this.radioSizingIndividual.Size = new System.Drawing.Size(101, 17);
            this.radioSizingIndividual.TabIndex = 0;
            this.radioSizingIndividual.TabStop = true;
            this.radioSizingIndividual.Text = "Individual Sizing";
            this.radioSizingIndividual.UseVisualStyleBackColor = true;
            this.radioSizingIndividual.CheckedChanged += new System.EventHandler(this.radioSizingIndividual_CheckedChanged);
            // 
            // groupBoxItemSizes
            // 
            this.groupBoxItemSizes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownBarFirstItemInset);
            this.groupBoxItemSizes.Controls.Add(this.label2);
            this.groupBoxItemSizes.Controls.Add(this.labelMaxSizeComma);
            this.groupBoxItemSizes.Controls.Add(this.labelMinSizeComma);
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownMaxItemSizeY);
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownMinItemSizeY);
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownBarMinHeight);
            this.groupBoxItemSizes.Controls.Add(this.label1);
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownMaxItemSizeX);
            this.groupBoxItemSizes.Controls.Add(this.labelMaxItemSize);
            this.groupBoxItemSizes.Controls.Add(this.labelMinItemSize);
            this.groupBoxItemSizes.Controls.Add(this.numericUpDownMinItemSizeX);
            this.groupBoxItemSizes.Location = new System.Drawing.Point(340, 363);
            this.groupBoxItemSizes.Name = "groupBoxItemSizes";
            this.groupBoxItemSizes.Size = new System.Drawing.Size(209, 120);
            this.groupBoxItemSizes.TabIndex = 6;
            this.groupBoxItemSizes.TabStop = false;
            this.groupBoxItemSizes.Text = "Sizing";
            // 
            // numericUpDownBarFirstItemInset
            // 
            this.numericUpDownBarFirstItemInset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownBarFirstItemInset.Location = new System.Drawing.Point(85, 22);
            this.numericUpDownBarFirstItemInset.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownBarFirstItemInset.Name = "numericUpDownBarFirstItemInset";
            this.numericUpDownBarFirstItemInset.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownBarFirstItemInset.TabIndex = 11;
            this.numericUpDownBarFirstItemInset.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBarFirstItemInset.ValueChanged += new System.EventHandler(this.numericUpDownBarFirstItemInset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Bar First Item";
            // 
            // labelMaxSizeComma
            // 
            this.labelMaxSizeComma.AutoSize = true;
            this.labelMaxSizeComma.Location = new System.Drawing.Point(134, 99);
            this.labelMaxSizeComma.Name = "labelMaxSizeComma";
            this.labelMaxSizeComma.Size = new System.Drawing.Size(11, 13);
            this.labelMaxSizeComma.TabIndex = 9;
            this.labelMaxSizeComma.Text = ",";
            // 
            // labelMinSizeComma
            // 
            this.labelMinSizeComma.AutoSize = true;
            this.labelMinSizeComma.Location = new System.Drawing.Point(135, 76);
            this.labelMinSizeComma.Name = "labelMinSizeComma";
            this.labelMinSizeComma.Size = new System.Drawing.Size(11, 13);
            this.labelMinSizeComma.TabIndex = 8;
            this.labelMinSizeComma.Text = ",";
            // 
            // numericUpDownMaxItemSizeY
            // 
            this.numericUpDownMaxItemSizeY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeY.Location = new System.Drawing.Point(148, 91);
            this.numericUpDownMaxItemSizeY.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeY.Name = "numericUpDownMaxItemSizeY";
            this.numericUpDownMaxItemSizeY.Size = new System.Drawing.Size(47, 21);
            this.numericUpDownMaxItemSizeY.TabIndex = 7;
            this.numericUpDownMaxItemSizeY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeY.ValueChanged += new System.EventHandler(this.numericUpDownMaxItemSize);
            // 
            // numericUpDownMinItemSizeY
            // 
            this.numericUpDownMinItemSizeY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeY.Location = new System.Drawing.Point(148, 68);
            this.numericUpDownMinItemSizeY.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeY.Name = "numericUpDownMinItemSizeY";
            this.numericUpDownMinItemSizeY.Size = new System.Drawing.Size(47, 21);
            this.numericUpDownMinItemSizeY.TabIndex = 6;
            this.numericUpDownMinItemSizeY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeY.ValueChanged += new System.EventHandler(this.numericUpDownMinItemSize);
            // 
            // numericUpDownBarMinHeight
            // 
            this.numericUpDownBarMinHeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownBarMinHeight.Location = new System.Drawing.Point(85, 45);
            this.numericUpDownBarMinHeight.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownBarMinHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBarMinHeight.Name = "numericUpDownBarMinHeight";
            this.numericUpDownBarMinHeight.Size = new System.Drawing.Size(110, 21);
            this.numericUpDownBarMinHeight.TabIndex = 5;
            this.numericUpDownBarMinHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBarMinHeight.ValueChanged += new System.EventHandler(this.numericUpDownBarMinHeight_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bar Min Height";
            // 
            // numericUpDownMaxItemSizeX
            // 
            this.numericUpDownMaxItemSizeX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeX.Location = new System.Drawing.Point(85, 91);
            this.numericUpDownMaxItemSizeX.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeX.Name = "numericUpDownMaxItemSizeX";
            this.numericUpDownMaxItemSizeX.Size = new System.Drawing.Size(47, 21);
            this.numericUpDownMaxItemSizeX.TabIndex = 3;
            this.numericUpDownMaxItemSizeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxItemSizeX.ValueChanged += new System.EventHandler(this.numericUpDownMaxItemSize);
            // 
            // labelMaxItemSize
            // 
            this.labelMaxItemSize.AutoSize = true;
            this.labelMaxItemSize.Location = new System.Drawing.Point(8, 95);
            this.labelMaxItemSize.Name = "labelMaxItemSize";
            this.labelMaxItemSize.Size = new System.Drawing.Size(74, 13);
            this.labelMaxItemSize.TabIndex = 2;
            this.labelMaxItemSize.Text = "Item Max Size";
            // 
            // labelMinItemSize
            // 
            this.labelMinItemSize.AutoSize = true;
            this.labelMinItemSize.Location = new System.Drawing.Point(12, 72);
            this.labelMinItemSize.Name = "labelMinItemSize";
            this.labelMinItemSize.Size = new System.Drawing.Size(70, 13);
            this.labelMinItemSize.TabIndex = 1;
            this.labelMinItemSize.Text = "Item Min Size";
            // 
            // numericUpDownMinItemSizeX
            // 
            this.numericUpDownMinItemSizeX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeX.Location = new System.Drawing.Point(85, 68);
            this.numericUpDownMinItemSizeX.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeX.Name = "numericUpDownMinItemSizeX";
            this.numericUpDownMinItemSizeX.Size = new System.Drawing.Size(47, 21);
            this.numericUpDownMinItemSizeX.TabIndex = 0;
            this.numericUpDownMinItemSizeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinItemSizeX.ValueChanged += new System.EventHandler(this.numericUpDownMinItemSize);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.Location = new System.Drawing.Point(473, 489);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioModeRibbonTabs);
            this.groupBox1.Controls.Add(this.radioModesCheckButton);
            this.groupBox1.Controls.Add(this.radioModeTabs);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modes";
            // 
            // radioModeRibbonTabs
            // 
            this.radioModeRibbonTabs.AutoSize = true;
            this.radioModeRibbonTabs.Location = new System.Drawing.Point(18, 47);
            this.radioModeRibbonTabs.Name = "radioModeRibbonTabs";
            this.radioModeRibbonTabs.Size = new System.Drawing.Size(81, 17);
            this.radioModeRibbonTabs.TabIndex = 1;
            this.radioModeRibbonTabs.TabStop = true;
            this.radioModeRibbonTabs.Text = "RibbonTabs";
            this.radioModeRibbonTabs.UseVisualStyleBackColor = true;
            this.radioModeRibbonTabs.CheckedChanged += new System.EventHandler(this.radioModeRibbonTabs_CheckedChanged);
            // 
            // radioModesCheckButton
            // 
            this.radioModesCheckButton.AutoSize = true;
            this.radioModesCheckButton.Location = new System.Drawing.Point(18, 70);
            this.radioModesCheckButton.Name = "radioModesCheckButton";
            this.radioModesCheckButton.Size = new System.Drawing.Size(91, 17);
            this.radioModesCheckButton.TabIndex = 2;
            this.radioModesCheckButton.TabStop = true;
            this.radioModesCheckButton.Text = "CheckButtons";
            this.radioModesCheckButton.UseVisualStyleBackColor = true;
            this.radioModesCheckButton.CheckedChanged += new System.EventHandler(this.radioModesCheckButton_CheckedChanged);
            // 
            // radioModeTabs
            // 
            this.radioModeTabs.AutoSize = true;
            this.radioModeTabs.Location = new System.Drawing.Point(18, 24);
            this.radioModeTabs.Name = "radioModeTabs";
            this.radioModeTabs.Size = new System.Drawing.Size(48, 17);
            this.radioModeTabs.TabIndex = 0;
            this.radioModeTabs.TabStop = true;
            this.radioModeTabs.Text = "Tabs";
            this.radioModeTabs.UseVisualStyleBackColor = true;
            this.radioModeTabs.CheckedChanged += new System.EventHandler(this.radioModeTabs_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 518);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxItemSizes);
            this.Controls.Add(this.groupBoxItemSizing);
            this.Controls.Add(this.groupBoxItemAlignment);
            this.Controls.Add(this.groupBoxItemOrientation);
            this.Controls.Add(this.groupBoxBarOrientation);
            this.Controls.Add(this.kryptonNavigator1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(569, 552);
            this.Name = "Form1";
            this.Text = "Orientation + Alignment";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            this.kryptonNavigator1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).EndInit();
            this.groupBoxBarOrientation.ResumeLayout(false);
            this.groupBoxBarOrientation.PerformLayout();
            this.groupBoxItemOrientation.ResumeLayout(false);
            this.groupBoxItemOrientation.PerformLayout();
            this.groupBoxItemAlignment.ResumeLayout(false);
            this.groupBoxItemAlignment.PerformLayout();
            this.groupBoxItemSizing.ResumeLayout(false);
            this.groupBoxItemSizing.PerformLayout();
            this.groupBoxItemSizes.ResumeLayout(false);
            this.groupBoxItemSizes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarFirstItemInset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxItemSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinItemSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBarMinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxItemSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinItemSizeX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private System.Windows.Forms.GroupBox groupBoxBarOrientation;
        private System.Windows.Forms.RadioButton radioOrientationRight;
        private System.Windows.Forms.RadioButton radioOrientationLeft;
        private System.Windows.Forms.RadioButton radioOrientationBottom;
        private System.Windows.Forms.RadioButton radioOrientationTop;
        private System.Windows.Forms.GroupBox groupBoxItemOrientation;
        private System.Windows.Forms.RadioButton radioItemFixedRight;
        private System.Windows.Forms.RadioButton radioItemFixedLeft;
        private System.Windows.Forms.RadioButton radioItemFixedBottom;
        private System.Windows.Forms.RadioButton radioItemFixedTop;
        private System.Windows.Forms.RadioButton radioItemAuto;
        private System.Windows.Forms.GroupBox groupBoxItemAlignment;
        private System.Windows.Forms.RadioButton radioItemFar;
        private System.Windows.Forms.RadioButton radioItemCenter;
        private System.Windows.Forms.RadioButton radioItemNear;
        private System.Windows.Forms.GroupBox groupBoxItemSizing;
        private System.Windows.Forms.RadioButton radioSizingSameWidthHeight;
        private System.Windows.Forms.RadioButton radioSizingSameWidth;
        private System.Windows.Forms.RadioButton radioSizingSameHeight;
        private System.Windows.Forms.RadioButton radioSizingIndividual;
        private System.Windows.Forms.GroupBox groupBoxItemSizes;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxItemSizeX;
        private System.Windows.Forms.Label labelMaxItemSize;
        private System.Windows.Forms.Label labelMinItemSize;
        private System.Windows.Forms.NumericUpDown numericUpDownMinItemSizeX;
        private System.Windows.Forms.Button buttonClose;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage4;
        private System.Windows.Forms.NumericUpDown numericUpDownBarMinHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMaxSizeComma;
        private System.Windows.Forms.Label labelMinSizeComma;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxItemSizeY;
        private System.Windows.Forms.NumericUpDown numericUpDownMinItemSizeY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioModesCheckButton;
        private System.Windows.Forms.RadioButton radioModeTabs;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private System.Windows.Forms.NumericUpDown numericUpDownBarFirstItemInset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioModeRibbonTabs;
    }
}

