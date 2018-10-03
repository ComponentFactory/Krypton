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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Implement storage for palette content text details.
	/// </summary>
    public class PaletteNavContentText : PaletteContentText
	{
		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteNavContentText class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavContentText(NeedPaintHandler needPaint)
            : base(needPaint)
		{
        }
		#endregion

		#region Font
		/// <summary>
		/// Gets the font for the text.
		/// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
            get { return base.Font; }
            set { base.Font = value; }
		}
		#endregion

		#region Hint
		/// <summary>
		/// Gets the text rendering hint for the text.
		/// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteTextHint Hint
		{
            get { return base.Hint; }
            set { base.Hint = value; }
		}
		#endregion

        #region Color1
        /// <summary>
        /// Gets and sets the first color for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color Color1
        {
            get { return base.Color1; }
            set { base.Color1 = value; }
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets and sets the second color for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color Color2
        {
            get { return base.Color2; }
            set { base.Color2 = value; }
        }
        #endregion

        #region ColorStyle
        /// <summary>
        /// Gets and sets the color drawing style for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteColorStyle ColorStyle
        {
            get { return base.ColorStyle; }
            set { base.ColorStyle = value; }
        }
        #endregion

        #region ColorAlign
        /// <summary>
        /// Gets and set the color alignment for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteRectangleAlign ColorAlign
        {
            get { return base.ColorAlign; }
            set { base.ColorAlign = value; }
        }
        #endregion

        #region ColorAngle
        /// <summary>
        /// Gets and sets the color angle for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float ColorAngle
        {
            get { return base.ColorAngle; }
            set { base.ColorAngle = value; }
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the image for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image Image
        {
            get { return base.Image; }
            set { base.Image = value; }
        }
        #endregion

        #region ImageStyle
        /// <summary>
        /// Gets and sets the image style for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteImageStyle ImageStyle
        {
            get { return base.ImageStyle; }
            set { base.ImageStyle = value; }
        }
        #endregion

        #region ImageAlign
        /// <summary>
        /// Gets and set the image alignment for the text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteRectangleAlign ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }
        #endregion
    }
}
