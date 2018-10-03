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
    /// Provide wrap label state storage.
    /// </summary>
    public class PaletteWrapLabel : Storage
    {
        #region Instance Fields
        private Font _font;
        private Color _textColor;
        private PaletteTextHint _hint;
        private KryptonWrapLabel _wrapLabel;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteWrapLabel class.
        /// </summary>
        /// <param name="wrapLabel">Reference to owning control.</param>
        public PaletteWrapLabel(KryptonWrapLabel wrapLabel)
        {
            _wrapLabel = wrapLabel;
            _font = null;
            _textColor = Color.Empty;
            _hint = PaletteTextHint.Inherit;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get { return (_font == null) && 
                         (_textColor == Color.Empty) && 
                         (_hint == PaletteTextHint.Inherit); }
        }
        #endregion

        #region Font
		/// <summary>
		/// Gets the font for label text.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Font for drawing the label text.")]
		[DefaultValue(null)]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public virtual Font Font
		{
            get { return _font; }

            set
            {
                _font = value;
                _wrapLabel.PerformLayout();
                _wrapLabel.Invalidate();
            }
        }
        #endregion

        #region TextColor
        /// <summary>
        /// Gets and sets the olor for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color for the text.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Color TextColor
        {
            get { return _textColor; }

            set
            {
                _textColor = value;
                _wrapLabel.Invalidate();
            }
        }
        #endregion

		#region Hint
		/// <summary>
		/// Gets the text rendering hint for the text.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Text rendering hint for the content text.")]
        [DefaultValue(typeof(PaletteTextHint), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual PaletteTextHint Hint
        {
            get { return _hint; }

            set
            {
                _hint = value;
                _wrapLabel.Invalidate();
            }
        }
        #endregion
    }
}
