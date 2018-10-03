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
	/// Draw a solid color block inside a context menu color column.
	/// </summary>
    public class ViewDrawMenuColorBlock : ViewLeaf
    {
        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuColorColumns _colorColumns;
        private Color _color;
        private Size _blockSize;
        private bool _first;
        private bool _last;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuColorBlock class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="colorColumns">Reference to owning color columns entry.</param>
        /// <param name="color">Drawing color for the block.</param>
        /// <param name="first">Is this element first in column.</param>
        /// <param name="last">Is this element last in column.</param>
        /// <param name="enabled">Is this column enabled</param>
        public ViewDrawMenuColorBlock(IContextMenuProvider provider,
                                      KryptonContextMenuColorColumns colorColumns,
                                      Color color, 
                                      bool first, 
                                      bool last, 
                                      bool enabled)
        {
            _provider = provider;
            _colorColumns = colorColumns;
            _color = color;
            _first = first;
            _last = last;
            _enabled = enabled;
            _blockSize = colorColumns.BlockSize;

            // Use context menu specific version of the radio button controller
            MenuColorBlockController mcbc = new MenuColorBlockController(provider.ProviderViewManager, this, this, provider.ProviderNeedPaintDelegate);
            mcbc.Click += new EventHandler(OnClick);
            MouseController = mcbc;
            KeyController = mcbc;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuColorBlock:" + Id;
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

        #region KryptonContextMenuColorColumns
        /// <summary>
        /// Gets access to the actual color columns definiton.
        /// </summary>
        public KryptonContextMenuColorColumns KryptonContextMenuColorColumns
        {
            get { return _colorColumns; }
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

        #region Color
        /// <summary>
        /// Gets the color associated with the block.
        /// </summary>
        public Color Color
        {
            get { return _color; }
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

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            return _blockSize;
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
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // Start with the full client rectangle
            Rectangle drawRect = ClientRectangle;

            // Do not draw over the left and right lines
            drawRect.X += 1;
            drawRect.Width -= 2;

            // Do we need to prevent drawing over top line?
            if (_first)
            {
                drawRect.Y += 1;
                drawRect.Height -= 1;
            }

            // Do we need to prevent drawing over last line?
            if (_last)
                drawRect.Height -= 1;

            // Draw ourself in the designated color
            using(SolidBrush brush = new SolidBrush(_color))
                context.Graphics.FillRectangle(brush, drawRect);
        }

        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderAfter(RenderContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // If not in normal state, then need to adorn display
            Color outside = Color.Empty;
            Color inside = Color.Empty;

            // Is this element selected?
            bool selected = (_colorColumns.SelectedColor != null) && (_colorColumns.SelectedColor.Equals(_color));

            switch (ElementState)
            {
                case PaletteState.Tracking:
                    if (_enabled)
                    {
                        outside = _provider.ProviderStateChecked.ItemImage.Border.GetBorderColor1(PaletteState.CheckedNormal);
                        inside = _provider.ProviderStateChecked.ItemImage.Back.GetBackColor1(PaletteState.CheckedNormal);
                    }
                    else
                    {
                        outside = _provider.ProviderStateHighlight.ItemHighlight.Border.GetBorderColor1(PaletteState.Disabled);
                        inside = _provider.ProviderStateHighlight.ItemHighlight.Back.GetBackColor1(PaletteState.Disabled);
                    }
                    break;
                case PaletteState.Pressed:
                case PaletteState.Normal:
                    if (selected || (ElementState == PaletteState.Pressed))
                    {
                        outside = _provider.ProviderStateChecked.ItemImage.Border.GetBorderColor1(PaletteState.CheckedNormal);
                        inside = _provider.ProviderStateChecked.ItemImage.Back.GetBackColor1(PaletteState.CheckedNormal);
                    }
                    break;
            }

            if (!outside.IsEmpty && !inside.IsEmpty)
            {
                // Draw the outside and inside areas of the block
                using (Pen outsidePen = new Pen(outside),
                           insidePen = new Pen(inside))
                {
                    context.Graphics.DrawRectangle(outsidePen, ClientLocation.X, ClientLocation.Y, ClientWidth - 1, ClientHeight - 1);
                    context.Graphics.DrawRectangle(insidePen, ClientLocation.X + 1, ClientLocation.Y + 1, ClientWidth - 3, ClientHeight - 3);
                }
            }
        }
        #endregion

        #region Private
        private void OnClick(object sender, EventArgs e)
        {
            _colorColumns.SelectedColor = _color;
        }
        #endregion
    }
}
