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
	/// Draws a ribbon group cluster button.
	/// </summary>
    internal class ViewDrawRibbonGroupClusterButton : ViewComposite,
                                                      IRibbonViewGroupItemView
    {
        #region Static Fields
        private static readonly Padding _smallImagePadding = new Padding(3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupClusterButton _ribbonButton;
        private NeedPaintHandler _needPaint;
        private PaletteBackInheritForced _backForced;
        private PaletteBorderInheritForced _borderForced;
        private ViewDrawRibbonGroupButtonBackBorder _viewMediumSmall;
        private ViewLayoutRibbonRowCenter _viewMediumSmallCenter;
        private ViewDrawRibbonGroupClusterButtonImage _viewMediumSmallImage;
        private ViewDrawRibbonGroupClusterButtonText _viewMediumSmallText1;
        private ViewDrawRibbonDropArrow _viewMediumSmallDropArrow;
        private ViewLayoutRibbonSeparator _viewMediumSmallText2Sep1;
        private ViewLayoutRibbonSeparator _viewMediumSmallText2Sep2;
        private GroupItemSize _currentSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupClusterButton class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonButton">Reference to source button definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupClusterButton(KryptonRibbon ribbon,
                                                KryptonRibbonGroupClusterButton ribbonButton,
                                                NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonButton != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonButton = ribbonButton;
            _needPaint = needPaint;
            _currentSize = _ribbonButton.ItemSizeCurrent;

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonButton;

            // Create the small button view
            CreateView();

            // Update view reflect current button state
            UpdateEnabledState();
            UpdateCheckedState();
            UpdateDropDownState();
            UpdateItemSizeState();

            // Hook into changes in the ribbon button definition
            _ribbonButton.PropertyChanged += new PropertyChangedEventHandler(OnButtonPropertyChanged);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupClusterButton:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ribbonButton != null)
                {
                    // Must unhook to prevent memory leaks
                    _ribbonButton.PropertyChanged -= new PropertyChangedEventHandler(OnButtonPropertyChanged);

                    // Remove association with definition
                    _ribbonButton.ClusterButtonView = null;
                    _ribbonButton = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupClusterButton
        /// <summary>
        /// Gets access to the connected button definition.
        /// </summary>
        public KryptonRibbonGroupClusterButton GroupClusterButton
        {
            get { return _ribbonButton; }
        }
        #endregion

        #region MaxBorderEdges
        /// <summary>
        /// Gets and sets the maximum edges allowed.
        /// </summary>
        public PaletteDrawBorders MaxBorderEdges
        {
            get { return _borderForced.MaxBorderEdges; }
            set { _borderForced.MaxBorderEdges = value; }
        }
        #endregion

        #region BorderIgnoreNormal
        /// <summary>
        /// ets and sets the ignoring of normal borders.
        /// </summary>
        public bool BorderIgnoreNormal
        {
            get { return _borderForced.BorderIgnoreNormal; }
            
            set 
            {
                _backForced.BorderIgnoreNormal = value;
                _borderForced.BorderIgnoreNormal = value; 
            }
        }
        #endregion

        #region ConstantBorder
        /// <summary>
        /// Gets and sets the drawing of a constant border.
        /// </summary>
        public bool ConstantBorder
        {
            get { return _viewMediumSmall.ConstantBorder; }
            set { _viewMediumSmall.ConstantBorder = value; }
        }
        #endregion

        #region DrawNonTrackingAreas
        /// <summary>
        /// Gets and sets if the non tracking areas are drawn.
        /// </summary>
        public bool DrawNonTrackingAreas
        {
            get { return _viewMediumSmall.DrawNonTrackingAreas; }
            set { _viewMediumSmall.DrawNonTrackingAreas = value; }
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            // Only take focus if we are visible and enabled
            if (_ribbonButton.Visible && _ribbonButton.Enabled)
                return _viewMediumSmall;
            else
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
            // Only take focus if we are visible and enabled
            if (_ribbonButton.Visible && _ribbonButton.Enabled)
                return _viewMediumSmall;
            else
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
            // Do we match the current item?
            matched = (current == _viewMediumSmall);
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
            // Do we match the current item?
            matched = (current == _viewMediumSmall);
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
            // Only provide a key tip if we are visible
            if (Visible)
            {
                // Get the screen location of the button
                Rectangle viewRect = _ribbon.KeyTipToScreen(this[0]);

                // Determine the screen position of the key tip dependant on item location
                Point screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);

                keyTipList.Add(new KeyTipInfo(_ribbonButton.Enabled, _ribbonButton.KeyTip, screenPt, 
                                              this[0].ClientRectangle, _viewMediumSmall.Controller));
            }
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

            // Update our enabled and checked state
            UpdateEnabledState();
            UpdateCheckedState();
            UpdateDropDownState();

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let child elements layout in given space
            base.Layout(context);

            // For split buttons we need to calculate the split button areas
            if (_ribbonButton.ButtonType == GroupButtonType.Split)
            {
                // Find the position of the split area
                int smallSplitRight = _viewMediumSmallText2Sep1.ClientLocation.X;
                _viewMediumSmall.SplitRectangle = new Rectangle(smallSplitRight, ClientLocation.Y, ClientRectangle.Right - smallSplitRight, ClientHeight);
            }
            else
                _viewMediumSmall.SplitRectangle = Rectangle.Empty;
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
        private void CreateView()
        {
            // Override the palette provided values
            _backForced = new PaletteBackInheritForced(_ribbon.StateCommon.RibbonGroupClusterButton.PaletteBack);
            _borderForced = new PaletteBorderInheritForced(_ribbon.StateCommon.RibbonGroupClusterButton.PaletteBorder);

            // Create the background and border view
            _viewMediumSmall = new ViewDrawRibbonGroupButtonBackBorder(_ribbon, _ribbonButton, _backForced,  _borderForced, true, _needPaint);
            _viewMediumSmall.SplitVertical = false;
            _viewMediumSmall.Click += new EventHandler(OnSmallButtonClick);
            _viewMediumSmall.DropDown += new EventHandler(OnSmallButtonDropDown);

            if (_ribbon.InDesignMode)
                _viewMediumSmall.ContextClick += new MouseEventHandler(OnContextClick);

            // Create the layout docker for the contents of the button
            ViewLayoutDocker contentLayout = new ViewLayoutDocker();

            // Create the image and drop down content
            _viewMediumSmallImage = new ViewDrawRibbonGroupClusterButtonImage(_ribbon, _ribbonButton);
            _viewMediumSmallText1 = new ViewDrawRibbonGroupClusterButtonText(_ribbon, _ribbonButton);
            _viewMediumSmallText1.Visible = (_currentSize != GroupItemSize.Small);
            _viewMediumSmallDropArrow = new ViewDrawRibbonDropArrow(_ribbon);
            _viewMediumSmallText2Sep1 = new ViewLayoutRibbonSeparator(3, false);
            _viewMediumSmallText2Sep2 = new ViewLayoutRibbonSeparator(3, false);
            ViewLayoutRibbonCenterPadding imagePadding = new ViewLayoutRibbonCenterPadding(_smallImagePadding);
            imagePadding.Add(_viewMediumSmallImage);

            // Layout the content in the center of a row
            _viewMediumSmallCenter = new ViewLayoutRibbonRowCenter();
            _viewMediumSmallCenter.Add(imagePadding);
            _viewMediumSmallCenter.Add(_viewMediumSmallText1);
            _viewMediumSmallCenter.Add(_viewMediumSmallText2Sep1);
            _viewMediumSmallCenter.Add(_viewMediumSmallDropArrow);
            _viewMediumSmallCenter.Add(_viewMediumSmallText2Sep2);

            // Use content as only fill item
            contentLayout.Add(_viewMediumSmallCenter, ViewDockStyle.Fill);

            // Add the content into the background and border
            _viewMediumSmall.Add(contentLayout);

            // Create controller for intercepting events to determine tool tip handling
            _viewMediumSmall.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager,
                                                                     _viewMediumSmall, _viewMediumSmall.MouseController);

            // Provide back reference to the button definition
            _ribbonButton.ClusterButtonView = _viewMediumSmall;

            // Define the actual view
            Add(_viewMediumSmall);
        }

        private void UpdateItemSizeState()
        {
            UpdateItemSizeState(_ribbonButton.ItemSizeCurrent);
        }

        private void UpdateItemSizeState(GroupItemSize size)
        {
            _currentSize = size;
            _viewMediumSmallCenter.CurrentSize = size;
            _viewMediumSmallText1.Visible = (size != GroupItemSize.Small);
        }

        private void UpdateEnabledState()
        {
            // Get the correct enabled state from the button definition
            bool buttonEnabled = _ribbonButton.Enabled;
            if (_ribbonButton.KryptonCommand != null)
                buttonEnabled = _ribbonButton.KryptonCommand.Enabled;

            // Take into account the ribbon state and mode
            bool enabled = _ribbon.InDesignHelperMode || (buttonEnabled && _ribbon.Enabled);

            _viewMediumSmall.Enabled = enabled;
            _viewMediumSmallText1.Enabled = enabled;
            _viewMediumSmallImage.Enabled = enabled;
            _viewMediumSmallDropArrow.Enabled = enabled;
        }

        private void UpdateCheckedState()
        {
            bool checkedState = false;

            // Only show as checked if also a check type button
            if (_ribbonButton.ButtonType == GroupButtonType.Check)
            {
                if (_ribbonButton.KryptonCommand != null)
                    checkedState = _ribbonButton.KryptonCommand.Checked;
                else
                    checkedState = _ribbonButton.Checked;
            }

            _viewMediumSmall.Checked = checkedState;
        }

        private void UpdateDropDownState()
        {
            bool dropDown = ((_ribbonButton.ButtonType == GroupButtonType.DropDown) ||
                             (_ribbonButton.ButtonType == GroupButtonType.Split));

            bool splitDown = (_ribbonButton.ButtonType == GroupButtonType.Split);

            _viewMediumSmallText2Sep1.Visible = splitDown;
            _viewMediumSmallDropArrow.Visible = dropDown;
            _viewMediumSmallText2Sep2.Visible = dropDown;

            // Update the view with the type of button being used
            _viewMediumSmall.ButtonType = _ribbonButton.ButtonType;
        }

        private void OnSmallButtonClick(object sender, EventArgs e)
        {
            GroupClusterButton.PerformClick(_viewMediumSmall.FinishDelegate);
        }

        private void OnSmallButtonDropDown(object sender, EventArgs e)
        {
            GroupClusterButton.PerformDropDown(_viewMediumSmall.FinishDelegate);
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            GroupClusterButton.OnDesignTimeContextMenu(e);
        }

        private void OnButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "Visible":
                    updateLayout = true;
                    break;
                case "TextLine":
                    _viewMediumSmallText1.MakeDirty();
                    updateLayout = true;
                    break;
                case "ButtonType":
                    UpdateDropDownState();
                    updateLayout = true;
                    break;
                case "Checked":
                    UpdateCheckedState();
                    updatePaint = true;
                    break;
                case "Enabled":
                    UpdateEnabledState();
                    updatePaint = true;
                    break;
                case "ImageSmall":
                    updatePaint = true;
                    break;
                case "ItemSizeMinimum":
                case "ItemSizeMaximum":
                case "ItemSizeCurrent":
                    UpdateItemSizeState();
                    updateLayout = true;
                    break;
                case "KryptonCommand":
                    _viewMediumSmallText1.MakeDirty();
                    UpdateEnabledState();
                    UpdateCheckedState();
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonButton.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonButton.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this button is actually defined as visible...
                if (_ribbonButton.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonButton.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonButton.RibbonTab))
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
