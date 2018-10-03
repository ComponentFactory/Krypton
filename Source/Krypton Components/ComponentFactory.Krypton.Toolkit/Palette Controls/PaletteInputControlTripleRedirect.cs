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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement storage for palette border, background and content for input control common state.
	/// </summary>
	public class PaletteInputControlTripleRedirect : Storage,
										             IPaletteTriple,
                                                     IPaletteMetric
	{
		#region Instance Fields
		private PaletteInputControlBackStates _back;
		private PaletteBorder _border;
		private PaletteInputControlContentStates _content;
		private PaletteBackInheritRedirect _backInherit;
		private PaletteBorderInheritRedirect _borderInherit;
		private PaletteContentInheritRedirect _contentInherit;
        private PaletteMetricRedirect _metricRedirect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteInputControlTripleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteInputControlTripleRedirect(PaletteRedirect redirect,
									             PaletteBackStyle backStyle,
									             PaletteBorderStyle borderStyle,
									             PaletteContentStyle contentStyle,
                                                 NeedPaintHandler needPaint)
		{
			Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;
            
            // Store the inherit instances
			_backInherit = new PaletteBackInheritRedirect(redirect, backStyle);
			_borderInherit = new PaletteBorderInheritRedirect(redirect, borderStyle);
            _contentInherit = new PaletteContentInheritRedirect(redirect, contentStyle);
            _metricRedirect = new PaletteMetricRedirect(redirect);

			// Create storage that maps onto the inherit instances
            _back = new PaletteInputControlBackStates(_backInherit, needPaint);
            _border = new PaletteBorder(_borderInherit, needPaint);
            _content = new PaletteInputControlContentStates(_contentInherit, needPaint);
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
				return (Back.IsDefault &&
						Border.IsDefault &&
						Content.IsDefault);
			}
		}
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public virtual void SetRedirector(PaletteRedirect redirect)
        {
            _backInherit.SetRedirector(redirect);
            _borderInherit.SetRedirector(redirect);
            _contentInherit.SetRedirector(redirect);
            _metricRedirect.SetRedirector(redirect);
        }
        #endregion

        #region SetStyles
        /// <summary>
        /// Update each individual style based on the input control style.
        /// </summary>
        /// <param name="style">New input control style.</param>
        public void SetStyles(InputControlStyle style)
        {
            switch (style)
            {
                case InputControlStyle.Standalone:
                    SetStyles(PaletteBackStyle.InputControlStandalone,
                              PaletteBorderStyle.InputControlStandalone,
                              PaletteContentStyle.InputControlStandalone);
                    break;
                case InputControlStyle.Ribbon:
                    SetStyles(PaletteBackStyle.InputControlRibbon,
                              PaletteBorderStyle.InputControlRibbon,
                              PaletteContentStyle.InputControlRibbon);
                    break;
                case InputControlStyle.Custom1:
                    SetStyles(PaletteBackStyle.InputControlCustom1,
                              PaletteBorderStyle.InputControlCustom1,
                              PaletteContentStyle.InputControlCustom1);
                    break;
            }
        }

        /// <summary>
        /// Update each individual style.
        /// </summary>
        /// <param name="backStyle">New background style.</param>
        /// <param name="borderStyle">New border style.</param>
        /// <param name="contentStyle">New content style.</param>
        public void SetStyles(PaletteBackStyle backStyle,
                              PaletteBorderStyle borderStyle,
                              PaletteContentStyle contentStyle)
        {
            BackStyle = backStyle;
            BorderStyle = borderStyle;
            ContentStyle = contentStyle;
        }
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _back.PopulateFromBase(state);
            _border.PopulateFromBase(state);
            _content.PopulateFromBase(state);
        }
        #endregion

		#region Back
		/// <summary>
		/// Gets access to the background palette details.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
		[Description("Overrides for defining background appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlBackStates Back
		{
			get { return _back; }
		}

		private bool ShouldSerializeBack()
		{
			return !_back.IsDefault;
		}

		/// <summary>
		/// Gets the background palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteBack PaletteBack
		{
			get { return Back; }
		}

		/// <summary>
		/// Gets and sets the back palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteBackStyle BackStyle
		{
			get { return _backInherit.Style; }
			set { _backInherit.Style = value; }
		}
		#endregion

		#region Border
		/// <summary>
		/// Gets access to the border palette details.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
		[Description("Overrides for defining border appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteBorder Border
		{
			get { return _border; }
		}

		private bool ShouldSerializeBorder()
		{
			return !_border.IsDefault;
		}

		/// <summary>
		/// Gets the border palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteBorder PaletteBorder
		{
			get { return Border; }
		}

		/// <summary>
		/// Gets and sets the border palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteBorderStyle BorderStyle
		{
			get { return _borderInherit.Style; }
			set { _borderInherit.Style = value; }
		}
		#endregion

		#region Content
		/// <summary>
		/// Gets access to the content palette details.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
		[Description("Overrides for defining content appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlContentStates Content
		{
			get { return _content; }
		}

		private bool ShouldSerializeContent()
		{
			return !_content.IsDefault;
		}

		/// <summary>
		/// Gets the content palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteContent PaletteContent
		{
			get { return Content; }
		}

		/// <summary>
		/// Gets and sets the content palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteContentStyle ContentStyle
		{
			get { return _contentInherit.Style; }
			set { _contentInherit.Style = value; }
		}
		#endregion

        #region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            return _metricRedirect.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            return _metricRedirect.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            return _metricRedirect.GetMetricPadding(state, metric);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Handle a change event from palette source.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="needLayout">True if a layout is also needed.</param>
        protected void OnNeedPaint(object sender, bool needLayout)
        {
            // Pass request from child to our own handler
            PerformNeedPaint(needLayout);
        }
        #endregion
    }
}
