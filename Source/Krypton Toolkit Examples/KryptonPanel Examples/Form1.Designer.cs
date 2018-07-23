namespace KryptonPanelExamples
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
            this.panel4Office = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel3Office = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel2Office = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1Office = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4Blue = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel3Blue = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel2Blue = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1Blue = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4Custom = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel2Custom = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel3Custom = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1Custom = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel4Office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Office)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Office)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel4Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Blue)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel4Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Custom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Custom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid);
            this.groupBox4.Location = new System.Drawing.Point(267, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(322, 556);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties for Selected KryptonPanel";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(310, 531);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(514, 574);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4Office);
            this.groupBox1.Controls.Add(this.panel3Office);
            this.groupBox1.Controls.Add(this.panel2Office);
            this.groupBox1.Controls.Add(this.panel1Office);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Professional - Office 2003";
            // 
            // panel4Office
            // 
            this.panel4Office.Location = new System.Drawing.Point(138, 84);
            this.panel4Office.Name = "panel4Office";
            this.panel4Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.panel4Office.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.panel4Office.Size = new System.Drawing.Size(100, 49);
            this.panel4Office.TabIndex = 3;
            this.panel4Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel3Office
            // 
            this.panel3Office.Location = new System.Drawing.Point(20, 84);
            this.panel3Office.Name = "panel3Office";
            this.panel3Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.panel3Office.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderSecondary;
            this.panel3Office.Size = new System.Drawing.Size(100, 49);
            this.panel3Office.TabIndex = 1;
            this.panel3Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel2Office
            // 
            this.panel2Office.Location = new System.Drawing.Point(138, 29);
            this.panel2Office.Name = "panel2Office";
            this.panel2Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.panel2Office.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.panel2Office.Size = new System.Drawing.Size(100, 49);
            this.panel2Office.TabIndex = 2;
            this.panel2Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel1Office
            // 
            this.panel1Office.Location = new System.Drawing.Point(20, 29);
            this.panel1Office.Name = "panel1Office";
            this.panel1Office.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.panel1Office.Size = new System.Drawing.Size(100, 49);
            this.panel1Office.TabIndex = 0;
            this.panel1Office.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel4Blue);
            this.groupBox2.Controls.Add(this.panel3Blue);
            this.groupBox2.Controls.Add(this.panel2Blue);
            this.groupBox2.Controls.Add(this.panel1Blue);
            this.groupBox2.Location = new System.Drawing.Point(8, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 146);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Office 2007 - Blue";
            // 
            // panel4Blue
            // 
            this.panel4Blue.Location = new System.Drawing.Point(138, 84);
            this.panel4Blue.Name = "panel4Blue";
            this.panel4Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.panel4Blue.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.panel4Blue.Size = new System.Drawing.Size(100, 49);
            this.panel4Blue.TabIndex = 3;
            this.panel4Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel3Blue
            // 
            this.panel3Blue.Location = new System.Drawing.Point(20, 84);
            this.panel3Blue.Name = "panel3Blue";
            this.panel3Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.panel3Blue.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderSecondary;
            this.panel3Blue.Size = new System.Drawing.Size(100, 49);
            this.panel3Blue.TabIndex = 1;
            this.panel3Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel2Blue
            // 
            this.panel2Blue.Location = new System.Drawing.Point(138, 29);
            this.panel2Blue.Name = "panel2Blue";
            this.panel2Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.panel2Blue.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.panel2Blue.Size = new System.Drawing.Size(100, 49);
            this.panel2Blue.TabIndex = 2;
            this.panel2Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel1Blue
            // 
            this.panel1Blue.Location = new System.Drawing.Point(20, 29);
            this.panel1Blue.Name = "panel1Blue";
            this.panel1Blue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.panel1Blue.Size = new System.Drawing.Size(100, 49);
            this.panel1Blue.TabIndex = 0;
            this.panel1Blue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel4Custom);
            this.groupBox3.Controls.Add(this.panel2Custom);
            this.groupBox3.Controls.Add(this.panel3Custom);
            this.groupBox3.Controls.Add(this.panel1Custom);
            this.groupBox3.Location = new System.Drawing.Point(8, 314);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 254);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Settings";
            // 
            // panel4Custom
            // 
            this.panel4Custom.Location = new System.Drawing.Point(138, 138);
            this.panel4Custom.Name = "panel4Custom";
            this.panel4Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.panel4Custom.Size = new System.Drawing.Size(100, 100);
            this.panel4Custom.StateNormal.Color1 = System.Drawing.Color.White;
            this.panel4Custom.StateNormal.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel4Custom.StateNormal.ColorAngle = 45F;
            this.panel4Custom.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.panel4Custom.StateNormal.Image = ((System.Drawing.Image)(resources.GetObject("panel4Custom.StateNormal.Image")));
            this.panel4Custom.StateNormal.ImageAlign = ComponentFactory.Krypton.Toolkit.PaletteRectangleAlign.Local;
            this.panel4Custom.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.panel4Custom.TabIndex = 3;
            this.panel4Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel2Custom
            // 
            this.panel2Custom.Location = new System.Drawing.Point(138, 32);
            this.panel2Custom.Name = "panel2Custom";
            this.panel2Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.panel2Custom.Size = new System.Drawing.Size(100, 100);
            this.panel2Custom.StateNormal.Color1 = System.Drawing.Color.White;
            this.panel2Custom.StateNormal.Color2 = System.Drawing.Color.Maroon;
            this.panel2Custom.StateNormal.ColorAngle = 10F;
            this.panel2Custom.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.panel2Custom.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.panel2Custom.TabIndex = 2;
            this.panel2Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel3Custom
            // 
            this.panel3Custom.Location = new System.Drawing.Point(20, 138);
            this.panel3Custom.Name = "panel3Custom";
            this.panel3Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.panel3Custom.Size = new System.Drawing.Size(100, 100);
            this.panel3Custom.StateNormal.Color1 = System.Drawing.Color.White;
            this.panel3Custom.StateNormal.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3Custom.StateNormal.ColorAngle = 45F;
            this.panel3Custom.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Linear;
            this.panel3Custom.StateNormal.Image = ((System.Drawing.Image)(resources.GetObject("panel3Custom.StateNormal.Image")));
            this.panel3Custom.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.panel3Custom.TabIndex = 1;
            this.panel3Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // panel1Custom
            // 
            this.panel1Custom.Location = new System.Drawing.Point(20, 32);
            this.panel1Custom.Name = "panel1Custom";
            this.panel1Custom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.panel1Custom.Size = new System.Drawing.Size(100, 100);
            this.panel1Custom.StateNormal.Color1 = System.Drawing.Color.White;
            this.panel1Custom.StateNormal.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1Custom.StateNormal.ColorAngle = 60F;
            this.panel1Custom.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Rounded;
            this.panel1Custom.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.panel1Custom.TabIndex = 0;
            this.panel1Custom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kryptonPanel_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 607);
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
            this.Text = "KryptonPanel Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel4Office)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Office)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Office)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Office)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel4Blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Blue)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel4Custom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel2Custom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel3Custom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1Custom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel1Office;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel2Office;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel3Office;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel4Office;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel1Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel2Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel3Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel4Blue;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel1Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel2Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel3Custom;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel4Custom;
    }
}

