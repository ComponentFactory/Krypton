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
	/// Redirects requests for radio button images from the RadioButtonImages instance.
	/// </summary>
    public class PaletteRedirectRadioButton : PaletteRedirect
    {
        #region Instance Fields
        private RadioButtonImages _images;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRedirectRadioButton class.
		/// </summary>
        /// <param name="images">Reference to source of radio button images.</param>
        public PaletteRedirectRadioButton(RadioButtonImages images)
            : this(null, images)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRadioButton class.
		/// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="images">Reference to source of radio button images.</param>
        public PaletteRedirectRadioButton(IPalette target,
                                          RadioButtonImages images)
            : base(target)
		{
            Debug.Assert(images != null);

            // Remember incoming target
            _images = images;
		}
		#endregion

        #region Images
        /// <summary>
        /// Gets a radio button image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the radio button enabled.</param>
        /// <param name="checkState">Is the radio button checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the radio button being hot tracked.</param>
        /// <param name="pressed">Is the radio button being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetRadioButtonImage(bool enabled, 
                                                  bool checkState, 
                                                  bool tracking, 
                                                  bool pressed)
        {
            Image retImage = null;

            if (checkState)
            {
                if (!enabled)
                    retImage = _images.CheckedDisabled;
                else if (pressed)
                    retImage = _images.CheckedPressed;
                else if (tracking)
                    retImage = _images.CheckedTracking;
                else
                    retImage = _images.CheckedNormal;
            }
            else
            {
                if (!enabled)
                    retImage = _images.UncheckedDisabled;
                else if (pressed)
                    retImage = _images.UncheckedPressed;
                else if (tracking)
                    retImage = _images.UncheckedTracking;
                else
                    retImage = _images.UncheckedNormal;
            }

            // Not found, then get the common image
            if (retImage == null)
                retImage = _images.Common;

            // Not found, then inherit from target
            if (retImage == null)
                retImage = Target.GetRadioButtonImage(enabled, checkState, tracking, pressed);

            return retImage;
        }
        #endregion
    }
}
