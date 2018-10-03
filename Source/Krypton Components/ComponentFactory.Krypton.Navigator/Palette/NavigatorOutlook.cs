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
	/// Storage for outlook mode related properties.
	/// </summary>
    public class NavigatorOutlook : Storage
    {
        #region Static Fields
        private static readonly string _defaultMoreButtons = "Show &More Buttons";
        private static readonly string _defaultFewerButtons = "Show Fe&wer Buttons";
        private static readonly string _defaultAddRemoveButtons = "&Add or Remove Buttons";
        #endregion

        #region Instance Fields
        private KryptonNavigator _navigator;
        private ButtonStyle _checkButtonStyle;
        private ButtonStyle _overflowButtonStyle;
        private PaletteBorderStyle _borderEdgeStyle;
        private ButtonOrientation _itemOrientation;
        private Orientation _orientation;
        private NavigatorOutlookFull _full;
        private NavigatorOutlookMini _mini;
        private InheritBool _headerSecondaryVisible;
        private string _textMoreButtons;
        private string _textFewerButtons;
        private string _textAddRemoveButtons;
        private bool _showDropDownButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorOutlook class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorOutlook(KryptonNavigator navigator,
                                NeedPaintHandler needPaint)
		{
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create compound objects
            _full = new NavigatorOutlookFull(navigator, needPaint);
            _mini = new NavigatorOutlookMini(navigator, needPaint);

            // Default values
            _checkButtonStyle = ButtonStyle.NavigatorStack;
            _overflowButtonStyle = ButtonStyle.NavigatorOverflow;
            _borderEdgeStyle = PaletteBorderStyle.ControlClient;
            _orientation = Orientation.Vertical;
            _itemOrientation = ButtonOrientation.Auto;
            _headerSecondaryVisible = InheritBool.False;
            _textMoreButtons = _defaultMoreButtons;
            _textFewerButtons = _defaultFewerButtons;
            _textAddRemoveButtons = _defaultAddRemoveButtons;
            _showDropDownButton = true;
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
                return (Full.IsDefault &&
                        Mini.IsDefault &&
                        (CheckButtonStyle == ButtonStyle.NavigatorStack) &&
                        (OverflowButtonStyle == ButtonStyle.NavigatorOverflow) &&
                        (BorderEdgeStyle == PaletteBorderStyle.ControlClient) &&
                        (Orientation == Orientation.Vertical) &&
                        (ItemOrientation == ButtonOrientation.Auto) &&
                        (HeaderSecondaryVisible == InheritBool.False) &&
                        (TextMoreButtons.Equals(_defaultMoreButtons)) &&
                        (TextFewerButtons.Equals(_defaultFewerButtons)) &&
                        (TextAddRemoveButtons.Equals(_defaultAddRemoveButtons)) &&
                        (ShowDropDownButton == true));
            }
        }
        #endregion

        #region Full
        /// <summary>
        /// Gets and sets settings appropriate for the Outlook - Full mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Settings for the Outlook - Full mode.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorOutlookFull Full
        {
            get { return _full; }
        }

        private bool ShouldSerializeFull()
        {
            return !_full.IsDefault;
        }
        #endregion

        #region Mini
        /// <summary>
        /// Gets and sets settings appropriate for the Outlook - Mini mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Settings for the Outlook - Mini mode.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorOutlookMini Mini
        {
            get { return _mini; }
        }

        private bool ShouldSerializeMini()
        {
            return !_mini.IsDefault;
        }
        #endregion

        #region CheckButtonStyle
        /// <summary>
        /// Gets and sets the check button style.
        /// </summary>
        [Category("Visuals")]
        [Description("Check button style.")]
        [DefaultValue(typeof(ButtonStyle), "NavigatorStack")]
        public ButtonStyle CheckButtonStyle
        {
            get { return _checkButtonStyle; }

            set
            {
                if (_checkButtonStyle != value)
                {
                    _checkButtonStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("CheckButtonStyleOutlook");
                }
            }
        }
        #endregion

        #region OverflowButtonStyle
        /// <summary>
        /// Gets and sets the outlook overflow button style.
        /// </summary>
        [Category("Visuals")]
        [Description("Outlook overflow button style.")]
        [DefaultValue(typeof(ButtonStyle), "NavigatorOverflow")]
        public ButtonStyle OverflowButtonStyle
        {
            get { return _overflowButtonStyle; }

            set
            {
                if (_overflowButtonStyle != value)
                {
                    _overflowButtonStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("OverflowButtonStyleOutlook");
                }
            }
        }
        #endregion

        #region BorderEdgeStyle
        /// <summary>
        /// Gets and sets the border edge style.
        /// </summary>
        [Category("Visuals")]
        [Description("Check button style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "ControlClient")]
        public PaletteBorderStyle BorderEdgeStyle
        {
            get { return _borderEdgeStyle; }

            set
            {
                if (_borderEdgeStyle != value)
                {
                    _borderEdgeStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("BorderEdgeStyleOutlook");
                }
            }
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the orientation for positioning stack and overflow items.
        /// </summary>
        [Category("Visuals")]
        [Description("Orientation for positioning stack and overflow items.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(Orientation), "Vertical")]
        public Orientation Orientation
        {
            get { return _orientation; }

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    _navigator.OnViewBuilderPropertyChanged("OrientationOutlook");
                }
            }
        }

        /// <summary>
        /// Resets the Orientation property to its default value.
        /// </summary>
        public void ResetOrientation()
        {
            Orientation = Orientation.Vertical;
        }
        #endregion

        #region ItemOrientation
        /// <summary>
        /// Gets and sets the orientation for positioning items in the stack.
        /// </summary>
        [Category("Visuals")]
        [Description("Orientation for positioning items in the stack.")]
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
                    _navigator.OnViewBuilderPropertyChanged("ItemOrientationOutlook");
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

        #region HeaderSecondaryVisible
        /// <summary>
        /// Gets and sets the secondary header visiblity when in Outlook mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Secondary header visiblity when in Outlook mode.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(InheritBool), "False")]
        public InheritBool HeaderSecondaryVisible
        {
            get { return _headerSecondaryVisible; }

            set
            {
                if (_headerSecondaryVisible != value)
                {
                    _headerSecondaryVisible = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderSecondaryVisibleOutlook");
                }
            }
        }

        /// <summary>
        /// Resets the HeaderSecondaryVisible property to its default value.
        /// </summary>
        public void ResetHeaderSecondaryVisible()
        {
            HeaderSecondaryVisible = InheritBool.False;
        }
        #endregion

        #region TextMoreButtons
        /// <summary>
        /// Gets and sets the text to use when asking if more buttons should be shown in Outlook mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use when asking if more buttons should be shown in Outlook mode.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Show &More Buttons")]
        [Localizable(true)]
        public string TextMoreButtons
        {
            get { return _textMoreButtons; }
            set { _textMoreButtons = value; }
        }

        /// <summary>
        /// Resets the TextMoreButtons property to its default value.
        /// </summary>
        public void ResetTextMoreButtons()
        {
            TextMoreButtons = _defaultMoreButtons;
        }
        #endregion

        #region TextFewerButtons
        /// <summary>
        /// Gets and sets the text to use when asking if fewer buttons should be shown in Outlook mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use when asking if fewer buttons should be shown in Outlook mode.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Show Fe&wer Buttons")]
        [Localizable(true)]
        public string TextFewerButtons
        {
            get { return _textFewerButtons; }
            set { _textFewerButtons = value; }
        }

        /// <summary>
        /// Resets the TextFewerButtons property to its default value.
        /// </summary>
        public void ResetTextFewerButtons()
        {
            TextFewerButtons = _defaultFewerButtons;
        }
        #endregion

        #region TextAddRemoveButtons
        /// <summary>
        /// Gets and sets the text to use when asking if buttons should be shown/hidden in Outlook mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use when asking if buttons should be shown/hidden in Outlook mode.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("&Add or Remove Buttons")]
        [Localizable(true)]
        public string TextAddRemoveButtons
        {
            get { return _textAddRemoveButtons; }
            set { _textAddRemoveButtons = value; }
        }

        /// <summary>
        /// Resets the TextAddRemoveButtons property to its default value.
        /// </summary>
        public void ResetTextAddRemoveButtons()
        {
            TextAddRemoveButtons = _defaultAddRemoveButtons;
        }
        #endregion

        #region ShowDropDownButton
        /// <summary>
        /// Gets and sets the visibility of the drop down button on the Outlook overflow bar.
        /// </summary>
        [Category("Visuals")]
        [Description("Visibility of the drop down button on the Outlook overflow bar.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool ShowDropDownButton
        {
            get { return _showDropDownButton; }

            set
            {
                if (_showDropDownButton != value)
                {
                    _showDropDownButton = value;
                    _navigator.OnViewBuilderPropertyChanged("ShowDropDownButtonOutlook");
                }
            }
        }

        /// <summary>
        /// Resets the ShowDropDownButton property to its default value.
        /// </summary>
        public void ResetShowDropDownButton()
        {
            ShowDropDownButton = true;
        }
        #endregion
    }
}
