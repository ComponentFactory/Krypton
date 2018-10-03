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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Temporary setup of the provided control in the context.
    /// </summary>
    public class CorrectContextControl : IDisposable
    {
        #region Instance Fields
        private ViewLayoutContext _context;
        private Control _startControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CorrectContextControl class.
        /// </summary>
        /// <param name="context">Context to update.</param>
        /// <param name="control">Actual parent control instance.</param>
        public CorrectContextControl(ViewLayoutContext context,
                                     Control control)
        {
            Debug.Assert(context != null);

            // Remmeber incoming context
            _context = context;

            // Remember staring setting
            _startControl = context.Control;

            // Update with correct control
            _context.Control = control;
        }

        /// <summary>
        /// Cleanup settings.
        /// </summary>
        public void Dispose()
        {
            // Put back the original setting
            _context.Control = _startControl;
        }
        #endregion
    }
}
