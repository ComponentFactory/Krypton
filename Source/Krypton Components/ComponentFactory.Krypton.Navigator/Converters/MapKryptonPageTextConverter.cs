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
    /// Custom type converter so that MapKryptonPageText values appear as neat text at design time.
    /// </summary>
    public class MapKryptonPageTextConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(MapKryptonPageText.None,                          "None (Empty string)"),
                                             new Pair(MapKryptonPageText.Text,                          "Text"),
                                             new Pair(MapKryptonPageText.TextTitle,                     "Text - Title"), 
                                             new Pair(MapKryptonPageText.TextTitleDescription,          "Text - Title - Description"),
                                             new Pair(MapKryptonPageText.TextDescription,               "Text - Description"), 
                                             new Pair(MapKryptonPageText.Title,                         "Title"), 
                                             new Pair(MapKryptonPageText.TitleText,                     "Title - Text"),
                                             new Pair(MapKryptonPageText.TitleDescription,              "Title - Description"),
                                             new Pair(MapKryptonPageText.Description,                   "Description"),
                                             new Pair(MapKryptonPageText.DescriptionText,               "Description - Text"),
                                             new Pair(MapKryptonPageText.DescriptionTitle,              "Description - Title"),
                                             new Pair(MapKryptonPageText.DescriptionTitleText,          "Description - Title - Text"),
                                             new Pair(MapKryptonPageText.ToolTipTitle,                  "ToolTipTitle"),
                                             new Pair(MapKryptonPageText.ToolTipBody,                   "ToolTipBody"),
        };
        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the MapKryptonPageTextConverter clas.
        /// </summary>
        public MapKryptonPageTextConverter()
            : base(typeof(MapKryptonPageText))
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
