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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for menu entries of the professional color table.
	/// </summary>
    public class KryptonPaletteTMSMenu : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTMenu class.
		/// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSMenu(KryptonInternalKCT internalKCT,
                                       NeedPaintHandler needPaint)
            : base(internalKCT, needPaint)
		{
		}
        #endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get
			{
                return (InternalKCT.InternalImageMarginGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalImageMarginGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalImageMarginGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalImageMarginRevealedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalImageMarginRevealedGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalImageMarginRevealedGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalMenuBorder == Color.Empty) &&
                       (InternalKCT.InternalMenuItemText == Color.Empty) &&
                       (InternalKCT.InternalMenuItemBorder == Color.Empty) &&
                       (InternalKCT.InternalMenuItemPressedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalMenuItemPressedGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalMenuItemPressedGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalMenuItemSelected == Color.Empty) &&
                       (InternalKCT.InternalMenuItemSelectedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalMenuItemSelectedGradientEnd == Color.Empty);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            ImageMarginGradientBegin = InternalKCT.ImageMarginGradientBegin;
            ImageMarginGradientEnd = InternalKCT.ImageMarginGradientEnd;
            ImageMarginGradientMiddle = InternalKCT.ImageMarginGradientMiddle;
            ImageMarginRevealedGradientBegin = InternalKCT.ImageMarginRevealedGradientBegin;
            ImageMarginRevealedGradientEnd = InternalKCT.ImageMarginRevealedGradientEnd;
            ImageMarginRevealedGradientMiddle = InternalKCT.ImageMarginRevealedGradientMiddle;
            MenuBorder = InternalKCT.MenuBorder;
            MenuItemText = InternalKCT.MenuItemText;
            MenuItemBorder = InternalKCT.MenuItemBorder;
            MenuItemPressedGradientBegin = InternalKCT.MenuItemPressedGradientBegin;
            MenuItemPressedGradientEnd = InternalKCT.MenuItemPressedGradientEnd;
            MenuItemPressedGradientMiddle = InternalKCT.MenuItemPressedGradientMiddle;
            MenuItemSelected = InternalKCT.MenuItemSelected;
            MenuItemSelectedGradientBegin = InternalKCT.MenuItemSelectedGradientBegin;
            MenuItemSelectedGradientEnd = InternalKCT.MenuItemSelectedGradientEnd;
        }
        #endregion

        #region ImageMarginGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Staring color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginGradientBegin
        {
            get { return InternalKCT.InternalImageMarginGradientBegin; }
            
            set 
            {
                InternalKCT.InternalImageMarginGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginGradientBegin property to its default value.
        /// </summary>
        public void ResetImageMarginGradientBegin()
        {
            ImageMarginGradientBegin = Color.Empty;
        }
        #endregion

        #region ImageMarginGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginGradientEnd
        {
            get { return InternalKCT.InternalImageMarginGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalImageMarginGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginGradientEnd property to its default value.
        /// </summary>
        public void ResetImageMarginGradientEnd()
        {
            ImageMarginGradientEnd = Color.Empty;
        }
        #endregion

        #region ImageMarginGradientMiddle
        /// <summary>
        /// Gets and sets the middle color color of the gradient used in the image margin of a ToolStripDropDownMenu.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle color color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginGradientMiddle
        {
            get { return InternalKCT.InternalImageMarginGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalImageMarginGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginGradientMiddle property to its default value.
        /// </summary>
        public void ResetImageMarginGradientMiddle()
        {
            ImageMarginGradientMiddle = Color.Empty;
        }
        #endregion

        #region ImageMarginRevealedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginRevealedGradientBegin
        {
            get { return InternalKCT.InternalImageMarginRevealedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalImageMarginRevealedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginRevealedGradientBegin property to its default value.
        /// </summary>
        public void ResetImageMarginRevealedGradientBegin()
        {
            ImageMarginRevealedGradientBegin = Color.Empty;
        }
        #endregion

        #region ImageMarginRevealedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginRevealedGradientEnd
        {
            get { return InternalKCT.InternalImageMarginRevealedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalImageMarginRevealedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginRevealedGradientEnd property to its default value.
        /// </summary>
        public void ResetImageMarginRevealedGradientEnd()
        {
            ImageMarginRevealedGradientEnd = Color.Empty;
        }
        #endregion

        #region ImageMarginRevealedGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageMarginRevealedGradientMiddle
        {
            get { return InternalKCT.InternalImageMarginRevealedGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalImageMarginRevealedGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ImageMarginRevealedGradientMiddle property to its default value.
        /// </summary>
        public void ResetImageMarginRevealedGradientMiddle()
        {
            ImageMarginRevealedGradientMiddle = Color.Empty;
        }
        #endregion

        #region MenuBorder
        /// <summary>
        /// Gets and sets the color that is the border color to use on a MenuStrip.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color that is the border color to use on a MenuStrip.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuBorder
        {
            get { return InternalKCT.InternalMenuBorder; }
            
            set 
            { 
                InternalKCT.InternalMenuBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuBorder property to its default value.
        /// </summary>
        public void ResetMenuBorder()
        {
            MenuBorder = Color.Empty;
        }
        #endregion

        #region MenuItemText
        /// <summary>
        /// Gets and sets the color to draw text for individual menu items.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to draw text for individual a ToolStripMenuItem.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemText
        {
            get { return InternalKCT.InternalMenuItemText; }

            set
            {
                InternalKCT.InternalMenuItemText = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemText property to its default value.
        /// </summary>
        public void ResetMenuItemText()
        {
            MenuItemText = Color.Empty;
        }
        #endregion

        #region MenuItemBorder
        /// <summary>
        /// Gets and sets the border color to use with a ToolStripMenuItem.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with a ToolStripMenuItem.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemBorder
        {
            get { return InternalKCT.InternalMenuItemBorder; }
            
            set 
            { 
                InternalKCT.InternalMenuItemBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemBorder property to its default value.
        /// </summary>
        public void ResetMenuItemBorder()
        {
            MenuItemBorder = Color.Empty;
        }
        #endregion

        #region MenuItemPressedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemPressedGradientBegin
        {
            get { return InternalKCT.InternalMenuItemPressedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalMenuItemPressedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemPressedGradientBegin property to its default value.
        /// </summary>
        public void ResetMenuItemPressedGradientBegin()
        {
            MenuItemPressedGradientBegin = Color.Empty;
        }
        #endregion

        #region MenuItemPressedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemPressedGradientEnd
        {
            get { return InternalKCT.InternalMenuItemPressedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalMenuItemPressedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemPressedGradientEnd property to its default value.
        /// </summary>
        public void ResetMenuItemPressedGradientEnd()
        {
            MenuItemPressedGradientEnd = Color.Empty;
        }
        #endregion

        #region MenuItemPressedGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemPressedGradientMiddle
        {
            get { return InternalKCT.InternalMenuItemPressedGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalMenuItemPressedGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemPressedGradientMiddle property to its default value.
        /// </summary>
        public void ResetMenuItemPressedGradientMiddle()
        {
            MenuItemPressedGradientMiddle = Color.Empty;
        }
        #endregion

        #region MenuItemSelected
        /// <summary>
        /// Gets and sets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemSelected
        {
            get { return InternalKCT.InternalMenuItemSelected; }
            
            set 
            { 
                InternalKCT.InternalMenuItemSelected = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemSelected property to its default value.
        /// </summary>
        public void ResetMenuItemSelected()
        {
            MenuItemSelected = Color.Empty;
        }
        #endregion

        #region MenuItemSelectedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used when the ToolStripMenuItem is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemSelectedGradientBegin
        {
            get { return InternalKCT.InternalMenuItemSelectedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalMenuItemSelectedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemSelectedGradientBegin property to its default value.
        /// </summary>
        public void ResetMenuItemSelectedGradientBegin()
        {
            MenuItemSelectedGradientBegin = Color.Empty;
        }
        #endregion

        #region MenuItemSelectedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used when the ToolStripMenuItem is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used when the ToolStripMenuItem is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color MenuItemSelectedGradientEnd
        {
            get { return InternalKCT.InternalMenuItemSelectedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalMenuItemSelectedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the MenuItemSelectedGradientEnd property to its default value.
        /// </summary>
        public void ResetMenuItemSelectedGradientEnd()
        {
            MenuItemSelectedGradientEnd = Color.Empty;
        }
        #endregion
    }
}
