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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement storage for input control palette content details.
	/// </summary>
	public class PaletteInputControlContentStates : Storage,
								                    IPaletteContent
	{
        #region Instance Fields
        private IPaletteContent _inherit;
        private Font _font;
        private Color _color1;
        private Padding _padding;
        #endregion

        #region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteInputControlContentStates class.
		/// </summary>
		/// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteInputControlContentStates(IPaletteContent inherit,
                                                NeedPaintHandler needPaint)
		{
			Debug.Assert(inherit != null);

			// Remember inheritance
			_inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default the initial values
            _font = null;
            _color1 = Color.Empty;
            _padding = CommonHelper.InheritPadding;
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
                return ((Font == null) &&
                        (Color1 == Color.Empty) &&
                        Padding.Equals(CommonHelper.InheritPadding));
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteContent inherit)
        {
            _inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Font = GetContentShortTextFont(state);
            Color1 = GetContentShortTextColor1(state);
            Padding = GetContentPadding(state);
        }
        #endregion

		#region Draw
		/// <summary>
		/// Gets the actual content draw value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetContentDraw(PaletteState state)
		{
			return _inherit.GetContentDraw(state);
		}
		#endregion

        #region DrawFocus
        /// <summary>
		/// Gets the actual content draw with focus value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetContentDrawFocus(PaletteState state)
		{
			return _inherit.GetContentDrawFocus(state);
		}
		#endregion

		#region ContentImage
		/// <summary>
		/// Gets the actual content image horizontal alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentImageH(PaletteState state)
		{
			return _inherit.GetContentImageH(state);
		}

		/// <summary>
		/// Gets the actual content image vertical alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentImageV(PaletteState state)
		{
			return _inherit.GetContentImageV(state);
		}

		/// <summary>
		/// Gets the actual image drawing effect value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		public PaletteImageEffect GetContentImageEffect(PaletteState state)
		{
			return _inherit.GetContentImageEffect(state);
		}

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorMap(PaletteState state)
        {
            return _inherit.GetContentImageColorMap(state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentImageColorTo(PaletteState state)
        {
            return _inherit.GetContentImageColorTo(state);
        }
        #endregion

        #region ContentShortText
        /// <summary>
        /// Gets the font for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Font for drawing the content text.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Font Font
        {
            get { return _font; }

            set
            {
                if (value != _font)
                {
                    _font = value;
                    PerformNeedPaint(true);
                }
            }
        }

		/// <summary>
		/// Gets the actual content short text font value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public virtual Font GetContentShortTextFont(PaletteState state)
		{
            if (_font != null)
                return _font;
            else
                return _inherit.GetContentShortTextFont(state);
		}

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentShortTextNewFont(PaletteState state)
		{
            if (_font != null)
                return _font;
            else
                return _inherit.GetContentShortTextNewFont(state);
		}

		/// <summary>
		/// Gets the actual text rendering hint for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public PaletteTextHint GetContentShortTextHint(PaletteState state)
		{
			return _inherit.GetContentShortTextHint(state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return _inherit.GetContentShortTextPrefix(state);
        }

        /// <summary>
		/// Gets the actual text trimming for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public PaletteTextTrim GetContentShortTextTrim(PaletteState state)
		{
			return _inherit.GetContentShortTextTrim(state);
		}

		/// <summary>
		/// Gets the actual content short text horizontal alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state)
		{
			return _inherit.GetContentShortTextH(state);
		}

		/// <summary>
		/// Gets the actual content short text vertical alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public virtual PaletteRelativeAlign GetContentShortTextV(PaletteState state)
		{
			return _inherit.GetContentShortTextV(state);
		}

        /// <summary>
		/// Gets the actual content short text horizontal multiline alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
		{
			return _inherit.GetContentShortTextMultiLineH(state);
		}

        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetContentShortTextMultiLine(PaletteState state)
		{
			return _inherit.GetContentShortTextMultiLine(state);
		}

        /// <summary>
        /// Gets and sets the color for the text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Main color for the text.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public virtual Color Color1
        {
            get { return _color1; }

            set
            {
                if (value != _color1)
                {
                    _color1 = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the first color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor1(PaletteState state)
        {
            if (_color1 != Color.Empty)
                return _color1;
            else
                return _inherit.GetContentShortTextColor1(state);
        }
        
        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentShortTextColor2(PaletteState state)
        {
            return _inherit.GetContentShortTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            return _inherit.GetContentShortTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            return _inherit.GetContentShortTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentShortTextColorAngle(PaletteState state)
        {
            return _inherit.GetContentShortTextColorAngle(state);
        }

        /// <summary>
        /// Gets an image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentShortTextImage(PaletteState state)
        {
            return _inherit.GetContentShortTextImage(state);
        }

        /// <summary>
        /// Gets the image style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            return _inherit.GetContentShortTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            return _inherit.GetContentShortTextImageAlign(state);
        }
        #endregion

        #region ContentLongText
        /// <summary>
		/// Gets the actual content long text font value.
		/// </summary>
		/// <returns>Font value.</returns>
		/// <param name="state">Palette value should be applicable to this state.</param>
		public Font GetContentLongTextFont(PaletteState state)
		{
			return _inherit.GetContentLongTextFont(state);
		}

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentLongTextNewFont(PaletteState state)
		{
            return _inherit.GetContentLongTextNewFont(state);
		}

		/// <summary>
		/// Gets the actual text rendering hint for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public PaletteTextHint GetContentLongTextHint(PaletteState state)
		{
			return _inherit.GetContentLongTextHint(state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            return _inherit.GetContentLongTextPrefix(state);
        }
        
        /// <summary>
		/// Gets the actual text trimming for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public PaletteTextTrim GetContentLongTextTrim(PaletteState state)
		{
			return _inherit.GetContentLongTextTrim(state);
		}

		/// <summary>
		/// Gets the actual content long text horizontal alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentLongTextH(PaletteState state)
		{
			return _inherit.GetContentLongTextH(state);
		}

		/// <summary>
		/// Gets the actual content long text vertical alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentLongTextV(PaletteState state)
		{
			return _inherit.GetContentLongTextV(state);
		}

		/// <summary>
		/// Gets the actual content long text horizontal multiline alignment value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
		{
			return _inherit.GetContentLongTextMultiLineH(state);
		}

		/// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetContentLongTextMultiLine(PaletteState state)
		{
			return _inherit.GetContentLongTextMultiLine(state);
		}

        /// <summary>
        /// Gets the first color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor1(PaletteState state)
        {
            return _inherit.GetContentLongTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetContentLongTextColor2(PaletteState state)
        {
            return _inherit.GetContentLongTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            return _inherit.GetContentLongTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            return _inherit.GetContentLongTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetContentLongTextColorAngle(PaletteState state)
        {
            return _inherit.GetContentLongTextColorAngle(state);
        }

        /// <summary>
        /// Gets an image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetContentLongTextImage(PaletteState state)
        {
            return _inherit.GetContentLongTextImage(state);
        }

        /// <summary>
        /// Gets the image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            return _inherit.GetContentLongTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            return _inherit.GetContentLongTextImageAlign(state);
        }
		#endregion

		#region Padding
        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding between the border and content drawing.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding Padding
        {
            get { return _padding; }

            set
            {
                if (!value.Equals(_padding))
                {
                    _padding = value;
                    PerformNeedPaint(true);
                }
            }
        }

            /// <summary>
		/// Gets the actual padding between the border and content drawing.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public virtual Padding GetContentPadding(PaletteState state)
		{
            if (!_padding.Equals(CommonHelper.InheritPadding))
                return _padding;
            else
			    return _inherit.GetContentPadding(state);
		}
		#endregion

		#region AdjacentGap
		/// <summary>
		/// Gets the actual padding between adjacent content items.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public int GetContentAdjacentGap(PaletteState state)
		{
			return _inherit.GetContentAdjacentGap(state);
		}
		#endregion

        #region ContentStyle
        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public PaletteContentStyle GetContentStyle()
        {
            return _inherit.GetContentStyle();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the inheritence parent.
        /// </summary>
        protected IPaletteContent Inherit
        {
            get { return _inherit; }
        }
        #endregion
    }
}
