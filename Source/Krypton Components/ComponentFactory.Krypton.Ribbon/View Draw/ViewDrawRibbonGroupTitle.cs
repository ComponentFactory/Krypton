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
	/// Draws the title string for a group.
	/// </summary>
    internal class ViewDrawRibbonGroupTitle : ViewLeaf,
                                              IContentValues
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroup _ribbonGroup;
        private RibbonGroupTextToContent _contentProvider;
        private IDisposable _memento;
        private int _height;
        private Rectangle _displayRect;
        private int _dirtyPaletteLayout;
        private PaletteState _cacheState;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupTitle class.
		/// </summary>
        /// <param name="ribbon">Source ribbon control.</param>
        /// <param name="ribbonGroup">Ribbon group to display title for.</param>
        public ViewDrawRibbonGroupTitle(KryptonRibbon ribbon,
                                        KryptonRibbonGroup ribbonGroup)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);

            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;

            // Use a class to convert from ribbon group to content interface
            _contentProvider = new RibbonGroupTextToContent(ribbon.StateCommon.RibbonGeneral,
                                                            ribbon.StateNormal.RibbonGroupNormalTitle);
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupTitle:" + Id;
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
            _dirtyPaletteLayout = 0;
        }
        #endregion

        #region PaletteRibbonGroup
        /// <summary>
        /// Gets and sets the ribbon group palette to use.
        /// </summary>
        public IPaletteRibbonText PaletteRibbonGroup
        {
            get { return _contentProvider.PaletteRibbonGroup; }
            set { _contentProvider.PaletteRibbonGroup = value; }
        }
        #endregion

        #region Height
        /// <summary>
        /// Gets and sets the height of the title string.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return new Size(0, _height);
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

                // Use the renderer to layout the text
                _memento = context.Renderer.RenderStandardContent.LayoutContent(context, ClientRectangle,
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
            // Use renderer to draw the text content
            if (_memento != null)
                context.Renderer.RenderStandardContent.DrawContent(context, ClientRectangle,
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
            if (!string.IsNullOrEmpty(_ribbonGroup.TextLine2))
                return _ribbonGroup.TextLine1 + " " + _ribbonGroup.TextLine2;
            else
                return _ribbonGroup.TextLine1;
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
