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
	/// Storage for button entries of the professional color table.
	/// </summary>
    public class KryptonPaletteTMSButton : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTButton class.
		/// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSButton(KryptonInternalKCT internalKCT,
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
                return (InternalKCT.InternalButtonCheckedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalButtonCheckedGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalButtonCheckedGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalButtonCheckedHighlight == Color.Empty) &&
                       (InternalKCT.InternalButtonCheckedHighlightBorder == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedBorder == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedHighlight == Color.Empty) &&
                       (InternalKCT.InternalButtonPressedHighlightBorder == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedBorder == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedHighlight == Color.Empty) &&
                       (InternalKCT.InternalButtonSelectedHighlightBorder == Color.Empty) &&
                       (InternalKCT.InternalCheckBackground == Color.Empty) &&
                       (InternalKCT.InternalCheckPressedBackground == Color.Empty) &&
                       (InternalKCT.InternalCheckSelectedBackground == Color.Empty) &&
                       (InternalKCT.InternalOverflowButtonGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalOverflowButtonGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalOverflowButtonGradientMiddle == Color.Empty);
			}
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            ButtonCheckedGradientBegin = InternalKCT.ButtonCheckedGradientBegin;
            ButtonCheckedGradientEnd = InternalKCT.ButtonCheckedGradientEnd;
            ButtonCheckedGradientMiddle = InternalKCT.ButtonCheckedGradientMiddle;
            ButtonCheckedHighlight = InternalKCT.ButtonCheckedHighlight;
            ButtonCheckedHighlightBorder = InternalKCT.ButtonCheckedHighlightBorder;
            ButtonPressedBorder = InternalKCT.ButtonPressedBorder;
            ButtonPressedGradientBegin = InternalKCT.ButtonPressedGradientBegin;
            ButtonPressedGradientEnd = InternalKCT.ButtonPressedGradientEnd;
            ButtonPressedGradientMiddle = InternalKCT.ButtonPressedGradientMiddle;
            ButtonPressedHighlight = InternalKCT.ButtonPressedHighlight;
            ButtonPressedHighlightBorder = InternalKCT.ButtonPressedHighlightBorder;
            ButtonSelectedBorder = InternalKCT.ButtonSelectedBorder;
            ButtonSelectedGradientBegin = InternalKCT.ButtonSelectedGradientBegin;
            ButtonSelectedGradientEnd = InternalKCT.ButtonSelectedGradientEnd;
            ButtonSelectedGradientMiddle = InternalKCT.ButtonSelectedGradientMiddle;
            ButtonSelectedHighlight = InternalKCT.ButtonSelectedHighlight;
            ButtonSelectedHighlightBorder = InternalKCT.ButtonSelectedHighlightBorder;
            CheckBackground = InternalKCT.CheckBackground;
            CheckPressedBackground = InternalKCT.CheckPressedBackground;
            CheckSelectedBackground = InternalKCT.CheckSelectedBackground;
            OverflowButtonGradientBegin = InternalKCT.OverflowButtonGradientBegin;
            OverflowButtonGradientEnd = InternalKCT.OverflowButtonGradientEnd;
            OverflowButtonGradientMiddle = InternalKCT.OverflowButtonGradientMiddle;
        }
        #endregion

        #region ButtonCheckedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used when the button is checked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used when the button is checked.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonCheckedGradientBegin
        {
            get { return InternalKCT.InternalButtonCheckedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalButtonCheckedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonCheckedGradientBegin property to its default value.
        /// </summary>
        public void ResetButtonCheckedGradientBegin()
        {
            ButtonCheckedGradientBegin = Color.Empty;
        }
        #endregion

        #region ButtonCheckedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used when the button is checked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used when the button is checked.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonCheckedGradientEnd
        {
            get { return InternalKCT.InternalButtonCheckedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalButtonCheckedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonCheckedGradientEnd property to its default value.
        /// </summary>
        public void ResetButtonCheckedGradientEnd()
        {
            ButtonCheckedGradientEnd = Color.Empty;
        }
        #endregion

        #region ButtonCheckedGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used when the button is checked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle color of the gradient used when the button is checked.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonCheckedGradientMiddle
        {
            get { return InternalKCT.InternalButtonCheckedGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalButtonCheckedGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonCheckedGradientMiddle property to its default value.
        /// </summary>
        public void ResetButtonCheckedGradientMiddle()
        {
            ButtonCheckedGradientMiddle = Color.Empty;
        }
        #endregion

        #region ButtonCheckedHighlight
        /// <summary>
        /// Gets and sets the highlight color used when the button is checked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Highlight color used when the button is checked.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonCheckedHighlight
        {
            get { return InternalKCT.InternalButtonCheckedHighlight; }
            
            set 
            { 
                InternalKCT.InternalButtonCheckedHighlight = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonCheckedHighlight property to its default value.
        /// </summary>
        public void ResetButtonCheckedHighlight()
        {
            ButtonCheckedHighlight = Color.Empty;
        }
        #endregion

        #region ButtonCheckedHighlightBorder
        /// <summary>
        /// Gets and sets the border color to use with ButtonCheckedHighlight.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with ButtonCheckedHighlight.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonCheckedHighlightBorder
        {
            get { return InternalKCT.InternalButtonCheckedHighlightBorder; }
            
            set 
            { 
                InternalKCT.InternalButtonCheckedHighlightBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonCheckedHighlightBorder property to its default value.
        /// </summary>
        public void ResetButtonCheckedHighlightBorder()
        {
            ButtonCheckedHighlightBorder = Color.Empty;
        }
        #endregion

        #region ButtonPressedBorder
        /// <summary>
        /// Gets and sets the border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedBorder
        {
            get { return InternalKCT.InternalButtonPressedBorder; }
            
            set 
            { 
                InternalKCT.InternalButtonPressedBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedBorder property to its default value.
        /// </summary>
        public void ResetButtonPressedBorder()
        {
            ButtonPressedBorder = Color.Empty;
        }
        #endregion

        #region ButtonPressedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used when the button is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used when the button is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedGradientBegin
        {
            get { return InternalKCT.InternalButtonPressedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalButtonPressedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedGradientBegin property to its default value.
        /// </summary>
        public void ResetButtonPressedGradientBegin()
        {
            ButtonPressedGradientBegin = Color.Empty;
        }
        #endregion

        #region ButtonPressedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used when the button is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used when the button is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedGradientEnd
        {
            get { return InternalKCT.InternalButtonPressedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalButtonPressedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedGradientEnd property to its default value.
        /// </summary>
        public void ResetButtonPressedGradientEnd()
        {
            ButtonPressedGradientEnd = Color.Empty;
        }
        #endregion

        #region ButtonPressedGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used when the button is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle olor of the gradient used when the button is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedGradientMiddle
        {
            get { return InternalKCT.InternalButtonPressedGradientMiddle; }
           
            set 
            { 
                InternalKCT.InternalButtonPressedGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedGradientMiddle property to its default value.
        /// </summary>
        public void ResetButtonPressedGradientMiddle()
        {
            ButtonPressedGradientMiddle = Color.Empty;
        }
        #endregion

        #region ButtonPressedHighlight
        /// <summary>
        /// Gets and sets the solid color used when the button is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color used when the button is pressed.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedHighlight
        {
            get { return InternalKCT.InternalButtonPressedHighlight; }
            
            set 
            { 
                InternalKCT.InternalButtonPressedHighlight = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedHighlight property to its default value.
        /// </summary>
        public void ResetButtonPressedHighlight()
        {
            ButtonPressedHighlight = Color.Empty;
        }
        #endregion

        #region ButtonPressedHighlightBorder
        /// <summary>
        /// Gets and sets the border color to use with ButtonPressedHighlight.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with ButtonPressedHighlight.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonPressedHighlightBorder
        {
            get { return InternalKCT.InternalButtonPressedHighlightBorder; }
            
            set 
            { 
                InternalKCT.InternalButtonPressedHighlightBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonPressedHighlightBorder property to its default value.
        /// </summary>
        public void ResetButtonPressedHighlightBorder()
        {
            ButtonPressedHighlightBorder = Color.Empty;
        }
        #endregion

        #region ButtonSelectedBorder
        /// <summary>
        /// Gets and sets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedBorder
        {
            get { return InternalKCT.InternalButtonSelectedBorder; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedBorder property to its default value.
        /// </summary>
        public void ResetButtonSelectedBorder()
        {
            ButtonSelectedBorder = Color.Empty;
        }
        #endregion

        #region ButtonSelectedGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used when the button is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used when the button is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedGradientBegin
        {
            get { return InternalKCT.InternalButtonSelectedGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedGradientBegin property to its default value.
        /// </summary>
        public void ResetButtonSelectedGradientBegin()
        {
            ButtonSelectedGradientBegin = Color.Empty;
        }
        #endregion

        #region ButtonSelectedGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used when the button is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used when the button is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedGradientEnd
        {
            get { return InternalKCT.InternalButtonSelectedGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedGradientEnd property to its default value.
        /// </summary>
        public void ResetButtonSelectedGradientEnd()
        {
            ButtonSelectedGradientEnd = Color.Empty;
        }
        #endregion

        #region ButtonSelectedGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used when the button is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle olor of the gradient used when the button is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedGradientMiddle
        {
            get { return InternalKCT.InternalButtonSelectedGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedGradientMiddle property to its default value.
        /// </summary>
        public void ResetButtonSelectedGradientMiddle()
        {
            ButtonSelectedGradientMiddle = Color.Empty;
        }
        #endregion

        #region ButtonSelectedHighlight
        /// <summary>
        /// Gets and sets the solid color used when the button is selected.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color used when the button is selected.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedHighlight
        {
            get { return InternalKCT.InternalButtonSelectedHighlight; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedHighlight = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedHighlight property to its default value.
        /// </summary>
        public void ResetButtonSelectedHighlight()
        {
            ButtonSelectedHighlight = Color.Empty;
        }
        #endregion

        #region ButtonSelectedHighlightBorder
        /// <summary>
        /// Gets and sets the border color to use with ButtonSelectedHighlight.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use with ButtonSelectedHighlight.")]
        [KryptonDefaultColorAttribute()]
        public Color ButtonSelectedHighlightBorder
        {
            get { return InternalKCT.InternalButtonSelectedHighlightBorder; }
            
            set 
            { 
                InternalKCT.InternalButtonSelectedHighlightBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the ButtonSelectedHighlightBorder property to its default value.
        /// </summary>
        public void ResetButtonSelectedHighlightBorder()
        {
            ButtonSelectedHighlightBorder = Color.Empty;
        }
        #endregion

        #region CheckBackground
        /// <summary>
        /// Gets and sets the solid color to use when the button is checked and gradients are being used.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color to use when the button is checked and gradients are being used.")]
        [KryptonDefaultColorAttribute()]
        public Color CheckBackground
        {
            get { return InternalKCT.InternalCheckBackground; }
            
            set 
            { 
                InternalKCT.InternalCheckBackground = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the CheckBackground property to its default value.
        /// </summary>
        public void ResetCheckBackground()
        {
            CheckBackground = Color.Empty;
        }
        #endregion

        #region CheckPressedBackground
        /// <summary>
        /// Gets and sets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color to use when the button is checked and selected and gradients are being used.")]
        [KryptonDefaultColorAttribute()]
        public Color CheckPressedBackground
        {
            get { return InternalKCT.InternalCheckPressedBackground; }
            
            set 
            { 
                InternalKCT.InternalCheckPressedBackground = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the CheckPressedBackground property to its default value.
        /// </summary>
        public void ResetCheckPressedBackground()
        {
            CheckPressedBackground = Color.Empty;
        }
        #endregion

        #region CheckSelectedBackground
        /// <summary>
        /// Gets and sets the solid color to use when the button is checked and selected and gradients are being used.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid color to use when the button is checked and selected and gradients are being used.")]
        [KryptonDefaultColorAttribute()]
        public Color CheckSelectedBackground
        {
            get { return InternalKCT.InternalCheckSelectedBackground; }
            
            set 
            { 
                InternalKCT.InternalCheckSelectedBackground = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the CheckSelectedBackground property to its default value.
        /// </summary>
        public void ResetCheckSelectedBackground()
        {
            CheckSelectedBackground = Color.Empty;
        }
        #endregion

        #region OverflowButtonGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the ToolStripOverflowButton.")]
        [KryptonDefaultColorAttribute()]
        public Color OverflowButtonGradientBegin
        {
            get { return InternalKCT.InternalOverflowButtonGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalOverflowButtonGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the OverflowButtonGradientBegin property to its default value.
        /// </summary>
        public void ResetOverflowButtonGradientBegin()
        {
            OverflowButtonGradientBegin = Color.Empty;
        }
        #endregion

        #region OverflowButtonGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStripOverflowButton.")]
        [KryptonDefaultColorAttribute()]
        public Color OverflowButtonGradientEnd
        {
            get { return InternalKCT.InternalOverflowButtonGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalOverflowButtonGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the OverflowButtonGradientEnd property to its default value.
        /// </summary>
        public void ResetOverflowButtonGradientEnd()
        {
            OverflowButtonGradientEnd = Color.Empty;
        }
        #endregion

        #region OverflowButtonGradientMiddle
        /// <summary>
        /// Gets and sets the middle color of the gradient used in the ToolStripOverflowButton.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Middle color of the gradient used in the ToolStripOverflowButton.")]
        [KryptonDefaultColorAttribute()]
        public Color OverflowButtonGradientMiddle
        {
            get { return InternalKCT.InternalOverflowButtonGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalOverflowButtonGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the OverflowButtonGradientMiddle property to its default value.
        /// </summary>
        public void ResetOverflowButtonGradientMiddle()
        {
            OverflowButtonGradientMiddle = Color.Empty;
        }
        #endregion
    }
}
