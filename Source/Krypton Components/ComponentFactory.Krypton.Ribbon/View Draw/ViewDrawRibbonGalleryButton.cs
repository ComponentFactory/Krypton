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
	/// Draws a gallery button with specified image.
	/// </summary>
    internal class ViewDrawRibbonGalleryButton : ViewLeaf, IContentValues
    {
        #region Instance Fields
        private IPalette _palette;
        private GalleryImages _images;
        private GalleryButtonController _controller;
        private PaletteRibbonGalleryButton _button;
        private PaletteBackToPalette _paletteBack;
        private PaletteBorderToPalette _paletteBorder;
        private PaletteContentToPalette _paletteContent;
        private PaletteRelativeAlign _alignment;
        private IDisposable _mementoBack;
        private IDisposable _mementoContent;
        private NeedPaintHandler _needPaint;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the mouse is used to left click the target.
        /// </summary>
        public event MouseEventHandler Click;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGalleryButton class.
		/// </summary>
        /// <param name="palette">Reference to inherited palette.</param>
        /// <param name="alignment">Button alignment within gallery.</param>
        /// <param name="button">Button content to display.</param>
        /// <param name="images">Button images.</param>
        /// <param name="needPaint">Paint event delegate.</param>
        public ViewDrawRibbonGalleryButton(IPalette palette,
                                           PaletteRelativeAlign alignment,
                                           PaletteRibbonGalleryButton button,
                                           GalleryImages images,
                                           NeedPaintHandler needPaint)
        {
            _palette = palette;
            _alignment = alignment;
            _button = button;
            _images = images;
            _needPaint = needPaint;
            _paletteBack = new PaletteBackToPalette(palette, PaletteBackStyle.ButtonGallery);
            _paletteBorder = new PaletteBorderToPalette(palette, PaletteBorderStyle.ButtonGallery);
            _paletteContent = new PaletteContentToPalette(palette, PaletteContentStyle.ButtonGallery);
            _controller = new GalleryButtonController(this, needPaint, (alignment != PaletteRelativeAlign.Far));
            _controller.Click += new MouseEventHandler(OnButtonClick);
            MouseController = _controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGalleryButton:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoBack != null)
                {
                    _mementoBack.Dispose();
                    _mementoBack = null;
                }

                if (_mementoContent != null)
                {
                    _mementoContent.Dispose();
                    _mementoContent = null;
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
            // Grab the required size for the content images
            return context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _paletteContent, 
                                                                                  this, VisualOrientation.Top,
                                                                                  State, false);
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

            // Dispose of any current memnto
            if (_mementoContent != null)
            {
                _mementoContent.Dispose();
                _mementoContent = null;
            }
            
            // Create new memento based on the new size and image
            _mementoContent = context.Renderer.RenderStandardContent.LayoutContent(context, ClientRectangle, 
                                                                                   _paletteContent, this, 
                                                                                   VisualOrientation.Top,
                                                                                   State, false);
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            // Reduce background to fit inside the border
            Rectangle backRect = ClientRectangle;
            backRect.Inflate(-1, -1);

            // If disabled then we need to reflect that immediately
            if (!Enabled)
                ElementState = PaletteState.Disabled;
            else if (ElementState == PaletteState.Disabled)
                ElementState = PaletteState.Normal;

            // Create border paths
            using (GraphicsPath borderPath = CreateBorderPath(ClientRectangle))
            {
                // Are we allowed to draw a background?
                if (_paletteBack.GetBackDraw(State) == InheritBool.True)
                {
                    _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, backRect, borderPath, _paletteBack,
                                                                                VisualOrientation.Top, State, _mementoBack);
                }

                // Are we allowed to draw the content?
                if (_paletteContent.GetContentDraw(State) == InheritBool.True)
                {
                    context.Renderer.RenderStandardContent.DrawContent(context, ClientRectangle, _paletteContent, 
                                                                       _mementoContent, VisualOrientation.Top, 
                                                                       State, false, false);
                }

                // Are we allowed to draw border?
                if (_paletteBorder.GetBorderDraw(State) == InheritBool.True)
                {
                    // Get the border color from palette
                    Color borderColor = _paletteBorder.GetBorderColor1(State);

                    // Draw the border last to overlap the background
                    using (AntiAlias aa = new AntiAlias(context.Graphics))
                        using (Pen borderPen = new Pen(borderColor))
                            context.Graphics.DrawPath(borderPen, borderPath);
                }
            }
        }
        #endregion

        #region ForceLeave
        /// <summary>
        /// Force the mouse to leave the button.
        /// </summary>
        public void ForceLeave()
        {
            _controller.ForceLeave();
        }
        #endregion

        #region Implementation
        private GraphicsPath CreateBorderPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();

            switch (_alignment)
            {
                case PaletteRelativeAlign.Near:
                    path.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top);
                    path.AddLine(rect.Left, rect.Top, rect.Right - 2, rect.Top);
                    path.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1);
                    path.AddLine(rect.Right - 1, rect.Top + 1, rect.Right - 1, rect.Bottom - 1);
                    path.AddLine(rect.Right - 1, rect.Bottom - 1, rect.Left, rect.Bottom - 1);
                    path.CloseFigure();
                    break;
                case PaletteRelativeAlign.Far:
                    path.AddLine(rect.Left, rect.Top, rect.Right - 1, rect.Top);
                    path.AddLine(rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 2);
                    path.AddLine(rect.Right - 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 1);
                    path.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Left, rect.Bottom - 1);
                    path.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top);
                    path.CloseFigure();
                    break;
                case PaletteRelativeAlign.Center:
                    path.AddLine(rect.Left, rect.Top, rect.Right - 1, rect.Top);
                    path.AddLine(rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
                    path.AddLine(rect.Right - 1, rect.Bottom - 1, rect.Left, rect.Bottom - 1);
                    path.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top);
                    path.CloseFigure();
                    break;
            }
            return path;
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetImage(PaletteState state)
        {
            // Find the correct collection of images
            GalleryButtonImages images = null;
            switch (_button)
            {
                case PaletteRibbonGalleryButton.Up:
                    images = _images.Up;
                    break;
                case PaletteRibbonGalleryButton.Down:
                    images = _images.Down;
                    break;
                case PaletteRibbonGalleryButton.DropDown:
                    images = _images.DropDown;
                    break;
            }

            // Get image based on state
            Image image = null;
            switch (State)
            {
                case PaletteState.Disabled:
                    image = images.Disabled;
                    break;
                case PaletteState.Normal:
                    image = images.Normal;
                    break;
                case PaletteState.Tracking:
                    image = images.Tracking;
                    break;
                case PaletteState.Pressed:
                    image = images.Pressed;
                    break;
            }

            // If no image then get the common image
            if (image == null)
                image = images.Common;

            // If still no image then get is from the palette
            if (image == null)
                return _palette.GetGalleryButtonImage(_button, State);
            else
                return image;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        public string GetShortText()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
        #endregion
    }
}
