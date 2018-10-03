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
	/// View element that positions the elements in a row centered in total area.
	/// </summary>
    internal class ViewLayoutRibbonRowCenter : ViewComposite
    {
        #region Type Definitions
        private class ItemToView : Dictionary<IRibbonGroupItem, ViewBase> { };
        private class ViewToSize : Dictionary<ViewBase, Size> { };
        #endregion

        #region Instance Fields
        private GroupItemSize _currentSize;
        private ViewToSize _viewToSmall;
        private ViewToSize _viewToMedium;
        private ViewToSize _viewToLarge;
        private Size _preferredSizeSmall;
        private Size _preferredSizeMedium;
        private Size _preferredSizeLarge;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonRowCenter class.
        /// </summary>
        public ViewLayoutRibbonRowCenter()
        {
            _currentSize = GroupItemSize.Large;
            _viewToSmall = new ViewToSize();
            _viewToMedium = new ViewToSize();
            _viewToLarge = new ViewToSize();
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonRowCenter:" + Id;
		}
        #endregion

        #region CurrentSize
        /// <summary>
        /// Gets and sets the current group item size.
        /// </summary>
        public GroupItemSize CurrentSize
        {
            get { return _currentSize; }
            set { _currentSize = value; }
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

            switch (_currentSize)
            {
                case GroupItemSize.Small:
                    _viewToSmall.Clear();
                    break;
                case GroupItemSize.Medium:
                    _viewToMedium.Clear();
                    break;
                case GroupItemSize.Large:
                    _viewToLarge.Clear();
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            Size preferredSize = Size.Empty;

            foreach (ViewBase child in this)
            {
                // Only investigate visible children
                if (child.Visible)
                {
                    // Ask child for it's own preferred size
                    Size childPreferred = child.GetPreferredSize(context);

                    // Cache the child preferred size for use in layout
                    switch (_currentSize)
                    {
                        case GroupItemSize.Small:
                            _viewToSmall.Add(child, childPreferred);
                            break;
                        case GroupItemSize.Medium:
                            _viewToMedium.Add(child, childPreferred);
                            break;
                        case GroupItemSize.Large:
                            _viewToLarge.Add(child, childPreferred);
                            break;
                    }

                    // Always add on the width of the child
                    preferredSize.Width += childPreferred.Width;

                    // Find the tallest of the children
                    preferredSize.Height = Math.Max(preferredSize.Height, childPreferred.Height);
                }
            }

            // Cache the size for the current item
            switch (_currentSize)
            {
                case GroupItemSize.Small:
                    _preferredSizeSmall = preferredSize;
                    break;
                case GroupItemSize.Medium:
                    _preferredSizeMedium = preferredSize;
                    break;
                case GroupItemSize.Large:
                    _preferredSizeLarge = preferredSize;
                    break;
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

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");
            
            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            Size preferredSize = Size.Empty;

            // Cache the size for the current item
            switch (_currentSize)
            {
                case GroupItemSize.Small:
                    preferredSize = _preferredSizeSmall;
                    break;
                case GroupItemSize.Medium:
                    preferredSize = _preferredSizeMedium;
                    break;
                case GroupItemSize.Large:
                    preferredSize = _preferredSizeLarge;
                    break;
            }

            // Starting left offset is half the difference between the client width and the total child widths
            int xOffset = (ClientWidth - preferredSize.Width) / 2;

            // Layout each child centered within this space
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Get the cached size of the child
                    Size childPreferred = Size.Empty;
                    
                    switch (_currentSize)
                    {
                        case GroupItemSize.Small:
                            if (_viewToSmall.ContainsKey(child))
                                childPreferred = _viewToSmall[child];
                            else
                                childPreferred = child.GetPreferredSize(context);
                            break;
                        case GroupItemSize.Medium:
                            if (_viewToMedium.ContainsKey(child))
                                childPreferred = _viewToMedium[child];
                            else
                                childPreferred = child.GetPreferredSize(context);
                            break;
                        case GroupItemSize.Large:
                            if (_viewToLarge.ContainsKey(child))
                                childPreferred = _viewToLarge[child];
                            else
                                childPreferred = child.GetPreferredSize(context);
                            break;
                    }

                    // Find vertical offset for centering
                    int yOffset = (ClientHeight - childPreferred.Height) / 2;

                    // Create the rectangle that centers the child in our space
                    context.DisplayRectangle = new Rectangle(ClientRectangle.X + xOffset,
                                                             ClientRectangle.Y + yOffset,
                                                             childPreferred.Width,
                                                             childPreferred.Height);

                    // Finally ask the child to layout
                    child.Layout(context);

                    // Move across to next horizontal position
                    xOffset += childPreferred.Width;
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
		#endregion
	}
}
