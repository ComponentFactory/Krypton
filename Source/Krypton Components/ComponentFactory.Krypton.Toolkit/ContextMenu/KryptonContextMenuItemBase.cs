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
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Base class that all menu types must derive from and implement.
    /// </summary>
    public abstract class KryptonContextMenuItemBase : Component, INotifyPropertyChanged
    {
        #region Instance Fields
        private object _tag;
        private bool _visible;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        public KryptonContextMenuItemBase()
        {
            _visible = true;
        }
        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract int ItemChildCount { get; }

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract KryptonContextMenuItemBase this[int index] { get; }

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public abstract bool ProcessShortcut(Keys keyData);

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierachy being added.</returns>
        public abstract ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn);

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [KryptonPersist]
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [DefaultValue(null)]
        [Bindable(true)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Gets and sets if the item is visible in the context menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Determines if the item is visible in the context menu.")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Visible
        {
            get { return _visible; }
            
            set 
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Visible"));
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion
    }
}
