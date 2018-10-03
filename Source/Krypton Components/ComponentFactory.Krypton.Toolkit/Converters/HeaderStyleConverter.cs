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
    /// Custom type converter so that HeaderStyle values appear as neat text at design time.
    /// </summary>
    internal class HeaderStyleConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(HeaderStyle.Primary,      "Primary"),
                                             new Pair(HeaderStyle.Secondary,    "Secondary"), 
                                             new Pair(HeaderStyle.DockInactive, "Dock - Inactive"), 
                                             new Pair(HeaderStyle.DockActive,   "Dock - Active"), 
                                             new Pair(HeaderStyle.Form,         "Form"), 
                                             new Pair(HeaderStyle.Calendar,     "Calendar"), 
                                             new Pair(HeaderStyle.Custom1,      "Custom1"),
                                             new Pair(HeaderStyle.Custom2,      "Custom2"),  };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderStyleConverter clas.
        /// </summary>
        public HeaderStyleConverter()
            : base(typeof(HeaderStyle))
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
