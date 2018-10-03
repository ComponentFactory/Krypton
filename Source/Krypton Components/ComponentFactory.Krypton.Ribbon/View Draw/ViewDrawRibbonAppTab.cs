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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws half of an application tab.
	/// </summary>
    internal class ViewDrawRibbonAppTab : ViewComposite, 
                                          IContentValues
    {
        #region Static Fields
        private static Padding _preferredBorder = new Padding(17, 4, 17, 3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private IDisposable[] _mementos;
        private PaletteRibbonGeneral _paletteGeneral;
        private ApplicationTabToContent _contentProvider;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppTab class.
		/// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        public ViewDrawRibbonAppTab(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);

            _ribbon = ribbon;
            _mementos = new IDisposable[4];

            // Use a class to convert from application tab to content interface
            _paletteGeneral = ribbon.StateCommon.RibbonGeneral;
            _contentProvider = new ApplicationTabToContent(ribbon, _paletteGeneral);

            // Create and add the draw content for display inside the tab
            Add(new ViewDrawContent(_contentProvider, this, VisualOrientation.Top));
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonAppTab:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementos != null)
                {
                    foreach (IDisposable memento in _mementos)
                        if (memento != null)
                            memento.Dispose();

                    _mementos = null;
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
            Debug.Assert(context != null);

            // Get base class calculated preferred size
            Size preferredSize = base.GetPreferredSize(context);

            // Add on the fixed border extra
            preferredSize.Width += _preferredBorder.Horizontal;
            preferredSize.Height += _preferredBorder.Vertical;

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
            base.Layout(context);
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            int memento;

            // Find the correct palette to use that matches the button state
            switch (State)
            {
                default:
                case PaletteState.Normal:
                    memento = 0;
                    break;
                case PaletteState.Tracking:
                    memento = 1;
                    break;
                case PaletteState.Tracking | PaletteState.FocusOverride:
                    memento = 2;
                    break;
                case PaletteState.Pressed:
                    memento = 3;
                    break;
            }

            // Draw the background
            _mementos[memento] = context.Renderer.RenderRibbon.DrawRibbonApplicationTab(_ribbon.RibbonShape, context, ClientRectangle, State, 
                                                                                        _ribbon.RibbonAppButton.AppButtonBaseColorDark,
                                                                                        _ribbon.RibbonAppButton.AppButtonBaseColorLight, 
                                                                                        _mementos[memento]);
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
            return _ribbon.RibbonAppButton.AppButtonText;
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
