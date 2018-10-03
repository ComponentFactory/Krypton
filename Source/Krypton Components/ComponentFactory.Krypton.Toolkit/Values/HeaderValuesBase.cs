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
	/// Storage for header content value information.
	/// </summary>
	public abstract class HeaderValuesBase : Storage,
											 IContentValues
	{
		#region Static Fields
        private static readonly Image _defaultImage = Properties.Resources.ComponentYellow;
		#endregion

		#region Instance Fields
        private Image _image;
        private Color _transparent;
		private string _heading;
		private string _description;
		#endregion

        #region Events
        /// <summary>
        /// Occures when the value of the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;
        #endregion
        
        #region Identity
		/// <summary>
		/// Initialize a new instance of the HeaderValuesBase class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        protected HeaderValuesBase(NeedPaintHandler needPaint)
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;
            
            // Set initial values to the default
            _image = GetImageDefault();
            _transparent = Color.Empty;
			_heading = GetHeadingDefault();
			_description = GetDescriptionDefault();
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
                return ((Image == GetImageDefault()) &&
                        (ImageTransparentColor == Color.Empty) &&
						(Heading == GetHeadingDefault()) &&
						(Description == GetDescriptionDefault()));
			}
		}
		#endregion

		#region Default Values
        /// <summary>
		/// Gets the default image value.
		/// </summary>
		/// <returns>Image reference.</returns>
		protected virtual Image GetImageDefault()
		{
			return _defaultImage;
		}

		/// <summary>
		/// Gets the default heading value.
		/// </summary>
		/// <returns>String reference.</returns>
		protected abstract string GetHeadingDefault();

		/// <summary>
		/// Gets the default description value.
		/// </summary>
		/// <returns>String reference.</returns>
		protected abstract string GetDescriptionDefault();
		#endregion

		#region Image
		/// <summary>
		/// Gets and sets the heading image.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Heading image.")]
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
			return Image != GetImageDefault();
		}

		/// <summary>
		/// Resets the Image property to its default value.
		/// </summary>
		public void ResetImage()
		{
			Image = GetImageDefault();
		}

		/// <summary>
		/// Gets the content image.
		/// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetImage(PaletteState state)
		{
			return Image;
		}
		#endregion

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the heading image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Heading image transparent color.")]
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
        public virtual Color GetImageTransparentColor(PaletteState state)
        {
            return ImageTransparentColor;
        }
        #endregion
        
        #region Heading
		/// <summary>
		/// Gets and sets the heading text.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Heading text.")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string Heading
		{
			get { return _heading; }

			set
			{
				if (_heading != value)
				{
					_heading = value;
					PerformNeedPaint(true);
                    if (TextChanged != null)
                        TextChanged(this, EventArgs.Empty);
                }
			}
		}

		private bool ShouldSerializeHeading()
		{
			return Heading != GetHeadingDefault();
		}

		/// <summary>
		/// Resets the Heading property to its default value.
		/// </summary>
		public void ResetHeading()
		{
			Heading = GetHeadingDefault();
		}

		/// <summary>
		/// Gets the content short text.
		/// </summary>
        public virtual string GetShortText()
		{
			return Heading;
		}
		#endregion

		#region Description
		/// <summary>
		/// Gets and sets the header description text.
		/// </summary>
		[Localizable(true)]
		[Category("Visuals")]
		[Description("Header description text.")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string Description
		{
			get { return _description; }

			set
			{
				if (_description != value)
				{
					_description = value;
					PerformNeedPaint(true);
				}
			}
		}

		private bool ShouldSerializeDescription()
		{
			return Description != GetDescriptionDefault();
		}

		/// <summary>
		/// Resets the Description property to its default value.
		/// </summary>
		public void ResetDescription()
		{
			Description = GetDescriptionDefault();
		}

		/// <summary>
		/// Gets the content long text.
		/// </summary>
        public virtual string GetLongText()
		{
			return Description;
		}
		#endregion
    }
}
