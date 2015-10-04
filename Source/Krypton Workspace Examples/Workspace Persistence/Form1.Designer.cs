namespace WorkspacePersistence
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
            this.buttonClearPages = new System.Windows.Forms.Button();
            this.buttonAddPage = new System.Windows.Forms.Button();
            this.kryptonWorkspace = new ComponentFactory.Krypton.Workspace.KryptonWorkspace();
            this.buttonClose = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bLoadFromArray = new System.Windows.Forms.Button();
            this.bSaveToArray = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bLoadFromFile = new System.Windows.Forms.Button();
            this.bSaveToFile = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClearPages
            // 
            this.buttonClearPages.Location = new System.Drawing.Point(389, 12);
            this.buttonClearPages.Name = "buttonClearPages";
            this.buttonClearPages.Size = new System.Drawing.Size(75, 26);
            this.buttonClearPages.TabIndex = 3;
            this.buttonClearPages.Text = "Clear Pages";
            this.buttonClearPages.UseVisualStyleBackColor = true;
            this.buttonClearPages.Click += new System.EventHandler(this.buttonClearPages_Click);
            // 
            // buttonAddPage
            // 
            this.buttonAddPage.Location = new System.Drawing.Point(308, 12);
            this.buttonAddPage.Name = "buttonAddPage";
            this.buttonAddPage.Size = new System.Drawing.Size(75, 26);
            this.buttonAddPage.TabIndex = 2;
            this.buttonAddPage.Text = "Add Page";
            this.buttonAddPage.UseVisualStyleBackColor = true;
            this.buttonAddPage.Click += new System.EventHandler(this.buttonAddPage_Click);
            // 
            // kryptonWorkspace
            // 
            this.kryptonWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonWorkspace.Location = new System.Drawing.Point(308, 48);
            this.kryptonWorkspace.Name = "kryptonWorkspace";
            // 
            // 
            // 
            this.kryptonWorkspace.Root.UniqueName = "BF526855C8FB4541BF526855C8FB4541";
            this.kryptonWorkspace.Root.WorkspaceControl = this.kryptonWorkspace;
            this.kryptonWorkspace.Size = new System.Drawing.Size(464, 404);
            this.kryptonWorkspace.TabIndex = 4;
            this.kryptonWorkspace.TabStop = true;
            this.kryptonWorkspace.PagesUnmatched += new System.EventHandler<ComponentFactory.Krypton.Workspace.PagesUnmatchedEventArgs>(this.kryptonWorkspace_PagesUnmatched);
            this.kryptonWorkspace.PageSaving += new System.EventHandler<ComponentFactory.Krypton.Workspace.PageSavingEventArgs>(this.kryptonWorkspace_PageSaving);
            this.kryptonWorkspace.PageLoading += new System.EventHandler<ComponentFactory.Krypton.Workspace.PageLoadingEventArgs>(this.kryptonWorkspace_PageLoading);
            this.kryptonWorkspace.RecreateLoadingPage += new System.EventHandler<ComponentFactory.Krypton.Workspace.RecreateLoadingPageEventArgs>(this.kryptonWorkspace_RecreateLoadingPage);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(697, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 26);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "flag_bulgaria.png");
            this.imageList.Images.SetKeyName(1, "flag_china.png");
            this.imageList.Images.SetKeyName(2, "flag_dominica.png");
            this.imageList.Images.SetKeyName(3, "flag_equatorial_guinea.png");
            this.imageList.Images.SetKeyName(4, "flag_falkland_islands.png");
            this.imageList.Images.SetKeyName(5, "flag_kenya.png");
            this.imageList.Images.SetKeyName(6, "flag_kyrgyzstan.png");
            this.imageList.Images.SetKeyName(7, "flag_malaysia.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bLoadFromArray);
            this.groupBox1.Controls.Add(this.bSaveToArray);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Persist to Array of Bytes";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 99);
            this.label1.TabIndex = 2;
            this.label1.Text = "Persisting to/from a byte array is useful when you only need to keep data for the" +
                " duration of your application. \r\n\r\nAlso great for persisting to and from a datab" +
                "ase for use between user sessions.";
            // 
            // bLoadFromArray
            // 
            this.bLoadFromArray.Enabled = false;
            this.bLoadFromArray.Location = new System.Drawing.Point(141, 29);
            this.bLoadFromArray.Name = "bLoadFromArray";
            this.bLoadFromArray.Size = new System.Drawing.Size(112, 34);
            this.bLoadFromArray.TabIndex = 1;
            this.bLoadFromArray.Text = "Load from Array";
            this.bLoadFromArray.UseVisualStyleBackColor = true;
            this.bLoadFromArray.Click += new System.EventHandler(this.bLoadFromArray_Click);
            // 
            // bSaveToArray
            // 
            this.bSaveToArray.Location = new System.Drawing.Point(23, 29);
            this.bSaveToArray.Name = "bSaveToArray";
            this.bSaveToArray.Size = new System.Drawing.Size(112, 34);
            this.bSaveToArray.TabIndex = 0;
            this.bSaveToArray.Text = "Save to Array";
            this.bSaveToArray.UseVisualStyleBackColor = true;
            this.bSaveToArray.Click += new System.EventHandler(this.bSaveToArray_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.bLoadFromFile);
            this.groupBox2.Controls.Add(this.bSaveToFile);
            this.groupBox2.Location = new System.Drawing.Point(13, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 143);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Persist to File (XML)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 63);
            this.label2.TabIndex = 2;
            this.label2.Text = "Using files for storage allows you to persist the layout when you application exi" +
                "ts in order to restore it at the next startup.";
            // 
            // bLoadFromFile
            // 
            this.bLoadFromFile.Location = new System.Drawing.Point(142, 26);
            this.bLoadFromFile.Name = "bLoadFromFile";
            this.bLoadFromFile.Size = new System.Drawing.Size(112, 34);
            this.bLoadFromFile.TabIndex = 1;
            this.bLoadFromFile.Text = "Load from File";
            this.bLoadFromFile.UseVisualStyleBackColor = true;
            this.bLoadFromFile.Click += new System.EventHandler(this.bLoadFromFile_Click);
            // 
            // bSaveToFile
            // 
            this.bSaveToFile.Location = new System.Drawing.Point(24, 26);
            this.bSaveToFile.Name = "bSaveToFile";
            this.bSaveToFile.Size = new System.Drawing.Size(112, 34);
            this.bSaveToFile.TabIndex = 0;
            this.bSaveToFile.Text = "Save to File";
            this.bSaveToFile.UseVisualStyleBackColor = true;
            this.bSaveToFile.Click += new System.EventHandler(this.bSaveToFile_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FileName = "example.xml";
            this.saveFileDialog.Filter = "XML files|*.xml";
            this.saveFileDialog.Title = "Save Workspace Layout";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "example.xml";
            this.openFileDialog.Filter = "XML files|*.xml";
            this.openFileDialog.Title = "Load Workspace Layout";
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 464);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonClearPages);
            this.Controls.Add(this.buttonAddPage);
            this.Controls.Add(this.kryptonWorkspace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(608, 432);
            this.Name = "Form1";
            this.Text = "Workspace Persistence";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClearPages;
        private System.Windows.Forms.Button buttonAddPage;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspace kryptonWorkspace;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bLoadFromArray;
        private System.Windows.Forms.Button bSaveToArray;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bLoadFromFile;
        private System.Windows.Forms.Button bSaveToFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

