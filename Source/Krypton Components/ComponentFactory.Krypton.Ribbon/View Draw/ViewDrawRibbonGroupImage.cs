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
	/// Draws the group image for a collapsed group.
	/// </summary>
    internal class ViewDrawRibbonGroupImage : ViewLeaf
                                              
    {
        #region Static Fields
        private static readonly Size _viewSize_2007 = new Size(30, 31);
        private static readonly Size _viewSize_2010 = new Size(31, 31);
        private static readonly Size _imageSize = new Size(16, 16);
        private static readonly int _imageOffsetX = 7;
        private static readonly int _imageOffsetY_2007 = 4;
        private static readonly int _imageOffsetY_2010 = 7;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroup _ribbonGroup;
        private ViewDrawRibbonGroup _viewGroup;
        private IDisposable _memento1;
        private IDisposable _memento2;
        private Size _viewSize;
        private int _offsetY;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGroup">Reference to ribbon group definition.</param>
        /// <param name="viewGroup">Reference to top level group element.</param>
        public ViewDrawRibbonGroupImage(KryptonRibbon ribbon,
                                        KryptonRibbonGroup ribbonGroup,
                                        ViewDrawRibbonGroup viewGroup)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);
            Debug.Assert(viewGroup != null);

            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;
            _viewGroup = viewGroup;
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupImage:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_memento1 != null)
                {
                    _memento1.Dispose();
                    _memento1 = null;
                }

                if (_memento2 != null)
                {
                    _memento2.Dispose();
                    _memento2 = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            switch (_ribbon.RibbonShape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    _viewSize = _viewSize_2007;
                    _offsetY = _imageOffsetY_2007;
                    break;
                case PaletteRibbonShape.Office2010:
                    _viewSize = _viewSize_2010;
                    _offsetY = _imageOffsetY_2010;
                    break;
            }

            return _viewSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Take on all the provided area
            ClientRectangle = context.DisplayRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            IPaletteRibbonBack paletteBorder;
            IPaletteRibbonBack paletteBack;

            // Are we a group inside a context tab?
            if ((_ribbon.SelectedTab != null) && !string.IsNullOrEmpty(_ribbon.SelectedTab.ContextName))
                ElementState = _viewGroup.Pressed ? PaletteState.Pressed : _viewGroup.Tracking ? PaletteState.ContextTracking : PaletteState.ContextNormal;
            else
                ElementState =  _viewGroup.Pressed ? PaletteState.Pressed : _viewGroup.Tracking ? PaletteState.Tracking : PaletteState.Normal;

            // Decide on the palette to use
            switch (State)
            {
                case PaletteState.Pressed:
                    paletteBorder = _ribbon.StatePressed.RibbonGroupCollapsedFrameBorder;
                    paletteBack = _ribbon.StatePressed.RibbonGroupCollapsedFrameBack;
                    break;
                case PaletteState.ContextNormal:
                    paletteBorder = _ribbon.StateContextNormal.RibbonGroupCollapsedFrameBorder;
                    paletteBack = _ribbon.StateContextNormal.RibbonGroupCollapsedFrameBack;
                    break;
                case PaletteState.ContextTracking:
                    paletteBorder = _ribbon.StateContextTracking.RibbonGroupCollapsedFrameBorder;
                    paletteBack = _ribbon.StateContextTracking.RibbonGroupCollapsedFrameBack;
                    break;
                case PaletteState.Tracking:
                    paletteBorder = _ribbon.StateTracking.RibbonGroupCollapsedFrameBorder;
                    paletteBack = _ribbon.StateTracking.RibbonGroupCollapsedFrameBack;
                    break;
                case PaletteState.Normal:
                default:
                    paletteBorder = _ribbon.StateNormal.RibbonGroupCollapsedFrameBorder;
                    paletteBack = _ribbon.StateNormal.RibbonGroupCollapsedFrameBack;
                    break;
            }

            // The background is slightly inside the rounded border
            Rectangle backRect = ClientRectangle;
            backRect.Inflate(-1, -1);

            // Draw the background for the group image area
            _memento1 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, backRect, State, paletteBack, VisualOrientation.Top, false, _memento1);

            // Draw the border around the group image area
            _memento2 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, ClientRectangle, State, paletteBorder, VisualOrientation.Top, false, _memento2);

            // If we have an image for drawing
            if (_ribbonGroup.Image != null)
            {
                // Determine the rectangle for the fixed size of image drawing
                Rectangle drawRect = new Rectangle(new Point(ClientLocation.X + _imageOffsetX,
                                                             ClientLocation.Y + _offsetY),
                                                   _imageSize);

                context.Graphics.DrawImage(_ribbonGroup.Image, drawRect);
            }
        }
        #endregion
    }
}
