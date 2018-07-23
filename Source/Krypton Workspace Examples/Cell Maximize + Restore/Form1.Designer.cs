namespace CellMaximizeAndRestore
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
            this.kryptonWorkspace = new ComponentFactory.Krypton.Workspace.KryptonWorkspace();
            this.kryptonWorkspaceCell1 = new ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.kryptonWorkspaceSequence1 = new ComponentFactory.Krypton.Workspace.KryptonWorkspaceSequence();
            this.kryptonWorkspaceCell2 = new ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.kryptonPage4 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.kryptonWorkspaceCell3 = new ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell();
            this.kryptonPage5 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.kryptonPage6 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).BeginInit();
            this.kryptonWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).BeginInit();
            this.kryptonWorkspaceCell1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            this.kryptonPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell2)).BeginInit();
            this.kryptonWorkspaceCell2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            this.kryptonPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).BeginInit();
            this.kryptonPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell3)).BeginInit();
            this.kryptonWorkspaceCell3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage5)).BeginInit();
            this.kryptonPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage6)).BeginInit();
            this.kryptonPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(547, 476);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 26);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // kryptonWorkspace
            // 
            this.kryptonWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonWorkspace.Location = new System.Drawing.Point(12, 12);
            this.kryptonWorkspace.Name = "kryptonWorkspace";
            // 
            // 
            // 
            this.kryptonWorkspace.Root.Children.AddRange(new System.ComponentModel.Component[] {
            this.kryptonWorkspaceCell1,
            this.kryptonWorkspaceSequence1});
            this.kryptonWorkspace.Root.UniqueName = "4364A9E9DAF34C0D4364A9E9DAF34C0D";
            this.kryptonWorkspace.Size = new System.Drawing.Size(610, 456);
            this.kryptonWorkspace.TabIndex = 0;
            this.kryptonWorkspace.TabStop = true;
            this.kryptonWorkspace.ActiveCellChanged += new System.EventHandler<ComponentFactory.Krypton.Workspace.ActiveCellChangedEventArgs>(this.kryptonWorkspace_ActiveCellChanged);
            this.kryptonWorkspace.WorkspaceCellAdding += new System.EventHandler<ComponentFactory.Krypton.Workspace.WorkspaceCellEventArgs>(this.kryptonWorkspace_WorkspaceCellAdding);
            // 
            // kryptonWorkspaceCell1
            // 
            this.kryptonWorkspaceCell1.AllowPageDrag = true;
            this.kryptonWorkspaceCell1.AllowTabFocus = false;
            this.kryptonWorkspaceCell1.Name = "kryptonWorkspaceCell1";
            this.kryptonWorkspaceCell1.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2});
            this.kryptonWorkspaceCell1.SelectedIndex = 1;
            this.kryptonWorkspaceCell1.UniqueName = "B46823ED744B4A87B46823ED744B4A87";
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.richTextBox1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageSmall")));
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage1.Size = new System.Drawing.Size(300, 423);
            this.kryptonPage1.Text = "Bug List";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "38D886AD20CD402D38D886AD20CD402D";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(5, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(290, 413);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Controls.Add(this.richTextBox2);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageSmall")));
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage2.Size = new System.Drawing.Size(300, 423);
            this.kryptonPage2.Text = "Workstation";
            this.kryptonPage2.ToolTipTitle = "Page ToolTip";
            this.kryptonPage2.UniqueName = "B057555EE9CE421BB057555EE9CE421B";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(5, 5);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(290, 413);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // kryptonWorkspaceSequence1
            // 
            this.kryptonWorkspaceSequence1.Children.AddRange(new System.ComponentModel.Component[] {
            this.kryptonWorkspaceCell2,
            this.kryptonWorkspaceCell3});
            this.kryptonWorkspaceSequence1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonWorkspaceSequence1.UniqueName = "99DF5376A7A6421599DF5376A7A64215";
            // 
            // kryptonWorkspaceCell2
            // 
            this.kryptonWorkspaceCell2.AllowPageDrag = true;
            this.kryptonWorkspaceCell2.AllowTabFocus = false;
            this.kryptonWorkspaceCell2.Name = "kryptonWorkspaceCell2";
            this.kryptonWorkspaceCell2.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage3,
            this.kryptonPage4});
            this.kryptonWorkspaceCell2.SelectedIndex = 1;
            this.kryptonWorkspaceCell2.UniqueName = "9182BBE062034D889182BBE062034D88";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Controls.Add(this.richTextBox4);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageSmall")));
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage3.Size = new System.Drawing.Size(301, 192);
            this.kryptonPage3.Text = "Server List";
            this.kryptonPage3.ToolTipTitle = "Page ToolTip";
            this.kryptonPage3.UniqueName = "05A80B272D8C411705A80B272D8C4117";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox4.Location = new System.Drawing.Point(5, 5);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(291, 182);
            this.richTextBox4.TabIndex = 1;
            this.richTextBox4.Text = "";
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage4.Controls.Add(this.richTextBox3);
            this.kryptonPage4.Flags = 65534;
            this.kryptonPage4.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageSmall")));
            this.kryptonPage4.LastVisibleSet = true;
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage4.Size = new System.Drawing.Size(301, 192);
            this.kryptonPage4.Text = "Planets";
            this.kryptonPage4.ToolTipTitle = "Page ToolTip";
            this.kryptonPage4.UniqueName = "0A2FA4EB0679438E0A2FA4EB0679438E";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(5, 5);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(291, 182);
            this.richTextBox3.TabIndex = 1;
            this.richTextBox3.Text = "";
            // 
            // kryptonWorkspaceCell3
            // 
            this.kryptonWorkspaceCell3.AllowPageDrag = true;
            this.kryptonWorkspaceCell3.AllowTabFocus = false;
            this.kryptonWorkspaceCell3.Name = "kryptonWorkspaceCell3";
            this.kryptonWorkspaceCell3.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage5,
            this.kryptonPage6});
            this.kryptonWorkspaceCell3.SelectedIndex = 0;
            this.kryptonWorkspaceCell3.UniqueName = "A69A2BEFC56C49EFA69A2BEFC56C49EF";
            // 
            // kryptonPage5
            // 
            this.kryptonPage5.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage5.Controls.Add(this.richTextBox6);
            this.kryptonPage5.Flags = 65534;
            this.kryptonPage5.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage5.ImageSmall")));
            this.kryptonPage5.LastVisibleSet = true;
            this.kryptonPage5.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage5.Name = "kryptonPage5";
            this.kryptonPage5.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage5.Size = new System.Drawing.Size(301, 193);
            this.kryptonPage5.Text = "Dance Steps";
            this.kryptonPage5.ToolTipTitle = "Page ToolTip";
            this.kryptonPage5.UniqueName = "5F44CFE539EF46555F44CFE539EF4655";
            // 
            // richTextBox6
            // 
            this.richTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox6.Location = new System.Drawing.Point(5, 5);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(291, 183);
            this.richTextBox6.TabIndex = 1;
            this.richTextBox6.Text = "";
            // 
            // kryptonPage6
            // 
            this.kryptonPage6.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage6.Controls.Add(this.richTextBox5);
            this.kryptonPage6.Flags = 65534;
            this.kryptonPage6.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage6.ImageSmall")));
            this.kryptonPage6.LastVisibleSet = true;
            this.kryptonPage6.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage6.Name = "kryptonPage6";
            this.kryptonPage6.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonPage6.Size = new System.Drawing.Size(301, 193);
            this.kryptonPage6.Text = "Music List";
            this.kryptonPage6.ToolTipTitle = "Page ToolTip";
            this.kryptonPage6.UniqueName = "20C9A0DF02044CE120C9A0DF02044CE1";
            // 
            // richTextBox5
            // 
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox5.Location = new System.Drawing.Point(5, 5);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(291, 183);
            this.richTextBox5.TabIndex = 1;
            this.richTextBox5.Text = "";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Restore.bmp");
            this.imageList.Images.SetKeyName(1, "Maximize.bmp");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 514);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.kryptonWorkspace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "Form1";
            this.Text = "Cell Maximize + Restore";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).EndInit();
            this.kryptonWorkspace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell1)).EndInit();
            this.kryptonWorkspaceCell1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            this.kryptonPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell2)).EndInit();
            this.kryptonWorkspaceCell2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            this.kryptonPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).EndInit();
            this.kryptonPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspaceCell3)).EndInit();
            this.kryptonWorkspaceCell3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage5)).EndInit();
            this.kryptonPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage6)).EndInit();
            this.kryptonPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspace kryptonWorkspace;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspaceSequence kryptonWorkspaceSequence1;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell2;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage4;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspaceCell kryptonWorkspaceCell3;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage5;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage6;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.ImageList imageList;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

