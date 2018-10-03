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
    /// Base class for implementation of 'Bar' modes.
	/// </summary>
    internal abstract class ViewBuilderBarItemBase : ViewBuilderItemBase
    {
        #region Instance Fields
        protected ViewLayoutDocker _layoutPanelDocker;
        protected ViewLayoutSeparator _layoutBarSeparatorFirst;
        protected ViewLayoutSeparator _layoutBarSeparatorLast;
        #endregion

        #region Public
        /// <summary>
        /// Gets the appropriate popup page position for the current mode.
        /// </summary>
        /// <returns>Calculated PopupPagePosition</returns>
        public override PopupPagePosition GetPopupPagePosition()
        {
            switch (Navigator.Bar.BarOrientation)
            {
                default:
                case VisualOrientation.Top:
                    return PopupPagePosition.BelowNear;
                case VisualOrientation.Bottom:
                    return PopupPagePosition.AboveNear;
                case VisualOrientation.Left:
                    return PopupPagePosition.FarTop;
                case VisualOrientation.Right:
                    return PopupPagePosition.NearTop;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Process the change in a property that might effect the view builder.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Property changed details.</param>
        protected override void OnViewBuilderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "BarOrientation":
                    UpdateOrientation();
                    UpdateItemOrientation();
                    _buttonManager.RecreateButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "BarFirstItemInset":
                    UpdateFirstItemInset();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "BarLastItemInset":
                    UpdateLastItemInset();
                    Navigator.PerformNeedPaint(true);
                    break;
                default:
                    // We do not recognise the property, let base process it
                    base.OnViewBuilderPropertyChanged(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Update the bar orientation.
        /// </summary>
        protected override void UpdateOrientation()
        {
            switch (Navigator.Bar.BarOrientation)
            {
                case VisualOrientation.Top:
                    _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Top);
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                    break;
                case VisualOrientation.Bottom:
                    _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Bottom);
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                    break;
                case VisualOrientation.Left:
                    _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Left);
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.CounterAlignment = RelativePositionAlign.Near;
                    break;
                case VisualOrientation.Right:
                    _layoutPanelDocker.SetDock(_layoutBarDocker, ViewDockStyle.Right);
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.CounterAlignment = RelativePositionAlign.Far;
                    break;
            }

            _layoutBar.Orientation = Navigator.Bar.BarOrientation;
            _layoutBarViewport.Orientation = Navigator.Bar.BarOrientation;

            UpdateFirstItemInset();
            UpdateLastItemInset();
        }

        /// <summary>
        /// Update the separator used to inset the first item.
        /// </summary>
        protected void UpdateFirstItemInset()
        {
            switch (Navigator.Bar.BarOrientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    _layoutBarSeparatorFirst.SeparatorSize = new Size(Navigator.Bar.BarFirstItemInset, 0);
                    _layoutBarDocker.SetDock(_layoutBarSeparatorFirst, ViewDockStyle.Left);
                    break;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    _layoutBarSeparatorFirst.SeparatorSize = new Size(0, Navigator.Bar.BarFirstItemInset);
                    _layoutBarDocker.SetDock(_layoutBarSeparatorFirst, ViewDockStyle.Left);
                    break;
            }
        }

        /// <summary>
        /// Update the separator used to inset the last item.
        /// </summary>
        protected void UpdateLastItemInset()
        {
            switch (Navigator.Bar.BarOrientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    _layoutBarSeparatorLast.SeparatorSize = new Size(Navigator.Bar.BarLastItemInset, 0);
                    _layoutBarDocker.SetDock(_layoutBarSeparatorLast, ViewDockStyle.Right);
                    break;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    _layoutBarSeparatorLast.SeparatorSize = new Size(0, Navigator.Bar.BarLastItemInset);
                    _layoutBarDocker.SetDock(_layoutBarSeparatorLast, ViewDockStyle.Right);
                    break;
            }
        }
        #endregion
    }
}
