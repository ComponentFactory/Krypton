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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteTextTrim values appear as neat text at design time.
    /// </summary>
    internal class PaletteTextTrimConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(PaletteTextTrim.Inherit,              "Inherit"),
                                             new Pair(PaletteTextTrim.Hide,                 "Hide"),
                                             new Pair(PaletteTextTrim.Character,            "Character"),
                                             new Pair(PaletteTextTrim.Word,                 "Word"),
                                             new Pair(PaletteTextTrim.EllipsisCharacter,    "Ellipsis Character"),
                                             new Pair(PaletteTextTrim.EllipsisWord,         "Ellipsis Word"),
                                             new Pair(PaletteTextTrim.EllipsisPath,         "Ellipsis Path") };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTextTrimConverter clas.
        /// </summary>
        public PaletteTextTrimConverter()
            : base(typeof(PaletteTextTrim))
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
