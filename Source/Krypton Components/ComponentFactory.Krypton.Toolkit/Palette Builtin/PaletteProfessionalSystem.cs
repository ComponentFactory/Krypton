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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provides a professional appearance using colors/fonts generated from system settings.
	/// </summary>
	public class PaletteProfessionalSystem : PaletteBase
	{
		#region Static Fields
        private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader1 = new Padding(3, 2, 3, 2);
        private static readonly Padding _contentPaddingHeader2 = new Padding(3, 2, 3, 2);
        private static readonly Padding _contentPaddingHeader3 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingCalendar = new Padding(2);
        private static readonly Padding _contentPaddingHeaderForm = new Padding(5, 1, 3, 1);
        private static readonly Padding _contentPaddingLabel = new Padding(3, 2, 3, 2);
        private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
        private static readonly Padding _contentPaddingButtonCalendar = new Padding(0);
        private static readonly Padding _contentPaddingButtonInputControl = new Padding(1);
        private static readonly Padding _contentPaddingButton12 = new Padding(3, 2, 3, 2);
        private static readonly Padding _contentPaddingButton3 = new Padding(1, 1, 1, 1);
        private static readonly Padding _contentPaddingButton4 = new Padding(4, 3, 4, 3);
        private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 2);
        private static readonly Padding _contentPaddingButton6 = new Padding(3);
        private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 3, 1);
        private static readonly Padding _contentPaddingButtonForm = new Padding(5, 5, 5, 5);
        private static readonly Padding _contentPaddingButtonGallery = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingToolTip = new Padding(2, 2, 2, 2);
        private static readonly Padding _contentPaddingSuperTip = new Padding(4, 4, 4, 4);
        private static readonly Padding _contentPaddingKeyTip = new Padding(1, -1, 0, -2);
        private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
        private static readonly Padding _contentPaddingContextMenuImage = new Padding(1);
        private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
        private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
        private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
        private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
        private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
        private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
        private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
        private static readonly Padding _metricPaddingHeaderForm = new Padding(0, 0, 0, 0);
        private static readonly Padding _metricPaddingBarInside = new Padding(3, 3, 3, 3);
        private static readonly Padding _metricPaddingBarTabs = new Padding(0, 0, 0, 0);
        private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
        private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);
        private static readonly Padding _metricPaddingContextMenuItemHighlight = new Padding(1, 0, 1, 0);
        private static readonly Padding _metricPaddingContextMenuItemsCollection = new Padding(0, 1, 0, 1);

        private static readonly Image _buttonSpecClose = Properties.Resources.ProfessionalCloseButton;
        private static readonly Image _buttonSpecContext = Properties.Resources.ProfessionalContextButton;
        private static readonly Image _buttonSpecNext = Properties.Resources.ProfessionalNextButton;
        private static readonly Image _buttonSpecPrevious = Properties.Resources.ProfessionalPreviousButton;
        private static readonly Image _buttonSpecArrowLeft = Properties.Resources.ProfessionalArrowLeftButton;
        private static readonly Image _buttonSpecArrowRight = Properties.Resources.ProfessionalArrowRightButton;
        private static readonly Image _buttonSpecArrowUp = Properties.Resources.ProfessionalArrowUpButton;
        private static readonly Image _buttonSpecArrowDown = Properties.Resources.ProfessionalArrowDownButton;
        private static readonly Image _buttonSpecDropDown = Properties.Resources.ProfessionalDropDownButton;
        private static readonly Image _buttonSpecPinVertical = Properties.Resources.ProfessionalPinVerticalButton;
        private static readonly Image _buttonSpecPinHorizontal = Properties.Resources.ProfessionalPinHorizontalButton;
        private static readonly Image _buttonSpecWorkspaceMaximize = Properties.Resources.ProfessionalMaximize;
        private static readonly Image _buttonSpecWorkspaceRestore = Properties.Resources.ProfessionalRestore;
        private static readonly Image _buttonSpecRibbonMinimize = Properties.Resources.RibbonUp2010;
        private static readonly Image _buttonSpecRibbonExpand = Properties.Resources.RibbonDown2010;
        private static readonly Image _systemCloseA = Properties.Resources.ProfessionalButtonCloseA;
        private static readonly Image _systemCloseI = Properties.Resources.ProfessionalButtonCloseI;
        private static readonly Image _systemMaxA = Properties.Resources.ProfessionalButtonMaxA;
        private static readonly Image _systemMaxI = Properties.Resources.ProfessionalButtonMaxI;
        private static readonly Image _systemMinA = Properties.Resources.ProfessionalButtonMinA;
        private static readonly Image _systemMinI = Properties.Resources.ProfessionalButtonMinI;
        private static readonly Image _systemRestoreA = Properties.Resources.ProfessionalButtonRestoreA;
        private static readonly Image _systemRestoreI = Properties.Resources.ProfessionalButtonRestoreI;
        private static readonly Image _pendantCloseA = Properties.Resources.ProfessionalPendantCloseA;
        private static readonly Image _pendantCloseI = Properties.Resources.ProfessionalPendantCloseI;
        private static readonly Image _pendantMinA = Properties.Resources.ProfessionalPendantMinA;
        private static readonly Image _pendantMinI = Properties.Resources.ProfessionalPendantMinI;
        private static readonly Image _pendantRestoreA = Properties.Resources.ProfessionalPendantRestoreA;
        private static readonly Image _pendantRestoreI = Properties.Resources.ProfessionalPendantRestoreI;
        private static readonly Image _pendantExpandA = Properties.Resources.ProfessionalPendantExpandA;
        private static readonly Image _pendantExpandI = Properties.Resources.ProfessionalPendantExpandI;
        private static readonly Image _pendantMinimizeA = Properties.Resources.ProfessionalPendantMinimizeA;
        private static readonly Image _pendantMinimizeI = Properties.Resources.ProfessionalPendantMinimizeI;
        private static readonly Image _contextMenuChecked = Properties.Resources.SystemChecked;
        private static readonly Image _contextMenuIndeterminate = Properties.Resources.SystemIndeterminate;
        private static readonly Image _contextMenuSubMenu = Properties.Resources.SystemContextMenuSub;
        private static readonly Image _treeExpandPlus = Properties.Resources.TreeExpandPlus;
        private static readonly Image _treeCollapseMinus = Properties.Resources.TreeCollapseMinus;

        private static readonly Color _contextTextColor = Color.White;
        private static readonly Color _lightGray = Color.FromArgb(242, 242, 242);
        private static readonly Color _contextCheckedTabBorder1 = Color.FromArgb(223, 119, 0);
        private static readonly Color _contextCheckedTabBorder2 = Color.FromArgb(230, 190, 129);
        private static readonly Color _contextCheckedTabBorder3 = Color.FromArgb(220, 202, 171);
        private static readonly Color _contextCheckedTabBorder4 = Color.FromArgb(255, 252, 247);
        #endregion

		#region Instance Fields
		private KryptonProfessionalKCT _table;
        private Font _header1ShortFont;
        private Font _header2ShortFont;
        private Font _header1LongFont;
        private Font _header2LongFont;
        private Font _superToolFont;
        private Font _headerFormFont;
        private Font _buttonFont;
        private Font _buttonFontNavigatorMini;
        private Font _tabFontNormal;
        private Font _tabFontSelected;
        private Font _gridFont;
        private Font _calendarFont;
        private Font _calendarBoldFont;
        private Font _boldFont;
        private Font _italicFont;
        private Image _disabledDropDownImage;
        private Image _normalDropDownImage;
        private Color _disabledDropDownColor;
        private Color _normalDropDownColor;
        private Color[] _ribbonColors;
        private Color _disabledText;
        private Color _disabledGlyphDark;
        private Color _disabledGlyphLight;
        private Color _contextCheckedTabBorder;
        private Color _contextCheckedTabFill;
        private Color _contextGroupAreaBorder;
        private Color _contextGroupAreaInside;
        private Color _contextGroupFrameTop;
        private Color _contextGroupFrameBottom;
        private Color _contextTabSeparator;
        private Color _focusTabFill;
        private Color _toolTipBack1;
        private Color _toolTipBack2;
        private Color _toolTipBorder;
        private Color _toolTipText;
        private Color[] _ribbonGroupCollapsedBackContext;
        private Color[] _ribbonGroupCollapsedBackContextTracking;
        private Color[] _ribbonGroupCollapsedBorderContext;
        private Color[] _ribbonGroupCollapsedBorderContextTracking;
        private Color[] _appButtonNormal;
        private Color[] _appButtonTrack;
        private Color[] _appButtonPressed;
        private Image _galleryImageUp;
        private Image _galleryImageDown;
        private Image _galleryImageDropDown;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteProfessionalSystem class.
		/// </summary>
        public PaletteProfessionalSystem()
		{
            // Get the font settings from the system
            DefineFonts();
            
            // Generate the myriad ribbon colors from system settings
            DefineRibbonColors();
        }
		#endregion

        #region AllowFormChrome
        /// <summary>
        /// Gets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetAllowFormChrome()
        {
            return InheritBool.False;
        }
        #endregion

        #region Renderer
        /// <summary>
        /// Gets the renderer to use for this palette.
        /// </summary>
        /// <returns>Renderer to use for drawing palette settings.</returns>
        public override IRenderer GetRenderer()
        {
            // We always want the professional renderer
            return KryptonManager.RenderProfessional;
        }
        #endregion

        #region Back
        /// <summary>
		/// Gets a value indicating if background should be drawn.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;
            switch (style)
            {
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                    return InheritBool.False;
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                default:
                    // Default to drawing the background
                    return InheritBool.True;
            }
		}

		/// <summary>
		/// Gets the graphics drawing hint for the background.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteGraphicsHint.Inherit;

            switch (style)
            {
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteGraphicsHint.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
		}

		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
				return Color.Empty;

			switch (style)
			{
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        default:
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.CheckedNormal:
                            return ColorTable.CheckBackground;
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return ColorTable.ButtonPressedHighlight;
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                    return ColorTable.MenuStripGradientBegin;
                case PaletteBackStyle.HeaderForm:
                    return Table.Header1Begin;
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return ColorTable.MenuStripGradientEnd;
                case PaletteBackStyle.PanelAlternate:
                    return ColorTable.MenuStripGradientBegin;
                case PaletteBackStyle.ControlClient:
				case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return SystemColors.Window;
                case PaletteBackStyle.ContextMenuHeading:
                    return ColorTable.OverflowButtonGradientBegin;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedBorder;
                        default:
                            return ColorTable.MenuBorder;
                    }
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return ColorTable.ImageMarginGradientBegin;
                case PaletteBackStyle.ContextMenuItemImage:
                    return ColorTable.ButtonSelectedHighlight;
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack1];
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                    return ColorTable.ToolStripDropDownBackground;
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return Table.Header1Begin;
                case PaletteBackStyle.HeaderDockInactive:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return SystemColors.InactiveCaption;
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return SystemColors.ActiveCaption;
                case PaletteBackStyle.HeaderSecondary:
					if (state == PaletteState.Disabled)
						return SystemColors.Control;
					else
						return ColorTable.MenuStripGradientEnd;
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return SystemColors.Control;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return SystemColors.Window;
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else if (style == PaletteBackStyle.TabHighProfile)
                                return ColorTable.ButtonPressedGradientMiddle;
                            else
                                return SystemColors.Window;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.TabHighProfile)
                                return ColorTable.ButtonPressedGradientMiddle;
                            else
                                return SystemColors.Window;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return SystemColors.Window;
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return SystemColors.Window;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack1;
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                    switch (state)
					{
						case PaletteState.Disabled:
                            return SystemColors.Control;
						case PaletteState.Normal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientEnd;
						case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientBegin;
						case PaletteState.Tracking:
							return ColorTable.ButtonSelectedGradientBegin;
						case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            if (style == PaletteBackStyle.ButtonAlternate)
                                return ColorTable.SeparatorDark;
                            else
                                return ColorTable.ButtonPressedGradientBegin;
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBackStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientEnd;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return ColorTable.MenuItemSelectedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
				return Color.Empty;

			switch (style)
			{
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        default:
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.CheckedNormal:
                            return ColorTable.CheckBackground;
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return ColorTable.ButtonPressedHighlight;
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                    return ColorTable.MenuStripGradientBegin;
                case PaletteBackStyle.HeaderForm:
                    return Table.Header1End;
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return ColorTable.MenuStripGradientEnd;
                case PaletteBackStyle.PanelAlternate:
                    return ColorTable.MenuStripGradientBegin;
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return SystemColors.Window;
                case PaletteBackStyle.ContextMenuHeading:
                    return ColorTable.OverflowButtonGradientBegin;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedBorder;
                        default:
                            return ColorTable.MenuBorder;
                    }
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return ColorTable.ImageMarginGradientEnd;
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack2];
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                    return ColorTable.ToolStripDropDownBackground;
                case PaletteBackStyle.ContextMenuItemImage:
                    return ColorTable.ButtonSelectedHighlight;
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return Table.Header1End;
                case PaletteBackStyle.HeaderSecondary:
					if (state == PaletteState.Disabled)
						return SystemColors.Control;
					else
						return ColorTable.MenuStripGradientBegin;
                case PaletteBackStyle.HeaderDockInactive:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return ControlPaint.Light(SystemColors.GradientInactiveCaption);
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return ControlPaint.Light(SystemColors.GradientActiveCaption);
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return SystemColors.Control;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return MergeColors(SystemColors.Window, 0.9f, SystemColors.ControlText, 0.1f);
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return MergeColors(SystemColors.Window, 0.95f, SystemColors.ControlText, 0.05f);
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return SystemColors.Window;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return MergeColors(SystemColors.Control, 0.8f, SystemColors.ControlDark, 0.2f);
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return MergeColors(SystemColors.Window, 0.8f, SystemColors.Highlight, 0.2f);
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return SystemColors.Window;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return MergeColors(SystemColors.Control, 0.8f, SystemColors.ControlDark, 0.2f);
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return MergeColors(SystemColors.Window, 0.8f, SystemColors.Highlight, 0.2f);
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack2;
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                    switch (state)
					{
						case PaletteState.Disabled:
                            return SystemColors.Control;
						case PaletteState.Normal:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientMiddle;
                        case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientBegin;
						case PaletteState.Tracking:
							return ColorTable.ButtonSelectedGradientEnd;
						case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            if (style == PaletteBackStyle.ButtonAlternate)
                                return ColorTable.MenuStripGradientBegin;
                            else
                                return ColorTable.ButtonPressedGradientEnd;
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientEnd;
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBackStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientEnd;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.CheckedNormal:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientEnd;
                        case PaletteState.Pressed:
                            return ColorTable.ButtonPressedGradientEnd;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return Color.Empty;
                        case PaletteState.Tracking:
                            return ColorTable.MenuItemSelectedGradientEnd;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color background drawing style.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteColorStyle.Inherit;

			switch (style)
			{
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.ButtonNavigatorMini:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return PaletteColorStyle.Switch90;
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    return PaletteColorStyle.Rounded;
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.TabHighProfile:
                    return PaletteColorStyle.QuarterPhase;
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    return PaletteColorStyle.OneNote;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color alignment.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRectangleAlign.Inherit;

			switch (style)
			{
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return PaletteRectangleAlign.Control;
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return PaletteRectangleAlign.Local;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return -1f;

			switch (style)
			{
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return 90f;
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return 0f;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public override Image GetBackImage(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return null;

			switch (style)
			{
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return null;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteImageStyle.Inherit;

			switch (style)
			{
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteImageStyle.Tile;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the image alignment.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRectangleAlign.Inherit;

			switch (style)
			{
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelRibbonInactive:
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.ControlToolTip:
                case PaletteBackStyle.ControlRibbon:
                case PaletteBackStyle.ControlRibbonAppMenu:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderDockActive:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabStandardProfile:
                case PaletteBackStyle.TabLowProfile:
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}
        #endregion

		#region Border
		/// <summary>
		/// Gets a value indicating if border should be drawn.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
		{
            // Check for the calendar day today override
            if (state == PaletteState.TodayOverride)
                if (style == PaletteBorderStyle.ButtonCalendarDay)
                    return InheritBool.True;
            
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return InheritBool.False;
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return InheritBool.True;
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                    return InheritBool.False;
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
                default:	
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteDrawBorders.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteDrawBorders.All;
                case PaletteBorderStyle.ContextMenuHeading:
                    return PaletteDrawBorders.Bottom;
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return PaletteDrawBorders.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }
        
        /// <summary>
		/// Gets the graphics drawing hint for the border.
		/// </summary>
        /// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteGraphicsHint.Inherit;

            switch (style)
            {
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    return PaletteGraphicsHint.AntiAlias;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteGraphicsHint.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
		}

		/// <summary>
		/// Gets the first border color.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
		{
            if (CommonHelper.IsOverrideState(state))
            {
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        else
                            return ColorTable.ButtonPressedBorder;
                    }

                return Color.Empty;
            }

			switch (style)
			{
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderForm:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return _toolTipBorder;
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return Table.Header1Begin;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return ColorTable.MenuBorder;
                case PaletteBorderStyle.ContextMenuHeading:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedBorder;
                        default:
                            return ColorTable.MenuBorder;
                    }
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return ColorTable.ToolStripDropDownBackground;
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return ColorTable.MenuBorder;
                case PaletteBorderStyle.InputControlRibbon:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else if (state == PaletteState.Normal)
                        return ColorTable.MenuStripGradientBegin;
                    else
                        return ColorTable.ButtonSelectedBorder;
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.GridDataCellSheet:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return ColorTable.SeparatorDark;
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return FadedColor(_ribbonColors[(int)SchemeOfficeColors.AppButtonBorder]);
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.ContextMenuItemImage:
                    return ColorTable.MenuItemBorder;
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return ColorTable.OverflowButtonGradientEnd;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.MenuBorder;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                            return ColorTable.OverflowButtonGradientEnd;
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f);
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.MenuBorder;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return ColorTable.OverflowButtonGradientEnd;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f);
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
					{
						case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
						case PaletteState.Normal:
                            return ColorTable.MenuBorder;
                        case PaletteState.CheckedNormal:
                            return ColorTable.MenuBorder;
						case PaletteState.Tracking:
							return ColorTable.ButtonSelectedBorder;
						case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            if (style == PaletteBorderStyle.ButtonAlternate)
                                return ColorTable.SeparatorDark;
                            else
                                return ColorTable.ButtonPressedBorder;
                    case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedBorder;
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientEnd;
                        case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return ColorTable.ButtonPressedGradientBegin;
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the second border color.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
		{            
            if (CommonHelper.IsOverrideState(state))
            {
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        else
                            return ColorTable.ButtonPressedBorder;
                    }

                return Color.Empty;
            }

			switch (style)
			{
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderForm:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return _toolTipBorder;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return ColorTable.MenuBorder;
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return SystemColors.Control;
                    else
                        return Table.Header1Begin;
                case PaletteBorderStyle.ContextMenuHeading:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedBorder;
                        default:
                            return ColorTable.MenuBorder;
                    }
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return ColorTable.ToolStripDropDownBackground;
                case PaletteBorderStyle.ContextMenuItemImage:
                    return ColorTable.MenuItemBorder;
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return ColorTable.MenuBorder;
                case PaletteBorderStyle.InputControlRibbon:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else if (state == PaletteState.Normal)
                        return ColorTable.MenuStripGradientBegin;
                    else
                        return ColorTable.ButtonSelectedBorder;
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return FadedColor(ColorTable.ButtonSelectedBorder);
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return FadedColor(_ribbonColors[(int)SchemeOfficeColors.AppButtonBorder]);
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                    return ColorTable.MenuBorder;
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return ColorTable.ButtonPressedHighlightBorder;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.MenuBorder;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                            return ColorTable.OverflowButtonGradientEnd;
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f);
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return ColorTable.MenuBorder;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return ColorTable.OverflowButtonGradientEnd;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return MergeColors(ColorTable.OverflowButtonGradientEnd, 0.5f, SystemColors.Highlight, 0.5f);
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
					{
						case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
						case PaletteState.Normal:
                            return ColorTable.MenuBorder;
                        case PaletteState.CheckedNormal:
                            return ColorTable.MenuBorder;
                        case PaletteState.Tracking:
							return ColorTable.ButtonSelectedBorder;
						case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            if (style == PaletteBorderStyle.ButtonAlternate)
                                return ColorTable.SeparatorDark;
                            else
                                return ColorTable.ButtonPressedBorder;
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedBorder;
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.CheckedNormal:
                            return ColorTable.ButtonPressedGradientEnd;
                        case PaletteState.NormalDefaultOverride:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.Tracking:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return ColorTable.ButtonPressedGradientBegin;
                        case PaletteState.CheckedTracking:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color border drawing style.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteColorStyle.Inherit;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color border alignment.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRectangleAlign.Inherit;

			switch (style)
			{
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return PaletteRectangleAlign.Control;
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the color border angle.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return -1f;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return 90f;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the border width.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer width.</returns>
		public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return -1;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return 0;
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return 1;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the border corner rounding.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer rounding.</returns>
		public override int GetBorderRounding(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return -1;

			switch (style)
			{
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return 0;
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlGroupBox:
                    return 3;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets a border image.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public override Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return null;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return null;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the border image style.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteImageStyle.Inherit;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteImageStyle.Tile;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the image border alignment.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRectangleAlign.Inherit;

			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}
        #endregion

		#region Content
		/// <summary>
		/// Gets a value indicating if content should be drawn.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
			if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;

			// Always draw everything
			return InheritBool.True;
		}

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
		{
			// By default the focus override shows the focus!
			if (state == PaletteState.FocusOverride)
				return InheritBool.True;

			// We do not override the other override states
            if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;

			// By default, never show the focus indication, we let individual controls
			// override this functionality as required by the controls requirements
			return InheritBool.False;
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ButtonNavigatorMini:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Far;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteRelativeAlign.Center;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteImageEffect.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderForm:
                    return PaletteImageEffect.Normal;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
						return PaletteImageEffect.Disabled;
					else
						return PaletteImageEffect.Normal;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTransparent(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return Color.Empty;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
		{
            if (CommonHelper.IsOverrideState(state))
            {
                if ((state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay))
                    return _calendarBoldFont;
                else
                    return null;
            }
            
			switch (style)
			{
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderCalendar:
                    return _gridFont;
                case PaletteContentStyle.HeaderForm:
                    return _headerFormFont;
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonCommand:
                    return _header1ShortFont;
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                    return _superToolFont;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _header2ShortFont;
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelBoldPanel:
                    return _boldFont;
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelItalicControl:
                    return _italicFont;
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _superToolFont;
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _tabFontNormal;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _tabFontSelected;
                        default:
                            return _tabFontNormal;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
                case PaletteContentStyle.ButtonNavigatorMini:
                    return _buttonFontNavigatorMini;
                case PaletteContentStyle.ButtonCalendarDay:
                    return _calendarFont;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentShortTextFont(style, state);
        }

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteTextHint.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHint.ClearTypeGridFit;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHotkeyPrefix.Inherit;

            switch (style)
            {
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderForm:
                    return PaletteTextHotkeyPrefix.Show;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteTextHotkeyPrefix.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

		/// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return InheritBool.True;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteTextTrim.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextTrim.EllipsisCharacter;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.HeaderCalendar:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Far;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelSuperTip:
                    return PaletteRelativeAlign.Near;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the horizontal relative alignment of multiline short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Near;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Always work out value for an override state
            if (CommonHelper.IsOverrideState(state))
            {
                switch (state)
                {
                    case PaletteState.LinkNotVisitedOverride:
                        return Color.Blue;
                    case PaletteState.LinkVisitedOverride:
                        return Color.Purple;
                    case PaletteState.LinkPressedOverride:
                        return Color.Red;
                    default:
                        // All other override states do nothing
                        return Color.Empty;
                }
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return ColorTable.SeparatorLight;
            }

            if (state == PaletteState.Disabled)
                return SystemColors.ControlDark;

            switch (style)
            {
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return ColorTable.SeparatorLight;
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.InactiveCaptionText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return SystemColors.ControlText;
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return SystemColors.HighlightText;
                    else
                        return SystemColors.ControlText;
                case PaletteContentStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return SystemColors.ControlText;
                        default:
                            return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return ColorTable.MenuItemText;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return ColorTable.SeparatorLight;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.ButtonInputControl))
                return SystemColors.ControlDark;

            switch (style)
            {
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return ColorTable.SeparatorLight;
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.InactiveCaptionText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return SystemColors.ControlText;
                case PaletteContentStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return SystemColors.ControlText;
                        default:
                            return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return SystemColors.HighlightText;
                    else
                        return SystemColors.ControlText;
                case PaletteContentStyle.ButtonInputControl:
                    return Color.Transparent;
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return ColorTable.MenuItemText;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteImageStyle.TileFlipXY;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

		/// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
		{
            if (CommonHelper.IsOverrideState(state))
            {
                if ((state == PaletteState.BoldedOverride) && (style == PaletteContentStyle.ButtonCalendarDay))
                    return _calendarBoldFont;
                else
                    return null;
            }

			switch (style)
			{
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                case PaletteContentStyle.HeaderCalendar:
                    return _gridFont;
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _header1LongFont;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.HeaderSecondary:
					return _header2LongFont;
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _tabFontNormal;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _tabFontSelected;
                        default:
                            return _tabFontNormal;
                    }
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
                case PaletteContentStyle.ButtonCalendarDay:
                    return _calendarFont;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state)
        {
            DefineFonts();
            return GetContentLongTextFont(style, state);
        }

		/// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteTextHint.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHint.ClearTypeGridFit;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return InheritBool.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return InheritBool.True;
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return InheritBool.False;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteTextTrim.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextTrim.EllipsisCharacter;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteTextHotkeyPrefix.Inherit;

            switch (style)
            {
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return PaletteTextHotkeyPrefix.Show;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteTextHotkeyPrefix.None;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }
        
        /// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Far;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnSheet:
                    return PaletteRelativeAlign.Center;
				default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return PaletteRelativeAlign.Far;
                case PaletteContentStyle.LabelSuperTip:
                    return PaletteRelativeAlign.Center;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the horizontal relative alignment of multiline long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return PaletteRelativeAlign.Inherit;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ButtonCommand:
                    return PaletteRelativeAlign.Near;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return PaletteRelativeAlign.Near;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return ColorTable.SeparatorLight;
            }

            if (state == PaletteState.Disabled)
                return SystemColors.ControlDark;

            switch (style)
            {
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return ColorTable.SeparatorLight;
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.InactiveCaptionText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return SystemColors.ControlText;
                case PaletteContentStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return SystemColors.ControlText;
                        default:
                            return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return ColorTable.MenuItemText;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    return ColorTable.SeparatorLight;
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.ButtonInputControl))
                return SystemColors.ControlDark;

            switch (style)
            {
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return ColorTable.SeparatorLight;
                case PaletteContentStyle.HeaderDockInactive:
                    return SystemColors.InactiveCaptionText;
                case PaletteContentStyle.HeaderDockActive:
                    return SystemColors.ActiveCaptionText;
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return SystemColors.ControlText;
                case PaletteContentStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return SystemColors.ControlText;
                        default:
                            return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                    }
                case PaletteContentStyle.TabDockAutoHidden:
                    return MergeColors(SystemColors.Window, 0.3f, SystemColors.ControlText, 0.7f);
                case PaletteContentStyle.ButtonInputControl:
                    return Color.Transparent;
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return ColorTable.MenuItemText;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteColorStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return -1f;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteImageStyle.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteImageStyle.TileFlipXY;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return PaletteRectangleAlign.Inherit;

            switch (style)
            {
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return PaletteRectangleAlign.Local;
                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }
        
        /// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public override Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return CommonHelper.InheritPadding;

			switch (style)
			{
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return _contentPaddingGrid;
                case PaletteContentStyle.HeaderForm:
                    return _contentPaddingHeaderForm;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _contentPaddingHeader1;
                case PaletteContentStyle.HeaderSecondary:
					return _contentPaddingHeader2;
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                    return _contentPaddingHeader3;
                case PaletteContentStyle.HeaderCalendar:
                    return _contentPaddingCalendar;
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                    return _contentPaddingLabel;
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _contentPaddingLabel2;
                case PaletteContentStyle.ContextMenuItemTextStandard:
                    return _contentPaddingContextMenuItemText;
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _contentPaddingContextMenuItemTextAlt;
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _contentPaddingContextMenuItemShortcutText;
                case PaletteContentStyle.LabelToolTip:
                    return _contentPaddingToolTip;
                case PaletteContentStyle.LabelSuperTip:
                    return _contentPaddingSuperTip;
                case PaletteContentStyle.LabelKeyTip:
                    return _contentPaddingKeyTip;
                case PaletteContentStyle.ContextMenuHeading:
                    return _contentPaddingContextMenuHeading;
                case PaletteContentStyle.ContextMenuItemImage:
                    return _contentPaddingContextMenuImage;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    return InputControlPadding;
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    return _contentPaddingButton12;
                case PaletteContentStyle.ButtonInputControl:
                    return _contentPaddingButtonInputControl;
                case PaletteContentStyle.ButtonCalendarDay:
                    return _contentPaddingButtonCalendar;
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonListItem:
                    return _contentPaddingButton3;
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return _contentPaddingButton4;
                case PaletteContentStyle.ButtonBreadCrumb:
                    return _contentPaddingButton6;
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    return _contentPaddingButtonForm;
                case PaletteContentStyle.ButtonGallery:
                    return _contentPaddingButtonGallery;
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                    return _contentPaddingButton5;
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                    return _contentPaddingButton7;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
		{
			// We do not provide override values
            if (CommonHelper.IsOverrideState(state))
				return -1;

			switch (style)
			{
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                case PaletteContentStyle.HeaderCalendar:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderForm:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuHeading:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabDockAutoHidden:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                case PaletteContentStyle.GridHeaderColumnList:
                case PaletteContentStyle.GridHeaderColumnSheet:
                case PaletteContentStyle.GridHeaderColumnCustom1:
                case PaletteContentStyle.GridHeaderRowList:
                case PaletteContentStyle.GridHeaderRowSheet:
                case PaletteContentStyle.GridHeaderRowCustom1:
                case PaletteContentStyle.GridDataCellList:
                case PaletteContentStyle.GridDataCellSheet:
                case PaletteContentStyle.GridDataCellCustom1:
                    return 1;
                case PaletteContentStyle.LabelSuperTip:
                    return 5;
                default:
					throw new ArgumentOutOfRangeException("style");
			}
		}
		#endregion

		#region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            switch (metric)
            {
                case PaletteMetricInt.PageButtonInset:
                case PaletteMetricInt.RibbonTabGap:
                case PaletteMetricInt.HeaderButtonEdgeInsetCalendar:
                    return 2;
                case PaletteMetricInt.CheckButtonGap:
                    return 5;
                case PaletteMetricInt.HeaderButtonEdgeInsetForm:
                    return 4;
                case PaletteMetricInt.HeaderButtonEdgeInsetInputControl:
                    return 1;
                case PaletteMetricInt.HeaderButtonEdgeInsetPrimary:
                case PaletteMetricInt.HeaderButtonEdgeInsetSecondary:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockInactive:
                case PaletteMetricInt.HeaderButtonEdgeInsetDockActive:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom1:
                case PaletteMetricInt.HeaderButtonEdgeInsetCustom2:
                case PaletteMetricInt.BarButtonEdgeOutside:
                case PaletteMetricInt.BarButtonEdgeInside:
                    return 3;
                case PaletteMetricInt.None:
                    return 0;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return -1;
        }
        
        /// <summary>
		/// Gets a boolean metric value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <param name="metric">Requested metric.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
		{
            switch (metric)
            {
                case PaletteMetricBool.HeaderGroupOverlay:
                case PaletteMetricBool.SplitWithFading:
                case PaletteMetricBool.TreeViewLines:
                    return InheritBool.True;
                case PaletteMetricBool.RibbonTabsSpareCaption:
                    return InheritBool.False;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return InheritBool.Inherit;
		}

		/// <summary>
		/// Gets a padding metric value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <param name="metric">Requested metric.</param>
		/// <returns>Padding value.</returns>
		public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
		{
            switch (metric)
            {
                case PaletteMetricPadding.PageButtonPadding:
                    return _metricPaddingPageButtons;
                case PaletteMetricPadding.BarPaddingTabs:
                    return _metricPaddingBarTabs;
                case PaletteMetricPadding.BarPaddingInside:
                case PaletteMetricPadding.BarPaddingOnly:
                    return _metricPaddingBarInside;
                case PaletteMetricPadding.BarPaddingOutside:
                    return _metricPaddingBarOutside;
                case PaletteMetricPadding.HeaderButtonPaddingForm:
                    return _metricPaddingHeaderForm;
                case PaletteMetricPadding.RibbonButtonPadding:
                    return _metricPaddingRibbon;
                case PaletteMetricPadding.RibbonAppButton:
                    return _metricPaddingRibbonAppButton;
                case PaletteMetricPadding.HeaderButtonPaddingInputControl:
                    return _metricPaddingInputControl;
                case PaletteMetricPadding.HeaderButtonPaddingPrimary:
                case PaletteMetricPadding.HeaderButtonPaddingSecondary:
                case PaletteMetricPadding.HeaderButtonPaddingDockInactive:
                case PaletteMetricPadding.HeaderButtonPaddingDockActive:
                case PaletteMetricPadding.HeaderButtonPaddingCustom1:
                case PaletteMetricPadding.HeaderButtonPaddingCustom2:
                case PaletteMetricPadding.HeaderButtonPaddingCalendar:
                case PaletteMetricPadding.BarButtonPadding:
                    return _metricPaddingHeader;
                case PaletteMetricPadding.ContextMenuItemHighlight:
                    return _metricPaddingContextMenuItemHighlight;
                case PaletteMetricPadding.ContextMenuItemsCollection:
                    return _metricPaddingContextMenuItemsCollection;
                case PaletteMetricPadding.ContextMenuItemOuter:
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                case PaletteMetricPadding.SeparatorPaddingLowProfile:
                case PaletteMetricPadding.SeparatorPaddingHighProfile:
                case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
                case PaletteMetricPadding.SeparatorPaddingCustom1:
                    return Padding.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Padding.Empty;
		}
		#endregion

        #region Images
        /// <summary>
        /// Gets a tree view image appropriate for the provided state.
        /// </summary>
        /// <param name="expanded">Is the node expanded</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetTreeViewImage(bool expanded)
        {
            if (expanded)
                return _treeCollapseMinus;
            else
                return _treeExpandPlus;
        }
        
        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed)
        {
            return null;
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the radio button enabled.</param>
        /// <param name="checkState">Is the radio button checked.</param>
        /// <param name="tracking">Is the radio button being hot tracked.</param>
        /// <param name="pressed">Is the radio button being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed)
        {
            return null;
        }

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            if (state != PaletteState.Disabled)
            {
                if (_normalDropDownImage == null)
                {
                    _normalDropDownImage = CreateDropDownImage(SystemColors.ControlText);
                    _normalDropDownColor = SystemColors.ControlText;
                }

                return _normalDropDownImage;
            }
            else
            {
                if (_disabledDropDownImage == null)
                {
                    _disabledDropDownImage = CreateDropDownImage(SystemColors.ControlDark);
                    _disabledDropDownColor = SystemColors.ControlDark;
                }

                return _disabledDropDownImage;
            }
        }

        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuCheckedImage()
        {
            return _contextMenuChecked;
        }

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuIndeterminateImage()
        {
            return _contextMenuIndeterminate;
        }

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuSubMenuImage()
        {
            return _contextMenuSubMenu;
        }

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state)
        {
            switch (button)
            {
                default:
                case PaletteRibbonGalleryButton.Up:
                        if (_galleryImageUp == null)
                            _galleryImageUp = CreateGalleryUpImage(SystemColors.ControlText);
                        return _galleryImageUp;
                case PaletteRibbonGalleryButton.Down:
                    if (_galleryImageDown == null)
                        _galleryImageDown = CreateGalleryDownImage(SystemColors.ControlText);
                    return _galleryImageDown;
                case PaletteRibbonGalleryButton.DropDown:
                    if (_galleryImageDropDown == null)
                        _galleryImageDropDown = CreateGalleryDropDownImage(SystemColors.ControlText);
                    return _galleryImageDropDown;
            }
        }
        #endregion

        #region ButtonSpec
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        public override Icon GetButtonSpecIcon(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public override Image GetButtonSpecImage(PaletteButtonSpecStyle style,
                                                 PaletteState state)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Close:
                    return _buttonSpecClose;
                case PaletteButtonSpecStyle.Context:
                    return _buttonSpecContext;
                case PaletteButtonSpecStyle.Next:
                    return _buttonSpecNext;
                case PaletteButtonSpecStyle.Previous:
                    return _buttonSpecPrevious;
                case PaletteButtonSpecStyle.ArrowLeft:
                    return _buttonSpecArrowLeft;
                case PaletteButtonSpecStyle.ArrowRight:
                    return _buttonSpecArrowRight;
                case PaletteButtonSpecStyle.ArrowUp:
                    return _buttonSpecArrowUp;
                case PaletteButtonSpecStyle.ArrowDown:
                    return _buttonSpecArrowDown;
                case PaletteButtonSpecStyle.DropDown:
                    return _buttonSpecDropDown;
                case PaletteButtonSpecStyle.PinVertical:
                    return _buttonSpecPinVertical;
                case PaletteButtonSpecStyle.PinHorizontal:
                    return _buttonSpecPinHorizontal;
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                    return _buttonSpecWorkspaceMaximize;
                case PaletteButtonSpecStyle.WorkspaceRestore:
                    return _buttonSpecWorkspaceRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    if (state == PaletteState.Disabled)
                        return _pendantMinimizeI;
                    else
                        return _pendantMinimizeA;
                case PaletteButtonSpecStyle.RibbonExpand:
                    if (state == PaletteState.Disabled)
                        return _pendantExpandI;
                    else
                        return _pendantExpandA;
                case PaletteButtonSpecStyle.FormClose:
                    if (state == PaletteState.Disabled)
                        return _systemCloseI;
                    else
                        return _systemCloseA;
                case PaletteButtonSpecStyle.FormMin:
                    if (state == PaletteState.Disabled)
                        return _systemMinI;
                    else
                        return _systemMinA;
                case PaletteButtonSpecStyle.FormMax:
                    if (state == PaletteState.Disabled)
                        return _systemMaxI;
                    else
                        return _systemMaxA;
                case PaletteButtonSpecStyle.FormRestore:
                    if (state == PaletteState.Disabled)
                        return _systemRestoreI;
                    else
                        return _systemRestoreA;
                case PaletteButtonSpecStyle.PendantClose:
                    if (state == PaletteState.Disabled)
                        return _pendantCloseI;
                    else
                        return _pendantCloseA;
                case PaletteButtonSpecStyle.PendantMin:
                    if (state == PaletteState.Disabled)
                        return _pendantMinI;
                    else
                        return _pendantMinA;
                case PaletteButtonSpecStyle.PendantRestore:
                    if (state == PaletteState.Disabled)
                        return _pendantRestoreI;
                    else
                        return _pendantRestoreA;
                case PaletteButtonSpecStyle.Generic:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.Magenta;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecShortText(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return string.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecLongText(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return string.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.Black;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the color to remap to transparent.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecColorTransparent(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                    return Color.Empty;
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return Color.Magenta;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Color.Empty;
            }
        }

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        public override PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                    return PaletteButtonStyle.Form;
                case PaletteButtonSpecStyle.FormClose:
                    return PaletteButtonStyle.FormClose;
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteButtonStyle.ButtonSpec;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonStyle.ButtonSpec;
            }
        }

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        public override HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return HeaderLocation.PrimaryHeader;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return HeaderLocation.PrimaryHeader;
            }
        }

        /// <summary>
        /// Gets the edge to positon the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteRelativeEdgeAlign.Far;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteRelativeEdgeAlign.Far;
            }
        }

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        public override PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style)
        {
            switch (style)
            {
                case PaletteButtonSpecStyle.Close:
                case PaletteButtonSpecStyle.Context:
                case PaletteButtonSpecStyle.ArrowLeft:
                case PaletteButtonSpecStyle.ArrowRight:
                case PaletteButtonSpecStyle.ArrowUp:
                case PaletteButtonSpecStyle.ArrowDown:
                case PaletteButtonSpecStyle.DropDown:
                case PaletteButtonSpecStyle.PinVertical:
                case PaletteButtonSpecStyle.PinHorizontal:
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                case PaletteButtonSpecStyle.WorkspaceRestore:
                case PaletteButtonSpecStyle.RibbonMinimize:
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteButtonOrientation.FixedTop;
                case PaletteButtonSpecStyle.Generic:
                case PaletteButtonSpecStyle.Next:
                case PaletteButtonSpecStyle.Previous:
                    return PaletteButtonOrientation.Auto;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonOrientation.Auto;
            }
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        public override PaletteRibbonShape GetRibbonShape()
        {
            return PaletteRibbonShape.Office2010;
        }

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }
        
        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonContextTextFont(PaletteState state)
        {
            return _buttonFont;
        }

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Color GetRibbonContextTextColor(PaletteState state)
        {
            return _contextTextColor;
        }

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledDark(PaletteState state)
        {
            return _disabledGlyphDark;
        }

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledLight(PaletteState state)
        {
            return _disabledGlyphLight;
        }

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogLight];
        }

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogDark];
        }

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogDark];
        }

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupDialogLight];
        }

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupSeparatorDark];
        }

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupSeparatorLight];
        }

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonMinimizeBarDark];
        }

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonMinimizeBarLight];
        }

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorColor(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSeparatorColor];
        }

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorContextColor(PaletteState state)
        {
            return _contextTabSeparator;
        }

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonTextFont(PaletteState state)
        {
            return _buttonFont;
        }

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetRibbonTextHint(PaletteState state)
        {
            return PaletteTextHint.SystemDefault;
        }

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonQATButtonDark];
        }

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonLight(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonQATButtonLight];
        }
        #endregion

        #region RibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return PaletteRibbonColorStyle.Solid;
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return PaletteRibbonColorStyle.RibbonAppMenuInner;
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return PaletteRibbonColorStyle.RibbonAppMenuOuter;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.CheckedNormal)
                        return PaletteRibbonColorStyle.RibbonQATMinibarDouble;
                    else
                        return PaletteRibbonColorStyle.RibbonQATMinibarSingle;
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return PaletteRibbonColorStyle.RibbonQATFullbarSquare;
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return PaletteRibbonColorStyle.RibbonQATOverflow;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return PaletteRibbonColorStyle.LinearBorder;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    if (state == PaletteState.Pressed)
                        return PaletteRibbonColorStyle.Empty;
                    else
                        return PaletteRibbonColorStyle.Linear;
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.ContextNormal:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderSep;
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepTrackingLight;
                        case PaletteState.Pressed:
                        case PaletteState.ContextPressed:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepPressedLight;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    return PaletteRibbonColorStyle.Empty;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return PaletteRibbonColorStyle.RibbonGroupAreaBorder3;
                        case PaletteState.ContextCheckedNormal:
                            return PaletteRibbonColorStyle.RibbonGroupAreaBorder4;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                            return PaletteRibbonColorStyle.Empty;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonTabTracking2010;
                        case PaletteState.FocusOverride:
                            return PaletteRibbonColorStyle.RibbonTabFocus2010;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                            return PaletteRibbonColorStyle.RibbonTabSelected2010;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return PaletteRibbonColorStyle.Empty;
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return SystemColors.Control;
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBackTracking];
                        case PaletteState.Normal:
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBackNormal];
                    }
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return FadedColor(ColorTable.ButtonSelectedBorder);
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBorder];
                    }
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonMenuDocsBack];
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonInner1];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter1];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini1I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar1];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATOverflow1];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameBorder1];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside1];
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[0];
                        case PaletteState.Tracking:
                            return _appButtonTrack[0];
                        case PaletteState.Pressed:
                            return _appButtonPressed[0];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    if (state == PaletteState.ContextCheckedNormal)
                        return _contextGroupAreaBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking1];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected1];
                        case PaletteState.FocusOverride:
                            return _contextCheckedTabBorder1;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonInner2];
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter2];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini2];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini2I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar2];
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATOverflow2];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameBorder2];
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupFrameInside2];
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    switch (state)
                    {
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextNormal:
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[1];
                        case PaletteState.Tracking:
                            return _appButtonTrack[1];
                        case PaletteState.Pressed:
                            return _appButtonPressed[1];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea2];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.ContextCheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected2];
                        case PaletteState.FocusOverride:
                            return _contextCheckedTabBorder2;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Color.Empty;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonOuter3];
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini3];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini3I];
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonQATFullbar3];
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder3];
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[2];
                        case PaletteState.Tracking:
                            return _appButtonTrack[2];
                        case PaletteState.Pressed:
                            return _appButtonPressed[2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea3];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking3];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected3];
                        case PaletteState.FocusOverride:
                            return _contextCheckedTabBorder3;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini4];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini4I];
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder4];
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[3];
                        case PaletteState.Tracking:
                            return _appButtonTrack[3];
                        case PaletteState.Pressed:
                            return _appButtonPressed[3];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea4];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking4];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                        case PaletteState.FocusOverride:
                            return _contextCheckedTabBorder4;
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder5];
                case PaletteRibbonBackStyle.RibbonAppMenuDocs:
                case PaletteRibbonBackStyle.RibbonAppMenuInner:
                case PaletteRibbonBackStyle.RibbonAppMenuOuter:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonQATFullbar:
                case PaletteRibbonBackStyle.RibbonQATOverflow:
                case PaletteRibbonBackStyle.RibbonGalleryBack:
                case PaletteRibbonBackStyle.RibbonGalleryBorder:
                    return Color.Empty;
                case PaletteRibbonBackStyle.RibbonQATMinibar:
                    if (state == PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini5];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonQATMini5I];
                case PaletteRibbonBackStyle.RibbonAppButton:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _appButtonNormal[4];
                        case PaletteState.Tracking:
                            return _appButtonTrack[4];
                        case PaletteState.Pressed:
                            return _appButtonPressed[4];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupArea:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea5];
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTracking2];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                        case PaletteState.Normal:
                            return Color.Empty;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region RibbonText
        /// <summary>
        /// Gets the =color for the item text.
        /// </summary>
        /// <param name="style">Text style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
                case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
                    return SystemColors.ControlText;
                case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleText];
                    }
                case PaletteRibbonTextStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledText;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                        case PaletteState.FocusOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTextChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonTabTextNormal];
                    }
                case PaletteRibbonTextStyle.RibbonGroupCollapsedText:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedText];
                case PaletteRibbonTextStyle.RibbonGroupButtonText:
                case PaletteRibbonTextStyle.RibbonGroupLabelText:
                case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
                case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                    if (state == PaletteState.Disabled)
                        return _disabledText;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupCollapsedText];
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region ElementColor
        /// <summary>
        /// Gets the first element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor1(PaletteElement element, PaletteState state)
        {
            // We do not provide override values
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return ColorTable.SeparatorDark;
                case PaletteElement.TrackBarTrack:
                    return ColorTable.OverflowButtonGradientEnd;
                case PaletteElement.TrackBarPosition:
                    return Color.FromArgb(128, Color.White);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the second element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor2(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return ColorTable.SeparatorDark;
                case PaletteElement.TrackBarTrack:
                    return ColorTable.MenuStripGradientBegin;
                case PaletteElement.TrackBarPosition:
                    switch (state)
					{
						case PaletteState.Disabled:
                            return ControlPaint.LightLight(ColorTable.MenuBorder);
						case PaletteState.Normal:
                            return ColorTable.MenuBorder;
                        case PaletteState.Tracking:
							return ColorTable.ButtonSelectedBorder;
						case PaletteState.Pressed:
                            return ColorTable.ButtonPressedBorder;
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the third element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor3(PaletteElement element, PaletteState state)
        {
            if (CommonHelper.IsOverrideState(state))
                return Color.Empty;

            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    return ColorTable.SeparatorDark;
                case PaletteElement.TrackBarTrack:
                    return ColorTable.OverflowButtonGradientBegin;
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(ColorTable.MenuStripGradientBegin);
                        case PaletteState.Normal:
                        case PaletteState.FocusOverride:
                            return ControlPaint.Light(ColorTable.MenuStripGradientBegin);
                        case PaletteState.Tracking:
                            return ControlPaint.Light(ColorTable.ButtonSelectedGradientBegin);
                        case PaletteState.Pressed:
                            return ControlPaint.Light(ColorTable.ButtonPressedGradientBegin);
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }

                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fourth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor4(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return ColorTable.SeparatorDark;
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return SystemColors.Control;
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(ColorTable.MenuStripGradientEnd);
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientEnd;
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return ColorTable.ButtonSelectedGradientBegin;
                        case PaletteState.Pressed:
                            return ColorTable.ButtonPressedGradientBegin;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }

        /// <summary>
        /// Gets the fifth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor5(PaletteElement element, PaletteState state)
        {
            switch (element)
            {
                case PaletteElement.TrackBarTick:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return ColorTable.SeparatorDark;
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return SystemColors.Control;
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(ColorTable.MenuStripGradientBegin);
                        case PaletteState.Normal:
                            return ColorTable.MenuStripGradientBegin;
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return ColorTable.ButtonSelectedGradientEnd;
                        case PaletteState.Pressed:
                            return ColorTable.ButtonPressedGradientEnd;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return Color.Red;
        }
        #endregion

        #region ColorTable
        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        public override KryptonColorTable ColorTable
        {
            get { return Table; }
        }

        internal KryptonProfessionalKCT Table
        {
            get
            {
                // If the table has not yet been generated
                if (_table == null)
                {
                    // Ask the virtual method to generate the table
                    _table = GenerateColorTable();
                }

                return _table;
            }
        }

        /// <summary>
        /// Generate an appropriate color table.
        /// </summary>
        /// <returns>KryptonProfessionalKCT instance.</returns>
        internal virtual KryptonProfessionalKCT GenerateColorTable()
        {
            // Create the color table to use as the base for getting krypton colors
            KryptonColorTable kct = new KryptonColorTable(this);

            // Always turn off the use of any theme specific colors
            kct.UseSystemColors = true;

            // Calculate the krypton specific colors
            Color[] colors = new Color[] { kct.OverflowButtonGradientEnd,   // Header1Begin
                                           kct.OverflowButtonGradientEnd,   // Header1End
                                         };

            // Create a krypton extension color table
            return new KryptonProfessionalKCT(colors, true, this);
        }
        #endregion

        #region OnUserPreferenceChanged
        /// <summary>
        /// Handle a change in the user preferences.
        /// </summary>
        /// <param name="sender">Source of event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Remove the current table, so it gets regenerated when next requested
            _table = null;

            if (_disabledDropDownImage != null)
            {
                _disabledDropDownImage.Dispose();
                _disabledDropDownImage = null;
            }

            if (_normalDropDownImage != null)
            {
                _normalDropDownImage.Dispose();
                _normalDropDownImage = null;
            }

            if (_galleryImageUp != null)
            {
                _galleryImageUp.Dispose();
                _galleryImageUp = null;
            }

            if (_galleryImageDown != null)
            {
                _galleryImageDown.Dispose();
                _galleryImageDown = null;
            }

            if (_galleryImageDropDown != null)
            {
                _galleryImageDropDown.Dispose();
                _galleryImageDropDown = null;
            }

            // Update fonts to reflect any change in system settings
            DefineFonts();

            // Generate the myriad ribbon colors from system settings
            DefineRibbonColors();

            base.OnUserPreferenceChanged(sender, e);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Update the fonts to reflect system or user defined changes.
        /// </summary>
        protected override void DefineFonts()
        {
            // Release existing resources
            if (_header1ShortFont != null) _header1ShortFont.Dispose();
            if (_header2ShortFont != null) _header2ShortFont.Dispose();
            if (_header1LongFont != null) _header1LongFont.Dispose();
            if (_header2LongFont != null) _header2LongFont.Dispose();
            if (_headerFormFont != null) _headerFormFont.Dispose();
            if (_buttonFont != null) _buttonFont.Dispose();
            if (_buttonFontNavigatorMini != null) _buttonFontNavigatorMini.Dispose();
            if (_tabFontSelected != null) _tabFontSelected.Dispose();
            if (_tabFontNormal != null) _tabFontNormal.Dispose();
            if (_gridFont != null) _gridFont.Dispose();
            if (_calendarFont != null) _calendarFont.Dispose();
            if (_calendarBoldFont != null) _calendarBoldFont.Dispose();
            if (_superToolFont != null) _superToolFont.Dispose();
            if (_boldFont != null) _boldFont.Dispose();
            if (_italicFont != null) _italicFont.Dispose();

            float baseFontSize = BaseFontSize;
            _header1ShortFont = new Font("Arial", baseFontSize + 4.5f, FontStyle.Bold);
            _header2ShortFont = SystemFonts.IconTitleFont;
            _header1LongFont = new Font(SystemFonts.MenuFont.FontFamily, baseFontSize + 1.5f, FontStyle.Regular);
            _header2LongFont = SystemFonts.IconTitleFont;
            _headerFormFont = new Font("Arial", SystemFonts.CaptionFont.SizeInPoints, FontStyle.Bold);
            _buttonFont = SystemFonts.IconTitleFont;
            _buttonFontNavigatorMini = new Font("Arial", baseFontSize + 3.5f, FontStyle.Bold);
            _tabFontNormal = SystemFonts.IconTitleFont;
            _tabFontSelected = new Font(_tabFontNormal, FontStyle.Bold);
            _gridFont = SystemFonts.IconTitleFont;
            _superToolFont = new Font(SystemFonts.MenuFont.FontFamily, baseFontSize, FontStyle.Bold);
            _calendarFont = new Font(SystemFonts.IconTitleFont.FontFamily, baseFontSize, FontStyle.Regular);
            _calendarBoldFont = new Font(SystemFonts.IconTitleFont.FontFamily, baseFontSize, FontStyle.Bold);
            _boldFont = new Font(SystemFonts.IconTitleFont.FontFamily, baseFontSize, FontStyle.Bold);
            _italicFont = new Font(SystemFonts.IconTitleFont.FontFamily, baseFontSize, FontStyle.Italic);
        }
        #endregion

        #region Implementation
        private void DefineRibbonColors()
        {
            // Main values
            Color groupLight = ColorTable.MenuStripGradientEnd;
            Color groupStart = ColorTable.RaftingContainerGradientBegin;
            Color groupEnd = ColorTable.MenuBorder;

            // Spot standard background colors and then tweak values 
            // so it looks good under the standard windows settings.
            switch (SystemColors.Control.ToArgb())
            {
                case -986896:   // Vista Aero/Basic
                case -1250856:  // XP Themes - Blue & Olive
                case -2039837:  // XP Themes - Silver
                    groupLight = MergeColors(groupLight, 0.93f, Color.Black, 0.07f);
                    groupStart = MergeColors(groupStart, 0.93f, Color.Black, 0.07f);
                    groupEnd = MergeColors(groupEnd, 0.93f, Color.Black, 0.07f);
                    break;
                case -2830136:  // Windows Standard
                case -4144960:  // Windows Classic
                    groupLight = MergeColors(groupLight, 0.95f, Color.Black, 0.05f);
                    groupStart = MergeColors(groupStart, 0.95f, Color.Black, 0.05f);
                    groupEnd = MergeColors(groupEnd, 0.95f, Color.Black, 0.05f);
                    break;
            }

            // Create colors, mainly by merging between two main values
            Color ribbonGroupsArea1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
            Color ribbonGroupsArea2 = MergeColors(groupStart, 0.20f, groupEnd, 0.80f);
            Color ribbonGroupsArea3 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
            Color ribbonGroupsArea4 = MergeColors(groupStart, 0.70f, Color.White, 0.30f);
            Color ribbonGroupsArea5 = MergeColors(groupStart, 0.90f, groupEnd, 0.10f);
            Color ribbonGroupBorder1 = Color.FromArgb(128, Color.White);
            Color ribbonGroupBorder2 = Color.FromArgb(196, Color.White);
            Color ribbonGroupBorder3 = MergeColors(groupStart, 0.20f, groupEnd, 0.80f);
            Color ribbonGroupBorder4 = MergeColors(groupStart, 0.30f, Color.White, 0.70f);
            Color ribbonGroupBorder5 = Color.FromArgb(249, 250, 250);
            Color ribbonGroupFrameBorder1 = MergeColors(groupStart, 0.60f, groupEnd, 0.40f);
            Color ribbonGroupFrameInside1 = MergeColors(groupStart, 0.40f, Color.White, 0.60f);
            Color ribbonGroupTitleText = Color.FromArgb(152, SystemColors.ControlText);
            Color ribbonGroupDialogDark = Color.FromArgb(104, SystemColors.ControlText);
            Color ribbonGroupDialogLight = Color.FromArgb(72, SystemColors.ControlText);
            Color ribbonGroupSepDark = MergeColors(groupStart, 0.50f, groupEnd, 0.50f);
            Color ribbonMinimizeLight = MergeColors(ColorTable.MenuStripGradientEnd, 0.40f, Color.White, 0.60f);
            Color ribbonMinimizeDark = MergeColors(groupStart, 0.70f, groupEnd, 0.30f);
            Color ribbonTabSelected1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
            Color ribbonTabSelected2 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
            Color ribbonTabSelected3 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
            Color ribbonTabSelected4 = MergeColors(groupStart, 0.10f, Color.White, 0.90f);
            Color ribbonTabTracking1 = MergeColors(groupStart, 0.80f, groupEnd, 0.20f);
            Color ribbonTabTracking2 = MergeColors(groupStart, 0.20f, Color.White, 0.80f);
            Color ribbonTabTracking3 = MergeColors(groupStart, 0.50f, Color.White, 0.50f);
            Color ribbonTabTracking4 = MergeColors(groupStart, 0.75f, Color.White, 0.25f);
            Color ribbonQATOverflowInside = MergeColors(ColorTable.MenuStripGradientEnd, 0.75f, groupStart, 0.25f);
            Color ribbonQATOverflowInside2 = MergeColors(ColorTable.MenuStripGradientEnd, 0.65f, groupStart, 0.35f);
            Color ribbonQATMini1 = MergeColors(groupStart, 0.70f, groupEnd, 0.30f);
            Color ribbonQATMini3 = MergeColors(groupStart, 0.90f, groupEnd, 0.10f);

            // Generate first set of ribbon colors
            _ribbonColors = new Color[] { // Non ribbon colors
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,    
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,    
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,  
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,   
                                          // Ribbon colors
                                          SystemColors.ControlText,     // RibbonTabTextNormal
                                          SystemColors.ControlText,     // RibbonTabTextChecked
                                          ribbonTabSelected1,           // RibbonTabSelected1
                                          ribbonTabSelected2,           // RibbonTabSelected2
                                          ribbonTabSelected3,           // RibbonTabSelected3
                                          ribbonTabSelected4,           // RibbonTabSelected4
                                          Color.Empty,                  // RibbonTabSelected5
                                          ribbonTabTracking1,           // RibbonTabTracking1
                                          ribbonTabTracking2,           // RibbonTabTracking2
                                          Color.FromArgb(196, ColorTable.ButtonSelectedGradientMiddle), // RibbonTabHighlight1
                                          ColorTable.ButtonSelectedGradientMiddle,                      // RibbonTabHighlight2
                                          ColorTable.ButtonPressedGradientMiddle,                       // RibbonTabHighlight3
                                          ColorTable.ButtonPressedGradientMiddle,                       // RibbonTabHighlight4
                                          ColorTable.ButtonSelectedGradientMiddle,                      // RibbonTabHighlight5
                                          ColorTable.MenuBorder,        // RibbonTabSeparatorColor
                                          ribbonGroupsArea1,            // RibbonGroupsArea1
                                          ribbonGroupsArea2,            // RibbonGroupsArea2
                                          ribbonGroupsArea3,            // RibbonGroupsArea3
                                          ribbonGroupsArea4,            // RibbonGroupsArea4
                                          ribbonGroupsArea4,            // RibbonGroupsArea5
                                          ribbonGroupBorder1,           // RibbonGroupBorder1
                                          ribbonGroupBorder2,           // RibbonGroupBorder2
                                          Color.Red,                    // RibbonGroupTitle1
                                          Color.Red,                    // RibbonGroupTitle2
                                          ribbonGroupBorder1,           // RibbonGroupBorderContext1
                                          ribbonGroupBorder2,           // RibbonGroupBorderContext2
                                          Color.Red,                    // RibbonGroupTitleContext1
                                          Color.Red,                    // RibbonGroupTitleContext2
                                          ribbonGroupDialogDark,        // RibbonGroupDialogDark
                                          ribbonGroupDialogLight,       // RibbonGroupDialogLight
                                          Color.Red,                    // RibbonGroupTitleTracking1
                                          Color.Red,                    // RibbonGroupTitleTracking2
                                          ribbonMinimizeDark,           // RibbonMinimizeBarDark
                                          ribbonMinimizeLight,          // RibbonMinimizeBarLight
                                          ribbonGroupBorder1,           // RibbonGroupCollapsedBorder1
                                          ribbonGroupBorder2,           // RibbonGroupCollapsedBorder2
                                          ribbonGroupsArea4,            // RibbonGroupCollapsedBorder3
                                          ribbonGroupsArea2,            // RibbonGroupCollapsedBorder4
                                          ribbonGroupsArea4,            // RibbonGroupCollapsedBack1
                                          Color.Red,                    // RibbonGroupCollapsedBack2
                                          Color.Red,                    // RibbonGroupCollapsedBack3
                                          ribbonGroupsArea2,            // RibbonGroupCollapsedBack4
                                          Color.Red,                    // RibbonGroupCollapsedBorderT1
                                          Color.Red,                    // RibbonGroupCollapsedBorderT2
                                          Color.Red,                    // RibbonGroupCollapsedBorderT3
                                          Color.Red,                    // RibbonGroupCollapsedBorderT4
                                          Color.Red,                    // RibbonGroupCollapsedBackT1
                                          Color.Red,                    // RibbonGroupCollapsedBackT2
                                          Color.Red,                    // RibbonGroupCollapsedBackT3
                                          Color.Red,                    // RibbonGroupCollapsedBackT4
                                          ribbonGroupFrameBorder1,      // RibbonGroupFrameBorder1
                                          ribbonGroupFrameBorder1,      // RibbonGroupFrameBorder2
                                          ribbonGroupFrameInside1,      // RibbonGroupFrameInside1
                                          ribbonGroupFrameInside1,      // RibbonGroupFrameInside2
                                          Color.Empty,                  // RibbonGroupFrameInside3
                                          Color.Empty,                  // RibbonGroupFrameInside4
                                          SystemColors.ControlText,     // RibbonGroupCollapsedText
                                          // Non ribbon colors
                                          Color.Red, Color.Red, Color.Red, 
                                          Color.Red, Color.Red, Color.Red, 
                                          Color.Red, Color.Red, Color.Red,  
                                          // Ribbon colors
                                          ColorTable.MenuBorder,            // RibbonQATMini1
                                          groupStart,                       // RibbonQATMini2
                                          ribbonQATMini3,                   // RibbonQATMini3
                                          Color.FromArgb(32, Color.White),  // RibbonQATMini4
                                          Color.FromArgb(32, Color.White),  // RibbonQATMini5                                                       
                                          ColorTable.MenuBorder,            // RibbonQATMini1I
                                          groupStart,                       // RibbonQATMini2I
                                          ribbonQATMini3,                   // RibbonQATMini3I
                                          Color.FromArgb(32, Color.White),  // RibbonQATMini4I
                                          Color.FromArgb(32, Color.White),  // RibbonQATMini5I                                                      
                                          groupStart,                       // RibbonQATFullbar1                                                      
                                          ribbonQATMini3,                   // RibbonQATFullbar2                                                      
                                          ribbonGroupsArea1,                // RibbonQATFullbar3                                                      
                                          SystemColors.ControlText,         // RibbonQATButtonDark                                                      
                                          SystemColors.ControlLight,        // RibbonQATButtonLight                                                      
                                          groupStart,                       // RibbonQATOverflow1                                                      
                                          ColorTable.MenuBorder,            // RibbonQATOverflow2                                                      
                                          ribbonGroupSepDark,               // RibbonGroupSeparatorDark                                                      
                                          ColorTable.GripLight,             // RibbonGroupSeparatorLight                                                      
                                          // Non ribbon colors
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          Color.Red, Color.Red, Color.Red, Color.Red, Color.Red,
                                          SystemColors.Window,              // AppButtonBack1
                                          ribbonGroupsArea1,                // AppButtonBack2
                                          ColorTable.MenuBorder,            // AppButtonBorder                              
                                          ColorTable.SeparatorDark,         // AppButtonOuter1
                                          ColorTable.SeparatorDark,         // AppButtonOuter2
                                          ColorTable.StatusStripGradientBegin,     // AppButtonOuter3
                                          ColorTable.ToolStripDropDownBackground,  // AppButtonInner1
                                          ColorTable.MenuBorder,                   // AppButtonInner2
                                          ColorTable.ImageMarginGradientMiddle,    // AppButtonMenuDocs
                                          SystemColors.ControlText,                // AppButtonMenuDocsText
                                          // Non ribbon colors
                                          Color.Red, Color.Red,
                                          ColorTable.MenuBorder,            // RibbonGalleryBorder
                                          ribbonTabSelected4,               // RibbonGalleryBackNormal
                                          SystemColors.Window,              // RibbonGalleryBackTracking
                                          Color.Red,                        // RibbonGalleryBack1
                                          Color.Red,                        // RibbonGalleryBack2
                                          ribbonTabTracking3,               // RibbonTabTracking3
                                          ribbonTabTracking4,               // RibbonTabTracking4
                                          ribbonGroupBorder3,               // RibbonGroupBorder3
                                          ribbonGroupBorder4,               // RibbonGroupBorder4
                                          ribbonGroupBorder5,               // RibbonGroupBorder5
                                          ribbonGroupTitleText,             // RibbonGroupTitleText
                                          Color.Red,                        // RibbonDropArrowLight
                                          Color.Red,                        // RibbonDropArrowDark
                                          Color.Red,                        // HeaderDockInactiveBack1
                                          Color.Red,                        // HeaderDockInactiveBack2
                                          Color.Red,                        // ButtonNavigatorBorder
                                          Color.Red,                        // ButtonNavigatorText
                                          Color.Red,                        // ButtonNavigatorTrack1
                                          Color.Red,                        // ButtonNavigatorTrack2
                                          Color.Red,                        // ButtonNavigatorPressed1
                                          Color.Red,                        // ButtonNavigatorPressed2
                                          Color.Red,                        // ButtonNavigatorChecked1
                                          Color.Red,                        // ButtonNavigatorChecked2
                                        };

            // Generate second set of ribbon colors
            _disabledText = SystemColors.ControlDark;
            _disabledGlyphDark = Color.FromArgb(183, 183, 183);
            _disabledGlyphLight = Color.FromArgb(237, 237, 237);
            _contextCheckedTabBorder = ribbonGroupsArea1;
            _contextCheckedTabFill = ColorTable.CheckBackground;
            _contextGroupAreaBorder = ribbonGroupsArea1;
            _contextGroupAreaInside = ribbonGroupsArea2;
            _contextGroupFrameTop = Color.FromArgb(250, 250, 250);
            _contextGroupFrameBottom = _contextGroupFrameTop;
            _contextTabSeparator = ColorTable.MenuBorder;
            _focusTabFill = ColorTable.CheckBackground;
            _toolTipBack1 = SystemColors.Info;
            _toolTipBack2 = SystemColors.Info;
            _toolTipBorder = SystemColors.WindowFrame;
            _toolTipText = SystemColors.InfoText;
            _disabledDropDownColor = Color.Empty;
            _normalDropDownColor = Color.Empty;
            _ribbonGroupCollapsedBackContext = new Color[] { Color.FromArgb(48, 235, 235, 235), Color.FromArgb(235, 235, 235) };
            _ribbonGroupCollapsedBackContextTracking = _ribbonGroupCollapsedBackContext;
            _ribbonGroupCollapsedBorderContext = new Color[] { Color.FromArgb(160, ribbonGroupBorder1), ribbonGroupBorder1, Color.FromArgb(48, ribbonGroupsArea4), ribbonGroupsArea4 };
            _ribbonGroupCollapsedBorderContextTracking = new Color[] { Color.FromArgb(200, ribbonGroupBorder1), ribbonGroupBorder1, Color.FromArgb(48, ribbonGroupBorder1), Color.FromArgb(196, ribbonGroupBorder1) };
            Color highlight1 = MergeColors(Color.White, 0.50f, ColorTable.ButtonSelectedGradientEnd, 0.50f);
            Color highlight2 = MergeColors(Color.White, 0.25f, ColorTable.ButtonSelectedGradientEnd, 0.75f);
            Color highlight3 = MergeColors(Color.White, 0.60f, ColorTable.ButtonPressedGradientMiddle, 0.40f);
            Color highlight4 = MergeColors(Color.White, 0.25f, ColorTable.ButtonPressedGradientMiddle, 0.75f);
            Color pressed3 = MergeColors(Color.White, 0.50f, ColorTable.CheckBackground, 0.50f);
            Color pressed4 = MergeColors(Color.White, 0.25f, ColorTable.CheckPressedBackground, 0.75f);
            _appButtonNormal = new Color[] { ColorTable.SeparatorLight, ColorTable.ImageMarginGradientBegin, ColorTable.ImageMarginGradientMiddle, ColorTable.GripLight, ColorTable.ImageMarginGradientBegin };
            _appButtonTrack = new Color[] { highlight1, highlight2, ColorTable.ButtonSelectedGradientEnd, highlight3, highlight4 };
            _appButtonPressed = new Color[] { highlight1, pressed4, ColorTable.CheckPressedBackground, highlight2, pressed4 };
        }

        private Image CreateDropDownImage(Color color)
        {
            // Create image that has an alpha channel
            Image image = new Bitmap(9, 9, PixelFormat.Format32bppArgb);
            
            // Use a graphics instance for drawing the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Draw a solid arrow
                using (SolidBrush fill = new SolidBrush(color))
                    g.FillPolygon(fill, new Point[] { new Point(2, 3), new Point(4, 6), new Point(7, 3)});

                // Draw semi-transparent outline around the arrow
                using(Pen outline = new Pen(Color.FromArgb(128, color)))
                    g.DrawLines(outline, new Point[]{ new Point(1, 3), new Point(4,6), new Point(7, 3)});
            }

            return image;
        }

        private Image CreateGalleryUpImage(Color color)
        {
            // Create image that has an alpha channel
            Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

            // Use a graphics instance for drawing the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Draw a solid arrow
                using (SolidBrush fill = new SolidBrush(color))
                    g.FillPolygon(fill, new Point[] { new Point(3, 6), new Point(6, 2), new Point(9, 6) });
            }

            return image;
        }

        private Image CreateGalleryDownImage(Color color)
        {
            // Create image that has an alpha channel
            Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

            // Use a graphics instance for drawing the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Draw a solid arrow
                using (SolidBrush fill = new SolidBrush(color))
                    g.FillPolygon(fill, new Point[] { new Point(4, 3), new Point(6, 6), new Point(9, 3) });
            }

            return image;
        }

        private Image CreateGalleryDropDownImage(Color color)
        {
            // Create image that has an alpha channel
            Image image = new Bitmap(13, 7, PixelFormat.Format32bppArgb);

            // Use a graphics instance for drawing the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Draw a solid arrow
                using (SolidBrush fill = new SolidBrush(color))
                    g.FillPolygon(fill, new Point[] { new Point(4, 3), new Point(6, 6), new Point(9, 3) });

                // Draw the line above the arrow
                using (Pen pen = new Pen(color))
                    g.DrawLine(pen, 4, 1, 8, 1);
            }

            return image;
        }
        #endregion
    }
}
