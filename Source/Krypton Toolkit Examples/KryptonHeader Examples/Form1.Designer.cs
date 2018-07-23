namespace KryptonHeaderExamples
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
            this.office6 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.buttonSpecUp = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecDown = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.office5 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.office4 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.office2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.office1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.office3 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.blue6 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.blue5 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.blue4 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.blue2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.blue1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.blue3 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.custom3 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.custom1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.custom2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid);
            this.groupBox4.Location = new System.Drawing.Point(306, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(322, 616);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties for Selected KryptonHeader";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(310, 591);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(553, 634);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.office6);
            this.groupBox1.Controls.Add(this.office5);
            this.groupBox1.Controls.Add(this.office4);
            this.groupBox1.Controls.Add(this.office2);
            this.groupBox1.Controls.Add(this.office1);
            this.groupBox1.Controls.Add(this.office3);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sparkle - Blue";
            // 
            // office6
            // 
            this.office6.AllowButtonSpecToolTips = true;
            this.office6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office6.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecUp,
            this.buttonSpecDown});
            this.office6.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.office6.Location = new System.Drawing.Point(98, 155);
            this.office6.Name = "office6";
            this.office6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office6.Size = new System.Drawing.Size(176, 34);
            this.office6.TabIndex = 5;
            this.office6.Values.Description = "";
            this.office6.Values.Heading = "Sort";
            this.office6.Values.Image = null;
            this.office6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // buttonSpecUp
            // 
            this.buttonSpecUp.Checked = ComponentFactory.Krypton.Toolkit.ButtonCheckState.Checked;
            this.buttonSpecUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonSpecUp.Image")));
            this.buttonSpecUp.ToolTipTitle = "Sort in Descending Order";
            this.buttonSpecUp.UniqueName = "406C13856FEF4CA3406C13856FEF4CA3";
            // 
            // buttonSpecDown
            // 
            this.buttonSpecDown.Checked = ComponentFactory.Krypton.Toolkit.ButtonCheckState.Unchecked;
            this.buttonSpecDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonSpecDown.Image")));
            this.buttonSpecDown.ToolTipTitle = "Sort in Ascending Order";
            this.buttonSpecDown.UniqueName = "BE9A34366DE6465CBE9A34366DE6465C";
            // 
            // office5
            // 
            this.office5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office5.Location = new System.Drawing.Point(98, 121);
            this.office5.Name = "office5";
            this.office5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office5.Size = new System.Drawing.Size(176, 33);
            this.office5.TabIndex = 4;
            this.office5.Values.Description = "";
            this.office5.Values.Heading = "Buttons";
            this.office5.Values.Image = null;
            this.office5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // office4
            // 
            this.office4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office4.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.office4.Location = new System.Drawing.Point(98, 57);
            this.office4.Name = "office4";
            this.office4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office4.Size = new System.Drawing.Size(176, 24);
            this.office4.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.office4.TabIndex = 3;
            this.office4.Values.Description = "";
            this.office4.Values.Heading = "Secondary";
            this.office4.Values.Image = ((System.Drawing.Image)(resources.GetObject("office4.Values.Image")));
            this.office4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // office2
            // 
            this.office2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office2.Location = new System.Drawing.Point(50, 23);
            this.office2.Name = "office2";
            this.office2.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.office2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office2.Size = new System.Drawing.Size(33, 178);
            this.office2.TabIndex = 1;
            this.office2.Values.Description = "Right";
            this.office2.Values.Heading = "Orientation";
            this.office2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // office1
            // 
            this.office1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office1.Location = new System.Drawing.Point(16, 23);
            this.office1.Name = "office1";
            this.office1.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.office1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office1.Size = new System.Drawing.Size(33, 178);
            this.office1.TabIndex = 0;
            this.office1.Values.Description = "Left";
            this.office1.Values.Heading = "Orientation";
            this.office1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // office3
            // 
            this.office3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.office3.Location = new System.Drawing.Point(98, 23);
            this.office3.Name = "office3";
            this.office3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.office3.Size = new System.Drawing.Size(176, 33);
            this.office3.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.office3.TabIndex = 2;
            this.office3.Values.Description = "";
            this.office3.Values.Heading = "Primary";
            this.office3.Values.Image = ((System.Drawing.Image)(resources.GetObject("office3.Values.Image")));
            this.office3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.blue6);
            this.groupBox2.Controls.Add(this.blue5);
            this.groupBox2.Controls.Add(this.blue4);
            this.groupBox2.Controls.Add(this.blue2);
            this.groupBox2.Controls.Add(this.blue1);
            this.groupBox2.Controls.Add(this.blue3);
            this.groupBox2.Location = new System.Drawing.Point(6, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 219);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Office 2010 - Blue";
            // 
            // blue6
            // 
            this.blue6.AllowButtonSpecToolTips = true;
            this.blue6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue6.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecUp,
            this.buttonSpecDown});
            this.blue6.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.blue6.Location = new System.Drawing.Point(98, 155);
            this.blue6.Name = "blue6";
            this.blue6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue6.Size = new System.Drawing.Size(176, 36);
            this.blue6.TabIndex = 5;
            this.blue6.Values.Description = "";
            this.blue6.Values.Heading = "Sort";
            this.blue6.Values.Image = null;
            this.blue6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // blue5
            // 
            this.blue5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue5.Location = new System.Drawing.Point(98, 117);
            this.blue5.Name = "blue5";
            this.blue5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue5.Size = new System.Drawing.Size(176, 34);
            this.blue5.TabIndex = 4;
            this.blue5.Values.Description = "";
            this.blue5.Values.Heading = "Buttons";
            this.blue5.Values.Image = null;
            this.blue5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // blue4
            // 
            this.blue4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue4.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.blue4.Location = new System.Drawing.Point(98, 61);
            this.blue4.Name = "blue4";
            this.blue4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue4.Size = new System.Drawing.Size(176, 25);
            this.blue4.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.blue4.TabIndex = 3;
            this.blue4.Values.Description = "";
            this.blue4.Values.Heading = "Secondary";
            this.blue4.Values.Image = ((System.Drawing.Image)(resources.GetObject("blue4.Values.Image")));
            this.blue4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // blue2
            // 
            this.blue2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue2.Location = new System.Drawing.Point(53, 23);
            this.blue2.Name = "blue2";
            this.blue2.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right;
            this.blue2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue2.Size = new System.Drawing.Size(34, 181);
            this.blue2.TabIndex = 1;
            this.blue2.Values.Description = "Right";
            this.blue2.Values.Heading = "Orientation";
            this.blue2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // blue1
            // 
            this.blue1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue1.Location = new System.Drawing.Point(16, 23);
            this.blue1.Name = "blue1";
            this.blue1.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Left;
            this.blue1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue1.Size = new System.Drawing.Size(34, 181);
            this.blue1.TabIndex = 0;
            this.blue1.Values.Description = "Left";
            this.blue1.Values.Heading = "Orientation";
            this.blue1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // blue3
            // 
            this.blue3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.blue3.Location = new System.Drawing.Point(98, 23);
            this.blue3.Name = "blue3";
            this.blue3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.blue3.Size = new System.Drawing.Size(176, 34);
            this.blue3.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.blue3.TabIndex = 2;
            this.blue3.Values.Description = "";
            this.blue3.Values.Heading = "Primary";
            this.blue3.Values.Image = ((System.Drawing.Image)(resources.GetObject("blue3.Values.Image")));
            this.blue3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.custom3);
            this.groupBox3.Controls.Add(this.custom1);
            this.groupBox3.Controls.Add(this.custom2);
            this.groupBox3.Location = new System.Drawing.Point(6, 457);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 171);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Settings";
            // 
            // custom3
            // 
            this.custom3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.custom3.Location = new System.Drawing.Point(16, 114);
            this.custom3.Name = "custom3";
            this.custom3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.custom3.Size = new System.Drawing.Size(258, 42);
            this.custom3.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.HalfCut;
            this.custom3.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.HalfCut;
            this.custom3.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.custom3.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.custom3.StateCommon.Border.Rounding = 2;
            this.custom3.StateCommon.Border.Width = 2;
            this.custom3.StateCommon.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.custom3.TabIndex = 2;
            this.custom3.Values.Description = "";
            this.custom3.Values.Heading = "Primary";
            this.custom3.Values.Image = ((System.Drawing.Image)(resources.GetObject("custom3.Values.Image")));
            this.custom3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // custom1
            // 
            this.custom1.Location = new System.Drawing.Point(16, 23);
            this.custom1.Name = "custom1";
            this.custom1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.custom1.Size = new System.Drawing.Size(162, 42);
            this.custom1.StateNormal.Back.Image = global::KryptonHeader_Examples.Properties.Resources.pageBg;
            this.custom1.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.custom1.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.custom1.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.custom1.StateNormal.Border.Rounding = 15;
            this.custom1.StateNormal.Border.Width = 2;
            this.custom1.StateNormal.Content.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.custom1.TabIndex = 0;
            this.custom1.Values.Description = "";
            this.custom1.Values.Heading = "Image Effect";
            this.custom1.Values.Image = ((System.Drawing.Image)(resources.GetObject("custom1.Values.Image")));
            this.custom1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // custom2
            // 
            this.custom2.Location = new System.Drawing.Point(16, 72);
            this.custom2.Name = "custom2";
            this.custom2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.custom2.Size = new System.Drawing.Size(127, 38);
            this.custom2.StateNormal.Back.Color1 = System.Drawing.SystemColors.InactiveCaptionText;
            this.custom2.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.custom2.StateNormal.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.custom2.StateNormal.Border.Color1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.custom2.StateNormal.Border.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.custom2.StateNormal.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.custom2.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.custom2.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.custom2.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.custom2.StateNormal.Border.Rounding = 7;
            this.custom2.StateNormal.Border.Width = 3;
            this.custom2.StateNormal.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.custom2.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.custom2.TabIndex = 1;
            this.custom2.Values.Description = "";
            this.custom2.Values.Heading = "Calendar";
            this.custom2.Values.Image = ((System.Drawing.Image)(resources.GetObject("custom2.Values.Image")));
            this.custom2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 669);
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
            this.Text = "KryptonHeader Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office3;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader custom2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader custom1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office4;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office5;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader office6;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecUp;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecDown;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue6;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue5;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue4;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue2;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader blue3;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader custom3;
    }
}

