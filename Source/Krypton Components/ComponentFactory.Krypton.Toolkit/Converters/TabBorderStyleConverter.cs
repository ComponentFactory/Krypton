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
    /// Custom type converter so that TabBorderStyle values appear as neat text at design time.
    /// </summary>
    internal class TabBorderStyleConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(TabBorderStyle.OneNote,               "OneNote"),
                                             new Pair(TabBorderStyle.SquareEqualSmall,      "Square Equal Small"),
                                             new Pair(TabBorderStyle.SquareEqualMedium,     "Square Equal Medium"),
                                             new Pair(TabBorderStyle.SquareEqualLarge,      "Square Equal Large"),
                                             new Pair(TabBorderStyle.SquareOutsizeSmall,    "Square Outsize Small"),
                                             new Pair(TabBorderStyle.SquareOutsizeMedium,   "Square Outsize Medium"),
                                             new Pair(TabBorderStyle.SquareOutsizeLarge,    "Square Outsize Large"),
                                             new Pair(TabBorderStyle.RoundedEqualSmall,     "Rounded Equal Small"),
                                             new Pair(TabBorderStyle.RoundedEqualMedium,    "Rounded Equal Medium"),
                                             new Pair(TabBorderStyle.RoundedEqualLarge,     "Rounded Equal Large"),
                                             new Pair(TabBorderStyle.RoundedOutsizeSmall,   "Rounded Outsize Small"),
                                             new Pair(TabBorderStyle.RoundedOutsizeMedium,  "Rounded Outsize Medium"),
                                             new Pair(TabBorderStyle.RoundedOutsizeLarge,   "Rounded Outsize Large"),
                                             new Pair(TabBorderStyle.SlantEqualNear,        "Slant Equal Near"),
                                             new Pair(TabBorderStyle.SlantEqualFar,         "Slant Equal Far"),
                                             new Pair(TabBorderStyle.SlantEqualBoth,        "Slant Equal Both"),
                                             new Pair(TabBorderStyle.SlantOutsizeNear,      "Slant Outsize Near"),
                                             new Pair(TabBorderStyle.SlantOutsizeFar,       "Slant Outsize Far"),
                                             new Pair(TabBorderStyle.SlantOutsizeBoth,      "Slant Outsize Both"),
                                             new Pair(TabBorderStyle.SmoothEqual,           "Smooth Equal"),
                                             new Pair(TabBorderStyle.SmoothOutsize,         "Smooth Outsize"),
                                             new Pair(TabBorderStyle.DockEqual,             "Dock Equal"),
                                             new Pair(TabBorderStyle.DockOutsize,           "Dock Outsize") };
        #endregion  

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TabBorderStyleConverter clas.
        /// </summary>
        public TabBorderStyleConverter()
            : base(typeof(TabBorderStyle))
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
