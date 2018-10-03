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
	/// View element that represents a single gallery item.
	/// </summary>
    internal class ViewDrawRibbonGalleryItem : ViewDrawButton,
                                               IContentValues
	{
		#region Instance Fields
        private KryptonGallery _gallery;
        private GalleryItemController _controller;
        private ImageList _imageList;
        private Image _image;
        private int _imageIndex;
        #endregion

		#region Identity
        /// <summary>
		/// Initialize a new instance of the ViewDrawButton class.
		/// </summary>
        /// <param name="gallery">Owning gallery instance.</param>
        /// <param name="palette">Palette used to recover values.</param>
        /// <param name="layout">Reference to item layout.</param>
        /// <param name="needPaint">Delegate for requesting paints.</param>
        public ViewDrawRibbonGalleryItem(KryptonGallery gallery,
                                         IPaletteTriple palette,
                                         ViewLayoutRibbonGalleryItems layout,
                                         NeedPaintHandler needPaint)
            : base(palette, palette, palette, palette, 
                   null, null, VisualOrientation.Top, false)
        {
            _gallery = gallery;

            // We provide the content for the button
            ButtonValues = this;

            // Need controller to handle tracking/pressing etc
            _controller = new GalleryItemController(this, layout, needPaint);
            _controller.Click += new MouseEventHandler(OnItemClick);
            MouseController = _controller;
            SourceController = _controller;
            KeyController = _controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
			return "ViewDrawRibbonGalleryItem:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_image != null)
                {
                    _image.Dispose();
                    _image = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Track
        /// <summary>
        /// Item is becoming tracked by the mouse.
        /// </summary>
        public void Track()
        {
            if (_gallery.TrackingIndex != _imageIndex)
                _gallery.SetTrackingIndex(_imageIndex, false);
        }
        #endregion

        #region Untrack
        /// <summary>
        /// Item is no longer tracked by the mouse.
        /// </summary>
        public void Untrack()
        {
            if (_gallery.TrackingIndex == _imageIndex)
                _gallery.SetTrackingIndex(-1, false);
        }
        #endregion

        #region ImageList
        /// <summary>
        /// Sets the image list to use for the source of the image.
        /// </summary>
        public ImageList ImageList
        {
            set 
            {
                if (_imageList != value)
                {
                    if (_image != null)
                    {
                        _image.Dispose();
                        _image = null;
                    }

                    _imageList = value;
                }
            }
        }
        #endregion

        #region ImageIndex
        /// <summary>
        /// Sets the index of the image to show.
        /// </summary>
        public int ImageIndex
        {
            set 
            {
                if (_imageIndex != value)
                {
                    if (_image != null)
                    {
                        _image.Dispose();
                        _image = null;
                    }

                    _imageIndex = value;
                }
            }
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);


            // If this item is being tracked, then show as tracking
            PaletteState tempState = ElementState;
            if (_gallery.TrackingIndex == _imageIndex)
            {
                switch(tempState)
                {
                    case PaletteState.Normal:
                        ElementState = PaletteState.Tracking;
                        break;
                    case PaletteState.CheckedNormal:
                        ElementState = PaletteState.CheckedTracking;
                        break;
                }
            }

            // Let base class draw using the temp state, then put back to original
            base.Render(context);
            ElementState = tempState;
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
           // Cache image so we do not copy it every time it is requested
           if ((_image == null) && (_imageList != null) && (_imageIndex >= 0))
                _image = _imageList.Images[_imageIndex];

            return _image;
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
        private void OnItemClick(object sender, MouseEventArgs e)
        {
            _gallery.SelectedIndex = _imageIndex;
        }
        #endregion
    }
}
