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
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonInternalKCT : KryptonColorTable
    {
        #region Instance Fields
        private KryptonColorTable _baseKCT;
        private InheritBool _useRoundedEdges;
        private Color[] _colors;
        private Font _menuFont;
        private Font _toolFont;
        private Font _statusFont;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonInternalKCT class.
        /// </summary>
        /// <param name="baseKCT">Initial base KCT to inherit values from.</param>
        /// <param name="palette">Reference to associated palette.</param>
        public KryptonInternalKCT(KryptonColorTable baseKCT,
                                  IPalette palette)
            : base(palette)
        {
            Debug.Assert(baseKCT != null);
            
            // Remember the base used for inheriting
            _baseKCT = baseKCT;

            // Always assume the same use of system colors
            UseSystemColors = _baseKCT.UseSystemColors;

            // Create the array for storing colors
            _colors = new Color[(int)PaletteColorIndex.Count];

            // Initialise all the colors to empty
            for (int i = 0; i < _colors.Length; i++)
                _colors[i] = Color.Empty;

            // Initialise other storage values
            _useRoundedEdges = InheritBool.Inherit;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public bool IsDefault 
        { 
            get { return (_useRoundedEdges == InheritBool.Inherit); }
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
                    return BaseKCT.ButtonCheckedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonCheckedGradientBegin value.
        /// </summary>
        public Color InternalButtonCheckedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin] = value; }
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
                    return BaseKCT.ButtonCheckedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonCheckedGradientEnd value.
        /// </summary>
        public Color InternalButtonCheckedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd] = value; }
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
                    return BaseKCT.ButtonCheckedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonCheckedGradientMiddle value.
        /// </summary>
        public Color InternalButtonCheckedGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle] = value; }
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
                    return BaseKCT.ButtonCheckedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedHighlight];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonCheckedHighlight value.
        /// </summary>
        public Color InternalButtonCheckedHighlight
        {
            get { return _colors[(int)PaletteColorIndex.ButtonCheckedHighlight]; }
            set { _colors[(int)PaletteColorIndex.ButtonCheckedHighlight] = value; }
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
                    return BaseKCT.ButtonCheckedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonCheckedHighlightBorder value.
        /// </summary>
        public Color InternalButtonCheckedHighlightBorder
        {
            get { return _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder]; }
            set { _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder] = value; }
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
                    return BaseKCT.ButtonPressedBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedBorder value.
        /// </summary>
        public Color InternalButtonPressedBorder
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedBorder]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedBorder] = value; }
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
                    return BaseKCT.ButtonPressedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedGradientBegin value.
        /// </summary>
        public Color InternalButtonPressedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin] = value; }
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
                    return BaseKCT.ButtonPressedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedGradientEnd value.
        /// </summary>
        public Color InternalButtonPressedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd] = value; }
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
                    return BaseKCT.ButtonPressedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedGradientMiddle value.
        /// </summary>
        public Color InternalButtonPressedGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle] = value; }
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
                    return BaseKCT.ButtonPressedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedHighlight];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedHighlight value.
        /// </summary>
        public Color InternalButtonPressedHighlight
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedHighlight]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedHighlight] = value; }
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
                    return BaseKCT.ButtonPressedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonPressedHighlightBorder value.
        /// </summary>
        public Color InternalButtonPressedHighlightBorder
        {
            get { return _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder]; }
            set { _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder] = value; }
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
                    return BaseKCT.ButtonSelectedBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedBorder value.
        /// </summary>
        public Color InternalButtonSelectedBorder
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedBorder]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedBorder] = value; }
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
                    return BaseKCT.ButtonSelectedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedGradientBegin value.
        /// </summary>
        public Color InternalButtonSelectedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin] = value; }
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
                    return BaseKCT.ButtonSelectedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedGradientEnd value.
        /// </summary>
        public Color InternalButtonSelectedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd] = value; }
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
                    return BaseKCT.ButtonSelectedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedGradientMiddle value.
        /// </summary>
        public Color InternalButtonSelectedGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle] = value; }
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
                    return BaseKCT.ButtonSelectedHighlight;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedHighlight];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedHighlight value.
        /// </summary>
        public Color InternalButtonSelectedHighlight
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedHighlight]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedHighlight] = value; }
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
                    return BaseKCT.ButtonSelectedHighlightBorder;
                else
                    return _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ButtonSelectedHighlightBorder value.
        /// </summary>
        public Color InternalButtonSelectedHighlightBorder
        {
            get { return _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder]; }
            set { _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder] = value; }
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
                    return BaseKCT.CheckBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckBackground];
            }
        }

        /// <summary>
        /// Sets and sets the internal CheckBackground value.
        /// </summary>
        public Color InternalCheckBackground
        {
            get { return _colors[(int)PaletteColorIndex.CheckBackground]; }
            set { _colors[(int)PaletteColorIndex.CheckBackground] = value; }
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
                    return BaseKCT.CheckPressedBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckPressedBackground];
            }
        }

        /// <summary>
        /// Sets and sets the internal CheckPressedBackground value.
        /// </summary>
        public Color InternalCheckPressedBackground
        {
            get { return _colors[(int)PaletteColorIndex.CheckPressedBackground]; }
            set { _colors[(int)PaletteColorIndex.CheckPressedBackground] = value; }
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
                    return BaseKCT.CheckSelectedBackground;
                else
                    return _colors[(int)PaletteColorIndex.CheckSelectedBackground];
            }
        }

        /// <summary>
        /// Sets and sets the internal CheckSelectedBackground value.
        /// </summary>
        public Color InternalCheckSelectedBackground
        {
            get { return _colors[(int)PaletteColorIndex.CheckSelectedBackground]; }
            set { _colors[(int)PaletteColorIndex.CheckSelectedBackground] = value; }
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
                    return BaseKCT.GripDark;
                else
                    return _colors[(int)PaletteColorIndex.GripDark];
            }
        }

        /// <summary>
        /// Sets and sets the internal GripDark value.
        /// </summary>
        public Color InternalGripDark
        {
            get { return _colors[(int)PaletteColorIndex.GripDark]; }
            set { _colors[(int)PaletteColorIndex.GripDark] = value; }
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
                    return BaseKCT.GripLight;
                else
                    return _colors[(int)PaletteColorIndex.GripLight];
            }
        }

        /// <summary>
        /// Sets and sets the internal GripLight value.
        /// </summary>
        public Color InternalGripLight
        {
            get { return _colors[(int)PaletteColorIndex.GripLight]; }
            set { _colors[(int)PaletteColorIndex.GripLight] = value; }
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
                    return BaseKCT.ImageMarginGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginGradientBegin value.
        /// </summary>
        public Color InternalImageMarginGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginGradientBegin] = value; }
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
                    return BaseKCT.ImageMarginGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginGradientEnd value.
        /// </summary>
        public Color InternalImageMarginGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginGradientEnd] = value; }
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
                    return BaseKCT.ImageMarginGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginGradientMiddle value.
        /// </summary>
        public Color InternalImageMarginGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle] = value; }
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
                    return BaseKCT.ImageMarginRevealedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginRevealedGradientBegin value.
        /// </summary>
        public Color InternalImageMarginRevealedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin] = value; }
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
                    return BaseKCT.ImageMarginRevealedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginRevealedGradientEnd value.
        /// </summary>
        public Color InternalImageMarginRevealedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd] = value; }
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
                    return BaseKCT.ImageMarginRevealedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ImageMarginRevealedGradientMiddle value.
        /// </summary>
        public Color InternalImageMarginRevealedGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle] = value; }
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
                    return BaseKCT.MenuBorder;
                else
                    return _colors[(int)PaletteColorIndex.MenuBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuBorder value.
        /// </summary>
        public Color InternalMenuBorder
        {
            get { return _colors[(int)PaletteColorIndex.MenuBorder]; }
            set { _colors[(int)PaletteColorIndex.MenuBorder] = value; }
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
                    return BaseKCT.MenuItemText;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemText];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemText value.
        /// </summary>
        public Color InternalMenuItemText
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemText]; }
            set { _colors[(int)PaletteColorIndex.MenuItemText] = value; }
        }
        #endregion

        #region MenuStripFont
        /// <summary>
        /// Gets the font used to draw text on a status strip.
        /// </summary>
        public override Font MenuStripFont
        {
            get
            {
                if (_menuFont == null)
                    return BaseKCT.MenuStripFont;
                else
                    return _menuFont;
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuStripFont value.
        /// </summary>
        public Font InternalMenuStripFont
        {
            get { return _menuFont; }
            set { _menuFont = value; }
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
                    return BaseKCT.MenuItemBorder;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemBorder value.
        /// </summary>
        public Color InternalMenuItemBorder
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemBorder]; }
            set { _colors[(int)PaletteColorIndex.MenuItemBorder] = value; }
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
                    return BaseKCT.MenuItemPressedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemPressedGradientBegin value.
        /// </summary>
        public Color InternalMenuItemPressedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin] = value; }
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
                    return BaseKCT.MenuItemPressedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemPressedGradientEnd value.
        /// </summary>
        public Color InternalMenuItemPressedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd] = value; }
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
                    return BaseKCT.MenuItemPressedGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemPressedGradientMiddle value.
        /// </summary>
        public Color InternalMenuItemPressedGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle] = value; }
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
                    return BaseKCT.MenuItemSelected;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelected];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemSelected value.
        /// </summary>
        public Color InternalMenuItemSelected
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemSelected]; }
            set { _colors[(int)PaletteColorIndex.MenuItemSelected] = value; }
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
                    return BaseKCT.MenuItemSelectedGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemSelectedGradientBegin value.
        /// </summary>
        public Color InternalMenuItemSelectedGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin] = value; }
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
                    return BaseKCT.MenuItemSelectedGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuItemSelectedGradientEnd value.
        /// </summary>
        public Color InternalMenuItemSelectedGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd] = value; }
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
                    return BaseKCT.MenuStripText;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripText];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuStripText value.
        /// </summary>
        public Color InternalMenuStripText
        {
            get { return _colors[(int)PaletteColorIndex.MenuStripText]; }
            set { _colors[(int)PaletteColorIndex.MenuStripText] = value; }
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
                    return BaseKCT.MenuStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuStripGradientBegin value.
        /// </summary>
        public Color InternalMenuStripGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.MenuStripGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.MenuStripGradientBegin] = value; }
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
                    return BaseKCT.MenuStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.MenuStripGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal MenuStripGradientEnd value.
        /// </summary>
        public Color InternalMenuStripGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.MenuStripGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.MenuStripGradientEnd] = value; }
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
                    return BaseKCT.OverflowButtonGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal OverflowButtonGradientBegin value.
        /// </summary>
        public Color InternalOverflowButtonGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin] = value; }
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
                    return BaseKCT.OverflowButtonGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal OverflowButtonGradientEnd value.
        /// </summary>
        public Color InternalOverflowButtonGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd] = value; }
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
                    return BaseKCT.OverflowButtonGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal OverflowButtonGradientMiddle value.
        /// </summary>
        public Color InternalOverflowButtonGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle] = value; }
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
                    return BaseKCT.RaftingContainerGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal RaftingContainerGradientBegin value.
        /// </summary>
        public Color InternalRaftingContainerGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin] = value; }
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
                    return BaseKCT.RaftingContainerGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal RaftingContainerGradientEnd value.
        /// </summary>
        public Color InternalRaftingContainerGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd] = value; }
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
                    return BaseKCT.SeparatorDark;
                else
                    return _colors[(int)PaletteColorIndex.SeparatorDark];
            }
        }

        /// <summary>
        /// Sets and sets the internal SeparatorDark value.
        /// </summary>
        public Color InternalSeparatorDark
        {
            get { return _colors[(int)PaletteColorIndex.SeparatorDark]; }
            set { _colors[(int)PaletteColorIndex.SeparatorDark] = value; }
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
                    return BaseKCT.SeparatorLight;
                else
                    return _colors[(int)PaletteColorIndex.SeparatorLight];
            }
        }

        /// <summary>
        /// Sets and sets the internal SeparatorLight value.
        /// </summary>
        public Color InternalSeparatorLight
        {
            get { return _colors[(int)PaletteColorIndex.SeparatorLight]; }
            set { _colors[(int)PaletteColorIndex.SeparatorLight] = value; }
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
                    return BaseKCT.StatusStripText;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripText];
            }
        }

        /// <summary>
        /// Sets and sets the internal StatusStripText value.
        /// </summary>
        public Color InternalStatusStripText
        {
            get { return _colors[(int)PaletteColorIndex.StatusStripText]; }
            set { _colors[(int)PaletteColorIndex.StatusStripText] = value; }
        }
        #endregion

        #region StatusStripFont
        /// <summary>
        /// Gets the font used to draw text on a status strip.
        /// </summary>
        public override Font StatusStripFont
        {
            get
            {
                if (_statusFont == null)
                    return BaseKCT.StatusStripFont;
                else
                    return _statusFont;
            }
        }

        /// <summary>
        /// Sets and sets the internal StatusStripFont value.
        /// </summary>
        public Font InternalStatusStripFont
        {
            get { return _statusFont; }
            set { _statusFont = value; }
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
                    return BaseKCT.StatusStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal StatusStripGradientBegin value.
        /// </summary>
        public Color InternalStatusStripGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.StatusStripGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.StatusStripGradientBegin] = value; }
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
                    return BaseKCT.StatusStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.StatusStripGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal StatusStripGradientEnd value.
        /// </summary>
        public Color InternalStatusStripGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.StatusStripGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.StatusStripGradientEnd] = value; }
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
                    return BaseKCT.ToolStripText;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripText];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripText value.
        /// </summary>
        public Color InternalToolStripText
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripText]; }
            set { _colors[(int)PaletteColorIndex.ToolStripText] = value; }
        }
        #endregion

        #region ToolStripFont
        /// <summary>
        /// Gets the font used to draw text on a tool strip.
        /// </summary>
        public override Font ToolStripFont
        {
            get
            {
                if (_toolFont == null)
                    return BaseKCT.ToolStripFont;
                else
                    return _toolFont;
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripFont value.
        /// </summary>
        public Font InternalToolStripFont
        {
            get { return _toolFont; }
            set { _toolFont = value; }
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
                    return BaseKCT.ToolStripBorder;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripBorder];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripBorder value.
        /// </summary>
        public Color InternalToolStripBorder
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripBorder]; }
            set { _colors[(int)PaletteColorIndex.ToolStripBorder] = value; }
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
                    return BaseKCT.ToolStripContentPanelGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripContentPanelGradientBegin value.
        /// </summary>
        public Color InternalToolStripContentPanelGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin] = value; }
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
                    return BaseKCT.ToolStripContentPanelGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripContentPanelGradientEnd value.
        /// </summary>
        public Color InternalToolStripContentPanelGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd] = value; }
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
                    return BaseKCT.ToolStripDropDownBackground;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripDropDownBackground];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripDropDownBackground value.
        /// </summary>
        public Color InternalToolStripDropDownBackground
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripDropDownBackground]; }
            set { _colors[(int)PaletteColorIndex.ToolStripDropDownBackground] = value; }
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
                    return BaseKCT.ToolStripGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripGradientBegin value.
        /// </summary>
        public Color InternalToolStripGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ToolStripGradientBegin] = value; }
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
                    return BaseKCT.ToolStripGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripGradientEnd value.
        /// </summary>
        public Color InternalToolStripGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ToolStripGradientEnd] = value; }
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
                    return BaseKCT.ToolStripGradientMiddle;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripGradientMiddle];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripGradientMiddle value.
        /// </summary>
        public Color InternalToolStripGradientMiddle
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripGradientMiddle]; }
            set { _colors[(int)PaletteColorIndex.ToolStripGradientMiddle] = value; }
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
                    return BaseKCT.ToolStripPanelGradientBegin;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripPanelGradientBegin value.
        /// </summary>
        public Color InternalToolStripPanelGradientBegin
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin]; }
            set { _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin] = value; }
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
                    return BaseKCT.ToolStripPanelGradientEnd;
                else
                    return _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd];
            }
        }

        /// <summary>
        /// Sets and sets the internal ToolStripPanelGradientEnd value.
        /// </summary>
        public Color InternalToolStripPanelGradientEnd
        {
            get { return _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd]; }
            set { _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd] = value; }
        }
        #endregion
        #endregion

        #region UseRoundedEdges
        /// <summary>
        /// Gets a value indicating if rounded egdes are required.
        /// </summary>
        public override InheritBool UseRoundedEdges
        {
            get
            {
                if (_useRoundedEdges == InheritBool.Inherit)
                    return BaseKCT.UseRoundedEdges;
                else
                    return _useRoundedEdges;
            }
        }

        /// <summary>
        /// Sets and sets the internal UseRoundedEdges value.
        /// </summary>
        public InheritBool InternalUseRoundedEdges
        {
            get { return _useRoundedEdges; }
            set { _useRoundedEdges = value; }
        }
        #endregion

        #region Internal
        internal KryptonColorTable BaseKCT
        {
            get { return _baseKCT; }
            
            set 
            {
                // Use the new inheritance
                _baseKCT = value;

                // Always assume the same use of system colors
                UseSystemColors = _baseKCT.UseSystemColors;
            }
        }
        #endregion
    }
}
