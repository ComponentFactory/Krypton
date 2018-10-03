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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage of user supplied values not used by Krypton.
	/// </summary>
    public class KryptonPaletteCargo : Storage
    {
        #region Instance Fields
        private Color _color1;
        private Color _color2;
        private Color _color3;
        private Color _color4;
        private Color _color5;
        private Font _font1;
        private Font _font2;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteCargo class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteCargo(NeedPaintHandler needPaint) 
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default cargo values
            _color1 = Color.Empty;
            _color2 = Color.Empty;
            _color3 = Color.Empty;
            _color4 = Color.Empty;
            _color5 = Color.Empty;
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
                return (_color1 == Color.Empty) &&
                       (_color2 == Color.Empty) &&
                       (_color3 == Color.Empty) &&
                       (_color4 == Color.Empty) &&
                       (_color5 == Color.Empty) &&
                       (_font1 == null) &&
                       (_font2 == null);
            }
		}
		#endregion

        #region Color1
        /// <summary>
		/// Gets and sets a user supplied color value.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("User supplied color value.")]
        [KryptonDefaultColorAttribute()]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public Color Color1
		{
            get { return _color1; }
            set { _color1 = value; }
        }

        /// <summary>
        /// esets the Color1 property to its default value.
        /// </summary>
        public void ResetColor1()
        {
            Color1 = Color.Empty;
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }

        /// <summary>
        /// esets the Color2 property to its default value.
        /// </summary>
        public void ResetColor2()
        {
            Color2 = Color.Empty;
        }
        #endregion

        #region Color3
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color3
        {
            get { return _color3; }
            set { _color3 = value; }
        }

        /// <summary>
        /// esets the Color3 property to its default value.
        /// </summary>
        public void ResetColor3()
        {
            Color3 = Color.Empty;
        }
        #endregion

        #region Color4
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color4
        {
            get { return _color4; }
            set { _color4 = value; }
        }

        /// <summary>
        /// esets the Color4 property to its default value.
        /// </summary>
        public void ResetColor4()
        {
            Color4 = Color.Empty;
        }
        #endregion

        #region Color5
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color5
        {
            get { return _color5; }
            set { _color5 = value; }
        }

        /// <summary>
        /// esets the Color5 property to its default value.
        /// </summary>
        public void ResetColor5()
        {
            Color5 = Color.Empty;
        }
        #endregion

        #region Font1
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied font value.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Font Font1
        {
            get { return _font1; }
            set { _font1 = value; }
        }

        /// <summary>
        /// esets the Font1 property to its default value.
        /// </summary>
        public void ResetFont1()
        {
            Font1 = null;
        }
        #endregion

        #region Font2
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied font value.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Font Font2
        {
            get { return _font2; }
            set { _font2 = value; }
        }

        /// <summary>
        /// esets the Font2 property to its default value.
        /// </summary>
        public void ResetFont2()
        {
            Font2 = null;
        }
        #endregion
    }
}
