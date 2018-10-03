// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for color button string properties.
	/// </summary>
    public class PaletteColorButtonStrings : Storage
    {
        #region Static Fields
        private static readonly string _defaultMoreColors = "&More Colors...";
        private static readonly string _defaultNoColor = "&No Color";
        private static readonly string _defaultRecentColors = "Recent Colors";
        private static readonly string _defaultStandardColors = "Standard Colors";
        private static readonly string _defaultThemeColors = "Theme Colors";
        #endregion

        #region Instance Fields
        private string _moreColors;
        private string _noColor;
        private string _recentColors;
        private string _standardColors;
        private string _themeColors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteColorButtonStrings class.
		/// </summary>
        public PaletteColorButtonStrings()
		{
            // Default values
            _moreColors = _defaultMoreColors;
            _noColor = _defaultNoColor;
            _recentColors = _defaultRecentColors;
            _standardColors = _defaultStandardColors;
            _themeColors = _defaultThemeColors;
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return (MoreColors.Equals(_defaultMoreColors)) &&
                       (NoColor.Equals(_defaultNoColor)) &&
                       (RecentColors.Equals(_defaultRecentColors)) &&
                       (StandardColors.Equals(_defaultStandardColors)) &&
                       (ThemeColors.Equals(_defaultThemeColors));
            }
        }
        #endregion

        #region MoreColors
        /// <summary>
        /// Gets and sets the menu string for a 'more colors' entry.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for a 'more colors' entry.")]
        [DefaultValue("&More Colors...")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string MoreColors
        {
            get { return _moreColors; }
            set { _moreColors = value; }
        }
        #endregion

        #region NoColor
        /// <summary>
        /// Gets and sets the menu string for a 'no color' entry.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for a 'no color' entry.")]
        [DefaultValue("&No Color")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string NoColor
        {
            get { return _noColor; }
            set { _noColor = value; }
        }
        #endregion

        #region RecentColors
        /// <summary>
        /// Gets and sets the title for the recent colors section of the color button menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for recent colors section of the color button menu.")]
        [DefaultValue("Recent Colors")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string RecentColors
        {
            get { return _recentColors; }
            set { _recentColors = value; }
        }
        #endregion

        #region StandardColors
        /// <summary>
        /// Gets and sets the title for the standard colors section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for standard colors section of the color button menu.")]
        [DefaultValue("Standard Colors")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string StandardColors
        {
            get { return _standardColors; }
            set { _standardColors = value; }
        }
        #endregion

        #region ThemeColors
        /// <summary>
        /// Gets and sets the title for the theme colors section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for theme colors section of the color button menu.")]
        [DefaultValue("Theme Colors")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string ThemeColors
        {
            get { return _themeColors; }
            set { _themeColors = value; }
        }
        #endregion
    }
}
