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
	/// Provides a fixed purple variantion on the sparkle appearance.
	/// </summary>
	public class PaletteSparklePurple : PaletteSparkleBase
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly Image[] _radioButtonArray;

        private static readonly Color[] _appButtonNormal = new Color[] { Color.FromArgb(243, 245, 248), 
                                                                         Color.FromArgb(214, 220, 231), 
                                                                         Color.FromArgb(188, 198, 211), 
                                                                         Color.FromArgb(254, 254, 255), 
                                                                         Color.FromArgb(206, 213, 225) };

        private static readonly Color[] _appButtonTrack = new Color[] { Color.FromArgb(239, 215, 245), 
                                                                        Color.FromArgb(214, 146, 238), 
                                                                        Color.FromArgb(155, 60, 201), 
                                                                        Color.FromArgb(201, 93, 248), 
                                                                        Color.FromArgb(168, 25, 238) };

        private static readonly Color[] _appButtonPressed = new Color[] { Color.FromArgb(227, 196, 235), 
                                                                          Color.FromArgb(198, 149, 228), 
                                                                          Color.FromArgb(97, 7, 166), 
                                                                          Color.FromArgb(155, 57, 242), 
                                                                          Color.FromArgb(136, 9, 236) };

        private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking = new Color[] { Color.FromArgb(128, 184, 168, 196), 
                                                                                                   Color.FromArgb(184, 168, 196), 
                                                                                                   Color.FromArgb(48, 255, 255, 255), 
                                                                                                   Color.FromArgb(207, 192, 220) };

        private static readonly Color[] _sparkleColors = new Color[] { Color.FromArgb(99, 108, 135),        // 0 _colorDark99
                                                                       Color.FromArgb(86, 94, 118),         // 1 _colorDark86
                                                                       Color.FromArgb(72, 81, 102),         // 2 _colorDark72
                                                                       Color.FromArgb(45, 45, 45),          // 3 _colorDark45
                                                                       Color.FromArgb(27, 31, 38),          // 4 _colorDark27
                                                                       Color.FromArgb(20, 21, 23),          // 5 _colorDark20
                                                                       Color.FromArgb(37, 19, 61),          // 6 _buttonTrackBack1
                                                                       Color.FromArgb(129, 60, 206),        // 7 _buttonTrackBack2
                                                                       Color.FromArgb(30, 13, 52),          // 8 _buttonPressBack1
                                                                       Color.FromArgb(205, 125, 248),       // 9 _buttonPressBack2
                                                                       Color.FromArgb(66, 28, 160),         // 10 _buttonCheckBack1
                                                                       Color.FromArgb(198, 87, 239),        // 11 _buttonCheckBack2
                                                                       Color.FromArgb(65, 14, 204),         // 12 _buttonCheckTrackBack1
                                                                       Color.FromArgb(212, 112, 255),       // 13 _buttonCheckTrackBack2
                                                                       Color.FromArgb(65, 27, 160),         // 14 _buttonCheckPressBack1
                                                                       Color.FromArgb(153, 51, 255),        // 15 _colorBlue
                                                                       Color.FromArgb(89, 29, 131),         // 16 _menuItemHeading
                                                                       Color.FromArgb(164, 236, 225, 244),  // 17 _menuItemTrackBack1
                                                                       Color.FromArgb(164, 215, 181, 231),  // 18 _menuItemTrackBack2
                                                                       Color.FromArgb(164, 187, 91, 230),   // 19 _menuItemTrackBorder
                                                                       Color.FromArgb(239, 230, 244),       // 20 _menuItemCheckedBack
                                                                       Color.FromArgb(196, 190, 230),       // 21 _menuItemCheckedBorder
                                                                       Color.FromArgb(57, 66, 102),         // 22 _buttonBack2
                                                                       Color.FromArgb(175, 57, 250),        // 23 _buttonDefaultBack
                                                                       Color.FromArgb(219, 177, 242),       // 24 _gridHeaderTracking1
                                                                       Color.FromArgb(218, 180, 242),       // 25 _gridHeaderTracking2
                                                                       Color.FromArgb(198, 145, 228),       // 26 _gridHeaderPressed1
                                                                       Color.FromArgb(197, 148, 228),       // 27 _gridHeaderPressed2
                                                                       Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
                                                                       Color.FromArgb(180, 79, 239),        // 29 _tabCheckedNormal
                                                                       Color.FromArgb(89, 48, 146),         // 30 _ribbonFrameBorder1
                                                                       Color.FromArgb(132, 85, 196),        // 31 _ribbonFrameBorder1
                                                                       Color.FromArgb(220, 209, 235),       // 32 _ribbonFrameBack1
                                                                       Color.FromArgb(211, 202, 222),       // 33 _ribbonFrameBack2
                                                                       Color.FromArgb(196, 176, 222),       // 34 _ribbonFrameBack3
                                                                       Color.FromArgb(120, 82, 213),        // 35 _ribbonFrameBack3
                                                                       Color.FromArgb(110, 72, 213),        // 36 _contextCheckedTabFill
                                                                       Color.FromArgb(20, 10, 255),         // 37 _focusTabFill
                                                                     };

        private static readonly Color[] _ribbonColors = new Color[] { Color.FromArgb( 76,  83,  92),    // TextLabelControl
                                                                      Color.FromArgb( 70,  70,  70),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(137, 135, 133),    // ButtonNormalBorder1
                                                                      Color.FromArgb(127, 125, 123),    // ButtonNormalBorder2
                                                                      Color.FromArgb(203, 213, 223),    // ButtonNormalBack1
                                                                      Color.FromArgb(255, 255, 255),    // ButtonNormalBack2
                                                                      Color.FromArgb(192, 187, 198),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(227, 224, 231),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(208, 204, 214),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(232, 229, 236),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb( 83,  83,  83),    // PanelClient
                                                                      Color.FromArgb( 70,  70,  70),    // PanelAlternative
                                                                      Color.FromArgb( 30,  30,  30),    // ControlBorder
                                                                      Color.FromArgb(167, 167, 167),    // SeparatorHighBorder1
                                                                      Color.FromArgb(119, 119, 119),    // SeparatorHighBorder2
                                                                      Color.FromArgb(240, 241, 242),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(189, 193, 200),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(221, 224, 227),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(221, 224, 227),    // HeaderSecondaryBack2
                                                                      Color.Black,                      // HeaderText
                                                                      Color.White,                      // StatusStripText
                                                                      Color.FromArgb(155, 163, 167),    // ButtonBorder
                                                                      Color.FromArgb(200, 200, 200),    // SeparatorLight
                                                                      Color.FromArgb(86, 94, 118),      // SeparatorDark
                                                                      Color.FromArgb(190, 190, 190),    // GripLight
                                                                      Color.Black,                      // GripDark
                                                                      Color.FromArgb(99, 108, 135),     // ToolStripBack
                                                                      Color.FromArgb(99, 108, 135),     // StatusStripLight
                                                                      Color.Black,                      // StatusStripDark
                                                                      Color.FromArgb(240, 240, 240),    // ImageMargin
                                                                      Color.FromArgb(200, 200, 200),    // ToolStripBegin
                                                                      Color.FromArgb(99, 108, 135),     // ToolStripMiddle
                                                                      Color.FromArgb(72, 81, 102),      // ToolStripEnd
                                                                      Color.FromArgb(72, 81, 102),      // OverflowBegin
                                                                      Color.FromArgb(72, 81, 102),      // OverflowMiddle
                                                                      Color.FromArgb( 30,  30,  30),    // OverflowEnd
                                                                      Color.FromArgb( 30,  30,  30),    // ToolStripBorder
                                                                      Color.FromArgb( 47,  47,  47),    // FormBorderActive
                                                                      Color.FromArgb(146, 146, 146),    // FormBorderInactive
                                                                      Color.FromArgb( 77,  77,  77),    // FormBorderActiveLight
                                                                      Color.FromArgb(102, 102, 102),    // FormBorderActiveDark
                                                                      Color.FromArgb(153, 153, 153),    // FormBorderInactiveLight
                                                                      Color.FromArgb(171, 171, 171),    // FormBorderInactiveDark
                                                                      Color.FromArgb( 65,  65,  65),    // FormBorderHeaderActive
                                                                      Color.FromArgb(154, 154, 154),    // FormBorderHeaderInactive
                                                                      Color.FromArgb( 42,  43,  43),    // FormBorderHeaderActive1
                                                                      Color.FromArgb( 74,  74,  74),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(146, 146, 146),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(158, 158, 158),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb(209, 174, 255),    // FormHeaderShortActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderShortInactive
                                                                      Color.FromArgb(255, 255, 255),    // FormHeaderLongActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderLongInactive
                                                                      Color.FromArgb(95,  88,  104),    // FormButtonBorderTrack
                                                                      Color.FromArgb(105,  91, 123),    // FormButtonBack1Track
                                                                      Color.FromArgb(199, 173, 214),    // FormButtonBack2Track
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderPressed
                                                                      Color.FromArgb(  0,   0,   0),    // FormButtonBack1Pressed
                                                                      Color.FromArgb( 83,  65, 102),    // FormButtonBack2Pressed
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
                                                                      Color.White,                      // RibbonTabTextNormal
                                                                      Color.Black,                      // RibbonTabTextChecked
                                                                      Color.Black,                      // RibbonTabSelected1
                                                                      Color.Silver,                     // RibbonTabSelected2
                                                                      Color.FromArgb(177, 177, 188),    // RibbonTabSelected3
                                                                      Color.FromArgb(167, 167, 178),    // RibbonTabSelected4
                                                                      Color.FromArgb(137, 137, 148),    // RibbonTabSelected5
                                                                      Color.FromArgb(159, 156, 150),    // RibbonTabTracking1
                                                                      Color.FromArgb(200, 200, 255),    // RibbonTabTracking2
                                                                      Color.Black,                      // RibbonTabHighlight1
                                                                      Color.FromArgb(145, 107, 238),    // RibbonTabHighlight2
                                                                      Color.FromArgb(135,  97, 228),    // RibbonTabHighlight3
                                                                      Color.FromArgb(120,  82, 213),    // RibbonTabHighlight4
                                                                      Color.FromArgb(137, 137, 148),    // RibbonTabHighlight5
                                                                      Color.Black,                      // RibbonTabSeparatorColor
                                                                      Color.Black,                      // RibbonGroupsArea1
                                                                      Color.Black,                      // RibbonGroupsArea2
                                                                      Color.FromArgb( 96,  96, 110),    // RibbonGroupsArea3
                                                                      Color.FromArgb(140, 140, 150),    // RibbonGroupsArea4
                                                                      Color.FromArgb(140, 140, 150),    // RibbonGroupsArea5
                                                                      Color.Black,                      // RibbonGroupBorder1
                                                                      Color.Black,                      // RibbonGroupBorder2
                                                                      Color.DimGray,                    // RibbonGroupTitle1
                                                                      Color.Black,                      // RibbonGroupTitle2
                                                                      Color.Black,                      // RibbonGroupBorderContext1
                                                                      Color.Black,                      // RibbonGroupBorderContext2
                                                                      Color.DimGray,                    // RibbonGroupTitleContext1
                                                                      Color.Black,                      // RibbonGroupTitleContext2
                                                                      Color.Black,                      // RibbonGroupDialogDark
                                                                      Color.White,                      // RibbonGroupDialogLight
                                                                      Color.FromArgb(120, 120, 120),    // RibbonGroupTitleTracking1
                                                                      Color.FromArgb(65, 65, 65),       // RibbonGroupTitleTracking2
                                                                      Color.FromArgb(27, 31, 38),       // RibbonMinimizeBarDark
                                                                      Color.FromArgb(150, 150, 150),    // RibbonMinimizeBarLight
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder2
                                                                      Color.FromArgb( 75,  78,  85),    // RibbonGroupCollapsedBorder3
                                                                      Color.FromArgb(133, 129, 145),    // RibbonGroupCollapsedBorder4
                                                                      Color.FromArgb(167, 167, 168),    // RibbonGroupCollapsedBack1
                                                                      Color.FromArgb( 93,  93,  95),    // RibbonGroupCollapsedBack2
                                                                      Color.FromArgb( 21,  20,  23),    // RibbonGroupCollapsedBack3
                                                                      Color.FromArgb( 60,  52,  92),    // RibbonGroupCollapsedBack4
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT2
                                                                      Color.FromArgb(  89, 48, 146),    // RibbonGroupCollapsedBorderT3
                                                                      Color.FromArgb( 132,  85,196),    // RibbonGroupCollapsedBorderT4
                                                                      Color.FromArgb( 173, 166,182),    // RibbonGroupCollapsedBackT1
                                                                      Color.FromArgb( 105,  92,121),    // RibbonGroupCollapsedBackT2
                                                                      Color.FromArgb(  37,  19, 61),    // RibbonGroupCollapsedBackT3
                                                                      Color.FromArgb( 119,  56,191),    // RibbonGroupCollapsedBackT4
                                                                      Color.Black,                      // RibbonGroupFrameBorder1
                                                                      Color.Gray,                       // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(225, 225, 225),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(170, 170, 170),    // RibbonGroupFrameInside2
                                                                      Color.FromArgb(150, 150, 150),    // RibbonGroupFrameInside3
                                                                      Color.FromArgb(205, 205, 205),    // RibbonGroupFrameInside4
                                                                      Color.White,                      // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(163, 158, 172),    // AlternatePressedBack1
                                                                      Color.FromArgb(215, 212, 216),    // AlternatePressedBack2
                                                                      Color.FromArgb(125, 124, 125),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb(  55,  43, 67),    // FormButtonBack1Checked
                                                                      Color.FromArgb( 122, 106,140),    // FormButtonBack2Checked
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderCheck
                                                                      Color.FromArgb(  45, 33,  57),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb( 152, 136,170),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(20, 21, 23),       // RibbonQATMini1
                                                                      Color.FromArgb(150, 150, 150),    // RibbonQATMini2
                                                                      Color.FromArgb(45, 45, 45),       // RibbonQATMini3
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini4
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5
                                                                      Color.Black,                      // RibbonQATMini1I
                                                                      Color.Black,                      // RibbonQATMini2I
                                                                      Color.Black,                      // RibbonQATMini3I
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini4I
                                                                      Color.FromArgb(14, Color.White),  // RibbonQATMini5I
                                                                      Color.FromArgb(150, 150, 150),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(45, 45, 45),       // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb(20, 21, 23),       // RibbonQATFullbar3                                                      
                                                                      Color.Black,                      // RibbonQATButtonDark                                                      
                                                                      Color.White,                      // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(240, 240, 240),    // RibbonQATOverflow1                                                      
                                                                      Color.Black,                      // RibbonQATOverflow2                                                      
                                                                      Color.Gray,                       // RibbonGroupSeparatorDark                                                      
                                                                      Color.Black,                      // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(210, 217, 219),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(214, 222, 223),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(179, 188, 191),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(145, 156, 159),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(235, 235, 235),    // NavigatorMiniBackColor                                                    
                                                                      Color.White,                      // GridListNormal1                                                    
                                                                      Color.FromArgb(212, 215, 219),    // GridListNormal2                                                    
                                                                      Color.FromArgb(213, 210, 218),    // GridListPressed1                                                    
                                                                      Color.FromArgb( 253,252, 253),    // GridListPressed2                                                    
                                                                      Color.FromArgb(186, 189, 194),    // GridListSelected                                                    
                                                                      Color.FromArgb(248, 248, 248),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(222, 222, 222),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(224, 224, 224),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(195, 195, 195),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(217, 249, 159),    // GridSheetColSelected1
                                                                      Color.FromArgb(193, 241,  95),    // GridSheetColSelected2
                                                                      Color.FromArgb(237, 237, 237),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(196, 196, 196),    // GridSheetRowPressed
                                                                      Color.FromArgb( 213,255, 141),    // GridSheetRowSelected
                                                                      Color.FromArgb(195,188,  209),    // GridDataCellBorder
                                                                      Color.FromArgb(217,194,  240),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(172, 168, 153),    // InputControlTextDisabled
                                                                      Color.FromArgb(137, 137, 137),    // InputControlBorderNormal
                                                                      Color.FromArgb(204, 204, 204),    // InputControlBorderDisabled
                                                                      Color.White,                      // InputControlBackNormal
                                                                      SystemColors.Control,             // InputControlBackDisabled
                                                                      Color.FromArgb(232, 232, 232),    // InputControlBackInactive
                                                                      Color.FromArgb(124, 124, 124),    // InputDropDownNormal1
                                                                      Color.FromArgb(255, 248, 203),    // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(235, 235, 235),    // ContextMenuHeading
                                                                      Color.FromArgb( 76,  83,  92),    // ContextMenuHeadingText
                                                                      Color.FromArgb(239, 239, 239),    // ContextMenuImageColumn
                                                                      Color.FromArgb(109, 108, 108),    // AppButtonBack1
                                                                      Color.FromArgb(104, 103, 103),    // AppButtonBack2
                                                                      Color.Black,                      // AppButtonBorder
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonOuter1
                                                                      Color.Black,                      // AppButtonOuter2
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonOuter3
                                                                      Color.FromArgb(20, 21, 23),       // AppButtonInner1
                                                                      Color.Black,                      // AppButtonInner2
                                                                      Color.FromArgb(237, 237, 242),    // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(240, 241, 242),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(195, 200, 206),    // SeparatorHighInternalBorder2
                                                                      Color.Black,                      // RibbonGalleryBorder
                                                                      Color.FromArgb(215, 215, 215),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(238, 238, 238),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(195, 200, 209),    // RibbonGalleryBack1
                                                                      Color.FromArgb(217, 220, 224),    // RibbonGalleryBack2
                                                                      Color.Empty,                      // RibbonTabTracking3
                                                                      Color.Empty,                      // RibbonTabTracking4
                                                                      Color.Empty,                      // RibbonGroupBorder3
                                                                      Color.Empty,                      // RibbonGroupBorder4
                                                                    };
        #endregion
        
		#region Identity
        static PaletteSparklePurple()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(Properties.Resources.CBSparklePurple);
            _radioButtonArray = new Image[]{Properties.Resources.RBSparkleD,
                                            Properties.Resources.RBSparkleN,
                                            Properties.Resources.RBSparklePurpleT,
                                            Properties.Resources.RBSparklePurpleP,
                                            Properties.Resources.RBSparkleDC,
                                            Properties.Resources.RBSparklePurpleNC,
                                            Properties.Resources.RBSparklePurpleTC,
                                            Properties.Resources.RBSparklePurplePC};
        }

        /// <summary>
        /// Initialize a new instance of the PaletteSparkleRed class.
        /// </summary>
        public PaletteSparklePurple()
            : base(_ribbonColors, _sparkleColors,
                   _appButtonNormal, _appButtonTrack, _appButtonPressed,
                   _ribbonGroupCollapsedBorderContextTracking,
                   _checkBoxList, _radioButtonArray)
        {
        }
		#endregion
    }
}
