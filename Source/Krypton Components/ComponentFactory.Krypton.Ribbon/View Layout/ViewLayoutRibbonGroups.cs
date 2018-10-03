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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Extends the ViewComposite by creating and laying out elements to represent ribbon groups.
	/// </summary>
    internal class ViewLayoutRibbonGroups : ViewComposite
    {
        #region Classes
        private class GroupToView : Dictionary<KryptonRibbonGroup, ViewDrawRibbonGroup> {};
        private class ViewDrawRibbonGroupSepList : List<ViewLayoutRibbonSeparator> { };
        #endregion

        #region Statis Fields
        private static readonly int SEP_LENGTH_2007 = 2;
        private static readonly int SEP_LENGTH_2010 = 0;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonTab _ribbonTab;
        private NeedPaintHandler _needPaint;
        private ViewDrawRibbonDesignGroup _viewAddGroup;
        private GroupToView _groupToView;
        private ViewDrawRibbonGroupSepList _groupSepCache;
        private int[] _groupWidths;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGroups class.
		/// </summary>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="ribbonTab">RibbonTab to organize groups.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewLayoutRibbonGroups(KryptonRibbon ribbon,
                                      KryptonRibbonTab ribbonTab,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonTab != null);
            Debug.Assert(needPaint != null);

            // Cache references
            _ribbon = ribbon;
            _ribbonTab = ribbonTab;
            _needPaint = needPaint;

            // Create initial lookup table
            _groupToView = new GroupToView();

            // Create cache of group separator elements
            _groupSepCache = new ViewDrawRibbonGroupSepList();
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonGroups:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Clear();

                foreach (ViewDrawRibbonGroup group in _groupToView.Values)
                    group.Dispose();

                foreach (ViewLayoutRibbonSeparator sep in _groupSepCache)
                    sep.Dispose();

                _groupToView.Clear();
                _groupSepCache.Clear();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region NeedPaintDelegate
        /// <summary>
        /// Set the new paint delegate to use for painting requests.
        /// </summary>
        public NeedPaintHandler NeedPaintDelegate
        {
            set { _needPaint = value; }
        }
        #endregion

        #region ViewGroupFromPoint
        /// <summary>
        /// Gets the view element group that the provided point is inside.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>Reference if inside a group; otherwise null.</returns>
        public ViewDrawRibbonGroup ViewGroupFromPoint(Point pt)
        {
            // Parent element should be a view layout
            ViewLayoutControl layoutControl = (ViewLayoutControl)Parent;

            // Get the location of the child control it contains
            Point layoutLocation = layoutControl.ChildControl.Location;

            // Adjust the incoming point for the location of the child control
            pt.X -= layoutLocation.X;
            pt.Y -= layoutLocation.Y;

            // Search the child collection for matching group elements
            foreach (ViewBase child in this)
            {
                // Ignore hidden elements
                if (child.Visible)
                {
                    ViewDrawRibbonGroup group = child as ViewDrawRibbonGroup;

                    // Only interested in group instances (not separators or others)
                    if (group != null)
                    {
                        // Does this group match?
                        if (group.ClientRectangle.Contains(pt))
                            return group;
                    }
                }
            }

            return null;
        }
        #endregion

        #region GetGroupKeyTips
        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <returns>Array of KeyTipInfo; otherwise null.</returns>
        public KeyTipInfo[] GetGroupKeyTips()
        {
            KeyTipInfoList keyTipList = new KeyTipInfoList();

            // Ask each visible group to add its own key tips
            foreach (ViewDrawRibbonGroup group in _groupToView.Values)
                if (group.Visible)
                    group.GetGroupKeyTips(keyTipList);

            return keyTipList.ToArray();
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the groups.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            ViewBase view = null;

            // Search each group until one of them returns a focus item
            foreach (ViewDrawRibbonGroup group in _groupToView.Values)
            {
                view = group.GetFirstFocusItem();
                if (view != null)
                    break;
            }

            return view;
        }
        #endregion

        #region GetLastFocusItem
        /// <summary>
        /// Gets the last focus item from the groups.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetLastFocusItem()
        {
            ViewBase view = null;

            ViewDrawRibbonGroup[] groups = new ViewDrawRibbonGroup[_groupToView.Count];
            _groupToView.Values.CopyTo(groups, 0);

            // Search each group until one of them returns a focus item
            for (int i = groups.Length - 1; i >= 0; i--)
            {
                view = groups[i].GetLastFocusItem();
                if (view != null)
                    break;
            }

            return view;
        }
        #endregion

        #region GetNextFocusItem
        /// <summary>
        /// Gets the next focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetNextFocusItem(ViewBase current)
        {
            ViewBase view = null;
            bool matched = false;

            // Search each group until one of them returns a focus item
            foreach (ViewDrawRibbonGroup group in _groupToView.Values)
            {
                // Already matched means we need the next item we come across,
                // otherwise we continue with the attempt to find next
                if (matched)
                    view = group.GetFirstFocusItem();
                else
                    view = group.GetNextFocusItem(current, ref matched);

                if (view != null)
                    break;
            }

            return view;
        }
        #endregion

        #region GetPreviousFocusItem
        /// <summary>
        /// Gets the previous focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetPreviousFocusItem(ViewBase current)
        {
            ViewBase view = null;
            bool matched = false;

            ViewDrawRibbonGroup[] groups = new ViewDrawRibbonGroup[_groupToView.Count];
            _groupToView.Values.CopyTo(groups, 0);

            // Search each group until one of them returns a focus item
            for (int i = groups.Length - 1; i >= 0; i--)
            {
                // Already matched means we need the next item we come across,
                // otherwise we continue with the attempt to find previous
                if (matched)
                    view = groups[i].GetLastFocusItem();
                else
                    view = groups[i].GetPreviousFocusItem(current, ref matched);

                if (view != null)
                    break;
            }

            return view;
        }
        #endregion

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
            // Sync to represent the current ribbon groups for tab
            SyncChildrenToRibbonGroups();

            // Find best size for groups to fill available space
            return new Size(AdjustGroupStateToMatchSpace(context), _ribbon.CalculatedValues.GroupHeight);
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

            int x = ClientLocation.X;

            // Are there any children to layout?
            if (this.Count > 0)
            {
                int y = ClientLocation.Y;
                int height = ClientHeight;

                // Position each item from left to right taking up entire height
                for (int i = 0, j = 0; i < this.Count; i++)
                {
                    ViewBase child = this[i];

                    // We only position visible items
                    if (child.Visible)
                    {
                        // Cache preferred size of the child
                        Size childSize;

                        // If a group then pull in the cached value
                        if (child is ViewDrawRibbonGroup)
                            childSize = new Size(_groupWidths[j++], _ribbon.CalculatedValues.GroupHeight);
                        else
                            childSize = this[i].GetPreferredSize(context);

                        // Only interested in items with some width
                        if (childSize.Width > 0)
                        {
                            // Define display rectangle for the group
                            context.DisplayRectangle = new Rectangle(x, y, childSize.Width, height);

                            // Position the element
                            this[i].Layout(context);

                            // Move across to next position
                            x += childSize.Width;
                        }
                    }
                }
            }

            // Update our own size to reflect how wide we actually need to be for all the children
            ClientRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));

            // Update the display rectangle we allocated for use by parent
            context.DisplayRectangle = new Rectangle(ClientLocation, new Size(x - ClientLocation.X, ClientHeight));
		}
		#endregion

        #region Implementation
        private Size SeparatorSize
        {
            get
            {
                Size retSize = Size.Empty;

                if (_ribbon != null)
                {
                    switch (_ribbon.RibbonShape)
                    {
                        default:
                        case PaletteRibbonShape.Office2007:
                            retSize = new Size(SEP_LENGTH_2007, SEP_LENGTH_2007);
                            break;
                        case PaletteRibbonShape.Office2010:
                            retSize = new Size(SEP_LENGTH_2010, SEP_LENGTH_2010);
                            break;
                    }
                }

                return retSize;
            }
        }

        private void SyncChildrenToRibbonGroups()
        {
            // Remove all child elements
            Clear();

            // Create a new lookup that reflects any changes in groups
            GroupToView regenerate = new GroupToView();
            
            // Make sure we have a view element to match each group
            foreach(KryptonRibbonGroup group in _ribbonTab.Groups)
            {
                ViewDrawRibbonGroup view = null;

                // Get the currently cached view for the group
                if (_groupToView.ContainsKey(group))
                    view = _groupToView[group];

                // If a new group, create a view for it now
                if (view == null)
                    view = new ViewDrawRibbonGroup(_ribbon, group, _needPaint);

                // Add to the lookup for future reference
                regenerate.Add(group, view);
            }

            if (_groupSepCache.Count < _ribbonTab.Groups.Count)
                for (int i = _groupSepCache.Count; i < _ribbonTab.Groups.Count; i++)
                    _groupSepCache.Add(new ViewLayoutRibbonSeparator(0, true));

            // Update size of all separators to match ribbon shape
            Size sepSize = SeparatorSize;
            foreach (ViewLayoutRibbonSeparator sep in _groupSepCache)
                sep.SeparatorSize = sepSize;

            // We ignore the first separator
            bool ignoreSep = true;

            // Add child elements appropriate for each ribbon group
            for (int i = 0; i < _ribbonTab.Groups.Count; i++)
            {
                KryptonRibbonGroup ribbonGroup = _ribbonTab.Groups[i];

                // Only make the separator visible if the group is and not the first sep
                bool groupVisible = (_ribbon.InDesignHelperMode || ribbonGroup.Visible);
                _groupSepCache[i].Visible = groupVisible && !ignoreSep;
                regenerate[ribbonGroup].Visible = groupVisible;

                // Only add a separator for the second group onwards
                if (groupVisible && ignoreSep)
                    ignoreSep = false;

                Add(_groupSepCache[i]);
                Add(regenerate[ribbonGroup]);
                
                // Remove entries we still are using
                if (_groupToView.ContainsKey(ribbonGroup))
                    _groupToView.Remove(ribbonGroup);
            }

            // When in design time help mode
            if (_ribbon.InDesignHelperMode)
            {
                // Create the design time 'Add Group' first time it is needed
                if (_viewAddGroup == null)
                    _viewAddGroup = new ViewDrawRibbonDesignGroup(_ribbon, _needPaint);

                // Always add at end of the list of groups
                Add(_viewAddGroup);
            }

            // Dispose of views no longer required
            foreach(ViewDrawRibbonGroup group in _groupToView.Values)
                group.Dispose();

            // No longer need the old lookup
            _groupToView = regenerate;
        }

        private int AdjustGroupStateToMatchSpace(ViewLayoutContext context)
        {
            List<GroupSizeWidth[]> listWidths = new List<GroupSizeWidth[]>();
            List<IRibbonViewGroupSize> listGroups = new List<IRibbonViewGroupSize>();

            // Scan all groups
            int pixelGaps = 0;
            int maxEntries = 0;
            foreach (ViewBase child in this)
            {
                if (child.Visible)
                {
                    // Only interested in children that are actually groups
                    if (child is IRibbonViewGroupSize)
                    {
                        // Cast child view to correct interface
                        IRibbonViewGroupSize childSize = (IRibbonViewGroupSize)child;

                        // Find list of possible sizes for this group
                        GroupSizeWidth[] widths = childSize.GetPossibleSizes(context);

                        // Track how many extra pixels are needed for inter group gaps
                        pixelGaps += SEP_LENGTH_2007;

                        // Add into list of all container values
                        listWidths.Add(widths);
                        listGroups.Add(childSize);

                        // Track the longest list found
                        maxEntries = Math.Max(maxEntries, widths.Length);
                    }
                }
            }

            int bestWidth = 0;
            int availableWidth = context.DisplayRectangle.Width;
            int[] bestIndexes = null;
            List<int> permIndexes = new List<int>();

            // Scan each horizontal slice of the 2D array of values
            for (int i = 0; i < maxEntries; i++)
            {
                // Move from right to left creating a permutation each time
                for (int j = listWidths.Count - 1; j >= 0; j--)
                {
                    // Does this cell actually exist?
                    if (listWidths[j].Length > i)
                    {
                        // Starting width is the pixel gaps
                        int permTotalWidth = pixelGaps;
                        permIndexes.Clear();

                        // Generate permutation by taking cell values
                        for (int k = listWidths.Count - 1; k >= 0; k--)
                        {
                            // If we are on the left of the 'j' cell then move up a level
                            int index = i + (k > j ? 1 : 0);

                            // Limit check the index to available height
                            index = Math.Min(index, listWidths[k].Length - 1);
                            permIndexes.Insert(0, index);

                            // Find width and size of the entry
                            int width = listWidths[k][index].Width;

                            // Track the total width of this permutation
                            permTotalWidth += width;
                        }

                        // We record this as the best match so far, if either it is the first permutation
                        // tried or if closest to filling the entire available width of the client area
                        if ((permTotalWidth > bestWidth) && (permTotalWidth <= availableWidth))
                        {
                            bestWidth = permTotalWidth;
                            bestIndexes = permIndexes.ToArray();
                        }
                    }
                }
            }

            // If we have a best fit solution
            if (bestWidth > 0)
            {
                // Use the best discovered solution and push it back to the groups
                _groupWidths = new int[listGroups.Count];
                for (int i = 0; i < listGroups.Count; i++)
                {
                    _groupWidths[i] = (listWidths[i][bestIndexes[i]].Width);
                    listGroups[i].SetSolutionSize(listWidths[i][bestIndexes[i]].Sizing);
                }
            }
            else
            {
                // Use the smallest solution and push it back to the groups
                _groupWidths = new int[listGroups.Count];
                for (int i = 0; i < listGroups.Count; i++)
                {
                    _groupWidths[i] = (listWidths[i][listWidths[i].Length - 1].Width);
                    listGroups[i].SetSolutionSize(listWidths[i][listWidths[i].Length - 1].Sizing);
                }
            }

            return bestWidth;
        }
        #endregion
    }
}
