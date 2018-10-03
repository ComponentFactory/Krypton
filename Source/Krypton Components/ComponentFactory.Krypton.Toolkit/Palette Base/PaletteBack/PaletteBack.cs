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
	public class PaletteBack : Storage,
							   IPaletteBack
    {
        #region Internal Classes
        private class InternalStorage
        {
            public InheritBool BackDraw;
            public PaletteGraphicsHint BackGraphicsHint;
            public Color BackColor1;
            public Color BackColor2;
            public PaletteColorStyle BackColorStyle;
            public PaletteRectangleAlign BackColorAlign;
            public float BackColorAngle;
            public Image BackImage;
            public PaletteImageStyle BackImageStyle;
            public PaletteRectangleAlign BackImageAlign;

            /// <summary>
            /// Initialize a new instance of the InternalStorage structure.
            /// </summary>
            public InternalStorage()
            {
                // Set to default values
                BackDraw = InheritBool.Inherit;
                BackGraphicsHint = PaletteGraphicsHint.Inherit;
                BackColor1 = Color.Empty;
                BackColor2 = Color.Empty;
                BackColorStyle = PaletteColorStyle.Inherit;
                BackColorAlign = PaletteRectangleAlign.Inherit;
                BackColorAngle = -1;
                BackImageStyle = PaletteImageStyle.Inherit;
                BackImageAlign = PaletteRectangleAlign.Inherit;
            }

            /// <summary>
		    /// Gets a value indicating if all values are default.
		    /// </summary>
            public bool IsDefault
            {
                get
                {
                    return (BackDraw == InheritBool.Inherit) &&
                           (BackGraphicsHint == PaletteGraphicsHint.Inherit) &&
                           (BackColor1 == Color.Empty) &&
                           (BackColor2 == Color.Empty) &&
                           (BackColorStyle == PaletteColorStyle.Inherit) &&
                           (BackColorAlign == PaletteRectangleAlign.Inherit) &&
                           (BackColorAngle == -1) &&
                           (BackImage == null) &&
                           (BackImageStyle == PaletteImageStyle.Inherit) &&
                           (BackImageAlign == PaletteRectangleAlign.Inherit);
                }
            }
        }
        #endregion

        #region Instance Fields
        private IPaletteBack _inherit;
        private InternalStorage _storage;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the PaletteBack class.
		/// </summary>
		/// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBack(IPaletteBack inherit,
                           NeedPaintHandler needPaint)
		{
			// Remember inheritance
			_inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;
        }
		#endregion

        #region IsDefault
        /// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get { return (_storage == null) || _storage.IsDefault; }
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteBack inherit)
        {
            _inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Draw = GetBackDraw(state);
            GraphicsHint = GetBackGraphicsHint(state);
            Color1 = GetBackColor1(state);
            Color2 = GetBackColor2(state);
            ColorStyle = GetBackColorStyle(state);
            ColorAlign = GetBackColorAlign(state);
            ColorAngle = GetBackColorAngle(state);
            Image = GetBackImage(state);
            ImageStyle = GetBackImageStyle(state);
            ImageAlign = GetBackImageAlign(state);
        }
        #endregion

        #region Draw
        /// <summary>
		/// Gets a value indicating if background should be drawn.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Should background be drawn.")]
		[DefaultValue(typeof(InheritBool), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public InheritBool Draw
		{
			get 
            {
                if (_storage == null)
                    return InheritBool.Inherit;
                else
                    return _storage.BackDraw; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackDraw != value)
                    {
                        _storage.BackDraw = value;
                        OnPropertyChanged("Draw");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != InheritBool.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackDraw = value;
                        OnPropertyChanged("Draw");
                        PerformNeedPaint();
                    }
                }
			}
		}

		/// <summary>
		/// Gets the actual background draw value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetBackDraw(PaletteState state)
		{
			if (Draw != InheritBool.Inherit)
				return Draw;
			else
				return _inherit.GetBackDraw(state);
		}
		#endregion

		#region GraphicsHint
		/// <summary>
		/// Gets the graphics hint for drawing the background.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Hint for drawing graphics.")]
		[DefaultValue(typeof(PaletteGraphicsHint), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public PaletteGraphicsHint GraphicsHint
		{
			get 
            {
                if (_storage == null)
                    return PaletteGraphicsHint.Inherit;
                else
                    return _storage.BackGraphicsHint; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackGraphicsHint != value)
                    {
                        _storage.BackGraphicsHint = value;
                        OnPropertyChanged("GraphicsHint");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteGraphicsHint.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackGraphicsHint = value;
                        OnPropertyChanged("GraphicsHint");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the actual background graphics hint value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
		{
			if (GraphicsHint != PaletteGraphicsHint.Inherit)
				return GraphicsHint;
			else
				return _inherit.GetBackGraphicsHint(state);
		}
		#endregion

		#region Color1
		/// <summary>
		/// Gets and sets the first background color.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Main background color.")]
        [KryptonDefaultColorAttribute()]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public Color Color1
		{
			get 
            {
                if (_storage == null)
                    return Color.Empty;
                else
                    return _storage.BackColor1; 
            }
			
			set 
			{
                if (_storage != null)
                {
                    if (_storage.BackColor1 != value)
                    {
                        _storage.BackColor1 = value;
                        OnPropertyChanged("Color1");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage();
                        _storage.BackColor1 = value;
                        OnPropertyChanged("Color1");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public Color GetBackColor1(PaletteState state)
		{
			if (Color1 != Color.Empty)
				return Color1;
			else
				return _inherit.GetBackColor1(state);
		}
		#endregion

		#region Color2
		/// <summary>
		/// Gets and sets the second background color.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Secondary background color.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
		public Color Color2
		{
			get 
            {
                if (_storage == null)
                    return Color.Empty;
                else
                    return _storage.BackColor2; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackColor2 != value)
                    {
                        _storage.BackColor2 = value;
                        OnPropertyChanged("Color2");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != Color.Empty)
                    {
                        _storage = new InternalStorage();
                        _storage.BackColor2 = value;
                        OnPropertyChanged("Color2");
                        PerformNeedPaint();
                    }
                }
			}
		}

		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public Color GetBackColor2(PaletteState state)
		{
			if (Color2 != Color.Empty)
				return Color2;
			else
				return _inherit.GetBackColor2(state);
		}
		#endregion

		#region ColorStyle
		/// <summary>
		/// Gets and sets the color drawing style.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background color drawing style.")]
		[DefaultValue(typeof(PaletteColorStyle), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public PaletteColorStyle ColorStyle
		{
			get 
            {
                if (_storage == null)
                    return PaletteColorStyle.Inherit;
                else
                    return _storage.BackColorStyle; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackColorStyle != value)
                    {
                        _storage.BackColorStyle = value;
                        OnPropertyChanged("ColorStyle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteColorStyle.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackColorStyle = value;
                        OnPropertyChanged("ColorStyle");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the color drawing style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public PaletteColorStyle GetBackColorStyle(PaletteState state)
		{
			if (ColorStyle != PaletteColorStyle.Inherit)
				return ColorStyle;
			else
				return _inherit.GetBackColorStyle(state);
		}
		#endregion

		#region ColorAlign
		/// <summary>
		/// Gets and set the color alignment.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background color alignment style.")]
		[DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public PaletteRectangleAlign ColorAlign
		{
			get 
            {
                if (_storage == null)
                    return PaletteRectangleAlign.Inherit;
                else
                    return _storage.BackColorAlign; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackColorAlign != value)
                    {
                        _storage.BackColorAlign = value;
                        OnPropertyChanged("ColorAlign");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteRectangleAlign.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackColorAlign = value;
                        OnPropertyChanged("ColorAlign");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the color alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
		{
			if (ColorAlign != PaletteRectangleAlign.Inherit)
				return ColorAlign;
			else
				return _inherit.GetBackColorAlign(state);
		}
		#endregion

		#region ColorAngle
		/// <summary>
		/// Gets and sets the color angle.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background color angle.")]
		[DefaultValue(-1f)]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public float ColorAngle
		{
			get 
            {
                if (_storage == null)
                    return -1;
                else
                    return _storage.BackColorAngle; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackColorAngle != value)
                    {
                        _storage.BackColorAngle = value;
                        OnPropertyChanged("ColorAngle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != -1)
                    {
                        _storage = new InternalStorage();
                        _storage.BackColorAngle = value;
                        OnPropertyChanged("ColorAngle");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public float GetBackColorAngle(PaletteState state)
		{
			if (ColorAngle != -1)
				return ColorAngle;
			else
				return _inherit.GetBackColorAngle(state);
		}
		#endregion

		#region Image
		/// <summary>
		/// Gets and sets the background image.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background image.")]
		[DefaultValue(null)]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public Image Image
		{
			get 
            {
                if (_storage == null)
                    return null;
                else
                    return _storage.BackImage; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackImage != value)
                    {
                        _storage.BackImage = value;
                        OnPropertyChanged("Image");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != null)
                    {
                        _storage = new InternalStorage();
                        _storage.BackImage = value;
                        OnPropertyChanged("Image");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public Image GetBackImage(PaletteState state)
		{
			if (Image != null)
				return Image;
			else
				return _inherit.GetBackImage(state);
		}
		#endregion

		#region ImageStyle
		/// <summary>
		/// Gets and sets the background image style.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background image style.")]
		[DefaultValue(typeof(PaletteImageStyle), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public PaletteImageStyle ImageStyle
		{
			get 
            {
                if (_storage == null)
                    return PaletteImageStyle.Inherit;
                else
                    return _storage.BackImageStyle; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackImageStyle != value)
                    {
                        _storage.BackImageStyle = value;
                        OnPropertyChanged("ImageStyle");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteImageStyle.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackImageStyle = value;
                        OnPropertyChanged("ImageStyle");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public PaletteImageStyle GetBackImageStyle(PaletteState state)
		{
			if (ImageStyle != PaletteImageStyle.Inherit)
				return ImageStyle;
			else
				return _inherit.GetBackImageStyle(state);
		}
		#endregion

		#region ImageAlign
		/// <summary>
		/// Gets and set the image alignment.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Background image alignment style.")]
		[DefaultValue(typeof(PaletteRectangleAlign), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public PaletteRectangleAlign ImageAlign
		{
			get 
            {
                if (_storage == null)
                    return PaletteRectangleAlign.Inherit;
                else
                    return _storage.BackImageAlign; 
            }

			set
			{
                if (_storage != null)
                {
                    if (_storage.BackImageAlign != value)
                    {
                        _storage.BackImageAlign = value;
                        OnPropertyChanged("ImageAlign");
                        PerformNeedPaint();
                    }
                }
                else
                {
                    if (value != PaletteRectangleAlign.Inherit)
                    {
                        _storage = new InternalStorage();
                        _storage.BackImageAlign = value;
                        OnPropertyChanged("ImageAlign");
                        PerformNeedPaint();
                    }
                }
            }
		}

		/// <summary>
		/// Gets the image alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
		{
			if (ImageAlign != PaletteRectangleAlign.Inherit)
				return ImageAlign;
			else
				return _inherit.GetBackImageAlign(state);
		}
		#endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of the property changed.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
