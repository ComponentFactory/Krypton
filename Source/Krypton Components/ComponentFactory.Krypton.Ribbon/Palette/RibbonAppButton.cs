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
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Storage for application button related properties.
	/// </summary>
    public class RibbonAppButton : Storage
    {
        #region Static Fields
        private static readonly Image _defaultAppImage = Properties.Resources.AppButtonDefault;
        private static readonly string _defaultAppText = "File";
        private static readonly Color _defaultAppBaseColorDark = Color.FromArgb(31, 72, 161);
        private static readonly Color _defaultAppBaseColorLight = Color.FromArgb(84, 158, 243);
        #endregion

        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAppMenu instances.
        /// </summary>
        public class AppMenuButtonSpecCollection : ButtonSpecCollection<ButtonSpecAppMenu> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the AppMenuButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public AppMenuButtonSpecCollection(KryptonRibbon owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private Image _appButtonImage;
        private Image _appButtonToolTipImage;
        private string _appButtonToolTipTitle;
        private string _appButtonToolTipBody;
        private Color _appButtonToolTipImageTransparentColor;
        private KryptonContextMenuItems _appButtonMenuItems;
        private KryptonRibbonRecentDocCollection _appButtonRecentDocs;
        private LabelStyle _appButtonToolTipStyle;
        private AppMenuButtonSpecCollection _appButtonSpecs;
        private Size _appButtonMinRecentSize;
        private Size _appButtonMaxRecentSize;
        private bool _appButtonShowRecentDocs;
        private bool _appButtonVisible;
        private Color _appButtonBaseColorDark;
        private Color _appButtonBaseColorLight;
        private Color _appButtonTextColor;
        private string _appButtonText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonAppButton class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public RibbonAppButton(KryptonRibbon ribbon)
		{
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;

            // Default values
            _appButtonMenuItems = new KryptonContextMenuItems();
            _appButtonMenuItems.ImageColumn = false;
            _appButtonImage = _defaultAppImage;
            _appButtonSpecs = new AppMenuButtonSpecCollection(ribbon);
            _appButtonRecentDocs = new KryptonRibbonRecentDocCollection();
            _appButtonToolTipTitle = string.Empty;
            _appButtonToolTipBody = string.Empty;
            _appButtonToolTipImageTransparentColor = Color.Empty;
            _appButtonToolTipStyle = LabelStyle.SuperTip;
            _appButtonMinRecentSize = new Size(250, 250);
            _appButtonMaxRecentSize = new Size(350, 350);
            _appButtonShowRecentDocs = true;
            _appButtonVisible = true;
            _appButtonBaseColorDark = _defaultAppBaseColorDark;
            _appButtonBaseColorLight = _defaultAppBaseColorLight;
            _appButtonTextColor = Color.White;
            _appButtonText = _defaultAppText;
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
                return ((AppButtonImage == _defaultAppImage) &&
                        (AppButtonText == _defaultAppText) &&
                        (AppButtonBaseColorDark == _defaultAppBaseColorDark) &&
                        (AppButtonBaseColorLight == _defaultAppBaseColorLight) &&
                        (AppButtonTextColor == Color.White) &&
                        (AppButtonMenuItems.Count == 0) &&
                        (AppButtonRecentDocs.Count == 0) &&
                        AppButtonMinRecentSize.Equals(new Size(250, 250)) &&
                        AppButtonMaxRecentSize.Equals(new Size(350, 350)) &&
                        AppButtonShowRecentDocs &&
                        (AppButtonSpecs.Count == 0) &&
                        string.IsNullOrEmpty(AppButtonToolTipBody) &&
                        string.IsNullOrEmpty(AppButtonToolTipBody) &&
                        (AppButtonToolTipImage == null) &&
                        (AppButtonToolTipImageTransparentColor == Color.Empty) &&
                        (AppButtonToolTipStyle == LabelStyle.SuperTip) &&
                        AppButtonVisible);

            }
        }
        #endregion

        #region AppButtonImage
        /// <summary>
        /// Gets and sets the application button image.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Application button image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image AppButtonImage
        {
            get { return _appButtonImage; }

            set
            {
                if (_appButtonImage != value)
                {
                    _appButtonImage = value;

                    // Captin area is not created when property first set to default value
                    if (_ribbon.CaptionArea != null)
                        _ribbon.CaptionArea.AppButtonChanged();
                }
            }
        }

        private bool ShouldSerializeAppButtonImage()
        {
            return AppButtonImage != _defaultAppImage;
        }
        #endregion

        #region AppButtonBaseColorDark
        /// <summary>
        /// Gets and sets the darker base color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Darker base color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColorAttribute()]
        [DefaultValue(typeof(Color), "31, 72, 161")]
        public Color AppButtonBaseColorDark
        {
            get { return _appButtonBaseColorDark; }
            
            set 
            {
                if (_appButtonBaseColorDark != null)
                {
                    _appButtonBaseColorDark = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonBaseColorLight
        /// <summary>
        /// Gets and sets the lighter base color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Lighter base color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColorAttribute()]
        [DefaultValue(typeof(Color), "84, 158, 243")]
        public Color AppButtonBaseColorLight
        {
            get { return _appButtonBaseColorLight; }
            
            set 
            {
                if (_appButtonBaseColorLight != null)
                {
                    _appButtonBaseColorLight = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonTextColor
        /// <summary>
        /// Gets and sets the text color used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Text color used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColorAttribute()]
        [DefaultValue(typeof(Color), "White")]
        public Color AppButtonTextColor
        {
            get { return _appButtonTextColor; }
            
            set 
            {
                if (_appButtonTextColor != null)
                {
                    _appButtonTextColor = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonText
        /// <summary>
        /// Gets and sets the text used for drawing an Office 2010 style application button.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Text used for drawing an Office 2010 style application button.")]
        [KryptonDefaultColorAttribute()]
        [DefaultValue("File")]
        [Localizable(true)]
        public string AppButtonText
        {
            get { return _appButtonText; }
            
            set 
            {
                if (_appButtonText != null)
                {
                    _appButtonText = value;
                    _ribbon.PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region AppButtonContextMenu
        /// <summary>
        /// Gets and sets the context menu items for the application button.
        /// </summary>
        [Category("Values")]
        [Description("Context menu items for the application button.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        public virtual KryptonContextMenuItemCollection AppButtonMenuItems
        {
            get { return _appButtonMenuItems.Items; }
        }
        #endregion

        #region AppButtonRecentDocs
        /// <summary>
        /// Gets and sets the recent document entries for the application button.
        /// </summary>
        [Category("Values")]
        [Description("Recent document entries for the application buttton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("ComponentFactory.Krypton.Ribbon.KryptonRibbonRecentDocCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        public virtual KryptonRibbonRecentDocCollection AppButtonRecentDocs
        {
            get { return _appButtonRecentDocs; }
        }
        #endregion

        #region AppButtonMinRecentSize
        /// <summary>
        /// Gets and sets the minimum size of the recent documents area of the application button.
        /// </summary>
        [Category("Values")]
        [Description("Minimum size of the recent documents area of the application button.")]
        [DefaultValue(typeof(Size), "250,250")]
        public Size AppButtonMinRecentSize
        {
            get { return _appButtonMinRecentSize; }
            set { _appButtonMinRecentSize = value; }
        }
        #endregion

        #region AppButtonMaxRecentSize
        /// <summary>
        /// Gets and sets the maximum size of the recent documents area of the application button.
        /// </summary>
        [Category("Values")]
        [Description("Maximum size of the recent documents area of the application button.")]
        [DefaultValue(typeof(Size), "350,350")]
        public Size AppButtonMaxRecentSize
        {
            get { return _appButtonMaxRecentSize; }
            set { _appButtonMaxRecentSize = value; }
        }
        #endregion

        #region AppButtonSpecs
        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications for the app button context menu.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AppMenuButtonSpecCollection AppButtonSpecs
        {
            get { return _appButtonSpecs; }
        }
        #endregion

        #region AppButtonShowRecentDocs
        /// <summary>
        /// GGets and sets if the recent documents area should be shown in the application button.
        /// </summary>
        [Category("Visuals")]
        [Description("Determine if the recent documents area should be shown in the application button.")]
        [DefaultValue(true)]
        public bool AppButtonShowRecentDocs
        {
            get { return _appButtonShowRecentDocs; }
            set { _appButtonShowRecentDocs = value; }
        }
        #endregion

        #region AppButtonToolTipStyle
        /// <summary>
        /// Gets and sets the tooltip label style for the application button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the application button.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
        [Localizable(true)]
        public LabelStyle AppButtonToolTipStyle
        {
            get { return _appButtonToolTipStyle; }
            set { _appButtonToolTipStyle = value; }
        }
        #endregion

        #region AppButtonToolTipImage
        /// <summary>
        /// Gets and sets the image for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Display image associated ToolTip.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public Image AppButtonToolTipImage
        {
            get { return _appButtonToolTipImage; }
            set { _appButtonToolTipImage = value; }
        }
        #endregion

        #region AppButtonToolTipImageTransparentColor
        /// <summary>
        /// Gets and sets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Color to draw as transparent in the ToolTipImage.")]
        [KryptonDefaultColorAttribute()]
        [Localizable(true)]
        public Color AppButtonToolTipImageTransparentColor
        {
            get { return _appButtonToolTipImageTransparentColor; }
            set { _appButtonToolTipImageTransparentColor = value; }
        }
        #endregion

        #region AppButtonToolTipTitle
        /// <summary>
        /// Gets and sets the title text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Title text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string AppButtonToolTipTitle
        {
            get { return _appButtonToolTipTitle; }
            set { _appButtonToolTipTitle = value; }
        }
        #endregion

        #region AppButtonToolTipBody
        /// <summary>
        /// Gets and sets the body text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Body text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string AppButtonToolTipBody
        {
            get { return _appButtonToolTipBody; }
            set { _appButtonToolTipBody = value; }
        }
        #endregion

        #region AppButtonVisible
        /// <summary>
        /// GGets and sets if the application button is shown.
        /// </summary>
        [Category("Visuals")]
        [Description("Determine if the application button is shown.")]
        [DefaultValue(true)]
        public bool AppButtonVisible
        {
            get { return _appButtonVisible; }
         
            set
            {
                if (_appButtonVisible != value)
                {
                    _appButtonVisible = value;

                    if (_ribbon.CaptionArea != null)
                    {
                        _ribbon.TabsArea.AppButtonVisibleChanged();
                        _ribbon.CaptionArea.AppButtonVisibleChanged();
                        _ribbon.CaptionArea.PerformFormChromeCheck();
                    }
                }
            }
        }
        #endregion
    }
}
