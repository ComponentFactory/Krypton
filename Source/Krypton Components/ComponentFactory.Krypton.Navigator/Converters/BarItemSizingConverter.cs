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
    /// Custom type converter so that BarItemSizing values appear as neat text at design time.
    /// </summary>
    public class BarItemSizingConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(BarItemSizing.Individual,           "Individual Sizing"),
                                             new Pair(BarItemSizing.SameHeight,           "All Same Height"),
                                             new Pair(BarItemSizing.SameWidth,            "All Same Width"),
                                             new Pair(BarItemSizing.SameWidthAndHeight,   "All Same Width & Height") };
        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the BarItemSizingConverter clas.
        /// </summary>
        public BarItemSizingConverter()
            : base(typeof(BarItemSizing))
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
