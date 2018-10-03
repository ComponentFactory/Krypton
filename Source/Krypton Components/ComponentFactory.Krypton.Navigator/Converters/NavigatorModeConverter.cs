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
    /// Custom type converter so that NavigatorMode values appear as neat text at design time.
    /// </summary>
    public class NavigatorModeConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(NavigatorMode.BarTabGroup,                    "Bar - Tab - Group"),
                                             new Pair(NavigatorMode.BarTabOnly,                     "Bar - Tab - Only"),
                                             new Pair(NavigatorMode.BarRibbonTabGroup,              "Bar - RibbonTab - Group"),
                                             new Pair(NavigatorMode.BarRibbonTabOnly,               "Bar - RibbonTab - Only"),
                                             new Pair(NavigatorMode.BarCheckButtonGroupOutside,     "Bar - CheckButton - Group - Outside"),
                                             new Pair(NavigatorMode.BarCheckButtonGroupInside,      "Bar - CheckButton - Group - Inside"),
                                             new Pair(NavigatorMode.BarCheckButtonGroupOnly,        "Bar - CheckButton - Group - Only"),
                                             new Pair(NavigatorMode.BarCheckButtonOnly,             "Bar - CheckButton - Only"),
                                             new Pair(NavigatorMode.HeaderBarCheckButtonGroup,      "HeaderBar - CheckButton - Group"),
                                             new Pair(NavigatorMode.HeaderBarCheckButtonHeaderGroup,"HeaderBar - CheckButton - HeaderGroup"),
                                             new Pair(NavigatorMode.HeaderBarCheckButtonOnly,       "HeaderBar - CheckButton - Only"),
                                             new Pair(NavigatorMode.StackCheckButtonGroup,          "Stack - CheckButton - Group"),
                                             new Pair(NavigatorMode.StackCheckButtonHeaderGroup,    "Stack - CheckButton - HeaderGroup"),
                                             new Pair(NavigatorMode.OutlookFull,                    "Outlook - Full"),
                                             new Pair(NavigatorMode.OutlookMini,                    "Outlook - Mini"),
                                             new Pair(NavigatorMode.Panel,                          "Panel"),
                                             new Pair(NavigatorMode.Group,                          "Group"),
                                             new Pair(NavigatorMode.HeaderGroup,                    "HeaderGroup"),
                                             new Pair(NavigatorMode.HeaderGroupTab,                 "HeaderGroup - Tab") };
        #endregion
                                             
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorMode clas.
        /// </summary>
        public NavigatorModeConverter()
            : base(typeof(NavigatorMode))
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
