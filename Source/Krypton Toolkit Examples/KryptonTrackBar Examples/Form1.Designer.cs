namespace KryptonTrackBarExamples
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
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kryptonTrackBar4 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar3 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar2 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar1 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kryptonTrackBar11 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar10 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar9 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar5 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar6 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar7 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonTrackBar8 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(6, 19);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(310, 364);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid);
            this.groupBox4.Location = new System.Drawing.Point(297, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(322, 389);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties for Selected KryptonTrackBar";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(544, 407);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kryptonTrackBar4);
            this.groupBox1.Controls.Add(this.kryptonTrackBar3);
            this.groupBox1.Controls.Add(this.kryptonTrackBar2);
            this.groupBox1.Controls.Add(this.kryptonTrackBar1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orientation = Horizontal";
            // 
            // kryptonTrackBar4
            // 
            this.kryptonTrackBar4.DrawBackground = true;
            this.kryptonTrackBar4.Location = new System.Drawing.Point(15, 148);
            this.kryptonTrackBar4.Name = "kryptonTrackBar4";
            this.kryptonTrackBar4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonTrackBar4.Size = new System.Drawing.Size(250, 27);
            this.kryptonTrackBar4.TabIndex = 3;
            this.kryptonTrackBar4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.kryptonTrackBar4.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.kryptonTrackBar4.Value = 7;
            this.kryptonTrackBar4.VolumeControl = true;
            this.kryptonTrackBar4.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar3
            // 
            this.kryptonTrackBar3.DrawBackground = true;
            this.kryptonTrackBar3.Location = new System.Drawing.Point(15, 97);
            this.kryptonTrackBar3.Maximum = 30;
            this.kryptonTrackBar3.Name = "kryptonTrackBar3";
            this.kryptonTrackBar3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonTrackBar3.Size = new System.Drawing.Size(250, 41);
            this.kryptonTrackBar3.TabIndex = 2;
            this.kryptonTrackBar3.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.kryptonTrackBar3.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.kryptonTrackBar3.Value = 15;
            this.kryptonTrackBar3.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar2
            // 
            this.kryptonTrackBar2.DrawBackground = true;
            this.kryptonTrackBar2.Location = new System.Drawing.Point(15, 60);
            this.kryptonTrackBar2.Maximum = 20;
            this.kryptonTrackBar2.Name = "kryptonTrackBar2";
            this.kryptonTrackBar2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonTrackBar2.Size = new System.Drawing.Size(250, 27);
            this.kryptonTrackBar2.TabIndex = 1;
            this.kryptonTrackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kryptonTrackBar2.Value = 5;
            this.kryptonTrackBar2.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar1
            // 
            this.kryptonTrackBar1.DrawBackground = true;
            this.kryptonTrackBar1.Location = new System.Drawing.Point(15, 30);
            this.kryptonTrackBar1.Name = "kryptonTrackBar1";
            this.kryptonTrackBar1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonTrackBar1.Size = new System.Drawing.Size(250, 20);
            this.kryptonTrackBar1.TabIndex = 0;
            this.kryptonTrackBar1.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Small;
            this.kryptonTrackBar1.Value = 1;
            this.kryptonTrackBar1.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kryptonTrackBar11);
            this.groupBox2.Controls.Add(this.kryptonTrackBar10);
            this.groupBox2.Controls.Add(this.kryptonTrackBar9);
            this.groupBox2.Controls.Add(this.kryptonTrackBar5);
            this.groupBox2.Controls.Add(this.kryptonTrackBar6);
            this.groupBox2.Controls.Add(this.kryptonTrackBar7);
            this.groupBox2.Controls.Add(this.kryptonTrackBar8);
            this.groupBox2.Location = new System.Drawing.Point(12, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 198);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Orientation = Vertical";
            // 
            // kryptonTrackBar11
            // 
            this.kryptonTrackBar11.DrawBackground = true;
            this.kryptonTrackBar11.Location = new System.Drawing.Point(141, 28);
            this.kryptonTrackBar11.Maximum = 20;
            this.kryptonTrackBar11.Name = "kryptonTrackBar11";
            this.kryptonTrackBar11.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar11.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Silver;
            this.kryptonTrackBar11.Size = new System.Drawing.Size(27, 159);
            this.kryptonTrackBar11.TabIndex = 4;
            this.kryptonTrackBar11.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kryptonTrackBar11.Value = 5;
            this.kryptonTrackBar11.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar10
            // 
            this.kryptonTrackBar10.DrawBackground = true;
            this.kryptonTrackBar10.Location = new System.Drawing.Point(74, 28);
            this.kryptonTrackBar10.Name = "kryptonTrackBar10";
            this.kryptonTrackBar10.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar10.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Black;
            this.kryptonTrackBar10.Size = new System.Drawing.Size(20, 159);
            this.kryptonTrackBar10.TabIndex = 2;
            this.kryptonTrackBar10.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Small;
            this.kryptonTrackBar10.Value = 1;
            this.kryptonTrackBar10.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar9
            // 
            this.kryptonTrackBar9.DrawBackground = true;
            this.kryptonTrackBar9.Location = new System.Drawing.Point(44, 28);
            this.kryptonTrackBar9.Name = "kryptonTrackBar9";
            this.kryptonTrackBar9.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar9.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kryptonTrackBar9.Size = new System.Drawing.Size(20, 159);
            this.kryptonTrackBar9.TabIndex = 1;
            this.kryptonTrackBar9.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Small;
            this.kryptonTrackBar9.Value = 1;
            this.kryptonTrackBar9.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar5
            // 
            this.kryptonTrackBar5.DrawBackground = true;
            this.kryptonTrackBar5.Location = new System.Drawing.Point(238, 28);
            this.kryptonTrackBar5.Name = "kryptonTrackBar5";
            this.kryptonTrackBar5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonTrackBar5.Size = new System.Drawing.Size(27, 159);
            this.kryptonTrackBar5.TabIndex = 7;
            this.kryptonTrackBar5.TickStyle = System.Windows.Forms.TickStyle.None;
            this.kryptonTrackBar5.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.kryptonTrackBar5.UseWaitCursor = true;
            this.kryptonTrackBar5.Value = 7;
            this.kryptonTrackBar5.VolumeControl = true;
            this.kryptonTrackBar5.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar6
            // 
            this.kryptonTrackBar6.DrawBackground = true;
            this.kryptonTrackBar6.Location = new System.Drawing.Point(182, 28);
            this.kryptonTrackBar6.Maximum = 20;
            this.kryptonTrackBar6.Name = "kryptonTrackBar6";
            this.kryptonTrackBar6.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.kryptonTrackBar6.Size = new System.Drawing.Size(41, 159);
            this.kryptonTrackBar6.TabIndex = 6;
            this.kryptonTrackBar6.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.kryptonTrackBar6.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.kryptonTrackBar6.Value = 10;
            this.kryptonTrackBar6.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar7
            // 
            this.kryptonTrackBar7.DrawBackground = true;
            this.kryptonTrackBar7.Location = new System.Drawing.Point(105, 28);
            this.kryptonTrackBar7.Maximum = 20;
            this.kryptonTrackBar7.Name = "kryptonTrackBar7";
            this.kryptonTrackBar7.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.kryptonTrackBar7.Size = new System.Drawing.Size(27, 159);
            this.kryptonTrackBar7.TabIndex = 3;
            this.kryptonTrackBar7.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kryptonTrackBar7.Value = 5;
            this.kryptonTrackBar7.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // kryptonTrackBar8
            // 
            this.kryptonTrackBar8.DrawBackground = true;
            this.kryptonTrackBar8.Location = new System.Drawing.Point(14, 28);
            this.kryptonTrackBar8.Name = "kryptonTrackBar8";
            this.kryptonTrackBar8.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonTrackBar8.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.kryptonTrackBar8.Size = new System.Drawing.Size(20, 159);
            this.kryptonTrackBar8.TabIndex = 0;
            this.kryptonTrackBar8.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Small;
            this.kryptonTrackBar8.Value = 1;
            this.kryptonTrackBar8.Enter += new System.EventHandler(this.trackBar_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 438);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonTrackBar Examples";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar4;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar3;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar2;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar11;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar10;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar9;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar5;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar6;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar7;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar8;
    }
}

