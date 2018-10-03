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
	/// Draws a large image from a gallery.
	/// </summary>
    internal class ViewDrawRibbonGroupGalleryImage : ViewDrawRibbonGroupImageBase
                                              
    {
        #region Static Fields
        private static readonly Size _largeSize = new Size(32, 32);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupGallery _ribbonGallery;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupGalleryImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGallery">Reference to ribbon group gallery definition.</param>
        public ViewDrawRibbonGroupGalleryImage(KryptonRibbon ribbon,
                                               KryptonRibbonGroupGallery ribbonGallery)
            : base(ribbon)
        {
            Debug.Assert(ribbonGallery != null);

            _ribbonGallery = ribbonGallery;
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupGalleryImage:" + Id;
		}
        #endregion

        #region Protected
        /// <summary>
        /// Gets the size to draw the image.
        /// </summary>
        protected override Size DrawSize 
        {
            get { return _largeSize; }
        }

        /// <summary>
        /// Gets the image to be drawn.
        /// </summary>
        protected override Image DrawImage 
        {
            get { return _ribbonGallery.ImageLarge; }
        }
        #endregion
    }
}
