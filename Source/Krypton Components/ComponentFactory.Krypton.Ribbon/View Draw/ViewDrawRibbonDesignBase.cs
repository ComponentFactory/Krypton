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
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a design time item.
	/// </summary>
    internal class ViewDrawRibbonDesignBase : ViewComposite,
                                              IContentValues
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaint;
        private DesignTextToContent _contentProvider;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignBase class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignBase(KryptonRibbon ribbon,
                                        NeedPaintHandler needPaint)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(needPaint != null);

            // Cache incoming values
            _ribbon = ribbon;
            _needPaint = needPaint;

            // Create and add the draw content for display inside the tab
            _contentProvider = new DesignTextToContent(ribbon);
            Add(new ViewDrawContent(_contentProvider, this, VisualOrientation.Top));

            // Use a controller to change state because of mouse movement
            ViewHightlightController controller = new ViewHightlightController(this, needPaint);
            controller.Click += new EventHandler(OnClick);
            MouseController = controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonDesignBase:" + Id;
		}
        #endregion

        #region Ribbon
        /// <summary>
        /// Gets access to the ribbon control instance.
        /// </summary>
        public KryptonRibbon Ribbon
        {
            get { return _ribbon; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Size retSize = base.GetPreferredSize(context);

            // Apply the padding around the child elements
            retSize = CommonHelper.ApplyPadding(Orientation.Horizontal, retSize, PreferredPadding);

            return retSize;
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

            // Reduce our size by a padding around the element
            ClientRectangle = new Rectangle(ClientLocation.X + OuterPadding.Left,
                                            ClientLocation.Y + OuterPadding.Top,
                                            ClientWidth - OuterPadding.Horizontal,
                                            ClientHeight - OuterPadding.Vertical);

            // Reduce again to arrive at size available for child elements
            context.DisplayRectangle = new Rectangle(ClientLocation.X + LayoutPadding.Left,
                                                     ClientLocation.Y + LayoutPadding.Top,
                                                     ClientWidth - LayoutPadding.Horizontal,
                                                     ClientHeight - LayoutPadding.Vertical);

            // Let contained content element we layed out
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
            // Ensure the child text view has same state as us
            this[0].ElementState = ElementState;

            // Draw background using the design time colors
            DesignTimeDraw.DrawArea(_ribbon, context, ClientRectangle, State);

            base.RenderBefore(context);
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
            return null;
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
        public virtual string GetShortText()
        {
            return "Unknown";
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

        #region Protected
        /// <summary>
        /// Gets the padding to use when calculating the preferred size.
        /// </summary>
        protected virtual Padding PreferredPadding
        {
            get { return Padding.Empty; }
        }

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected virtual Padding LayoutPadding
        {
            get { return Padding.Empty; }
        }

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected virtual Padding OuterPadding
        {
            get { return Padding.Empty; }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnClick(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
