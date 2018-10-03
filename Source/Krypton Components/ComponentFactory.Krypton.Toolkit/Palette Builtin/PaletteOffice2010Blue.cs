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
	/// Provides the Blue color scheme variant of the Office 2010 palette.
	/// </summary>
    public class PaletteOffice2010Blue : PaletteOffice2010Base
    {
        #region Static Fields
        private static readonly ImageList _checkBoxList;
        private static readonly ImageList _galleryButtonList;
        private static readonly Image[] _radioButtonArray;
        private static readonly Image _blueDropDownButton = Properties.Resources._2010BlueDropDownButton;
        private static readonly Image _contextMenuSubMenu = Properties.Resources._2010BlueContextMenuSub;
        private static readonly Image _formCloseH = Properties.Resources._2010ButtonCloseH;
        private static readonly Image _formClose = Properties.Resources._2010ButtonClose;
        private static readonly Image _formMax = Properties.Resources._2010ButtonMax;
        private static readonly Image _formMin = Properties.Resources._2010ButtonMin;
        private static readonly Image _formRestore = Properties.Resources._2010ButtonRestore;
        private static readonly Color[] _trackBarColors = new Color[] { Color.FromArgb(116, 150, 194),      // Tick marks
                                                                        Color.FromArgb(116, 150, 194),      // Top track
                                                                        Color.FromArgb(152, 190, 241),      // Bottom track
                                                                        Color.FromArgb(142, 180, 231),      // Fill track
                                                                        Color.FromArgb(64, Color.White),    // Outside position
                                                                        Color.FromArgb(63, 101, 152)        // Border (normal) position
                                                                      };
        private static readonly Color[] _schemeColors = new Color[] { Color.FromArgb( 30,  57,  91),    // TextLabelControl
                                                                      Color.FromArgb( 30,  57,  91),    // TextButtonNormal
                                                                      Color.Black,                      // TextButtonChecked
                                                                      Color.FromArgb(171, 186, 208),    // ButtonNormalBorder
                                                                      Color.FromArgb(117, 144, 175),    // ButtonNormalDefaultBorder
                                                                      Color.FromArgb(225, 237, 250),    // ButtonNormalBack1
                                                                      Color.FromArgb(208, 223, 238),    // ButtonNormalBack2
                                                                      Color.FromArgb(255, 255, 255),    // ButtonNormalDefaultBack1
                                                                      Color.FromArgb(210, 229, 250),    // ButtonNormalDefaultBack2
                                                                      Color.FromArgb(174, 194, 219),    // ButtonNormalNavigatorBack1
                                                                      Color.FromArgb(174, 194, 219),    // ButtonNormalNavigatorBack2
                                                                      Color.FromArgb(187, 206, 230),    // PanelClient
                                                                      Color.FromArgb(174, 194, 219),    // PanelAlternative
                                                                      Color.FromArgb(133, 158, 191),    // ControlBorder
                                                                      Color.FromArgb(239, 245, 255),    // SeparatorHighBorder1
                                                                      Color.FromArgb(200, 217, 239),    // SeparatorHighBorder2
                                                                      Color.FromArgb(207, 221, 238),    // HeaderPrimaryBack1
                                                                      Color.FromArgb(174, 194, 219),    // HeaderPrimaryBack2
                                                                      Color.FromArgb(239, 246, 253),    // HeaderSecondaryBack1
                                                                      Color.FromArgb(216, 228, 242),    // HeaderSecondaryBack2
                                                                      Color.FromArgb( 30,  57,  91),    // HeaderText
                                                                      Color.FromArgb( 30,  57,  91),    // StatusStripText
                                                                      Color.FromArgb(236, 199,  87),    // ButtonBorder
                                                                      Color.FromArgb(245, 249, 255),    // SeparatorLight
                                                                      Color.FromArgb(120, 141, 165),    // SeparatorDark
                                                                      Color.FromArgb(212, 225, 241),    // GripLight
                                                                      Color.FromArgb(132, 157, 189),    // GripDark
                                                                      Color.FromArgb(187, 206, 230),    // ToolStripBack
                                                                      Color.FromArgb(220, 232, 246),    // StatusStripLight
                                                                      Color.FromArgb(179, 196, 216),    // StatusStripDark
                                                                      Color.White,                      // ImageMargin
                                                                      Color.FromArgb(220, 232, 246),    // ToolStripBegin
                                                                      Color.FromArgb(179, 196, 216),    // ToolStripMiddle
                                                                      Color.FromArgb(179, 196, 216),    // ToolStripEnd
                                                                      Color.FromArgb(132, 157, 189),    // OverflowBegin
                                                                      Color.FromArgb(132, 157, 189),    // OverflowMiddle
                                                                      Color.FromArgb(132, 157, 189),    // OverflowEnd
                                                                      Color.FromArgb(132, 157, 189),    // ToolStripBorder
                                                                      Color.FromArgb(144, 154, 166),    // FormBorderActive
                                                                      Color.FromArgb(162, 173, 185),    // FormBorderInactive
                                                                      Color.FromArgb(187, 206, 230),    // FormBorderActiveLight
                                                                      Color.FromArgb(212, 230, 245),    // FormBorderActiveDark
                                                                      Color.FromArgb(223, 235, 247),    // FormBorderInactiveLight
                                                                      Color.FromArgb(223, 235, 247),    // FormBorderInactiveDark
                                                                      Color.FromArgb(144, 154, 166),    // FormBorderHeaderActive
                                                                      Color.FromArgb(162, 173, 185),    // FormBorderHeaderInactive
                                                                      Color.FromArgb(193, 212, 236),    // FormBorderHeaderActive1
                                                                      Color.FromArgb(187, 206, 230),    // FormBorderHeaderActive2
                                                                      Color.FromArgb(223, 235, 247),    // FormBorderHeaderInctive1
                                                                      Color.FromArgb(223, 235, 247),    // FormBorderHeaderInctive2
                                                                      Color.FromArgb( 30,  57,  91),    // FormHeaderShortActive
                                                                      Color.FromArgb(106, 128, 168),    // FormHeaderShortInactive
                                                                      Color.FromArgb( 30,  57,  91),    // FormHeaderLongActive
                                                                      Color.FromArgb(106, 128, 168),    // FormHeaderLongInactive
                                                                      Color.FromArgb(143, 165, 191),    // FormButtonBorderTrack
                                                                      Color.FromArgb(214, 234, 255),    // FormButtonBack1Track
                                                                      Color.FromArgb(188, 207, 231),    // FormButtonBack2Track
                                                                      Color.FromArgb(143, 165, 191),    // FormButtonBorderPressed
                                                                      Color.FromArgb(187, 206, 230),    // FormButtonBack1Pressed
                                                                      Color.FromArgb(166, 182, 213),    // FormButtonBack2Pressed
                                                                      Color.FromArgb( 21,  66, 139),    // TextButtonFormNormal
                                                                      Color.FromArgb( 21,  66, 139),    // TextButtonFormTracking
                                                                      Color.FromArgb( 21,  66, 139),    // TextButtonFormPressed
                                                                      Color.Blue,                       // LinkNotVisitedOverrideControl
                                                                      Color.Purple,                     // LinkVisitedOverrideControl
                                                                      Color.Red,                        // LinkPressedOverrideControl
                                                                      Color.Blue,                       // LinkNotVisitedOverridePanel
                                                                      Color.Purple,                     // LinkVisitedOverridePanel
                                                                      Color.Red,                        // LinkPressedOverridePanel
                                                                      Color.FromArgb( 30,  57,  91),    // TextLabelPanel
                                                                      Color.FromArgb( 30,  57,  91),    // RibbonTabTextNormal
                                                                      Color.FromArgb( 30,  57,  91),    // RibbonTabTextChecked
                                                                      Color.FromArgb(159, 178, 199),    // RibbonTabSelected1
                                                                      Color.FromArgb(245, 250, 255),    // RibbonTabSelected2
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabSelected3
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabSelected4
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabSelected5
                                                                      Color.FromArgb(159, 178, 199),    // RibbonTabTracking1
                                                                      Color.FromArgb(237, 241, 247),    // RibbonTabTracking2
                                                                      Color.FromArgb(159, 178, 199),    // RibbonTabHighlight1
                                                                      Color.FromArgb(245, 250, 255),    // RibbonTabHighlight2
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabHighlight3
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabHighlight4
                                                                      Color.FromArgb(239, 246, 253),    // RibbonTabHighlight5
                                                                      Color.FromArgb(182, 186, 191),    // RibbonTabSeparatorColor
                                                                      Color.FromArgb(159, 178, 199),    // RibbonGroupsArea1
                                                                      Color.FromArgb(114, 142, 173),    // RibbonGroupsArea2
                                                                      Color.FromArgb(239, 246, 253),    // RibbonGroupsArea3
                                                                      Color.FromArgb(221, 234, 247),    // RibbonGroupsArea4
                                                                      Color.FromArgb(216, 228, 242),    // RibbonGroupsArea5
                                                                      Color.FromArgb(235, 240, 246),    // RibbonGroupBorder1
                                                                      Color.FromArgb(240, 246, 252),    // RibbonGroupBorder2
                                                                      Color.Empty,                      // RibbonGroupTitle1
                                                                      Color.Empty,                      // RibbonGroupTitle2
                                                                      Color.Empty,                      // RibbonGroupBorderContext1
                                                                      Color.Empty,                      // RibbonGroupBorderContext2
                                                                      Color.Empty,                      // RibbonGroupTitleContext1
                                                                      Color.Empty,                      // RibbonGroupTitleContext2
                                                                      Color.FromArgb(135, 142, 152),    // RibbonGroupDialogDark
                                                                      Color.FromArgb(165, 174, 183),    // RibbonGroupDialogLight
                                                                      Color.Empty,                      // RibbonGroupTitleTracking1
                                                                      Color.Empty,                      // RibbonGroupTitleTracking2
                                                                      Color.FromArgb(139, 160, 188),    // RibbonMinimizeBarDark
                                                                      Color.FromArgb(198, 218, 240),    // RibbonMinimizeBarLight
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
                                                                      Color.FromArgb(189, 203, 218),    // RibbonGroupFrameBorder1
                                                                      Color.FromArgb(184, 199, 216),    // RibbonGroupFrameBorder2
                                                                      Color.FromArgb(233, 241, 250),    // RibbonGroupFrameInside1
                                                                      Color.FromArgb(222, 233, 246),    // RibbonGroupFrameInside2
                                                                      Color.Empty,                      // RibbonGroupFrameInside3
                                                                      Color.Empty,                      // RibbonGroupFrameInside4
                                                                      Color.FromArgb( 30,  57,  91),    // RibbonGroupCollapsedText
                                                                      Color.FromArgb(118, 153, 200),    // AlternatePressedBack1
                                                                      Color.FromArgb(184, 215, 253),    // AlternatePressedBack2
                                                                      Color.FromArgb(135, 156, 175),    // AlternatePressedBorder1
                                                                      Color.FromArgb(177, 198, 216),    // AlternatePressedBorder2
                                                                      Color.FromArgb(150, 194, 239),    // FormButtonBack1Checked
                                                                      Color.FromArgb(210, 228, 254),    // FormButtonBack2Checked
                                                                      Color.FromArgb(158, 193, 241),    // FormButtonBorderCheck
                                                                      Color.FromArgb(140, 184, 229),    // FormButtonBack1CheckTrack
                                                                      Color.FromArgb(225, 241, 255),    // FormButtonBack2CheckTrack
                                                                      Color.FromArgb(154, 179, 213),    // RibbonQATMini1
                                                                      Color.FromArgb(219, 231, 247),    // RibbonQATMini2
                                                                      Color.FromArgb(195, 213, 236),    // RibbonQATMini3
                                                                      Color.FromArgb(128, Color.White), // RibbonQATMini4
                                                                      Color.FromArgb(72, Color.White),  // RibbonQATMini5                                                       
                                                                      Color.FromArgb(153, 176, 206),    // RibbonQATMini1I
                                                                      Color.FromArgb(226, 233, 241),    // RibbonQATMini2I
                                                                      Color.FromArgb(198, 210, 226),    // RibbonQATMini3I
                                                                      Color.FromArgb(128, Color.White), // RibbonQATMini4I
                                                                      Color.FromArgb(72, Color.White),  // RibbonQATMini5I                                                      
                                                                      Color.FromArgb(213, 232, 254),    // RibbonQATFullbar1                                                      
                                                                      Color.FromArgb(205, 223, 245),    // RibbonQATFullbar2                                                      
                                                                      Color.FromArgb(114, 142, 173),    // RibbonQATFullbar3                                                      
                                                                      Color.FromArgb( 90,  90,  90),    // RibbonQATButtonDark                                                      
                                                                      Color.FromArgb(207, 214, 224),    // RibbonQATButtonLight                                                      
                                                                      Color.FromArgb(222, 236, 252),    // RibbonQATOverflow1                                                      
                                                                      Color.FromArgb(123, 139, 156),    // RibbonQATOverflow2                                                      
                                                                      Color.FromArgb(145, 166, 194),    // RibbonGroupSeparatorDark                                                      
                                                                      Color.FromArgb(239, 245, 250),    // RibbonGroupSeparatorLight                                                      
                                                                      Color.FromArgb(192, 212, 241),    // ButtonClusterButtonBack1                                                      
                                                                      Color.FromArgb(200, 219, 238),    // ButtonClusterButtonBack2                                                      
                                                                      Color.FromArgb(155, 183, 224),    // ButtonClusterButtonBorder1                                                      
                                                                      Color.FromArgb(117, 150, 191),    // ButtonClusterButtonBorder2                                                      
                                                                      Color.FromArgb(213, 228, 242),    // NavigatorMiniBackColor                                                    
                                                                      Color.FromArgb(244, 249, 255),    // GridListNormal1                                                    
                                                                      Color.FromArgb(218, 231, 245),    // GridListNormal2                                                    
                                                                      Color.FromArgb(198, 211, 225),    // GridListPressed1                                                    
                                                                      Color.FromArgb(244, 249, 255),    // GridListPressed2                                                    
                                                                      Color.FromArgb(160, 185, 230),    // GridListSelected                                                    
                                                                      Color.FromArgb(233, 246, 255),    // GridSheetColNormal1                                                    
                                                                      Color.FromArgb(213, 226, 240),    // GridSheetColNormal2                                                    
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetColPressed1                                                    
                                                                      Color.FromArgb(255, 252, 230),    // GridSheetColPressed2                                                    
                                                                      Color.FromArgb(255, 211,  89),    // GridSheetColSelected1
                                                                      Color.FromArgb(255, 239, 113),    // GridSheetColSelected2
                                                                      Color.FromArgb(218, 231, 245),    // GridSheetRowNormal                                                   
                                                                      Color.FromArgb(255, 223, 107),    // GridSheetRowPressed
                                                                      Color.FromArgb(245, 210,  87),    // GridSheetRowSelected
                                                                      Color.FromArgb(218, 220, 221),    // GridDataCellBorder
                                                                      Color.FromArgb(183, 219, 255),    // GridDataCellSelected
                                                                      Color.Black,                      // InputControlTextNormal
                                                                      Color.FromArgb(168, 168, 168),    // InputControlTextDisabled
                                                                      Color.FromArgb(177, 192, 214),    // InputControlBorderNormal
                                                                      Color.FromArgb(177, 187, 198),    // InputControlBorderDisabled
                                                                      Color.FromArgb(255, 255, 255),    // InputControlBackNormal
                                                                      Color.FromArgb(240, 240, 240),    // InputControlBackDisabled
                                                                      Color.FromArgb(237, 245, 253),    // InputControlBackInactive
                                                                      Color.Black,                      // InputDropDownNormal1
                                                                      Color.Transparent,                // InputDropDownNormal2
                                                                      Color.FromArgb(172, 168, 153),    // InputDropDownDisabled1
                                                                      Color.Transparent,                // InputDropDownDisabled2
                                                                      Color.FromArgb(240, 242, 245),    // ContextMenuHeadingBack
                                                                      Color.FromArgb( 30,  57,  91),    // ContextMenuHeadingText
                                                                      Color.White,                      // ContextMenuImageColumn
                                                                      Color.FromArgb(195, 212, 235),    // AppButtonBack1
                                                                      Color.FromArgb(195, 212, 235),    // AppButtonBack2
                                                                      Color.FromArgb(114, 142, 173),    // AppButtonBorder
                                                                      Color.FromArgb(195, 212, 235),    // AppButtonOuter1
                                                                      Color.FromArgb(195, 212, 235),    // AppButtonOuter2
                                                                      Color.FromArgb(195, 212, 235),    // AppButtonOuter3
                                                                      Color.Empty,                      // AppButtonInner1
                                                                      Color.FromArgb(114, 142, 173),    // AppButtonInner2
                                                                      Color.White,                      // AppButtonMenuDocs
                                                                      Color.Black,                      // AppButtonMenuDocsText
                                                                      Color.FromArgb(239, 245, 255),    // SeparatorHighInternalBorder1
                                                                      Color.FromArgb(200, 217, 239),    // SeparatorHighInternalBorder2
                                                                      Color.FromArgb(177, 192, 214),    // RibbonGalleryBorder
                                                                      Color.FromArgb(237, 245, 253),    // RibbonGalleryBackNormal
                                                                      Color.FromArgb(242, 247, 252),    // RibbonGalleryBackTracking
                                                                      Color.FromArgb(237, 245, 253),    // RibbonGalleryBack1
                                                                      Color.FromArgb(206, 221, 237),    // RibbonGalleryBack2
                                                                      Color.FromArgb(214, 222, 234),    // RibbonTabTracking3
                                                                      Color.FromArgb(200, 215, 233),    // RibbonTabTracking4
                                                                      Color.FromArgb(147, 167, 195),    // RibbonGroupBorder3
                                                                      Color.FromArgb(226, 236, 247),    // RibbonGroupBorder4
                                                                      Color.FromArgb(251, 251, 252),    // RibbonGroupBorder5
                                                                      Color.FromArgb( 56,  78, 115),    // RibbonGroupTitleText
                                                                      Color.FromArgb(151, 156, 163),    // RibbonDropArrowLight
                                                                      Color.FromArgb( 39,  49,  60),    // RibbonDropArrowDark
                                                                      Color.FromArgb(208, 226, 248),    // HeaderDockInactiveBack1
                                                                      Color.FromArgb(178, 196, 218),    // HeaderDockInactiveBack2
                                                                      Color.FromArgb(133, 158, 191),    // ButtonNavigatorBorder
                                                                      Color.FromArgb(  0,  25,  56),    // ButtonNavigatorText
                                                                      Color.FromArgb(177, 198, 224),    // ButtonNavigatorTrack1
                                                                      Color.FromArgb(211, 224, 240),    // ButtonNavigatorTrack2
                                                                      Color.FromArgb(148, 174, 205),    // ButtonNavigatorPressed1
                                                                      Color.FromArgb(198, 214, 231),    // ButtonNavigatorPressed2
                                                                      Color.FromArgb(200, 219, 240),    // ButtonNavigatorChecked1
                                                                      Color.FromArgb(177, 201, 228),    // ButtonNavigatorChecked2
                                                                      Color.FromArgb(201, 217, 239),    // ToolTipBottom                                                                      
        };
        #endregion

        #region Identity
        static PaletteOffice2010Blue()
        {
            _checkBoxList = new ImageList();
            _checkBoxList.ImageSize = new Size(13, 13);
            _checkBoxList.ColorDepth = ColorDepth.Depth24Bit;
            _checkBoxList.Images.AddStrip(Properties.Resources.CB2010Blue);
            _galleryButtonList = new ImageList();
            _galleryButtonList.ImageSize = new Size(13, 7);
            _galleryButtonList.ColorDepth = ColorDepth.Depth24Bit;
            _galleryButtonList.TransparentColor = Color.Magenta;
            _galleryButtonList.Images.AddStrip(Properties.Resources.Gallery2010);
            _radioButtonArray = new Image[]{Properties.Resources.RB2010BlueD,
                                            Properties.Resources.RB2010BlueN,
                                            Properties.Resources.RB2010BlueT,
                                            Properties.Resources.RB2010BlueP,
                                            Properties.Resources.RB2010BlueDC,
                                            Properties.Resources.RB2010BlueNC,
                                            Properties.Resources.RB2010BlueTC,
                                            Properties.Resources.RB2010BluePC};
        }

        /// <summary>
        /// Initialize a new instance of the PaletteOffice2010Blue class.
		/// </summary>
        public PaletteOffice2010Blue()
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
                return _blueDropDownButton;
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
