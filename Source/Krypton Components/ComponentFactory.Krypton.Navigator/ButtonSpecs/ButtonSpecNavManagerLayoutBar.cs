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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Manage a collection of button specs for use with a ViewLayoutDocker style bar.
    /// </summary>
    internal class ButtonSpecNavManagerLayoutBar : ButtonSpecManagerLayout
    {
        #region Instance Fields
        private ButtonSpecNavRemap.ButtonSpecRemapTarget _remapTarget;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavManagerLayoutBar class.
        /// </summary>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="viewDockers">Array of target view dockers.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricIntOutside">Array of target metrics for outside spacer size.</param>
        /// <param name="viewMetricIntInside">Array of target metrics for inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecNavManagerLayoutBar(Control control,
                                             PaletteRedirect redirector,
                                             ButtonSpecCollectionBase variableSpecs,
                                             ViewLayoutDocker[] viewDockers,
                                             IPaletteMetric[] viewMetrics,
                                             PaletteMetricInt[] viewMetricIntOutside,
                                             PaletteMetricInt[] viewMetricIntInside,
                                             PaletteMetricPadding[] viewMetricPaddings,
                                             GetToolStripRenderer getRenderer,
                                             NeedPaintHandler needPaint)
            : this(control, redirector, variableSpecs,
                   null, viewDockers, viewMetrics, 
                   viewMetricIntOutside, viewMetricIntInside,
                   viewMetricPaddings, getRenderer, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavManagerLayoutBar class.
        /// </summary>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="fixedSpecs">Fixed set of button specifications.</param>
        /// <param name="viewDockers">Array of target view dockers.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricIntOutside">Array of target metrics for outside spacer size.</param>
        /// <param name="viewMetricIntInside">Array of target metrics for inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecNavManagerLayoutBar(Control control,
                                             PaletteRedirect redirector,
                                             ButtonSpecCollectionBase variableSpecs,
                                             ButtonSpecCollectionBase fixedSpecs,
                                             ViewLayoutDocker[] viewDockers,
                                             IPaletteMetric[] viewMetrics,
                                             PaletteMetricInt[] viewMetricIntOutside,
                                             PaletteMetricInt[] viewMetricIntInside,
                                             PaletteMetricPadding[] viewMetricPaddings,
                                             GetToolStripRenderer getRenderer,
                                             NeedPaintHandler needPaint)
            : base(control, redirector, variableSpecs, fixedSpecs, 
                   viewDockers, viewMetrics, viewMetricIntOutside,
                   viewMetricIntInside, viewMetricPaddings, getRenderer, 
                   needPaint)
        {
            _remapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.LabelPanel;
        }
        #endregion

        #region Public
        /// <summary>
        /// Required target for the remapping.
        /// </summary>
        public ButtonSpecNavRemap.ButtonSpecRemapTarget RemapTarget
        {
            get { return _remapTarget; }
            set { _remapTarget = value; }
        }

        /// <summary>
        /// Update the remap target to match the tab style.
        /// </summary>
        /// <param name="style">Tab style to match.</param>
        public void SetRemapTarget(TabStyle style)
        {
            switch (style)
            {
                case TabStyle.HighProfile:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabHighProfile;
                    break;
                case TabStyle.StandardProfile:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabStandardProfile;
                    break;
                case TabStyle.LowProfile:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabLowProfile;
                    break;
                case TabStyle.OneNote:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabOneNote;
                    break;
                case TabStyle.Dock:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabDock;
                    break;
                case TabStyle.DockAutoHidden:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabDockAutoHidden;
                    break;
                case TabStyle.Custom1:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabCustom1;
                    break;
                case TabStyle.Custom2:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabCustom2;
                    break;
                case TabStyle.Custom3:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.TabCustom3;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Update the remap target to match the button style.
        /// </summary>
        /// <param name="style">Button style to match.</param>
        public void SetRemapTarget(ButtonStyle style)
        {
            switch (style)
            {
                case ButtonStyle.Alternate:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonAlternate;
                    break;
                case ButtonStyle.BreadCrumb:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonBreadCrumb;
                    break;
                case ButtonStyle.ButtonSpec:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonButtonSpec;
                    break;
                case ButtonStyle.CalendarDay:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCalendarDay;
                    break;
                case ButtonStyle.Cluster:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCluster;
                    break;
                case ButtonStyle.Custom1:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCustom1;
                    break;
                case ButtonStyle.Custom2:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCustom2;
                    break;
                case ButtonStyle.Custom3:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCustom3;
                    break;
                case ButtonStyle.Form:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonForm;
                    break;
                case ButtonStyle.FormClose:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonFormClose;
                    break;
                case ButtonStyle.Gallery:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonGallery;
                    break;
                case ButtonStyle.InputControl:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonInputControl;
                    break;
                case ButtonStyle.ListItem:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonListItem;
                    break;
                case ButtonStyle.LowProfile:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonLowProfile;
                    break;
                case ButtonStyle.NavigatorMini:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonNavigatorMini;
                    break;
                case ButtonStyle.NavigatorOverflow:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonNavigatorOverflow;
                    break;
                case ButtonStyle.NavigatorStack:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonNavigatorStack;
                    break;
                case ButtonStyle.Standalone:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonStandalone;
                    break;
                case ButtonStyle.Command:
                    RemapTarget = ButtonSpecNavRemap.ButtonSpecRemapTarget.ButtonCommand;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Create a palette redirector for remapping button spec colors.
        /// </summary>
        /// <param name="redirector">Base palette class.</param>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>Palette redirector for the button spec instance.</returns>
        public override PaletteRedirect CreateButtonSpecRemap(PaletteRedirect redirector,
                                                              ButtonSpec buttonSpec)
        {
            return new ButtonSpecNavRemap(redirector, buttonSpec, RemapTarget);
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets a value indicating if inside edge spacers are required.
        /// </summary>
        protected override bool UseInsideSpacers
        {
            get { return true; }
        }

        /// <summary>
        /// Perform final steps now that the button spec has been created.
        /// </summary>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <param name="buttonView">Associated ButtonSpecView instance.</param>
        /// <param name="viewDockerIndex">Index of view docker button is placed onto.</param>
        protected override void ButtonSpecCreated(ButtonSpec buttonSpec, 
                                                  ButtonSpecView buttonView, 
                                                  int viewDockerIndex)
        {
            // Nothing extra to do
        }        
        #endregion
    }
}
