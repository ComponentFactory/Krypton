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

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details for a close button action event.
	/// </summary>
    public class CloseActionEventArgs : KryptonPageEventArgs
	{
		#region Instance Fields
		private CloseButtonAction _action;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the CloseActionEventArgs class.
		/// </summary>
		/// <param name="page">Page effected by event.</param>
		/// <param name="index">Index of page in the owning collection.</param>
        /// <param name="action">Close action to take.</param>
        public CloseActionEventArgs(KryptonPage page, 
                                    int index, 
                                    CloseButtonAction action)
			: base(page, index)
		{
            _action = action;
		}
		#endregion

        #region Action
        /// <summary>
		/// Gets and sets the close action to take.
		/// </summary>
        public CloseButtonAction Action
		{
            get { return _action; }
            set { _action = value; }
		}
		#endregion
	}
}
