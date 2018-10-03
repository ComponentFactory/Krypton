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
	/// Redirects requests for check box images from the CheckBoxImages instance.
	/// </summary>
    public class PaletteRedirectCheckBox : PaletteRedirect
    {
        #region Instance Fields
        private CheckBoxImages _images;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRedirectCheckBox class.
		/// </summary>
        /// <param name="images">Reference to source of check box images.</param>
        public PaletteRedirectCheckBox(CheckBoxImages images)
            : this(null, images)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteRedirectCheckBox class.
		/// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="images">Reference to source of check box images.</param>
        public PaletteRedirectCheckBox(IPalette target,
                                       CheckBoxImages images)
            : base(target)
		{
            Debug.Assert(images != null);

            // Remember incoming target
            _images = images;
		}
		#endregion

        #region Images
        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetCheckBoxImage(bool enabled, 
                                               CheckState checkState, 
                                               bool tracking, 
                                               bool pressed)
        {
            Image retImage = null;

            // Get the state specific image
            switch (checkState)
            {
                default:
                case CheckState.Unchecked:
                    if (!enabled)
                        retImage = _images.UncheckedDisabled;
                    else if (pressed)
                        retImage = _images.UncheckedPressed;
                    else if (tracking)
                        retImage = _images.UncheckedTracking;
                    else
                        retImage = _images.UncheckedNormal;
                    break;
                case CheckState.Checked:
                    if (!enabled)
                        retImage = _images.CheckedDisabled;
                    else if (pressed)
                        retImage = _images.CheckedPressed;
                    else if (tracking)
                        retImage = _images.CheckedTracking;
                    else
                        retImage = _images.CheckedNormal;
                    break;
                case CheckState.Indeterminate:
                    if (!enabled)
                        retImage = _images.IndeterminateDisabled;
                    else if (pressed)
                        retImage = _images.IndeterminatePressed;
                    else if (tracking)
                        retImage = _images.IndeterminateTracking;
                    else
                        retImage = _images.IndeterminateNormal;
                    break;
            }

            // Not found, then get the common image
            if (retImage == null)
                retImage = _images.Common;

            // Not found, then inherit from target
            if (retImage == null)
                retImage = Target.GetCheckBoxImage(enabled, checkState, tracking, pressed);

            return retImage;
        }
        #endregion
    }
}
