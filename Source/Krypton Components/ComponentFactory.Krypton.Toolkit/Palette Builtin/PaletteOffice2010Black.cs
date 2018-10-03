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
	/// Provides the Black color scheme variant of the Office 2010 palette.
	/// </summary>
    public class PaletteOffice2010Black : PaletteOffice2010Base
	{
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;
        private static readonly Image[] _radioButtonArray;
        private static readonly Image _blackDropDownButton = Properties.Resources._2010BlackDropDownButton;
        private static readonly Image _contextMenuSubMenu = Properties.Resources._2010BlackContextMenuSub;
        private static readonly Image _formCloseH = Properties.Resources._2010ButtonCloseH;
        private static readonly Image _formClose = Properties.Resources._2010ButtonCloseBlack;
        private static readonly Image _formMax = Properties.Resources._2010ButtonMaxBlack;
        private static readonly Image _formMin = Properties.Resources._2010ButtonMinBlack;
        private static readonly Image _formRestore = Properties.Resources._2010ButtonRestore;
        private static readonly Image _buttonSpecPendantClose = Properties.Resources._2010ButtonMDICloseBlack;
        private static readonly Image _buttonSpecPendantMin = Properties.Resources._2010ButtonMDIMinBlack;
        private static readonly Image _buttonSpecPendantRestore = Properties.Resources._2010ButtonMDIRestoreBlack;
        private static readonly Image _buttonSpecRibbonMinimize = Properties.Resources.RibbonUp2010Black;
        private static readonly Image _buttonSpecRibbonExpand = Properties.Resources.RibbonDown2010Black;
        private static readonly Color _disabledRibbonText = Color.FromArgb(205, 205, 205);
        private static readonly Color[] _trackBarColors = new Color[] { Color.FromArgb( 17,  17,  17),      // Tick marks
                                                                        Color.FromArgb( 37,  37,  37),      // Top track
                                                                        Color.FromArgb(174, 174, 174),      // Bottom track
                                                                        Color.FromArgb(131, 132, 132),      // Fill track
                                                                        Color.FromArgb(64, Color.White),    // Outside position
                                                                        Color.FromArgb(35, 35, 35)          // Border (normal) position
                                                                      };
        private static readonly Color[] _schemeColors = new Color[] { Color.FromArgb( 76,  83,  92),    // TextLabelControl
                                                                      Color.Black,                      // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(106, 106, 106),    // ButtonNormalBorder1
                                                                      Color.FromArgb( 94,  94,  94),    // ButtonNormalDefaultBorder
                                                                      Color.FromArgb(189, 189, 189),    // ButtonNormalBack1
                                                                      Color.FromArgb(169, 169, 169),    // ButtonNormalBack2
                                                                      Color.FromArgb(225, 225, 225),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(185, 185, 185),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb( 94,  94,  94),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb( 94,  94,  94),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb(113, 113, 113),    // PanelClient
                                                                      Color.FromArgb( 71,  71,  71),    // PanelAlternative
                                                                      Color.FromArgb( 46,  46,  46),    // ControlBorder
                                                                      Color.FromArgb(172, 172, 172),    // SeparatorHighBorder1
                                                                      Color.FromArgb(111, 111, 111),    // SeparatorHighBorder2
                                                                      Color.FromArgb(139, 139, 139),    // HeaderPrimaryBack1
                                                                      Color.FromArgb( 72,  72,  72),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(190, 190, 190),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(145, 145, 145),    // HeaderSecondaryBack2
                                                                      Color.Black,                      // HeaderText
                                                                      Color.FromArgb(226, 226, 226),    // StatusStripText
                                                                      Color.FromArgb(236, 199,  87),    // ButtonBorder
                                                                      Color.FromArgb( 89,  89,  89),    // SeparatorLight
                                                                      Color.Black,                      // SeparatorDark
                                                                      Color.FromArgb( 89,  89,  89),    // GripLight
                                                                      Color.FromArgb( 27,  27,  27),    // GripDark
                                                                      Color.FromArgb(113, 113, 113),    // ToolStripBack
                                                                      Color.FromArgb( 75,  75,  75),    // StatusStripLight
                                                                      Color.FromArgb( 50,  50,  50),    // StatusStripDark
                                                                      Color.White,                      // ImageMargin
                                                                      Color.FromArgb( 75,  75,  75),    // ToolStripBegin
                                                                      Color.FromArgb( 50,  50,  50),    // ToolStripMiddle
                                                                      Color.FromArgb( 50,  50,  50),    // ToolStripEnd
                                                                      Color.FromArgb( 44,  44,  44),    // OverflowBegin
                                                                      Color.FromArgb(167, 167, 167),    // OverflowMiddle
                                                                      Color.FromArgb( 44,  44,  44),    // OverflowEnd
                                                                      Color.FromArgb( 44,  44,  44),    // ToolStripBorder
                                                                      Color.FromArgb( 99,  99,  99),    // FormBorderActive
                                                                      Color.FromArgb(119, 119, 119),    // FormBorderInactive
                                                                      Color.FromArgb(113, 113, 113),    // FormBorderActiveLight
                                                                      Color.FromArgb(131, 131, 131),    // FormBorderActiveDark
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderInactiveLight
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderInactiveDark
                                                                      Color.FromArgb( 65,  65,  65),    // FormBorderHeaderActive
                                                                      Color.FromArgb(154, 154, 154),    // FormBorderHeaderInactive
                                                                      Color.FromArgb(121, 121, 121),    // FormBorderHeaderActive1
                                                                      Color.FromArgb(113, 113, 113),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb(226, 226, 226),    // FormHeaderShortActive
                                                                      Color.FromArgb(212, 212, 212),    // FormHeaderShortInactive
                                                                      Color.FromArgb(226, 226, 226),    // FormHeaderLongActive
                                                                      Color.FromArgb(212, 212, 212),    // FormHeaderLongInactive
                                                                      Color.FromArgb( 81,  81,  81),    // FormButtonBorderTrack
                                                                      Color.FromArgb(151, 151, 151),    // FormButtonBack1Track
                                                                      Color.FromArgb(116, 116, 116),    // FormButtonBack2Track
                                                                      Color.FromArgb( 81,  81,  81),    // FormButtonBorderPressed
                                                                      Color.FromArgb(113, 113, 113),    // FormButtonBack1Pressed
                                                                      Color.FromArgb( 93,  93,  93),    // FormButtonBack2Pressed
                                                                      Color.FromArgb( 70,  70,  70),    // TextButtonFormNormal
                                                                      Color.FromArgb(255, 255, 255),    // TextButtonFormTracking
                                                                      Color.FromArgb(255, 255, 255),    // TextButtonFormPressed
                                                                      Color.Blue,                       // LinkNotVisitedOverrideControl
                                                                      Color.Purple,                     // LinkVisitedOverrideControl
                                                                      Color.Red,                        // LinkPressedOverrideControl
                                                                      Color.FromArgb(180, 210, 255),    // LinkNotVisitedOverridePanel
                                                                      Color.Violet,                     // LinkVisitedOverridePanel
                                                                      Color.FromArgb(255,  90,  90),    // LinkPressedOverridePanel
                                                                      Color.White,                      // TextLabelPanel
                                                                      Color.FromArgb(226, 226, 226),    // RibbonTabTextNormal
                                                                      Color.Black,                      // RibbonTabTextChecked
                                                                      Color.FromArgb( 94,  94,  94),    // RibbonTabSelected1
                                                                      Color.FromArgb(201, 201, 201),    // RibbonTabSelected2
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabSelected3
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabSelected4
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabSelected5
                                                                      Color.FromArgb( 94,  94,  94),    // RibbonTabTracking1
                                                                      Color.FromArgb(183, 183, 183),    // RibbonTabTracking2
                                                                      Color.FromArgb( 94,  94,  94),    // RibbonTabHighlight1
                                                                      Color.FromArgb(201, 201, 201),    // RibbonTabHighlight2
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabHighlight3
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabHighlight4
                                                                      Color.FromArgb(192, 192, 192),    // RibbonTabHighlight5
                                                                      Color.FromArgb( 54,  54,  54),    // RibbonTabSeparatorColor
                                                                      Color.FromArgb( 94,  94,  94),    // RibbonGroupsArea1
                                                                      Color.FromArgb( 50,  50,  50),    // RibbonGroupsArea2
                                                                      Color.FromArgb(191, 191, 191),    // RibbonGroupsArea3
                                                                      Color.FromArgb(164, 164, 164),    // RibbonGroupsArea4
                                                                      Color.FromArgb(145, 145, 145),    // RibbonGroupsArea5
                                                                      Color.FromArgb(159, 159, 159),    // RibbonGroupBorder1
                                                                      Color.FromArgb(194, 194, 194),    // RibbonGroupBorder2
                                                                      Color.Empty,                      // RibbonGroupTitle1
                                                                      Color.Empty,                      // RibbonGroupTitle2
                                                                      Color.Empty,                      // RibbonGroupBorderContext1
                                                                      Color.Empty,                      // RibbonGroupBorderContext2
                                                                      Color.Empty,                      // RibbonGroupTitleContext1
                                                                      Color.Empty,                      // RibbonGroupTitleContext2
                                                                      Color.FromArgb( 92,  92,  94),    // RibbonGroupDialogDark
                                                                      Color.FromArgb(123, 125, 125),    // RibbonGroupDialogLight
                                                                      Color.Empty,                      // RibbonGroupTitleTracking1
                                                                      Color.Empty,                      // RibbonGroupTitleTracking2
                                                                      Color.FromArgb( 78,  78,  78),    // RibbonMinimizeBarDark
                                                                      Color.FromArgb(110, 110, 110),    // RibbonMinimizeBarLight
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorder1
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorder2
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorder3
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorder4
                                                                      Color.Empty,                      // RibbonGroupCollapsedBack1
                                                                      Color.Empty,                      // RibbonGroupCollapsedBack2
                                                                      Color.Empty,                      // RibbonGroupCollapsedBack3
                                                                      Color.Empty,                      // RibbonGroupCollapsedBack4
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorderT1
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorderT2
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorderT3
                                                                      Color.Empty,                      // RibbonGroupCollapsedBorderT4
                                                                      Color.Empty,                      // RibbonGroupCollapsedBackT1
                                                                      Color.Empty,                      // RibbonGroupCollapsedBackT2
                                                                      Color.Empty,                      // RibbonGroupCollapsedBackT3
                                                                      Color.Empty,                      // RibbonGroupCollapsedBackT4
                                                                      Color.FromArgb(147, 147, 147),    // RibbonGroupFrameBorder1
                                                                      Color.FromArgb(139, 139, 139),    // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(187, 187, 188),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(167, 167, 168),    // RibbonGroupFrameInside2
                                                                      Color.Empty,                      // RibbonGroupFrameInside3
                                                                      Color.Empty,                      // RibbonGroupFrameInside4
                                                                      Color.FromArgb( 59,  59,  59),    // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(158, 163, 172),    // AlternatePressedBack1
                                                                      Color.FromArgb(212, 215, 216),    // AlternatePressedBack2
                                                                      Color.FromArgb(124, 125, 125),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb( 43,  55,  67),    // FormButtonBack1Checked
                                                                      Color.FromArgb(106, 122, 140),    // FormButtonBack2Checked
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderCheck
                                                                      Color.FromArgb( 33,  45,  57),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(136, 152, 170),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb( 55,  55,  55),    // RibbonQATMini1
                                                                      Color.FromArgb(100, 100, 100),    // RibbonQATMini2
                                                                      Color.FromArgb( 73,  73,  73),    // RibbonQATMini3
                                                                      Color.FromArgb(12, Color.White),  // RibbonQATMini4
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5
                                                                      Color.FromArgb(100, 100, 100),    // RibbonQATMini1I
                                                                      Color.FromArgb(170, 170, 170),    // RibbonQATMini2I
                                                                      Color.FromArgb(140, 140, 140),    // RibbonQATMini3I
                                                                      Color.FromArgb(12, Color.White),  // RibbonQATMini4I
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5I
                                                                      Color.FromArgb(132, 132, 132),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(121, 121, 121),    // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb( 50,  49,  49),    // RibbonQATFullbar3                                                      
                                                                      Color.FromArgb( 90,  90,  90),    // RibbonQATButtonDark                                                      
                                                                      Color.FromArgb(174, 174, 175),    // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(161, 161, 161),    // RibbonQATOverflow1                                                      
                                                                      Color.FromArgb( 68,  68,  68),    // RibbonQATOverflow2                                                      
                                                                      Color.FromArgb( 82,  82,  82),    // RibbonGroupSeparatorDark                                                      
                                                                      Color.FromArgb(190, 190, 190),    // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(210, 217, 219),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(214, 222, 223),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(179, 188, 191),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(145, 156, 159),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(235, 235, 235),    // NavigatorMiniBackColor                                                    
                                                                      Color.FromArgb(205, 205, 205),    // GridListNormal1                                                    
                                                                      Color.FromArgb(166, 166, 166),    // GridListNormal2                                                    
                                                                      Color.FromArgb(166, 166, 166),    // GridListPressed1                                                    
                                                                      Color.FromArgb(205, 205, 205),    // GridListPressed2                                                    
                                                                      Color.FromArgb(150, 150, 150),    // GridListSelected                                                    
                                                                      Color.FromArgb(220, 220, 220),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(200, 200, 200),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(255, 252, 230),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(255, 211,  89),    // GridSheetColSelected1
                                                                      Color.FromArgb(255, 239, 113),    // GridSheetColSelected2
                                                                      Color.FromArgb(205, 205, 205),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetRowPressed
                                                                      Color.FromArgb(245, 210,  87),    // GridSheetRowSelected
                                                                      Color.FromArgb(218, 220, 221),    // GridDataCellBorder
                                                                      Color.FromArgb(183, 219, 255),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(168, 168, 168),    // InputControlTextDisabled
                                                                      Color.FromArgb(132, 132, 132),    // InputControlBorderNormal
                                                                      Color.FromArgb(187, 187, 187),    // InputControlBorderDisabled
                                                                      Color.FromArgb(255, 255, 255),    // InputControlBackNormal
                                                                      Color.FromArgb(240, 240, 240),    // InputControlBackDisabled
                                                                      Color.FromArgb(192, 192, 192),    // InputControlBackInactive
                                                                      Color.Black,                      // InputDropDownNormal1
                                                                      Color.Transparent,                // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(240, 242, 245),    // ContextMenuHeadingBack
                                                                      Color.Black,                      // ContextMenuHeadingText
                                                                      Color.White,                      // ContextMenuImageColumn
                                                                      Color.FromArgb( 70,  70,  70),    // AppButtonBack1
                                                                      Color.FromArgb( 70,  70,  70),    // AppButtonBack2
                                                                      Color.FromArgb( 50,  50,  50),    // AppButtonBorder
                                                                      Color.FromArgb( 70,  70,  70),    // AppButtonOuter1
                                                                      Color.FromArgb( 70,  70,  70),    // AppButtonOuter2
                                                                      Color.FromArgb( 70,  70,  70),    // AppButtonOuter3
                                                                      Color.Empty,                      // AppButtonInner1
                                                                      Color.FromArgb( 50,  50,  50),    // AppButtonInner2
                                                                      Color.White,                      // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(172, 172, 172),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(111, 111, 111),    // SeparatorHighInternalBorder2
                                                                      Color.FromArgb(132, 132, 132),    // RibbonGalleryBorder
                                                                      Color.FromArgb(187, 187, 187),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(193, 193, 193),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(176, 176, 176),    // RibbonGalleryBack1
                                                                      Color.FromArgb(150, 150, 150),    // RibbonGalleryBack2
                                                                      Color.FromArgb(148, 149, 151),    // RibbonTabTracking3
                                                                      Color.FromArgb(127, 127, 127),    // RibbonTabTracking4
                                                                      Color.FromArgb( 82,  82,  82),    // RibbonGroupBorder3
                                                                      Color.FromArgb(176, 176, 176),    // RibbonGroupBorder4
                                                                      Color.FromArgb(178, 178, 178),    // RibbonGroupBorder5
                                                                      Color.FromArgb( 36,  36,  36),    // RibbonGroupTitleText
                                                                      Color.FromArgb(155, 157, 160),    // RibbonDropArrowLight
                                                                      Color.FromArgb( 27,  29,  40),    // RibbonDropArrowDark
                                                                      Color.FromArgb(137, 137, 137),    // HeaderDockInactiveBack1
                                                                      Color.FromArgb(125, 125, 125),    // HeaderDockInactiveBack2
                                                                      Color.FromArgb( 46,  46,  46),    // ButtonNavigatorBorder
                                                                      Color.White,                      // ButtonNavigatorText
                                                                      Color.FromArgb( 76,  76,  76),    // ButtonNavigatorTrack1
                                                                      Color.FromArgb(147, 147, 143),    // ButtonNavigatorTrack2
                                                                      Color.FromArgb( 66,  66,  66),    // ButtonNavigatorPressed1
                                                                      Color.FromArgb(148, 148, 143),    // ButtonNavigatorPressed2
                                                                      Color.FromArgb( 91,  91,  91),    // ButtonNavigatorChecked1
                                                                      Color.FromArgb( 73,  73,  73),    // ButtonNavigatorChecked2
                                                                      Color.FromArgb(201, 201, 201),    // ToolTipBottom                                                                      
        };        
        #endregion

		#region Identity
        static PaletteOffice2010Black()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(Properties.Resources.CB2010Black);
            _galleryButtonList = new ImageList();
            _galleryButtonList.ImageSize = new Size(13, 7);
            _galleryButtonList.ColorDepth = ColorDepth.Depth24Bit;
            _galleryButtonList.TransparentColor = Color.Magenta;
            _galleryButtonList.Images.AddStrip(Properties.Resources.Gallery2010);
            _radioButtonArray = new Image[]{Properties.Resources.RB2010BlueD,
                                            Properties.Resources.RB2010SilverN,
                                            Properties.Resources.RB2010BlueT,
                                            Properties.Resources.RB2010BlueP,
                                            Properties.Resources.RB2010BlueDC,
                                            Properties.Resources.RB2010SilverNC,
                                            Properties.Resources.RB2010SilverTC,
                                            Properties.Resources.RB2010SilverPC};
        }

		/// <summary>
        /// Initialize a new instance of the PaletteOffice2010Black class.
		/// </summary>
        public PaletteOffice2010Black()
            : base(_schemeColors, 
                   _checkBoxList, 
                   _galleryButtonList, 
                   _radioButtonArray,
                   _trackBarColors)
		{
		}
		#endregion

        #region Back
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
                case PaletteBackStyle.ButtonNavigatorStack:
                case PaletteBackStyle.ButtonNavigatorOverflow:
                case PaletteBackStyle.ButtonNavigatorMini:
                    switch (state)
                    {
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                            return PaletteColorStyle.ExpertSquareHighlight2;
                    }
                    break;
            }

            return base.GetBackColorStyle(style, state);
        }

        /// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBackStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeColors[(int)SchemeOfficeColors.HeaderSecondaryBack1];
                    }
                    break;
            }

            return base.GetBackColor2(style, state);
        }
        #endregion

        #region Border
		/// <summary>
		/// Gets the first border color.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeColors[(int)SchemeOfficeColors.ControlBorder];
                    }
                    break;
            }

            return base.GetBorderColor1(style, state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteBorderStyle.TabDock:
                    switch (state)
                    {
                        case PaletteState.Normal:
                            return _schemeColors[(int)SchemeOfficeColors.ControlBorder];
                    }
                    break;
            }

            return base.GetBorderColor2(style, state);
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            switch (style)
            {
                case PaletteContentStyle.ButtonStandalone:
                case PaletteContentStyle.ButtonGallery:
                case PaletteContentStyle.ButtonAlternate:
                case PaletteContentStyle.ButtonCluster:
                case PaletteContentStyle.ButtonCustom1:
                case PaletteContentStyle.ButtonCustom2:
                case PaletteContentStyle.ButtonCustom3:
                    if (state == PaletteState.NormalDefaultOverride)
                        return _schemeColors[(int)SchemeOfficeColors.TextButtonChecked];
                    break;
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state == PaletteState.NormalDefaultOverride)
                        return _schemeColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _schemeColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderCalendar:
                    if (state != PaletteState.Disabled)
                        return Color.White;
                    break;
            }
        
            return base.GetContentShortTextColor1(style, state);
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state == PaletteState.NormalDefaultOverride)
                        return _schemeColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _schemeColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderCalendar:
                    if (state != PaletteState.Disabled)
                        return Color.White;
                    break;
            }
        
            return base.GetContentShortTextColor2(style, state);
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state == PaletteState.NormalDefaultOverride)
                        return _schemeColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _schemeColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderCalendar:
                    if (state != PaletteState.Disabled)
                        return Color.White;
                    break;
            }

            return base.GetContentLongTextColor1(style, state);
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
                case PaletteContentStyle.ButtonNavigatorMini:
                case PaletteContentStyle.ButtonNavigatorStack:
                case PaletteContentStyle.ButtonNavigatorOverflow:
                    if (state == PaletteState.NormalDefaultOverride)
                        return _schemeColors[(int)SchemeOfficeColors.TextButtonChecked];
                    else
                        return _schemeColors[(int)SchemeOfficeColors.ButtonNavigatorText];
                case PaletteContentStyle.HeaderPrimary:
                case PaletteContentStyle.HeaderDockInactive:
                case PaletteContentStyle.HeaderCalendar:
                    if (state != PaletteState.Disabled)
                        return Color.White;
                    break;
            }

            return base.GetContentLongTextColor2(style, state);
        }
        #endregion

        #region Images
        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        public override Image GetDropDownButtonImage(PaletteState state)
        {
            if (state != PaletteState.Disabled)
                return _blackDropDownButton;
            else
                return base.GetDropDownButtonImage(state);
        }

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        public override Image GetContextMenuSubMenuImage()
        {
            return _contextMenuSubMenu;
        }
        #endregion

        #region ButtonSpec
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
                case PaletteButtonSpecStyle.PendantClose:
                    return _buttonSpecPendantClose;
                case PaletteButtonSpecStyle.PendantMin:
                    return _buttonSpecPendantMin;
                case PaletteButtonSpecStyle.PendantRestore:
                    return _buttonSpecPendantRestore;
                case PaletteButtonSpecStyle.FormClose:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                            return _formCloseH;
                        default:
                            return _formClose;
                    }
                case PaletteButtonSpecStyle.FormMin:
                    return _formMin;
                case PaletteButtonSpecStyle.FormMax:
                    return _formMax;
                case PaletteButtonSpecStyle.FormRestore:
                    return _formRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    return _buttonSpecRibbonMinimize;
                case PaletteButtonSpecStyle.RibbonExpand:
                    return _buttonSpecRibbonExpand;
                default:
                    return base.GetButtonSpecImage(style, state);
            }
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
                case PaletteRibbonTextStyle.RibbonGroupNormalTitle:
                    if (state == PaletteState.Disabled)
                        return _disabledRibbonText;
                        break;
                case PaletteRibbonTextStyle.RibbonGroupButtonText:
                case PaletteRibbonTextStyle.RibbonGroupLabelText:
                case PaletteRibbonTextStyle.RibbonGroupCheckBoxText:
                case PaletteRibbonTextStyle.RibbonGroupRadioButtonText:
                    if (state == PaletteState.Disabled)
                        return _disabledRibbonText;
                    break;
            }

            return base.GetRibbonTextColor(style, state);
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
                case PaletteRibbonBackStyle.RibbonTab:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.Pressed:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonTabTracking2010Alt;
                        case PaletteState.CheckedNormal:
                        case PaletteState.CheckedTracking:
                        case PaletteState.CheckedPressed:
                        case PaletteState.ContextCheckedNormal:
                        case PaletteState.ContextCheckedTracking:
                            return PaletteRibbonColorStyle.RibbonTabSelected2010Alt;
                    }
                    break;
                case PaletteRibbonBackStyle.RibbonGroupCollapsedBorder:
                case PaletteRibbonBackStyle.RibbonGroupNormalBorder:
                    switch (state)
                    {
                        case PaletteState.Tracking:
                        case PaletteState.ContextTracking:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepTrackingDark;
                        case PaletteState.Pressed:
                            return PaletteRibbonColorStyle.RibbonGroupNormalBorderSepPressedDark;
                    }
                    break;
            }

            return base.GetRibbonBackColorStyle(style, state);
        }
        #endregion
    }
}
