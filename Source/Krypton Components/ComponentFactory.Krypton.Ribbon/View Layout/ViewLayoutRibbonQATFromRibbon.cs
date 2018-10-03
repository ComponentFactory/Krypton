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
    /// Extends the ViewLayoutRibbonQATContents by providing the definitions from the ribbon control itself.
	/// </summary>
    internal class ViewLayoutRibbonQATFromRibbon : ViewLayoutRibbonQATContents
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonQATFromRibbon class.
		/// </summary>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        /// <param name="showExtraButton">Should the extra button be shown.</param>
        public ViewLayoutRibbonQATFromRibbon(KryptonRibbon ribbon,
                                             NeedPaintHandler needPaint,
                                             bool showExtraButton)
            : base(ribbon, needPaint, showExtraButton)
        {
        }
        #endregion

        #region DisplayButtons
        /// <summary>
        /// Returns a collection of all the quick access toolbar definitions.
        /// </summary>
        public override IQuickAccessToolbarButton[] QATButtons 
        { 
            get 
            {
                IQuickAccessToolbarButton[] qatButtons = new IQuickAccessToolbarButton[Ribbon.QATButtons.Count];

                // Copy all the entries into the new array 
                Ribbon.QATButtons.CopyTo(qatButtons, 0);

                return qatButtons;
            }
        }
        #endregion
    }
}
