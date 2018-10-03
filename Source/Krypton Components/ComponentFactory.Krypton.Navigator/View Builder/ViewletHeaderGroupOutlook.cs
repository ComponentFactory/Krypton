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
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Provides HeaderGroup functionality modified to work in the Outlook mode.
    /// </summary>
    internal class ViewletHeaderGroupOutlook : ViewletHeaderGroup
    {
        #region Instance Fields
        private bool _full;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewletHeaderGroupOutlook class.
        /// </summary>
        /// <param name="navigator">Reference to navigator instance.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint requests.</param>
        public ViewletHeaderGroupOutlook(KryptonNavigator navigator,
                                         PaletteRedirect redirector,
                                         NeedPaintHandler needPaintDelegate)
            : base(navigator, redirector, needPaintDelegate)
        {
            // Are we using the full or mini outlook mode.
            _full = (navigator.NavigatorMode == NavigatorMode.OutlookFull);
        }
        #endregion

        #region Public
        /// <summary>
        /// Process the change in a property that might effect the viewlet.
        /// </summary>
        /// <param name="e">Property changed details.</param>
        public override void ViewBuilderPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HeaderSecondaryVisibleOutlook":
                    // Call base class but update the standard visible property
                    e = new PropertyChangedEventArgs("HeaderVisibleSecondary");
                    break;
            }

            // Let base class handle property
            base.ViewBuilderPropertyChanged(e);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the visible state of the secondary header.
        /// </summary>
        /// <returns>Boolean value.</returns>
        protected override bool GetHeaderSecondaryVisible()
        {
            // Work out the correct visiblity value to use
            switch (Navigator.Outlook.HeaderSecondaryVisible)
            {
                case InheritBool.Inherit:
                    return Navigator.Header.HeaderVisibleSecondary;
                case InheritBool.True:
                    return true;
                case InheritBool.False:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the source of the primary header values.
        /// </summary>
        /// <returns></returns>
        protected override IContentValues GetPrimaryValues()
        {
            if (_full)
                return Navigator.Header.HeaderValuesPrimary;
            else
                return CommonHelper.NullContentValues;
        }

        /// <summary>
        /// Gets the source of the secondary header values.
        /// </summary>
        /// <returns></returns>
        protected override IContentValues GetSecondaryValues()
        {
            if (_full)
                return Navigator.Header.HeaderValuesSecondary;
            else
                return CommonHelper.NullContentValues;
        }
        #endregion
    }
}
