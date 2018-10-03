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

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Storage for bar related properties.
	/// </summary>
    public class NavigatorBar : Storage
    {
        #region Static Fields
        private const int _defaultBarMinimumHeight = 21;
        private const int _defaultBarFirstItemInset = 0;
        private const int _defaultBarLastItemInset = 0;
        private static readonly Size _defaultItemMinimumSize = new Size(20, 20);
        private static readonly Size _defaultItemMaximumSize = new Size(200, 200);
        #endregion

        #region Instance Fields
        private KryptonNavigator _navigator;
        private BarMultiline _barMultiline;
        private bool _barAnimation;
        private VisualOrientation _barOrientation;
        private int _barMinimumHeight;
        private int _barFirstItemInset;
        private int _barLastItemInset;
        private ButtonStyle _checkButtonStyle;
        private TabStyle _tabStyle;
        private TabBorderStyle _tabBorderStyle;
        private RelativePositionAlign _itemAlignment;
        private Size _itemMinimumSize;
        private Size _itemMaximumSize;
        private ButtonOrientation _itemOrientation;
        private BarItemSizing _itemSizing;
        private MapKryptonPageText _barMapText;
        private MapKryptonPageText _barMapExtraText;
        private MapKryptonPageImage _barMapImage;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorBar class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorBar(KryptonNavigator navigator,
                            NeedPaintHandler needPaint)
		{
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            _barAnimation = true;
            _barFirstItemInset = 0;
            _barLastItemInset = 0;
            _barOrientation = VisualOrientation.Top;
            _barMinimumHeight = _defaultBarMinimumHeight;
            _barMultiline = BarMultiline.Singleline;
            _checkButtonStyle = ButtonStyle.Standalone;
            _tabStyle = TabStyle.HighProfile;
            _tabBorderStyle = TabBorderStyle.RoundedOutsizeMedium;
            _itemAlignment = RelativePositionAlign.Near;
            _itemMinimumSize = _defaultItemMinimumSize;
            _itemMaximumSize = _defaultItemMaximumSize;
            _itemOrientation = ButtonOrientation.Auto;
            _itemSizing = BarItemSizing.SameHeight;
            _barMapImage = MapKryptonPageImage.Small;
            _barMapText = MapKryptonPageText.TextTitle;
            _barMapExtraText = MapKryptonPageText.None;
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
                return ((CheckButtonStyle == ButtonStyle.Standalone) &&
                        (TabStyle == TabStyle.HighProfile) &&
                        (TabBorderStyle == TabBorderStyle.RoundedOutsizeMedium) &&
                        (BarFirstItemInset == _defaultBarFirstItemInset) &&
                        (BarLastItemInset == _defaultBarLastItemInset) &&
                        (BarMapImage == MapKryptonPageImage.Small) &&
                        (BarMapText == MapKryptonPageText.TextTitle) &&
                        (BarMapExtraText == MapKryptonPageText.None) &&
                        (BarOrientation == VisualOrientation.Top) &&
                        (ItemSizing == BarItemSizing.SameHeight) &&
                        (ItemMinimumSize == _defaultItemMinimumSize) &&
                        (ItemMaximumSize == _defaultItemMaximumSize) &&
                        (ItemOrientation == ButtonOrientation.Auto) &&
                        (ItemAlignment == RelativePositionAlign.Near) &&
                        (BarMinimumHeight == _defaultBarMinimumHeight) &&
                        (BarAnimation == true) &&
                        (BarMultiline == BarMultiline.Singleline));
            }
        }
        #endregion

        #region BarAnimation
        /// <summary>
        /// Gets and sets if animation should be used on the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Should animation effects be used on the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool BarAnimation
        {
            get { return _barAnimation; }

            set
            {
                if (_barAnimation != value)
                {
                    _barAnimation = value;
                    _navigator.OnViewBuilderPropertyChanged("BarAnimation");
                }
            }
        }

        /// <summary>
        /// Resets the BarAnimation property to its default value.
        /// </summary>
        public void ResetBarAnimation()
        {
            BarAnimation = true;
        }
        #endregion

        #region BarOrientation
        /// <summary>
        /// Gets and sets the orientation for positioning the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Orientation for positioning of the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation BarOrientation
        {
            get { return _barOrientation; }

            set
            {
                if (_barOrientation != value)
                {
                    _barOrientation = value;
                    _navigator.OnViewBuilderPropertyChanged("BarOrientation");
                }
            }
        }

        /// <summary>
        /// Resets the BarOrientation property to its default value.
        /// </summary>
        public void ResetBarOrientation()
        {
            BarOrientation = VisualOrientation.Top;
        }
        #endregion

        #region BarFirstItemInset
        /// <summary>
        /// Gets and sets the distance to inset the first bar item.
        /// </summary>
        [Category("Visuals")]
        [Description("Distance to inset the first bar item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(0)]
        public int BarFirstItemInset
        {
            get { return _barFirstItemInset; }

            set
            {
                if (_barFirstItemInset != value)
                {
                    _barFirstItemInset = value;
                    _navigator.OnViewBuilderPropertyChanged("BarFirstItemInset");
                }
            }
        }

        /// <summary>
        /// Resets the BarFirstItemInset property to its default value.
        /// </summary>
        public void ResetBarFirstItemInset()
        {
            BarFirstItemInset = _defaultBarFirstItemInset;
        }
        #endregion

        #region BarLastItemInset
        /// <summary>
        /// Gets and sets the distance to inset the last bar item.
        /// </summary>
        [Category("Visuals")]
        [Description("Distance to inset the last bar item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(0)]
        public int BarLastItemInset
        {
            get { return _barLastItemInset; }

            set
            {
                if (_barLastItemInset != value)
                {
                    _barLastItemInset = value;
                    _navigator.OnViewBuilderPropertyChanged("BarLastItemInset");
                }
            }
        }

        /// <summary>
        /// Resets the BarLastItemInset property to its default value.
        /// </summary>
        public void ResetBarLastItemInset()
        {
            BarLastItemInset = _defaultBarLastItemInset;
        }
        #endregion

        #region BarMinimumHeight
        /// <summary>
        /// Gets and sets the minimum height of the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum height of the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(21)]
        public int BarMinimumHeight
        {
            get { return _barMinimumHeight; }

            set
            {
                if (_barMinimumHeight != value)
                {
                    _barMinimumHeight = value;
                    _navigator.OnViewBuilderPropertyChanged("BarMinimumHeight");
                }
            }
        }

        /// <summary>
        /// Resets the BarMinimumHeight property to its default value.
        /// </summary>
        public void ResetBarMinimumHeight()
        {
            BarMinimumHeight = _defaultBarMinimumHeight;
        }
        #endregion

        #region BarMinimumHeight
        /// <summary>
        /// Gets and sets the showing of multilines of items in the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Multiline items in the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(BarMultiline), "Singleline")]
        public BarMultiline BarMultiline
        {
            get { return _barMultiline; }

            set
            {
                if (_barMultiline != value)
                {
                    _barMultiline = value;
                    _navigator.OnViewBuilderPropertyChanged("BarMultiline");
                }
            }
        }

        /// <summary>
        /// Resets the BarMultiline property to its default value.
        /// </summary>
        public void ResetBarMultiline()
        {
            BarMultiline = BarMultiline.Singleline;
        }
        #endregion

        #region CheckButtonStyle
        /// <summary>
        /// Gets and sets the check button style.
        /// </summary>
        [Category("Visuals")]
        [Description("Check button style.")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle CheckButtonStyle
        {
            get { return _checkButtonStyle; }

            set
            {
                if (_checkButtonStyle != value)
                {
                    _checkButtonStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("CheckButtonStyleBar");
                }
            }
        }
        #endregion

        #region TabStyle
        /// <summary>
        /// Gets and sets the tab style.
        /// </summary>
        [Category("Visuals")]
        [Description("Tab style.")]
        [DefaultValue(typeof(TabStyle), "HighProfile")]
        public TabStyle TabStyle
        {
            get { return _tabStyle; }

            set
            {
                if (_tabStyle != value)
                {
                    _tabStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("TabStyleBar");
                }
            }
        }
        #endregion

        #region TabBorderStyle
        /// <summary>
        /// Gets and sets the tab border style.
        /// </summary>
        [Category("Visuals")]
        [Description("Tab border style.")]
        [DefaultValue(typeof(TabBorderStyle), "RoundedOutsizeMedium")]
        public TabBorderStyle TabBorderStyle
        {
            get { return _tabBorderStyle; }

            set
            {
                if (_tabBorderStyle != value)
                {
                    _tabBorderStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("TabBorderStyleBar");
                }
            }
        }
        #endregion

        #region ItemAlignment
        /// <summary>
        /// Gets and sets the alignment of items within the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Alignment of items within the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(RelativePositionAlign), "Near")]
        public RelativePositionAlign ItemAlignment
        {
            get { return _itemAlignment; }

            set
            {
                if (_itemAlignment != value)
                {
                    _itemAlignment = value;
                    _navigator.OnViewBuilderPropertyChanged("ItemAlignment");
                }
            }
        }

        /// <summary>
        /// Resets the ItemAlignment property to its default value.
        /// </summary>
        public void ResetItemAlignment()
        {
            ItemAlignment = RelativePositionAlign.Near;
        }
        #endregion

        #region ItemMinimumSize
        /// <summary>
        /// Gets the sets the minimum size of each bar item.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum size of each bar item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(Size), "20,20")]
        public Size ItemMinimumSize
        {
            get { return _itemMinimumSize; }

            set
            {
                if (_itemMinimumSize != value)
                {
                    // None of the minimum values can be less than 1
                    if (value.Width < 1) throw new ArgumentException("Width cannot be less than 1", "ItemMinimumSize");
                    if (value.Height < 1) throw new ArgumentException("Height cannot be less than 1", "ItemMinimumSize");

                    // Minimum value must be less than or equal to the maximum
                    if (value.Width > ItemMaximumSize.Width) throw new ArgumentException("Width cannot be greater than the ItemMaximumSize.Width", "ItemMinimumSize");
                    if (value.Height > ItemMaximumSize.Height) throw new ArgumentException("Height cannot be greater than the ItemMaximumSize.Height", "ItemMinimumSize");

                    _itemMinimumSize = value;
                    _navigator.OnViewBuilderPropertyChanged("ItemMinimumSize");
                }
            }
        }

        /// <summary>
        /// Reset the ItemMinimumSize to the default value.
        /// </summary>
        public void ResetItemMinimumSize()
        {
            ItemMinimumSize = _defaultItemMinimumSize;
        }
        #endregion

        #region ItemMaximumSize
        /// <summary>
        /// Gets the sets the minimum size of each bar item.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum size of each bar item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(Size), "200,200")]
        public Size ItemMaximumSize
        {
            get { return _itemMaximumSize; }

            set
            {
                if (_itemMaximumSize != value)
                {
                    // None of the maximum values can be less than 1
                    if (value.Width < 1) throw new ArgumentException("Width cannot be less than 1", "ItemMaximumSize");
                    if (value.Height < 1) throw new ArgumentException("Height cannot be less than 1", "ItemMaximumSize");

                    // Maximum value must be greater than or equal to the minimum
                    if (value.Width < ItemMinimumSize.Width) throw new ArgumentException("Width cannot be less than the ItemMinimumSize.Width", "ItemMaximumSize");
                    if (value.Height < ItemMinimumSize.Height) throw new ArgumentException("Height cannot be less than the ItemMinimumSize.Width", "ItemMaximumSize");

                    _itemMaximumSize = value;
                    _navigator.OnViewBuilderPropertyChanged("ItemMaximumSize");
                }
            }
        }

        /// <summary>
        /// Reset the ItemMaximumSize to the default value.
        /// </summary>
        public void ResetItemMaximumSize()
        {
            ItemMaximumSize = _defaultItemMaximumSize;
        }
        #endregion

        #region ItemOrientation
        /// <summary>
        /// Gets and sets the orientation for positioning items on the bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Orientation for positioning items on the bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(ButtonOrientation), "Auto")]
        public ButtonOrientation ItemOrientation
        {
            get { return _itemOrientation; }

            set
            {
                if (_itemOrientation != value)
                {
                    _itemOrientation = value;
                    _navigator.OnViewBuilderPropertyChanged("ItemOrientationBar");
                }
            }
        }

        /// <summary>
        /// Resets the ItemOrientation property to its default value.
        /// </summary>
        public void ResetItemOrientation()
        {
            ItemOrientation = ButtonOrientation.Auto;
        }
        #endregion

        #region ItemSizing
        /// <summary>
        /// Gets the sets how to calculate the size of each bar item.
        /// </summary>
        [Category("Visuals")]
        [Description("How to calculate the size of each bar item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(BarItemSizing), "All Same Height")]
        public BarItemSizing ItemSizing
        {
            get { return _itemSizing; }

            set
            {
                if (_itemSizing != value)
                {
                    _itemSizing = value;
                    _navigator.OnViewBuilderPropertyChanged("ItemSizing");
                }
            }
        }

        /// <summary>
        /// Reset the ItemSizing to the default value.
        /// </summary>
        public void ResetItemSizing()
        {
            ItemSizing = BarItemSizing.SameHeight;
        }
        #endregion

        #region BarMapImage
        /// <summary>
        /// Gets and sets the mapping used for the bar item image.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Mapping used for the bar item image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageImage), "Small")]
        public virtual MapKryptonPageImage BarMapImage
        {
            get { return _barMapImage; }

            set
            {
                if (_barMapImage != value)
                {
                    _barMapImage = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the BarMapImage property to its default value.
        /// </summary>
        public void ResetBarMapImage()
        {
            BarMapImage = MapKryptonPageImage.Small;
        }
        #endregion

        #region BarMapText
        /// <summary>
        /// Gets and sets the mapping used for the bar item text.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used for the bar item text.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageText), "Text - Title")]
        public MapKryptonPageText BarMapText
        {
            get { return _barMapText; }

            set
            {
                if (_barMapText != value)
                {
                    _barMapText = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the BarMapText property to its default value.
        /// </summary>
        public void ResetBarMapText()
        {
            BarMapText = MapKryptonPageText.TextTitle;
        }
        #endregion

        #region BarMapExtraText
        /// <summary>
        /// Gets and sets the mapping used for the bar item description.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used for the bar item description.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
        public MapKryptonPageText BarMapExtraText
        {
            get { return _barMapExtraText; }

            set
            {
                if (_barMapExtraText != value)
                {
                    _barMapExtraText = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the BarMapExtraText property to its default value.
        /// </summary>
        public void ResetBarMapExtraText()
        {
            BarMapExtraText = MapKryptonPageText.None;
        }
        #endregion
    }
}
