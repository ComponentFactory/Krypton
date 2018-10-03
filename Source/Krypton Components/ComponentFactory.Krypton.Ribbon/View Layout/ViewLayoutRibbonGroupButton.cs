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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Layout area for the group button.
	/// </summary>
    internal class ViewLayoutRibbonGroupButton : ViewLayoutDocker
    {
        #region Instance Fields
        private ViewDrawRibbonGroupDialogButton _groupButton;
        private ViewLayoutRibbonCenter _centerButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGroupButton class.
		/// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewLayoutRibbonGroupButton(KryptonRibbon ribbon,
                                           KryptonRibbonGroup ribbonGroup,
                                           NeedPaintHandler needPaint)
        {
            _groupButton = new ViewDrawRibbonGroupDialogButton(ribbon, ribbonGroup, needPaint);
            _centerButton = new ViewLayoutRibbonCenter();

            // Fill remainder with the actual button, but centered within space
            _centerButton.Add(_groupButton);
            Add(_centerButton, ViewDockStyle.Fill);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonGroupButton:" + Id;
		}
		#endregion

        #region DialogButtonController
        /// <summary>
        /// Gets access to the controller used for the button.
        /// </summary>
        public DialogLauncherButtonController DialogButtonController
        {
            get { return _groupButton.DialogButtonController; }
        }
        #endregion

        #region GetFocusView
        /// <summary>
        /// Gets the view to use for the group dialog button.
        /// </summary>
        /// <returns>ViewBase if valid as a focus item; otherwise false.</returns>
        public ViewBase GetFocusView()
        {
            if (Visible && Enabled && _groupButton.Visible && _groupButton.Enabled)
                return _groupButton;
            else
                return null;
        }
        #endregion
    }
}
