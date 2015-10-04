namespace ButtonSpecPlayground
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
            this.groupBoxProperties = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonButtonAdd = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonRemove = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBoxExample = new System.Windows.Forms.GroupBox();
            this.groupBoxButtonSpecs = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.groupBoxPrimary = new System.Windows.Forms.GroupBox();
            this.kryptonButtonBottomP = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonTopP = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonRightP = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonLeftP = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBoxSecondary = new System.Windows.Forms.GroupBox();
            this.kryptonButtonBottomS = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonTopS = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonRightS = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonLeftS = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBoxProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).BeginInit();
            this.kryptonHeaderGroup1.SuspendLayout();
            this.groupBoxExample.SuspendLayout();
            this.groupBoxButtonSpecs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxPrimary.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxSecondary.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxProperties
            // 
            this.groupBoxProperties.Controls.Add(this.propertyGrid);
            this.groupBoxProperties.Location = new System.Drawing.Point(285, 12);
            this.groupBoxProperties.Name = "groupBoxProperties";
            this.groupBoxProperties.Size = new System.Drawing.Size(294, 436);
            this.groupBoxProperties.TabIndex = 2;
            this.groupBoxProperties.TabStop = false;
            this.groupBoxProperties.Text = "Properties for Selected ButtonSpec";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(282, 411);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(504, 454);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // kryptonHeaderGroup1
            // 
            this.kryptonHeaderGroup1.CollapseTarget = ComponentFactory.Krypton.Toolkit.HeaderGroupCollapsedTarget.CollapsedToPrimary;
            this.kryptonHeaderGroup1.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonHeaderGroup1.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.kryptonHeaderGroup1.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Primary;
            this.kryptonHeaderGroup1.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroup1.Location = new System.Drawing.Point(11, 23);
            this.kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            this.kryptonHeaderGroup1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeaderGroup1.Size = new System.Drawing.Size(246, 165);
            this.kryptonHeaderGroup1.TabIndex = 0;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // kryptonButtonAdd
            // 
            this.kryptonButtonAdd.AutoSize = true;
            this.kryptonButtonAdd.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonAdd.Location = new System.Drawing.Point(10, 25);
            this.kryptonButtonAdd.Name = "kryptonButtonAdd";
            this.kryptonButtonAdd.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonAdd.Size = new System.Drawing.Size(65, 27);
            this.kryptonButtonAdd.TabIndex = 0;
            this.kryptonButtonAdd.Values.Text = "Add";
            this.kryptonButtonAdd.Click += new System.EventHandler(this.kryptonButtonAdd_Click);
            // 
            // kryptonButtonRemove
            // 
            this.kryptonButtonRemove.AutoSize = true;
            this.kryptonButtonRemove.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonRemove.Enabled = false;
            this.kryptonButtonRemove.Location = new System.Drawing.Point(11, 56);
            this.kryptonButtonRemove.Name = "kryptonButtonRemove";
            this.kryptonButtonRemove.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonRemove.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonRemove.TabIndex = 1;
            this.kryptonButtonRemove.Values.Text = "Remove";
            this.kryptonButtonRemove.Click += new System.EventHandler(this.kryptonButtonRemove_Click);
            // 
            // kryptonButtonClear
            // 
            this.kryptonButtonClear.AutoSize = true;
            this.kryptonButtonClear.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonClear.Enabled = false;
            this.kryptonButtonClear.Location = new System.Drawing.Point(11, 87);
            this.kryptonButtonClear.Name = "kryptonButtonClear";
            this.kryptonButtonClear.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonClear.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonClear.TabIndex = 2;
            this.kryptonButtonClear.Values.Text = "Clear";
            this.kryptonButtonClear.Click += new System.EventHandler(this.kryptonButtonClear_Click);
            // 
            // groupBoxExample
            // 
            this.groupBoxExample.Controls.Add(this.kryptonHeaderGroup1);
            this.groupBoxExample.Location = new System.Drawing.Point(12, 12);
            this.groupBoxExample.Name = "groupBoxExample";
            this.groupBoxExample.Size = new System.Drawing.Size(267, 198);
            this.groupBoxExample.TabIndex = 0;
            this.groupBoxExample.TabStop = false;
            this.groupBoxExample.Text = "Example HeaderGroup";
            // 
            // groupBoxButtonSpecs
            // 
            this.groupBoxButtonSpecs.Controls.Add(this.kryptonButtonAdd);
            this.groupBoxButtonSpecs.Controls.Add(this.kryptonButtonClear);
            this.groupBoxButtonSpecs.Controls.Add(this.kryptonButtonRemove);
            this.groupBoxButtonSpecs.Location = new System.Drawing.Point(12, 216);
            this.groupBoxButtonSpecs.Name = "groupBoxButtonSpecs";
            this.groupBoxButtonSpecs.Size = new System.Drawing.Size(85, 154);
            this.groupBoxButtonSpecs.TabIndex = 1;
            this.groupBoxButtonSpecs.TabStop = false;
            this.groupBoxButtonSpecs.Text = "ButtonSpec";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInstructions);
            this.groupBox1.Location = new System.Drawing.Point(11, 376);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 72);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // labelInstructions
            // 
            this.labelInstructions.Location = new System.Drawing.Point(8, 20);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(250, 50);
            this.labelInstructions.TabIndex = 0;
            this.labelInstructions.Text = "Use the Add/Remove buttons to create/delete ButtonSpec instances.  Click the butt" +
                "on in order to display its properties in the property window.";
            // 
            // groupBoxPrimary
            // 
            this.groupBoxPrimary.Controls.Add(this.kryptonButtonBottomP);
            this.groupBoxPrimary.Controls.Add(this.groupBox2);
            this.groupBoxPrimary.Controls.Add(this.kryptonButtonTopP);
            this.groupBoxPrimary.Controls.Add(this.kryptonButtonRightP);
            this.groupBoxPrimary.Controls.Add(this.kryptonButtonLeftP);
            this.groupBoxPrimary.Location = new System.Drawing.Point(103, 216);
            this.groupBoxPrimary.Name = "groupBoxPrimary";
            this.groupBoxPrimary.Size = new System.Drawing.Size(85, 154);
            this.groupBoxPrimary.TabIndex = 3;
            this.groupBoxPrimary.TabStop = false;
            this.groupBoxPrimary.Text = "Primary";
            // 
            // kryptonButtonBottomP
            // 
            this.kryptonButtonBottomP.AutoSize = true;
            this.kryptonButtonBottomP.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonBottomP.Location = new System.Drawing.Point(11, 118);
            this.kryptonButtonBottomP.Name = "kryptonButtonBottomP";
            this.kryptonButtonBottomP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonBottomP.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonBottomP.TabIndex = 5;
            this.kryptonButtonBottomP.Values.Text = "Bottom";
            this.kryptonButtonBottomP.Click += new System.EventHandler(this.kryptonButtonBottomP_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kryptonButton1);
            this.groupBox2.Controls.Add(this.kryptonButton2);
            this.groupBox2.Controls.Add(this.kryptonButton3);
            this.groupBox2.Location = new System.Drawing.Point(96, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(90, 130);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Primary";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.AutoSize = true;
            this.kryptonButton1.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton1.Location = new System.Drawing.Point(10, 28);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton1.Size = new System.Drawing.Size(65, 27);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.Text = "Top";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.AutoSize = true;
            this.kryptonButton2.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton2.Enabled = false;
            this.kryptonButton2.Location = new System.Drawing.Point(11, 90);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton2.Size = new System.Drawing.Size(64, 27);
            this.kryptonButton2.TabIndex = 2;
            this.kryptonButton2.Values.Text = "Right";
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.AutoSize = true;
            this.kryptonButton3.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButton3.Enabled = false;
            this.kryptonButton3.Location = new System.Drawing.Point(11, 59);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButton3.Size = new System.Drawing.Size(64, 27);
            this.kryptonButton3.TabIndex = 1;
            this.kryptonButton3.Values.Text = "Left";
            // 
            // kryptonButtonTopP
            // 
            this.kryptonButtonTopP.AutoSize = true;
            this.kryptonButtonTopP.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonTopP.Location = new System.Drawing.Point(10, 25);
            this.kryptonButtonTopP.Name = "kryptonButtonTopP";
            this.kryptonButtonTopP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonTopP.Size = new System.Drawing.Size(65, 27);
            this.kryptonButtonTopP.TabIndex = 0;
            this.kryptonButtonTopP.Values.Text = "Top";
            this.kryptonButtonTopP.Click += new System.EventHandler(this.kryptonButtonTopP_Click);
            // 
            // kryptonButtonRightP
            // 
            this.kryptonButtonRightP.AutoSize = true;
            this.kryptonButtonRightP.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonRightP.Location = new System.Drawing.Point(11, 87);
            this.kryptonButtonRightP.Name = "kryptonButtonRightP";
            this.kryptonButtonRightP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonRightP.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonRightP.TabIndex = 2;
            this.kryptonButtonRightP.Values.Text = "Right";
            this.kryptonButtonRightP.Click += new System.EventHandler(this.kryptonButtonRightP_Click);
            // 
            // kryptonButtonLeftP
            // 
            this.kryptonButtonLeftP.AutoSize = true;
            this.kryptonButtonLeftP.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonLeftP.Location = new System.Drawing.Point(11, 56);
            this.kryptonButtonLeftP.Name = "kryptonButtonLeftP";
            this.kryptonButtonLeftP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonLeftP.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonLeftP.TabIndex = 1;
            this.kryptonButtonLeftP.Values.Text = "Left";
            this.kryptonButtonLeftP.Click += new System.EventHandler(this.kryptonButtonLeftP_Click);
            // 
            // groupBoxSecondary
            // 
            this.groupBoxSecondary.Controls.Add(this.kryptonButtonBottomS);
            this.groupBoxSecondary.Controls.Add(this.kryptonButtonTopS);
            this.groupBoxSecondary.Controls.Add(this.kryptonButtonRightS);
            this.groupBoxSecondary.Controls.Add(this.kryptonButtonLeftS);
            this.groupBoxSecondary.Location = new System.Drawing.Point(194, 216);
            this.groupBoxSecondary.Name = "groupBoxSecondary";
            this.groupBoxSecondary.Size = new System.Drawing.Size(85, 154);
            this.groupBoxSecondary.TabIndex = 5;
            this.groupBoxSecondary.TabStop = false;
            this.groupBoxSecondary.Text = "Secondary";
            // 
            // kryptonButtonBottomS
            // 
            this.kryptonButtonBottomS.AutoSize = true;
            this.kryptonButtonBottomS.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonBottomS.Location = new System.Drawing.Point(11, 118);
            this.kryptonButtonBottomS.Name = "kryptonButtonBottomS";
            this.kryptonButtonBottomS.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonBottomS.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonBottomS.TabIndex = 6;
            this.kryptonButtonBottomS.Values.Text = "Bottom";
            this.kryptonButtonBottomS.Click += new System.EventHandler(this.kryptonButtonBottomS_Click);
            // 
            // kryptonButtonTopS
            // 
            this.kryptonButtonTopS.AutoSize = true;
            this.kryptonButtonTopS.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonTopS.Location = new System.Drawing.Point(10, 25);
            this.kryptonButtonTopS.Name = "kryptonButtonTopS";
            this.kryptonButtonTopS.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonTopS.Size = new System.Drawing.Size(65, 27);
            this.kryptonButtonTopS.TabIndex = 0;
            this.kryptonButtonTopS.Values.Text = "Top";
            this.kryptonButtonTopS.Click += new System.EventHandler(this.kryptonButtonTopS_Click);
            // 
            // kryptonButtonRightS
            // 
            this.kryptonButtonRightS.AutoSize = true;
            this.kryptonButtonRightS.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonRightS.Location = new System.Drawing.Point(11, 87);
            this.kryptonButtonRightS.Name = "kryptonButtonRightS";
            this.kryptonButtonRightS.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonRightS.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonRightS.TabIndex = 2;
            this.kryptonButtonRightS.Values.Text = "Right";
            this.kryptonButtonRightS.Click += new System.EventHandler(this.kryptonButtonRightS_Click);
            // 
            // kryptonButtonLeftS
            // 
            this.kryptonButtonLeftS.AutoSize = true;
            this.kryptonButtonLeftS.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonLeftS.Location = new System.Drawing.Point(11, 56);
            this.kryptonButtonLeftS.Name = "kryptonButtonLeftS";
            this.kryptonButtonLeftS.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonButtonLeftS.Size = new System.Drawing.Size(64, 27);
            this.kryptonButtonLeftS.TabIndex = 1;
            this.kryptonButtonLeftS.Values.Text = "Left";
            this.kryptonButtonLeftS.Click += new System.EventHandler(this.kryptonButtonLeftS_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 485);
            this.Controls.Add(this.groupBoxSecondary);
            this.Controls.Add(this.groupBoxPrimary);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxButtonSpecs);
            this.Controls.Add(this.groupBoxExample);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxProperties);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ButtonSpec Playground";
            this.groupBoxProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup1)).EndInit();
            this.kryptonHeaderGroup1.ResumeLayout(false);
            this.groupBoxExample.ResumeLayout(false);
            this.groupBoxButtonSpecs.ResumeLayout(false);
            this.groupBoxButtonSpecs.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxPrimary.ResumeLayout(false);
            this.groupBoxPrimary.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSecondary.ResumeLayout(false);
            this.groupBoxSecondary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxProperties;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button buttonClose;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonAdd;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonRemove;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonClear;
        private System.Windows.Forms.GroupBox groupBoxExample;
        private System.Windows.Forms.GroupBox groupBoxButtonSpecs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.GroupBox groupBoxPrimary;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonTopP;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonRightP;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonLeftP;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonBottomP;
        private System.Windows.Forms.GroupBox groupBoxSecondary;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonBottomS;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonTopS;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonRightS;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonLeftS;
    }
}

