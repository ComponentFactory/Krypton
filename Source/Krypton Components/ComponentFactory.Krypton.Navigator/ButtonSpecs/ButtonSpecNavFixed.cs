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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Implementation for the fixed navigator buttons.
    /// </summary>
    [TypeConverter(typeof(ButtonSpecNavFixedConverter))]
    public abstract class ButtonSpecNavFixed : ButtonSpec
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private HeaderLocation _location;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavFixed class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="fixedStyle">Fixed style to use.</param>
        public ButtonSpecNavFixed(KryptonNavigator navigator,
                                  PaletteButtonSpecStyle fixedStyle)
        {
            Debug.Assert(navigator != null);

            // Remember back reference to owning navigator.
            _navigator = navigator;

            // Fix the type
            ProtectedType = fixedStyle;

            // Default other properties
            _location = HeaderLocation.PrimaryHeader;
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
                return (base.IsDefault && 
                        HeaderLocation == HeaderLocation.PrimaryHeader);
            }
        }
        #endregion

        #region AllowComponent
        /// <summary>
        /// Gets a value indicating if the component is allowed to be selected at design time.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowComponent
        {
            get { return false; }
        }
        #endregion

        #region HeaderLocation
        /// <summary>
        /// Gets and sets if the button header location.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines header location for the button.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(HeaderLocation), "PrimaryHeader")]
        public HeaderLocation HeaderLocation
        {
            get { return _location; }

            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnButtonSpecPropertyChanged("Location");
                }
            }
        }

        /// <summary>
        /// Resets the HeaderLocation property to its default value.
        /// </summary>
        public void ResetHeaderLocation()
        {
            HeaderLocation = HeaderLocation.PrimaryHeader;
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button location value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button location.</returns>
        public override HeaderLocation GetLocation(IPalette palette)
        {
            // Ask the view builder to recover the correct location
            return _navigator.ViewBuilder.GetFixedButtonLocation(this);
        }
        #endregion

        #region Navigator
        /// <summary>
        /// Gets access to the owning navigator control.
        /// </summary>
        protected KryptonNavigator Navigator
        {
            get { return _navigator; }
        }
        #endregion
    }
}
