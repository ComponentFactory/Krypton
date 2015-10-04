namespace KryptonBorderEdgeExamples
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
            this.borderAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderL = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderT = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderR = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderB = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderLT = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderTR = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderBR = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderBL = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderTB = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderLR = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderNone = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderTBR = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderLRB = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderTBL = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderLRT = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.borderEdgeH1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.borderEdgeV = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.panelButtonHost = new System.Windows.Forms.Panel();
            this.buttonEnd = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.borderEdgeSep3 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonNext = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.borderEdgeSep2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonPrevious = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.borderEdgeSep1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonBegin = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonCheckSet = new ComponentFactory.Krypton.Toolkit.KryptonCheckSet(this.components);
            this.groupBoxBorderEdge = new System.Windows.Forms.GroupBox();
            this.groupBoxButtonGroup = new System.Windows.Forms.GroupBox();
            this.groupBoxKryptonBorderEdge = new System.Windows.Forms.GroupBox();
            this.kryptonBorderEdge4 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge3 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxPalettes = new System.Windows.Forms.GroupBox();
            this.buttonOffice2007Blue = new System.Windows.Forms.Button();
            this.buttonOffice2010Blue = new System.Windows.Forms.Button();
            this.buttonSystem = new System.Windows.Forms.Button();
            this.buttonSparkle = new System.Windows.Forms.Button();
            this.buttonCustom = new System.Windows.Forms.Button();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPaletteOffice2010Blue = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonPaletteOffice2007Blue = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonPaletteCustom = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonPaletteSparkle = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonPaletteSystem = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.panelButtonHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).BeginInit();
            this.groupBoxBorderEdge.SuspendLayout();
            this.groupBoxButtonGroup.SuspendLayout();
            this.groupBoxKryptonBorderEdge.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxPalettes.SuspendLayout();
            this.SuspendLayout();
            // 
            // borderAll
            // 
            this.borderAll.AutoSize = true;
            this.borderAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderAll.Location = new System.Drawing.Point(78, 28);
            this.borderAll.Name = "borderAll";
            this.borderAll.Size = new System.Drawing.Size(47, 25);
            this.borderAll.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderAll.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderAll.StateCommon.Border.Rounding = 5;
            this.borderAll.TabIndex = 3;
            this.borderAll.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderL
            // 
            this.borderL.AutoSize = true;
            this.borderL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderL.Location = new System.Drawing.Point(16, 68);
            this.borderL.Name = "borderL";
            this.borderL.Size = new System.Drawing.Size(42, 19);
            this.borderL.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left;
            this.borderL.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderL.StateCommon.Border.Rounding = 5;
            this.borderL.TabIndex = 6;
            this.borderL.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderT
            // 
            this.borderT.AutoSize = true;
            this.borderT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderT.Location = new System.Drawing.Point(81, 67);
            this.borderT.Name = "borderT";
            this.borderT.Size = new System.Drawing.Size(41, 20);
            this.borderT.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top;
            this.borderT.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderT.StateCommon.Border.Rounding = 5;
            this.borderT.TabIndex = 7;
            this.borderT.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderR
            // 
            this.borderR.AutoSize = true;
            this.borderR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderR.Location = new System.Drawing.Point(141, 68);
            this.borderR.Name = "borderR";
            this.borderR.Size = new System.Drawing.Size(42, 19);
            this.borderR.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right;
            this.borderR.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderR.StateCommon.Border.Rounding = 5;
            this.borderR.TabIndex = 8;
            this.borderR.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderB
            // 
            this.borderB.AutoSize = true;
            this.borderB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderB.Location = new System.Drawing.Point(204, 67);
            this.borderB.Name = "borderB";
            this.borderB.Size = new System.Drawing.Size(41, 20);
            this.borderB.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom;
            this.borderB.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderB.StateCommon.Border.Rounding = 5;
            this.borderB.TabIndex = 9;
            this.borderB.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderLT
            // 
            this.borderLT.AutoSize = true;
            this.borderLT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderLT.Location = new System.Drawing.Point(15, 104);
            this.borderLT.Name = "borderLT";
            this.borderLT.Size = new System.Drawing.Size(44, 22);
            this.borderLT.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.borderLT.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderLT.StateCommon.Border.Rounding = 5;
            this.borderLT.TabIndex = 10;
            this.borderLT.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderTR
            // 
            this.borderTR.AutoSize = true;
            this.borderTR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderTR.Location = new System.Drawing.Point(79, 104);
            this.borderTR.Name = "borderTR";
            this.borderTR.Size = new System.Drawing.Size(44, 22);
            this.borderTR.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderTR.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderTR.StateCommon.Border.Rounding = 5;
            this.borderTR.TabIndex = 11;
            this.borderTR.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderBR
            // 
            this.borderBR.AutoSize = true;
            this.borderBR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderBR.Location = new System.Drawing.Point(140, 104);
            this.borderBR.Name = "borderBR";
            this.borderBR.Size = new System.Drawing.Size(44, 22);
            this.borderBR.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderBR.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderBR.StateCommon.Border.Rounding = 5;
            this.borderBR.TabIndex = 12;
            this.borderBR.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderBL
            // 
            this.borderBL.AutoSize = true;
            this.borderBL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderBL.Location = new System.Drawing.Point(202, 104);
            this.borderBL.Name = "borderBL";
            this.borderBL.Size = new System.Drawing.Size(44, 22);
            this.borderBL.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.borderBL.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderBL.StateCommon.Border.Rounding = 5;
            this.borderBL.TabIndex = 13;
            this.borderBL.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderTB
            // 
            this.borderTB.AutoSize = true;
            this.borderTB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderTB.Location = new System.Drawing.Point(142, 30);
            this.borderTB.Name = "borderTB";
            this.borderTB.Size = new System.Drawing.Size(41, 21);
            this.borderTB.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.borderTB.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderTB.StateCommon.Border.Rounding = 5;
            this.borderTB.TabIndex = 4;
            this.borderTB.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderLR
            // 
            this.borderLR.AutoSize = true;
            this.borderLR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderLR.Location = new System.Drawing.Point(203, 31);
            this.borderLR.Name = "borderLR";
            this.borderLR.Size = new System.Drawing.Size(43, 19);
            this.borderLR.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderLR.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderLR.StateCommon.Border.Rounding = 5;
            this.borderLR.TabIndex = 5;
            this.borderLR.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderNone
            // 
            this.borderNone.AutoSize = true;
            this.borderNone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderNone.Location = new System.Drawing.Point(17, 31);
            this.borderNone.Name = "borderNone";
            this.borderNone.Size = new System.Drawing.Size(41, 19);
            this.borderNone.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.borderNone.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderNone.StateCommon.Border.Rounding = 5;
            this.borderNone.TabIndex = 2;
            this.borderNone.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderTBR
            // 
            this.borderTBR.AutoSize = true;
            this.borderTBR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderTBR.Location = new System.Drawing.Point(15, 140);
            this.borderTBR.Name = "borderTBR";
            this.borderTBR.Size = new System.Drawing.Size(44, 25);
            this.borderTBR.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderTBR.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderTBR.StateCommon.Border.Rounding = 5;
            this.borderTBR.TabIndex = 14;
            this.borderTBR.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderLRB
            // 
            this.borderLRB.AutoSize = true;
            this.borderLRB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderLRB.Location = new System.Drawing.Point(78, 141);
            this.borderLRB.Name = "borderLRB";
            this.borderLRB.Size = new System.Drawing.Size(47, 22);
            this.borderLRB.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderLRB.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderLRB.StateCommon.Border.Rounding = 5;
            this.borderLRB.TabIndex = 15;
            this.borderLRB.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderTBL
            // 
            this.borderTBL.AutoSize = true;
            this.borderTBL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderTBL.Location = new System.Drawing.Point(140, 140);
            this.borderTBL.Name = "borderTBL";
            this.borderTBL.Size = new System.Drawing.Size(44, 25);
            this.borderTBL.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.borderTBL.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderTBL.StateCommon.Border.Rounding = 5;
            this.borderTBL.TabIndex = 16;
            this.borderTBL.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderLRT
            // 
            this.borderLRT.AutoSize = true;
            this.borderLRT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderLRT.Location = new System.Drawing.Point(201, 141);
            this.borderLRT.Name = "borderLRT";
            this.borderLRT.Size = new System.Drawing.Size(47, 22);
            this.borderLRT.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.borderLRT.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.borderLRT.StateCommon.Border.Rounding = 5;
            this.borderLRT.TabIndex = 17;
            this.borderLRT.Enter += new System.EventHandler(this.button_Enter);
            // 
            // borderEdgeH1
            // 
            this.borderEdgeH1.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeH1.Location = new System.Drawing.Point(21, 29);
            this.borderEdgeH1.Name = "borderEdgeH1";
            this.borderEdgeH1.Size = new System.Drawing.Size(75, 1);
            this.borderEdgeH1.Text = "kryptonBorderEdge1";
            this.borderEdgeH1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderEdge_MouseDown);
            // 
            // borderEdgeV
            // 
            this.borderEdgeV.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeV.Location = new System.Drawing.Point(21, 49);
            this.borderEdgeV.Name = "borderEdgeV";
            this.borderEdgeV.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeV.Size = new System.Drawing.Size(1, 50);
            this.borderEdgeV.Text = "kryptonBorderEdge2";
            this.borderEdgeV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderEdge_MouseDown);
            // 
            // panelButtonHost
            // 
            this.panelButtonHost.AutoSize = true;
            this.panelButtonHost.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtonHost.Controls.Add(this.buttonEnd);
            this.panelButtonHost.Controls.Add(this.borderEdgeSep3);
            this.panelButtonHost.Controls.Add(this.buttonNext);
            this.panelButtonHost.Controls.Add(this.borderEdgeSep2);
            this.panelButtonHost.Controls.Add(this.buttonPrevious);
            this.panelButtonHost.Controls.Add(this.borderEdgeSep1);
            this.panelButtonHost.Controls.Add(this.buttonBegin);
            this.panelButtonHost.Location = new System.Drawing.Point(18, 28);
            this.panelButtonHost.Name = "panelButtonHost";
            this.panelButtonHost.Size = new System.Drawing.Size(147, 42);
            this.panelButtonHost.TabIndex = 18;
            // 
            // buttonEnd
            // 
            this.buttonEnd.AutoSize = true;
            this.buttonEnd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEnd.Location = new System.Drawing.Point(109, 0);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(38, 42);
            this.buttonEnd.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonEnd.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonEnd.StateCommon.Border.Rounding = 8;
            this.buttonEnd.TabIndex = 3;
            this.buttonEnd.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonEnd.Values.Image")));
            this.buttonEnd.Values.Text = "";
            this.buttonEnd.Enter += new System.EventHandler(this.checkButton_Enter);
            // 
            // borderEdgeSep3
            // 
            this.borderEdgeSep3.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep3.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep3.Location = new System.Drawing.Point(108, 0);
            this.borderEdgeSep3.Name = "borderEdgeSep3";
            this.borderEdgeSep3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep3.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep3.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep3.Text = "kryptonBorderEdge2";
            this.borderEdgeSep3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderEdge_MouseDown);
            // 
            // buttonNext
            // 
            this.buttonNext.AutoSize = true;
            this.buttonNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonNext.Location = new System.Drawing.Point(74, 0);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(34, 42);
            this.buttonNext.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.buttonNext.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonNext.StateCommon.Border.Rounding = 8;
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonNext.Values.Image")));
            this.buttonNext.Values.Text = "";
            this.buttonNext.Enter += new System.EventHandler(this.checkButton_Enter);
            // 
            // borderEdgeSep2
            // 
            this.borderEdgeSep2.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep2.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep2.Location = new System.Drawing.Point(73, 0);
            this.borderEdgeSep2.Name = "borderEdgeSep2";
            this.borderEdgeSep2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep2.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep2.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep2.Text = "kryptonBorderEdge1";
            this.borderEdgeSep2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderEdge_MouseDown);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.AutoSize = true;
            this.buttonPrevious.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevious.Location = new System.Drawing.Point(39, 0);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(34, 42);
            this.buttonPrevious.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.buttonPrevious.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonPrevious.StateCommon.Border.Rounding = 8;
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrevious.Values.Image")));
            this.buttonPrevious.Values.Text = "";
            this.buttonPrevious.Enter += new System.EventHandler(this.checkButton_Enter);
            // 
            // borderEdgeSep1
            // 
            this.borderEdgeSep1.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep1.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep1.Location = new System.Drawing.Point(38, 0);
            this.borderEdgeSep1.Name = "borderEdgeSep1";
            this.borderEdgeSep1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep1.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep1.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep1.Text = "kryptonBorderEdge3";
            this.borderEdgeSep1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderEdge_MouseDown);
            // 
            // buttonBegin
            // 
            this.buttonBegin.AutoSize = true;
            this.buttonBegin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonBegin.Checked = true;
            this.buttonBegin.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBegin.Location = new System.Drawing.Point(0, 0);
            this.buttonBegin.Name = "buttonBegin";
            this.buttonBegin.Size = new System.Drawing.Size(38, 42);
            this.buttonBegin.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.buttonBegin.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonBegin.StateCommon.Border.Rounding = 8;
            this.buttonBegin.TabIndex = 0;
            this.buttonBegin.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonBegin.Values.Image")));
            this.buttonBegin.Values.Text = "";
            this.buttonBegin.Enter += new System.EventHandler(this.checkButton_Enter);
            // 
            // kryptonCheckSet
            // 
            this.kryptonCheckSet.CheckButtons.Add(this.buttonEnd);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonNext);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonPrevious);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonBegin);
            this.kryptonCheckSet.CheckedButton = this.buttonBegin;
            // 
            // groupBoxBorderEdge
            // 
            this.groupBoxBorderEdge.Controls.Add(this.borderNone);
            this.groupBoxBorderEdge.Controls.Add(this.borderAll);
            this.groupBoxBorderEdge.Controls.Add(this.borderL);
            this.groupBoxBorderEdge.Controls.Add(this.borderT);
            this.groupBoxBorderEdge.Controls.Add(this.borderLRT);
            this.groupBoxBorderEdge.Controls.Add(this.borderR);
            this.groupBoxBorderEdge.Controls.Add(this.borderTBL);
            this.groupBoxBorderEdge.Controls.Add(this.borderB);
            this.groupBoxBorderEdge.Controls.Add(this.borderLRB);
            this.groupBoxBorderEdge.Controls.Add(this.borderLT);
            this.groupBoxBorderEdge.Controls.Add(this.borderTBR);
            this.groupBoxBorderEdge.Controls.Add(this.borderTR);
            this.groupBoxBorderEdge.Controls.Add(this.borderBR);
            this.groupBoxBorderEdge.Controls.Add(this.borderLR);
            this.groupBoxBorderEdge.Controls.Add(this.borderBL);
            this.groupBoxBorderEdge.Controls.Add(this.borderTB);
            this.groupBoxBorderEdge.Location = new System.Drawing.Point(12, 133);
            this.groupBoxBorderEdge.Name = "groupBoxBorderEdge";
            this.groupBoxBorderEdge.Size = new System.Drawing.Size(355, 183);
            this.groupBoxBorderEdge.TabIndex = 1;
            this.groupBoxBorderEdge.TabStop = false;
            this.groupBoxBorderEdge.Text = "KryptonButton with StateCommon -> Border -> DrawBorders applied";
            // 
            // groupBoxButtonGroup
            // 
            this.groupBoxButtonGroup.Controls.Add(this.panelButtonHost);
            this.groupBoxButtonGroup.Location = new System.Drawing.Point(12, 322);
            this.groupBoxButtonGroup.Name = "groupBoxButtonGroup";
            this.groupBoxButtonGroup.Size = new System.Drawing.Size(355, 85);
            this.groupBoxButtonGroup.TabIndex = 2;
            this.groupBoxButtonGroup.TabStop = false;
            this.groupBoxButtonGroup.Text = "Combine KryptonCheckButton + KryptonBorderEdge to create group";
            // 
            // groupBoxKryptonBorderEdge
            // 
            this.groupBoxKryptonBorderEdge.Controls.Add(this.kryptonBorderEdge4);
            this.groupBoxKryptonBorderEdge.Controls.Add(this.kryptonBorderEdge3);
            this.groupBoxKryptonBorderEdge.Controls.Add(this.kryptonBorderEdge2);
            this.groupBoxKryptonBorderEdge.Controls.Add(this.kryptonBorderEdge1);
            this.groupBoxKryptonBorderEdge.Controls.Add(this.borderEdgeH1);
            this.groupBoxKryptonBorderEdge.Controls.Add(this.borderEdgeV);
            this.groupBoxKryptonBorderEdge.Location = new System.Drawing.Point(12, 12);
            this.groupBoxKryptonBorderEdge.Name = "groupBoxKryptonBorderEdge";
            this.groupBoxKryptonBorderEdge.Size = new System.Drawing.Size(355, 115);
            this.groupBoxKryptonBorderEdge.TabIndex = 0;
            this.groupBoxKryptonBorderEdge.TabStop = false;
            this.groupBoxKryptonBorderEdge.Text = "KryptonBorderEdge Instances";
            // 
            // kryptonBorderEdge4
            // 
            this.kryptonBorderEdge4.AutoSize = false;
            this.kryptonBorderEdge4.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonBorderEdge4.Location = new System.Drawing.Point(199, 49);
            this.kryptonBorderEdge4.Name = "kryptonBorderEdge4";
            this.kryptonBorderEdge4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge4.Size = new System.Drawing.Size(10, 50);
            this.kryptonBorderEdge4.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge3
            // 
            this.kryptonBorderEdge3.AutoSize = false;
            this.kryptonBorderEdge3.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonBorderEdge3.Location = new System.Drawing.Point(113, 49);
            this.kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            this.kryptonBorderEdge3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge3.Size = new System.Drawing.Size(5, 50);
            this.kryptonBorderEdge3.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.AutoSize = false;
            this.kryptonBorderEdge2.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(199, 29);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(75, 10);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.AutoSize = false;
            this.kryptonBorderEdge1.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(110, 29);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(75, 5);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid);
            this.groupBox4.Location = new System.Drawing.Point(373, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 488);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties for Selection";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(289, 463);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(599, 512);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxPalettes
            // 
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2007Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2010Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonSystem);
            this.groupBoxPalettes.Controls.Add(this.buttonSparkle);
            this.groupBoxPalettes.Controls.Add(this.buttonCustom);
            this.groupBoxPalettes.Location = new System.Drawing.Point(12, 413);
            this.groupBoxPalettes.Name = "groupBoxPalettes";
            this.groupBoxPalettes.Size = new System.Drawing.Size(355, 87);
            this.groupBoxPalettes.TabIndex = 3;
            this.groupBoxPalettes.TabStop = false;
            this.groupBoxPalettes.Text = "Palettes";
            // 
            // buttonOffice2007Blue
            // 
            this.buttonOffice2007Blue.Location = new System.Drawing.Point(25, 50);
            this.buttonOffice2007Blue.Name = "buttonOffice2007Blue";
            this.buttonOffice2007Blue.Size = new System.Drawing.Size(102, 25);
            this.buttonOffice2007Blue.TabIndex = 1;
            this.buttonOffice2007Blue.Text = "Office 2007 - Blue";
            this.buttonOffice2007Blue.UseVisualStyleBackColor = true;
            this.buttonOffice2007Blue.Click += new System.EventHandler(this.buttonOffice2007Blue_Click);
            // 
            // buttonOffice2010Blue
            // 
            this.buttonOffice2010Blue.Location = new System.Drawing.Point(25, 21);
            this.buttonOffice2010Blue.Name = "buttonOffice2010Blue";
            this.buttonOffice2010Blue.Size = new System.Drawing.Size(102, 25);
            this.buttonOffice2010Blue.TabIndex = 0;
            this.buttonOffice2010Blue.Text = "Office 2010 - Blue";
            this.buttonOffice2010Blue.UseVisualStyleBackColor = true;
            this.buttonOffice2010Blue.Click += new System.EventHandler(this.buttonOffice2010Blue_Click);
            // 
            // buttonSystem
            // 
            this.buttonSystem.Location = new System.Drawing.Point(142, 50);
            this.buttonSystem.Name = "buttonSystem";
            this.buttonSystem.Size = new System.Drawing.Size(101, 25);
            this.buttonSystem.TabIndex = 3;
            this.buttonSystem.Text = "System";
            this.buttonSystem.UseVisualStyleBackColor = true;
            this.buttonSystem.Click += new System.EventHandler(this.buttonSystem_Click);
            // 
            // buttonSparkle
            // 
            this.buttonSparkle.Location = new System.Drawing.Point(141, 21);
            this.buttonSparkle.Name = "buttonSparkle";
            this.buttonSparkle.Size = new System.Drawing.Size(102, 25);
            this.buttonSparkle.TabIndex = 2;
            this.buttonSparkle.Text = "Sparkle - Blue";
            this.buttonSparkle.UseVisualStyleBackColor = true;
            this.buttonSparkle.Click += new System.EventHandler(this.buttonSparkle_Click);
            // 
            // buttonCustom
            // 
            this.buttonCustom.Location = new System.Drawing.Point(259, 21);
            this.buttonCustom.Name = "buttonCustom";
            this.buttonCustom.Size = new System.Drawing.Size(75, 25);
            this.buttonCustom.TabIndex = 4;
            this.buttonCustom.Text = "Custom";
            this.buttonCustom.UseVisualStyleBackColor = true;
            this.buttonCustom.Click += new System.EventHandler(this.buttonCustom_Click);
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPalette = this.kryptonPaletteOffice2010Blue;
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // kryptonPaletteOffice2007Blue
            // 
            this.kryptonPaletteOffice2007Blue.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            // 
            // kryptonPaletteCustom
            // 
            this.kryptonPaletteCustom.AllowFormChrome = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateDisabled.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Border.Color1 = System.Drawing.Color.Transparent;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Content.Padding = new System.Windows.Forms.Padding(3);
            this.kryptonPaletteCustom.ButtonStyles.ButtonButtonSpec.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCheckedNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCheckedPressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCheckedPressed.Content.Padding = new System.Windows.Forms.Padding(5, 5, 1, 1);
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCheckedTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.Rounding = 3;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Border.Width = 3;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Content.LongText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Content.LongText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Content.Padding = new System.Windows.Forms.Padding(3);
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateCommon.Content.ShortText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateDisabled.Border.Color1 = System.Drawing.Color.Silver;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateDisabled.Content.LongText.Color1 = System.Drawing.Color.Silver;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.Silver;
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StatePressed.Content.Padding = new System.Windows.Forms.Padding(5, 5, 1, 1);
            this.kryptonPaletteCustom.ButtonStyles.ButtonCommon.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateDisabled.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Border.Color1 = System.Drawing.Color.Transparent;
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Content.Padding = new System.Windows.Forms.Padding(3);
            this.kryptonPaletteCustom.ButtonStyles.ButtonLowProfile.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateCommon.Border.Rounding = 9;
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateCommon.Border.Width = 3;
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateDisabled.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ControlStyles.ControlCommon.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.HeaderGroup.StateCommon.OverlayHeaders = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.HeaderGroup.StateCommon.PrimaryHeaderPadding = new System.Windows.Forms.Padding(3);
            this.kryptonPaletteCustom.HeaderGroup.StateCommon.SecondaryHeaderPadding = new System.Windows.Forms.Padding(3);
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Border.Rounding = 7;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Border.Width = 3;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.AdjacentGap = 2;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.LongText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.ShortText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateDisabled.Content.LongText.Color1 = System.Drawing.Color.Silver;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCommon.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.Silver;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom1.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom1.StateDisabled.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom1.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(213)))), ((int)(((byte)(194)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom1.StateNormal.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom2.StateDisabled.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom2.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom2.StateNormal.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderCustom2.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(213)))), ((int)(((byte)(194)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateDisabled.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(213)))), ((int)(((byte)(194)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.Color2 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderSecondary.StateDisabled.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderSecondary.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.HeaderStyles.HeaderSecondary.StateNormal.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.HeaderStyles.HeaderSecondary.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(213)))), ((int)(((byte)(194)))));
            this.kryptonPaletteCustom.PanelStyles.PanelAlternate.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.PanelStyles.PanelClient.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.kryptonPaletteCustom.PanelStyles.PanelCommon.StateCommon.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.PanelStyles.PanelCustom1.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Border.Width = 2;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Content.LongText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Content.LongText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Content.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateCommon.Content.ShortText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StatePressed.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StatePressed.Content.Padding = new System.Windows.Forms.Padding(4, 6, 2, 4);
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateSelected.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateSelected.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateSelected.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.QuarterPhase;
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateSelected.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateSelected.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.TabStyles.TabCommon.StateTracking.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateCommon.Content.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateNormal.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateNormal.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StatePressed.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StatePressed.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateSelected.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateSelected.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateSelected.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateTracking.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateTracking.Content.LongText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabLowProfile.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.TabStyles.TabOneNote.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonPaletteCustom.TabStyles.TabOneNote.StateSelected.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.TabStyles.TabStandardProfile.StateSelected.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.TabStyles.TabStandardProfile.StateSelected.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonCheckedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonCheckedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonCheckedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonCheckedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonCheckedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonPressedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.ButtonSelectedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.CheckBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.CheckPressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.CheckSelectedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.OverflowButtonGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.OverflowButtonGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(77)))), ((int)(((byte)(144)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Button.OverflowButtonGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(126)))), ((int)(((byte)(226)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Grip.GripDark = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(133)))), ((int)(((byte)(215)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Grip.GripLight = System.Drawing.Color.Transparent;
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(126)))), ((int)(((byte)(226)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginRevealedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginRevealedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.ImageMarginRevealedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(126)))), ((int)(((byte)(226)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemSelected = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Menu.MenuItemText = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ToolMenuStatus.MenuStrip.MenuStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.MenuStrip.MenuStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.MenuStrip.MenuStripText = System.Drawing.Color.WhiteSmoke;
            this.kryptonPaletteCustom.ToolMenuStatus.Rafting.RaftingContainerGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Rafting.RaftingContainerGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Separator.SeparatorDark = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.Separator.SeparatorLight = System.Drawing.Color.Transparent;
            this.kryptonPaletteCustom.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.StatusStrip.StatusStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.StatusStrip.StatusStripText = System.Drawing.Color.WhiteSmoke;
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripBorder = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(77)))), ((int)(((byte)(144)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripContentPanelGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripContentPanelGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripDropDownBackground = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(77)))), ((int)(((byte)(144)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(77)))), ((int)(((byte)(144)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(77)))), ((int)(((byte)(144)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripPanelGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripPanelGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.kryptonPaletteCustom.ToolMenuStatus.ToolStrip.ToolStripText = System.Drawing.Color.WhiteSmoke;
            this.kryptonPaletteCustom.ToolMenuStatus.UseRoundedEdges = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            // 
            // kryptonPaletteSparkle
            // 
            this.kryptonPaletteSparkle.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPaletteSystem
            // 
            this.kryptonPaletteSystem.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 547);
            this.Controls.Add(this.groupBoxPalettes);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxKryptonBorderEdge);
            this.Controls.Add(this.groupBoxButtonGroup);
            this.Controls.Add(this.groupBoxBorderEdge);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonBorderEdge Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelButtonHost.ResumeLayout(false);
            this.panelButtonHost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).EndInit();
            this.groupBoxBorderEdge.ResumeLayout(false);
            this.groupBoxBorderEdge.PerformLayout();
            this.groupBoxButtonGroup.ResumeLayout(false);
            this.groupBoxButtonGroup.PerformLayout();
            this.groupBoxKryptonBorderEdge.ResumeLayout(false);
            this.groupBoxKryptonBorderEdge.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBoxPalettes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeH1;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeV;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderL;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderT;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderR;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderLT;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderTR;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderBR;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderBL;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderTB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderLR;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderNone;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderTBR;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderLRB;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderTBL;
        private ComponentFactory.Krypton.Toolkit.KryptonButton borderLRT;
        private System.Windows.Forms.Panel panelButtonHost;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckSet kryptonCheckSet;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonBegin;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonPrevious;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonNext;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonEnd;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep1;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep2;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep3;
        private System.Windows.Forms.GroupBox groupBoxBorderEdge;
        private System.Windows.Forms.GroupBox groupBoxButtonGroup;
        private System.Windows.Forms.GroupBox groupBoxKryptonBorderEdge;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxPalettes;
        private System.Windows.Forms.Button buttonCustom;
        private System.Windows.Forms.Button buttonSystem;
        private System.Windows.Forms.Button buttonSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteSystem;
        private System.Windows.Forms.Button buttonOffice2010Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteOffice2007Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge4;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge3;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private System.Windows.Forms.Button buttonOffice2007Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteOffice2010Blue;
    }
}

