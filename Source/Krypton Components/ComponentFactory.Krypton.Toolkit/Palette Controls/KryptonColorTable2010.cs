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
    /// Provide KryptonColorTable2010 values using an array of Color values as the source.
    /// </summary>
    public class KryptonColorTable2010 : KryptonColorTable
    {
        #region Static Fields
        private static readonly Color _contextMenuBackground = Color.White;
        private static readonly Color _menuBorder = Color.FromArgb(167, 171, 176);
        private static readonly Color _checkBackground = Color.FromArgb(252, 241, 194);
        private static readonly Color _buttonSelectedBegin = Color.FromArgb(251, 242, 215);
        private static readonly Color _buttonSelectedEnd = Color.FromArgb(247, 224, 135);
        private static readonly Color _buttonPressedBegin = Color.FromArgb(255, 228, 138);
        private static readonly Color _buttonPressedEnd = Color.FromArgb(255, 228, 138);
        private static readonly Color _buttonCheckedBegin = Color.FromArgb(255, 216, 107);
        private static readonly Color _buttonCheckedEnd = Color.FromArgb(255, 216, 107);
        private static readonly Color _menuItemSelectedBegin = Color.FromArgb(251, 242, 215);
        private static readonly Color _menuItemSelectedEnd = Color.FromArgb(247, 224, 135);
        private static Font _menuToolFont;
        private static Font _statusFont;
        #endregion

        #region Instance Fields
        private Color[] _colors;
        private InheritBool _roundedEdges;
        #endregion

        #region Identity
        static KryptonColorTable2010()
        {
            // Get the font settings from the system
            DefineFonts();

            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
        }

        /// <summary>
        /// Initialize a new instance of the KryptonColorTable2010 class.
        /// </summary>
        /// <param name="colors">Source of </param>
        /// <param name="roundedEdges">Should have rounded edges.</param>
        /// <param name="palette">Associated palette instance.</param>
        public KryptonColorTable2010(Color[] colors,
                                     InheritBool roundedEdges,
                                     IPalette palette)
            : base(palette)
        {
            Debug.Assert(colors != null);
            _colors = colors;
            _roundedEdges = roundedEdges;
        }
        #endregion

        #region Colors
        /// <summary>
        /// Gets the raw set of colors.
        /// </summary>
        public Color[] Colors
        {
            get { return _colors; }
        }
        #endregion

        #region RoundedEdges
        /// <summary>
        /// Gets a value indicating if rounded egdes are required.
        /// </summary>
        public override InheritBool UseRoundedEdges
        {
            get { return _roundedEdges; }
        }
        #endregion

        #region ButtonPressed
        #region ButtonPressedBorder
        /// <summary>
        /// Gets the border color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion

        #region ButtonPressedGradientBegin
        /// <summary>
        /// Gets the background starting color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientBegin
        {
            get { return _buttonPressedBegin; }
        }
        #endregion

        #region ButtonPressedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientMiddle
        {
            get { return _buttonPressedBegin; }
        }
        #endregion

        #region ButtonPressedGradientEnd
        /// <summary>
        /// Gets the background ending color for a button being pressed.
        /// </summary>
        public override Color ButtonPressedGradientEnd
        {
            get { return _buttonPressedEnd; }
        }
        #endregion

        #region ButtonPressedHighlight
        /// <summary>
        /// Gets the highlight background for a pressed button.
        /// </summary>
        public override Color ButtonPressedHighlight
        {
            get { return _buttonPressedBegin; }
        }
        #endregion

        #region ButtonPressedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a pressed button.
        /// </summary>
        public override Color ButtonPressedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region ButtonSelected
        #region ButtonSelectedBorder
        /// <summary>
        /// Gets the border color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion

        #region ButtonSelectedGradientBegin
        /// <summary>
        /// Gets the background starting color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientBegin
        {
            get { return _buttonSelectedBegin; }
        }
        #endregion

        #region ButtonSelectedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientMiddle
        {
            get { return _buttonSelectedBegin; }
        }
        #endregion

        #region ButtonSelectedGradientEnd
        /// <summary>
        /// Gets the background ending color for a button being selected.
        /// </summary>
        public override Color ButtonSelectedGradientEnd
        {
            get { return _buttonSelectedEnd; }
        }
        #endregion

        #region ButtonSelectedHighlight
        /// <summary>
        /// Gets the highlight background for a selected button.
        /// </summary>
        public override Color ButtonSelectedHighlight
        {
            get { return _buttonSelectedBegin; }
        }
        #endregion

        #region ButtonSelectedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a selected button.
        /// </summary>
        public override Color ButtonSelectedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region ButtonChecked
        #region ButtonCheckedGradientBegin
        /// <summary>
        /// Gets the background starting color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientBegin
        {
            get { return _buttonCheckedBegin; }
        }
        #endregion

        #region ButtonCheckedGradientMiddle
        /// <summary>
        /// Gets the background middle color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientMiddle
        {
            get { return _buttonCheckedBegin; }
        }
        #endregion

        #region ButtonCheckedGradientEnd
        /// <summary>
        /// Gets the background ending color for a checked button.
        /// </summary>
        public override Color ButtonCheckedGradientEnd
        {
            get { return _buttonCheckedEnd; }
        }
        #endregion

        #region ButtonCheckedHighlight
        /// <summary>
        /// Gets the highlight background for a checked button.
        /// </summary>
        public override Color ButtonCheckedHighlight
        {
            get { return _buttonCheckedBegin; }
        }
        #endregion

        #region ButtonCheckedHighlightBorder
        /// <summary>
        /// Gets the highlight border for a checked button.
        /// </summary>
        public override Color ButtonCheckedHighlightBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion
        #endregion

        #region Check
        #region CheckBackground
        /// <summary>
        /// Get background of the check mark area.
        /// </summary>
        public override Color CheckBackground
        {
            get { return _checkBackground; }
        }
        #endregion

        #region CheckBackground
        /// <summary>
        /// Get background of a pressed check mark area.
        /// </summary>
        public override Color CheckPressedBackground
        {
            get { return _checkBackground; }
        }
        #endregion

        #region CheckBackground
        /// <summary>
        /// Get background of a selected check mark area.
        /// </summary>
        public override Color CheckSelectedBackground
        {
            get { return _checkBackground; }
        }
        #endregion
        #endregion

        #region Grip
        #region GripLight
        /// <summary>
        /// Gets the light color used to draw grips.
        /// </summary>
        public override Color GripLight
        {
            get { return _colors[(int)SchemeOfficeColors.GripLight]; }
        }
        #endregion

        #region GripDark
        /// <summary>
        /// Gets the dark color used to draw grips.
        /// </summary>
        public override Color GripDark
        {
            get { return _colors[(int)SchemeOfficeColors.GripDark]; }
        }
        #endregion
        #endregion

        #region ImageMargin
        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin.
        /// </summary>
        public override Color ImageMarginGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets the starting color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets the middle color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets the ending color for the context menu margin revealed.
        /// </summary>
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ImageMargin]; }
        }
        #endregion
        #endregion

        #region MenuBorder
        /// <summary>
        /// Gets the color of the border around menus.
        /// </summary>
        public override Color MenuBorder
        {
            get { return _menuBorder; }
        }
        #endregion

        #region MenuItem
        #region MenuItemBorder
        /// <summary>
        /// Gets the border color for around the menu item.
        /// </summary>
        public override Color MenuItemBorder
        {
            get { return _menuBorder; }
        }
        #endregion

        #region MenuItemSelected
        /// <summary>
        /// Gets the color of a selected menu item.
        /// </summary>
        public override Color MenuItemSelected
        {
            get { return _colors[(int)SchemeOfficeColors.ButtonBorder]; }
        }
        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBegin]; }
        }
        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripEnd]; }
        }
        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed down.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripMiddle]; }
        }
        #endregion

        #region MenuItemSelectedGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get { return _menuItemSelectedBegin; }
        }
        #endregion

        #region MenuItemSelectedGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get { return _menuItemSelectedEnd; }
        }
        #endregion
        #endregion

        #region MenuStrip
        #region MenuStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region MenuStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the MenuStrip.
        /// </summary>
        public override Color MenuStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
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
            get { return _colors[(int)SchemeOfficeColors.OverflowBegin]; }
        }
        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.OverflowEnd]; }
        }
        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        public override Color OverflowButtonGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.OverflowMiddle]; }
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
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripContainer.
        /// </summary>
        public override Color RaftingContainerGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #endregion

        #region Separator
        #region SeparatorLight
        /// <summary>
        /// Gets the light separator color.
        /// </summary>
        public override Color SeparatorLight
        {
            get { return _colors[(int)SchemeOfficeColors.SeparatorLight]; }
        }
        #endregion

        #region SeparatorDark
        /// <summary>
        /// Gets the dark separator color.
        /// </summary>
        public override Color SeparatorDark
        {
            get { return _colors[(int)SchemeOfficeColors.SeparatorDark]; }
        }
        #endregion
        #endregion

        #region StatusStrip
        #region StatusStripGradientBegin
        /// <summary>
        /// Gets the starting color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripLight]; }
        }
        #endregion

        #region StatusStripGradientEnd
        /// <summary>
        /// Gets the ending color for the status strip background.
        /// </summary>
        public override Color StatusStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripDark]; }
        }
        #endregion
        #endregion

        #region Text
        #region MenuItemText
        /// <summary>
        /// Gets the text color used on the menu items.
        /// </summary>
        public override Color MenuItemText
        {
            get { return _colors[(int)SchemeOfficeColors.TextButtonNormal]; }
        }
        #endregion

        #region MenuStripText
        /// <summary>
        /// Gets the text color used on the menu strip.
        /// </summary>
        public override Color MenuStripText
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripText]; }
        }
        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets the text color used on the tool strip.
        /// </summary>
        public override Color ToolStripText
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripText]; }
        }
        #endregion

        #region StatusStripText
        /// <summary>
        /// Gets the text color used on the status strip.
        /// </summary>
        public override Color StatusStripText
        {
            get { return _colors[(int)SchemeOfficeColors.StatusStripText]; }
        }
        #endregion

        #region MenuStripFont
        /// <summary>
        /// Gets the font used on the menu strip.
        /// </summary>
        public override Font MenuStripFont
        {
            get { return _menuToolFont; }
        }
        #endregion

        #region ToolStripFont
        /// <summary>
        /// Gets the font used on the tool strip.
        /// </summary>
        public override Font ToolStripFont
        {
            get { return _menuToolFont; }
        }
        #endregion

        #region StatusStripFont
        /// <summary>
        /// Gets the font used on the status strip.
        /// </summary>
        public override Font StatusStripFont
        {
            get { return _statusFont; }
        }
        #endregion
        #endregion

        #region ToolStrip
        #region ToolStripBorder
        /// <summary>
        /// Gets the border color to use on the bottom edge of the ToolStrip.
        /// </summary>
        public override Color ToolStripBorder
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBorder]; }
        }
        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets the starting color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets the ending color for the content panel background.
        /// </summary>
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets the background color for drop down menus.
        /// </summary>
        public override Color ToolStripDropDownBackground
        {
            get { return _contextMenuBackground; }
        }
        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBegin]; }
        }
        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripEnd]; }
        }
        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets the middle color of the gradient used in the ToolStrip background.
        /// </summary>
        public override Color ToolStripGradientMiddle
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripMiddle]; }
        }
        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets the starting color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientBegin
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets the end color of the gradient used in the ToolStripPanel.
        /// </summary>
        public override Color ToolStripPanelGradientEnd
        {
            get { return _colors[(int)SchemeOfficeColors.ToolStripBack]; }
        }
        #endregion
        #endregion

        #region Implementation
        private static void DefineFonts()
        {
            // Release existing resources
            if (_menuToolFont != null) _menuToolFont.Dispose();
            if (_statusFont != null) _statusFont.Dispose();

            // Create new font using system information
            _menuToolFont = new Font("Segoe UI", SystemFonts.MenuFont.SizeInPoints, FontStyle.Regular);
            _statusFont = new Font("Segoe UI", SystemFonts.StatusFont.SizeInPoints, FontStyle.Regular);
        }

        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Update fonts to reflect any change in system settings
            DefineFonts();
        }
        #endregion
    }
}
