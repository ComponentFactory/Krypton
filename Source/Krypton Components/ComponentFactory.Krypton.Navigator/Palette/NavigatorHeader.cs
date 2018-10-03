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
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Storage for header related properties.
	/// </summary>
    public class NavigatorHeader : Storage
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private bool _headerVisiblePrimary;
        private bool _headerVisibleSecondary;
        private bool _headerVisibleBar;
        private HeaderStyle _headerStylePrimary;
        private HeaderStyle _headerStyleSecondary;
        private HeaderStyle _headerStyleBar;
        private VisualOrientation _headerPositionPrimary;
        private VisualOrientation _headerPositionSecondary;
        private VisualOrientation _headerPositionBar;
        private HeaderGroupMappingPrimary _headerValuesPrimary;
        private HeaderGroupMappingSecondary _headerValuesSecondary;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorHeader class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorHeader(KryptonNavigator navigator,
                               NeedPaintHandler needPaint)
		{
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            _headerStylePrimary = HeaderStyle.Primary;
            _headerStyleSecondary = HeaderStyle.Secondary;
            _headerStyleBar = HeaderStyle.Secondary;
            _headerPositionPrimary = VisualOrientation.Top;
            _headerPositionSecondary = VisualOrientation.Bottom;
            _headerPositionBar = VisualOrientation.Top;
            _headerVisiblePrimary = true;
            _headerVisibleSecondary = true;
            _headerVisibleBar = true;
            _headerValuesPrimary = new HeaderGroupMappingPrimary(_navigator, needPaint);
            _headerValuesSecondary = new HeaderGroupMappingSecondary(_navigator, needPaint);
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return ((HeaderStylePrimary == HeaderStyle.Primary) &&
                        (HeaderStyleSecondary == HeaderStyle.Secondary) &&
                        (HeaderStyleBar == HeaderStyle.Secondary) &&
                        (HeaderPositionPrimary == VisualOrientation.Top) &&
                        (HeaderPositionSecondary == VisualOrientation.Bottom) &&
                        (HeaderPositionBar == VisualOrientation.Top) &&
                        HeaderVisiblePrimary &&
                        HeaderVisibleSecondary &&
                        HeaderVisibleBar &&
                        HeaderValuesPrimary.IsDefault &&
                        HeaderValuesSecondary.IsDefault);
            }
        }
        #endregion

        #region HeaderStylePrimary
        /// <summary>
        /// Gets and sets the primary header style.
        /// </summary>
        [Category("Visuals")]
        [Description("Primary header style.")]
        [DefaultValue(typeof(HeaderStyle), "Primary")]
        public HeaderStyle HeaderStylePrimary
        {
            get { return _headerStylePrimary; }

            set
            {
                if (_headerStylePrimary != value)
                {
                    _headerStylePrimary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderStylePrimary");
                }
            }
        }
        #endregion

        #region HeaderStyleSecondary
        /// <summary>
        /// Gets and sets the secondary header style.
        /// </summary>
        [Category("Visuals")]
        [Description("Secondary header style.")]
        [DefaultValue(typeof(HeaderStyle), "Secondary")]
        public HeaderStyle HeaderStyleSecondary
        {
            get { return _headerStyleSecondary; }

            set
            {
                if (_headerStyleSecondary != value)
                {
                    _headerStyleSecondary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderStyleSecondary");
                }
            }
        }
        #endregion

        #region HeaderStyleBar
        /// <summary>
        /// Gets and sets the bar header style.
        /// </summary>
        [Category("Visuals")]
        [Description("Bar header style.")]
        [DefaultValue(typeof(HeaderStyle), "Secondary")]
        public HeaderStyle HeaderStyleBar
        {
            get { return _headerStyleBar; }

            set
            {
                if (_headerStyleBar != value)
                {
                    _headerStyleBar = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderStyleBar");
                }
            }
        }
        #endregion

        #region HeaderPositionPrimary
        /// <summary>
        /// Gets and sets the position of the primary header.
        /// </summary>
        [Category("Visuals")]
        [Description("Edge position of the primary header.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation HeaderPositionPrimary
        {
            get { return _headerPositionPrimary; }

            set
            {
                if (_headerPositionPrimary != value)
                {
                    _headerPositionPrimary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderPositionPrimary");
                }
            }
        }
        #endregion

        #region HeaderPositionSecondary
        /// <summary>
        /// Gets and sets the position of the secondary header.
        /// </summary>
        [Category("Visuals")]
        [Description("Edge position of the secondary header.")]
        [DefaultValue(typeof(VisualOrientation), "Bottom")]
        public VisualOrientation HeaderPositionSecondary
        {
            get { return _headerPositionSecondary; }

            set
            {
                if (_headerPositionSecondary != value)
                {
                    _headerPositionSecondary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderPositionSecondary");
                }
            }
        }
        #endregion

        #region HeaderPositionBar
        /// <summary>
        /// Gets and sets the position of the bar header.
        /// </summary>
        [Category("Visuals")]
        [Description("Edge position of the bar header.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation HeaderPositionBar
        {
            get { return _headerPositionBar; }

            set
            {
                if (_headerPositionBar != value)
                {
                    _headerPositionBar = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderPositionBar");
                }
            }
        }
        #endregion

        #region HeaderVisiblePrimary
        /// <summary>
        /// Gets and sets the primary header visibility.
        /// </summary>
        [Category("Visuals")]
        [Description("Primary header visibility.")]
        [DefaultValue(true)]
        public bool HeaderVisiblePrimary
        {
            get { return _headerVisiblePrimary; }

            set
            {
                if (_headerVisiblePrimary != value)
                {
                    _headerVisiblePrimary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderVisiblePrimary");
                }
            }
        }
        #endregion

        #region HeaderVisibleSecondary
        /// <summary>
        /// Gets and sets the secondary header visibility.
        /// </summary>
        [Category("Visuals")]
        [Description("Secondary header visibility.")]
        [DefaultValue(true)]
        public bool HeaderVisibleSecondary
        {
            get { return _headerVisibleSecondary; }

            set
            {
                if (_headerVisibleSecondary != value)
                {
                    _headerVisibleSecondary = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderVisibleSecondary");
                }
            }
        }
        #endregion

        #region HeaderVisibleBar
        /// <summary>
        /// Gets and sets the bar header visibility.
        /// </summary>
        [Category("Visuals")]
        [Description("Bar header visibility.")]
        [DefaultValue(true)]
        public bool HeaderVisibleBar
        {
            get { return _headerVisibleBar; }

            set
            {
                if (_headerVisibleBar != value)
                {
                    _headerVisibleBar = value;
                    _navigator.OnViewBuilderPropertyChanged("HeaderVisibleBar");
                }
            }
        }
        #endregion

        #region HeaderValuesPrimary
        /// <summary>
        /// Gets access to the primary header content.
        /// </summary>
        [Category("Visuals")]
        [Description("Primary header values")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HeaderGroupMappingPrimary HeaderValuesPrimary
        {
            get { return _headerValuesPrimary; }
        }

        private bool ShouldSerializeHeaderValuesPrimary()
        {
            return !_headerValuesPrimary.IsDefault;
        }
        #endregion

        #region HeaderValuesSecondary
        /// <summary>
        /// Gets access to the secondary header content.
        /// </summary>
        [Category("Visuals")]
        [Description("Secondary header values")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HeaderGroupMappingSecondary HeaderValuesSecondary
        {
            get { return _headerValuesSecondary; }
        }

        private bool ShouldSerializeHeaderValuesSecondary()
        {
            return !_headerValuesSecondary.IsDefault;
        }
        #endregion
    }
}
