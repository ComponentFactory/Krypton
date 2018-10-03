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
	/// Draws a long vertical group separator.
	/// </summary>
    internal class ViewDrawRibbonGroupSeparator : ViewLeaf,
                                                  IRibbonViewGroupContainerView
    {
        #region Static Fields
        private static readonly Size _preferredSize2007 = new Size(4, 4);
        private static readonly Size _preferredSize2010 = new Size(7, 4);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupSeparator _ribbonSeparator;
        private NeedPaintHandler _needPaint;
        private Size _preferredSize;
        private PaletteRibbonShape _lastShape;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupSeparator class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonSeparator">Reference to group separator definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupSeparator(KryptonRibbon ribbon,
                                            KryptonRibbonGroupSeparator ribbonSeparator,
                                            NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonSeparator != null);
            Debug.Assert(needPaint != null);

            _ribbon = ribbon;
            _ribbonSeparator = ribbonSeparator;
            _needPaint = needPaint;

            // Associate this view with the source component (required for design time selection)
            Component = _ribbonSeparator;

            if (_ribbon.InDesignMode)
            {
                // At design time we need to know when the user right clicks the label
                ContextClickController controller = new ContextClickController();
                controller.ContextClick += new MouseEventHandler(OnContextClick);
                MouseController = controller;
            }

            // Define back reference to view for the separator definition
            _ribbonSeparator.SeparatorView = this;

            // Hook into changes in the ribbon separator definition
            _ribbonSeparator.PropertyChanged += new PropertyChangedEventHandler(OnSeparatorPropertyChanged);

            // Default the preferred size
            _lastShape = PaletteRibbonShape.Office2007;
            _preferredSize = _preferredSize2007;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupSeparator:" + Id;
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
                _ribbonSeparator.PropertyChanged -= new PropertyChangedEventHandler(OnSeparatorPropertyChanged);

                // Remove association with definition
                _ribbonSeparator.SeparatorView = null;
                _ribbonSeparator = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GetFirstFocusItem
        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public ViewBase GetFirstFocusItem()
        {
            // We never have any child items that can take focus
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
            // We never have any child items that can take focus
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
            // We never have any child items that can take focus
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
            // We never have any child items that can take focus
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
            // Separator never has key tips
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
            if (_lastShape != _ribbon.RibbonShape)
            {
                switch (_ribbon.RibbonShape)
                {
                    default:
                    case PaletteRibbonShape.Office2007:
                        _lastShape = PaletteRibbonShape.Office2007;
                        _preferredSize = _preferredSize2007;
                        break;
                    case PaletteRibbonShape.Office2010:
                        _lastShape = PaletteRibbonShape.Office2010;
                        _preferredSize = _preferredSize2010;
                        break;
                }
            }

            // Return the one possible size allowed
            return new ItemSizeWidth[] { new ItemSizeWidth(GroupItemSize.Large, _preferredSize.Width) };
        }

        /// <summary>
        /// Update the group with the provided sizing solution.
        /// </summary>
        /// <param name="size">Value for the container.</param>
        public void SetSolutionSize(ItemSizeWidth size)
        {
            // Solution should always be the large, the only size we can be
            Debug.Assert(size.GroupItemSize == GroupItemSize.Large);
        }

        /// <summary>
        /// Reset the container back to its requested size.
        /// </summary>
        public void ResetSolutionSize()
        {
        }

        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return _preferredSize;
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
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            context.Renderer.RenderGlyph.DrawRibbonGroupSeparator(_ribbon.RibbonShape, 
                                                                  context, 
                                                                  ClientRectangle, 
                                                                  _ribbon.StateCommon.RibbonGeneral, 
                                                                  State);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected virtual void OnNeedPaint(bool needLayout)
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
            _ribbonSeparator.OnDesignTimeContextMenu(e);
        }

        private void OnSeparatorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Visible":
                    // If we are on the currently selected tab then...
                    if ((_ribbonSeparator.RibbonTab != null) &&
                        (_ribbon.SelectedTab == _ribbonSeparator.RibbonTab))
                    {
                        // ...layout so the visible change is made
                        OnNeedPaint(true);
                    }
                    break;
            }
        }
        #endregion
    }
}
