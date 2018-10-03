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
	public class PaletteTriple : Storage,
								 IPaletteTriple
	{
		#region Instance Fields
		private PaletteBack _back;
		private PaletteBorder _border;
		private PaletteContent _content;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTriple class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        public PaletteTriple(IPaletteTriple inherit)
            : this(inherit, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteTriple class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTriple(IPaletteTriple inherit,
                             NeedPaintHandler needPaint)
		{
            Debug.Assert(inherit != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _back = new PaletteBack(inherit.PaletteBack, needPaint);
            _border = new PaletteBorder(inherit.PaletteBorder, needPaint);
            _content = new PaletteContent(inherit.PaletteContent, needPaint);
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

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteTriple inherit)
        {
            _back.SetInherit(inherit.PaletteBack);
            _border.SetInherit(inherit.PaletteBorder);
            _content.SetInherit(inherit.PaletteContent);
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
		#endregion

		#region Implementation
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
