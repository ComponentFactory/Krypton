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
    /// Display text and images with the styling features of the Krypton Toolkit
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonLinkLabel), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
    [DefaultEvent("LinkClicked")]
	[DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonLinkLabelDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Displays descriptive information as a hyperlink.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonLinkLabel : KryptonLabel
	{
		#region Instance Fields
        private PaletteContent _stateVisited;
        private PaletteContent _stateNotVisited;
        private PaletteContent _statePressed;
        private PaletteContent _stateFocus;
        private PaletteContentInheritRedirect _stateVisitedRedirect;
        private PaletteContentInheritRedirect _stateNotVisitedRedirect;
        private PaletteContentInheritRedirect _statePressedRedirect;
        private PaletteContentInheritRedirect _stateFocusRedirect;
        private PaletteContentInheritOverride _overrideVisited;
        private PaletteContentInheritOverride _overrideNotVisited;
        private PaletteContentInheritOverride _overrideFocusNotVisited;
        private PaletteContentInheritOverride _overridePressed;
        private PaletteContentInheritOverride _overridePressedFocus;
        private LinkLabelBehaviorInherit _inheritBehavior;
        private LinkLabelController _controller;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the link is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the link is clicked.")]
        public event EventHandler LinkClicked;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonLinkLabel class.
		/// </summary>
        public KryptonLinkLabel()
		{
            // The link label cannot take the focus
            SetStyle(ControlStyles.Selectable, true);

            // Turn off the target functionality present in the base class
            EnabledTarget = false;

            // Create the override states that redirect without inheriting
            _stateVisitedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);
            _stateNotVisitedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);
            _statePressedRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);
            _stateFocusRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);
            _stateVisited = new PaletteContent(_stateVisitedRedirect, NeedPaintDelegate);
            _stateNotVisited = new PaletteContent(_stateNotVisitedRedirect, NeedPaintDelegate);
            _stateFocus = new PaletteContent(_stateFocusRedirect, NeedPaintDelegate);
            _statePressed = new PaletteContent(_statePressedRedirect, NeedPaintDelegate);

            // Override the normal state to implement the underling logic
            _inheritBehavior = new LinkLabelBehaviorInherit(StateNormal, KryptonLinkBehavior.AlwaysUnderline);

            // Create the override handling classes
            _overrideVisited = new PaletteContentInheritOverride(_stateVisited, _inheritBehavior, PaletteState.LinkVisitedOverride, false);
            _overrideNotVisited = new PaletteContentInheritOverride(_stateNotVisited, _overrideVisited, PaletteState.LinkNotVisitedOverride, true);
            _overrideFocusNotVisited = new PaletteContentInheritOverride(_stateFocus, _overrideNotVisited, PaletteState.FocusOverride, false);
            _overridePressed = new PaletteContentInheritOverride(_statePressed, _inheritBehavior, PaletteState.LinkPressedOverride, false);
            _overridePressedFocus = new PaletteContentInheritOverride(_stateFocus, _overridePressed, PaletteState.FocusOverride, false);

            // Create controller for updating the view state/click events
            _controller = new LinkLabelController(ViewDrawContent, StateDisabled, _overrideFocusNotVisited, _overrideFocusNotVisited, _overridePressedFocus, _overridePressed, NeedPaintDelegate);
            _controller.Click += new MouseEventHandler(OnControllerClick);
            ViewDrawContent.MouseController = _controller;
            ViewDrawContent.KeyController = _controller;
            ViewDrawContent.SourceController = _controller;

            // Set initial palette for drawing the content
            ViewDrawContent.SetPalette(_overrideFocusNotVisited);
        }
		#endregion

        #region Public
        /// <summary>
        /// Gets and sets a value that determines the underline behavior of the link label.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines the underline behavior of the link label.")]
        public KryptonLinkBehavior LinkBehavior
        {
            get { return _inheritBehavior.LinkBehavior; }

            set
            {
                if (_inheritBehavior.LinkBehavior != value)
                {
                    _inheritBehavior.LinkBehavior = value;
                    PerformNeedPaint(false);
                }
            }
        }

        private void ResetLinkBehavior()
        {
            LinkBehavior = KryptonLinkBehavior.AlwaysUnderline;
        }

        private bool ShouldSerializeLinkBehavior()
        {
            return (LinkBehavior != KryptonLinkBehavior.AlwaysUnderline);
        }

        /// <summary>
        /// Gets and sets a value indicating if the label has been visited.
        /// </summary>
        [Category("Visuals")]
        [Description("Indicates if the hyperlink has been visited already.")]
        [DefaultValue(false)]
        public bool LinkVisited
        {
            get { return _overrideVisited.Apply; }

            set
            {
                if (_overrideVisited.Apply != value)
                {
                    _overrideVisited.Apply = value;
                    _overrideNotVisited.Apply = !value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the pressed label appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverridePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeOverridePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the label appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining label appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
        /// Gets access to normal state modifications when label has been visited.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when label has been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideVisited
        {
            get { return _stateVisited; }
        }

        private bool ShouldSerializeOverrideVisited()
        {
            return !_stateVisited.IsDefault;
        }

        /// <summary>
        /// Gets access to normal state modifications when label has not been visited.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when label has not been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideNotVisited
        {
            get { return _stateNotVisited; }
        }

        private bool ShouldSerializeOverrideNotVisited()
        {
            return !_stateNotVisited.IsDefault;
        }

        /// <summary>
        /// Gets access to the target for mnemonic and click actions.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Control Target
        {
            get { return base.Target; }
            set { base.Target = value; }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public override void SetFixedState(PaletteState state)
        {
            // Let base class update state
            base.SetFixedState(state);

            // Update display to reflect change
            _controller.Update(this);
            PerformNeedPaint(true);
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the LinkClicked event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLinkClicked(LinkClickedEventArgs e)
        {
            if (LinkClicked != null)
                LinkClicked(this, e);

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }
        #endregion

        #region Protected Overrides
        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
            // Let base class fire standard event
            base.OnEnabledChanged(e);

            // Ask controller to update with correct palette to match state
            _controller.Update(this);
		}       
 
		/// <summary>
		/// Raises the GotFocus event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnGotFocus(EventArgs e)
		{
            // Apply the focus overrides
            _overrideFocusNotVisited.Apply = true;
            _overridePressedFocus.Apply = true;

            // Change in focus requires a repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
			base.OnGotFocus(e);
		}

		/// <summary>
		/// Raises the LostFocus event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
            // Apply the focus overrides
            _overrideFocusNotVisited.Apply = false;
            _overridePressedFocus.Apply = false;

            // Change in focus requires a repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
			base.OnLostFocus(e);
        }

        /// <summary>
        /// Update the view elements based on the requested label style.
        /// </summary>
        /// <param name="style">New label style.</param>
        protected override void SetLabelStyle(LabelStyle style)
        {
            // Let base class update the standard label style
            base.SetLabelStyle(style);

            PaletteContentStyle contentStyle = CommonHelper.ContentStyleFromLabelStyle(style);

            // Update all redirectors with new style
            _stateVisitedRedirect.Style = contentStyle;
            _stateNotVisitedRedirect.Style = contentStyle;
            _statePressedRedirect.Style = contentStyle;
            _stateFocusRedirect.Style = contentStyle;
        }
        #endregion

        #region Implementation
        private void OnControllerClick(object sender, MouseEventArgs e)
        {
            OnLinkClicked(new LinkClickedEventArgs(Text));
        }
        #endregion
    }
}
