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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the ViewComposite by laying out children in horizontal/vertical stack.
	/// </summary>
	public class ViewLayoutStack : ViewComposite
	{
		#region Instance Fields
		private bool _horizontal;
        private bool _fillLastChild;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutStack class.
		/// </summary>
        public ViewLayoutStack(bool horizontal)
        {
            // Create child to dock style lookup
            _horizontal = horizontal;

            // By default we fill the remainder area with the last child
            _fillLastChild = true;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutStack:" + Id;
		}
		#endregion

        #region Horizontal
        /// <summary>
        /// Gets and sets the stack orientation.
        /// </summary>
        public bool Horizontal
        {
            get { return _horizontal; }
            set { _horizontal = value; }
        }
        #endregion

        #region FillLastChild
        /// <summary>
        /// Gets and sets if the last child fills the remainder of the space.
        /// </summary>
        public bool FillLastChild
        {
            get { return _fillLastChild; }
            set { _fillLastChild = value; }
        }
        #endregion

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            // Accumulate the stacked size
            Size preferredSize = Size.Empty;

            foreach (ViewBase child in this)
            {
                if (child.Visible)
                {
                    // Get the preferred size of the child
                    Size childSize = child.GetPreferredSize(context);

                    // Depending on orientation, add up child sizes
                    if (Horizontal)
                    {
                        preferredSize.Height = Math.Max(preferredSize.Height, childSize.Height);
                        preferredSize.Width += childSize.Width;
                    }
                    else
                    {
                        preferredSize.Height += childSize.Height;
                        preferredSize.Width = Math.Max(preferredSize.Width, childSize.Width);
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

            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            // Maximum space available for the next child
            Rectangle childRectangle = ClientRectangle;

            // Find the last visible child
            ViewBase lastVisible = null;
            foreach(ViewBase child in this.Reverse())
                if (child.Visible)
                {
                    lastVisible = child;
                    break;
                }

            // Position each entry, with last entry filling remaining of space
			foreach (ViewBase child in this)
			{
                if (child.Visible)
				{
                    // Provide the total space currently available
                    context.DisplayRectangle = childRectangle;

					// Get the preferred size of the child
					Size childSize = child.GetPreferredSize(context);

                    if (Horizontal)
                    {
                        // Ask child to fill the available height
                        childSize.Height = childRectangle.Height;

                        if ((child == lastVisible) && FillLastChild)
                        {
                            // This child takes all remainder width
                            childSize.Width = childRectangle.Width;
                        }
                        else
                        {
                            // Reduce remainder space to exclude this child
                            childRectangle.X += childSize.Width;
                            childRectangle.Width -= childSize.Width;
                        }
                    }
                    else
                    {
                        // Ask child to fill the available width
                        childSize.Width = childRectangle.Width;

                        if ((child == lastVisible) && FillLastChild)
                        {
                            // This child takes all remainder height
                            childSize.Height = childRectangle.Height;
                        }
                        else
                        {
                            // Reduce remainder space to exclude this child
                            childRectangle.Y += childSize.Height;
                            childRectangle.Height -= childSize.Height;
                        }
                    }

                    // Use the update child size as the actual space for layout
                    context.DisplayRectangle = new Rectangle(context.DisplayRectangle.Location, childSize);

					// Layout child in the provided space
					child.Layout(context);
				}
			}

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
		#endregion
    }
}
