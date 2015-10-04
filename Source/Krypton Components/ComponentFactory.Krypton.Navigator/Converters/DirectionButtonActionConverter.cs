// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.ComponentModel;

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that DirectionButtonAction values appear as neat text at design time.
    /// </summary>
    public class DirectionButtonActionConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(DirectionButtonAction.None,                   "None (Do nothing)"),
                                             new Pair(DirectionButtonAction.SelectPage,             "Select Page"),
                                             new Pair(DirectionButtonAction.MoveBar,                "Move Bar"),
                                             new Pair(DirectionButtonAction.ModeAppropriateAction,  "Mode Appropriate Action") };
        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DirectionButtonActionConverter clas.
        /// </summary>
        public DirectionButtonActionConverter()
            : base(typeof(DirectionButtonAction))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs 
        {
            get { return _pairs; }
        }
        #endregion
    }
}
