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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents the base class for all ribbon group items.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public abstract class KryptonRibbonGroupItem : Component,
                                                   IRibbonGroupItem
    {
        #region Instance Fields
        private object _tag;
        private KryptonRibbon _ribbon;
        private KryptonRibbonTab _ribbonTab;
        private KryptonRibbonGroupContainer _ribbonContainer;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupItem class.
        /// </summary>
        public KryptonRibbonGroupItem()
        {
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbon Ribbon
        {
            get { return _ribbon; }
            set { _ribbon = value; }
        }

        /// <summary>
        /// Gets access to the owning ribbon tab.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbonTab RibbonTab
        {
            get { return _ribbonTab; }
            set { _ribbonTab = value; }
        }

        /// <summary>
        /// Gets and sets the owning ribbon container instance.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual KryptonRibbonGroupContainer RibbonContainer
        {
            get { return _ribbonContainer; }
            set { _ribbonContainer = value; }
        }

        /// <summary>
        /// Gets the visible state of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract bool Visible { get; set; }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeMaximum { get; set; }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeMinimum { get; set; }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract GroupItemSize ItemSizeCurrent { get; set; }

        /// <summary>
        /// Return the spacing gap between the provided previous item and this item.
        /// </summary>
        /// <param name="previousItem">Previous item.</param>
        /// <returns>Pixel gap between previous item and this item.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int ItemGap(IRibbonGroupItem previousItem)
        {
            // If the previous item is a group button cluster then we want 3 pixels
            if (previousItem is KryptonRibbonGroupCluster)
                return 3;

            // By default we just want a single pixel gap
            return 1;
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract ViewBase CreateView(KryptonRibbon ribbon, NeedPaintHandler needPaint);

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [Bindable(true)]
        public object Tag
        {
            get { return _tag; }

            set
            {
                if (value != _tag)
                    _tag = value;
            }
        }

        private bool ShouldSerializeTag()
        {
            return (Tag != null);
        }

        private void ResetTag()
        {
            Tag = null;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Get a value indicating if all parent containers are visible.
        /// </summary>
        protected bool ChainVisible
        {
            get
            {
                KryptonRibbonGroupContainer parent = RibbonContainer;

                // Search up chain until we find the top
                while (parent != null)
                {
                    // If any parent is not visible, then abort
                    if (!parent.Visible)
                        return false;

                    // Move up a level
                    parent = parent.RibbonContainer;
                }

                return true;
            }
        }
        #endregion

        #region Internal
        internal abstract bool ProcessCmdKey(ref Message msg, Keys keyData);

        internal virtual Image InternalToolTipImage 
        {
            get { return null; }
        }

        internal virtual LabelStyle InternalToolTipStyle
        {
            get { return LabelStyle.SuperTip; }
        }

        internal virtual Color InternalToolTipImageTransparentColor 
        {
            get { return Color.Empty; }
        }

        internal virtual string InternalToolTipTitle 
        {
            get { return string.Empty; }
        }

        internal virtual string InternalToolTipBody 
        {
            get { return string.Empty; }
        }
        #endregion
    }
}
