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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Represents a shortcut menu with Krypton palette styling.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonContextMenu), "ToolboxBitmaps.KryptonContextMenu.bmp")]
    [DefaultEvent("Opening")]
    [DefaultProperty("PaletteMode")]
    [DesignerCategory("code")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonContextMenuDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [Description("Displays a shortcut menu in popup window.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonContextMenu : Component
    {
        #region Instance Fields
        private IPalette _localPalette;
        private PaletteMode _paletteMode;
        private VisualContextMenu _contextMenu;
        private PaletteContextMenuRedirect _stateCommon;
        private PaletteContextMenuItemState _stateNormal;
        private PaletteContextMenuItemState _stateDisabled;
        private PaletteContextMenuItemStateHighlight _stateHighlight;
        private PaletteContextMenuItemStateChecked _stateChecked;
        private PaletteRedirectContextMenu _redirectorImages;
        private PaletteRedirect _redirector;
        private NeedPaintHandler _needPaintDelegate;
        private KryptonContextMenuCollection _items;
        private ToolStripDropDownCloseReason _closeReason;
        private ContextMenuImages _images;
        private bool _enabled;
        private object _caller;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the context menu is opening.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when context menu is opening but not displayed as yet.")]
        public event CancelEventHandler Opening;

        /// <summary>
        /// Occurs when the context menu is opended.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the context menu is fully opened for display.")]
        public event EventHandler Opened;

        /// <summary>
        /// Occurs when the context menu is about to close.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the context menu is about to close.")]
        public event CancelEventHandler Closing;

        /// <summary>
        /// Occurs when the context menu has been closed.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the context menu has been closed.")]
        public event ToolStripDropDownClosedEventHandler Closed;
        #endregion

        #region Identity
        /// <summary>
        ///  Initialize a new instance of the KryptonContextMenu class.
        /// </summary>
        public KryptonContextMenu()
        {
            // Setup the need paint delegate
            _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);

            // Set default settings
            _localPalette = null;
            _paletteMode = PaletteMode.Global;
            _images = new ContextMenuImages(_needPaintDelegate);
            _redirector = new PaletteRedirect(null);
            _redirectorImages = new PaletteRedirectContextMenu(_redirector, _images);
            _enabled = true;

            // Create the palette storage
            _stateCommon = new PaletteContextMenuRedirect(_redirector, _needPaintDelegate);
            _stateNormal = new PaletteContextMenuItemState(_stateCommon);
            _stateDisabled = new PaletteContextMenuItemState(_stateCommon);
            _stateHighlight = new PaletteContextMenuItemStateHighlight(_stateCommon);
            _stateChecked = new PaletteContextMenuItemStateChecked(_stateCommon);

            // Create the top level collection for menu items
            _items = new KryptonContextMenuCollection();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Close();

            base.Dispose(disposing);
        }        
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the image value overrides.
        /// </summary>
        [Category("Visuals")]
        [Description("Image value overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ContextMenuImages Images
        {
            get { return _images; }
        }

        private bool ShouldSerializeImages()
        {
            return !_images.IsDefault;
        }
        
        /// <summary>
        /// Gets access to the common context menu appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common context menu appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the context menu disabled appearance values.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining context menu disabled appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the context menu normal appearance values.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for context menu item normal appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the context menu normal appearance values.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining context menu checked appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateChecked StateChecked
        {
            get { return _stateChecked; }
        }

        private bool ShouldSerializeStateChecked()
        {
            return !_stateChecked.IsDefault;
        }

        /// <summary>
        /// Gets access to the context menu highlight appearance values.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining context menu highlight appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateHighlight StateHighlight
        {
            get { return _stateHighlight; }
        }

        private bool ShouldSerializeStateHighlight()
        {
            return !_stateHighlight.IsDefault;
        }

        /// <summary>
        /// Collection of menu items for display.
        /// </summary>
        [Category("Data")]
        [Description("Collection of menu items.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonContextMenuCollection Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [Bindable(true)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private void ResetTag()
        {
            Tag = null;
        }

        private bool ShouldSerializeTag()
        {
            return (Tag != null);
        }

        /// <summary>
        /// Gets and sets if the context menu is enabled.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the context menu is enabled.")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
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
            set { _paletteMode = value; }
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
            set { _localPalette = value; }
        }

        /// <summary>
        /// Resets the Palette property to its default value.
        /// </summary>
        public void ResetPalette()
        {
            PaletteMode = PaletteMode.Global;
        }

        /// <summary>
        /// Gets a reference to the caller that caused the context menu to be shown.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Caller
        {
            get { return _caller; }
        }

        /// <summary>
        /// Show the context menu at the current mouse location.
        /// </summary>
        /// <returns>Has the context menu become displayed.</returns>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        public bool Show(object caller)
        {
            // Without a screen location we just place it at the same location as the mouse
            return Show(caller, Control.MousePosition);
        }

        /// <summary>
        /// Show the context menu relative to the current mouse location.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="horz">Horizontal location relative to screen rectangle.</param>
        /// <param name="vert">Vertical location relative to screen rectangle.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         KryptonContextMenuPositionH horz,
                         KryptonContextMenuPositionV vert)
        {
            // Without a screen location we just place it at the same location as the mouse
            return Show(caller, Control.MousePosition, horz, vert);
        }


        /// <summary>
        /// Show the context menu relative to the provided screen point.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="screenPt">Screen location.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         Point screenPt)
        {
            // Convert to a zero sized rectangle
            return Show(caller, new Rectangle(screenPt, Size.Empty));
        }

        /// <summary>
        /// Show the context menu relative to the provided screen rectangle.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="screenRect">Screen rectangle.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         Rectangle screenRect)
        {
            // When the relative position is not provided we assume a default 
            // of below and aligned to the left edge of the screen rectangle.
            return Show(caller, screenRect, KryptonContextMenuPositionH.Left, KryptonContextMenuPositionV.Below);
        }

        /// <summary>
        /// Show the context menu relative to the provided screen point.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="screenPt">Screen location.</param>
        /// <param name="horz">Horizontal location relative to screen rectangle.</param>
        /// <param name="vert">Vertical location relative to screen rectangle.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         Point screenPt,
                         KryptonContextMenuPositionH horz,
                         KryptonContextMenuPositionV vert)
        {
            // When providing just a point we turn this into a rectangle that happens to
            // have a zero size. We always position relative to a screen rectangle.
            return Show(caller, new Rectangle(screenPt, Size.Empty), horz, vert);
        }

        /// <summary>
        /// Show the context menu relative to the provided screen rectangle.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="screenRect">Screen rectangle.</param>
        /// <param name="horz">Horizontal location relative to screen rectangle.</param>
        /// <param name="vert">Vertical location relative to screen rectangle.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         Rectangle screenRect,
                         KryptonContextMenuPositionH horz,
                         KryptonContextMenuPositionV vert)
        {
            // By default we assume the context menu was not activated using the keyboard.
            return Show(caller, screenRect, horz, vert, false, true);
        }

        /// <summary>
        /// Show the context menu relative to the provided screen rectangle.
        /// </summary>
        /// <param name="caller">Reference to object causing the context menu to be shown.</param>
        /// <param name="screenRect">Screen rectangle.</param>
        /// <param name="horz">Horizontal location relative to screen rectangle.</param>
        /// <param name="vert">Vertical location relative to screen rectangle.</param>
        /// <param name="keyboardActivated">Was context menu initiated via a keyboard action.</param>
        /// <param name="constrain">Should size and position of menu be constrained by display size.</param>
        /// <returns>Has the context menu become displayed.</returns>
        public bool Show(object caller,
                         Rectangle screenRect,
                         KryptonContextMenuPositionH horz,
                         KryptonContextMenuPositionV vert,
                         bool keyboardActivated,
                         bool constrain)
        {
            bool displayed = false;

            // Only need to show if not already displaying it
            if (_contextMenu == null)
            {
                // Remember the caller for us in events
                _caller = caller;

                // Give event handler a change to cancel the open request
                CancelEventArgs cea = new CancelEventArgs();
                OnOpening(cea);

                if (!cea.Cancel)
                {
                    // Set a default reason for the menu being dismissed
                    _closeReason = ToolStripDropDownCloseReason.AppFocusChange;

                    // Create the actual control used to show the context menu
                    _contextMenu = CreateContextMenu(this, _localPalette, _paletteMode, 
                                                     _redirector, _redirectorImages,
                                                     Items, Enabled, keyboardActivated);

                    // Need to know when the visual control is removed
                    _contextMenu.Disposed += new EventHandler(OnContextMenuDisposed);

                    // Request the menu be shown immediately
                    _contextMenu.Show(screenRect, horz, vert, false, constrain);

                    // Override the horz, vert setting so that sub menus appear right and below
                    _contextMenu.ShowHorz = KryptonContextMenuPositionH.After;
                    _contextMenu.ShowVert = KryptonContextMenuPositionV.Top;

                    // Indicate the context menu is fully constructed and displayed
                    OnOpened(EventArgs.Empty);

                    // The menu has actually become displayed
                    displayed = true;
                }
            }

            return displayed;
        }

        /// <summary>
        /// Close any showing context menu.
        /// </summary>
        public void Close()
        {
            Close(ToolStripDropDownCloseReason.CloseCalled);
        }

        /// <summary>
        /// Close any showing context menu.
        /// </summary>
        /// <param name="reason">Reason why the context menu is being closed.</param>
        public void Close(ToolStripDropDownCloseReason reason)
        {
            // Remove any showing context menu
            if (_contextMenu != null)
            {
                _closeReason = reason;
                VisualPopupManager.Singleton.EndPopupTracking(_contextMenu);
            }
        }

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public bool ProcessShortcut(Keys keyData)
        {
            return Items.ProcessShortcut(keyData);
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Create a new visual context menu for showing the defined items.
        /// </summary>
        /// <param name="kcm">Owning KryptonContextMenu instance.</param>
        /// <param name="palette">Drawing palette.</param>
        /// <param name="paletteMode">Drawing palette mode.</param>
        /// <param name="redirector">Redirector for sourcing base values.</param>
        /// <param name="redirectorImages">Redirector for sourcing base images.</param>
        /// <param name="items">Colletion of menu items.</param>
        /// <param name="enabled">Enabled state of the menu.</param>
        /// <param name="keyboardActivated">True is menu was keyboard initiated.</param>
        /// <returns>VisualContextMenu reference.</returns>
        protected virtual VisualContextMenu CreateContextMenu(KryptonContextMenu kcm,
                                                              IPalette palette,
                                                              PaletteMode paletteMode,
                                                              PaletteRedirect redirector,
                                                              PaletteRedirectContextMenu redirectorImages,
                                                              KryptonContextMenuCollection items,
                                                              Boolean enabled,
                                                              bool keyboardActivated)
        {
            return new VisualContextMenu(kcm, palette, paletteMode, redirector, redirectorImages, items, enabled, keyboardActivated);
        }

        /// <summary>
        /// Raises the Opening event.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        protected virtual void OnOpening(CancelEventArgs e)
        {
            if (Opening != null)
                Opening(this, e);
        }

        /// <summary>
        /// Raises the Opened event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnOpened(EventArgs e)
        {
            if (Opened != null)
                Opened(this, e);
        }

        /// <summary>
        /// Raises the Closing event.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        internal protected virtual void OnClosing(CancelEventArgs e)
        {
            if (Closing != null)
                Closing(this, e);
        }

        /// <summary>
        /// Raises the Closed event.
        /// </summary>
        /// <param name="e">An ToolStripDropDownClosedEventArgs containing the event data.</param>
        protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            if (Closed != null)
                Closed(this, e);
        }
        #endregion

        #region Internal
        internal ToolStripDropDownCloseReason CloseReason
        {
            get { return _closeReason; }
            set { _closeReason = value; }
        }

        internal VisualContextMenu VisualContextMenu
        {
            get { return _contextMenu; }
        }
        #endregion

        #region Implementation
        private void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        private void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Pass request onto the displaying control if we have one
            if (_contextMenu != null)
                _contextMenu.PerformNeedPaint(e.NeedLayout);
        }

        private void OnContextMenuDisposed(object sender, EventArgs e)
        {
            // Should still be caching a reference to actual display control
            if (_contextMenu != null)
            {
                // Unhook from control, so it can be garbage collected
                _contextMenu.Disposed -= new EventHandler(OnContextMenuDisposed);

                // Copy to ourself the close reason
                if (_contextMenu.CloseReason.HasValue)
                    CloseReason = _contextMenu.CloseReason.Value;

                // No longer need to cache reference
                _contextMenu = null;

                // Notify event handlers the context menu has been closed and why it closed
                OnClosed(new ToolStripDropDownClosedEventArgs(CloseReason));
            }
        }
        #endregion
    }
}
