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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Storage for shortcut related properties.
	/// </summary>
    public class RibbonShortcuts : Storage
    {
        #region Static Fields
        private static readonly Keys _defaultToggleMinimizeMode = (Keys.Control | Keys.F1);
        private static readonly Keys _defaultToggleKeyboardAccess1 = (Keys.RButton | Keys.ShiftKey | Keys.Alt);
        private static readonly Keys _defaultToggleKeyboardAccess2 = Keys.F10;
        #endregion

        #region Instance Fields
        private Keys _toggleMinimizeMode;
        private Keys _toggleKeyboardAccess1;
        private Keys _toggleKeyboardAccess2;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonShortcuts class.
		/// </summary>
        public RibbonShortcuts()
		{
            // Default values
            _toggleMinimizeMode = _defaultToggleMinimizeMode;
            _toggleKeyboardAccess1 = _defaultToggleKeyboardAccess1;
            _toggleKeyboardAccess2 = _defaultToggleKeyboardAccess2;
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
                return ((ToggleMinimizeMode == _defaultToggleMinimizeMode) &&
                        (ToggleKeyboardAccess1 == _defaultToggleKeyboardAccess1) &&
                        (ToggleKeyboardAccess2 == _defaultToggleKeyboardAccess2));
            }
        }
        #endregion

        #region ToggleMinimizeMode
        /// <summary>
        /// Gets and sets the shortcut to toggle the ribbon minimized mode..
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut to toggle the ribbon minimized mode.")]
        [DefaultValue(typeof(Keys), "F1, Control")]
        public Keys ToggleMinimizeMode
        {
            get { return _toggleMinimizeMode; }
            set { _toggleMinimizeMode = value; }
        }

        private bool ShouldSerializeToggleMinimizeMode()
        {
            return (ToggleMinimizeMode != _defaultToggleMinimizeMode);
        }

        /// <summary>
        /// Resets the ToggleMinimizeMode property to its default value.
        /// </summary>
        public void ResetToggleMinimizeMode()
        {
            ToggleMinimizeMode = _defaultToggleMinimizeMode;
        }
        #endregion

        #region ToggleKeyboardAccess1
        /// <summary>
        /// Gets and sets the shortcut to toggle keyboard access to the ribbon.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut to toggle keyboard access to the ribbon.")]
        [DefaultValue(typeof(Keys), "Menu, Alt")]
        public Keys ToggleKeyboardAccess1
        {
            get { return _toggleKeyboardAccess1; }
            set { _toggleKeyboardAccess1 = value; }
        }

        private bool ShouldSerializeToggleKeyboardAccess1()
        {
            return (ToggleKeyboardAccess1 != _defaultToggleKeyboardAccess1);
        }

        /// <summary>
        /// Resets the ToggleKeyboardAccess1 property to its default value.
        /// </summary>
        public void ResetToggleKeyboardAccess1()
        {
            ToggleKeyboardAccess1 = _defaultToggleKeyboardAccess1;
        }
        #endregion

        #region ToggleKeyboardAccess2
        /// <summary>
        /// Gets and sets the shortcut to toggle keyboard access to the ribbon.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut to toggle keyboard access to the ribbon.")]
        [DefaultValue(typeof(Keys), "F10")]
        public Keys ToggleKeyboardAccess2
        {
            get { return _toggleKeyboardAccess2; }
            set { _toggleKeyboardAccess2 = value; }
        }

        private bool ShouldSerializeToggleKeyboardAccess2()
        {
            return (ToggleKeyboardAccess2 != _defaultToggleKeyboardAccess2);
        }

        /// <summary>
        /// Resets the ToggleKeyboardAccess2 property to its default value.
        /// </summary>
        public void ResetToggleKeyboardAccess2()
        {
            ToggleKeyboardAccess2 = _defaultToggleKeyboardAccess2;
        }
        #endregion
    }
}
