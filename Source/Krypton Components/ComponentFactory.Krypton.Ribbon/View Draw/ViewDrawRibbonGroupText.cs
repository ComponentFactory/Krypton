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
	/// Draws the text string for a group.
	/// </summary>
    internal class ViewDrawRibbonGroupText : ViewLeaf,
                                             IContentValues
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroup _ribbonGroup;
        private RibbonGroupTextToContent _contentProvider;
        private IDisposable _memento;
        private bool _firstText;
        private int _heightExtra;
        private Size _preferredSize;
        private Rectangle _displayRect;
        private int _dirtyPaletteSize;
        private int _dirtyPaletteLayout;
        private PaletteState _cacheState;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupText class.
		/// </summary>
        /// <param name="ribbon">Source ribbon control.</param>
        /// <param name="ribbonGroup">Ribbon group to display title for.</param>
        /// <param name="firstText">Should show the first group text.</param>
        public ViewDrawRibbonGroupText(KryptonRibbon ribbon,
                                       KryptonRibbonGroup ribbonGroup,
                                       bool firstText)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);

            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;
            _firstText = firstText;

            // Use a class to convert from ribbon group to content interface
            _contentProvider = new RibbonGroupTextToContent(ribbon.StateCommon.RibbonGeneral,
                                                            ribbon.StateNormal.RibbonGroupCollapsedText);
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupText:" + Id;
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

        #region MakeDirty
        /// <summary>
        /// Make dirty so cached values are not used.
        /// </summary>
        public void MakeDirty()
        {
            _dirtyPaletteSize = 0;
            _dirtyPaletteLayout = 0;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // A change in state always causes a size and layout calculation
            if (_cacheState != State)
            {
                MakeDirty();
                _cacheState = State;
            }

            // If the palette has changed since we last calculated
            if (_ribbon.DirtyPaletteCounter != _dirtyPaletteSize)
            {
                // Ask the renderer for the contents preferred size
                _preferredSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _contentProvider, this,
                                                                                                VisualOrientation.Top,
                                                                                                PaletteState.Normal, false);

                // Subtract the extra space used to ensure it draws
                _heightExtra = (_ribbon.CalculatedValues.DrawFontHeight - _ribbon.CalculatedValues.RawFontHeight) * 2;
                _preferredSize.Height -= _heightExtra;

                // Cached value is valid till dirty palette noticed
                _dirtyPaletteSize = _ribbon.DirtyPaletteCounter;
            }

            return _preferredSize;
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

            // A change in state always causes a size and layout calculation
            if (_cacheState != State)
            {
                MakeDirty();
                _cacheState = State;
            }

            // Do we need to actually perform the relayout?
            if ((_displayRect != ClientRectangle) ||
                (_ribbon.DirtyPaletteCounter != _dirtyPaletteLayout))
            {
                // Remember to dispose of old memento
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }

                Rectangle drawRect = ClientRectangle;

                // Adjust the client rect so the text has enough room to be drawn
                drawRect.Height += _heightExtra;
                drawRect.Y -= (_heightExtra / 2);

                // Use the renderer to layout the text
                _memento = context.Renderer.RenderStandardContent.LayoutContent(context, drawRect,
                                                                                _contentProvider, this,
                                                                                VisualOrientation.Top,
                                                                                PaletteState.Normal, false);
                // Cache values that are needed to decide if layout is needed
                _displayRect = ClientRectangle;
                _dirtyPaletteLayout = _ribbon.DirtyPaletteCounter;
            }
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            Rectangle drawRect = ClientRectangle;

            // Adjust the client rect so the text has enough room to be drawn
            drawRect.Height += _heightExtra;
            drawRect.Y -= (_heightExtra / 2);

            // Use renderer to draw the text content
            if (_memento != null)
                context.Renderer.RenderStandardContent.DrawContent(context, drawRect,
                                                                   _contentProvider, _memento,
                                                                   VisualOrientation.Top,
                                                                   PaletteState.Normal, false, true);
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the image used for the ribbon tab.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Image.</returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image color that should be interpreted as transparent.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Transparent Color.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetShortText()
        {
            if (_firstText)
                return _ribbonGroup.TextLine1;
            else if (!string.IsNullOrEmpty(_ribbonGroup.TextLine2))
                return _ribbonGroup.TextLine2;
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the long text used as the secondary ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion
    }
}
