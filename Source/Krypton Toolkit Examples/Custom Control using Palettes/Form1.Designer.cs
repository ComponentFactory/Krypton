namespace CustomControlUsingPalettes
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
            this.buttonSparkle = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonSystem = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonCheckSet = new ComponentFactory.Krypton.Toolkit.KryptonCheckSet(this.components);
            this.buttonCustom = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonOffice2010Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonOffice2007Blue = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.groupBoxPalettes = new System.Windows.Forms.GroupBox();
            this.groupBoxCustomControl = new System.Windows.Forms.GroupBox();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.myUserControl1 = new CustomControlUsingPalettes.MyUserControl();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonPaletteCustom = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).BeginInit();
            this.groupBoxPalettes.SuspendLayout();
            this.groupBoxCustomControl.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // buttonSparkle
            // 
            this.buttonSparkle.AutoSize = true;
            this.buttonSparkle.Location = new System.Drawing.Point(20, 98);
            this.buttonSparkle.Name = "buttonSparkle";
            this.buttonSparkle.Size = new System.Drawing.Size(127, 27);
            this.buttonSparkle.TabIndex = 2;
            this.buttonSparkle.Values.Text = "Sparkle - Blue";
            // 
            // buttonSystem
            // 
            this.buttonSystem.AutoSize = true;
            this.buttonSystem.Location = new System.Drawing.Point(20, 131);
            this.buttonSystem.Name = "buttonSystem";
            this.buttonSystem.Size = new System.Drawing.Size(127, 27);
            this.buttonSystem.TabIndex = 3;
            this.buttonSystem.Values.Text = "System";
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
            // buttonCustom
            // 
            this.buttonCustom.AutoSize = true;
            this.buttonCustom.Location = new System.Drawing.Point(20, 164);
            this.buttonCustom.Name = "buttonCustom";
            this.buttonCustom.Size = new System.Drawing.Size(127, 27);
            this.buttonCustom.TabIndex = 4;
            this.buttonCustom.Values.Text = "Custom";
            // 
            // buttonOffice2010Blue
            // 
            this.buttonOffice2010Blue.AutoSize = true;
            this.buttonOffice2010Blue.Checked = true;
            this.buttonOffice2010Blue.Location = new System.Drawing.Point(20, 32);
            this.buttonOffice2010Blue.Name = "buttonOffice2010Blue";
            this.buttonOffice2010Blue.Size = new System.Drawing.Size(127, 27);
            this.buttonOffice2010Blue.TabIndex = 0;
            this.buttonOffice2010Blue.Values.Text = "Office 2010 - Blue";
            // 
            // buttonOffice2007Blue
            // 
            this.buttonOffice2007Blue.AutoSize = true;
            this.buttonOffice2007Blue.Location = new System.Drawing.Point(20, 65);
            this.buttonOffice2007Blue.Name = "buttonOffice2007Blue";
            this.buttonOffice2007Blue.Size = new System.Drawing.Size(127, 27);
            this.buttonOffice2007Blue.TabIndex = 1;
            this.buttonOffice2007Blue.Values.Text = "Office 2007 - Blue";
            // 
            // groupBoxPalettes
            // 
            this.groupBoxPalettes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2010Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonSystem);
            this.groupBoxPalettes.Controls.Add(this.buttonOffice2007Blue);
            this.groupBoxPalettes.Controls.Add(this.buttonCustom);
            this.groupBoxPalettes.Controls.Add(this.buttonSparkle);
            this.groupBoxPalettes.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPalettes.Name = "groupBoxPalettes";
            this.groupBoxPalettes.Size = new System.Drawing.Size(170, 222);
            this.groupBoxPalettes.TabIndex = 0;
            this.groupBoxPalettes.TabStop = false;
            this.groupBoxPalettes.Text = "Palettes";
            // 
            // groupBoxCustomControl
            // 
            this.groupBoxCustomControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCustomControl.Controls.Add(this.checkBoxEnabled);
            this.groupBoxCustomControl.Controls.Add(this.myUserControl1);
            this.groupBoxCustomControl.Location = new System.Drawing.Point(188, 13);
            this.groupBoxCustomControl.Name = "groupBoxCustomControl";
            this.groupBoxCustomControl.Size = new System.Drawing.Size(262, 221);
            this.groupBoxCustomControl.TabIndex = 1;
            this.groupBoxCustomControl.TabStop = false;
            this.groupBoxCustomControl.Text = "MyCustomControl Instance";
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Checked = true;
            this.checkBoxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnabled.Location = new System.Drawing.Point(15, 190);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(64, 17);
            this.checkBoxEnabled.TabIndex = 1;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabled_CheckedChanged);
            // 
            // myUserControl1
            // 
            this.myUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.myUserControl1.Location = new System.Drawing.Point(15, 31);
            this.myUserControl1.Name = "myUserControl1";
            this.myUserControl1.Size = new System.Drawing.Size(228, 148);
            this.myUserControl1.TabIndex = 0;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDescription.Controls.Add(this.textBox1);
            this.groupBoxDescription.Location = new System.Drawing.Point(12, 241);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxDescription.Size = new System.Drawing.Size(438, 156);
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
            this.textBox1.Size = new System.Drawing.Size(428, 132);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(375, 403);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // kryptonPaletteCustom
            // 
            this.kryptonPaletteCustom.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Back.Color2 = System.Drawing.Color.Fuchsia;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedNormal.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Back.Color2 = System.Drawing.Color.Fuchsia;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedPressed.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Back.Color2 = System.Drawing.Color.Fuchsia;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCheckedTracking.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateCommon.Back.ColorAngle = 60F;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color1 = System.Drawing.Color.Gray;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Back.Color2 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Border.Color1 = System.Drawing.Color.Black;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateNormal.Back.Color2 = System.Drawing.Color.Yellow;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateNormal.Border.Color1 = System.Drawing.Color.Olive;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Back.Color2 = System.Drawing.Color.Red;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
                        | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPaletteCustom.ButtonStyles.ButtonStandalone.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Berlin Sans FB", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonPaletteCustom.PanelStyles.PanelAlternate.StateDisabled.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.PanelStyles.PanelAlternate.StateDisabled.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.kryptonPaletteCustom.PanelStyles.PanelAlternate.StateNormal.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.kryptonPaletteCustom.PanelStyles.PanelAlternate.StateNormal.Color2 = System.Drawing.Color.GreenYellow;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 432);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxDescription);
            this.Controls.Add(this.groupBoxCustomControl);
            this.Controls.Add(this.groupBoxPalettes);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(432, 466);
            this.Name = "Form1";
            this.Text = "Custom Control using Palettes";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonCheckSet)).EndInit();
            this.groupBoxPalettes.ResumeLayout(false);
            this.groupBoxPalettes.PerformLayout();
            this.groupBoxCustomControl.ResumeLayout(false);
            this.groupBoxCustomControl.PerformLayout();
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyUserControl myUserControl1;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonSparkle;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonSystem;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckSet kryptonCheckSet;
        private System.Windows.Forms.GroupBox groupBoxPalettes;
        private System.Windows.Forms.GroupBox groupBoxCustomControl;
        private System.Windows.Forms.GroupBox groupBoxDescription;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteCustom;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonCustom;
        private System.Windows.Forms.TextBox textBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonOffice2010Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonOffice2007Blue;
    }
}

