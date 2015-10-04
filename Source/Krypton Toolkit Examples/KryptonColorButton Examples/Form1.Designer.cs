namespace KryptonColorButtonExamples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupProperties = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBlue = new System.Windows.Forms.GroupBox();
            this.blueRight = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.blueLeft = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.blueBottom = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.blueTop = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.blueDropDown = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.blueSplitter = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.group2003 = new System.Windows.Forms.GroupBox();
            this.sparkleBottom = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.sparkleTop = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.sparkleLeft = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.sparkleRight = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.systemRight = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.systemDown = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.systemLeft = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.systemUp = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.silverRight = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.silverDown = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.silverLeft = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.silverUp = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.groupProperties.SuspendLayout();
            this.groupBlue.SuspendLayout();
            this.group2003.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(483, 464);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupProperties
            // 
            this.groupProperties.Controls.Add(this.propertyGrid);
            this.groupProperties.Location = new System.Drawing.Point(239, 12);
            this.groupProperties.Name = "groupProperties";
            this.groupProperties.Size = new System.Drawing.Size(319, 446);
            this.groupProperties.TabIndex = 4;
            this.groupProperties.TabStop = false;
            this.groupProperties.Text = "Properties for Selected KryptonColorButton";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(307, 421);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // groupBlue
            // 
            this.groupBlue.Controls.Add(this.blueRight);
            this.groupBlue.Controls.Add(this.blueLeft);
            this.groupBlue.Controls.Add(this.blueBottom);
            this.groupBlue.Controls.Add(this.blueTop);
            this.groupBlue.Controls.Add(this.blueDropDown);
            this.groupBlue.Controls.Add(this.blueSplitter);
            this.groupBlue.Location = new System.Drawing.Point(12, 12);
            this.groupBlue.Name = "groupBlue";
            this.groupBlue.Size = new System.Drawing.Size(221, 107);
            this.groupBlue.TabIndex = 0;
            this.groupBlue.TabStop = false;
            this.groupBlue.Text = "Office 2007 - Blue";
            // 
            // blueRight
            // 
            this.blueRight.AutoSize = true;
            this.blueRight.Location = new System.Drawing.Point(171, 69);
            this.blueRight.Name = "blueRight";
            this.blueRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueRight.SchemeStandard = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.blueRight.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Mono8;
            this.blueRight.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.blueRight.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.blueRight.Size = new System.Drawing.Size(34, 25);
            this.blueRight.Splitter = false;
            this.blueRight.Strings.StandardColors = "Monochrome Colors";
            this.blueRight.Strings.ThemeColors = "Basic Colors";
            this.blueRight.TabIndex = 5;
            this.blueRight.Values.Image = ((System.Drawing.Image)(resources.GetObject("blueRight.Values.Image")));
            this.blueRight.Values.Text = "";
            this.blueRight.VisibleMoreColors = false;
            this.blueRight.VisibleNoColor = false;
            this.blueRight.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // blueLeft
            // 
            this.blueLeft.AutoSize = true;
            this.blueLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.blueLeft.Location = new System.Drawing.Point(133, 69);
            this.blueLeft.Name = "blueLeft";
            this.blueLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueLeft.SchemeStandard = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.blueLeft.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Mono8;
            this.blueLeft.SelectedColor = System.Drawing.Color.Yellow;
            this.blueLeft.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.blueLeft.Size = new System.Drawing.Size(34, 25);
            this.blueLeft.Splitter = false;
            this.blueLeft.Strings.StandardColors = "Monochrome Colors";
            this.blueLeft.Strings.ThemeColors = "Basic Colors";
            this.blueLeft.TabIndex = 3;
            this.blueLeft.Values.Image = ((System.Drawing.Image)(resources.GetObject("blueLeft.Values.Image")));
            this.blueLeft.Values.Text = "";
            this.blueLeft.VisibleMoreColors = false;
            this.blueLeft.VisibleNoColor = false;
            this.blueLeft.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // blueBottom
            // 
            this.blueBottom.AutoSize = true;
            this.blueBottom.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.blueBottom.Location = new System.Drawing.Point(171, 29);
            this.blueBottom.Name = "blueBottom";
            this.blueBottom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueBottom.SchemeStandard = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.blueBottom.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Mono8;
            this.blueBottom.SelectedColor = System.Drawing.Color.Fuchsia;
            this.blueBottom.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.blueBottom.Size = new System.Drawing.Size(34, 34);
            this.blueBottom.Splitter = false;
            this.blueBottom.Strings.StandardColors = "Monochrome Colors";
            this.blueBottom.Strings.ThemeColors = "Basic Colors";
            this.blueBottom.TabIndex = 4;
            this.blueBottom.Values.Image = ((System.Drawing.Image)(resources.GetObject("blueBottom.Values.Image")));
            this.blueBottom.Values.Text = "";
            this.blueBottom.VisibleMoreColors = false;
            this.blueBottom.VisibleNoColor = false;
            this.blueBottom.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // blueTop
            // 
            this.blueTop.AutoSize = true;
            this.blueTop.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.blueTop.Location = new System.Drawing.Point(133, 29);
            this.blueTop.Name = "blueTop";
            this.blueTop.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueTop.SchemeStandard = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.blueTop.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Mono8;
            this.blueTop.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.blueTop.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.blueTop.Size = new System.Drawing.Size(34, 34);
            this.blueTop.Splitter = false;
            this.blueTop.Strings.StandardColors = "Monochrome Colors";
            this.blueTop.Strings.ThemeColors = "Basic Colors";
            this.blueTop.TabIndex = 2;
            this.blueTop.Values.Image = global::KryptonColorButtonExamples.Properties.Resources.Empty16x16;
            this.blueTop.Values.Text = "";
            this.blueTop.VisibleMoreColors = false;
            this.blueTop.VisibleNoColor = false;
            this.blueTop.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // blueDropDown
            // 
            this.blueDropDown.Location = new System.Drawing.Point(16, 60);
            this.blueDropDown.Name = "blueDropDown";
            this.blueDropDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueDropDown.SelectedColor = System.Drawing.Color.Blue;
            this.blueDropDown.Size = new System.Drawing.Size(102, 25);
            this.blueDropDown.Splitter = false;
            this.blueDropDown.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.blueDropDown.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.blueDropDown.TabIndex = 1;
            this.blueDropDown.Values.Text = "DropDown";
            this.blueDropDown.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // blueSplitter
            // 
            this.blueSplitter.Location = new System.Drawing.Point(16, 29);
            this.blueSplitter.Name = "blueSplitter";
            this.blueSplitter.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.blueSplitter.Size = new System.Drawing.Size(102, 25);
            this.blueSplitter.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.blueSplitter.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.blueSplitter.TabIndex = 0;
            this.blueSplitter.Values.Text = "Splitter";
            this.blueSplitter.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // group2003
            // 
            this.group2003.Controls.Add(this.sparkleBottom);
            this.group2003.Controls.Add(this.sparkleTop);
            this.group2003.Controls.Add(this.sparkleLeft);
            this.group2003.Controls.Add(this.sparkleRight);
            this.group2003.Location = new System.Drawing.Point(12, 238);
            this.group2003.Name = "group2003";
            this.group2003.Size = new System.Drawing.Size(221, 107);
            this.group2003.TabIndex = 2;
            this.group2003.TabStop = false;
            this.group2003.Text = "Sparkle - Blue";
            // 
            // sparkleBottom
            // 
            this.sparkleBottom.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.sparkleBottom.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.sparkleBottom.Location = new System.Drawing.Point(106, 20);
            this.sparkleBottom.Name = "sparkleBottom";
            this.sparkleBottom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.sparkleBottom.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.sparkleBottom.Size = new System.Drawing.Size(83, 44);
            this.sparkleBottom.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleBottom.StateCommon.Content.Padding = new System.Windows.Forms.Padding(6, 0, 2, 0);
            this.sparkleBottom.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleBottom.TabIndex = 2;
            this.sparkleBottom.Values.Text = "Bottom";
            this.sparkleBottom.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // sparkleTop
            // 
            this.sparkleTop.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.sparkleTop.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.sparkleTop.Location = new System.Drawing.Point(16, 20);
            this.sparkleTop.Name = "sparkleTop";
            this.sparkleTop.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.sparkleTop.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.sparkleTop.Size = new System.Drawing.Size(83, 44);
            this.sparkleTop.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleTop.StateCommon.Content.Padding = new System.Windows.Forms.Padding(6, 0, 2, 0);
            this.sparkleTop.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleTop.TabIndex = 0;
            this.sparkleTop.Values.Text = "Top";
            this.sparkleTop.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // sparkleLeft
            // 
            this.sparkleLeft.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.sparkleLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.sparkleLeft.Location = new System.Drawing.Point(16, 70);
            this.sparkleLeft.Name = "sparkleLeft";
            this.sparkleLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.sparkleLeft.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.sparkleLeft.Size = new System.Drawing.Size(83, 25);
            this.sparkleLeft.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleLeft.StateCommon.Content.Padding = new System.Windows.Forms.Padding(6, 0, 2, 0);
            this.sparkleLeft.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleLeft.TabIndex = 1;
            this.sparkleLeft.Values.Text = "Left";
            this.sparkleLeft.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // sparkleRight
            // 
            this.sparkleRight.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Cluster;
            this.sparkleRight.Location = new System.Drawing.Point(106, 70);
            this.sparkleRight.Name = "sparkleRight";
            this.sparkleRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.sparkleRight.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.sparkleRight.Size = new System.Drawing.Size(83, 25);
            this.sparkleRight.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleRight.StateCommon.Content.Padding = new System.Windows.Forms.Padding(6, 0, 2, 0);
            this.sparkleRight.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.sparkleRight.TabIndex = 3;
            this.sparkleRight.Values.Text = "Right";
            this.sparkleRight.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.systemRight);
            this.groupBox1.Controls.Add(this.systemDown);
            this.groupBox1.Controls.Add(this.systemLeft);
            this.groupBox1.Controls.Add(this.systemUp);
            this.groupBox1.Location = new System.Drawing.Point(12, 351);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 107);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Professional - System";
            // 
            // systemRight
            // 
            this.systemRight.AutoSize = true;
            this.systemRight.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.systemRight.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.systemRight.Location = new System.Drawing.Point(160, 29);
            this.systemRight.Name = "systemRight";
            this.systemRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.systemRight.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.systemRight.SelectedColor = System.Drawing.Color.Olive;
            this.systemRight.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.systemRight.Size = new System.Drawing.Size(43, 54);
            this.systemRight.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.systemRight.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.systemRight.Strings.ThemeColors = "Basic Colors";
            this.systemRight.TabIndex = 3;
            this.systemRight.Values.Image = ((System.Drawing.Image)(resources.GetObject("systemRight.Values.Image")));
            this.systemRight.Values.Text = "Right";
            this.systemRight.VisibleMoreColors = false;
            this.systemRight.VisibleNoColor = false;
            this.systemRight.VisibleStandard = false;
            this.systemRight.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // systemDown
            // 
            this.systemDown.AutoSize = true;
            this.systemDown.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.systemDown.Location = new System.Drawing.Point(100, 29);
            this.systemDown.Name = "systemDown";
            this.systemDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.systemDown.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.systemDown.SelectedColor = System.Drawing.Color.MediumBlue;
            this.systemDown.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.systemDown.Size = new System.Drawing.Size(54, 54);
            this.systemDown.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.systemDown.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.systemDown.Strings.ThemeColors = "Basic Colors";
            this.systemDown.TabIndex = 2;
            this.systemDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("systemDown.Values.Image")));
            this.systemDown.Values.Text = "Down";
            this.systemDown.VisibleMoreColors = false;
            this.systemDown.VisibleNoColor = false;
            this.systemDown.VisibleStandard = false;
            this.systemDown.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // systemLeft
            // 
            this.systemLeft.AutoSize = true;
            this.systemLeft.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.systemLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.systemLeft.Location = new System.Drawing.Point(56, 29);
            this.systemLeft.Name = "systemLeft";
            this.systemLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.systemLeft.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.systemLeft.SelectedColor = System.Drawing.Color.Gray;
            this.systemLeft.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.systemLeft.Size = new System.Drawing.Size(38, 54);
            this.systemLeft.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.systemLeft.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.systemLeft.Strings.ThemeColors = "Basic Colors";
            this.systemLeft.TabIndex = 1;
            this.systemLeft.Values.Image = ((System.Drawing.Image)(resources.GetObject("systemLeft.Values.Image")));
            this.systemLeft.Values.Text = "Left";
            this.systemLeft.VisibleMoreColors = false;
            this.systemLeft.VisibleNoColor = false;
            this.systemLeft.VisibleStandard = false;
            this.systemLeft.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // systemUp
            // 
            this.systemUp.AutoSize = true;
            this.systemUp.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.systemUp.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.systemUp.Location = new System.Drawing.Point(16, 29);
            this.systemUp.Name = "systemUp";
            this.systemUp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.systemUp.SchemeThemes = ComponentFactory.Krypton.Toolkit.ColorScheme.Basic16;
            this.systemUp.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.systemUp.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.systemUp.Size = new System.Drawing.Size(34, 54);
            this.systemUp.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.systemUp.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.systemUp.Strings.ThemeColors = "Basic Colors";
            this.systemUp.TabIndex = 0;
            this.systemUp.Values.Image = global::KryptonColorButtonExamples.Properties.Resources.Empty16x16;
            this.systemUp.Values.Text = "Up";
            this.systemUp.VisibleMoreColors = false;
            this.systemUp.VisibleNoColor = false;
            this.systemUp.VisibleStandard = false;
            this.systemUp.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.silverRight);
            this.groupBox2.Controls.Add(this.silverDown);
            this.groupBox2.Controls.Add(this.silverLeft);
            this.groupBox2.Controls.Add(this.silverUp);
            this.groupBox2.Location = new System.Drawing.Point(12, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Office 2010 - Blue";
            // 
            // silverRight
            // 
            this.silverRight.AutoSize = true;
            this.silverRight.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.silverRight.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.silverRight.Location = new System.Drawing.Point(160, 29);
            this.silverRight.Name = "silverRight";
            this.silverRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.silverRight.SelectedColor = System.Drawing.Color.Olive;
            this.silverRight.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.silverRight.Size = new System.Drawing.Size(43, 54);
            this.silverRight.Splitter = false;
            this.silverRight.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.silverRight.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.silverRight.TabIndex = 3;
            this.silverRight.Values.Image = ((System.Drawing.Image)(resources.GetObject("silverRight.Values.Image")));
            this.silverRight.Values.Text = "Right";
            this.silverRight.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // silverDown
            // 
            this.silverDown.AutoSize = true;
            this.silverDown.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.silverDown.Location = new System.Drawing.Point(100, 29);
            this.silverDown.Name = "silverDown";
            this.silverDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.silverDown.SelectedColor = System.Drawing.Color.MediumBlue;
            this.silverDown.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.silverDown.Size = new System.Drawing.Size(54, 54);
            this.silverDown.Splitter = false;
            this.silverDown.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.silverDown.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.silverDown.TabIndex = 2;
            this.silverDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("silverDown.Values.Image")));
            this.silverDown.Values.Text = "Down";
            this.silverDown.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // silverLeft
            // 
            this.silverLeft.AutoSize = true;
            this.silverLeft.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.silverLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.silverLeft.Location = new System.Drawing.Point(56, 29);
            this.silverLeft.Name = "silverLeft";
            this.silverLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.silverLeft.SelectedColor = System.Drawing.Color.Gray;
            this.silverLeft.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.silverLeft.Size = new System.Drawing.Size(38, 54);
            this.silverLeft.Splitter = false;
            this.silverLeft.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.silverLeft.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.silverLeft.TabIndex = 1;
            this.silverLeft.Values.Image = ((System.Drawing.Image)(resources.GetObject("silverLeft.Values.Image")));
            this.silverLeft.Values.Text = "Left";
            this.silverLeft.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // silverUp
            // 
            this.silverUp.AutoSize = true;
            this.silverUp.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.silverUp.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.silverUp.Location = new System.Drawing.Point(16, 29);
            this.silverUp.Name = "silverUp";
            this.silverUp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.silverUp.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.silverUp.SelectedRect = new System.Drawing.Rectangle(0, 0, 16, 16);
            this.silverUp.Size = new System.Drawing.Size(34, 54);
            this.silverUp.Splitter = false;
            this.silverUp.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.silverUp.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.silverUp.TabIndex = 0;
            this.silverUp.Values.Image = global::KryptonColorButtonExamples.Properties.Resources.Empty16x16;
            this.silverUp.Values.Text = "Up";
            this.silverUp.Enter += new System.EventHandler(this.colorButtonEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group2003);
            this.Controls.Add(this.groupBlue);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupProperties);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonColorButton Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupProperties.ResumeLayout(false);
            this.groupBlue.ResumeLayout(false);
            this.groupBlue.PerformLayout();
            this.group2003.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBlue;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueSplitter;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueDropDown;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueTop;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueRight;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton blueBottom;
        private System.Windows.Forms.GroupBox group2003;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton sparkleRight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton silverRight;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton silverDown;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton silverLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton silverUp;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton systemRight;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton systemDown;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton systemLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton systemUp;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton sparkleBottom;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton sparkleTop;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton sparkleLeft;
    }
}

