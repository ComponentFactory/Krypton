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
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a AutoHiddenSeparatorResize event.
	/// </summary>
    public class AutoHiddenSeparatorResizeEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonSeparator _separator;
        private KryptonDockspace _dockspace;
        private KryptonPage _page;
        private Rectangle _resizeRect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the AutoHiddenSeparatorResizeEventArgs class.
		/// </summary>
        /// <param name="separator">Reference to separator control instance.</param>
        /// <param name="dockspace">Reference to dockspace control instance.</param>
        /// <param name="page">Reference to page contained in the dockspace.</param>
        /// <param name="resizeRect">Initial resizing rectangle.</param>
        public AutoHiddenSeparatorResizeEventArgs(KryptonSeparator separator,
                                                  KryptonDockspace dockspace,
                                                  KryptonPage page,
                                                  Rectangle resizeRect)
		{
            _separator = separator;
            _dockspace = dockspace;
            _page = page;
            _resizeRect = resizeRect;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonSeparator control.
        /// </summary>
        public KryptonSeparator SeparatorControl
        {
            get { return _separator; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl
        {
            get { return _dockspace; }
        }

        /// <summary>
        /// Gets a reference to the KryptonPage instance.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }

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
