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
    /// Draws the border around the quick access toolbar.
    /// </summary>
    internal class ViewDrawRibbonQATBorder  : ViewComposite
    {
        #region Static Fields
        private static readonly Padding _minibarBorderPaddingOverlap = new Padding(8, 2, 11, 2);
        private static readonly Padding _minibarBorderPaddingNoOverlap = new Padding(17, 2, 11, 2);
        private static readonly Padding _fullbarBorderPadding_2007 = new Padding(1, 3, 2, 2);
        private static readonly Padding _fullbarBorderPadding_2010 = new Padding(2);
        private static readonly Padding _noBorderPadding = new Padding(1, 0, 1, 0);
        private static readonly int QAT_BUTTON_WIDTH = 22;
        private static readonly int QAT_HEIGHT_MINI = 26;
        private static readonly int QAT_HEIGHT_FULL = 27;
        private static readonly int QAT_MINIBAR_LEFT = 6;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaintDelegate;
        private KryptonForm _kryptonForm;
        private IDisposable _memento;
        private bool _minibar;
        private bool _overlapAppButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonQATBorder class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        /// <param name="minibar">Minibar or full bar drawing.</param>
        public ViewDrawRibbonQATBorder(KryptonRibbon ribbon,
                                       NeedPaintHandler needPaintDelegate,
                                       bool minibar)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember incoming references
            _ribbon = ribbon;
            _needPaintDelegate = needPaintDelegate;
            _minibar = minibar;
            _overlapAppButton = true;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonQATBorder:" + Id;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region OwnerForm
        /// <summary>
        /// Gets and sets the associated form instance.
        /// </summary>
        public KryptonForm OwnerForm
        {
            get { return _kryptonForm; }
            set { _kryptonForm = value; }
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets the visible state of the element.
        /// </summary>
        public override bool Visible
        {
            get { return (_ribbon.Visible && base.Visible); }
            set { base.Visible = value; }
        }
        #endregion

        #region OverlapAppButton
        /// <summary>
        /// Should the element overlap the app button to the left.
        /// </summary>
        public bool OverlapAppButton
        {
            get { return _overlapAppButton; }
            set { _overlapAppButton = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get size of the contained items
            Size preferredSize = base.GetPreferredSize(context);

            // Add on the border padding
            preferredSize = CommonHelper.ApplyPadding(Orientation.Horizontal, preferredSize, BarPadding);
            preferredSize.Height = Math.Max(preferredSize.Height, BarHeight);

            // If we are inside the custom chrome area
            if (OwnerForm != null)
            {
                // Calculate the maximum width allowed
                int maxWidth = ((OwnerForm.Width - 100) / 3) * 2;

                // Adjust so the width is a multiple of a button size
                int buttons = (maxWidth - BarPadding.Horizontal) / QAT_BUTTON_WIDTH;
                maxWidth = (buttons * QAT_BUTTON_WIDTH) + BarPadding.Horizontal;

                preferredSize.Width = Math.Min(maxWidth, preferredSize.Width);
            }

            return preferredSize;
        }
            
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            Rectangle clientRect = context.DisplayRectangle;

            // For the minibar we have to position ourself at bottom of available area
            if (_minibar)
            {
                clientRect.Y = clientRect.Bottom - 1 - QAT_HEIGHT_MINI;
                clientRect.Height = QAT_HEIGHT_MINI;
            }

            ClientRectangle = clientRect;

            // Remove QAT border for positioning children
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, BarPadding);

            // Let children be layed out inside border area
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            // We never draw the background/border for Office 2010 shape QAT
            if (_minibar && (_ribbon.RibbonShape == PaletteRibbonShape.Office2010))
                return;

            IPaletteRibbonBack palette;
            PaletteState state = PaletteState.Normal;
            Rectangle drawRect = ClientRectangle;

            // Get the correct drawing palette
            if (_minibar)
            {
                if (Active)
                {
                    palette = _ribbon.StateCommon.RibbonQATMinibarActive;
                    if (!OverlapAppButton)
                        state = PaletteState.CheckedNormal;
                }
                else
                {
                    palette = _ribbon.StateCommon.RibbonQATMinibarInactive;
                    state = PaletteState.Disabled;
                }

                if (OverlapAppButton)
                {
                    // If we need to overlap the app button, then shift left
                    drawRect.X -= QAT_MINIBAR_LEFT;
                    drawRect.Width += QAT_MINIBAR_LEFT;
                }
                else
                {
                    // Otherwise shift right so there is a small gap on the left
                    drawRect.X += QAT_MINIBAR_LEFT;
                    drawRect.Width -= QAT_MINIBAR_LEFT;
                }
            }
            else
                palette = _ribbon.StateCommon.RibbonQATFullbar;

            // Decide if we need to draw onto a composition area
            bool composition = (OwnerForm != null) ? OwnerForm.ApplyComposition && OwnerForm.ApplyCustomChrome : false;

            // Perform actual drawing
            _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, state, palette, VisualOrientation.Top, composition, _memento);
        }
        #endregion

        #region Implementation
        private bool Active
        {
            get
            {
                if (OwnerForm != null)
                    return OwnerForm.WindowActive;
                else
                    return true;
            }
        }

        private Padding BarPadding
        {
            get
            {
                if (_minibar)
                {
                    if (_ribbon.RibbonShape == PaletteRibbonShape.Office2010)
                        return _noBorderPadding;
                    else
                    {
                        if (OverlapAppButton)
                            return _minibarBorderPaddingOverlap;
                        else
                            return _minibarBorderPaddingNoOverlap;
                    }
                }
                else
                {
                    if (_ribbon.RibbonShape == PaletteRibbonShape.Office2010)
                        return _fullbarBorderPadding_2010;
                    else
                        return _fullbarBorderPadding_2007;
                }
            }
        }

        private int BarHeight
        {
            get
            {
                if (_minibar)
                    return QAT_HEIGHT_MINI;
                else
                    return QAT_HEIGHT_FULL;
            }
        }
        #endregion
    }
}
