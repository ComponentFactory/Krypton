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
    /// Button specification that can be assigned as any button type.
    /// </summary>
    public class ButtonSpecAny : ButtonSpec
    {
        #region Instance Fields
        private bool _visible;
        private ButtonEnabled _enabled;
        private ButtonCheckState _checked;
        private ButtonEnabled _wasEnabled;
        private ButtonCheckState _wasChecked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AnyButtonSpec class.
		/// </summary>
        public ButtonSpecAny()
		{
            _visible = true;
            _enabled = ButtonEnabled.Container;
            _checked = ButtonCheckState.NotCheckButton;
        }

        /// <summary>
        /// Make a clone of this object.
        /// </summary>
        /// <returns>New instance.</returns>
        public override object Clone()
        {
            ButtonSpecAny clone = (ButtonSpecAny)base.Clone();
            clone.Visible = Visible;
            clone.Enabled = Enabled;
            clone.Checked = Checked;
            clone.Type = Type;
            return clone;
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
                        (Visible == true) &&
                        (Enabled == ButtonEnabled.Container) &&
                        (Checked == ButtonCheckState.NotCheckButton));
            }
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets if the button should be shown.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Should the button be shown.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool Visible
        {
            get { return _visible; }

            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnButtonSpecPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Resets the Visible property to its default value.
        /// </summary>
        public void ResetVisible()
        {
            Visible = true;
        }
        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the button enabled state.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines the button enabled state.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(ButtonEnabled), "Container")]
        public ButtonEnabled Enabled
        {
            get { return _enabled; }

            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnButtonSpecPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        /// Resets the Enabled property to its default value.
        /// </summary>
        public void ResetEnabled()
        {
            Enabled = ButtonEnabled.Container;
        }
        #endregion

        #region Checked
        /// <summary>
        /// Gets and sets if the button is checked or capable of being checked.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines if the button is checked or capable of being checked.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(ButtonCheckState), "NotCheckButton")]
        public ButtonCheckState Checked
        {
            get { return _checked; }

            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnButtonSpecPropertyChanged("Checked");
                }
            }
        }

        private bool ShouldSerializeChecked()
        {
            return (Checked != ButtonCheckState.NotCheckButton);
        }

        /// <summary>
        /// Resets the Checked property to its default value.
        /// </summary>
        public void ResetChecked()
        {
            Checked = ButtonCheckState.NotCheckButton;
        }
        #endregion

        #region KryptonCommand
        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the button.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public override KryptonCommand KryptonCommand
        {
            get { return base.KryptonCommand; }

            set
            {
                if (base.KryptonCommand != value)
                {
                    if (base.KryptonCommand == null)
                    {
                        _wasEnabled = Enabled;
                        _wasChecked = Checked;
                    }

                    base.KryptonCommand = value;

                    if (base.KryptonCommand == null)
                    {
                        Enabled = _wasEnabled;
                        Checked = _wasChecked;
                    }
                }
            }
        }
        #endregion

        #region Type
        /// <summary>
        /// Gets and sets the button type.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines the type of button specification.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(PaletteButtonSpecStyle), "Generic")]
        public PaletteButtonSpecStyle Type
        {
            get { return ProtectedType; }

            set
            {
                if (ProtectedType != value)
                {
                    ProtectedType = value;
                    OnButtonSpecPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// Resets the Type property to its default value.
        /// </summary>
        public void ResetType()
        {
            Type = PaletteButtonSpecStyle.Generic;
        }
        #endregion

        #region CopyFrom
        /// <summary>
        /// Value copy form the provided source to ourself.
        /// </summary>
        /// <param name="source">Source instance.</param>
        public void CopyFrom(ButtonSpecAny source)
        {
            // Copy class specific values
            Visible = source.Visible;
            Enabled = source.Enabled;
            Checked = source.Checked;

            // Let base class copy the base values
            base.CopyFrom(source);
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            return Visible;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            return Enabled;
        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette)
        {
            return Checked;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the ButtonSpecPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the appearance property that has changed.</param>
        protected override void OnButtonSpecPropertyChanged(string propertyName)
        {
            base.OnButtonSpecPropertyChanged(propertyName);

            if (propertyName == "KryptonCommand")
            {
                if (KryptonCommand != null)
                {
                    if (Checked != ButtonCheckState.NotCheckButton)
                        Checked = (KryptonCommand.Checked ? ButtonCheckState.Checked : ButtonCheckState.Unchecked);

                    Enabled = (KryptonCommand.Enabled ? ButtonEnabled.True : ButtonEnabled.False);
                }
            }
            else if (propertyName == "Checked")
            {
                if (KryptonCommand != null)
                    KryptonCommand.Checked = (Checked == ButtonCheckState.Checked);
            }
        }

        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected override void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCommandPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Checked":
                    Checked = (KryptonCommand.Checked ? ButtonCheckState.Checked : ButtonCheckState.Unchecked);
                    break;
                case "Enabled":
                    Enabled = (KryptonCommand.Enabled ? ButtonEnabled.True : ButtonEnabled.False);
                    break;
            }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                // If a checked style button
                if (Checked != ButtonCheckState.NotCheckButton)
                {
                    // Then invert the checked state
                    if (Checked == ButtonCheckState.Unchecked)
                        Checked = ButtonCheckState.Checked;
                    else
                        Checked = ButtonCheckState.Unchecked;
                }
            }

            base.OnClick(e);
        }
        #endregion
    }
}
