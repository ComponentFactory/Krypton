namespace MDIRibbon
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
            this.ribbon = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.buttonSpecHelp = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.tabHome = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.buttonNewWindow = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupSeparator1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupSeparator();
            this.kryptonRibbonGroupTriple3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.buttonCloseWindow = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.buttonCloseAllWindows = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroup1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.buttonCascade = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.buttonTileHorizontal = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.buttonTileVertical = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecHelp});
            this.ribbon.Name = "ribbon";
            this.ribbon.QATLocation = ComponentFactory.Krypton.Ribbon.QATLocation.Hidden;
            this.ribbon.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1});
            this.ribbon.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.ribbon.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.tabHome});
            this.ribbon.SelectedTab = this.tabHome;
            this.ribbon.Size = new System.Drawing.Size(692, 114);
            this.ribbon.TabIndex = 0;
            // 
            // buttonSpecHelp
            // 
            this.buttonSpecHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonSpecHelp.Image")));
            this.buttonSpecHelp.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.ButtonSpec;
            this.buttonSpecHelp.UniqueName = "06E98F3735BC4B1106E98F3735BC4B11";
            this.buttonSpecHelp.Click += new System.EventHandler(this.buttonSpecHelp_Click);
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonContextMenuItem1.Image")));
            this.kryptonContextMenuItem1.Text = "E&xit";
            this.kryptonContextMenuItem1.Click += new System.EventHandler(this.appMenu_Click);
            // 
            // tabHome
            // 
            this.tabHome.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup2,
            this.kryptonRibbonGroup1});
            this.tabHome.KeyTip = "H";
            this.tabHome.Text = "Home";
            // 
            // kryptonRibbonGroup2
            // 
            this.kryptonRibbonGroup2.DialogBoxLauncher = false;
            this.kryptonRibbonGroup2.Image = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroup2.Image")));
            this.kryptonRibbonGroup2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple2,
            this.kryptonRibbonGroupSeparator1,
            this.kryptonRibbonGroupTriple3});
            this.kryptonRibbonGroup2.KeyTipGroup = "O";
            this.kryptonRibbonGroup2.TextLine1 = "Operations";
            // 
            // kryptonRibbonGroupTriple2
            // 
            this.kryptonRibbonGroupTriple2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.buttonNewWindow});
            // 
            // buttonNewWindow
            // 
            this.buttonNewWindow.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonNewWindow.ImageLarge")));
            this.buttonNewWindow.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonNewWindow.ImageSmall")));
            this.buttonNewWindow.KeyTip = "N";
            this.buttonNewWindow.TextLine1 = "New";
            this.buttonNewWindow.TextLine2 = "Window";
            this.buttonNewWindow.Click += new System.EventHandler(this.buttonNewWindow_Click);
            // 
            // kryptonRibbonGroupTriple3
            // 
            this.kryptonRibbonGroupTriple3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.buttonCloseWindow,
            this.buttonCloseAllWindows});
            // 
            // buttonCloseWindow
            // 
            this.buttonCloseWindow.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonCloseWindow.ImageLarge")));
            this.buttonCloseWindow.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonCloseWindow.ImageSmall")));
            this.buttonCloseWindow.KeyTip = "X";
            this.buttonCloseWindow.TextLine1 = "Close";
            this.buttonCloseWindow.TextLine2 = "Window";
            this.buttonCloseWindow.Click += new System.EventHandler(this.buttonCloseWindow_Click);
            // 
            // buttonCloseAllWindows
            // 
            this.buttonCloseAllWindows.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonCloseAllWindows.ImageLarge")));
            this.buttonCloseAllWindows.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonCloseAllWindows.ImageSmall")));
            this.buttonCloseAllWindows.KeyTip = "A";
            this.buttonCloseAllWindows.TextLine1 = "Close All";
            this.buttonCloseAllWindows.TextLine2 = "Windows";
            this.buttonCloseAllWindows.Click += new System.EventHandler(this.buttonCloseAllWindows_Click);
            // 
            // kryptonRibbonGroup1
            // 
            this.kryptonRibbonGroup1.DialogBoxLauncher = false;
            this.kryptonRibbonGroup1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonRibbonGroup1.Image")));
            this.kryptonRibbonGroup1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple4});
            this.kryptonRibbonGroup1.KeyTipGroup = "A";
            this.kryptonRibbonGroup1.TextLine1 = "Arrange";
            // 
            // kryptonRibbonGroupTriple4
            // 
            this.kryptonRibbonGroupTriple4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.buttonCascade,
            this.buttonTileHorizontal,
            this.buttonTileVertical});
            // 
            // buttonCascade
            // 
            this.buttonCascade.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonCascade.ImageLarge")));
            this.buttonCascade.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonCascade.ImageSmall")));
            this.buttonCascade.KeyTip = "C";
            this.buttonCascade.TextLine1 = "Cascade";
            this.buttonCascade.Click += new System.EventHandler(this.buttonCascade_Click);
            // 
            // buttonTileHorizontal
            // 
            this.buttonTileHorizontal.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonTileHorizontal.ImageLarge")));
            this.buttonTileHorizontal.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonTileHorizontal.ImageSmall")));
            this.buttonTileHorizontal.KeyTip = "H";
            this.buttonTileHorizontal.TextLine1 = "Tile";
            this.buttonTileHorizontal.TextLine2 = "Horizontal";
            this.buttonTileHorizontal.Click += new System.EventHandler(this.buttonTileHorizontal_Click);
            // 
            // buttonTileVertical
            // 
            this.buttonTileVertical.ImageLarge = ((System.Drawing.Image)(resources.GetObject("buttonTileVertical.ImageLarge")));
            this.buttonTileVertical.ImageSmall = ((System.Drawing.Image)(resources.GetObject("buttonTileVertical.ImageSmall")));
            this.buttonTileVertical.KeyTip = "V";
            this.buttonTileVertical.TextLine1 = "Tile";
            this.buttonTileVertical.TextLine2 = "Vertical";
            this.buttonTileVertical.Click += new System.EventHandler(this.buttonTileVertical_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 516);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(350, 350);
            this.Name = "Form1";
            this.Text = "MDI Ribbon";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonRibbon ribbon;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab tabHome;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonNewWindow;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupSeparator kryptonRibbonGroupSeparator1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple3;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonCloseWindow;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonCloseAllWindows;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple4;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonCascade;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonTileHorizontal;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton buttonTileVertical;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecHelp;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

