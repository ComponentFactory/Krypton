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
	/// Draws an design time only for adding a new button to a cluster.
	/// </summary>
    internal class ViewDrawRibbonDesignCluster : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _padding = new Padding(1, 2, 0, 2);
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupCluster _ribbonCluster;
        private ContextMenuStrip _cms;
        #endregion

        #region Identity
        static ViewDrawRibbonDesignCluster()
        {
            // Use image list to convert background Magenta to transparent
            _imageList = new ImageList();
            _imageList.TransparentColor = Color.Magenta;
            _imageList.Images.AddRange(new Image[]{Properties.Resources.KryptonRibbonGroupClusterButton,                                                   
                                                   Properties.Resources.KryptonRibbonGroupClusterColorButton});
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignCluster class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonCluster">Reference to cluster definition.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignCluster(KryptonRibbon ribbon,
                                           KryptonRibbonGroupCluster ribbonCluster,
                                           NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonCluster != null);
            _ribbonCluster = ribbonCluster;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonDesignCluster:" + Id;
		}
        #endregion

        #region Protected
        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public override string GetShortText()
        {
            return "Item";
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
                ToolStripMenuItem menuButton = new ToolStripMenuItem("Add Cluster Button", null, new EventHandler(OnAddButton));
                ToolStripMenuItem menuColorButton = new ToolStripMenuItem("Add Cluster Color Button", null, new EventHandler(OnAddColorButton));

                // Assign correct images
                menuButton.ImageIndex = 0;
                menuColorButton.ImageIndex = 1;
                
                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuButton, menuColorButton });
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
        private void OnAddButton(object sender, EventArgs e)
        {
            _ribbonCluster.OnDesignTimeAddButton();
        }

        private void OnAddColorButton(object sender, EventArgs e)
        {
            _ribbonCluster.OnDesignTimeAddColorButton();
        }
        #endregion
    }
}
