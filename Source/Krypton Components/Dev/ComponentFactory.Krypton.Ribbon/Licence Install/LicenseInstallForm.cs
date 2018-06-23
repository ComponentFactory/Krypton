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

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Provides a basic form for installing Infralution Encrypted Licenses that can be extended or modified using 
	/// visual inheritance
	/// </summary>
	/// <seealso cref="EncryptedLicenseProvider"/>
    internal class LicenseInstallForm : System.Windows.Forms.Form
	{
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(444, 336);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseInstallForm";
        }
        #endregion
	}

    class EncryptedLicenseProvider : LicenseProvider
    {
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            return new EncryptedLicense();
        }
    }

    class EncryptedLicense : License
    {
        public override void Dispose()
        {
        }

        public override string LicenseKey
        {
            get { return string.Empty; }
        }
    }
}
