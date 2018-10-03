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
	/// Draws a ribbon group label.
	/// </summary>
    internal class ViewDrawRibbonGroupLabel : ViewComposite,
                                              IRibbonViewGroupItemView
    {
        #region Static Fields
        private static readonly Padding _largeImagePadding = new Padding(3, 2, 3, 3);
        private static readonly Padding _smallImagePadding = new Padding(3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupLabel _ribbonLabel;
        private NeedPaintHandler _needPaint;
        private ViewLayoutDocker _viewLarge;
        private ViewLayoutRibbonCenterPadding _viewLargeImage;
        private ViewDrawRibbonGroupLabelImage _viewLargeLabelImage;
        private ViewDrawRibbonGroupLabelText _viewLargeText1;
        private ViewDrawRibbonGroupLabelText _viewLargeText2;
        private ViewLayoutDocker _viewMediumSmall;
        private ViewLayoutRibbonRowCenter _viewMediumSmallCenter;
        private ViewLayoutRibbonCenterPadding _viewMediumSmallImage;
        private ViewDrawRibbonGroupLabelImage _viewMediumSmallLabelImage;
        private ViewDrawRibbonGroupLabelText _viewMediumSmallText1;
        private ViewDrawRibbonGroupLabelText _viewMediumSmallText2;
        private GroupItemSize _currentSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupLabel class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonLabel">Reference to source label definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupLabel(KryptonRibbon ribbon,
                                        KryptonRibbonGroupLabel ribbonLabel,
                                        NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonLabel != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonLabel = ribbonLabel;
            _needPaint = needPaint;

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonLabel;

            // Give paint delegate to label so its palette changes are redrawn
            _ribbonLabel.ViewPaintDelegate = needPaint;

            // Create the different views for different sizes of the label
            CreateLargeLabelView();
            CreateMediumSmallLabelView();

            // Update all views to reflect current label state
            UpdateEnabledState();
            UpdateImageSmallState();
            UpdateItemSizeState();

            // Hook into changes in the ribbon button definition
            _ribbonLabel.PropertyChanged += new PropertyChangedEventHandler(OnLabelPropertyChanged);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupLabel:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ribbonLabel != null)
                {
                    // Remove back reference to the paint delegate
                    _ribbonLabel.ViewPaintDelegate = null;

                    // Must unhook to prevent memory leaks
                    _ribbonLabel.PropertyChanged -= new PropertyChangedEventHandler(OnLabelPropertyChanged);

                    // Remove association with definition
                    _ribbonLabel.LabelView = null;
                    _ribbonLabel = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupLabel
        /// <summary>
        /// Gets access to the owning group label instance.
        /// </summary>
        public KryptonRibbonGroupLabel GroupLabel
        {
            get { return _ribbonLabel; }
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            // A label can never have the focus
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
            // A label can never have the focus
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
            // We have nothing to provide even if we are the selected item
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
            // We have nothing to provide even if we are the selected item
            return null;
        }
        #endregion

        #region GetGroupKeyTips
        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <param name="keyTipList">List to add new entries into.</param>
        /// <param name="lineHint">Provide hint to item about its location.</param>
        public void GetGroupKeyTips(KeyTipInfoList keyTipList, int lineHint)
        {
            // A label never has a key tip
        }
        #endregion

        #region Layout
        /// <summary>
        /// Override the group item size if possible.
        /// </summary>
        /// <param name="size">New size to use.</param>
        public void SetGroupItemSize(GroupItemSize size)
        {
            UpdateItemSizeState(size);
        }

        /// <summary>
        /// Reset the group item size to the item definition.
        /// </summary>
        public void ResetGroupItemSize()
        {
            UpdateItemSizeState();
        }

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get the preferred size of button view
            Size preferredSize = base.GetPreferredSize(context);

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

            // Update our enabled state
            UpdateEnabledState();
            UpdateImageSmallState();

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let child elements layout in given space
            base.Layout(context);
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
        private void CreateLargeLabelView()
        {
            // Create the layout docker for the contents of the label
            _viewLarge = new ViewLayoutDocker();

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the label
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                _viewLarge.MouseController = controller;
            }

            // Add the large button at the top
            _viewLargeLabelImage = new ViewDrawRibbonGroupLabelImage(_ribbon, _ribbonLabel, true);
            _viewLargeImage = new ViewLayoutRibbonCenterPadding(_largeImagePadding);
            _viewLargeImage.Add(_viewLargeLabelImage);
            _viewLarge.Add(_viewLargeImage, ViewDockStyle.Top);

            // Add the first line of text
            _viewLargeText1 = new ViewDrawRibbonGroupLabelText(_ribbon, _ribbonLabel, true);
            _viewLarge.Add(_viewLargeText1, ViewDockStyle.Bottom);

            // Add the second line of text
            _viewLargeText2 = new ViewDrawRibbonGroupLabelText(_ribbon, _ribbonLabel, false);
            _viewLarge.Add(_viewLargeText2, ViewDockStyle.Bottom);

            // Add a 1 pixel separator at bottom of button before the text
            _viewLarge.Add(new ViewLayoutRibbonSeparator(1, false), ViewDockStyle.Bottom);

            // Create controller for intercepting events to determine tool tip handling
            _viewLarge.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager,
                                                               _viewLarge, _viewLarge.MouseController);
        }

        private void CreateMediumSmallLabelView()
        {
            // Create the layout docker for the contents of the label
            _viewMediumSmall = new ViewLayoutDocker();

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the label
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                _viewMediumSmall.MouseController = controller;
            }

            // Create the image and drop down content
            _viewMediumSmallLabelImage = new ViewDrawRibbonGroupLabelImage(_ribbon, _ribbonLabel, false);
            _viewMediumSmallText1 = new ViewDrawRibbonGroupLabelText(_ribbon, _ribbonLabel, true);
            _viewMediumSmallText2 = new ViewDrawRibbonGroupLabelText(_ribbon, _ribbonLabel, false);
            _viewMediumSmallImage = new ViewLayoutRibbonCenterPadding(_smallImagePadding);
            _viewMediumSmallImage.Add(_viewMediumSmallLabelImage);

            // Layout the content in the center of a row
            _viewMediumSmallCenter = new ViewLayoutRibbonRowCenter();
            _viewMediumSmallCenter.Add(_viewMediumSmallImage);
            _viewMediumSmallCenter.Add(_viewMediumSmallText1);
            _viewMediumSmallCenter.Add(_viewMediumSmallText2);

            // Use content as only fill item
            _viewMediumSmall.Add(_viewMediumSmallCenter, ViewDockStyle.Fill);

            // Create controller for intercepting events to determine tool tip handling
            _viewMediumSmall.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager,
                                                                     _viewMediumSmall, _viewMediumSmall.MouseController);
        }

        private void DefineRootView(ViewBase view)
        {
            // Remove any existing view
            Clear();

            // Use the provided view
            Add(view);

            // Provide back reference to the button definition
            _ribbonLabel.LabelView = view;
        }

        private void UpdateEnabledState()
        {
            // Get the correct enabled state from the button definition
            bool buttonEnabled = _ribbonLabel.Enabled;
            if (_ribbonLabel.KryptonCommand != null)
                buttonEnabled = _ribbonLabel.KryptonCommand.Enabled;

            // Take into account the ribbon state and mode
            bool enabled = _ribbon.InDesignHelperMode || (buttonEnabled && _ribbon.Enabled);

            // Update enabled for the large button view
            _viewLarge.Enabled = enabled;
            _viewLargeImage.Enabled = enabled;
            _viewLargeLabelImage.Enabled = enabled;
            _viewLargeText1.Enabled = enabled;
            _viewLargeText2.Enabled = enabled;

            // Update enabled for the medium/small button view
            _viewMediumSmall.Enabled = enabled;
            _viewMediumSmallImage.Enabled = enabled;
            _viewMediumSmallLabelImage.Enabled = enabled;
            _viewMediumSmallText1.Enabled = enabled;
            _viewMediumSmallText2.Enabled = enabled;
        }

        private void UpdateImageSmallState()
        {
            _viewMediumSmallImage.Visible = (_ribbonLabel.ImageSmall != null);
        }

        private void UpdateItemSizeState()
        {
            UpdateItemSizeState(_ribbonLabel.ItemSizeCurrent);
        }

        private void UpdateItemSizeState(GroupItemSize size)
        {
            _currentSize = size;

            switch (size)
            {
                case GroupItemSize.Small:
                case GroupItemSize.Medium:
                    bool show = (size == GroupItemSize.Medium);
                    _viewMediumSmallCenter.CurrentSize = size;
                    _viewMediumSmallText2.Visible = show;

                    DefineRootView(_viewMediumSmall);
                    break;
                case GroupItemSize.Large:
                    DefineRootView(_viewLarge);
                    break;
            }
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            _ribbonLabel.OnDesignTimeContextMenu(e);
        }

        private void OnLabelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "Visible":
                    updateLayout = true;
                    break;
                case "TextLine1":
                    _viewLargeText1.MakeDirty();
                    _viewMediumSmallText1.MakeDirty();
                    updateLayout = true;
                    break;
                case "TextLine2":
                    _viewLargeText2.MakeDirty();
                    _viewMediumSmallText2.MakeDirty();
                    updateLayout = true;
                    break;
                case "ImageSmall":
                    UpdateImageSmallState();
                    updateLayout = true;
                    break;
                case "Enabled":
                    UpdateEnabledState();
                    updatePaint = true;
                    break;
                case "ImageLarge":
                    updatePaint = true;
                    break;
                case "ItemSizeMinimum":
                case "ItemSizeMaximum":
                case "ItemSizeCurrent":
                    UpdateItemSizeState();
                    updateLayout = true;
                    break;
                case "KryptonCommand":
                    _viewLargeText1.MakeDirty();
                    _viewLargeText2.MakeDirty();
                    _viewMediumSmallText1.MakeDirty();
                    _viewMediumSmallText2.MakeDirty();
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonLabel.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonLabel.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this button is actually defined as visible...
                if (_ribbonLabel.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonLabel.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonLabel.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
        }
        #endregion
    }
}
