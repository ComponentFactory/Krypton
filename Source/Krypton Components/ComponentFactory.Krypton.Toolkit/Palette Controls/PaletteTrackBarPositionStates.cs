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
	/// Implement storage for a track bar position only states.
	/// </summary>
	public class PaletteTrackBarPositionStates : Storage
	{
		#region Instance Fields
        private PaletteElementColor _positionState;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarPositionStates class.
		/// </summary>
        /// <param name="redirect">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTrackBarPositionStates(PaletteTrackBarRedirect redirect,
                                             NeedPaintHandler needPaint)
            : this(redirect.Position, needPaint)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarPositionStates class.
		/// </summary>
        /// <param name="inheritPosition">Source for inheriting position values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTrackBarPositionStates(IPaletteElementColor inheritPosition,
                                             NeedPaintHandler needPaint)
		{
            Debug.Assert(inheritPosition != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _positionState = new PaletteElementColor(inheritPosition, needPaint);
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
                return Position.IsDefault;
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritPosition">Source for inheriting position values.</param>
        public void SetInherit(IPaletteElementColor inheritPosition)
        {
            _positionState.SetInherit(inheritPosition);
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _positionState.PopulateFromBase(state);
        }
        #endregion

        #region Position
        /// <summary>
        /// Gets access to the position appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining position appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteElementColor Position
        {
            get { return _positionState; }
        }

        private bool ShouldSerializePosition()
        {
            return !_positionState.IsDefault;
        }
        #endregion
	}
}
