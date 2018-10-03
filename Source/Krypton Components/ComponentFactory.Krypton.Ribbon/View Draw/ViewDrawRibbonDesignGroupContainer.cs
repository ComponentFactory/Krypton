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
	/// Draws an design time only for adding a new container to a group.
	/// </summary>
    internal class ViewDrawRibbonDesignGroupContainer : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _padding = new Padding(1, 0, 0, 0);
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private KryptonRibbonGroup _ribbonGroup;
        private ContextMenuStrip _cms;
        #endregion

		#region Identity
        static ViewDrawRibbonDesignGroupContainer()
        {
            // Use image list to convert background Magenta to transparent
            _imageList = new ImageList();
            _imageList.TransparentColor = Color.Magenta;
            _imageList.Images.AddRange(new Image[]{Properties.Resources.KryptonRibbonGroupTriple,
                                                   Properties.Resources.KryptonRibbonGroupLines,
                                                   Properties.Resources.KryptonRibbonGroupSeparator,
                                                   Properties.Resources.KryptonGallery});
        }

		/// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignGroup class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGroup">Associated ribbon group.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignGroupContainer(KryptonRibbon ribbon,
                                                  KryptonRibbonGroup ribbonGroup,
                                                  NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonGroup != null);
            _ribbonGroup = ribbonGroup;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonDesignGroupContainer:" + Id;
		}
        #endregion

        #region Protected
        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public override string GetShortText()
        {
            return "New";
        }

        /// <summary>
        /// Gets the padding to use when calculating the preferred size.
        /// </summary>
        protected override Padding PreferredPadding
        {
            get { return _padding; }
        }

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected override Padding LayoutPadding
        {
            get { return Padding.Empty; }
        }

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected override Padding OuterPadding
        {
            get { return _padding; }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(object sender, EventArgs e)
        {
            // Create the context strip the first time around
            if (_cms == null)
            {
                _cms = new ContextMenuStrip();
                _cms.ImageList = _imageList;

                // Create child items
                ToolStripMenuItem menuTriple = new ToolStripMenuItem("Add Triple", null, new EventHandler(OnAddTriple));
                ToolStripMenuItem menuLines = new ToolStripMenuItem("Add Lines", null, new EventHandler(OnAddLines));
                ToolStripMenuItem menuSeparator = new ToolStripMenuItem("Add Separator", null, new EventHandler(OnAddSeparator));
                ToolStripMenuItem menuGallery = new ToolStripMenuItem("Add Gallery", null, new EventHandler(OnAddGallery));

                // Assign correct images
                menuTriple.ImageIndex = 0;
                menuLines.ImageIndex = 1;
                menuSeparator.ImageIndex = 2;
                menuGallery.ImageIndex = 3;

                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuTriple, menuLines, menuSeparator, menuGallery });
            }

            if (CommonHelper.ValidContextMenuStrip(_cms))
            {
                // Find the screen area of this view item
                Rectangle screenRect = Ribbon.ViewRectangleToScreen(this);

                // Make sure the popup is shown in a compatible way with any popups
                VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, new Point(screenRect.X, screenRect.Bottom));
            }
        }
        #endregion

        #region Implementation
        private void OnAddTriple(object sender, EventArgs e)
        {
            _ribbonGroup.OnDesignTimeAddTriple();
        }

        private void OnAddLines(object sender, EventArgs e)
        {
            _ribbonGroup.OnDesignTimeAddLines();
        }

        private void OnAddSeparator(object sender, EventArgs e)
        {
            _ribbonGroup.OnDesignTimeAddSeparator();
        }

        private void OnAddGallery(object sender, EventArgs e)
        {
            _ribbonGroup.OnDesignTimeAddGallery();
        }
        #endregion
    }
}
