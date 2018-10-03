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
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Inherit properties from primary source in preference to the backup source.
	/// </summary>
    public class PaletteBackInheritOverride : PaletteBackInherit
	{
		#region Instance Fields
		private bool _apply;
        private bool _override;
		private PaletteState _state;
		private IPaletteBack _primary;
		private IPaletteBack _backup;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteBackInheritOverride class.
		/// </summary>
		/// <param name="primary">First choice inheritence.</param>
		/// <param name="backup">Backup inheritence.</param>
		public PaletteBackInheritOverride(IPaletteBack primary,
										  IPaletteBack backup)
		{
			Debug.Assert(primary != null);
			Debug.Assert(backup != null);

			// Store incoming alternatives
			_primary = primary;
			_backup = backup;

			// Default other state
            _apply = true;
            _override = true;
			_state = PaletteState.Normal;
		}
		#endregion

        #region SetPalettes
        /// <summary>
        /// Update the the primary and backup palettes.
        /// </summary>
        /// <param name="primary">New primary palette.</param>
        /// <param name="backup">New backup palette.</param>
        public void SetPalettes(IPaletteBack primary,
                                IPaletteBack backup)
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

		#region IPaletteBack
		/// <summary>
		/// Gets a value indicating if background should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetBackDraw(PaletteState state)
		{
			if (_apply)
			{
				InheritBool ret = _primary.GetBackDraw(_override ? _state : state);

				if (ret == InheritBool.Inherit)
					ret = _backup.GetBackDraw(state);

				return ret;
			}
			else
				return _backup.GetBackDraw(state);
		}

		/// <summary>
		/// Gets the graphics drawing hint.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
		{
			if (_apply)
			{
                PaletteGraphicsHint ret = _primary.GetBackGraphicsHint(_override ? _state : state);

				if (ret == PaletteGraphicsHint.Inherit)
					ret = _backup.GetBackGraphicsHint(state);

				return ret;
			}
			else
				return _backup.GetBackGraphicsHint(state);
		}

		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBackColor1(PaletteState state)
		{
			if (_apply)
			{
                Color ret = _primary.GetBackColor1(_override ? _state : state);

				if (ret == Color.Empty)
					ret = _backup.GetBackColor1(state);

				return ret;
			}
			else
				return _backup.GetBackColor1(state);
		}

		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBackColor2(PaletteState state)
		{
			if (_apply)
			{
                Color ret = _primary.GetBackColor2(_override ? _state : state);

				if (ret == Color.Empty)
					ret = _backup.GetBackColor2(state);

				return ret;
			}
			else
				return _backup.GetBackColor2(state);
		}

		/// <summary>
		/// Gets the color drawing style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public override PaletteColorStyle GetBackColorStyle(PaletteState state)
		{
			if (_apply)
			{
                PaletteColorStyle ret = _primary.GetBackColorStyle(_override ? _state : state);

				if (ret == PaletteColorStyle.Inherit)
					ret = _backup.GetBackColorStyle(state);

				return ret;
			}
			else
				return _backup.GetBackColorStyle(state);
		}

		/// <summary>
		/// Gets the color alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public override PaletteRectangleAlign GetBackColorAlign(PaletteState state)
		{
			if (_apply)
			{
                PaletteRectangleAlign ret = _primary.GetBackColorAlign(_override ? _state : state);

				if (ret == PaletteRectangleAlign.Inherit)
					ret = _backup.GetBackColorAlign(state);

				return ret;
			}
			else
				return _backup.GetBackColorAlign(state);
		}

		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public override float GetBackColorAngle(PaletteState state)
		{
			if (_apply)
			{
                float ret = _primary.GetBackColorAngle(_override ? _state : state);

				if (ret == -1)
					ret = _backup.GetBackColorAngle(state);

				return ret;
			}
			else
				return _backup.GetBackColorAngle(state);
		}

		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public override Image GetBackImage(PaletteState state)
		{
			if (_apply)
			{
                Image ret = _primary.GetBackImage(_override ? _state : state);

				if (ret == null)
					ret = _backup.GetBackImage(state);

				return ret;
			}
			else
				return _backup.GetBackImage(state);
		}

		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public override PaletteImageStyle GetBackImageStyle(PaletteState state)
		{
			if (_apply)
			{
                PaletteImageStyle ret = _primary.GetBackImageStyle(_override ? _state : state);

				if (ret == PaletteImageStyle.Inherit)
					ret = _backup.GetBackImageStyle(state);

				return ret;
			}
			else
				return _backup.GetBackImageStyle(state);
		}

		/// <summary>
		/// Gets the image alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public override PaletteRectangleAlign GetBackImageAlign(PaletteState state)
		{
			if (_apply)
			{
                PaletteRectangleAlign ret = _primary.GetBackImageAlign(_override ? _state : state);

				if (ret == PaletteRectangleAlign.Inherit)
					ret = _backup.GetBackImageAlign(state);

				return ret;
			}
			else
				return _backup.GetBackImageAlign(state);
		}
         #endregion
	}
}
