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
using System.Drawing.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Provide a ComboBox with Krypton styling applied.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonComboBoxDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Displays an editable textbox with a drop-down list of permitted values.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonComboBox : VisualControlBase,
                                   IContainedInputControl,
                                   ISupportInitializeNotification

    {
        #region Classes
        private class InternalPanel : Panel
        {
            #region Instance Fields
            private KryptonComboBox _kryptonComboBox;
            #endregion

            #region Identity
            /// <summary>
            /// Initialise a new instance of the InternalPanel class.
            /// </summary>
            /// <param name="kryptonComboBox">Reference to owning control.</param>
            public InternalPanel(KryptonComboBox kryptonComboBox)
            {
                _kryptonComboBox = kryptonComboBox;
            }
            #endregion

            #region Public
            /// <summary>
            /// Retrieves the size of a rectangular area into which a control can be fitted.
            /// </summary>
            public override Size GetPreferredSize(Size proposedSize)
            {
                Size maxSize = Size.Empty;

                // Find the largest size of any child control
                foreach (Control c in Controls)
                {
                    Size cSize = c.GetPreferredSize(proposedSize);
                    maxSize.Width = Math.Max(maxSize.Width, cSize.Width);
                    maxSize.Height = Math.Max(maxSize.Height, cSize.Height);
                }

                // The panel needs to be 2 above and 2 below bigger than the height of an item
                return new Size(maxSize.Width - 3, _kryptonComboBox._comboBox.ItemHeight + 4);
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
                        if (_kryptonComboBox.InTransparentDesignMode)
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
        }

        private class InternalComboBox : ComboBox, IContentValues
        {
            #region Instance Fields
            private KryptonComboBox _kryptonComboBox;
            private PaletteTripleToPalette _palette;
            private ViewDrawButton _viewButton;
            private Nullable<bool> _appThemed;
            private bool _mouseTracking;
            private bool _mouseOver;
            private bool _dropped;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalComboBox.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalComboBox.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the InternalComboBox class.
            /// </summary>
            /// <param name="kryptonComboBox">Reference to owning control.</param>
            public InternalComboBox(KryptonComboBox kryptonComboBox)
            {
                // Remember incoming reference
                _kryptonComboBox = kryptonComboBox;

                // Remove from view until size for the first time by the Krypton control
                ItemHeight = 15;
                DropDownHeight = 200;
                DrawMode = DrawMode.OwnerDrawVariable;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets and sets if the combo box is currently dropped.
            /// </summary>
            public bool Dropped
            {
                get { return _dropped; }
                set { _dropped = value; }
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
                            OnTrackMouseLeave(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Reset the app themed setting so it is retested when next required.
            /// </summary>
            public void ClearAppThemed()
            {
                _appThemed = null;
            }

            /// <summary>
            /// Gets the content short text.
            /// </summary>
            /// <returns>String value.</returns>
            public virtual string GetShortText()
            {
                return string.Empty;
            }

            /// <summary>
            /// Gets the content image.
            /// </summary>
            /// <param name="state">The state for which the image is needed.</param>
            /// <returns>Image value.</returns>
            public virtual Image GetImage(PaletteState state)
            {
                return null;
            }

            /// <summary>
            /// Gets the image color that should be transparent.
            /// </summary>
            /// <param name="state">The state for which the image is needed.</param>
            /// <returns>Color value.</returns>
            public virtual Color GetImageTransparentColor(PaletteState state)
            {
                return Color.Empty;
            }

            /// <summary>
            /// Gets the content long text.
            /// </summary>
            /// <returns>String value.</returns>
            public virtual string GetLongText()
            {
                return string.Empty;
            }
            #endregion

            #region Protected
            /// <summary>
            /// Raises the FontChanged event.
            /// </summary>
            /// <param name="e">Contains the event data.</param>
            protected override void OnFontChanged(EventArgs e)
            {
                // Working on Windows XP or earlier systems?
                if (_osMajorVersion < 6)
                {
                    // Fudge by adding one to the font height, this gives the actual space used by the
                    // combo box control to draw an individual item in the main part of the control
                    ItemHeight = Font.Height + 1;
                }
                else
                {
                    // Vista performs differently depending of the use of themes...
                    if (IsAppThemed)
                    {
                        // Fudge by subtracting 1, which ensure correct sizing of combo box main area
                        ItemHeight = Font.Height - 1;
                    }
                    else
                    {
                        // On under Vista without themes is the font height the actual height used
                        // by the combo box for the space required for drawing the actual item
                        ItemHeight = Font.Height;
                    }
                }
                base.OnFontChanged(e);
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
                        if (_kryptonComboBox.InTransparentDesignMode)
                            m.Result = (IntPtr)PI.HTTRANSPARENT;
                        else
                            base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        {
                            // Mouse is not over the control
                            MouseOver = false;
                            _mouseTracking = false;
                            _kryptonComboBox.PerformNeedPaint(false);
                            Invalidate();
                        }
                        break;
                    case PI.WM_MOUSEMOVE:
                        {
                            // Mouse is over the control
                            if (!MouseOver)
                            {
                                MouseOver = true;
                                _kryptonComboBox.PerformNeedPaint(false);
                                Invalidate();
                            }

                            // Grab the client area of the control
                            PI.RECT rect = new PI.RECT();
                            PI.GetClientRect(Handle, out rect);

                            // Get the constant used to crack open the display
                            int dropDownWidth = SystemInformation.VerticalScrollBarWidth;
                            Size borderSize = SystemInformation.BorderSize;

                            // Create rect for the text area
                            rect.left += borderSize.Width;
                            rect.right -= (borderSize.Width + dropDownWidth);
                            rect.top += borderSize.Height;
                            rect.bottom -= borderSize.Height;

                            // Create rectangle that represents the drop down button
                            Rectangle dropRect = new Rectangle(rect.right + 2, rect.top, dropDownWidth - 2, (rect.bottom - rect.top));

                            // Extract the point in client coordinates
                            Point clientPoint = new Point((int)m.LParam);
                            bool mouseTracking = dropRect.Contains(clientPoint);
                            if (mouseTracking != _mouseTracking)
                            {
                                _mouseTracking = mouseTracking;
                                _kryptonComboBox.PerformNeedPaint(false);
                                Invalidate();
                            }
                        }
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

                                // Get the constant used to crack open the display
                                int dropDownWidth = SystemInformation.VerticalScrollBarWidth;
                                Size borderSize = SystemInformation.BorderSize;

                                // Create rect for the text area
                                rect.top += borderSize.Height;
                                rect.bottom -= borderSize.Height;

                                // Create rectangle that represents the drop down button
                                Rectangle dropRect;

                                // Update text and drop down rects dependant on the right to left setting
                                if (_kryptonComboBox.RightToLeft == RightToLeft.Yes)
                                {
                                    dropRect = new Rectangle(rect.left + borderSize.Width + 1, rect.top + 1, dropDownWidth - 2, (rect.bottom - rect.top - 2));
                                    rect.left += borderSize.Width + dropDownWidth;
                                    rect.right -= borderSize.Width;
                                }
                                else
                                {
                                    rect.left += borderSize.Width;
                                    rect.right -= (borderSize.Width + dropDownWidth);
                                    dropRect = new Rectangle(rect.right + 1, rect.top + 1, dropDownWidth - 2, (rect.bottom - rect.top - 2));
                                }

                                // Exclude border from being drawn, we need to take off another 2 pixels from all edges
                                PI.IntersectClipRect(hdc, rect.left + 2, rect.top + 2, rect.right - 2, rect.bottom - 2);

                                // If enabled then let the combo draw the text area
                                if (_kryptonComboBox.Enabled)
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
                                    g.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(_kryptonComboBox.StateDisabled.Item.PaletteContent.GetContentShortTextHint(PaletteState.Disabled));

                                    // Define the string formatting requirements
                                    StringFormat stringFormat = new StringFormat();
                                    stringFormat.LineAlignment = StringAlignment.Center;
                                    stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                    stringFormat.Trimming = StringTrimming.None;

                                    if (_kryptonComboBox.RightToLeft == RightToLeft.Yes)
                                        stringFormat.Alignment = StringAlignment.Far;
                                    else
                                        stringFormat.Alignment = StringAlignment.Near;

                                    // Use the correct prefix setting
                                    stringFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;

                                    // Draw using a solid brush
                                    try
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(ForeColor))
                                            g.DrawString(Text, Font, foreBrush,
                                                         new RectangleF(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top),
                                                         stringFormat);
                                    }
                                    catch(ArgumentException)
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(ForeColor))
                                            g.DrawString(Text, _kryptonComboBox.GetComboBoxTripleState().PaletteContent.GetContentShortTextFont(PaletteState.Disabled), foreBrush,
                                                         new RectangleF(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top),
                                                         stringFormat);
                                    }
                                }

                                // Remove clipping settings
                                PI.SelectClipRgn(hdc, IntPtr.Zero);

                                // Draw the drop down button
                                DrawDropButton(g, dropRect);
                            }

                            // Do we need to match the original BeginPaint?
                            if (m.WParam == IntPtr.Zero)
                                PI.EndPaint(Handle, ref ps);
                        }
                        break;
                    case PI.WM_CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (_kryptonComboBox.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)((long)m.LParam)) == -1)
                                mousePt = PointToScreen(new Point(Width / 2, Height / 2));

                            // Show the context menu
                            _kryptonComboBox.KryptonContextMenu.Show(_kryptonComboBox, mousePt);

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

            #region Implementation
            private void DrawDropButton(Graphics g, Rectangle drawRect)
            {
                // Create the view and palette entries first time around
                if (_viewButton == null)
                {
                    // Create helper object to get all values from the KryptonComboBox redirector
                    _palette = new PaletteTripleToPalette(_kryptonComboBox.Redirector,
                                                          PaletteBackStyle.ButtonStandalone,
                                                          PaletteBorderStyle.ButtonStandalone,
                                                          PaletteContentStyle.ButtonStandalone);

                    // Create view element for drawing the actual button
                    _viewButton = new ViewDrawButton(_palette, _palette, _palette, 
                                                     _palette, _palette, _palette, _palette,
                                                     new PaletteMetricRedirect(_kryptonComboBox.Redirector),
                                                     this, VisualOrientation.Top, false);
                }

                // Update with the latest button style for the drop down
                _palette.SetStyles(_kryptonComboBox.DropButtonStyle);

                // Find the new state for the button
                PaletteState state;
                if (_kryptonComboBox.Enabled)
                {
                    if (Dropped)
                        state = PaletteState.Pressed;
                    else if (_mouseTracking)
                        state = PaletteState.Tracking;
                    else if (_kryptonComboBox.IsActive || (_kryptonComboBox.IsFixedActive && (_kryptonComboBox.InputControlStyle == InputControlStyle.Standalone)))
                    {
                        if (_kryptonComboBox.InputControlStyle == InputControlStyle.Standalone)
                            state = PaletteState.CheckedNormal;
                        else
                            state = PaletteState.CheckedTracking;
                    }
                    else
                        state = PaletteState.Normal;
                }
                else
                    state = PaletteState.Disabled;

                _viewButton.ElementState = state;

                // Position the button element inside the available drop down button area
                using (ViewLayoutContext layoutContext = new ViewLayoutContext(_kryptonComboBox, _kryptonComboBox.Renderer))
                {
                    // Define the available area for layout
                    layoutContext.DisplayRectangle = drawRect;

                    // Perform actual layout inside that area
                    _viewButton.Layout(layoutContext);
                }

                // Fill background with the solid background color
                using (SolidBrush backBrush = new SolidBrush(BackColor))
                    g.FillRectangle(backBrush, drawRect);

                // Ask the element to draw now
                using (RenderContext renderContext = new RenderContext(_kryptonComboBox, g, drawRect, _kryptonComboBox.Renderer))
                {
                    // Ask the button element to draw itself
                    _viewButton.Render(renderContext);

                    // Call the renderer directly to draw the drop down glyph
                    renderContext.Renderer.RenderGlyph.DrawInputControlDropDownGlyph(renderContext,
                                                                                     _viewButton.ClientRectangle,
                                                                                     _palette.PaletteContent,
                                                                                     state);
                }
            }

            private bool IsAppThemed
            {
                get
                {
                    try
                    {
                        if (!_appThemed.HasValue)
                            _appThemed = (PI.IsThemeActive() && PI.IsAppThemed());

                        return _appThemed.Value;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            #endregion
        }

        private class SubclassEdit : NativeWindow
        {
            #region Instance Fields
            private KryptonComboBox _kryptonComboBox;
            private bool _mouseOver;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalComboBox.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalComboBox.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the SubclassEdit class.
            /// </summary>
            /// <param name="editControl">Handle of the Edit control to subclass.</param>
            /// <param name="kryptonComboBox">Reference to top level control.</param>
            public SubclassEdit(IntPtr editControl,
                                KryptonComboBox kryptonComboBox)
            {
                _kryptonComboBox = kryptonComboBox;

                // Attach ourself to the provided control, subclassing it
                AssignHandle(editControl);
            }
            #endregion

            #region Public
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

            /// <summary>
            /// Sets the visible state of the control.
            /// </summary>
            public bool Visible
            {
                set 
                {
                    PI.SetWindowPos(Handle,
                                    IntPtr.Zero,
                                    0, 0, 0, 0,
                                    (uint)(PI.SWP_NOMOVE | PI.SWP_NOSIZE |
                                    (value ? PI.SWP_SHOWWINDOW : PI.SWP_HIDEWINDOW))); 
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
                        if (_kryptonComboBox.InTransparentDesignMode)
                            m.Result = (IntPtr)PI.HTTRANSPARENT;
                        else
                            base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        // Mouse is not over the control
                        MouseOver = false;
                        _kryptonComboBox.PerformNeedPaint(false);
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            PI.TRACKMOUSEEVENTS tme = new PI.TRACKMOUSEEVENTS();

                            // This structure needs to know its own size in bytes
                            tme.cbSize = (uint)Marshal.SizeOf(typeof(PI.TRACKMOUSEEVENTS));
                            tme.dwHoverTime = 100;

                            // We need to know then the mouse leaves the client window area
                            tme.dwFlags = (int)(PI.TME_LEAVE);

                            // We want to track our own window
                            tme.hWnd = Handle;

                            // Call Win32 API to start tracking
                            PI.TrackMouseEvent(ref tme);

                            MouseOver = true;
                            _kryptonComboBox.PerformNeedPaint(false);
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (_kryptonComboBox.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)((long)m.LParam)) == -1)
                            {
                                PI.RECT clientRect;
                                PI.GetClientRect(Handle, out clientRect);
                                mousePt = new Point((clientRect.right - clientRect.left) / 2,
                                                    (clientRect.bottom - clientRect.top) / 2);
                            }

                            // Show the context menu
                            _kryptonComboBox.KryptonContextMenu.Show(_kryptonComboBox, mousePt);

                            // We eat the message!
                            return;
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_DESTROY:
                        // Remove this code as it prevents the auto suggest features from working
                        // _kryptonComboBox.DetachEditControl();
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
        public class ComboBoxButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the ComboBoxButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public ComboBoxButtonSpecCollection(KryptonComboBox owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static int _osMajorVersion;
        #endregion

        #region Instance Fields
        private ToolTipManager _toolTipManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ButtonSpecManagerLayout _buttonManager;
        private ComboBoxButtonSpecCollection _buttonSpecs;
        private PaletteComboBoxRedirect _stateCommon;
        private PaletteComboBoxStates _stateDisabled;
        private PaletteComboBoxStates _stateNormal;
        private PaletteComboBoxJustComboStates _stateActive;
        private PaletteComboBoxJustItemStates _stateTracking;
        private ViewLayoutDocker _drawDockerInner;
        private ViewDrawDocker _drawDockerOuter;
        private ViewLayoutFill _layoutFill;
        private InternalComboBox _comboBox;
        private InternalPanel _comboHolder;
        private SubclassEdit _subclassEdit;
        private ButtonStyle _dropButtonStyle;
        private PaletteBackStyle _dropBackStyle;
        private InputControlStyle _inputControlStyle;
        private Nullable<bool> _fixedActive;
        private FixedContentValue _contentValues;
        private ButtonStyle _style;
        private ViewDrawButton _drawButton;
        private ViewDrawPanel _drawPanel;
        private AutoCompleteMode _autoCompleteMode;
        private AutoCompleteSource _autoCompleteSource;
        private Padding _layoutPadding;
        private IntPtr _screenDC;
        private bool _initializing;
        private bool _initialized;
        private bool _firstTimePaint;
        private bool _trackingMouseEnter;
        private bool _inRibbonDesignMode;
        private bool _forcedLayout;
        private bool _mouseOver;
        private bool _alwaysActive;
        private bool _allowButtonSpecToolTips;
        private int _cachedHeight;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the control is initialized.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the control has been fully initialized.")]
        public event EventHandler Initialized;

        /// <summary>
        /// Occurs when the drop-down portion of the KryptonComboBox is shown.
        /// </summary>
        [Description("Occurs when the drop-down portion of the KryptonComboBox is shown.")]
        [Category("Behavior")]
        public event EventHandler DropDown;

        /// <summary>
        /// Indicates that the drop-down portion of the KryptonComboBox has closed.
        /// </summary>
        [Description("Indicates that the drop-down portion of the KryptonComboBox has closed.")]
        [Category("Behavior")]
        public event EventHandler DropDownClosed;

        /// <summary>
        /// Occurs when the value of the DropDownStyle property changed.
        /// </summary>
        [Description("Occurs when the value of the DropDownStyle property changed.")]
        [Category("Behavior")]
        public event EventHandler DropDownStyleChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Description("Occurs when the value of the SelectedIndex property changes.")]
        [Category("Behavior")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when an item is chosen from the drop-down list and the drop-down list is closed.
        /// </summary>
        [Description("Occurs when an item is chosen from the drop-down list and the drop-down list is closed.")]
        [Category("Behavior")]
        public event EventHandler SelectionChangeCommitted;

        /// <summary>
        /// Occurs when the value of the DataSource property changed.
        /// </summary>
        [Description("Occurs when the value of the DataSource property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler DataSourceChanged;

        /// <summary>
        /// Occurs when the value of the DisplayMember property changed.
        /// </summary>
        [Description("Occurs when the value of the DisplayMember property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler DisplayMemberChanged;

        /// <summary>
        /// Occurs when the list format has changed.
        /// </summary>
        [Description("Occurs when the list format has changed.")]
        [Category("PropertyChanged")]
        public event ListControlConvertEventHandler Format;

        /// <summary>
        /// Occurs when the value of the FormatInfo property changed.
        /// </summary>
        [Description("Occurs when the value of the FormatInfo property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormatInfoChanged;

        /// <summary>
        /// Occurs when the value of the FormatString property changed.
        /// </summary>
        [Description("Occurs when the value of the FormatString property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormatStringChanged;

        /// <summary>
        /// Occurs when the value of the FormattingEnabled property changed.
        /// </summary>
        [Description("Occurs when the value of the FormattingEnabled property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormattingEnabledChanged;

        /// <summary>
        /// Occurs when the value of the SelectedValue property changed.
        /// </summary>
        [Description("Occurs when the value of the SelectedValue property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler SelectedValueChanged;

        /// <summary>
        /// Occurs when the value of the ValueMember property changed.
        /// </summary>
        [Description("Occurs when the value of the ValueMember property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ValueMemberChanged;

        /// <summary>
        /// Occurs when the KryptonComboBox text has changed.
        /// </summary>
        [Description("Occurs when the KryptonComboBox text has changed.")]
        [Category("Behavior")]
        public event EventHandler TextUpdate;

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

        /// <summary>
        /// Occurs when the value of the Paint property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Paint;

        /// <summary>
        /// Occurs when the value of the PaddingChanged property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler PaddingChanged;
        #endregion

        #region Identity
        static KryptonComboBox()
        {
            // Cache the major os version number
            _osMajorVersion = Environment.OSVersion.Version.Major;
        }

        /// <summary>
        /// Initialize a new instance of the KryptonComboBox class.
		/// </summary>
        public KryptonComboBox()
        {
            // Contains another control and needs marking as such for validation to work
            SetStyle(ControlStyles.ContainerControl, true);

            // The height is always fixed
            SetStyle(ControlStyles.FixedHeight, true);

            // Cannot select this control, only the child TextBox
            SetStyle(ControlStyles.Selectable, false);

            // Default values
            _alwaysActive = true;
            _allowButtonSpecToolTips = false;
            _cachedHeight = -1;
            _inputControlStyle = InputControlStyle.Standalone;
            _dropButtonStyle = ButtonStyle.InputControl;
            _dropBackStyle = PaletteBackStyle.ControlClient;
            _style = ButtonStyle.ListItem;
            _firstTimePaint = true;
            _autoCompleteMode = AutoCompleteMode.None;
            _autoCompleteSource = AutoCompleteSource.None;

            // Create storage properties
            _buttonSpecs = new ComboBoxButtonSpecCollection(this);

            // Create the palette storage
            _stateCommon = new PaletteComboBoxRedirect(Redirector, NeedPaintDelegate);
            _stateDisabled = new PaletteComboBoxStates(_stateCommon.ComboBox, _stateCommon.Item, NeedPaintDelegate);
            _stateNormal = new PaletteComboBoxStates(_stateCommon.ComboBox, _stateCommon.Item, NeedPaintDelegate);
            _stateActive = new PaletteComboBoxJustComboStates(_stateCommon.ComboBox, NeedPaintDelegate);
            _stateTracking = new PaletteComboBoxJustItemStates(_stateCommon.Item, NeedPaintDelegate);

            // Create the draw element for owner drawing individual items
            _contentValues = new FixedContentValue();
            _drawPanel = new ViewDrawPanel(_stateCommon.DropBack);
            _drawButton = new ViewDrawButton(_stateDisabled.Item, _stateNormal.Item,
                                             _stateTracking.Item, _stateTracking.Item,
                                             new PaletteMetricRedirect(Redirector),
                                             _contentValues, VisualOrientation.Top, false);

            // Create the internal combo box used for containing content
            _comboBox = new InternalComboBox(this);
            _comboBox.DrawItem += new DrawItemEventHandler(OnComboBoxDrawItem);
            _comboBox.MeasureItem += new MeasureItemEventHandler(OnComboBoxMeasureItem);
            _comboBox.TrackMouseEnter += new EventHandler(OnComboBoxMouseChange);
            _comboBox.TrackMouseLeave += new EventHandler(OnComboBoxMouseChange);
            _comboBox.DropDown += new EventHandler(OnComboBoxDropDown);
            _comboBox.DropDownClosed += new EventHandler(OnComboBoxDropDownClosed);
            _comboBox.DropDownStyleChanged += new EventHandler(OnComboBoxDropDownStyleChanged);
            _comboBox.SelectedIndexChanged += new EventHandler(OnComboBoxSelectedIndexChanged);
            _comboBox.SelectionChangeCommitted += new EventHandler(OnComboBoxSelectionChangeCommitted);
            _comboBox.TextUpdate += new EventHandler(OnComboBoxTextUpdate);
            _comboBox.TextChanged += new EventHandler(OnComboBoxTextChanged);
            _comboBox.GotFocus += new EventHandler(OnComboBoxGotFocus);
            _comboBox.LostFocus += new EventHandler(OnComboBoxLostFocus);
            _comboBox.KeyDown += new KeyEventHandler(OnComboBoxKeyDown);
            _comboBox.KeyUp += new KeyEventHandler(OnComboBoxKeyUp);
            _comboBox.KeyPress += new KeyPressEventHandler(OnComboBoxKeyPress);
            _comboBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnComboBoxPreviewKeyDown);
            _comboBox.DataSourceChanged += new EventHandler(OnComboBoxDataSourceChanged);
            _comboBox.DisplayMemberChanged += new EventHandler(OnComboBoxDisplayMemberChanged);
            _comboBox.Format += new ListControlConvertEventHandler(OnComboBoxFormat);
            _comboBox.FormatInfoChanged += new EventHandler(OnComboBoxFormatInfoChanged);
            _comboBox.FormatStringChanged += new EventHandler(OnComboBoxFormatStringChanged);
            _comboBox.FormattingEnabledChanged += new EventHandler(OnComboBoxFormattingEnabledChanged);
            _comboBox.SelectedValueChanged += new EventHandler(OnComboBoxSelectedValueChanged);
            _comboBox.ValueMemberChanged += new EventHandler(OnComboBoxValueMemberChanged);
            _comboBox.Validating += new CancelEventHandler(OnComboBoxValidating);
            _comboBox.Validated += new EventHandler(OnComboBoxValidated);
            _comboHolder = new InternalPanel(this);
            _comboHolder.Controls.Add(_comboBox);

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_comboHolder);

            // Create inner view for placing inside the drawing docker
            _drawDockerInner = new ViewLayoutDocker();
            _drawDockerInner.Add(_layoutFill, ViewDockStyle.Fill);

            // Create view for the control border and background
            _drawDockerOuter = new ViewDrawDocker(_stateNormal.ComboBox.Back, _stateNormal.ComboBox.Border);
            _drawDockerOuter.Add(_drawDockerInner, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDockerOuter);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerLayout(this, Redirector, _buttonSpecs, null,
                                                         new ViewLayoutDocker[] { _drawDockerInner },
                                                         new IPaletteMetric[] { _stateCommon.ComboBox },
                                                         new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetInputControl },
                                                         new PaletteMetricPadding[] { PaletteMetricPadding.HeaderButtonPaddingInputControl },
                                                         new GetToolStripRenderer(CreateToolStripRenderer),
                                                         NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;

            // We need to create and cache a device context compatible with the display
            _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);

            // Add combo box holder to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_comboHolder);

            // Must set the initial font otherwise the Form level font setting will cause the control
            // to not work correctly. Happens on Vista when the Form has non-default Font setting.
            IPaletteTriple triple = _stateActive.ComboBox;
            _comboBox.BackColor = triple.PaletteBack.GetBackColor1(PaletteState.Tracking);
            _comboBox.ForeColor = triple.PaletteContent.GetContentShortTextColor1(PaletteState.Tracking);
            _comboBox.Font = (Font)triple.PaletteContent.GetContentShortTextFont(PaletteState.Tracking);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // If we are tracking an edit control
                DetachEditControl();

                // Remove any showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);

                // Remember to pull down the manager instance
                _buttonManager.Destruct();
            }

            base.Dispose(disposing);

            if (_screenDC != IntPtr.Zero)
            {
                PI.DeleteDC(_screenDC);
                _screenDC = IntPtr.Zero;
            }
        }
        #endregion

		#region Public
        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public virtual void BeginInit()
        {
            // Remember that fact we are inside a BeginInit/EndInit pair
            _initializing = true;
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public virtual void EndInit()
        {
            // We are now initialized
            _initialized = true;

            // We are no longer initializing
            _initializing = false;

            // Force calculation of the drop down items again so they are sized correctly
            _comboBox.DrawMode = DrawMode.OwnerDrawVariable;             

            // Raise event to show control is now initialized
            OnInitialized(EventArgs.Empty);
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsInitialized
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _initialized; }
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsInitializing
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _initializing; }
        }

        /// <summary>
        /// Gets and sets if the control is in the tab chain.
        /// </summary>
        public new bool TabStop
        {
            get { return _comboBox.TabStop; }
            set { _comboBox.TabStop = value; }
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
        /// Gets access to the contained ComboBox instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public ComboBox ComboBox
        {
            get { return _comboBox; }
        }

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl
        {
            get { return ComboBox; }
        }

        /// <summary>
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        [Browsable(false)]
        public override bool Focused
        {
            get { return ComboBox.Focused; }
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
            
            set 
            { 
                base.Font = value; 
            }
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
        /// Gets and sets the text associated associated with the control.
        /// </summary>
        public override string Text
        {
            get { return _comboBox.Text; }
            set { _comboBox.Text = value; }
        }

        /// <summary>
        /// Gets and sets the selected item.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return _comboBox.SelectedItem; }
            set { _comboBox.SelectedItem = value; }
        }

        /// <summary>
        /// Gets and sets the text that is selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get { return _comboBox.SelectedText; }
            set { _comboBox.SelectedText = value; }
        }

        /// <summary>
        /// Gets and sets the selected index.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _comboBox.SelectedIndex; }
            set { _comboBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets and sets the selected value.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get { return _comboBox.SelectedValue; }
            set { _comboBox.SelectedValue = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating whether the control is displaying its drop-down portion.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get { return _comboBox.DroppedDown; }
            set { _comboBox.DroppedDown = value; }
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
                _comboBox.ContextMenuStrip = value; 
            }
        }

        /// <summary>
        /// Gets and sets the value member.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to use as the actual value of the items in the control.")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ValueMember
        {
            get { return _comboBox.ValueMember; }
            set { _comboBox.ValueMember = value; }
        }

        /// <summary>
        /// Gets and sets the list that this control will use to gets its items.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the list that this control will use to gets its items.")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue((string)null)]
        public object DataSource
        {
            get { return _comboBox.DataSource; }
            set { _comboBox.DataSource = value; }
        }

        /// <summary>
        /// Gets and sets the property to display for the items in this control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]        
        public string DisplayMember
        {
            get { return _comboBox.DisplayMember; }
            set { _comboBox.DisplayMember = value; }
        }

        /// <summary>
        /// Gets and sets the formatting provider.
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IFormatProvider FormatInfo
        {
            get { return _comboBox.FormatInfo; }
            set { _comboBox.FormatInfo = value; }
        }

        /// <summary>
        /// Gets and sets the number of characters selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get { return _comboBox.SelectionLength; }
            set { _comboBox.SelectionLength = value; }
        }

        /// <summary>
        /// Gets and sets the starting index of selected text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return _comboBox.SelectionStart; }
            set { _comboBox.SelectionStart = value; }
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
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Controls the appearance and functionality of the KryptonComboBox.")]
        [DefaultValue(typeof(ComboBoxStyle), "DropDown")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get { return _comboBox.DropDownStyle; }
            
            set 
            {
                if (_comboBox.DropDownStyle != value)
                {
                    if (value == ComboBoxStyle.Simple)
                        throw new ArgumentOutOfRangeException("KryptonComboBox does not support the DropDownStyle.Simple style.");

                    _comboBox.DropDownStyle = value;
                    UpdateEditControl();
                }
            }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The height, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(200)]
        [Browsable(true)]
        public int DropDownHeight
        {
            get { return _comboBox.DropDownHeight; }
            set { _comboBox.DropDownHeight = value; }
        }

        /// <summary>
        /// Gets and sets the width, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The width, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int DropDownWidth
        {
            get { return _comboBox.DropDownWidth; }
            set { _comboBox.DropDownWidth = value; }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of items in an owner-draw KryptomComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("Do not use this property, it is provided for backwards compatability only.")]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ItemHeight
        {
            get { return _comboBox.ItemHeight; }
            
            set 
            { 
                // Do nothing, we set the ItemHeight internally to match the font 
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of entries to display in the drop-down list.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum number of entries to display in the drop-down list.")]
        [Localizable(true)]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get { return _comboBox.MaxDropDownItems; }
            set { _comboBox.MaxDropDownItems = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int MaxLength
        {
            get { return _comboBox.MaxLength; }
            set { _comboBox.MaxLength = value; }
        }

        /// <summary>
        /// Gets or sets whether the items in the list portion of the KryptonComboBox are sorted.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
        [DefaultValue(false)]
        public bool Sorted
        {
            get { return _comboBox.Sorted; }
            set { _comboBox.Sorted = value; }
        }

        /// <summary>
        /// Gets or sets the items in the KryptonComboBox.
        /// </summary>
        [Category("Data")]
        [Description("The items in the KryptonComboBox.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        [Localizable(true)]
        public ComboBox.ObjectCollection Items
        {
            get { return _comboBox.Items; }
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

        private void ResetInputControlStyle()
        {
            InputControlStyle = InputControlStyle.Standalone;
        }

        private bool ShouldSerializeInputControlStyle()
        {
            return (InputControlStyle != InputControlStyle.Standalone);
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
                    _stateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetItemStyle()
        {
            ItemStyle = ButtonStyle.ListItem;
        }

        private bool ShouldSerializeItemStyle()
        {
            return (ItemStyle != ButtonStyle.ListItem);
        }

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category("Visuals")]
        [Description("DropButton style.")]
        public ButtonStyle DropButtonStyle
        {
            get { return _dropButtonStyle; }

            set
            {
                if (_dropButtonStyle != value)
                {
                    _dropButtonStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetDropButtonStyle()
        {
            DropButtonStyle = ButtonStyle.InputControl;
        }

        private bool ShouldSerializeDropButtonStyle()
        {
            return (DropButtonStyle != ButtonStyle.InputControl);
        }

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category("Visuals")]
        [Description("DropButton style.")]
        public PaletteBackStyle DropBackStyle
        {
            get { return _dropBackStyle; }

            set
            {
                if (_dropBackStyle != value)
                {
                    _dropBackStyle = value;
                    _stateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetDropBackStyle()
        {
            DropBackStyle = PaletteBackStyle.ControlClient;
        }

        private bool ShouldSerializeDropBackStyle()
        {
            return (DropBackStyle != PaletteBackStyle.ControlClient);
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
        public ComboBoxButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return _comboBox.AutoCompleteCustomSource; }            
            set { _comboBox.AutoCompleteCustomSource = value; }
        }

        /// <summary>
        /// Gets or sets the text completion behavior of the combobox.
        /// </summary>
        [Description("Indicates the text completion behavior of the combobox.")]
        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _comboBox.AutoCompleteMode; }
            
            set 
            {
                _autoCompleteMode = value;
                _comboBox.AutoCompleteMode = value; 
            }
        }

        /// <summary>
        /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
        /// </summary>
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [DefaultValue(typeof(AutoCompleteSource), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return _comboBox.AutoCompleteSource; }
            
            set 
            {
                _autoCompleteSource = value;
                _comboBox.AutoCompleteSource = value; 
            }
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
            get { return _comboBox.FormatString; }
            set { _comboBox.FormatString = value; }
        }

        /// <summary>
        /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.
        /// </summary>
        [Description("If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.")]
        [DefaultValue(false)]
        public bool FormattingEnabled
        {
            get { return _comboBox.FormattingEnabled; }
            set { _comboBox.FormattingEnabled = value; }
        }

        /// <summary>
        /// Gets access to the common combobox appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common combobox appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
        /// Gets access to the disabled combobox appearance entries.
		/// </summary>
		[Category("Visuals")]
        [Description("Overrides for defining disabled combobox appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxStates StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
        /// Gets access to the normal combobox appearance entries.
		/// </summary>
		[Category("Visuals")]
        [Description("Overrides for defining normal combobox appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxStates StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

        /// <summary>
        /// Gets access to the active combobox appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxJustComboStates StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking combobox appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking combobox appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteComboBoxJustItemStates StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Finds the first item in the combo box that starts with the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindString(string str)
        {
            return _comboBox.FindString(str);
        }

        /// <summary>
        /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindString(string str, int startIndex)
        {
            return _comboBox.FindString(str, startIndex);
        }

        /// <summary>
        /// Finds the first item in the combo box that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindStringExact(string str)
        {
            return _comboBox.FindStringExact(str);
        }

        /// <summary>
        /// Finds the first item after the specified index that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindStringExact(string str, int startIndex)
        {
            return _comboBox.FindStringExact(str, startIndex);
        }

        /// <summary>
        /// Returns the height of an item in the ComboBox.
        /// </summary>
        /// <param name="index">The index of the item to return the height of.</param>
        /// <returns>The height, in pixels, of the item at the specified index.</returns>
        public int GetItemHeight(int index)
        {
            return _comboBox.GetItemHeight(index);
        }

        /// <summary>
        /// Returns the text representation of the specified item.
        /// </summary>
        /// <param name="item">The object from which to get the contents to display.</param>
        /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
        public string GetItemText(object item)
        {
            return _comboBox.GetItemText(item);
        }

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _comboBox.Select(start, length);
        }

        /// <summary>
        /// Selects all text in the control.
        /// </summary>
        public void SelectAll()
        {
            _comboBox.SelectAll();
        }

        /// <summary>
        /// Maintains performance when items are added to the ComboBox one at a time.
        /// </summary>
        public void BeginUpdate()
        {
            _comboBox.BeginUpdate();
        }

        /// <summary>
        /// Resumes painting the ComboBox control after painting is suspended by the BeginUpdate method. 
        /// </summary>
        public void EndUpdate()
        {
            _comboBox.EndUpdate();
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
                    return (DesignMode || AlwaysActive ||
                            ContainsFocus || _mouseOver || _comboBox.MouseOver ||
                           ((_subclassEdit != null) && (_subclassEdit.MouseOver)));
            }
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
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus()
        {
            if (ComboBox != null)
                return ComboBox.Focus();
            else
                return false;
        }

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select()
        {
            if (ComboBox != null)
                ComboBox.Select();
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

                // The inside combo box is the client rectangle size
                return new Rectangle(_comboHolder.Location, _comboHolder.Size);
			}
		}

        /// <summary>
        /// Override the display padding for the layout fill.
        /// </summary>
        /// <param name="padding">Display padding value.</param>
        public void SetLayoutDisplayPadding(Padding padding)
        {
            _layoutPadding = padding;
        }

        /// <summary>
        /// Internal desing mode method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
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
        /// Internal desing mode method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        /// <summary>
        /// Internal desing mode method.
        /// </summary>
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
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the TextUpdate event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTextUpdate(EventArgs e)
        {
            if (TextUpdate != null)
                TextUpdate(this, e);
        }

        /// <summary>
        /// Raises the SelectionChangeCommitted event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectionChangeCommitted(EventArgs e)
        {
            if (SelectionChangeCommitted != null)
                SelectionChangeCommitted(this, e);
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
        /// Raises the DropDownStyleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDownStyleChanged(EventArgs e)
        {
            if (DropDownStyleChanged != null)
                DropDownStyleChanged(this, e);
        }

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
        /// Raises the Format event.
        /// </summary>
        /// <param name="e">An ListControlConvertEventArgs containing the event data.</param>
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
        /// Raises the ValueMemberChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueMemberChanged(EventArgs e)
        {
            if (ValueMemberChanged != null)
                ValueMemberChanged(this, e);
        }

        /// <summary>
        /// Raises the DropDownClosed event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDownClosed(EventArgs e)
        {
            if (DropDownClosed != null)
                DropDownClosed(this, e);
        }

        /// <summary>
        /// Raises the DropDown event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDown(EventArgs e)
        {
            if (DropDown != null)
                DropDown(this, e);
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
        /// Creates a new instance of the control collection for the KryptonComboBox.
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

            // Subclass the child edit control
            UpdateEditControl();

            // Force the font to be set into the text box child control
            PerformNeedPaint(false);

            // We need a layout to occur before any painting
            InvokeLayout();

            // We need to recalculate the correct height
            Height = PreferredHeight;
        }

        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
            // Ensure we have subclassed the contained edit control
            UpdateEditControl();

            // Update view elements
            _drawDockerInner.Enabled = Enabled;
            _drawDockerOuter.Enabled = Enabled;

            // Update state to reflect change in enabled state
            _buttonManager.RefreshButtons();

            // Change in enabled state requires a layout and repaint
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
            ComboBox.TabStop = TabStop;
            base.OnTabStopChanged(e);
        }

        /// <summary>
        /// Raises the CausesValidationChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnCausesValidationChanged(EventArgs e)
        {
            ComboBox.CausesValidation = CausesValidation;
            base.OnCausesValidationChanged(e);
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // First time we paint we perform a layout to ensure drawing works correcly
            if (_firstTimePaint)
            {
                _firstTimePaint = false;
                ForceControlLayout();
            }

            base.OnPaint(e);
            if (Paint != null)
                Paint(this, e);
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
        /// Raises the MouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseOver = true;
            PerformNeedPaint(false);
            _comboBox.Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseOver = false;
            PerformNeedPaint(false);
            _comboBox.Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _comboBox.Focus();
        }

        /// <summary>
        /// Occurs when a user preference has changed.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event details.</param>
        protected override void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (_comboBox != null)
            {
                UpdateStateAndPalettes();
                IPaletteTriple triple = GetComboBoxTripleState();
                PaletteState state = _drawDockerOuter.State;
                _comboBox.BackColor = triple.PaletteBack.GetBackColor1(state);
                _comboBox.ForeColor = triple.PaletteContent.GetContentShortTextColor1(state);
                _comboBox.Font = (Font)triple.PaletteContent.GetContentShortTextFont(state);
                _comboBox.ClearAppThemed();
                _comboHolder.BackColor = _comboBox.BackColor;
            }

            base.OnUserPreferenceChanged(sender, e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!IsDisposed && !Disposing && !DroppedDown)
            {
                AttachEditControl();

                // Update to match the new palette settings
                Height = PreferredHeight;

                // Let base class calulcate fill rectangle
                base.OnLayout(levent);

                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if (_forcedLayout || (DesignMode && (_comboHolder != null)))
                {
                    // Only need to relayout if there is something that would be visible
                    if ((_layoutFill.FillRect.Height > 0) && (_layoutFill.FillRect.Width > 0))
                    {
                        // Only update the bounds if they have changed
                        Rectangle fillRect = _layoutFill.FillRect;
                        if (fillRect != _comboHolder.Bounds)
                        {
                            _comboHolder.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
                            _comboBox.SetBounds(-(1 + _layoutPadding.Left), 
                                                -(1 + _layoutPadding.Top), 
                                                fillRect.Width + 2 + _layoutPadding.Right,
                                                fillRect.Height + 2 + _layoutPadding.Bottom);
                        }
                    }
                }
            }
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
            // If setting the actual height
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                // First time the height is set, remember it
                if (_cachedHeight == -1)
                    _cachedHeight = height;

                // Override the actual height used
                height = PreferredHeight;
            }

            // If setting the actual height then cache it for later
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
                _cachedHeight = height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(121, PreferredHeight); }
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (!e.NeedLayout)
                _comboBox.Invalidate();
            else if (!DroppedDown)
                ForceControlLayout();

            if (!IsDisposed && !Disposing)
            {
                UpdateStateAndPalettes();
                IPaletteTriple triple = GetComboBoxTripleState();
                PaletteState state = _drawDockerOuter.State;
                _comboBox.BackColor = triple.PaletteBack.GetBackColor1(state);
                _comboBox.ForeColor = triple.PaletteContent.GetContentShortTextColor1(state);
                _comboBox.Font = triple.PaletteContent.GetContentShortTextFont(state);
                _comboHolder.BackColor = _comboBox.BackColor;
            }

            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnPaletteChanged(EventArgs e)
        {
            base.OnPaletteChanged(e);
            _comboBox.Invalidate();
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            base.OnPaletteChanged(e);
            _comboBox.Invalidate();
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

        internal bool IsFixedActive
        {
            get { return (_fixedActive != null); }
        }

        internal void DetachEditControl()
        {
            if (_subclassEdit != null)
            {
                // Stop subclassing
                //_subclassEdit.ReleaseHandle();
                _subclassEdit = null;
            }
        }
        #endregion

        #region Implementation
        private void AttachEditControl()
        {
            if (!IsDisposed && !Disposing)
            {
                // Only need to subclass once
                if (_subclassEdit == null)
                {
                    // Find the first child
                    IntPtr childPtr = PI.GetWindow(_comboBox.Handle, PI.GW_CHILD);

                    // If we found a child then it is the edit class
                    if (childPtr != IntPtr.Zero)
                    {
                        _subclassEdit = new SubclassEdit(childPtr, this);
                        _subclassEdit.TrackMouseEnter += new EventHandler(OnComboBoxMouseChange);
                        _subclassEdit.TrackMouseLeave += new EventHandler(OnComboBoxMouseChange);
                    }
                }
            }
        }

        private void UpdateEditControl()
        {
            AttachEditControl();

            // Only show the child edit control when we are enabled
            if (_subclassEdit != null)
                _subclassEdit.Visible = Enabled;
        }

        private void UpdateStateAndPalettes()
        {
            // Get the correct palette settings to use
            IPaletteTriple tripleState = GetComboBoxTripleState();
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

        internal IPaletteTriple GetComboBoxTripleState()
        {
            if (Enabled)
            {
                if (IsActive)
                    return _stateActive.ComboBox;
                else
                    return _stateNormal.ComboBox;
            }
            else
                return _stateDisabled.ComboBox;
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

        private void OnComboBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle drawBounds = e.Bounds;

            // Do we need to draw the edit area
            if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
            {
                // Always get base implementation to draw the background
                e.DrawBackground();

                // Find correct text color
                Color textColor = _comboBox.ForeColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    textColor = SystemColors.HighlightText;

                // Find correct background color
                Color backColor = _comboBox.BackColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    backColor = SystemColors.Highlight;
                    
                // Is there an item to draw
                if (e.Index >= 0)
                {
                    // Set the correct text rendering hint for the text drawing. We only draw if the edit text is enabled so we
                    // just always grab the normal state value. Without this line the wrong hint can occur because it inherits
                    // it from the device context. Resulting in blurred text.
                    e.Graphics.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(StateNormal.Item.PaletteContent.GetContentShortTextHint(PaletteState.Normal));

                    TextFormatFlags flags = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding;
                    
                    // Use the correct prefix setting
                    flags |= TextFormatFlags.NoPrefix;

                    // Do we need to switch drawing direction?
                    if (RightToLeft == RightToLeft.Yes)
                        flags |= TextFormatFlags.Right;

                    // Draw text using font defined by the control
                    TextRenderer.DrawText(e.Graphics,
                                          _comboBox.Text, _comboBox.Font,
                                          drawBounds,
                                          textColor, backColor,
                                          flags);
                }
            }
            else
            {
                // Is there an item to draw
                if (e.Index >= 0)
                {
                    // Update our content object with values from the list item
                    UpdateContentFromItemIndex(e.Index);

                    // By default the button is in the normal state
                    PaletteState buttonState = PaletteState.Normal;

                    // Is this item disabled
                    if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
                        buttonState = PaletteState.Disabled;
                    else
                    {
                        // If selected then show as a checked item
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            buttonState = PaletteState.Tracking;
                    }

                    // Update the view with the calculated state
                    _drawButton.ElementState = buttonState;

                    // Grab the raw device context for the graphics instance
                    IntPtr hdc = e.Graphics.GetHdc();

                    try
                    {
                        // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                        IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, drawBounds.Right, drawBounds.Bottom);

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
                                        context.DisplayRectangle = drawBounds;
                                        _drawPanel.Layout(context);
                                        _drawButton.Layout(context);
                                    }

                                    // Ask the view element to actually draw
                                    using (RenderContext context = new RenderContext(this, g, drawBounds, Renderer))
                                    {
                                        _drawPanel.Render(context);
                                        _drawButton.Render(context);
                                    }

                                    // Now blit from the bitmap from the screen to the real dc
                                    PI.BitBlt(hdc, drawBounds.X, drawBounds.Y, drawBounds.Width, drawBounds.Height, _screenDC, drawBounds.X, drawBounds.Y, PI.SRCCOPY);
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
            }
        }

        private void OnComboBoxMeasureItem(object sender, MeasureItemEventArgs e)
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
                _contentValues.ShortText = _comboBox.GetItemText(Items[index]);
                _contentValues.LongText = null;
                _contentValues.Image = null;
                _contentValues.ImageTransparentColor = Color.Empty;
            }

            // Always ensure there is some text that can be measured, if only a single space. The height of
            // the first item is used to calculate the total height of the drop down. So if the first time
            // had null then the height would be very small for the item and also the drop down.
            if (string.IsNullOrEmpty(_contentValues.ShortText))
                _contentValues.ShortText = " ";
        }

        private void OnComboBoxMouseChange(object sender, EventArgs e)
        {
            // Find new tracking mouse change state
            bool tracking = _comboBox.MouseOver || ((_subclassEdit != null) && _subclassEdit.MouseOver);

            // Change in tracking state?
            if (tracking != _trackingMouseEnter)
            {
                _trackingMouseEnter = tracking;

                // Raise appropriate event
                if (_trackingMouseEnter)
                    OnTrackMouseEnter(EventArgs.Empty);
                else
                    OnTrackMouseLeave(EventArgs.Empty);
            }

            PerformNeedPaint(false);
            _comboBox.Invalidate();
        }

        private void OnComboBoxGotFocus(object sender, EventArgs e)
        {
            base.OnGotFocus(e);
            PerformNeedPaint(false);
            _comboBox.Invalidate();
        }

        private void OnComboBoxLostFocus(object sender, EventArgs e)
        {
            base.OnLostFocus(e);
            PerformNeedPaint(false);
            _comboBox.Invalidate();
        }

        private void OnComboBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void OnComboBoxTextUpdate(object sender, EventArgs e)
        {
            OnTextUpdate(e);
        }

        private void OnComboBoxSelectionChangeCommitted(object sender, EventArgs e)
        {
            OnSelectionChangeCommitted(e);
        }

        private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(e);
        }

        private void OnComboBoxDropDownStyleChanged(object sender, EventArgs e)
        {
            OnDropDownStyleChanged(e);
        }

        private void OnComboBoxDataSourceChanged(object sender, EventArgs e)
        {
            OnDataSourceChanged(e);
        }

        private void OnComboBoxDisplayMemberChanged(object sender, EventArgs e)
        {
            OnDisplayMemberChanged(e);
        }

        private void OnComboBoxDropDownClosed(object sender, EventArgs e)
        {
            _comboBox.Dropped = false;
            Refresh();
            OnDropDownClosed(e);
        }

        private void OnComboBoxDropDown(object sender, EventArgs e)
        {
            _comboBox.Dropped = true;
            Refresh();
            OnDropDown(e);
        }

        private void OnComboBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnComboBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnComboBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnComboBoxValidated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void OnComboBoxValidating(object sender, CancelEventArgs e)
        {
            OnValidating(e);
        }

        private void OnComboBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            OnFormat(e);
        }

        private void OnComboBoxFormatInfoChanged(object sender, EventArgs e)
        {
            OnFormatInfoChanged(e);
        }

        private void OnComboBoxFormatStringChanged(object sender, EventArgs e)
        {
            OnFormatStringChanged(e);
        }

        private void OnComboBoxFormattingEnabledChanged(object sender, EventArgs e)
        {
            OnFormattingEnabledChanged(e);
        }

        private void OnComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            UpdateEditControl();
            PerformNeedPaint(false);
            _comboBox.Invalidate();
            OnSelectedValueChanged(e);
        }

        private void OnComboBoxValueMemberChanged(object sender, EventArgs e)
        {
            OnValueMemberChanged(e);
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
        #endregion
    }
}
