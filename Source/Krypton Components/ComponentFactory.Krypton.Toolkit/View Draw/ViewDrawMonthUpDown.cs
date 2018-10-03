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
	/// View element that can draw a month calendar button.
	/// </summary>
    public class ViewDrawMonthUpDown : ViewDrawButton
    {
        #region Enums
        /// <summary>
        /// Specific the possible glyphs the button can draw.
        /// </summary>
        public enum DrawMonthCalendarGlyph
        {
            /// <summary>
            /// Specifies the drop down button glyph.
            /// </summary>
            DropDownButton,

            /// <summary>
            /// Specifies the up button glyph.
            /// </summary>
            UpButton,

            /// <summary>
            /// Specifies the down button glyph.
            /// </summary>
            DownButton
        }
        #endregion

        #region Instance Fields
        private DrawMonthCalendarGlyph _glyph;
        private ButtonController _controller;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the mouse is used to left select the target.
        /// </summary>
        public event MouseEventHandler MouseSelect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMonthUpDown class.
		/// </summary>
        /// <param name="paletteState">Palette source for states.</param>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="buttonValues">Source for content values.</param>
        /// <param name="glyph">Glyph to be drawn.</param>
        /// <param name="needPaintHandler">Delegate for requests repainting.</param>
        public ViewDrawMonthUpDown(IPaletteTriple paletteState,
                                   IPaletteMetric paletteMetric,
                                   IContentValues buttonValues,
                                   DrawMonthCalendarGlyph glyph,
                                   NeedPaintHandler needPaintHandler)        
            : base(paletteState, paletteState, paletteState, paletteState, 
                   paletteMetric, buttonValues, VisualOrientation.Top, false)
        {
            _glyph = glyph;

            // Assign a controller to handle visual interaction
            _controller = new ButtonController(this, needPaintHandler);
            _controller.Click += new MouseEventHandler(OnButtonClick);
            _controller.MouseSelect += new MouseEventHandler(OnButtonMouseSelect);
            _controller.Repeat = true;
            _controller.ClickOnDown = true;
            MouseController = _controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMonthUpDown:" + Id;
		}
		#endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // We want to be as wide as drop down buttons on standard controls
            return new Size(SystemInformation.VerticalScrollBarWidth - 2, 0);
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

            // Layout the button drawing elements using a reduced size
            Rectangle beforeRect = context.DisplayRectangle;
            switch (_glyph)
            {
                case DrawMonthCalendarGlyph.DropDownButton:
                    context.DisplayRectangle = new Rectangle(beforeRect.X, beforeRect.Y + 1, beforeRect.Width, beforeRect.Height - 2);
                    break;
                case DrawMonthCalendarGlyph.UpButton:
                    context.DisplayRectangle = new Rectangle(beforeRect.X, beforeRect.Y + 1, beforeRect.Width, beforeRect.Height - 1);
                    break;
                case DrawMonthCalendarGlyph.DownButton:
                    context.DisplayRectangle = new Rectangle(beforeRect.X, beforeRect.Y, beforeRect.Width, beforeRect.Height - 1);
                    break;
            }

            base.Layout(context);

            // Restore the original size and use that as the actual client rectane
            context.DisplayRectangle = beforeRect;
            ClientRectangle = beforeRect;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderAfter(RenderContext context)
        {
            switch (_glyph)
            {
                case DrawMonthCalendarGlyph.DropDownButton:
                    context.Renderer.RenderGlyph.DrawInputControlDropDownGlyph(context, ClientRectangle, CurrentPalette.PaletteContent, State);
                    break;
                case DrawMonthCalendarGlyph.UpButton:
                    context.Renderer.RenderGlyph.DrawInputControlNumericUpGlyph(context, ClientRectangle, CurrentPalette.PaletteContent, State);
                    break;
                case DrawMonthCalendarGlyph.DownButton:
                    context.Renderer.RenderGlyph.DrawInputControlNumericDownGlyph(context, ClientRectangle, CurrentPalette.PaletteContent, State);
                    break;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments assocaited with the event.</param>
        protected void OnButtonClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        /// <summary>
        /// Raises the MouseSelect event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments assocaited with the event.</param>
        protected void OnButtonMouseSelect(object sender, MouseEventArgs e)
        {
            if (MouseSelect != null)
                MouseSelect(this, e);
        }
        #endregion
    }
}
