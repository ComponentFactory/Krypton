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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Inherit properties from primary source in preference to the backup source.
	/// </summary>
	public class PaletteContentInheritOverride : PaletteContentInherit
	{
		#region Instance Fields
		private bool _apply;
        private bool _override;
        private PaletteState _state;
		private IPaletteContent _primary;
		private IPaletteContent _backup;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteContentInheritOverride class.
		/// </summary>
		/// <param name="primary">First choice inheritence.</param>
		/// <param name="backup">Backup inheritence.</param>
        public PaletteContentInheritOverride(IPaletteContent primary,
                                             IPaletteContent backup)
            : this(primary, backup, PaletteState.Normal, true)
        {
        }

		/// <summary>
		/// Initialize a new instance of the PaletteContentInheritOverride class.
		/// </summary>
		/// <param name="primary">First choice inheritence.</param>
		/// <param name="backup">Backup inheritence.</param>
        /// <param name="overrideState">State used by the override.</param>
        /// <param name="apply">Should the override we used.</param>
		public PaletteContentInheritOverride(IPaletteContent primary,
											 IPaletteContent backup,
                                             PaletteState overrideState,
                                             bool apply)
		{
			Debug.Assert(primary != null);
			Debug.Assert(backup != null);

			// Store incoming values
			_primary = primary;
			_backup = backup;
            _apply = apply;
            _state = overrideState;

            // By default we do override the state
            _override = true;
		}
		#endregion

        #region SetPalettes
        /// <summary>
        /// Update the the primary and backup palettes.
        /// </summary>
        /// <param name="primary">New primary palette.</param>
        /// <param name="backup">New backup palette.</param>
        public void SetPalettes(IPaletteContent primary,
                                IPaletteContent backup)
        {
            // Store incoming alternatives
            _primary = primary;
            _backup = backup;
        }
        #endregion

        #region Apply
		/// <summary>
		/// Gets and sets a value indicating if override should be applied.
		/// </summary>
		public bool Apply
		{
			get { return _apply; }
            set { _apply = value; }
		}
		#endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get { return _override; }
            set { _override = value; }
        }
        #endregion

		#region OverrideState
		/// <summary>
		/// Gets and sets the override palette state to use.
		/// </summary>
		public PaletteState OverrideState
		{
			get { return _state; }
			set { _state = value; }
		}
		#endregion

		#region IPaletteContent
		/// <summary>
		/// Gets a value indicating if content should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDraw(PaletteState state)
		{
			if (_apply)
			{
                InheritBool ret = _primary.GetContentDraw(_override ? _state : state);

				if (ret == InheritBool.Inherit)
					ret = _backup.GetContentDraw(state);

				return ret;
			}
			else
				return _backup.GetContentDraw(state);
		}

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDrawFocus(PaletteState state)
		{
			if (_apply)
			{
                InheritBool ret = _primary.GetContentDrawFocus(_override ? _state : state);

				if (ret == InheritBool.Inherit)
					ret = _backup.GetContentDrawFocus(state);

				return ret;
			}
			else
				return _backup.GetContentDrawFocus(state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageH(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentImageH(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentImageH(state);

				return ret;
			}
			else
				return _backup.GetContentImageH(state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageV(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentImageV(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentImageV(state);

				return ret;
			}
			else
				return _backup.GetContentImageV(state);
		}

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		public override PaletteImageEffect GetContentImageEffect(PaletteState state)
		{
			if (_apply)
			{
                PaletteImageEffect ret = _primary.GetContentImageEffect(_override ? _state : state);

				if (ret == PaletteImageEffect.Inherit)
					ret = _backup.GetContentImageEffect(state);

				return ret;
			}
			else
				return _backup.GetContentImageEffect(state);
		}

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentImageColorMap(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentImageColorMap(state);

                return ret;
            }
            else
                return _backup.GetContentImageColorMap(state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentImageColorTo(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentImageColorTo(state);

                return ret;
            }
            else
                return _backup.GetContentImageColorTo(state);
        }

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentShortTextFont(PaletteState state)
		{
			if (_apply)
			{
                Font ret = _primary.GetContentShortTextFont(_override ? _state : state);

				if (ret == null)
					ret = _backup.GetContentShortTextFont(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextFont(state);
		}

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteState state)
		{
			if (_apply)
			{
                Font ret = _primary.GetContentShortTextNewFont(_override ? _state : state);

				if (ret == null)
                    ret = _backup.GetContentShortTextNewFont(state);

				return ret;
			}
			else
                return _backup.GetContentShortTextNewFont(state);
		}

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentShortTextHint(PaletteState state)
		{
			if (_apply)
			{
                PaletteTextHint ret = _primary.GetContentShortTextHint(_override ? _state : state);

				if (ret == PaletteTextHint.Inherit)
					ret = _backup.GetContentShortTextHint(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextHint(state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            if (_apply)
            {
                PaletteTextHotkeyPrefix ret = _primary.GetContentShortTextPrefix(_override ? _state : state);

                if (ret == PaletteTextHotkeyPrefix.Inherit)
                    ret = _backup.GetContentShortTextPrefix(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextPrefix(state);
        }

		/// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentShortTextMultiLine(PaletteState state)
		{
			if (_apply)
			{
                InheritBool ret = _primary.GetContentShortTextMultiLine(_override ? _state : state);

				if (ret == InheritBool.Inherit)
					ret = _backup.GetContentShortTextMultiLine(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextMultiLine(state);
		}

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
		{
			if (_apply)
			{
                PaletteTextTrim ret = _primary.GetContentShortTextTrim(_override ? _state : state);

				if (ret == PaletteTextTrim.Inherit)
					ret = _backup.GetContentShortTextTrim(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextTrim(state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentShortTextH(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentShortTextH(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextH(state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextV(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentShortTextV(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentShortTextV(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextV(state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentShortTextMultiLineH(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentShortTextMultiLineH(state);

				return ret;
			}
			else
				return _backup.GetContentShortTextMultiLineH(state);
		}

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentShortTextColor1(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentShortTextColor1(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentShortTextColor2(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentShortTextColor2(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            if (_apply)
            {
                PaletteColorStyle ret = _primary.GetContentShortTextColorStyle(_override ? _state : state);

                if (ret == PaletteColorStyle.Inherit)
                    ret = _backup.GetContentShortTextColorStyle(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            if (_apply)
            {
                PaletteRectangleAlign ret = _primary.GetContentShortTextColorAlign(_override ? _state : state);

                if (ret == PaletteRectangleAlign.Inherit)
                    ret = _backup.GetContentShortTextColorAlign(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteState state)
        {
            if (_apply)
            {
                float ret = _primary.GetContentShortTextColorAngle(_override ? _state : state);

                if (ret == -1f)
                    ret = _backup.GetContentShortTextColorAngle(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextColorAngle(state);
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteState state)
        {
            if (_apply)
            {
                Image ret = _primary.GetContentShortTextImage(_override ? _state : state);

                if (ret == null)
                    ret = _backup.GetContentShortTextImage(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextImage(state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            if (_apply)
            {
                PaletteImageStyle ret = _primary.GetContentShortTextImageStyle(_override ? _state : state);

                if (ret == PaletteImageStyle.Inherit)
                    ret = _backup.GetContentShortTextImageStyle(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            if (_apply)
            {
                PaletteRectangleAlign ret = _primary.GetContentShortTextImageAlign(_override ? _state : state);

                if (ret == PaletteRectangleAlign.Inherit)
                    ret = _backup.GetContentShortTextImageAlign(state);

                return ret;
            }
            else
                return _backup.GetContentShortTextImageAlign(state);
        }

		/// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentLongTextFont(PaletteState state)
		{
			if (_apply)
			{
                Font ret = _primary.GetContentLongTextFont(_override ? _state : state);

				if (ret == null)
					ret = _backup.GetContentLongTextFont(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextFont(state);
		}

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextNewFont(PaletteState state)
		{
			if (_apply)
			{
                Font ret = _primary.GetContentLongTextNewFont(_override ? _state : state);

				if (ret == null)
                    ret = _backup.GetContentLongTextNewFont(state);

				return ret;
			}
			else
                return _backup.GetContentLongTextNewFont(state);
		}

		/// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentLongTextHint(PaletteState state)
		{
			if (_apply)
			{
                PaletteTextHint ret = _primary.GetContentLongTextHint(_override ? _state : state);

				if (ret == PaletteTextHint.Inherit)
					ret = _backup.GetContentLongTextHint(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextHint(state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            if (_apply)
            {
                PaletteTextHotkeyPrefix ret = _primary.GetContentLongTextPrefix(_override ? _state : state);

                if (ret == PaletteTextHotkeyPrefix.Inherit)
                    ret = _backup.GetContentLongTextPrefix(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextPrefix(state);
        }
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentLongTextMultiLine(PaletteState state)
		{
			if (_apply)
			{
                InheritBool ret = _primary.GetContentLongTextMultiLine(_override ? _state : state);

				if (ret == InheritBool.Inherit)
					ret = _backup.GetContentLongTextMultiLine(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextMultiLine(state);
		}

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
		{
			if (_apply)
			{
                PaletteTextTrim ret = _primary.GetContentLongTextTrim(_override ? _state : state);

				if (ret == PaletteTextTrim.Inherit)
					ret = _backup.GetContentLongTextTrim(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextTrim(state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextH(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentLongTextH(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentLongTextH(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextH(state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextV(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentLongTextV(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentLongTextV(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextV(state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
		{
			if (_apply)
			{
                PaletteRelativeAlign ret = _primary.GetContentLongTextMultiLineH(_override ? _state : state);

				if (ret == PaletteRelativeAlign.Inherit)
					ret = _backup.GetContentLongTextMultiLineH(state);

				return ret;
			}
			else
				return _backup.GetContentLongTextMultiLineH(state);
		}

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentLongTextColor1(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentLongTextColor1(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetContentLongTextColor2(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetContentLongTextColor2(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            if (_apply)
            {
                PaletteColorStyle ret = _primary.GetContentLongTextColorStyle(_override ? _state : state);

                if (ret == PaletteColorStyle.Inherit)
                    ret = _backup.GetContentLongTextColorStyle(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            if (_apply)
            {
                PaletteRectangleAlign ret = _primary.GetContentLongTextColorAlign(_override ? _state : state);

                if (ret == PaletteRectangleAlign.Inherit)
                    ret = _backup.GetContentLongTextColorAlign(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteState state)
        {
            if (_apply)
            {
                float ret = _primary.GetContentLongTextColorAngle(_override ? _state : state);

                if (ret == -1f)
                    ret = _backup.GetContentLongTextColorAngle(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextColorAngle(state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteState state)
        {
            if (_apply)
            {
                Image ret = _primary.GetContentLongTextImage(_override ? _state : state);

                if (ret == null)
                    ret = _backup.GetContentLongTextImage(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextImage(state);
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            if (_apply)
            {
                PaletteImageStyle ret = _primary.GetContentLongTextImageStyle(_override ? _state : state);

                if (ret == PaletteImageStyle.Inherit)
                    ret = _backup.GetContentLongTextImageStyle(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            if (_apply)
            {
                PaletteRectangleAlign ret = _primary.GetContentLongTextImageAlign(_override ? _state : state);

                if (ret == PaletteRectangleAlign.Inherit)
                    ret = _backup.GetContentLongTextImageAlign(state);

                return ret;
            }
            else
                return _backup.GetContentLongTextImageAlign(state);
        }

		/// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public override Padding GetContentPadding(PaletteState state)
		{
			if (_apply)
			{
                Padding ret = _primary.GetContentPadding(_override ? _state : state);

				if (ret.All == -1)
					ret = _backup.GetContentPadding(state);

				return ret;
			}
			else
				return _backup.GetContentPadding(state);
		}

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public override int GetContentAdjacentGap(PaletteState state)
		{
			if (_apply)
			{
                int ret = _primary.GetContentAdjacentGap(_override ? _state : state);

				if (ret == -1)
					ret = _backup.GetContentAdjacentGap(state);

				return ret;
			}
			else
				return _backup.GetContentAdjacentGap(state);
		}

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public override PaletteContentStyle GetContentStyle()
        {
            if (_apply)
                return _primary.GetContentStyle();
            else
                return _backup.GetContentStyle();
        }
        #endregion
	}
}
