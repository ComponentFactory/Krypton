// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace CustomControlUsingRenderers
{
    public class MyUserControl : UserControl, 
                                 IContentValues
    {
        private VisualOrientation _orientation;
        private bool _mouseOver;
        private bool _mouseDown;
        private IPalette _palette;
        private PaletteRedirect _paletteRedirect;
        private PaletteBackInheritRedirect _paletteBack;
        private PaletteBorderInheritRedirect _paletteBorder;
        private PaletteContentInheritRedirect _paletteContent;
        private IDisposable _mementoContent;
        private IDisposable _mementoBack1;
        private IDisposable _mementoBack2;

        public MyUserControl()
        {
            // To remove flicker we use double buffering for drawing
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
                    
            // Cache the current global palette setting
            _palette = KryptonManager.CurrentGlobalPalette;

            // Hook into palette events
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            // We want to be notified whenever the global palette changes
            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            // Create redirection object to the base palette
            _paletteRedirect = new PaletteRedirect(_palette);

            // Create accessor objects for the back, border and content
            _paletteBack = new PaletteBackInheritRedirect(_paletteRedirect);
            _paletteBorder = new PaletteBorderInheritRedirect(_paletteRedirect);
            _paletteContent = new PaletteContentInheritRedirect(_paletteRedirect);
        }

        public VisualOrientation Orientation
        {
            get { return _orientation; }

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    PerformLayout();
                    Invalidate();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoContent != null)
                {
                    _mementoContent.Dispose();
                    _mementoContent = null;
                }

                if (_mementoBack1 != null)
                {
                    _mementoBack1.Dispose();
                    _mementoBack1 = null;
                }

                if (_mementoBack2 != null)
                {
                    _mementoBack2.Dispose();
                    _mementoBack2 = null;
                }

                // Unhook from the palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);
                    _palette = null;
                }

                // Unhook from the static events, otherwise we cannot be garbage collected
                KryptonManager.GlobalPaletteChanged -= new EventHandler(OnGlobalPaletteChanged);
            }

            base.Dispose(disposing);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseOver = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseDown = (e.Button == MouseButtons.Left);
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseDown = _mouseDown && (e.Button != MouseButtons.Left);
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseOver = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_palette != null)
            {
                // We want the inner part of the control to act like a button, so 
                // we need to find the correct palette state based on if the mouse 
                // is over the control and currently being pressed down or not.
                PaletteState buttonState = GetButtonState();

                // Create a rectangle inset, this is where we will draw a button
                Rectangle innerRect = ClientRectangle;
                innerRect.Inflate(-20, -20);

                // Get the renderer associated with this palette
                IRenderer renderer = _palette.GetRenderer();

                // Create a layout context used to allow the renderer to layout the content
                using (ViewLayoutContext viewContext = new ViewLayoutContext(this, renderer))
                {
                    // Setup the appropriate style for the content
                    _paletteContent.Style = PaletteContentStyle.ButtonStandalone;

                    // Cleaup resources by disposing of old memento instance
                    if (_mementoContent != null)
                        _mementoContent.Dispose();

                    // Ask the renderer to work out how the Content values will be layed out and
                    // return a memento object that we cache for use when actually performing painting
                    _mementoContent = renderer.RenderStandardContent.LayoutContent(viewContext, innerRect,  _paletteContent, 
                                                                                   this, Orientation, buttonState, false);
                }
            }

            base.OnLayout(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_palette != null)
            {
                // Get the renderer associated with this palette
                IRenderer renderer = _palette.GetRenderer();

                // Create the rendering context that is passed into all renderer calls
                using (RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, renderer))
                {
                    /////////////////////////////////////////////////////////////////////////////////
                    // We want to draw the background of the entire control over the entire client //
                    // area. In this example we are using a background style of HeaderPrimary      //
                    /////////////////////////////////////////////////////////////////////////////////
                    using (GraphicsPath path = CreateRectGraphicsPath(ClientRectangle))
                    {
                        // Set the style we want picked up from the base palette
                        _paletteBack.Style = PaletteBackStyle.HeaderPrimary;

                        // Ask renderer to draw the background
                        _mementoBack1 = renderer.RenderStandardBack.DrawBack(renderContext, ClientRectangle, path, _paletteBack, Orientation,
                                                                             Enabled ? PaletteState.Normal : PaletteState.Disabled, _mementoBack1);
                    }

                    // We want the inner part of the control to act like a button, so 
                    // we need to find the correct palette state based on if the mouse 
                    // is over the control if the mouse button is pressed down or not.
                    PaletteState buttonState = GetButtonState();

                    // Create a rectangle inset, this is where we will draw a button
                    Rectangle innerRect = ClientRectangle;
                    innerRect.Inflate(-20, -20);

                    // Set the style of button we want to draw
                    _paletteBack.Style = PaletteBackStyle.ButtonStandalone;
                    _paletteBorder.Style = PaletteBorderStyle.ButtonStandalone;
                    _paletteContent.Style = PaletteContentStyle.ButtonStandalone;

                    // Do we need to draw the background?
                    if (_paletteBack.GetBackDraw(buttonState) == InheritBool.True)
                    {
                        //////////////////////////////////////////////////////////////////////////////////
                        // In case the border has a rounded effect we need to get the background path   //
                        // to draw from the border part of the renderer. It will return a path that is  //
                        // appropriate for use drawing within the border settings.                      //
                        //////////////////////////////////////////////////////////////////////////////////
                        using (GraphicsPath path = renderer.RenderStandardBorder.GetBackPath(renderContext,
                                                                                             innerRect,
                                                                                             _paletteBorder,
                                                                                             Orientation,
                                                                                             buttonState))
                        {
                            // Ask renderer to draw the background
                            _mementoBack2 = renderer.RenderStandardBack.DrawBack(renderContext, innerRect, path, _paletteBack,
                                                                                 Orientation, buttonState, _mementoBack2);
                        }
                    }

                    // Do we need to draw the border?
                    if (_paletteBorder.GetBorderDraw(buttonState) == InheritBool.True)
                    {
                        // Now we draw the border of the inner area, also in ButtonStandalone style
                        renderer.RenderStandardBorder.DrawBorder(renderContext, innerRect, _paletteBorder, Orientation, buttonState);
                    }

                    // Do we need to draw the content?
                    if (_paletteContent.GetContentDraw(buttonState) == InheritBool.True)
                    {
                        // Last of all we draw the content over the top of the border and background
                        renderer.RenderStandardContent.DrawContent(renderContext, innerRect, 
                                                                   _paletteContent, _mementoContent, 
                                                                   Orientation, buttonState, false, true);
                    }
                }
            }

            base.OnPaint(e);
        }

        private PaletteState GetButtonState()
        {
            // Find the correct state when getting button values
            if (!Enabled)
                return PaletteState.Disabled;
            else
            {
                if (_mouseOver)
                {
                    if (_mouseDown)
                        return PaletteState.Pressed;
                    else
                        return PaletteState.Tracking;
                }
                else
                    return PaletteState.Normal;
            }
        }

        private GraphicsPath CreateRectGraphicsPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            return path;
        }

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // Unhook events from old palette
            if (_palette != null)
                _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            // Cache the new IPalette that is the global palette
            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect.Target = _palette;

            // Hook into events for the new palette
            if (_palette != null)
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            // Change of palette means we should repaint to show any changes
            Invalidate();
        }

        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // Palette indicates we might need to repaint, so lets do it
            Invalidate();
        }

        #region IContentValues
        public Image GetImage(PaletteState state)
        {
            return global::CustomControlUsingRenderers.Properties.Resources.wizard;
        }

        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        public string GetLongText()
        {
            return "Click me!";
        }

        public string GetShortText()
        {
            return string.Empty;
        }
        #endregion
    }
}
