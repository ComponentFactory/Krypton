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
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Base class for implementation of 'Bar - Tab' modes.
	/// </summary>
    internal abstract class ViewBuilderBarTabBase : ViewBuilderBarItemBase
    {
        #region Public
        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            // Let base class perform common actions
            base.UpdateStatePalettes();

            // Base class sets metrics reference, but we want to override
            // this to be null so that the tab border gap is used instead
            _layoutBar.SetMetrics(null);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create a new check item with initial settings.
        /// </summary>
        /// <param name="page">Page for which the check button is to be created.</param>
        /// <param name="orientation">Initial orientation of the check button.</param>
        protected override INavCheckItem CreateCheckItem(KryptonPage page,
                                                         VisualOrientation orientation)
        {
            // Create a check button view element
            ViewDrawNavCheckButtonTab checkButton = new ViewDrawNavCheckButtonTab(Navigator, page, orientation);

            // Convert the button orientation to the appropriate visual orientations
            VisualOrientation orientBackBorder = ConvertButtonBorderBackOrientation();
            VisualOrientation orientContent = ConvertButtonContentOrientation();

            // Set the correct initial orientation of the button
            checkButton.SetOrientation(orientBackBorder, orientContent);

            // Draw the border in a tab style
            checkButton.DrawTabBorder = true;

            // Define the style of tab border
            checkButton.TabBorderStyle = Navigator.Bar.TabBorderStyle;

            return checkButton;
        }

        /// <summary>
        /// Gets the visual orientation of the check buttton.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected override VisualOrientation ConvertButtonBorderBackOrientation()
        {
            switch (Navigator.Bar.BarOrientation)
            {
                case VisualOrientation.Top:
                    return VisualOrientation.Top;
                case VisualOrientation.Bottom:
                    return VisualOrientation.Bottom;
                case VisualOrientation.Left:
                    if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                        (Navigator.RightToLeft == RightToLeft.Yes))
                        return VisualOrientation.Right;
                    else
                        return VisualOrientation.Left;
                case VisualOrientation.Right:
                    if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                        (Navigator.RightToLeft == RightToLeft.Yes))
                        return VisualOrientation.Left;
                    else
                        return VisualOrientation.Right;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return VisualOrientation.Top;
            }
        }

        /// <summary>
        /// Gets the visual orientation of the check butttons content.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected override VisualOrientation ConvertButtonContentOrientation()
        {
            return ResolveButtonContentOrientation(Navigator.Bar.BarOrientation);
        }

        /// <summary>
        /// Process the change in a property that might effect the view builder.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Property changed details.</param>
        protected override void OnViewBuilderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TabStyleBar":
                    UpdateTabStyle();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "TabBorderStyleBar":
                    UpdateTabBorderStyle();
                    Navigator.PerformNeedPaint(true);
                    break;
                default:
                    // We do not recognise the property, let base process it
                    base.OnViewBuilderPropertyChanged(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Update the state objects with the latest tab style.
        /// </summary>
        protected void UpdateTabStyle()
        {
            Navigator.StateCommon.Tab.SetStyles(Navigator.Bar.TabStyle);
            Navigator.OverrideFocus.Tab.SetStyles(Navigator.Bar.TabStyle);

            // Update each individual tab with the new style for remapping page level button specs
            if (PageLookup != null)
                foreach (KeyValuePair<KryptonPage, INavCheckItem> pair in PageLookup)
                {
                    ViewDrawNavCheckButtonTab tabHeader = (ViewDrawNavCheckButtonTab)pair.Value;
                    if (tabHeader.ButtonSpecManager != null)
                        tabHeader.ButtonSpecManager.SetRemapTarget(Navigator.Bar.TabStyle);
                }
        }
        #endregion

        #region Implementation
        private void UpdateTabBorderStyle()
        {
            // Cache the new border style
            TabBorderStyle tabBorderStyle = Navigator.Bar.TabBorderStyle;

            // Update the border style of each check button
            foreach (ViewDrawNavCheckButtonTab tab in _pageLookup.Values)
                tab.TabBorderStyle = tabBorderStyle;

            // Update border style used to space each tab item
            _layoutBar.TabBorderStyle = tabBorderStyle;
        }
        #endregion
    }
}
