namespace NavigatorToolTips
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
            this.groupBoxTooltipProperties = new System.Windows.Forms.GroupBox();
            this.checkAllowButtonSpecTooltips = new System.Windows.Forms.CheckBox();
            this.checkAllowPageTooltips = new System.Windows.Forms.CheckBox();
            this.comboMapImage = new System.Windows.Forms.ComboBox();
            this.comboMapExtraText = new System.Windows.Forms.ComboBox();
            this.comboMapText = new System.Windows.Forms.ComboBox();
            this.labelMapImage = new System.Windows.Forms.Label();
            this.labelMapExtraText = new System.Windows.Forms.Label();
            this.labelMapText = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.kryptonNavigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPage1 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage2 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage3 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonPage4 = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.buttonClose = new System.Windows.Forms.Button();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.groupBoxTooltipProperties.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).BeginInit();
            this.kryptonNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTooltipProperties
            // 
            this.groupBoxTooltipProperties.Controls.Add(this.checkAllowButtonSpecTooltips);
            this.groupBoxTooltipProperties.Controls.Add(this.checkAllowPageTooltips);
            this.groupBoxTooltipProperties.Controls.Add(this.comboMapImage);
            this.groupBoxTooltipProperties.Controls.Add(this.comboMapExtraText);
            this.groupBoxTooltipProperties.Controls.Add(this.comboMapText);
            this.groupBoxTooltipProperties.Controls.Add(this.labelMapImage);
            this.groupBoxTooltipProperties.Controls.Add(this.labelMapExtraText);
            this.groupBoxTooltipProperties.Controls.Add(this.labelMapText);
            this.groupBoxTooltipProperties.Location = new System.Drawing.Point(13, 13);
            this.groupBoxTooltipProperties.Name = "groupBoxTooltipProperties";
            this.groupBoxTooltipProperties.Size = new System.Drawing.Size(323, 176);
            this.groupBoxTooltipProperties.TabIndex = 1;
            this.groupBoxTooltipProperties.TabStop = false;
            this.groupBoxTooltipProperties.Text = "ToolTip Properties";
            // 
            // checkAllowButtonSpecTooltips
            // 
            this.checkAllowButtonSpecTooltips.AutoSize = true;
            this.checkAllowButtonSpecTooltips.Location = new System.Drawing.Point(102, 49);
            this.checkAllowButtonSpecTooltips.Name = "checkAllowButtonSpecTooltips";
            this.checkAllowButtonSpecTooltips.Size = new System.Drawing.Size(154, 17);
            this.checkAllowButtonSpecTooltips.TabIndex = 1;
            this.checkAllowButtonSpecTooltips.Text = "Allow ButtonSpec ToolTips";
            this.checkAllowButtonSpecTooltips.UseVisualStyleBackColor = true;
            this.checkAllowButtonSpecTooltips.CheckedChanged += new System.EventHandler(this.checkAllowButtonSpecTooltips_CheckedChanged);
            // 
            // checkAllowPageTooltips
            // 
            this.checkAllowPageTooltips.AutoSize = true;
            this.checkAllowPageTooltips.Location = new System.Drawing.Point(102, 25);
            this.checkAllowPageTooltips.Name = "checkAllowPageTooltips";
            this.checkAllowPageTooltips.Size = new System.Drawing.Size(123, 17);
            this.checkAllowPageTooltips.TabIndex = 0;
            this.checkAllowPageTooltips.Text = "Allow Page ToolTips";
            this.checkAllowPageTooltips.UseVisualStyleBackColor = true;
            this.checkAllowPageTooltips.CheckedChanged += new System.EventHandler(this.checkAllowPageTooltips_CheckedChanged);
            // 
            // comboMapImage
            // 
            this.comboMapImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMapImage.FormattingEnabled = true;
            this.comboMapImage.Location = new System.Drawing.Point(102, 140);
            this.comboMapImage.Name = "comboMapImage";
            this.comboMapImage.Size = new System.Drawing.Size(198, 21);
            this.comboMapImage.TabIndex = 4;
            this.comboMapImage.SelectedIndexChanged += new System.EventHandler(this.comboMapImage_SelectedIndexChanged);
            // 
            // comboMapExtraText
            // 
            this.comboMapExtraText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMapExtraText.FormattingEnabled = true;
            this.comboMapExtraText.Location = new System.Drawing.Point(102, 113);
            this.comboMapExtraText.Name = "comboMapExtraText";
            this.comboMapExtraText.Size = new System.Drawing.Size(198, 21);
            this.comboMapExtraText.TabIndex = 3;
            this.comboMapExtraText.SelectedIndexChanged += new System.EventHandler(this.comboMapExtraText_SelectedIndexChanged);
            // 
            // comboMapText
            // 
            this.comboMapText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMapText.FormattingEnabled = true;
            this.comboMapText.Location = new System.Drawing.Point(102, 86);
            this.comboMapText.Name = "comboMapText";
            this.comboMapText.Size = new System.Drawing.Size(198, 21);
            this.comboMapText.TabIndex = 2;
            this.comboMapText.SelectedIndexChanged += new System.EventHandler(this.comboMapText_SelectedIndexChanged);
            // 
            // labelMapImage
            // 
            this.labelMapImage.AutoSize = true;
            this.labelMapImage.Location = new System.Drawing.Point(35, 143);
            this.labelMapImage.Name = "labelMapImage";
            this.labelMapImage.Size = new System.Drawing.Size(60, 13);
            this.labelMapImage.TabIndex = 8;
            this.labelMapImage.Text = "Map Image";
            // 
            // labelMapExtraText
            // 
            this.labelMapExtraText.AutoSize = true;
            this.labelMapExtraText.Location = new System.Drawing.Point(16, 116);
            this.labelMapExtraText.Name = "labelMapExtraText";
            this.labelMapExtraText.Size = new System.Drawing.Size(79, 13);
            this.labelMapExtraText.TabIndex = 7;
            this.labelMapExtraText.Text = "Map Extra Text";
            // 
            // labelMapText
            // 
            this.labelMapText.AutoSize = true;
            this.labelMapText.Location = new System.Drawing.Point(43, 89);
            this.labelMapText.Name = "labelMapText";
            this.labelMapText.Size = new System.Drawing.Size(52, 13);
            this.labelMapText.TabIndex = 6;
            this.labelMapText.Text = "Map Text";
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.kryptonNavigator);
            this.panel.Location = new System.Drawing.Point(351, 18);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(313, 171);
            this.panel.TabIndex = 1;
            // 
            // kryptonNavigator
            // 
            this.kryptonNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator.Name = "kryptonNavigator";
            this.kryptonNavigator.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPage1,
            this.kryptonPage2,
            this.kryptonPage3,
            this.kryptonPage4});
            this.kryptonNavigator.SelectedIndex = 0;
            this.kryptonNavigator.Size = new System.Drawing.Size(313, 171);
            this.kryptonNavigator.TabIndex = 0;
            this.kryptonNavigator.Text = "kryptonNavigator1";
            this.kryptonNavigator.ToolTips.AllowButtonSpecToolTips = true;
            this.kryptonNavigator.ToolTips.AllowPageToolTips = true;
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageLarge")));
            this.kryptonPage1.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageMedium")));
            this.kryptonPage1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ImageSmall")));
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(311, 145);
            this.kryptonPage1.Text = "Page 1";
            this.kryptonPage1.TextDescription = "Description of page 1";
            this.kryptonPage1.TextTitle = "Page Title 1";
            this.kryptonPage1.ToolTipBody = "This is a long description of \r\npage 1 which covers several\r\nlines of text.";
            this.kryptonPage1.ToolTipImage = ((System.Drawing.Image)(resources.GetObject("kryptonPage1.ToolTipImage")));
            this.kryptonPage1.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kryptonPage1.ToolTipTitle = "Page 1 SuperTip";
            this.kryptonPage1.UniqueName = "A2A9C435C1E1424FA2A9C435C1E1424F";
            // 
            // kryptonPage2
            // 
            this.kryptonPage2.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage2.Flags = 65534;
            this.kryptonPage2.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageLarge")));
            this.kryptonPage2.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageMedium")));
            this.kryptonPage2.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ImageSmall")));
            this.kryptonPage2.LastVisibleSet = true;
            this.kryptonPage2.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage2.Name = "kryptonPage2";
            this.kryptonPage2.Size = new System.Drawing.Size(304, 145);
            this.kryptonPage2.Text = "Page 2";
            this.kryptonPage2.TextDescription = "A description of page 2";
            this.kryptonPage2.TextTitle = "Page Title 2";
            this.kryptonPage2.ToolTipBody = "Information about the second page\r\nalso covers more than a single line\r\nof text.";
            this.kryptonPage2.ToolTipImage = ((System.Drawing.Image)(resources.GetObject("kryptonPage2.ToolTipImage")));
            this.kryptonPage2.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kryptonPage2.ToolTipTitle = "Page 2 SuperTip";
            this.kryptonPage2.UniqueName = "9A45A535BEE6487F9A45A535BEE6487F";
            // 
            // kryptonPage3
            // 
            this.kryptonPage3.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage3.Flags = 65534;
            this.kryptonPage3.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageLarge")));
            this.kryptonPage3.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageMedium")));
            this.kryptonPage3.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage3.ImageSmall")));
            this.kryptonPage3.LastVisibleSet = true;
            this.kryptonPage3.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage3.Name = "kryptonPage3";
            this.kryptonPage3.Size = new System.Drawing.Size(304, 145);
            this.kryptonPage3.Text = "Page 3";
            this.kryptonPage3.TextDescription = "The description of page 3";
            this.kryptonPage3.TextTitle = "Page Title 3";
            this.kryptonPage3.ToolTipTitle = "Detailed information of page 3 in a tooltip";
            this.kryptonPage3.UniqueName = "46E548699395412546E5486993954125";
            // 
            // kryptonPage4
            // 
            this.kryptonPage4.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage4.Flags = 65534;
            this.kryptonPage4.ImageLarge = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageLarge")));
            this.kryptonPage4.ImageMedium = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageMedium")));
            this.kryptonPage4.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonPage4.ImageSmall")));
            this.kryptonPage4.LastVisibleSet = true;
            this.kryptonPage4.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage4.Name = "kryptonPage4";
            this.kryptonPage4.Size = new System.Drawing.Size(311, 145);
            this.kryptonPage4.Text = "Page 4";
            this.kryptonPage4.TextDescription = "Brief description of page 4";
            this.kryptonPage4.TextTitle = "Page Title 4";
            this.kryptonPage4.ToolTipTitle = "Explanation of what page 4 is all about";
            this.kryptonPage4.UniqueName = "483D78EBE8814B82483D78EBE8814B82";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(588, 199);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 234);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.groupBoxTooltipProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(643, 260);
            this.Name = "Form1";
            this.Text = "Navigator ToolTips";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxTooltipProperties.ResumeLayout(false);
            this.groupBoxTooltipProperties.PerformLayout();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).EndInit();
            this.kryptonNavigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage1;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage2;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage3;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPage4;
        private System.Windows.Forms.GroupBox groupBoxTooltipProperties;
        private System.Windows.Forms.Label labelMapText;
        private System.Windows.Forms.Label labelMapExtraText;
        private System.Windows.Forms.Label labelMapImage;
        private System.Windows.Forms.ComboBox comboMapImage;
        private System.Windows.Forms.ComboBox comboMapExtraText;
        private System.Windows.Forms.ComboBox comboMapText;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkAllowButtonSpecTooltips;
        private System.Windows.Forms.CheckBox checkAllowPageTooltips;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    }
}

