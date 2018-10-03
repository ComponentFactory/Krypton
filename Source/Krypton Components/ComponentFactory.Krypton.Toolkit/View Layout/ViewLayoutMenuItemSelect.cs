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
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// View element that creates and lays out the image list items.
	/// </summary>
    internal class ViewLayoutMenuItemSelect : ViewComposite
    {
        #region Instance Fields
        private ViewContextMenuManager _viewManager;
        private KryptonContextMenuImageSelect _itemSelect;
        private IContextMenuProvider _provider;
        private PaletteTripleToPalette _triple;
        private NeedPaintHandler _needPaint;
        private ImageList _imageList;
        private int _selectedIndex;
        private int _imageIndexStart;
        private int _imageIndexEnd;
        private int _imageIndexCount;
        private int _imageCount;
        private int _lineItems;
        private Padding _padding;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutMenuItemSelect class.
        /// </summary>
        /// <param name="itemSelect">Reference to owning instance.</param>
        /// <param name="provider">Provider of context menu information.</param>
        public ViewLayoutMenuItemSelect(KryptonContextMenuImageSelect itemSelect,
                                        IContextMenuProvider provider)
        {
            Debug.Assert(itemSelect != null);
            Debug.Assert(provider != null);

            // Store incoming references
            _itemSelect = itemSelect;
            _provider = provider;

            _itemSelect.TrackingIndex = -1;
            _enabled = provider.ProviderEnabled;
            _viewManager = provider.ProviderViewManager;

            // Cache the values to use when running
            _imageList = _itemSelect.ImageList;
            _imageIndexStart = _itemSelect.ImageIndexStart;
            _imageIndexEnd = _itemSelect.ImageIndexEnd;
            _lineItems = _itemSelect.LineItems;
            _needPaint = provider.ProviderNeedPaintDelegate;
            _padding = _itemSelect.Padding;
            _imageCount = (_imageList == null ? 0 : _imageList.Images.Count);

            // Limit check the start and end values
            _imageIndexStart = Math.Max(0, _imageIndexStart);
            _imageIndexEnd = Math.Min(_imageIndexEnd, _imageCount - 1);
            _imageIndexCount = Math.Max(0, (_imageIndexEnd - _imageIndexStart) + 1);

            IPalette palette = provider.ProviderPalette;
            if (palette == null)
                palette = KryptonManager.GetPaletteForMode(provider.ProviderPaletteMode);

            // Create triple that can be used by the draw button
            _triple = new PaletteTripleToPalette(palette,
                                                 PaletteBackStyle.ButtonLowProfile,
                                                 PaletteBorderStyle.ButtonLowProfile,
                                                 PaletteContentStyle.ButtonLowProfile);

            // Update with current button style
            _triple.SetStyles(itemSelect.ButtonStyle);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutMenuItemSelect:" + Id;
		}
        #endregion

        #region ItemEnabled
        /// <summary>
        /// Gets the enabled state of the item.
        /// </summary>
        public bool ItemEnabled
        {
            get { return _enabled; }
        }
        #endregion

        #region CanCloseMenu
        /// <summary>
        /// Gets a value indicating if the menu is capable of being closed.
        /// </summary>
        public bool CanCloseMenu
        {
            get { return _provider.ProviderCanCloseMenu; }
        }
        #endregion

        #region Closing
        /// <summary>
        /// Raises the Closing event on the provider.
        /// </summary>
        /// <param name="cea">A CancelEventArgs containing the event data.</param>
        public void Closing(CancelEventArgs cea)
        {
            _provider.OnClosing(cea);
        }
        #endregion

        #region Close
        /// <summary>
        /// Raises the Close event on the provider.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        public void Close(CloseReasonEventArgs e)
        {
            _provider.OnClose(e);
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

            // Ensure that the correct number of children are created
            SyncChildren();

            Size preferredSize = Size.Empty;

            // Find size of the first item, if there is one
            if (Count > 0)
            {
                // Ask child for it's own preferred size
                preferredSize = this[0].GetPreferredSize(context);

                // Find preferred size from the preferred item size
                int lineItems = Math.Max(1, _lineItems);
                preferredSize.Width *= lineItems;
                preferredSize.Height *= (Count + (lineItems - 1)) / lineItems;
            }

            // Add on the requests padding
            preferredSize.Width += _padding.Horizontal;
            preferredSize.Height += _padding.Vertical;

            return preferredSize;
        }

        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");
            
            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            // Ensure that the correct number of children are created
            SyncChildren();

            // Is there anything to layout?
            if (Count > 0)
            {
                // Reduce the client area by the requested padding before the internal children
                Rectangle displayRect = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _padding);

                // Get size of the first child, assume all others are same size
                Size itemSize = this[0].GetPreferredSize(context);

                // Starting position for first item
                Point nextPoint = displayRect.Location;
                for (int i = 0; i < Count; i++)
                {
                    // Find rectangle for the child
                    context.DisplayRectangle = new Rectangle(nextPoint, itemSize);

                    // Layout the child
                    this[i].Layout(context);

                    // Move to next position across
                    nextPoint.X += itemSize.Width;

                    // Do we need to move to next line?
                    if (((i + 1) % _lineItems) == 0)
                    {
                        nextPoint.X = displayRect.X;
                        nextPoint.Y += itemSize.Height;
                    }
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
		#endregion

        #region Private
        public void SyncChildren()
        {
            _selectedIndex = _itemSelect.SelectedIndex;

            // If we do not have enough already
            if (Count < _imageIndexCount)
            {
                // Create and add the number extra needed
                int create = _imageIndexCount - Count;
                for (int i = 0; i < create; i++)
                    Add(new ViewDrawMenuImageSelectItem(_viewManager, _itemSelect, _triple, this, _needPaint));
            }
            else if (Count > _imageIndexCount)
            {
                // Destroy the extra ones no longer needed
                int remove = Count - _imageIndexCount;
                for (int i = 0; i < remove; i++)
                    RemoveAt(0);
            }

            // Tell each item the image it should be displaying
            for (int i = 0; i < _imageIndexCount; i++)
            {
                int imageIndex = i + _imageIndexStart;
                ViewDrawMenuImageSelectItem item = (ViewDrawMenuImageSelectItem)this[i];
                item.ImageList = _imageList;
                item.ImageIndex = imageIndex;
                item.Checked = (_selectedIndex == imageIndex);
                item.Enabled = _enabled;
            }
        }
        #endregion
    }
}
