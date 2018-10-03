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
	/// Implement storage for tab specific palette border, background and content.
	/// </summary>
	public class PaletteTabTripleRedirect : Storage,
										    IPaletteTriple
	{
		#region Instance Fields
		private PaletteBack _back;
		private PaletteTabBorder _border;
		private PaletteContent _content;
		private PaletteBackInheritRedirect _backInherit;
		private PaletteBorderInheritRedirect _borderInherit;
		private PaletteContentInheritRedirect _contentInherit;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTabTripleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTabTripleRedirect(PaletteRedirect redirect,
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

			// Create storage that maps onto the inherit instances
            _back = new PaletteBack(_backInherit, needPaint);
            _border = new PaletteTabBorder(_borderInherit, needPaint);
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
		/// Update the palette styles using a tab style.
		/// </summary>
        /// <param name="tabStyle">New tab style.</param>
        public void SetStyles(TabStyle tabStyle)
        {
            switch (tabStyle)
            {
                case TabStyle.HighProfile:
                    SetStyles(PaletteBackStyle.TabHighProfile,
                              PaletteBorderStyle.TabHighProfile,
                              PaletteContentStyle.TabHighProfile);
                    break;
                case TabStyle.StandardProfile:
                    SetStyles(PaletteBackStyle.TabStandardProfile,
                              PaletteBorderStyle.TabStandardProfile,
                              PaletteContentStyle.TabStandardProfile);
                    break;
                case TabStyle.LowProfile:
                    SetStyles(PaletteBackStyle.TabLowProfile,
                              PaletteBorderStyle.TabLowProfile,
                              PaletteContentStyle.TabLowProfile);
                    break;
                case TabStyle.OneNote:
                    SetStyles(PaletteBackStyle.TabOneNote,
                              PaletteBorderStyle.TabOneNote,
                              PaletteContentStyle.TabOneNote);
                    break;
                case TabStyle.Dock:
                    SetStyles(PaletteBackStyle.TabDock,
                              PaletteBorderStyle.TabDock,
                              PaletteContentStyle.TabDock);
                    break;
                case TabStyle.DockAutoHidden:
                    SetStyles(PaletteBackStyle.TabDockAutoHidden,
                              PaletteBorderStyle.TabDockAutoHidden,
                              PaletteContentStyle.TabDockAutoHidden);
                    break;
                case TabStyle.Custom1:
                    SetStyles(PaletteBackStyle.TabCustom1,
                              PaletteBorderStyle.TabCustom1,
                              PaletteContentStyle.TabCustom1);
                    break;
                case TabStyle.Custom2:
                    SetStyles(PaletteBackStyle.TabCustom2,
                              PaletteBorderStyle.TabCustom2,
                              PaletteContentStyle.TabCustom2);
                    break;
                case TabStyle.Custom3:
                    SetStyles(PaletteBackStyle.TabCustom3,
                              PaletteBorderStyle.TabCustom3,
                              PaletteContentStyle.TabCustom3);
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
		public PaletteTabBorder Border
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

        #region Implementation
        private void SetStyles(PaletteBackStyle backStyle,
                               PaletteBorderStyle borderStyle,
                               PaletteContentStyle contentStyle)
        {
            BackStyle = backStyle;
            BorderStyle = borderStyle;
            ContentStyle = contentStyle;
        }
		#endregion
	}
}
