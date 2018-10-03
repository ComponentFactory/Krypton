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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Inherit properties from primary source in preference to the backup source.
	/// </summary>
    public class PaletteElementColorInheritOverride : PaletteElementColorInherit
	{
		#region Instance Fields
        private bool _apply;
        private bool _override;
        private PaletteState _state;
        private IPaletteElementColor _primary;
        private IPaletteElementColor _backup;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteElementColorInheritOverride class.
		/// </summary>
        /// <param name="primary">First choice inheritence.</param>
        /// <param name="backup">Backup inheritence.</param>
        public PaletteElementColorInheritOverride(IPaletteElementColor primary,
                                                  IPaletteElementColor backup)
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
        public void SetPalettes(IPaletteElementColor primary,
                                IPaletteElementColor backup)
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

        #region IPaletteElementColor
        /// <summary>
        /// Gets the first color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor1(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetElementColor1(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetElementColor1(state);

                return ret;
            }
            else
                return _backup.GetElementColor1(state);
        }

        /// <summary>
        /// Gets the second color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor2(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetElementColor2(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetElementColor2(state);

                return ret;
            }
            else
                return _backup.GetElementColor2(state);
        }

        /// <summary>
        /// Gets the third color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor3(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetElementColor3(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetElementColor3(state);

                return ret;
            }
            else
                return _backup.GetElementColor3(state);
        }

        /// <summary>
        /// Gets the fourth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor4(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetElementColor4(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetElementColor4(state);

                return ret;
            }
            else
                return _backup.GetElementColor4(state);
        }

        /// <summary>
        /// Gets the fifth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor5(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primary.GetElementColor5(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backup.GetElementColor5(state);

                return ret;
            }
            else
                return _backup.GetElementColor5(state);
        }
        #endregion
    }
}
