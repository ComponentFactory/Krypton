// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Layout area for the application tab.
	/// </summary>
    internal class ViewLayoutRibbonAppTab : ViewLayoutDocker
    {
        #region Static Fields
        private static bool _usageShown = false;
        private static string _monitorId = "CFPLKS";
        private static string _licenseParameters = @"<LicenseParameters><RSAKeyValue><Modulus>2QVQ7gvGIKeN0Z/2gJzEnCnoE0pub4Lc61wiPi83+zhE1jjeeiA9D/mLpM3/u+k5DOqllaUKc6bK1jy1t0FCeBzEoH8YEmsxKVXtUUFLq52jYPPEc/gHysxhq3gA1yotOsOfXfhpWhSRJVtcPW4LpFfe3ljwcou8B0q+7yQkfVk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue><DesignSignature>XhiqhrSS4tTJlHf7xRQlgvt/0EBmS4Z1mD7QckhItcdN1G4Pqt7T4yUEk9Cb7EB6aaL0Dz1pibk6kgbcEDtuUzHshJba0jVxQztRN5uO+O3NCFFUe8V08MMGiIhUvUlMTabpsPWO3Zt2GJtp6SscjT+7YKZ6QLa8PvI2pVZrLKI=</DesignSignature><RuntimeSignature>DagPmrsazrCol/DNay/fdGDFLdun4DrZezFnGDxdeRTMr7Nxyag9lsy7REfgXMY6jvSYmpGa1QnItJdVzLbywH605EfPG+5EiQ6Ts3If6cQuNe/Xy42OhFqiKUsdo7v+l3ug3yJuuoqyVUfAUtK508bg0QuUXDwALkHvJWkf2u0=</RuntimeSignature></LicenseParameters>";
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewDrawRibbonAppTab _appTab;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonAppTab class.
		/// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        public ViewLayoutRibbonAppTab(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;

            // Check the control is licenced
            PerformLicenceChecking(ribbon);

            _appTab = new ViewDrawRibbonAppTab(ribbon);

            // Dock it against the appropriate edge
            Add(_appTab, ViewDockStyle.Bottom);
            Add(new ViewLayoutSeparator(1), ViewDockStyle.Left);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonAppTab:" + Id;
		}
		#endregion

        #region AppTab
        /// <summary>
        /// Gets the view element that represents the button.
        /// </summary>
        public ViewDrawRibbonAppTab AppTab
        {
            get { return _appTab; }
        }
        #endregion

        #region PerformLicenceChecking
        /// <summary>
        /// Perform licence checking actions.
        /// </summary>
        /// <param name="ribbon">Ribbon control reference.</param>
        protected void PerformLicenceChecking(KryptonRibbon ribbon)
        {
            // Define the encryted licence information
            EncryptedLicenseProvider.SetParameters(_licenseParameters);

            // If an error has already been shown, then no need to test license again
            bool validated = _usageShown;
            if (!validated)
            {
                // Is there a valid license registered?
                License license = null;
                validated = LicenseManager.IsValid(typeof(KryptonRibbon), ribbon, out license);

                // Valid license is not enough!
                if (validated)
                {
                    validated = false;
                    EncryptedLicense encryptedLicense = license as EncryptedLicense;

                    string[] productInfo = encryptedLicense.ProductInfo.Split(',');

                    // Must contain two fields separated by a comma
                    if (productInfo.Length == 2)
                    {
                        // Both fields must not be empty
                        if (!string.IsNullOrEmpty(productInfo[0]) &&
                            !string.IsNullOrEmpty(productInfo[1]))
                        {
                            // Product code must be ...
                            //    'S' = Krypton Suite
                            // And version number...
                            //    '400'
                            validated = (productInfo[1].Equals("400")) && (productInfo[0][0] == 'S');
                        }
                    }
                }
            }

            // If we need to indicate the invalid licensing state...
            if (!validated)
            {
                // Get hold of the assembly version number
                Version thisVersion = Assembly.GetExecutingAssembly().GetName().Version;

                // We want a unique 30 day evaluation period for each major/minor version
                EvaluationMonitor monitor = new EvaluationMonitor(_monitorId + thisVersion.Major.ToString() + thisVersion.Minor.ToString());

                // If the first time we have failed to get the licence or 
                // the 30 days evaluation period has expired or the component
                // has been created over a 3000 times then...
                if ((monitor.UsageCount == 0) ||
                    (monitor.UsageCount > 3000) ||
                    (monitor.DaysInUse > 30))
                {
                    // At runtime show a NAG screen to prevent unauthorized use of the control
                    if (LicenseManager.CurrentContext.UsageMode == LicenseUsageMode.Runtime)
                    {
                        MessageBox.Show("This application was created using an unlicensed version of\n" +
                                        "the Krypton Suite control from Component Factory Pty Ltd.\n\n" +
                                        "You must contact your software supplier in order to resolve\n" +
                                        "the licencing issue.",
                                        "Unlicensed Application",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else
                    {
                        LicenseInstallForm form = new LicenseInstallForm();
                        form.ShowDialog(typeof(KryptonRibbon));
                    }
                }
            }

            // No need to perform check check more than once
            _usageShown = true;
        }
        #endregion
    }
}
