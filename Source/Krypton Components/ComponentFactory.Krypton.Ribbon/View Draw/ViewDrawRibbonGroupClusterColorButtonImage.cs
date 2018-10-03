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
	/// Draws a small image from a group cluster color button.
	/// </summary>
    internal class ViewDrawRibbonGroupClusterColorButtonImage : ViewDrawRibbonGroupImageBase
                                              
    {
        #region Static Fields
        private static readonly Size _smallSize = new Size(16, 16);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupClusterColorButton _ribbonColorButton;
        private Image _compositeImage;
        private Color _selectedColor;
        private Color _emptyBorderColor;
        private Rectangle _selectedRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibViewDrawRibbonGroupClusterColorButtonImagebonGroupClusterButtonImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonColorButton">Reference to ribbon group color button definition.</param>
        public ViewDrawRibbonGroupClusterColorButtonImage(KryptonRibbon ribbon,
                                                          KryptonRibbonGroupClusterColorButton ribbonColorButton)
            : base(ribbon)
        {
            Debug.Assert(ribbonColorButton != null);
            _ribbonColorButton = ribbonColorButton;
            _selectedColor = ribbonColorButton.SelectedColor;
            _emptyBorderColor = ribbonColorButton.EmptyBorderColor;
            _selectedRect = ribbonColorButton.SelectedRect;
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupClusterColorButtonImage:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            SelectedColorRectChanged();
            base.Dispose(disposing);
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
            _selectedRect = _ribbonColorButton.SelectedRect;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize 
        {
            get { return _smallSize; }
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
                    newImage = _ribbonColorButton.KryptonCommand.ImageSmall;
                else
                    newImage = _ribbonColorButton.ImageSmall;
                    
                // Do we need to create another composite image?
                if ((newImage != null) && (_compositeImage == null))
                {
                    // Create a copy of the source image
                    Bitmap copyBitmap = new Bitmap(newImage);

                    // Paint over the image with a color indicator
                    using (Graphics g = Graphics.FromImage(copyBitmap))
                    {
                        // If the color is not defined, i.e. it is empty then...
                        if (_selectedColor.Equals(Color.Empty))
                        {
                            // Indicate the absense of a color by drawing a border around 
                            // the selected color area, thus indicating the area inside the
                            // block is blank/empty.
                            using(Pen borderPen = new Pen(_emptyBorderColor))
                                g.DrawRectangle(borderPen, new Rectangle(_selectedRect.X,
                                                                         _selectedRect.Y,
                                                                         _selectedRect.Width - 1,
                                                                         _selectedRect.Height - 1));
                        }
                        else
                        {
                            // We have a valid selected color so draw a solid block of color
                            using (SolidBrush colorBrush = new SolidBrush(_selectedColor))
                                g.FillRectangle(colorBrush, _selectedRect);
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
