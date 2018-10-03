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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonProfessionalCustomKCT : KryptonProfessionalKCT
    {
        #region Instance Fields
        private Color[] _colors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonProfessionalCustomKCT class.
        /// </summary>
        /// <param name="headerColors">Set of header colors to customize with.</param>
        /// <param name="colorTableColors">Set of ColorTable colors to customize with.</param>
        /// <param name="useSystemColors">Should be forced to use system colors.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonProfessionalCustomKCT(Color[] headerColors,
                                            Color[] colorTableColors,
                                            bool useSystemColors,
                                            IPalette palette)
            : base(headerColors, useSystemColors, palette)
        {
            _colors = colorTableColors;
        }
        #endregion

        #region Button
        #region ButtonCheckedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin] == Color.Empty)
                    return base.ButtonCheckedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin];
            }
        }
        #endregion

        #region ButtonCheckedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd] == Color.Empty)
                    return base.ButtonCheckedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd];
            }
        }
        #endregion

        #region ButtonCheckedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle] == Color.Empty)
                    return base.ButtonCheckedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle];
            }
        }
        #endregion

        #region ButtonCheckedHighlight
        /// <summary>
        /// Gets the solid color used when the button is checked.
        /// </summary>
        public override Color ButtonCheckedHighlight
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonCheckedHighlight] == Color.Empty)
                    return base.ButtonCheckedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedHighlight];
            }
        }
        #endregion

        #region ButtonCheckedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonCheckedHighlight.
        /// </summary>
        public override Color ButtonCheckedHighlightBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder] == Color.Empty)
                    return base.ButtonCheckedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder];
            }
        }
        #endregion

        #region ButtonPressedBorder
        /// <summary>
        /// Gets the border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.
        /// </summary>
        public override Color ButtonPressedBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedBorder] == Color.Empty)
                    return base.ButtonPressedBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedBorder];
            }
        }
        #endregion

        #region ButtonPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedGradientBegin] == Color.Empty)
                    return base.ButtonPressedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin];
            }
        }
        #endregion

        #region ButtonPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedGradientEnd] == Color.Empty)
                    return base.ButtonPressedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd];
            }
        }
        #endregion

        #region ButtonPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle] == Color.Empty)
                    return base.ButtonPressedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle];
            }
        }
        #endregion

        #region ButtonPressedHighlight
        /// <summary>
        /// Gets the solid color used when the button is pressed.
        /// </summary>
        public override Color ButtonPressedHighlight
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedHighlight] == Color.Empty)
                    return base.ButtonPressedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedHighlight];
            }
        }
        #endregion

        #region ButtonPressedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonPressedHighlight.
        /// </summary>
        public override Color ButtonPressedHighlightBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder] == Color.Empty)
                    return base.ButtonPressedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder];
            }
        }
        #endregion

        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
        /// </summary>
        public override Color ButtonSelectedBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedBorder] == Color.Empty)
                    return base.ButtonSelectedBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedBorder];
            }
        }
        #endregion

        #region ButtonSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin] == Color.Empty)
                    return base.ButtonSelectedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin];
            }
        }
        #endregion

        #region ButtonSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd] == Color.Empty)
                    return base.ButtonSelectedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd];
            }
        }
        #endregion

        #region ButtonSelectedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle] == Color.Empty)
                    return base.ButtonSelectedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle];
            }
        }
        #endregion

        #region ButtonSelectedHighlight
        /// <summary>
        /// Gets the solid color used when the button is selected.
        /// </summary>
        public override Color ButtonSelectedHighlight
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedHighlight] == Color.Empty)
                    return base.ButtonSelectedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedHighlight];
            }
        }
        #endregion

        #region ButtonSelectedHighlightBorder
        /// <summary>
        /// Gets the border color to use with ButtonSelectedHighlight.
        /// </summary>
        public override Color ButtonSelectedHighlightBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder] == Color.Empty)
                    return base.ButtonSelectedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder];
            }
        }
        #endregion
        #endregion

        #region Check
        #region CheckBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and gradients are being used.
        /// </summary>
        public override Color CheckBackground
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.CheckBackground] == Color.Empty)
                    return base.CheckBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckBackground];
            }
        }
        #endregion

        #region CheckPressedBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        public override Color CheckPressedBackground
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.CheckPressedBackground] == Color.Empty)
                    return base.CheckPressedBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckPressedBackground];
            }
        }
        #endregion

        #region CheckSelectedBackground
        /// <summary>
        /// Gets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        public override Color CheckSelectedBackground
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.CheckSelectedBackground] == Color.Empty)
                    return base.CheckSelectedBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckSelectedBackground];
            }
        }
        #endregion
        #endregion

        #region Grip
        #region GripDark
        /// <summary>
        /// Gets the color to use for shadow effects on the grip (move handle).
        /// </summary>
        public override Color GripDark
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.GripDark] == Color.Empty)
                    return base.GripDark;
                else
                    return _colors[(int)PaletteColorIndex.GripDark];
            }
        }
        #endregion

        #region GripLight
        /// <summary>
        /// Gets the color to use for highlight effects on the grip (move handle).
        /// </summary>
        public override Color GripLight
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.GripLight] == Color.Empty)
                    return base.GripLight;
                else
                    return _colors[(int)PaletteColorIndex.GripLight];
            }
        }
        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginGradientBegin] == Color.Empty)
                    return base.ImageMarginGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientBegin];
            }
        }
        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginGradientEnd] == Color.Empty)
                    return base.ImageMarginGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientEnd];
            }
        }
        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginGradientMiddle] == Color.Empty)
                    return base.ImageMarginGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle];
            }
        }
        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin] == Color.Empty)
                    return base.ImageMarginRevealedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin];
            }
        }
        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd] == Color.Empty)
                    return base.ImageMarginRevealedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd];
            }
        }
        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle] == Color.Empty)
                    return base.ImageMarginRevealedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle];
            }
        }
        #endregion
        #endregion

        #region Menu
        #region MenuBorder
        /// <summary>
        /// Gets the color that is the border color to use on a MenuStrip.
        /// </summary>
        public override Color MenuBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuBorder] == Color.Empty)
                    return base.MenuBorder;
                else
                    return _colors[(int)PaletteColorIndex.MenuBorder];
            }
        }
        #endregion

        #region MenuItemText
        /// <summary>
        /// Gets the color used to draw menu item text.
        /// </summary>
        public override Color MenuItemText
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemText] == Color.Empty)
                    return base.MenuItemText;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemText];
            }
        }
        #endregion

        #region MenuItemBorder
        /// <summary>
        /// Gets the border color to use with a ToolStripMenuItem.
        /// </summary>
        public override Color MenuItemBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemBorder] == Color.Empty)
                    return base.MenuItemBorder;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemBorder];
            }
        }
        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin] == Color.Empty)
                    return base.MenuItemPressedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin];
            }
        }
        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd] == Color.Empty)
                    return base.MenuItemPressedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd];
            }
        }
        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle] == Color.Empty)
                    return base.MenuItemPressedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle];
            }
        }
        #endregion

        #region MenuItemSelected
        /// <summary>
        /// Gets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelected
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemSelected] == Color.Empty)
                    return base.MenuItemSelected;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelected];
            }
        }
        #endregion

        #region MenuItemSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin] == Color.Empty)
                    return base.MenuItemSelectedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin];
            }
        }
        #endregion

        #region MenuItemSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd] == Color.Empty)
                    return base.MenuItemSelectedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd];
            }
        }
        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the color used to draw text on a menu strip.
        /// </summary>
        public override Color MenuStripText
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuStripText] == Color.Empty)
                    return base.MenuStripText;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripText];
            }
        }
        #endregion

        #region MenuStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuStripGradientBegin] == Color.Empty)
                    return base.MenuStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripGradientBegin];
            }
        }
        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.MenuStripGradientEnd] == Color.Empty)
                    return base.MenuStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripGradientEnd];
            }
        }
        #endregion
        #endregion

        #region OverflowButton
        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.OverflowButtonGradientBegin] == Color.Empty)
                    return base.OverflowButtonGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin];
            }
        }
        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.OverflowButtonGradientEnd] == Color.Empty)
                    return base.OverflowButtonGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd];
            }
        }
        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle] == Color.Empty)
                    return base.OverflowButtonGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle];
            }
        }
        #endregion
        #endregion

        #region RaftingContainer
        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.RaftingContainerGradientBegin] == Color.Empty)
                    return base.RaftingContainerGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin];
            }
        }
        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.RaftingContainerGradientEnd] == Color.Empty)
                    return base.RaftingContainerGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd];
            }
        }
        #endregion
        #endregion

        #region Separator
        #region SeparatorDark
        /// <summary>
        /// Gets the color to use to for shadow effects on the ToolStripSeparator.
        /// </summary>
        public override Color SeparatorDark
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.SeparatorDark] == Color.Empty)
                    return base.SeparatorDark;
                else
                    return _colors[(int)PaletteColorIndex.SeparatorDark];
            }
        }
        #endregion

        #region SeparatorLight
        /// <summary>
        /// Gets the color to use to for highlight effects on the ToolStripSeparator.
        /// </summary>
        public override Color SeparatorLight
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.SeparatorLight] == Color.Empty)
                    return base.SeparatorLight;
                else
                    return _colors[(int)PaletteColorIndex.SeparatorLight];
            }
        }
        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripText
        /// <summary>
        /// Gets the color used to draw text on a status strip.
        /// </summary>
        public override Color StatusStripText
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.StatusStripText] == Color.Empty)
                    return base.StatusStripText;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripText];
            }
        }
        #endregion

        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used on the StatusStrip.
        /// </summary>
        public override Color StatusStripGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.StatusStripGradientBegin] == Color.Empty)
                    return base.StatusStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripGradientBegin];
            }
        }
        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used on the StatusStrip.
        /// </summary>
        public override Color StatusStripGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.StatusStripGradientEnd] == Color.Empty)
                    return base.StatusStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripGradientEnd];
            }
        }
        #endregion
        #endregion

        #region ToolStrip
        #region ToolStripText
        /// <summary>
        /// Gets the color used to draw text on a tool strip.
        /// </summary>
        public override Color ToolStripText
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripText] == Color.Empty)
                    return base.ToolStripText;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripText];
            }
        }
        #endregion

        #region ToolStripBorder
        /// <summary>
        /// Gets the border color to use on the bottom edge of the ToolStrip.
        /// </summary>
        public override Color ToolStripBorder
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripBorder] == Color.Empty)
                    return base.ToolStripBorder;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripBorder];
            }
        }
        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripContentPanel.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin] == Color.Empty)
                    return base.ToolStripContentPanelGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin];
            }
        }
        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContentPanel.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd] == Color.Empty)
                    return base.ToolStripContentPanelGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd];
            }
        }
        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets the solid background color of the ToolStripDropDown.
        /// </summary>
        public override Color ToolStripDropDownBackground
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripDropDownBackground] == Color.Empty)
                    return base.ToolStripDropDownBackground;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripDropDownBackground];
            }
        }
        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripGradientBegin] == Color.Empty)
                    return base.ToolStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientBegin];
            }
        }
        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripGradientEnd] == Color.Empty)
                    return base.ToolStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientEnd];
            }
        }
        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripGradientMiddle] == Color.Empty)
                    return base.ToolStripGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientMiddle];
            }
        }
        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin] == Color.Empty)
                    return base.ToolStripPanelGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin];
            }
        }
        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd
        {
            get
            {
                if (_colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd] == Color.Empty)
                    return base.ToolStripPanelGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd];
            }
        }
        #endregion
        #endregion
    }
}
