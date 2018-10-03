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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Storage for string related properties.
	/// </summary>
    public class RibbonStrings : Storage
    {
        #region Static Fields
        private static readonly string _defaultAppButtonKeyTip = "F";
        private static readonly string _defaultCustomizeQuickAccessToolbar = "Customize Quick Access Toolbar";
        private static readonly string _defaultMinimize = "Mi&nimize the Ribbon";
        private static readonly string _defaultMoreColors = "&More Colors...";
        private static readonly string _defaultNoColor = "&No Color";
        private static readonly string _defaultRecentDocuments = "Recent Documents";
        private static readonly string _defaultRecentColors = "Recent Colors";
        private static readonly string _defaultShowQATAboveRibbon = "&Show Quick Access Toolbar Above the Ribbon";
        private static readonly string _defaultShowQATBelowRibbon = "&Show Quick Access Toolbar Below the Ribbon";
        private static readonly string _defaultShowAboveRibbon = "&Show Above the Ribbon";
        private static readonly string _defaultShowBelowRibbon = "&Show Below the Ribbon";
        private static readonly string _defaultStandardColors = "Standard Colors";
        private static readonly string _defaultThemeColors = "Theme Colors";
        #endregion

        #region Instance Fields
        private string _appButtonKeyTip;
        private string _customizeQuickAccessToolbar;
        private string _minimize;
        private string _moreColors;
        private string _noColor;
        private string _recentDocuments;
        private string _recentColors;
        private string _showAboveRibbon;
        private string _showBelowRibbon;
        private string _showQATAboveRibbon;
        private string _showQATBelowRibbon;
        private string _standardColors;
        private string _themeColors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonStrings class.
		/// </summary>
        public RibbonStrings()
		{
            // Default values
            _appButtonKeyTip = _defaultAppButtonKeyTip;
            _customizeQuickAccessToolbar = _defaultCustomizeQuickAccessToolbar;
            _minimize = _defaultMinimize;
            _moreColors = _defaultMoreColors;
            _noColor = _defaultNoColor;
            _recentDocuments = _defaultRecentDocuments;
            _recentColors = _defaultRecentColors;
            _showAboveRibbon = _defaultShowAboveRibbon;
            _showBelowRibbon = _defaultShowBelowRibbon;
            _showQATAboveRibbon = _defaultShowQATAboveRibbon;
            _showQATBelowRibbon = _defaultShowQATBelowRibbon;
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
                return (AppButtonKeyTip.Equals(_defaultAppButtonKeyTip)) &&
                       (CustomizeQuickAccessToolbar.Equals(_defaultCustomizeQuickAccessToolbar)) &&
                       (Minimize.Equals(_defaultMinimize)) &&
                       (MoreColors.Equals(_defaultMoreColors)) &&
                       (NoColor.Equals(_defaultNoColor)) &&
                       (RecentDocuments.Equals(_defaultRecentDocuments)) &&
                       (RecentColors.Equals(_defaultRecentColors)) &&
                       (ShowAboveRibbon.Equals(_defaultShowAboveRibbon)) &&
                       (ShowBelowRibbon.Equals(_defaultShowBelowRibbon)) &&
                       (ShowQATAboveRibbon.Equals(_defaultShowQATAboveRibbon)) &&
                       (ShowQATBelowRibbon.Equals(_defaultShowQATBelowRibbon)) &&
                       (StandardColors.Equals(_defaultStandardColors)) &&
                       (ThemeColors.Equals(_defaultThemeColors));
            }
        }
        #endregion

        #region AppButtonKeyTip
        /// <summary>
        /// Gets and sets the application button key tip string.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Application button key tip string.")]
        [DefaultValue("F")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string AppButtonKeyTip
        {
            get { return _appButtonKeyTip; }

            set
            {
                // We only allow uppercase strings of minimum 1 character length
                if (!string.IsNullOrEmpty(value))
                    _appButtonKeyTip = value.ToUpper();
            }
        }
        #endregion

        #region CustomizeQuickAccessToolbar
        /// <summary>
        /// Gets and sets the heading for the quick access toolbar menu.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Heading for quick access toolbar menu.")]
        [DefaultValue("Customize Quick Access Toolbar")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string CustomizeQuickAccessToolbar
        {
            get { return _customizeQuickAccessToolbar; }
            set { _customizeQuickAccessToolbar = value; }
        }
        #endregion

        #region Minimize
        /// <summary>
        /// Gets and sets the menu string for minimizing the ribbon option.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for minimizing the ribbon option.")]
        [DefaultValue("Mi&nimize the Ribbon")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Minimize
        {
            get { return _minimize; }
            set { _minimize = value; }
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

        #region RecentDocuments     
        /// <summary>
        /// Gets and sets the title for the recent documents section of the application menu.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Title for recent documents section of the application menu.")]
        [DefaultValue("Recent Documents")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string RecentDocuments
        {
            get { return _recentDocuments; }
            set { _recentDocuments = value; }
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

        #region ShowAboveRibbon
        /// <summary>
        /// Gets and sets the menu string for showing above the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing above the ribbon.")]
        [DefaultValue("&Show Above the Ribbon")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string ShowAboveRibbon
        {
            get { return _showAboveRibbon; }
            set { _showAboveRibbon = value; }
        }
        #endregion

        #region ShowBelowRibbon
        /// <summary>
        /// Gets and sets the menu string for showing below the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing below the ribbon.")]
        [DefaultValue("&Show Below the Ribbon")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string ShowBelowRibbon
        {
            get { return _showBelowRibbon; }
            set { _showBelowRibbon = value; }
        }
        #endregion

        #region ShowQATAboveRibbon
        /// <summary>
        /// Gets and sets the menu string for showing QAT above the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing QAT above the ribbon.")]
        [DefaultValue("&Show Quick Access Toolbar Above the Ribbon")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string ShowQATAboveRibbon
        {
            get { return _showQATAboveRibbon; }
            set { _showQATAboveRibbon = value; }
        }
        #endregion

        #region ShowQATBelowRibbon
        /// <summary>
        /// Gets and sets the menu string for showing QAT below the ribbon.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Menu string for showing QAT below the ribbon.")]
        [DefaultValue("&Show Quick Access Toolbar Below the Ribbon")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string ShowQATBelowRibbon
        {
            get { return _showQATBelowRibbon; }
            set { _showQATBelowRibbon = value; }
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
