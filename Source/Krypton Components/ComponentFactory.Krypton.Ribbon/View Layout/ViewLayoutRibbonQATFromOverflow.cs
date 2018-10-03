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
    /// Extends the ViewLayoutRibbonQATContents by providing the definitions that are overflowing the original source.
	/// </summary>
    internal class ViewLayoutRibbonQATFromOverflow : ViewLayoutRibbonQATContents
    {
        #region Instance Fields
        private Control _parentControl;
        private ViewLayoutRibbonQATContents _contents;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonQATFromOverflow class.
		/// </summary>
        /// <param name="parentControl">Owning control used to find screen positions.</param>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        /// <param name="showExtraButton">Should the extra button be shown.</param>
        /// <param name="contents">Source for finding buttons that are overflowing.</param>
        public ViewLayoutRibbonQATFromOverflow(Control parentControl,
                                               KryptonRibbon ribbon,
                                               NeedPaintHandler needPaint,
                                               bool showExtraButton,
                                               ViewLayoutRibbonQATContents contents)
            : base(ribbon, needPaint, showExtraButton)
        {
            Debug.Assert(parentControl != null);
            Debug.Assert(contents != null);
            
            _contents = contents;
            _parentControl = parentControl;
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
                List<IQuickAccessToolbarButton> qatOverflow = new List<IQuickAccessToolbarButton>();

                // Scan all the defined buttons for ones to show as overflowing
                foreach (IQuickAccessToolbarButton qatButton in Ribbon.QATButtons)
                {
                    // If the button requests to be shown...
                    if (qatButton.GetVisible())
                    {
                        ViewBase qatView = _contents.ViewForButton(qatButton);

                        //...but the view is not displayed, then show on overflow
                        if ((qatView != null) && !qatView.Visible)
                            qatOverflow.Add(qatButton);
                    }
                }

                return qatOverflow.ToArray();
            }
        }
        #endregion

        #region ParentControl
        /// <summary>
        /// Gets a reference to the owning control of this element.
        /// </summary>
        /// <returns>Control reference.</returns>
        public override Control ParentControl
        {
            get { return _parentControl; }
        }
        #endregion
    }
}
