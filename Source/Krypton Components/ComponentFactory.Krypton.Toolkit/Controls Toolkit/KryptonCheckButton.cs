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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Presents the user with a binary choice such as Yes/No or True/False.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonCheckButton), "ToolboxBitmaps.KryptonCheckButton.bmp")]
    [DefaultEvent("Click")]
	[DefaultProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonCheckButtonDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Toggles checked state when user clicks button.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonCheckButton : KryptonButton
	{
		#region Instance Fields
        private PaletteTriple _stateCheckedNormal;
        private PaletteTriple _stateCheckedTracking;
        private PaletteTriple _stateCheckedPressed;
        private PaletteTripleOverride _overrideCheckedFocus;
        private PaletteTripleOverride _overrideCheckedNormal;
        private PaletteTripleOverride _overrideCheckedTracking;
        private PaletteTripleOverride _overrideCheckedPressed;
        private CheckButtonValues _checkedValues;
        private bool _wasChecked;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Checked property is about to change.
        /// </summary>
        [Category("Property Changing")]
        [Description("Occurs whenever the Checked property is about to change.")]
        public event CancelEventHandler CheckedChanging;

        /// <summary>
        /// Occurs when the value of the Checked property has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs whenever the Checked property has changed.")]
        public event EventHandler CheckedChanged;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonCheckButton class.
		/// </summary>
        public KryptonCheckButton()
		{
			// Create the extra state needed for the checked additions the the base button
            _stateCheckedNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);
            _stateCheckedTracking = new PaletteTriple(StateCommon, NeedPaintDelegate);
            _stateCheckedPressed = new PaletteTriple(StateCommon, NeedPaintDelegate);

			// Create the override handling classes
            _overrideCheckedFocus = new PaletteTripleOverride(OverrideFocus, _stateCheckedNormal, PaletteState.FocusOverride);
            _overrideCheckedNormal = new PaletteTripleOverride(OverrideDefault, _overrideCheckedFocus, PaletteState.NormalDefaultOverride);
            _overrideCheckedTracking = new PaletteTripleOverride(OverrideFocus, _stateCheckedTracking, PaletteState.FocusOverride);
            _overrideCheckedPressed = new PaletteTripleOverride(OverrideFocus, _stateCheckedPressed, PaletteState.FocusOverride);

            // Add the checked specific palettes to the existing view button
            ViewDrawButton.SetCheckedPalettes(_overrideCheckedNormal,
                                              _overrideCheckedTracking,
                                              _overrideCheckedPressed);
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets access to the normal checked button appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal checked button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteTriple StateCheckedNormal
		{
			get { return _stateCheckedNormal; }
		}

        private bool ShouldSerializeStateCheckedNormal()
		{
            return !_stateCheckedNormal.IsDefault;
		}

		/// <summary>
        /// Gets access to the hot tracking checked button appearance entries.
		/// </summary>
		[Category("Visuals")]
        [Description("Overrides for defining hot tracking checked button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedTracking
		{
			get { return _stateCheckedTracking; }
		}

        private bool ShouldSerializeStateCheckedTracking()
		{
            return !_stateCheckedTracking.IsDefault;
		}

		/// <summary>
        /// Gets access to the pressed checked button appearance entries.
		/// </summary>
		[Category("Visuals")]
        [Description("Overrides for defining pressed checked button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedPressed
		{
			get { return _stateCheckedPressed; }
		}

        private bool ShouldSerializeStateCheckedPressed()
		{
            return !_stateCheckedPressed.IsDefault;
		}

        /// <summary>
        /// Gets or sets a value indicating whether the KryptonCheckButton is in the checked state. 
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether the control is in the checked state.")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool Checked
        {
            get { return ViewDrawButton.Checked; }

            set
            {
                if (value != ViewDrawButton.Checked)
                {
                    // Generate a pre-change event allowing it to be cancelled
                    CancelEventArgs ce = new CancelEventArgs();
                    OnCheckedChanging(ce);

                    // If the change is allowed to occur
                    if (!ce.Cancel)
                    {
                        // Use new checked state
                        ViewDrawButton.Checked = value;

                        // Generate the change event
                        OnCheckedChanged(EventArgs.Empty);

                        // Need to repaint to reflect change in visual state
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can uncheck the button when in the checked state.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the user can uncheck the button when in the checked state.")]
        [DefaultValue(true)]
        public bool AllowUncheck
        {
            get { return ViewDrawButton.AllowUncheck; }
            set { ViewDrawButton.AllowUncheck = value; }
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the check button.")]
        [DefaultValue(null)]
        public override IKryptonCommand KryptonCommand
        {
            get { return base.KryptonCommand; }

            set
            {
                if (base.KryptonCommand != value)
                {
                    if (base.KryptonCommand == null)
                        _wasChecked = Checked;

                    base.KryptonCommand = value;

                    if (base.KryptonCommand == null)
                        Checked = _wasChecked;
                }
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideCheckedFocus.Apply = true;
                _overrideCheckedTracking.Apply = true;
                _overrideCheckedPressed.Apply = true;
            }

            // Let base class fire standard event
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideCheckedFocus.Apply = false;
                _overrideCheckedTracking.Apply = false;
                _overrideCheckedPressed.Apply = false;
            }

            // Let base class fire standard event
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Toggle the checked state of the button
            if (!Checked || AllowUncheck)
                Checked = !Checked;

            // Let base class fire standard event
            base.OnClick(e);
        }

        /// <summary>
        /// Raises the KryptonCommandChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnKryptonCommandChanged(EventArgs e)
        {
            // Let base class update with base button properties
            base.OnKryptonCommandChanged(e);

            // Update the check button specific properties from the command
            if (KryptonCommand != null)
                Checked = KryptonCommand.Checked;
        }

        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected override void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CheckState":
                    Checked = KryptonCommand.Checked;
                    break;
            }

            base.OnCommandPropertyChanged(sender, e);
        }

        /// <summary>
        /// Creates a values storage object appropriate for control.
        /// </summary>
        /// <returns>Set of button values.</returns>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        protected override ButtonValues CreateButtonValues(NeedPaintHandler needPaint)
        {
            // Create a version of button values with checked entries
            _checkedValues = new CheckButtonValues(needPaint);
            return _checkedValues;
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the CheckedChanging event.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        protected virtual void OnCheckedChanging(CancelEventArgs e)
        {
            if (CheckedChanging != null)
                CheckedChanging(this, e);
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);

            // If there is a command associated then update with new state
            if (KryptonCommand != null)
                KryptonCommand.Checked = Checked;
        }
        #endregion
    }
}
