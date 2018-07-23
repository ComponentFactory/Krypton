namespace CustomControlUsingRenderers
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
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxCustomControl = new System.Windows.Forms.GroupBox();
            this.radioRight = new System.Windows.Forms.RadioButton();
            this.radioLeft = new System.Windows.Forms.RadioButton();
            this.radioBottom = new System.Windows.Forms.RadioButton();
            this.radioTop = new System.Windows.Forms.RadioButton();
            this.myUserControl1 = new CustomControlUsingRenderers.MyUserControl();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBoxPalettes = new System.Windows.Forms.GroupBox();
            this.buttonOffice2010Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonOffice2007Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonCustom = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonSparkle = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonSystem = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCheckSet = new ComponentFactory.Krypton.Toolkit.KryptonCheckSet(this.components);
            this.kryptonPaletteCustom = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.groupBoxDescription.SuspendLayout();
            this.groupBoxCustomControl.SuspendLayout();
            this.groupBoxPalettes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDescription.Controls.Add(this.textBox1);
            this.groupBoxDescription.Location = new System.Drawing.Point(12, 267);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxDescription.Size = new System.Drawing.Size(430, 156);
            this.groupBoxDescription.TabIndex = 2;
            this.groupBoxDescription.TabStop = false;
            this.groupBoxDescription.Text = "Description";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(5, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(420, 132);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // groupBoxCustomControl
            // 
            this.groupBoxCustomControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCustomControl.Controls.Add(this.radioRight);
            this.groupBoxCustomControl.Controls.Add(this.radioLeft);
            this.groupBoxCustomControl.Controls.Add(this.radioBottom);
            this.groupBoxCustomControl.Controls.Add(this.radioTop);
            this.groupBoxCustomControl.Controls.Add(this.myUserControl1);
            this.groupBoxCustomControl.Controls.Add(this.checkBoxEnabled);
            this.groupBoxCustomControl.Location = new System.Drawing.Point(176, 13);
            this.groupBoxCustomControl.Name = "groupBoxCustomControl";
            this.groupBoxCustomControl.Size = new System.Drawing.Size(266, 247);
            this.groupBoxCustomControl.TabIndex = 1;
            this.groupBoxCustomControl.TabStop = false;
            this.groupBoxCustomControl.Text = "MyCustomControl Instance";
            // 
            // radioRight
            // 
            this.radioRight.AutoSize = true;
            this.radioRight.Location = new System.Drawing.Point(15, 100);
            this.radioRight.Name = "radioRight";
            this.radioRight.Size = new System.Drawing.Size(50, 17);
            this.radioRight.TabIndex = 3;
            this.radioRight.Text = "Right";
            this.radioRight.UseVisualStyleBackColor = true;
            this.radioRight.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioLeft
            // 
            this.radioLeft.AutoSize = true;
            this.radioLeft.Location = new System.Drawing.Point(15, 77);
            this.radioLeft.Name = "radioLeft";
            this.radioLeft.Size = new System.Drawing.Size(44, 17);
            this.radioLeft.TabIndex = 2;
            this.radioLeft.Text = "Left";
            this.radioLeft.UseVisualStyleBackColor = true;
            this.radioLeft.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioBottom
            // 
            this.radioBottom.AutoSize = true;
            this.radioBottom.Location = new System.Drawing.Point(15, 54);
            this.radioBottom.Name = "radioBottom";
            this.radioBottom.Size = new System.Drawing.Size(59, 17);
            this.radioBottom.TabIndex = 1;
            this.radioBottom.Text = "Bottom";
            this.radioBottom.UseVisualStyleBackColor = true;
            this.radioBottom.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioTop
            // 
            this.radioTop.AutoSize = true;
            this.radioTop.Checked = true;
            this.radioTop.Location = new System.Drawing.Point(15, 31);
            this.radioTop.Name = "radioTop";
            this.radioTop.Size = new System.Drawing.Size(43, 17);
            this.radioTop.TabIndex = 0;
            this.radioTop.TabStop = true;
            this.radioTop.Text = "Top";
            this.radioTop.UseVisualStyleBackColor = true;
            this.radioTop.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // myUserControl1
            // 
            this.myUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.myUserControl1.Location = new System.Drawing.Point(86, 31);
            this.myUserControl1.Name = "myUserControl1";
            this.myUserControl1.Orientation = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top;
            this.myUserControl1.Size = new System.Drawing.Size(161, 200);
            this.myUserControl1.TabIndex = 5;
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Checked = true;
            this.checkBoxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnabled.Location = new System.Drawing.Point(15, 214);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(64, 17);
            this.checkBoxEnabled.TabIndex = 4;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabled_CheckedChanged);
            // 
            // groupBoxPalettes
            // 
            this.groupBoxPalettes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2010Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2007Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonCustom);
            this.groupBoxPalettes.Controls.Add(this.buttonSparkle);
            this.groupBoxPalettes.Controls.Add(this.buttonSystem);
            this.groupBoxPalettes.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPalettes.Name = "groupBoxPalettes";
            this.groupBoxPalettes.Size = new System.Drawing.Size(158, 248);
            this.groupBoxPalettes.TabIndex = 0;
            this.groupBoxPalettes.TabStop = false;
            this.groupBoxPalettes.Text = "Palettes";
            // 
            // buttonOffice2010Blue
            // 
            this.buttonOffice2010Blue.AutoSize = true;
            this.buttonOffice2010Blue.Checked = true;
            this.buttonOffice2010Blue.Location = new System.Drawing.Point(14, 32);
            this.buttonOffice2010Blue.Name = "buttonOffice2010Blue";
            this.buttonOffice2010Blue.Size = new System.Drawing.Size(127, 27);
            this.buttonOffice2010Blue.TabIndex = 0;
            this.buttonOffice2010Blue.Values.Text = "Office 2010 - Blue";
            // 
            // buttonOffice2007Blue
            // 
            this.buttonOffice2007Blue.AutoSize = true;
            this.buttonOffice2007Blue.Location = new System.Drawing.Point(14, 65);
            this.buttonOffice2007Blue.Name = "buttonOffice2007Blue";
            this.buttonOffice2007Blue.Size = new System.Drawing.Size(127, 27);
            this.buttonOffice2007Blue.TabIndex = 1;
            this.buttonOffice2007Blue.Values.Text = "Office 2007 - Blue";
            // 
            // buttonCustom
            // 
            this.buttonCustom.AutoSize = true;
            this.buttonCustom.Location = new System.Drawing.Point(14, 164);
            this.buttonCustom.Name = "buttonCustom";
            this.buttonCustom.Size = new System.Drawing.Size(127, 27);
            this.buttonCustom.TabIndex = 4;
            this.buttonCustom.Values.Text = "Custom";
            // 
            // buttonSparkle
            // 
            this.buttonSparkle.AutoSize = true;
            this.buttonSparkle.Location = new System.Drawing.Point(14, 98);
            this.buttonSparkle.Name = "buttonSparkle";
            this.buttonSparkle.Size = new System.Drawing.Size(127, 27);
            this.buttonSparkle.TabIndex = 2;
            this.buttonSparkle.Values.Text = "Sparkle - Blue";
            // 
            // buttonSystem
            // 
            this.buttonSystem.AutoSize = true;
            this.buttonSystem.Location = new System.Drawing.Point(14, 131);
            this.buttonSystem.Name = "buttonSystem";
            this.buttonSystem.Size = new System.Drawing.Size(127, 27);
            this.buttonSystem.TabIndex = 3;
            this.buttonSystem.Values.Text = "System";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(367, 429);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // kryptonCheckSet
            // 
            this.kryptonCheckSet.CheckButtons.Add(this.buttonSparkle);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonSystem);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonCustom);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonOffice2010Blue);
            this.kryptonCheckSet.CheckButtons.Add(this.buttonOffice2007Blue);
            this.kryptonCheckSet.CheckedButton = this.buttonOffice2010Blue;
            this.kryptonCheckSet.CheckedButtonChanged += new System.EventHandler(this.kryptonCheckSet_CheckedButtonChanged);
            // 
            // kryptonPaletteCustom
            // 
            this.kryptonPaletteCustom.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.HalfCut;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.Color1 = System.Drawing.Color.Blue;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.Rounding = 12;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Border.Width = 3;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Border.Color1 = System.Drawing.Color.Gray;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = System.Drawing.Color.Red;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateDisabled.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateDisabled.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.Color1 = System.Drawing.Color.WhiteSmoke;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.Color2 = System.Drawing.Color.Lavender;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.ColorAngle = 45F;
            this.kryptonPaletteCustom.HeaderStyles.HeaderPrimary.StateNormal.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 458);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxDescription);
            this.Controls.Add(this.groupBoxCustomControl);
            this.Controls.Add(this.groupBoxPalettes);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(462, 492);
            this.Name = "Form1";
            this.Text = "Custom Control using Renderers";
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.groupBoxCustomControl.ResumeLayout(false);
            this.groupBoxCustomControl.PerformLayout();
            this.groupBoxPalettes.ResumeLayout(false);
            this.groupBoxPalettes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDescription;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBoxCustomControl;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.GroupBox groupBoxPalettes;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckSet kryptonCheckSet;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonSystem;
        private System.Windows.Forms.Button buttonClose;
        private MyUserControl myUserControl1;
        private System.Windows.Forms.RadioButton radioRight;
        private System.Windows.Forms.RadioButton radioLeft;
        private System.Windows.Forms.RadioButton radioBottom;
        private System.Windows.Forms.RadioButton radioTop;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonOffice2010Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonOffice2007Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteCustom;
    }
}

