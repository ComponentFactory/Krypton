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
	/// Implement storage for palette border and background.
	/// </summary>
	public class PaletteDoubleRedirect : Storage,
										 IPaletteDouble
	{
		#region Instance Fields
		private PaletteBack _back;
		private PaletteBorder _border;
		private PaletteBackInheritRedirect _backInherit;
		private PaletteBorderInheritRedirect _borderInherit;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteDoubleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
        public PaletteDoubleRedirect(PaletteRedirect redirect,
                                     PaletteBackStyle backStyle,
                                     PaletteBorderStyle borderStyle)
            : this(redirect, backStyle, borderStyle, null)
        {
        }

        /// <summary>
		/// Initialize a new instance of the PaletteDoubleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDoubleRedirect(PaletteRedirect redirect,
									 PaletteBackStyle backStyle,
									 PaletteBorderStyle borderStyle,
                                     NeedPaintHandler needPaint)
		{
            // Store the inherit instances
            PaletteBackInheritRedirect backInherit = new PaletteBackInheritRedirect(redirect, backStyle);
            PaletteBorderInheritRedirect borderInherit = new PaletteBorderInheritRedirect(redirect, borderStyle);

			// Create storage that maps onto the inherit instances
            PaletteBack back = new PaletteBack(backInherit, needPaint);
            PaletteBorder border = new PaletteBorder(borderInherit, needPaint);

            Construct(redirect, back, backInherit, border, borderInherit, needPaint);
        }

        /// <summary>
        /// Initialize a new instance of the PaletteDoubleRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="back">Storage for back values.</param>
        /// <param name="backInherit">Inheritence for back values.</param>
        /// <param name="border">Storage for border values.</param>
        /// <param name="borderInherit">Inheritence for border values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDoubleRedirect(PaletteRedirect redirect,
                                     PaletteBack back,
                                     PaletteBackInheritRedirect backInherit,
                                     PaletteBorder border,
                                     PaletteBorderInheritRedirect borderInherit,
                                     NeedPaintHandler needPaint)
        {
            Construct(redirect, back, backInherit, border, borderInherit, needPaint);
        }
        #endregion

        #region GetRedirector
        /// <summary>
        /// Gets the redirector instance.
        /// </summary>
        /// <returns>Return the currently used redirector.</returns>
        public PaletteRedirect GetRedirector()
        {
            return _backInherit.GetRedirector();
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
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">State to use when pulling values.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _back.PopulateFromBase(state);
            _border.PopulateFromBase(state);
        }
        #endregion

        #region IsDefault
        /// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get { return (Back.IsDefault && Border.IsDefault); }
		}
		#endregion

		#region SetStyles
        /// <summary>
        /// Update the palette styles to the separator style.
        /// </summary>
        /// <param name="backStyle">New back style.</param>
        /// <param name="borderStyle">New border style.</param>
        public void SetStyles(PaletteBackStyle backStyle,
                              PaletteBorderStyle borderStyle)
        {
            BackStyle = backStyle;
            BorderStyle = borderStyle;
        }

		/// <summary>
		/// Update the palette styles to the separator style.
		/// </summary>
        /// <param name="separatorStyle">New separator style.</param>
        public void SetStyles(SeparatorStyle separatorStyle)
        {
            switch (separatorStyle)
            {
                case SeparatorStyle.LowProfile:
                    SetStyles(PaletteBackStyle.SeparatorLowProfile, PaletteBorderStyle.SeparatorLowProfile);
                    break;
                case SeparatorStyle.HighProfile:
                    SetStyles(PaletteBackStyle.SeparatorHighProfile, PaletteBorderStyle.SeparatorHighProfile);
                    break;
                case SeparatorStyle.HighInternalProfile:
                    SetStyles(PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile);
                    break;
                case SeparatorStyle.Custom1:
                    SetStyles(PaletteBackStyle.SeparatorCustom1, PaletteBorderStyle.SeparatorCustom1);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Update the palette styles to the input control style.
        /// </summary>
        /// <param name="inputControlStyle">New input control style.</param>
        public void SetStyles(InputControlStyle inputControlStyle)
        {
            switch (inputControlStyle)
            {
                case InputControlStyle.Standalone:
                    SetStyles(PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone);
                    break;
                case InputControlStyle.Ribbon:
                    SetStyles(PaletteBackStyle.InputControlRibbon, PaletteBorderStyle.InputControlRibbon);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
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
        public virtual PaletteBack Back
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
        public virtual IPaletteBack PaletteBack
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
		public virtual PaletteBorder Border
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
        public virtual IPaletteBorder PaletteBorder
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

        #region Internal
        internal PaletteBorderInheritRedirect BorderRedirect
        {
            get { return _borderInherit; }
        }
        #endregion

        #region Private
        private void Construct(PaletteRedirect redirect,
                               PaletteBack back,
                               PaletteBackInheritRedirect backInherit,
                               PaletteBorder border,
                               PaletteBorderInheritRedirect borderInherit,
                               NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;
            _backInherit = backInherit;
            _borderInherit = borderInherit;
            _back = back;
            _border = border;
        }
        #endregion
    }
}
