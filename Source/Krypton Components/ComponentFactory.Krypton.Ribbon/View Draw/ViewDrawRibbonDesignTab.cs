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

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws an design time only for adding a new tab to the ribbon.
	/// </summary>
    internal class ViewDrawRibbonDesignTab : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _padding = new Padding(2, 4, 2, 0);
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignTab class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignTab(KryptonRibbon ribbon,
                                       NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonDesignTab:" + Id;
		}
        #endregion

        #region Protected
        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public override string GetShortText()
        {
            return "Tab";
        }

        /// <summary>
        /// Gets the padding to use when calculating the preferred size.
        /// </summary>
        protected override Padding PreferredPadding
        {
            get { return _padding; }
        }

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected override Padding LayoutPadding
        {
            get { return _padding; }
        }

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected override Padding OuterPadding
        {
            get { return Padding.Empty; }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(object sender, EventArgs e)
        {
            // Ask the ribbon to add a new tab at design time
            Ribbon.OnDesignTimeAddTab();
        }
        #endregion
    }
}
