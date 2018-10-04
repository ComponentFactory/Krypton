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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Provide a collection of menu items.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuItems), "ToolboxBitmaps.KryptonContextMenuItems.bmp")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemsDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Items")]
    public class KryptonContextMenuItems : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private bool _standardStyle;
        private bool _imageColumn;
        private KryptonContextMenuItemCollection _items;
        private PaletteDoubleRedirect _stateNormal;
        private PaletteRedirectDouble _redirectImageColumn;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItems class.
        /// </summary>
        public KryptonContextMenuItems()
            : this(null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItems class.
        /// </summary>
        /// <param name="children">Array of initial child items.</param>
        public KryptonContextMenuItems(KryptonContextMenuItemBase[] children)
        {
            // Default fields
            _standardStyle = true;
            _imageColumn = true;
            _items = new KryptonContextMenuItemCollection();

            // Add any initial set of item
            if (children != null)
                _items.AddRange(children);

            // Create the redirector that can get values from the krypton context menu
            _redirectImageColumn = new PaletteRedirectDouble();

            // Create the column image storage for overriding specific values
            _stateNormal = new PaletteDoubleRedirect(_redirectImageColumn,
                                                     PaletteBackStyle.ContextMenuItemImageColumn,
                                                     PaletteBorderStyle.ContextMenuItemImageColumn);
        }

        /// <summary>
        /// Returns a description of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return "(Items)";
        }
        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemChildCount
        {
            get { return Items.Count; }
        }

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override KryptonContextMenuItemBase this[int index]
        {
            get { return Items[index]; }
        }

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public override bool ProcessShortcut(Keys keyData)
        {
            return Items.ProcessShortcut(keyData);
        }

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierachy being added.</returns>
        public override ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn)
        {
            // Add child items into columns of display views
            ViewLayoutStack itemsColumns = new ViewLayoutStack(true);
            Items.GenerateView(provider, this, this, itemsColumns, StandardStyle, ImageColumn);
            return itemsColumns;
        }

        /// <summary>
        /// Collection of standard menu items.
        /// </summary>
        [Category("Data")]
        [Description("Collection of standard menu items.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemCollectionEditor, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        public KryptonContextMenuItemCollection Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets and sets if the collection appears as standard or alternate items.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Determines if collection appears as standard or alternate items.")]
        [DefaultValue(true)]
        public bool StandardStyle
        {
            get { return _standardStyle; }
            
            set 
            {
                if (_standardStyle != value)
                {
                    _standardStyle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("StandardStyle"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the an image column is provided for background of images.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Determines if an image column is provided for background of images.")]
        [DefaultValue(true)]
        public bool ImageColumn
        {
            get { return _imageColumn; }
            
            set 
            {
                if (_imageColumn != value)
                {
                    _imageColumn = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageColumn"));
                }
            }
        }

        /// <summary>
        /// Gets access to the image column specific appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining image column specific appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion

        #region Internal
        internal void SetPaletteRedirect(PaletteDoubleRedirect redirector)
        {
            _redirectImageColumn.SetRedirectStates(redirector, redirector);
        }
        #endregion
    }
}
