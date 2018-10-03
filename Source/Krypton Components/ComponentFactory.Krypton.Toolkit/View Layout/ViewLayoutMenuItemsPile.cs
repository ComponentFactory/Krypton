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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the ViewLayoutPile so that menu items are layed out in columns.
	/// </summary>
    public class ViewLayoutMenuItemsPile : ViewLayoutPile
    {
        #region Type Definitions
        private class ColumnToWidth : Dictionary<int, int> { };
        #endregion

        #region Instance Fields
        private PaletteDoubleMetricRedirect _paletteItemHighlight;
        private ViewDrawMenuImageColumn _imageColumn;
        private ViewLayoutStack _itemStack;
        private ColumnToWidth _columnToWidth;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutMenuItemsPile class.
		/// </summary>
        /// <param name="provider">Provider of context menu values.</param>
        /// <param name="items">Reference to the owning collection.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        public ViewLayoutMenuItemsPile(IContextMenuProvider provider,
                                       KryptonContextMenuItems items,
                                       bool standardStyle,
                                       bool imageColumn)
        {
            // Cache access to the highlight item palette
            _paletteItemHighlight = provider.ProviderStateCommon.ItemHighlight;

            // Create and place an image column inside a docker so it appears on the left side
            _imageColumn = new ViewDrawMenuImageColumn(items, provider.ProviderStateCommon.ItemImageColumn);
            ViewLayoutDocker imageDocker = new ViewLayoutDocker();
            imageDocker.Add(_imageColumn, ViewDockStyle.Left);

            // Only show the image column when in a standard collection of items
            imageDocker.Visible = imageColumn;

            // Create a vertical stack that contains each individual menu item
            _itemStack = new ViewLayoutStack(false);
            _itemStack.FillLastChild = false;

            // Use a docker with the item stack as the fill
            ViewLayoutDocker stackDocker = new ViewLayoutDocker();
            stackDocker.Add(_itemStack, ViewDockStyle.Fill);

            // Grab the padding for around the item stack
            Padding itemsPadding = _paletteItemHighlight.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.ContextMenuItemsCollection);
            stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Left), ViewDockStyle.Left);
            stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Right), ViewDockStyle.Right);
            stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Top), ViewDockStyle.Top);
            stackDocker.Add(new ViewLayoutSeparator(itemsPadding.Bottom), ViewDockStyle.Bottom);

            // The background of the pile is the image column
            Add(imageDocker);

            // The foreground of the pile is the stack of menu items
            Add(stackDocker);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutMenuItemsPile:" + Id;
		}
		#endregion

        #region ItemStack
        /// <summary>
        /// Gets access to the stack containing individual menu items
        /// </summary>
        public ViewLayoutStack ItemStack
        {
            get { return _itemStack; }
        }
        #endregion

        #region Layout
		/// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            // Reset the column size information
            _columnToWidth = new ColumnToWidth();

            // Remove any override currently in place for columns
            ClearMenuItemColumns(this);

            base.GetPreferredSize(context);

            // Gather the largest size of each column instance
            GatherMenuItemColumns(this);

            // Tell each column to use the largest size for that column
            OverrideMenuItemColumns(this);

            // Modify the first (image) column for the padding of the item highlight
            UpdateImageColumnWidth(context.Renderer);

            return base.GetPreferredSize(context);
		}

        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
			Debug.Assert(context != null);
            base.Layout(context);
		}
		#endregion  
  
        #region Implementation
        private void GatherMenuItemColumns(ViewBase element)
        {
            // Does this element expose the column interface?
            if (element is IContextMenuItemColumn)
            {
                IContextMenuItemColumn column = (IContextMenuItemColumn)element;
                int columnIndex = column.ColumnIndex;
                Size columnPreferredSize = column.LastPreferredSize;

                // If the first entry for this column...
                if (!_columnToWidth.ContainsKey(columnIndex))
                    _columnToWidth.Add(columnIndex, columnPreferredSize.Width);
                else
                {
                    // Grab the current preferred size
                    int preferredWidth = _columnToWidth[columnIndex];

                    // Find the largest sizing
                    preferredWidth = Math.Max(preferredWidth, columnPreferredSize.Width);

                    // Put modified value back
                    _columnToWidth[columnIndex] = preferredWidth;
                }
            }

            // Process child elements
            foreach (ViewBase child in element)
                GatherMenuItemColumns(child);
        }

        private void OverrideMenuItemColumns(ViewBase element)
        {
            // Does this element expose the column interface?
            if (element is IContextMenuItemColumn)
            {
                IContextMenuItemColumn column = (IContextMenuItemColumn)element;
                column.OverridePreferredWidth = _columnToWidth[column.ColumnIndex];
            }

            // Process child elements
            foreach (ViewBase child in element)
                OverrideMenuItemColumns(child);
        }

        private void ClearMenuItemColumns(ViewBase element)
        {
            // Does this element expose the column interface?
            if (element is IContextMenuItemColumn)
            {
                IContextMenuItemColumn column = (IContextMenuItemColumn)element;
                column.OverridePreferredWidth = 0;
            }

            // Process child elements
            foreach (ViewBase child in element)
                ClearMenuItemColumns(child);
        }

        private void UpdateImageColumnWidth(IRenderer renderer)
        {
            // If there is an image column then we will have a entry for index 0
            if (_columnToWidth.ContainsKey(0))
            {
                // Find the border padding that is applied to the content of the menu item
                Padding borderPadding = renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteItemHighlight.Border,
                                                                                              PaletteState.Normal,
                                                                                              VisualOrientation.Top);

                // Add double the left edge to the right edge of the image background coumn
                int imageColumnWidth = _columnToWidth[0];
                imageColumnWidth += borderPadding.Left * 3;

                // Add double the metric padding that occurs outside the item highlight
                Padding itemMetricPadding = _paletteItemHighlight.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.ContextMenuItemHighlight);
                imageColumnWidth += itemMetricPadding.Left * 2;

                _imageColumn.ColumnWidth = imageColumnWidth;
            }
        }
        #endregion
    }
}
