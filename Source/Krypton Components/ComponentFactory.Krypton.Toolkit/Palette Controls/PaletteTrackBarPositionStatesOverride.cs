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
    public class PaletteTrackBarPositionStatesOverride : GlobalId
	{
		#region Instance Fields
        private PaletteElementColorInheritOverride _overridePositionState;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarPositionStatesOverride class.
		/// </summary>
        /// <param name="normalStates">Normal state values.</param>
        /// <param name="overrideStates">Override state values.</param>
        /// <param name="overrideState">State to override.</param>
        public PaletteTrackBarPositionStatesOverride(PaletteTrackBarRedirect normalStates,
                                                     PaletteTrackBarPositionStates overrideStates,
                                                     PaletteState overrideState)
        {
            Debug.Assert(normalStates != null);
            Debug.Assert(overrideStates != null);

            // Validate incoming references
            if (normalStates == null) throw new ArgumentNullException("normalStates");
            if (overrideStates == null) throw new ArgumentNullException("overrideStates");

            // Create the override instance
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
                                PaletteTrackBarPositionStates overrideStates)
        {
            _overridePositionState.SetPalettes(normalStates.Position, overrideStates.Position);
        }
        #endregion

        #region Apply
        /// <summary>
		/// Gets and sets a value indicating if override should be applied.
		/// </summary>
		public bool Apply
		{
            get { return _overridePositionState.Apply; }
			set { _overridePositionState.Apply = value; }
		}
		#endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get { return _overridePositionState.Override; }
            set { _overridePositionState.Override = value; }
        }
        #endregion

		#region OverrideState
		/// <summary>
		/// Gets and sets the override palette state to use.
		/// </summary>
		public PaletteState OverrideState
		{
            get { return _overridePositionState.OverrideState; }
            set { _overridePositionState.OverrideState = value; }
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
