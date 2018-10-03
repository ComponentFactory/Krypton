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
	/// Provides a base for Office 2010 palettes.
	/// </summary>
	public abstract class PaletteOffice2010Base : PaletteBase
    {
		#region Static Fields
        private static readonly Padding _contentPaddingGrid = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader1 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingHeader2 = new Padding(2, 1, 2, 1);
        private static readonly Padding _contentPaddingDock = new Padding(2, 2, 2, 1);
        private static readonly Padding _contentPaddingCalendar = new Padding(2);
        private static readonly Padding _contentPaddingHeaderForm = new Padding(5, 2, 3, 0);
        private static readonly Padding _contentPaddingLabel = new Padding(3, 1, 3, 1);
        private static readonly Padding _contentPaddingLabel2 = new Padding(8, 2, 8, 2);
        private static readonly Padding _contentPaddingButtonInputControl = new Padding(0);
        private static readonly Padding _contentPaddingButton12 = new Padding(1);
        private static readonly Padding _contentPaddingButton3 = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButton4 = new Padding(4, 3, 4, 3);
        private static readonly Padding _contentPaddingButton5 = new Padding(3, 3, 3, 2);
        private static readonly Padding _contentPaddingButton6 = new Padding(3);
        private static readonly Padding _contentPaddingButton7 = new Padding(1, 1, 0, 1);
        private static readonly Padding _contentPaddingButtonForm = new Padding(0);
        private static readonly Padding _contentPaddingButtonGallery = new Padding(1, 0, 1, 0);
        private static readonly Padding _contentPaddingButtonListItem = new Padding(0, -1, 0, -1);
        private static readonly Padding _contentPaddingToolTip = new Padding(2);
        private static readonly Padding _contentPaddingSuperTip = new Padding(4);
        private static readonly Padding _contentPaddingKeyTip = new Padding(0, -1, 0, -3);
        private static readonly Padding _contentPaddingContextMenuHeading = new Padding(8, 2, 8, 0);
        private static readonly Padding _contentPaddingContextMenuImage = new Padding(0);
        private static readonly Padding _contentPaddingContextMenuItemText = new Padding(9, 1, 7, 0);
        private static readonly Padding _contentPaddingContextMenuItemTextAlt = new Padding(7, 1, 6, 0);
        private static readonly Padding _contentPaddingContextMenuItemShortcutText = new Padding(3, 1, 4, 0);
        private static readonly Padding _metricPaddingRibbon = new Padding(0, 1, 1, 1);
        private static readonly Padding _metricPaddingRibbonAppButton = new Padding(3, 0, 3, 0);
        private static readonly Padding _metricPaddingHeader = new Padding(0, 3, 1, 3);
        private static readonly Padding _metricPaddingHeaderForm = new Padding(0);
        private static readonly Padding _metricPaddingInputControl = new Padding(0, 1, 0, 1);
        private static readonly Padding _metricPaddingBarInside = new Padding(3);
        private static readonly Padding _metricPaddingBarTabs = new Padding(0);
        private static readonly Padding _metricPaddingBarOutside = new Padding(0, 0, 0, 3);
        private static readonly Padding _metricPaddingPageButtons = new Padding(1, 3, 1, 3);
        
        private static readonly Image _disabledDropDown = Properties.Resources.DisabledDropDownButton;
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
        private static readonly Image _buttonSpecPendantClose = Properties.Resources._2010ButtonMDIClose;
        private static readonly Image _buttonSpecPendantMin = Properties.Resources._2010ButtonMDIMin;
        private static readonly Image _buttonSpecPendantRestore = Properties.Resources._2010ButtonMDIRestore;
        private static readonly Image _buttonSpecWorkspaceMaximize = Properties.Resources.ProfessionalMaximize;
        private static readonly Image _buttonSpecWorkspaceRestore = Properties.Resources.ProfessionalRestore;
        private static readonly Image _buttonSpecRibbonMinimize = Properties.Resources.RibbonUp2010;
        private static readonly Image _buttonSpecRibbonExpand = Properties.Resources.RibbonDown2010;
        private static readonly Image _contextMenuChecked = Properties.Resources.Office2007Checked;
        private static readonly Image _contextMenuIndeterminate = Properties.Resources.Office2007Indeterminate;
        private static readonly Image _treeExpandWhite = Properties.Resources.TreeExpandWhite;
        private static readonly Image _treeCollapseBlack = Properties.Resources.TreeCollapseBlack;
        
        private static readonly Color _gridTextColor = Color.Black;
        private static readonly Color _disabledText2 = Color.FromArgb(128, 128, 128);
        private static readonly Color _disabledText = Color.FromArgb(167, 167, 167);
        private static readonly Color _disabledBack = Color.FromArgb(235, 235, 235);
        private static readonly Color _disabledBorder = Color.FromArgb(212, 212, 212);
        private static readonly Color _disabledGlyphDark = Color.FromArgb(183, 183, 183);
        private static readonly Color _disabledGlyphLight = Color.FromArgb(237, 237, 237);
        private static readonly Color _contextCheckedTabBorder1 = Color.FromArgb(223, 119, 0);
        private static readonly Color _contextCheckedTabBorder2 = Color.FromArgb(230, 190, 129);
        private static readonly Color _contextCheckedTabBorder3 = Color.FromArgb(220, 202, 171);
        private static readonly Color _contextCheckedTabBorder4 = Color.FromArgb(255, 252, 247);
        private static readonly Color _contextTabSeparator = Color.White;
        private static readonly Color _contextTextColor = Color.White;
        private static readonly Color _todayBorder = Color.FromArgb(187, 85, 3);
        private static readonly Color _toolTipBack1 = Color.FromArgb(255, 255, 255);
        private static readonly Color _toolTipBack2 = Color.FromArgb(201, 217, 239);
        private static readonly Color _toolTipBorder = Color.FromArgb(118, 118, 118);
        private static readonly Color _toolTipText = Color.FromArgb(76, 76, 76);
        private static readonly Color _contextMenuBack = Color.White;
        private static readonly Color _contextMenuBorder = Color.FromArgb(134, 134, 134);
        private static readonly Color _contextMenuHeadingBorder = Color.FromArgb(197, 197, 197);
        private static readonly Color _contextMenuImageBackChecked = Color.FromArgb(252, 241, 194);
        private static readonly Color _contextMenuImageBorderChecked = Color.FromArgb(242, 149, 54);
        private static readonly Color _formCloseBorderTracking = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseBorderPressed = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseBorderCheckedNormal = Color.FromArgb(155, 61, 61);
        private static readonly Color _formCloseTracking1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseTracking2 = Color.FromArgb(227, 97, 98);
        private static readonly Color _formClosePressed1 = Color.FromArgb(242, 119, 118);
        private static readonly Color _formClosePressed2 = Color.FromArgb(206, 85, 84);
        private static readonly Color _formCloseChecked1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseChecked2 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseCheckedTracking1 = Color.FromArgb(255, 132, 130);
        private static readonly Color _formCloseCheckedTracking2 = Color.FromArgb(255, 132, 130);
        private static readonly Color[] _appButtonNormal = new Color[] { Color.FromArgb(243, 245, 248), Color.FromArgb(214, 220, 231), Color.FromArgb(188, 198, 211), Color.FromArgb(254, 254, 255), Color.FromArgb(206, 213, 225) };
        private static readonly Color[] _appButtonTrack = new Color[] { Color.FromArgb(255, 251, 230), Color.FromArgb(248, 230, 143), Color.FromArgb(238, 213, 126), Color.FromArgb(254, 247, 129), Color.FromArgb(240, 201, 41) };
        private static readonly Color[] _appButtonPressed = new Color[] { Color.FromArgb(235, 227, 196), Color.FromArgb(228, 198, 149), Color.FromArgb(166, 97, 7), Color.FromArgb(242, 155, 57), Color.FromArgb(236, 136, 9) };
        private static readonly Color[] _buttonBorderColors = new Color[]{ Color.FromArgb(180, 180, 180), // Button, Disabled, Border
                                                                           Color.FromArgb(237, 201, 88),  // Button, Tracking, Border 1
                                                                           Color.FromArgb(243, 213, 73),  // Button, Tracking, Border 2
                                                                           Color.FromArgb(194, 118, 43),  // Button, Pressed, Border 1
                                                                           Color.FromArgb(194, 158, 71),  // Button, Pressed, Border 2
                                                                           Color.FromArgb(194, 138, 48),  // Button, Checked, Border 1
                                                                           Color.FromArgb(194, 164, 77)   // Button, Checked, Border 2
                                                                         };
        private static readonly Color[] _buttonBackColors = new Color[]{ Color.FromArgb(250, 250, 250), // Button, Disabled, Back 1
                                                                         Color.FromArgb(250, 250, 250), // Button, Disabled, Back 2
                                                                         Color.FromArgb(248, 225, 135), // Button, Tracking, Back 1
                                                                         Color.FromArgb(251, 248, 224), // Button, Tracking, Back 2
                                                                         Color.FromArgb(255, 228, 138), // Button, Pressed, Back 1
                                                                         Color.FromArgb(194, 118, 43),  // Button, Pressed, Back 2
                                                                         Color.FromArgb(255, 216, 108), // Button, Checked, Back 1
                                                                         Color.FromArgb(255, 244, 128), // Button, Checked, Back 2
                                                                         Color.FromArgb(255, 225, 104), // Button, Checked Tracking, Back 1
                                                                         Color.FromArgb(255, 249, 196)  // Button, Checked Tracking, Back 2
                                                                       };
        #endregion

		#region Instance Fields
        private KryptonColorTable2010 _table;
        private Color[] _ribbonColors;
        private Color[] _trackBarColors;
        private ImageList _checkBoxList;
        private ImageList _galleryButtonList;
        private Image[] _radioButtonArray;
        private Font _header1ShortFont;
        private Font _header2ShortFont;
        private Font _header1LongFont;
        private Font _header2LongFont;
        private Font _superToolFont;
        private Font _headerFormFont;
        private Font _buttonFont;
        private Font _buttonFontNavigatorStack;
        private Font _buttonFontNavigatorMini;
        private Font _tabFontNormal;
        private Font _tabFontSelected;
        private Font _ribbonTabFont;
        private Font _ribbonTabContextFont;
        private Font _gridFont;
        private Font _calendarFont;
        private Font _calendarBoldFont;
        private Font _boldFont;
        private Font _italicFont;
        private string _baseFontName;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteOffice2010Base class.
		/// </summary>
        /// <param name="schemeColors">Array of palette specific colors.</param>
        /// <param name="checkBoxList">List of images for check box.</param>
        /// <param name="galleryButtonList">List of images for gallery buttons.</param>
        /// <param name="radioButtonArray">Array of images for radio button.</param>
        /// <param name="trackBarColors">Array of track bar specific colors.</param>
        public PaletteOffice2010Base(Color[] schemeColors,
                                     ImageList checkBoxList,
                                     ImageList galleryButtonList,
                                     Image[] radioButtonArray,
                                     Color[] trackBarColors)
		{
            Debug.Assert(schemeColors != null);
            Debug.Assert(checkBoxList != null);
            Debug.Assert(galleryButtonList != null);
            Debug.Assert(radioButtonArray != null);

            // Remember incoming sets of values
            _ribbonColors = schemeColors;
            _checkBoxList = checkBoxList;
            _galleryButtonList = galleryButtonList;
            _radioButtonArray = radioButtonArray;
            _trackBarColors = trackBarColors;

            // Get the font settings from the system
            DefineFonts();
        }
		#endregion

        #region AllowFormChrome
        /// <summary>
        /// Gets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetAllowFormChrome()
        {
            return InheritBool.True;
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
            return KryptonManager.RenderOffice2010;
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
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCalendarDay:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
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
                case PaletteBackStyle.ButtonInputControl:
                    if ((state == PaletteState.Disabled) ||
                        (state == PaletteState.Normal))
                        return InheritBool.False;
                    else
                        return InheritBool.True;
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
                case PaletteBackStyle.PanelAlternate:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.SeparatorHighInternalProfile:
                case PaletteBackStyle.SeparatorHighProfile:
                case PaletteBackStyle.SeparatorLowProfile:
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
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderForm:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonCalendarDay:
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
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListNormal1];
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListPressed1];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListSelected];
                    }
                case PaletteBackStyle.GridHeaderColumnSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColNormal1];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColPressed1];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColSelected1];
                    }
                case PaletteBackStyle.GridHeaderRowSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowNormal];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowPressed];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowSelected];
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _ribbonColors[(int)SchemeOfficeColors.GridDataCellSelected];
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.GridDataCellSheet:
                    if (state == PaletteState.CheckedNormal)
                        return _buttonBackColors[6];
                    else
                        return SystemColors.Window;
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
                                return _disabledBack;
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
                            {
                                if (state == PaletteState.Tracking)
                                    return _buttonBackColors[2];
                                else
                                    return _buttonBackColors[4];
                            }
                            else
                                return SystemColors.Window;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.TabHighProfile)
                            {
                                if (state == PaletteState.CheckedNormal)
                                    return _buttonBackColors[6];
                                else if (state == PaletteState.CheckedPressed)
                                    return _buttonBackColors[4];
                                else
                                    return _buttonBackColors[8];
                            }
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
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.Tracking:
                            return SystemColors.Window;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderInactive1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderActive1];
                case PaletteBackStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack2];
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                case PaletteBackStyle.HeaderDockInactive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderDockInactiveBack1];
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _buttonBackColors[6];
                case PaletteBackStyle.HeaderSecondary:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderSecondaryBack1];
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.SeparatorHighInternalBorder1];
                case PaletteBackStyle.SeparatorHighProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.SeparatorHighBorder1];
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return _ribbonColors[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.PanelAlternate:
                    return _ribbonColors[(int)SchemeOfficeColors.PanelAlternative];
                case PaletteBackStyle.PanelRibbonInactive:
                    return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactiveLight];
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactiveLight];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderActiveLight];
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return SystemColors.Window;
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBackDisabled];
                    else
                    {
                        if ((state == PaletteState.Tracking) ||(style == PaletteBackStyle.InputControlStandalone))
                            return _ribbonColors[(int)SchemeOfficeColors.InputControlBackNormal];
                        else
                            return _ribbonColors[(int)SchemeOfficeColors.InputControlBackInactive];
                    }
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack1];
                case PaletteBackStyle.ControlToolTip:
                    return _toolTipBack1;
                case PaletteBackStyle.ContextMenuOuter:
                    return _contextMenuBack;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                                return _buttonBackColors[2];
                        default:
                            return _contextMenuBack;
                    }
                case PaletteBackStyle.ContextMenuInner:
                    return _contextMenuBack;
                case PaletteBackStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingBack];
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuImageColumn];
                case PaletteBackStyle.ContextMenuItemImage:
                    return _contextMenuImageBackChecked;
                case PaletteBackStyle.ButtonForm:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack1Checked];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack1Track];
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack1CheckTrack];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack1Pressed];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _formCloseChecked1;
                        case PaletteState.Tracking:
                            return _formCloseTracking1;
                        case PaletteState.CheckedTracking:
                            return _formCloseCheckedTracking1;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _formClosePressed1;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
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
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
					{
	                    case PaletteState.Disabled:
                            if (style == PaletteBackStyle.ButtonGallery)
                                return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBack1];
                            else
                                return _disabledBack;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                        case PaletteState.NormalDefaultOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBack1];
                        case PaletteState.CheckedNormal:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                            else
                                return _buttonBackColors[6];
                        case PaletteState.Tracking:
                                return _buttonBackColors[2];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBackColors[4];
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                            else
                                return _buttonBackColors[8];
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                    switch(state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBackColors[1];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorTrack1];
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorPressed1];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorChecked1];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalNavigatorBack1];
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
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListNormal2];
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListPressed2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridListSelected];
                    }
                case PaletteBackStyle.GridHeaderColumnSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColNormal2];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColPressed2];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetColSelected2];
                    }
                case PaletteBackStyle.GridHeaderRowSheet:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        default:
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowNormal];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowPressed];
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.GridSheetRowSelected];
                    }
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    if (state == PaletteState.CheckedNormal)
                        return _ribbonColors[(int)SchemeOfficeColors.GridDataCellSelected];
                    else
                        return SystemColors.Window;
                case PaletteBackStyle.GridDataCellSheet:
                    if (state == PaletteState.CheckedNormal)
                        return _buttonBackColors[7];
                    else
                        return SystemColors.Window;
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
                                return _disabledBack;
                        case PaletteState.Normal:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack2];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBackStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return SystemColors.Window;
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
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.HeaderDockInactiveBack1];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _buttonBackColors[4];
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
                            return _disabledBack;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.HeaderDockInactiveBack1];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBackColors[4];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderInactive2];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderActive2];
                case PaletteBackStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack2];
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack2];
                case PaletteBackStyle.HeaderDockInactive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderDockInactiveBack2];
                case PaletteBackStyle.HeaderDockActive:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _buttonBackColors[7];
                case PaletteBackStyle.HeaderSecondary:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderSecondaryBack2];
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.SeparatorHighInternalBorder2];
                case PaletteBackStyle.SeparatorHighProfile:
                    if (state == PaletteState.Disabled)
                        return _disabledBack;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.SeparatorHighBorder2];
                case PaletteBackStyle.SeparatorLowProfile:
                case PaletteBackStyle.SeparatorCustom1:
                case PaletteBackStyle.PanelClient:
                case PaletteBackStyle.PanelCustom1:
                case PaletteBackStyle.ControlGroupBox:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                    return _ribbonColors[(int)SchemeOfficeColors.PanelClient];
                case PaletteBackStyle.PanelAlternate:
                    return _ribbonColors[(int)SchemeOfficeColors.PanelAlternative];
                case PaletteBackStyle.PanelRibbonInactive:
                    return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactiveDark];
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactiveDark];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderActiveDark];
                case PaletteBackStyle.ControlClient:
                case PaletteBackStyle.ControlAlternate:
                case PaletteBackStyle.ControlCustom1:
                    return SystemColors.Window;
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBackDisabled];
                    else
                    {
                        if ((state == PaletteState.Tracking) || (style == PaletteBackStyle.InputControlStandalone))
                            return _ribbonColors[(int)SchemeOfficeColors.InputControlBackNormal];
                        else
                            return _ribbonColors[(int)SchemeOfficeColors.InputControlBackInactive];
                    }
                case PaletteBackStyle.ControlRibbon:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonTabSelected4];
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonBack2];
                case PaletteBackStyle.ControlToolTip:
                    return _ribbonColors[(int)SchemeOfficeColors.ToolTipBottom];
                case PaletteBackStyle.ContextMenuOuter:
                    return _contextMenuBack;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                            return _buttonBackColors[3];
                        default:
                            return _contextMenuBack;
                    }
                case PaletteBackStyle.ContextMenuInner:
                    return _contextMenuBack;
                case PaletteBackStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingBack];
                case PaletteBackStyle.ContextMenuItemImageColumn:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuImageColumn];
                case PaletteBackStyle.ContextMenuItemImage:
                    return _contextMenuImageBackChecked;
                case PaletteBackStyle.ButtonForm:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack2Checked];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack2Track];
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack2CheckTrack];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBack2Pressed];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _formCloseChecked2;
                        case PaletteState.Tracking:
                            return _formCloseTracking2;
                        case PaletteState.CheckedTracking:
                            return _formCloseCheckedTracking2;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _formClosePressed2;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
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
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            if (style == PaletteBackStyle.ButtonGallery)
                                return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBack1];
                            else
                                return _buttonBackColors[1];
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack2];
                        case PaletteState.NormalDefaultOverride:
                            if ((style == PaletteBackStyle.ButtonLowProfile) ||
                                (style == PaletteBackStyle.ButtonBreadCrumb) ||
                                (style == PaletteBackStyle.ButtonListItem) ||
                                (style == PaletteBackStyle.ButtonCommand) ||
                                (style == PaletteBackStyle.ButtonButtonSpec) ||
                                (style == PaletteBackStyle.ContextMenuItemHighlight))
                                return Color.Empty;
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBack2];
                        case PaletteState.CheckedNormal:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack2];
                            else
                                return _buttonBackColors[7];
                        case PaletteState.Tracking:
                                return _buttonBackColors[3];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBackColors[5];
                        case PaletteState.CheckedTracking:
                            if (style == PaletteBackStyle.ButtonInputControl)
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                            else
                                return _buttonBackColors[9];
                        default:
                            throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBackColors[1];
                        case PaletteState.Tracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorTrack2];
                        case PaletteState.Pressed:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorPressed2];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorChecked2];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalNavigatorBack2];
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
                case PaletteBackStyle.HeaderForm:
                    return PaletteColorStyle.Rounding5;
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                    return PaletteColorStyle.Rounded;
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowCustom1:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderRowSheet:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellCustom1:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.GridDataCellSheet:
                    return PaletteColorStyle.ExpertChecked;
                case PaletteBackStyle.TabHighProfile:
                case PaletteBackStyle.TabCustom1:
                case PaletteBackStyle.TabCustom2:
                case PaletteBackStyle.TabCustom3:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.GlassFade;
                        default:
                            return PaletteColorStyle.QuarterPhase;
                    }
                case PaletteBackStyle.TabStandardProfile:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.Solid;
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return PaletteColorStyle.GlassFade;
                        default:
                            return PaletteColorStyle.QuarterPhase;
                    }
                case PaletteBackStyle.TabLowProfile:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.TabOneNote:
                case PaletteBackStyle.TabDock:
                case PaletteBackStyle.TabDockAutoHidden:
                    return PaletteColorStyle.Linear;
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
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ControlCustom1:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuItemImageColumn:
                case PaletteBackStyle.InputControlStandalone:
                case PaletteBackStyle.InputControlRibbon:
                case PaletteBackStyle.InputControlCustom1:
                case PaletteBackStyle.GridBackgroundList:
                case PaletteBackStyle.GridBackgroundSheet:
                case PaletteBackStyle.GridBackgroundCustom1:
                case PaletteBackStyle.HeaderCalendar:
                case PaletteBackStyle.ButtonCalendarDay:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ControlRibbonAppMenu:
                    return PaletteColorStyle.Switch90;
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Tracking)
                        return PaletteColorStyle.GlassTrackingFull;
                    else
                        return PaletteColorStyle.Solid;
                case PaletteBackStyle.ControlToolTip:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.FormMain:
                case PaletteBackStyle.FormCustom1:
                    return PaletteColorStyle.SolidAllLine;
                case PaletteBackStyle.SeparatorHighProfile:
                    return PaletteColorStyle.RoundedTopLight;
                case PaletteBackStyle.SeparatorHighInternalProfile:
                    return PaletteColorStyle.Linear;
                case PaletteBackStyle.HeaderPrimary:
                case PaletteBackStyle.HeaderDockInactive:
                case PaletteBackStyle.HeaderSecondary:
                case PaletteBackStyle.HeaderCustom1:
                case PaletteBackStyle.HeaderCustom2:
                case PaletteBackStyle.HeaderDockActive:
                    return PaletteColorStyle.Rounded;
                case PaletteBackStyle.ButtonForm:
                case PaletteBackStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                        case PaletteState.CheckedNormal:
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return PaletteColorStyle.Linear;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.LinearShadow;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ButtonAlternate:
                case PaletteBackStyle.ButtonStandalone:
                case PaletteBackStyle.ButtonLowProfile:
                case PaletteBackStyle.ButtonBreadCrumb:
                case PaletteBackStyle.ButtonListItem:
                case PaletteBackStyle.ButtonCommand:
                case PaletteBackStyle.ButtonButtonSpec:
                case PaletteBackStyle.ButtonCluster:
                case PaletteBackStyle.ButtonGallery:
                case PaletteBackStyle.ButtonCustom1:
                case PaletteBackStyle.ButtonCustom2:
                case PaletteBackStyle.ButtonCustom3:
                case PaletteBackStyle.ButtonInputControl:
                case PaletteBackStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return PaletteColorStyle.Solid;
                        case PaletteState.Normal:
                            return PaletteColorStyle.Linear;
                        case PaletteState.Tracking:
                            return PaletteColorStyle.ExpertTracking;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.ExpertPressed;
                        case PaletteState.CheckedNormal:
                            return PaletteColorStyle.ExpertChecked;
                        case PaletteState.CheckedTracking:
                            return PaletteColorStyle.ExpertCheckedTracking;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBackStyle.ContextMenuItemImage:
                    return PaletteColorStyle.Solid;
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                    switch(state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return PaletteColorStyle.SolidAllLine;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.ExpertSquareHighlight;
                        default:
                            return PaletteColorStyle.Solid;
                    }
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
                case PaletteBackStyle.GridHeaderColumnList:
                case PaletteBackStyle.GridHeaderColumnSheet:
                case PaletteBackStyle.GridHeaderColumnCustom1:
                case PaletteBackStyle.GridHeaderRowList:
                case PaletteBackStyle.GridHeaderRowSheet:
                case PaletteBackStyle.GridHeaderRowCustom1:
                case PaletteBackStyle.GridDataCellList:
                case PaletteBackStyle.GridDataCellSheet:
                case PaletteBackStyle.GridDataCellCustom1:
                case PaletteBackStyle.ContextMenuItemImage:
                case PaletteBackStyle.ContextMenuItemHighlight:
                case PaletteBackStyle.ContextMenuOuter:
                case PaletteBackStyle.ContextMenuInner:
                case PaletteBackStyle.ContextMenuHeading:
                case PaletteBackStyle.ContextMenuSeparator:
                case PaletteBackStyle.ContextMenuItemSplit:
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
                    return 90f;
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
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return InheritBool.False;
                        default:
                            return InheritBool.True;
                    }
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
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
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
                    return PaletteDrawBorders.All;
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    return PaletteDrawBorders.All;
                case PaletteBorderStyle.ContextMenuHeading:
                    return PaletteDrawBorders.Bottom;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    return PaletteDrawBorders.Top;
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return PaletteDrawBorders.Right;
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ContextMenuInner:
                    return PaletteDrawBorders.None;
                case PaletteBorderStyle.HeaderForm:
                    return PaletteDrawBorders.TopLeftRight;
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
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabDock:
                case PaletteBorderStyle.TabDockAutoHidden:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
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
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    return PaletteGraphicsHint.AntiAlias;
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
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
            {
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return _disabledBorder;
                        else
                            return _todayBorder;
                    }

                return Color.Empty;
            }
            
			switch (style)
			{
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
                                return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _buttonBorderColors[2];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[2];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack2];
                case PaletteBorderStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderActive];
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return _contextMenuHeadingBorder;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBorderColors[0];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[1];
                        default:
                            return _contextMenuHeadingBorder;
                    }
                case PaletteBorderStyle.ContextMenuItemImage:
                    return _contextMenuImageBorderChecked;
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBorderDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBorderNormal];
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.GridDataCellBorder];
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                case PaletteBorderStyle.ContextMenuOuter:
                    return _contextMenuBorder;
                case PaletteBorderStyle.ContextMenuInner:
                    return _contextMenuBack;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _toolTipBorder;
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderActive];
                case PaletteBorderStyle.ButtonForm:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderCheck];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderTrack];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderPressed];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _formCloseBorderCheckedNormal;
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _formCloseBorderTracking;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _formCloseBorderPressed;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
					{
						case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.ButtonGallery)
                                return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBack2];
                            else
                                return _buttonBorderColors[0];
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.NormalDefaultOverride:
                            if ((style == PaletteBorderStyle.ButtonLowProfile) ||
                                (style == PaletteBorderStyle.ButtonBreadCrumb) ||
                                (style == PaletteBorderStyle.ButtonListItem) ||
                                (style == PaletteBorderStyle.ButtonCommand) ||
                                (style == PaletteBorderStyle.ButtonButtonSpec) ||
                                (style == PaletteBorderStyle.ContextMenuItemHighlight))
                                return Color.Empty;
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBorder];
                        case PaletteState.CheckedNormal:
                            return _buttonBorderColors[5];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[1];
						case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[3];
                        case PaletteState.CheckedTracking:
                            return _buttonBorderColors[3];
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBorderStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBorderColors[0];
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[1];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[3];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                        case PaletteState.NormalDefaultOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBack1];
                        case PaletteState.CheckedNormal:
                            return _buttonBackColors[6];
                        case PaletteState.Tracking:
                            return _buttonBackColors[2];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBackColors[4];
                        case PaletteState.CheckedTracking:
                            return _buttonBackColors[8];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                    return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorBorder];
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
                // Check for the calendar day today override
                if (state == PaletteState.TodayOverride)
                    if (style == PaletteBorderStyle.ButtonCalendarDay)
                    {
                        if (state == PaletteState.Disabled)
                            return _disabledBorder;
                        else
                            return _todayBorder;
                    }
                
                return Color.Empty;
            }

			switch (style)
			{
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
                                return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            if (style == PaletteBorderStyle.TabLowProfile)
                                return Color.Empty;
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _buttonBorderColors[2];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBorder;
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[2];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderHeaderActive];
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlGroupBox:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.ControlBorder];
                case PaletteBorderStyle.HeaderCalendar:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.HeaderPrimaryBack2];
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                    return _contextMenuHeadingBorder;
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBorderColors[0];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[2];
                        default:
                            return _contextMenuHeadingBorder;
                    }
                case PaletteBorderStyle.ContextMenuItemImage:
                    return _contextMenuImageBorderChecked;
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBorderDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlBorderNormal];
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.GridDataCellBorder];
                case PaletteBorderStyle.ControlRibbon:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupsArea1];
                case PaletteBorderStyle.ControlRibbonAppMenu:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.AppButtonBorder];
                case PaletteBorderStyle.ContextMenuOuter:
                    return _contextMenuBorder;
                case PaletteBorderStyle.ContextMenuInner:
                    return _contextMenuBack;
                case PaletteBorderStyle.ControlToolTip:
                    if (state == PaletteState.Disabled)
                        return _disabledBorder;
                    else
                        return _toolTipBorder;
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormBorderActive];
                case PaletteBorderStyle.ButtonForm:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderCheck];
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderTrack];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.FormButtonBorderPressed];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                        case PaletteState.Normal:
                        case PaletteState.NormalDefaultOverride:
                            return Color.Empty;
                        case PaletteState.CheckedNormal:
                            return _formCloseBorderCheckedNormal;
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _formCloseBorderTracking;
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _formCloseBorderPressed;
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
					{
						case PaletteState.Disabled:
                            if (style == PaletteBorderStyle.ButtonGallery)
                                return _ribbonColors[(int)SchemeOfficeColors.RibbonGalleryBack2];
                            else
                                return _buttonBorderColors[0];
						case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.NormalDefaultOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBorder];
                        case PaletteState.CheckedNormal:
                            return _buttonBorderColors[6];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[2];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[4];
                        case PaletteState.CheckedTracking:
                            return _buttonBorderColors[4];
						default:
							throw new ArgumentOutOfRangeException("state");
					}
                case PaletteBorderStyle.ButtonInputControl:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _buttonBorderColors[0];
                        case PaletteState.Normal:
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                            return _buttonBorderColors[2];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBorderColors[4];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return _disabledBack;
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                        case PaletteState.NormalDefaultOverride:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalDefaultBack1];
                        case PaletteState.CheckedNormal:
                            return _buttonBackColors[6];
                        case PaletteState.Tracking:
                            return _buttonBackColors[2];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _buttonBackColors[4];
                        case PaletteState.CheckedTracking:
                            return _buttonBackColors[8];
                        default:
                            throw new ArgumentOutOfRangeException("state");
                    }
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                    return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorBorder];
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
            if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
                return PaletteColorStyle.Inherit;
            
			switch (style)
			{
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.HeaderPrimary:
                case PaletteBorderStyle.HeaderDockInactive:
                case PaletteBorderStyle.HeaderDockActive:
                case PaletteBorderStyle.HeaderSecondary:
                case PaletteBorderStyle.HeaderCustom1:
                case PaletteBorderStyle.HeaderCustom2:
                case PaletteBorderStyle.TabHighProfile:
                case PaletteBorderStyle.TabStandardProfile:
                case PaletteBorderStyle.TabLowProfile:
                case PaletteBorderStyle.TabOneNote:
                case PaletteBorderStyle.TabCustom1:
                case PaletteBorderStyle.TabCustom2:
                case PaletteBorderStyle.TabCustom3:
                    return PaletteColorStyle.Sigma;
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return PaletteColorStyle.Solid;
                        default:
                            return PaletteColorStyle.Sigma;
                    }
                case PaletteBorderStyle.TabDockAutoHidden:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.Solid;
                        default:
                            return PaletteColorStyle.Sigma;
                    }
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
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                case PaletteBorderStyle.HeaderForm:
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.HeaderCalendar:
                case PaletteBorderStyle.ButtonCalendarDay:
                    return PaletteColorStyle.Solid;
                case PaletteBorderStyle.ContextMenuItemSplit:
                    if (state == PaletteState.Tracking)
                        return PaletteColorStyle.Sigma;
                    else
                        return PaletteColorStyle.Solid;
                case PaletteBorderStyle.ContextMenuSeparator:
                    return PaletteColorStyle.Dashed;
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
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return PaletteColorStyle.Solid;
                        case PaletteState.Disabled:
                        case PaletteState.NormalDefaultOverride:
                            return PaletteColorStyle.Solid;
                        default:
                            return PaletteColorStyle.Linear;
                    }
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
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
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
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
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
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
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
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
                case PaletteBorderStyle.ButtonInputControl:
                case PaletteBorderStyle.SeparatorLowProfile:
                case PaletteBorderStyle.SeparatorHighInternalProfile:
                case PaletteBorderStyle.SeparatorHighProfile:
                case PaletteBorderStyle.SeparatorCustom1:
                case PaletteBorderStyle.ControlClient:
                case PaletteBorderStyle.ControlAlternate:
                case PaletteBorderStyle.ControlCustom1:
                case PaletteBorderStyle.ContextMenuInner:
                case PaletteBorderStyle.ContextMenuHeading:
                case PaletteBorderStyle.ContextMenuSeparator:
                case PaletteBorderStyle.ContextMenuItemSplit:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.InputControlStandalone:
                case PaletteBorderStyle.InputControlRibbon:
                case PaletteBorderStyle.InputControlCustom1:
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
                case PaletteBorderStyle.GridHeaderColumnList:
                case PaletteBorderStyle.GridHeaderColumnSheet:
                case PaletteBorderStyle.GridHeaderColumnCustom1:
                case PaletteBorderStyle.GridHeaderRowList:
                case PaletteBorderStyle.GridHeaderRowSheet:
                case PaletteBorderStyle.GridHeaderRowCustom1:
                case PaletteBorderStyle.GridDataCellList:
                case PaletteBorderStyle.GridDataCellSheet:
                case PaletteBorderStyle.GridDataCellCustom1:
                case PaletteBorderStyle.ButtonCalendarDay:
                case PaletteBorderStyle.ButtonNavigatorStack:
                case PaletteBorderStyle.ButtonNavigatorOverflow:
                case PaletteBorderStyle.ButtonNavigatorMini:
                    return 0;
                case PaletteBorderStyle.ControlToolTip:
                case PaletteBorderStyle.ContextMenuItemImage:
                    return 1;
                case PaletteBorderStyle.ButtonStandalone:
                case PaletteBorderStyle.ButtonGallery:
                case PaletteBorderStyle.ButtonAlternate:
                case PaletteBorderStyle.ButtonLowProfile:
                case PaletteBorderStyle.ButtonBreadCrumb:
                case PaletteBorderStyle.ButtonListItem:
                case PaletteBorderStyle.ButtonCommand:
                case PaletteBorderStyle.ButtonButtonSpec:
                case PaletteBorderStyle.ButtonCluster:
                case PaletteBorderStyle.ButtonForm:
                case PaletteBorderStyle.ButtonFormClose:
                case PaletteBorderStyle.ButtonCustom1:
                case PaletteBorderStyle.ButtonCustom2:
                case PaletteBorderStyle.ButtonCustom3:
                case PaletteBorderStyle.ContextMenuOuter:
                case PaletteBorderStyle.ContextMenuItemHighlight:
                    return 2;
                case PaletteBorderStyle.ControlRibbon:
                case PaletteBorderStyle.ControlRibbonAppMenu:
                case PaletteBorderStyle.ControlGroupBox:
                    return 3;
                case PaletteBorderStyle.FormMain:
                case PaletteBorderStyle.FormCustom1:
                    return 5;
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
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemHighlight:
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
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemHighlight:
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
                case PaletteBorderStyle.ContextMenuItemImage:
                case PaletteBorderStyle.ContextMenuItemImageColumn:
                case PaletteBorderStyle.ContextMenuItemHighlight:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
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
                case PaletteContentStyle.ContextMenuItemImage:
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
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return _buttonFontNavigatorStack;
                case PaletteContentStyle.ButtonNavigatorMini:
                    return _buttonFontNavigatorMini;
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorStack:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonNavigatorStack:
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
                case PaletteContentStyle.LabelKeyTip:
                case PaletteContentStyle.LabelSuperTip:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.HeaderCalendar:
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
                case PaletteContentStyle.ButtonCalendarDay:
                case PaletteContentStyle.ButtonAlternate:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                switch (style)
                {
                    case PaletteContentStyle.LabelNormalControl:
                    case PaletteContentStyle.LabelBoldControl:
                    case PaletteContentStyle.LabelItalicControl:
                    case PaletteContentStyle.LabelTitleControl:
                        switch (state)
                        {
                            case PaletteState.LinkNotVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkNotVisitedOverrideControl];
                            case PaletteState.LinkVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkVisitedOverrideControl];
                            case PaletteState.LinkPressedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkPressedOverrideControl];
                            default:
                                // All other override states do nothing
                                return Color.Empty;
                        }
                    case PaletteContentStyle.LabelNormalPanel:
                    case PaletteContentStyle.LabelBoldPanel:
                    case PaletteContentStyle.LabelItalicPanel:
                    case PaletteContentStyle.LabelTitlePanel:
                    case PaletteContentStyle.LabelGroupBoxCaption:
                        switch (state)
                        {
                            case PaletteState.LinkNotVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkNotVisitedOverridePanel];
                            case PaletteState.LinkVisitedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkVisitedOverridePanel];
                            case PaletteState.LinkPressedOverride:
                                return _ribbonColors[(int)SchemeOfficeColors.LinkPressedOverridePanel];
                            default:
                                // All other override states do nothing
                                return Color.Empty;
                        }
                    default:
                        return Color.Empty;
                    }
            }

            switch (style)
            {
                case PaletteContentStyle.HeaderForm:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortActive];
            }

            if ((state == PaletteState.Disabled) && 
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonInputControl) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
                return _disabledText;

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
                    return _gridTextColor;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _ribbonColors[(int)SchemeOfficeColors.HeaderText];
                case PaletteContentStyle.HeaderDockActive:
                    return Color.Black;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextNormal];
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingText];
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.TabDockAutoHidden:
                    return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.ButtonCalendarDay:
                    if (state == PaletteState.Disabled)
                        return _disabledText2;
                    else
                        return Color.Black;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            if (style == PaletteContentStyle.ButtonListItem)
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormTracking];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                        case PaletteState.CheckedNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormNormal];
                    }
                case PaletteContentStyle.ButtonInputControl:
                    if (state != PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownNormal1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownDisabled1];
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
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
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderShortActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonInputControl) &&
                (style != PaletteContentStyle.ButtonCalendarDay))
                return _disabledText;

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
                    return _gridTextColor;
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _ribbonColors[(int)SchemeOfficeColors.HeaderText];
                case PaletteContentStyle.HeaderDockActive:
                    return Color.Black;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextNormal];
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingText];
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.TabDockAutoHidden:
                    return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.ButtonCalendarDay:
                    if (state == PaletteState.Disabled)
                        return _disabledText2;
                    else
                        return Color.Black;
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonButtonSpec:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            if (style == PaletteContentStyle.ButtonListItem)
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormTracking];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormNormal];
                    }
                case PaletteContentStyle.ButtonInputControl:
                    if (state != PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownNormal2];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownDisabled2];
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonCalendarDay:
                    return _calendarFont;
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                case PaletteContentStyle.ButtonInputControl:
                    return _buttonFont;
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                    return PaletteRelativeAlign.Center;
                case PaletteContentStyle.ButtonCalendarDay:
                    return PaletteRelativeAlign.Far;
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                    return PaletteRelativeAlign.Far;
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
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderLongInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderLongActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonInputControl))
                return _disabledText;

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
                    return _gridTextColor;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _ribbonColors[(int)SchemeOfficeColors.HeaderText];
                case PaletteContentStyle.HeaderDockActive:
                    return Color.Black;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextNormal];
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingText];
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.TabDockAutoHidden:
                    return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            if (style == PaletteContentStyle.ButtonListItem)
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormTracking];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormNormal];
                    }
                case PaletteContentStyle.ButtonInputControl:
                    if (state != PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownNormal1];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownDisabled1];
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
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
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderLongInactive];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.FormHeaderLongActive];
            }

            if ((state == PaletteState.Disabled) &&
                (style != PaletteContentStyle.LabelToolTip) &&
                (style != PaletteContentStyle.LabelSuperTip) &&
                (style != PaletteContentStyle.LabelKeyTip) &&
                (style != PaletteContentStyle.InputControlStandalone) &&
                (style != PaletteContentStyle.InputControlRibbon) &&
                (style != PaletteContentStyle.InputControlCustom1) &&
                (style != PaletteContentStyle.ButtonInputControl))
                return _disabledText;

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
                    return _gridTextColor;
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderSecondary:
                case PaletteContentStyle.HeaderCustom1:
                case PaletteContentStyle.HeaderCustom2:
                    return _ribbonColors[(int)SchemeOfficeColors.HeaderText];
                case PaletteContentStyle.HeaderDockActive:
                    return Color.Black;
                case PaletteContentStyle.InputControlStandalone:
                case PaletteContentStyle.InputControlRibbon:
                case PaletteContentStyle.InputControlCustom1:
                    if (state == PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextDisabled];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputControlTextNormal];
                case PaletteContentStyle.LabelNormalPanel:
                case PaletteContentStyle.LabelBoldPanel:
                case PaletteContentStyle.LabelItalicPanel:
                case PaletteContentStyle.LabelTitlePanel:
                case PaletteContentStyle.LabelGroupBoxCaption:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                case PaletteContentStyle.LabelNormalControl:
                case PaletteContentStyle.LabelBoldControl:
                case PaletteContentStyle.LabelItalicControl:
                case PaletteContentStyle.LabelTitleControl:
                case PaletteContentStyle.LabelCustom1:
                case PaletteContentStyle.LabelCustom2:
                case PaletteContentStyle.LabelCustom3:
                case PaletteContentStyle.ContextMenuItemImage:
                case PaletteContentStyle.ContextMenuItemTextStandard:
                case PaletteContentStyle.ContextMenuItemTextAlternate:
                case PaletteContentStyle.ContextMenuItemShortcutText:
                    return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                case PaletteContentStyle.LabelToolTip:
                case PaletteContentStyle.LabelSuperTip:
                case PaletteContentStyle.LabelKeyTip:
                    return _toolTipText;
                case PaletteContentStyle.ContextMenuHeading:
                    return _ribbonColors[(int)SchemeOfficeColors.ContextMenuHeadingText];
                case PaletteContentStyle.TabHighProfile:
                case PaletteContentStyle.TabStandardProfile:
                case PaletteContentStyle.TabLowProfile:
                case PaletteContentStyle.TabOneNote:
                case PaletteContentStyle.TabDock:
                case PaletteContentStyle.TabCustom1:
                case PaletteContentStyle.TabCustom2:
                case PaletteContentStyle.TabCustom3:
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.TabDockAutoHidden:
                    return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                case PaletteContentStyle.ButtonLowProfile:
                case PaletteContentStyle.ButtonBreadCrumb:
                case PaletteContentStyle.ButtonListItem:
                case PaletteContentStyle.ButtonCommand:
                case PaletteContentStyle.ButtonButtonSpec:
                case PaletteContentStyle.ButtonCalendarDay:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            if (style == PaletteContentStyle.ButtonListItem)
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelControl];
                            else
                                return _ribbonColors[(int)SchemeOfficeColors.TextLabelPanel];
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonChecked];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
                    }
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.CheckedTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormTracking];
                        case PaletteState.Pressed:
                        case PaletteState.CheckedPressed:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormPressed];
                        default:
                            return _ribbonColors[(int)SchemeOfficeColors.TextButtonFormNormal];
                    }
                case PaletteContentStyle.ButtonInputControl:
                    if (state != PaletteState.Disabled)
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownNormal2];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.InputDropDownDisabled2];
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state != PaletteState.Normal)
                        return _ribbonColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                    else
                        return _ribbonColors[(int)SchemeOfficeColors.TextButtonNormal];
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderDockActive:
                    return _contentPaddingDock;
                case PaletteContentStyle.HeaderSecondary:
					return _contentPaddingHeader2;
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
                case PaletteContentStyle.ContextMenuItemImage:
                    return _contentPaddingContextMenuImage;
                case PaletteContentStyle.LabelToolTip:
                    return _contentPaddingToolTip;
                case PaletteContentStyle.LabelSuperTip:
                    return _contentPaddingSuperTip;
                case PaletteContentStyle.LabelKeyTip:
                    return _contentPaddingKeyTip;
                case PaletteContentStyle.ContextMenuHeading:
                    return _contentPaddingContextMenuHeading;
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
                case PaletteContentStyle.ButtonCalendarDay:
                    return _contentPaddingButtonInputControl;
                case PaletteContentStyle.ButtonButtonSpec:
                    return _contentPaddingButton3;
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    return _contentPaddingButton4;
                case PaletteContentStyle.ButtonForm:
                case PaletteContentStyle.ButtonFormClose:
                    return _contentPaddingButtonForm;
                case PaletteContentStyle.ButtonGallery:
                    return _contentPaddingButtonGallery;
                case PaletteContentStyle.ButtonListItem:
                    return _contentPaddingButtonListItem;
                case PaletteContentStyle.ButtonBreadCrumb:
                    return _contentPaddingButton6;
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
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
                case PaletteMetricBool.RibbonTabsSpareCaption:
                    return InheritBool.True;
                case PaletteMetricBool.TreeViewLines:
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
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                case PaletteMetricPadding.SeparatorPaddingLowProfile:
                case PaletteMetricPadding.SeparatorPaddingHighInternalProfile:
                case PaletteMetricPadding.SeparatorPaddingHighProfile:
                case PaletteMetricPadding.SeparatorPaddingCustom1:
                case PaletteMetricPadding.ContextMenuItemHighlight:
                case PaletteMetricPadding.ContextMenuItemsCollection:
                case PaletteMetricPadding.ContextMenuItemOuter:
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
                return _treeCollapseBlack;
            else
                return _treeExpandWhite;
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
            switch (checkState)
            {
                default:
                case CheckState.Unchecked:
                    if (!enabled)
                        return _checkBoxList.Images[0];
                    else if (pressed)
                        return _checkBoxList.Images[3];
                    else if (tracking)
                        return _checkBoxList.Images[2];
                    else
                        return _checkBoxList.Images[1];
                case CheckState.Checked:
                    if (!enabled)
                        return _checkBoxList.Images[4];
                    else if (pressed)
                        return _checkBoxList.Images[7];
                    else if (tracking)
                        return _checkBoxList.Images[6];
                    else
                        return _checkBoxList.Images[5];
                case CheckState.Indeterminate:
                    if (!enabled)
                        return _checkBoxList.Images[8];
                    else if (pressed)
                        return _checkBoxList.Images[11];
                    else if (tracking)
                        return _checkBoxList.Images[10];
                    else
                        return _checkBoxList.Images[9];
            }
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
            if (!checkState)
            {
                if (!enabled)
                    return _radioButtonArray[0];
                else if (pressed)
                    return _radioButtonArray[3];
                else if (tracking)
                    return _radioButtonArray[2];
                else
                    return _radioButtonArray[1];
            }
            else
            {
                if (!enabled)
                    return _radioButtonArray[4];
                else if (pressed)
                    return _radioButtonArray[7];
                else if (tracking)
                    return _radioButtonArray[6];
                else
                    return _radioButtonArray[5];
            }
        }

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            return _disabledDropDown;
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
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state)
        {         
            switch(button)
            {
                default:
                case PaletteRibbonGalleryButton.Down:
                    return _galleryButtonList.Images[0];
                case PaletteRibbonGalleryButton.Up:
                    return _galleryButtonList.Images[1];
                case PaletteRibbonGalleryButton.DropDown:
                    return _galleryButtonList.Images[2];
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.PendantClose:
                    return _buttonSpecPendantClose;
                case PaletteButtonSpecStyle.PendantMin:
                    return _buttonSpecPendantMin;
                case PaletteButtonSpecStyle.PendantRestore:
                    return _buttonSpecPendantRestore;
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                    return _buttonSpecWorkspaceMaximize;
                case PaletteButtonSpecStyle.WorkspaceRestore:
                    return _buttonSpecWorkspaceRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    return _buttonSpecRibbonMinimize;
                case PaletteButtonSpecStyle.RibbonExpand:
                    return _buttonSpecRibbonExpand;
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormClose:
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
                case PaletteButtonSpecStyle.FormRestore:
                case PaletteButtonSpecStyle.PendantClose:
                case PaletteButtonSpecStyle.PendantMin:
                case PaletteButtonSpecStyle.PendantRestore:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
                case PaletteButtonSpecStyle.FormMin:
                case PaletteButtonSpecStyle.FormMax:
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
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonContextTextFont(PaletteState state)
        {
            return _ribbonTabContextFont;
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
            return _ribbonColors[(int)SchemeOfficeColors.RibbonDropArrowLight];
        }

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDropArrowDark(PaletteState state)
        {
            return _ribbonColors[(int)SchemeOfficeColors.RibbonDropArrowDark];
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
            return _ribbonTabFont;
        }

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetRibbonTextHint(PaletteState state)
        {
            return PaletteTextHint.ClearTypeGridFit;
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
            switch(style)
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
                            return _disabledBack;
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
                            return _disabledBorder;
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
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                    return Color.Empty;
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
                        return Color.Empty;
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
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitle2];
                        case PaletteState.ContextNormal:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleContext2];
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupTitleTracking2];
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }
                    break;
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
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
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
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBack:
                case PaletteRibbonBackStyle.RibbonGroupNormalTitle:
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
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                    return _ribbonColors[(int)SchemeOfficeColors.RibbonGroupBorder5];
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
                case PaletteRibbonTextStyle.RibbonAppMenuDocsTitle:
                case PaletteRibbonTextStyle.RibbonAppMenuDocsEntry:
                    return _ribbonColors[(int)SchemeOfficeColors.AppButtonMenuDocsText];
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
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[1];
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return Color.Empty;
                        default:
                            return _trackBarColors[4];
                    }
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
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[2];
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.Light(_ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder]);
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBorder];
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return _buttonBorderColors[1];
                        case PaletteState.Pressed:
                            return _buttonBorderColors[3];
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
                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1]);
                        case PaletteState.Normal:
                            return ControlPaint.Light(_ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1]);
                        case PaletteState.Tracking:
                            return ControlPaint.Light(_buttonBackColors[2]);
                        case PaletteState.Pressed:
                            return ControlPaint.Light(_buttonBackColors[4]);
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

                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                    switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1]);
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1];
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return _buttonBackColors[2];
                        case PaletteState.Pressed:
                            return _buttonBackColors[4];
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

                    return _trackBarColors[0];
                case PaletteElement.TrackBarTrack:
                    if (CommonHelper.IsOverrideState(state))
                        return Color.Empty;

                    return _trackBarColors[3];
                case PaletteElement.TrackBarPosition:
                    if (CommonHelper.IsOverrideStateExclude(state, PaletteState.FocusOverride))
                        return Color.Empty;

                switch (state)
                    {
                        case PaletteState.Disabled:
                            return ControlPaint.LightLight(_ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack1]);
                        case PaletteState.Normal:
                            return _ribbonColors[(int)SchemeOfficeColors.ButtonNormalBack2];
                        case PaletteState.Tracking:
                        case PaletteState.FocusOverride:
                            return _buttonBackColors[3];
                        case PaletteState.Pressed:
                            return _buttonBackColors[5];
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

        #region Public
        /// <summary>
        /// Gets and sets the base font name used when defining fonts.
        /// </summary>
        public virtual string BaseFontName
        {
            get
            {
                if (string.IsNullOrEmpty(_baseFontName))
                    return "Segoe UI";
                else
                    return _baseFontName;
            }

            set
            {
                // Is there a change in value?
                if ((string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_baseFontName)) ||
                    (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(_baseFontName)))
                {
                    // Cache new value
                    _baseFontName = value;

                    // Update fonts to reflect change
                    DefineFonts();

                    // Use event to indicate palette has caused layout changes
                    OnPalettePaint(this, new PaletteLayoutEventArgs(true, false));
                }
            }
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
            if (_headerFormFont != null) _headerFormFont.Dispose();
            if (_header1LongFont != null) _header1LongFont.Dispose();
            if (_header2LongFont != null) _header2LongFont.Dispose();
            if (_buttonFont != null) _buttonFont.Dispose();
            if (_buttonFontNavigatorStack != null) _buttonFontNavigatorStack.Dispose();
            if (_buttonFontNavigatorMini != null) _buttonFontNavigatorMini.Dispose();
            if (_tabFontSelected != null) _tabFontSelected.Dispose();
            if (_tabFontNormal != null) _tabFontNormal.Dispose();
            if (_ribbonTabFont != null) _ribbonTabFont.Dispose();
            if (_ribbonTabContextFont != null) _ribbonTabContextFont.Dispose();
            if (_gridFont != null) _gridFont.Dispose();
            if (_calendarFont != null) _calendarFont.Dispose();
            if (_calendarBoldFont != null) _calendarBoldFont.Dispose();
            if (_superToolFont != null) _superToolFont.Dispose();
            if (_boldFont != null) _boldFont.Dispose();
            if (_italicFont != null) _italicFont.Dispose();

            float baseFontSize = BaseFontSize;
            string baseFontName = BaseFontName;
            _header1ShortFont = new Font(baseFontName, baseFontSize + 4.5f, FontStyle.Bold);
            _header2ShortFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _headerFormFont = new Font(baseFontName, SystemFonts.CaptionFont.SizeInPoints, FontStyle.Regular);
            _header1LongFont = new Font(baseFontName, baseFontSize + 1.5f, FontStyle.Regular);
            _header2LongFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _buttonFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _buttonFontNavigatorStack = new Font(_buttonFont, FontStyle.Bold);
            _buttonFontNavigatorMini = new Font(baseFontName, baseFontSize + 3.0f, FontStyle.Bold);
            _tabFontNormal = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _tabFontSelected = new Font(_tabFontNormal, FontStyle.Bold);
            _ribbonTabFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _ribbonTabContextFont = new Font(_ribbonTabFont, FontStyle.Bold);
            _gridFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _superToolFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _calendarFont = new Font(baseFontName, baseFontSize, FontStyle.Regular);
            _calendarBoldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _boldFont = new Font(baseFontName, baseFontSize, FontStyle.Bold);
            _italicFont = new Font(baseFontName, baseFontSize, FontStyle.Italic);
        }   
        #endregion

        #region ColorTable
        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        public override KryptonColorTable ColorTable
        {
            get 
            {
                if (_table == null)
                    _table = new KryptonColorTable2010(_ribbonColors, InheritBool.True, this);

                return _table;
            }
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

            // Update fonts to reflect any change in system settings
            DefineFonts();

            base.OnUserPreferenceChanged(sender, e);
        }
        #endregion
    }
}
