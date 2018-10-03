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
	/// Implement storage for a track bar state.
	/// </summary>
	public class PaletteTrackBarStates : Storage
	{
		#region Instance Fields
        private PaletteElementColor _tickState;
        private PaletteElementColor _trackState;
        private PaletteElementColor _positionState;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarStates class.
		/// </summary>
        /// <param name="redirect">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTrackBarStates(PaletteTrackBarRedirect redirect,
                                     NeedPaintHandler needPaint)
            : this(redirect.Tick, redirect.Track, redirect.Position, needPaint)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarStates class.
		/// </summary>
        /// <param name="inheritTick">Source for inheriting tick values.</param>
        /// <param name="inheritTrack">Source for inheriting track values.</param>
        /// <param name="inheritPosition">Source for inheriting position values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTrackBarStates(IPaletteElementColor inheritTick,
                                     IPaletteElementColor inheritTrack,
                                     IPaletteElementColor inheritPosition,
                                     NeedPaintHandler needPaint)
		{
            Debug.Assert(inheritTick != null);
            Debug.Assert(inheritTrack != null);
            Debug.Assert(inheritPosition != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _tickState = new PaletteElementColor(inheritTick, needPaint);
            _trackState = new PaletteElementColor(inheritTrack, needPaint);
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
                return (Tick.IsDefault &&
                        Track.IsDefault &&
                        Position.IsDefault);
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritTick">Source for inheriting tick values.</param>
        /// <param name="inheritTrack">Source for inheriting track values.</param>
        /// <param name="inheritPosition">Source for inheriting position values.</param>
        public void SetInherit(IPaletteElementColor inheritTick,
                               IPaletteElementColor inheritTrack,
                               IPaletteElementColor inheritPosition)
        {
            _tickState.SetInherit(inheritTick);
            _trackState.SetInherit(inheritTrack);
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
            _tickState.PopulateFromBase(state);
            _trackState.PopulateFromBase(state);
            _positionState.PopulateFromBase(state);
        }
        #endregion

        #region Tick
        /// <summary>
        /// Gets access to the tick appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tick appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteElementColor Tick
        {
            get { return _tickState; }
        }

        private bool ShouldSerializeTick()
        {
            return !_tickState.IsDefault;
        }
        #endregion

        #region Track
        /// <summary>
        /// Gets access to the track appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining track appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteElementColor Track
        {
            get { return _trackState; }
        }

        private bool ShouldSerializeTrack()
        {
            return !_trackState.IsDefault;
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
