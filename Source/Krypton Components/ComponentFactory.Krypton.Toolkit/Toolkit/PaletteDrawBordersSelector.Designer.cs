namespace ComponentFactory.Krypton.Toolkit
{
    partial class PaletteDrawBordersSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxInherit = new System.Windows.Forms.CheckBox();
            this.checkBoxTop = new System.Windows.Forms.CheckBox();
            this.checkBoxBottom = new System.Windows.Forms.CheckBox();
            this.checkBoxLeft = new System.Windows.Forms.CheckBox();
            this.checkBoxRight = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxInherit
            // 
            this.checkBoxInherit.AutoSize = true;
            this.checkBoxInherit.Location = new System.Drawing.Point(12, 12);
            this.checkBoxInherit.Name = "checkBoxInherit";
            this.checkBoxInherit.Size = new System.Drawing.Size(55, 17);
            this.checkBoxInherit.TabIndex = 0;
            this.checkBoxInherit.Text = "Inherit";
            this.checkBoxInherit.UseVisualStyleBackColor = true;
            this.checkBoxInherit.CheckedChanged += new System.EventHandler(this.checkBoxInherit_CheckedChanged);
            // 
            // checkBoxTop
            // 
            this.checkBoxTop.AutoSize = true;
            this.checkBoxTop.Location = new System.Drawing.Point(12, 60);
            this.checkBoxTop.Name = "checkBoxTop";
            this.checkBoxTop.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTop.TabIndex = 1;
            this.checkBoxTop.Text = "Top";
            this.checkBoxTop.UseVisualStyleBackColor = true;
            // 
            // checkBoxBottom
            // 
            this.checkBoxBottom.AutoSize = true;
            this.checkBoxBottom.Location = new System.Drawing.Point(12, 80);
            this.checkBoxBottom.Name = "checkBoxBottom";
            this.checkBoxBottom.Size = new System.Drawing.Size(59, 17);
            this.checkBoxBottom.TabIndex = 2;
            this.checkBoxBottom.Text = "Bottom";
            this.checkBoxBottom.UseVisualStyleBackColor = true;
            // 
            // checkBoxLeft
            // 
            this.checkBoxLeft.AutoSize = true;
            this.checkBoxLeft.Location = new System.Drawing.Point(12, 100);
            this.checkBoxLeft.Name = "checkBoxLeft";
            this.checkBoxLeft.Size = new System.Drawing.Size(44, 17);
            this.checkBoxLeft.TabIndex = 3;
            this.checkBoxLeft.Text = "Left";
            this.checkBoxLeft.UseVisualStyleBackColor = true;
            // 
            // checkBoxRight
            // 
            this.checkBoxRight.AutoSize = true;
            this.checkBoxRight.Location = new System.Drawing.Point(12, 120);
            this.checkBoxRight.Name = "checkBoxRight";
            this.checkBoxRight.Size = new System.Drawing.Size(51, 17);
            this.checkBoxRight.TabIndex = 4;
            this.checkBoxRight.Text = "Right";
            this.checkBoxRight.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "---- OR ----";
            // 
            // PaletteDrawBordersSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxTop);
            this.Controls.Add(this.checkBoxRight);
            this.Controls.Add(this.checkBoxBottom);
            this.Controls.Add(this.checkBoxInherit);
            this.Controls.Add(this.checkBoxLeft);
            this.Name = "PaletteDrawBordersSelector";
            this.Size = new System.Drawing.Size(76, 146);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxInherit;
        private System.Windows.Forms.CheckBox checkBoxTop;
        private System.Windows.Forms.CheckBox checkBoxBottom;
        private System.Windows.Forms.CheckBox checkBoxLeft;
        private System.Windows.Forms.CheckBox checkBoxRight;
        private System.Windows.Forms.Label label1;
    }
}
