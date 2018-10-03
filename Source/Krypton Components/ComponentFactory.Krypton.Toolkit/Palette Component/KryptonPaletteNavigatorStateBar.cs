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
	/// Storage for bar values for a navigator state.
	/// </summary>
    public class KryptonPaletteNavigatorStateBar : Storage,
                                                   IPaletteMetric
    {
        #region Instance Fields
        private PaletteRedirect _redirect;
        private Padding _barPaddingTabs;
        private Padding _barPaddingInside;
        private Padding _barPaddingOutside;
        private Padding _barPaddingOnly;
        private Padding _buttonPadding;
        private int _buttonEdgeInside;
        private int _buttonEdgeOutside;
        private int _checkButtonGap;
        private int _ribbonTabGap;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteNavigatorStateBar class.
		/// </summary>
        /// <param name="redirect">Redirection for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteNavigatorStateBar(PaletteRedirect redirect,
                                               NeedPaintHandler needPaint) 
		{
            Debug.Assert(redirect != null);

            // Remember redirection for inheritence
            _redirect = redirect;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Define default values
            _barPaddingTabs = CommonHelper.InheritPadding;
            _barPaddingInside = CommonHelper.InheritPadding;
            _barPaddingOutside = CommonHelper.InheritPadding;
            _barPaddingOnly = CommonHelper.InheritPadding;
            _buttonPadding = CommonHelper.InheritPadding;
            _buttonEdgeInside = -1;
            _buttonEdgeOutside = -1;
            _checkButtonGap = -1;
            _ribbonTabGap = -1;
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
                return BarPaddingTabs.Equals(CommonHelper.InheritPadding) &&
                       BarPaddingInside.Equals(CommonHelper.InheritPadding) &&
                       BarPaddingOutside.Equals(CommonHelper.InheritPadding) &&
                       BarPaddingOnly.Equals(CommonHelper.InheritPadding) &&
                       ButtonPadding.Equals(CommonHelper.InheritPadding) &&
                       (ButtonEdgeInside == -1) &&
                       (ButtonEdgeOutside == -1) &&
                       (CheckButtonGap == -1) &&
                       (RibbonTabGap == -1);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            BarPaddingInside = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.BarPaddingInside);
            BarPaddingOutside = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.BarPaddingOutside);
            BarPaddingOnly = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.BarPaddingOnly);
            ButtonPadding = _redirect.GetMetricPadding(PaletteState.Normal, PaletteMetricPadding.BarButtonPadding);
            ButtonEdgeInside = _redirect.GetMetricInt(PaletteState.Normal, PaletteMetricInt.BarButtonEdgeInside);
            ButtonEdgeOutside = _redirect.GetMetricInt(PaletteState.Normal, PaletteMetricInt.BarButtonEdgeOutside);
            CheckButtonGap = _redirect.GetMetricInt(PaletteState.Normal, PaletteMetricInt.CheckButtonGap);
            RibbonTabGap = _redirect.GetMetricInt(PaletteState.Normal, PaletteMetricInt.RibbonTabGap);
        }
        #endregion

        #region BarPaddingTabs
        /// <summary>
        /// Gets and sets the padding used around the bar when displaying tabs.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used around the bar when displaying tabs.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding BarPaddingTabs
        {
            get { return _barPaddingTabs; }

            set
            {
                if (_barPaddingTabs != value)
                {
                    _barPaddingTabs = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BarPaddingTabs to the default value.
        /// </summary>
        public void ResetBarPaddingTabs()
        {
            BarPaddingTabs = CommonHelper.InheritPadding;
        }
        #endregion

        #region BarPaddingInside
        /// <summary>
        /// Gets and sets the padding used around the bar when placed inside the group.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used around the bar when placed inside the group.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding BarPaddingInside
        {
            get { return _barPaddingInside; }

            set
            {
                if (_barPaddingInside != value)
                {
                    _barPaddingInside = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BarPaddingInside to the default value.
        /// </summary>
        public void ResetBarPaddingInside()
        {
            BarPaddingInside = CommonHelper.InheritPadding;
        }
        #endregion

        #region BarPaddingOutside
        /// <summary>
        /// Gets and sets the padding used around the bar when placed outside the group.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used around the bar when placed outside the group.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding BarPaddingOutside
        {
            get { return _barPaddingOutside; }

            set
            {
                if (_barPaddingOutside != value)
                {
                    _barPaddingOutside = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BarPaddingOutside to the default value.
        /// </summary>
        public void ResetBarPaddingOutside()
        {
            BarPaddingOutside = CommonHelper.InheritPadding;
        }
        #endregion

        #region BarPaddingOnly
        /// <summary>
        /// Gets and sets the padding used around the bar when placed on its own.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used around the bar when placed on its own.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding BarPaddingOnly
        {
            get { return _barPaddingOnly; }

            set
            {
                if (_barPaddingOnly != value)
                {
                    _barPaddingOnly = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BarPaddingOnly to the default value.
        /// </summary>
        public void ResetBarPaddingOnly()
        {
            BarPaddingOnly = CommonHelper.InheritPadding;
        }
        #endregion

        #region ButtonPadding
        /// <summary>
        /// Gets and sets the padding used around each button on the button bar.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used around each button on the button bar.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding ButtonPadding
        {
            get { return _buttonPadding; }

            set
            {
                if (_buttonPadding != value)
                {
                    _buttonPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonPadding to the default value.
        /// </summary>
        public void ResetButtonPadding()
        {
            ButtonPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region ButtonEdgeOutside
        /// <summary>
        /// Gets the sets how far to inset buttons from the control edge.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("How far to inset buttons from the control edge.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int ButtonEdgeOutside
        {
            get { return _buttonEdgeOutside; }

            set
            {
                if (_buttonEdgeOutside != value)
                {
                    _buttonEdgeOutside = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonEdgeOutside to the default value.
        /// </summary>
        public void ResetButtonEdgeOutside()
        {
            ButtonEdgeOutside = -1;
        }
        #endregion

        #region ButtonEdgeInside
        /// <summary>
        /// Gets the sets how far to inset buttons from the button bar.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("How far to inset buttons from the button bar.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int ButtonEdgeInside
        {
            get { return _buttonEdgeInside; }

            set
            {
                if (_buttonEdgeInside != value)
                {
                    _buttonEdgeInside = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonEdgeInside to the default value.
        /// </summary>
        public void ResetButtonEdgeInside()
        {
            ButtonEdgeInside = -1;
        }
        #endregion

        #region CheckButtonGap
        /// <summary>
        /// Gets the sets the spacing gap between each check button.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Spacing gap between each check button.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int CheckButtonGap
        {
            get { return _checkButtonGap; }

            set
            {
                if (_checkButtonGap != value)
                {
                    _checkButtonGap = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the CheckButtonGap to the default value.
        /// </summary>
        public void ResetCheckButtonGap()
        {
            CheckButtonGap = -1;
        }
        #endregion

        #region RibbonTabGap
        /// <summary>
        /// Gets the sets the spacing gap between each ribbon tab.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Spacing gap between each ribbon tab.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int RibbonTabGap
        {
            get { return _ribbonTabGap; }

            set
            {
                if (_ribbonTabGap != value)
                {
                    _ribbonTabGap = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the RibbonTabGap to the default value.
        /// </summary>
        public void ResetRibbonTabGap()
        {
            RibbonTabGap = -1;
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
            switch (metric)
            {
                case PaletteMetricInt.BarButtonEdgeInside:
                    if (ButtonEdgeInside != -1)
                        return ButtonEdgeInside;
                    break;
                case PaletteMetricInt.BarButtonEdgeOutside:
                    if (ButtonEdgeOutside != -1)
                        return ButtonEdgeOutside;
                    break;
                case PaletteMetricInt.CheckButtonGap:
                    if (CheckButtonGap != -1)
                        return CheckButtonGap;
                    break;
                case PaletteMetricInt.RibbonTabGap:
                    if (RibbonTabGap != -1)
                        return RibbonTabGap;
                    break;
            }

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
                case PaletteMetricPadding.BarPaddingTabs:
                    if (!BarPaddingTabs.Equals(CommonHelper.InheritPadding))
                        return BarPaddingTabs;
                    break;
                case PaletteMetricPadding.BarPaddingInside:
                    if (!BarPaddingInside.Equals(CommonHelper.InheritPadding))
                        return BarPaddingInside;
                    break;
                case PaletteMetricPadding.BarPaddingOutside:
                    if (!BarPaddingOutside.Equals(CommonHelper.InheritPadding))
                        return BarPaddingOutside;
                    break;
                case PaletteMetricPadding.BarPaddingOnly:
                    if (!BarPaddingOnly.Equals(CommonHelper.InheritPadding))
                        return BarPaddingOnly;
                    break;
                case PaletteMetricPadding.BarButtonPadding:
                    if (!ButtonPadding.Equals(CommonHelper.InheritPadding))
                        return ButtonPadding;
                    break;
            }

            // Pass onto the inheritance
            return _redirect.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
