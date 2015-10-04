namespace KryptonDropButtonExamples
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
            this.groupSplitter = new System.Windows.Forms.GroupBox();
            this.splitterArrowLeft = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.kcmDropDown = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.splitterArrowRight = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterArrowUp = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterArrowDown = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterPosBottom = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterPosTop = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterPosLeft = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.splitterPosRight = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.groupDropDown = new System.Windows.Forms.GroupBox();
            this.dropArrowLeft = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropArrowRight = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropArrowUp = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropArrowDown = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropPosBottom = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropPosTop = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropPosLeft = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.dropPosRight = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.groupCustom = new System.Windows.Forms.GroupBox();
            this.dropCustom = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.kcmCustom = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems5 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItem13 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem14 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuItem15 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.splitterCustom = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.groupProperties.SuspendLayout();
            this.groupSplitter.SuspendLayout();
            this.groupDropDown.SuspendLayout();
            this.groupCustom.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(553, 526);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupProperties
            // 
            this.groupProperties.Controls.Add(this.propertyGrid);
            this.groupProperties.Location = new System.Drawing.Point(306, 12);
            this.groupProperties.Name = "groupProperties";
            this.groupProperties.Size = new System.Drawing.Size(322, 508);
            this.groupProperties.TabIndex = 3;
            this.groupProperties.TabStop = false;
            this.groupProperties.Text = "Properties for Selected KryptonDropButton";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(310, 483);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // groupSplitter
            // 
            this.groupSplitter.Controls.Add(this.splitterArrowLeft);
            this.groupSplitter.Controls.Add(this.splitterArrowRight);
            this.groupSplitter.Controls.Add(this.splitterArrowUp);
            this.groupSplitter.Controls.Add(this.splitterArrowDown);
            this.groupSplitter.Controls.Add(this.splitterPosBottom);
            this.groupSplitter.Controls.Add(this.splitterPosTop);
            this.groupSplitter.Controls.Add(this.splitterPosLeft);
            this.groupSplitter.Controls.Add(this.splitterPosRight);
            this.groupSplitter.Location = new System.Drawing.Point(12, 12);
            this.groupSplitter.Name = "groupSplitter";
            this.groupSplitter.Size = new System.Drawing.Size(288, 191);
            this.groupSplitter.TabIndex = 0;
            this.groupSplitter.TabStop = false;
            this.groupSplitter.Text = "Splitter";
            // 
            // splitterArrowLeft
            // 
            this.splitterArrowLeft.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.splitterArrowLeft.KryptonContextMenu = this.kcmDropDown;
            this.splitterArrowLeft.Location = new System.Drawing.Point(154, 92);
            this.splitterArrowLeft.Name = "splitterArrowLeft";
            this.splitterArrowLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.splitterArrowLeft.Size = new System.Drawing.Size(115, 25);
            this.splitterArrowLeft.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowLeft.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowLeft.TabIndex = 6;
            this.splitterArrowLeft.Values.Image = ((System.Drawing.Image)(resources.GetObject("splitterArrowLeft.Values.Image")));
            this.splitterArrowLeft.Values.Text = "Arrow Left";
            this.splitterArrowLeft.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // kcmDropDown
            // 
            this.kcmDropDown.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1});
            this.kcmDropDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1,
            this.kryptonContextMenuItem2,
            this.kryptonContextMenuItem3});
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Text = "Menu Item 1";
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Checked = true;
            this.kryptonContextMenuItem2.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.kryptonContextMenuItem2.Text = "Menu Item 2";
            // 
            // kryptonContextMenuItem3
            // 
            this.kryptonContextMenuItem3.Checked = true;
            this.kryptonContextMenuItem3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kryptonContextMenuItem3.Text = "Menu Item 3";
            // 
            // splitterArrowRight
            // 
            this.splitterArrowRight.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.splitterArrowRight.KryptonContextMenu = this.kcmDropDown;
            this.splitterArrowRight.Location = new System.Drawing.Point(154, 30);
            this.splitterArrowRight.Name = "splitterArrowRight";
            this.splitterArrowRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.splitterArrowRight.Size = new System.Drawing.Size(115, 25);
            this.splitterArrowRight.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowRight.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowRight.TabIndex = 4;
            this.splitterArrowRight.Values.Image = ((System.Drawing.Image)(resources.GetObject("splitterArrowRight.Values.Image")));
            this.splitterArrowRight.Values.Text = "Arrow Right";
            this.splitterArrowRight.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterArrowUp
            // 
            this.splitterArrowUp.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.splitterArrowUp.KryptonContextMenu = this.kcmDropDown;
            this.splitterArrowUp.Location = new System.Drawing.Point(154, 123);
            this.splitterArrowUp.Name = "splitterArrowUp";
            this.splitterArrowUp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.splitterArrowUp.Size = new System.Drawing.Size(115, 25);
            this.splitterArrowUp.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowUp.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowUp.TabIndex = 7;
            this.splitterArrowUp.Values.Image = ((System.Drawing.Image)(resources.GetObject("splitterArrowUp.Values.Image")));
            this.splitterArrowUp.Values.Text = "Arrow Up";
            this.splitterArrowUp.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterArrowDown
            // 
            this.splitterArrowDown.KryptonContextMenu = this.kcmDropDown;
            this.splitterArrowDown.Location = new System.Drawing.Point(154, 61);
            this.splitterArrowDown.Name = "splitterArrowDown";
            this.splitterArrowDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.splitterArrowDown.Size = new System.Drawing.Size(115, 25);
            this.splitterArrowDown.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowDown.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.splitterArrowDown.TabIndex = 5;
            this.splitterArrowDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("splitterArrowDown.Values.Image")));
            this.splitterArrowDown.Values.Text = "Arrow Down";
            this.splitterArrowDown.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterPosBottom
            // 
            this.splitterPosBottom.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.splitterPosBottom.KryptonContextMenu = this.kcmDropDown;
            this.splitterPosBottom.Location = new System.Drawing.Point(25, 136);
            this.splitterPosBottom.Name = "splitterPosBottom";
            this.splitterPosBottom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.splitterPosBottom.Size = new System.Drawing.Size(104, 38);
            this.splitterPosBottom.TabIndex = 3;
            this.splitterPosBottom.Values.Text = "Position Bottom";
            this.splitterPosBottom.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterPosTop
            // 
            this.splitterPosTop.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.splitterPosTop.KryptonContextMenu = this.kcmDropDown;
            this.splitterPosTop.Location = new System.Drawing.Point(25, 92);
            this.splitterPosTop.Name = "splitterPosTop";
            this.splitterPosTop.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.splitterPosTop.Size = new System.Drawing.Size(104, 38);
            this.splitterPosTop.TabIndex = 2;
            this.splitterPosTop.Values.Text = "Position Top";
            this.splitterPosTop.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterPosLeft
            // 
            this.splitterPosLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.splitterPosLeft.KryptonContextMenu = this.kcmDropDown;
            this.splitterPosLeft.Location = new System.Drawing.Point(25, 61);
            this.splitterPosLeft.Name = "splitterPosLeft";
            this.splitterPosLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.splitterPosLeft.Size = new System.Drawing.Size(104, 25);
            this.splitterPosLeft.TabIndex = 1;
            this.splitterPosLeft.Values.Text = "Position Left";
            this.splitterPosLeft.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // splitterPosRight
            // 
            this.splitterPosRight.KryptonContextMenu = this.kcmDropDown;
            this.splitterPosRight.Location = new System.Drawing.Point(25, 30);
            this.splitterPosRight.Name = "splitterPosRight";
            this.splitterPosRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.splitterPosRight.Size = new System.Drawing.Size(104, 25);
            this.splitterPosRight.TabIndex = 0;
            this.splitterPosRight.Values.Text = "Position Right";
            this.splitterPosRight.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // groupDropDown
            // 
            this.groupDropDown.Controls.Add(this.dropArrowLeft);
            this.groupDropDown.Controls.Add(this.dropArrowRight);
            this.groupDropDown.Controls.Add(this.dropArrowUp);
            this.groupDropDown.Controls.Add(this.dropArrowDown);
            this.groupDropDown.Controls.Add(this.dropPosBottom);
            this.groupDropDown.Controls.Add(this.dropPosTop);
            this.groupDropDown.Controls.Add(this.dropPosLeft);
            this.groupDropDown.Controls.Add(this.dropPosRight);
            this.groupDropDown.Location = new System.Drawing.Point(12, 209);
            this.groupDropDown.Name = "groupDropDown";
            this.groupDropDown.Size = new System.Drawing.Size(288, 191);
            this.groupDropDown.TabIndex = 1;
            this.groupDropDown.TabStop = false;
            this.groupDropDown.Text = "DropDown";
            // 
            // dropArrowLeft
            // 
            this.dropArrowLeft.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.dropArrowLeft.KryptonContextMenu = this.kcmDropDown;
            this.dropArrowLeft.Location = new System.Drawing.Point(154, 92);
            this.dropArrowLeft.Name = "dropArrowLeft";
            this.dropArrowLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dropArrowLeft.Size = new System.Drawing.Size(115, 25);
            this.dropArrowLeft.Splitter = false;
            this.dropArrowLeft.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowLeft.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowLeft.TabIndex = 6;
            this.dropArrowLeft.Values.Image = ((System.Drawing.Image)(resources.GetObject("dropArrowLeft.Values.Image")));
            this.dropArrowLeft.Values.Text = "Arrow Left";
            this.dropArrowLeft.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropArrowRight
            // 
            this.dropArrowRight.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.dropArrowRight.KryptonContextMenu = this.kcmDropDown;
            this.dropArrowRight.Location = new System.Drawing.Point(154, 30);
            this.dropArrowRight.Name = "dropArrowRight";
            this.dropArrowRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dropArrowRight.Size = new System.Drawing.Size(115, 25);
            this.dropArrowRight.Splitter = false;
            this.dropArrowRight.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowRight.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowRight.TabIndex = 4;
            this.dropArrowRight.Values.Image = ((System.Drawing.Image)(resources.GetObject("dropArrowRight.Values.Image")));
            this.dropArrowRight.Values.Text = "Arrow Right";
            this.dropArrowRight.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropArrowUp
            // 
            this.dropArrowUp.DropDownOrientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.dropArrowUp.KryptonContextMenu = this.kcmDropDown;
            this.dropArrowUp.Location = new System.Drawing.Point(154, 123);
            this.dropArrowUp.Name = "dropArrowUp";
            this.dropArrowUp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dropArrowUp.Size = new System.Drawing.Size(115, 25);
            this.dropArrowUp.Splitter = false;
            this.dropArrowUp.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowUp.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowUp.TabIndex = 7;
            this.dropArrowUp.Values.Image = ((System.Drawing.Image)(resources.GetObject("dropArrowUp.Values.Image")));
            this.dropArrowUp.Values.Text = "Arrow Up";
            this.dropArrowUp.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropArrowDown
            // 
            this.dropArrowDown.KryptonContextMenu = this.kcmDropDown;
            this.dropArrowDown.Location = new System.Drawing.Point(154, 61);
            this.dropArrowDown.Name = "dropArrowDown";
            this.dropArrowDown.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.dropArrowDown.Size = new System.Drawing.Size(115, 25);
            this.dropArrowDown.Splitter = false;
            this.dropArrowDown.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowDown.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.dropArrowDown.TabIndex = 5;
            this.dropArrowDown.Values.Image = ((System.Drawing.Image)(resources.GetObject("dropArrowDown.Values.Image")));
            this.dropArrowDown.Values.Text = "Arrow Down";
            this.dropArrowDown.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropPosBottom
            // 
            this.dropPosBottom.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.dropPosBottom.KryptonContextMenu = this.kcmDropDown;
            this.dropPosBottom.Location = new System.Drawing.Point(25, 136);
            this.dropPosBottom.Name = "dropPosBottom";
            this.dropPosBottom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.dropPosBottom.Size = new System.Drawing.Size(104, 38);
            this.dropPosBottom.Splitter = false;
            this.dropPosBottom.TabIndex = 3;
            this.dropPosBottom.Values.Text = "Position Bottom";
            this.dropPosBottom.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropPosTop
            // 
            this.dropPosTop.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.dropPosTop.KryptonContextMenu = this.kcmDropDown;
            this.dropPosTop.Location = new System.Drawing.Point(25, 92);
            this.dropPosTop.Name = "dropPosTop";
            this.dropPosTop.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.dropPosTop.Size = new System.Drawing.Size(104, 38);
            this.dropPosTop.Splitter = false;
            this.dropPosTop.TabIndex = 2;
            this.dropPosTop.Values.Text = "Position Top";
            this.dropPosTop.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropPosLeft
            // 
            this.dropPosLeft.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.dropPosLeft.KryptonContextMenu = this.kcmDropDown;
            this.dropPosLeft.Location = new System.Drawing.Point(25, 61);
            this.dropPosLeft.Name = "dropPosLeft";
            this.dropPosLeft.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.dropPosLeft.Size = new System.Drawing.Size(104, 25);
            this.dropPosLeft.Splitter = false;
            this.dropPosLeft.TabIndex = 1;
            this.dropPosLeft.Values.Text = "Position Left";
            this.dropPosLeft.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // dropPosRight
            // 
            this.dropPosRight.KryptonContextMenu = this.kcmDropDown;
            this.dropPosRight.Location = new System.Drawing.Point(25, 30);
            this.dropPosRight.Name = "dropPosRight";
            this.dropPosRight.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.dropPosRight.Size = new System.Drawing.Size(104, 25);
            this.dropPosRight.Splitter = false;
            this.dropPosRight.TabIndex = 0;
            this.dropPosRight.Values.Text = "Position Right";
            this.dropPosRight.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // groupCustom
            // 
            this.groupCustom.Controls.Add(this.dropCustom);
            this.groupCustom.Controls.Add(this.splitterCustom);
            this.groupCustom.Location = new System.Drawing.Point(12, 407);
            this.groupCustom.Name = "groupCustom";
            this.groupCustom.Size = new System.Drawing.Size(288, 113);
            this.groupCustom.TabIndex = 2;
            this.groupCustom.TabStop = false;
            this.groupCustom.Text = "Custom";
            // 
            // dropCustom
            // 
            this.dropCustom.DropDownPosition = ComponentFactory.Krypton.Toolkit.VisualOrientation.Bottom;
            this.dropCustom.Images.Normal = ((System.Drawing.Image)(resources.GetObject("dropCustom.Images.Normal")));
            this.dropCustom.Images.Pressed = ((System.Drawing.Image)(resources.GetObject("dropCustom.Images.Pressed")));
            this.dropCustom.Images.Tracking = ((System.Drawing.Image)(resources.GetObject("dropCustom.Images.Tracking")));
            this.dropCustom.KryptonContextMenu = this.kcmCustom;
            this.dropCustom.Location = new System.Drawing.Point(158, 30);
            this.dropCustom.Name = "dropCustom";
            this.dropCustom.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dropCustom.OverrideDefault.Border.Color1 = System.Drawing.Color.Green;
            this.dropCustom.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dropCustom.OverrideDefault.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dropCustom.Size = new System.Drawing.Size(115, 52);
            this.dropCustom.Splitter = false;
            this.dropCustom.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.dropCustom.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.dropCustom.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dropCustom.StateCommon.Border.Rounding = 4;
            this.dropCustom.StateCommon.Border.Width = 2;
            this.dropCustom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dropCustom.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.dropCustom.StateNormal.Border.Color1 = System.Drawing.Color.Green;
            this.dropCustom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dropCustom.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dropCustom.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dropCustom.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dropCustom.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dropCustom.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dropCustom.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dropCustom.StateTracking.Border.Color1 = System.Drawing.Color.Navy;
            this.dropCustom.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dropCustom.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dropCustom.TabIndex = 1;
            this.dropCustom.Values.Text = "Custom DropDown";
            this.dropCustom.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // kcmCustom
            // 
            this.kcmCustom.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading5,
            this.kryptonContextMenuItems5});
            this.kcmCustom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kcmCustom.StateCommon.ControlInner.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.kcmCustom.StateCommon.ControlInner.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kcmCustom.StateCommon.ControlInner.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.kcmCustom.StateCommon.ControlInner.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kcmCustom.StateCommon.ControlInner.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kcmCustom.StateCommon.ControlInner.Border.Rounding = 4;
            this.kcmCustom.StateCommon.ControlInner.Border.Width = 2;
            this.kcmCustom.StateCommon.ControlOuter.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.kcmCustom.StateCommon.ControlOuter.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kcmCustom.StateCommon.ControlOuter.Border.Color1 = System.Drawing.Color.Green;
            this.kcmCustom.StateCommon.ControlOuter.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kcmCustom.StateCommon.ControlOuter.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kcmCustom.StateCommon.ControlOuter.Border.Rounding = 4;
            this.kcmCustom.StateCommon.ControlOuter.Border.Width = 2;
            this.kcmCustom.StateCommon.Heading.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.kcmCustom.StateCommon.Heading.Back.Color2 = System.Drawing.Color.Lime;
            this.kcmCustom.StateCommon.Heading.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.kcmCustom.StateCommon.Heading.Border.Color1 = System.Drawing.Color.Green;
            this.kcmCustom.StateCommon.Heading.Border.Color2 = System.Drawing.Color.Green;
            this.kcmCustom.StateCommon.Heading.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom;
            this.kcmCustom.StateCommon.Heading.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kcmCustom.StateCommon.ItemImageColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(254)))), ((int)(((byte)(188)))));
            this.kcmCustom.StateCommon.ItemImageColumn.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kcmCustom.StateCommon.ItemImageColumn.Border.Color1 = System.Drawing.Color.Teal;
            this.kcmCustom.StateCommon.ItemImageColumn.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kcmCustom.StateCommon.ItemTextStandard.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            // 
            // kryptonContextMenuHeading5
            // 
            this.kryptonContextMenuHeading5.ExtraText = "";
            // 
            // kryptonContextMenuItems5
            // 
            this.kryptonContextMenuItems5.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem13,
            this.kryptonContextMenuItem14,
            this.kryptonContextMenuItem15});
            // 
            // kryptonContextMenuItem13
            // 
            this.kryptonContextMenuItem13.Text = "Menu Item 1";
            // 
            // kryptonContextMenuItem14
            // 
            this.kryptonContextMenuItem14.Checked = true;
            this.kryptonContextMenuItem14.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.kryptonContextMenuItem14.Text = "Menu Item 2";
            // 
            // kryptonContextMenuItem15
            // 
            this.kryptonContextMenuItem15.Checked = true;
            this.kryptonContextMenuItem15.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kryptonContextMenuItem15.Text = "Menu Item 3";
            // 
            // splitterCustom
            // 
            this.splitterCustom.Images.Normal = ((System.Drawing.Image)(resources.GetObject("splitterCustom.Images.Normal")));
            this.splitterCustom.Images.Pressed = ((System.Drawing.Image)(resources.GetObject("splitterCustom.Images.Pressed")));
            this.splitterCustom.Images.Tracking = ((System.Drawing.Image)(resources.GetObject("splitterCustom.Images.Tracking")));
            this.splitterCustom.KryptonContextMenu = this.kcmCustom;
            this.splitterCustom.Location = new System.Drawing.Point(16, 40);
            this.splitterCustom.Name = "splitterCustom";
            this.splitterCustom.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.splitterCustom.OverrideDefault.Border.Color1 = System.Drawing.Color.Green;
            this.splitterCustom.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.splitterCustom.OverrideDefault.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.splitterCustom.Size = new System.Drawing.Size(122, 30);
            this.splitterCustom.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.splitterCustom.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.splitterCustom.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.splitterCustom.StateCommon.Border.Rounding = 4;
            this.splitterCustom.StateCommon.Border.Width = 2;
            this.splitterCustom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.splitterCustom.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.splitterCustom.StateNormal.Border.Color1 = System.Drawing.Color.Green;
            this.splitterCustom.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.splitterCustom.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.splitterCustom.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.splitterCustom.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.splitterCustom.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.splitterCustom.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.splitterCustom.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.splitterCustom.StateTracking.Border.Color1 = System.Drawing.Color.Navy;
            this.splitterCustom.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.splitterCustom.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.splitterCustom.TabIndex = 0;
            this.splitterCustom.Values.Text = "Custom Splitter";
            this.splitterCustom.Enter += new System.EventHandler(this.dropButtonEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 561);
            this.Controls.Add(this.groupCustom);
            this.Controls.Add(this.groupDropDown);
            this.Controls.Add(this.groupSplitter);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupProperties);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonDropButton Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupProperties.ResumeLayout(false);
            this.groupSplitter.ResumeLayout(false);
            this.groupDropDown.ResumeLayout(false);
            this.groupCustom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupSplitter;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterPosBottom;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterPosTop;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterPosLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterPosRight;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu kcmDropDown;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem3;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterArrowLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterArrowRight;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterArrowUp;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterArrowDown;
        private System.Windows.Forms.GroupBox groupDropDown;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropArrowLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropArrowRight;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropArrowUp;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropArrowDown;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropPosBottom;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropPosTop;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropPosLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropPosRight;
        private System.Windows.Forms.GroupBox groupCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton splitterCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu kcmCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems5;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem13;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem14;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem15;
    }
}

