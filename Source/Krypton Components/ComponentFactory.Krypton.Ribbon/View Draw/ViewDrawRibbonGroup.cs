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
	/// Draws an individual RibbonGroup.
	/// </summary>
    internal class ViewDrawRibbonGroup : ViewComposite,
                                         IRibbonViewGroupSize
    {
        #region Static Fields
        private static readonly int MINIMUM_GROUP_WIDTH = 32;
        private static readonly int NORMAL_BORDER_TOPLEFT2007 = 2;
        private static readonly int NORMAL_BORDER_RIGHT2007 = 4;
        private static readonly int NORMAL_BORDER_TOP2010 = 3;
        private static readonly int NORMAL_BORDER_LEFT2010 = 3;
        private static readonly int NORMAL_BORDER_RIGHT2010 = 6;
        private static readonly int TOTAL_LEFT_RIGHT_BORDERS_2007 = 7;
        private static readonly int TOTAL_LEFT_RIGHT_BORDERS_2010 = 10;
        private static readonly int VERT_OFFSET_2007 = 0;
        private static readonly int VERT_OFFSET_2010 = 2;
        private static readonly Padding COLLAPSED_PADDING = new Padding(2);
        private static readonly Padding COLLAPSED_IMAGE_PADDING_2007 = new Padding(3, 3, 3, 4);
        private static readonly Padding COLLAPSED_IMAGE_PADDING_2010 = new Padding(3, 1, 5, 5);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroup _ribbonGroup;
        private VisualPopupGroup _popupGroup;
        private ViewLayoutDocker _layoutCollapsedMain;
        private ViewDrawRibbonGroupText _viewCollapsedText1;
        private ViewDrawRibbonGroupText _viewCollapsedText2;
        private ViewLayoutRibbonCenterPadding _layoutCollapsedImagePadding;
        private CollapsedGroupController _collapsedController;
        private ViewLayoutRibbonTitle _layoutNormalMain;
        private ViewLayoutRibbonSeparator _layoutNormalSepTop;
        private ViewLayoutRibbonSeparator _layoutNormalSepLeft;
        private ViewLayoutRibbonSeparator _layoutNormalSepRight;
        private ViewLayoutRibbonGroupContent _layoutNormalContent;
        private ViewLayoutRibbonGroupButton _viewNormalDialog;
        private ViewLayoutDocker _layoutNormalTitle;
        private ViewDrawRibbonGroupTitle _viewNormalTitle;
        private PaletteRibbonContextBack _paletteContextBack;
        private PaletteRibbonShape _lastRibbonShape;
        private NeedPaintHandler _needPaint;
        private IDisposable _mementoRibbonBack1;
        private IDisposable _mementoRibbonBack2;
        private IDisposable _mementoStandardBack;
        private Control _container;
        private bool _collapsed;
        private bool _tracking;
        private bool _pressed;
        private int _totalBorders;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroup class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroup(KryptonRibbon ribbon,
                                   KryptonRibbonGroup ribbonGroup,
                                   NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);
            Debug.Assert(needPaint != null);

            // Cache source of state specific settings
            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;
            _needPaint = needPaint;

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonGroup;

            CreateNormalView();
            CreateCollapsedView();

            // We are always created in the normal state
            Add(_layoutNormalMain);

            // Set back reference to the actual group definition
            _ribbonGroup.GroupView = this;

            // Hook into changes in the ribbon button definition
            _ribbonGroup.PropertyChanged += new PropertyChangedEventHandler(OnGroupPropertyChanged);
        }
        
		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroup:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Must unhook to prevent memory leaks
                _ribbonGroup.PropertyChanged -= new PropertyChangedEventHandler(OnGroupPropertyChanged);

                DisposeMementos();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Collapsed
        /// <summary>
        /// Gets and sets the collapsed setting of the group.
        /// </summary>
        public bool Collapsed
        {
            get { return _collapsed; }

            set
            {
                _collapsed = value;
                _ribbonGroup.IsCollapsed = value;

                // Reset the view to match the collapsed setting
                Clear();
                Add(Collapsed ? _layoutCollapsedMain : _layoutNormalMain);

                // Remove mementos as they are collapsed state specific
                DisposeMementos();
            }
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets a value indicating if the group is tracking.
        /// </summary>
        public bool Tracking
        {
            get { return _tracking; }
            set { _tracking = value; }
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets a value indicating if the group is tracking.
        /// </summary>
        public bool Pressed
        {
            get { return _pressed; }
            set { _pressed = value; }
        }
        #endregion

        #region GetGroupKeyTips
        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <param name="keyTipList">List to add new entries into.</param>
        public void GetGroupKeyTips(KeyTipInfoList keyTipList)
        {
            if (Collapsed)
            {
                // Get the screen location of the collapsed view
                Rectangle viewRect = _ribbon.KeyTipToScreen(_layoutCollapsedMain);

                // The keytip should be centered at the bottom of the view
                Point screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom + 4);

                keyTipList.Add(new KeyTipInfo(true, _ribbonGroup.KeyTipGroup, screenPt, 
                                              _layoutCollapsedMain.ClientRectangle, _collapsedController));
            }
            else
            {
                // Do we add a key tip for the dialog box launcher button
                if (_ribbonGroup.DialogBoxLauncher)
                {
                    // Get the screen location of the dialog button view
                    Rectangle viewRect = _ribbon.KeyTipToScreen(_viewNormalDialog);

                    // The keytip should be centered at the bottom of the view
                    Point screenPt = new Point(viewRect.Left + (viewRect.Width / 2), viewRect.Bottom + 4);

                    keyTipList.Add(new KeyTipInfo(true, _ribbonGroup.KeyTipDialogLauncher, screenPt,
                                                  _viewNormalDialog.ClientRectangle, _viewNormalDialog.DialogButtonController));
                }

                // Populate with key tips for the content of the group
                _layoutNormalContent.GetGroupKeyTips(keyTipList);
            }
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            ViewBase view = null;

            if (Collapsed)
                view = _layoutCollapsedMain;
            else
                view = _layoutNormalContent.GetFirstFocusItem();

            return view;
        }
        #endregion

        #region GetLastFocusItem
        /// <summary>
        /// Gets the last focus item from the group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetLastFocusItem()
        {
            ViewBase view = null;

            if (Collapsed)
                view = _layoutCollapsedMain;
            else
                view = _layoutNormalContent.GetLastFocusItem();

            return view;
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
            ViewBase view = null;

            if (Collapsed)
            {
                if (matched)
                    view = _layoutCollapsedMain;
                else
                    matched = (current == _layoutCollapsedMain);
            }
            else
                view = _layoutNormalContent.GetNextFocusItem(current, ref matched);

            return view;
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
            ViewBase view = null;

            if (Collapsed)
            {
                if (matched)
                    view = _layoutCollapsedMain;
                else
                    matched = (current == _layoutCollapsedMain);
            }
            else
                view = _layoutNormalContent.GetPreviousFocusItem(current, ref matched);

            return view;
        }
        #endregion

        #region PerformNeedPaint
        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        /// <param name="invalidRect">Rectangle to invalidate.</param>
        public void PerformNeedPaint(bool needLayout, Rectangle invalidRect)
        {
            OnNeedPaint(needLayout, invalidRect);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Get an array of available widths for the group with associated sizing values.
        /// </summary>
        /// <param name="context">Context used to calculate the sizes.</param>
        /// <returns>Array of size values.</returns>
        public GroupSizeWidth[] GetPossibleSizes(ViewLayoutContext context)
        {
            // Make changes to ensure ribbon shape is honored
            UpdateShapeValues();

            // Ask the normal group content for its possible sizes
            IRibbonViewGroupSize viewSize = (IRibbonViewGroupSize)_layoutNormalContent;

            // Get the permutations from the content area
            List<GroupSizeWidth> retWidths = new List<GroupSizeWidth>();
            retWidths.AddRange(viewSize.GetPossibleSizes(context));

            // Grab the requested min/max sizes of the group
            int minWidth = _ribbonGroup.MinimumWidth;
            int maxWidth = _ribbonGroup.MaximumWidth;
            bool ignoreMin = (minWidth < 0);

            // If a minus number then max width is effectively as big as you like
            if (maxWidth <= 0)  maxWidth = int.MaxValue;
            if (minWidth < 0)   minWidth = 0;

            // Prevent the minimum being bigger than the maximum
            minWidth = Math.Min(minWidth, maxWidth);

            int firstUnderMax = -1;
            int lastOverMin = -1;
            int smallestWidth = int.MaxValue;
            for(int i=0; i<retWidths.Count; i++)
            {
                // Add on the fixed widths of the left and right borders so that the
                // permutations all reflect the actual width of the whole group
                GroupSizeWidth retWidth = retWidths[i];
                retWidth.Width += _totalBorders;

                // Find the first entry that is smaller than the maximum allowed
                if ((retWidth.Width <= maxWidth) && (firstUnderMax == -1))
                    firstUnderMax = i;

                // Find the last entry that is bigger than the minimum
                if (retWidth.Width >= minWidth)
                    lastOverMin = i;

                smallestWidth = Math.Min(smallestWidth, retWidth.Width);
            }

            // We only enforce min/max when not using the design helpers
            if (!_ribbon.InDesignHelperMode)
            {
                // If all permutations are above the maximum
                if (firstUnderMax == -1)
                {
                    if (retWidths.Count > 0)
                    {
                        // ...then use the smallest permutation by removing all the others
                        retWidths.RemoveRange(0, retWidths.Count - 2);
                        retWidths[0].Width = maxWidth;
                        smallestWidth = maxWidth;
                    }
                }
                else if (lastOverMin == -1)
                {
                    // All permutations are less than the minimum
                    if (retWidths.Count > 0)
                    {
                        // ...then use the largest permutation by removing all the others
                        retWidths.RemoveRange(1, retWidths.Count - 1);
                        retWidths[0].Width = minWidth;
                        smallestWidth = minWidth;
                    }
                }
                else if ((firstUnderMax > 0) || (smallestWidth < minWidth))
                {
                    // Create new list list with just the allowed sizes
                    List<GroupSizeWidth> newWidths = new List<GroupSizeWidth>();

                    // If the min/max are such that they both fall betweem two of the items then switch 
                    // to using the smaller of the two items. This can happen when max is same as min
                    // and does not exactly match an entry. Makes most sense to use the smaller perm.
                    if (firstUnderMax > lastOverMin)
                        lastOverMin = firstUnderMax;

                    // Reset smallest value which needs finding again
                    smallestWidth = int.MaxValue;
                    for (int i = firstUnderMax; i <= lastOverMin; i++)
                    {
                        // Get the original value
                        GroupSizeWidth retWidth = retWidths[i];

                        // If the last entry we override width with the minimum
                        if (!ignoreMin && (i == lastOverMin) && (retWidth.Width < minWidth))
                            retWidth.Width = minWidth;

                        // Append to end of the new list
                        newWidths.Add(retWidth);

                        // Remember the smallest width encountered
                        smallestWidth = Math.Min(smallestWidth, retWidth.Width);
                    }

                    // Use the new list
                    retWidths = newWidths;
                }
            }

            // Does the group allow itself to become collapsed?
            // (at design time we are never allowed to be collapsed)
            if (_ribbonGroup.AllowCollapsed && !_ribbon.InDesignHelperMode)
            {
                // We never allow a collapsed state if that is bigger than the smallest valid permutation
                if (smallestWidth > MINIMUM_GROUP_WIDTH)
                {
                    // Find the size of the group when collapsed
                    bool collapsed = Collapsed;
                    Collapsed = true;
                    GroupSizeWidth retCollapsed = new GroupSizeWidth(GetPreferredSize(context).Width, null);
                    Collapsed = collapsed;

                    // We never allow a collapsed state if that is smaller than the smallest valid permutation
                    if (smallestWidth > retCollapsed.Width)
                        retWidths.Add(retCollapsed);
                }
            }

            return retWidths.ToArray();
        }

        /// <summary>
        /// Update the group with the provided sizing solution.
        /// </summary>
        /// <param name="size">Solution size.</param>
        public void SetSolutionSize(ItemSizeWidth[] size)
        {
            // Should we become collapsed?
            Collapsed = (size == null);

            // Pass solution onto the contained view
            IRibbonViewGroupSize viewSize = (IRibbonViewGroupSize)_layoutNormalContent;
            viewSize.SetSolutionSize(size);
        }

        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
            Size preferredSize = base.GetPreferredSize(context);
            preferredSize.Width = Math.Max(preferredSize.Width, MINIMUM_GROUP_WIDTH);
            preferredSize.Height = _ribbon.CalculatedValues.GroupHeight;
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

            // Update the title element with the height of the group title area
            _viewNormalTitle.Height = _ribbon.CalculatedValues.GroupTitleHeight;

            // We only show the dialog box launcher button if group says we need one
            _viewNormalDialog.Visible = _ribbonGroup.DialogBoxLauncher;

            // Let contained content element be layed out
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
		}
		#endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            // Cache the control that we are showing inside
            _container = context.Control;

            if (Collapsed)
            {
                if (Pressed)
                    RenderCollapsedPressedBefore(context);
                else
                    RenderCollapsedBefore(context);
            }
            else
                RenderNormalBefore(context);
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
        private void UpdateShapeValues()
        {
            if (_ribbon != null)
            {
                if (_lastRibbonShape != _ribbon.RibbonShape)
                {
                    switch (_ribbon.RibbonShape)
                    {
                        default:
                        case PaletteRibbonShape.Office2007:
                            _totalBorders = TOTAL_LEFT_RIGHT_BORDERS_2007;
                            _layoutNormalMain.VertOffset = VERT_OFFSET_2007;
                            _layoutNormalSepTop.SeparatorSize = new Size(NORMAL_BORDER_TOPLEFT2007, NORMAL_BORDER_TOPLEFT2007);
                            _layoutNormalSepLeft.SeparatorSize = new Size(NORMAL_BORDER_TOPLEFT2007, NORMAL_BORDER_TOPLEFT2007);
                            _layoutNormalSepRight.SeparatorSize = new Size(NORMAL_BORDER_RIGHT2007, NORMAL_BORDER_RIGHT2007);
                            _layoutCollapsedImagePadding.PreferredPadding = COLLAPSED_IMAGE_PADDING_2007;
                            _lastRibbonShape = PaletteRibbonShape.Office2007;
                            break;
                        case PaletteRibbonShape.Office2010:
                            _totalBorders = TOTAL_LEFT_RIGHT_BORDERS_2010;
                            _layoutNormalMain.VertOffset = VERT_OFFSET_2010;
                            _layoutNormalSepTop.SeparatorSize = new Size(NORMAL_BORDER_TOP2010, NORMAL_BORDER_TOP2010);
                            _layoutNormalSepLeft.SeparatorSize = new Size(NORMAL_BORDER_LEFT2010, NORMAL_BORDER_LEFT2010);
                            _layoutNormalSepRight.SeparatorSize = new Size(NORMAL_BORDER_RIGHT2010, NORMAL_BORDER_RIGHT2010);
                            _layoutCollapsedImagePadding.PreferredPadding = COLLAPSED_IMAGE_PADDING_2010;
                            _lastRibbonShape = PaletteRibbonShape.Office2010; 
                            break;
                    }
                }
            }
        }

        private void CreateNormalView()
        {
            // Create a layout for the main area
            _layoutNormalMain = new ViewLayoutRibbonTitle();

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the group
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                _layoutNormalMain.MouseController = controller;
            }

            // Create layout elements
            _layoutNormalTitle = new ViewLayoutDocker();
            _layoutNormalContent = new ViewLayoutRibbonGroupContent(_ribbon, _ribbonGroup, _needPaint);
            _layoutNormalSepTop = new ViewLayoutRibbonSeparator(NORMAL_BORDER_TOPLEFT2007, true);
            _layoutNormalSepLeft = new ViewLayoutRibbonSeparator(NORMAL_BORDER_TOPLEFT2007, true);
            _layoutNormalSepRight = new ViewLayoutRibbonSeparator(NORMAL_BORDER_RIGHT2007, true);

            // Add layout elements to correct areas of the normal group
            _layoutNormalMain.Add(_layoutNormalTitle, ViewDockStyle.Bottom);
            _layoutNormalMain.Add(_layoutNormalSepTop, ViewDockStyle.Top);
            _layoutNormalMain.Add(_layoutNormalSepLeft, ViewDockStyle.Left);
            _layoutNormalMain.Add(_layoutNormalSepRight, ViewDockStyle.Right);
            _layoutNormalMain.Add(_layoutNormalContent, ViewDockStyle.Fill);

            // Create and add the title string that fills remainder title area
            _viewNormalTitle = new ViewDrawRibbonGroupTitle(_ribbon, _ribbonGroup);
            _layoutNormalTitle.Add(_viewNormalTitle, ViewDockStyle.Fill);

            // Add the dialog box launcher button to the right side of title area
            _viewNormalDialog = new ViewLayoutRibbonGroupButton(_ribbon, _ribbonGroup, _needPaint);
            _layoutNormalContent.DialogView = _viewNormalDialog;
            _layoutNormalTitle.Add(_viewNormalDialog, ViewDockStyle.Right);
            
            // Use this class to return the context color for any null values
            _paletteContextBack = new PaletteRibbonContextBack(_ribbon);

            // All values are equal to a default of Office 2007 shape
            _lastRibbonShape = PaletteRibbonShape.Office2007;
            _totalBorders = TOTAL_LEFT_RIGHT_BORDERS_2007;
        }

        private void CreateCollapsedView()
        {
            // Create a layout for the main area
            _layoutCollapsedMain = new ViewLayoutDocker();

            // Add a mouse controller so we know when it has been pressed
            _collapsedController = new CollapsedGroupController(_ribbon, _layoutCollapsedMain, _needPaint);
            _collapsedController.Click += new MouseEventHandler(OnCollapsedClick);
            _layoutCollapsedMain.MouseController = _collapsedController;
            _layoutCollapsedMain.SourceController = _collapsedController;
            _layoutCollapsedMain.KeyController = _collapsedController;

            // Reduce layout area to remove the group border
            ViewLayoutRibbonPadding layoutCollapsedInsidePadding = new ViewLayoutRibbonPadding(COLLAPSED_PADDING);
            _layoutCollapsedMain.Add(layoutCollapsedInsidePadding, ViewDockStyle.Fill);

            // Position at top an area that is padded for containing the image
            ViewLayoutDocker layoutCollapsedInside = new ViewLayoutDocker();
            layoutCollapsedInsidePadding.Add(layoutCollapsedInside);

            // Create the layout for the second line of text
            ViewLayoutRibbonRowCenter layoutCollapsedText2 = new ViewLayoutRibbonRowCenter();
            _viewCollapsedText2 = new ViewDrawRibbonGroupText(_ribbon, _ribbonGroup, false);
            layoutCollapsedText2.Add(_viewCollapsedText2);
            layoutCollapsedText2.Add(new ViewLayoutRibbonSeparator(2, 10, true));
            layoutCollapsedText2.Add(new ViewDrawRibbonDropArrow(_ribbon));
            layoutCollapsedText2.Add(new ViewLayoutRibbonSeparator(2, 10, true));
            layoutCollapsedInside.Add(layoutCollapsedText2, ViewDockStyle.Top);

            // Add the first line of text
            _viewCollapsedText1 = new ViewDrawRibbonGroupText(_ribbon, _ribbonGroup, true);
            layoutCollapsedInside.Add(_viewCollapsedText1, ViewDockStyle.Top);
            
            // Add group image frame
            _layoutCollapsedImagePadding = new ViewLayoutRibbonCenterPadding(COLLAPSED_IMAGE_PADDING_2007);
            layoutCollapsedInside.Add(_layoutCollapsedImagePadding, ViewDockStyle.Top);

            // Finally we add the actual drawing element for the collapsed group image
            ViewDrawRibbonGroupImage drawCollapsedImage = new ViewDrawRibbonGroupImage(_ribbon, _ribbonGroup, this);
            _layoutCollapsedImagePadding.Add(drawCollapsedImage);
        }

        private void RenderNormalBefore(RenderContext context)
        {
            Rectangle drawRect = ClientRectangle;

            IPaletteRibbonBack paletteBorder;
            IPaletteRibbonBack paletteTitle;

            // Are we a group inside a context tab?
            PaletteState elementState;
            if ((_ribbon.SelectedTab != null) && !string.IsNullOrEmpty(_ribbon.SelectedTab.ContextName))
                elementState = Tracking ? PaletteState.ContextTracking : PaletteState.ContextNormal;
            else
                elementState = Tracking ? PaletteState.Tracking : PaletteState.Normal;

            // Decide on the palette to use
            switch (elementState)
            {
                case PaletteState.ContextNormal:
                    paletteBorder = _ribbon.StateContextNormal.RibbonGroupNormalBorder;
                    paletteTitle = _ribbon.StateContextNormal.RibbonGroupNormalTitle;
                    break;
                case PaletteState.ContextTracking:
                    paletteBorder = _ribbon.StateContextTracking.RibbonGroupNormalBorder;
                    paletteTitle = _ribbon.StateContextTracking.RibbonGroupNormalTitle;
                    break;
                case PaletteState.Tracking:
                    paletteBorder = _ribbon.StateTracking.RibbonGroupNormalBorder;
                    paletteTitle = _ribbon.StateTracking.RibbonGroupNormalTitle;
                    break;
                case PaletteState.Normal:
                default:
                    paletteBorder = _ribbon.StateNormal.RibbonGroupNormalBorder;
                    paletteTitle = _ribbon.StateNormal.RibbonGroupNormalTitle;
                    break;
            }

            ElementState = elementState;

            // Indicate the showing of the group inside a popup by adding focus override
            if (_ribbonGroup.ShowingAsPopup)
                elementState |= PaletteState.FocusOverride;
            
            // Draw the group border
            _paletteContextBack.SetInherit(paletteBorder);
            _mementoRibbonBack1 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, elementState, _paletteContextBack, VisualOrientation.Top, false, _mementoRibbonBack1);

            // Reduce the drawing rectangle to just the title area
            Rectangle titleRect = drawRect;
            titleRect.X++;
            titleRect.Width -= 2;
            titleRect.Y = titleRect.Bottom - _viewNormalTitle.Height;
            titleRect.Height = _viewNormalTitle.Height - 1;

            if (paletteBorder.GetRibbonBackColorStyle(State) == PaletteRibbonColorStyle.RibbonGroupNormalBorderTrackingLight)
            {
                // Redraw the title area inside the light border area
                titleRect.X++;
                titleRect.Width -= 2;
                titleRect.Height--;
            }

            // Draw the group title
            _mementoRibbonBack2 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, titleRect, State, paletteTitle, VisualOrientation.Top, false, _mementoRibbonBack2);
        }

        private void RenderCollapsedBefore(RenderContext context)
        {
            Rectangle drawRect = ClientRectangle;

            IPaletteRibbonBack paletteBack;
            IPaletteRibbonBack paletteBorder;

            if (_collapsedController.HasFocus)
                ElementState = PaletteState.Tracking;
            else
            {
                // Are we a group inside a context tab?
                if ((_ribbon.SelectedTab != null) && !string.IsNullOrEmpty(_ribbon.SelectedTab.ContextName))
                    ElementState = Tracking ? PaletteState.ContextTracking : PaletteState.ContextNormal;
                else
                    ElementState = Tracking ? PaletteState.Tracking : PaletteState.Normal;
            }

            // Decide on the palette to use
            switch (State)
            {
                case PaletteState.ContextNormal:
                    paletteBack = _ribbon.StateContextNormal.RibbonGroupCollapsedBack;
                    paletteBorder = _ribbon.StateContextNormal.RibbonGroupCollapsedBorder;
                    break;
                case PaletteState.ContextTracking:
                    paletteBack = _ribbon.StateContextTracking.RibbonGroupCollapsedBack;
                    paletteBorder = _ribbon.StateContextTracking.RibbonGroupCollapsedBorder;
                    break;
                case PaletteState.Tracking:
                    paletteBack = _ribbon.StateTracking.RibbonGroupCollapsedBack;
                    paletteBorder = _ribbon.StateTracking.RibbonGroupCollapsedBorder;
                    break;
                case PaletteState.Normal:
                default:
                    paletteBack = _ribbon.StateNormal.RibbonGroupCollapsedBack;
                    paletteBorder = _ribbon.StateNormal.RibbonGroupCollapsedBorder;
                    break;
            }

            // Draw the group border
            _paletteContextBack.SetInherit(paletteBorder);
            _mementoRibbonBack1 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, State, _paletteContextBack, VisualOrientation.Top, false, _mementoRibbonBack1);

            Rectangle backRect = drawRect;
            backRect.Inflate(-2, -2);

            // Draw the inside background
            _mementoRibbonBack2 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, backRect, State, paletteBack, VisualOrientation.Top, false, _mementoRibbonBack2);
        }

        private void RenderCollapsedPressedBefore(RenderContext context)
        {
            switch (_lastRibbonShape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    {
                        IPaletteBack paletteBack = _ribbon.StateCommon.RibbonGroupCollapsedButton.PaletteBack;
                        IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonGroupCollapsedButton.PaletteBorder;

                        // Do we need to draw the background?
                        if (paletteBack.GetBackDraw(PaletteState.Pressed) == InheritBool.True)
                        {
                            // Get the border path which the background is clipped to drawing within
                            using (GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder, VisualOrientation.Top, PaletteState.Pressed))
                            {
                                Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, PaletteState.Pressed, VisualOrientation.Top);

                                // Apply the padding depending on the orientation
                                Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

                                // Render the background inside the border path
                                _mementoStandardBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                                                                                                    paletteBack, VisualOrientation.Top,
                                                                                                    PaletteState.Pressed, _mementoStandardBack);
                            }
                        }

                        // Do we need to draw the border?
                        if (paletteBorder.GetBorderDraw(PaletteState.Pressed) == InheritBool.True)
                            context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder,
                                                                             VisualOrientation.Top, PaletteState.Pressed);
                    }
                    break;
                case PaletteRibbonShape.Office2010:
                    {
                        Rectangle drawRect = ClientRectangle;

                        IPaletteRibbonBack paletteBack = _ribbon.StatePressed.RibbonGroupCollapsedBack;
                        IPaletteRibbonBack paletteBorder = _ribbon.StatePressed.RibbonGroupCollapsedBorder;

                        PaletteState state = PaletteState.Pressed;

                        // Are we a group inside a context tab?
                        if ((_ribbon.SelectedTab != null) && !string.IsNullOrEmpty(_ribbon.SelectedTab.ContextName))
                            state = PaletteState.ContextPressed;

                        // Draw the group border
                        _paletteContextBack.SetInherit(paletteBorder);
                        _mementoRibbonBack1 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, drawRect, state, _paletteContextBack, VisualOrientation.Top, false, _mementoRibbonBack1);

                        Rectangle backRect = drawRect;
                        backRect.Inflate(-2, -2);

                        // Draw the inside background
                        _mementoRibbonBack2 = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, backRect, state, paletteBack, VisualOrientation.Top, false, _mementoRibbonBack2);
                    }
                    break;
            }
        }

        private void DisposeMementos()
        {
            if (_mementoRibbonBack1 != null)
            {
                _mementoRibbonBack1.Dispose();
                _mementoRibbonBack1 = null;
            }

            if (_mementoRibbonBack2 != null)
            {
                _mementoRibbonBack2.Dispose();
                _mementoRibbonBack2 = null;
            }

            if (_mementoStandardBack != null)
            {
                _mementoStandardBack.Dispose();
                _mementoStandardBack = null;
            }
        }

        private void OnCollapsedClick(object sender, MouseEventArgs e)
        {
            // We do not operate the collapsed button at design time
            if (!_ribbon.InDesignMode)
            {
                // We are pressed until the popup window is removed
                _pressed = true;
                _container.Refresh();

                // Create the popup window for the group
                _popupGroup = new VisualPopupGroup(_ribbon, _ribbonGroup, _ribbon.Renderer);

                // We need to know when disposed so the pressed state can be reversed
                _popupGroup.Disposed += new EventHandler(OnVisualPopupGroupDisposed);

                // Ask the popup to show itself relative to ourself
                _popupGroup.ShowCalculatingSize(this, _container.RectangleToScreen(ClientRectangle));
            }
        }

        private void OnVisualPopupGroupDisposed(object sender, EventArgs e)
        {
            // Not pressed any more
            _popupGroup = null;
            _pressed = false;
            _tracking = false;
            _container.Refresh();
        }

        private void OnGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool updateLayout = false;
            bool updatePaint = false;

            switch (e.PropertyName)
            {
                case "Visible":
                case "AllowCollapsed":
                case "DialogBoxLauncher":
                case "MaximumWidth":
                case "MinimumWidth":
                    updateLayout = true;
                    break;
                case "TextLine1":
                    _viewNormalTitle.MakeDirty();
                    _viewCollapsedText1.MakeDirty();
                    updateLayout = true;
                    break;
                case "TextLine2":
                    _viewNormalTitle.MakeDirty();
                    _viewCollapsedText2.MakeDirty();
                    updateLayout = true;
                    break;
                case "Image":
                    updatePaint = true;
                    break;
            }

            if (updateLayout)
            {
                // If we are on the currently selected tab then...
                if ((_ribbonGroup.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonGroup.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
            }

            if (updatePaint)
            {
                // If this button is actually defined as visible...
                if (_ribbonGroup.Visible)
                {
                    // ...and on the currently selected tab then...
                    if ((_ribbonGroup.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonGroup.RibbonTab))
                    {
                        // ...repaint it right now
                        OnNeedPaint(false, ClientRectangle);
                    }
                }
            }
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            _ribbonGroup.OnDesignTimeContextMenu(new MouseEventArgs(MouseButtons.Right, 1, e.X, e.Y, 0));
        }
        #endregion
    }
}
