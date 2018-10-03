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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Navigator view element for drawing a selected check button for the Outlook mini mode.
	/// </summary>
    internal class ViewDrawNavOutlookMini : ViewDrawNavCheckButtonBase
    {
        #region Instance Fields
        private OutlookMiniController _controller;
        private EventHandler _finishDelegate;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNavOutlookSelected class.
        /// </summary>
        /// <param name="navigator">Owning navigator instance.</param>
        /// <param name="page">Page this check button represents.</param>
        /// <param name="orientation">Orientation for the check button.</param>
        public ViewDrawNavOutlookMini(KryptonNavigator navigator,
                                      KryptonPage page,
                                      VisualOrientation orientation)
            : base(navigator, page, orientation,
                   navigator.StateDisabled.MiniButton,
                   navigator.StateNormal.MiniButton,
                   navigator.StateTracking.MiniButton,
                   navigator.StatePressed.MiniButton,
                   navigator.StateSelected.MiniButton,
                   navigator.OverrideFocus.MiniButton)

        {
            // Create the finish handler for when popup is removed
            _finishDelegate = new EventHandler(OnPopupFinished);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawNavOutlookMini:" + Id + " Text:" + Page.Text;
		}
		#endregion

        #region Page
        /// <summary>
        /// Gets the page this view represents.
        /// </summary>
        public override KryptonPage Page
        {
            set
            {
                base.Page = value;

                if (Page != null)
                {
                    _overrideDisabled.SetPalettes(Page.OverrideFocus.MiniButton, Page.StateDisabled.MiniButton);
                    _overrideNormal.SetPalettes(Page.OverrideFocus.MiniButton, Page.StateNormal.MiniButton);
                    _overrideTracking.SetPalettes(Page.OverrideFocus.MiniButton, Page.StateTracking.MiniButton);
                    _overridePressed.SetPalettes(Page.OverrideFocus.MiniButton, Page.StatePressed.MiniButton);
                    _overrideSelected.SetPalettes(Page.OverrideFocus.MiniButton, Page.StateSelected.MiniButton);
                }
                else
                {
                    _overrideDisabled.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateDisabled.MiniButton);
                    _overrideNormal.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateNormal.MiniButton);
                    _overrideTracking.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateTracking.MiniButton);
                    _overridePressed.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StatePressed.MiniButton);
                    _overrideSelected.SetPalettes(Navigator.OverrideFocus.MiniButton, Navigator.StateSelected.MiniButton);
                }
            }
        }
        #endregion

        #region AllowButtonSpecs
        /// <summary>
        /// Gets a value indicating if button specs are allowed on the button.
        /// </summary>
        public override bool AllowButtonSpecs
        {
            get { return false; }
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public override Image GetImage(PaletteState state)
        {
            if (Page != null)
                return Page.GetImageMapping(Navigator.Outlook.Mini.MiniMapImage);
            else
                return null;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetShortText()
        {
            if (Page != null)
                return Page.GetTextMapping(Navigator.Outlook.Mini.MiniMapText);
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetLongText()
        {
            if (Page != null)
                return Page.GetTextMapping(Navigator.Outlook.Mini.MiniMapExtraText);
            else
                return string.Empty;
        }
        #endregion

        #region CreateMouseController
        /// <summary>
        /// Create a mouse controller appropriate for operating this button.
        /// </summary>
        /// <returns>Reference to IMouseController interface.</returns>
        protected override IMouseController CreateMouseController()
        {
            _controller = new OutlookMiniController(this, new NeedPaintHandler(OnNeedPaint));
            _controller.Click += new EventHandler(OnMiniClick);
            return _controller;
        }
        #endregion

        #region Implementation
        private void OnMiniClick(object sender, EventArgs e)
        {
            // Ask the navigator to show the specified page as a popup window 
            // relative to our location as an element and firing the provided
            // delegate when the popup is dismissed.
            Navigator.ShowPopupPage(Page, this, _finishDelegate);
        }

        private void OnPopupFinished(object sender, EventArgs e)
        {
            // Remove the fixed display of the button, now the associated popup has been removed
            _controller.RemoveFixed();
        }
        #endregion
    }
}
