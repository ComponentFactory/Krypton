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
using System.Windows.Forms;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details of an event that is fired just before a page is reordered.
	/// </summary>
    public class PageReorderEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private KryptonPage _pageMoving;
        private KryptonPage _pageTarget;
        private bool _movingBefore;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PageReorderEventArgs class.
		/// </summary>
        /// <param name="pageMoving">Reference to page being moved.</param>
        /// <param name="pageTarget">Reference to target paged.</param>
        /// <param name="movingBefore">True if moving page is to be positioned before the target; otherwise after the target.</param>
        public PageReorderEventArgs(KryptonPage pageMoving, 
                                    KryptonPage pageTarget, 
                                    bool movingBefore)
		{
            _pageMoving = pageMoving;
            _pageTarget = pageTarget;
            _movingBefore = movingBefore;
		}
		#endregion

        #region PageMoving
        /// <summary>
		/// Gets the page that is being moved.
		/// </summary>
        public KryptonPage PageMoving
		{
            get { return _pageMoving; }
		}
		#endregion

        #region PageTarget
        /// <summary>
        /// Gets the page that is the target for the move.
        /// </summary>
        public KryptonPage PageTarget
        {
            get { return _pageTarget; }
        }
        #endregion

        #region MovingBefore
        /// <summary>
        /// Gets a value indicating if the page being moved is to be placed before the target page.
        /// </summary>
        public bool MovingBefore
        {
            get { return _movingBefore; }
        }
        #endregion
    }
}
