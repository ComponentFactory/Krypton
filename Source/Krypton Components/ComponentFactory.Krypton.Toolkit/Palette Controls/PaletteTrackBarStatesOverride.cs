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
	/// Allow the palette to be overriden optionally.
	/// </summary>
    public class PaletteTrackBarStatesOverride : GlobalId
	{
		#region Instance Fields
        private PaletteBack _back;
        private PaletteElementColorInheritOverride _overrideTickState;
        private PaletteElementColorInheritOverride _overrideTrackState;
        private PaletteElementColorInheritOverride _overridePositionState;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarStatesOverride class.
		/// </summary>
        /// <param name="normalStates">Normal state values.</param>
        /// <param name="overrideStates">Override state values.</param>
        /// <param name="overrideState">State to override.</param>
        public PaletteTrackBarStatesOverride(PaletteTrackBarRedirect normalStates,
                                             PaletteTrackBarStates overrideStates,
                                             PaletteState overrideState)
        {
            Debug.Assert(normalStates != null);
            Debug.Assert(overrideStates != null);

            // Validate incoming references
            if (normalStates == null) throw new ArgumentNullException("normalStates");
            if (overrideStates == null) throw new ArgumentNullException("overrideStates");

            // Create the triple override instances
            _back = normalStates.Back;
            _overrideTickState = new PaletteElementColorInheritOverride(normalStates.Tick, overrideStates.Tick);
            _overrideTrackState = new PaletteElementColorInheritOverride(normalStates.Track, overrideStates.Track);
            _overridePositionState = new PaletteElementColorInheritOverride(normalStates.Position, overrideStates.Position);

            // Do not apply an override by default
            Apply = false;

            // Always override the state
            Override = true;
            OverrideState = overrideState;
        }
        #endregion

        #region SetPalettes
        /// <summary>
        /// Update the the normal and override palettes.
        /// </summary>
        /// <param name="normalStates">New normal palette.</param>
        /// <param name="overrideStates">New override palette.</param>
        public void SetPalettes(PaletteTrackBarRedirect normalStates,
                                PaletteTrackBarStates overrideStates)
        {
            _overrideTickState.SetPalettes(normalStates.Tick, overrideStates.Tick);
            _overrideTrackState.SetPalettes(normalStates.Track, overrideStates.Track);
            _overridePositionState.SetPalettes(normalStates.Position, overrideStates.Position);
        }
        #endregion

        #region Apply
        /// <summary>
		/// Gets and sets a value indicating if override should be applied.
		/// </summary>
		public bool Apply
		{
            get { return _overrideTickState.Apply; }

			set
			{
                _overrideTickState.Apply = value;
                _overrideTrackState.Apply = value;
                _overridePositionState.Apply = value;
            }
		}
		#endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get { return _overrideTickState.Override; }

            set
            {
                _overrideTickState.Override = value;
                _overrideTrackState.Override = value;
                _overridePositionState.Override = value;
            }
        }
        #endregion

		#region OverrideState
		/// <summary>
		/// Gets and sets the override palette state to use.
		/// </summary>
		public PaletteState OverrideState
		{
            get { return _overrideTickState.OverrideState; }

            set
            {
                _overrideTickState.OverrideState = value;
                _overrideTrackState.OverrideState = value;
                _overridePositionState.OverrideState = value;
            }
		}
		#endregion

        #region Back
        /// <summary>
        /// Gets access to the back appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack Back
        {
            get { return _back; }
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
        public PaletteElementColorInheritOverride Tick
        {
            get { return _overrideTickState; }
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
        public PaletteElementColorInheritOverride Track
        {
            get { return _overrideTrackState; }
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
        public PaletteElementColorInheritOverride Position
        {
            get { return _overridePositionState; }
        }
        #endregion
    }
}
