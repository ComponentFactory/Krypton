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
	/// Draws a quick access toolbar button based on a IQuickAccessToolbarButton source.
	/// </summary>
    internal class ViewDrawRibbonQATButton : ViewComposite,
                                             IContentValues
    {
        #region Static Fields
        private static readonly Size _viewSize = new Size(22, 22);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private IQuickAccessToolbarButton _qatButton;
        private QATButtonToContent _contentProvider;
        private ViewDrawContent _drawContent;
        private IDisposable _mementoBack;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonQATButton class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="qatButton">Reference to button definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonQATButton(KryptonRibbon ribbon,
                                       IQuickAccessToolbarButton qatButton,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(qatButton != null);

            // Remember incoming references
            _ribbon = ribbon;
            _qatButton = qatButton;

            // If the source interface comes from a component then allow it to 
            // be selected at design time by clicking on the view instance
            Component = qatButton as System.ComponentModel.Component;

            // Attach a controller to this element for the pressing of the button
            QATButtonController controller = new QATButtonController(ribbon, this, needPaint);
            controller.Click += new MouseEventHandler(OnClick);
            SourceController = controller;
            KeyController = controller;

            // Create controller for intercepting events to determine tool tip handling
            MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager,
                                                    this, controller);


            // Use a class to convert from ribbon tab to content interface
            _contentProvider = new QATButtonToContent(qatButton);

            // Create and add the draw content for display inside the button
            _drawContent = new ViewDrawContent(_contentProvider, this, VisualOrientation.Top);
            Add(_drawContent);

            // Need to notice when the ribbon enable state changes
            _ribbon.EnabledChanged += new EventHandler(OnRibbonEnableChanged);

            // Set the initial enabled state
            UpdateEnabled();
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonQATButton:" + Id;
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

        #region QATButton
        /// <summary>
        /// Gets access to the source button this view represents.
        /// </summary>
        public IQuickAccessToolbarButton QATButton
        {
            get { return _qatButton; }
        }
        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get { return (base.Enabled && _ribbon.Enabled); }

            set
            {
                base.Enabled = value;
                UpdateEnabled();
            }
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
            // Make sure we reflect the current enabled state
            if (!Enabled && _ribbon.InDesignHelperMode)
                ElementState = PaletteState.Disabled;

            IPaletteBack paletteBack = _ribbon.StateCommon.RibbonQATButton.PaletteBack;
            IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonQATButton.PaletteBorder;
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

            base.RenderBefore(context);
        }
        #endregion

        #region Implementation
        private void OnRibbonEnableChanged(object sender, EventArgs e)
        {
            UpdateEnabled();
        }

        private void UpdateEnabled()
        {
            // Content is only enabled if the QAT button is enabled
            // and the owning ribbon control is also enabled
            _drawContent.Enabled = base.Enabled && _ribbon.Enabled;
        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            // We do not operate the qat button at design time
            if (!_ribbon.InDesignMode)
            {
                Form ownerForm = _ribbon.FindForm();

                // Ensure the form we are inside is active
                if (ownerForm != null)
                    ownerForm.Activate();

                // Inform quick access toolbar button it has been clicked
                _qatButton.PerformClick();
            }
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the image used for the ribbon tab.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Image.</returns>
        public Image GetImage(PaletteState state)
        {
            return _qatButton.GetImage();
        }

        /// <summary>
        /// Gets the image color that should be interpreted as transparent.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Transparent Color.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetShortText()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the long text used as the secondary ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion
    }
}
