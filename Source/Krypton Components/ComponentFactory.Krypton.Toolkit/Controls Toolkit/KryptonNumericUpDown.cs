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
    /// Provide a NumericUpDown with Krypton styling applied.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonNumericUpDown), "ToolboxBitmaps.KryptonNumericUpDown.bmp")]
    [DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
    [DefaultBindingProperty("Value")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonNumericUpDownDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Represents a Windows spin box (also known as an up-down control) that displays numeric values.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonNumericUpDown : VisualControlBase,
                                        IContainedInputControl
    {
        #region Classes
        private class InternalNumericUpDown : NumericUpDown
        {
            #region Instance Fields
            private KryptonNumericUpDown _kryptonNumericUpDown;
            private bool _mouseOver;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalTextBox.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalTextBox.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the InternalTextBox class.
            /// </summary>
            /// <param name="kryptonNumericUpDown">Reference to owning control.</param>
            public InternalNumericUpDown(KryptonNumericUpDown kryptonNumericUpDown)
            {
                _kryptonNumericUpDown = kryptonNumericUpDown;

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
                        if (_kryptonNumericUpDown.InTransparentDesignMode)
                            m.Result = (IntPtr)PI.HTTRANSPARENT;
                        else
                            base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        // Mouse is not over the control
                        MouseOver = false;
                        _kryptonNumericUpDown.PerformNeedPaint(true);
                        Invalidate();
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Mouse is over the control
                        if (!MouseOver)
                        {
                            MouseOver = true;
                            _kryptonNumericUpDown.PerformNeedPaint(true);
                            Invalidate();
                        }
                        base.WndProc(ref m);
                        break;
                    case PI.WM_CONTEXTMENU:
                        // Only interested in overriding the behavior when we have a krypton context menu...
                        if (_kryptonNumericUpDown.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)((long)m.LParam)) == -1)
                                mousePt = PointToScreen(new Point(Width / 2, Height / 2));

                            // Show the context menu
                            _kryptonNumericUpDown.KryptonContextMenu.Show(_kryptonNumericUpDown, mousePt);

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

            #region Internal
            /// <summary>
            /// Gets or sets a value indicating whether a value has been entered by the user.
            /// </summary>
            internal protected bool InternalUserEdit 
            {
                get { return UserEdit; }
                set { UserEdit = value; }
            }   
            #endregion
        }

        private class SubclassEdit : NativeWindow
        {
            #region Instance Fields
            private KryptonNumericUpDown _kryptonNumericUpDown;
            private InternalNumericUpDown _internalNumericUpDown;
            private bool _mouseOver;
            private Point _mousePoint;
            #endregion

            #region Events
            /// <summary>
            /// Occurs when the mouse enters the InternalNumericUpDown.
            /// </summary>
            public event EventHandler TrackMouseEnter;

            /// <summary>
            /// Occurs when the mouse leaves the InternalNumericUpDown.
            /// </summary>
            public event EventHandler TrackMouseLeave;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the SubclassEdit class.
            /// </summary>
            /// <param name="editControl">Handle of the Edit control to subclass.</param>
            /// <param name="kryptonNumericUpDown">Reference to top level control.</param>
            /// <param name="internalNumericUpDown">Reference to numeric internal control.</param>
            public SubclassEdit(IntPtr editControl,
                                KryptonNumericUpDown kryptonNumericUpDown,
                                InternalNumericUpDown internalNumericUpDown)
            {
                _kryptonNumericUpDown = kryptonNumericUpDown;
                _internalNumericUpDown = internalNumericUpDown;

                // Attach ourself to the provided control, subclassing it
                AssignHandle(editControl);

                // By default, not over a valid part of the client
                _mousePoint = new Point(-int.MaxValue, -int.MaxValue);
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
            /// Gets the last mouse point if the mouse is over the control.
            /// </summary>
            public Point MousePoint
            {
                get { return _mousePoint; }
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
            /// Gets access to the owning numeric up down control.
            /// </summary>
            protected KryptonNumericUpDown NumericUpDown
            {
                get { return _kryptonNumericUpDown; }
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
                        if (NumericUpDown.InTransparentDesignMode)
                            m.Result = (IntPtr)PI.HTTRANSPARENT;
                        else
                            base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSELEAVE:
                        // Mouse is not over the control
                        MouseOver = false;
                        _mousePoint = new Point(-int.MaxValue, -int.MaxValue);
                        NumericUpDown.PerformNeedPaint(true);
                        base.WndProc(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Extra mouse position
                        _mousePoint = new Point((int)m.LParam.ToInt64());

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
                            NumericUpDown.PerformNeedPaint(true);
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
                                using (SolidBrush backBrush = new SolidBrush(_internalNumericUpDown.BackColor))
                                    g.FillRectangle(backBrush, new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top));

                                // Create rect for the text area
                                Size borderSize = SystemInformation.BorderSize;
                                rect.left -= (borderSize.Width + 1);

                                // If enabled then let the combo draw the text area
                                if (_kryptonNumericUpDown.Enabled)
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
                                    g.TextRenderingHint = CommonHelper.PaletteTextHintToRenderingHint(_kryptonNumericUpDown.StateDisabled.PaletteContent.GetContentShortTextHint(PaletteState.Disabled));

                                    // Define the string formatting requirements
                                    StringFormat stringFormat = new StringFormat();
                                    stringFormat.LineAlignment = StringAlignment.Near;
                                    stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                    stringFormat.Trimming = StringTrimming.None;

                                    switch (_kryptonNumericUpDown.TextAlign)
                                    {
                                        case HorizontalAlignment.Left:
                                            if (_kryptonNumericUpDown.RightToLeft == RightToLeft.Yes)
                                                stringFormat.Alignment = StringAlignment.Far;
                                            else
                                                stringFormat.Alignment = StringAlignment.Near;
                                            break;
                                        case HorizontalAlignment.Right:
                                            if (_kryptonNumericUpDown.RightToLeft == RightToLeft.Yes)
                                                stringFormat.Alignment = StringAlignment.Far;
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
                                    try
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(_internalNumericUpDown.ForeColor))
                                            g.DrawString(_internalNumericUpDown.Text, _internalNumericUpDown.Font, foreBrush,
                                                         new RectangleF(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top),
                                                         stringFormat);
                                    }
                                    catch (ArgumentException)
                                    {
                                        using (SolidBrush foreBrush = new SolidBrush(_internalNumericUpDown.ForeColor))
                                            g.DrawString(_internalNumericUpDown.Text, _kryptonNumericUpDown.GetTripleState().PaletteContent.GetContentShortTextFont(PaletteState.Disabled), foreBrush,
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
                        if (NumericUpDown.KryptonContextMenu != null)
                        {
                            // Extract the screen mouse position (if might not actually be provided)
                            Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                            // If keyboard activated, the menu position is centered
                            if (((int)((long)m.LParam)) == -1)
                            {
                                PI.RECT clientRect;
                                PI.GetClientRect(Handle, out clientRect);
                                mousePt = NumericUpDown.PointToScreen(new Point((clientRect.right - clientRect.left) / 2,
                                                                                (clientRect.bottom - clientRect.top) / 2));
                            }

                            // Show the context menu
                            NumericUpDown.KryptonContextMenu.Show(NumericUpDown, mousePt);

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
        
        private class SubclassButtons : SubclassEdit, IContentValues, IDisposable
        {
            #region Instance Fields
            private PaletteTripleToPalette _palette;
            private ViewDrawButton _viewButton;
            private IntPtr _screenDC;
            private Point _mousePressed;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the SubclassButtons class.
            /// </summary>
            /// <param name="buttonsPtr">Handle of the Buttons control to subclass.</param>
            /// <param name="kryptonNumericUpDown">Reference to top level control.</param>
            /// <param name="internalNumericUpDown">Reference to internal numeric control.</param>
            public SubclassButtons(IntPtr buttonsPtr,
                                   KryptonNumericUpDown kryptonNumericUpDown,
                                   InternalNumericUpDown internalNumericUpDown)
                : base(buttonsPtr, kryptonNumericUpDown, internalNumericUpDown)
            {
                _mousePressed = new Point(-int.MaxValue, -int.MaxValue);

                // We need to create and cache a device context compatible with the display
                _screenDC = PI.CreateCompatibleDC(IntPtr.Zero);
            }
            #endregion  

            #region Public
            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            public void Dispose()
            {
                if (_screenDC != IntPtr.Zero)
                {
                    PI.DeleteDC(_screenDC);
                    _screenDC = IntPtr.Zero;
                }
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
            /// Process Windows-based messages.
            /// </summary>
            /// <param name="m">A Windows-based message.</param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_LBUTTONDBLCLK:
                    case PI.WM_LBUTTONDOWN:
                        _mousePressed = new Point((int)m.LParam.ToInt64());
                        base.WndProc(ref m);
                        PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x300);
                        break;
                    case PI.WM_LBUTTONUP:
                    case PI.WM_MBUTTONUP:
                    case PI.WM_MBUTTONDOWN:
                    case PI.WM_RBUTTONUP:
                    case PI.WM_RBUTTONDOWN:
                        _mousePressed = new Point(-int.MaxValue, -int.MaxValue);
                        base.WndProc(ref m);
                        PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x300);
                        break;
                    case PI.WM_PRINTCLIENT:
                    case PI.WM_PAINT:
                        IntPtr hdc;
                        PI.PAINTSTRUCT ps = new PI.PAINTSTRUCT();

                        // Do we need to BeginPaint or just take the given HDC?
                        if (m.WParam == IntPtr.Zero)
                            hdc = PI.BeginPaint(Handle, ref ps);
                        else
                            hdc = m.WParam;

                        // Grab the client area of the control
                        PI.RECT rect = new PI.RECT();
                        PI.GetClientRect(Handle, out rect);
                        Rectangle clientRect = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

                        try
                        {
                            // Create bitmap that all drawing occurs onto, then we can blit it later to remove flicker
                            IntPtr hBitmap = PI.CreateCompatibleBitmap(hdc, clientRect.Right, clientRect.Bottom);

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
                                        // Drawn entire client area in the background color
                                        using (SolidBrush backBrush = new SolidBrush(NumericUpDown.NumericUpDown.BackColor))
                                            g.FillRectangle(backBrush, clientRect);

                                        // Draw the actual up and down buttons split inside the client rectangle
                                        DrawUpDownButtons(g, new Rectangle(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height - 1));

                                        // Now blit from the bitmap from the screen to the real dc
                                        PI.BitBlt(hdc, clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height, _screenDC, clientRect.X, clientRect.Y, PI.SRCCOPY);
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
                            // Do we need to match the original BeginPaint?
                            if (m.WParam == IntPtr.Zero)
                                PI.EndPaint(Handle, ref ps);
                        }

                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            #endregion

            #region Private
            private void DrawUpDownButtons(Graphics g, Rectangle clientRect)
            {
                // Create the views and palette entries first time around
                if (_viewButton == null)
                {
                    // Create helper object to get all values from the KryptonNumericUpDown redirector
                    _palette = new PaletteTripleToPalette(NumericUpDown.Redirector,
                                                          PaletteBackStyle.ButtonStandalone,
                                                          PaletteBorderStyle.ButtonStandalone,
                                                          PaletteContentStyle.ButtonStandalone);

                    // Create view element for drawing the actual buttons
                    _viewButton = new ViewDrawButton(_palette, _palette, _palette,
                                                     _palette, _palette, _palette, _palette,
                                                     new PaletteMetricRedirect(NumericUpDown.Redirector),
                                                     this, VisualOrientation.Top, false);
                }

                // Update with the latset button style for the up/down buttons
                _palette.SetStyles(NumericUpDown.UpDownButtonStyle);

                // Find button rectangles
                Rectangle upRect = new Rectangle(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height / 2);
                Rectangle downRect = new Rectangle(clientRect.X, upRect.Bottom, clientRect.Width, clientRect.Bottom - upRect.Bottom);

                // Position and draw the up/down buttons
                using (ViewLayoutContext layoutContext = new ViewLayoutContext(NumericUpDown, NumericUpDown.Renderer))
                    using (RenderContext renderContext = new RenderContext(NumericUpDown, g, clientRect, NumericUpDown.Renderer))
                    {
                        // Up button
                        layoutContext.DisplayRectangle = upRect;
                        _viewButton.ElementState = ButtonElementState(upRect);
                        _viewButton.Layout(layoutContext);
                        _viewButton.Render(renderContext);
                        renderContext.Renderer.RenderGlyph.DrawInputControlNumericUpGlyph(renderContext, _viewButton.ClientRectangle, _palette.PaletteContent, _viewButton.ElementState);

                        // Down button
                        layoutContext.DisplayRectangle = downRect;
                        _viewButton.ElementState = ButtonElementState(downRect);
                        _viewButton.Layout(layoutContext);
                        _viewButton.Render(renderContext);
                        renderContext.Renderer.RenderGlyph.DrawInputControlNumericDownGlyph(renderContext, _viewButton.ClientRectangle, _palette.PaletteContent, _viewButton.ElementState);
                    }
            }

            private PaletteState ButtonElementState(Rectangle buttonRect)
            {
                if (NumericUpDown.Enabled)
                {
                    if (MouseOver && buttonRect.Contains(MousePoint))
                    {
                        if (buttonRect.Contains(_mousePressed))
                            return PaletteState.Pressed;
                        else if (_mousePressed.X == -int.MaxValue)
                            return PaletteState.Tracking;
                    }

                    if (NumericUpDown.IsActive || (NumericUpDown.IsFixedActive && (NumericUpDown.InputControlStyle == InputControlStyle.Standalone)))
                    {
                        if (NumericUpDown.InputControlStyle == InputControlStyle.Standalone)
                            return PaletteState.CheckedNormal;
                        else
                            return PaletteState.CheckedTracking;
                    }
                    else
                        return PaletteState.Normal;
                }
                else
                    return PaletteState.Disabled;
            }
            #endregion
        }
        #endregion

        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class NumericUpDownButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the NumericUpDownButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public NumericUpDownButtonSpecCollection(KryptonNumericUpDown owner)
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
        private NumericUpDownButtonSpecCollection _buttonSpecs;
        private PaletteInputControlTripleRedirect _stateCommon;
        private PaletteInputControlTripleStates _stateDisabled;
        private PaletteInputControlTripleStates _stateNormal;
        private PaletteInputControlTripleStates _stateActive;
        private ViewLayoutDocker _drawDockerInner;
        private ViewDrawDocker _drawDockerOuter;
        private ViewLayoutFill _layoutFill;
        private InternalNumericUpDown _numericUpDown;
        private InputControlStyle _inputControlStyle;
        private ButtonStyle _upDownButtonStyle;
        private SubclassEdit _subclassEdit;
        private SubclassButtons _subclassButtons;
        private Nullable<bool> _fixedActive;
        private bool _inRibbonDesignMode;
        private bool _forcedLayout;
        private bool _mouseOver;
        private bool _alwaysActive;
        private bool _allowButtonSpecToolTips;
        private bool _trackingMouseEnter;
        private int _cachedHeight;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Value property changes.
        /// </summary>
        [Description("Occurs when the value of the Value property changes.")]
        [Category("Action")]
        public event EventHandler ValueChanged;

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
        /// Occurs when the value of the PaddingChanged property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler PaddingChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonNumericUpDown class.
		/// </summary>
        public KryptonNumericUpDown()
        {
            // Contains another control and needs marking as such for validation to work
            SetStyle(ControlStyles.ContainerControl, true);

            // By default we are not multiline and so the height is fixed
            SetStyle(ControlStyles.FixedHeight, true);

            // Cannot select this control, only the child TextBox
            SetStyle(ControlStyles.Selectable, false);

            // Defaults
            _inputControlStyle = InputControlStyle.Standalone;
            _upDownButtonStyle = ButtonStyle.InputControl;
            _cachedHeight = -1;
            _alwaysActive = true;
            _allowButtonSpecToolTips = false;

            // Create storage properties
            _buttonSpecs = new NumericUpDownButtonSpecCollection(this);

            // Create the palette storage
            _stateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
            _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateActive = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);

            // Create the internal numeric updown used for containing content
            _numericUpDown = new InternalNumericUpDown(this);
            _numericUpDown.TextChanged += new EventHandler(OnNumericUpDownTextChanged);
            _numericUpDown.ValueChanged += new EventHandler(OnNumericUpDownValueChanged);
            _numericUpDown.TrackMouseEnter += new EventHandler(OnNumericUpDownMouseChange);
            _numericUpDown.TrackMouseLeave += new EventHandler(OnNumericUpDownMouseChange);
            _numericUpDown.GotFocus += new EventHandler(OnNumericUpDownGotFocus);
            _numericUpDown.LostFocus += new EventHandler(OnNumericUpDownLostFocus);
            _numericUpDown.KeyDown += new KeyEventHandler(OnNumericUpDownKeyDown);
            _numericUpDown.KeyUp += new KeyEventHandler(OnNumericUpDownKeyUp);
            _numericUpDown.KeyPress += new KeyPressEventHandler(OnNumericUpDownKeyPress);
            _numericUpDown.PreviewKeyDown += new PreviewKeyDownEventHandler(OnNumericUpDownPreviewKeyDown);
            _numericUpDown.Validating += new CancelEventHandler(OnNumericUpDownValidating);
            _numericUpDown.Validated += new EventHandler(OnNumericUpDownValidated);

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_numericUpDown);
            _layoutFill.DisplayPadding = new Padding(1, 1, 1, 0);

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
            ((KryptonReadOnlyControls)Controls).AddInternal(_numericUpDown);
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

                // Tell the buttons class to cleanup resources
                if (_subclassButtons != null)
                    _subclassButtons.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

		#region Public
        /// <summary>
        /// Gets and sets if the control is in the tab chain.
        /// </summary>
        public new bool TabStop
        {
            get { return _numericUpDown.TabStop; }
            set { _numericUpDown.TabStop = value; }
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
        /// Gets access to the contained NumericUpDown instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public NumericUpDown NumericUpDown
        {
            get { return _numericUpDown; }
        }

        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public Control ContainedControl
        {
            get { return NumericUpDown; }
        }

        /// <summary>
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        [Browsable(false)]
        public override bool Focused
        {
            get { return NumericUpDown.Focused; }
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
        /// Gets and sets the Text value.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public override string Text
        {
            get { return _numericUpDown.Text; }
            set { _numericUpDown.Text = value; }
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
                _numericUpDown.ContextMenuStrip = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of decimal places to display.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the number of decimal places to display.")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return _numericUpDown.DecimalPlaces; }
            set { _numericUpDown.DecimalPlaces = value; }
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
        /// Gets or sets the amount to increment or decrement one each button click.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the amount to increment or decrement one each button click.")]
        [DefaultValue(typeof(Decimal), "1")]
        public decimal Increment
        {
            get { return _numericUpDown.Increment; }
            set { _numericUpDown.Increment = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value for the numeric up-down control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the maximum value for the numeric up-down control.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(Decimal), "100")]
        public decimal Maximum
        {
            get { return _numericUpDown.Maximum; }
            set { _numericUpDown.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the minimum value for the numeric up-down control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the minimum value for the numeric up-down control.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(Decimal), "0")]
        public decimal Minimum
        {
            get { return _numericUpDown.Minimum; }
            set { _numericUpDown.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets whether the thousands separator wil be inserted between each three decimal digits.
        /// </summary>
        [Category("Data")]
        [Description("Indicates whether the thousands separator wil be inserted between each three decimal digits.")]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool ThousandsSeparator
        {
            get { return _numericUpDown.ThousandsSeparator; }
            set { _numericUpDown.ThousandsSeparator = value; }
        }

        /// <summary>
        /// Gets or sets the current value of the numeric up-down control.
        /// </summary>
        [Category("Appearance")]
        [Description("The current value of the numeric up-down control.")]
        [DefaultValue(typeof(Decimal), "0")]
        [Bindable(true)]
        public decimal Value
        {
            get { return _numericUpDown.Value; }
            set { _numericUpDown.Value = value; }
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
            get { return _numericUpDown.TextAlign; }
            set { _numericUpDown.TextAlign = value; }
        }

        /// <summary>
        /// Gets or sets wheather the numeric up-down should display its value in hexadecimal.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates wheather the numeric up-down should display its value in hexadecimal.")]
        [DefaultValue(false)]
        public bool Hexadecimal
        {
            get { return _numericUpDown.Hexadecimal; }
            set { _numericUpDown.Hexadecimal = value; }
        }

        /// <summary>
        /// Gets or sets how the up-down control will position the up down buttons relative to its text box.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates how the up-down control will position the up down buttons relative to its text box.")]
        [DefaultValue(typeof(LeftRightAlignment), "Right")]
        [Localizable(true)]
        public LeftRightAlignment UpDownAlign
        {
            get { return _numericUpDown.UpDownAlign; }
            set { _numericUpDown.UpDownAlign = value; }
        }

        /// <summary>
        /// Gets or sets whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.")]
        [DefaultValue(true)]
        public bool InterceptArrowKeys
        {
            get { return _numericUpDown.InterceptArrowKeys; }
            set { _numericUpDown.InterceptArrowKeys = value; }
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
            get { return _numericUpDown.ReadOnly; }
            set { _numericUpDown.ReadOnly = value; }
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
        /// Gets and sets the up and down buttons style.
        /// </summary>
        [Category("Visuals")]
        [Description("Up and down buttons style.")]
        public ButtonStyle UpDownButtonStyle
        {
            get { return _upDownButtonStyle; }

            set
            {
                if (_upDownButtonStyle != value)
                {
                    _upDownButtonStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetUpDownButtonStyle()
        {
            UpDownButtonStyle = ButtonStyle.InputControl;
        }

        private bool ShouldSerializeUpDownButtonStyle()
        {
            return (UpDownButtonStyle != ButtonStyle.InputControl);
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
        public NumericUpDownButtonSpecCollection ButtonSpecs
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
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _numericUpDown.Select(start, length);
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
                    return (DesignMode || AlwaysActive ||
                            ContainsFocus || _mouseOver || _numericUpDown.MouseOver ||
                           ((_subclassEdit != null) && (_subclassEdit.MouseOver)) ||
                           ((_subclassButtons != null) && (_subclassButtons.MouseOver)));
            }
        }

        /// <summary>
        /// Sets input focus to the control.
        /// </summary>
        /// <returns>true if the input focus request was successful; otherwise, false.</returns>
        public new bool Focus()
        {
            if (NumericUpDown != null)
                return NumericUpDown.Focus();
            else
                return false;
        }

        /// <summary>
        /// Activates the control.
        /// </summary>
        public new void Select()
        {
            if (NumericUpDown != null)
                NumericUpDown.Select();
        }

        /// <summary>
        /// Displays the previous item in the collection.
        /// </summary>
        public void UpButton()
        {
            NumericUpDown.UpButton();
        }

        /// <summary>
        /// Displays the next item in the collection.
        /// </summary>
        public void DownButton()
        {
            NumericUpDown.DownButton();
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
                return new Rectangle(_numericUpDown.Location, _numericUpDown.Size);
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
            if (!IsHandleCreated)
            {
                _forcedLayout = true;
                OnLayout(new LayoutEventArgs(null, null));
                _forcedLayout = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a value has been entered by the user.
        /// </summary>
        protected bool UserEdit 
        {
            get { return _numericUpDown.InternalUserEdit; }
            set { _numericUpDown.InternalUserEdit = value; }
        }        
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
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

            // Subclass the various child controls of the numeric up down control
            UpdateChildEditControl();
            SubclassButtonsControl();

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
			// Change in enabled state requires a layout and repaint
            UpdateStateAndPalettes();

            // Ensure we have subclassed the contained edit control
            UpdateChildEditControl();

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
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnPaletteChanged(EventArgs e)
        {
            InvalidateChildren();
            base.OnPaletteChanged(e);
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            InvalidateChildren();
            base.OnPaletteChanged(e);
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
                // Subclass the various child controls of the numeric up down control
                SubclassEditControl();
                SubclassButtonsControl();

                // Update to match the new palette settings
                Height = PreferredHeight;

                // Let base class calulcate fill rectangle
                base.OnLayout(levent);

                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if (IsHandleCreated || _forcedLayout || (DesignMode && (_numericUpDown != null)))
                {
                    Rectangle fillRect = _layoutFill.FillRect;
                    _numericUpDown.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
                }
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
            InvalidateChildren();
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
            InvalidateChildren();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _numericUpDown.Focus();
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
            get { return new Size(120, PreferredHeight); }
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (IsHandleCreated && !e.NeedLayout)
                InvalidateChildren();
            else
                ForceControlLayout();

            if (!IsDisposed && !Disposing)
            {
                // Update the back/fore/font from the palette settings
                UpdateStateAndPalettes();
                IPaletteTriple triple = GetTripleState();
                PaletteState state = _drawDockerOuter.State;
                _numericUpDown.BackColor = triple.PaletteBack.GetBackColor1(state);
                _numericUpDown.ForeColor = triple.PaletteContent.GetContentShortTextColor1(state);

                // Only set the font if the numeric up down has been created
                Font font = triple.PaletteContent.GetContentShortTextFont(state);
                if ((_numericUpDown.Handle != IntPtr.Zero) && !_numericUpDown.Font.Equals(font))
                    _numericUpDown.Font = font;
            }
            
            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Raises the PaddingChanged event.
        /// </summary>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
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
            NumericUpDown.TabStop = TabStop;
            base.OnTabStopChanged(e);
        }

        /// <summary>
        /// Raises the CausesValidationChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnCausesValidationChanged(EventArgs e)
        {
            NumericUpDown.CausesValidation = CausesValidation;
            base.OnCausesValidationChanged(e);
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
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
        #endregion

        #region Implementation
        private void InvalidateChildren()
        {
            if (NumericUpDown != null)
            {
                NumericUpDown.Invalidate();
                PI.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, 0x85);
            }
        }

        private void SubclassEditControl()
        {
            // If the edit control has been recreated, then release our current subclassing
            if (_subclassEdit != null) 
            {
                if (_numericUpDown.Controls.Count >= 2)
                {
                    if (_subclassEdit.Handle != _numericUpDown.Controls[1].Handle)
                    {
                        _subclassEdit.TrackMouseEnter -= new EventHandler(OnNumericUpDownMouseChange);
                        _subclassEdit.TrackMouseLeave -= new EventHandler(OnNumericUpDownMouseChange);
                        _subclassEdit.ReleaseHandle();
                        _subclassEdit = null;
                    }
                }
            }

            // Do we need to subclass the edit control
            if (_subclassEdit == null)
            {
                if (_numericUpDown.Controls.Count >= 2)
                {
                    _subclassEdit = new SubclassEdit(_numericUpDown.Controls[1].Handle, this, _numericUpDown);
                    _subclassEdit.TrackMouseEnter += new EventHandler(OnNumericUpDownMouseChange);
                    _subclassEdit.TrackMouseLeave += new EventHandler(OnNumericUpDownMouseChange);
                }
            }
        }

        private void SubclassButtonsControl()
        {
            // If the buttons have been recreated, then release our current subclassing
            if (_subclassButtons != null)
            {
                if (_numericUpDown.Controls.Count >= 1)
                {
                    if (_subclassButtons.Handle != _numericUpDown.Controls[0].Handle)
                    {
                        _subclassButtons.TrackMouseEnter -= new EventHandler(OnNumericUpDownMouseChange);
                        _subclassButtons.TrackMouseLeave -= new EventHandler(OnNumericUpDownMouseChange);
                        _subclassButtons.ReleaseHandle();
                        _subclassButtons = null;
                    }
                }
            }

            if (_subclassButtons == null)
            {
                if (_numericUpDown.Controls.Count >= 1)
                {
                    _subclassButtons = new SubclassButtons(_numericUpDown.Controls[0].Handle, this, _numericUpDown);
                    _subclassButtons.TrackMouseEnter += new EventHandler(OnNumericUpDownMouseChange);
                    _subclassButtons.TrackMouseLeave += new EventHandler(OnNumericUpDownMouseChange);
                }
            }
        }

        private void UpdateChildEditControl()
        {
            SubclassEditControl();
        }

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

        private void OnNumericUpDownTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void OnNumericUpDownValueChanged(object sender, EventArgs e)
        {
            OnValueChanged(e);
        }

        private void OnNumericUpDownGotFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            InvalidateChildren();
            base.OnGotFocus(e);
        }

        private void OnNumericUpDownLostFocus(object sender, EventArgs e)
        {
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
            InvalidateChildren();
            base.OnLostFocus(e);
        }

        private void OnNumericUpDownKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnNumericUpDownKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnNumericUpDownKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnNumericUpDownPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnNumericUpDownValidated(object sender, EventArgs e)
        {
            OnValidated(e);
        }

        private void OnNumericUpDownValidating(object sender, CancelEventArgs e)
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

        private void OnNumericUpDownMouseChange(object sender, EventArgs e)
        {
            // Find new tracking mouse change state
            bool tracking = _numericUpDown.MouseOver ||
                            ((_subclassEdit != null) && _subclassEdit.MouseOver) ||
                            ((_subclassButtons != null) && _subclassButtons.MouseOver);

            // Change in tracking state?
            if (tracking != _trackingMouseEnter)
            {
                _trackingMouseEnter = tracking;
                InvalidateChildren();

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
