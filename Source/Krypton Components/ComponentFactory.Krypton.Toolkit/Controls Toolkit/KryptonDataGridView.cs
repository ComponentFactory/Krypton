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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Display text and images with the styling features of the Krypton Toolkit
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonDataGridView), "ToolboxBitmaps.KryptonDataGridView.bmp")]
    [DesignerCategory("code")]
    [Description("Display rows and columns of data if a grid you can customize.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonDataGridView : DataGridView
    {
        #region Type Declaractions
        private class ColumnHeaderCache : Dictionary<int, bool> {};
        private class RowHeaderCache : Dictionary<int, Rectangle> { };
        #endregion

        #region Classes
        private class ToolTipContent : IContentValues
        {
            #region Instance Fields
            private string _toolTipText;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the ToolTipContent class.
            /// </summary>
            /// <param name="toolTipText">Text to show as a tooltip.</param>
            public ToolTipContent(string toolTipText)
            {
                _toolTipText = toolTipText;
            }
            #endregion

            #region IContentValues
            /// <summary>
            /// Gets the content image.
            /// </summary>
            /// <param name="state">The state for which the image is needed.</param>
            /// <returns>Image value.</returns>
            public Image GetImage(PaletteState state)
            {
                return null;
            }

            /// <summary>
            /// Gets the image color that should be transparent.
            /// </summary>
            /// <param name="state">The state for which the image is needed.</param>
            /// <returns>Color value.</returns>
            public Color GetImageTransparentColor(PaletteState state)
            {
                return Color.Empty;
            }

            /// <summary>
            /// Gets the content short text.
            /// </summary>
            /// <returns>String value.</returns>
            public string GetShortText()
            {
                return _toolTipText;
            }

            /// <summary>
            /// Gets the content long text.
            /// </summary>
            /// <returns>String value.</returns>
            public string GetLongText()
            {
                return string.Empty;
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly Point _nullCell = new Point(-2, -2);

        // Cached access to private parent values
        private static PropertyInfo _piRTL;
        private static PropertyInfo _piCML;
        private static PropertyInfo _piCG;
        private static MethodInfo _miPTB;
        private static MethodInfo _miGCI;
        private static MethodInfo _miGTTT;
        private static MethodInfo _miGET;
        private static MethodInfo _miATT;
        private static MethodInfo _miGPW;
        private static MethodInfo _miGPH;
        private static FieldInfo _fiLayout;
        private static FieldInfo _fiColumnHeaders;
        private static FieldInfo _fiRowHeaders;
        private static FieldInfo _fiColumnHeadersVisible;
        private static FieldInfo _fiRowHeadersVisible;
        #endregion

        #region Instance Fields
        // Standard Krypton layout/rendering fields
        private bool _refresh;
        private bool _refreshAll;
        private bool _layoutDirty;
        private bool _paintTransparent;
        private bool _evalTransparent;
        private Size _lastLayoutSize;
        private IPalette _localPalette;
        private IPalette _palette;
        private IRenderer _renderer;
        private PaletteMode _paletteMode;
        private ViewDrawPanel _drawPanel;
        private ViewManager _viewManager;
        private NeedPaintHandler _needPaintDelegate;
        private SimpleCall _refreshCall;

        // States and redirector
        private PaletteRedirect _redirector;
        private PaletteDataGridViewRedirect _stateCommon;
        private PaletteDataGridViewAll _stateDisabled;
        private PaletteDataGridViewAll _stateNormal;
        private PaletteDataGridViewHeaders _stateTracking;
        private PaletteDataGridViewHeaders _statePressed;
        private PaletteDataGridViewCells _stateSelected;

        // Cached values for determining cell style overrides
        private DataGridViewStyles _gridSyles;
        private Font _columnFont;
        private Font _rowFont;
        private Font _dataCellFont;
        private Padding _columnPadding;
        private Padding _rowPadding;
        private Padding _dataCellPadding;
        private DataGridViewContentAlignment _columnAlign;
        private DataGridViewContentAlignment _rowAlign;
        private DataGridViewContentAlignment _dataCellAlign;
        private Color _columnBackColor, _rowBackColor, _dataCellBackColor;
        private Color _columnForeColor, _rowForeColor, _dataCellForeColor;
        private Color _columnSelBackColor, _rowSelBackColor, _dataCellSelBackColor;
        private Color _columnSelForeColor, _rowSelForeColor, _dataCellSelForeColor;

        // Implementation fields
        private ShortTextValue _shortTextValue;
        private VisualPopupToolTip _visualPopupToolTip;
        private PaletteBorderInheritForced _borderForced;
        private PaletteDataGridViewBackInherit _backInherit;
        private PaletteDataGridViewContentInherit _contentInherit;
        private ColumnHeaderCache _columnCache;
        private RowHeaderCache _rowCache;
        private Point _cellOver;
        private Point _cellDown;
        private Timer _showTimer;
        private bool _hideOuterBorders;
        private bool _showCellToolTips;
        private string _toolTipText;
        private byte _oldLocation;
        private DataGridViewCell _oldCell;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the palette changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the Palette property is changed.")]
        public event EventHandler PaletteChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridView class.
        /// </summary>
        public KryptonDataGridView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            // We need to allow a transparent background
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            // We need to repaint entire control whenever resized
			SetStyle(ControlStyles.ResizeRedraw, true);

            // Yes, we want to be drawn double buffered by default
            DoubleBuffered = true;

            SetupVisuals();
            SetupViewAndStates();
            SetupDefaults();
            SetupSyncCellStyles();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Must remove the timer so we can dispose ourself
                if (_showTimer != null)
                {
                    _showTimer.Stop();
                    _showTimer.Tick -= new EventHandler(OnTimerTick);
                    _showTimer.Dispose();
                    _showTimer = null;
                }

                // Must unhook from the palette paint event
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnNeedResyncPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                }

                // Unhook from global events
                KryptonManager.GlobalPaletteChanged -= new EventHandler(OnGlobalPaletteChanged);
                SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);

                // Dispose of view manager related resources
                ViewManager.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public New
        /// <summary>
        /// Gets or sets the background color of the DataGridView.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackgroundColor
        {
            get { return base.BackgroundColor; }
            set { base.BackgroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the border style for the DataGridView.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { /* Do nothing, we do not allow a border style change! */ }
        }

        /// <summary>
        /// Gets the cell border style for the DataGridView.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellBorderStyle CellBorderStyle
        {
            get { return base.CellBorderStyle; }
            set { base.CellBorderStyle = value; }
        }

        /// <summary>
        /// Gets the border style applied to the column headers.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle
        {
            get { return base.ColumnHeadersBorderStyle; }
            set { base.ColumnHeadersBorderStyle = value; }
        }

        /// <summary>
        /// Gets or sets the default column header style.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle ColumnHeadersDefaultCellStyle
        {
            get { return base.ColumnHeadersDefaultCellStyle; }
            set { base.ColumnHeadersDefaultCellStyle = value; }
        }

        /// <summary>
        /// Gets or sets the default cell style.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle DefaultCellStyle
        {
            get { return base.DefaultCellStyle; }
            set { base.DefaultCellStyle = value; }
        }

        /// <summary>
        /// Gets or sets the use of visual styles for headers.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool EnableHeadersVisualStyles
        {
            get { return base.EnableHeadersVisualStyles; }
            set { base.EnableHeadersVisualStyles = value; }
        }

        /// <summary>
        /// Gets or sets the color of the grid lines separating the cells of the DataGridView. 
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color GridColor
        {
            get { return base.GridColor; }
            set { base.GridColor = value; }
        }

        /// <summary>
        /// Gets or sets the border style of the row header cells.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewHeaderBorderStyle RowHeadersBorderStyle
        {
            get { return base.RowHeadersBorderStyle; }
            set { base.RowHeadersBorderStyle = value; }
        }

        /// <summary>
        /// Gets or sets the default row header style.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellStyle RowHeadersDefaultCellStyle
        {
            get { return base.RowHeadersDefaultCellStyle; }
            set { base.RowHeadersDefaultCellStyle = value; }
        }

        /// <summary>
        /// Indicates if tool tips are displayed when the mouse hovers over the cell.
        /// </summary>
        public new bool ShowCellToolTips
        {
            get { return _showCellToolTips; }
            set { _showCellToolTips = value; }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets a value determining if the outer borders of the grid cells are drawn.
        /// </summary>
        [Category("Visuals")]
        [Description("Determine if the outer borders of the grid cells are drawn.")]
        [DefaultValue(false)]
        public bool HideOuterBorders
        {
            get { return _hideOuterBorders; }

            set
            {
                if (value != _hideOuterBorders)
                {
                    _hideOuterBorders = value;
                    PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _paletteMode; }

            set
            {
                if (_paletteMode != value)
                {
                    // Action despends on new value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            // Use the new value
                            _paletteMode = value;

                            // Get a reference to the standard palette from its name
                            _localPalette = null;
                            SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                            // Must raise event to change palette in redirector
                            OnPaletteChanged(EventArgs.Empty);

                            // Need to layout again use new palette
                            PerformLayout();
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializePaletteMode()
        {
            return (PaletteMode != PaletteMode.Global);
        }

        /// <summary>
        /// Resets the PaletteMode property to its default value.
        /// </summary>
        public void ResetPaletteMode()
        {
            PaletteMode = PaletteMode.Global;
        }

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        [Category("Visuals")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette Palette
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _localPalette; }

            set
            {
                // Only interested in changes of value
                if (_localPalette != value)
                {
                    // Remember the starting palette
                    IPalette old = _localPalette;

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // No custom palette, so revert back to the global setting
                        _paletteMode = PaletteMode.Global;

                        // Get the appropriate palette for the global mode
                        _localPalette = null;
                        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                    }
                    else
                    {
                        // No longer using a standard palette
                        _localPalette = value;
                        _paletteMode = PaletteMode.Custom;
                    }

                    // If real change has occured
                    if (old != _localPalette)
                    {
                        // Raise the change event
                        OnPaletteChanged(EventArgs.Empty);

                        // Need to layout again use new palette
                        PerformLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Palette property to its default value.
        /// </summary>
        public void ResetPalette()
        {
            PaletteMode = PaletteMode.Global;
        }

        /// <summary>
        /// Gets access to the current renderer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer Renderer
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _renderer; }
        }

        /// <summary>
        /// Gets access to the common data grid view appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common data grid view appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled data grid view appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled data grid view appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewAll StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal data grid view appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal data grid view appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewAll StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking data grid view appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking data grid view appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewHeaders StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed data grid view appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed data grid view appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewHeaders StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the selected data grid view appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining selected data grid view appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewCells StateSelected
        {
            get { return _stateSelected; }
        }

        private bool ShouldSerializeStateSelected()
        {
            return !_stateSelected.IsDefault;
        }

        /// <summary>
        /// Gets access to the grid styles.
        /// </summary>
        [Category("Visuals")]
        [Description("Set of grid styles.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewStyles GridStyles
        {
            get { return _gridSyles; }
        }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Recovers the back/border/content palettes to use for drawing the provided cell.
        /// </summary>
        /// <param name="state">State of the cell.</param>
        /// <param name="rowIndex">Row index of cell (-1 for row headers).</param>
        /// <param name="columnIndex">Column index of cell (-1 for cell headers).</param>
        /// <param name="paletteBack">IPaletteBack to be used for cell drawing.</param>
        /// <param name="paletteBorder">IPaletteBorder to be used for cell drawing.</param>
        /// <param name="paletteContent">IPaletteContent to be used for cell drawing.</param>
        /// <returns></returns>
        public virtual PaletteState GetCellTriple(DataGridViewElementStates state,
                                                  int rowIndex,
                                                  int columnIndex,
                                                  out IPaletteBack paletteBack,
                                                  out IPaletteBorder paletteBorder,
                                                  out IPaletteContent paletteContent)
        {
            PaletteState retState;

            // If control is disabled, then draw cell as disabled
            if (!Enabled)
                retState = PaletteState.Disabled;
            else
            {
                retState = PaletteState.Normal;

                // If the cell is selected, then use the checked state
                if ((state & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                    retState = PaletteState.CheckedNormal;
                else
                {
                    // A data cell cannot become tracking or pressed
                    if ((rowIndex < 0) || (columnIndex < 0))
                    {
                        Point cellIndex = new Point(columnIndex, rowIndex);

                        // If the user has pressed down on this cell
                        if (cellIndex.Equals(_cellDown))
                        {
                            // ..and the mouse is still over the cell
                            if (cellIndex.Equals(_cellOver))
                            {
                                // ...then displayed as pressed
                                retState = PaletteState.Pressed;
                            }
                        }
                        else
                        {
                            // Cell not pressed, but if mouse over the cell anyway
                            if (cellIndex.Equals(_cellOver))
                                retState = PaletteState.Tracking;
                        }
                    }
                }
            }

            // Is this a data cell?
            if ((rowIndex >= 0) && (columnIndex >= 0))
            {
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.DataCell.Back;
                        paletteBorder = StateNormal.DataCell.Border;
                        paletteContent = StateNormal.DataCell.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.DataCell.Back;
                        paletteBorder = StateDisabled.DataCell.Border;
                        paletteContent = StateDisabled.DataCell.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.DataCell.Back;
                        paletteBorder = StateSelected.DataCell.Border;
                        paletteContent = StateSelected.DataCell.Content;
                        break;
                }
            }
            else if (rowIndex < 0)
            {
                // Negative row index means it is a header cell
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.HeaderColumn.Back;
                        paletteBorder = StateNormal.HeaderColumn.Border;
                        paletteContent = StateNormal.HeaderColumn.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.HeaderColumn.Back;
                        paletteBorder = StateDisabled.HeaderColumn.Border;
                        paletteContent = StateDisabled.HeaderColumn.Content;
                        break;
                    case PaletteState.Tracking:
                        paletteBack = StateTracking.HeaderColumn.Back;
                        paletteBorder = StateTracking.HeaderColumn.Border;
                        paletteContent = StateTracking.HeaderColumn.Content;
                        break;
                    case PaletteState.Pressed:
                        paletteBack = StatePressed.HeaderColumn.Back;
                        paletteBorder = StatePressed.HeaderColumn.Border;
                        paletteContent = StatePressed.HeaderColumn.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.HeaderColumn.Back;
                        paletteBorder = StateSelected.HeaderColumn.Border;
                        paletteContent = StateSelected.HeaderColumn.Content;
                        break;
                }
            }
            else
            {
                // Negative column index means it is a row cell
                switch (retState)
                {
                    default:
                    case PaletteState.Normal:
                        paletteBack = StateNormal.HeaderRow.Back;
                        paletteBorder = StateNormal.HeaderRow.Border;
                        paletteContent = StateNormal.HeaderRow.Content;
                        break;
                    case PaletteState.Disabled:
                        paletteBack = StateDisabled.HeaderRow.Back;
                        paletteBorder = StateDisabled.HeaderRow.Border;
                        paletteContent = StateDisabled.HeaderRow.Content;
                        break;
                    case PaletteState.Tracking:
                        paletteBack = StateTracking.HeaderRow.Back;
                        paletteBorder = StateTracking.HeaderRow.Border;
                        paletteContent = StateTracking.HeaderRow.Content;
                        break;
                    case PaletteState.Pressed:
                        paletteBack = StatePressed.HeaderRow.Back;
                        paletteBorder = StatePressed.HeaderRow.Border;
                        paletteContent = StatePressed.HeaderRow.Content;
                        break;
                    case PaletteState.CheckedNormal:
                        paletteBack = StateSelected.HeaderRow.Back;
                        paletteBorder = StateSelected.HeaderRow.Border;
                        paletteContent = StateSelected.HeaderRow.Content;
                        break;
                }
            }

            return retState;
        }

        /// <summary>
        /// Gets the ViewManager instance.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ViewManager GetViewManager()
        {
            return _viewManager;
        }

        /// <summary>
        /// Gets the resolved palette to actually use when drawing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IPalette GetResolvedPalette()
        {
            return _palette;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets and sets the ViewManager instance.
        /// </summary>
        protected ViewManager ViewManager
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewManager; }
            set { _viewManager = value; }
        }

        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _needPaintDelegate; }
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected void OnNeedResyncPaint(object sender, NeedLayoutEventArgs e)
        {
            // Ensure the current cell style values are in sync with the new 
            // palette setting and any state overrides that are defined
            SyncCellStylesWithPalette();

            // Continue with usual painting logic
            OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Change in setting means we need to evaluate transparent painting
            _evalTransparent = true;

            // If required, layout the control
            if (e.NeedLayout)
                _layoutDirty = true;

            if (IsHandleCreated && (!_refreshAll || !e.InvalidRect.IsEmpty))
            {
                // Always request the repaint immediately
                if (e.InvalidRect.IsEmpty)
                {
                    _refreshAll = true;
                    Invalidate();
                }
                else
                    Invalidate(e.InvalidRect);

                // Do we need to use an Invoke to force repaint?
                if (!_refresh && EvalInvokePaint)
                    BeginInvoke(_refreshCall);

                // A refresh is outstanding
                _refresh = true;
            }
        }

        /// <summary>
        /// Gets a value indicating if transparent paint is needed
        /// </summary>
        protected bool NeedTransparentPaint
        {
            get
            {
                // Do we need to evaluate the need for a tranparent paint
                if (_evalTransparent)
                {
                    _paintTransparent = EvalTransparentPaint();

                    // Answer is cached until paint values are changed
                    _evalTransparent = false;
                }

                return _paintTransparent;
            }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnPaletteChanged(EventArgs e)
        {
            // Update the redirector with latest palette
            Redirector.Target = _palette;

            // A new palette source means we need to layout and redraw
            OnNeedPaint(Palette, new NeedLayoutEventArgs(true));

            if (PaletteChanged != null)
                PaletteChanged(this, e);
        }

        /// <summary>
        /// Work out if this control needs to paint transparent areas.
        /// </summary>
        /// <returns>True if paint required; otherwise false.</returns>
        protected virtual bool EvalTransparentPaint()
        {
            // Do we have a manager to use for asking about painting?
            if (ViewManager != null)
            {
                // Ask the view if it needs to paint transparent areas
                return ViewManager.EvalTransparentPaint(_renderer);
            }
            else
            {
                // If there is no view then do not transparent paint
                return false;
            }
        }

        /// <summary>
        /// Work out if this control needs to use Invoke to force a repaint.
        /// </summary>
        protected virtual bool EvalInvokePaint
        {
            get
            {
                // By default the paint can occur safely via a simple Invalidate() call,
                // but some controls might need to override this the entire client area can
                // be covered by child controls and so Invalidate() becomes redundant and the
                // control is never layed out.
                return false;
            }
        }

        /// <summary>
        /// Gets the control reference that is the parent for transparent drawing.
        /// </summary>
        protected virtual Control TransparentParent
        {
            get { return Parent; }
        }

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnButtonSpecChanged(object sender, EventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
        }
        #endregion

        #region Protected Override
        /// <summary>
        /// Raises the PaintBackground event.  
        /// </summary>
        /// <param name="pevent">An PaintEventArgs that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Do nothing
        }

        /// <summary>
        /// Raises the CellMouseEnter event. 
        /// </summary>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            _cellOver = new Point(e.ColumnIndex, e.RowIndex);
            base.OnCellMouseEnter(e);
        }

        /// <summary>
        /// Raises the CellMouseMove event. 
        /// </summary>
        /// <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
        protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        {
            // Cache mouse location before calling base class
            DataGridViewCell cell = GetCellInternal(e.ColumnIndex, e.RowIndex);

            byte oldLocation = CurrentMouseLocation(cell);
            if ((cell is DataGridViewRowHeaderCell) && (_oldCell == cell))
                oldLocation = _oldLocation;

            base.OnCellMouseMove(e);
            
            byte newLocation = UpdateLocationForRowErrors(e, cell, CurrentMouseLocation(cell));
            if (cell is DataGridViewRowHeaderCell)
            {
                _oldLocation = newLocation;
                _oldCell = cell;
            }
            else
                _oldCell = null;

            // Use the cached value from before the call to base class
            switch(oldLocation)
            {
                case 0:
                    if (newLocation != 1)
                        CellErrorAreaMouseEnterInternal(cell);

                    CellDataAreaMouseEnterInternal(cell);
                    break;
                case 1:
                    if (newLocation == 2)
                    {
                        CellAreaMouseLeaveInternal();
                        CellErrorAreaMouseEnterInternal(cell);
                    }
                    break;
                case 2:
                    if (newLocation == 1)
                    {
                        CellAreaMouseLeaveInternal();
                        CellDataAreaMouseEnterInternal(cell);
                    }
                    break;
            }
        }

        /// <summary>
        /// Raises the CellMouseLeave event. 
        /// </summary>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            switch (CurrentMouseLocation(GetCellInternal(e.ColumnIndex, e.RowIndex)))
            {
                case 1:
                case 2:
                    CellAreaMouseLeaveInternal();
                    break;
            }

            _cellOver = _nullCell;
            base.OnCellMouseLeave(e);
        }

        /// <summary>
        /// Raises the CellMouseDown event. 
        /// </summary>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            _cellDown = new Point(e.ColumnIndex, e.RowIndex);

            // If mouse down over the columns or row headers then turn off double buffering,
            // because if the user is using the mouse to resize a header item then it will use
            // an XOR painting technique to draw the resizing bar and double buffering causes
            // the painting to fail.
            if ((_cellDown.X == -1) || (_cellDown.Y == -1))
                DoubleBuffered = false;

            base.OnCellMouseDown(e);
        }

        /// <summary>
        /// Raises the CellMouseUp event. 
        /// </summary>
        /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            _cellDown = _nullCell;

            // Put back double buffered if it was turned off in the OnCellMouseDown
            if (!DoubleBuffered)
                DoubleBuffered = true;

            base.OnCellMouseUp(e);
        }

        /// <summary>
        /// Raises the EditingControlShowing event.
        /// </summary>
        /// <param name="e">A DataGridViewEditingControlShowingEventArgs that contains information about the editing control.</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            // Prevent a tooltip from showing while the editing control is showing
            CellAreaMouseLeaveInternal();
            base.OnEditingControlShowing(e);
        }

        /// <summary>
        /// Raises the CellPainting event.
        /// </summary>
        /// <param name="e">A DataGridViewCellPaintingEventArgs that contains the event data.</param>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            IPaletteBack paletteBack;
            IPaletteBorder paletteBorder;
            IPaletteContent paletteContent;

            // Get the palette and state values for this cell
            PaletteState state = GetCellTriple(e.State, 
                                               e.RowIndex, 
                                               e.ColumnIndex,
                                               out paletteBack, 
                                               out paletteBorder, 
                                               out paletteContent);

            try
            {
                // If the font we get from the base palette is a system font that is invalid this will throw exception
                int hContent = _contentInherit.GetContentShortTextFont(state).Height;
            }
            catch
            {
                // Get the latest font from the base palette that will have been updated to be valid
                SyncCellStylesWithPalette();
            }

            bool rtl = RightToLeftInternal;

            // Use an offscreen bitmap to draw onto before blitting it to the screen
            Rectangle tempCellBounds = new Rectangle(0, 0, e.CellBounds.Width, e.CellBounds.Height);
            using (Bitmap tempBitmap = new Bitmap(e.CellBounds.Width, e.CellBounds.Height, e.Graphics))
            {
                using (Graphics tempG = Graphics.FromImage(tempBitmap))
                {
                    using (RenderContext renderContext = new RenderContext(this, tempG, tempCellBounds, _renderer))
                    {
                        // Force the border to have a specificed maximum border edge
                        _borderForced.SetInherit(paletteBorder);
                        _borderForced.MaxBorderEdges = GetCellMaxBorderEdges(e.CellBounds, e.ColumnIndex, e.RowIndex);

                        // Get the padding used to decide how to draw the background
                        Padding borderPadding = _renderer.RenderStandardBorder.GetBorderRawPadding(_borderForced, state, VisualOrientation.Top);

                        // Get the border path used to limit drawing of the background
                        GraphicsPath borderPath = _renderer.RenderStandardBorder.GetBackPath(renderContext, tempCellBounds, _borderForced, VisualOrientation.Top, state);

                        // Reduce background drawing rect by the raw padding
                        Rectangle tempCellBackBounds = CommonHelper.ApplyPadding(VisualOrientation.Top, tempCellBounds, borderPadding);

                        // Update the back interceptor class
                        _backInherit.SetInherit(paletteBack, e.CellStyle);

                        IDisposable unused =  _renderer.RenderStandardBack.DrawBack(renderContext, tempCellBackBounds, borderPath, _backInherit, VisualOrientation.Top, state, null);

                        // We never save the memento for reuse later
                        if (unused != null)
                        {
                            unused.Dispose();
                            unused = null;
                        }

                        _renderer.RenderStandardBorder.DrawBorder(renderContext, tempCellBounds, _borderForced, VisualOrientation.Top, state);

                        // Must remember to release resources!
                        borderPath.Dispose();

                        // If this is a column header cell
                        if ((e.RowIndex == -1) && (e.ColumnIndex >= 0))
                        {
                            // If this column needs a sort glyph drawn
                            if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection != SortOrder.None)
                            {
                                // Draw the sort glyph and update the remainder cell bounds left over
                                tempCellBounds = _renderer.RenderGlyph.DrawGridSortGlyph(renderContext, Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection, tempCellBounds, paletteContent, state, rtl);
                            }
                        }
                        else
                        {
                            // If this is a row header cell
                            if ((e.RowIndex >= 0) && (e.ColumnIndex == -1))
                            {
                                // By default there is no glyph needed for the row
                                GridRowGlyph glpyh = GridRowGlyph.None;

                                // Find the correct glyph that should be drawn
                                if (CurrentCellAddress.Y == e.RowIndex)
                                {
                                    if (VirtualMode)
                                    {
                                        if (IsCurrentRowDirty && ShowEditingIcon)
                                            glpyh = GridRowGlyph.Pencil;
                                        else if (NewRowIndex == e.RowIndex)
                                            glpyh = GridRowGlyph.ArrowStar;
                                        else
                                            glpyh = GridRowGlyph.Arrow;
                                    }
                                    else if (IsCurrentCellDirty && ShowEditingIcon)
                                        glpyh = GridRowGlyph.Pencil;
                                    else if (NewRowIndex == e.RowIndex)
                                        glpyh = GridRowGlyph.ArrowStar;
                                    else
                                        glpyh = GridRowGlyph.Arrow;
                                }
                                else if (NewRowIndex == e.RowIndex)
                                    glpyh = GridRowGlyph.Star;

                                // Do we need to draw an image?
                                if (glpyh != GridRowGlyph.None)
                                {
                                    // Draw the row glyph and update the remainder cell bounds left over
                                    tempCellBounds = _renderer.RenderGlyph.DrawGridRowGlyph(renderContext, glpyh, tempCellBounds, paletteContent, state, rtl);
                                }

                                // Is there an error icon associated with the row that needs showing
                                if (ShowRowErrors && !string.IsNullOrEmpty(Rows[e.RowIndex].ErrorText))
                                {
                                    // Draw error icon and update the remainder cell bounds left over
                                    Rectangle beforeCellBounds = tempCellBounds;
                                    tempCellBounds = _renderer.RenderGlyph.DrawGridErrorGlyph(renderContext, tempCellBounds, state, rtl);

                                    // Calculate the icon rectangle
                                    Rectangle iconBounds = new Rectangle(tempCellBounds.Right + 1, tempCellBounds.Top,
                                                                         beforeCellBounds.Width - tempCellBounds.Width,
                                                                         tempCellBounds.Height);

                                    // Cache the icon area
                                    if (_rowCache.ContainsKey(e.RowIndex))
                                        _rowCache[e.RowIndex] = iconBounds;
                                    else
                                        _rowCache.Add(e.RowIndex, iconBounds);
                                }
                                else
                                {
                                    // Remove any cache entry
                                    if (_rowCache.ContainsKey(e.RowIndex))
                                        _rowCache.Remove(e.RowIndex);
                                }
                            }
                            else
                            {
                                // Is this a data cell
                                if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                                {
                                    // Is there an error icon associated with the cell that needs showing
                                    if (ShowCellErrors && !string.IsNullOrEmpty(Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText))
                                    {
                                        // Draw error icon and update the remainder cell bounds left over
                                        tempCellBounds = _renderer.RenderGlyph.DrawGridErrorGlyph(renderContext, tempCellBounds, state, rtl);
                                    }
                                }
                            }
                        }

                        if (((e.PaintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground) ||
                            ((e.PaintParts & DataGridViewPaintParts.ContentBackground) == DataGridViewPaintParts.ContentBackground))
                        {
                            // Only consider drawing content for the data cells
                            if ((e.ColumnIndex >= 0) && (e.RowIndex >= 0))
                            {
                                // Blit the image onto the screen
                                e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);

                                // Let column do the painting
                                e.Paint(e.ClipBounds, e.PaintParts & (DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ContentBackground));
                            }
                            else
                            {
                                // Update the content interceptor class
                                _contentInherit.SetInherit(paletteContent, e.CellStyle);

                                // Is there any text to be displayed?
                                if (e.FormattedValue != null)
                                {
                                    // Use the display value of the header cell
                                    _shortTextValue.ShortText = e.FormattedValue.ToString();

                                    using (ViewLayoutContext layoutContext = new ViewLayoutContext(this, _renderer))
                                    {
                                        // If a column header cell...
                                        if ((e.RowIndex == -1) && (e.ColumnIndex != -1))
                                        {
                                            // Find size needed to show header text fully
                                            Size prefSize = _renderer.RenderStandardContent.GetContentPreferredSize(layoutContext, _contentInherit, _shortTextValue,
                                                                                                                    VisualOrientation.Top, state, false);

                                            bool contentsFit = (prefSize.Width <= tempCellBounds.Width) &&
                                                               (prefSize.Height <= tempCellBounds.Height);

                                            // Cache if the column cell can display all the content
                                            if (_columnCache.ContainsKey(e.ColumnIndex))
                                                _columnCache[e.ColumnIndex] = contentsFit;
                                            else
                                                _columnCache.Add(e.ColumnIndex, contentsFit);
                                        }

                                        // Find the correct layout for the header content
                                        using (IDisposable memento = _renderer.RenderStandardContent.LayoutContent(layoutContext, tempCellBounds,
                                                                                                                   _contentInherit, _shortTextValue,
                                                                                                                   VisualOrientation.Top, state, false))
                                        {
                                           // Perform actual drawing of the content
                                            _renderer.RenderStandardContent.DrawContent(renderContext, tempCellBounds,
                                                                                        _contentInherit, memento,
                                                                                        VisualOrientation.Top,
                                                                                        state, false, true);
                                        }
                                    }
                                }

                                // Blit the image onto the screen
                                e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);
                            }
                        }
                        else
                        {
                            // Blit the image onto the screen
                            e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);
                        }
                    }
                }
            }

            if ((e.PaintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
            {
                // Only consider drawing the focus rectangle if the control has focus wants to show the cues
                if (ShowFocusCues && Focused)
                {
                    // Only consider drawing focus for data cells
                    if ((e.ColumnIndex >= 0) && (e.RowIndex >= 0))
                    {
                        // Is the cell being drawn the current cell
                        if ((CurrentCellAddress.X == e.ColumnIndex) &&
                            (CurrentCellAddress.Y == e.RowIndex))
                        {
                            Rectangle focusCellBounds = e.CellBounds;
                            focusCellBounds.Width--;
                            focusCellBounds.Height--;

                            // If RTL then need to shift from left edge instead of right
                            if (rtl)
                                focusCellBounds.X++;

                            ControlPaint.DrawFocusRectangle(e.Graphics, focusCellBounds, Color.Empty, paletteContent.GetContentShortTextColor1(state));
                        }
                    }
                }
            }

            // Prevent base class from doing the standard drawing
            e.Handled = true;

            base.OnCellPainting(e);
        }

        /// <summary>
        /// Paints the background of the DataGridView.
        /// </summary>
        /// <param name="graphics">The Graphics used to paint the background.</param>
        /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be painted.</param>
        /// <param name="gridBounds">A Rectangle that represents the area in which cells are drawn.</param>
        protected override void PaintBackground(Graphics graphics, 
                                                Rectangle clipBounds, 
                                                Rectangle gridBounds)
        {
            if (!IsDisposed)
            {
                // Do we have a manager to use for painting?
                if (ViewManager != null)
                {
                    // If the layout is dirty, or the size of the control has changed 
                    // without a layout being performed, then perform a layout now
                    if (_layoutDirty && (!Size.Equals(_lastLayoutSize)))
                        ViewManagerLayout();

                    // Do not currently clip because it causes issues when the scroll bars are not showing and the user
                    // scrolls by using the keyboard or by sorting the columns. So it does cause a little flicker
                    //   using (Clipping clip = new Clipping(graphics, GetBackgroundClipRect(), true))
                    {
                        // Draw the background as transparent, by drawing parent
                        PaintTransparentBackground(graphics, clipBounds);

                        // Use the view manager to paint the view panel that fills the entire areas as the background
                        using (RenderContext context = new RenderContext(this, graphics, clipBounds, Renderer))
                            ViewManager.Paint(context);
                    }

                    // Request for a refresh has been serviced
                    _refresh = false;
                    _refreshAll = false;
                }
            }
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Push correct palettes into the view
            _drawPanel.SetPalettes(Enabled ? _stateNormal.Background : _stateDisabled.Background);

            // Update with latest enabled state
            _drawPanel.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            OnNeedResyncPaint(this, new NeedLayoutEventArgs(true));

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Get the view manager to relayout its elements
            ViewManagerLayout();

            // Let base class layout child controls
            base.OnLayout(levent);
        }
        #endregion

        #region Internal
        internal PaletteRedirect Redirector
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _redirector; }
        }

        internal void SyncStyles()
        {
            // Update with individual grid styles required
            _stateCommon.SetGridStyles(_gridSyles.StyleColumn,
                                       _gridSyles.StyleRow,
                                       _gridSyles.StyleDataCells);

            // Update the background separately
            _stateCommon.BackStyle = _gridSyles.StyleBackground;

            SyncCellStylesWithPalette();
        }

        internal bool RightToLeftInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piRTL == null)
                {
                    // Cache access to the internal get property 'RightToLeftInternal'
                    _piRTL = typeof(DataGridView).GetProperty("RightToLeftInternal", BindingFlags.Instance |
                                                                                     BindingFlags.NonPublic |
                                                                                     BindingFlags.GetField);

                }

                // Grab the internal calculated value of the right to left setting
                return (bool)_piRTL.GetValue(this, null);
            }
        }
        #endregion

        #region Implementation
        private void SetupVisuals()
        {
            // Setup the invoke used to refresh display
            _refreshCall = new SimpleCall(OnPerformRefresh);

            // Setup the need paint delegate
            _needPaintDelegate = new NeedPaintHandler(OnNeedResyncPaint);

            // Must layout before first draw attempt
            _layoutDirty = true;
            _evalTransparent = true;
            _lastLayoutSize = Size.Empty;

            // Set the palette to the defaults as specified by the manager
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            _paletteMode = PaletteMode.Global;

            // Create constant target for resolving palette delegates
            _redirector = new PaletteRedirect(_palette);

            // Hook into global palette changing events
            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
        }

        private void SetupViewAndStates()
        {
            // Create the state storgate objects
            _stateCommon = new PaletteDataGridViewRedirect(_redirector, NeedPaintDelegate);
            _stateDisabled = new PaletteDataGridViewAll(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteDataGridViewAll(_stateCommon, NeedPaintDelegate);
            _stateTracking = new PaletteDataGridViewHeaders(_stateCommon, NeedPaintDelegate);
            _statePressed = new PaletteDataGridViewHeaders(_stateCommon, NeedPaintDelegate);
            _stateSelected = new PaletteDataGridViewCells(_stateCommon, NeedPaintDelegate);

            // Our view contains just a simple canvas that is the background
            _drawPanel = new ViewDrawPanel(_stateNormal.Background);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawPanel);
        }

        private void SetupDefaults()
        {
            // Create internal objects
            _shortTextValue = new ShortTextValue();
            _borderForced = new PaletteBorderInheritForced(null);
            _backInherit = new PaletteDataGridViewBackInherit();
            _contentInherit = new PaletteDataGridViewContentInherit();
            _gridSyles = new DataGridViewStyles(this);
            _columnCache = new ColumnHeaderCache();
            _rowCache = new RowHeaderCache();
            _showTimer = new Timer();
            _showTimer.Interval = 500;
            _showTimer.Tick += new EventHandler(OnTimerTick);

            // Default internal fields
            _cellDown = _nullCell;
            _cellOver = _nullCell;
            _hideOuterBorders = false;
            _showCellToolTips = true;

            // Remove border from being drawn, border is drawn according to system settings
            // and we do not want that appearance. So set to 'None' and override the 
            // BorderStyle property so it cannot be set to anything else.
            base.BorderStyle = BorderStyle.None;

            // Always turn off the base functionality as we do it instead.
            base.ShowCellToolTips = false;
        }

        private void SetupSyncCellStyles()
        {
            // Grab the default font (etc...) values as the starting remembered values. 
            // Then when we test for changes it will not look as if the user has changed
            // them. This ensures the call to SyncCellStylesWithPalette updated them.
            _columnFont = ColumnHeadersDefaultCellStyle.Font;
            _rowFont = RowHeadersDefaultCellStyle.Font;
            _dataCellFont = DefaultCellStyle.Font;
            _columnPadding = ColumnHeadersDefaultCellStyle.Padding;
            _rowPadding = RowHeadersDefaultCellStyle.Padding;
            _dataCellPadding = DefaultCellStyle.Padding;
            _columnAlign = ColumnHeadersDefaultCellStyle.Alignment;
            _rowAlign = RowHeadersDefaultCellStyle.Alignment;
            _dataCellAlign = DefaultCellStyle.Alignment;
            _columnBackColor = ColumnHeadersDefaultCellStyle.BackColor;
            _columnForeColor = ColumnHeadersDefaultCellStyle.ForeColor;
            _columnSelBackColor = ColumnHeadersDefaultCellStyle.SelectionBackColor;
            _columnSelForeColor = ColumnHeadersDefaultCellStyle.SelectionForeColor;
            _rowBackColor = RowHeadersDefaultCellStyle.BackColor;
            _rowForeColor = RowHeadersDefaultCellStyle.ForeColor;
            _rowSelBackColor = RowHeadersDefaultCellStyle.SelectionBackColor;
            _rowSelForeColor = RowHeadersDefaultCellStyle.SelectionForeColor;
            _dataCellBackColor = DefaultCellStyle.BackColor;
            _dataCellForeColor = DefaultCellStyle.ForeColor;
            _dataCellSelBackColor = DefaultCellStyle.SelectionBackColor;
            _dataCellSelForeColor = DefaultCellStyle.SelectionForeColor;

            // Ensure the current cell style values are in sync with the new palette 
            // setting and any state overrides that are defined.
            SyncCellStylesWithPalette();

            // We need to know when the common values we sync are changed
            StateCommon.HeaderColumn.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateCommon.HeaderRow.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateCommon.DataCell.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateNormal.HeaderColumn.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateNormal.HeaderRow.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateNormal.DataCell.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateSelected.HeaderColumn.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateSelected.HeaderRow.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateSelected.DataCell.Content.SyncPropertyChanged += new EventHandler(OnSyncPropertyChanged);
            StateNormal.HeaderColumn.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
            StateNormal.HeaderRow.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
            StateNormal.DataCell.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
            StateSelected.HeaderColumn.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
            StateSelected.HeaderRow.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
            StateSelected.DataCell.Back.PropertyChanged += new PropertyChangedEventHandler(OnSyncBackPropertyChanged);
        }

        private void SyncCellStylesWithPalette()
        {
            if (StateCommon != null)
            {
                SyncFontCellStylesWithPalette();
                SyncPaddingCellStylesWithPalette();
                SyncAlignmentCellStylesWithPalette();
                SyncBackColorCellStylesWithPalette();
                SyncSelBackColorCellStylesWithPalette();
                SyncForeColorCellStylesWithPalette();
                SyncSelForeColorCellStylesWithPalette();
            }
        }

        private void SyncFontCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            // If the column headers default font is null or if the same as when we last
            // set the value then we do need to update with the latest value. Otherwise
            // the programmer has modified the value and so leave it alone as overrriden.
            if ((ColumnHeadersDefaultCellStyle.Font == null) ||
                (ColumnHeadersDefaultCellStyle.Font.Equals(_columnFont)))
            {
                // Get the overriden value from the stat common
                _columnFont = StateCommon.HeaderColumn.Content.Font;

                // If not found, get it from the inheritance palette
                if (_columnFont == null)
                    _columnFont = StateCommon.HeaderColumn.Content.GetContentShortTextFont(state);

                ColumnHeadersDefaultCellStyle.Font = _columnFont;
            }

            if ((RowHeadersDefaultCellStyle.Font == null) ||
                (RowHeadersDefaultCellStyle.Font.Equals(_rowFont)))
            {
                _rowFont = StateCommon.HeaderRow.Content.Font;
                if (_rowFont == null)
                    _rowFont = StateCommon.HeaderRow.Content.GetContentShortTextFont(state);

                RowHeadersDefaultCellStyle.Font = _rowFont;
            }

            if ((DefaultCellStyle.Font == null) ||
                (DefaultCellStyle.Font.Equals(_dataCellFont)))
            {
                _dataCellFont = StateCommon.DataCell.Content.Font;
                if (_dataCellFont == null)
                    _dataCellFont = StateCommon.DataCell.Content.GetContentShortTextFont(state);

                DefaultCellStyle.Font = _dataCellFont;
            }
        }

        private void SyncPaddingCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            if (ColumnHeadersDefaultCellStyle.Padding.Equals(_columnPadding))
            {
                _columnPadding = StateCommon.HeaderColumn.Content.Padding;
                if (_columnPadding.Equals(CommonHelper.InheritPadding))
                    _columnPadding = StateCommon.HeaderColumn.Content.GetContentPadding(state);

                ColumnHeadersDefaultCellStyle.Padding = _columnPadding;
            }

            if (RowHeadersDefaultCellStyle.Padding.Equals(_rowPadding))
            {
                _rowPadding = StateCommon.HeaderRow.Content.Padding;
                if (_rowPadding.Equals(CommonHelper.InheritPadding))
                    _rowPadding = StateCommon.HeaderRow.Content.GetContentPadding(state);

                RowHeadersDefaultCellStyle.Padding = _rowPadding;
            }

            if (DefaultCellStyle.Padding.Equals(_dataCellPadding))
            {
                _dataCellPadding = StateCommon.DataCell.Content.Padding;
                if (_dataCellPadding.Equals(CommonHelper.InheritPadding))
                    _dataCellPadding = StateCommon.DataCell.Content.GetContentPadding(state);

                DefaultCellStyle.Padding = _dataCellPadding;
            }
        }

        private void SyncAlignmentCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            if (ColumnHeadersDefaultCellStyle.Alignment == _columnAlign)
            {
                PaletteRelativeAlign textH = StateCommon.HeaderColumn.Content.TextH;
                PaletteRelativeAlign textV = StateCommon.HeaderColumn.Content.TextV;

                if (textH == PaletteRelativeAlign.Inherit)
                    textH = StateCommon.HeaderColumn.Content.GetContentShortTextH(state);

                if (textV == PaletteRelativeAlign.Inherit)
                    textV = StateCommon.HeaderColumn.Content.GetContentShortTextV(state);

                _columnAlign = RelativeToAlign(textH, textV);
                ColumnHeadersDefaultCellStyle.Alignment = _columnAlign;
            }

            if (RowHeadersDefaultCellStyle.Alignment == _rowAlign)
            {
                PaletteRelativeAlign textH = StateCommon.HeaderRow.Content.TextH;
                PaletteRelativeAlign textV = StateCommon.HeaderRow.Content.TextV;

                if (textH == PaletteRelativeAlign.Inherit)
                    textH = StateCommon.HeaderRow.Content.GetContentShortTextH(state);

                if (textV == PaletteRelativeAlign.Inherit)
                    textV = StateCommon.HeaderRow.Content.GetContentShortTextV(state);

                _rowAlign = RelativeToAlign(textH, textV);
                RowHeadersDefaultCellStyle.Alignment = _rowAlign;
            }

            if (DefaultCellStyle.Alignment == _dataCellAlign)
            {
                PaletteRelativeAlign textH = StateCommon.DataCell.Content.TextH;
                PaletteRelativeAlign textV = StateCommon.DataCell.Content.TextV;

                if (textH == PaletteRelativeAlign.Inherit)
                    textH = StateCommon.DataCell.Content.GetContentShortTextH(state);

                if (textV == PaletteRelativeAlign.Inherit)
                    textV = StateCommon.DataCell.Content.GetContentShortTextV(state);

                _dataCellAlign = RelativeToAlign(textH, textV);
                DefaultCellStyle.Alignment = _dataCellAlign;
            }
        }

        private void SyncBackColorCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            if ((ColumnHeadersDefaultCellStyle.BackColor == Color.Empty) ||
                (ColumnHeadersDefaultCellStyle.BackColor == _columnBackColor))
            {
                _columnBackColor = StateNormal.HeaderColumn.Back.Color1;

                if (_columnBackColor == Color.Empty)
                    _columnBackColor = StateNormal.HeaderColumn.Back.GetBackColor1(state);

                ColumnHeadersDefaultCellStyle.BackColor = _columnBackColor;
            }

            if ((RowHeadersDefaultCellStyle.BackColor == Color.Empty) ||
                (RowHeadersDefaultCellStyle.BackColor == _rowBackColor))
            {
                _rowBackColor = StateNormal.HeaderRow.Back.Color1;

                if (_rowBackColor == Color.Empty)
                    _rowBackColor = StateNormal.HeaderRow.Back.GetBackColor1(state);

                RowHeadersDefaultCellStyle.BackColor = _rowBackColor;
            }

            if ((DefaultCellStyle.BackColor == Color.Empty) ||
                (DefaultCellStyle.BackColor == _dataCellBackColor))
            {
                _dataCellBackColor = StateNormal.DataCell.Back.Color1;

                if (_dataCellBackColor == Color.Empty)
                    _dataCellBackColor = StateNormal.DataCell.Back.GetBackColor1(state);

                DefaultCellStyle.BackColor = _dataCellBackColor;
            }
        }

        private void SyncSelBackColorCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.CheckedNormal : PaletteState.Disabled);

            if ((ColumnHeadersDefaultCellStyle.SelectionBackColor == Color.Empty) ||
                (ColumnHeadersDefaultCellStyle.SelectionBackColor == _columnSelBackColor))
            {
                _columnSelBackColor = StateSelected.HeaderColumn.Back.Color1;

                if (_columnSelBackColor == Color.Empty)
                    _columnSelBackColor = StateSelected.HeaderColumn.Back.GetBackColor1(state);

                ColumnHeadersDefaultCellStyle.SelectionBackColor = _columnSelBackColor;
            }

            if ((RowHeadersDefaultCellStyle.SelectionBackColor == Color.Empty) ||
                (RowHeadersDefaultCellStyle.SelectionBackColor == _rowSelBackColor))
            {
                _rowSelBackColor = StateSelected.HeaderRow.Back.Color1;

                if (_rowSelBackColor == Color.Empty)
                    _rowSelBackColor = StateSelected.HeaderRow.Back.GetBackColor1(state);

                RowHeadersDefaultCellStyle.SelectionBackColor = _rowSelBackColor;
            }

            if ((DefaultCellStyle.SelectionBackColor == Color.Empty) ||
                (DefaultCellStyle.SelectionBackColor == _dataCellSelBackColor))
            {
                _dataCellSelBackColor = StateSelected.DataCell.Back.Color1;

                if (_dataCellSelBackColor == Color.Empty)
                    _dataCellSelBackColor = StateSelected.DataCell.Back.GetBackColor1(state);

                DefaultCellStyle.SelectionBackColor = _dataCellSelBackColor;
            }
        }

        private void SyncForeColorCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            if ((ColumnHeadersDefaultCellStyle.ForeColor == Color.Empty) ||
                (ColumnHeadersDefaultCellStyle.ForeColor == _columnForeColor))
            {
                _columnForeColor = StateNormal.HeaderColumn.Content.Color1;

                if (_columnForeColor == Color.Empty)
                    _columnForeColor = StateNormal.HeaderColumn.Content.GetContentShortTextColor1(state);

                ColumnHeadersDefaultCellStyle.ForeColor = _columnForeColor;
            }

            if ((RowHeadersDefaultCellStyle.ForeColor == Color.Empty) ||
                (RowHeadersDefaultCellStyle.ForeColor == _rowForeColor))
            {
                _rowForeColor = StateNormal.HeaderRow.Content.Color1;

                if (_rowForeColor == Color.Empty)
                    _rowForeColor = StateNormal.HeaderRow.Content.GetContentShortTextColor1(state);

                RowHeadersDefaultCellStyle.ForeColor = _rowForeColor;
            }

            if ((DefaultCellStyle.ForeColor == Color.Empty) ||
                (DefaultCellStyle.ForeColor == _dataCellForeColor))
            {
                _dataCellForeColor = StateNormal.DataCell.Content.Color1;

                if (_dataCellForeColor == Color.Empty)
                    _dataCellForeColor = StateNormal.DataCell.Content.GetContentShortTextColor1(state);

                DefaultCellStyle.ForeColor = _dataCellForeColor;
            }
        }

        private void SyncSelForeColorCellStylesWithPalette()
        {
            PaletteState state = (Enabled ? PaletteState.CheckedNormal : PaletteState.Disabled);

            if ((ColumnHeadersDefaultCellStyle.SelectionForeColor == Color.Empty) ||
                (ColumnHeadersDefaultCellStyle.SelectionForeColor == _columnSelForeColor))
            {
                _columnSelForeColor = StateSelected.HeaderColumn.Content.Color1;

                if (_columnSelForeColor == Color.Empty)
                    _columnSelForeColor = StateSelected.HeaderColumn.Content.GetContentShortTextColor1(state);

                ColumnHeadersDefaultCellStyle.SelectionForeColor = _columnSelForeColor;
            }

            if ((RowHeadersDefaultCellStyle.SelectionForeColor == Color.Empty) ||
                (RowHeadersDefaultCellStyle.SelectionForeColor == _rowSelForeColor))
            {
                _rowSelForeColor = StateSelected.HeaderRow.Content.Color1;

                if (_rowSelForeColor == Color.Empty)
                    _rowSelForeColor = StateSelected.HeaderRow.Content.GetContentShortTextColor1(state);

                RowHeadersDefaultCellStyle.SelectionForeColor = _rowSelForeColor;
            }

            if ((DefaultCellStyle.SelectionForeColor == Color.Empty) ||
                (DefaultCellStyle.SelectionForeColor == _dataCellSelForeColor))
            {
                _dataCellSelForeColor = StateSelected.DataCell.Content.Color1;

                if (_dataCellSelForeColor == Color.Empty)
                    _dataCellSelForeColor = StateSelected.DataCell.Content.GetContentShortTextColor1(state);

                DefaultCellStyle.SelectionForeColor = _dataCellSelForeColor;
            }
        }

        private byte UpdateLocationForRowErrors(DataGridViewCellMouseEventArgs e,
                                                DataGridViewCell cell,
                                                byte location)
        {
            // If over the main area of a row header cell...
            if ((cell is DataGridViewRowHeaderCell) && (location == 1))
            {
                // Check is really over the error icon area
                if (_rowCache.ContainsKey(e.RowIndex))
                {
                    // Mark as location=2 which is over icon bounds
                    if (_rowCache[e.RowIndex].Contains(new Point(e.X, e.Y)))
                        location = 2;
                }
            }

            return location;
        }

        private DataGridViewContentAlignment RelativeToAlign(PaletteRelativeAlign textH, 
                                                             PaletteRelativeAlign textV)
        {
            switch (textH)
            {
                case PaletteRelativeAlign.Near:
                    switch (textV)
                    {
                        case PaletteRelativeAlign.Near:
                            return DataGridViewContentAlignment.TopLeft;
                        case PaletteRelativeAlign.Center:
                            return DataGridViewContentAlignment.MiddleLeft;
                        case PaletteRelativeAlign.Far:
                            return DataGridViewContentAlignment.BottomLeft;
                    }
                    break;
                case PaletteRelativeAlign.Center:
                    switch (textV)
                    {
                        case PaletteRelativeAlign.Near:
                            return DataGridViewContentAlignment.TopCenter;
                        case PaletteRelativeAlign.Center:
                            return DataGridViewContentAlignment.MiddleCenter;
                        case PaletteRelativeAlign.Far:
                            return DataGridViewContentAlignment.BottomCenter;
                    }
                    break;
                case PaletteRelativeAlign.Far:
                    switch (textV)
                    {
                        case PaletteRelativeAlign.Near:
                            return DataGridViewContentAlignment.TopRight;
                        case PaletteRelativeAlign.Center:
                            return DataGridViewContentAlignment.MiddleRight;
                        case PaletteRelativeAlign.Far:
                            return DataGridViewContentAlignment.BottomRight;
                    }
                    break;
            }

            // Should never happen!
            Debug.Assert(false);
            return DataGridViewContentAlignment.MiddleLeft;
        }

        private PaletteDrawBorders GetCellMaxBorderEdges(Rectangle cellBounds,
                                                         int column, 
                                                         int row)
        {
            // We always draw the bottom border and left/right depending on RTL setting
            PaletteDrawBorders maxBorders = PaletteDrawBorders.Bottom |
                                            (RightToLeftInternal ? PaletteDrawBorders.Left : 
                                                                   PaletteDrawBorders.Right);
                                            
            // Do we need a top border
            if (!HideOuterBorders && ((row == -1) || ((row == 0) && !ColumnHeadersVisible)))
                maxBorders |= PaletteDrawBorders.Top;

            // Do we need a left/right border
            if (!HideOuterBorders && ((column == -1) || ((column == 0) && !RowHeadersVisible)))
                maxBorders |= (RightToLeftInternal ? PaletteDrawBorders.Right : 
                                                     PaletteDrawBorders.Left);

            // Check if the cell is hard against the far or bottom edges, if so do not need to draw 
            // border that is hard against the edge as it will then look like it has double borders
            if (HideOuterBorders)
            {
                // With RTL we check the left border
                if (RightToLeftInternal)
                {
                    if (cellBounds.Left == 0)
                        maxBorders &= ~PaletteDrawBorders.Left;
                }
                else
                {
                    // Check the right border
                    if (cellBounds.Right == Width)
                        maxBorders &= ~PaletteDrawBorders.Right;
                }

                // Check the bottom border
                if (cellBounds.Bottom == Height)
                    maxBorders &= ~PaletteDrawBorders.Bottom;
            }

            return maxBorders;
        }

        private void ViewManagerLayout()
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager to use for laying out?
                if (ViewManager != null)
                {
                    // Prevent infinite loop by looping a maximum number of times
                    int max = 5;

                    do
                    {
                        // Layout cannot now be dirty
                        _layoutDirty = false;

                        // Ask the view to peform a layout
                        ViewManager.Layout(_renderer);

                    } while (_layoutDirty && (max-- > 0));

                    // Remember size when last layout was performed
                    _lastLayoutSize = Size;
                }
            }
        }

        private void CellDataAreaMouseEnterInternal(DataGridViewCell cell)
        {
            Point currentCellAddress = CurrentCellAddress;

            if (!((cell.RowIndex >= 0) && (cell.ColumnIndex == -1)))
            {
                // Are we allowed to show a tooltip?
                if (ShowCellToolTips &&
                    ((currentCellAddress.X == -1) || (currentCellAddress.X != cell.ColumnIndex) ||
                    (currentCellAddress.Y != cell.RowIndex) || (EditingControl == null)))
                {
                    // Grab the correct tooltip text for the cell
                    _toolTipText = GetToolTipText(cell, cell.RowIndex);

                    // No explicit text provided?
                    if (string.IsNullOrEmpty(_toolTipText))
                    {
                        // Only interested in string values
                        if (cell.FormattedValueType == typeof(string))
                        {
                            // If for a data row and NOT the header
                            if ((cell.RowIndex != -1) && (cell.OwningColumn != null))
                            {
                                if ((cell.OwningColumn.Width < GetCellPreferredWidth(cell)) ||
                                    (cell.OwningRow.Height < GetCellPreferredHeight(cell)))
                                {
                                    string editedValue = cell.GetEditedFormattedValue(cell.RowIndex, DataGridViewDataErrorContexts.Display) as string;
                                    if (!string.IsNullOrEmpty(editedValue))
                                        _toolTipText = TruncateToolTipText(editedValue);
                                }
                            }
                            else if ((cell.RowIndex == -1) && (cell.ColumnIndex != -1) && _columnCache.ContainsKey(cell.ColumnIndex))
                            {
                                // If for a column cell and the contents do not fit...
                                if (!_columnCache[cell.ColumnIndex])
                                {
                                    try
                                    {
                                        string editedValue = cell.GetEditedFormattedValue(cell.RowIndex, DataGridViewDataErrorContexts.Display) as string;
                                        if (!string.IsNullOrEmpty(editedValue))
                                            _toolTipText = TruncateToolTipText(editedValue);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }

                    // Restart the timer for showing the tooltip
                    if (_showTimer != null)
                    {
                        _showTimer.Stop();
                        _showTimer.Start();
                    }
                }
                else
                    CellAreaMouseLeaveInternal();
            }
        }

        private void CellErrorAreaMouseEnterInternal(DataGridViewCell cell)
        {
            // Grab the correct error text for the cell
            _toolTipText = GetErrorText(cell, cell.RowIndex);

            // Restart the timer for showing the error tooltip
            if (_showTimer != null)
            {
                _showTimer.Stop();
                _showTimer.Start();
            }
        }

        private void CellAreaMouseLeaveInternal()
        {
            // Stop the timer from showing a tooltip
            if (_showTimer != null)
                _showTimer.Stop();

            // If there is a popup tooltip showing
            if (_visualPopupToolTip != null)
                VisualPopupManager.Singleton.EndPopupTracking(_visualPopupToolTip);
        }

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            // Only need a one tick timer
            if (_showTimer != null)
            {
                _showTimer.Stop();

                if (!string.IsNullOrEmpty(_toolTipText))
                {
                    // Prevent the base class from showing a tooltip itself
                    DismissBaseToolTips();

                    // Remove any currently showing tooltip
                    if (_visualPopupToolTip != null)
                        _visualPopupToolTip.Dispose();

                    // Create the actual tooltip popup object
                    _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                 new ToolTipContent(_toolTipText),
                                                                 Renderer,
                                                                 PaletteBackStyle.ControlToolTip,
                                                                 PaletteBorderStyle.ControlToolTip,
                                                                 PaletteContentStyle.LabelToolTip);

                    _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                    // Show relative to the provided screen point
                    _visualPopupToolTip.ShowCalculatingSize(Control.MousePosition);
                }
            }
        }

        private void CacheAccessToLayout()
        {
            // Only need to cache reflection info the first time it is needed
            if (_fiLayout == null)
            {
                // Cache field info about the internal 'layout' instance
                _fiLayout = typeof(DataGridView).GetField("layout", BindingFlags.Instance |
                                                                    BindingFlags.NonPublic |
                                                                    BindingFlags.GetField);

                // Cache field info about the various 'layout' fields we need
                Type layoutType = _fiLayout.GetValue(this).GetType();
                _fiColumnHeaders = layoutType.GetField("ColumnHeaders");
                _fiRowHeaders = layoutType.GetField("RowHeaders");
                _fiColumnHeadersVisible = layoutType.GetField("ColumnHeadersVisible");
                _fiRowHeadersVisible = layoutType.GetField("RowHeadersVisible");
            }
        }

        private DataGridViewCell GetCellInternal(int column, int row)
        {
            // Only need to cache reflection info the first time around
            if (_miGCI == null)
            {
                // Cache access to the internal method 'GetCellInternal'
                _miGCI = typeof(DataGridView).GetMethod("GetCellInternal", BindingFlags.Instance |
                                                                           BindingFlags.NonPublic |
                                                                           BindingFlags.GetField);
            }

            return (DataGridViewCell)_miGCI.Invoke(this, new object[] { column, row });
        }

        private Graphics CachedGraphics
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piCG == null)
                {
                    // Cache access to the internal get property 'CachedGraphics'
                    _piCG = typeof(DataGridView).GetProperty("CachedGraphics", BindingFlags.Instance |
                                                                               BindingFlags.NonPublic |
                                                                               BindingFlags.GetField);
                }

                return (Graphics)_piCG.GetValue(this, null);
            }
        }

        private string GetToolTipText(DataGridViewCell cell, int row)
        {
            // Only need to cache reflection info the first time around
            if (_miGTTT == null)
            {
                // Cache access to the internal get property 'GetToolTipText'
                _miGTTT = typeof(DataGridViewCell).GetMethod("GetToolTipText", BindingFlags.Instance |
                                                                               BindingFlags.NonPublic |
                                                                               BindingFlags.GetField);
            }

            try
            {
                return (string)_miGTTT.Invoke(cell, new object[] { row });
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetErrorText(DataGridViewCell cell, int row)
        {
            // Only need to cache reflection info the first time around
            if (_miGET == null)
            {
                // Cache access to the internal get property 'GetErrorText'
                _miGET = typeof(DataGridViewCell).GetMethod("GetErrorText", BindingFlags.Instance |
                                                                            BindingFlags.NonPublic |
                                                                            BindingFlags.GetField);
            }

            try
            {
                return (string)_miGET.Invoke(cell, new object[] { row });
            }
            catch
            {
                return string.Empty;
            }
        }

        private byte CurrentMouseLocation(DataGridViewCell cell)
        {
            // Only need to cache reflection info the first time around
            if (_piCML == null)
            {
                // Cache access to the internal get property 'CurrentMouseLocation'
                _piCML = typeof(DataGridViewCell).GetProperty("CurrentMouseLocation", BindingFlags.Instance |
                                                                                      BindingFlags.NonPublic |
                                                                                      BindingFlags.GetField);
            }

            // Grab the internal calculated value of the right to left setting
            return (byte)_piCML.GetValue(cell, null);
        }

        private int GetCellPreferredWidth(DataGridViewCell cell)
        {
            // Only need to cache reflection info the first time around
            if (_miGPW == null)
            {
                // Cache access to the internal method 'GetPreferredWidth' of cells
                _miGPW = typeof(DataGridViewCell).GetMethod("GetPreferredWidth", BindingFlags.Instance |
                                                                                 BindingFlags.NonPublic |
                                                                                 BindingFlags.GetField);
            }

            return (int)_miGPW.Invoke(cell, new object[] { cell.RowIndex, cell.OwningRow.Height });
        }

        private int GetCellPreferredHeight(DataGridViewCell cell)
        {
            // Only need to cache reflection info the first time around
            if (_miGPH == null)
            {
                // Cache access to the internal method 'GetPreferredHeight' of cells
                _miGPH = typeof(DataGridViewCell).GetMethod("GetPreferredHeight", BindingFlags.Instance |
                                                                                  BindingFlags.NonPublic |
                                                                                  BindingFlags.GetField);
            }

            return (int)_miGPH.Invoke(cell, new object[] { cell.RowIndex, cell.OwningColumn.Width });
        }

        private string DismissBaseToolTips()
        {
            // Only need to cache reflection info the first time around
            if (_miATT == null)
            {
                // Cache access to the internal get property 'ActivateToolTip'
                _miATT = typeof(DataGridView).GetMethod("ActivateToolTip", BindingFlags.Instance |
                                                                           BindingFlags.NonPublic |
                                                                           BindingFlags.GetField);
            }

            return (string)_miATT.Invoke(this, new object[] { false, string.Empty, -1, -1 });
        }

        private string TruncateToolTipText(string toolTipText)
        {
            if (toolTipText.Length > 0x120)
            {
                StringBuilder builder = new StringBuilder(toolTipText.Substring(0, 0x100), 0x103);
                builder.Append("...");
                return builder.ToString();
            }
            return toolTipText;
        }

        private Rectangle GetBackgroundClipRect()
        {
            Rectangle cellsRect = Rectangle.Empty;

            // Ensure we have cached access to the internal layout fields
            CacheAccessToLayout();

            // Get access to the actual internal instance
            object layout = _fiLayout.GetValue(this);

            // Grab the current internal fields we need
            Rectangle columnHeaders = (Rectangle)_fiColumnHeaders.GetValue(layout);
            Rectangle rowHeaders = (Rectangle)_fiRowHeaders.GetValue(layout);
            bool columnHeadersVisible = (bool)_fiColumnHeadersVisible.GetValue(layout);
            bool rowHeadersVisible = (bool)_fiRowHeadersVisible.GetValue(layout);

            // Find the width/height of the data cells area
            int columnsWidth = Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int rowsHeight = Rows.GetRowsHeight(DataGridViewElementStates.Visible);

            // Add on the width/height from showing the optional headers
            if (columnHeadersVisible) rowsHeight += columnHeaders.Height;

            if (rowHeadersVisible)
                columnsWidth += rowHeaders.Width;
            else
            {
                // Seems to be a bug in the base implementation such that without the row
                // headers showing the column width is 1 too thin. So add one the extra 1
                // pixel needed when there are no row headers showing.
                columnsWidth++;
            }

            // If there are no rows or columns, then not much to do
            if ((Rows.Count > 0) && (Columns.Count > 0))
            {
                // Set the height/width of the cells area
                cellsRect.Height = rowsHeight;
                cellsRect.Width = columnsWidth;

                // Adjust to reflect the scrolling
                cellsRect.Y -= VerticalScrollingOffset;
                cellsRect.X -= HorizontalScrollingOffset;

                // Adjust the rectangle if using right to left setting
                if (RightToLeft == RightToLeft.Yes)
                    cellsRect.X = (Width - columnsWidth) + HorizontalScrollingOffset;
            }

            return cellsRect;
        }

        private void SetPalette(IPalette palette)
        {
            if (palette != _palette)
            {
                // Unhook from current palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnNeedResyncPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                }

                // Remember the new palette
                _palette = palette;

                // Get the renderer associated with the palette
                _renderer = _palette.GetRenderer();

                // Hook to new palette events
                if (_palette != null)
                {
                    _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnNeedResyncPaint);
                    _palette.ButtonSpecChanged += new EventHandler(OnButtonSpecChanged);
                }

                // Ensure the current cell style values are in sync with the new 
                // palette setting and any state overrides that are defined
                SyncCellStylesWithPalette();
            }
        }

        private void PaintTransparentBackground(Graphics g, Rectangle clipRect)
        {
            // Get the parent control for transparent drawing purposes
            Control parent = TransparentParent;

            // Do we have a parent control and we need to paint background?
            if ((parent != null) && NeedTransparentPaint)
            {
                // Only grab the required reference once
                if (_miPTB == null)
                {
                    // Use reflection so we can call the Windows Forms internal method for painting parent background
                    _miPTB = typeof(Control).GetMethod("PaintTransparentBackground",
                                                       BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                                                       null, CallingConventions.HasThis,
                                                       new Type[] { typeof(PaintEventArgs), typeof(Rectangle), typeof(Region) },
                                                       null);
                }

                _miPTB.Invoke(this, new object[] { new PaintEventArgs(g, clipRect), ClientRectangle, null });
            }
        }

        private void OnPerformRefresh()
        {
            // If we still need to perform the refresh
            if (_refresh)
            {
                // Perform the requested paint of the control
                Refresh();

                // If the layout is still dirty after the refresh
                if (_layoutDirty)
                {
                    // Then non of the control is visible, so perform manual request
                    // for a layout to ensure that child controls can be resized
                    PerformLayout();

                    // Need another repaint to take the layout change into account
                    Refresh();
                }

                // Refresh request has been serviced
                _refresh = false;
                _refreshAll = false;
            }
        }

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // We only care if we are using the global palette
            if (PaletteMode == PaletteMode.Global)
            {
                // Update ourself with the new global palette
                _localPalette = null;
                SetPalette(KryptonManager.CurrentGlobalPalette);
                Redirector.Target = _palette;
                SyncCellStylesWithPalette();

                // A new palette source means we need to layout and redraw
                OnNeedPaint(Palette, new NeedLayoutEventArgs(true));
            }
        }

        private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            OnNeedResyncPaint(Palette, new NeedLayoutEventArgs(true));
        }

        private void OnSyncPropertyChanged(object sender, EventArgs e)
        {
            // Ensure the current cell style values are in sync with the new palette 
            // setting and any state overrides that are defined.
            SyncCellStylesWithPalette();
        }

        private void OnSyncBackPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Only interested in the first color from the background palettes
            if (e.PropertyName == "Color1")
            {
                // Ensure the current cell style values are in sync with the new palette 
                // setting and any state overrides that are defined.
                SyncCellStylesWithPalette();
            }
        }
        #endregion
    }
}
