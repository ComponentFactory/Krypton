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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Display a separator with generated events to operation.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonSeparator), "ToolboxBitmaps.KryptonSeparator.bmp")]
    [DefaultEvent("SplitterMoved")]
    [DefaultProperty("Orientation")]
    [DesignerCategory("code")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonSeparatorDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [Description("Display a separator generated events to operation.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonSeparator : VisualControl,
                                    ISeparatorSource
    {
        #region Instance Fields
        private SeparatorStyle _style;
        private ViewDrawDocker _drawDocker;
        private ViewDrawSeparator _drawSeparator;
        private SeparatorController _separatorController;
        private PaletteSplitContainerRedirect _stateCommon;
        private PaletteSplitContainer _stateDisabled;
        private PaletteSplitContainer _stateNormal;
        private PaletteSeparatorPadding _stateTracking;
        private PaletteSeparatorPadding _statePressed;
        private Orientation _orientation;
        private Timer _redrawTimer;
        private Point _designLastPt;
        private int _splitterWidth;
        private int _splitterIncrements;
        private bool _allowMove;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the AutoSize property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler AutoSizeChanged;

        /// <summary>
        /// Occurs when the value of the BackgroundImage property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler BackgroundImageChanged;

        /// <summary>
        /// Occurs when the value of the BackgroundImageLayout property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler BackgroundImageLayoutChanged;

        /// <summary>
        /// Occurs when the value of the ControlAdded property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event ControlEventHandler ControlAdded;

        /// <summary>
        /// Occurs when the value of the ControlRemoved property changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event ControlEventHandler ControlRemoved;

        /// <summary>
        /// Occurs when the separator is about to be moved and requests the rectangle of allowed movement.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator is about to be moved and requests the rectangle of allowed movement.")]
        public event EventHandler<SplitterMoveRectMenuArgs> SplitterMoveRect;
        
        /// <summary>
        /// Occurs when the separator move finishes and a move has occured.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator move finishes and a move has occured.")]
        public event SplitterEventHandler SplitterMoved;

        /// <summary>
        /// Occurs when the separator move finishes and a move has not occured.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator move finishes and a move has not occured.")]
        public event EventHandler SplitterNotMoved;

        /// <summary>
        /// Occurs when the separator is currently in the process of moving.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator is currently in the process of moving.")]
        public event SplitterCancelEventHandler SplitterMoving;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonSeparator class.
        /// </summary>
        public KryptonSeparator()
        {
            // The label cannot take the focus
            SetStyle(ControlStyles.Selectable, false);

            // Create the palette storage
            _stateCommon = new PaletteSplitContainerRedirect(Redirector, PaletteBackStyle.PanelClient,
                                                             PaletteBorderStyle.ControlClient, PaletteBackStyle.SeparatorHighProfile,
                                                             PaletteBorderStyle.SeparatorHighProfile, NeedPaintDelegate);

            // Never draw the border around the background
            _stateCommon.BorderRedirect.OverrideBorderToFalse = true;

            _stateDisabled = new PaletteSplitContainer(_stateCommon, _stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _stateNormal = new PaletteSplitContainer(_stateCommon, _stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _stateTracking = new PaletteSeparatorPadding(_stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _statePressed = new PaletteSeparatorPadding(_stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);

            // Our view contains just a simple canvas that covers entire client area and a separator view
            _drawSeparator = new ViewDrawSeparator(_stateDisabled.Separator, _stateNormal.Separator, _stateTracking, _statePressed,
                                                   _stateDisabled.Separator, _stateNormal.Separator, _stateTracking, _statePressed,
                                                    PaletteMetricPadding.SeparatorPaddingLowProfile, Orientation.Vertical);

            // Get the separator to fill the entire client area
            _drawDocker = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border);
            _drawDocker.IgnoreAllBorderAndPadding = true;
            _drawDocker.Add(_drawSeparator, ViewDockStyle.Fill);

            // Create a separator controller to handle separator style behaviour
            _separatorController = new SeparatorController(this, _drawSeparator, true, true, NeedPaintDelegate);

            // Assign the controller to the view element to treat as a separator
            _drawSeparator.MouseController = _separatorController;
            _drawSeparator.KeyController = _separatorController;
            _drawSeparator.SourceController = _separatorController;

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);

            // Use timer to redraw after windows messages are processed
            _redrawTimer = new Timer();
            _redrawTimer.Interval = 1;
            _redrawTimer.Tick += new EventHandler(OnRedrawTick);

            // Set other internal starting values
            _style = SeparatorStyle.HighProfile;
            _orientation = Orientation.Vertical;
            _allowMove = true;
            _splitterIncrements = 1;
            _splitterWidth = 5;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_redrawTimer != null)
                {
                    _redrawTimer.Stop();
                    _redrawTimer.Dispose();
                    _redrawTimer = null;
                }

                // Must remember to dispose of the separator, as it can create a 
                // message filter that would prevent it from being garbage collected
                _separatorController.Dispose();
            }
            
            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
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
        /// Gets and sets the separator background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Separator background style.")]
        public PaletteBackStyle ContainerBackStyle
        {
            get { return _stateCommon.BackStyle; }

            set
            {
                if (_stateCommon.BackStyle != value)
                {
                    _stateCommon.BackStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeContainerBackStyle()
        {
            return (ContainerBackStyle != PaletteBackStyle.PanelClient);
        }

        private void ResetContainerBackStyle()
        {
            ContainerBackStyle = PaletteBackStyle.PanelClient;
        }

        /// <summary>
        /// Gets and sets the separator style.
        /// </summary>
        [Category("Visuals")]
        [Description("Separator style.")]
        public SeparatorStyle SeparatorStyle
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    _stateCommon.Separator.SetStyles(_style);
                    _drawSeparator.MetricPadding = CommonHelper.SeparatorStyleToMetricPadding(_style);
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeSeparatorStyle()
        {
            return (SeparatorStyle != SeparatorStyle.HighProfile);
        }

        private void ResetSeparatorStyle()
        {
            SeparatorStyle = SeparatorStyle.HighProfile;
        }

        /// <summary>
        /// Gets access to the common separator appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common separator appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainerRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled separator appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainer StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal separator appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainer StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the hot tracking separator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed separator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets and sets the thickness of the splitter.
        /// </summary>
        [Category("Layout")]
        [Description("Determines the thickness of the splitter.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "5")]
        public int SplitterWidth
        {
            get { return _splitterWidth; }

            set
            {
                // Only interested in changes of value
                if (_splitterWidth != value)
                {
                    // Cannot assign a value of less than zero
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("SplitterWidth", "Value cannot be less than zero");

                    // Use new width of the splitter area
                    _splitterWidth = value;

                    UpdateSize();
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the increment used for moving.
        /// </summary>
        [Category("Layout")]
        [Description("Determines the increment used for moving.")]
        [DefaultValue(typeof(int), "1")]
        public int SplitterIncrements
        {
            get { return _splitterIncrements; }
            set { _splitterIncrements = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating the horizontal or vertical orientation of the separator.
        /// </summary>
        [Category("Layout")]
        [Description("Determines if the separator is vertical or horizontal.")]
        [Localizable(true)]
        [DefaultValue(typeof(Orientation), "Vertical")]
        public Orientation Orientation
        {
            get { return _orientation; }

            set
            {
                // Only interested in changes of value
                if (_orientation != value)
                {
                    // Use the new orientation
                    _orientation = value;

                    // Must update the visual drawing with new orientation as well
                    _drawSeparator.Orientation = _orientation;

                    UpdateSize();
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the separator is allowed to notify a move.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the separator is allowed to notify a move.")]
        [DefaultValue(true)]
        public bool AllowMove
        {
            get { return _allowMove; }
            set { _allowMove = value; }
        }

        /// <summary>
        /// Gets and sets the drawing of the movement indicator.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines if the move indicator is drawn when moving the separator.")]
        [DefaultValue(true)]
        public bool DrawMoveIndicator
        {
            get { return _separatorController.DrawMoveIndicator; }
            set { _separatorController.DrawMoveIndicator = value; }
        }
        #endregion

        #region Public ISeparatorSource
        /// <summary>
        /// Gets the top level control of the source.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control SeparatorControl
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the orientation of the separator.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Orientation SeparatorOrientation
        {
            get { return Orientation; }
        }

        /// <summary>
        /// Can the separator be moved by the user.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SeparatorCanMove
        {
            get { return AllowMove; }
        }

        /// <summary>
        /// Gets the amount the splitter can be incremented.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SeparatorIncrements
        {
            get { return SplitterIncrements; }
        }

        /// <summary>
        /// Gets the box representing the minimum and maximum allowed splitter movement.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle SeparatorMoveBox
        {
            get
            {
                // Fire event to recover the rectangle of allowed separator movement
                SplitterMoveRectMenuArgs args = new SplitterMoveRectMenuArgs(Rectangle.Empty);
                OnSplitterMoveRect(args);

                if (Orientation == Orientation.Horizontal)
                    return new Rectangle(0, args.MoveRect.Y, 0, args.MoveRect.Height);
                else
                    return new Rectangle(args.MoveRect.X, 0, args.MoveRect.Width, 0);
            }
        }

        /// <summary>
        /// Indicates the separator is moving.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        /// <returns>True if movement should be cancelled; otherwise false.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SeparatorMoving(Point mouse, Point splitter)
        {
            // Fire the event that indicates the splitter is being moved
            SplitterCancelEventArgs e = new SplitterCancelEventArgs(mouse.X, mouse.Y, splitter.X, splitter.Y);
            OnSplitterMoving(e);

            // Tell caller if the movement should be cancelled or not
            return e.Cancel;
        }

        /// <summary>
        /// Indicates the separator has finished and been moved.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SeparatorMoved(Point mouse, Point splitter)
        {
            // Fire the event that indicates the splitter has finished being moved
            SplitterEventArgs e = new SplitterEventArgs(mouse.X, mouse.Y, splitter.X, splitter.Y);
            OnSplitterMoved(e);

            if (_redrawTimer != null)
                _redrawTimer.Start();
        }

        /// <summary>
        /// Indicates the separator has not been moved.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SeparatorNotMoved()
        {
            // Fire the event that indicates the splitter has finished but not been moved
            OnSplitterNotMoved(EventArgs.Empty);

            if (_redrawTimer != null)
                _redrawTimer.Start();
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Gets or sets padding within the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the SplitterMoveRect event.
        /// </summary>
        /// <param name="e">A SplitterMoveRectMenuArgs containing the event data.</param>
        protected virtual void OnSplitterMoveRect(SplitterMoveRectMenuArgs e)
        {
            if (SplitterMoveRect != null)
                SplitterMoveRect(this, e);
        }

        /// <summary>
        /// Raises the SplitterMoved event.
        /// </summary>
        /// <param name="e">A SplitterEventArgs containing the event data.</param>
        protected virtual void OnSplitterMoved(SplitterEventArgs e)
        {
            if (SplitterMoved != null)
                SplitterMoved(this, e);
        }

        /// <summary>
        /// Raises the SplitterNotMoved event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSplitterNotMoved(EventArgs e)
        {
            if (SplitterNotMoved != null)
                SplitterNotMoved(this, e);
        }

        /// <summary>
        /// Raises the SplitterMoving event.
        /// </summary>
        /// <param name="e">A SplitterEventArgs containing the event data.</param>
        protected virtual void OnSplitterMoving(SplitterCancelEventArgs e)
        {
            if (SplitterMoving != null)
                SplitterMoving(this, e);
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(5, 5); }
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Raises the DockChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnDockChanged(EventArgs e)
        {
            UpdateSize();
            base.OnDockChanged(e);
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Push correct palettes into the view
            if (Enabled)
                _drawDocker.SetPalettes(_stateNormal.Back, _stateNormal.Border);
            else
                _drawDocker.SetPalettes(_stateDisabled.Back, _stateDisabled.Border);

            _drawDocker.Enabled = Enabled;
            _drawSeparator.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }
        #endregion

        #region Protected Overrides (Events)
        /// <summary>
        /// Raises the AutoSizeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnAutoSizeChanged(EventArgs e)
        {
            if (AutoSizeChanged != null)
                AutoSizeChanged(this, e);
        }

        /// <summary>
        /// Raises the BackgroundImageChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            if (BackgroundImageChanged != null)
                BackgroundImageChanged(this, e);
        }

        /// <summary>
        /// Raises the BackgroundImageLayoutChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnBackgroundImageLayoutChanged(EventArgs e)
        {
            if (BackgroundImageLayoutChanged != null)
                BackgroundImageLayoutChanged(this, e);
        }

        /// <summary>
        /// Raises the ControlAdded event.
        /// </summary>
        /// <param name="e">An ControlEventArgs containing the event data.</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (ControlAdded != null)
                ControlAdded(this, e);
        }

        /// <summary>
        /// Raises the ControlRemoved event.
        /// </summary>
        /// <param name="e">An ControlEventArgs containing the event data.</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (ControlRemoved != null)
                ControlRemoved(this, e);
        }
        #endregion

        #region Internal (Design Time Support)
        internal Cursor DesignGetHitTest(Point pt)
        {
            // Is the cursor inside the splitter area or if currently moving the splitter
            if (_drawSeparator.ClientRectangle.Contains(pt) || _separatorController.IsMoving)
            {
                // Cursor depends on orientation direction
                if (Orientation == Orientation.Vertical)
                    return Cursors.VSplit;
                else
                    return Cursors.HSplit;
            }

            return null;
        }

        internal void DesignMouseEnter()
        {
            // Pass message directly onto the separator controller
            _separatorController.MouseEnter(this);
        }

        internal bool DesignMouseDown(Point pt, MouseButtons button)
        {
            // Remember last point encountered
            _designLastPt = pt;

            // Pass message directly onto the separator controller
            return _separatorController.MouseDown(this, pt, button);
        }

        internal void DesignMouseMove(Point pt)
        {
            // Remember last point encountered
            _designLastPt = pt;

            // Pass message directly onto the separator controller
            _separatorController.MouseMove(this, pt);
        }

        internal void DesignMouseUp(MouseButtons button)
        {
            // Pass message directly onto the separator controller
            _separatorController.MouseUp(this, _designLastPt, button);
        }

        internal void DesignMouseLeave()
        {
            // Pass message directly onto the separator controller
            _separatorController.MouseLeave(this, null);
        }

        internal void DesignAbortMoving()
        {
            // Pass message directly onto the separator controller
            _separatorController.AbortMoving();
        }
        #endregion

        #region Private
        private void UpdateSize()
        {
            if ((Dock != DockStyle.None) && (Dock != DockStyle.Fill))
            {
                if (Orientation == Orientation.Vertical)
                    Size = new Size(Width, _splitterWidth);
                else
                    Size = new Size(_splitterWidth, Height);
            }
        }

        private void OnRedrawTick(object sender, EventArgs e)
        {
            if (_redrawTimer != null)
                _redrawTimer.Stop();
            
            Refresh();
        }
        #endregion
    }
}
