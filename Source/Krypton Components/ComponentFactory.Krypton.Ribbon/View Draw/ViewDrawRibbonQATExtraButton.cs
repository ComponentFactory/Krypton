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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws the extra quick access button used for customization or overflowing.
	/// </summary>
    internal class ViewDrawRibbonQATExtraButton : ViewLeaf
    {
        #region Static Fields
        private static readonly Size _viewSize = new Size(13, 22);
        private static readonly Size _contentSize = new Size(-4, -7);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private IDisposable _mementoBack;
        private EventHandler _finishDelegate;
        private bool _overflow;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the quick access toolbar button has been clicked.
        /// </summary>
        public event ClickAndFinishHandler ClickAndFinish;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonQATExtraButton class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonQATExtraButton(KryptonRibbon ribbon,
                                            NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);

            // Remember incoming references
            _ribbon = ribbon;

            // Create delegate used to process end of click action
            _finishDelegate = new EventHandler(ClickFinished);

            // Attach a controller to this element for the pressing of the button
            QATExtraButtonController controller = new QATExtraButtonController(ribbon, this, needPaint);
            controller.Click += new MouseEventHandler(OnClick);
            MouseController = controller;
            SourceController = controller;
            KeyController = controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonQATExtraButton:" + Id;
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

        #region KeyTipTarget
        /// <summary>
        /// Gets the key tip target for this view.
        /// </summary>
        public IRibbonKeyTipTarget KeyTipTarget
        {
            get { return SourceController as IRibbonKeyTipTarget; }
        }
        #endregion

        #region Overflow
        /// <summary>
        /// Gets and sets a value indicating if the button should be drawn as an overflow or context arrow.
        /// </summary>
        public bool Overflow
        {
            get { return _overflow; }
            set { _overflow = value; }
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets the visible state of the element.
        /// </summary>
        public override bool Visible
        {
            get { return (_ribbon.Visible && base.Visible); }
            set { base.Visible = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return _viewSize;
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
            // Update the enabled state of the button
            Enabled = _ribbon.Enabled;

            IPaletteBack paletteBack = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBack;
            IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBorder;
            IPaletteRibbonGeneral paletteGeneral = _ribbon.StateCommon.RibbonGeneral;

            // Do we need to draw the background?
            if (paletteBack.GetBackDraw(State) == InheritBool.True)
			{
                // Get the border path which the background is clipped to drawing within
                using (GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder, VisualOrientation.Top, State))
                {
                    Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, State, VisualOrientation.Top);

                    // Apply the padding depending on the orientation
                    Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

				    // Render the background inside the border path
                    _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                                                                                paletteBack, VisualOrientation.Top,
                                                                                State, _mementoBack);
                }
			}

            // Do we need to draw the border?
            if (paletteBorder.GetBorderDraw(State) == InheritBool.True)
                context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder, 
                                                                 VisualOrientation.Top, State);

            // Find the content area inside the button rectangle
            Rectangle contentRect = ClientRectangle;
            contentRect.Inflate(_contentSize);

            // Decide if we are drawing an overflow or context arrow image
            if (Overflow)
                context.Renderer.RenderGlyph.DrawRibbonOverflow(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
            else
                context.Renderer.RenderGlyph.DrawRibbonContextArrow(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
        }
        #endregion

        #region Implementation
        private void ClickFinished(object sender, EventArgs e)
        {
            // Get access to our mouse controller
            LeftDownButtonController controller = (LeftDownButtonController)MouseController;

            // Remove the fixed pressed appearance
            controller.RemoveFixed();
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            Form ownerForm = _ribbon.FindForm();

            // Ensure the form we are inside is active
            if (ownerForm != null)
                ownerForm.Activate();

            if ((ClickAndFinish != null) && !_ribbon.InDesignMode)
                ClickAndFinish(this, _finishDelegate);
            else
                ClickFinished(this, EventArgs.Empty);
        }
        #endregion
    }
}
