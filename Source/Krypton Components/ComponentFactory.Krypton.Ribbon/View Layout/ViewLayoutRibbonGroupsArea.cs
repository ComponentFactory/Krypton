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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Contains all the layout of the groups area.
    /// </summary>
    internal class ViewLayoutRibbonGroupsArea : ViewDrawPanel
    {
        #region Static Fields
        private static readonly Padding _preferredNormalPadding = new Padding(0, 0, 1, 0);
        private static readonly Padding _preferredMinimizedPadding = new Padding(0, 1, 1, 0);
        private static readonly Padding _layoutNormalPadding = new Padding(0, -1, 1, 1);
        private static readonly Padding _layoutMinimizedPadding = new Padding(0, 0, 1, 1);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewDrawRibbonGroupsBorderSynch _viewGroups;
        private PaletteBackInheritRedirect _backInherit;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGroupsArea class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="redirect">Reference to redirector for palette settings.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        public ViewLayoutRibbonGroupsArea(KryptonRibbon ribbon,
                                          PaletteRedirect redirect,
                                          NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(redirect != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember the incoming reference
            _ribbon = ribbon;

            // Create access to the redirector and use as our palette source
            _backInherit = new PaletteBackInheritRedirect(redirect, PaletteBackStyle.PanelClient);
            SetPalettes(_backInherit);

            // Create and add the only child we need, the groups area border element
            _viewGroups = new ViewDrawRibbonGroupsBorderSynch(ribbon, needPaintDelegate);
            Add(_viewGroups);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutRibbonGroupsArea:" + Id;
        }
        #endregion

        #region ViewGroups
        /// <summary>
        /// Gets access to the groups border view.
        /// </summary>
        public ViewDrawRibbonGroupsBorderSynch ViewGroups
        {
            get { return _viewGroups; }
        }
        #endregion

        #region BackStyle
        /// <summary>
        /// Gets and sets the background style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get { return _backInherit.Style; }
            set { _backInherit.Style = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get the preferred size of the contained content
            Size preferredSize = new Size(0, _ribbon.CalculatedValues.GroupsHeight);

            // Add on the padding we need around edges
            if (_ribbon.RealMinimizedMode)
                return new Size(preferredSize.Width + _preferredMinimizedPadding.Horizontal,
                                preferredSize.Height + _preferredMinimizedPadding.Vertical);
            else
                return new Size(preferredSize.Width + _preferredNormalPadding.Horizontal,
                                preferredSize.Height + _preferredNormalPadding.Vertical);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Find the correct padding to use
            Padding padding = (_ribbon.RealMinimizedMode ? _layoutMinimizedPadding : _layoutNormalPadding);

            // Reduce display rect by our border size
            context.DisplayRectangle = new Rectangle(ClientLocation.X + padding.Left,
                                                     ClientLocation.Y + padding.Top,
                                                     ClientWidth - padding.Horizontal,
                                                     ClientHeight - padding.Vertical);

            // Let contained content element be layed out
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
