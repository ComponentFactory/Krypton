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
	/// Draws a ribbon group radio button.
	/// </summary>
    internal class ViewDrawRibbonGroupRadioButton : ViewComposite,
                                                    IRibbonViewGroupItemView,
                                                    IContentValues
    {
        #region Static Fields
        private static readonly Padding _largeImagePadding = new Padding(3, 2, 3, 3);
        private static readonly Padding _smallImagePadding = new Padding(3);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupRadioButton _ribbonRadioButton;
        private ViewLayoutRibbonRadioButton _viewLarge;
        private ViewDrawRibbonGroupRadioButtonImage _viewLargeImage;
        private ViewDrawRibbonGroupRadioButtonText _viewLargeText1;
        private ViewDrawRibbonGroupRadioButtonText _viewLargeText2;
        private GroupRadioButtonController _viewLargeController;
        private EventHandler _finishDelegateLarge;
        private ViewLayoutRibbonRadioButton _viewMediumSmall;
        private ViewLayoutRibbonRowCenter _viewMediumSmallCenter;
        private ViewDrawRibbonGroupRadioButtonImage _viewMediumSmallImage;
        private ViewDrawRibbonGroupRadioButtonText _viewMediumSmallText1;
        private ViewDrawRibbonGroupRadioButtonText _viewMediumSmallText2;
        private GroupRadioButtonController _viewMediumSmallController;
        private EventHandler _finishDelegateMediumSmall;
        private NeedPaintHandler _needPaint;
        private GroupItemSize _currentSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupRadioButton class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonRadioButton">Reference to source radio button definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupRadioButton(KryptonRibbon ribbon,
                                              KryptonRibbonGroupRadioButton ribbonRadioButton,
                                              NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonRadioButton != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonRadioButton = ribbonRadioButton;
            _needPaint = needPaint;
            _currentSize = _ribbonRadioButton.ItemSizeCurrent;

            // Create delegate used to process end of click action
            _finishDelegateLarge = new EventHandler(ActionFinishedLarge);
            _finishDelegateMediumSmall = new EventHandler(ActionFinishedMediumSmall);

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonRadioButton;

            // Create the different views for different sizes of the radio button
            CreateLargeRadioButtonView();
            CreateMediumSmallRadioButtonView();

            // Update all views to reflect current radio button state
            UpdateEnabledState();
            UpdateCheckedState();
            UpdateItemSizeState();

            // Hook into changes in the ribbon radio button definition
            _ribbonRadioButton.PropertyChanged += new PropertyChangedEventHandler(OnRadioButtonPropertyChanged);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupRadioButton:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ribbonRadioButton != null)
                {
                    // Must unhook to prevent memory leaks
                    _ribbonRadioButton.PropertyChanged -= new PropertyChangedEventHandler(OnRadioButtonPropertyChanged);

                    // Remove association with definition
                    _ribbonRadioButton.RadioButtonView = null;
                    _ribbonRadioButton = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupRadioButton
        /// <summary>
        /// Gets access to the connected radio button definition.
        /// </summary>
        public KryptonRibbonGroupRadioButton GroupRadioButton
        {
            get { return _ribbonRadioButton; }
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the item.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            // Only take focus if we are visible and enabled
            if (_ribbonRadioButton.Visible && _ribbonRadioButton.Enabled)
            {
                if (_viewLarge == _ribbonRadioButton.RadioButtonView)
                    return _viewLarge;
                else
                    return _viewMediumSmall;
            }
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
            if (_ribbonRadioButton.Visible && _ribbonRadioButton.Enabled)
            {
                if (_viewLarge == _ribbonRadioButton.RadioButtonView)
                    return _viewLarge;
                else
                    return _viewMediumSmall;
            }
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
            matched = (current == _viewLarge) || (current == _viewMediumSmall);
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
            matched = (current == _viewLarge) || (current == _viewMediumSmall);
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
                // Get the screen location of the radio button
                Rectangle viewRect = _ribbon.KeyTipToScreen(this[0]);

                Point screenPt = Point.Empty;
                GroupRadioButtonController controller = null;

                // Determine the screen position of the key tip dependant on item location/size
                switch (_currentSize)
                {
                    case GroupItemSize.Large:
                        screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom);
                        controller = _viewLargeController;
                        break;
                    case GroupItemSize.Medium:
                    case GroupItemSize.Small:
                        screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);
                        controller = _viewMediumSmallController;
                        break;
                }

                keyTipList.Add(new KeyTipInfo(_ribbonRadioButton.Enabled, _ribbonRadioButton.KeyTip, 
                                              screenPt, this[0].ClientRectangle, controller));
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
            // Get the preferred size of radio button view
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

            // Update our enabled and checked state
            UpdateEnabledState();
            UpdateCheckedState();
            UpdateItemSizeState();

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
        private void CreateLargeRadioButtonView()
        {
            // Create the layout docker for the contents of the button
            _viewLarge = new ViewLayoutRibbonRadioButton();

            // Add the large button at the top
            _viewLargeImage = new ViewDrawRibbonGroupRadioButtonImage(_ribbon, _ribbonRadioButton, true);
            ViewLayoutRibbonCenterPadding largeImagePadding = new ViewLayoutRibbonCenterPadding(_largeImagePadding);
            largeImagePadding.Add(_viewLargeImage);
            _viewLarge.Add(largeImagePadding, ViewDockStyle.Top);

            // Add the first line of text
            _viewLargeText1 = new ViewDrawRibbonGroupRadioButtonText(_ribbon, _ribbonRadioButton, true);
            _viewLarge.Add(_viewLargeText1, ViewDockStyle.Bottom);

            // Add the second line of text
            _viewLargeText2 = new ViewDrawRibbonGroupRadioButtonText(_ribbon, _ribbonRadioButton, false);
            _viewLarge.Add(_viewLargeText2, ViewDockStyle.Bottom);

            // Add a 1 pixel separator at bottom of button before the text
            _viewLarge.Add(new ViewLayoutRibbonSeparator(1, false), ViewDockStyle.Bottom);

            // Create controller for handling mouse, keyboard and focus
            _viewLargeController = new GroupRadioButtonController(_ribbon, _viewLarge, _viewLargeImage, _needPaint);
            _viewLargeController.Click += new EventHandler(OnLargeRadioButtonClick);
            _viewLargeController.ContextClick += new MouseEventHandler(OnContextClick);
            _viewLarge.MouseController = _viewLargeController;
            _viewLarge.SourceController = _viewLargeController;
            _viewLarge.KeyController = _viewLargeController;

            // Create controller for intercepting events to determine tool tip handling
            _viewLarge.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager, 
                                                               _viewLarge, _viewLarge.MouseController);
        }

        private void CreateMediumSmallRadioButtonView()
        {
            // Create the layout docker for the contents of the button
            _viewMediumSmall = new ViewLayoutRibbonRadioButton();

            // Create the image and drop down content
            _viewMediumSmallImage = new ViewDrawRibbonGroupRadioButtonImage(_ribbon, _ribbonRadioButton, false);
            _viewMediumSmallText1 = new ViewDrawRibbonGroupRadioButtonText(_ribbon, _ribbonRadioButton, true);
            _viewMediumSmallText2 = new ViewDrawRibbonGroupRadioButtonText(_ribbon, _ribbonRadioButton, false);
            ViewLayoutRibbonCenterPadding imagePadding = new ViewLayoutRibbonCenterPadding(_smallImagePadding);
            imagePadding.Add(_viewMediumSmallImage);

            // Layout the content in the center of a row
            _viewMediumSmallCenter = new ViewLayoutRibbonRowCenter();
            _viewMediumSmallCenter.Add(imagePadding);
            _viewMediumSmallCenter.Add(_viewMediumSmallText1);
            _viewMediumSmallCenter.Add(_viewMediumSmallText2);

            // Use content as only fill item
            _viewMediumSmall.Add(_viewMediumSmallCenter, ViewDockStyle.Fill);

            // Create controller for handling mouse, keyboard and focus
            _viewMediumSmallController = new GroupRadioButtonController(_ribbon, _viewMediumSmall, _viewMediumSmallImage, _needPaint);
            _viewMediumSmallController.Click += new EventHandler(OnMediumSmallRadioButtonClick);
            _viewMediumSmallController.ContextClick += new MouseEventHandler(OnContextClick);
            _viewMediumSmall.MouseController = _viewMediumSmallController;
            _viewMediumSmall.SourceController = _viewMediumSmallController;
            _viewMediumSmall.KeyController = _viewMediumSmallController;

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

            // Provide back reference to the radio button definition
            _ribbonRadioButton.RadioButtonView = view;
        }

        private void UpdateEnabledState()
        {
            bool enabled = _ribbon.InDesignHelperMode || (_ribbonRadioButton.Enabled && _ribbon.Enabled);

            // Update enabled for the large radio button view
            _viewLarge.Enabled = enabled;
            _viewLargeImage.Enabled = enabled;
            _viewLargeText1.Enabled = enabled;
            _viewLargeText2.Enabled = enabled;

            // Update enabled for the medium/small radio button view
            _viewMediumSmall.Enabled = enabled;
            _viewMediumSmallText1.Enabled = enabled;
            _viewMediumSmallText2.Enabled = enabled;
            _viewMediumSmallImage.Enabled = enabled;
        }

        private void UpdateCheckedState()
        {
            _viewLargeImage.Checked = _ribbonRadioButton.Checked;
            _viewMediumSmallImage.Checked = _ribbonRadioButton.Checked;
        }

        private void UpdateItemSizeState()
        {
            UpdateItemSizeState(_ribbonRadioButton.ItemSizeCurrent);
        }

        private void UpdateItemSizeState(GroupItemSize size)
        {
            _currentSize = size;

            switch (size)
            {
                case GroupItemSize.Small:
                case GroupItemSize.Medium:
                    DefineRootView(_viewMediumSmall);
                    break;
                case GroupItemSize.Large:
                    DefineRootView(_viewLarge);
                    break;
            }
        }

        private void OnLargeRadioButtonClick(object sender, EventArgs e)
        {
            GroupRadioButton.PerformClick(_finishDelegateLarge);
        }

        private void OnMediumSmallRadioButtonClick(object sender, EventArgs e)
        {
            GroupRadioButton.PerformClick(_finishDelegateMediumSmall);
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            GroupRadioButton.OnDesignTimeContextMenu(e);
        }

        private void ActionFinishedLarge(object sender, EventArgs e)
        {
            // Remove any popups that result from an action occuring
            if (_ribbon != null)
                _ribbon.ActionOccured();

            // Remove the fixed pressed appearance
            _viewLargeController.RemoveFixed();
        }

        private void ActionFinishedMediumSmall(object sender, EventArgs e)
        {
            // Remove any popups that result from an action occuring
            if (_ribbon != null)
                _ribbon.ActionOccured();

            // Remove the fixed pressed appearance
            _viewMediumSmallController.RemoveFixed();
        }

        private void OnRadioButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "Visible":
                    updateLayout = true;
                    break;
                case "TextLine1":
                    updateLayout = true;
                    _viewLargeText1.MakeDirty();
                    _viewMediumSmallText1.MakeDirty();
                    break;
                case "TextLine2":
                    updateLayout = true;
                    _viewLargeText2.MakeDirty();
                    _viewMediumSmallText2.MakeDirty();
                    break;
                case "Checked":
                    UpdateCheckedState();
                    updatePaint = true;
                    break;
                case "Enabled":
                    UpdateEnabledState();
                    updatePaint = true;
                    break;
                case "ItemSizeMinimum":
                case "ItemSizeMaximum":
                case "ItemSizeCurrent":
                    UpdateItemSizeState();
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonRadioButton.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonRadioButton.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this radio button is actually defined as visible...
                if (_ribbonRadioButton.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonRadioButton.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonRadioButton.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the short text.
        /// </summary>
        /// <returns></returns>
        public string GetShortText()
        {
            return _ribbonRadioButton.TextLine1;
        }

        /// <summary>
        /// Gets the long text.
        /// </summary>
        /// <returns></returns>
        public string GetLongText()
        {
            return _ribbonRadioButton.TextLine2;
        }
        #endregion
    }
}
