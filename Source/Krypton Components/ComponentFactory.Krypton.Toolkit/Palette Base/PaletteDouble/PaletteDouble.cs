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
	public class PaletteDouble : Storage,
								 IPaletteDouble
	{
		#region Instance Fields
		private PaletteBack _back;
		private PaletteBorder _border;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteDouble class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        public PaletteDouble(IPaletteDouble inherit)
            : this(inherit, null)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteDouble class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDouble(IPaletteDouble inherit,
                             NeedPaintHandler needPaint)
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Create storage that maps onto the inherit instances
            _back = new PaletteBack(inherit.PaletteBack, needPaint);
            _border = new PaletteBorder(inherit.PaletteBorder, needPaint);
		}

		/// <summary>
        /// Initialize a new instance of the PaletteDouble class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="back">Reference to back storage.</param>
        /// <param name="border">Reference to border storage.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDouble(IPaletteDouble inherit,
                             PaletteBack back,
                             PaletteBorder border,
                             NeedPaintHandler needPaint)
		{
            // Store the provided references
            NeedPaint = needPaint;
            _back = back;
            _border = border;
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

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _back.PopulateFromBase(state);
            _border.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteDouble inherit)
        {
            _back.SetInherit(inherit.PaletteBack);
            _border.SetInherit(inherit.PaletteBorder);
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
