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
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Allocate a spacer for the right side of a window that prevents layout over the min/max/close buttons.
	/// </summary>
    internal class ViewDrawRibbonCompoRightBorder : ViewLeaf
    {
        #region Static Fields
        private static readonly int SPACING_GAP = 10;
        #endregion

        #region Instance Fields
        private VisualForm _ownerForm;
        private int _width;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonCompoRightBorder class.
		/// </summary>
        public ViewDrawRibbonCompoRightBorder()
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonCompoRightBorder:" + Id;
		}
        #endregion

        #region CompOwnerForm
        /// <summary>
        /// Gets and sets the owner form to use when compositing.
        /// </summary>
        public VisualForm CompOwnerForm
        {
            get { return _ownerForm; }
            set { _ownerForm = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferredSize = Size.Empty;

            // We need an owning form to perform calculations
            if (_ownerForm != null)
            {
                // We only have size if custom chrome is being used with composition
                if (_ownerForm.ApplyCustomChrome && _ownerForm.ApplyComposition)
                {
                    try
                    {
                        // Create structure that will be populated by call to WM_GETTITLEBARINFOEX
                        PI.TITLEBARINFOEX tbi = new PI.TITLEBARINFOEX();
                        tbi.cbSize = (uint)Marshal.SizeOf(tbi);

                        // Ask the window for the title bar information
                        PI.SendMessage(_ownerForm.Handle, PI.WM_GETTITLEBARINFOEX, IntPtr.Zero, ref tbi);

                        // Find width of the button rectangle
                        int closeWidth = tbi.rcCloseButton.right - tbi.rcCloseButton.left;
                        int helpWidth = tbi.rcHelpButton.right - tbi.rcHelpButton.left;
                        int minWidth = tbi.rcMinButton.right - tbi.rcMinButton.left;
                        int maxWidth = tbi.rcMaxButton.right - tbi.rcMaxButton.left;

                        int clientWidth = _ownerForm.ClientSize.Width;
                        int clientScreenRight = _ownerForm.RectangleToScreen(_ownerForm.ClientRectangle).Right;
                        int leftMost = clientScreenRight;

                        // Find the left most button edge (start with right side of client area)
                        if ((closeWidth > 0) && (closeWidth < clientWidth))
                            leftMost = Math.Min(leftMost, tbi.rcCloseButton.left);

                        if ((helpWidth > 0) && (helpWidth < clientWidth))
                            leftMost = Math.Min(leftMost, tbi.rcHelpButton.left);

                        if ((minWidth > 0) && (minWidth < clientWidth))
                            leftMost = Math.Min(leftMost, tbi.rcMinButton.left);

                        if ((maxWidth > 0) && (maxWidth < clientWidth))
                            leftMost = Math.Min(leftMost, tbi.rcMaxButton.left);

                        // Our width is the distance between the left most button edge and the right
                        // side of the client area (this space the buttons are taking up). Plus a small
                        // extra gap between the first button and the caption elements to its left.
                        _width = (clientScreenRight - leftMost) + SPACING_GAP;

                        preferredSize.Width = _width;
                    }
                    catch(ObjectDisposedException)
                    {
                        // Asking for the WM_GETTITLEBARINFOEX can cause exception if the form level
                        // Icon has already been disposed. This happens in rare circumstances.
                    }
                }
            }

            return preferredSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Start with all the provided space
            ClientRectangle = context.DisplayRectangle;
        }
        #endregion
    }
}
