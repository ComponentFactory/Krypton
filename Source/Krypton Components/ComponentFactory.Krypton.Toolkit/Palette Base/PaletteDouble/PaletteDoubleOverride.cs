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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Allow a palette to be overriden optionally.
	/// </summary>
    public class PaletteDoubleOverride : GlobalId,
                                         IPaletteDouble
	{
		#region Intance Fields
		private PaletteBackInheritOverride _overrideBack;
		private PaletteBorderInheritOverride _overrideBorder;
		#endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDoubleOverride class.
		/// </summary>
		/// <param name="normalTriple">Normal palette to use.</param>
        /// <param name="overrideTriple">Override palette to use.</param>
		/// <param name="overrideState">State used by the override.</param>
        public PaletteDoubleOverride(IPaletteDouble normalTriple,
									 IPaletteDouble overrideTriple,
									 PaletteState overrideState)
		{
            Debug.Assert(normalTriple != null);
            Debug.Assert(overrideTriple != null);

            if (normalTriple == null) throw new ArgumentNullException("normalTriple");
            if (overrideTriple == null) throw new ArgumentNullException("overrideTriple");

            // Create the triple override instances
            _overrideBack = new PaletteBackInheritOverride(normalTriple.PaletteBack, overrideTriple.PaletteBack);
            _overrideBorder = new PaletteBorderInheritOverride(normalTriple.PaletteBorder, overrideTriple.PaletteBorder);

            // Do not apply an override by default
            Apply = false;

            // Always override the state
            Override = true;
            OverrideState = overrideState;
		}
		#endregion

		#region Apply
		/// <summary>
		/// Gets and sets a value indicating if override should be applied.
		/// </summary>
		public bool Apply
		{
			get { return _overrideBack.Apply; }

			set
			{
				_overrideBack.Apply = value;
				_overrideBorder.Apply = value;
			}
		}
		#endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get { return _overrideBack.Override; }

            set
            {
                _overrideBack.Override = value;
                _overrideBorder.Override = value;
            }
        }
        #endregion

        #region OverrideState
        /// <summary>
		/// Gets and sets the override palette state to use.
		/// </summary>
		public PaletteState OverrideState
		{
			get { return _overrideBack.OverrideState; }

			set
			{
				_overrideBack.OverrideState = value;
				_overrideBorder.OverrideState = value;
			}
		}
		#endregion

		#region Palette Accessors
		/// <summary>
		/// Gets the background palette.
		/// </summary>
		public IPaletteBack PaletteBack
		{
			get { return _overrideBack; }
		}

		/// <summary>
		/// Gets the border palette.
		/// </summary>
		public IPaletteBorder PaletteBorder
		{
			get { return _overrideBorder; }
		}
		#endregion
	}
}
