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
	/// View element that insets children by the border rounding value of a source.
	/// </summary>
	internal class ViewLayoutInsetOverlap : ViewComposite
    {
        #region Instance Fields
        private ViewDrawCanvas _drawCanvas;
        private VisualOrientation _orientation;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutInsetOverlap class.
        /// </summary>
        public ViewLayoutInsetOverlap(ViewDrawCanvas drawCanvas)
        {
            Debug.Assert(drawCanvas != null);

            // Remember source of the rounding values
            _drawCanvas = drawCanvas;

            // Default other state
            _orientation = VisualOrientation.Top;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutInsetForRounding:" + Id;
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the bar orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _orientation; }
            set { _orientation = value; }
        }
        #endregion

        #region Rounding
        /// <summary>
        /// Gets the rounding value to apply on the edges.
        /// </summary>
        public int Rounding
        {
            get
            {
                // Get the rounding and width values for the border
                int rounding = _drawCanvas.PaletteBorder.GetBorderRounding(_drawCanvas.State);
                int width = _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State);

                // We have to add half the width as that increases the rounding effect
                return rounding + width / 2;
            }
        }
        #endregion

        #region BorderWidth
        /// <summary>
        /// Gets the rounding value to apply on the edges.
        /// </summary>
        public int BorderWidth
        {
            get { return _drawCanvas.PaletteBorder.GetBorderWidth(_drawCanvas.State); }
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

            // Get the preferred size requested by the children
            Size size =  base.GetPreferredSize(context);

            // Apply the rounding in the appropriate orientation
            if ((Orientation == VisualOrientation.Top) || (Orientation == VisualOrientation.Bottom))
            {
                size.Width += Rounding * 2;
                size.Height += BorderWidth;
            }
            else
            {
                size.Height += Rounding * 2;
                size.Width += BorderWidth;
            }

            return size;
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

            // Find the rectangle available to each child by removing the rounding
            Rectangle childRect = ClientRectangle;

            // Find the amount of rounding to apply
            int rounding = Rounding;

            // Apply the rounding in the appropriate orientation
            if ((Orientation == VisualOrientation.Top) || (Orientation == VisualOrientation.Bottom))
            {
                childRect.Width -= rounding * 2;
                childRect.X += rounding;
            }
            else
            {
                childRect.Height -= rounding * 2;
                childRect.Y += rounding;
            }

            // Inform each child to layout inside the reduced rectangle
            foreach (ViewBase child in this)
            {
                context.DisplayRectangle = childRect;
                child.Layout(context);
            }

            // Remember the set context to the size we were given
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
