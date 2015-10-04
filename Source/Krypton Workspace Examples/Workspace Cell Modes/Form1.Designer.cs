namespace WorkspaceCellModes
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioOutlookMini = new System.Windows.Forms.RadioButton();
            this.radioBarRibbonTabGroup = new System.Windows.Forms.RadioButton();
            this.radioBarTabGroup = new System.Windows.Forms.RadioButton();
            this.radioHeaderBarCheckButtonHeaderGroup = new System.Windows.Forms.RadioButton();
            this.radioHeaderBarCheckButtonGroup = new System.Windows.Forms.RadioButton();
            this.radioStackCheckButtonHeaderGroup = new System.Windows.Forms.RadioButton();
            this.radioStackCheckButtonGroup = new System.Windows.Forms.RadioButton();
            this.radioOutlookFull = new System.Windows.Forms.RadioButton();
            this.radioPanel = new System.Windows.Forms.RadioButton();
            this.radioGroup = new System.Windows.Forms.RadioButton();
            this.radioHeaderGroup = new System.Windows.Forms.RadioButton();
            this.radioBarCheckButtonGroupInside = new System.Windows.Forms.RadioButton();
            this.radioBarCheckButtonGroupOutside = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonWorkspace = new ComponentFactory.Krypton.Workspace.KryptonWorkspace();
            this.buttonAddPage = new System.Windows.Forms.Button();
            this.buttonClearPages = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioOutlookMini);
            this.groupBox1.Controls.Add(this.radioBarRibbonTabGroup);
            this.groupBox1.Controls.Add(this.radioBarTabGroup);
            this.groupBox1.Controls.Add(this.radioHeaderBarCheckButtonHeaderGroup);
            this.groupBox1.Controls.Add(this.radioHeaderBarCheckButtonGroup);
            this.groupBox1.Controls.Add(this.radioStackCheckButtonHeaderGroup);
            this.groupBox1.Controls.Add(this.radioStackCheckButtonGroup);
            this.groupBox1.Controls.Add(this.radioOutlookFull);
            this.groupBox1.Controls.Add(this.radioPanel);
            this.groupBox1.Controls.Add(this.radioGroup);
            this.groupBox1.Controls.Add(this.radioHeaderGroup);
            this.groupBox1.Controls.Add(this.radioBarCheckButtonGroupInside);
            this.groupBox1.Controls.Add(this.radioBarCheckButtonGroupOutside);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 405);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modes";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 57);
            this.label1.TabIndex = 13;
            this.label1.Text = "Note: TabStrip style modes have been excluded from the list as they serve no usef" +
                "ul \r\npurpose within the Workspace control.";
            // 
            // radioOutlookMini
            // 
            this.radioOutlookMini.AutoSize = true;
            this.radioOutlookMini.Location = new System.Drawing.Point(16, 235);
            this.radioOutlookMini.Name = "radioOutlookMini";
            this.radioOutlookMini.Size = new System.Drawing.Size(90, 17);
            this.radioOutlookMini.TabIndex = 9;
            this.radioOutlookMini.Text = "Outlook - Mini";
            this.radioOutlookMini.UseVisualStyleBackColor = true;
            this.radioOutlookMini.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioBarRibbonTabGroup
            // 
            this.radioBarRibbonTabGroup.AutoSize = true;
            this.radioBarRibbonTabGroup.Location = new System.Drawing.Point(16, 51);
            this.radioBarRibbonTabGroup.Name = "radioBarRibbonTabGroup";
            this.radioBarRibbonTabGroup.Size = new System.Drawing.Size(141, 17);
            this.radioBarRibbonTabGroup.TabIndex = 1;
            this.radioBarRibbonTabGroup.Text = "Bar - RibbonTab - Group";
            this.radioBarRibbonTabGroup.UseVisualStyleBackColor = true;
            this.radioBarRibbonTabGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioBarTabGroup
            // 
            this.radioBarTabGroup.AutoSize = true;
            this.radioBarTabGroup.Checked = true;
            this.radioBarTabGroup.Location = new System.Drawing.Point(16, 28);
            this.radioBarTabGroup.Name = "radioBarTabGroup";
            this.radioBarTabGroup.Size = new System.Drawing.Size(108, 17);
            this.radioBarTabGroup.TabIndex = 0;
            this.radioBarTabGroup.TabStop = true;
            this.radioBarTabGroup.Text = "Bar - Tab - Group";
            this.radioBarTabGroup.UseVisualStyleBackColor = true;
            this.radioBarTabGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioHeaderBarCheckButtonHeaderGroup
            // 
            this.radioHeaderBarCheckButtonHeaderGroup.AutoSize = true;
            this.radioHeaderBarCheckButtonHeaderGroup.Location = new System.Drawing.Point(16, 143);
            this.radioHeaderBarCheckButtonHeaderGroup.Name = "radioHeaderBarCheckButtonHeaderGroup";
            this.radioHeaderBarCheckButtonHeaderGroup.Size = new System.Drawing.Size(221, 17);
            this.radioHeaderBarCheckButtonHeaderGroup.TabIndex = 5;
            this.radioHeaderBarCheckButtonHeaderGroup.Text = "HeaderBar - CheckButton - HeaderGroup";
            this.radioHeaderBarCheckButtonHeaderGroup.UseVisualStyleBackColor = true;
            this.radioHeaderBarCheckButtonHeaderGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioHeaderBarCheckButtonGroup
            // 
            this.radioHeaderBarCheckButtonGroup.AutoSize = true;
            this.radioHeaderBarCheckButtonGroup.Location = new System.Drawing.Point(16, 120);
            this.radioHeaderBarCheckButtonGroup.Name = "radioHeaderBarCheckButtonGroup";
            this.radioHeaderBarCheckButtonGroup.Size = new System.Drawing.Size(186, 17);
            this.radioHeaderBarCheckButtonGroup.TabIndex = 4;
            this.radioHeaderBarCheckButtonGroup.Text = "HeaderBar - CheckButton - Group";
            this.radioHeaderBarCheckButtonGroup.UseVisualStyleBackColor = true;
            this.radioHeaderBarCheckButtonGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioStackCheckButtonHeaderGroup
            // 
            this.radioStackCheckButtonHeaderGroup.AutoSize = true;
            this.radioStackCheckButtonHeaderGroup.Location = new System.Drawing.Point(16, 189);
            this.radioStackCheckButtonHeaderGroup.Name = "radioStackCheckButtonHeaderGroup";
            this.radioStackCheckButtonHeaderGroup.Size = new System.Drawing.Size(196, 17);
            this.radioStackCheckButtonHeaderGroup.TabIndex = 7;
            this.radioStackCheckButtonHeaderGroup.Text = "Stack - CheckButton - HeaderGroup";
            this.radioStackCheckButtonHeaderGroup.UseVisualStyleBackColor = true;
            this.radioStackCheckButtonHeaderGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioStackCheckButtonGroup
            // 
            this.radioStackCheckButtonGroup.AutoSize = true;
            this.radioStackCheckButtonGroup.Location = new System.Drawing.Point(16, 166);
            this.radioStackCheckButtonGroup.Name = "radioStackCheckButtonGroup";
            this.radioStackCheckButtonGroup.Size = new System.Drawing.Size(161, 17);
            this.radioStackCheckButtonGroup.TabIndex = 6;
            this.radioStackCheckButtonGroup.Text = "Stack - CheckButton - Group";
            this.radioStackCheckButtonGroup.UseVisualStyleBackColor = true;
            this.radioStackCheckButtonGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioOutlookFull
            // 
            this.radioOutlookFull.AutoSize = true;
            this.radioOutlookFull.Location = new System.Drawing.Point(16, 212);
            this.radioOutlookFull.Name = "radioOutlookFull";
            this.radioOutlookFull.Size = new System.Drawing.Size(88, 17);
            this.radioOutlookFull.TabIndex = 8;
            this.radioOutlookFull.Text = "Outlook - Full";
            this.radioOutlookFull.UseVisualStyleBackColor = true;
            this.radioOutlookFull.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioPanel
            // 
            this.radioPanel.AutoSize = true;
            this.radioPanel.Location = new System.Drawing.Point(16, 304);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.Size = new System.Drawing.Size(51, 17);
            this.radioPanel.TabIndex = 12;
            this.radioPanel.Text = "Panel";
            this.radioPanel.UseVisualStyleBackColor = true;
            this.radioPanel.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioGroup
            // 
            this.radioGroup.AutoSize = true;
            this.radioGroup.Location = new System.Drawing.Point(16, 281);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Size = new System.Drawing.Size(54, 17);
            this.radioGroup.TabIndex = 11;
            this.radioGroup.Text = "Group";
            this.radioGroup.UseVisualStyleBackColor = true;
            this.radioGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioHeaderGroup
            // 
            this.radioHeaderGroup.AutoSize = true;
            this.radioHeaderGroup.Location = new System.Drawing.Point(16, 258);
            this.radioHeaderGroup.Name = "radioHeaderGroup";
            this.radioHeaderGroup.Size = new System.Drawing.Size(89, 17);
            this.radioHeaderGroup.TabIndex = 10;
            this.radioHeaderGroup.Text = "HeaderGroup";
            this.radioHeaderGroup.UseVisualStyleBackColor = true;
            this.radioHeaderGroup.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioBarCheckButtonGroupInside
            // 
            this.radioBarCheckButtonGroupInside.AutoSize = true;
            this.radioBarCheckButtonGroupInside.Location = new System.Drawing.Point(16, 97);
            this.radioBarCheckButtonGroupInside.Name = "radioBarCheckButtonGroupInside";
            this.radioBarCheckButtonGroupInside.Size = new System.Drawing.Size(190, 17);
            this.radioBarCheckButtonGroupInside.TabIndex = 3;
            this.radioBarCheckButtonGroupInside.Text = "Bar - CheckButton - Group - Inside";
            this.radioBarCheckButtonGroupInside.UseVisualStyleBackColor = true;
            this.radioBarCheckButtonGroupInside.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // radioBarCheckButtonGroupOutside
            // 
            this.radioBarCheckButtonGroupOutside.AutoSize = true;
            this.radioBarCheckButtonGroupOutside.Location = new System.Drawing.Point(16, 74);
            this.radioBarCheckButtonGroupOutside.Name = "radioBarCheckButtonGroupOutside";
            this.radioBarCheckButtonGroupOutside.Size = new System.Drawing.Size(198, 17);
            this.radioBarCheckButtonGroupOutside.TabIndex = 2;
            this.radioBarCheckButtonGroupOutside.Text = "Bar - CheckButton - Group - Outside";
            this.radioBarCheckButtonGroupOutside.UseVisualStyleBackColor = true;
            this.radioBarCheckButtonGroupOutside.CheckedChanged += new System.EventHandler(this.radioMode_CheckedChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(687, 21);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 26);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // kryptonWorkspace
            // 
            this.kryptonWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonWorkspace.Location = new System.Drawing.Point(285, 57);
            this.kryptonWorkspace.Name = "kryptonWorkspace";
            // 
            // 
            // 
            this.kryptonWorkspace.Root.UniqueName = "EBB79296DCDD40AAF79B5D0F977EEA9A";
            this.kryptonWorkspace.Size = new System.Drawing.Size(477, 475);
            this.kryptonWorkspace.TabIndex = 4;
            this.kryptonWorkspace.TabStop = true;
            this.kryptonWorkspace.WorkspaceCellAdding += new System.EventHandler<ComponentFactory.Krypton.Workspace.WorkspaceCellEventArgs>(this.kryptonWorkspace_WorkspaceCellAdding);
            // 
            // buttonAddPage
            // 
            this.buttonAddPage.Location = new System.Drawing.Point(285, 21);
            this.buttonAddPage.Name = "buttonAddPage";
            this.buttonAddPage.Size = new System.Drawing.Size(75, 26);
            this.buttonAddPage.TabIndex = 1;
            this.buttonAddPage.Text = "Add Page";
            this.buttonAddPage.UseVisualStyleBackColor = true;
            this.buttonAddPage.Click += new System.EventHandler(this.buttonAddPage_Click);
            // 
            // buttonClearPages
            // 
            this.buttonClearPages.Location = new System.Drawing.Point(366, 21);
            this.buttonClearPages.Name = "buttonClearPages";
            this.buttonClearPages.Size = new System.Drawing.Size(75, 26);
            this.buttonClearPages.TabIndex = 2;
            this.buttonClearPages.Text = "Clear Pages";
            this.buttonClearPages.UseVisualStyleBackColor = true;
            this.buttonClearPages.Click += new System.EventHandler(this.buttonClearPages_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 544);
            this.Controls.Add(this.buttonClearPages);
            this.Controls.Add(this.buttonAddPage);
            this.Controls.Add(this.kryptonWorkspace);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(558, 463);
            this.Name = "Form1";
            this.Text = "Workspace Cell Modes";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioOutlookMini;
        private System.Windows.Forms.RadioButton radioBarRibbonTabGroup;
        private System.Windows.Forms.RadioButton radioBarTabGroup;
        private System.Windows.Forms.RadioButton radioHeaderBarCheckButtonHeaderGroup;
        private System.Windows.Forms.RadioButton radioHeaderBarCheckButtonGroup;
        private System.Windows.Forms.RadioButton radioStackCheckButtonHeaderGroup;
        private System.Windows.Forms.RadioButton radioStackCheckButtonGroup;
        private System.Windows.Forms.RadioButton radioOutlookFull;
        private System.Windows.Forms.RadioButton radioPanel;
        private System.Windows.Forms.RadioButton radioGroup;
        private System.Windows.Forms.RadioButton radioHeaderGroup;
        private System.Windows.Forms.RadioButton radioBarCheckButtonGroupInside;
        private System.Windows.Forms.RadioButton radioBarCheckButtonGroupOutside;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private ComponentFactory.Krypton.Workspace.KryptonWorkspace kryptonWorkspace;
        private System.Windows.Forms.Button buttonAddPage;
        private System.Windows.Forms.Button buttonClearPages;
        private System.Windows.Forms.ImageList imageList;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

