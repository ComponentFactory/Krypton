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
	/// Implement storage for palette border, background and content.
	/// </summary>
	public class PaletteTripleRedirect : Storage,
										 IPaletteTriple
	{
		#region Instance Fields
		private PaletteBack _back;
		private PaletteBorder _border;
		private PaletteContent _content;
		private PaletteBackInheritRedirect _backInherit;
		private PaletteBorderInheritRedirect _borderInherit;
		private PaletteContentInheritRedirect _contentInherit;
		#endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTripleRedirect class.
        /// </summary>
        /// <param name="backStyle">Initial background style.</param>
        /// <param name="borderStyle">Initial border style.</param>
        /// <param name="contentStyle">Initial content style.</param>
        public PaletteTripleRedirect(PaletteBackStyle backStyle,
                                     PaletteBorderStyle borderStyle,
                                     PaletteContentStyle contentStyle)
            : this(null, backStyle, borderStyle, contentStyle, null)
        {
        }
        
        /// <summary>
		/// Initialize a new instance of the PaletteTripleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        public PaletteTripleRedirect(PaletteRedirect redirect,
                                     PaletteBackStyle backStyle,
                                     PaletteBorderStyle borderStyle,
                                     PaletteContentStyle contentStyle)
            : this(redirect, backStyle, borderStyle, contentStyle, null)
        {
        }

        /// <summary>
		/// Initialize a new instance of the PaletteTripleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTripleRedirect(PaletteRedirect redirect,
									 PaletteBackStyle backStyle,
									 PaletteBorderStyle borderStyle,
									 PaletteContentStyle contentStyle,
                                     NeedPaintHandler needPaint)
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;
            
            // Store the inherit instances
			_backInherit = new PaletteBackInheritRedirect(redirect, backStyle);
			_borderInherit = new PaletteBorderInheritRedirect(redirect, borderStyle);
			_contentInherit = new PaletteContentInheritRedirect(redirect, contentStyle);

			// Create storage that maps onto the inherit instances
            _back = new PaletteBack(_backInherit, needPaint);
            _border = new PaletteBorder(_borderInherit, needPaint);
            _content = new PaletteContent(_contentInherit, needPaint);
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
        }
        #endregion

        #region SetStyles
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

        /// <summary>
		/// Update the palette styles using a button style.
		/// </summary>
        /// <param name="buttonStyle">New button style.</param>
        public void SetStyles(ButtonStyle buttonStyle)
        {
            switch (buttonStyle)
            {
                case ButtonStyle.Standalone:
                    SetStyles(PaletteBackStyle.ButtonStandalone,
                              PaletteBorderStyle.ButtonStandalone,
                              PaletteContentStyle.ButtonStandalone);
                    break;
                case ButtonStyle.Alternate:
                    SetStyles(PaletteBackStyle.ButtonAlternate,
                              PaletteBorderStyle.ButtonAlternate,
                              PaletteContentStyle.ButtonAlternate);
                    break;
                case ButtonStyle.LowProfile:
                    SetStyles(PaletteBackStyle.ButtonLowProfile,
                              PaletteBorderStyle.ButtonLowProfile,
                              PaletteContentStyle.ButtonLowProfile);
                    break;
                case ButtonStyle.ButtonSpec:
                    SetStyles(PaletteBackStyle.ButtonButtonSpec,
                              PaletteBorderStyle.ButtonButtonSpec,
                              PaletteContentStyle.ButtonButtonSpec);
                    break;
                case ButtonStyle.BreadCrumb:
                    SetStyles(PaletteBackStyle.ButtonBreadCrumb,
                              PaletteBorderStyle.ButtonBreadCrumb,
                              PaletteContentStyle.ButtonBreadCrumb);
                    break;
                case ButtonStyle.CalendarDay:
                    SetStyles(PaletteBackStyle.ButtonCalendarDay,
                              PaletteBorderStyle.ButtonCalendarDay,
                              PaletteContentStyle.ButtonCalendarDay);
                    break;
                case ButtonStyle.Cluster:
                    SetStyles(PaletteBackStyle.ButtonCluster,
                              PaletteBorderStyle.ButtonCluster,
                              PaletteContentStyle.ButtonCluster);
                    break;
                case ButtonStyle.Gallery:
                    SetStyles(PaletteBackStyle.ButtonGallery,
                              PaletteBorderStyle.ButtonGallery,
                              PaletteContentStyle.ButtonGallery);
                    break;
                case ButtonStyle.NavigatorStack:
                    SetStyles(PaletteBackStyle.ButtonNavigatorStack,
                              PaletteBorderStyle.ButtonNavigatorStack,
                              PaletteContentStyle.ButtonNavigatorStack);
                    break;
                case ButtonStyle.NavigatorOverflow:
                    SetStyles(PaletteBackStyle.ButtonNavigatorOverflow,
                              PaletteBorderStyle.ButtonNavigatorOverflow,
                              PaletteContentStyle.ButtonNavigatorOverflow);
                    break;
                case ButtonStyle.NavigatorMini:
                    SetStyles(PaletteBackStyle.ButtonNavigatorMini,
                              PaletteBorderStyle.ButtonNavigatorMini,
                              PaletteContentStyle.ButtonNavigatorMini);
                    break;
                case ButtonStyle.InputControl:
                    SetStyles(PaletteBackStyle.ButtonInputControl,
                              PaletteBorderStyle.ButtonInputControl,
                              PaletteContentStyle.ButtonInputControl);
                    break;
                case ButtonStyle.ListItem:
                    SetStyles(PaletteBackStyle.ButtonListItem,
                              PaletteBorderStyle.ButtonListItem,
                              PaletteContentStyle.ButtonListItem);
                    break;
                case ButtonStyle.Form:
                    SetStyles(PaletteBackStyle.ButtonForm,
                              PaletteBorderStyle.ButtonForm,
                              PaletteContentStyle.ButtonForm);
                    break;
                case ButtonStyle.FormClose:
                    SetStyles(PaletteBackStyle.ButtonFormClose,
                              PaletteBorderStyle.ButtonFormClose,
                              PaletteContentStyle.ButtonFormClose);
                    break;
                case ButtonStyle.Command:
                    SetStyles(PaletteBackStyle.ButtonCommand,
                              PaletteBorderStyle.ButtonCommand,
                              PaletteContentStyle.ButtonCommand);
                    break;
                case ButtonStyle.Custom1:
                    SetStyles(PaletteBackStyle.ButtonCustom1,
                              PaletteBorderStyle.ButtonCustom1,
                              PaletteContentStyle.ButtonCustom1);
                    break;
                case ButtonStyle.Custom2:
                    SetStyles(PaletteBackStyle.ButtonCustom2,
                              PaletteBorderStyle.ButtonCustom2,
                              PaletteContentStyle.ButtonCustom2);
                    break;
                case ButtonStyle.Custom3:
                    SetStyles(PaletteBackStyle.ButtonCustom3,
                              PaletteBorderStyle.ButtonCustom3,
                              PaletteContentStyle.ButtonCustom3);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// Update the palette styles using a header style.
        /// </summary>
        /// <param name="headerStyle">New header style.</param>
        public void SetStyles(HeaderStyle headerStyle)
        {
            switch (headerStyle)
            {
                case HeaderStyle.Primary:
                    SetStyles(PaletteBackStyle.HeaderPrimary,
                              PaletteBorderStyle.HeaderPrimary,
                              PaletteContentStyle.HeaderPrimary);
                    break;
                case HeaderStyle.Secondary:
                    SetStyles(PaletteBackStyle.HeaderSecondary,
                              PaletteBorderStyle.HeaderSecondary,
                              PaletteContentStyle.HeaderSecondary);
                    break;
                case HeaderStyle.DockActive:
                    SetStyles(PaletteBackStyle.HeaderDockActive,
                              PaletteBorderStyle.HeaderDockActive,
                              PaletteContentStyle.HeaderDockActive);
                    break;
                case HeaderStyle.DockInactive:
                    SetStyles(PaletteBackStyle.HeaderDockInactive,
                              PaletteBorderStyle.HeaderDockInactive,
                              PaletteContentStyle.HeaderDockInactive);
                    break;
                case HeaderStyle.Form:
                    SetStyles(PaletteBackStyle.HeaderForm,
                              PaletteBorderStyle.HeaderForm,
                              PaletteContentStyle.HeaderForm);
                    break;
                case HeaderStyle.Calendar:
                    SetStyles(PaletteBackStyle.HeaderCalendar,
                              PaletteBorderStyle.HeaderCalendar,
                              PaletteContentStyle.HeaderCalendar);
                    break;
                case HeaderStyle.Custom1:
                    SetStyles(PaletteBackStyle.HeaderCustom1,
                              PaletteBorderStyle.HeaderCustom1,
                              PaletteContentStyle.HeaderCustom1);
                    break;
                case HeaderStyle.Custom2:
                    SetStyles(PaletteBackStyle.HeaderCustom2,
                              PaletteBorderStyle.HeaderCustom2,
                              PaletteContentStyle.HeaderCustom2);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
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
		public PaletteBack Back
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
		public PaletteContent Content
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
