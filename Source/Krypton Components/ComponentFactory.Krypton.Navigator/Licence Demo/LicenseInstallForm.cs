using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Microsoft.Win32;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Provides a basic form for installing Infralution Encrypted Licenses that can be extended or modified using 
	/// visual inheritance
	/// </summary>
	/// <seealso cref="EncryptedLicenseProvider"/>
    internal class LicenseInstallForm : System.Windows.Forms.Form
	{
        #region Member Variables
        private KryptonLabel _keyLabel;
        private KryptonTextBox _keyText;
        private EncryptedLicense _license;
        private System.ComponentModel.Container components = null;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelTop;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelTopHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelBottom;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonIgnore;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelBottomBorder;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelTopBorder;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButtonHelp;
        private PictureBox pictureBoxRight;
        private Label _msgLabel;
        private Label _msgError;
        private Label label1;
        private System.Type _licenseType;
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseInstallForm));
            this._keyText = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this._keyLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanelTop = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.labelTopHeader = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.kryptonPanelBottom = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonButtonHelp = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButtonIgnore = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanelBottomBorder = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanelTopBorder = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this._msgError = new System.Windows.Forms.Label();
            this._msgLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelTop)).BeginInit();
            this.kryptonPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelBottom)).BeginInit();
            this.kryptonPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelBottomBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelTopBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _keyText
            // 
            this._keyText.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Standalone;
            this._keyText.Location = new System.Drawing.Point(89, 98);
            this._keyText.Multiline = true;
            this._keyText.Name = "_keyText";
            this._keyText.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this._keyText.Size = new System.Drawing.Size(320, 35);
            this._keyText.TabIndex = 0;
            this._keyText.TextChanged += new System.EventHandler(this.OnLicenceTextChanged);
            // 
            // _keyLabel
            // 
            this._keyLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._keyLabel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this._keyLabel.Location = new System.Drawing.Point(17, 101);
            this._keyLabel.Name = "_keyLabel";
            this._keyLabel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this._keyLabel.Size = new System.Drawing.Size(69, 19);
            this._keyLabel.TabIndex = 3;
            this._keyLabel.Values.Text = "Licence Key";
            // 
            // kryptonPanelTop
            // 
            this.kryptonPanelTop.Controls.Add(this.labelTopHeader);
            this.kryptonPanelTop.Controls.Add(this.pictureBoxRight);
            this.kryptonPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanelTop.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelTop.Name = "kryptonPanelTop";
            this.kryptonPanelTop.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanelTop.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.kryptonPanelTop.Size = new System.Drawing.Size(437, 90);
            this.kryptonPanelTop.TabIndex = 0;
            // 
            // labelTopHeader
            // 
            this.labelTopHeader.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.labelTopHeader.Location = new System.Drawing.Point(12, 30);
            this.labelTopHeader.Name = "labelTopHeader";
            this.labelTopHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.labelTopHeader.Size = new System.Drawing.Size(215, 53);
            this.labelTopHeader.StateCommon.ShortText.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTopHeader.StateCommon.ShortText.Hint = ComponentFactory.Krypton.Toolkit.PaletteTextHint.AntiAlias;
            this.labelTopHeader.StateCommon.ShortText.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.labelTopHeader.StateCommon.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Inherit;
            this.labelTopHeader.TabIndex = 1;
            this.labelTopHeader.Values.Text = "Krypton Suite\r\nInstall Licence";
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxRight.Image = global::ComponentFactory.Krypton.Navigator.Properties.Resources.CF_Logo;
            this.pictureBoxRight.Location = new System.Drawing.Point(231, 0);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(206, 90);
            this.pictureBoxRight.TabIndex = 0;
            this.pictureBoxRight.TabStop = false;
            // 
            // kryptonPanelBottom
            // 
            this.kryptonPanelBottom.Controls.Add(this.kryptonButtonHelp);
            this.kryptonPanelBottom.Controls.Add(this.kryptonButtonOK);
            this.kryptonPanelBottom.Controls.Add(this.kryptonButtonIgnore);
            this.kryptonPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanelBottom.Location = new System.Drawing.Point(0, 330);
            this.kryptonPanelBottom.Name = "kryptonPanelBottom";
            this.kryptonPanelBottom.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanelBottom.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonPanelBottom.Size = new System.Drawing.Size(437, 45);
            this.kryptonPanelBottom.StateNormal.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonPanelBottom.StateNormal.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.Inherit;
            this.kryptonPanelBottom.TabIndex = 1;
            // 
            // kryptonButtonHelp
            // 
            this.kryptonButtonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButtonHelp.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonHelp.Location = new System.Drawing.Point(10, 10);
            this.kryptonButtonHelp.Name = "kryptonButtonHelp";
            this.kryptonButtonHelp.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonButtonHelp.Size = new System.Drawing.Size(74, 25);
            this.kryptonButtonHelp.TabIndex = 0;
            this.kryptonButtonHelp.Values.Image = global::ComponentFactory.Krypton.Navigator.Properties.Resources.help2;
            this.kryptonButtonHelp.Values.Text = "Help";
            this.kryptonButtonHelp.Click += new System.EventHandler(this.OnHelpButtonClick);
            // 
            // kryptonButtonOK
            // 
            this.kryptonButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButtonOK.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonOK.Enabled = false;
            this.kryptonButtonOK.Location = new System.Drawing.Point(273, 10);
            this.kryptonButtonOK.Name = "kryptonButtonOK";
            this.kryptonButtonOK.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonButtonOK.Size = new System.Drawing.Size(74, 25);
            this.kryptonButtonOK.TabIndex = 1;
            this.kryptonButtonOK.Values.Image = global::ComponentFactory.Krypton.Navigator.Properties.Resources.check2;
            this.kryptonButtonOK.Values.Text = "OK";
            this.kryptonButtonOK.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // kryptonButtonIgnore
            // 
            this.kryptonButtonIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonButtonIgnore.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.kryptonButtonIgnore.Location = new System.Drawing.Point(353, 10);
            this.kryptonButtonIgnore.Name = "kryptonButtonIgnore";
            this.kryptonButtonIgnore.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonButtonIgnore.Size = new System.Drawing.Size(74, 25);
            this.kryptonButtonIgnore.TabIndex = 2;
            this.kryptonButtonIgnore.Values.Image = global::ComponentFactory.Krypton.Navigator.Properties.Resources.delete2;
            this.kryptonButtonIgnore.Values.Text = "Ignore";
            this.kryptonButtonIgnore.Click += new System.EventHandler(this.OnIgnoreButtonClick);
            // 
            // kryptonPanelBottomBorder
            // 
            this.kryptonPanelBottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanelBottomBorder.Location = new System.Drawing.Point(0, 327);
            this.kryptonPanelBottomBorder.Name = "kryptonPanelBottomBorder";
            this.kryptonPanelBottomBorder.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanelBottomBorder.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.kryptonPanelBottomBorder.Size = new System.Drawing.Size(437, 3);
            this.kryptonPanelBottomBorder.TabIndex = 13;
            // 
            // kryptonPanelTopBorder
            // 
            this.kryptonPanelTopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanelTopBorder.Location = new System.Drawing.Point(0, 90);
            this.kryptonPanelTopBorder.Name = "kryptonPanelTopBorder";
            this.kryptonPanelTopBorder.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanelTopBorder.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderPrimary;
            this.kryptonPanelTopBorder.Size = new System.Drawing.Size(437, 3);
            this.kryptonPanelTopBorder.TabIndex = 14;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.label1);
            this.kryptonPanel1.Controls.Add(this._msgError);
            this.kryptonPanel1.Controls.Add(this._msgLabel);
            this.kryptonPanel1.Controls.Add(this._keyText);
            this.kryptonPanel1.Controls.Add(this._keyLabel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 93);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(437, 234);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // _msgError
            // 
            this._msgError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._msgError.BackColor = System.Drawing.Color.Transparent;
            this._msgError.ForeColor = System.Drawing.Color.Red;
            this._msgError.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._msgError.Location = new System.Drawing.Point(88, 138);
            this._msgError.Name = "_msgError";
            this._msgError.Size = new System.Drawing.Size(333, 83);
            this._msgError.TabIndex = 5;
            // 
            // _msgLabel
            // 
            this._msgLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._msgLabel.BackColor = System.Drawing.Color.Transparent;
            this._msgLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._msgLabel.Location = new System.Drawing.Point(10, 10);
            this._msgLabel.Name = "_msgLabel";
            this._msgLabel.Size = new System.Drawing.Size(417, 63);
            this._msgLabel.TabIndex = 4;
            this._msgLabel.Text = "Krypton Suite is not currently licenced. Install your licence by entering the" +
                " serial key and clicking OK.";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(10, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 51);
            this.label1.TabIndex = 6;
            this.label1.Text = "Click Ignore to start a 30 day evaluation period. Once your 30 day evaluation per" +
                "iod expires you will receive this message at design time and a warning message a" +
                "t runtime.";
            // 
            // LicenseInstallForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(437, 375);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonPanelTopBorder);
            this.Controls.Add(this.kryptonPanelBottomBorder);
            this.Controls.Add(this.kryptonPanelBottom);
            this.Controls.Add(this.kryptonPanelTop);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseInstallForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install Krypton Suite Licence";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelTop)).EndInit();
            this.kryptonPanelTop.ResumeLayout(false);
            this.kryptonPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelBottom)).EndInit();
            this.kryptonPanelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelBottomBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelTopBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Initialize a new instance of the form
        /// </summary>
        public LicenseInstallForm()
		{
			InitializeComponent();
		}

        /// <summary>
        /// The type of the component/control being licensed
        /// </summary>
        public Type TypeToLicense
        {
            get { return _licenseType; }
            set { _licenseType = value; }
        }

        /// <summary>
        /// The license installed by this form (if any)
        /// </summary>
        public EncryptedLicense InstalledLicense
        {
            get { return _license; }
        }

        /// <summary>
        /// Display the InstallLicense Dialog
        /// </summary>
        /// <param name="typeToLicence">Type to licence.</param>
        /// <returns>The newly installed license (if any)</returns>
        public EncryptedLicense ShowDialog(Type typeToLicence)
        {
            _licenseType = typeToLicence;
            this.ShowDialog();
            return _license;
        }
        #endregion

        #region Local Methods and Overrides
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        /// <summary>
        /// Return the license provider to use
        /// </summary>
        /// <returns>The license provider to use for installing licensing</returns>
        protected virtual EncryptedLicenseProvider GetLicenseProvider()
        {
            return new EncryptedLicenseProvider();
        }

        /// <summary>
        /// Install the license key entered by the user
        /// </summary>
        /// <param name="key">The key to install</param>
        /// <returns>True if the license was installed successfully</returns>
        protected virtual bool InstallLicenseKey(string key)
        {
            try
            {
                _license = GetLicenseProvider().InstallLicense(_licenseType, key);
                return (_license != null);
            }
            catch 
            {
            }
            return false;
        }

        /// <summary>
        /// Handle TextChanged event for the License Text button
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLicenceTextChanged(object sender, EventArgs e)
        {
            bool valid = false;
            string error = string.Empty;

            EncryptedLicense license = GetLicenseProvider().ValidateLicenseKey(_keyText.Text);
            if (license != null)
            {
                int thisVersion = 400;
                switch (LicenseInstallForm.GetProductCode(license.ProductInfo))
                {
                    case "S":
                        int version = int.Parse(LicenseInstallForm.GetVersionCode(license.ProductInfo));
                        if (version == thisVersion)
                        {
                            valid = true;
                            error = "Key Validated. Press OK to continue.";
                        }
                        else
                            error = "Invalid Version Number. This serial key is valid for version '" + version + "' of Krypton but you " +
                                    "need a version '" + thisVersion.ToString() + "' serial key. You would have been emailed a matching key " +
                                    "when you purchased or were sent a release notification. Please find and enter that matching serial key.";
                        break;
                    default:
                        error = "Invalid key. Please enter the key that was emailed with the purchase of the software.";
                        break;
                }
            }
            else
                error = "Invalid key. Check you have entered all serial key characters.";

            kryptonButtonIgnore.Enabled = !valid;
            kryptonButtonOK.Enabled = valid;
            _msgError.ForeColor = (valid ? Color.Green : Color.Red);
            if (string.IsNullOrEmpty(_keyText.Text))
                _msgError.Text = string.Empty;
            else
                _msgError.Text = error;
        }

        /// <summary>
        /// Handle Click event for the Ignore button
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnIgnoreButtonClick(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handle Click event for the Ignore button
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnOkButtonClick(object sender, EventArgs e)
        {
            if (InstallLicenseKey(_keyText.Text))
                Close();
        }

        /// <summary>
        /// Handle Click event for the Help button
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnHelpButtonClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"http://www.componentfactory.com/installhelp.php");
            }
            catch { }
        }

        /// <summary>
        /// Gets the product code from the license.
        /// </summary>
        /// <param name="licenseString">License code to decompose.</param>
        /// <returns>Product code string.</returns>
        internal protected static string GetProductCode(string licenseString)
        {
            string[] parts = licenseString.Split(',');
            if (parts.Length == 2)
                return parts[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the version code from the license.
        /// </summary>
        /// <param name="licenseString">License code to decompose.</param>
        /// <returns>Product code string.</returns>
        internal protected static string GetVersionCode(string licenseString)
        {
            string[] parts = licenseString.Split(',');
            if (parts.Length == 2)
                return parts[1];
            else
                return string.Empty;
        }
        #endregion
	}

    /// <summary>
    /// Defines an encrypted license for a component or control generated using the Infralution
    /// Licensing System.
    /// </summary>
    /// <remarks>
    /// The Infralution Licensing System provides a secure way of licensing .NET controls,
    /// components and applications.   Licenses are protected using public key encryption to
    /// minimize possibility of cracking.
    /// </remarks>
    /// <seealso cref="EncryptedLicenseProvider"/>
    internal class EncryptedLicense : License
    {
        #region Member Variables

        private string _key;
        private UInt16 _serialNo;
        private string _productInfo;

        #endregion

        #region Public Interface

        /// <summary>
        /// Create a new Infralution Encrypted License
        /// </summary>
        /// <param name="key">The key for the license</param>
        /// <param name="serialNo">The serial number of the license</param>
        /// <param name="productInfo">The product data associated with the license</param>
        public EncryptedLicense(string key, UInt16 serialNo, string productInfo)
        {
            _key = key;
            _serialNo = serialNo;
            _productInfo = productInfo;
        }

        /// <summary>
        /// The license key for the license
        /// </summary>
        public override string LicenseKey
        {
            get { return _key; }
        }

        /// <summary>
        /// The product data associated with the license
        /// </summary>
        public string ProductInfo
        {
            get { return _productInfo; }
        }

        /// <summary>
        /// The unique serial no for the license
        /// </summary>
        public UInt16 SerialNo
        {
            get { return _serialNo; }
        }

        /// <summary>
        /// Cleans up any resources held by the license
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Returns a four character checksum based on the given input string
        /// </summary>
        /// <param name="input">The input string to return a checksum for</param>
        /// <returns>An checksum that can be used to validate the given input string</returns>
        /// <remarks>
        /// <para>
        /// This function can be used to generate a short checksum that can be embedded in a
        /// license key as <see cref="ProductInfo"/>.  This allows you to tie the license key to 
        /// information supplied by the user (for instance the name of the purchaser) without
        /// having to include the full information in the license key.  This enables license keys
        /// to be kept reasonably short.
        /// </para>
        /// <para>
        /// When the license is checked by the application it performs a checksum on the information
        /// supplied by the user and checks that it matches the information in the ProductInfo that
        /// was generated when the license was issued.   The License Tracker application provides
        /// support for "CustomGenerators" which allow you provide the code to generate the ProductInfo
        /// from customer and other information.
        /// </para>
        /// </remarks>
        static public string Checksum(string input)
        {
            int hash = (input == null) ? 0 : input.GetHashCode();
            hash = Math.Abs(hash % 1000);
            return hash.ToString();
        }

        #endregion
    }

    /// <summary>
    /// Defines a .NET LicenseProvider that uses encrypted licenses generated by the 
    /// Infralution Licensing System.
    /// </summary>
    /// <remarks>
    /// <p>The Infralution Licensing System provides a secure way of licensing .NET controls,
    /// components and applications.   Licenses are protected using public key encryption to
    /// minimize possibility of cracking.</p>
    /// <p>Components or controls use the <see cref="LicenseProviderAttribute"/> to specify
    /// this class as the LicenseProvider </p>
    /// </remarks>
    /// <example>
    /// This example demonstrates the typical usage of the EncryptedLicenseProvider to license a component or
    /// control.  The component/control must first initialize the EncryptedLicenseProvider with the public 
    /// key encryption parameters required to validate the license using <see cref="SetParameters"/>.  
    /// It can then use the .NET license manager to obtain a license for the current context 
    /// (runtime or design).  If there is not a valid license the component designer can  
    /// choose the appropriate course of action based on the context.  In this case the code uses the generic
    /// Infralution.Licensing.LicenseInstallForm to prompt the user to install a license - but allows use of 
    /// the component without a license for evaluation purposes. Applications created using the unlicensed evaluation 
    /// component will display a nag MessageBox.
    /// <code lang="C#" escaped="true" >
    /// #using Infralution.Licensing;
    /// #using System.ComponentModel;
    /// [LicenseProvider(typeof(Infralution.Licensing.EncryptedLicenseProvider))]
    /// public class MyControl : Control
    /// {
    ///     const string _licenseParameters = 
    ///        @"<LicenseParameters>
    ///             <RSAKeyValue>
    ///                 <Modulus>z7ijwu7osE4HcGZH7+PhOYw3WuZp/+1szNEjrEo61WVO2sklsdVJpjShXPzUDjlDDvnIFZo4d0l8IFswCYleRS+5PVOkqH0OnAHOSIvoHMNxRyKi9klj7ZD55sKfDJu17MUsjLFLc741B8EvQ3dXPLQoTc+TF5mKOm6o8BnrSuU=</Modulus>
    ///                 <Exponent>AQAB</Exponent>
    ///             </RSAKeyValue>
    ///             <DesignSignature>NqPti5+oayvPPlaETb5gNi9u32sze0o9AhlVEtWm3rfo3gGV/jKXDqQBd9Icy9xgfrEOVKvCyrhnCUEowQDOBPaVMiDqdm9UBRM/KAQt4kUAb2bhl8W47R09bikvahfJWfP+zyic3oin61B1jvuI2CSYjiRi4a5Qz2hudDP5MxM=</DesignSignature>
    ///             <RuntimeSignature>lL2tfh7eUgkZS+I0/yoRowAP7p++zXma4JgO/Npf0QXnbBy3pKw+B5U9jtfwydKleM22Wgk/KBG5uyYXWaJeG/Pe6I29uLD1s5uo+Y9EzVaXDdi9HMQwIIvrv7dcgZlPPkzmkSm1X3uqfHl9R4+NfP9noDBKiQBVSWkem/iH87E=</RuntimeSignature>
    ///          </LicenseParameters>";
    ///
    ///     public MyControl()
    ///     {
    ///         InitializeComponent();
    ///         EncryptedLicenseProvider.SetParameters(_licenseParameters);
    ///         License license;
    ///         if (!LicenseManager.IsValid(typeof(MyControl), this, out license))
    ///         {
    ///             if (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Runtime)
    ///             {
    ///                 MessageBox.Show("This application was created using an unlicensed version of MyControl", 
    ///                                 "Unlicensed Application");
    ///             }
    ///             else
    ///             {
    ///                 LicenseInstallForm form = new LicenseInstallForm();
    ///                 form.ShowDialog("MyControl", "www.mycompany.com", typeof(MyControl)); 
    ///             }
    ///         }
    ///     }
    /// } 
    /// </code>
    /// <code lang ="Visual Basic">
    /// Imports System.ComponentModel
    /// Imports Infralution.Licensing
    /// &lt;LicenseProvider(GetType(Infralution.Licensing.EncryptedLicenseProvider))&gt; _
    /// Public Class MyControl
    ///    Inherits System.Windows.Forms.UserControl
    ///
    ///    Const _licenseParameters As String = _
    ///         "&lt;LicenseParameters&gt;" &amp; _
    ///         "       &lt;RSAKeyValue&gt;" &amp; _
    ///         "       &lt;Modulus&gt;2/dDoUDRnh2gT//5UhGkQRnviVmbrqZj6JdMHhMfZsXslK3x4Yz2QFjpjWtKI2REj8Z3rLd0iOHlOgEaRCM82qGaSMyaoYBYovOtIqU20hpYS6VBtNzwQCJ58d27cnQnMiAmpAjfnKc+gSD/ZIJeO6FdTjfx86+aWrsMOiSpjFk=&lt;/Modulus&gt;" &amp; _
    ///         "       &lt;Exponent&gt;AQAB&lt;/Exponent&gt;" &amp; _
    ///         "       &lt;/RSAKeyValue&gt;" &amp; _
    ///         "       &lt;DesignSignature&gt;IGODC7CpxUR/sFHG38Z09/tpCK115hGWHNm7HvziN/RAw2H7i5o4F6vwgSF7+Uw0ZBKALNqGXWZQg+vaskbf4cotbN1SAhSi3/qYYeH7s5tAiS4ZJfN4DVDaB/IZOfL+3X+hehuO+Ot+X/u8cDqZKCA+1kxeSX4aqwJ+iFwWaAM=&lt;/DesignSignature&gt;" &amp; _
    ///         "       &lt;RuntimeSignature&gt;xaala0UVs/pvODuBtGxJ/V8R633EWmprSYZga0rVA5C3+TdSl5ywHPc2OXVLvm4MyqtK7VX0bsnCZXMKkcek0BkV7TWOIt0xb5ORu1L5iKpOqqXuNhdrTDF9M+FXwQAFPLmts/HObZ4Ul89PmSbTG3pjhQVVUA20ioj7AlEcLQo=&lt;/RuntimeSignature&gt;" &amp; _
    ///         "&lt;/LicenseParameters&gt;"
    ///
    ///    Public Sub New()
    ///         MyBase.New()
    ///         InitializeComponent()
    ///         EncryptedLicenseProvider.SetParameters(_licenseParameters)
    ///         Dim slicense as License
    ///         If Not LicenseManager.IsValid(GetType(MyControl), Me, slicense) Then
    ///             If LicenseManager.CurrentContext.UsageMode = LicenseUsageMode.Runtime Then
    ///                 MessageBox.Show("This application was created using an unlicensed version of MyControl", "Unlicensed Application")
    ///             Else
    ///                 Dim myForm As New LicenseInstallForm
    ///                 myForm.ShowDialog("MyControl", "www.mycompany.com", GetType(MyControl))
    ///             End If
    ///         End If
    ///    End Sub
    /// End Class   
    /// </code>
    /// </example>
    /// <seealso cref="EncryptedLicense"/>
    internal class EncryptedLicenseProvider : LicenseProvider
    {

        #region Member Variables
        private static string _rsaParameters;
        private static byte[] _designSignature;
        private static byte[] _runtimeSignature;

        private static byte[] _desKey = new byte[] { 0x92, 0x15, 0x38, 0xA1, 0x12, 0xED, 0xB3, 0xC2 };
        private static byte[] _desIV = new byte[] { 0xAD, 0x3F, 0xC6, 0x11, 0x47, 0x90, 0xDD, 0xA1 };

        private const int keyLength = 7;

        // the license parameters for the Licensing System itself
        //
        private const string _systemParameters = @"<LicenseParameters><RSAKeyValue><Modulus>u0Uz9OHGLVyLPZul6xeJDmFonpRo7dxI+26vxpm5vU0XHYp/7TQzqOcJVnSW1U6fIDHYynKIwfV/AzwVRV6K1dJB+Ag+bfDExQgJSniEVJq88wXz0iyyhklOx69F37Fglvz4m5p8xvG95KPrKkNHju3dp7gKr/XdfHeqO5MipEE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue><DesignSignature>Rafnrs1FVMy497Y2Bq38LFw3t2vvR2g6qWhC8BCD5FH1Rs7ArcnuQ093AalWqdhZPPvVvEbVltiFOKM0Ycr58J1uXxAUOXtd54wKE2IdfsfsbLiCXarUteFsKdmRO5dylEupq/oyGKaDHKm6PpDKKMgkofQ4Z1M7kq7pVa0gZUk=</DesignSignature><RuntimeSignature>pn47clfpjjV4wUG5YGHPZHyZFaJwEdHVGX8vh4ifSeHtMFxtLDdZg/YFgNKqRAr337bdFgz6YgWjfpBmP6lGB1ydKcT24aF/6DplaPoJiuovJrkE38iOeVLiP4vBd/7tuYc7KObCdenro/02Ur/4j6UL4UxBQsbjVUjuJM3jd80=</RuntimeSignature></LicenseParameters>";

        // the license for the Licensing System
        //
        private static System.ComponentModel.License _systemLicense;
        private static readonly string _licensePath = @"Software\Component Factory\Krypton\";
        private static readonly int _licenseVersion = 400;
        private static readonly string[] _licenseProducts = new string[] { "S" };
        #endregion

        #region Public Interface
        /// <summary>
        /// Set the parameters used to validate licenses created by this provider.
        /// </summary>
        /// <remarks>
        /// This must be called by the client software prior to obtaining licenses using the EncryptedLicenseProvider.
        /// The parameters are generated using the Infralution License Key Generator and pasted into the calling client code.
        /// </remarks>
        /// <param name="licenseParameters">An XML string containing parameters used to validate licenses</param>
        public static void SetParameters(string licenseParameters)
        {
            // parse the validation parameters
            // 
            XmlReader reader = new XmlTextReader(licenseParameters, XmlNodeType.Element, null);
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.LocalName == "RSAKeyValue")
                    {
                        _rsaParameters = reader.ReadOuterXml();
                    }
                    if (reader.LocalName == "DesignSignature")
                    {
                        string key = reader.ReadElementString();
                        _designSignature = Convert.FromBase64String(key);
                    }
                    if (reader.LocalName == "RuntimeSignature")
                    {
                        string key = reader.ReadElementString();
                        _runtimeSignature = Convert.FromBase64String(key);
                    }
                }
            }
            reader.Close();
        }

        /// <summary>
        /// Generate the XML license parameter string that is used to validate licenses generated using the 
        /// given password
        /// </summary>
        /// <param name="password">The password used to encrypted the license data</param>
        /// <returns>An XML string which is used to initialise the EncryptedLicenseProvider</returns>
        public string GenerateLicenseParameters(string password)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            string rsaParam = rsa.ToXmlString(false);

            byte[] designKey = GetEncryptionKey(password);
            byte[] designSignature = rsa.SignData(designKey, new SHA1CryptoServiceProvider());

            // encrypt the design key to produce the runtime key
            //
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = _desKey;
            des.IV = designKey;
            byte[] encKey = des.CreateEncryptor().TransformFinalBlock(designKey, 0, designKey.Length);
            byte[] runtimeKey = new byte[ArraySize(8)];
            Array.Copy(encKey, 0, runtimeKey, 0, keyLength);

            // sign the runtime key
            //
            byte[] runtimeSignature = rsa.SignData(runtimeKey, new SHA1CryptoServiceProvider());

            // write the license parameters out to an XML string
            //
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.ASCII);
            writer.WriteStartElement("LicenseParameters");
            writer.WriteRaw(rsaParam);
            writer.WriteElementString("DesignSignature", Convert.ToBase64String(designSignature));
            writer.WriteElementString("RuntimeSignature", Convert.ToBase64String(runtimeSignature));
            writer.WriteEndElement();
            writer.Close();
            string xml = ASCIIEncoding.ASCII.GetString(stream.ToArray());
            stream.Close();
            return xml;
        }

        /// <summary>
        /// Generate a new encrypted license using the given password
        /// </summary>
        /// <param name="password">The password used to encrypted the license data</param>
        /// <param name="productInfo">User defined data about the product being licensed</param>
        /// <param name="serialNo">The unique license serial number</param>
        /// <returns>A hex encoded ecnrypted license key</returns>
        /// <remarks>
        /// If there is no installed license for the Infralution Licensing System then the only 
        /// allowed password is "TEST" and the only allowed serial numbers are 1 or 0.  To use the
        /// licensed version of this method ensure that the file Infralution.Licensing.EncryptedLicenseProvider.lic
        /// exists in the same directory as the Infralution.Licensing.dll and contains a valid
        /// license key for the Licensing System.
        /// </remarks>
        public virtual string GenerateKey(string password, string productInfo, UInt16 serialNo)
        {
            // Public Key token for the Infralution signed assemblies
            //            
            byte[] requiredToken = { 0x3E, 0x7E, 0x8E, 0x37, 0x44, 0xA5, 0xC1, 0x3F };
            byte[] designKey = GetEncryptionKey(password);
            byte[] productData = ASCIIEncoding.UTF8.GetBytes(productInfo);
            byte[] clientData = BitConverter.GetBytes(serialNo);
            byte[] payload = new byte[ArraySize(productData.Length + clientData.Length)];
            byte[] publicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();
            byte[] testKey = { 0x3E, 0x7E, 0x8E, 0x37, 0x44, 0xA5, 0xC1, 0x3F };

            clientData.CopyTo(payload, 0);
            productData.CopyTo(payload, 2);

#if CHECK_PUBLIC_KEY

            const string passwordErrorMsg = "The only allowable password in evaluation mode is 'TEST'";
            const string serialNoErrorMsg = "The only allowable serial numbers in evaluation mode are '0' or '1'";

            // if the Licensing System is not licensed then we need to check the password and client ID
            //
            if (SystemLicense == null)
            {
                if (password != "TEST")
                    throw new LicenseException(typeof(EncryptedLicenseProvider), this, passwordErrorMsg);
                if (serialNo < 0 || serialNo > 1)
                    throw new LicenseException(typeof(EncryptedLicenseProvider), this, serialNoErrorMsg);
            }

            // Validate this assembly - if it isn't signed with the correct public key
            // then copy rubbish into the key.  This is to make it just a little more
            // difficult for the casual hacker.
            //
            if (!ArrayEqual(publicKeyToken, requiredToken))
            {
                _desKey.CopyTo(designKey, 0);
            }
#endif

            // encrypt the payload using the design key
            //
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = _desKey;
            des.IV = designKey;
            byte[] encPayload = des.CreateEncryptor().TransformFinalBlock(payload, 0, payload.Length);

            // Combine the design key and encrypted payload 
            // Note that only the first 7 bytes of the key contain information so we
            // only pack this much information - this enables us to reduce the size of
            // the final key by 8 bytes.
            //
            byte[] data = new byte[ArraySize(keyLength + encPayload.Length)];
            designKey.CopyTo(data, 0);
            encPayload.CopyTo(data, keyLength);

            // encrypt again to obscure the design key - this time using preset encryption key
            //
            des.IV = _desIV;
            byte[] encData = des.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);

            return ToHex(encData);
        }

        /// <summary>
        /// Install a license for the given type.
        /// </summary>
        /// <remarks>
        /// This method is used by client applications to allow customers to register license for components.  The 
        /// generic Infralution.Licensing.LicenseInstallForm uses this method to install licenses.  Client
        /// components may implement their own registration forms that call this method.
        /// You must call <see cref="SetParameters"/> before using this method.
        /// </remarks>
        /// <param name="type">The type to install the license for</param>
        /// <param name="licenseKey">The license key to install</param>
        /// <returns>A license if succesful or null/nothing if not</returns>
        public virtual EncryptedLicense InstallLicense(Type type, string licenseKey)
        {
            // Create an instance of the provider to validate the license
            EncryptedLicense license = LoadLicense(LicenseManager.CurrentContext, type, licenseKey);
            if (license != null)
            {
                // Grab information we need for putting license into the registry
                string version = LicenseInstallForm.GetVersionCode(license.ProductInfo);
                string product = LicenseInstallForm.GetProductCode(license.ProductInfo);

                // Open (create if not present) the version specific path
                string versionPath = _licensePath + version;
                RegistryKey versionKey = Registry.CurrentUser.OpenSubKey(versionPath, true);
                if (versionKey == null)
                    versionKey = Registry.CurrentUser.CreateSubKey(versionPath);
                if (versionKey != null)
                {
                    // Write the license key into the product code entry
                    versionKey.SetValue(product, licenseKey, RegistryValueKind.String);
                }
            }
            return license;
        }

        /// <summary>
        /// Validate that the given license key is valid for the current licensing parameters
        /// </summary>
        /// <param name="licenseKey">The license key to validate</param>
        /// <returns>The encrypted license if the key is valid otherwise null</returns>
        /// <remarks>
        /// This method provides a mechanism to validate that a given license key is valid
        /// prior to attempting to install it.   This can be useful if you want to check
        /// the <see cref="EncryptedLicense.ProductInfo"/> before installing the license.
        /// You must call <see cref="SetParameters"/> before using this method.
        /// </remarks>
        public virtual EncryptedLicense ValidateLicenseKey(string licenseKey)
        {
            return LoadLicense(LicenseManager.CurrentContext, null, licenseKey);
        }

        /// <summary>
        /// Get a license (if installed) for the given context, type and instance
        /// </summary>
        /// <param name="context">The context (design or runtime)</param>
        /// <param name="type">The type to get the license for</param>
        /// <param name="instance"></param>
        /// <param name="allowExceptions">If true LicenseException is thrown if a valid license cannot be loaded</param>
        /// <returns>An encrypted license</returns>
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            string licenseKey = GetLicenseKey(context, type);
            License license = LoadLicense(context, type, licenseKey);
            if (license == null && allowExceptions)
            {
                if (instance == null)
                    throw new LicenseException(type);
                else
                    throw new LicenseException(type, instance);
            }
            return license;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Return the license for the Infralution Licensing System
        /// </summary>
        /// <returns>The license data if successful or null if not</returns>
        internal static License SystemLicense
        {
            get
            {
                if (_systemLicense == null)
                {
                    SetParameters(_systemParameters);
                    EncryptedLicenseProvider provider = new EncryptedLicenseProvider();
                    _systemLicense = provider.GetLicense(System.ComponentModel.LicenseManager.CurrentContext, typeof(EncryptedLicenseProvider), null, false);
                }
                return _systemLicense;
            }
        }

        /// <summary>
        /// Return the license information from the given license key
        /// </summary>
        /// <param name="licenseKey">The license key to extract the license information from</param>
        /// <param name="password">The password - required to open the license key</param>
        /// <returns>The license data if successful or null if not</returns>
        internal EncryptedLicense GetLicense(string licenseKey, string password)
        {
            try
            {
                byte[] encData = FromHex(licenseKey);
                byte[] requiredToken = { 0x3E, 0x7E, 0x8E, 0x37, 0x44, 0xA5, 0xC1, 0x3F };
                byte[] publicKeyToken = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();

                // Validate this assembly - if it isn't signed with the correct public key
                // then copy rubbish into the key.  This is to make it just a little more
                // difficult for the casual hacker.
                //
#if CHECK_PUBLIC_KEY

                if (!ArrayEqual(publicKeyToken, requiredToken))
                {
                    _desKey.CopyTo(requiredToken, 0);
                }
#endif

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = _desKey;
                des.IV = _desIV;

                byte[] data = des.CreateDecryptor().TransformFinalBlock(encData, 0, encData.Length);

                // extract the encryption key and encrypted product data - note that the encryption
                // key has only 7 significant bytes 
                //
                byte[] encryptionKey = new byte[ArraySize(8)];
                byte[] encPayload = new byte[ArraySize(data.Length - keyLength)];

                Array.Copy(data, 0, encryptionKey, 0, keyLength);
                Array.Copy(data, keyLength, encPayload, 0, encPayload.Length);

                // validate that the password matches that passed in
                //
                byte[] requiredEncryptionKey = GetEncryptionKey(password);
                if (!ArrayEqual(encryptionKey, requiredEncryptionKey))
                {
                    return null;
                }

                // decrypt the payload using the encryption key
                //
                des.IV = encryptionKey;
                byte[] payload = des.CreateDecryptor().TransformFinalBlock(encPayload, 0, encPayload.Length);
                byte[] productData = new byte[ArraySize(payload.Length - 2)];
                Array.Copy(payload, 2, productData, 0, productData.Length);

                UInt16 serialNo = BitConverter.ToUInt16(payload, 0);
                string productInfo = System.Text.ASCIIEncoding.UTF8.GetString(productData);

                return new EncryptedLicense(licenseKey, serialNo, productInfo);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Local Methods
        /// <summary>
        /// Return the array size to use when declaring an array of the given length.
        /// </summary>
        /// <param name="length">The length of the array you are declaring</param>
        /// <returns>The size to declare the array</returns>
        /// <remarks>
        /// This is used to account for the difference between declaring VB and C# arrays and
        /// permit automatic conversion of the code to VB
        /// </remarks>
        private static int ArraySize(int length)
        {
            return length;
        }

        /// <summary>
        /// Return the given input string stripped of the given characters
        /// </summary>
        /// <param name="value">The string to strip</param>
        /// <param name="characters">The characters to strip from the string</param>
        /// <returns>The input string with the given characters removed</returns>
        private static string Strip(string value, string characters)
        {
            if (value == null) return null;
            StringBuilder sb = new StringBuilder();
            foreach (char ch in value)
            {
                if (characters.IndexOf(ch, 0) < 0)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts a byte array into a hexadecimal representation.
        /// </summary>
        /// <param name="data">The byte data to convert</param>
        /// <returns>Hexadecimal representation of the data</returns>
        private static string ToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (i > 0 && i % 2 == 0)
                {
                    sb.Append("-");
                }
                sb.Append(data[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts a hexadecimal string into a byte array.
        /// </summary>
        /// <param name="hex">Te hexadecimal string to convert</param>
        /// <returns>The converted byte data</returns>
        private static byte[] FromHex(string hex)
        {
            string strippedHex = Strip(hex, "\t\r\n -");
            if (strippedHex == null || strippedHex.Length % 2 != 0)
                throw new FormatException("Invalid hexadecimal string");
            byte[] result = new byte[ArraySize(strippedHex.Length / 2)];
            for (int i = 0, j = 0; i < strippedHex.Length; i += 2, j++)
            {
                string s = strippedHex.Substring(i, 2);
                result[j] = byte.Parse(s, NumberStyles.HexNumber);
            }
            return result;
        }

        /// <summary>
        /// Are the contents of the two byte arrays equal
        /// </summary>
        /// <param name="a1">The first array</param>
        /// <param name="a2">The second array </param>
        /// <returns>True if the contents of the arrays is equal</returns>
        private static bool ArrayEqual(byte[] a1, byte[] a2)
        {
            if (a1 == a2) return true;
            if ((a1 == null) || (a2 == null)) return false;
            if (a1.Length != a2.Length) return false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i]) return false;
            }
            return true;
        }

        /// <summary>
        /// Generate an 8 byte DES encryption key using the given password
        /// </summary>
        /// <remarks>
        /// Only the first 7 bytes of the key returned are used.  This enables us
        /// to reduce the size of the final license keys by 8 bytes.
        /// </remarks>
        /// <param name="password">The password used to generate the key</param>
        /// <returns>An 8 byte key</returns> 
        private static byte[] GetEncryptionKey(string password)
        {
            byte[] key = new byte[] { 0xF2, 0xA1, 0x03, 0x9D, 0x63, 0x87, 0x35, 0x5E };
            byte[] iv = new byte[] { 0xAB, 0xB8, 0x94, 0x7E, 0x1D, 0xE5, 0xD1, 0x33 };

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = key;
            des.IV = iv;

            if (password.Length < 8)
                password = password.PadRight(8, '*');
            byte[] data = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] encData = des.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            byte[] result = new byte[ArraySize(8)];
            Array.Copy(encData, 0, result, 0, keyLength);
            return result;
        }

        /// <summary>
        /// Extract the license for the given type from the given licenseKey
        /// </summary>
        /// <param name="context">The current licensing context</param>
        /// <param name="type">The type to be licensed</param>
        /// <param name="licenseKey">The encrypted hexadecimal license key</param>
        /// <returns>A license for the given type or NULL if the licenseKey was invalid</returns>
        private EncryptedLicense LoadLicense(LicenseContext context, Type type, string licenseKey)
        {
            // check that validation parameters have been set by the client
            //
            if (_rsaParameters == null || _designSignature == null || _runtimeSignature == null)
                throw new InvalidOperationException("EncryptedLicenseProvider.SetParameters must be called prior to using the EncryptedLicenseProvider");
            if (licenseKey == null) return null;

            try
            {
                byte[] encData = FromHex(licenseKey);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = _desKey;
                des.IV = _desIV;

                byte[] data = des.CreateDecryptor().TransformFinalBlock(encData, 0, encData.Length);

                // extract the encryption key and encrypted product data - note that the encryption
                // key has only 7 significant bytes 
                //
                byte[] encryptionKey = new byte[ArraySize(8)];
                byte[] encPayload = new byte[ArraySize(data.Length - keyLength)];

                Array.Copy(data, 0, encryptionKey, 0, keyLength);
                Array.Copy(data, keyLength, encPayload, 0, encPayload.Length);

                // validate that the password matches what the client is expecting
                //
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(_rsaParameters);

                if (context.UsageMode == LicenseUsageMode.Designtime)
                {
                    // if design time license requested then the license MUST be a design license
                    //
                    if (!rsa.VerifyData(encryptionKey, new SHA1CryptoServiceProvider(), _designSignature)) return null;
                }
                else
                {
                    // if runtime license requested then first check if the license is a runtime license
                    // also allow design licenses to work at runtime
                    //
                    if (!rsa.VerifyData(encryptionKey, new SHA1CryptoServiceProvider(), _runtimeSignature))
                    {
                        if (!rsa.VerifyData(encryptionKey, new SHA1CryptoServiceProvider(), _designSignature)) return null;
                    }
                }

                // decrypt the payload using the encryption key
                //
                des.IV = encryptionKey;
                byte[] payload = des.CreateDecryptor().TransformFinalBlock(encPayload, 0, encPayload.Length);
                byte[] productData = new byte[ArraySize(payload.Length - 2)];
                Array.Copy(payload, 2, productData, 0, productData.Length);

                UInt16 serialNo = BitConverter.ToUInt16(payload, 0);
                string product = System.Text.ASCIIEncoding.UTF8.GetString(productData);

                // if in design time then create a runtime license and save it
                //
                if (context.UsageMode == LicenseUsageMode.Designtime && type != null)
                {
                    // create the runtime password by encrypting the design time license
                    //
                    byte[] encKey = des.CreateEncryptor().TransformFinalBlock(encryptionKey, 0, encryptionKey.Length);
                    byte[] runtimeKey = new byte[ArraySize(8)];
                    Array.Copy(encKey, 0, runtimeKey, 0, keyLength);

                    // encrypt the payload using the runtime key
                    //
                    des.IV = runtimeKey;
                    encPayload = des.CreateEncryptor().TransformFinalBlock(payload, 0, payload.Length);

                    // Combine the runtime key and encrypted payload 
                    // Note that only the first 7 bytes of the key contain information so we
                    // only pack this much information - this enables us to reduce the size of
                    // the final key by 8 bytes.
                    //
                    data = new byte[ArraySize(keyLength + encPayload.Length)];
                    runtimeKey.CopyTo(data, 0);
                    encPayload.CopyTo(data, keyLength);

                    // encrypt again to obscure the password - this time using preset encryption key
                    //
                    des.IV = _desIV;
                    encData = des.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);

                    string runtimeLicenseKey = ToHex(encData);

                    // save the runtime key into the context
                    //
                    context.SetSavedLicenseKey(type, runtimeLicenseKey);
                }
                return new EncryptedLicense(licenseKey, serialNo, product);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read a license key from the given file
        /// </summary>
        /// <param name="licenseFile">The path to the license file to read the key from</param>
        /// <returns>The license key if any</returns>
        protected virtual string ReadKeyFromFile(string licenseFile)
        {
            string key = null;
            if (File.Exists(licenseFile))
            {
                Stream stream = new FileStream(licenseFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader reader = new StreamReader(stream);
                key = reader.ReadLine();
                reader.Close();
            }
            return key;
        }

        /// <summary>
        /// Return the license key for the given context and type
        /// </summary>
        /// <remarks>
        /// This can be overridden to change where the license key is stored by the provider.   For
        /// instance a derived class could override this method to store the key in the Registry.
        /// </remarks>
        /// <param name="context">The license context</param>
        /// <param name="type">The type to get the key for</param>
        /// <returns>The license key</returns>
        protected virtual string GetLicenseKey(LicenseContext context, Type type)
        {
            string key = null;
            if (context.UsageMode == LicenseUsageMode.Runtime)
            {
                key = context.GetSavedLicenseKey(type, null);
            }

            // Try and loaded license from registry
            if (key == null)
            {
                key = ReadKeyFromRegistry();
            }

            // if we're in design mode or a suitable license key wasn't found in 
            // the runtime context look for a .LIC file
            //
            if (key == null)
            {
                key = ReadKeyFromFile(GetLicenseFilePath(context, type));
            }
            return key;
        }

        /// <summary>
        /// Grab the key from the registry.
        /// </summary>
        /// <returns>Key if found; otherwise null.</returns>
        protected virtual string ReadKeyFromRegistry()
        {
            // Open the version specific key we need
            string versionPath = _licensePath + _licenseVersion.ToString();
            RegistryKey versionKey = Registry.CurrentUser.OpenSubKey(versionPath, false);
            if (versionKey != null)
            {
                // Seach for each of the product codes we allow
                foreach (string code in _licenseProducts)
                {
                    object ret = versionKey.GetValue(code, null);
                    if ((ret != null) && (ret is string))
                        return (string)ret;
                }
            }

            return null;
        }

        /// <summary>
        /// Return the directory used to store license files
        /// </summary>
        /// <param name="context">The licence context</param>
        /// <param name="type">The type being licensed</param>
        /// <returns>The directory to look for license files</returns>
        protected virtual string GetLicenseDirectory(LicenseContext context, Type type)
        {
            string result = null;

            // try to use the type resolver service if available
            //
            if (context != null)
            {
                ITypeResolutionService resolver = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
                if (resolver != null)
                    result = resolver.GetPathOfAssembly(type.Assembly.GetName());
            }

            if (result == null)
            {
                // use the code base if possible - because this works properly under ASP.NET
                // where as the Module.FullyQualifiedName points to a temporary file
                //
                result = type.Assembly.CodeBase;
                if (result.StartsWith(@"file:///"))
                {
                    result = result.Replace(@"file:///", "");
                }
                else
                {
                    result = type.Module.FullyQualifiedName;
                }
            }
            return Path.GetDirectoryName(result);
        }

        /// <summary>
        /// Called by GetLicenseKey to get the file path to obtain the license from (if there is no runtime license saved in the context)
        /// </summary>
        /// <remarks>
        /// This can be overridden to change the file used to store the design time license for the provider.   By default the
        /// the license file is stored in the same directory as the component executable with the name based on the fully
        /// qualified type name eg MyNamespace.MyControl.lic
        /// </remarks>
        /// <seealso cref="GetLicenseKey"/>
        /// <param name="context">The licence context</param>
        /// <param name="type">The type to get the license for</param>
        /// <returns>The path of the license file</returns>
        protected virtual string GetLicenseFilePath(LicenseContext context, Type type)
        {
            string dir = GetLicenseDirectory(context, type);
            return String.Format(@"{0}\{1}.lic", dir, type.FullName);
        }
        #endregion
    }

    /// <summary>
    /// Provides a mechanism for managing time/usage limited evaluations of products.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instantiate an instance of this class to read/write the evaluation parameters for the   
    /// given product.  The <see cref="FirstUseDate"/> is set the first time that
    /// the class is instantiated.  The <see cref="LastUseDate"/> is set each time the class
    /// is instantiated.  The <see cref="UsageCount"/> is incremented each time the class is 
    /// instantiated.
    /// </para>
    /// <para>
    /// Note that evaluation data must be stored somewhere on the users 
    /// hard disk.  It is therefore not too difficult for a sophisticated user to determine the 
    /// changes made either to registry keys or files (using file/registry monitoring software) 
    /// and restore the state of these to their pre-installation state (thus resetting the 
    /// evaluation period).  For this reason it is recommended that you don't rely on this 
    /// mechanism alone.  You should also consider limiting the functionality of your product 
    /// in some way or adding nag screens to discourage long term use of evaluation versions.
    /// </para>
    /// <para>
    /// If you have a data oriented application you can increase the security of evaluations by
    /// storing the current <see cref="UsageCount"/> somewhere in your database each time the 
    /// application runs and cross checking this with the number returned by the EvaluationMonitor.
    /// </para>
    /// </remarks>
    internal class EvaluationMonitor : IDisposable
    {
        #region Member Variables

        private byte[] _productData;
        private int _usageCount = 0;
        private DateTime _firstUseDate = DateTime.MinValue;
        private DateTime _lastUseDate = DateTime.MinValue;
        private bool _invalid = false;

        private RegistryKey _rootKey;
        private RegistryKey _baseKey;
        private string _usageKeyName;
        private string _firstUseKeyName;
        private string _lastUseKeyName;

        // Sub field names for saving data.  Designed to
        // blend in with their surroundings
        //
        private const string classUsageKey = "TypeLib";
        private const string classFirstUseKey = "InprocServer32";
        private const string classLastUseKey = "Control";

        private const string userUsageKey = "Microsoft\\WAB\\WAB4";
        private const string userFirstUseKey = "Microsoft\\WAB\\WAB Sort State";
        private const string userLastUseKey = "Microsoft\\WAB\\WAB4\\LastFind";

        // parameters for encrypting evaluation data
        //
        private static byte[] _desKey = new byte[] { 0x12, 0x75, 0xA8, 0xF1, 0x32, 0xED, 0x13, 0xF2 };
        private static byte[] _desIV = new byte[] { 0xA3, 0xEF, 0xD6, 0x21, 0x37, 0x80, 0xCC, 0xB1 };


        #endregion

        #region Public Interface

        /// <summary>
        /// Initialize a new instance of the evaluation monitor.
        /// </summary>
        /// <remarks>
        /// The <see cref="UsageCount"/> is incremented each time a new evaluation
        /// monitor is instantiated for a product
        /// </remarks>
        /// <param name="productID">A string which uniquely identifies the product</param>
        public EvaluationMonitor(string productID)
        {
            _productData = Encrypt(productID);
            RegistryKey parentKey = null;

            // test whether we can write to HKEY_CLASSES_ROOT
            //
            try
            {
                RegistryKey key = Registry.ClassesRoot.CreateSubKey(productID);
                Registry.ClassesRoot.DeleteSubKey(productID, false);

                // if that succeeded then set up the keys appropriately
                //
                _rootKey = Registry.ClassesRoot;
                parentKey = _rootKey.OpenSubKey("CLSID", true);
                _usageKeyName = classUsageKey;
                _firstUseKeyName = classFirstUseKey;
                _lastUseKeyName = classLastUseKey;
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (parentKey == null)
            {
                try
                {
                    // revert to using HKEY_CURRENT_USER
                    //
                    _rootKey = Registry.CurrentUser;
                    parentKey = _rootKey.OpenSubKey("Software", true);
                    _usageKeyName = userUsageKey;
                    _firstUseKeyName = userFirstUseKey;
                    _lastUseKeyName = userLastUseKey;
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            if (parentKey != null)
            {
                try
                {
                    // find the base key
                    //
                    _baseKey = FindBaseKey(parentKey);

                    // OK we couldn't find a key so create it
                    //
                    if (_baseKey == null)
                    {
                        _usageCount = 0;
                        _firstUseDate = DateTime.UtcNow;
                        _lastUseDate = DateTime.UtcNow;
                        CreateBaseKey(parentKey);
                    }
                    else
                    {
                        GetDateData();
                        GetUsageData();
                    }
                    parentKey.Close();
                }
                catch
                {
                    _invalid = true;
                }
            }
            else
            {
                _usageCount = 1;
                _firstUseDate = DateTime.UtcNow;
                _lastUseDate = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Return the number of times the product has been used 
        /// </summary>
        /// <remarks>
        /// This is calculated by the number of times the Evaluation object for the
        /// product has been instantiated, so typically you should instantiate the
        /// Evaluation object just once in your software.
        /// </remarks>
        public int UsageCount
        {
            get { return _usageCount; }
        }

        /// <summary>
        /// Return the date/time the product was first used
        /// </summary>
        public DateTime FirstUseDate
        {
            get { return _firstUseDate; }
        }

        /// <summary>
        /// Return the date/time the product was last used
        /// </summary>
        public DateTime LastUseDate
        {
            get { return _lastUseDate; }
        }

        /// <summary>
        /// Return the number of days since the product was first run.
        /// </summary>
        public int DaysInUse
        {
            get { return DateTime.UtcNow.Subtract(_firstUseDate).Days + 1; }
        }

        /// <summary>
        /// Returns true if the evaluation monitor detects attempts to circumvent
        /// evaluation limits by tampering with the hidden evaluation data or winding
        /// the PC clock backwards 
        /// </summary>
        public bool Invalid
        {
            get { return _invalid; }
        }

        /// <summary>
        /// Allows you to reset the evaluation period.
        /// </summary>
        /// <remarks>
        /// This may be useful if a customer needs an extension or if somehow they
        /// invalidate their evaluation data by attempting to fiddle
        /// </remarks>
        public void Reset()
        {
            if (_baseKey != null)
            {
                string name = _baseKey.Name;
                int i = name.IndexOf("\\") + 1;
                name = name.Substring(i);
                _baseKey.Close();
                _baseKey = null;
                try
                {
                    _rootKey.DeleteSubKeyTree(name);
                }
                catch { } // ignore possible failures
            }
            _usageCount = 0;
            _firstUseDate = DateTime.UtcNow;
            _lastUseDate = _firstUseDate;
            _invalid = false;
        }

        #endregion

        #region Local Methods


        /// <summary>
        /// Find the base key for this product
        /// </summary>
        /// <param name="parent">The key to search under</param>
        /// <returns>The base registry key used to store the data</returns>
        private RegistryKey FindBaseKey(RegistryKey parent)
        {
            string[] classIDs = parent.GetSubKeyNames();
            foreach (string classID in classIDs)
            {
                RegistryKey key = parent.OpenSubKey(classID);
                object keyValue = key.GetValue(null);
                if (keyValue is byte[])
                {
                    if (Equals((keyValue as byte[]), _productData))
                    {
                        return key;
                    }
                }
                key.Close();
            }
            return null;
        }

        /// <summary>
        /// Create the base key for this product
        /// </summary>
        /// <param name="parent">The key to place the information under</param>
        private void CreateBaseKey(RegistryKey parent)
        {
            // create the registry key with a unique name each time - this makes it a little
            // more difficult for people to find the key
            //
            string baseKeyName = string.Format("{{{0}}}", Guid.NewGuid().ToString().ToUpper());
            _baseKey = parent.CreateSubKey(baseKeyName);
            _baseKey.SetValue(null, _productData);
            RegistryKey dateKey = _baseKey.CreateSubKey(_firstUseKeyName);
            dateKey.SetValue(null, Encrypt(_firstUseDate.ToString()));
            dateKey.Close();

            // create the usage key and set the initial value
            //
            RegistryKey usageKey = _baseKey.CreateSubKey(_usageKeyName);
            usageKey.SetValue(null, Encrypt(_usageCount.ToString()));
            usageKey.Close();

            // create the last use key and set the initial value
            //
            RegistryKey lastUseKey = _baseKey.CreateSubKey(_lastUseKeyName);
            lastUseKey.SetValue(null, Encrypt(_lastUseDate.ToString()));
            usageKey.Close();
        }

        /// <summary>
        /// Calculate the number of days the product has been in use
        /// </summary>
        private void GetDateData()
        {
            string dateString;
            RegistryKey firstUseKey = _baseKey.OpenSubKey(_firstUseKeyName);
            dateString = Decrypt((byte[])firstUseKey.GetValue(null));
            _firstUseDate = DateTime.Parse(dateString);
            firstUseKey.Close();

            RegistryKey lastUseKey = _baseKey.OpenSubKey(_lastUseKeyName, true);
            dateString = Decrypt((byte[])lastUseKey.GetValue(null));
            _lastUseDate = DateTime.Parse(dateString);

            // detect winding the clock back on the PC - give them six hours of grace to allow for
            // daylight saving adjustments etc
            //
            double hoursSinceLastUse = DateTime.UtcNow.Subtract(_lastUseDate).TotalHours;
            if (hoursSinceLastUse < -6.0)
            {
                _invalid = true;
            }
            else
            {
                string test = DateTime.UtcNow.ToString();
                lastUseKey.SetValue(null, Encrypt(DateTime.UtcNow.ToString()));
            }
            lastUseKey.Close();
        }

        /// <summary>
        /// Get the number of times the product has been used (and increment)
        /// </summary>
        private void GetUsageData()
        {
            // get the previous usage count
            //
            RegistryKey usageKey = _baseKey.OpenSubKey(_usageKeyName, true);
            string countString = Decrypt((byte[])usageKey.GetValue(null));
            _usageCount = int.Parse(countString);

            // increment the usage count
            //
            _usageCount++;
            usageKey.SetValue(null, Encrypt(_usageCount.ToString()));
            usageKey.Close();
        }

        /// <summary>
        /// Encrypt the given text
        /// </summary>
        /// <param name="text">The text to encrypt</param>
        /// <returns>Encrypted byte array</returns>
        private byte[] Encrypt(string text)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = _desKey;
            des.IV = _desIV;
            byte[] data = ASCIIEncoding.ASCII.GetBytes(text);
            return des.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
        }

        /// <summary>
        /// Decrypt the given byte data to text
        /// </summary>
        /// <param name="data">The byte data to decrypt</param>
        /// <returns>The decrypted text</returns>
        private string Decrypt(byte[] data)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = _desKey;
            des.IV = _desIV;
            byte[] decryptedData = des.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            return ASCIIEncoding.ASCII.GetString(decryptedData);
        }

        /// <summary>
        /// Are the contents of the two byte arrays equal
        /// </summary>
        /// <param name="a1">The first array</param>
        /// <param name="a2">The second array </param>
        /// <returns>True if the contents of the arrays is equal</returns>
        private bool Equals(byte[] a1, byte[] a2)
        {
            if (a1 == a2) return true;
            if ((a1 == null) || (a2 == null)) return false;
            if (a1.Length != a2.Length) return false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i]) return false;
            }
            return true;
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Free resources used by the EvaluationMonitor
        /// </summary>
        public void Dispose()
        {
            if (_baseKey != null)
            {
                _baseKey.Close();
                _baseKey = null;
            }
        }

        #endregion
    }
}
