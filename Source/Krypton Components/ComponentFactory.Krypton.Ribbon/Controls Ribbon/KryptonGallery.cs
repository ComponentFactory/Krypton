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
using System.Diagnostics;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
    /// Provides a gallery for selecting from a group of possible images.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonGallery), "ToolboxBitmaps.KryptonGallery.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("SelectedIndex")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonGalleryDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Select from a group of possible images.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonGallery : VisualSimpleBase
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonGalleryRangeCollection _dropButtonRanges;
        private PaletteGalleryRedirect _stateCommon;
        private PaletteGalleryState _stateNormal;
        private PaletteGalleryState _stateDisabled;
        private PaletteGalleryState _stateActive;
        private PaletteGalleryBackBorder _backBorder;
        private ViewLayoutRibbonGalleryButtons _buttonsLayout;
        private ViewDrawRibbonGalleryButton _buttonUp;
        private ViewDrawRibbonGalleryButton _buttonDown;
        private ViewDrawRibbonGalleryButton _buttonContext;
        private ViewLayoutRibbonGalleryItems _drawItems;
        private ImageList _imageList;
        private GalleryImages _images;
        private ViewLayoutDocker _layoutDocker;
        private ViewDrawDocker _drawDocker;
        private Nullable<bool> _fixedActive;
        private Size _preferredItemSize;
        private bool _inRibbonDesignMode;
        private bool _mouseOver;
        private bool _alwaysActive;
        private int _dropMaxItemWidth;
        private int _dropMinItemWidth;
        private int _selectedIndex;
        private int _trackingIndex;
        private int _cacheTrackingIndex;
        private int _eventTrackingIndex;
        private Timer _trackingEventTimer;
        private KryptonContextMenu _dropMenu;
        private EventHandler _finishDelegate;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the ImageList property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the ImageList property changes.")]
        public event EventHandler ImageListChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the SelectedIndex property changes.")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when the user is tracking over a color.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user is tracking over an image.")]
        public event EventHandler<ImageSelectEventArgs> TrackingImage;

        /// <summary>
        /// Occurs when the user invokes the drop down menu.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user invokes the drop down menu.")]
        public event EventHandler<GalleryDropMenuEventArgs> GalleryDropMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGallery class.
		/// </summary>
        public KryptonGallery()
		{
            // Defaults
            _mouseOver = false;
            _alwaysActive = true;
            _selectedIndex = -1;
            _trackingIndex = -1;
            _eventTrackingIndex = -1;
            _preferredItemSize = new Size(5, 1);
            _dropMaxItemWidth = 128;
            _dropMinItemWidth = 3;

            // Timer used to generate tracking change event
            _trackingEventTimer = new Timer();
            _trackingEventTimer.Interval = 120;
            _trackingEventTimer.Tick += new EventHandler(OnTrackingTick);

            // Create content storage
            _images = new GalleryImages(NeedPaintDelegate);
            _dropButtonRanges = new KryptonGalleryRangeCollection();

            // Create the palette storage
            _stateCommon = new PaletteGalleryRedirect(Redirector, NeedPaintDelegate);
            _stateNormal = new PaletteGalleryState(_stateCommon, NeedPaintDelegate);
            _stateDisabled = new PaletteGalleryState(_stateCommon, NeedPaintDelegate);
            _stateActive = new PaletteGalleryState(_stateCommon, NeedPaintDelegate);

            // Create and organize the buttons
            _buttonUp = new ViewDrawRibbonGalleryButton(Redirector, PaletteRelativeAlign.Near, PaletteRibbonGalleryButton.Up, _images, NeedPaintDelegate);
            _buttonDown = new ViewDrawRibbonGalleryButton(Redirector, PaletteRelativeAlign.Center, PaletteRibbonGalleryButton.Down, _images, NeedPaintDelegate);
            _buttonContext = new ViewDrawRibbonGalleryButton(Redirector, PaletteRelativeAlign.Far, PaletteRibbonGalleryButton.DropDown, _images, NeedPaintDelegate);
            _buttonsLayout = new ViewLayoutRibbonGalleryButtons();
            _buttonsLayout.Add(_buttonUp);
            _buttonsLayout.Add(_buttonDown);
            _buttonsLayout.Add(_buttonContext);

            // The draw layout that contains the actual selection images
            _backBorder = new PaletteGalleryBackBorder(_stateNormal);
            _drawDocker = new ViewDrawDocker(_backBorder, _backBorder);
            _drawItems = new ViewLayoutRibbonGalleryItems(Redirector, this, NeedPaintDelegate, _buttonUp, _buttonDown, _buttonContext);
            _drawDocker.Add(_drawItems, ViewDockStyle.Fill);

            // Top level layout view
            _layoutDocker = new ViewLayoutDocker();
            _layoutDocker.Add(_drawDocker, ViewDockStyle.Fill);
            _layoutDocker.Add(_buttonsLayout, ViewDockStyle.Right);

			// Create the view manager instance
            ViewManager = new ViewManager(this, _layoutDocker);

            // Set the default padding value
            base.Padding = new Padding(3);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
		#endregion

		#region Public
        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        /// <summary>
        /// Gets and sets the control text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [DefaultValue(typeof(Padding), "3,3,3,3")]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets the collection of drop down ranges.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of drop down ranges")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonGalleryRangeCollection DropButtonRanges
        {
            get { return _dropButtonRanges; }
        }

        /// <summary>
        /// Gets and sets the preferred size based on the number of items per line and number of lines.
        /// </summary>
        [Category("Layout")]
        [Description("Preferred size measured in items per line and number of display lines.")]
        [DefaultValue(typeof(Size), "5,1")]
        public Size PreferredItemSize
        {
            get { return _preferredItemSize; }
            
            set 
            {
                if (!_preferredItemSize.Equals(value))
                {
                    // Ensure minimum values
                    value.Width = Math.Max(1, value.Width);
                    value.Height = Math.Max(1, value.Height);

                    // Use new value
                    _preferredItemSize = value;
                    PerformLayout();
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of lines items for the drop down menu.
        /// </summary>
        [Category("Layout")]
        [Description("Maximum number of line items for the drop down menu.")]
        [DefaultValue(128)]
        public int DropMaxItemWidth
        {
            get { return _dropMaxItemWidth; }

            set
            {
                if (_dropMaxItemWidth != value)
                {
                    value = Math.Max(1, value);
                    _dropMaxItemWidth = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the minimum number of lines items for the drop down menu.
        /// </summary>
        [Category("Layout")]
        [Description("Minimum number of line items for the drop down menu.")]
        [DefaultValue(3)]
        public int DropMinItemWidth
        {
            get { return _dropMinItemWidth; }

            set
            {
                if (_dropMinItemWidth != value)
                {
                    value = Math.Max(1, value);
                    _dropMinItemWidth = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button style used for each image item.
        /// </summary>
        [Category("Visuals")]
        [Description("Button style used for each image item.")]
        [DefaultValue(typeof(ButtonStyle), "LowProfile")]
        public ButtonStyle ButtonStyle
        {
            get { return _drawItems.ButtonStyle; }
            set { _drawItems.ButtonStyle = value; }
        }

        /// <summary>
        /// Gets and sets if scrolling is animated or a jump straight to target..
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if scrolling is animated or a jump straight to target.")]
        [DefaultValue(true)]
        public bool SmoothScrolling
        {
            get { return _drawItems.ScrollIntoView; }
            set { _drawItems.ScrollIntoView = value; }
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
        /// Gets access to the collection of images for display and selection.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of images for display and selection.")]
        public ImageList ImageList
        {
            get { return _imageList; }
            
            set 
            { 
                _imageList = value;
                PerformNeedPaint(true);
                OnImageListChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets access to the collection of images for display and selection.
        /// </summary>
        [Category("Visuals")]
        [Description("The index of the selected image.")]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get { return _selectedIndex; }

            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    BringIntoView(_selectedIndex);
                    PerformNeedPaint(true);
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets access to the button image overrides.
        /// </summary>
        [Category("Visuals")]
        [Description("Gallery button image overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GalleryImages Images
        {
            get { return _images; }
        }

        private bool ShouldSerializeImages()
        {
            return !_images.IsDefault;
        }

        /// <summary>
        /// Gets access to the common gallery appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common gallery appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGalleryRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
            
        /// <summary>
        /// Gets access to the disabled gallery appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled gallery appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGalleryState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal gallery appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal gallery appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGalleryState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the active gallery appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining active gallery appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGalleryState StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
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
        /// Bring the selected index into view.
        /// </summary>
        public void BringIntoView()
        {
            BringIntoView(SelectedIndex);
        }

        /// <summary>
        /// Bring the specified image index into view.
        /// </summary>
        /// <param name="index">Index to bring into view.</param>
        public void BringIntoView(int index)
        {
            // Get number of images available
            int images = (_imageList != null) ? _imageList.Images.Count : 0;

            // Check the index is within range of what we actually have
            if ((index >= 0) && (index < images))
                _drawItems.BringIntoView(index);
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
                    return (DesignMode || AlwaysActive || ContainsFocus || _mouseOver);
            }
        }

        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the ImageListChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnImageListChanged(EventArgs e)
        {
            if (ImageListChanged != null)
                ImageListChanged(this, e);
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
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">An ImageSelectEventArgs containing the event data.</param>
        protected virtual void OnTrackingImage(ImageSelectEventArgs e)
        {
            _eventTrackingIndex = e.ImageIndex;
            if (TrackingImage != null)
                TrackingImage(this, e);
        }

        /// <summary>
        /// Raises the GalleryDropMenu event.
        /// </summary>
        /// <param name="e">An GalleryDropMenuEventArgs containing the event data.</param>
        protected virtual void OnGalleryDropMenu(GalleryDropMenuEventArgs e)
        {
            if (GalleryDropMenu != null)
                GalleryDropMenu(this, e);
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Raises the PaddingChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            PerformNeedPaint(true);
            base.OnPaddingChanged(e);
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
        /// Raises the MouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseOver = true;
            PerformNeedPaint(true);
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
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            // If there are some images displayed
            if ((_imageList != null) && (_imageList.Images.Count > 0))
            {
                if (TrackingIndex < 0)
                {
                    // Use the selected index if it matches a visible item, otherwise default to first item
                    if ((SelectedIndex < _imageList.Images.Count) && (SelectedIndex >= 0))
                        SetTrackingIndex(SelectedIndex, true);
                    else
                        SetTrackingIndex(0, true);
                }
            }

            PerformNeedPaint(true);
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // When losing the focus we lose the tracking index
            PerformNeedPaint(true);
            base.OnLostFocus(e);
            SetTrackingIndex(-1, false);
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Only interested in keys if we have the focus
            if (ContainsFocus)
            {
                // If nothing currently has the tracking index
                if (_trackingIndex == -1)
                {
                    // If it is possible to give tracking to an item
                    if ((_imageList != null) && (_imageList.Images.Count > 0))
                    {
                        // Use the selected index if it matches a visible item, otherwise default to first item
                        if ((SelectedIndex < _imageList.Images.Count) && (SelectedIndex >= 0))
                            SetTrackingIndex(SelectedIndex, true);
                        else
                            SetTrackingIndex(0, true);

                        return true;
                    }
                }
                else
                {
                    // We pass movements keys onto the view
                    switch (keyData)
                    {
                        case Keys.Up:
                        case Keys.Down:
                        case Keys.Left:
                        case Keys.Right:
                        case Keys.Home:
                        case Keys.End:
                        case Keys.PageDown:
                        case Keys.PageUp:
                            // If inside a ribbon then we ignore the movement keys
                            if ((_ribbon == null) || ((_ribbon != null) && !_ribbon.InKeyboardMode))
                            {
                                _drawItems[_trackingIndex].KeyDown(new KeyEventArgs(keyData));
                                return true;
                            }
                            break;
                        case Keys.Space:
                        case Keys.Enter:
                            _drawItems[_trackingIndex].KeyDown(new KeyEventArgs(keyData));
                            return true;
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (IsHandleCreated)
                UpdateStateAndPalettes();
            
            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Work out if this control needs to paint transparent areas.
        /// </summary>
        /// <returns>True if paint required; otherwise false.</returns>
        protected override bool EvalTransparentPaint()
        {
            // Always need to draw the background because always transparent
            return true;
        }

        /// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(240, 30); }
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
        internal int TrackingIndex
        {
            get { return _trackingIndex; }
            
            set 
            {
                if (_trackingIndex != value)
                {
                    _trackingIndex = value;

                    // Must stop and then start to restart the length of time passing
                    _cacheTrackingIndex = _trackingIndex;
                    _trackingEventTimer.Stop();
                    _trackingEventTimer.Start();

                    PerformNeedPaint(true);
                }
            }
        }

        internal void SetTrackingIndex(int index, bool bringIntoView)
        {
            // Set the actual tracking index
            TrackingIndex = index;

            // Find the target view and bring it into view
            if (_trackingIndex != -1)
                if (bringIntoView)
                    BringIntoView(_trackingIndex);
        }

        internal bool InTransparentDesignMode
        {
            get { return InRibbonDesignMode; }
        }

        internal bool DesignerGetHitTest(Point pt)
        {
            return false;
        }

        internal Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        internal void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }

        internal Size InternalPreferredItemSize
        {
            get { return _preferredItemSize; }
            set { _preferredItemSize = value; }
        }

        internal KryptonRibbon Ribbon
        {
            get { return _ribbon; }
            set { _ribbon = value; }
        }

        internal void OnDropButton()
        {
            ShownGalleryDropDown(RectangleToScreen(ClientRectangle),
                                 KryptonContextMenuPositionH.Left,
                                 KryptonContextMenuPositionV.Top,
                                 null,
                                 _drawItems.ActualLineItems);
        }

        internal void ShownGalleryDropDown(Rectangle screenRect,
                                           KryptonContextMenuPositionH hPosition,
                                           KryptonContextMenuPositionV vPosition,
                                           EventHandler finishDelegate,
                                           int actualLineItems)
        {
            // First time around create the context menu, otherwise just clear it down
            if (_dropMenu == null)
                _dropMenu = new KryptonContextMenu();

            // Number of line items equals the number actually used
            int lineItems = Math.Max(DropMinItemWidth, Math.Min(DropMaxItemWidth, actualLineItems));

            // If there are no ranges defined, just add a single entry showing all enties
            if (_dropButtonRanges.Count == 0)
            {
                KryptonContextMenuImageSelect imageSelect = new KryptonContextMenuImageSelect();
                imageSelect.ImageList = ImageList;
                imageSelect.ImageIndexStart = 0;
                imageSelect.ImageIndexEnd = (ImageList == null ? 0 : ImageList.Images.Count - 1);
                imageSelect.SelectedIndex = SelectedIndex;
                imageSelect.LineItems = lineItems;
                _dropMenu.Items.Add(imageSelect);
            }
            else
            {
                foreach (KryptonGalleryRange range in _dropButtonRanges)
                {
                    // If not the first item in the menu, add a separator
                    if (_dropMenu.Items.Count > 0)
                        _dropMenu.Items.Add(new KryptonContextMenuSeparator());

                    // Only add a heading if the heading text is not empty
                    if (!string.IsNullOrEmpty(range.Heading))
                    {
                        KryptonContextMenuHeading heading = new KryptonContextMenuHeading();
                        heading.Text = range.Heading;
                        _dropMenu.Items.Add(heading);
                    }

                    // Add the image select for the range
                    KryptonContextMenuImageSelect imageSelect = new KryptonContextMenuImageSelect();
                    imageSelect.ImageList = ImageList;
                    imageSelect.ImageIndexStart = Math.Max(0, range.ImageIndexStart);
                    imageSelect.ImageIndexEnd = Math.Min(range.ImageIndexEnd, (ImageList == null ? 0 : ImageList.Images.Count - 1));
                    imageSelect.SelectedIndex = SelectedIndex;
                    imageSelect.LineItems = lineItems;
                    _dropMenu.Items.Add(imageSelect);
                }
            }

            // Give event handler a change to modify the menu
            GalleryDropMenuEventArgs args = new GalleryDropMenuEventArgs(_dropMenu);
            OnGalleryDropMenu(args);

            if (!args.Cancel && CommonHelper.ValidKryptonContextMenu(args.KryptonContextMenu))
            {
                // Hook into relevant events of the image select areas
                foreach (KryptonContextMenuItemBase item in _dropMenu.Items)
                    if (item is KryptonContextMenuImageSelect)
                    {
                        KryptonContextMenuImageSelect itemSelect = (KryptonContextMenuImageSelect)item;
                        itemSelect.SelectedIndexChanged += new EventHandler(OnDropImageSelect);
                        itemSelect.TrackingImage += new EventHandler<ImageSelectEventArgs>(OnDropImageTracking);
                    }

                // Need to know when the menu is dismissed
                args.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnDropMenuClosed);

                // Remember the delegate we need to fire when the menu is dismissed
                _finishDelegate = finishDelegate;

                // Show the menu to the user
                args.KryptonContextMenu.Show(this, screenRect, hPosition, vPosition);
            }
            else
            {
                // Nothing to show, but still need to call the finished delegate?
                if (finishDelegate != null)
                    finishDelegate(this, EventArgs.Empty);
            }
        }

        private void OnDropMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (_dropMenu != null)
            {
                // Remove any tracking caused by the drop down menu
                TrackingIndex = -1;

                // Unhook from events
                _dropMenu.Closed -= new ToolStripDropDownClosedEventHandler(OnDropMenuClosed);

                // Unhook from the image select events
                foreach (KryptonContextMenuItemBase item in _dropMenu.Items)
                    if (item is KryptonContextMenuImageSelect)
                    {
                        KryptonContextMenuImageSelect itemSelect = (KryptonContextMenuImageSelect)item;
                        itemSelect.SelectedIndexChanged -= new EventHandler(OnDropImageSelect);
                        itemSelect.TrackingImage -= new EventHandler<ImageSelectEventArgs>(OnDropImageTracking);
                    }

                // Remove all items from the menu
                _dropMenu.Items.Clear();
                _dropMenu.Dispose();
                _dropMenu = null;

                // Do we need to fire a delegate stating the menu has been dismissed?
                if (_finishDelegate != null)
                {
                    _finishDelegate(this, e);
                    _finishDelegate = null;
                }
            }
        }
        #endregion

        #region Implementation
        private void OnDropImageSelect(object sender, EventArgs e)
        {
            KryptonContextMenuImageSelect imageSelect = (KryptonContextMenuImageSelect)sender;
            SelectedIndex = imageSelect.SelectedIndex;
        }

        private void OnDropImageTracking(object sender, ImageSelectEventArgs e)
        {
            KryptonContextMenuImageSelect imageSelect = (KryptonContextMenuImageSelect)sender;
            TrackingIndex = e.ImageIndex;
        }

        private void UpdateStateAndPalettes()
        {
            // Update state values used for the background/border handler
            _backBorder.SetState(GetGalleryState());

            // Update enabled state
            _drawDocker.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state;
            if (IsActive)
                state = PaletteState.Tracking;
            else
                state = PaletteState.Normal;

            _drawDocker.ElementState = state;
        }

        private PaletteGalleryState GetGalleryState()
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

        private void OnTrackingTick(object sender, EventArgs e)
        {
            // If no change in tracking index over last interval
            if (_trackingIndex == _cacheTrackingIndex)
            {
                // Kill timer and generate the change event
                _trackingEventTimer.Stop();

                // But only generate if actual event would yield a different value
                if (_eventTrackingIndex != _trackingIndex)
                    OnTrackingImage(new ImageSelectEventArgs(_imageList, _trackingIndex));
            }
            else
            {
                // Cache the updated value and wait for next tick before generating event
                _cacheTrackingIndex = _trackingIndex;
            }
        }
        #endregion
    }
}
