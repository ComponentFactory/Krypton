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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a ribbon group gallery.
	/// </summary>
    internal class ViewDrawRibbonGroupGallery : ViewComposite,
                                                IRibbonViewGroupContainerView
    {
        #region Static Fields
        private static readonly int NULL_CONTROL_WIDTH = 50;
        private static readonly Padding _largeImagePadding = new Padding(3, 2, 3, 3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupGallery _ribbonGallery;
        private ViewDrawRibbonGroup _activeGroup;
        private GalleryController _controller;
        private NeedPaintHandler _needPaint;
        private GroupItemSize _currentSize;
        private ViewDrawRibbonGroupButtonBackBorder _viewLarge;
        private ViewLayoutRibbonRowCenter _viewLargeCenter;
        private ViewDrawRibbonGroupGalleryImage _viewLargeImage;
        private ViewDrawRibbonGroupGalleryText _viewLargeText1;
        private ViewDrawRibbonGroupGalleryText _viewLargeText2;
        private ViewDrawRibbonDropArrow _viewLargeDropArrow;
        private ViewLayoutRibbonSeparator _viewLargeText2Sep1;
        private ViewLayoutRibbonSeparator _viewLargeText2Sep2;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupGallery class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGallery">Reference to source gallery.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupGallery(KryptonRibbon ribbon,
                                          KryptonRibbonGroupGallery ribbonGallery,
                                          NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGallery != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonGallery = ribbonGallery;
            _needPaint = needPaint;
            _currentSize = _ribbonGallery.ItemSizeCurrent;

            // Create the button view used in small setting
            CreateLargeButtonView();

            // Hook into the gallery events
            _ribbonGallery.MouseEnterControl += new EventHandler(OnMouseEnterControl);
            _ribbonGallery.MouseLeaveControl += new EventHandler(OnMouseLeaveControl);

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonGallery;

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the gallery
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                MouseController = controller;
            }

            // Create controller needed for handling focus and key tip actions
            _controller = new GalleryController(_ribbon, _ribbonGallery, this);
            SourceController = _controller;
            KeyController = _controller;

            // We need to rest visibility of the gallery for each layout cycle
            _ribbon.ViewRibbonManager.LayoutBefore += new EventHandler(OnLayoutAction);
            _ribbon.ViewRibbonManager.LayoutAfter += new EventHandler(OnLayoutAction);

            // Define back reference to view for the gallery definition
            _ribbonGallery.GalleryView = this;

            // Give paint delegate to gallery so its palette changes are redrawn
            _ribbonGallery.ViewPaintDelegate = needPaint;

            // Hook into changes in the ribbon custom definition
            _ribbonGallery.PropertyChanged += new PropertyChangedEventHandler(OnGalleryPropertyChanged);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupGallery:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ribbonGallery != null)
                {
                    // Must unhook to prevent memory leaks
                    if (_ribbonGallery.LastGallery != null)
                        _ribbonGallery.LastGallery.Ribbon = null;

                    _ribbonGallery.MouseEnterControl -= new EventHandler(OnMouseEnterControl);
                    _ribbonGallery.MouseLeaveControl -= new EventHandler(OnMouseLeaveControl);
                    _ribbonGallery.ViewPaintDelegate = null;
                    _ribbonGallery.PropertyChanged -= new PropertyChangedEventHandler(OnGalleryPropertyChanged);
                    _ribbon.ViewRibbonManager.LayoutAfter -= new EventHandler(OnLayoutAction);
                    _ribbon.ViewRibbonManager.LayoutBefore -= new EventHandler(OnLayoutAction);

                    // Remove association with definition
                    _ribbonGallery.GalleryView = null; 
                    _ribbonGallery = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region KeyTipSelect
        /// <summary>
        /// Perform action expected when a key tip is used to select the item.
        /// </summary>
        public void KeyTipSelect()
        {
            if (_ribbonGallery.LastGallery != null)
            {
                _ribbonGallery.LastGallery.ShownGalleryDropDown(_ribbonGallery.LastGallery.RectangleToScreen(_ribbonGallery.LastGallery.ClientRectangle),
                                                                KryptonContextMenuPositionH.Left,
                                                                KryptonContextMenuPositionV.Top,
                                                                null,
                                                                _ribbonGallery.DropButtonItemWidth);
            }
        }
        #endregion

        #region GroupGallery
        /// <summary>
        /// Gets access to the owning group gallery instance.
        /// </summary>
        public KryptonRibbonGroupGallery GroupGallery
        {
            get { return _ribbonGallery; }
        }
        #endregion

        #region LostFocus
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void LostFocus(Control c)
        {
            // Ask ribbon to shift focus to the embedded control
            _ribbon.HideFocus(_ribbonGallery.Gallery);
            base.LostFocus(c);
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            if (_viewLarge.Visible)
            {
                if (_ribbonGallery.Visible && _ribbonGallery.Enabled)
                    return _viewLarge;
            }
            else
            {
                if ((_ribbonGallery.Visible) &&
                    (_ribbonGallery.LastGallery != null) &&
                    (_ribbonGallery.LastGallery.CanSelect))
                    return this;
            }
                
            return null;
        }
        #endregion

        #region GetLastFocusItem
        /// <summary>
        /// Gets the last focus item from the item.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetLastFocusItem()
        {
            if (_viewLarge.Visible)
            {
                if (_ribbonGallery.Visible && _ribbonGallery.Enabled)
                    return _viewLarge;
            }
            else
            {
                if ((_ribbonGallery.Visible) &&
                    (_ribbonGallery.LastGallery != null) &&
                    (_ribbonGallery.LastGallery.CanSelect))
                    return this;
            }

            return null;
        }
        #endregion

        #region GetNextFocusItem
        /// <summary>
        /// Gets the next focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetNextFocusItem(ViewBase current, ref bool matched)
        {
            matched = ((current == this) || (current == _viewLarge));
            return null;
        }
        #endregion

        #region GetPreviousFocusItem
        /// <summary>
        /// Gets the previous focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched)
        {
            matched = ((current == this) || (current == _viewLarge));
            return null;
        }
        #endregion

        #region GetGroupKeyTips
        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <param name="keyTipList">List to add new entries into.</param>
        public void GetGroupKeyTips(KeyTipInfoList keyTipList)
        {
            if (_ribbonGallery.Visible)
            {
                if (_viewLarge.Visible)
                {
                    // Get the screen location of the button
                    Rectangle viewRect = _ribbon.KeyTipToScreen(_viewLarge); 
                    keyTipList.Add(new KeyTipInfo(_ribbonGallery.Enabled,
                                                  _ribbonGallery.KeyTip,
                                                  new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom),
                                                  ClientRectangle,
                                                  _viewLarge.Controller));
                }
                else if (LastGallery.CanFocus)
                {
                    // Get the screen location of the button
                    Rectangle viewRect = _ribbon.KeyTipToScreen(this);
                    keyTipList.Add(new KeyTipInfo(_ribbonGallery.Enabled,
                                                  _ribbonGallery.KeyTip,
                                                  new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom),
                                                  ClientRectangle,
                                                  _controller));
                }
            }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Gets an array of the allowed possible sizes of the container.
        /// </summary>
        /// <param name="context">Context used to calculate the sizes.</param>
        /// <returns>Array of size values.</returns>
        public ItemSizeWidth[] GetPossibleSizes(ViewLayoutContext context)
        {
            // Ensure the control has the correct parent
            UpdateParent(context.Control);

            if (LastGallery != null)
            {
                Size originalItemSize = LastGallery.PreferredItemSize;
                GroupItemSize originalSize = _currentSize;

                // Create a list of results
                List<ItemSizeWidth> results = new List<ItemSizeWidth>();

                // Are we allowed to be in the large size?
                if (_ribbonGallery.ItemSizeMaximum == GroupItemSize.Large)
                {
                    // Allow a maximum of 39 steps between the large and medium values (with a minimum of 1)
                    int step = Math.Max(1, (_ribbonGallery.LargeItemCount - _ribbonGallery.MediumItemCount) / 20);

                    // Process each step from large to medium
                    int itemCount = _ribbonGallery.LargeItemCount;
                    while (itemCount > _ribbonGallery.MediumItemCount)
                    {
                        LastGallery.InternalPreferredItemSize = new Size(itemCount, 1);
                        results.Add(new ItemSizeWidth(GroupItemSize.Large, GetPreferredSize(context).Width, itemCount));
                        itemCount -= step;
                    }
                }

                // Are we allowed to be in the medium size?
                if (((int)_ribbonGallery.ItemSizeMaximum >= (int)GroupItemSize.Medium) &&
                    ((int)_ribbonGallery.ItemSizeMinimum <= (int)GroupItemSize.Medium))
                {
                    LastGallery.InternalPreferredItemSize = new Size(_ribbonGallery.MediumItemCount, 1);
                    ItemSizeWidth mediumWidth = new ItemSizeWidth(GroupItemSize.Medium, GetPreferredSize(context).Width);

                    if (_ribbon.InDesignHelperMode)
                    {
                        // Only add if we are the first calculation, as in design mode we
                        // always provide a single possible size which is the largest item
                        if (results.Count == 0)
                            results.Add(mediumWidth);
                    }
                    else
                    {
                        // Only add the medium size if there is no other entry or we are
                        // smaller than the existing size and so represent a useful shrinkage
                        if ((results.Count == 0) || (results[results.Count - 1].Width > mediumWidth.Width))
                            results.Add(mediumWidth);
                    }
                }

                // Are we allowed to be in the small size?
                if ((int)_ribbonGallery.ItemSizeMinimum == (int)GroupItemSize.Small)
                {
                    // Temporary set the item size to be size
                    _viewLarge.Visible = true;
                    _currentSize = GroupItemSize.Small;

                    // Get the width of the large button view
                    ItemSizeWidth smallWidth = new ItemSizeWidth(GroupItemSize.Small, GetPreferredSize(context).Width);

                    if (_ribbon.InDesignHelperMode)
                    {
                        // Only add if we are the first calculation, as in design mode we
                        // always provide a single possible size which is the largest item
                        if (results.Count == 0)
                            results.Add(smallWidth);
                    }
                    else
                    {
                        // Only add the medium size if there is no other entry or we are
                        // smaller than the existing size and so represent a useful shrinkage
                        if ((results.Count == 0) || (results[results.Count - 1].Width > smallWidth.Width))
                            results.Add(smallWidth);
                    }
                }

                // Ensure original value is put back
                LastGallery.InternalPreferredItemSize = originalItemSize;
                _currentSize = originalSize;

                return results.ToArray();
            }
            else
                return new ItemSizeWidth[] { new ItemSizeWidth(GroupItemSize.Large, NULL_CONTROL_WIDTH) };
        }

        /// <summary>
        /// Update the group with the provided sizing solution.
        /// </summary>
        /// <param name="size">Value for the container.</param>
        public void SetSolutionSize(ItemSizeWidth size)
        {
            // Update the container definition
            _ribbonGallery.ItemSizeCurrent = size.GroupItemSize;
            _ribbonGallery.InternalItemCount = size.Tag;
            _viewLarge.Visible = (size.GroupItemSize == GroupItemSize.Small);
        }

        /// <summary>
        /// Reset the container back to its requested size.
        /// </summary>
        public void ResetSolutionSize()
        {
            // Restore the container back to the defined size
            _ribbonGallery.ItemSizeCurrent = _ribbonGallery.ItemSizeMaximum;
            _ribbonGallery.InternalItemCount = _ribbonGallery.LargeItemCount;
            _viewLarge.Visible = (_ribbonGallery.ItemSizeCurrent == GroupItemSize.Small);
        }

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size preferredSize = Size.Empty;

            // Ensure the control has the correct parent
            UpdateParent(context.Control);

            if (_currentSize == GroupItemSize.Small)
                preferredSize = base.GetPreferredSize(context);
            else
            {
                // If there is a gallery associated then ask for its requested size
                if (LastGallery != null)
                {
                    if (ActualVisible(LastGallery))
                    {
                        preferredSize = LastGallery.GetPreferredSize(context.DisplayRectangle.Size);

                        // Add two pixels, one for the left and right edges that will be padded
                        preferredSize.Width += 2;
                    }
                }
                else
                    preferredSize.Width = NULL_CONTROL_WIDTH;
            }

            if (_currentSize == GroupItemSize.Large)
                preferredSize.Height = _ribbon.CalculatedValues.GroupTripleHeight;
            else
                preferredSize.Height = _ribbon.CalculatedValues.GroupLineHeight;

            return preferredSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Are we allowed to change the layout of controls?
            if (!context.ViewManager.DoNotLayoutControls)
            {
                // If we have an actual control, position it with a pixel padding all around
                if (LastGallery != null)
                {
                    LastGallery.SetBounds(ClientLocation.X + 1,
                                          ClientLocation.Y + 1,
                                          ClientWidth - 2,
                                          ClientHeight - 2);
                }
            }

            // Let child elements layout in given space
            base.Layout(context);
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);

            // If we do not have a gallery
            if (_ribbonGallery.Gallery == null)
            {
                // And we are in design time
                if (_ribbon.InDesignMode)
                {
                    // Draw rectangle is 1 pixel less per edge
                    Rectangle drawRect = ClientRectangle;
                    drawRect.Inflate(-1, -1);
                    drawRect.Height--;

                    // Draw an indication of where the gallery will be
                    context.Graphics.FillRectangle(Brushes.Goldenrod, drawRect);
                    context.Graphics.DrawRectangle(Pens.Gold, drawRect);
                }
            }

            base.Render(context);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected virtual void OnNeedPaint(bool needLayout)
        {
            OnNeedPaint(needLayout, Rectangle.Empty);
        }

        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        /// <param name="invalidRect">Rectangle to invalidate.</param>
        protected virtual void OnNeedPaint(bool needLayout, Rectangle invalidRect)
        {
            if (_needPaint != null)
            {
                _needPaint(this, new NeedLayoutEventArgs(needLayout));

                if (needLayout)
                    _ribbon.PerformLayout();
            }
        }
        #endregion

        #region Implementation
        private void CreateLargeButtonView()
        {
            // Create the background and border view
            _viewLarge = new ViewDrawRibbonGroupButtonBackBorder(_ribbon, _ribbonGallery,
                                                                 _ribbon.StateCommon.RibbonGroupButton.PaletteBack,
                                                                 _ribbon.StateCommon.RibbonGroupButton.PaletteBorder,
                                                                 false, _needPaint);
            _viewLarge.ButtonType = GroupButtonType.DropDown;
            _viewLarge.DropDown += new EventHandler(OnLargeButtonDropDown);

            if (_ribbon.InDesignMode)
                _viewLarge.ContextClick += new MouseEventHandler(OnContextClick);

            // Create the layout docker for the contents of the button
            ViewLayoutDocker contentLayout = new ViewLayoutDocker();

            // Add the large button at the top
            _viewLargeImage = new ViewDrawRibbonGroupGalleryImage(_ribbon, _ribbonGallery);
            ViewLayoutRibbonCenterPadding largeImagePadding = new ViewLayoutRibbonCenterPadding(_largeImagePadding);
            largeImagePadding.Add(_viewLargeImage);
            contentLayout.Add(largeImagePadding, ViewDockStyle.Top);

            // Add the first line of text
            _viewLargeText1 = new ViewDrawRibbonGroupGalleryText(_ribbon, _ribbonGallery, true);
            contentLayout.Add(_viewLargeText1, ViewDockStyle.Bottom);

            // Add the second line of text
            _viewLargeCenter = new ViewLayoutRibbonRowCenter();
            _viewLargeText2 = new ViewDrawRibbonGroupGalleryText(_ribbon, _ribbonGallery, false);
            _viewLargeDropArrow = new ViewDrawRibbonDropArrow(_ribbon);
            _viewLargeText2Sep1 = new ViewLayoutRibbonSeparator(4, false);
            _viewLargeText2Sep2 = new ViewLayoutRibbonSeparator(4, false);
            _viewLargeCenter.Add(_viewLargeText2);
            _viewLargeCenter.Add(_viewLargeText2Sep1);
            _viewLargeCenter.Add(_viewLargeDropArrow);
            _viewLargeCenter.Add(_viewLargeText2Sep2);
            contentLayout.Add(_viewLargeCenter, ViewDockStyle.Bottom);

            // Add a 1 pixel separator at bottom of button before the text
            contentLayout.Add(new ViewLayoutRibbonSeparator(1, false), ViewDockStyle.Bottom);

            // Add the content into the background and border
            _viewLarge.Add(contentLayout);

            // Create controller for intercepting events to determine tool tip handling
            _viewLarge.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager,
                                                               _viewLarge, _viewLarge.MouseController);

            // Add as a child view but as hidden, will become visible only in small mode
            _viewLarge.Visible = false;
            Add(_viewLarge);
        }

        private void OnLargeButtonDropDown(object sender, EventArgs e)
        {
            if (_ribbonGallery.LastGallery != null)
            {
                _ribbonGallery.LastGallery.ShownGalleryDropDown(_ribbon.ViewRectangleToScreen(_viewLarge),
                                                                KryptonContextMenuPositionH.Left,
                                                                KryptonContextMenuPositionV.Below,
                                                                _viewLarge.FinishDelegate,
                                                                _ribbonGallery.DropButtonItemWidth);
            }
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            _ribbonGallery.OnDesignTimeContextMenu(e);
        }

        private void OnGalleryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "TextLine1":
                    _viewLargeText1.MakeDirty();
                    updateLayout = true;
                    break;
                case "TextLine2":
                    _viewLargeText2.MakeDirty();
                    updateLayout = true;
                    break;
                case "ImageLarge":
                case "ImageList":
                case "LargeItemCount":
                case "MediumItemCount":
                case "ItemSizeMinimum":
                case "ItemSizeMaximum":
                case "ItemSizeCurrent":
                    updateLayout = true;
                    break;
                case "Enabled":
                    UpdateEnabled(LastGallery);
                    break;
                case "Visible":
                    UpdateVisible(LastGallery);
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonGallery.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonGallery.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this button is actually defined as visible...
                if (_ribbonGallery.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonGallery.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonGallery.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
        }

        private Control LastParentControl
        {
            get { return _ribbonGallery.LastParentControl; }
            set { _ribbonGallery.LastParentControl = value; }
        }

        private KryptonGallery LastGallery
        {
            get { return _ribbonGallery.LastGallery; }
            set { _ribbonGallery.LastGallery = value; }
        }

        private void UpdateParent(Control parentControl)
        {
            // Is there a change in the gallery or a change in 
            // the parent control that is hosting the control...
            if ((parentControl != LastParentControl) ||
                (LastGallery != _ribbonGallery.Gallery))
            {
                // We only modify the parent and visible state if processing for correct container
                if ((_ribbonGallery.RibbonGroup.ShowingAsPopup && (parentControl is VisualPopupGroup)) ||
                    (!_ribbonGallery.RibbonGroup.ShowingAsPopup && !(parentControl is VisualPopupGroup)))
                {
                    // If we have added the custrom control to a parent before
                    if ((LastGallery != null) && (LastParentControl != null))
                    {
                        // If that control is still a child of the old parent
                        if (LastParentControl.Controls.Contains(LastGallery))
                        {
                            // Check for a collection that is based on the read only class
                            LastParentControl.Controls.Remove(LastGallery);
                        }
                    }

                    // Remove ribbon reference from old last gallery reference
                    if (LastGallery != null)
                        LastGallery.Ribbon = null;

                    // Remember the current control and new parent
                    LastGallery = _ribbonGallery.Gallery;
                    LastParentControl = parentControl;

                    // Add ribbon reference to new gallery reference
                    if (LastGallery != null)
                        LastGallery.Ribbon = _ribbon;

                    // If we have a new gallery and parent
                    if ((LastGallery != null) && (LastParentControl != null))
                    {
                        // Ensure the control is not in the display area when first added
                        LastGallery.Location = new Point(-LastGallery.Width, -LastGallery.Height);

                        // Check for the correct visible state of the gallery
                        UpdateVisible(LastGallery);
                        UpdateEnabled(LastGallery);

                        // Check for a collection that is based on the read only class
                        LastParentControl.Controls.Add(LastGallery);
                    }
                }
            }
        }

        private void UpdateEnabled(Control c)
        {
            if (c != null)
            {
                // Start with the enabled state of the group element
                bool enabled = _ribbonGallery.Enabled;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonGallery.GalleryDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    enabled = _ribbonGallery.GalleryDesigner.DesignEnabled;
                }

                c.Enabled = enabled;
            }
        }

        private bool ActualVisible(Control c)
        {
            if (c != null)
            {
                // Start with the visible state of the group element
                bool visible = _ribbonGallery.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonGallery.GalleryDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = _ribbonGallery.GalleryDesigner.DesignVisible;
                }

                return visible;
            }

            return false;
        }

        private void UpdateVisible(Control c)
        {
            if (c != null)
            {
                // Start with the visible state of the group element
                bool visible = _ribbonGallery.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonGallery.GalleryDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = _ribbonGallery.GalleryDesigner.DesignVisible;
                }

                if (visible)
                {
                    // Only visible if on the currently selected page
                    if ((_ribbonGallery.RibbonTab == null) ||
                        (_ribbon.SelectedTab != _ribbonGallery.RibbonTab))
                        visible = false;
                    else
                    {
                        // Check the owning group is visible
                        if ((_ribbonGallery.RibbonGroup != null) &&
                            !_ribbonGallery.RibbonGroup.Visible &&
                            !_ribbon.InDesignMode)
                            visible = false;
                        else
                        {
                            // Check that the group is not collapsed
                            if ((_ribbonGallery.RibbonGroup.IsCollapsed) &&
                                ((_ribbon.GetControllerControl(_ribbonGallery.Gallery) is KryptonRibbon) ||
                                 (_ribbon.GetControllerControl(_ribbonGallery.Gallery) is VisualPopupMinimized)))
                                visible = false;
                        }
                    }
                }

                c.Visible = (visible && (_ribbonGallery.ItemSizeCurrent != GroupItemSize.Small));
            }
        }

        private void OnLayoutAction(object sender, EventArgs e)
        {
            // If not disposed then we still have a element reference
            if (_ribbonGallery != null)
            {
                // Change in selected tab requires a retest of the control visibility
                UpdateVisible(LastGallery);
                UpdateEnabled(LastGallery);
            }
        }

        private void OnMouseEnterControl(object sender, EventArgs e)
        {
            // Reset the active group setting
            _activeGroup = null;

            // Find the parent group instance
            ViewBase parent = Parent;

            // Keep going till we get to the top or find a group
            while (parent != null)
            {
                if (parent is ViewDrawRibbonGroup)
                {
                    _activeGroup = (ViewDrawRibbonGroup)parent;
                    break;
                }

                // Move up a level
                parent = parent.Parent;
            }

            // If we found a group we are inside
            if (_activeGroup != null)
            {
                _activeGroup.Tracking = true;
                _needPaint(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
            }
        }

        private void OnMouseLeaveControl(object sender, EventArgs e)
        {
            // If we have a cached group we made active
            if (_activeGroup != null)
            {
                _activeGroup.Tracking = false;
                _needPaint(this, new NeedLayoutEventArgs(false, _activeGroup.ClientRectangle));
                _activeGroup = null;
            }
        }
        #endregion
    }
}
