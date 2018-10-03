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
    /// Provides a fixed orange variantion on the sparkle appearance.
	/// </summary>
    public class PaletteSparkleOrange : PaletteSparkleBase
    {
		#region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly Image[] _radioButtonArray;

        private static readonly Color[] _appButtonNormal = new Color[] { Color.FromArgb(243, 245, 248), 
                                                                         Color.FromArgb(214, 220, 231), 
                                                                         Color.FromArgb(188, 198, 211), 
                                                                         Color.FromArgb(254, 254, 255), 
                                                                         Color.FromArgb(206, 213, 225) };
        
        private static readonly Color[] _appButtonTrack = new Color[] { Color.FromArgb(245, 239, 215), 
                                                                        Color.FromArgb(238, 214, 146), 
                                                                        Color.FromArgb(201, 155, 60), 
                                                                        Color.FromArgb(248, 201, 93), 
                                                                        Color.FromArgb(238, 168, 25) };
        
        private static readonly Color[] _appButtonPressed = new Color[] { Color.FromArgb(235, 227, 196), 
                                                                          Color.FromArgb(228, 198, 149), 
                                                                          Color.FromArgb(166, 97, 7), 
                                                                          Color.FromArgb(242, 155, 57), 
                                                                          Color.FromArgb(236, 136, 9) };
        
        private static readonly Color[] _ribbonGroupCollapsedBorderContextTracking = new Color[] { Color.FromArgb(128, 196, 184, 168), 
                                                                                                   Color.FromArgb(196, 184, 169), 
                                                                                                   Color.FromArgb(48, 255, 255, 255), 
                                                                                                   Color.FromArgb(220, 207, 192) };

        private static readonly Color[] _sparkleColors = new Color[] { Color.FromArgb(99, 108, 135),        // 0 _colorDark99
                                                                       Color.FromArgb(86, 94, 118),         // 1 _colorDark86
                                                                       Color.FromArgb(72, 81, 102),         // 2 _colorDark72
                                                                       Color.FromArgb(45, 45, 45),          // 3 _colorDark45
                                                                       Color.FromArgb(27, 31, 38),          // 4 _colorDark27
                                                                       Color.FromArgb(20, 21, 23),          // 5 _colorDark20
                                                                       Color.FromArgb(61, 37, 19),          // 6 _buttonTrackBack1
                                                                       Color.FromArgb(206, 129, 60),        // 7 _buttonTrackBack2
                                                                       Color.FromArgb(52, 30, 13),          // 8 _buttonPressBack1
                                                                       Color.FromArgb(248, 205, 125),       // 9 _buttonPressBack2
                                                                       Color.FromArgb(160, 66, 28),         // 10 _buttonCheckBack1
                                                                       Color.FromArgb(239, 198, 87),        // 11 _buttonCheckBack2
                                                                       Color.FromArgb(204, 65, 14),         // 12 _buttonCheckTrackBack1
                                                                       Color.FromArgb(255, 212, 112),       // 13 _buttonCheckTrackBack2
                                                                       Color.FromArgb(160, 65, 27),         // 14 _buttonCheckPressBack1
                                                                       Color.FromArgb(240, 153, 51),        // 15 _colorBlue
                                                                       Color.FromArgb(171, 91, 91),         // 16 _menuItemHeading
                                                                       Color.FromArgb(225, 244, 236, 225),  // 17 _menuItemTrackBack1
                                                                       Color.FromArgb(225, 231, 215, 181),  // 18 _menuItemTrackBack2
                                                                       Color.FromArgb(225, 230, 187, 91),   // 19 _menuItemTrackBorder
                                                                       Color.FromArgb(244, 239, 230),       // 20 _menuItemCheckedBack
                                                                       Color.FromArgb(230, 191, 185),       // 21 _menuItemCheckedBorder
                                                                       Color.FromArgb( 80,  80,  80),       // 22 _buttonBack2
                                                                       Color.FromArgb(250, 175, 57),        // 23 _buttonDefaultBack
                                                                       Color.FromArgb(242, 219, 177),       // 24 _gridHeaderTracking1
                                                                       Color.FromArgb(242, 218, 180),       // 25 _gridHeaderTracking2
                                                                       Color.FromArgb(228, 198, 145),       // 26 _gridHeaderPressed1
                                                                       Color.FromArgb(228, 197, 148),       // 27 _gridHeaderPressed2
                                                                       Color.FromArgb(190, 190, 190),       // 28 _gridCellBorder
                                                                       Color.FromArgb(239, 180, 79),        // 29 _tabCheckedNormal
                                                                       Color.FromArgb(146, 89, 48),         // 30 _ribbonFrameBorder1
                                                                       Color.FromArgb(196, 132, 85),        // 31 _ribbonFrameBorder1
                                                                       Color.FromArgb(235, 220, 209),       // 32 _ribbonFrameBack1
                                                                       Color.FromArgb(222, 211, 202),       // 33 _ribbonFrameBack2
                                                                       Color.FromArgb(222, 196, 176),       // 34 _ribbonFrameBack3
                                                                       Color.FromArgb(213, 120, 82),        // 35 _ribbonFrameBack4
                                                                       Color.FromArgb(213, 110, 72),        // 36 _contextCheckedTabFill
                                                                       Color.FromArgb(255, 20, 10),         // 37 _focusTabFill
                                                                     };

        private static readonly Color[] _ribbonColors = new Color[] { Color.FromArgb( 92,  83,  76),    // TextLabelControl
                                                                      Color.FromArgb( 70,  70,  70),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(133, 135, 137),    // ButtonNormalBorder1
                                                                      Color.FromArgb(123, 125, 127),    // ButtonNormalBorder2
                                                                      Color.FromArgb(223, 213, 203),    // ButtonNormalBack1
                                                                      Color.FromArgb(255, 255, 255),    // ButtonNormalBack2
                                                                      Color.FromArgb(198, 192, 187),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(231, 227, 224),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(214, 208, 204),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(236, 232, 229),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb( 83,  83,  83),    // PanelClient
                                                                      Color.FromArgb( 70,  70,  70),    // PanelAlternative
                                                                      Color.FromArgb( 30,  30,  30),    // ControlBorder
                                                                      Color.FromArgb(167, 167, 167),    // SeparatorHighBorder1
                                                                      Color.FromArgb(119, 119, 119),    // SeparatorHighBorder2
                                                                      Color.FromArgb(242, 241, 240),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(200, 193, 189),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(227, 224, 221),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(227, 224, 221),    // HeaderSecondaryBack2
                                                                      Color.Black,                      // HeaderText
                                                                      Color.White,                      // StatusStripText
                                                                      Color.FromArgb(167, 163, 155),    // ButtonBorder
                                                                      Color.FromArgb(200, 200, 200),    // SeparatorLight
                                                                      Color.FromArgb(118, 94, 86),      // SeparatorDark
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
                                                                      Color.FromArgb(255, 209, 174),    // FormHeaderShortActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderShortInactive
                                                                      Color.FromArgb(255, 255, 255),    // FormHeaderLongActive
                                                                      Color.FromArgb(225, 225, 225),    // FormHeaderLongInactive
                                                                      Color.FromArgb(104,  95, 88),     // FormButtonBorderTrack
                                                                      Color.FromArgb(123, 105, 91),     // FormButtonBack1Track
                                                                      Color.FromArgb(214, 199, 173),    // FormButtonBack2Track
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderPressed
                                                                      Color.FromArgb(  0,   0,   0),    // FormButtonBack1Pressed
                                                                      Color.FromArgb(102,  83, 65),     // FormButtonBack2Pressed
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
                                                                      Color.FromArgb(150, 156, 159),    // RibbonTabTracking1
                                                                      Color.FromArgb(255, 200, 200),    // RibbonTabTracking2
                                                                      Color.Black,                      // RibbonTabHighlight1
                                                                      Color.FromArgb(238, 145, 107),    // RibbonTabHighlight2
                                                                      Color.FromArgb(228, 135, 97),     // RibbonTabHighlight3
                                                                      Color.FromArgb(213, 120, 82),     // RibbonTabHighlight4
                                                                      Color.FromArgb(148, 137, 137),    // RibbonTabHighlight5
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
                                                                      Color.FromArgb(38, 31, 27),       // RibbonMinimizeBarDark
                                                                      Color.FromArgb(150, 150, 150),    // RibbonMinimizeBarLight
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorder2
                                                                      Color.FromArgb( 85,  78,  75),    // RibbonGroupCollapsedBorder3
                                                                      Color.FromArgb(145, 133, 129),    // RibbonGroupCollapsedBorder4
                                                                      Color.FromArgb(167, 167, 168),    // RibbonGroupCollapsedBack1
                                                                      Color.FromArgb( 95,  93,  93),    // RibbonGroupCollapsedBack2
                                                                      Color.FromArgb( 23,  21,  20),    // RibbonGroupCollapsedBack3
                                                                      Color.FromArgb( 92,  60,  52),    // RibbonGroupCollapsedBack4
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT1
                                                                      Color.Black,                      // RibbonGroupCollapsedBorderT2
                                                                      Color.FromArgb(146,  89, 48),     // RibbonGroupCollapsedBorderT3
                                                                      Color.FromArgb(196, 132, 85),     // RibbonGroupCollapsedBorderT4
                                                                      Color.FromArgb(182, 173, 166),    // RibbonGroupCollapsedBackT1
                                                                      Color.FromArgb(121, 105, 92),     // RibbonGroupCollapsedBackT2
                                                                      Color.FromArgb(61,  37,  19),     // RibbonGroupCollapsedBackT3
                                                                      Color.FromArgb(191, 119, 56),     // RibbonGroupCollapsedBackT4
                                                                      Color.Black,                      // RibbonGroupFrameBorder1
                                                                      Color.Gray,                       // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(225, 225, 225),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(170, 170, 170),    // RibbonGroupFrameInside2
                                                                      Color.FromArgb(150, 150, 150),    // RibbonGroupFrameInside3
                                                                      Color.FromArgb(205, 205, 205),    // RibbonGroupFrameInside4
                                                                      Color.White,                      // RibbonGroupCollapsedText         
                                                                      Color.FromArgb(172, 163, 158),    // AlternatePressedBack1
                                                                      Color.FromArgb(216, 215, 212),    // AlternatePressedBack2
                                                                      Color.FromArgb(125, 125, 124),    // AlternatePressedBorder1
                                                                      Color.FromArgb(186, 186, 186),    // AlternatePressedBorder2
                                                                      Color.FromArgb( 67,  55,  43),    // FormButtonBack1Checked
                                                                      Color.FromArgb(140, 122, 106),    // FormButtonBack2Checked
                                                                      Color.FromArgb( 18,  18,  18),    // FormButtonBorderCheck
                                                                      Color.FromArgb( 57,  45,  33),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(170, 152, 136),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(23, 21, 20),       // RibbonQATMini1
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
                                                                      Color.FromArgb(23, 21, 20),       // RibbonQATFullbar3                                                      
                                                                      Color.Black,                      // RibbonQATButtonDark                                                      
                                                                      Color.White,                      // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(240, 240, 240),    // RibbonQATOverflow1                                                      
                                                                      Color.Black,                      // RibbonQATOverflow2                                                      
                                                                      Color.Gray,                       // RibbonGroupSeparatorDark                                                      
                                                                      Color.Black,                      // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(219, 217, 210),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(223, 222, 214),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(191, 188, 179),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(159, 156, 145),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(235, 235, 235),    // NavigatorMiniBackColor                                                    
                                                                      Color.White,                      // GridListNormal1                                                    
                                                                      Color.FromArgb(219, 215, 212),    // GridListNormal2                                                    
                                                                      Color.FromArgb(218, 213, 210),    // GridListPressed1                                                    
                                                                      Color.FromArgb(253, 253, 252),    // GridListPressed2                                                    
                                                                      Color.FromArgb(194, 189, 186),    // GridListSelected                                                    
                                                                      Color.FromArgb(248, 248, 248),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(222, 222, 222),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(224, 224, 224),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(195, 195, 195),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(159, 217, 249),    // GridSheetColSelected1
                                                                      Color.FromArgb( 95, 193, 241),    // GridSheetColSelected2
                                                                      Color.FromArgb(237, 237, 237),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(196, 196, 196),    // GridSheetRowPressed
                                                                      Color.FromArgb(141, 213, 255),    // GridSheetRowSelected
                                                                      Color.FromArgb(209, 195, 188),    // GridDataCellBorder
                                                                      Color.FromArgb(230, 207, 184),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(153, 168, 172),    // InputControlTextDisabled
                                                                      Color.FromArgb(137, 137, 137),    // InputControlBorderNormal
                                                                      Color.FromArgb(204, 204, 204),    // InputControlBorderDisabled
                                                                      Color.White,                      // InputControlBackNormal
                                                                      SystemColors.Control,             // InputControlBackDisabled
                                                                      Color.FromArgb(232, 232, 232),    // InputControlBackInactive
                                                                      Color.FromArgb(124, 124, 124),    // InputDropDownNormal1
                                                                      Color.FromArgb(203, 248, 255),    // InputDropDownNormal2
                                                                      Color.FromArgb(153, 168, 172),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(235, 235, 235),    // ContextMenuHeading
                                                                      Color.FromArgb( 92,  83,  76),    // ContextMenuHeadingText
                                                                      Color.FromArgb(239, 239, 239),    // ContextMenuImageColumn
                                                                      Color.FromArgb(108, 108, 109),    // AppButtonBack1
                                                                      Color.FromArgb(103, 103, 104),    // AppButtonBack2
                                                                      Color.Black,                      // AppButtonBorder
                                                                      Color.FromArgb(23, 21, 20),       // AppButtonOuter1
                                                                      Color.Black,                      // AppButtonOuter2
                                                                      Color.FromArgb(23, 21, 20),       // AppButtonOuter3
                                                                      Color.FromArgb(23, 21, 20),       // AppButtonInner1
                                                                      Color.Black,                      // AppButtonInner2
                                                                      Color.FromArgb(242, 237, 237),    // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(242, 241, 240),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(206, 200, 195),    // SeparatorHighInternalBorder2
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
        static PaletteSparkleOrange()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(Properties.Resources.CBSparkleOrange);
            _radioButtonArray = new Image[]{Properties.Resources.RBSparkleD,
                                            Properties.Resources.RBSparkleN,
                                            Properties.Resources.RBSparkleOrangeT,
                                            Properties.Resources.RBSparkleOrangeP,
                                            Properties.Resources.RBSparkleDC,
                                            Properties.Resources.RBSparkleOrangeNC,
                                            Properties.Resources.RBSparkleOrangeTC,
                                            Properties.Resources.RBSparkleOrangePC};
        }

        /// <summary>
        /// Initialize a new instance of the PaletteSparkleOrange class.
        /// </summary>
        public PaletteSparkleOrange()
            : base(_ribbonColors, _sparkleColors,
                   _appButtonNormal, _appButtonTrack, _appButtonPressed,
                   _ribbonGroupCollapsedBorderContextTracking,
                   _checkBoxList, _radioButtonArray)
        {
        }
		#endregion
    }
}
