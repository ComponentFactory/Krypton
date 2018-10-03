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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement storage for palette background details.
	/// </summary>
    public class PaletteBackColor1 : PaletteBack
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBackColor1 class.
		/// </summary>
		/// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBackColor1(IPaletteBack inherit,
                                 NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
        }
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public new void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Color1 = GetBackColor1(state);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        [Browsable(false)]
		public new InheritBool Draw
		{
            get { return base.Draw; }
            set { base.Draw = value; }
		}
		#endregion

		#region GraphicsHint
		/// <summary>
		/// Gets the graphics hint for drawing the background.
		/// </summary>
        [Browsable(false)]
        public new PaletteGraphicsHint GraphicsHint
		{
            get { return base.GraphicsHint; }
            set { base.GraphicsHint = value; }
		}
		#endregion

		#region Color2
		/// <summary>
		/// Gets and sets the second background color.
		/// </summary>
        [Browsable(false)]
        public new Color Color2
		{
            get { return base.Color2; }
            set { base.Color2 = value; }
		}
		#endregion

		#region ColorStyle
		/// <summary>
		/// Gets and sets the color drawing style.
		/// </summary>
        [Browsable(false)]
        public new PaletteColorStyle ColorStyle
		{
            get { return base.ColorStyle; }
            set { base.ColorStyle = value; }
		}
		#endregion

		#region ColorAlign
		/// <summary>
		/// Gets and set the color alignment.
		/// </summary>
        [Browsable(false)]
        public new PaletteRectangleAlign ColorAlign
		{
            get { return base.ColorAlign; }
            set { base.ColorAlign = value; }
		}
		#endregion

		#region ColorAngle
		/// <summary>
		/// Gets and sets the color angle.
		/// </summary>
        [Browsable(false)]
        public new float ColorAngle
		{
            get { return base.ColorAngle; }
            set { base.ColorAngle = value; }
		}
		#endregion

		#region Image
		/// <summary>
		/// Gets and sets the background image.
		/// </summary>
        [Browsable(false)]
        public new Image Image
		{
            get { return base.Image; }
            set { base.Image = value; }
		}
		#endregion

		#region ImageStyle
		/// <summary>
		/// Gets and sets the background image style.
		/// </summary>
        [Browsable(false)]
        public new PaletteImageStyle ImageStyle
		{
            get { return base.ImageStyle; }
            set { base.ImageStyle = value; }
		}
		#endregion

		#region ImageAlign
		/// <summary>
		/// Gets and set the image alignment.
		/// </summary>
        [Browsable(false)]
        public new PaletteRectangleAlign ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }
        #endregion
    }
}
