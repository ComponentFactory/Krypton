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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Details for close reason event handlers.
	/// </summary>
	public class CloseReasonEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private ToolStripDropDownCloseReason _closeReason;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the CloseReasonEventArgs class.
		/// </summary>
        /// <param name="closeReason">Reason for the close action occuring.</param>
        public CloseReasonEventArgs(ToolStripDropDownCloseReason closeReason)
		{
            _closeReason = closeReason;
		}
        #endregion

		#region Public
		/// <summary>
		/// Gets access to the reason for the context menu closing.
		/// </summary>
        public ToolStripDropDownCloseReason CloseReason
		{
            get { return _closeReason; }
		}
        #endregion
	}
}
