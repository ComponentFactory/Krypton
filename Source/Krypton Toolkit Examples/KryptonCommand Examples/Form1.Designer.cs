namespace KryptonCommandExamples
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
            this.groupBoxProperties = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.kryptonCommand = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.kryptonDropButton1 = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonColorButton1 = new ComponentFactory.Krypton.Toolkit.KryptonColorButton();
            this.kryptonLinkLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonCheckBox1 = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonCheckButton1 = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.buttonAttach = new System.Windows.Forms.Button();
            this.buttonUnattach = new System.Windows.Forms.Button();
            this.groupBoxProperties.SuspendLayout();
            this.groupBoxControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(360, 514);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 26);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxProperties
            // 
            this.groupBoxProperties.Controls.Add(this.propertyGrid);
            this.groupBoxProperties.Location = new System.Drawing.Point(13, 13);
            this.groupBoxProperties.Name = "groupBoxProperties";
            this.groupBoxProperties.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxProperties.Size = new System.Drawing.Size(422, 259);
            this.groupBoxProperties.TabIndex = 3;
            this.groupBoxProperties.TabStop = false;
            this.groupBoxProperties.Text = "Properties for KryptonCommand";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(10, 24);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this.kryptonCommand;
            this.propertyGrid.Size = new System.Drawing.Size(402, 225);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // kryptonCommand
            // 
            this.kryptonCommand.Checked = true;
            this.kryptonCommand.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.kryptonCommand.ExtraText = "Extra";
            this.kryptonCommand.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonCommand.ImageLarge")));
            this.kryptonCommand.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonCommand.ImageSmall")));
            this.kryptonCommand.Text = "Text";
            this.kryptonCommand.TextLine1 = "";
            this.kryptonCommand.TextLine2 = "";
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.kryptonDropButton1);
            this.groupBoxControls.Controls.Add(this.kryptonHeader1);
            this.groupBoxControls.Controls.Add(this.kryptonColorButton1);
            this.groupBoxControls.Controls.Add(this.kryptonLinkLabel1);
            this.groupBoxControls.Controls.Add(this.kryptonLabel1);
            this.groupBoxControls.Controls.Add(this.kryptonCheckBox1);
            this.groupBoxControls.Controls.Add(this.kryptonCheckButton1);
            this.groupBoxControls.Controls.Add(this.kryptonButton1);
            this.groupBoxControls.Location = new System.Drawing.Point(13, 279);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(422, 225);
            this.groupBoxControls.TabIndex = 4;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Controls attached to KryptonCommand";
            // 
            // kryptonDropButton1
            // 
            this.kryptonDropButton1.AutoSize = true;
            this.kryptonDropButton1.Location = new System.Drawing.Point(19, 103);
            this.kryptonDropButton1.Name = "kryptonDropButton1";
            this.kryptonDropButton1.Size = new System.Drawing.Size(136, 25);
            this.kryptonDropButton1.TabIndex = 2;
            this.kryptonDropButton1.Values.Text = "DropButton";
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.kryptonHeader1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
            this.kryptonHeader1.Location = new System.Drawing.Point(19, 165);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(170, 31);
            this.kryptonHeader1.TabIndex = 4;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Header";
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Text = "ButtonSpec";
            this.buttonSpecAny1.UniqueName = "BC4FEDE1ECC34E64BC4FEDE1ECC34E64";
            // 
            // kryptonColorButton1
            // 
            this.kryptonColorButton1.AutoSize = true;
            this.kryptonColorButton1.Location = new System.Drawing.Point(19, 134);
            this.kryptonColorButton1.Name = "kryptonColorButton1";
            this.kryptonColorButton1.Size = new System.Drawing.Size(136, 25);
            this.kryptonColorButton1.TabIndex = 3;
            this.kryptonColorButton1.Values.Text = "ColorButton";
            // 
            // kryptonLinkLabel1
            // 
            this.kryptonLinkLabel1.Location = new System.Drawing.Point(256, 72);
            this.kryptonLinkLabel1.Name = "kryptonLinkLabel1";
            this.kryptonLinkLabel1.Size = new System.Drawing.Size(61, 20);
            this.kryptonLinkLabel1.TabIndex = 6;
            this.kryptonLinkLabel1.Values.Text = "LinkLabel";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(256, 41);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(39, 20);
            this.kryptonLabel1.TabIndex = 5;
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(261, 103);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(76, 20);
            this.kryptonCheckBox1.TabIndex = 7;
            this.kryptonCheckBox1.Values.Text = "CheckBox";
            // 
            // kryptonCheckButton1
            // 
            this.kryptonCheckButton1.AutoSize = true;
            this.kryptonCheckButton1.Location = new System.Drawing.Point(19, 72);
            this.kryptonCheckButton1.Name = "kryptonCheckButton1";
            this.kryptonCheckButton1.Size = new System.Drawing.Size(122, 25);
            this.kryptonCheckButton1.TabIndex = 1;
            this.kryptonCheckButton1.Values.Text = "CheckButton";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.Location = new System.Drawing.Point(19, 41);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(122, 25);
            this.kryptonButton1.TabIndex = 0;
            // 
            // buttonAttach
            // 
            this.buttonAttach.Location = new System.Drawing.Point(13, 514);
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.Size = new System.Drawing.Size(159, 26);
            this.buttonAttach.TabIndex = 0;
            this.buttonAttach.Text = "Attach KryptonCommand";
            this.buttonAttach.UseVisualStyleBackColor = true;
            this.buttonAttach.Click += new System.EventHandler(this.buttonAttach_Click);
            // 
            // buttonUnattach
            // 
            this.buttonUnattach.Location = new System.Drawing.Point(178, 514);
            this.buttonUnattach.Name = "buttonUnattach";
            this.buttonUnattach.Size = new System.Drawing.Size(75, 26);
            this.buttonUnattach.TabIndex = 1;
            this.buttonUnattach.Text = "Unattach";
            this.buttonUnattach.UseVisualStyleBackColor = true;
            this.buttonUnattach.Click += new System.EventHandler(this.buttonUnattach_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonAttach;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(446, 553);
            this.Controls.Add(this.buttonUnattach);
            this.Controls.Add(this.buttonAttach);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.groupBoxProperties);
            this.Controls.Add(this.buttonClose);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonCommand Examples";
            this.groupBoxProperties.ResumeLayout(false);
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel kryptonLinkLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox kryptonCheckBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton kryptonCheckButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand kryptonCommand;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton kryptonDropButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private ComponentFactory.Krypton.Toolkit.KryptonColorButton kryptonColorButton1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.Button buttonUnattach;
    }
}

