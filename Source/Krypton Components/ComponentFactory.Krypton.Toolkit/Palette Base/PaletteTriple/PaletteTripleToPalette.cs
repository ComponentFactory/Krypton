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
	/// Redirect all triple requests directly to the redirector with a fixed state.
	/// </summary>
	public class PaletteTripleToPalette : IPaletteTriple
	{
		#region Instance Fields
		private PaletteBackToPalette _back;
		private PaletteBorderToPalette _border;
		private PaletteContentToPalette _content;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTripleToPalette class.
		/// </summary>
        /// <param name="palette">Inheritence of values.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        public PaletteTripleToPalette(IPalette palette,
									  PaletteBackStyle backStyle,
									  PaletteBorderStyle borderStyle,
									  PaletteContentStyle contentStyle)
		{
            // Store the inherit instances
            _back = new PaletteBackToPalette(palette, backStyle);
            _border = new PaletteBorderToPalette(palette, borderStyle);
            _content = new PaletteContentToPalette(palette, contentStyle);
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
        #endregion

        #region Back
        /// <summary>
		/// Gets the background palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteBack PaletteBack
		{
			get { return _back; }
		}

		/// <summary>
		/// Gets and sets the back palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteBackStyle BackStyle
		{
            get { return _back.BackStyle; }
            set { _back.BackStyle = value; }
		}
		#endregion

		#region Border
		/// <summary>
		/// Gets the border palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteBorder PaletteBorder
		{
			get { return _border; }
		}

		/// <summary>
		/// Gets and sets the border palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteBorderStyle BorderStyle
		{
            get { return _border.BorderStyle; }
            set { _border.BorderStyle = value; }
		}
		#endregion

		#region Content
		/// <summary>
		/// Gets the content palette.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPaletteContent PaletteContent
		{
			get { return _content; }
		}

		/// <summary>
		/// Gets and sets the content palette style.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteContentStyle ContentStyle
		{
            get { return _content.ContentStyle; }
            set { _content.ContentStyle = value; }
		}
		#endregion
	}
}
