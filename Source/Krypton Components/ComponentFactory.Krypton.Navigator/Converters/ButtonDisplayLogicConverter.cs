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
using System.ComponentModel;

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that ButtonDisplayLogic values appear as neat text at design time.
    /// </summary>
    public class ButtonDisplayLogicConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(ButtonDisplayLogic.None,                  "None"),
                                             new Pair(ButtonDisplayLogic.Context,               "Context"),
                                             new Pair(ButtonDisplayLogic.NextPrevious,          "Next/Previous"),
                                             new Pair(ButtonDisplayLogic.ContextNextPrevious,   "Context & Next/Previous") };
        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonDisplayLogicConverter clas.
        /// </summary>
        public ButtonDisplayLogicConverter()
            : base(typeof(ButtonDisplayLogic))
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
