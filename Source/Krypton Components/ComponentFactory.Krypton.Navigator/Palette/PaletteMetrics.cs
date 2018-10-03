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
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Storage for metrics that can be overriden by the developer.
	/// </summary>
    public class PaletteMetrics : Storage
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private int _pageButtonSpecInset;
        private Padding _pageButtonSpecPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMetrics class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMetrics(KryptonNavigator navigator,
                              NeedPaintHandler needPaint)
		{
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            _pageButtonSpecInset = -1;
            _pageButtonSpecPadding = CommonHelper.InheritPadding;
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return ((PageButtonSpecInset == -1) && 
                         PageButtonSpecPadding.Equals(CommonHelper.InheritPadding));
            }
        }
        #endregion

        #region PageButtonSpecInset
        /// <summary>
        /// Gets and sets the pixel inset of button specs from the edge of the page header.
        /// </summary>
        [Category("Visuals")]
        [Description("Pixel inset of button specs from the edge of the page header.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(-1)]
        public int PageButtonSpecInset
        {
            get { return _pageButtonSpecInset; }

            set
            {
                if (_pageButtonSpecInset != value)
                {
                    _pageButtonSpecInset = value;

                    if (_navigator != null)
                        _navigator.OnViewBuilderPropertyChanged("PageButtonSpecInset");
                }
            }
        }

        /// <summary>
        /// Resets the PageButtonSpecInset property to its default value.
        /// </summary>
        public void ResetPageButtonSpecInset()
        {
            PageButtonSpecInset = -1;
        }
        #endregion

        #region PageButtonSpecPadding
        /// <summary>
        /// Gets and sets the pixel padding around the button specs on a page header.
        /// </summary>
        [Category("Visuals")]
        [Description("Pixel padding around the button specs on a page header.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding PageButtonSpecPadding
        {
            get { return _pageButtonSpecPadding; }

            set
            {
                if (_pageButtonSpecPadding != value)
                {
                    _pageButtonSpecPadding = value;

                    if (_navigator != null)
                        _navigator.OnViewBuilderPropertyChanged("PageButtonSpecPadding");
                }
            }
        }

        /// <summary>
        /// Resets the PageButtonSpecPadding property to its default value.
        /// </summary>
        public void ResetPageButtonSpecPadding()
        {
            PageButtonSpecPadding = CommonHelper.InheritPadding;
        }

        private bool ShouldSerializePageButtonSpecPadding()
        {
            return !PageButtonSpecPadding.Equals(CommonHelper.InheritPadding);
        }
        #endregion
    }
}
