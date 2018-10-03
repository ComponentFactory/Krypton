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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Draws a window containing rounded docking indicators.
	/// </summary>
    public class DropDockingIndicatorsRounded : NativeWindow, 
                                                IDisposable,
                                                IDropDockingIndicator
    {
        #region Instance Fields
        private IRenderer _renderer;
        private IPaletteDragDrop _paletteDragDrop;
        private RenderDragDockingData _dragData;
        private Rectangle _showRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DropDockingIndicatorsRounded class.
		/// </summary>
        /// <param name="paletteDragDrop">Drawing palette.</param>
        /// <param name="renderer">Drawing renderer.</param>
		/// <param name="showLeft">Show left hot area.</param>
		/// <param name="showRight">Show right hot area.</param>
		/// <param name="showTop">Show top hot area.</param>
		/// <param name="showBottom">Show bottom hot area.</param>
        /// <param name="showMiddle">Show middle hot area.</param>
        public DropDockingIndicatorsRounded(IPaletteDragDrop paletteDragDrop, 
                                            IRenderer renderer,
							                bool showLeft, bool showRight,
							                bool showTop, bool showBottom,
                                            bool showMiddle)
		{
            _paletteDragDrop = paletteDragDrop;
            _renderer = renderer;

            // Initialize the drag data that indicators which docking indicators are needed
            _dragData = new RenderDragDockingData(showLeft, showRight, showTop, showBottom, showMiddle);

            // Ask the renderer to measure the sizing of the indicators that are displayed
            _renderer.RenderGlyph.MeasureDragDropDockingGlyph(_dragData, _paletteDragDrop, PaletteDragFeedback.Rounded);
            _showRect = new Rectangle(Point.Empty, _dragData.DockWindowSize);

            // Any old title will do as it will not be shown
            CreateParams cp = new CreateParams();
            cp.Caption = "DropDockingIndicatorsRounded";

            // Define the screen position/size
            cp.X = _showRect.X;
            cp.Y = _showRect.Y;
            cp.Height = _showRect.Width;
            cp.Width = _showRect.Height;

            // As a top-level window it has no parent
            cp.Parent = IntPtr.Zero;

            // Appear as a top-level window
            cp.Style = unchecked((int)(uint)PI.WS_POPUP);

            // Set styles so that it does not have a caption bar and is above all other 
            // windows in the ZOrder, i.e. TOPMOST
            cp.ExStyle = (int)PI.WS_EX_TOPMOST +
                         (int)PI.WS_EX_TOOLWINDOW;

            // We are going to use per-pixrl alpha blending and so need a layered window
            cp.ExStyle += (int)PI.WS_EX_LAYERED;

            // Create the actual window
            this.CreateHandle(cp);
        }

		/// <summary>
		/// Make sure the resources are disposed of gracefully.
		/// </summary>
        public void Dispose()
        {
            DestroyHandle();
        }
        #endregion

        #region Public
        /// <summary>
		/// Show the window relative to provided screen rectangle.
		/// </summary>
		/// <param name="screenRect">Screen rectangle.</param>
		public void ShowRelative(Rectangle screenRect)
		{
			// Find screen middle points
			int yMid = screenRect.Y + (screenRect.Height / 2);
			int xMid = screenRect.X + (screenRect.Width / 2);

            // Find docking size middle points
            int yHalf = _dragData.DockWindowSize.Height / 2;
            int xHalf = _dragData.DockWindowSize.Width / 2;

            Point location;
            if (_dragData.ShowLeft && !_dragData.ShowRight && !_dragData.ShowMiddle && !_dragData.ShowTop && !_dragData.ShowBottom)
                location = new Point(screenRect.Left + 10, yMid - yHalf);
            else if (!_dragData.ShowLeft && _dragData.ShowRight && !_dragData.ShowMiddle && !_dragData.ShowTop && !_dragData.ShowBottom)
                location = new Point(screenRect.Right - _dragData.DockWindowSize.Width - 10, yMid - yHalf);
            else if (!_dragData.ShowLeft && !_dragData.ShowRight && !_dragData.ShowMiddle && _dragData.ShowTop && !_dragData.ShowBottom)
                location = new Point(xMid - xHalf, screenRect.Top + 10);
            else if (!_dragData.ShowLeft && !_dragData.ShowRight && !_dragData.ShowMiddle && !_dragData.ShowTop && _dragData.ShowBottom)
                location = new Point(xMid - xHalf, screenRect.Bottom - _dragData.DockWindowSize.Height - 10);
            else
                location = new Point(xMid - xHalf, yMid - yHalf);

            // Update the image for display
            UpdateLayeredWindow(new Rectangle(location, _showRect.Size));
            
			// Show the window without activating it (i.e. do not take focus)
			PI.ShowWindow(this.Handle, (short)PI.SW_SHOWNOACTIVATE);
		}

        /// <summary>
        /// Hide the window from display.
        /// </summary>
        public void Hide()
        {
            PI.ShowWindow(this.Handle, (short)PI.SW_HIDE);
        }

		/// <summary>
		/// Perform mouse hit testing against a screen point.
		/// </summary>
		/// <param name="screenPoint">Screen point.</param>
		/// <returns>Area that is active.</returns>
		public int ScreenMouseMove(Point screenPoint)
		{
			// Convert from screen to client coordinates
            Point pt = new Point(screenPoint.X - _showRect.X, screenPoint.Y - _showRect.Y);

			// Remember the current active value
            int activeBefore = _dragData.ActiveFlags;

			// Reset active back to nothing
            _dragData.ClearActive();

			// Find new active area
            if (_dragData.ShowLeft && _dragData.RectLeft.Contains(pt))      _dragData.ActiveLeft = true;
            if (_dragData.ShowRight && _dragData.RectRight.Contains(pt))    _dragData.ActiveRight = true;
            if (_dragData.ShowTop && _dragData.RectTop.Contains(pt))        _dragData.ActiveTop = true;
            if (_dragData.ShowBottom && _dragData.RectBottom.Contains(pt))  _dragData.ActiveBottom = true;
			
			// Only consider the middle if the others do not match
            if ((_dragData.ActiveFlags == 0) && _dragData.ShowMiddle && _dragData.RectMiddle.Contains(pt))
                _dragData.ActiveMiddle = true;

			// Do we need to update the display?
            if (_dragData.ActiveFlags != activeBefore)
                UpdateLayeredWindow(_showRect);

            return _dragData.ActiveFlags;
		}

		/// <summary>
		/// Ensure the state is updated to reflect the mouse not being over the control.
		/// </summary>
		public void MouseReset()
		{
			// Do we need to update display?
			if (_dragData.AnyActive)
			{
                _dragData.ClearActive();
                UpdateLayeredWindow(_showRect);
			}
        }
        #endregion

        #region Implementation
        private void UpdateLayeredWindow(Rectangle rect)
        {
            // Cache the latest size and location
            _showRect = rect;

            // Must have a visible rectangle to render
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Draw onto a bitmap that is then used as the window display
                Bitmap memoryBitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(memoryBitmap))
                {
                    // Perform actual painting onto the bitmap
                    Rectangle area = new Rectangle(0, 0, rect.Width, rect.Height);
                    using (RenderContext context = new RenderContext(null, g, area, _renderer))
                        _renderer.RenderGlyph.DrawDragDropDockingGlyph(context, _dragData, _paletteDragDrop, PaletteDragFeedback.Rounded);

                    // Get hold of the screen DC
                    IntPtr hDC = PI.GetDC(IntPtr.Zero);

                    // Create a memory based DC compatible with the screen DC
                    IntPtr memoryDC = PI.CreateCompatibleDC(hDC);

                    // Get access to the bitmap handle contained in the Bitmap object
                    IntPtr hBitmap = memoryBitmap.GetHbitmap(Color.FromArgb(0));

                    // Select this bitmap for updating the window presentation
                    IntPtr oldBitmap = PI.SelectObject(memoryDC, hBitmap);

                    // New window size
                    PI.SIZE ulwsize;
                    ulwsize.cx = rect.Width;
                    ulwsize.cy = rect.Height;

                    // New window position
                    PI.POINT topPos;
                    topPos.x = rect.Left;
                    topPos.y = rect.Top;

                    // Offset into memory bitmap is always zero
                    PI.POINT pointSource;
                    pointSource.x = 0;
                    pointSource.y = 0;

                    // We want to make the entire bitmap opaque 
                    PI.BLENDFUNCTION blend = new PI.BLENDFUNCTION();
                    blend.BlendOp = (byte)PI.AC_SRC_OVER;
                    blend.BlendFlags = 0;
                    blend.SourceConstantAlpha = 255;
                    blend.AlphaFormat = (byte)PI.AC_SRC_ALPHA;

                    // Tell operating system to use our bitmap for painting
                    PI.UpdateLayeredWindow(Handle, hDC, ref topPos, ref ulwsize,
                                           memoryDC, ref pointSource, 0, ref blend,
                                           (int)PI.ULW_ALPHA);

                    // Put back the old bitmap handle
                    PI.SelectObject(memoryDC, oldBitmap);

                    // Cleanup resources
                    PI.ReleaseDC(IntPtr.Zero, hDC);
                    PI.DeleteObject(hBitmap);
                    PI.DeleteDC(memoryDC);
                }
            }
        }
        #endregion
    }
}
