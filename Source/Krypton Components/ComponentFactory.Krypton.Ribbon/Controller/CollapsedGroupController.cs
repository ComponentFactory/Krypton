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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Process mouse events for a collapsed group.
	/// </summary>
    internal class CollapsedGroupController : GlobalId,
                                              IMouseController,
                                              ISourceController,
                                              IKeyController,
                                              IRibbonKeyTipTarget
	{
		#region Instance Fields
        private KryptonRibbon _ribbon;
        private bool _hasFocus;
        private bool _mouseOver;
        private NeedPaintHandler _needPaint;
        private ViewLayoutDocker _target;
		#endregion

		#region Events
		/// <summary>
		/// Occurs when the mouse is used to left click the target.
		/// </summary>
		public event MouseEventHandler Click;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the LeftDownController class.
		/// </summary>
        /// <param name="ribbon">Reference to owning control instance.</param>
        /// <param name="target">View element that owns this controller.</param>
        /// <param name="needPaint">Paint delegate for notifying visual changes.</param>
        public CollapsedGroupController(KryptonRibbon ribbon,
                                        ViewLayoutDocker target,
                                        NeedPaintHandler needPaint)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(target != null);
            Debug.Assert(needPaint != null);

            _ribbon = ribbon;
            _target = target;
            _needPaint = needPaint;
        }
		#endregion

        #region HasFocus
        /// <summary>
        /// Gets a value indicating if the controller has focus.
        /// </summary>
        public bool HasFocus
        {
            get { return _hasFocus; }
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
		/// Mouse has entered the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
		{
            // Mouse is over the target
            _mouseOver = true;
		}

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
		{
		}

		/// <summary>
		/// Mouse button has been pressed in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button pressed down.</param>
		/// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
		{
            if (_mouseOver && (button == MouseButtons.Left))
            {
                // Generate the click on the down and not the usual mouse up
                OnClick(new MouseEventArgs(button, 1, pt.X, pt.Y, 0));
            }

            return false;
		}

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
		{
		}

		/// <summary>
		/// Mouse has left the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase next)
		{
            // Mouse is no longer over the target
            _mouseOver = false;

		}
        
        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void DoubleClick(Point pt)
        {
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public virtual bool IgnoreVisualFormLeftButtonDown
        {
            get { return false; }
        }
        #endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public virtual void KeyDown(Control c, KeyEventArgs e)
        {
            // Get the root control that owns the provided control
            c = _ribbon.GetControllerControl(c);

            if (c is KryptonRibbon)
                KeyDownRibbon(c as KryptonRibbon, e);
            else if (c is VisualPopupGroup)
                KeyDownPopupGroup(c as VisualPopupGroup, e);
            else if (c is VisualPopupMinimized)
                KeyDownPopupMinimized(c as VisualPopupMinimized, e);
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public virtual void KeyPress(Control c, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool KeyUp(Control c, KeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
            _hasFocus = true;
            OnNeedPaint(false, _target.ClientRectangle);
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
            _hasFocus = false;
            OnNeedPaint(false, _target.ClientRectangle);
        }
        #endregion

        #region KeyTipSelect
        /// <summary>
        /// Perform actual selection of the item.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public void KeyTipSelect(KryptonRibbon ribbon)
        {
            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

            // We should have a visual popup for showing the collapsed group
            if (VisualPopupManager.Singleton.IsTracking &&
                (VisualPopupManager.Singleton.CurrentPopup is VisualPopupGroup))
            {
                // Cast to correct type
                VisualPopupGroup popupGroup = (VisualPopupGroup)VisualPopupManager.Singleton.CurrentPopup;
                
                // Grab the list of key tips from the popup group
                _ribbon.KeyTipMode = KeyTipMode.PopupGroup;
                KeyTipInfoList keyTipList = new KeyTipInfoList();
                popupGroup.ViewGroup.GetGroupKeyTips(keyTipList);

                // Update key tips with those appropriate for this tab
                _ribbon.SetKeyTips(keyTipList, KeyTipMode.PopupGroup);
            }
        }
        #endregion

		#region Protected
        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        /// <param name="invalidRect">Client area to be invalidated.</param>
        protected virtual void OnNeedPaint(bool needLayout,
                                           Rectangle invalidRect)
        {
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout, invalidRect));
        }
        
        /// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">A MouseEventArgs containing the event data.</param>
		protected virtual void OnClick(MouseEventArgs e)
		{
			if (Click != null)
				Click(this, e);
		}
		#endregion

        #region Implementation
        private void KeyDownRibbon(KryptonRibbon ribbon, KeyEventArgs e)
        {
            ViewBase newView = null;

            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Get the previous focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(_target);

                    // Got to the actual tab header
                    if (newView == null)
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                    break;
                case Keys.Tab:
                case Keys.Right:
                    // Get the next focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetNextFocusItem(_target);

                    // Move across to any far defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                    // Rotate around to application button
                    if (newView == null)
                    {
                        if (ribbon.TabsArea.LayoutAppButton.Visible)
                            newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                        else if (ribbon.TabsArea.LayoutAppTab.Visible)
                            newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                    }                        
                    break;
                case Keys.Space:
                case Keys.Enter:
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

                    // Get access to the popup for the group
                    if ((VisualPopupManager.Singleton.CurrentPopup != null) &&
                        (VisualPopupManager.Singleton.CurrentPopup is VisualPopupGroup))
                    {
                        // Cast to correct type
                        VisualPopupGroup popupGroup = (VisualPopupGroup)VisualPopupManager.Singleton.CurrentPopup;
                        popupGroup.SetFirstFocusItem();
                    }
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != _target))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !ribbon.RealMinimizedMode)
                    ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                ribbon.FocusView = newView;
            }
        }


        private void KeyDownPopupGroup(VisualPopupGroup popupGroup, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupGroup.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupGroup.SetNextFocusItem();
                    break;
                case Keys.Space:
                case Keys.Enter:
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

                    // Get access to the popup for the group
                    if ((VisualPopupManager.Singleton.CurrentPopup != null) &&
                        (VisualPopupManager.Singleton.CurrentPopup is VisualPopupGroup))
                    {
                        // Cast to correct type
                        VisualPopupGroup popup = (VisualPopupGroup)VisualPopupManager.Singleton.CurrentPopup;
                        popup.SetFirstFocusItem();
                    }
                    break;
            }
        }

        private void KeyDownPopupMinimized(VisualPopupMinimized popupMinimized, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupMinimized.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupMinimized.SetNextFocusItem();
                    break;
                case Keys.Space:
                case Keys.Enter:
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

                    // Get access to the popup for the group
                    if ((VisualPopupManager.Singleton.CurrentPopup != null) &&
                        (VisualPopupManager.Singleton.CurrentPopup is VisualPopupGroup))
                    {
                        // Cast to correct type
                        VisualPopupGroup popupGroup = (VisualPopupGroup)VisualPopupManager.Singleton.CurrentPopup;
                        popupGroup.SetFirstFocusItem();
                    }
                    break;
            }
        }
        #endregion
    }
}
