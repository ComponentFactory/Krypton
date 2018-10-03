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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Redirect requests for image/text colors to remap.
    /// </summary>
    internal class ButtonSpecNavRemap : PaletteRedirect
    {
        #region Enum ButtonSpecRemapTarget
        /// <summary>
        /// Specifies the target for remapping the source color onto.
        /// </summary>
        public enum ButtonSpecRemapTarget
        {
            /// <summary>Specifies remapping to the label control text color.</summary>
            LabelControl,

            /// <summary>Specifies remapping to the label panel text color.</summary>
            LabelPanel,

            /// <summary>Specifies remapping to the button standalone text color.</summary>
            ButtonStandalone,

            /// <summary>Specifies remapping to the button alternate text color.</summary>
            ButtonAlternate,

            /// <summary>Specifies remapping to the button bread crumb text color.</summary>
            ButtonBreadCrumb,

            /// <summary>Specifies remapping to the button spec text color.</summary>
            ButtonButtonSpec,

            /// <summary>Specifies remapping to the button calendar day text color.</summary>
            ButtonCalendarDay,

            /// <summary>Specifies remapping to the button cluster text color.</summary>
            ButtonCluster,

            /// <summary>Specifies remapping to the button custom1 text color.</summary>
            ButtonCustom1,

            /// <summary>Specifies remapping to the button custom2 text color.</summary>
            ButtonCustom2,

            /// <summary>Specifies remapping to the button custom3 text color.</summary>
            ButtonCustom3,

            /// <summary>Specifies remapping to the button form text color.</summary>
            ButtonForm,

            /// <summary>Specifies remapping to the button form close text color.</summary>
            ButtonFormClose,

            /// <summary>Specifies remapping to the button gallery text color.</summary>
            ButtonGallery,

            /// <summary>Specifies remapping to the button input control text color.</summary>
            ButtonInputControl,

            /// <summary>Specifies remapping to the button list item text color.</summary>
            ButtonListItem,

            /// <summary>Specifies remapping to the button low profile text color.</summary>
            ButtonLowProfile,

            /// <summary>Specifies remapping to the button navigator mini text color.</summary>
            ButtonNavigatorMini,

            /// <summary>Specifies remapping to the button navigator overflow text color.</summary>
            ButtonNavigatorOverflow,

            /// <summary>Specifies remapping to the button navigator stack text color.</summary>
            ButtonNavigatorStack,

            /// <summary>Specifies remapping to the button command text color.</summary>
            ButtonCommand,

            /// <summary>Specifies remapping to the tab high profile text color.</summary>
            TabHighProfile,

            /// <summary>Specifies remapping to the tab standard profile text color.</summary>
            TabStandardProfile,

            /// <summary>Specifies remapping to the tab low profile text color.</summary>
            TabLowProfile,

            /// <summary>Specifies remapping to the tab one note text color.</summary>
            TabOneNote,

            /// <summary>Specifies remapping to the tab dock text color.</summary>
            TabDock,

            /// <summary>Specifies remapping to the tab dock auto hidden text color.</summary>
            TabDockAutoHidden,

            /// <summary>Specifies remapping to the tab custom1 text color.</summary>
            TabCustom1,

            /// <summary>Specifies remapping to the tab custom2 text color.</summary>
            TabCustom2,

            /// <summary>Specifies remapping to the tab custom3 text color.</summary>
            TabCustom3
        }
        #endregion

        #region Instance Fields
        private ButtonSpecRemapTarget _remapTarget;
        private ButtonSpec _buttonSpec;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavRemapDisabled class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        /// <param name="remapTarget">Target for remapping the color onto.</param>
        public ButtonSpecNavRemap(IPalette target,
                                  ButtonSpec buttonSpec,
                                  ButtonSpecRemapTarget remapTarget)
            : base(target)
        {
            Debug.Assert(buttonSpec != null);

            _buttonSpec = buttonSpec;
            _remapTarget = remapTarget;
        }
		#endregion

        #region GetContentImageColorMap
        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            // If allowed to override then get the map color
            Color mapColor = OverrideImageColor(state);

            // If a map color provided then return is
            if (mapColor != Color.Empty)
                return mapColor;
            else
                return base.GetContentImageColorMap(style, state);
        }
        #endregion

        #region GetContentImageColorTo
        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            // If allowed to override then get the map color
            Color mapColor = OverrideImageColor(state);

            // If mapping occuring then return the target remap color
            if (mapColor != Color.Empty)
                return GetRemapTarget(style, state);
            else
                return base.GetContentImageColorTo(style, state);
        }
        #endregion

        #region GetContentShortTextColor1
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Do we need to override the text color
            if (OverrideTextColor(state))
                return GetRemapTarget(style, state);
            else
                return base.GetContentShortTextColor1(style, state);
        }
        #endregion

        #region GetContentLongTextColor1
        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Do we need to override the text color
            if (OverrideTextColor(state))
                return GetRemapTarget(style, state);
            else
                return base.GetContentLongTextColor1(style, state);
        }
        #endregion

        #region Implementation
        private Color OverrideImageColor(PaletteState state)
        {
            // We are only interested in overriding the normal and disabled colors
            if ((state == PaletteState.Normal) ||
                (state == PaletteState.Disabled))
            {
                // Get the color map from the button spec
                Color mapColor = _buttonSpec.GetColorMap(base.Target);

                // If we are supposed to remap a color
                if (mapColor != Color.Empty)
                {
                    // Get the button style requested
                    ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);

                    // If we are checking for button styles of ButtonSpec only, then do so
                    if (buttonStyle == ButtonStyle.ButtonSpec)
                        return mapColor;
                }
            }

            return Color.Empty;
        }

        private bool OverrideTextColor(PaletteState state)
        {
            // We are only interested in overriding the disabled or normal colors
            if ((state == PaletteState.Normal) ||
                (state == PaletteState.Disabled))
            {
                // Get the button style requested
                ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);

                // If we are checking for button styles of ButtonSpec only, then do so
                if (buttonStyle == ButtonStyle.ButtonSpec)
                    return true;
            }

            return false;
        }

        private Color GetRemapTarget(PaletteContentStyle style, PaletteState state)
        {
            // Choose appropriate remapping target for color to use
            switch (_remapTarget)
            {
                case ButtonSpecRemapTarget.LabelControl:
                    return base.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, state);
                case ButtonSpecRemapTarget.LabelPanel:
                    return base.GetContentShortTextColor1(PaletteContentStyle.LabelNormalPanel, state);
                case ButtonSpecRemapTarget.ButtonAlternate:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonAlternate, state);
                case ButtonSpecRemapTarget.ButtonBreadCrumb:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonBreadCrumb, state);
                case ButtonSpecRemapTarget.ButtonButtonSpec:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonButtonSpec, state);
                case ButtonSpecRemapTarget.ButtonCalendarDay:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCalendarDay, state);
                case ButtonSpecRemapTarget.ButtonCluster:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCluster, state);
                case ButtonSpecRemapTarget.ButtonCustom1:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCustom1, state);
                case ButtonSpecRemapTarget.ButtonCustom2:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCustom2, state);
                case ButtonSpecRemapTarget.ButtonCustom3:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCustom3, state);
                case ButtonSpecRemapTarget.ButtonForm:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonForm, state);
                case ButtonSpecRemapTarget.ButtonFormClose:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonFormClose, state);
                case ButtonSpecRemapTarget.ButtonGallery:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonGallery, state);
                case ButtonSpecRemapTarget.ButtonInputControl:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonInputControl, state);
                case ButtonSpecRemapTarget.ButtonListItem:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonListItem, state);
                case ButtonSpecRemapTarget.ButtonLowProfile:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonLowProfile, state);
                case ButtonSpecRemapTarget.ButtonNavigatorMini:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonNavigatorMini, state);
                case ButtonSpecRemapTarget.ButtonNavigatorOverflow:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonNavigatorOverflow, state);
                case ButtonSpecRemapTarget.ButtonNavigatorStack:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonNavigatorStack, state);
                case ButtonSpecRemapTarget.ButtonStandalone:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonStandalone, state);
                case ButtonSpecRemapTarget.ButtonCommand:
                    return base.GetContentShortTextColor1(PaletteContentStyle.ButtonCommand, state);
                case ButtonSpecRemapTarget.TabHighProfile:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabHighProfile, state);
                case ButtonSpecRemapTarget.TabStandardProfile:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabStandardProfile, state);
                case ButtonSpecRemapTarget.TabLowProfile:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabLowProfile, state);
                case ButtonSpecRemapTarget.TabOneNote:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabOneNote, state);
                case ButtonSpecRemapTarget.TabDock:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabDock, state);
                case ButtonSpecRemapTarget.TabDockAutoHidden:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabDockAutoHidden, state);
                case ButtonSpecRemapTarget.TabCustom1:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabCustom1, state);
                case ButtonSpecRemapTarget.TabCustom2:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabCustom2, state);
                case ButtonSpecRemapTarget.TabCustom3:
                    return base.GetContentShortTextColor1(PaletteContentStyle.TabCustom3, state);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Black;
            }
        }
        #endregion
    }
}
