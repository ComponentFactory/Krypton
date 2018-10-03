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
	/// View element that can draw a button.
	/// </summary>
	public class ViewDrawButton : ViewComposite
	{
		#region Instance Fields
        private IPaletteTriple _paletteCurrent;
        private IPaletteTriple _paletteDisabled;
        private IPaletteTriple _paletteNormal;
        private IPaletteTriple _paletteTracking;
        private IPaletteTriple _palettePressed;
        private IPaletteTriple _paletteCheckedNormal;
        private IPaletteTriple _paletteCheckedTracking;
        private IPaletteTriple _paletteCheckedPressed;
        private PaletteBorderEdgeRedirect _edgeRedirect;
        private ViewDrawSplitCanvas _drawCanvas;
        private ViewDrawContent _drawContent;
        private ViewDrawBorderEdge _drawSplitBorder;
        private ViewLayoutCenter _drawDropDown;
        private ViewDrawDropDownButton _drawDropDownButton;
        private ViewLayoutDocker _layoutDocker;
        private VisualOrientation _dropDownPosition;
        private ViewLayoutSeparator _drawOuterSeparator;
        private Rectangle _splitRectangle;
        private Rectangle _nonSplitRectangle;
        private bool _dropDown;
        private bool _splitter;
        private bool _checked;
        private bool _allowUncheck;
        private bool _forcePaletteUpdate;
		#endregion

		#region Identity
        /// <summary>
		/// Initialize a new instance of the ViewDrawButton class.
		/// </summary>
		/// <param name="paletteDisabled">Palette source for the disabled state.</param>
		/// <param name="paletteNormal">Palette source for the normal state.</param>
		/// <param name="paletteTracking">Palette source for the tracking state.</param>
		/// <param name="palettePressed">Palette source for the pressed state.</param>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="buttonValues">Source for content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="useMnemonic">Use mnemonics.</param>
        public ViewDrawButton(IPaletteTriple paletteDisabled,
                              IPaletteTriple paletteNormal,
                              IPaletteTriple paletteTracking,
                              IPaletteTriple palettePressed,
                              IPaletteMetric paletteMetric,
                              IContentValues buttonValues,
                              VisualOrientation orientation,
                              bool useMnemonic)
            : this(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
                   paletteNormal, paletteTracking, palettePressed, paletteMetric,
                   buttonValues, orientation, useMnemonic)
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewDrawButton class.
		/// </summary>
		/// <param name="paletteDisabled">Palette source for the disabled state.</param>
		/// <param name="paletteNormal">Palette source for the normal state.</param>
		/// <param name="paletteTracking">Palette source for the tracking state.</param>
		/// <param name="palettePressed">Palette source for the pressed state.</param>
        /// <param name="paletteCheckedNormal">Palette source for the normal checked state.</param>
        /// <param name="paletteCheckedTracking">Palette source for the tracking checked state.</param>
        /// <param name="paletteCheckedPressed">Palette source for the pressed checked state.</param>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="buttonValues">Source for content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="useMnemonic">Use mnemonics.</param>
		public ViewDrawButton(IPaletteTriple paletteDisabled,
							  IPaletteTriple paletteNormal,
							  IPaletteTriple paletteTracking,
							  IPaletteTriple palettePressed,
                              IPaletteTriple paletteCheckedNormal,
                              IPaletteTriple paletteCheckedTracking,
                              IPaletteTriple paletteCheckedPressed,
                              IPaletteMetric paletteMetric,
                              IContentValues buttonValues,
							  VisualOrientation orientation,
							  bool useMnemonic)
		{
			// Remember the source information
			_paletteDisabled = paletteDisabled;
			_paletteNormal = paletteNormal;
			_paletteTracking = paletteTracking;
			_palettePressed = palettePressed;
			_paletteCheckedNormal = paletteCheckedNormal;
			_paletteCheckedTracking = paletteCheckedTracking;
			_paletteCheckedPressed = paletteCheckedPressed;
            _paletteCurrent = _paletteNormal;

            // Default to not being checked
            _checked = false;
            _allowUncheck = true;
            _dropDown = false;
            _splitter = false;
            _dropDownPosition = VisualOrientation.Right;

            // Create the drop down view
            _drawDropDown = new ViewLayoutCenter(1);
            _drawDropDownButton = new ViewDrawDropDownButton();
            _drawDropDown.Add(_drawDropDownButton);
            _drawOuterSeparator = new ViewLayoutSeparator(1);

            // Create the view used to draw the split edge
            _edgeRedirect = new PaletteBorderEdgeRedirect(_paletteNormal.PaletteBorder, null);
            _drawSplitBorder = new ViewDrawBorderEdge(new PaletteBorderEdge(_edgeRedirect, null), CommonHelper.VisualToOrientation(orientation));

			// Our view contains background and border with content inside
            _drawContent = new ViewDrawContent(_paletteNormal.PaletteContent, buttonValues, orientation);
            _drawCanvas = new ViewDrawSplitCanvas(_paletteNormal.PaletteBack, _paletteNormal.PaletteBorder, paletteMetric, PaletteMetricPadding.None, orientation);

            // Use a docker layout to organize the contents of the canvas
            _layoutDocker = new ViewLayoutDocker();
            _layoutDocker.Add(_drawContent, ViewDockStyle.Fill);
            _layoutDocker.Add(_drawSplitBorder, ViewDockStyle.Right);
            _layoutDocker.Add(_drawDropDown, ViewDockStyle.Right);
            _layoutDocker.Add(_drawOuterSeparator, ViewDockStyle.Right);
            _layoutDocker.Tag = this;

			// Pass the mnemonic default to the content view
			_drawContent.UseMnemonic = useMnemonic;

			// Place the content inside the canvas
            _drawCanvas.Add(_layoutDocker);

            // Set initial view element visible states
            UpdateDropDown();

			// Place the canvas inside ourself
			Add(_drawCanvas);
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
			return "ViewDrawButton:" + Id;
		}
		#endregion

        #region LayoutDocker
        /// <summary>
        /// Gets access to the contained layout docker.
        /// </summary>
        public ViewLayoutDocker LayoutDocker
        {
            get { return _layoutDocker; }
        }
        #endregion

        #region CurrentPalette
        /// <summary>
        /// Gets access to the currently selected palette.
        /// </summary>
        public IPaletteTriple CurrentPalette
        {
            get { return _paletteCurrent; }
        }
        #endregion

        #region DropDown
        /// <summary>
        /// Gets and sets the drop down capability of the button.
        /// </summary>
        public bool DropDown
        {
            get { return _dropDown; }

            set
            {
                if (_dropDown != value)
                {
                    _dropDown = value;
                    UpdateDropDown();
                }
            }
        }
        #endregion

        #region DropDownPosition
        /// <summary>
        /// Gets and sets the drop down position.
        /// </summary>
        public VisualOrientation DropDownPosition
        {
            get { return _dropDownPosition; }

            set
            {
                if (_dropDownPosition != value)
                {
                    _dropDownPosition = value;
                    UpdateDropDown();
                }
            }
        }
        #endregion

        #region DropDownOrientation
        /// <summary>
        /// Gets and sets the drop down orientation.
        /// </summary>
        public VisualOrientation DropDownOrientation
        {
            get { return _drawDropDownButton.Orientation; }

            set
            {
                if (_drawDropDownButton.Orientation != value)
                {
                    _drawDropDownButton.Orientation = value;
                    UpdateDropDown();
                }
            }
        }
        #endregion

        #region DropDownPalette
        /// <summary>
        /// Gets and sets the drop down capability of the button.
        /// </summary>
        public IPalette DropDownPalette
        {
            get { return _drawDropDownButton.Palette; }
            set { _drawDropDownButton.Palette = value; }
        }
        #endregion

        #region Splitter
        /// <summary>
        /// Gets and sets if the drop down button needs a splitter.
        /// </summary>
        public bool Splitter
        {
            get { return _splitter; }

            set
            {
                if (_splitter != value)
                {
                    _splitter = value;
                    UpdateDropDown();
                }
            }
        }
        #endregion

        #region SplitRectangle
        /// <summary>
        /// Gets the split rectangle area.
        /// </summary>
        public Rectangle SplitRectangle
        {
            get { return _splitRectangle; }
        }
        #endregion

        #region NonSplitRectangle
        /// <summary>
        /// Gets the non split rectangle area.
        /// </summary>
        public Rectangle NonSplitRectangle
        {
            get { return _nonSplitRectangle; }
        }
        #endregion

        #region ButtonValues
        /// <summary>
        /// Gets and sets the source for button values.
        /// </summary>
        public IContentValues ButtonValues
        {
            get { return _drawContent.Values; }
            set { _drawContent.Values = value; }
        }
        #endregion

        #region DrawTabBorder
        /// <summary>
        /// Gets and sets if the border should be drawn as a tab border.
        /// </summary>
        public bool DrawTabBorder
        {
            get { return _drawCanvas.DrawTabBorder; }
            set { _drawCanvas.DrawTabBorder = value; }
        }
        #endregion

        #region TabBorderStyle
        /// <summary>
        /// Gets and sets the tab border style of the button.
        /// </summary>
        public TabBorderStyle TabBorderStyle
        {
            get { return _drawCanvas.TabBorderStyle; }
            set { _drawCanvas.TabBorderStyle = value; }
        }
        #endregion

        #region Enabled
        /// <summary>
		/// Gets and sets the enabled state of the element.
		/// </summary>
		public override bool Enabled
		{
			get { return base.Enabled; }

			set 
			{ 
				base.Enabled = value;

                if (Enabled && (ElementState == PaletteState.Disabled))
                {
                    if (Checked)
                        ElementState = PaletteState.CheckedNormal;
                    else
                        ElementState = PaletteState.Normal;
                }

				// Pass on the new state to the child elements
				_drawCanvas.Enabled = value;
				_drawContent.Enabled = value;
                _drawSplitBorder.Enabled = value;
                _drawDropDownButton.Enabled = value;
			}
		}
		#endregion

		#region Orientation
		/// <summary>
		/// Gets and sets the visual orientation.
		/// </summary>
		public virtual VisualOrientation Orientation
		{
            get { return _drawCanvas.Orientation; }
			set { SetOrientation(value, value); }
		}

        /// <summary>
        /// Set the orientation of the two button components.
        /// </summary>
        /// <param name="borderBackOrient">Orientation of the button border and background..</param>
        /// <param name="contentOrient">Orientation of the button contents.</param>
        public void SetOrientation(VisualOrientation borderBackOrient, 
                                   VisualOrientation contentOrient)
        {
            _drawCanvas.Orientation = borderBackOrient;
            _drawContent.Orientation = contentOrient;
            UpdateDropDown();
        }
		#endregion

		#region UseMnemonic
		/// <summary>
		/// Gets and sets usage of mnemonics.
		/// </summary>
		public bool UseMnemonic
		{
			get { return _drawContent.UseMnemonic; }
			set { _drawContent.UseMnemonic = value; }
		}
		#endregion

        #region Checked
        /// <summary>
        /// Gets and sets the checked state.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        #endregion

        #region AllowUncheck
        /// <summary>
        /// Gets and sets the allow uncheck state.
        /// </summary>
        public bool AllowUncheck
        {
            get { return _allowUncheck; }
            set { _allowUncheck = value; }
        }
        #endregion

        #region DrawButtonComposition
        /// <summary>
        /// Gets and sets the composition usage of the button.
        /// </summary>
        public bool DrawButtonComposition
        {
            get { return _drawCanvas.DrawCanvasOnComposition; }
            set { _drawCanvas.DrawCanvasOnComposition = value; }
        }
        #endregion

        #region TestForFocusCues
        /// <summary>
        /// Gets and sets the use of focus cues for deciding if focus rects are allowed.
        /// </summary>
        public bool TestForFocusCues
        {
            get { return _drawContent.TestForFocusCues; }
            set { _drawContent.TestForFocusCues = value; }
        }
        #endregion

        #region Palettes
        /// <summary>
        /// Update the source palettes for non-checked drawing.
        /// </summary>
        /// <param name="paletteDisabled">Palette source for the disabled state.</param>
        /// <param name="paletteNormal">Palette source for the normal state.</param>
        /// <param name="paletteTracking">Palette source for the tracking state.</param>
        /// <param name="palettePressed">Palette source for the pressed state.</param>
        public void SetPalettes(IPaletteTriple paletteDisabled,
                                IPaletteTriple paletteNormal,
                                IPaletteTriple paletteTracking,
                                IPaletteTriple palettePressed)
        {
            Debug.Assert(paletteDisabled != null);
            Debug.Assert(paletteNormal != null);
            Debug.Assert(paletteTracking != null);
            Debug.Assert(palettePressed != null);

            // Remember the new palette settings
            _paletteDisabled = paletteDisabled; 
            _paletteNormal = paletteNormal;
            _paletteTracking = paletteTracking;
            _palettePressed = palettePressed;

            // Must force update of palettes to use latest ones provided
            _forcePaletteUpdate = true;
        }

        /// <summary>
        /// Update the source palettes for checked state drawing.
        /// </summary>
        /// <param name="paletteCheckedNormal">Palette source for the normal checked state.</param>
        /// <param name="paletteCheckedTracking">Palette source for the tracking checked state.</param>
        /// <param name="paletteCheckedPressed">Palette source for the pressed checked state.</param>
        public void SetCheckedPalettes(IPaletteTriple paletteCheckedNormal,
                                       IPaletteTriple paletteCheckedTracking,
                                       IPaletteTriple paletteCheckedPressed)
        {
            Debug.Assert(paletteCheckedNormal != null);
            Debug.Assert(paletteCheckedTracking != null);
            Debug.Assert(paletteCheckedPressed != null);

            // Remember the new palette settings
            _paletteCheckedNormal = paletteCheckedNormal;
            _paletteCheckedTracking = paletteCheckedTracking;
            _paletteCheckedPressed = paletteCheckedPressed;

            // Must force update of palettes to use latest ones provided
            _forcePaletteUpdate = true;
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

            // Ensure that child elements have correct palette state
            CheckPaletteState(context);

            // Ask the renderer to evaluate the given palette
            return _drawCanvas.EvalTransparentPaint(context);
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
			Debug.Assert(_drawCanvas != null);

			// Ensure that child elements have correct palette state
			CheckPaletteState(context);

			// Delegate work to the child canvas
			return _drawCanvas.GetPreferredSize(context);
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

			// Ensure that child elements have correct palette state
			CheckPaletteState(context);

			// Let base class perform usual processing
			base.Layout(context);

            // Extend the split border so it is not restricted by the content size
            Rectangle splitClientRect = _drawSplitBorder.ClientRectangle;
            if (_drawSplitBorder.Orientation == System.Windows.Forms.Orientation.Vertical)
                splitClientRect = new Rectangle(splitClientRect.X, ClientRectangle.Y, splitClientRect.Width, ClientHeight);
            else
                splitClientRect = new Rectangle(ClientRectangle.X, splitClientRect.Y, ClientWidth, splitClientRect.Height);
            _drawSplitBorder.ClientRectangle = splitClientRect;

            // Calculate the split and non-split area
            _nonSplitRectangle = ClientRectangle;
            if (_dropDown && _splitter)
            {
                // Splitter rectangle depends on drop down position
                switch (_dropDownPosition)
                {
                    case VisualOrientation.Top:
                        _splitRectangle = ClientRectangle;
                        _splitRectangle.Height = _drawSplitBorder.ClientRectangle.Bottom;
                        _nonSplitRectangle.Height = ClientHeight - _splitRectangle.Height;
                        _nonSplitRectangle.Y = _splitRectangle.Bottom;
                        break;
                    case VisualOrientation.Bottom:
                        _splitRectangle = ClientRectangle;
                        _splitRectangle.Height = _splitRectangle.Bottom - _drawSplitBorder.ClientRectangle.Top;
                        _splitRectangle.Y = ClientRectangle.Bottom - _splitRectangle.Height;
                        _nonSplitRectangle.Height = ClientHeight - _splitRectangle.Height;
                        break;
                    case VisualOrientation.Left:
                        _splitRectangle = ClientRectangle;
                        _splitRectangle.Width = _drawSplitBorder.ClientRectangle.Right;
                        _nonSplitRectangle.Width = ClientWidth - _splitRectangle.Width;
                        _nonSplitRectangle.X = _splitRectangle.Right;
                        break;
                    case VisualOrientation.Right:
                        _splitRectangle = ClientRectangle;
                        _splitRectangle.Width = _splitRectangle.Right - _drawSplitBorder.ClientRectangle.Left;
                        _splitRectangle.X = ClientRectangle.Right - _splitRectangle.Width;
                        _nonSplitRectangle.Width = ClientWidth - _splitRectangle.Width;
                        break;
                }
            }
            else
                _splitRectangle = CommonHelper.NullRectangle;

            // Update canvas with the rectangle to use for drawing the split area and the non-split area
            _drawCanvas.SplitRectangle = _splitRectangle;
            _drawCanvas.NonSplitRectangle = _nonSplitRectangle;
        }
		#endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            Debug.Assert(context != null);

            // Ensure that child elements have correct palette state
            CheckPaletteState(context);

            // Let base class perform standard rendering
            base.Render(context);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Check that the palette and state are correct.
        /// </summary>
        /// <param name="context">Reference to the view context.</param>
        protected virtual void CheckPaletteState(ViewContext context)
        {
            // Default to using this element calculated state
            PaletteState buttonState = State;

            // If the actual control is not enabled, force to disabled state
            if (!IsFixed && !context.Control.Enabled)
                buttonState = PaletteState.Disabled;

            // Apply the checked state if not fixed
            if (!IsFixed && Checked)
            {
                // Is the checked button allowed to become unchecked
                if (AllowUncheck)
                {
                    // Show feedback on tracking and presssed
                    switch (buttonState)
                    {
                        case PaletteState.Normal:
                            buttonState = PaletteState.CheckedNormal;
                            break;
                        case PaletteState.Tracking:
                            buttonState = PaletteState.CheckedTracking;
                            break;
                        case PaletteState.Pressed:
                            buttonState = PaletteState.CheckedPressed;
                            break;
                    }
                }
                else
                {
                    // Always use the normal state as user cannot uncheck the button
                    buttonState = PaletteState.CheckedNormal;
                }
            }

            // If the child elements are not in correct state
            if (_forcePaletteUpdate || (_drawCanvas.ElementState != buttonState))
            {
                // No longer need to force the palettes to be updated
                _forcePaletteUpdate = false;

                // Switch the child elements over to correct state
                _drawCanvas.ElementState = buttonState;
                _drawContent.ElementState = buttonState;
                _drawSplitBorder.ElementState = buttonState;
                _drawDropDownButton.ElementState = buttonState;

                // Push the correct palettes into them
                switch (buttonState)
                {
                    case PaletteState.Disabled:
                        _paletteCurrent = _paletteDisabled;
                        break;
                    case PaletteState.Normal:
                        _paletteCurrent = _paletteNormal;
                        break;
                    case PaletteState.CheckedNormal:
                        _paletteCurrent = _paletteCheckedNormal;
                        break;
                    case PaletteState.Pressed:
                        _paletteCurrent = _palettePressed;
                        break;
                    case PaletteState.CheckedPressed:
                        _paletteCurrent = _paletteCheckedPressed;
                        break;
                    case PaletteState.Tracking:
                        _paletteCurrent = _paletteTracking;
                        break;
                    case PaletteState.CheckedTracking:
                        _paletteCurrent = _paletteCheckedTracking;
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }

                // Update with the correct palettes
                _drawCanvas.SetPalettes(_paletteCurrent.PaletteBack, _paletteCurrent.PaletteBorder);
                _drawContent.SetPalette(_paletteCurrent.PaletteContent);
                _edgeRedirect.SetPalette(_paletteCurrent.PaletteBorder);
            }
        }
        #endregion

        #region Implementation
        private void UpdateDropDown()
        {
            _drawDropDown.Visible = _dropDown;
            _drawSplitBorder.Visible = _splitter & _dropDown;
            _drawOuterSeparator.Visible = !_splitter & _dropDown;
            _drawCanvas.Splitter = _splitter & _dropDown;

            ViewDockStyle dockStyle = ViewDockStyle.Right;
            System.Windows.Forms.Orientation splitOrientation = System.Windows.Forms.Orientation.Vertical;
            switch (_dropDownPosition)
            {
                case VisualOrientation.Top:
                    dockStyle = ViewDockStyle.Top;
                    splitOrientation = System.Windows.Forms.Orientation.Horizontal;
                    break;
                case VisualOrientation.Bottom:
                    dockStyle = ViewDockStyle.Bottom;
                    splitOrientation = System.Windows.Forms.Orientation.Horizontal;
                    break;
                case VisualOrientation.Left:
                    dockStyle = ViewDockStyle.Left;
                    splitOrientation = System.Windows.Forms.Orientation.Vertical;
                    break;
                case VisualOrientation.Right:
                    dockStyle = ViewDockStyle.Right;
                    splitOrientation = System.Windows.Forms.Orientation.Vertical;
                    break;
            }

            _drawSplitBorder.Orientation = splitOrientation;
            _drawSplitBorder.VisualOrientation = Orientation;
            _layoutDocker.SetDock(_drawSplitBorder, dockStyle);
            _layoutDocker.SetDock(_drawDropDown, dockStyle);
            _layoutDocker.SetDock(_drawOuterSeparator, dockStyle);
        }
		#endregion
	}
}
