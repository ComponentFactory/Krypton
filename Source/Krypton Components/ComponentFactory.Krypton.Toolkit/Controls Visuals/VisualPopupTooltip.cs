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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Visual display of tooltip information.
    /// </summary>
    public class VisualPopupToolTip : VisualPopup
    {
        #region Static Fields
        private static readonly int VERT_OFFSET = 22;
        private static readonly int HORZ_OFFSET = 8;
        #endregion

        #region Instance Fields
        private PaletteTripleMetricRedirect _palette;
        private ViewDrawDocker _drawDocker;
        private ViewDrawContent _drawContent;
        private IContentValues _contentValues;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopupTooltip class.
        /// </summary>
        /// <param name="redirector">Redirector for recovering palette values.</param>
        /// <param name="contentValues">Source of content values.</param>
        /// <param name="renderer">Drawing renderer.</param>
        public VisualPopupToolTip(PaletteRedirect redirector,
                                  IContentValues contentValues,
                                  IRenderer renderer)
            : this(redirector, contentValues, renderer,
                   PaletteBackStyle.ControlToolTip,
                   PaletteBorderStyle.ControlToolTip,
                   PaletteContentStyle.LabelToolTip)
        {
        }
            
        /// <summary>
        /// Initialize a new instance of the VisualPopupTooltip class.
        /// </summary>
        /// <param name="redirector">Redirector for recovering palette values.</param>
        /// <param name="contentValues">Source of content values.</param>
        /// <param name="renderer">Drawing renderer.</param>
        /// <param name="backStyle">Style for the tooltip background.</param>
        /// <param name="borderStyle">Style for the tooltip border.</param>
        /// <param name="contentStyle">Style for the tooltip content.</param>
        public VisualPopupToolTip(PaletteRedirect redirector,
                                  IContentValues contentValues,
                                  IRenderer renderer,
                                  PaletteBackStyle backStyle,
                                  PaletteBorderStyle borderStyle,
                                  PaletteContentStyle contentStyle)
            : base(renderer, true)
        {
            Debug.Assert(contentValues != null);

            // Remember references needed later
            _contentValues = contentValues;

            // Create the triple redirector needed by view elements
            _palette = new PaletteTripleMetricRedirect(redirector, backStyle, borderStyle, contentStyle, NeedPaintDelegate);

            // Our view contains background and border with content inside
            _drawDocker = new ViewDrawDocker(_palette.Back, _palette.Border, null);
            _drawContent = new ViewDrawContent(_palette.Content, _contentValues, VisualOrientation.Top);
            _drawDocker.Add(_drawContent, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a value indicating if the keyboard is passed to this popup.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool KeyboardInert
        {
            get { return true; }
        }

        /// <summary>
        /// Should the mouse move at provided screen point be allowed.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to alow; otherwise false.</returns>
        public override bool AllowMouseMove(Message m, Point pt)
        {
            // We allow all mouse moves when we are showing
            return true;
        }

        /// <summary>
        /// Show the tooltip popup relative to the provided screen position.
        /// </summary>
        /// <param name="screenPt">Screen point of cursor.</param>
        public void ShowCalculatingSize(Point screenPt)
        {
            // Get the size the popup would like to be
            Size popupSize = ViewManager.GetPreferredSize(Renderer, Size.Empty);

            // Find the screen position the popup will be relative to
            Rectangle screenRect = new Rectangle(screenPt.X + HORZ_OFFSET - (popupSize.Width / 2),
                                                 screenPt.Y - VERT_OFFSET ,
                                                 1, VERT_OFFSET * 2);
            
            // Show it now!
            Show(screenRect, popupSize);
        }

        /// <summary>
        /// Show the tooltip popup relative to the provided screen position.
        /// </summary>
        /// <param name="screenRect">Screen position to display relative to.</param>
        public void ShowCalculatingSize(Rectangle screenRect)
        {
            // Get the size the popup would like to be
            Size popupSize = ViewManager.GetPreferredSize(Renderer, Size.Empty);

            // Find the screen position the popup will be relative to
            screenRect = new Rectangle(screenRect.X, screenRect.Y - VERT_OFFSET,
                                       screenRect.Width, screenRect.Height + (VERT_OFFSET * 2));

            // Show it now!
            Show(screenRect, popupSize);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Let base class calulcate fill rectangle
            base.OnLayout(levent);

            // Need a render context for accessing the renderer
            using (RenderContext context = new RenderContext(this, null, ClientRectangle, Renderer))
            {

                // Grab a path that is the outside edge of the border
                Rectangle borderRect = ClientRectangle;
                GraphicsPath borderPath1 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);
                borderRect.Inflate(-1, -1);
                GraphicsPath borderPath2 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);
                borderRect.Inflate(-1, -1);
                GraphicsPath borderPath3 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _palette.Border, VisualOrientation.Top, PaletteState.Normal);

                // Update the region of the popup to be the border path
                Region = new Region(borderPath1);

                // Inform the shadow to use the same paths for drawing the shadow
                DefineShadowPaths(borderPath1, borderPath2, borderPath3);
            }
        }
        #endregion
    }
}
