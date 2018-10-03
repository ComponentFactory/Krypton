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
	/// View element that can track the size and position of the selected page.
	/// </summary>
    internal class ViewLayoutPageShow : ViewLayoutNull
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private bool _minimumAsPreferred;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutPageShow class.
		/// </summary>
        public ViewLayoutPageShow(KryptonNavigator navigator)
		{
			Debug.Assert(navigator != null);

			// Remember back reference
			_navigator = navigator;
            _minimumAsPreferred = false;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutPageShow:" + Id;
		}
		#endregion

        #region SetMinimumAsPreferred
        /// <summary>
        /// Sets if the minimum size should be used instead of preferred.
        /// </summary>
        /// <param name="minimum">Should minimum be used instead of preferred.</param>
        public void SetMinimumAsPreferred(bool minimum)
        {
            // Update preferred calculation details
            _minimumAsPreferred = minimum;
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

            Size ret = Size.Empty;

            // Use the minimum size instead of preferred size?
            if (_minimumAsPreferred)
            {
                // Grab the selected pages minimum size
                if (_navigator.SelectedPage != null)
                    ret = _navigator.SelectedPage.MinimumSize;
            }
            else
            {
                // Is there a selected page?
                if (_navigator.SelectedPage != null)
                    ret = _navigator.SelectedPage.GetPreferredSize(Size.Empty);
            }

            return ret;
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

            // Are we allowed to layout child controls?
            if (!context.ViewManager.DoNotLayoutControls)
            {
                // Are we allowed to actually layout the pages?
                if (_navigator.InternalCanLayout)
                {
                    // Do not position the child panel or pages if it is borrowed
                    if (!_navigator.IsChildPanelBorrowed)
                    {
                        // Position the child panel for showing page information
                        _navigator.ChildPanel.SetBounds(ClientLocation.X,
                                                        ClientLocation.Y,
                                                        ClientWidth,
                                                        ClientHeight);

                        // Is there a selected page?
                        if (_navigator.SelectedPage != null)
                        {
                            // Position all the contained pages in to the correct area
                            foreach (KryptonPage page in _navigator.Pages)
                            {
                                if (page == _navigator.SelectedPage)
                                {
                                    page.SetBounds(0, 0, ClientWidth, ClientHeight);

                                    // Ensure the selected page is the highest in the z-order
                                    _navigator.ChildPanel.Controls.SetChildIndex(_navigator.SelectedPage, 0);
                                    _navigator.ChildPanel.Controls.SetChildIndex(_navigator.SelectedPage, 0);
                                }
                            }
                        }
                    }
                }
            }
		}
		#endregion
	}
}
