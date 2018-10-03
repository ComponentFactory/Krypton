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
using System.Reflection;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// View element that can draw a content
	/// </summary>
	public class ViewDrawContent : ViewLeaf
    {
        #region Static Fields
        private static PropertyInfo _pi = null;
        #endregion

        #region Instance Fields
        internal IPaletteContent _paletteContent;
        private IDisposable _memento;
		private IContentValues _values;
		private VisualOrientation _orientation;
        private bool _useMnemonic;
        private bool _drawOnComposition;
        private bool _testForFocusCues;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the ViewDrawContent class.
		/// </summary>
		/// <param name="paletteContent">Palette source for the content.</param>
		/// <param name="values">Reference to actual content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		public ViewDrawContent(IPaletteContent paletteContent, 
							   IContentValues values,
							   VisualOrientation orientation)
		{
			// Cache the starting values
			_paletteContent = paletteContent;
			_values = values;
			_orientation = orientation;

            // Default other state
            _drawOnComposition = false;
            _testForFocusCues = false;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
			return "ViewDrawContent:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of old memento first
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
		#endregion

        #region DrawContentOnComposition
        /// <summary>
        /// Gets and sets the composition value.
        /// </summary>
        public bool DrawContentOnComposition
        {
            get { return _drawOnComposition; }
            set { _drawOnComposition = value; }
        }
        #endregion

        #region TestForFocusCues
        /// <summary>
        /// Gets and sets the use of focus cues for deciding if focus rects are allowed.
        /// </summary>
        public bool TestForFocusCues
        {
            get { return _testForFocusCues; }
            set { _testForFocusCues = value; }
        }
        #endregion

        #region Values
        /// <summary>
        /// Gets and sets the source for values.
        /// </summary>
        public IContentValues Values
        {
            get { return _values; }
            set { _values = value; }
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

		#region UseMnemonic
		/// <summary>
		/// Gets and sets the use of mnemonics.
		/// </summary>
        public bool UseMnemonic
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _useMnemonic; }
			set { _useMnemonic = value; }
		}
		#endregion

		#region SetPalette
		/// <summary>
		/// Update the source palette for drawing.
		/// </summary>
		/// <param name="paletteContent">Palette source for the content.</param>
		public void SetPalette(IPaletteContent paletteContent)
		{
			Debug.Assert(paletteContent != null);

			// Use newly provided palette
			_paletteContent = paletteContent;
		}
		#endregion

        #region GetPalette
        /// <summary>
        /// Gets the source palette used for drawing.
        /// </summary>
        /// <returns>Palette source for the content.</returns>
        public IPaletteContent GetPalette()
        {
            return _paletteContent;
        }
        #endregion

        #region IsImageDisplayed
        /// <summary>
		/// Get a value indicating if the content image is being displayed.
		/// </summary>
        /// <param name="context">ViewLayoutContext context.</param>
        public bool IsImageDisplayed(ViewContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            bool isDisplayed = false;

			// If we have some content to investigate
            if (_paletteContent.GetContentDraw(State) == InheritBool.True)
                isDisplayed = context.Renderer.RenderStandardContent.GetContentImageDisplayed(_memento);

            return isDisplayed;
        }
        #endregion

        #region ImageRectangle
        /// <summary>
        /// Get a value indicating if the content image is being displayed.
        /// </summary>
        /// <param name="context">ViewLayoutContext context.</param>
        public Rectangle ImageRectangle(ViewContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            Rectangle imageRect = Rectangle.Empty;

            // If we have some content to investigate
            if (_paletteContent.GetContentDraw(State) == InheritBool.True)
                imageRect = context.Renderer.RenderStandardContent.GetContentImageRectangle(_memento);

            return imageRect;
        }
        #endregion

        #region ShortTextRect
        /// <summary>
        /// Gets the short text drawing rectangle.
        /// </summary>
        /// <returns>Rectangle of short text drawing.</returns>
        public Rectangle ShortTextRect(ViewContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            Rectangle textRect = Rectangle.Empty;

            // If we have some content to investigate
            if (_paletteContent.GetContentDraw(State) == InheritBool.True)
                textRect = context.Renderer.RenderStandardContent.GetContentShortTextRectangle(_memento);

            return textRect;
        }
        #endregion

        #region LongTextRect
        /// <summary>
        /// Gets the short text drawing rectangle.
        /// </summary>
        /// <returns>Rectangle of short text drawing.</returns>
        public Rectangle LongTextRect(ViewContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            Rectangle textRect = Rectangle.Empty;

            // If we have some content to investigate
            if (_paletteContent.GetContentDraw(State) == InheritBool.True)
                textRect = context.Renderer.RenderStandardContent.GetContentLongTextRectangle(_memento);

            return textRect;
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
            
            // By default we take up no space at all
			Size preferredSize = Size.Empty;

			// If we have some content to encompass
			if (_paletteContent.GetContentDraw(State) == InheritBool.True)
			{
				// Ask the renderer for the contents preferred size
				preferredSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context,
																					           _paletteContent,
																					           _values,
																					           Orientation,
																					           State,
                                                                                               DrawContentOnComposition);
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

			// Do we need to draw the content?
			if (_paletteContent.GetContentDraw(State) == InheritBool.True)
			{
                // Dispose of old memento first
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }

				// Ask the renderer to perform any internal laying out calculations and 
				// store the returned memento ready for whenever a draw is required
				_memento = context.Renderer.RenderStandardContent.LayoutContent(context,
																		        ClientRectangle,
																		        _paletteContent,
																		        _values,
																		        Orientation,
																		        State,
                                                                                DrawContentOnComposition);
			}
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

            // Do we need to draw the content?
			if (_paletteContent.GetContentDraw(State) == InheritBool.True)
			{
                bool allowFocusRect = (_testForFocusCues ? ShowFocusCues(context.Control) : true);

				// Draw using memento returned from render layout
				context.Renderer.RenderStandardContent.DrawContent(context, 
														           ClientRectangle,
														           _paletteContent, 
														           _memento, 
														           Orientation,
														           State,
                                                                   DrawContentOnComposition,
                                                                   allowFocusRect);
			}
		}
		#endregion

        #region Implementation
        private bool ShowFocusCues(Control c)
        {
            if (_pi == null)
            {
                _pi = typeof(Control).GetProperty("ShowFocusCues", BindingFlags.Instance |
                                                                   BindingFlags.GetProperty |
                                                                   BindingFlags.NonPublic);
            }

            return (bool)_pi.GetValue(c, null);
        }
        #endregion
    }
}
