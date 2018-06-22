﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Manage a collection of button specs for placing within a collection of ViewLayoutDocker instances.
    /// </summary>
    public class ButtonSpecManagerLayoutAppButton : ButtonSpecManagerLayout
    {
        #region Instance Fields
        private ViewContextMenuManager _viewManager;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecManagerLayoutAppButton class.
        /// </summary>
        /// <param name="viewManager">Reference to context menu view manager.</param>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="fixedSpecs">Fixed set of button specifications.</param>
        /// <param name="viewDockers">Array of target view dockers.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricInt">Array of target metrics for outside/inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecManagerLayoutAppButton(ViewContextMenuManager viewManager,
                                                Control control,
                                                PaletteRedirect redirector,
                                                ButtonSpecCollectionBase variableSpecs,
                                                ButtonSpecCollectionBase fixedSpecs,
                                                ViewLayoutDocker[] viewDockers,
                                                IPaletteMetric[] viewMetrics,
                                                PaletteMetricInt[] viewMetricInt,
                                                PaletteMetricPadding[] viewMetricPaddings,
                                                GetToolStripRenderer getRenderer,
                                                NeedPaintHandler needPaint)
            : base(control, redirector, variableSpecs, fixedSpecs,
                   viewDockers, viewMetrics, viewMetricInt,
                   viewMetricPaddings, getRenderer, needPaint)
        {
            _viewManager = viewManager;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the view manager associated with the button specs.
        /// </summary>
        public ViewContextMenuManager ViewManager
        {
            get { return _viewManager; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the button spec view appropriate for the button spec.
        /// </summary>
        /// <param name="redirector">Redirector for acquiring palette values.</param>
        /// <param name="viewPaletteMetric">Target metric providers.</param>
        /// <param name="viewMetricPadding">Target metric padding.</param>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>ButtonSpecView derived class.</returns>
        protected override ButtonSpecView CreateButtonSpecView(PaletteRedirect redirector, 
                                                               IPaletteMetric viewPaletteMetric, 
                                                               PaletteMetricPadding viewMetricPadding, 
                                                               ButtonSpec buttonSpec)
        {
            return new ButtonSpecViewAppButton(redirector, viewPaletteMetric,
                                               viewMetricPadding, this, buttonSpec);
        }
        #endregion
    }
}
