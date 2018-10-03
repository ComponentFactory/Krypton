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
	/// Inherit properties from primary source in preference to the backup source.
	/// </summary>
    public class PaletteRibbonDoubleInheritOverride : PaletteRibbonDoubleInherit
    {
        #region Instance Fields
        private bool _apply;
        private bool _override;
        private PaletteState _state;
        private IPaletteRibbonBack _primaryBack;
        private IPaletteRibbonBack _backupBack;
        private IPaletteRibbonText _primaryText;
        private IPaletteRibbonText _backupText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonDoubleInheritOverride class.
		/// </summary>
        /// <param name="primaryBack">First choice inheritence background.</param>
        /// <param name="primaryText">First choice inheritence text.</param>
        /// <param name="backupBack">Backup inheritence background.</param>
        /// <param name="backupText">Backup inheritence text.</param>
        /// <param name="state">Palette state to override.</param>
        public PaletteRibbonDoubleInheritOverride(IPaletteRibbonBack primaryBack,
                                                  IPaletteRibbonText primaryText,
                                                  IPaletteRibbonBack backupBack,
                                                  IPaletteRibbonText backupText,
                                                  PaletteState state) 
		{
            Debug.Assert(primaryBack != null);
            Debug.Assert(primaryText != null);
            Debug.Assert(backupBack != null);
            Debug.Assert(backupText != null);

            // Remember values
            _primaryBack = primaryBack;
            _primaryText = primaryText;
            _backupBack = backupBack;
            _backupText = backupText;

            // Default state
            _apply = false;
            _override = true;
            _state = state;
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

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            if (_apply)
            {
                PaletteRibbonColorStyle ret = _primaryBack.GetRibbonBackColorStyle(_override ? _state : state);

                if (ret == PaletteRibbonColorStyle.Inherit)
                    ret = _backupBack.GetRibbonBackColorStyle(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColorStyle(state);
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryBack.GetRibbonBackColor1(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupBack.GetRibbonBackColor1(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColor1(state);
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryBack.GetRibbonBackColor2(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupBack.GetRibbonBackColor2(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColor2(state);
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryBack.GetRibbonBackColor3(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupBack.GetRibbonBackColor3(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColor3(state);
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryBack.GetRibbonBackColor4(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupBack.GetRibbonBackColor4(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColor4(state);
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryBack.GetRibbonBackColor5(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupBack.GetRibbonBackColor5(state);

                return ret;
            }
            else
                return _backupBack.GetRibbonBackColor5(state);
        }
        #endregion

        #region IPaletteRibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteState state)
        {
            if (_apply)
            {
                Color ret = _primaryText.GetRibbonTextColor(_override ? _state : state);

                if (ret == Color.Empty)
                    ret = _backupText.GetRibbonTextColor(state);

                return ret;
            }
            else
                return _backupText.GetRibbonTextColor(state);
        }
        #endregion
    }
}
