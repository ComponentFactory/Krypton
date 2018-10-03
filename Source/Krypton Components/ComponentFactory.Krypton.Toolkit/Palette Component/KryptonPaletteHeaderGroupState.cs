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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for header group values for a specific state.
	/// </summary>
    public class KryptonPaletteHeaderGroupState : Storage,
                                                  IPaletteMetric
    {
        #region Instance Fields
        private PaletteRedirect _redirect;
        private InheritBool _overlayHeaders;
        private Padding _primaryHeaderPadding;
        private Padding _secondaryHeaderPadding;
        private Padding _dockInactiveHeaderPadding;
        private Padding _dockActiveHeaderPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteHeaderGroupState class.
		/// </summary>
        /// <param name="redirect">Redirection for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteHeaderGroupState(PaletteRedirect redirect,
                                              NeedPaintHandler needPaint) 
		{
            Debug.Assert(redirect != null);

            // Remember redirection for inheritence
            _redirect = redirect;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Define default values
            _primaryHeaderPadding = CommonHelper.InheritPadding;
            _secondaryHeaderPadding = CommonHelper.InheritPadding;
            _dockInactiveHeaderPadding = CommonHelper.InheritPadding;
            _dockActiveHeaderPadding = CommonHelper.InheritPadding;
            _overlayHeaders = InheritBool.Inherit;
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
                return PrimaryHeaderPadding.Equals(CommonHelper.InheritPadding) &&
                       SecondaryHeaderPadding.Equals(CommonHelper.InheritPadding) &&
                       DockInactiveHeaderPadding.Equals(CommonHelper.InheritPadding) &&
                       DockActiveHeaderPadding.Equals(CommonHelper.InheritPadding) &&
                       (OverlayHeaders == InheritBool.Inherit);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            PrimaryHeaderPadding = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.HeaderGroupPaddingPrimary);
            SecondaryHeaderPadding = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.HeaderGroupPaddingSecondary);
            DockInactiveHeaderPadding = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.HeaderGroupPaddingDockInactive);
            DockActiveHeaderPadding = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.HeaderGroupPaddingDockActive);
            OverlayHeaders = _redirect.GetMetricBool(PaletteState.Normal, PaletteMetricBool.HeaderGroupOverlay);
        }
        #endregion

        #region PrimaryHeaderPadding
        /// <summary>
        /// Gets the padding used to position the primary header.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used to position the primary header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding PrimaryHeaderPadding
        {
            get { return _primaryHeaderPadding; }

            set
            {
                if (_primaryHeaderPadding != value)
                {
                    _primaryHeaderPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the PrimaryHeaderPadding to the default value.
        /// </summary>
        public void ResetPrimaryHeaderPadding()
        {
            PrimaryHeaderPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region SecondaryHeaderPadding
        /// <summary>
        /// Gets the padding used to position the secondary header.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used to position the secondary header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding SecondaryHeaderPadding
        {
            get { return _secondaryHeaderPadding; }

            set
            {
                if (_secondaryHeaderPadding != value)
                {
                    _secondaryHeaderPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the SecondaryHeaderPadding to the default value.
        /// </summary>
        public void ResetSecondaryHeaderPadding()
        {
            SecondaryHeaderPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region DockInactiveHeaderPadding
        /// <summary>
        /// Gets the padding used to position the dock inactive header.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used to position the dock inactive header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding DockInactiveHeaderPadding
        {
            get { return _dockInactiveHeaderPadding; }

            set
            {
                if (_dockInactiveHeaderPadding != value)
                {
                    _dockInactiveHeaderPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the DockInactiveHeaderPadding to the default value.
        /// </summary>
        public void ResetDockInactiveHeaderPadding()
        {
            DockInactiveHeaderPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region DockActiveHeaderPadding
        /// <summary>
        /// Gets the padding used to position the dock active header.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used to position the dock active header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding DockActiveHeaderPadding
        {
            get { return _dockActiveHeaderPadding; }

            set
            {
                if (_dockActiveHeaderPadding != value)
                {
                    _dockActiveHeaderPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the DockActiveHeaderPadding to the default value.
        /// </summary>
        public void ResetDockActiveHeaderPadding()
        {
            DockActiveHeaderPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region OverlayHeaders
        /// <summary>
		/// Gets and sets a value indicating if headers should overlay the border.
		/// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
		[Description("Should headers overlay the border.")]
		[DefaultValue(typeof(InheritBool), "Inherit")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public InheritBool OverlayHeaders
		{
			get { return _overlayHeaders; }

			set
			{
				if (_overlayHeaders != value)
				{
					_overlayHeaders = value;
					PerformNeedPaint();
				}
			}
        }

        /// <summary>
        /// Resets the OverlayHeaders property to its default value.
        /// </summary>
        public void ResetOverlayHeaders()
        {
            OverlayHeaders = InheritBool.Inherit;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public virtual int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            // Always pass onto the inheritance
            return _redirect.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            // Is this the metric we provide?
            if (metric == PaletteMetricBool.HeaderGroupOverlay)
            {
                // If the user has defined an actual value to use
                if (OverlayHeaders != InheritBool.Inherit)
                    return OverlayHeaders;
            }

            // Pass onto the inheritance
            return _redirect.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            switch (metric)
            {
                case PaletteMetricPadding.HeaderGroupPaddingPrimary:
                    if (!PrimaryHeaderPadding.Equals(CommonHelper.InheritPadding))
                        return PrimaryHeaderPadding;
                    break;
                case PaletteMetricPadding.HeaderGroupPaddingSecondary:
                    if (!SecondaryHeaderPadding.Equals(CommonHelper.InheritPadding))
                        return SecondaryHeaderPadding;
                    break;
                case PaletteMetricPadding.HeaderGroupPaddingDockInactive:
                    if (!DockInactiveHeaderPadding.Equals(CommonHelper.InheritPadding))
                        return DockInactiveHeaderPadding;
                    break;
                case PaletteMetricPadding.HeaderGroupPaddingDockActive:
                    if (!DockActiveHeaderPadding.Equals(CommonHelper.InheritPadding))
                        return DockActiveHeaderPadding;
                    break;
            }

            // Pass onto the inheritance
            return _redirect.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
