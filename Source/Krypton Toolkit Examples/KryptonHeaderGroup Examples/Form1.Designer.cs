namespace KryptonHeaderGroupExamples
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.headerGroup2Office = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.headerGroup1Office = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.headerGroup1OfficeRTB = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.headerGroup2Blue = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.headerGroup1Blue = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup3 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.headerGroup4Custom = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.headerGroup3Custom = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup5 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.headerGroup2Custom = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.headerGroup1Custom = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Office.Panel)).BeginInit();
            this.headerGroup2Office.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Office.Panel)).BeginInit();
            this.headerGroup1Office.Panel.SuspendLayout();
            this.headerGroup1Office.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Blue.Panel)).BeginInit();
            this.headerGroup2Blue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Blue.Panel)).BeginInit();
            this.headerGroup1Blue.Panel.SuspendLayout();
            this.headerGroup1Blue.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup4Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup4Custom.Panel)).BeginInit();
            this.headerGroup4Custom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup3Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup3Custom.Panel)).BeginInit();
            this.headerGroup3Custom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Custom.Panel)).BeginInit();
            this.headerGroup2Custom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Custom.Panel)).BeginInit();
            this.headerGroup1Custom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.propertyGrid);
            this.groupBox4.Location = new System.Drawing.Point(333, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(321, 628);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties for Selected KryptonHeaderGroup";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(309, 603);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(580, 646);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.headerGroup2Office);
            this.groupBox1.Controls.Add(this.headerGroup1Office);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 175);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Professional - Office 2003";
            // 
            // headerGroup2Office
            // 
            this.headerGroup2Office.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.headerGroup2Office.Location = new System.Drawing.Point(141, 28);
            this.headerGroup2Office.Name = "headerGroup2Office";
            this.headerGroup2Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            // 
            // headerGroup2Office.Panel
            // 
            this.headerGroup2Office.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup2Office.Size = new System.Drawing.Size(158, 129);
            this.headerGroup2Office.StateNormal.HeaderPrimary.Content.Image.Effect = ComponentFactory.Krypton.Toolkit.PaletteImageEffect.Inherit;
            this.headerGroup2Office.StateNormal.HeaderPrimary.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerGroup2Office.TabIndex = 1;
            this.headerGroup2Office.ValuesPrimary.Heading = "Calendar";
            this.headerGroup2Office.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("headerGroup2Office.ValuesPrimary.Image")));
            this.headerGroup2Office.ValuesSecondary.Description = "25th December 2005";
            this.headerGroup2Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // headerGroup1Office
            // 
            this.headerGroup1Office.AllowButtonSpecToolTips = true;
            this.headerGroup1Office.AutoSize = true;
            this.headerGroup1Office.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup1});
            this.headerGroup1Office.Location = new System.Drawing.Point(11, 28);
            this.headerGroup1Office.Name = "headerGroup1Office";
            this.headerGroup1Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            // 
            // headerGroup1Office.Panel
            // 
            this.headerGroup1Office.Panel.Controls.Add(this.headerGroup1OfficeRTB);
            this.headerGroup1Office.Panel.MinimumSize = new System.Drawing.Size(106, 80);
            this.headerGroup1Office.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.headerGroup1Office.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup1Office.Size = new System.Drawing.Size(124, 133);
            this.headerGroup1Office.TabIndex = 0;
            this.headerGroup1Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecHeaderGroup1.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecHeaderGroup1.ToolTipTitle = "Toggle the Collapsed/Expanded state";
            this.buttonSpecHeaderGroup1.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup1.UniqueName = "A3F93613DE6E4171A3F93613DE6E4171";
            // 
            // headerGroup1OfficeRTB
            // 
            this.headerGroup1OfficeRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.headerGroup1OfficeRTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroup1OfficeRTB.Location = new System.Drawing.Point(5, 5);
            this.headerGroup1OfficeRTB.Name = "headerGroup1OfficeRTB";
            this.headerGroup1OfficeRTB.Size = new System.Drawing.Size(112, 70);
            this.headerGroup1OfficeRTB.TabIndex = 0;
            this.headerGroup1OfficeRTB.Text = "Use the arrow on the top header to see the expand and collapse in operation.";
            this.headerGroup1OfficeRTB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtb_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.headerGroup2Blue);
            this.groupBox2.Controls.Add(this.headerGroup1Blue);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 175);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Office 2007 - Blue";
            // 
            // headerGroup2Blue
            // 
            this.headerGroup2Blue.HeaderPositionSecondary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.headerGroup2Blue.Location = new System.Drawing.Point(141, 28);
            this.headerGroup2Blue.Name = "headerGroup2Blue";
            this.headerGroup2Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            // 
            // headerGroup2Blue.Panel
            // 
            this.headerGroup2Blue.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup2Blue.Size = new System.Drawing.Size(158, 134);
            this.headerGroup2Blue.StateNormal.HeaderPrimary.Content.Image.Effect = ComponentFactory.Krypton.Toolkit.PaletteImageEffect.Inherit;
            this.headerGroup2Blue.StateNormal.HeaderPrimary.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerGroup2Blue.TabIndex = 2;
            this.headerGroup2Blue.ValuesPrimary.Heading = "Calendar";
            this.headerGroup2Blue.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("headerGroup2Blue.ValuesPrimary.Image")));
            this.headerGroup2Blue.ValuesSecondary.Description = "25th December 2005";
            this.headerGroup2Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // headerGroup1Blue
            // 
            this.headerGroup1Blue.AllowButtonSpecToolTips = true;
            this.headerGroup1Blue.AutoSize = true;
            this.headerGroup1Blue.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup3});
            this.headerGroup1Blue.Location = new System.Drawing.Point(11, 28);
            this.headerGroup1Blue.Name = "headerGroup1Blue";
            this.headerGroup1Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            // 
            // headerGroup1Blue.Panel
            // 
            this.headerGroup1Blue.Panel.Controls.Add(this.richTextBox1);
            this.headerGroup1Blue.Panel.MinimumSize = new System.Drawing.Size(106, 80);
            this.headerGroup1Blue.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.headerGroup1Blue.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup1Blue.Size = new System.Drawing.Size(124, 133);
            this.headerGroup1Blue.TabIndex = 1;
            this.headerGroup1Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // buttonSpecHeaderGroup3
            // 
            this.buttonSpecHeaderGroup3.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecHeaderGroup3.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecHeaderGroup3.ToolTipTitle = "Toggle the Collapsed/Expanded state";
            this.buttonSpecHeaderGroup3.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            this.buttonSpecHeaderGroup3.UniqueName = "7C2CD73A480A44C57C2CD73A480A44C5";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(5, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(112, 70);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Use the arrow on the top header to see the expand and collapse in operation.";
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtb_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.headerGroup4Custom);
            this.groupBox3.Controls.Add(this.headerGroup3Custom);
            this.groupBox3.Controls.Add(this.headerGroup2Custom);
            this.groupBox3.Controls.Add(this.headerGroup1Custom);
            this.groupBox3.Location = new System.Drawing.Point(12, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 265);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Settings";
            // 
            // headerGroup4Custom
            // 
            this.headerGroup4Custom.Location = new System.Drawing.Point(162, 148);
            this.headerGroup4Custom.Name = "headerGroup4Custom";
            this.headerGroup4Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // headerGroup4Custom.Panel
            // 
            this.headerGroup4Custom.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup4Custom.Size = new System.Drawing.Size(137, 104);
            this.headerGroup4Custom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(125)))), ((int)(((byte)(222)))));
            this.headerGroup4Custom.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(186)))), ((int)(((byte)(247)))));
            this.headerGroup4Custom.StateNormal.Back.ColorAngle = 15F;
            this.headerGroup4Custom.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerGroup4Custom.StateNormal.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.headerGroup4Custom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup4Custom.StateNormal.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.Border.Width = 1;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Border.Width = 1;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Content.Image.Effect = ComponentFactory.Krypton.Toolkit.PaletteImageEffect.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderPrimary.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Border.Width = 1;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.LongText.Color1 = System.Drawing.Color.White;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.LongText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.LongText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup4Custom.StateNormal.HeaderSecondary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup4Custom.TabIndex = 5;
            this.headerGroup4Custom.ValuesPrimary.Heading = "Computer";
            this.headerGroup4Custom.ValuesPrimary.Image = global::KryptonHeaderGroupExamples.Properties.Resources.WinLogo;
            this.headerGroup4Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // headerGroup3Custom
            // 
            this.headerGroup3Custom.AllowButtonSpecToolTips = true;
            this.headerGroup3Custom.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup5});
            this.headerGroup3Custom.Location = new System.Drawing.Point(15, 148);
            this.headerGroup3Custom.Name = "headerGroup3Custom";
            this.headerGroup3Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // headerGroup3Custom.Panel
            // 
            this.headerGroup3Custom.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup3Custom.Size = new System.Drawing.Size(137, 104);
            this.headerGroup3Custom.StateCommon.HeaderPrimary.ButtonEdgeInset = 4;
            this.headerGroup3Custom.StateCommon.HeaderPrimary.ButtonPadding = new System.Windows.Forms.Padding(2, -1, 0, -1);
            this.headerGroup3Custom.StateCommon.OverlayHeaders = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerGroup3Custom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(201)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup3Custom.StateNormal.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.Border.Width = 2;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(201)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Back.ColorAngle = 70F;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Rounded;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Content.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderPrimary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(201)))), ((int)(((byte)(255)))));
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Back.ColorAngle = 70F;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Rounded;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Content.LongText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerGroup3Custom.StateNormal.HeaderSecondary.Content.LongText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup3Custom.TabIndex = 4;
            this.headerGroup3Custom.ValuesPrimary.Heading = "Notepad";
            this.headerGroup3Custom.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("headerGroup3Custom.ValuesPrimary.Image")));
            this.headerGroup3Custom.ValuesSecondary.Description = "c:\\Temp.txt";
            this.headerGroup3Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // buttonSpecHeaderGroup5
            // 
            this.buttonSpecHeaderGroup5.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Far;
            this.buttonSpecHeaderGroup5.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.ButtonSpec;
            this.buttonSpecHeaderGroup5.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecHeaderGroup5.ToolTipTitle = "Drop down for option selection";
            this.buttonSpecHeaderGroup5.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Context;
            this.buttonSpecHeaderGroup5.UniqueName = "02BAE7AC90E9401302BAE7AC90E94013";
            // 
            // headerGroup2Custom
            // 
            this.headerGroup2Custom.Location = new System.Drawing.Point(162, 26);
            this.headerGroup2Custom.Name = "headerGroup2Custom";
            this.headerGroup2Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // headerGroup2Custom.Panel
            // 
            this.headerGroup2Custom.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup2Custom.Size = new System.Drawing.Size(137, 111);
            this.headerGroup2Custom.StateCommon.OverlayHeaders = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerGroup2Custom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.headerGroup2Custom.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.headerGroup2Custom.StateNormal.Back.ColorAngle = 180F;
            this.headerGroup2Custom.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.headerGroup2Custom.StateNormal.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.Border.Color1 = System.Drawing.SystemColors.Control;
            this.headerGroup2Custom.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.headerGroup2Custom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup2Custom.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup2Custom.StateNormal.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.Border.Rounding = 11;
            this.headerGroup2Custom.StateNormal.Border.Width = 1;
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Content.Padding = new System.Windows.Forms.Padding(12, 6, -1, -1);
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Content.ShortText.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Content.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderPrimary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.LongText.Color1 = System.Drawing.Color.White;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.LongText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.LongText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.LongText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.Padding = new System.Windows.Forms.Padding(12, -1, -1, 5);
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup2Custom.StateNormal.HeaderSecondary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup2Custom.TabIndex = 3;
            this.headerGroup2Custom.ValuesPrimary.Heading = "Dark Style";
            this.headerGroup2Custom.ValuesPrimary.Image = null;
            this.headerGroup2Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // headerGroup1Custom
            // 
            this.headerGroup1Custom.Location = new System.Drawing.Point(15, 26);
            this.headerGroup1Custom.Name = "headerGroup1Custom";
            this.headerGroup1Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // headerGroup1Custom.Panel
            // 
            this.headerGroup1Custom.Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.headerGroup1Custom.Size = new System.Drawing.Size(137, 111);
            this.headerGroup1Custom.StateCommon.HeaderPrimary.HeaderPadding = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.headerGroup1Custom.StateCommon.HeaderSecondary.HeaderPadding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.headerGroup1Custom.StateCommon.OverlayHeaders = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerGroup1Custom.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(192)))));
            this.headerGroup1Custom.StateNormal.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.headerGroup1Custom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup1Custom.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup1Custom.StateNormal.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.Border.Rounding = 11;
            this.headerGroup1Custom.StateNormal.Border.Width = 5;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(192)))));
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Back.Color2 = System.Drawing.Color.White;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Border.Rounding = 6;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Border.Width = 0;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Content.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderPrimary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Back.Color1 = System.Drawing.Color.White;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(192)))));
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Border.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Border.Rounding = 6;
            this.headerGroup1Custom.StateNormal.HeaderSecondary.Border.Width = 0;
            this.headerGroup1Custom.TabIndex = 2;
            this.headerGroup1Custom.ValuesPrimary.Heading = "Blogging";
            this.headerGroup1Custom.ValuesPrimary.Image = null;
            this.headerGroup1Custom.ValuesSecondary.Description = "What is a blog?";
            this.headerGroup1Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.headerGroup_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 677);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonHeaderGroup Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Office.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Office)).EndInit();
            this.headerGroup2Office.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Office.Panel)).EndInit();
            this.headerGroup1Office.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Office)).EndInit();
            this.headerGroup1Office.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Blue.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Blue)).EndInit();
            this.headerGroup2Blue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Blue.Panel)).EndInit();
            this.headerGroup1Blue.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Blue)).EndInit();
            this.headerGroup1Blue.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup4Custom.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup4Custom)).EndInit();
            this.headerGroup4Custom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup3Custom.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup3Custom)).EndInit();
            this.headerGroup3Custom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Custom.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup2Custom)).EndInit();
            this.headerGroup2Custom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Custom.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroup1Custom)).EndInit();
            this.headerGroup1Custom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup2Office;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup1Office;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup2Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup1Blue;
        private System.Windows.Forms.GroupBox groupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup1Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup2Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup4Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroup3Custom;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup3;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup5;
        private System.Windows.Forms.RichTextBox headerGroup1OfficeRTB;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

