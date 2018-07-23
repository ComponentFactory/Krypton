namespace KryptonInputBoxExamples
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
            this.labelPrompt = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelDefaultResponse = new System.Windows.Forms.Label();
            this.textBoxPrompt = new System.Windows.Forms.TextBox();
            this.textBoxCaption = new System.Windows.Forms.TextBox();
            this.textBoxDefaultResponse = new System.Windows.Forms.TextBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPrompt
            // 
            this.labelPrompt.AutoSize = true;
            this.labelPrompt.Location = new System.Drawing.Point(72, 25);
            this.labelPrompt.Name = "labelPrompt";
            this.labelPrompt.Size = new System.Drawing.Size(41, 13);
            this.labelPrompt.TabIndex = 0;
            this.labelPrompt.Text = "Prompt";
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Location = new System.Drawing.Point(69, 52);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(44, 13);
            this.labelCaption.TabIndex = 2;
            this.labelCaption.Text = "Caption";
            // 
            // labelDefaultResponse
            // 
            this.labelDefaultResponse.AutoSize = true;
            this.labelDefaultResponse.Location = new System.Drawing.Point(21, 79);
            this.labelDefaultResponse.Name = "labelDefaultResponse";
            this.labelDefaultResponse.Size = new System.Drawing.Size(92, 13);
            this.labelDefaultResponse.TabIndex = 4;
            this.labelDefaultResponse.Text = "Default Response";
            // 
            // textBoxPrompt
            // 
            this.textBoxPrompt.Location = new System.Drawing.Point(119, 22);
            this.textBoxPrompt.Name = "textBoxPrompt";
            this.textBoxPrompt.Size = new System.Drawing.Size(245, 21);
            this.textBoxPrompt.TabIndex = 1;
            this.textBoxPrompt.Text = "User Prompt";
            // 
            // textBoxCaption
            // 
            this.textBoxCaption.Location = new System.Drawing.Point(119, 49);
            this.textBoxCaption.Name = "textBoxCaption";
            this.textBoxCaption.Size = new System.Drawing.Size(245, 21);
            this.textBoxCaption.TabIndex = 3;
            this.textBoxCaption.Text = "Window Title";
            // 
            // textBoxDefaultResponse
            // 
            this.textBoxDefaultResponse.Location = new System.Drawing.Point(119, 76);
            this.textBoxDefaultResponse.Name = "textBoxDefaultResponse";
            this.textBoxDefaultResponse.Size = new System.Drawing.Size(245, 21);
            this.textBoxDefaultResponse.TabIndex = 5;
            this.textBoxDefaultResponse.Text = "Default Response";
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(119, 116);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(245, 41);
            this.buttonShow.TabIndex = 6;
            this.buttonShow.Text = "Show";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 177);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.textBoxDefaultResponse);
            this.Controls.Add(this.textBoxCaption);
            this.Controls.Add(this.textBoxPrompt);
            this.Controls.Add(this.labelDefaultResponse);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.labelPrompt);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "KryptonInputBox Examples";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPrompt;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Label labelDefaultResponse;
        private System.Windows.Forms.TextBox textBoxPrompt;
        private System.Windows.Forms.TextBox textBoxCaption;
        private System.Windows.Forms.TextBox textBoxDefaultResponse;
        private System.Windows.Forms.Button buttonShow;
    }
}

