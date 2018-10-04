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
    /// Provide a ListBox with Krypton styling applied.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("Items")]
    [DefaultBindingProperty("SelectedValue")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonListBoxDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Represents a list box control that allows single or multiple item selection.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonListBox : VisualControlBase,
                                  IContainedInputControl
    {
        #region Classes
        private class InternalListBox : ListBox
        {
            #region Instance Fields
            private ViewManager _viewManager;
            private ViewDrawPanel _drawPanel;
            private KryptonListBox _kryptonListBox;
            private IntPtr _screenDC;
            private bool _mouseOver;
            private int _mouseIndex;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalListBox.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalListBox.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the InternalListBox class.
            /// </summary>
            /// <param name="kryptonListBox">Reference to owning control.</param>
            public InternalListBox(KryptonListBox kryptonListBox)
            {
                SetStyle(ControlStyles.ResizeRedraw, true);

                _kryptonListBox = kryptonListBox;
                _mouseIndex = -1;

                // Create manager and view for drawing the background
                _drawPanel = new ViewDrawPanel();
                _viewManager = new ViewManager(this, _drawPanel);

                // Set required properties to act as an owner draw list box
                base.Size = Size.Empty;
                base.BorderStyle = BorderStyle.None;
                base.IntegralHeight = false;
                base.MultiColumn = false;
                base.DrawMode = DrawMode.OwnerDrawVariable;

                // We need to create and cache a device context compatible with the display
                _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
            }

            /// <summary>
            /// Releases all resources used by the Control. 
            /// </summary>
            /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (_screenDC != IntPtr.Zero)
                    PI.DeleteDC(_screenDC);
            }
            #endregion

            #region Public
            /// <summary>
            /// Recreate the window handle.
            /// </summary>
            public void Recreate()
            {
                RecreateHandle();
            }

            /// <summary>
            /// Gets access to the contained view draw panel instance.
            /// </summary>
            public ViewDrawPanel ViewDrawPanel
            {
                get { return _drawPanel; }
            }

            /// <summary>
            /// Gets the item index the mouse is over.
            /// </summary>
            public int MouseIndex
            {
                get { return _mouseIndex; }
            }

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
                        {
                            OnTrackMouseLeave(EventArgs.Empty);
                            _mouseIndex = -1;
                        }
                    }
                }
            }

            /// <summary>
            /// Gets and sets the drawing mode of the checked list box.
            /// </summary>
            public override DrawMode DrawMode
            {
                get { return DrawMode.OwnerDrawVariable; }
                set { }
            }

            /// <summary>
            /// Force the remeasure of items so they are sized correctly.
            /// </summary>
            public void RefreshItemSizes()
            {
                base.DrawMode = DrawMode.OwnerDrawFixed;
                base.DrawMode = DrawMode.OwnerDrawVariable;
            }
            #endregion

            #region Protected
            /// <summary>
            /// Raises the Layout event.
            /// </summary>
            /// <param name="levent">A LayoutEventArgs containing the event data.</param>
            protected override void OnLayout(LayoutEventArgs levent)
            {
                base.OnLayout(levent);

                // Ask the panel to layout given our available size
                using (ViewLayoutContext context = new ViewLayoutContext(_viewManager, this, _kryptonListBox, _kryptonListBox.Renderer))
                    _drawPanel.Layout(context);
            }

            /// <summary>
            /// Process Windows-based messages.
            /// </summary>
            /// <param name="m">A Windows-based message.</param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_ERASEBKGND:
                        // Do not draw the background here, always do it in the paint 
                        // instead to prevent flicker because of a two stage drawing process
                        break;
                    case PI.WM_PRINTCLIENT:
                    case PI.WM_PAINT:
                        WmPaint(ref m);
                        break;
                    case PI.WM_VSCROLL:
                    case PI.WM_HSCROLL:
                    case PI.WM_MOUSEWHEEL:
                        Invalidate();
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        // Mouse is not over the control
                        MouseOver = false;
                        _kryptonListBox.PerformNeedPaint(true);
                        Invalidate();
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            MouseOver = true;
                            _kryptonListBox.PerformNeedPaint(true);
                            Invalidate();
                        }
                        else
                        {
                            // Find the item under the mouse
                            Point mousePoint = new Point((int)m.LParam.ToInt64());
                            int mouseIndex = IndexFromPoint(mousePoint);

                            // If we have an actual item from the point
                            if ((mouseIndex >= 0) && (mouseIndex < Items.Count))
                            {
                                // Check that the mouse really is in the item rectangle
                                Rectangle indexRect = GetItemRectangle(mouseIndex);
                                if (!indexRect.Contains(mousePoint))
                                    mouseIndex = -1;
                            }

                            // If item under mouse has changed, then need to reflect for tracking
                            if (_mouseIndex != mouseIndex)
                            {
                                Invalidate();
                                _mouseIndex = mouseIndex;
                            }
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

            #region Private
            private void WmPaint(ref Message m)
            {
                IntPtr hdc;
                PI.PAINTSTRUCT ps = new PI.PAINTSTRUCT();

                // Do we need to BeginPaint or just take the given HDC?
                if (m.WParam == IntPtr.Zero)
                    hdc = PI.BeginPaint(Handle, ref ps);
                else
                    hdc = m.WParam;

                // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                Rectangle realRect = CommonHelper.RealClientRectangle(Handle);

                // No point drawing when one of the dimensions is zero
                if ((realRect.Width > 0) && (realRect.Height > 0))
                {
                    IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, realRect.Width, realRect.Height);

                    // If we managed to get a compatible bitmap
                    if (hBitmap != IntPtr.Zero)
                    {
                        try
                        {
                            // Must use the screen device context for the bitmap when drawing into the 
                            // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                            PI.SelectObject(_screenDC, hBitmap);

                            // Easier to draw using a graphics instance than a DC!
                            using (Graphics g = Graphics.FromHdc(_screenDC))
                            {
                                // Ask the view element to layout in given space, needs this before a render call
                                using (ViewLayoutContext context = new ViewLayoutContext(this, _kryptonListBox.Renderer))
                                {
                                    context.DisplayRectangle = realRect;
                                    _drawPanel.Layout(context);
                                }

                                using (RenderContext context = new RenderContext(this, _kryptonListBox, g, realRect, _kryptonListBox.Renderer))
                                    _drawPanel.Render(context);

                                // Replace given DC with the screen DC for base window proc drawing
                                IntPtr beforeDC = m.WParam;
                                m.WParam = _screenDC;
                                DefWndProc(ref m);
                                m.WParam = beforeDC;

                                if (Items.Count == 0)
                                    using (RenderContext context = new RenderContext(this, _kryptonListBox, g, realRect, _kryptonListBox.Renderer))
                                        _drawPanel.Render(context);
                            }

                            // Now blit from the bitmap from the screen to the real dc
                            PI.BitBlt(hdc, 0, 0, realRect.Width, realRect.Height, _screenDC, 0, 0, PI.SRCCOPY);

                            // When disabled with no items the above code does not draw the backround!
                            if (Items.Count == 0)
                                using (Graphics g = Graphics.FromHdc(hdc))
                                    using (RenderContext context = new RenderContext(this, _kryptonListBox, g, realRect, _kryptonListBox.Renderer))
                                        _drawPanel.Render(context);
                        }
                        finally
                        {
                            // Delete the temporary bitmap
                            PI.DeleteObject(hBitmap);
                        }
                    }
                }

                // Do we need to match the original BeginPaint?
                if (m.WParam == IntPtr.Zero)
                    PI.EndPaint(Handle, ref ps);
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private PaletteListStateRedirect _stateCommon;
        private PaletteListState _stateDisabled;
        private PaletteListState _stateNormal;
        private PaletteDouble _stateActive;
        private PaletteListItemTriple _stateTracking;
        private PaletteListItemTriple _statePressed;
        private PaletteListItemTriple _stateCheckedNormal;
        private PaletteListItemTriple _stateCheckedTracking;
        private PaletteListItemTriple _stateCheckedPressed;
        private PaletteListItemTripleRedirect _stateFocus;
        private PaletteTripleOverride _overrideNormal;
        private PaletteTripleOverride _overrideTracking;
        private PaletteTripleOverride _overridePressed;
        private PaletteTripleOverride _overrideCheckedNormal;
        private PaletteTripleOverride _overrideCheckedTracking;
        private PaletteTripleOverride _overrideCheckedPressed;
        private ViewLayoutDocker _drawDockerInner;
        private ViewDrawDocker _drawDockerOuter;
        private ViewLayoutFill _layoutFill;
        private ViewDrawButton _drawButton;
        private InternalListBox _listBox;
        private FixedContentValue _contentValues;
        private Nullable<bool> _fixedActive;
        private ButtonStyle _style;
        private IntPtr _screenDC;
        private int[] _lastSelectedColl;
        private int _lastSelectedIndex;
        private bool _mouseOver;
        private bool _alwaysActive;
        private bool _forcedLayout;
        private bool _trackingMouseEnter;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the DataSource property changes.
        /// </summary>
        [Description("Occurs when the value of the DataSource property changes.")]
        [Category("Property Changed")]
        public event EventHandler DataSourceChanged;

        /// <summary>
        /// Occurs when the value of the DisplayMember property changes.
        /// </summary>
        [Description("Occurs when the value of the DisplayMember property changes.")]
        [Category("Property Changed")]
        public event EventHandler DisplayMemberChanged;

        /// <summary>
        /// Occurs when the property of a control is bound to a data value. 
        /// </summary>
        [Description("Occurs when the property of a control is bound to a data value.")]
        [Category("Property Changed")]
        public event EventHandler Format;

        /// <summary>
        /// Occurs when the value of the FormatInfo property changes.
        /// </summary>
        [Description("Occurs when the value of the FormatInfo property changes.")]
        [Category("Property Changed")]
        public event EventHandler FormatInfoChanged;

        /// <summary>
        /// Occurs when the value of the FormatString property changes.
        /// </summary>
        [Description("Occurs when the value of the FormatString property changes.")]
        [Category("Property Changed")]
        public event EventHandler FormatStringChanged;

        /// <summary>
        /// Occurs when the value of the FormattingEnabled property changes.
        /// </summary>
        [Description("Occurs when the value of the FormattingEnabled property changes.")]
        [Category("Property Changed")]
        public event EventHandler FormattingEnabledChanged;

        /// <summary>
        /// Occurs when the value of the SelectedValue property changes.
        /// </summary>
        [Description("Occurs when the value of the SelectedValue property changes.")]
        [Category("Property Changed")]
        public event EventHandler SelectedValueChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Description("Occurs when the value of the SelectedIndex property changes.")]
        [Category("Behavior")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when the value of the ValueMember property changes.
        /// </summary>
        [Description("Occurs when the value of the ValueMember property changes.")]
        [Category("Property Changed")]
        public event EventHandler ValueMemberChanged;

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

        /// <summary>
        /// Occurs when the value of the MouseClick property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler PaddingChanged;

        /// <summary>
        /// Occurs when the value of the MouseClick property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event PaintEventHandler Paint;

        /// <summary>
        /// Occurs when the value of the TextChanged property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TextChanged;

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
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonListBox class.
        /// </summary>
        public KryptonListBox()
        {
            // Contains another control and needs marking as such for validation to work
            SetStyle(ControlStyles.ContainerControl, true);
            
            // Cannot select this control, only the child ListBox and does not generate a click event
            SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

            // Default fields
            _alwaysActive = true;
            _lastSelectedIndex = -1;
            _style = ButtonStyle.ListItem;
            base.Padding = new Padding(1);

            // Create the palette storage
            _stateCommon = new PaletteListStateRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, NeedPaintDelegate);
            _stateFocus = new PaletteListItemTripleRedirect(Redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, NeedPaintDelegate);
            _stateDisabled = new PaletteListState(_stateCommon, NeedPaintDelegate);
            _stateActive = new PaletteDouble(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteListState(_stateCommon, NeedPaintDelegate);
            _stateTracking = new PaletteListItemTriple(_stateCommon.Item, NeedPaintDelegate);
            _statePressed = new PaletteListItemTriple(_stateCommon.Item, NeedPaintDelegate);
            _stateCheckedNormal = new PaletteListItemTriple(_stateCommon.Item, NeedPaintDelegate);
            _stateCheckedTracking = new PaletteListItemTriple(_stateCommon.Item, NeedPaintDelegate);
            _stateCheckedPressed = new PaletteListItemTriple(_stateCommon.Item, NeedPaintDelegate);

            // Create the override handling classes
            _overrideNormal = new PaletteTripleOverride(_stateFocus.Item, _stateNormal.Item, PaletteState.FocusOverride);
            _overrideTracking = new PaletteTripleOverride(_stateFocus.Item, _stateTracking.Item, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(_stateFocus.Item, _statePressed.Item, PaletteState.FocusOverride);
            _overrideCheckedNormal = new PaletteTripleOverride(_stateFocus.Item, _stateCheckedNormal.Item, PaletteState.FocusOverride);
            _overrideCheckedTracking = new PaletteTripleOverride(_stateFocus.Item, _stateCheckedTracking.Item, PaletteState.FocusOverride);
            _overrideCheckedPressed = new PaletteTripleOverride(_stateFocus.Item, _stateCheckedPressed.Item, PaletteState.FocusOverride);

            // Create the draw element for owner drawing individual items
            _contentValues = new FixedContentValue();
            _drawButton = new ViewDrawButton(StateDisabled.Item, _overrideNormal,
                                             _overrideTracking, _overridePressed,
                                             _overrideCheckedNormal, _overrideCheckedTracking,
                                             _overrideCheckedPressed,
                                             new PaletteMetricRedirect(Redirector), 
                                             _contentValues, VisualOrientation.Top, false);

            // Create the internal list box used for containing content
            _listBox = new InternalListBox(this);
            _listBox.DrawItem += new DrawItemEventHandler(OnListBoxDrawItem);
            _listBox.MeasureItem += new MeasureItemEventHandler(OnListBoxMeasureItem);
            _listBox.TrackMouseEnter += new EventHandler(OnListBoxMouseChange);
            _listBox.TrackMouseLeave += new EventHandler(OnListBoxMouseChange);
            _listBox.DataSourceChanged += new EventHandler(OnListBoxDataSourceChanged);
            _listBox.DisplayMemberChanged += new EventHandler(OnListBoxDisplayMemberChanged);
            _listBox.ValueMemberChanged += new EventHandler(OnListBoxValueMemberChanged);
            _listBox.SelectedIndexChanged += new EventHandler(OnListBoxSelectedIndexChanged);
            _listBox.SelectedValueChanged += new EventHandler(OnListBoxSelectedValueChanged);
            _listBox.DisplayMemberChanged += new EventHandler(OnListBoxDisplayMemberChanged);
            _listBox.Format += new ListControlConvertEventHandler(OnListBoxFormat);
            _listBox.FormatInfoChanged += new EventHandler(OnListBoxFormatInfoChanged);
            _listBox.FormatStringChanged += new EventHandler(OnListBoxFormatStringChanged);
            _listBox.FormattingEnabledChanged += new EventHandler(OnListBoxFormattingEnabledChanged);
            _listBox.GotFocus += new EventHandler(OnListBoxGotFocus);
            _listBox.LostFocus += new EventHandler(OnListBoxLostFocus);
            _listBox.KeyDown += new KeyEventHandler(OnListBoxKeyDown);
            _listBox.KeyUp += new KeyEventHandler(OnListBoxKeyUp);
            _listBox.KeyPress += new KeyPressEventHandler(OnListBoxKeyPress);
            _listBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnListBoxPreviewKeyDown);
            _listBox.Validating += new CancelEventHandler(OnListBoxValidating);
            _listBox.Validated += new EventHandler(OnListBoxValidated);

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_listBox);
            _layoutFill.DisplayPadding = new Padding(1);

            // Create inner view for placing inside the drawing docker
            _drawDockerInner = new ViewLayoutDocker();
            _drawDockerInner.Add(_layoutFill, ViewDockStyle.Fill);

            // Create view for the control border and background
            _drawDockerOuter = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border);
            _drawDockerOuter.Add(_drawDockerInner, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDockerOuter);

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

            // Add list box to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_listBox);
        }

        /// <summary>
        /// Releases all resources used by the Control. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_screenDC != IntPtr.Zero)
                PI.DeleteDC(_screenDC);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the contained ListBox instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public ListBox ListBox
        {
            get { return _listBox; }
        }

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl
        {
            get { return ListBox; }
        }

        /// <summary>
        /// Gets or sets the text for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [DefaultValue(typeof(Padding), "1,1,1,1")]
        public new Padding Padding
        {
            get { return base.Padding; }

            set
            {
                base.Padding = value;
                _layoutFill.DisplayPadding = value;
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Gets or sets the zero-based index of the currently selected item in a KryptonListBox.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _listBox.SelectedIndex; }
            set { _listBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.
        /// </summary>
        [Category("Data")]
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue((string)null)]
        public object SelectedValue
        {
            get { return _listBox.SelectedValue; }
            set { _listBox.SelectedValue = value; }
        }

        /// <summary>
        /// Gets a collection that contains the zero-based indexes of all currently selected items in the KryptonListBox.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListBox.SelectedIndexCollection SelectedIndices
        {
            get { return _listBox.SelectedIndices; }
        }

        /// <summary>
        /// Gets or sets the currently selected item in the KryptonListBox.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return _listBox.SelectedItem; }
            set { _listBox.SelectedItem = value; }
        }

        /// <summary>
        /// Gets a collection containing the currently selected items in the KryptonListBox.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListBox.SelectedObjectCollection SelectedItems
        {
            get { return _listBox.SelectedItems; }
        }

        /// <summary>
        /// Gets or sets the index of the first visible item in the KryptonListBox.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TopIndex
        {
            get { return _listBox.TopIndex; }
            set { _listBox.TopIndex = value; }
        }

        /// <summary>
        /// Gets and sets the item style.
        /// </summary>
        [Category("Visuals")]
        [Description("Item style.")]
        public ButtonStyle ItemStyle
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    _stateCommon.Item.SetStyles(_style);
                    _stateFocus.Item.SetStyles(_style);
                    _listBox.Recreate();
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeItemStyle()
        {
            return (ItemStyle != ButtonStyle.ListItem);
        }

        private void ResetItemStyle()
        {
            ItemStyle = ButtonStyle.ListItem;
        }

        /// <summary>
        /// Gets or sets the width by which the horizontal scroll bar of a KryptonListBox can scroll. 
        /// </summary>
        [Category("Behavior")]
        [Description("The width, in pixels, by which a list box can be scrolled horizontally. Only valid HorizontalScrollbar is true.")]
        [Localizable(true)]
        [DefaultValue(0)]
        public virtual int HorizontalExtent
        {
            get { return _listBox.HorizontalExtent; }
            set { _listBox.HorizontalExtent = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a horizontal scroll bar is displayed in the control. 
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the KryptonListBox will display a horizontal scrollbar for items beyond the right edge of the KryptonListBox.")]
        [Localizable(true)]
        [DefaultValue(false)]
        public virtual bool HorizontalScrollbar
        {
            get { return _listBox.HorizontalScrollbar; }
            set { _listBox.HorizontalScrollbar = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the vertical scroll bar is shown at all times. 
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the list box should always have a scroll bar present, regardless of how many items are present.")]
        [Localizable(true)]
        [DefaultValue(false)]
        public virtual bool ScrollAlwaysVisible
        {
            get { return _listBox.ScrollAlwaysVisible; }
            set { _listBox.ScrollAlwaysVisible = value; }
        }

        /// <summary>
        /// Gets or sets the selection mode of the KryptonListBox control.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the list box is to be single-select, multi-select or not selectable.")]
        [DefaultValue(typeof(SelectionMode), "One")]
        public virtual SelectionMode SelectionMode
        {
            get { return _listBox.SelectionMode; }
            set { _listBox.SelectionMode = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the items in the KryptonListBox are sorted alphabetically.
        /// </summary>
        [Category("Behavior")]
        [Description("Controls whether the list is sorted.")]
        [DefaultValue(false)]
        public virtual bool Sorted
        {
            get { return _listBox.Sorted; }
            set { _listBox.Sorted = value; }
        }

        /// <summary>
        /// Gets and sets the value member.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to use as the actual value of the items in the control.")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string ValueMember
        {
            get { return _listBox.ValueMember; }
            set { _listBox.ValueMember = value; }
        }

        /// <summary>
        /// Gets and sets the list that this control will use to gets its items.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the list that this control will use to gets its items.")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue((string)null)]
        public virtual object DataSource
        {
            get { return _listBox.DataSource; }
            set { _listBox.DataSource = value; }
        }

        /// <summary>
        /// Gets and sets the property to display for the items in this control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string DisplayMember
        {
            get { return _listBox.DisplayMember; }
            set { _listBox.DisplayMember = value; }
        }

        /// <summary>
        /// Gets the items of the KryptonListBox. 
        /// </summary>
        [Category("Data")]
        [Description("The items in the KryptonListBox.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        [Localizable(true)]
        public virtual ListBox.ObjectCollection Items
        {
            get { return _listBox.Items; }
        }

        /// <summary>
        /// Gets or sets the format specifier characters that indicate how a value is to be displayed.
        /// </summary>
        [Description("The format specifier characters that indicate how a value is to be displayed.")]
        [Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [DefaultValue("")]
        public string FormatString
        {
            get { return _listBox.FormatString; }
            set { _listBox.FormatString = value; }
        }

        /// <summary>
        /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.
        /// </summary>
        [Description("If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get { return _listBox.FormattingEnabled; }
            set { _listBox.FormattingEnabled = value; }
        }

        /// <summary>
        /// Gets and sets the background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Style used to draw the background.")]
        public PaletteBackStyle BackStyle
        {
            get { return _stateCommon.BackStyle; }

            set
            {
                if (_stateCommon.BackStyle != value)
                {
                    _stateCommon.BackStyle = value;
                    _listBox.Recreate();
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBackStyle()
        {
            return (BackStyle != PaletteBackStyle.InputControlStandalone);
        }

        private void ResetBackStyle()
        {
            BackStyle = PaletteBackStyle.InputControlStandalone;
        }

        /// <summary>
        /// Gets and sets the border style.
        /// </summary>
        [Category("Visuals")]
        [Description("Style used to draw the border.")]
        public PaletteBorderStyle BorderStyle
        {
            get { return _stateCommon.BorderStyle; }

            set
            {
                if (_stateCommon.BorderStyle != value)
                {
                    _stateCommon.BorderStyle = value;
                    _listBox.Recreate();
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeBorderStyle()
        {
            return (BorderStyle != PaletteBorderStyle.InputControlStandalone);
        }

        private void ResetBorderStyle()
        {
            BorderStyle = PaletteBorderStyle.InputControlStandalone;
        }

        /// <summary>
        /// Gets access to the item appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining item appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTripleRedirect OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
        /// Gets access to the common appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListStateRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the active appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
        }

        /// <summary>
        /// Gets access to the hot tracking item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the hot tracking checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed checked item appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed checked item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteListItemTriple StateCheckedPressed
        {
            get { return _stateCheckedPressed; }
        }

        private bool ShouldSerializeStateCheckedPressed()
        {
            return !_stateCheckedPressed.IsDefault;
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
        /// Unselects all items in the KryptonListBox.
        /// </summary>
        public void ClearSelected()
        {
            _listBox.ClearSelected();
        }

        /// <summary>
        /// Finds the first item in the list box that starts with the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindString(string str)
        {
            return _listBox.FindString(str);
        }

        /// <summary>
        /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindString(string str, int startIndex)
        {
            return _listBox.FindString(str, startIndex);
        }

        /// <summary>
        /// Finds the first item in the list box that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindStringExact(string str)
        {
            return _listBox.FindStringExact(str);
        }

        /// <summary>
        /// Finds the first item after the specified index that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindStringExact(string str, int startIndex)
        {
            return _listBox.FindStringExact(str, startIndex);
        }

        /// <summary>
        /// Returns the height of an item in the KryptonListBox.
        /// </summary>
        /// <param name="index">The index of the item to return the height of.</param>
        /// <returns>The height, in pixels, of the item at the specified index.</returns>
        public int GetItemHeight(int index)
        {
            return _listBox.GetItemHeight(index);
        }

        /// <summary>
        /// Returns the bounding rectangle for an item in the KryptonListBox.
        /// </summary>
        /// <param name="index">The zero-based index of item whose bounding rectangle you want to return.</param>
        /// <returns>A Rectangle that represents the bounding rectangle for the specified item.</returns>
        public Rectangle GetItemRectangle(int index)
        {
            return _listBox.GetItemRectangle(index);
        }

        /// <summary>
        /// Returns a value indicating whether the specified item is selected.
        /// </summary>
        /// <param name="index">The zero-based index of the item that determines whether it is selected.</param>
        /// <returns>true if the specified item is currently selected in the KryptonListBox; otherwise, false.</returns>
        public bool GetSelected(int index)
        {
            return _listBox.GetSelected(index);
        }

        /// <summary>
        /// Returns the zero-based index of the item at the specified coordinates.
        /// </summary>
        /// <param name="p">A Point object containing the coordinates used to obtain the item index.</param>
        /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
        public int IndexFromPoint(Point p)
        {
            return _listBox.IndexFromPoint(p);
        }

        /// <summary>
        /// Returns the zero-based index of the item at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the location to search.</param>
        /// <param name="y">The y-coordinate of the location to search.</param>
        /// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
        public int IndexFromPoint(int x, int y)
        {
            return _listBox.IndexFromPoint(x, y);
        }

        /// <summary>
        /// Selects or clears the selection for the specified item in a KryptonListBox. 
        /// </summary>
        /// <param name="index">The zero-based index of the item in a KryptonListBox to select or clear the selection for.</param>
        /// <param name="value">true to select the specified item; otherwise, false.</param>
        public void SetSelected(int index, bool value)
        {
            _listBox.SetSelected(index, value);
        }

        /// <summary>
        /// Returns the text representation of the specified item.
        /// </summary>
        /// <param name="item">The object from which to get the contents to display.</param>
        /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
        public string GetItemText(object item)
        {
            return _listBox.GetItemText(item);
        }

        /// <summary>
        /// Maintains performance while items are added to the ListBox one at a time by preventing the control from drawing until the EndUpdate method is called.
        /// </summary>
        public void BeginUpdate()
        {
            _listBox.BeginUpdate();
        }

        /// <summary>
        /// Resumes painting the ListBox control after painting is suspended by the BeginUpdate method. 
        /// </summary>
        public void EndUpdate()
        {
            _listBox.EndUpdate();
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
                    return (DesignMode || AlwaysActive || ContainsFocus || _mouseOver || _listBox.MouseOver);
            }
        }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus()
        {
            if (ListBox != null)
                return ListBox.Focus();
            else
                return false;
        }

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select()
        {
            if (ListBox != null)
                ListBox.Select();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Force the layout logic to size and position the controls.
        /// </summary>
        protected void ForceControlLayout()
        {
            if (!IsHandleCreated)
            {
                _forcedLayout = true;
                OnLayout(new LayoutEventArgs(null, null));
                _forcedLayout = false;
            }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the DataSourceChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            if (DataSourceChanged != null)
                DataSourceChanged(this, e);
        }

        /// <summary>
        /// Raises the DisplayMemberChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDisplayMemberChanged(EventArgs e)
        {
            if (DisplayMemberChanged != null)
                DisplayMemberChanged(this, e);
        }

        /// <summary>
        /// Raises the ValueMemberChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueMemberChanged(EventArgs e)
        {
            if (ValueMemberChanged != null)
                ValueMemberChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, e);
        }

        /// <summary>
        /// Raises the Format event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormat(ListControlConvertEventArgs e)
        {
            if (Format != null)
                Format(this, e);
        }

        /// <summary>
        /// Raises the FormatInfoChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormatInfoChanged(EventArgs e)
        {
            if (FormatInfoChanged != null)
                FormatInfoChanged(this, e);
        }

        /// <summary>
        /// Raises the FormatStringChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormatStringChanged(EventArgs e)
        {
            if (FormatStringChanged != null)
                FormatStringChanged(this, e);
        }

        /// <summary>
        /// Raises the FormattingEnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormattingEnabledChanged(EventArgs e)
        {
            if (FormattingEnabledChanged != null)
                FormattingEnabledChanged(this, e);
        }
        #endregion

        #region Protected Override
        /// <summary>
        /// Creates a new instance of the control collection for the KryptonListBox.
        /// </summary>
        /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new KryptonReadOnlyControls(this);
        }

        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnPaletteChanged(EventArgs e)
        {
            _listBox.Recreate();
            _listBox.RefreshItemSizes();
            _listBox.Invalidate();
            base.OnPaletteChanged(e);
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            _listBox.RefreshItemSizes();
            base.OnPaletteChanged(e);
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Change in enabled state requires a layout and repaint
            UpdateStateAndPalettes();
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
        /// Raises the PaddingChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            if (PaddingChanged != null)
                PaddingChanged(this, e);
        }

        /// <summary>
        /// Raises the TabStop event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnTabStopChanged(EventArgs e)
        {
            ListBox.TabStop = TabStop;
            base.OnTabStopChanged(e);
        }

        /// <summary>
        /// Raises the CausesValidationChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnCausesValidationChanged(EventArgs e)
        {
            ListBox.CausesValidation = CausesValidation;
            base.OnCausesValidationChanged(e);
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Paint != null)
                Paint(this, e);

            base.OnPaint(e);
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
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
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (IsHandleCreated && !e.NeedLayout)
                _listBox.Invalidate();
            else
                ForceControlLayout();

            // Update palette to reflect latest state
            UpdateStateAndPalettes();
            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (IsHandleCreated || _forcedLayout || (DesignMode && (_listBox != null)))
            {
                Rectangle fillRect = _layoutFill.FillRect;
                _listBox.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
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
            _listBox.Invalidate();
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
            _listBox.Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(120, 96); }
        }
        #endregion

        #region Implementation
        private void UpdateStateAndPalettes()
        {
            if (!IsDisposed)
            {
                // Get the correct palette settings to use
                IPaletteDouble doubleState = GetDoubleState();
                _listBox.ViewDrawPanel.SetPalettes(doubleState.PaletteBack);
                _drawDockerOuter.SetPalettes(doubleState.PaletteBack, doubleState.PaletteBorder);
                _drawDockerOuter.Enabled = Enabled;

                // Find the new state of the main view element
                PaletteState state;
                if (IsActive)
                    state = PaletteState.Tracking;
                else
                {
                    if (Enabled)
                        state = PaletteState.Normal;
                    else
                        state = PaletteState.Disabled;
                }

                _listBox.ViewDrawPanel.ElementState = state;
                _drawDockerOuter.ElementState = state;
            }
        }

        private IPaletteDouble GetDoubleState()
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

        private void OnListBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            // We cannot do anything with an invalid index
            if (e.Index < 0)
                return;

            // Update our content object with values from the list item
            UpdateContentFromItemIndex(e.Index);

            // By default the button is in the normal state
            PaletteState buttonState = PaletteState.Normal;

            // Is this item disabled
            if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
                buttonState = PaletteState.Disabled;
            else
            {
                // Is the mouse over the item about to be drawn
                bool mouseOver = (e.Index >= 0) && (e.Index == _listBox.MouseIndex);

                // If selected then show as a checked item
                if (((e.State & DrawItemState.Selected) == DrawItemState.Selected) &&
                    (SelectionMode != SelectionMode.None))
                {
                    _drawButton.Checked = true;
                    buttonState = (mouseOver ? PaletteState.CheckedTracking : PaletteState.CheckedNormal);
                }
                else
                {
                    _drawButton.Checked = false;
                    if (mouseOver)
                        buttonState = PaletteState.Tracking;
                }

                // Do we need to show item as having the focus
                bool hasFocus = false;
                if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) &&
                    ((e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect))
                    hasFocus = true;

                _overrideNormal.Apply = hasFocus;
                _overrideTracking.Apply = hasFocus;
                _overridePressed.Apply = hasFocus;
                _overrideCheckedTracking.Apply = hasFocus;
                _overrideCheckedNormal.Apply = hasFocus;
                _overrideCheckedPressed.Apply = hasFocus;
            }

            // Update the view with the calculated state
            _drawButton.ElementState = buttonState;

            // Grab the raw device context for the graphics instance
            IntPtr hdc = e.Graphics.GetHdc();

            try
            {
                // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, e.Bounds.Right, e.Bounds.Bottom);

                // If we managed to get a compatible bitmap
                if (hBitmap != IntPtr.Zero)
                {
                    try
                    {
                        // Must use the screen device context for the bitmap when drawing into the 
                        // bitmap otherwise the Opacity and RightToLeftLayout will not work correctly.
                        PI.SelectObject(_screenDC, hBitmap);

                        // Easier to draw using a graphics instance than a DC!
                        using (Graphics g = Graphics.FromHdc(_screenDC))
                        {

                            // Ask the view element to layout in given space, needs this before a render call
                            using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
                            {
                                context.DisplayRectangle = e.Bounds;
                                _listBox.ViewDrawPanel.Layout(context);
                                _drawButton.Layout(context);
                            }

                            // Ask the view element to actually draw
                            using (RenderContext context = new RenderContext(this, g, e.Bounds, Renderer))
                            {
                                _listBox.ViewDrawPanel.Render(context);
                                _drawButton.Render(context);
                            }

                            // Now blit from the bitmap from the screen to the real dc
                            PI.BitBlt(hdc, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, _screenDC, e.Bounds.X, e.Bounds.Y, PI.SRCCOPY);
                        }
                    }
                    finally
                    {
                        // Delete the temporary bitmap
                        PI.DeleteObject(hBitmap);
                    }
                }
            }
            finally
            {
                // Must reserve the GetHdc() call before
                e.Graphics.ReleaseHdc();
            }
        }

        private void OnListBoxMeasureItem(object sender, MeasureItemEventArgs e)
        {
            UpdateContentFromItemIndex(e.Index);

            // Ask the view element to layout in given space, needs this before a render call
            using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
            {
                Size size = _drawButton.GetPreferredSize(context);
                e.ItemWidth = size.Width;
                e.ItemHeight = size.Height;
            }
        }

        private void UpdateContentFromItemIndex(int index)
        {
            IContentValues itemValues = Items[index] as IContentValues;

            // If the object exposes the rich interface then use is...
            if (itemValues != null)
            {
                _contentValues.ShortText = itemValues.GetShortText();
                _contentValues.LongText = itemValues.GetLongText();
                _contentValues.Image = itemValues.GetImage(PaletteState.Normal);
                _contentValues.ImageTransparentColor = itemValues.GetImageTransparentColor(PaletteState.Normal);
            }
            else
            {
                // Get the text string for the item
                _contentValues.ShortText = _listBox.GetItemText(Items[index]);
                _contentValues.LongText = null;
                _contentValues.Image = null;
                _contentValues.ImageTransparentColor = Color.Empty;
            }
        }

        private void OnListBoxDataSourceChanged(object sender, EventArgs e)
        {
            OnDataSourceChanged(e);
        }

        private void OnListBoxDisplayMemberChanged(object sender, EventArgs e)
        {
            OnDisplayMemberChanged(e);
        }

        private void OnListBoxValueMemberChanged(object sender, EventArgs e)
        {
            OnValueMemberChanged(e);
        }

        private void OnListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_listBox.SelectionMode)
            {
                case SelectionMode.One:
                    if (_lastSelectedIndex != _listBox.SelectedIndex)
                    {
                        _lastSelectedIndex = _listBox.SelectedIndex;
                        UpdateStateAndPalettes();
                        _listBox.Invalidate();
                        OnSelectedIndexChanged(e);
                    }
                    break;
                case SelectionMode.MultiSimple:
                case SelectionMode.MultiExtended:
                    if (SelectedIndicesChanged(_lastSelectedColl, _listBox.SelectedIndices))
                    {
                        // Clone the selected index collection
                        _lastSelectedColl = new int[_listBox.SelectedIndices.Count];
                        _listBox.SelectedIndices.CopyTo(_lastSelectedColl, 0);

                        UpdateStateAndPalettes();
                        _listBox.Invalidate();
                        OnSelectedIndexChanged(e);
                    }
                    break;
            }
        }

        private bool SelectedIndicesChanged(int[] left,
                                            ListBox.SelectedIndexCollection right)
        {
            // First time around the left can be null
            if ((left == null) && (right != null))
                return true;

            // Quickest check is to see if they have different number of entries
            if (left.Length != right.Count)
                return true;

            // Do it the slow way, check each entry and assume they are in numerical order
            for (int i = 0; i < left.Length; i++)
                if (left[i] != right[i])
                    return true;

            return false;
        }            

        private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            _listBox.Invalidate();
            OnSelectedValueChanged(e);
        }

        private void OnListBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            OnFormat(e);
        }

        private void OnListBoxFormatInfoChanged(object sender, EventArgs e)
        {
            OnFormatInfoChanged(e);
        }

        private void OnListBoxFormatStringChanged(object sender, EventArgs e)
        {
            OnFormatStringChanged(e);
        }

        private void OnListBoxFormattingEnabledChanged(object sender, EventArgs e)
        {
            OnFormattingEnabledChanged(e);
        }

        private void OnListBoxGotFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            _listBox.Invalidate();
            PerformNeedPaint(true);
            OnGotFocus(e);
        }

        private void OnListBoxLostFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            _listBox.Invalidate();
            PerformNeedPaint(true);
            OnLostFocus(e);
        }

        private void OnListBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnListBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnListBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnListBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnListBoxValidated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void OnListBoxValidating(object sender, CancelEventArgs e)
        {
            OnValidating(e);
        }

        private void OnListBoxMouseChange(object sender, EventArgs e)
        {
            // Change in tracking state?
            if (_listBox.MouseOver != _trackingMouseEnter)
            {
                _trackingMouseEnter = _listBox.MouseOver;

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
