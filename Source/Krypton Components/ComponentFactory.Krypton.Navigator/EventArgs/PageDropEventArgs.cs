// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Details for an event that indicates a page is being dropped.
	/// </summary>
	public class PageDropEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PageDropEventArgs class.
		/// </summary>
        /// <param name="page">Page that is being dropped.</param>
        public PageDropEventArgs(KryptonPage page)
		{
            _page = page;
		}
        #endregion

        #region Page
        /// <summary>
        /// Gets and sets the page to be dropped.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
            set { _page = Page; }
        }
        #endregion
    }
}
