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
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Provide a MaskedTextBox with Krypton styling applied.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonMaskedTextBox), "ToolboxBitmaps.KryptonMaskedTextBox.bmp")]
    [DefaultEvent("MaskInputRejected")]
	[DefaultProperty("Mask")]
    [DefaultBindingProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBoxDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Uses a mask to distinguish between proper and improper user input.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonMaskedTextBox : VisualControlBase,
                                        IContainedInputControl
    {
        #region Classes
        private class InternalMaskedTextBox : MaskedTextBox
        {
            #region Instance Fields
            private KryptonMaskedTextBox _kryptonMaskedTextBox;
            private bool _mouseOver;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalMaskedTextBox.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalMaskedTextBox.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the InternalMaskedTextBox class.
            /// </summary>
            /// <param name="kryptonMaskedTextBox">Reference to owning control.</param>
            public InternalMaskedTextBox(KryptonMaskedTextBox kryptonMaskedTextBox)
            {
                _kryptonMaskedTextBox = kryptonMaskedTextBox;

                // Remove from view until size for the first time by the Krypton control
                Size = Size.Empty;

                // We provide the border manually
                BorderStyle = BorderStyle.None;
            }
            #endregion

            #region MouseOver
            /// <summary>
            /// Gets and sets if the mouse is currently over the combo box.
            /// </summary>
            public bool MouseOver
            {
                get { return _mouseOver; }
                
                set 
                {
                    // Only interested in changes
                    if (_mouseOver != value)
                    {
                        _mouseOver = value;

                        // Generate appropriate change event
                        if (_mouseOver)
                            OnTrackMouseEnter(EventArgs.Empty);
                        else
                            OnTrackMouseLeave(EventArgs.Empty);
                    }
                }
            }
            #endregion

            #region Protected
            /// <summary>
            /// Process Windows-based messages.
            /// </summary>
            /// <param name="m">A Windows-based message.</param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_NCHITTEST:
                        if (_kryptonMaskedTextBox.InTransparentDesignMode)
                            m.Result = (IntPtr)PI.HTTRANSPARENT;
                        else
                            base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        // Mouse is not over the control
                        MouseOver = false;
                        _kryptonMaskedTextBox.PerformNeedPaint(true);
                        Invalidate();
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            MouseOver = true;
                            _kryptonMaskedTextBox.PerformNeedPaint(true);
                            Invalidate();
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_PRINTCLIENT:
                    case PI.WM_PAINT:
                        {
                            IntPtr hdc;
                            PI.PAINTSTRUCT ps = new PI.PAINTSTRUCT();

                            // Do we need to BeginPaint or just take the given HDC?
                            if (m.WParam == IntPtr.Zero)
                                hdc = PI.BeginPaint(Handle, ref ps);
                            else
                                hdc = m.WParam;

                            // Paint the entire area in the background color
                            using (Graphics g = Graphics.FromHdc(hdc))
                            {
                                // Grab the client area of the control
                                PI.RECT rect = new PI.RECT();
                                PI.GetClientRect(Handle, out rect);

                                // Drawn entire client area in the background color
                                using (SolidBrush backBrush = new SolidBrush(BackColor))
                                    g.FillRectangle(backBrush, new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top));

                                // Create rect for the text area
                                Size borderSize = SystemInformation.BorderSize;
                                rect.left -= (borderSize.Width + 1);

                                // If enabled then let the combo draw the text area
                                if (_kryptonMaskedTextBox.Enabled)
                                {
                                    // Let base implementation draw the actual text area
                                    if (m.WParam == IntPtr.Zero)
                                    {
                                        m.WParam = hdc;
                                        DefWndProc(ref m);
                                        m.WParam = IntPtr.Zero;
                                    }
                                    else
                                        DefWndProc(ref m);
                                }
                                else
                                {
                                    // Set the correct text rendering hint for the text drawing. We only draw if the edit text is disabled so we
                                    // just always grab the disable state value. Without this line the wrong hint can occur because it inherits
                                    // it from the device context. Resulting in blurred text.
                                    g.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(_kryptonMaskedTextBox.StateDisabled.PaletteContent.GetContentShortTextHint(PaletteState.Disabled));

                                    // Define the string formatting requirements
                                    StringFormat stringFormat = new StringFormat();
                                    stringFormat.LineAlignment = StringAlignment.Center;
                                    stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                    stringFormat.Trimming = StringTrimming.None;

                                    switch (_kryptonMaskedTextBox.TextAlign)
                                    {
                                        case HorizontalAlignment.Left:
                                            if (RightToLeft == RightToLeft.Yes)
                                                stringFormat.Alignment = StringAlignment.Far;
                                            else
                                                stringFormat.Alignment = StringAlignment.Near;
                                            break;
                                        case HorizontalAlignment.Right:
                                            if (RightToLeft == RightToLeft.Yes)
                                                stringFormat.Alignment = StringAlignment.Near;
                                            else
                                                stringFormat.Alignment = StringAlignment.Far;
                                            break;
                                        case HorizontalAlignment.Center:
                                            stringFormat.Alignment = StringAlignment.Center;
                                            break;
                                    }

                                    // Use the correct prefix setting
                                    stringFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;

                                    // Draw using a solid brush
                                    string drawText = (MaskedTextProvider == null ? Text : MaskedTextProvider.ToDisplayString());
                                    try
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(ForeColor))
                                            g.DrawString(drawText, Font, foreBrush,
                                                         new RectangleF(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top),
                                                         stringFormat);
                                    }
                                    catch (ArgumentException)
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(ForeColor))
                                            g.DrawString(drawText, _kryptonMaskedTextBox.GetTripleState().PaletteContent.GetContentShortTextFont(PaletteState.Disabled), foreBrush,
                                                         new RectangleF(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top),
                                                         stringFormat);
                                    }
                                }

                                // Remove clipping settings
                                PI.SelectClipRgn(hdc, IntPtr.Zero);
                            }

                            // Do we need to match the original BeginPaint?
                            if (m.WParam == IntPtr.Zero)
                                PI.EndPaint(Handle, ref ps);
                        }
                        break;
                    case PI.WM_CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (_kryptonMaskedTextBox.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)((long)m.LParam)) == -1)
                                mousePt = PointToScreen(new Point(Width / 2, Height / 2));

                            // Show the context menu
                            _kryptonMaskedTextBox.KryptonContextMenu.Show(_kryptonMaskedTextBox, mousePt);

                            // We eat the message!
                            return;
                        }
                        base.WndProc(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            /// <summary>
            /// Raises the TrackMouseEnter event.
            /// </summary>
            /// <param name="e">An EventArgs containing the event data.</param>
            protected virtual void OnTrackMouseEnter(EventArgs e)
            {
                if (TrackMouseEnter != null)
                    TrackMouseEnter(this, e);
            }

            /// <summary>
            /// Raises the TrackMouseLeave event.
            /// </summary>
            /// <param name="e">An EventArgs containing the event data.</param>
            protected virtual void OnTrackMouseLeave(EventArgs e)
            {
                if (TrackMouseLeave != null)
                    TrackMouseLeave(this, e);
            }
            #endregion
        }
        #endregion

        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class MaskedTextBoxButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the MaskedTextBoxButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public MaskedTextBoxButtonSpecCollection(KryptonMaskedTextBox owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private ToolTipManager _toolTipManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ButtonSpecManagerLayout _buttonManager;
        private MaskedTextBoxButtonSpecCollection _buttonSpecs;
        private PaletteInputControlTripleRedirect _stateCommon;
        private PaletteInputControlTripleStates _stateDisabled;
        private PaletteInputControlTripleStates _stateNormal;
        private PaletteInputControlTripleStates _stateActive;
        private ViewLayoutDocker _drawDockerInner;
        private ViewDrawDocker _drawDockerOuter;
        private ViewLayoutFill _layoutFill;
        private InternalMaskedTextBox _maskedTextBox;
        private InputControlStyle _inputControlStyle;
        private Nullable<bool> _fixedActive;
        private bool _inRibbonDesignMode;
        private bool _forcedLayout;
        private bool _autoSize;
        private bool _mouseOver;
        private bool _alwaysActive;
        private bool _allowButtonSpecToolTips;
        private bool _trackingMouseEnter;
        private int _cachedHeight;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the HideSelection property changes.
        /// </summary>
        [Description("Occurs when the value of the HideSelection property changes.")]
        [Category("Property Changed")]
        public event EventHandler HideSelectionChanged;

        /// <summary>
        /// Occurs when the value of the TextAlign property changes.
        /// </summary>
        [Description("Occurs when the value of the TextAlign property changes.")]
        [Category("Property Changed")]
        public event EventHandler TextAlignChanged;

        /// <summary>
        /// Occurs when the value of the Modified property changes.
        /// </summary>
        [Description("Occurs when the value of the Modified property changes.")]
        [Category("Property Changed")]
        public event EventHandler ModifiedChanged;

        /// <summary>
        /// Occurs when the value of the ReadOnly property changes.
        /// </summary>
        [Description("Occurs when the value of the ReadOnly property changes.")]
        [Category("Property Changed")]
        public event EventHandler ReadOnlyChanged;

        /// <summary>
        /// Occurs when the value of the Mask property changes.
        /// </summary>
        [Description("Occurs when the value of the Mask property changes.")]
        [Category("Property Changed")]
        public event EventHandler MaskChanged;

        /// <summary>
        /// Occurs when the value of the IsOverwriteMode property changes.
        /// </summary>
        [Description("Occurs when the value of the IsOverwriteMode property changes.")]
        [Category("Property Changed")]
        public event EventHandler IsOverwriteModeChanged;

        /// <summary>
        /// Occurs when the input character or text does not comply with the mask specification.
        /// </summary>
        [Description("Occurs when the input character or text does not comply with the mask specification.")]
        [Category("Behavior")]
        public event MaskInputRejectedEventHandler MaskInputRejected;

        /// <summary>
        /// Occurs when the validating type object has completed parsing the input text.
        /// </summary>
        [Description("Occurs when the validating type object has completed parsing the input text.")]
        [Category("Focus")]
        public event TypeValidationEventHandler TypeValidationCompleted;

        /// <summary>
        /// Occurs when the mouse enters the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler TrackMouseEnter;

        /// <summary>
        /// Occurs when the mouse leaves the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler TrackMouseLeave;

        /// <summary>
        /// Occurs when the value of the BackColor property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackColorChanged;

        /// <summary>
        /// Occurs when the value of the BackgroundImage property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged;

        /// <summary>
        /// Occurs when the value of the BackgroundImageLayout property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageLayoutChanged;

        /// <summary>
        /// Occurs when the value of the ForeColor property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler ForeColorChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMaskedTextBox class.
		/// </summary>
        public KryptonMaskedTextBox()
        {
            // Contains another control and needs marking as such for validation to work
            SetStyle(ControlStyles.ContainerControl, true);

            // By default we are not multiline and so the height is fixed
            SetStyle(ControlStyles.FixedHeight, true);

            // Cannot select this control, only the child TextBox
            SetStyle(ControlStyles.Selectable, false);

            // Defaults
            _inputControlStyle = InputControlStyle.Standalone;
            _autoSize = true;
            _cachedHeight = -1;
            _alwaysActive = true;
            _allowButtonSpecToolTips = false;

            // Create storage properties
            _buttonSpecs = new MaskedTextBoxButtonSpecCollection(this);

            // Create the palette storage
            _stateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
            _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateActive = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);

            // Create the internal text box used for containing content
            _maskedTextBox = new InternalMaskedTextBox(this);
            _maskedTextBox.TrackMouseEnter += new EventHandler(OnMaskedTextBoxMouseChange);
            _maskedTextBox.TrackMouseLeave += new EventHandler(OnMaskedTextBoxMouseChange);
            _maskedTextBox.TextAlignChanged += new EventHandler(OnMaskedTextBoxTextAlignChanged);
            _maskedTextBox.TextChanged += new EventHandler(OnMaskedTextBoxTextChanged);
            _maskedTextBox.HideSelectionChanged += new EventHandler(OnMaskedTextBoxHideSelectionChanged);
            _maskedTextBox.ModifiedChanged += new EventHandler(OnMaskedTextBoxModifiedChanged);
            _maskedTextBox.ReadOnlyChanged += new EventHandler(OnMaskedTextBoxReadOnlyChanged);
            _maskedTextBox.MaskChanged += new EventHandler(OnMaskedMaskChanged);
            _maskedTextBox.IsOverwriteModeChanged += new EventHandler(OnMaskedIsOverwriteModeChanged);
            _maskedTextBox.MaskInputRejected += new MaskInputRejectedEventHandler(OnMaskedMaskInputRejected);
            _maskedTextBox.TypeValidationCompleted += new TypeValidationEventHandler(OnMaskedTypeValidationCompleted);
            _maskedTextBox.GotFocus += new EventHandler(OnMaskedTextBoxGotFocus);
            _maskedTextBox.LostFocus += new EventHandler(OnMaskedTextBoxLostFocus);
            _maskedTextBox.KeyDown += new KeyEventHandler(OnMaskedTextBoxKeyDown);
            _maskedTextBox.KeyUp += new KeyEventHandler(OnMaskedTextBoxKeyUp);
            _maskedTextBox.KeyPress += new KeyPressEventHandler(OnMaskedTextBoxKeyPress);
            _maskedTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnMaskedTextBoxPreviewKeyDown);
            _maskedTextBox.Validating += new CancelEventHandler(OnMaskedTextBoxValidating);
            _maskedTextBox.Validated += new EventHandler(OnMaskedTextBoxValidated);

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_maskedTextBox);

            // Create inner view for placing inside the drawing docker
            _drawDockerInner = new ViewLayoutDocker();
            _drawDockerInner.Add(_layoutFill, ViewDockStyle.Fill);

            // Create view for the control border and background
            _drawDockerOuter = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border);
            _drawDockerOuter.Add(_drawDockerInner, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDockerOuter);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerLayout(this, Redirector, _buttonSpecs, null,
                                                         new ViewLayoutDocker[] { _drawDockerInner },
                                                         new IPaletteMetric[] { _stateCommon },
                                                         new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetInputControl },
                                                         new PaletteMetricPadding[] { PaletteMetricPadding.HeaderButtonPaddingInputControl },
                                                         new GetToolStripRenderer(CreateToolStripRenderer),
                                                         NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;

            // Add text box to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_maskedTextBox);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);

                // Remember to pull down the manager instance
                _buttonManager.Destruct();
            }

            base.Dispose(disposing);
        }
        #endregion

		#region Public
        /// <summary>
        /// Overridden. Returns a string that represents the current masked text box. This method overrides ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _maskedTextBox.ToString();
        }

        /// <summary>
        /// Gets and sets if the control is in the tab chain.
        /// </summary>
        public new bool TabStop
        {
            get { return _maskedTextBox.TabStop; }
            set { _maskedTextBox.TabStop = value; }
        }

        /// <summary>
        /// Gets and sets if the control is in the ribbon design mode.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool InRibbonDesignMode
        {
            get { return _inRibbonDesignMode; }
            set { _inRibbonDesignMode = value; }
        }

        /// <summary>
        /// Gets access to the contained MaskedTextBox instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public MaskedTextBox MaskedTextBox
        {
            get { return _maskedTextBox; }
        }

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl
        {
            get { return MaskedTextBox; }
        }

        /// <summary>
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        [Browsable(false)]
        public override bool Focused
        {
            get { return MaskedTextBox.Focused; }
        }

        /// <summary>
        /// Gets and sets a value indicating if the control is automatically sized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoSize
        {
            get { return _autoSize; }
            
            set
            {
                if (_autoSize != value)
                {
                    _autoSize = value;

                    // ...need to update the fixed height setting
                    SetStyle(ControlStyles.FixedHeight, value);

                    // Add adjust actual height to match new setting
                    AdjustHeight(false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        public override string Text
        {
            get { return _maskedTextBox.Text; }
            set { _maskedTextBox.Text = value; }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }

            set
            {
                base.ContextMenuStrip = value;
                _maskedTextBox.ContextMenuStrip = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the contents have changed since last last.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get { return _maskedTextBox.Modified; }
        }

        /// <summary>
        /// Gets and sets the selected text within the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get { return _maskedTextBox.SelectedText; }
            set { _maskedTextBox.SelectedText = value; }
        }

        /// <summary>
        /// Gets and sets the selection length for the selected area.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get { return _maskedTextBox.SelectionLength; }
            set { _maskedTextBox.SelectionLength = value; }
        }

        /// <summary>
        /// Gets and sets the starting point of text selected in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return _maskedTextBox.SelectionStart; }
            set { _maskedTextBox.SelectionStart = value; }
        }

        /// <summary>
        /// Gets the length of text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLength
        {
            get { return _maskedTextBox.TextLength; }
        }

        /// <summary>
        /// Gets a value that specifies whether new user input overwrites existing input.
        /// </summary>
        [Browsable(false)]
        public bool IsOverwriteMode
        {
            get { return _maskedTextBox.IsOverwriteMode; }
        }

        /// <summary>
        /// Gets a value indicating whether all required inputs have been entered into the input mask.
        /// </summary>
        [Browsable(false)]
        public bool MaskCompleted
        {
            get { return _maskedTextBox.MaskCompleted; }
        }

        /// <summary>
        /// Gets a clone of the mask provider associated with this instance of the masked text box control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MaskedTextProvider MaskedTextProvider
        {
            get { return _maskedTextBox.MaskedTextProvider; }
        }

        /// <summary>
        /// Gets a value indicating whether all required and optional inputs have been entered into the input mask.
        /// </summary>
        [Browsable(false)]
        public bool MaskFull
        {
            get { return _maskedTextBox.MaskFull; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxLength
        {
            get { return _maskedTextBox.MaxLength; }
            set { _maskedTextBox.MaxLength = value; }
        }

        /// <summary>
        /// Gets or sets the data type used to verify the data input by the user.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        public Type ValidatingType
        {
            get { return _maskedTextBox.ValidatingType; }
            set { _maskedTextBox.ValidatingType = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
        /// </summary>
        [Category("Appearance")]
        [Description("Defines if mnemonic characters generate click events for button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _buttonManager.UseMnemonic; }

            set
            {
                if (_buttonManager.UseMnemonic != value)
                {
                    _buttonManager.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        [DefaultValue(true)]
        public bool AlwaysActive
        {
            get { return _alwaysActive; }

            set
            {
                if (_alwaysActive != value)
                {
                    _alwaysActive = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets how the text should be aligned for edit controls.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates how the text should be aligned for edit controls.")]
        [DefaultValue(typeof(HorizontalAlignment), "Left")]
        [Localizable(true)]
        public HorizontalAlignment TextAlign
        {
            get { return _maskedTextBox.TextAlign; }
            set { _maskedTextBox.TextAlign = value; }
        }

        /// <summary>
        /// Indicates the character used as the placeholder.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the character used as the placeholder.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue('_')]
        [Localizable(true)]
        public char PromptChar
        {
            get { return _maskedTextBox.PromptChar; }
            set { _maskedTextBox.PromptChar = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PromptChar can be entered as valid data by the user.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the prompt character is valid as input.")]
        [DefaultValue(true)]
        public bool AllowPromptAsInput
        {
            get { return _maskedTextBox.AllowPromptAsInput; }
            set { _maskedTextBox.AllowPromptAsInput = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the MaskedTextBox control accepts characters outside of the ASCII character set.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether only Ascii characters are valid as input.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool AsciiOnly
        {
            get { return _maskedTextBox.AsciiOnly; }
            set { _maskedTextBox.AsciiOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the control will beep when an invalid character is typed.")]
        [DefaultValue(false)]
        public bool BeepOnError
        {
            get { return _maskedTextBox.BeepOnError; }
            set { _maskedTextBox.BeepOnError = value; }
        }

        /// <summary>
        /// Gets or sets the culture information associated with the masked text box.
        /// </summary>
        [Category("Behavior")]
        [Description("The culture that determines the value of the locaizable mask language separators and placeholders.")]
        [RefreshProperties(RefreshProperties.All)]
        public CultureInfo Culture
        {
            get { return _maskedTextBox.Culture; }
            set { _maskedTextBox.Culture = value; }
        }

        private bool ShouldSerializeCulture()
        {
            return !CultureInfo.CurrentCulture.Equals(Culture);
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat CutCopyMaskFormat
        {
            get { return _maskedTextBox.CutCopyMaskFormat; }
            set { _maskedTextBox.CutCopyMaskFormat = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether prompt characters are displayed when the control does not have focus.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool HidePromptOnLeave
        {
            get { return _maskedTextBox.HidePromptOnLeave; }
            set { _maskedTextBox.HidePromptOnLeave = value; }
        }

        /// <summary>
        /// Gets or sets the text insertion mode of the masked text box control.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the masked text box input character typing mode.")]
        [DefaultValue(typeof(InsertKeyMode), "Default")]
        public InsertKeyMode InsertKeyMode
        {
            get { return _maskedTextBox.InsertKeyMode; }
            set { _maskedTextBox.InsertKeyMode = value; }
        }

        /// <summary>
        /// Gets or sets the input mask to use at run time. 
        /// </summary>
        [Category("Behavior")]
        [Description("Sets the string governing the input allowed for the control.")]
        [RefreshProperties(RefreshProperties.All)]
        [MergableProperty(false)]
        [DefaultValue("")]
        [Localizable(true)]
        public string Mask
        {
            get { return _maskedTextBox.Mask; }
            set { _maskedTextBox.Mask = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        [DefaultValue(true)]
        public bool HideSelection
        {
            get { return _maskedTextBox.HideSelection; }
            set { _maskedTextBox.HideSelection = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text in the edit control can be changed or not.
        /// </summary>
        [Category("Behavior")]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _maskedTextBox.ReadOnly; }
            set { _maskedTextBox.ReadOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.
        /// </summary>
        [Category("Behavior")]
        [Description("If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
        [DefaultValue(false)]
        public bool RejectInputOnFirstFailure
        {
            get { return _maskedTextBox.RejectInputOnFirstFailure; }
            set { _maskedTextBox.RejectInputOnFirstFailure = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines how an input character that matches the prompt character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
        [DefaultValue(true)]
        public bool ResetOnPrompt
        {
            get { return _maskedTextBox.ResetOnPrompt; }
            set { _maskedTextBox.ResetOnPrompt = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines how a space input character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
        [DefaultValue(true)]
        public bool ResetOnSpace
        {
            get { return _maskedTextBox.ResetOnSpace; }
            set { _maskedTextBox.ResetOnSpace = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
        [DefaultValue(true)]
        public bool SkipLiterals
        {
            get { return _maskedTextBox.SkipLiterals; }
            set { _maskedTextBox.SkipLiterals = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public MaskFormat TextMaskFormat
        {
            get { return _maskedTextBox.TextMaskFormat; }
            set { _maskedTextBox.TextMaskFormat = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether shortcuts defined for the control are enabled.")]
        [DefaultValue(true)]
        public bool ShortcutsEnabled
        {
            get { return _maskedTextBox.ShortcutsEnabled; }
            set { _maskedTextBox.ShortcutsEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a the character to display for password input for single-line edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the character to display for password input for single-line edit controls.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue('\0')]
        [Localizable(true)]
        public char PasswordChar
        {
            get { return _maskedTextBox.PasswordChar; }
            set { _maskedTextBox.PasswordChar = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return _maskedTextBox.UseSystemPasswordChar; }
            set { _maskedTextBox.UseSystemPasswordChar = value; }
        }

        /// <summary>
		/// Gets and sets the input control style.
		/// </summary>
		[Category("Visuals")]
		[Description("Input control style.")]
        public InputControlStyle InputControlStyle
		{
            get 
            {
                return _inputControlStyle; 
            }

			set
			{
                if (_inputControlStyle != value)
				{
                    _inputControlStyle = value;
                    _stateCommon.SetStyles(value);
					PerformNeedPaint(true);
				}
			}
		}

        private bool ShouldSerializeInputControlStyle()
        {
            return (InputControlStyle != InputControlStyle.Standalone);
        }

        private void ResetInputControlStyle()
        {
            InputControlStyle = InputControlStyle.Standalone;
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _allowButtonSpecToolTips; }
            set { _allowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MaskedTextBoxButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets access to the common textbox appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common textbox appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
        /// Gets access to the disabled textbox appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled textbox appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
        /// Gets access to the normal textbox appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal textbox appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

        /// <summary>
        /// Gets access to the active textbox appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active textbox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
        }

        /// <summary>
        /// Appends text to the current text of a rich text box.
        /// </summary>
        /// <param name="text">The text to append to the current contents of the text box.</param>
        public void AppendText(string text)
        {
            _maskedTextBox.AppendText(text);
        }

        /// <summary>
        /// Clears all text from the text box control.
        /// </summary>
        public void Clear()
        {
            _maskedTextBox.Clear();
        }

        /// <summary>
        /// Copies the current selection in the text box to the Clipboard.
        /// </summary>
        public void Copy()
        {
            _maskedTextBox.Copy();
        }

        /// <summary>
        /// Moves the current selection in the text box to the Clipboard.
        /// </summary>
        public void Cut()
        {
            _maskedTextBox.Cut();
        }

        /// <summary>
        /// Replaces the current selection in the text box with the contents of the Clipboard.
        /// </summary>
        public void Paste()
        {
            _maskedTextBox.Paste();
        }

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _maskedTextBox.Select(start, length);
        }

        /// <summary>
        /// Selects all text in the control.
        /// </summary>
        public void SelectAll()
        {
            _maskedTextBox.SelectAll();
        }

        /// <summary>
        /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
        /// </summary>
        public void DeselectAll()
        {
            _maskedTextBox.DeselectAll();
        }

        /// <summary>
        /// Retrieves the character that is closest to the specified location within the control.
        /// </summary>
        /// <param name="pt">The location from which to seek the nearest character.</param>
        /// <returns>The character at the specified location.</returns>
        public int GetCharFromPosition(Point pt)
        {
            return _maskedTextBox.GetCharFromPosition(pt);
        }

        /// <summary>
        /// Retrieves the index of the character nearest to the specified location.
        /// </summary>
        /// <param name="pt">The location to search.</param>
        /// <returns>The zero-based character index at the specified location.</returns>
        public int GetCharIndexFromPosition(Point pt)
        {
            return _maskedTextBox.GetCharIndexFromPosition(pt);
        }

        /// <summary>
        /// Retrieves the location within the control at the specified character index.
        /// </summary>
        /// <param name="index">The index of the character for which to retrieve the location.</param>
        /// <returns>The location of the specified character.</returns>
        public Point GetPositionFromCharIndex(int index)
        {
            return _maskedTextBox.GetPositionFromCharIndex(index);
        }

        /// <summary>
        /// Sets the fixed state of the control.
        /// </summary>
        /// <param name="active">Should the control be fixed as active.</param>
        public void SetFixedState(bool active)
        {
            _fixedActive = active;
        }

        /// <summary>
        /// Converts the user input string to an instance of the validating type.
        /// </summary>
        /// <returns></returns>
        public object ValidateText()
        {
            return _maskedTextBox.ValidateText();
        }

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
        }

        /// <summary>
        /// Gets a value indicating if the input control is active.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsActive
        {
            get 
            {
                if (_fixedActive != null)
                    return _fixedActive.Value;
                else
                    return (DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _maskedTextBox.MouseOver); 
            }
        }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus()
        {
            if (MaskedTextBox != null)
                return MaskedTextBox.Focus();
            else
                return false;
        }

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select()
        {
            if (MaskedTextBox != null)
                MaskedTextBox.Select();
        }

        /// <summary>
        /// Get the preferred size of the control based on a proposed size.
        /// </summary>
        /// <param name="proposedSize">Starting size proposed by the caller.</param>
        /// <returns>Calculated preferred size.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            // Do we have a manager to ask for a preferred size?
            if (ViewManager != null)
            {
                // Ask the view to peform a layout
                Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

                // Apply the maximum sizing
                if (MaximumSize.Width > 0)  retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
                if (MaximumSize.Height > 0) retSize.Height = Math.Min(MaximumSize.Height, retSize.Width);

                // Apply the minimum sizing
                if (MinimumSize.Width > 0)  retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
                if (MinimumSize.Height > 0) retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);

                return retSize;
            }
            else
            {
                // Fall back on default control processing
                return base.GetPreferredSize(proposedSize);
            }
        }

        /// <summary>
		/// Gets the rectangle that represents the display area of the control.
		/// </summary>
		public override Rectangle DisplayRectangle
		{
			get
			{
                // Ensure that the layout is calculated in order to know the remaining display space
                ForceViewLayout();

                // The inside text box is the client rectangle size
                return new Rectangle(_maskedTextBox.Location, _maskedTextBox.Size);
			}
		}

        /// <summary>
        /// Override the display padding for the layout fill.
        /// </summary>
        /// <param name="padding">Display padding value.</param>
        public void SetLayoutDisplayPadding(Padding padding)
        {
            _layoutFill.DisplayPadding = padding;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool DesignerGetHitTest(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return false;

            // Check if any of the button specs want the point
            if ((_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Force the layout logic to size and position the controls.
        /// </summary>
        protected void ForceControlLayout()
        {
            _forcedLayout = true;
            OnLayout(new LayoutEventArgs(null, null));
            _forcedLayout = false;
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the TextAlignChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTextAlignChanged(EventArgs e)
        {
            if (TextAlignChanged != null)
                TextAlignChanged(this, e);
        }

        /// <summary>
        /// Raises the HideSelectionChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            if (HideSelectionChanged != null)
                HideSelectionChanged(this, e);
        }

        /// <summary>
        /// Raises the ModifiedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnModifiedChanged(EventArgs e)
        {
            if (ModifiedChanged != null)
                ModifiedChanged(this, e);
        }

        /// <summary>
        /// Raises the ReadOnlyChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (ReadOnlyChanged != null)
                ReadOnlyChanged(this, e);
        }

        /// <summary>
        /// Raises the MaskChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnMaskChanged(EventArgs e)
        {
            if (MaskChanged != null)
                MaskChanged(this, e);
        }

        /// <summary>
        /// Raises the IsOverwriteModeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnIsOverwriteModeChanged(EventArgs e)
        {
            if (IsOverwriteModeChanged != null)
                IsOverwriteModeChanged(this, e);
        }

        /// <summary>
        /// Raises the MaskInputRejected event.
        /// </summary>
        /// <param name="e">An MaskInputRejectedEventArgs that contains the event data.</param>
        protected virtual void OnMaskInputRejected(MaskInputRejectedEventArgs e)
        {
            if (MaskInputRejected != null)
                MaskInputRejected(this, e);
        }

        /// <summary>
        /// Raises the TypeValidationCompleted event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnTypeValidationCompleted(TypeValidationEventArgs e)
        {
            if (TypeValidationCompleted != null)
                TypeValidationCompleted(this, e);
        }

        /// <summary>
        /// Raises the TrackMouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTrackMouseEnter(EventArgs e)
        {
            if (TrackMouseEnter != null)
                TrackMouseEnter(this, e);
        }

        /// <summary>
        /// Raises the TrackMouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTrackMouseLeave(EventArgs e)
        {
            if (TrackMouseLeave != null)
                TrackMouseLeave(this, e);
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new instance of the control collection for the KryptonTextBox.
        /// </summary>
        /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new KryptonReadOnlyControls(this);
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // Let base class do standard stuff
            base.OnHandleCreated(e);

            // Force the font to be set into the text box child control
            PerformNeedPaint(false);

            // We need a layout to occur before any painting
            InvokeLayout();

            // We need to recalculate the correct height
            AdjustHeight(true);
        }

        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Change in enabled state requires a layout and repaint
            UpdateStateAndPalettes();

            // Update view elements
            _drawDockerInner.Enabled = Enabled;
            _drawDockerOuter.Enabled = Enabled;

            // Update state to reflect change in enabled state
            _buttonManager.RefreshButtons();

            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the BackColorChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            if (BackColorChanged != null)
                BackColorChanged(this, e);
        }

        /// <summary>
        /// Raises the BackgroundImageChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            if (BackgroundImageChanged != null)
                BackgroundImageChanged(this, e);
        }

        /// <summary>
        /// Raises the BackgroundImageLayoutChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackgroundImageLayoutChanged(EventArgs e)
        {
            if (BackgroundImageLayoutChanged != null)
                BackgroundImageLayoutChanged(this, e);
        }

        /// <summary>
        /// Raises the ForeColorChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            if (ForeColorChanged != null)
                ForeColorChanged(this, e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            // Let base class raise events
            base.OnResize(e);

            // We must have a layout calculation
            ForceControlLayout();
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!IsDisposed && !Disposing)
            {
                // Update with latest content padding for placing around the contained text box control
                Padding contentPadding = GetTripleState().PaletteContent.GetContentPadding(_drawDockerOuter.State);
                _layoutFill.DisplayPadding = contentPadding;
            }

            // Ensure the height is correct
            AdjustHeight(false);

            // Let base class calulcate fill rectangle
            base.OnLayout(levent);

            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (_forcedLayout || (DesignMode && (_maskedTextBox != null)))
            {
                Rectangle fillRect = _layoutFill.FillRect;
                _maskedTextBox.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
            }
        }

        /// <summary>
        /// Raises the MouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseOver = true;
            PerformNeedPaint(true);
            _maskedTextBox.Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseOver = false;
            PerformNeedPaint(true);
            _maskedTextBox.Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _maskedTextBox.Focus();
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new Left property value of the control.</param>
        /// <param name="y">The new Top property value of the control.</param>
        /// <param name="width">The new Width property value of the control.</param>
        /// <param name="height">The new Height property value of the control.</param>
        /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
        protected override void SetBoundsCore(int x, int y, 
                                              int width, int height, 
                                              BoundsSpecified specified)
        {
            // Do we need to prevent the height from being altered?
            if (_autoSize)
            {
                switch (Dock)
                {
                    case DockStyle.Fill:
                    case DockStyle.Left:
                    case DockStyle.Right:
                        if ((specified & ~BoundsSpecified.Height) == specified)
                            _cachedHeight = height;
                        break;
                }

                // Override the actual height used to the fixed height for single line
                height = PreferredHeight;
            }
            else
                _cachedHeight = height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(100, PreferredHeight); }
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (!e.NeedLayout)
                _maskedTextBox.Invalidate();
            else
                ForceControlLayout();

            if (!IsDisposed && !Disposing)
            {
                // Update the back/fore/font from the palette settings
                UpdateStateAndPalettes();
                IPaletteTriple triple = GetTripleState();
                PaletteState state = _drawDockerOuter.State;
                _maskedTextBox.BackColor = triple.PaletteBack.GetBackColor1(state);
                _maskedTextBox.ForeColor = triple.PaletteContent.GetContentShortTextColor1(state);

                // Only set the font if the masked text box has been created
                Font font = triple.PaletteContent.GetContentShortTextFont(state);
                if ((_maskedTextBox.Handle != IntPtr.Zero) && !_maskedTextBox.Font.Equals(font))
                    _maskedTextBox.Font = font;
            }

            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Raises the PaddingChanged event.
        /// </summary>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            // Add adjust actual height to match new setting
            AdjustHeight(false);
        }

        /// <summary>
        /// Raises the TabStop event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnTabStopChanged(EventArgs e)
        {
            MaskedTextBox.TabStop = TabStop;
            base.OnTabStopChanged(e);
        }

        /// <summary>
        /// Raises the CausesValidationChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnCausesValidationChanged(EventArgs e)
        {
            MaskedTextBox.CausesValidation = CausesValidation;
            base.OnCausesValidationChanged(e);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_NCHITTEST:
                    if (InTransparentDesignMode)
                        m.Result = (IntPtr)PI.HTTRANSPARENT;
                    else
                        base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region Internal
        internal bool InTransparentDesignMode
        {
            get { return InRibbonDesignMode; }
        }
        #endregion

        #region Implementation
        private void UpdateStateAndPalettes()
        {
            // Get the correct palette settings to use
            IPaletteTriple tripleState = GetTripleState();
            _drawDockerOuter.SetPalettes(tripleState.PaletteBack, tripleState.PaletteBorder);
            
            // Update enabled state
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state;
            if (IsActive)
                state = PaletteState.Tracking;
            else
                state = PaletteState.Normal;

            _drawDockerOuter.ElementState = state;
        }

        internal IPaletteTriple GetTripleState()
        {
            if (Enabled)
            {
                if (IsActive)
                    return _stateActive;
                else
                    return _stateNormal;
            }
            else
                return _stateDisabled;
        }

        private int PreferredHeight
        {
            get
            {
                // Get the preferred size of the entire control
                Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

                // We only need to the height
                return preferredSize.Height;
            }
        }

        private void AdjustHeight(bool ignoreAnchored)
        {
            // If any of the vertical edges are anchored then we might need to ignore the call
            if (!ignoreAnchored || ((Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top)))
            {
                if (_autoSize)
                    Height = PreferredHeight;
                else
                    Height = _cachedHeight;
            }
        }

        private void OnMaskedTextBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void OnMaskedTextBoxTextAlignChanged(object sender, EventArgs e)
        {
            OnTextAlignChanged(e);
        }

        private void OnMaskedTextBoxHideSelectionChanged(object sender, EventArgs e)
        {
            OnHideSelectionChanged(e);
        }

        private void OnMaskedTextBoxModifiedChanged(object sender, EventArgs e)
        {
            OnModifiedChanged(e);
        }

        private void OnMaskedTextBoxReadOnlyChanged(object sender, EventArgs e)
        {
            OnReadOnlyChanged(e);
        }

        private void OnMaskedMaskChanged(object sender, EventArgs e)
        {
            OnMaskChanged(e);
        }

        private void OnMaskedIsOverwriteModeChanged(object sender, EventArgs e)
        {
            OnIsOverwriteModeChanged(e);
        }

        private void OnMaskedMaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            OnMaskInputRejected(e);
        }

        private void OnMaskedTypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            OnTypeValidationCompleted(e);
        }

        private void OnMaskedTextBoxGotFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            OnGotFocus(e);
        }

        private void OnMaskedTextBoxLostFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            OnLostFocus(e);
        }

        private void OnMaskedTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnMaskedTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnMaskedTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnMaskedTextBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnMaskedTextBoxValidated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void OnMaskedTextBoxValidating(object sender, CancelEventArgs e)
        {
            OnValidating(e);
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed && !Disposing)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!DesignMode)
                {
                    IContentValues sourceContent = null;
                    LabelStyle toolTipStyle = LabelStyle.ToolTip;

                    // Find the button spec associated with the tooltip request
                    ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                    // If the tooltip is for a button spec
                    if (buttonSpec != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (AllowButtonSpecToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                            // Is there actually anything to show for the tooltip
                            if (buttonSpecMapping.HasContent)
                            {
                                sourceContent = buttonSpecMapping;
                                toolTipStyle = buttonSpec.ToolTipStyle;
                            }
                        }
                    }

                    if (sourceContent != null)
                    {
                        // Remove any currently showing tooltip
                        if (_visualPopupToolTip != null)
                            _visualPopupToolTip.Dispose();

                        // Create the actual tooltip popup object
                        _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                     sourceContent,
                                                                     Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // Show relative to the provided screen rectangle
                        _visualPopupToolTip.ShowCalculatingSize(RectangleToScreen(e.Target.ClientRectangle));
                    }
                }
            }
        }

        private void OnCancelToolTip(object sender, EventArgs e)
        {
            // Remove any currently showing tooltip
            if (_visualPopupToolTip != null)
                _visualPopupToolTip.Dispose();
        }

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnMaskedTextBoxMouseChange(object sender, EventArgs e)
        {
            // Change in tracking state?
            if (_maskedTextBox.MouseOver != _trackingMouseEnter)
            {
                _trackingMouseEnter = _maskedTextBox.MouseOver;

                // Raise appropriate event
                if (_trackingMouseEnter)
                    OnTrackMouseEnter(EventArgs.Empty);
                else
                    OnTrackMouseLeave(EventArgs.Empty);
            }
        }
        #endregion
    }
}
