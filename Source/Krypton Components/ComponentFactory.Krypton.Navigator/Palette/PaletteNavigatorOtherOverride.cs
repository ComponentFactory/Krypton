// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Allow the palette to be overriden optionally.
	/// </summary>
    public class PaletteNavigatorOverride
    {
        #region Instance Fields
        private PaletteTripleOverride _overrideCheckButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOverride class.
		/// </summary>
        /// <param name="normalOther">Normal palette to use.</param>
        /// <param name="overrideOther">Override palette to use.</param>
        /// <param name="overrideState">State used by the override.</param>
        public PaletteNavigatorOverride(PaletteNavigatorOtherRedirect normalOther,
                                             PaletteNavigatorOther overrideOther,
                                             PaletteState overrideState)
            : this(normalOther.CheckButton, overrideOther.CheckButton, overrideState)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOverride class.
        /// </summary>
        /// <param name="normalOther">Normal palette to use.</param>
        /// <param name="overrideOther">Override palette to use.</param>
        /// <param name="overrideState">State used by the override.</param>
        public PaletteNavigatorOverride(PaletteNavigatorOtherRedirect normalOther,
                                        PaletteNavigator overrideOther,
                                        PaletteState overrideState)
            : this(normalOther.CheckButton, overrideOther.CheckButton, overrideState)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOverride class.
		/// </summary>
        /// <param name="checkButtonNormal">Normal palette to use.</param>
        /// <param name="checkButtonOther">Override palette to use.</param>
        /// <param name="overrideState">State used by the override.</param>
        public PaletteNavigatorOverride(IPaletteTriple checkButtonNormal,
                                        IPaletteTriple checkButtonOther,
                                        PaletteState overrideState) 
		{
            Debug.Assert(checkButtonNormal != null);
            Debug.Assert(checkButtonOther != null);

            // Create the palette storage
            _overrideCheckButton = new PaletteTripleOverride(checkButtonNormal, 
                                                             checkButtonOther, 
                                                             overrideState);

            // Do not apply an override by default
            Apply = false;
        }
        #endregion

        #region Apply
        /// <summary>
        /// Gets and sets a value indicating if override should be applied.
        /// </summary>
        public bool Apply
        {
            get { return _overrideCheckButton.Apply; }
            set { _overrideCheckButton.Apply = value; }
        }
        #endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get { return _overrideCheckButton.Override; }
            set { _overrideCheckButton.Override = value; }
        }
        #endregion

        #region OverrideState
        /// <summary>
        /// Gets and sets the override palette state to use.
        /// </summary>
        public PaletteState OverrideState
        {
            get { return _overrideCheckButton.OverrideState; }
            set { _overrideCheckButton.OverrideState = value; }
        }
        #endregion

        #region Palette Accessors
        /// <summary>
        /// Gets access to the check button palette.
        /// </summary>
        public PaletteTripleOverride CheckButton
        {
            get { return _overrideCheckButton; }
        }
        #endregion
    }
}
