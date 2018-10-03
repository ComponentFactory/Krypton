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
	/// Draws an design time only for adding a new item to a triple container.
	/// </summary>
    internal class ViewDrawRibbonDesignGroupTriple : ViewDrawRibbonDesignBase
    {
        #region Static Fields
        private static readonly Padding _preferredPaddingL = new Padding(1, 3, 1, 3);
        private static readonly Padding _layoutPaddingL = new Padding(1);
        private static readonly Padding _outerPaddingL = new Padding(0, 2, 0, 2);
        private static readonly Padding _paddingMS = new Padding(0, 2, 0, 2);
        private static readonly ImageList _imageList;
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupTriple _ribbonTriple;
        private ContextMenuStrip _cms;
        private GroupItemSize _currentSize;
        #endregion

		#region Identity
        static ViewDrawRibbonDesignGroupTriple()
        {
            // Use image list to convert background Magenta to transparent
            _imageList = new ImageList();
            _imageList.TransparentColor = Color.Magenta;
            _imageList.Images.AddRange(new Image[]{Properties.Resources.KryptonRibbonGroupButton,                                                   
                                                   Properties.Resources.KryptonRibbonGroupColorButton,                                                   
                                                   Properties.Resources.KryptonRibbonGroupCheckBox,
                                                   Properties.Resources.KryptonRibbonGroupRadioButton,
                                                   Properties.Resources.KryptonRibbonGroupLabel,
                                                   Properties.Resources.KryptonRibbonGroupCustomControl,
                                                   Properties.Resources.KryptonRibbonGroupTextBox,
                                                   Properties.Resources.KryptonRibbonGroupRichTextBox,
                                                   Properties.Resources.KryptonRibbonGroupComboBox,
                                                   Properties.Resources.KryptonRibbonGroupMaskedTextBox,
                                                   Properties.Resources.KryptonRibbonGroupNumericUpDown,
                                                   Properties.Resources.KryptonRibbonGroupDomainUpDown,
                                                   Properties.Resources.KryptonRibbonGroupDateTimePicker,
                                                   Properties.Resources.KryptonRibbonGroupTrackBar});
        }

		/// <summary>
        /// Initialize a new instance of the ViewDrawRibbonDesignGroupTriple class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonTriple">Associated ribbon group triple.</param>
        /// <param name="currentSize">Size the view should use.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewDrawRibbonDesignGroupTriple(KryptonRibbon ribbon,
                                               KryptonRibbonGroupTriple ribbonTriple,
                                               GroupItemSize currentSize,
                                               NeedPaintHandler needPaint)
            : base(ribbon, needPaint)
        {
            Debug.Assert(ribbonTriple != null);

            _ribbonTriple = ribbonTriple;
            _currentSize = currentSize;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonDesignGroupTriple:" + Id;
		}
        #endregion

        #region CurrentSize
        /// <summary>
        /// Gets and sets the size the view should use.
        /// </summary>
        public GroupItemSize CurrentSize
        {
            get { return _currentSize; }
            set { _currentSize = value; }
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
            get { return (CurrentSize == GroupItemSize.Large ? _preferredPaddingL : _paddingMS); }
        }

        /// <summary>
        /// Gets the padding to use when laying out the view.
        /// </summary>
        protected override Padding LayoutPadding
        {
            get { return (CurrentSize == GroupItemSize.Large ? _layoutPaddingL : Padding.Empty); }
        }

        /// <summary>
        /// Gets the padding to shrink the client area by when laying out.
        /// </summary>
        protected override Padding OuterPadding
        {
            get { return (CurrentSize == GroupItemSize.Large ? _outerPaddingL : _paddingMS); }
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
                ToolStripMenuItem menuButton = new ToolStripMenuItem("Add Button", null, new EventHandler(OnAddButton));
                ToolStripMenuItem menuColorButton = new ToolStripMenuItem("Add Color Button", null, new EventHandler(OnAddColorButton));
                ToolStripMenuItem menuCheckBox = new ToolStripMenuItem("Add CheckBox", null, new EventHandler(OnAddCheckBox));
                ToolStripMenuItem menuCustomControl = new ToolStripMenuItem("Add Custom Control", null, new EventHandler(OnAddCustomControl));
                ToolStripMenuItem menuLabel = new ToolStripMenuItem("Add Label", null, new EventHandler(OnAddLabel));
                ToolStripMenuItem menuRadioButton = new ToolStripMenuItem("Add RadioButton", null, new EventHandler(OnAddRadioButton));
                ToolStripMenuItem menuTextBox = new ToolStripMenuItem("Add TextBox", null, new EventHandler(OnAddTextBox));
                ToolStripMenuItem menuMaskedTextBox = new ToolStripMenuItem("Add MaskedTextBox", null, new EventHandler(OnAddMaskedTextBox));
                ToolStripMenuItem menuRichTextBox = new ToolStripMenuItem("Add RichTextBox", null, new EventHandler(OnAddRichTextBox));
                ToolStripMenuItem menuComboBox = new ToolStripMenuItem("Add ComboBox", null, new EventHandler(OnAddComboBox));
                ToolStripMenuItem menuNumericUpDown = new ToolStripMenuItem("Add NumericUpDown", null, new EventHandler(OnAddNumericUpDown));
                ToolStripMenuItem menuDomainUpDown = new ToolStripMenuItem("Add DomainUpDown", null, new EventHandler(OnAddDomainUpDown));
                ToolStripMenuItem menuDateTimePicker = new ToolStripMenuItem("Add DateTimePicker", null, new EventHandler(OnAddDateTimePicker));
                ToolStripMenuItem menuTrackBar = new ToolStripMenuItem("Add TrackBar", null, new EventHandler(OnAddTrackBar));

                // Assign correct images
                menuButton.ImageIndex = 0;
                menuColorButton.ImageIndex = 1;
                menuCheckBox.ImageIndex = 2;
                menuRadioButton.ImageIndex = 3;
                menuLabel.ImageIndex = 4;
                menuCustomControl.ImageIndex = 5;
                menuTextBox.ImageIndex = 6;
                menuRichTextBox.ImageIndex = 7;
                menuComboBox.ImageIndex = 8;
                menuMaskedTextBox.ImageIndex = 9;
                menuNumericUpDown.ImageIndex = 10;
                menuDomainUpDown.ImageIndex = 11;
                menuDateTimePicker.ImageIndex = 12;
                menuTrackBar.ImageIndex = 13;

                // Finally, add all items to the strip
                _cms.Items.AddRange(new ToolStripItem[] { menuButton, menuColorButton, menuCheckBox, menuComboBox, menuCustomControl, menuDateTimePicker, menuDomainUpDown, menuLabel, menuNumericUpDown, menuRadioButton, menuRichTextBox, menuTextBox, menuTrackBar, menuMaskedTextBox });
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
            _ribbonTriple.OnDesignTimeAddButton();
        }

        private void OnAddColorButton(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddColorButton();
        }

        private void OnAddCheckBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddCheckBox();
        }

        private void OnAddRadioButton(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddRadioButton();
        }

        private void OnAddLabel(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddLabel();
        }

        private void OnAddCustomControl(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddCustomControl();
        }

        private void OnAddTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddTextBox();
        }

        private void OnAddMaskedTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddMaskedTextBox();
        }

        private void OnAddRichTextBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddRichTextBox();
        }

        private void OnAddComboBox(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddComboBox();
        }

        private void OnAddNumericUpDown(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddNumericUpDown();
        }

        private void OnAddDomainUpDown(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddDomainUpDown();
        }

        private void OnAddDateTimePicker(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddDateTimePicker();
        }

        private void OnAddTrackBar(object sender, EventArgs e)
        {
            _ribbonTriple.OnDesignTimeAddTrackBar();
        }
        #endregion
    }
}
