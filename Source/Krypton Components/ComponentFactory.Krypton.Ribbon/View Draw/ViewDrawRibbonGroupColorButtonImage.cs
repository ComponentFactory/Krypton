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
	/// Draws either a large or small image from a group color button.
	/// </summary>
    internal class ViewDrawRibbonGroupColorButtonImage : ViewDrawRibbonGroupImageBase
                                              
    {
        #region Static Fields
        private static readonly Size _smallSize = new Size(16, 16);
        private static readonly Size _largeSize = new Size(32, 32);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupColorButton _ribbonColorButton;
        private bool _large;
        private Image _compositeImage;
        private Color _selectedColor;
        private Color _emptyBorderColor;
        private Rectangle _selectedRectSmall;
        private Rectangle _selectedRectLarge;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupColorButtonImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonColorButton">Reference to ribbon group color button definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupColorButtonImage(KryptonRibbon ribbon,
                                                   KryptonRibbonGroupColorButton ribbonColorButton,
                                                   bool large)
            : base(ribbon)
        {
            Debug.Assert(ribbonColorButton != null);
            _ribbonColorButton = ribbonColorButton;
            _selectedColor = ribbonColorButton.SelectedColor;
            _emptyBorderColor = ribbonColorButton.EmptyBorderColor;
            _selectedRectSmall = ribbonColorButton.SelectedRectSmall;
            _selectedRectLarge = ribbonColorButton.SelectedRectLarge;
            _large = large;
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupColorButtonImage:" + Id;
		}
        #endregion

        #region Public
        /// <summary>
        /// Notification that the selected color has changed.
        /// </summary>
        public void SelectedColorRectChanged()
        {
            // If we have a cache image we need to release it
            if (_compositeImage != null)
            {
                _compositeImage.Dispose();
                _compositeImage = null;
            }

            _emptyBorderColor = _ribbonColorButton.EmptyBorderColor;
            _selectedColor = _ribbonColorButton.SelectedColor;
            _selectedRectSmall = _ribbonColorButton.SelectedRectSmall;
            _selectedRectLarge = _ribbonColorButton.SelectedRectLarge;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize 
        {
            get
            {
                if (_large)
                    return _largeSize;
                else
                    return _smallSize;
            }
        }

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage 
        {
            get
            {
                Image newImage = null;
                if (_ribbonColorButton.KryptonCommand != null)
                {
                    if (_large)
                        newImage = _ribbonColorButton.KryptonCommand.ImageLarge;
                    else
                        newImage = _ribbonColorButton.KryptonCommand.ImageSmall;
                }
                else
                {
                    if (_large)
                        newImage = _ribbonColorButton.ImageLarge;
                    else
                        newImage = _ribbonColorButton.ImageSmall;
                }

                // Do we need to create another composite image?
                if ((newImage != null) && (_compositeImage == null))
                {
                    // Create a copy of the source image
                    Bitmap copyBitmap = new Bitmap(newImage);

                    // Paint over the image with a color indicator
                    using (Graphics g = Graphics.FromImage(copyBitmap))
                    {
                        Rectangle selectedRect = (_large ? _selectedRectLarge : _selectedRectSmall);

                        // If the color is not defined, i.e. it is empty then...
                        if (_selectedColor.Equals(Color.Empty))
                        {
                            // Indicate the absense of a color by drawing a border around 
                            // the selected color area, thus indicating the area inside the
                            // block is blank/empty.
                            using (Pen borderPen = new Pen(_emptyBorderColor))
                                g.DrawRectangle(borderPen, new Rectangle(selectedRect.X,
                                                                         selectedRect.Y,
                                                                         selectedRect.Width - 1,
                                                                         selectedRect.Height - 1));
                        }
                        else
                        {
                            // We have a valid selected color so draw a solid block of color
                            using (SolidBrush colorBrush = new SolidBrush(_selectedColor))
                                g.FillRectangle(colorBrush, selectedRect);
                        }
                    }

                    // Cache it for future use
                    _compositeImage = copyBitmap;
                }

                return _compositeImage;
            }
        }
        #endregion
    }
}
