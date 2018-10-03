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
	/// View element that applies padding to the drawing of a border and background.
	/// </summary>
	public class ViewDrawSplitCanvas : ViewComposite
	{
		#region Instance Fields
        private IPaletteBack _paletteBack;
        private IPaletteBorder _paletteBorder;
        private IPaletteMetric _paletteMetric;
        private IPaletteBack _paletteBackNormal;
        private IPaletteBorder _paletteBorderNormal;
        private PaletteMetricPadding _metricPadding;
        private PaletteBackInheritForced _paletteBackDraw;
        private PaletteBackLightenColors _paletteBackLight;
        private IDisposable _mementoBack;
        private PaletteBorderInheritForced _borderForced;
		private VisualOrientation _orientation;
        private TabBorderStyle _tabBorderStyle;
		private Region _clipRegion;
        private Rectangle _splitRectangle;
        private Rectangle _nonSplitRectangle;
        private bool _drawTabBorder;
        private bool _drawCanvas;
        private bool _drawOnComposition;
        private bool _splitter;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawSplitCanvas class.
		/// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>		
		/// <param name="paletteBorder">Palette source for the border.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
        public ViewDrawSplitCanvas(IPaletteBack paletteBack,
							       IPaletteBorder paletteBorder,
							       VisualOrientation orientation)
            : this(paletteBack, 
                   paletteBorder, 
                   null, 
                   PaletteMetricPadding.HeaderGroupPaddingPrimary, 
                   orientation)
		{
        }

		/// <summary>
        /// Initialize a new instance of the ViewDrawSplitCanvas class.
		/// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>		
        /// <param name="paletteBorder">Palette source for the border.</param>
		/// <param name="paletteMetric">Palette source for metric values.</param>
		/// <param name="metricPadding">Matric used to get padding values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
        public ViewDrawSplitCanvas(IPaletteBack paletteBack,
							       IPaletteBorder paletteBorder,
							       IPaletteMetric paletteMetric,
							       PaletteMetricPadding metricPadding,
							       VisualOrientation orientation)
		{
			// Cache the starting values
			_paletteBorder = paletteBorder;
			_paletteBack = paletteBack;
            _paletteBackDraw = new PaletteBackInheritForced(_paletteBack);
            _paletteBackDraw.ForceDraw = InheritBool.True;
            _paletteBackLight = new PaletteBackLightenColors(_paletteBack);
            _paletteMetric = paletteMetric;
            _paletteBorderNormal = paletteBorder;
            _paletteBackNormal = paletteBack;
			_metricPadding = metricPadding;
			_orientation = orientation;
            _drawTabBorder = false;
            _drawCanvas = true;
            _splitter = false;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawSplitCanvas:" + Id;
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

                if (_clipRegion != null)
                {
                    _clipRegion.Dispose();
                    _clipRegion = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region PaletteBack
        /// <summary>
        /// Gets access to the currently used background palette.
        /// </summary>
        public IPaletteBack PaletteBack
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _paletteBack; }
        }
        #endregion

        #region PaletteBorder
        /// <summary>
        /// Gets access to the currently used border palette.
        /// </summary>
        public IPaletteBorder PaletteBorder
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _paletteBorder; }
        }
        #endregion

        #region PaletteMetric
        /// <summary>
        /// Gets access to the currently used metric palette.
        /// </summary>
        public IPaletteMetric PaletteMetric
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _paletteMetric; }
        }
        #endregion

        #region SplitRectangle
        /// <summary>
        /// Gets and sets the split area of the canvas.
        /// </summary>
        public Rectangle SplitRectangle
        {
            get { return _splitRectangle; }
            
            set 
            { 
                _splitRectangle = value; 
                
                ButtonController controller = this.FindMouseController() as ButtonController;
                if (controller != null)
                    controller.SplitRectangle = value;
            }
        }
        #endregion

        #region NonSplitRectangle
        /// <summary>
        /// Gets and sets the non split area of the canvas.
        /// </summary>
        public Rectangle NonSplitRectangle
        {
            get { return _nonSplitRectangle; }
            set { _nonSplitRectangle = value; }
        }
        #endregion

        #region Splitter
        /// <summary>
        /// Is the canvas split into a normal and splitter area.
        /// </summary>
        public bool Splitter
        {
            get { return _splitter; }
            set { _splitter = value; }
        }
        #endregion

        #region SetPalettes
        /// <summary>
		/// Update the source palettes for drawing.
		/// </summary>
		/// <param name="paletteBack">Palette source for the background.</param>		
		/// <param name="paletteBorder">Palette source for the border.</param>
        public virtual void SetPalettes(IPaletteBack paletteBack,
                                        IPaletteBorder paletteBorder)
        {
            SetPalettes(paletteBack, paletteBorder, _paletteMetric);
        }

        /// <summary>
		/// Update the source palettes for drawing.
		/// </summary>
		/// <param name="paletteBack">Palette source for the background.</param>		
		/// <param name="paletteBorder">Palette source for the border.</param>
        /// <param name="paletteMetric">Palette source for the metric.</param>
        public virtual void SetPalettes(IPaletteBack paletteBack, 
                                        IPaletteBorder paletteBorder,
                                        IPaletteMetric paletteMetric)
		{
            Debug.Assert(paletteBorder != null);
            Debug.Assert(paletteBack != null);

			// Use newly provided palettes
			_paletteBack = paletteBack;
            _paletteBackDraw.SetInherit(paletteBack);
            _paletteBackLight.Inherit = paletteBack;

            // If not using a forced override decorator, then just store the new border palette
            // otherwise we update the decorator with the palette as the new inheritance to use
            if (_borderForced == null)
                _paletteBorder = paletteBorder;
            else
                _borderForced.SetInherit(paletteBorder);

            _paletteMetric = paletteMetric;
		}
		#endregion

		#region Orientation
		/// <summary>
		/// Gets and sets the visual orientation.
		/// </summary>
		public VisualOrientation Orientation
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _orientation; }
			set { _orientation = value; }
		}
		#endregion

        #region DrawTabBorder
        /// <summary>
        /// Gets and sets if the border should be drawn as a tab border.
        /// </summary>
        public bool DrawTabBorder
        {
            get { return _drawTabBorder; }
            set { _drawTabBorder = value; }
        }
        #endregion

        #region TabBorderStyle
        /// <summary>
        /// Gets and sets the tab border style to use.
        /// </summary>
        public TabBorderStyle TabBorderStyle
        {
            get { return _tabBorderStyle; }
            set { _tabBorderStyle = value; }
        }
        #endregion

        #region MaxBorderEdges
        /// <summary>
        /// Gets and sets the maximum edges allowed.
        /// </summary>
        public PaletteDrawBorders MaxBorderEdges
        {
            get
            {
                if (_borderForced == null)
                    return PaletteDrawBorders.All;
                else
                    return _borderForced.MaxBorderEdges;
            }

            set 
            {
                // If the decorator object used to override the border palette is not created...
                if (_borderForced == null)
                {
                    // Then create it and pass the existing border palette as the inheritence
                    _borderForced = new PaletteBorderInheritForced(_paletteBorder);

                    // Now we want to always use the forced version instead
                    _paletteBorder = _borderForced;
                }
                
                _borderForced.MaxBorderEdges = value; 
            }
        }
        #endregion

        #region ForceGraphicsHint
        /// <summary>
        /// Gets and sets the forced value for the graphics hint.
        /// </summary>
        public PaletteGraphicsHint ForceGraphicsHint
        {
            get
            {
                if (_borderForced == null)
                    return PaletteGraphicsHint.Inherit;
                else
                    return _borderForced.ForceGraphicsHint;
            }

            set 
            {
                // If the decorator object used to override the border palette is not created...
                if (_borderForced == null)
                {
                    // Then create it and pass the existing border palette as the inheritence
                    _borderForced = new PaletteBorderInheritForced(_paletteBorder);

                    // Now we want to always use the forced version instead
                    _paletteBorder = _borderForced;
                }

                _borderForced.ForceGraphicsHint = value; 
            }
        }
        #endregion

        #region DrawBorderAfter
        /// <summary>
		/// Gets the drawing of the border before or after children.
		/// </summary>
		public virtual bool DrawBorderLast
		{
			get
			{
				// By default the border is drawn after the child content
				return true;
			}
		}
		#endregion

        #region DrawCanvas
        /// <summary>
        /// Gets and sets if the canvas should 
        /// </summary>
        public bool DrawCanvas
        {
            get { return _drawCanvas; }
            set { _drawCanvas = value; }
        }
        #endregion

        #region DrawOnComposition
        /// <summary>
        /// Gets and sets a value indicating if the canvas is drawing on composition.
        /// </summary>
        public bool DrawCanvasOnComposition
        {
            get { return _drawOnComposition; }
            set { _drawOnComposition = value; }
        }
        #endregion

        #region GetOuterBorderPath
        /// <summary>
        /// Gets a path that describes the outside of the border.
        /// </summary>
        /// <param name="context">Context used by the renderer.</param>
        /// <returns>Path instance.</returns>
        public GraphicsPath GetOuterBorderPath(RenderContext context)
        {
            if (_paletteBorder != null)
            {
                return context.Renderer.RenderStandardBorder.GetOutsideBorderPath(context, ClientRectangle,
                                                                                  _paletteBorder, Orientation,
                                                                                  State);
            }

            // No palette details to use
            return null;
        }
        #endregion

        #region Eval
        /// <summary>
        /// Evaluate the need for drawing transparent areas.
        /// </summary>
        /// <param name="context">Evaluation context.</param>
        /// <returns>True if transparent areas exist; otherwise false.</returns>
        public override bool EvalTransparentPaint(ViewContext context)
        {
            Debug.Assert(context != null);

            // Ask the renderer to evaluate the given palette
            return context.Renderer.EvalTransparentPaint(_paletteBack, _paletteBorder, State);
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

            // Ensure any content children have correct composition setting
            foreach (ViewBase child in this)
                if (child is ViewDrawContent)
                {
                    ViewDrawContent viewContent = (ViewDrawContent)child;
                    viewContent.DrawContentOnComposition = DrawCanvasOnComposition;
                }
            
            // Let base class find preferred size of the children
			Size preferredSize = base.GetPreferredSize(context);

			// Apply space the border takes up
            if (DrawTabBorder)
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, _paletteBorder, State, Orientation, TabBorderStyle));
            else
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder, State, Orientation));

			// Do we have a metric source for additional padding?
            if ((_paletteMetric != null) && (_metricPadding != PaletteMetricPadding.None))
			{
				// Apply padding needed outside the border of the canvas
                preferredSize = CommonHelper.ApplyPadding(Orientation, preferredSize, _paletteMetric.GetMetricPadding(State, _metricPadding));
			}

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

			// Do we have a metric source for additional padding?
			if ((_paletteMetric != null) && (_metricPadding != PaletteMetricPadding.None))
			{
				// Get the padding to be applied before the canvas drawing
                Padding outerPadding = _paletteMetric.GetMetricPadding(State, _metricPadding);

                // Apply the padding to the client rectangle
                ClientRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, outerPadding);
			}

            Padding padding;

            // Calculate how much space the border takes up
            if (DrawTabBorder)
                padding = context.Renderer.RenderTabBorder.GetTabBorderDisplayPadding(context, _paletteBorder, State, Orientation, TabBorderStyle);
            else
                padding = context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_paletteBorder, State, Orientation);

            // Apply the padding to the client rectangle
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation, ClientRectangle, padding);

            // Ensure any content children have correct composition setting
            foreach (ViewBase child in this)
                if (child is ViewDrawContent)
                {
                    // Do we need to draw the background?
                    bool drawBackground = _drawCanvas && (_paletteBack.GetBackDraw(State) == InheritBool.True);

                    // Update the content accordingly
                    ViewDrawContent viewContent = (ViewDrawContent)child;
                    viewContent.DrawContentOnComposition = DrawCanvasOnComposition && !drawBackground;
                }

			// Let child elements layout
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
			Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            RenderBackground(context, ClientRectangle);

            if (_drawCanvas && (_paletteBorder != null))
            {
                // Do we draw the border before the children?
                if (!DrawBorderLast)
                    RenderBorder(context, ClientRectangle);
                else
                {
                    // Drawing border afterwards, and so clip children to prevent drawing
                    // over the corners if they are rounded.  We only clip children if the 
                    // border is drawn afterwards.

                    // Remember the current clipping region
                    _clipRegion = context.Graphics.Clip.Clone();

                    GraphicsPath borderPath;

                    // Restrict the clipping to the area inside the canvas border
                    if (DrawTabBorder)
                        borderPath = context.Renderer.RenderTabBorder.GetTabBorderPath(context, ClientRectangle, _paletteBorder, Orientation, State, TabBorderStyle);
                    else
                        borderPath = context.Renderer.RenderStandardBorder.GetBorderPath(context, ClientRectangle, _paletteBorder, Orientation, State);

                    // Create a new region the same as the existing clipping region
                    Region combineRegion = new Region(borderPath);

                    // Reduce clipping region down by our border path
                    combineRegion.Intersect(_clipRegion);
                    context.Graphics.Clip = combineRegion;

                    borderPath.Dispose();
                }
            }
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

            if (_drawCanvas && (_paletteBorder != null))
            {
                // Do we draw the border after the children?
                if (DrawBorderLast)
                {
                    // Set the clipping region back to original setting
                    Region oldRegion = context.Graphics.Clip;
                    context.Graphics.Clip = _clipRegion;
                    _clipRegion = null;

                    // Remember to dispose of the temporary region, no longer needed
                    oldRegion.Dispose();

                    RenderBorder(context, ClientRectangle);
                }
            }
		}
		#endregion

        #region Implementation
        private void RenderBackground(RenderContext context, Rectangle rect)
        {
            // Do we need to draw the background?
            if (_drawCanvas && (_paletteBack.GetBackDraw(State) == InheritBool.True))
            {
                if (Splitter)
                {
                    bool mouseInSplit = MouseInSplit;
                    switch (State)
                    {
                        case PaletteState.Tracking:
                            using (Clipping clipToSplitter = new Clipping(context.Graphics, _nonSplitRectangle))
                            {
                                if (SplitWithFading)
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBackLight : _paletteBack), _paletteBorder, PaletteState.Tracking);
                                else
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBackDraw : _paletteBack), _paletteBorder, (mouseInSplit ? PaletteState.Normal : PaletteState.Tracking));
                            }

                            using (Clipping clipToSplitter = new Clipping(context.Graphics, _splitRectangle))
                            {
                                if (SplitWithFading)
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBack : _paletteBackLight), _paletteBorder, PaletteState.Tracking);
                                else
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBack : _paletteBackDraw), _paletteBorder, (mouseInSplit ? PaletteState.Tracking : PaletteState.Normal));
                            }
                            break;
                        case PaletteState.Pressed:
                            using (Clipping clipToSplitter = new Clipping(context.Graphics, _splitRectangle))
                            {
                                if (SplitWithFading)
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBack : _paletteBackLight),
                                                   _paletteBorder, (mouseInSplit ? PaletteState.Pressed : PaletteState.Tracking));
                                else
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBack : _paletteBackDraw),
                                                   _paletteBorder, (mouseInSplit ? PaletteState.Pressed : PaletteState.Normal));
                            }

                            using (Clipping clipToSplitter = new Clipping(context.Graphics, _nonSplitRectangle))
                            {
                                if (SplitWithFading)
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBackLight : _paletteBack),
                                                  _paletteBorder, (mouseInSplit ? PaletteState.Tracking : PaletteState.Pressed));
                                else
                                    DrawBackground(context, rect, (mouseInSplit ? _paletteBackDraw : _paletteBack),
                                                   _paletteBorder, (mouseInSplit ? PaletteState.Normal : PaletteState.Pressed));
                            }
                            break;
                        default:
                            DrawBackground(context, rect, _paletteBack, _paletteBorder, State);
                            break;
                    }
                }
                else
                    DrawBackground(context, rect, _paletteBack, _paletteBorder, State);
            }
        }

        private void RenderBorder(RenderContext context, Rectangle rect)
		{
			Debug.Assert(context != null);

			// Do we need to draw the border?
			if (_paletteBorder.GetBorderDraw(State) == InheritBool.True)
			{
                if (Splitter)
                {
                    bool mouseInSplit = MouseInSplit;
                    switch (State)
                    {
                        case PaletteState.Tracking:
                            DrawBorder(context, rect, _paletteBorder, PaletteState.Tracking);
                            break;
                        case PaletteState.Pressed:
                            DrawBorder(context, rect, _paletteBorder, PaletteState.Tracking);

                            using (Clipping clipToSplitter = new Clipping(context.Graphics, (mouseInSplit ? _splitRectangle : _nonSplitRectangle)))
                                DrawBorder(context, rect, _paletteBorder, PaletteState.Pressed);
                            break;
                        default:
                            DrawBorder(context, rect, _paletteBorder, State);
                            break;
                    }
                }
                else
                    DrawBorder(context, rect, _paletteBorder, State);
            }
		}

        private void DrawBackground(RenderContext context, 
                                    Rectangle rect, 
                                    IPaletteBack paletteBack, 
                                    IPaletteBorder paletteBorder,
                                    PaletteState state)
        {
            GraphicsPath borderPath;
            Padding borderPadding;

            // Ask the border renderer for a path that encloses the border
            if (DrawTabBorder)
            {
                borderPath = context.Renderer.RenderTabBorder.GetTabBackPath(context, rect, paletteBorder, Orientation, state, TabBorderStyle);
                borderPadding = Padding.Empty;
            }
            else
            {
                borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, rect, paletteBorder, Orientation, state);
                borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, state, Orientation);
            }

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(_orientation, rect, borderPadding);

            // Render the background inside the border path
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath, paletteBack, _orientation, state, _mementoBack);

            borderPath.Dispose();
        }

        private void DrawBorder(RenderContext context, 
                                Rectangle rect, 
                                IPaletteBorder paletteBorder, 
                                PaletteState state)
        {
            // Render the border over the background and children
            if (DrawTabBorder)
                context.Renderer.RenderTabBorder.DrawTabBorder(context, rect, _paletteBorder, _orientation, state, TabBorderStyle);
            else
                context.Renderer.RenderStandardBorder.DrawBorder(context, rect, _paletteBorder, _orientation, state);
        }

        private bool MouseInSplit
        {
            get
            {
                ButtonController controller = this.FindMouseController() as ButtonController;
                if (controller != null)
                    if (!controller.MousePoint.Equals(CommonHelper.NullPoint))
                        return _splitRectangle.Contains(controller.MousePoint);

                return false;
            }
        }

        private bool SplitWithFading
        {
            get
            {
                return (_paletteMetric == null) || (_paletteMetric.GetMetricBool(State, PaletteMetricBool.SplitWithFading) == InheritBool.True);
            }
        }
        #endregion
	}
}
