namespace ChildControlStack
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolOffice2010 = new System.Windows.Forms.ToolStripButton();
            this.toolOffice2007 = new System.Windows.Forms.ToolStripButton();
            this.toolSparkle = new System.Windows.Forms.ToolStripButton();
            this.toolSystem = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOffice2010 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOffice2007 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSparkle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.kryptonPanelMain = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupMain = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.textBoxFiller = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonBorderEdge5 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonBorderEdge4 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonCheckButton4 = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonBorderEdge3 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonCheckButton3 = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonBorderEdge2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonCheckButton2 = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonCheckButton1 = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonCheckSet = new ComponentFactory.Krypton.Toolkit.KryptonCheckSet(this.components);
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupMain.Panel)).BeginInit();
            this.kryptonGroupMain.Panel.SuspendLayout();
            this.kryptonGroupMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOffice2010,
            this.toolOffice2007,
            this.toolSparkle,
            this.toolSystem});
            this.toolStrip.Location = new System.Drawing.Point(3, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(180, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolOffice2010
            // 
            this.toolOffice2010.Checked = true;
            this.toolOffice2010.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolOffice2010.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOffice2010.Image = ((System.Drawing.Image)(resources.GetObject("toolOffice2010.Image")));
            this.toolOffice2010.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOffice2010.Name = "toolOffice2010";
            this.toolOffice2010.Size = new System.Drawing.Size(35, 22);
            this.toolOffice2010.Text = "2010";
            this.toolOffice2010.Click += new System.EventHandler(this.toolOffice2010_Click);
            // 
            // toolOffice2007
            // 
            this.toolOffice2007.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOffice2007.Image = ((System.Drawing.Image)(resources.GetObject("toolOffice2007.Image")));
            this.toolOffice2007.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOffice2007.Name = "toolOffice2007";
            this.toolOffice2007.Size = new System.Drawing.Size(35, 22);
            this.toolOffice2007.Text = "2007";
            this.toolOffice2007.Click += new System.EventHandler(this.toolOffice2007_Click);
            // 
            // toolSparkle
            // 
            this.toolSparkle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSparkle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSparkle.Name = "toolSparkle";
            this.toolSparkle.Size = new System.Drawing.Size(49, 22);
            this.toolSparkle.Text = "Sparkle";
            this.toolSparkle.Click += new System.EventHandler(this.toolSparkle_Click);
            // 
            // toolSystem
            // 
            this.toolSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSystem.ImageTransparentColor = System.Drawing.Color.YellowGreen;
            this.toolSystem.Name = "toolSystem";
            this.toolSystem.Size = new System.Drawing.Size(49, 22);
            this.toolSystem.Text = "System";
            this.toolSystem.Click += new System.EventHandler(this.toolSystem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(322, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOffice2010,
            this.menuOffice2007,
            this.menuSparkle,
            this.menuSystem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // menuOffice2010
            // 
            this.menuOffice2010.Checked = true;
            this.menuOffice2010.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuOffice2010.Name = "menuOffice2010";
            this.menuOffice2010.Size = new System.Drawing.Size(167, 22);
            this.menuOffice2010.Text = "Office 2010 - Blue";
            this.menuOffice2010.Click += new System.EventHandler(this.toolOffice2010_Click);
            // 
            // menuOffice2007
            // 
            this.menuOffice2007.Name = "menuOffice2007";
            this.menuOffice2007.Size = new System.Drawing.Size(167, 22);
            this.menuOffice2007.Text = "Office 2007 - Blue";
            this.menuOffice2007.Click += new System.EventHandler(this.toolOffice2007_Click);
            // 
            // menuSparkle
            // 
            this.menuSparkle.Name = "menuSparkle";
            this.menuSparkle.Size = new System.Drawing.Size(167, 22);
            this.menuSparkle.Text = "Sparkle - Blue";
            this.menuSparkle.Click += new System.EventHandler(this.toolSparkle_Click);
            // 
            // menuSystem
            // 
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(167, 22);
            this.menuSystem.Text = "System";
            this.menuSystem.Click += new System.EventHandler(this.toolSystem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.kryptonPanelMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(322, 265);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(322, 290);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // kryptonPanelMain
            // 
            this.kryptonPanelMain.Controls.Add(this.kryptonGroupMain);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPanelMain.Size = new System.Drawing.Size(322, 265);
            this.kryptonPanelMain.TabIndex = 0;
            // 
            // kryptonGroupMain
            // 
            this.kryptonGroupMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonGroupMain.Location = new System.Drawing.Point(5, 5);
            this.kryptonGroupMain.Name = "kryptonGroupMain";
            // 
            // kryptonGroupMain.Panel
            // 
            this.kryptonGroupMain.Panel.Controls.Add(this.textBoxFiller);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonBorderEdge5);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonHeader);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonBorderEdge4);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonCheckButton4);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonBorderEdge3);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonCheckButton3);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonBorderEdge2);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonCheckButton2);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonGroupMain.Panel.Controls.Add(this.kryptonCheckButton1);
            this.kryptonGroupMain.Size = new System.Drawing.Size(312, 255);
            this.kryptonGroupMain.TabIndex = 0;
            // 
            // textBoxFiller
            // 
            this.textBoxFiller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFiller.Location = new System.Drawing.Point(0, 29);
            this.textBoxFiller.Multiline = true;
            this.textBoxFiller.Name = "textBoxFiller";
            this.textBoxFiller.Size = new System.Drawing.Size(310, 120);
            this.textBoxFiller.StateCommon.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.textBoxFiller.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.textBoxFiller.TabIndex = 0;
            this.textBoxFiller.Text = resources.GetString("textBoxFiller.Text");
            // 
            // kryptonBorderEdge5
            // 
            this.kryptonBorderEdge5.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge5.Location = new System.Drawing.Point(0, 28);
            this.kryptonBorderEdge5.Name = "kryptonBorderEdge5";
            this.kryptonBorderEdge5.Size = new System.Drawing.Size(310, 1);
            this.kryptonBorderEdge5.Text = "kryptonBorderEdge5";
            // 
            // kryptonHeader
            // 
            this.kryptonHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader.Name = "kryptonHeader";
            this.kryptonHeader.Size = new System.Drawing.Size(310, 28);
            this.kryptonHeader.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kryptonHeader.TabIndex = 5;
            this.kryptonHeader.Values.Description = "";
            this.kryptonHeader.Values.Heading = "Calendar";
            this.kryptonHeader.Values.Image = null;
            // 
            // kryptonBorderEdge4
            // 
            this.kryptonBorderEdge4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge4.Location = new System.Drawing.Point(0, 149);
            this.kryptonBorderEdge4.Name = "kryptonBorderEdge4";
            this.kryptonBorderEdge4.Size = new System.Drawing.Size(310, 1);
            this.kryptonBorderEdge4.Text = "kryptonBorderEdge4";
            // 
            // kryptonCheckButton4
            // 
            this.kryptonCheckButton4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonCheckButton4.Location = new System.Drawing.Point(0, 150);
            this.kryptonCheckButton4.Name = "kryptonCheckButton4";
            this.kryptonCheckButton4.Size = new System.Drawing.Size(310, 25);
            this.kryptonCheckButton4.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kryptonCheckButton4.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton4.StateCommon.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton4.StateCommon.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonCheckButton4.StateCommon.Content.LongText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton4.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton4.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton4.TabIndex = 2;
            this.kryptonCheckButton4.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonCheckButton4.Values.Image")));
            this.kryptonCheckButton4.Values.Text = "Tasks";
            // 
            // kryptonBorderEdge3
            // 
            this.kryptonBorderEdge3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge3.Location = new System.Drawing.Point(0, 175);
            this.kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            this.kryptonBorderEdge3.Size = new System.Drawing.Size(310, 1);
            this.kryptonBorderEdge3.Text = "kryptonBorderEdge3";
            // 
            // kryptonCheckButton3
            // 
            this.kryptonCheckButton3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonCheckButton3.Location = new System.Drawing.Point(0, 176);
            this.kryptonCheckButton3.Name = "kryptonCheckButton3";
            this.kryptonCheckButton3.Size = new System.Drawing.Size(310, 25);
            this.kryptonCheckButton3.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kryptonCheckButton3.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton3.StateCommon.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton3.StateCommon.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonCheckButton3.StateCommon.Content.LongText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton3.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton3.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton3.TabIndex = 3;
            this.kryptonCheckButton3.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonCheckButton3.Values.Image")));
            this.kryptonCheckButton3.Values.Text = "Mail";
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(0, 201);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(310, 1);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // kryptonCheckButton2
            // 
            this.kryptonCheckButton2.Checked = true;
            this.kryptonCheckButton2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonCheckButton2.Location = new System.Drawing.Point(0, 202);
            this.kryptonCheckButton2.Name = "kryptonCheckButton2";
            this.kryptonCheckButton2.Size = new System.Drawing.Size(310, 25);
            this.kryptonCheckButton2.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kryptonCheckButton2.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton2.StateCommon.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton2.StateCommon.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonCheckButton2.StateCommon.Content.LongText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton2.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton2.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton2.TabIndex = 4;
            this.kryptonCheckButton2.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonCheckButton2.Values.Image")));
            this.kryptonCheckButton2.Values.Text = "Calendar";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 227);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(310, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonCheckButton1
            // 
            this.kryptonCheckButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonCheckButton1.Location = new System.Drawing.Point(0, 228);
            this.kryptonCheckButton1.Name = "kryptonCheckButton1";
            this.kryptonCheckButton1.Size = new System.Drawing.Size(310, 25);
            this.kryptonCheckButton1.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.None;
            this.kryptonCheckButton1.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton1.StateCommon.Content.Image.ImageV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton1.StateCommon.Content.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.kryptonCheckButton1.StateCommon.Content.LongText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton1.StateCommon.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonCheckButton1.StateCommon.Content.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonCheckButton1.TabIndex = 5;
            this.kryptonCheckButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonCheckButton1.Values.Image")));
            this.kryptonCheckButton1.Values.Text = "Notes";
            // 
            // kryptonCheckSet
            // 
            this.kryptonCheckSet.CheckButtons.Add(this.kryptonCheckButton1);
            this.kryptonCheckSet.CheckButtons.Add(this.kryptonCheckButton2);
            this.kryptonCheckSet.CheckButtons.Add(this.kryptonCheckButton3);
            this.kryptonCheckSet.CheckButtons.Add(this.kryptonCheckButton4);
            this.kryptonCheckSet.CheckedButton = this.kryptonCheckButton2;
            this.kryptonCheckSet.CheckedButtonChanged += new System.EventHandler(this.kryptonCheckSet_CheckedButtonChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 314);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(257, 348);
            this.Name = "Form1";
            this.Text = "Child Control Stack";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupMain.Panel)).EndInit();
            this.kryptonGroupMain.Panel.ResumeLayout(false);
            this.kryptonGroupMain.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupMain)).EndInit();
            this.kryptonGroupMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolSystem;
        private System.Windows.Forms.ToolStripButton toolSparkle;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuSparkle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroupMain;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge4;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton kryptonCheckButton4;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge3;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton kryptonCheckButton3;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton kryptonCheckButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton kryptonCheckButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckSet kryptonCheckSet;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge5;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox textBoxFiller;
        private System.Windows.Forms.ToolStripButton toolOffice2010;
        private System.Windows.Forms.ToolStripButton toolOffice2007;
        private System.Windows.Forms.ToolStripMenuItem menuOffice2010;
        private System.Windows.Forms.ToolStripMenuItem menuOffice2007;

    }
}

