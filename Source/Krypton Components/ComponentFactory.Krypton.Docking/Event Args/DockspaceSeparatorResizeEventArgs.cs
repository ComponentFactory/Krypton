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
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a DockspaceSeparatorResize event.
	/// </summary>
    public class DockspaceSeparatorResizeEventArgs : DockspaceSeparatorEventArgs
	{
		#region Instance Fields
        private Rectangle _resizeRect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the DockspaceSeparatorResizeEventArgs class.
		/// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="element">Reference to dockspace docking element that is managing the separator.</param>
        /// <param name="resizeRect">Initial resizing rectangle.</param>
        public DockspaceSeparatorResizeEventArgs(KryptonSeparator separator,
                                                 KryptonDockingDockspace element,
                                                 Rectangle resizeRect)
            : base(separator, element)
		{
            _resizeRect = resizeRect;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets and sets the rectangle that limits resizing of the dockspace using the separator.
        /// </summary>
        public Rectangle ResizeRect
        {
            get { return _resizeRect; }
            set { _resizeRect = value; }
        }
        #endregion
	}
}
