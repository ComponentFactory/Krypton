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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
	/// View element that can draw an auto hidden tab based on a KryptonPage as the source.
	/// </summary>
    internal class ViewDrawAutoHiddenTab : ViewDrawButton,
                                           IContentValues
    {
        #region Instance Fields
        private KryptonPage _page;
        private VisualOrientation _orientation;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawAutoHiddenTab class.
        /// </summary>
        /// <param name="page">Reference to the page this tab represents.</param>
        /// <param name="orientation">Visual orientation used for drawing the tab.</param>
        public ViewDrawAutoHiddenTab(KryptonPage page,
                                     VisualOrientation orientation)
            : base(page.StateDisabled.CheckButton, 
                   page.StateNormal.CheckButton,
                   page.StateTracking.CheckButton, 
                   page.StatePressed.CheckButton,
                   null, null, orientation, false)
        {
            _page = page;
            _orientation = orientation;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the page associated with the view.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }
        #endregion

        #region Public IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return _page.ImageSmall;
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
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _page.Text;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion
    }
}
