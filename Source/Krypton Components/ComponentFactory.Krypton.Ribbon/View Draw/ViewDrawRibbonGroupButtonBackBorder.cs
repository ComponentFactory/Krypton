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
	/// Draws the background and border for a group button.
	/// </summary>
    internal class ViewDrawRibbonGroupButtonBackBorder : ViewComposite
    {
        #region Static Fields
        private static readonly Size _viewSize = new Size(22, 22);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupItem _groupItem;
        private GroupButtonController _controller;
        private EventHandler _finishDelegate;
        private IDisposable _mementoBack;
        private IPaletteBack _paletteBack;
        private PaletteBackInheritForced _paletteBackDraw;
        private PaletteBackLightenColors _paletteBackLight;
        private IPaletteBorder _paletteBorder;
        private PaletteBorderInheritForced _paletteBorderAll;
        private bool _splitVertical;
        private bool _constantBorder;
        private bool _drawNonTrackingAreas;
        private bool _checked;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the button is left clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the button is right clicked.
        /// </summary>
        public event MouseEventHandler ContextClick;

        /// <summary>
        /// Occurs when the drop down button is clicked.
        /// </summary>
        public event EventHandler DropDown;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupButtonBackBorder class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="groupItem">Reference to owning group item.</param>
        /// <param name="paletteBack">Palette to use for the background.</param>
        /// <param name="paletteBorder">Palette to use for the border.</param>
        /// <param name="constantBorder">Should the border be a constant normal state.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonGroupButtonBackBorder(KryptonRibbon ribbon,
                                                   KryptonRibbonGroupItem groupItem,
                                                   IPaletteBack paletteBack,
                                                   IPaletteBorder paletteBorder,
                                                   bool constantBorder,
                                                   NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(groupItem != null);
            Debug.Assert(paletteBack != null);
            Debug.Assert(paletteBorder != null);

            // Remember incoming references
            _ribbon = ribbon;
            _groupItem = groupItem;
            _paletteBack = paletteBack;
            _paletteBackDraw = new PaletteBackInheritForced(paletteBack);
            _paletteBackDraw.ForceDraw = InheritBool.True;
            _paletteBackLight = new PaletteBackLightenColors(paletteBack);
            _paletteBorderAll = new PaletteBorderInheritForced(paletteBorder);
            _paletteBorderAll.ForceBorderEdges(PaletteDrawBorders.All);
            _paletteBorder = paletteBorder;
            _constantBorder = constantBorder;

            // Default other fields
            _checked = false;
            _drawNonTrackingAreas = true;

            // Create delegate used to process end of click action
            _finishDelegate = new EventHandler(ActionFinished);

            // Attach a controller to this element for the pressing of the button
            _controller = new GroupButtonController(_ribbon, this, needPaint);
            _controller.Click += new EventHandler(OnClick);
            _controller.ContextClick += new MouseEventHandler(OnContextClick);
            _controller.DropDown += new EventHandler(OnDropDown);
            MouseController = _controller;
            SourceController = _controller;
            KeyController = _controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupButtonBackBorder:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoBack != null)
                {
                    _mementoBack.Dispose();
                    _mementoBack = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region GroupItem
        /// <summary>
        /// Gets access to the associated ribbon group item.
        /// </summary>
        public KryptonRibbonGroupItem GroupItem
        {
            get { return _groupItem; }
        }
        #endregion

        #region Controller
        /// <summary>
        /// Gets access to the associated controller.
        /// </summary>
        public GroupButtonController Controller
        {
            get { return _controller; }
        }
        #endregion

        #region SplitVertical
        /// <summary>
        /// Gets and sets if the split button is vertical or horizontal.
        /// </summary>
        public bool SplitVertical
        {
            get { return _splitVertical; }
            set { _splitVertical = value; }
        }
        #endregion

        #region SplitRectangle
        /// <summary>
        /// Gets and sets the rectangle for the split area.
        /// </summary>
        public Rectangle SplitRectangle
        {
            get { return _controller.SplitRectangle; }
            set { _controller.SplitRectangle = value; }
        }
        #endregion

        #region ButtonType
        /// <summary>
        /// Gets and sets the type of button the view represents.
        /// </summary>
        public GroupButtonType ButtonType
        {
            get { return _controller.ButtonType; }
            set { _controller.ButtonType = value; }
        }
        #endregion

        #region Checked
        /// <summary>
        /// Gets and sets the checked state of the button background/border.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        #endregion

        #region ConstantBorder
        /// <summary>
        /// Gets and sets the drawing of a constant border.
        /// </summary>
        public bool ConstantBorder
        {
            get { return _constantBorder; }
            set { _constantBorder = value; }
        }
        #endregion

        #region DrawNonTrackingAreas
        /// <summary>
        /// Gets and sets if the non tracking areas are drawn.
        /// </summary>
        public bool DrawNonTrackingAreas
        {
            get { return _drawNonTrackingAreas; }
            set { _drawNonTrackingAreas = value; }
        }
        #endregion

        #region FinishDelegate
        /// <summary>
        /// Gets access to the associated finish delegate.
        /// </summary>
        public EventHandler FinishDelegate
        {
            get { return _finishDelegate; }
        }
        #endregion

        #region Layout
        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let child elements layout in given space
            base.Layout(context);
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            // Get that basic drawing state that does not reflect checked state
            PaletteState drawState = State;

            // We never draw background/border as disabled, let content show disabled state
            if (drawState == PaletteState.Disabled)
                drawState = PaletteState.Normal;
            else
            {
                // Do we need to modify this for the checked state?
                if (Checked && (ButtonType == GroupButtonType.Check))
                {
                    switch (drawState)
                    {
                        case PaletteState.Normal:
                            drawState = PaletteState.CheckedNormal;
                            break;
                        case PaletteState.Tracking:
                            drawState = PaletteState.CheckedTracking;
                            break;
                        case PaletteState.Pressed:
                            drawState = PaletteState.CheckedPressed;
                            break;
                    }
                }
            }

            switch (ButtonType)
            {
                case GroupButtonType.Push:
                case GroupButtonType.Check:
                case GroupButtonType.DropDown:
                    // Entire area is drawn using draw state
                    DrawBackground(_paletteBack, context, ClientRectangle, drawState);

                    if (_constantBorder)
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                    else
                        DrawBorder(_paletteBorder, context, ClientRectangle, drawState);
                    break;
                case GroupButtonType.Split:
                    if (_splitVertical)
                        DrawVerticalSplit(context, drawState);
                    else
                        DrawHorizontalSplit(context, drawState);
                    break;
            }

            base.RenderBefore(context);
        }
        #endregion

        #region Implementation
        private void DrawVerticalSplit(RenderContext context, PaletteState drawState)
        {
            // We need the rectangle that represents just the split area
            int partialHeight = (ClientHeight / 3 * 2);
            Rectangle partialRect = new Rectangle(ClientLocation, new Size(ClientWidth, partialHeight));
            Rectangle splitRectangle = _controller.SplitRectangle;
            Rectangle aboveSplitRect = new Rectangle(ClientLocation, new Size(ClientWidth, splitRectangle.Y - ClientLocation.Y));
            Rectangle splitterRect = new Rectangle(splitRectangle.Location, new Size(ClientWidth, 1));
            Rectangle belowSplitRect = new Rectangle(ClientLocation.X, splitRectangle.Y, ClientWidth, splitRectangle.Height);

            bool splitWithFading = SplitWithFading(drawState);
            switch (drawState)
            {
                case PaletteState.Normal:
                    // Draw the entire border around the button
                    if (_constantBorder)
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Normal);
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                    }
                    break;
                case PaletteState.Tracking:
                    // Draw the background for the click and split areas
                    if (_controller.MouseInSplit)
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);

                        Rectangle belowSplitRect1 = new Rectangle(belowSplitRect.X, belowSplitRect.Y + 1, belowSplitRect.Width, belowSplitRect.Height - 1);
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect1))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, partialRect, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, partialRect, PaletteState.Normal);
                    }
                    else
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                            DrawBackground(_paletteBack, context, partialRect, PaletteState.Tracking);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }

                    // Draw the single pixel splitter line
                    using (Clipping clipToSplitter = new Clipping(context.Graphics, splitterRect))
                        DrawBorder(_paletteBorderAll, context, new Rectangle(splitRectangle.X, splitRectangle.Y, splitRectangle.Width, 2), PaletteState.Tracking);

                    // Draw the entire border around the button
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);
                    break;
                case PaletteState.Pressed:
                    // Draw the background for the click and split areas
                    if (_controller.MouseInSplit)
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Pressed);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);

                        Rectangle belowSplitRect1 = new Rectangle(belowSplitRect.X, belowSplitRect.Y + 1, belowSplitRect.Width, belowSplitRect.Height - 1);
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect1))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, partialRect, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, partialRect, PaletteState.Normal);
                    }
                    else
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                            DrawBackground(_paletteBack, context, partialRect, PaletteState.Pressed);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }

                    // Draw the entire border around the button
                    DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);

                    // Draw the single pixel splitter line
                    using (Clipping clipToSplitter = new Clipping(context.Graphics, splitterRect))
                        DrawBorder(_paletteBorderAll, context, new Rectangle(splitRectangle.X, splitRectangle.Y, splitRectangle.Width, 2), PaletteState.Pressed);

                    // Draw the border for the click and split areas
                    if (_controller.MouseInSplit)
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, belowSplitRect))
                            DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                    else
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, aboveSplitRect))
                            DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                    break;
                default:
                    // Should never happen
                    Debug.Assert(false);
                    break;
            }
        }

        private void DrawHorizontalSplit(RenderContext context, PaletteState drawState)
        {
            // We need the rectangle that represents just the split area
            int partialWidth = (ClientWidth / 3 * 2);
            Rectangle splitRectangle = _controller.SplitRectangle;
            Rectangle beforeSplitRect = new Rectangle(ClientLocation, new Size(splitRectangle.X - ClientLocation.X, ClientHeight));
            Rectangle splitterRect = new Rectangle(splitRectangle.Location, new Size(1, ClientHeight));
            Rectangle afterSplitRect = new Rectangle(splitRectangle.X, ClientLocation.Y , splitRectangle.Width, ClientHeight);

            bool splitWithFading = SplitWithFading(drawState);
            switch (drawState)
            {
                case PaletteState.Normal:
                    // Draw the entire border around the button
                    if (_constantBorder)
                    {
                        DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Normal);
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);
                    }
                    break;
                case PaletteState.Tracking:
                    // Draw the background for the click and split areas
                    if (_controller.MouseInSplit)
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);

                        Rectangle afterSplitRect1 = new Rectangle(afterSplitRect.X + 1, afterSplitRect.Y, afterSplitRect.Width - 1, afterSplitRect.Height); 
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect1))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }
                    else
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Tracking);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }

                    // Draw the single pixel splitter line
                    using (Clipping clipToSplitter = new Clipping(context.Graphics, splitterRect))
                        DrawBorder(_paletteBorderAll, context, new Rectangle(splitRectangle.X, splitRectangle.Y, 2, splitRectangle.Height), PaletteState.Tracking);

                    // Draw the entire border around the button
                    if (_constantBorder)
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);

                    // If the border is not constant, drawn it now
                    if (!_constantBorder)
                    {
                        // Draw the entire border around the button
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);
                    }
                    break;
                case PaletteState.Pressed:
                    // Draw the background for the click and split areas
                    if (_controller.MouseInSplit)
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Pressed);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);

                        Rectangle afterSplitRect1 = new Rectangle(afterSplitRect.X + 1, afterSplitRect.Y, afterSplitRect.Width - 1, afterSplitRect.Height);
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect1))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }
                    else
                    {
                        using (Clipping clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                            DrawBackground(_paletteBack, context, ClientRectangle, PaletteState.Pressed);

                        using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                            if (splitWithFading)
                            {
                                if (_drawNonTrackingAreas)
                                    DrawBackground(_paletteBackLight, context, ClientRectangle, PaletteState.Tracking);
                            }
                            else
                                DrawBackground(_paletteBackDraw, context, ClientRectangle, PaletteState.Normal);
                    }

                    // Draw the single pixel splitter line
                    using (Clipping clipToSplitter = new Clipping(context.Graphics, splitterRect))
                        DrawBorder(_paletteBorderAll, context, new Rectangle(splitRectangle.X, splitRectangle.Y, 2, splitRectangle.Height), PaletteState.Pressed);

                    // Draw the entire border around the button
                    if (_constantBorder)
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Normal);

                    // If the border is not constant, drawn it now
                    if (!_constantBorder)
                    {
                        // Draw the entire border around the button
                        DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Tracking);

                        // Draw the border for the click and split areas
                        if (_controller.MouseInSplit)
                            using (Clipping clipToSplitter = new Clipping(context.Graphics, afterSplitRect))
                                DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                        else
                            using (Clipping clipToSplitter = new Clipping(context.Graphics, beforeSplitRect))
                                DrawBorder(_paletteBorder, context, ClientRectangle, PaletteState.Pressed);
                    }
                    break;
                default:
                    // Should never happen
                    Debug.Assert(false);
                    break;
            }
        }

        private void DrawBackground(IPaletteBack paletteBack,
                                    RenderContext context,
                                    Rectangle rect, 
                                    PaletteState state)
        {
            // Do we need to draw the background?
            if (paletteBack.GetBackDraw(state) == InheritBool.True)
            {
                // Get the border path which the background is clipped to drawing within
                using (GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, rect, _paletteBorder, VisualOrientation.Top, state))
                {
                    Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(_paletteBorder, state, VisualOrientation.Top);

                    // Apply the padding depending on the orientation
                    Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, rect, borderPadding);

                    // Render the background inside the border path
                    _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                                                                                paletteBack, VisualOrientation.Top,
                                                                                state, _mementoBack);
                }
            }
        }

        private void DrawBorder(IPaletteBorder paletteBorder,
                                RenderContext context,
                                Rectangle rect,
                                PaletteState state)
        {
            // Do we need to draw the border?
            if (paletteBorder.GetBorderDraw(state) == InheritBool.True)
                context.Renderer.RenderStandardBorder.DrawBorder(context, rect, paletteBorder,
                                                                 VisualOrientation.Top, state);

        }

        private bool SplitWithFading(PaletteState drawState)
        {
            IPalette palette = _ribbon.GetRedirector();
            return palette.GetMetricBool(drawState, PaletteMetricBool.SplitWithFading) == InheritBool.True;
        }

        private void ActionFinished(object sender, EventArgs e)
        {
            bool fireAction = true;

            if (e is ToolStripDropDownClosedEventArgs)
            {
                ToolStripDropDownClosedEventArgs closedArgs = (ToolStripDropDownClosedEventArgs)e;
                if (closedArgs.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
                    fireAction = false;
            }

            // Remove any popups that result from an action occuring
            if ((_ribbon != null) && fireAction)
                _ribbon.ActionOccured();

            // Remove the fixed pressed appearance
            _controller.RemoveFixed();
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        private void OnContextClick(object sender, MouseEventArgs e)
        {
            if (ContextClick != null)
                ContextClick(this, e);
        }

        private void OnDropDown(object sender, EventArgs e)
        {
            if (DropDown != null)
                DropDown(this, e);
        }
        #endregion
    }
}
