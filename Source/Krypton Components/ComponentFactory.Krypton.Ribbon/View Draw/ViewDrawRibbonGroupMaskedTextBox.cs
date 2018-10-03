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
	/// Draws a ribbon group masked textbox.
	/// </summary>
    internal class ViewDrawRibbonGroupMaskedTextBox : ViewComposite,
                                                      IRibbonViewGroupItemView
    {
        #region Static Fields
        private static readonly int NULL_CONTROL_WIDTH = 50;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupMaskedTextBox _ribbonMaskedTextBox;
        private ViewDrawRibbonGroup _activeGroup;
        private MaskedTextBoxController _controller;
        private NeedPaintHandler _needPaint;
        private GroupItemSize _currentSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupMaskedTextBox class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonMaskedTextBox">Reference to source masked textbox.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupMaskedTextBox(KryptonRibbon ribbon,
                                                KryptonRibbonGroupMaskedTextBox ribbonMaskedTextBox,
                                                NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonMaskedTextBox != null);
            Debug.Assert(needPaint != null);

            // Remember incoming references
            _ribbon = ribbon;
            _ribbonMaskedTextBox = ribbonMaskedTextBox;
            _needPaint = needPaint;
            _currentSize = _ribbonMaskedTextBox.ItemSizeCurrent;

            // Hook into the masked textbox events
            _ribbonMaskedTextBox.MouseEnterControl += new EventHandler(OnMouseEnterControl);
            _ribbonMaskedTextBox.MouseLeaveControl += new EventHandler(OnMouseLeaveControl);

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonMaskedTextBox;

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the masked textbox
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                MouseController = controller;
            }

            // Create controller needed for handling focus and key tip actions
            _controller = new MaskedTextBoxController(_ribbon, _ribbonMaskedTextBox, this);
            SourceController = _controller;
            KeyController = _controller;

            // We need to rest visibility of the masked textbox for each layout cycle
            _ribbon.ViewRibbonManager.LayoutBefore += new EventHandler(OnLayoutAction);
            _ribbon.ViewRibbonManager.LayoutAfter += new EventHandler(OnLayoutAction);

            // Define back reference to view for the masked text box definition
            _ribbonMaskedTextBox.MaskedTextBoxView = this;

            // Give paint delegate to masked textbox so its palette changes are redrawn
            _ribbonMaskedTextBox.ViewPaintDelegate = needPaint;

            // Hook into changes in the ribbon custom definition
            _ribbonMaskedTextBox.PropertyChanged += new PropertyChangedEventHandler(OnMaskedTextBoxPropertyChanged);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupMaskedTextBox:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ribbonMaskedTextBox != null)
                {
                    // Must unhook to prevent memory leaks
                    _ribbonMaskedTextBox.MouseEnterControl -= new EventHandler(OnMouseEnterControl);
                    _ribbonMaskedTextBox.MouseLeaveControl -= new EventHandler(OnMouseLeaveControl);
                    _ribbonMaskedTextBox.ViewPaintDelegate = null;
                    _ribbonMaskedTextBox.PropertyChanged -= new PropertyChangedEventHandler(OnMaskedTextBoxPropertyChanged);
                    _ribbon.ViewRibbonManager.LayoutAfter -= new EventHandler(OnLayoutAction);
                    _ribbon.ViewRibbonManager.LayoutBefore -= new EventHandler(OnLayoutAction);

                    // Remove association with definition
                    _ribbonMaskedTextBox.MaskedTextBoxView = null; 
                    _ribbonMaskedTextBox = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupTextBox
        /// <summary>
        /// Gets access to the owning group masked textbox instance.
        /// </summary>
        public KryptonRibbonGroupMaskedTextBox GroupMaskedTextBox
        {
            get { return _ribbonMaskedTextBox; }
        }
        #endregion

        #region LostFocus
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void LostFocus(Control c)
        {
            // Ask ribbon to shift focus to the hidden control
            _ribbon.HideFocus(_ribbonMaskedTextBox.MaskedTextBox);
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
            if ((_ribbonMaskedTextBox.Visible) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox != null) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox.MaskedTextBox != null) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox.MaskedTextBox.CanSelect))
                return this;
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
            if ((_ribbonMaskedTextBox.Visible) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox != null) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox.MaskedTextBox != null) &&
                (_ribbonMaskedTextBox.LastMaskedTextBox.MaskedTextBox.CanSelect))
                return this;
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
            matched = (current == this);
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
            matched = (current == this);
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
            // Only provide a key tip if we are visible and the target control can accept focus
            if (Visible && LastMaskedTextBox.CanFocus)
            {
                // Get the screen location of the button
                Rectangle viewRect = _ribbon.KeyTipToScreen(this);

                // Determine the screen position of the key tip
                Point screenPt = Point.Empty;

                // Determine the screen position of the key tip dependant on item location/size
                switch (_currentSize)
                {
                    case GroupItemSize.Large:
                        screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom);
                        break;
                    case GroupItemSize.Medium:
                    case GroupItemSize.Small:
                        screenPt = _ribbon.CalculatedValues.KeyTipRectToPoint(viewRect, lineHint);
                        break;
                }

                keyTipList.Add(new KeyTipInfo(_ribbonMaskedTextBox.Enabled, 
                                              _ribbonMaskedTextBox.KeyTip,
                                              screenPt, 
                                              ClientRectangle,
                                              _controller));
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
            _currentSize = size;
        }

        /// <summary>
        /// Reset the group item size to the item definition.
        /// </summary>
        public void ResetGroupItemSize()
        {
            _currentSize = _ribbonMaskedTextBox.ItemSizeCurrent;
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

            // If there is a masked textbox associated then ask for its requested size
            if (LastMaskedTextBox != null)
            {
                if (ActualVisible(LastMaskedTextBox))
                {
                    preferredSize = LastMaskedTextBox.GetPreferredSize(context.DisplayRectangle.Size);

                    // Add two pixels, one for the left and right edges that will be padded
                    preferredSize.Width += 2;
                }
            }
            else
                preferredSize.Width = NULL_CONTROL_WIDTH;

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
                if (LastMaskedTextBox != null)
                {
                    LastMaskedTextBox.SetBounds(ClientLocation.X + 1,
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

            // If we do not have a masked textbox
            if (_ribbonMaskedTextBox.MaskedTextBox == null)
            {
                // And we are in design time
                if (_ribbon.InDesignMode)
                {
                    // Draw rectangle is 1 pixel less per edge
                    Rectangle drawRect = ClientRectangle;
                    drawRect.Inflate(-1, -1);
                    drawRect.Height--;

                    // Draw an indication of where the masked textbox will be
                    context.Graphics.FillRectangle(Brushes.Goldenrod, drawRect);
                    context.Graphics.DrawRectangle(Pens.Gold, drawRect);
                }
            }
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
        private void OnContextClick(object sender, MouseEventArgs e)
        {
            _ribbonMaskedTextBox.OnDesignTimeContextMenu(e);
        }

        private void OnMaskedTextBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "Enabled":
                    UpdateEnabled(LastMaskedTextBox);
                    break;
                case "Visible":
                    UpdateVisible(LastMaskedTextBox);
                    updateLayout = true;
                    break;
                case "CustomControl":
                    updateLayout = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonMaskedTextBox.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonMaskedTextBox.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this button is actually defined as visible...
                if (_ribbonMaskedTextBox.Visible || _ribbon.InDesignMode)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonMaskedTextBox.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonMaskedTextBox.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
        }

        private Control LastParentControl
        {
            get { return _ribbonMaskedTextBox.LastParentControl; }
            set { _ribbonMaskedTextBox.LastParentControl = value; }
        }

        private KryptonMaskedTextBox LastMaskedTextBox
        {
            get { return _ribbonMaskedTextBox.LastMaskedTextBox; }
            set { _ribbonMaskedTextBox.LastMaskedTextBox = value; }
        }

        private void UpdateParent(Control parentControl)
        {
            // Is there a change in the masked textbox or a change in 
            // the parent control that is hosting the control...
            if ((parentControl != LastParentControl) ||
                (LastMaskedTextBox != _ribbonMaskedTextBox.MaskedTextBox))
            {
                // We only modify the parent and visible state if processing for correct container
                if ((_ribbonMaskedTextBox.RibbonContainer.RibbonGroup.ShowingAsPopup && (parentControl is VisualPopupGroup)) ||
                    (!_ribbonMaskedTextBox.RibbonContainer.RibbonGroup.ShowingAsPopup && !(parentControl is VisualPopupGroup)))
                {
                    // If we have added the custrom control to a parent before
                    if ((LastMaskedTextBox != null) && (LastParentControl != null))
                    {
                        // If that control is still a child of the old parent
                        if (LastParentControl.Controls.Contains(LastMaskedTextBox))
                        {
                            // Check for a collection that is based on the read only class
                            LastParentControl.Controls.Remove(LastMaskedTextBox);
                        }
                    }

                    // Remember the current control and new parent
                    LastMaskedTextBox = _ribbonMaskedTextBox.MaskedTextBox;
                    LastParentControl = parentControl;

                    // If we have a new masked textbox and parent
                    if ((LastMaskedTextBox != null) && (LastParentControl != null))
                    {
                        // Ensure the control is not in the display area when first added
                        LastMaskedTextBox.Location = new Point(-LastMaskedTextBox.Width, -LastMaskedTextBox.Height);

                        // Check for the correct visible state of the masked textbox
                        UpdateVisible(LastMaskedTextBox);
                        UpdateEnabled(LastMaskedTextBox);

                        // Check for a collection that is based on the read only class
                        LastParentControl.Controls.Add(LastMaskedTextBox);
                    }
                }
            }
        }

        private void UpdateEnabled(Control c)
        {
            if (c != null)
            {
                // Start with the enabled state of the group element
                bool enabled = _ribbonMaskedTextBox.Enabled;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonMaskedTextBox.MaskedTextBoxDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    enabled = _ribbonMaskedTextBox.MaskedTextBoxDesigner.DesignEnabled;
                }

                c.Enabled = enabled;
            }
        }

        private bool ActualVisible(Control c)
        {
            if (c != null)
            {
                // Start with the visible state of the group element
                bool visible = _ribbonMaskedTextBox.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonMaskedTextBox.MaskedTextBoxDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = _ribbonMaskedTextBox.MaskedTextBoxDesigner.DesignVisible;
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
                bool visible = _ribbonMaskedTextBox.Visible;

                // If we have an associated designer setup...
                if (!_ribbon.InDesignHelperMode && (_ribbonMaskedTextBox.MaskedTextBoxDesigner != null))
                {
                    // And we are not using the design helpers, then use the design specified value
                    visible = _ribbonMaskedTextBox.MaskedTextBoxDesigner.DesignVisible;
                }

                if (visible)
                {
                    // Only visible if on the currently selected page
                    if ((_ribbonMaskedTextBox.RibbonTab == null) ||
                        (_ribbon.SelectedTab != _ribbonMaskedTextBox.RibbonTab))
                        visible = false;
                    else
                    {
                        // Check the owning group is visible
                        if ((_ribbonMaskedTextBox.RibbonContainer != null) &&
                            (_ribbonMaskedTextBox.RibbonContainer.RibbonGroup != null) &&
                            !_ribbonMaskedTextBox.RibbonContainer.RibbonGroup.Visible &&
                            !_ribbon.InDesignMode)
                            visible = false;
                        else
                        {
                            // Check that the group is not collapsed
                            if ((_ribbonMaskedTextBox.RibbonContainer.RibbonGroup.IsCollapsed) &&
                                ((_ribbon.GetControllerControl(_ribbonMaskedTextBox.MaskedTextBox) is KryptonRibbon) ||
                                 (_ribbon.GetControllerControl(_ribbonMaskedTextBox.MaskedTextBox) is VisualPopupMinimized)))
                                visible = false;
                            else
                            {
                                // Check that the hierarchy of containers are all visible
                                KryptonRibbonGroupContainer container = _ribbonMaskedTextBox.RibbonContainer;

                                // Keep going until we have searched the entire parent chain of containers
                                while (container != null)
                                {
                                    // If any parent container is not visible, then we are not visible
                                    if (!container.Visible)
                                    {
                                        visible = false;
                                        break;
                                    }

                                    // Move up a level
                                    container = container.RibbonContainer;
                                }
                            }
                        }
                    }
                }

                c.Visible = visible;
            }
        }

        private void OnLayoutAction(object sender, EventArgs e)
        {
            // If not disposed then we still have a element reference
            if (_ribbonMaskedTextBox != null)
            {
                // Change in selected tab requires a retest of the control visibility
                UpdateVisible(LastMaskedTextBox);
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
