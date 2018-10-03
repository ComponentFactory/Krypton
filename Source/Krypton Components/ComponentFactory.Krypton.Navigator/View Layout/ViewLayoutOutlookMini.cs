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

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// View element that knows how to enforce the visible state of the stacked items.
    /// </summary>
    internal class ViewLayoutOutlookMini : ViewLayoutDocker
    {
        #region Instance Fields
        private ViewBuilderOutlookBase _viewBuilder; 
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutOutlookMini class.
		/// </summary>
        /// <param name="viewBuilder">View builder reference.</param>
        public ViewLayoutOutlookMini(ViewBuilderOutlookBase viewBuilder)
        {
            Debug.Assert(viewBuilder != null);
            _viewBuilder = viewBuilder;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutOutlookMini:" + Id;
        }
        #endregion

        #region ViewBuilder
        /// <summary>
        /// Gets access to the associated view builder.
        /// </summary>
        public ViewBuilderOutlookBase ViewBuilder
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewBuilder; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Make all stacking items that should be visible are visible
            ViewBuilder.UnshrinkAppropriatePages();

            // Let base class continue with standard layout
            base.Layout(context);
        }
        #endregion
    }
}
