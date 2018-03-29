namespace ExternalDragToDocking
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
      this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
      this.kryptonDockingManager = new ComponentFactory.Krypton.Docking.KryptonDockingManager();
      this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.kryptonDockableWorkspace = new ComponentFactory.Krypton.Docking.KryptonDockableWorkspace();
      this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
      this.kryptonPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).BeginInit();
      this.SuspendLayout();
      // 
      // kryptonManager
      // 
      this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.SparklePurple;
      // 
      // kryptonPanel
      // 
      this.kryptonPanel.Controls.Add(this.kryptonDockableWorkspace);
      this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
      this.kryptonPanel.Name = "kryptonPanel";
      this.kryptonPanel.Size = new System.Drawing.Size(749, 520);
      this.kryptonPanel.TabIndex = 0;
      // 
      // kryptonDockableWorkspace
      // 
      this.kryptonDockableWorkspace.AutoHiddenHost = false;
      this.kryptonDockableWorkspace.CompactFlags = ((ComponentFactory.Krypton.Workspace.CompactFlags)(((ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptyCells | ComponentFactory.Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | ComponentFactory.Krypton.Workspace.CompactFlags.PromoteLeafs)));
      this.kryptonDockableWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
      this.kryptonDockableWorkspace.Location = new System.Drawing.Point(0, 0);
      this.kryptonDockableWorkspace.Name = "kryptonDockableWorkspace";
      // 
      // 
      // 
      this.kryptonDockableWorkspace.Root.UniqueName = "6C9A3354441646DC46BF0E62E96721F3";
      this.kryptonDockableWorkspace.Root.WorkspaceControl = this.kryptonDockableWorkspace;
      this.kryptonDockableWorkspace.ShowMaximizeButton = false;
      this.kryptonDockableWorkspace.Size = new System.Drawing.Size(749, 520);
      this.kryptonDockableWorkspace.TabIndex = 0;
      this.kryptonDockableWorkspace.TabStop = true;
      // 
      // imageListSmall
      // 
      this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
      this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListSmall.Images.SetKeyName(0, "document_plain.png");
      this.imageListSmall.Images.SetKeyName(1, "preferences.png");
      this.imageListSmall.Images.SetKeyName(2, "information2.png");
      this.imageListSmall.Images.SetKeyName(3, "dictionary.png");
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(749, 520);
      this.Controls.Add(this.kryptonPanel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "External Drag To Docking";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
      this.kryptonPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Docking.KryptonDockingManager kryptonDockingManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace;
        private System.Windows.Forms.ImageList imageListSmall;
    }
}

