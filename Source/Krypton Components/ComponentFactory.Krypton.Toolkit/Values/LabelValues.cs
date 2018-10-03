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
	/// Storage for label content value information.
	/// </summary>
	public class LabelValues : Storage,
							   IContentValues
	{
		#region Static Fields
		private const string _defaultText = "Label";
        private static readonly string _defaultExtraText = string.Empty;
		#endregion

		#region Instance Fields
        private Image _image;
        private Color _transparent;
		private string _text;
		private string _extraText;
		#endregion

        #region Events
        /// <summary>
        /// Occures when the value of the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the LabelValues class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public LabelValues(NeedPaintHandler needPaint)
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Set initial values
            _image = null;
            _transparent = Color.Empty;
			_text = _defaultText;
			_extraText = _defaultExtraText;
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
                return ((Image == null) &&
                        (ImageTransparentColor == Color.Empty) &&
						(Text == _defaultText) &&
						(ExtraText == _defaultExtraText));
			}
		}
		#endregion

		#region Image
		/// <summary>
		/// Gets and sets the label image.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Label image.")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public Image Image
		{
			get { return _image; }

			set
			{
				if (_image != value)
				{
					_image = value;
					PerformNeedPaint(true);
				}
			}
		}

		private bool ShouldSerializeImage()
		{
			return Image != null;
		}

		/// <summary>
		/// Resets the Image property to its default value.
		/// </summary>
		public void ResetImage()
		{
			Image = null;
		}

		/// <summary>
		/// Gets the content image.
		/// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
		{
			return Image;
		}
		#endregion

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the label image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Label image transparent color.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [KryptonDefaultColorAttribute()]
        public Color ImageTransparentColor
        {
            get { return _transparent; }

            set
            {
                if (_transparent != value)
                {
                    _transparent = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor()
        {
            return ImageTransparentColor != Color.Empty;
        }

        /// <summary>
        /// Resets the ImageTransparentColor property to its default value.
        /// </summary>
        public void ResetImageTransparentColor()
        {
            ImageTransparentColor = Color.Empty;
        }

        /// <summary>
        /// Gets the content image transparent color.
        /// </summary>
        /// <param name="state">The state for which the image color is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return ImageTransparentColor;
        }
        #endregion

		#region Text
		/// <summary>
		/// Gets and sets the label text.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Label text.")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text
		{
			get { return _text; }

			set
			{
				if (_text != value)
				{
					_text = value;
					PerformNeedPaint(true);
                    if (TextChanged != null)
                        TextChanged(this, EventArgs.Empty);
				}
			}
		}

		private bool ShouldSerializeText()
		{
			return Text != _defaultText;
		}

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public void ResetText()
		{
			Text = _defaultText;
		}

		/// <summary>
		/// Gets the content short text.
		/// </summary>
		public string GetShortText()
		{
			return Text;
		}
		#endregion

		#region ExtraText
		/// <summary>
		/// Gets and sets the label extra text.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Label extra text.")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ExtraText
		{
			get { return _extraText; }

			set
			{
				if (_extraText != value)
				{
					_extraText = value;
					PerformNeedPaint(true);
				}
			}
		}

		private bool ShouldSerializeExtraText()
		{
			return ExtraText != _defaultExtraText;
		}

		/// <summary>
		/// Resets the Description property to its default value.
		/// </summary>
		public void ResetExtraText()
		{
			ExtraText = ExtraText;
		}

		/// <summary>
		/// Gets the content long text.
		/// </summary>
		public string GetLongText()
		{
			return ExtraText;
		}
		#endregion
	}
}
