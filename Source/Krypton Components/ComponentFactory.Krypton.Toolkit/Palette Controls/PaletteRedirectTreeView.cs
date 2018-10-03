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
	/// Redirects requests for tree view images from the TreeViewImages instance.
	/// </summary>
    public class PaletteRedirectTreeView : PaletteRedirect
    {
        #region Instance Fields
        private TreeViewImages _plusMinusImages;
        private CheckBoxImages _checkboxImages;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRedirectTreeView class.
		/// </summary>
        /// <param name="plusMinusImages">Reference to source of tree view images.</param>
        /// <param name="checkboxImages">Reference to source of check box images.</param>
        public PaletteRedirectTreeView(TreeViewImages plusMinusImages,
                                       CheckBoxImages checkboxImages)
            : this(null, plusMinusImages, checkboxImages)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteRedirectTreeView class.
		/// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="plusMinusImages">Reference to source of tree view images.</param>
        /// <param name="checkboxImages">Reference to source of check box images.</param>
        public PaletteRedirectTreeView(IPalette target,
                                       TreeViewImages plusMinusImages,
                                       CheckBoxImages checkboxImages)
            : base(target)
		{
            Debug.Assert(plusMinusImages != null);

            // Remember incoming targets
            _plusMinusImages = plusMinusImages;
            _checkboxImages = checkboxImages;
		}
		#endregion

        #region Images
        /// <summary>
        /// Gets a tree view image appropriate for the provided state.
        /// </summary>
        /// <param name="expanded">Is the node expanded</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetTreeViewImage(bool expanded)
        {
            Image retImage = null;

            if (expanded)
                retImage = _plusMinusImages.Minus;
            else
                retImage = _plusMinusImages.Plus;

            // Not found, then inherit from target
            if (retImage == null)
                retImage = Target.GetTreeViewImage(expanded);

            return retImage;
        }

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
                        retImage = _checkboxImages.UncheckedDisabled;
                    else if (pressed)
                        retImage = _checkboxImages.UncheckedPressed;
                    else if (tracking)
                        retImage = _checkboxImages.UncheckedTracking;
                    else
                        retImage = _checkboxImages.UncheckedNormal;
                    break;
                case CheckState.Checked:
                    if (!enabled)
                        retImage = _checkboxImages.CheckedDisabled;
                    else if (pressed)
                        retImage = _checkboxImages.CheckedPressed;
                    else if (tracking)
                        retImage = _checkboxImages.CheckedTracking;
                    else
                        retImage = _checkboxImages.CheckedNormal;
                    break;
                case CheckState.Indeterminate:
                    if (!enabled)
                        retImage = _checkboxImages.IndeterminateDisabled;
                    else if (pressed)
                        retImage = _checkboxImages.IndeterminatePressed;
                    else if (tracking)
                        retImage = _checkboxImages.IndeterminateTracking;
                    else
                        retImage = _checkboxImages.IndeterminateNormal;
                    break;
            }

            // Not found, then get the common image
            if (retImage == null)
                retImage = _checkboxImages.Common;

            // Not found, then inherit from target
            if (retImage == null)
                retImage = Target.GetCheckBoxImage(enabled, checkState, tracking, pressed);

            return retImage;
        }
        #endregion
    }
}
