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
    /// Draws the border around the groups inside the groups area.
    /// </summary>
    internal class ViewDrawRibbonGroupsBorder  : ViewComposite,
                                                 IPaletteRibbonBack
    {
        #region Static Fields
        private static readonly Padding _borderPadding2007 = new Padding(3, 3, 3, 2);
        private static readonly Padding _borderPadding2010 = new Padding(1, 1, 1, 3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaintDelegate;
        private IPaletteRibbonBack _inherit;
        private IDisposable _memento;
        private bool _borderOutside;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupsBorder class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="borderOutside">Should border be placed outside the contents.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        public ViewDrawRibbonGroupsBorder(KryptonRibbon ribbon,
                                          bool borderOutside,
                                          NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember incoming references
            _ribbon = ribbon;
            _needPaintDelegate = needPaintDelegate;
            _borderOutside = borderOutside;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonGroupsBorder:" + Id;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of old memento first
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region BorderPadding
        /// <summary>
        /// Gets the border padding applied to the view element.
        /// </summary>
        public Padding BorderPadding
        {
            get
            {
                if (_ribbon == null)
                    return Padding.Empty;

                switch (_ribbon.RibbonShape)
                {
                    default:
                    case PaletteRibbonShape.Office2007:
                        return _borderPadding2007;
                    case PaletteRibbonShape.Office2010:
                        return _borderPadding2010;
                }
            }
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

            // Do we need to add on our own border size
            if (!_borderOutside)
            {
                // Add on the border padding
                preferredSize = CommonHelper.ApplyPadding(Orientation.Horizontal, preferredSize, BorderPadding);
            }

            // Override the height to the correct fixed value
            preferredSize.Height = Ribbon.CalculatedValues.GroupsHeight;

            return preferredSize;
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

            // Do we need to add on our own border size
            if (!_borderOutside)
            {
                // Reduce the display rectangle by the border
                context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, context.DisplayRectangle, BorderPadding);
            }

            // Let children be layed out inside our space
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
            // If there is a selected tab and it is a context tab use the context specific palette
            if ((Ribbon.SelectedTab != null) &&
                (!string.IsNullOrEmpty(Ribbon.SelectedTab.ContextName)))
            {
                _inherit = _ribbon.StateContextCheckedNormal.RibbonGroupArea;
                ElementState = PaletteState.ContextCheckedNormal;
            }
            else
            {
                _inherit = _ribbon.StateCheckedNormal.RibbonGroupArea;
                ElementState = PaletteState.CheckedNormal;
            }

            Rectangle drawRect = ClientRectangle;

            // If we need to show the border outside of the client area?
            if (_borderOutside)
            {
                Padding borderPadding = BorderPadding;
                drawRect.X -= borderPadding.Left;
                drawRect.Y -= borderPadding.Top;
                drawRect.Width += borderPadding.Horizontal;
                drawRect.Height += borderPadding.Vertical;
            }
            else if ((_ribbon.CaptionArea.DrawCaptionOnComposition) && 
                     (_ribbon.RibbonShape == PaletteRibbonShape.Office2010))
            {
                // Prevent the left and right edges from being drawn
                drawRect.X -= 1;
                drawRect.Width += 2;
            }

            // Use renderer to draw the tab background
            _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, State, this, VisualOrientation.Top, false, _memento);
        }
        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            return _inherit.GetRibbonBackColorStyle(state);
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor1(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor1(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor2(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor2(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor3(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor3(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor4(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor4(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor5(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor5(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access the source ribbon control.
        /// </summary>
        protected KryptonRibbon Ribbon
        {
            get { return _ribbon; }
        }

        /// <summary>
        /// Gets access the paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            get { return _needPaintDelegate; }
        }
        #endregion

        #region Implementation
        private Color CheckForContextColor(PaletteState state)
        {
            // We need an associated ribbon tab
            if (Ribbon.SelectedTab != null)
            {
                // Does the ribbon tab have a context setting?
                if (!string.IsNullOrEmpty(Ribbon.SelectedTab.ContextName))
                {
                    // Find the context definition for this context
                    KryptonRibbonContext ribbonContext = Ribbon.RibbonContexts[Ribbon.SelectedTab.ContextName];

                    // Should always work, but you never know!
                    if (ribbonContext != null)
                    {
                        // Return the context specific color
                        return ribbonContext.ContextColor;
                    }
                }
            }

            return Color.Empty;
        }
        #endregion
    }
}
