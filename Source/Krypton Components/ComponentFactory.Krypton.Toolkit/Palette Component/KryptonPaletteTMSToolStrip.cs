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
	/// Storage for tool strip entries of the professional color table.
	/// </summary>
    public class KryptonPaletteTMSToolStrip : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTToolStrip class.
		/// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSToolStrip(KryptonInternalKCT internalKCT,
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
                return (InternalKCT.InternalToolStripText == Color.Empty) &&
                       (InternalKCT.InternalToolStripFont == null) &&
                       (InternalKCT.InternalToolStripBorder == Color.Empty) &&
                       (InternalKCT.InternalToolStripContentPanelGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalToolStripContentPanelGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalToolStripDropDownBackground == Color.Empty) &&
                       (InternalKCT.InternalToolStripGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalToolStripGradientEnd == Color.Empty) &&
                       (InternalKCT.InternalToolStripGradientMiddle == Color.Empty) &&
                       (InternalKCT.InternalToolStripPanelGradientBegin == Color.Empty) &&
                       (InternalKCT.InternalToolStripPanelGradientEnd == Color.Empty);
			}
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            ToolStripText = InternalKCT.ToolStripText;
            ToolStripFont = InternalKCT.ToolStripFont;
            ToolStripBorder = InternalKCT.ToolStripBorder;
            ToolStripContentPanelGradientBegin = InternalKCT.ToolStripContentPanelGradientBegin;
            ToolStripContentPanelGradientEnd = InternalKCT.ToolStripContentPanelGradientEnd;
            ToolStripDropDownBackground = InternalKCT.ToolStripDropDownBackground;
            ToolStripGradientBegin = InternalKCT.ToolStripGradientBegin;
            ToolStripGradientEnd = InternalKCT.ToolStripGradientEnd;
            ToolStripGradientMiddle = InternalKCT.ToolStripGradientMiddle;
            ToolStripPanelGradientBegin = InternalKCT.ToolStripPanelGradientBegin;
            ToolStripPanelGradientEnd = InternalKCT.ToolStripPanelGradientEnd;
        }
        #endregion

        #region ToolStripText
        /// <summary>
        /// Gets and sets the color to draw text on the tool strip.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to draw text on the ToolStrip.")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripText
        {
            get { return InternalKCT.InternalToolStripText; }

            set
            {
                InternalKCT.InternalToolStripText = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripText property to its default value.
        /// </summary>
        public void ResetToolStripText()
        {
            ToolStripText = Color.Empty;
        }
        #endregion

        #region ToolStripFont
        /// <summary>
        /// Gets and sets the font to draw text on the tool strip.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Font to draw text on the ToolStrip.")]
        [DefaultValue(null)]
        public Font ToolStripFont
        {
            get { return InternalKCT.InternalToolStripFont; }

            set
            {
                InternalKCT.InternalToolStripFont = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripFont property to its default value.
        /// </summary>
        public void ResetToolStripFont()
        {
            ToolStripFont = null;
        }
        #endregion

        #region ToolStripBorder
        /// <summary>
        /// Gets and sets the border color to use on the bottom edge of the ToolStrip.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Border color to use on the bottom edge of the ToolStrip.")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripBorder
        {
            get { return InternalKCT.InternalToolStripBorder; }
            
            set 
            { 
                InternalKCT.InternalToolStripBorder = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripBorder property to its default value.
        /// </summary>
        public void ResetToolStripBorder()
        {
            ToolStripBorder = Color.Empty;
        }
        #endregion

        #region ToolStripContentPanelGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the ToolStripContentPanel..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the ToolStripContentPanel..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripContentPanelGradientBegin
        {
            get { return InternalKCT.InternalToolStripContentPanelGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalToolStripContentPanelGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripContentPanelGradientBegin property to its default value.
        /// </summary>
        public void ResetToolStripContentPanelGradientBegin()
        {
            ToolStripContentPanelGradientBegin = Color.Empty;
        }
        #endregion

        #region ToolStripContentPanelGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStripContentPanel.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStripContentPanel.")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripContentPanelGradientEnd
        {
            get { return InternalKCT.InternalToolStripContentPanelGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalToolStripContentPanelGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripContentPanelGradientEnd property to its default value.
        /// </summary>
        public void ResetToolStripContentPanelGradientEnd()
        {
            ToolStripContentPanelGradientEnd = Color.Empty;
        }
        #endregion

        #region ToolStripDropDownBackground
        /// <summary>
        /// Gets and sets the solid background color of the ToolStripDropDown..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Solid background color solid background color of the ToolStripDropDown..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripDropDownBackground
        {
            get { return InternalKCT.InternalToolStripDropDownBackground; }
            
            set 
            { 
                InternalKCT.InternalToolStripDropDownBackground = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripDropDownBackground property to its default value.
        /// </summary>
        public void ResetToolStripDropDownBackground()
        {
            ToolStripDropDownBackground = Color.Empty;
        }
        #endregion

        #region ToolStripGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the ToolStrip background..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the ToolStrip background..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripGradientBegin
        {
            get { return InternalKCT.InternalToolStripGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalToolStripGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripGradientBegin property to its default value.
        /// </summary>
        public void ResetToolStripGradientBegin()
        {
            ToolStripGradientBegin = Color.Empty;
        }
        #endregion

        #region ToolStripGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStrip background..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStrip background..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripGradientEnd
        {
            get { return InternalKCT.InternalToolStripGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalToolStripGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripGradientEnd property to its default value.
        /// </summary>
        public void ResetToolStripGradientEnd()
        {
            ToolStripGradientEnd = Color.Empty;
        }
        #endregion

        #region ToolStripGradientMiddle
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStrip background..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStrip background..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripGradientMiddle
        {
            get { return InternalKCT.InternalToolStripGradientMiddle; }
            
            set 
            { 
                InternalKCT.InternalToolStripGradientMiddle = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripGradientMiddle property to its default value.
        /// </summary>
        public void ResetToolStripGradientMiddle()
        {
            ToolStripGradientMiddle = Color.Empty;
        }
        #endregion

        #region ToolStripPanelGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the ToolStripPanel..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the ToolStripPanel..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripPanelGradientBegin
        {
            get { return InternalKCT.InternalToolStripPanelGradientBegin; }
            
            set 
            { 
                InternalKCT.InternalToolStripPanelGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripPanelGradientBegin property to its default value.
        /// </summary>
        public void ResetToolStripPanelGradientBegin()
        {
            ToolStripPanelGradientBegin = Color.Empty;
        }
        #endregion

        #region ToolStripPanelGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStripPanel..
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStripPanel..")]
        [KryptonDefaultColorAttribute()]
        public Color ToolStripPanelGradientEnd
        {
            get { return InternalKCT.InternalToolStripPanelGradientEnd; }
            
            set 
            { 
                InternalKCT.InternalToolStripPanelGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the ToolStripPanelGradientEnd property to its default value.
        /// </summary>
        public void ResetToolStripPanelGradientEnd()
        {
            ToolStripPanelGradientEnd = Color.Empty;
        }
        #endregion
    }
}
