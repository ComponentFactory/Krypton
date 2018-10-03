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
	/// Navigator view element for drawing a tab check button for a krypton page.
	/// </summary>
    internal class ViewDrawNavCheckButtonTab : ViewDrawNavCheckButtonBar
	{
		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNavCheckButtonTab class.
        /// </summary>
        /// <param name="navigator">Owning navigator instance.</param>
        /// <param name="page">Page this check button represents.</param>
        /// <param name="orientation">Orientation for the check button.</param>
        public ViewDrawNavCheckButtonTab(KryptonNavigator navigator,
                                         KryptonPage page,
                                         VisualOrientation orientation)
            : base(navigator, page, orientation,
                   page.StateDisabled.Tab, 
                   page.StateNormal.Tab,
                   page.StateTracking.Tab, 
                   page.StatePressed.Tab,
                   page.StateSelected.Tab,
                   page.OverrideFocus.Tab)
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawNavCheckButtonTab:" + Id;
		}
		#endregion

        #region UpdateButtonSpecMapping
        /// <summary>
        /// Update the button spec manager mapping to reflect current settings.
        /// </summary>
        public override void UpdateButtonSpecMapping()
        {
            // Update the button spec manager for this tab to use a tab style for remapping
            ButtonSpecManager.SetRemapTarget(Navigator.Bar.TabStyle);
            ButtonSpecManager.RecreateButtons();
        }
        #endregion

        #region ButtonClickOnDown
        /// <summary>
        /// Should the item be selected on the mouse down.
        /// </summary>
        protected override bool ButtonClickOnDown
        {
            get { return true; }
        }
        #endregion
    }
}
