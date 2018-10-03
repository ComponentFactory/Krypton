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
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Redirects requests for drop down button images from the DropDownButtonImages instance.
	/// </summary>
    public class PaletteRedirectDropDownButton : PaletteRedirect
    {
        #region Instance Fields
        private DropDownButtonImages _images;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRedirectDropDownButton class.
		/// </summary>
        /// <param name="images">Reference to source of drop down button images.</param>
        public PaletteRedirectDropDownButton(DropDownButtonImages images)
            : this(null, images)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteRedirectDropDownButton class.
		/// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="images">Reference to source of drop down button images.</param>
        public PaletteRedirectDropDownButton(IPalette target,
                                             DropDownButtonImages images)
            : base(target)
		{
            Debug.Assert(images != null);

            // Remember incoming target
            _images = images;
		}
		#endregion

        #region Images
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            // Grab state specific image
            Image retImage = null;
            switch(state)
            {
                case PaletteState.Disabled:
                    retImage = _images.Disabled;
                    break;
                case PaletteState.Normal:
                    retImage = _images.Normal;
                    break;
                case PaletteState.Tracking:
                    retImage = _images.Tracking;
                    break;
                case PaletteState.Pressed:
                    retImage = _images.Pressed;
                    break;
            }

            // Not found, then get the common image
            if (retImage == null)
                retImage = _images.Common;

            // Not found, then inherit from target
            if (retImage == null)
                retImage = Target.GetDropDownButtonImage(state);

            return retImage;
        }
        #endregion
    }
}
