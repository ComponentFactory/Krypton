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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that SeparatorStyle values appear as neat text at design time.
    /// </summary>
    internal class SeparatorStyleConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(SeparatorStyle.LowProfile,            "Low Profile"),
                                             new Pair(SeparatorStyle.HighProfile,           "High Profile"),  
                                             new Pair(SeparatorStyle.HighInternalProfile,   "High Internal Profile"),  
                                             new Pair(SeparatorStyle.Custom1,               "Custom1"),  };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the SeparatorStyleConverter clas.
        /// </summary>
        public SeparatorStyleConverter()
            : base(typeof(SeparatorStyle))
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
