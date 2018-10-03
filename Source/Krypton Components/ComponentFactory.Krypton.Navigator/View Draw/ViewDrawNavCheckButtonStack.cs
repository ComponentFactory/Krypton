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
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Navigator view element for drawing a stack check button for a krypton page.
	/// </summary>
    internal class ViewDrawNavCheckButtonStack : ViewDrawNavCheckButtonBase
	{
		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNavCheckButtonStack class.
        /// </summary>
        /// <param name="navigator">Owning navigator instance.</param>
        /// <param name="page">Page this check button represents.</param>
        /// <param name="orientation">Orientation for the check button.</param>
        public ViewDrawNavCheckButtonStack(KryptonNavigator navigator,
                                           KryptonPage page,
                                           VisualOrientation orientation)
            : base(navigator, page, orientation)
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawNavCheckButtonStack:" + Id;
		}
		#endregion

        #region UpdateButtonSpecMapping
        /// <summary>
        /// Update the button spec manager mapping to reflect current settings.
        /// </summary>
        public override void UpdateButtonSpecMapping()
        {
            // Define a default mapping for text color and recreate to use that new setting
            ButtonSpecManager.SetRemapTarget(Navigator.Stack.CheckButtonStyle);
            ButtonSpecManager.RecreateButtons();
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public override Image GetImage(PaletteState state)
        {
            return Page.GetImageMapping(Navigator.Stack.StackMapImage);
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetShortText()
        {
            return Page.GetTextMapping(Navigator.Stack.StackMapText);
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetLongText()
        {
            return Page.GetTextMapping(Navigator.Stack.StackMapExtraText);
        }
        #endregion
    }
}
