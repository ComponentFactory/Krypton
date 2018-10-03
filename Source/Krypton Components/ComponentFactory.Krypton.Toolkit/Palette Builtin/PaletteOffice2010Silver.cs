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
	/// Provides the Silver color scheme variant of the Office 2010 palette.
	/// </summary>
    public class PaletteOffice2010Silver : PaletteOffice2010Base
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;
        private static readonly Image[] _radioButtonArray;
        private static readonly Image _silverDropDownButton = Properties.Resources._2010BlueDropDownButton;
        private static readonly Image _contextMenuSubMenu = Properties.Resources._2010BlueContextMenuSub;
        private static readonly Image _formCloseH = Properties.Resources._2010ButtonCloseH;
        private static readonly Image _formClose = Properties.Resources._2010ButtonClose;
        private static readonly Image _formMax = Properties.Resources._2010ButtonMax;
        private static readonly Image _formMin = Properties.Resources._2010ButtonMin;
        private static readonly Image _formRestore = Properties.Resources._2010ButtonRestore;
        private static readonly Color[] _trackBarColors = new Color[] { Color.FromArgb(170, 170, 170),      // Tick marks
                                                                        Color.FromArgb(166, 170, 175),      // Top track
                                                                        Color.FromArgb(226, 220, 235),      // Bottom track
                                                                        Color.FromArgb(206, 200, 215),      // Fill track
                                                                        Color.FromArgb(64, Color.White),    // Outside position
                                                                        Color.FromArgb(80, 81, 82)          // Border (normal) position
                                                                      };
        private static readonly Color[] _schemeColors = new Color[] { Color.FromArgb( 59,  59,  59),    // TextLabelControl
                                                                      Color.FromArgb( 59,  59,  59),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(187, 191, 196),    // ButtonNormalBorder1
                                                                      Color.FromArgb(158, 166, 172),    // ButtonNormalDefaultBorder
                                                                      Color.FromArgb(247, 250, 252),    // ButtonNormalBack1
                                                                      Color.FromArgb(231, 234, 238),    // ButtonNormalBack2
                                                                      Color.FromArgb(235, 235, 235),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(195, 195, 195),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(207, 212, 218),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(207, 212, 218),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb(227, 230, 232),    // PanelClient
                                                                      Color.FromArgb(207, 212, 218),    // PanelAlternative
                                                                      Color.FromArgb(161, 169, 179),    // ControlBorder
                                                                      Color.FromArgb(250, 253, 255),    // SeparatorHighBorder1
                                                                      Color.FromArgb(227, 232, 237),    // SeparatorHighBorder2
                                                                      Color.FromArgb(233, 237, 241),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(207, 212, 218),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(255, 255, 255),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(234, 237, 241),    // HeaderSecondaryBack2
                                                                      Color.FromArgb( 59,  59,  59),    // HeaderText
                                                                      Color.FromArgb( 59,  59,  59),    // StatusStripText
                                                                      Color.FromArgb(236, 199,  87),    // ButtonBorder
                                                                      Color.FromArgb(247, 250, 252),    // SeparatorLight
                                                                      Color.FromArgb(119, 123, 127),    // SeparatorDark
                                                                      Color.FromArgb(255, 255, 255),    // GripLight
                                                                      Color.FromArgb(181, 190, 199),    // GripDark
                                                                      Color.FromArgb(227, 230, 232),    // ToolStripBack
                                                                      Color.FromArgb(230, 234, 238),    // StatusStripLight
                                                                      Color.FromArgb(183, 188, 193),    // StatusStripDark
                                                                      Color.White,                      // ImageMargin
                                                                      Color.FromArgb(230, 234, 238),    // ToolStripBegin
                                                                      Color.FromArgb(183, 188, 193),    // ToolStripMiddle
                                                                      Color.FromArgb(183, 188, 193),    // ToolStripEnd
                                                                      Color.FromArgb(147, 154, 163),    // OverflowBegin
                                                                      Color.FromArgb(147, 154, 163),    // OverflowMiddle
                                                                      Color.FromArgb(147, 154, 163),    // OverflowEnd
                                                                      Color.FromArgb(147, 154, 163),    // ToolStripBorder
                                                                      Color.FromArgb(101, 109, 117),    // FormBorderActive
                                                                      Color.FromArgb(134, 139, 145),    // FormBorderInactive
                                                                      Color.FromArgb(228, 230, 232),    // FormBorderActiveLight
                                                                      Color.FromArgb(255, 255, 255),    // FormBorderActiveDark
                                                                      Color.FromArgb(248, 247, 247),    // FormBorderInactiveLight
                                                                      Color.FromArgb(248, 247, 247),    // FormBorderInactiveDark
                                                                      Color.FromArgb(101, 109, 117),    // FormBorderHeaderActive
                                                                      Color.FromArgb(134, 139, 145),    // FormBorderHeaderInactive
                                                                      Color.FromArgb(235, 237, 240),    // FormBorderHeaderActive1
                                                                      Color.FromArgb(228, 230, 232),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(248, 247, 247),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(248, 247, 247),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb( 59,  59,  59),    // FormHeaderShortActive
                                                                      Color.FromArgb(138, 138, 138),    // FormHeaderShortInactive
                                                                      Color.FromArgb( 59,  59,  59),    // FormHeaderLongActive
                                                                      Color.FromArgb(138, 138, 138),    // FormHeaderLongInactive
                                                                      Color.FromArgb(166, 172, 179),    // FormButtonBorderTrack
                                                                      Color.FromArgb(255, 255, 255),    // FormButtonBack1Track
                                                                      Color.FromArgb(228, 228, 229),    // FormButtonBack2Track
                                                                      Color.FromArgb(166, 172, 179),    // FormButtonBorderPressed
                                                                      Color.FromArgb(223, 228, 235),    // FormButtonBack1Pressed
                                                                      Color.FromArgb(188, 193, 200),    // FormButtonBack2Pressed
                                                                      Color.Black,                      // TextButtonFormNormal
                                                                      Color.Black,                      // TextButtonFormTracking
                                                                      Color.Black,                      // TextButtonFormPressed
                                                                      Color.Blue,                       // LinkNotVisitedOverrideControl
                                                                      Color.Purple,                     // LinkVisitedOverrideControl
                                                                      Color.Red,                        // LinkPressedOverrideControl
                                                                      Color.Blue,                       // LinkNotVisitedOverridePanel
                                                                      Color.Purple,                     // LinkVisitedOverridePanel
                                                                      Color.Red,                        // LinkPressedOverridePanel
                                                                      Color.FromArgb( 59,  59,  59),    // TextLabelPanel
                                                                      Color.FromArgb( 59,  59,  59),    // RibbonTabTextNormal
                                                                      Color.FromArgb( 76,  83,  92),    // RibbonTabTextChecked
                                                                      Color.FromArgb(182, 186, 191),    // RibbonTabSelected1
                                                                      Color.White,                      // RibbonTabSelected2
                                                                      Color.White,                      // RibbonTabSelected3
                                                                      Color.White,                      // RibbonTabSelected4
                                                                      Color.White,                      // RibbonTabSelected5
                                                                      Color.FromArgb(177, 181, 186),    // RibbonTabTracking1
                                                                      Color.FromArgb(248, 249, 249),    // RibbonTabTracking2
                                                                      Color.FromArgb(182, 186, 191),    // RibbonTabHighlight1
                                                                      Color.White,                      // RibbonTabHighlight2
                                                                      Color.White,                      // RibbonTabHighlight3
                                                                      Color.White,                      // RibbonTabHighlight4
                                                                      Color.White,                      // RibbonTabHighlight5
                                                                      Color.FromArgb(182, 186, 191),    // RibbonTabSeparatorColor
                                                                      Color.FromArgb(182, 186, 191),    // RibbonGroupsArea1
                                                                      Color.FromArgb(135, 140, 146),    // RibbonGroupsArea2
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGroupsArea3
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGroupsArea4
                                                                      Color.FromArgb(229, 233, 238),    // RibbonGroupsArea5
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGroupBorder1
                                                                      Color.FromArgb(253, 253, 253),    // RibbonGroupBorder2
                                                                      Color.Empty,                      // RibbonGroupTitle1
                                                                      Color.Empty,                      // RibbonGroupTitle2
                                                                      Color.Empty,                      // RibbonGroupBorderContext1
                                                                      Color.Empty,                      // RibbonGroupBorderContext2
                                                                      Color.Empty,                      // RibbonGroupTitleContext1
                                                                      Color.Empty,                      // RibbonGroupTitleContext2
                                                                      Color.FromArgb(148, 149, 152),    // RibbonGroupDialogDark
                                                                      Color.FromArgb(180, 182, 183),    // RibbonGroupDialogLight
                                                                      Color.Empty,                      // RibbonGroupTitleTracking1
                                                                      Color.Empty,                      // RibbonGroupTitleTracking2
                                                                      Color.FromArgb(139, 144, 151),    // RibbonMinimizeBarDark
                                                                      Color.FromArgb(205, 209, 214),    // RibbonMinimizeBarLight
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
                                                                      Color.FromArgb(242, 244, 247),    // RibbonGroupCollapsedBackT2
                                                                      Color.FromArgb(238, 241, 245),    // RibbonGroupCollapsedBackT3
                                                                      Color.FromArgb(234, 235, 235),    // RibbonGroupCollapsedBackT4
                                                                      Color.FromArgb(208, 212, 217),    // RibbonGroupFrameBorder1
                                                                      Color.FromArgb(208, 212, 217),    // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(254, 254, 254),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(254, 254, 254),    // RibbonGroupFrameInside2
                                                                      Color.Empty,                      // RibbonGroupFrameInside3
                                                                      Color.Empty,                      // RibbonGroupFrameInside4
                                                                      Color.FromArgb( 59,  59,  59),    // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(179, 185, 195),    // AlternatePressedBack1
                                                                      Color.FromArgb(216, 224, 224),    // AlternatePressedBack2
                                                                      Color.FromArgb(125, 125, 125),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb(157, 166, 174),    // FormButtonBack1Checked
                                                                      Color.FromArgb(222, 230, 242),    // FormButtonBack2Checked
                                                                      Color.FromArgb(149, 154, 160),    // FormButtonBorderCheck
                                                                      Color.FromArgb(147, 156, 164),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(237, 245, 250),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(180, 180, 180),    // RibbonQATMini1
                                                                      Color.FromArgb(210, 215, 221),    // RibbonQATMini2
                                                                      Color.FromArgb(195, 200, 206),    // RibbonQATMini3
                                                                      Color.FromArgb(10, Color.White),  // RibbonQATMini4
                                                                      Color.FromArgb(32, Color.White),  // RibbonQATMini5                                                       
                                                                      Color.FromArgb(200, 200, 200),    // RibbonQATMini1I
                                                                      Color.FromArgb(233, 234, 238),    // RibbonQATMini2I
                                                                      Color.FromArgb(223, 224, 228),    // RibbonQATMini3I
                                                                      Color.FromArgb(10, Color.White),  // RibbonQATMini4I
                                                                      Color.FromArgb(32, Color.White),  // RibbonQATMini5I                                                       
                                                                      Color.FromArgb(223, 227, 234),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(213, 217, 222),    // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb(135, 140, 146),    // RibbonQATFullbar3                                                      
                                                                      Color.FromArgb( 90,  90,  90),    // RibbonQATButtonDark                                                      
                                                                      Color.FromArgb(210, 212, 215),    // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(233, 237, 241),    // RibbonQATOverflow1                                                      
                                                                      Color.FromArgb(138, 144, 150),    // RibbonQATOverflow2                                                      
                                                                      Color.FromArgb(191, 195, 199),    // RibbonGroupSeparatorDark                                                      
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(231, 234, 238),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(241, 243, 243),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(197, 198, 199),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(157, 158, 159),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(238, 238, 244),    // NavigatorMiniBackColor                                                    
                                                                      Color.FromArgb(248, 252, 255),    // GridListNormal1                                                    
                                                                      Color.FromArgb(223, 227, 232),    // GridListNormal2                                                    
                                                                      Color.FromArgb(203, 207, 212),    // GridListPressed1                                                    
                                                                      Color.White,                      // GridListPressed2                                                    
                                                                      Color.FromArgb(186, 189, 194),    // GridListSelected                                                    
                                                                      Color.FromArgb(238, 241, 247),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(218, 222, 227),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(255, 252, 230),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(255, 211,  89),    // GridSheetColSelected1
                                                                      Color.FromArgb(255, 239, 113),    // GridSheetColSelected2
                                                                      Color.FromArgb(223, 227, 232),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetRowPressed
                                                                      Color.FromArgb(245, 210,  87),    // GridSheetRowSelected
                                                                      Color.FromArgb(218, 220, 221),    // GridDataCellBorder
                                                                      Color.FromArgb(183, 219, 255),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(168, 168, 168),    // InputControlTextDisabled
                                                                      Color.FromArgb(212, 214, 217),    // InputControlBorderNormal
                                                                      Color.FromArgb(187, 187, 187),    // InputControlBorderDisabled
                                                                      Color.FromArgb(255, 255, 255),    // InputControlBackNormal
                                                                      Color.FromArgb(240, 240, 240),    // InputControlBackDisabled
                                                                      Color.FromArgb(247, 247, 247),    // InputControlBackInactive
                                                                      Color.Black,                      // InputDropDownNormal1
                                                                      Color.Transparent,                // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(240, 242, 245),    // ContextMenuHeadingBack
                                                                      Color.FromArgb( 59,  59,  59),    // ContextMenuHeadingText
                                                                      Color.White,                      // ContextMenuImageColumn
                                                                      Color.FromArgb(224, 227, 231),    // AppButtonBack1
                                                                      Color.FromArgb(224, 227, 231),    // AppButtonBack2
                                                                      Color.FromArgb(135, 140, 146),    // AppButtonBorder
                                                                      Color.FromArgb(224, 227, 231),    // AppButtonOuter1
                                                                      Color.FromArgb(224, 227, 231),    // AppButtonOuter2
                                                                      Color.FromArgb(224, 227, 231),    // AppButtonOuter3
                                                                      Color.Empty,                      // AppButtonInner1
                                                                      Color.FromArgb(135, 140, 146),    // AppButtonInner2
                                                                      Color.White,                      // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(250, 253, 255),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(227, 232, 237),    // SeparatorHighInternalBorder2
                                                                      Color.FromArgb(198, 202, 205),    // RibbonGalleryBorder
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(255, 255, 255),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(250, 250, 250),    // RibbonGalleryBack1
                                                                      Color.FromArgb(228, 231, 235),    // RibbonGalleryBack2                                                                                                                                      Color.FromArgb(177, 181, 186),    // RibbonTabTracking1
                                                                      Color.FromArgb(229, 231, 235),    // RibbonTabTracking3
                                                                      Color.FromArgb(231, 233, 235),    // RibbonTabTracking4
                                                                      Color.FromArgb(176, 182, 188),    // RibbonGroupBorder3
                                                                      Color.FromArgb(246, 247, 248),    // RibbonGroupBorder4
                                                                      Color.FromArgb(249, 250, 250),    // RibbonGroupBorder5
                                                                      Color.FromArgb(102, 109, 124),    // RibbonGroupTitleText
                                                                      Color.FromArgb(151, 156, 163),    // RibbonDropArrowLight
                                                                      Color.FromArgb( 39,  49,  60),    // RibbonDropArrowDark
                                                                      Color.FromArgb(237, 242, 248),    // HeaderDockInactiveBack1
                                                                      Color.FromArgb(207, 213, 220),    // HeaderDockInactiveBack2
                                                                      Color.FromArgb(161, 169, 179),    // ButtonNavigatorBorder
                                                                      Color.Black,                      // ButtonNavigatorText
                                                                      Color.FromArgb(207, 213, 220),    // ButtonNavigatorTrack1
                                                                      Color.FromArgb(232, 234, 238),    // ButtonNavigatorTrack2
                                                                      Color.FromArgb(191, 196, 202),    // ButtonNavigatorPressed1
                                                                      Color.FromArgb(225, 226, 230),    // ButtonNavigatorPressed2
                                                                      Color.FromArgb(222, 227, 234),    // ButtonNavigatorChecked1
                                                                      Color.FromArgb(206, 214, 221),    // ButtonNavigatorChecked2
                                                                      Color.FromArgb(221, 221, 221),    // ToolTipBottom                                                                      
        };
        #endregion

        #region Identity
        static PaletteOffice2010Silver()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(Properties.Resources.CB2010Silver);
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
        /// Initialize a new instance of the PaletteOffice2010Silver class.
		/// </summary>
        public PaletteOffice2010Silver()
            : base(_schemeColors, 
                   _checkBoxList, 
                   _galleryButtonList, 
                   _radioButtonArray,
                   _trackBarColors)
        {
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
                return _silverDropDownButton;
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
                default:
                    return base.GetButtonSpecImage(style, state);
            }
        }
        #endregion
    }
}
