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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Storage for common palette settings.
    /// </summary>
    public class KryptonPaletteCommon : Storage
    {
        #region Instance Fields
        private PaletteTripleRedirect _stateCommon;
        private PaletteTriple _stateDisabled;
        private PaletteTriple _stateOthers;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteCommon class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteCommon(PaletteRedirect redirector,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the common palettes
            _stateCommon = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            _stateDisabled = new PaletteTriple(_stateCommon, needPaint);
            _stateOthers = new PaletteTriple(_stateCommon, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault
        {
            get
            {
                return _stateCommon.IsDefault &&
                       _stateDisabled.IsDefault &&
                       _stateOthers.IsDefault;
            }
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the all appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the all appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        #endregion

        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the disabled appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }
        #endregion

        #region StateOthers
        /// <summary>
        /// Gets access to the non-disabled appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the non-disabled appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateOthers
        {
            get { return _stateOthers; }
        }

        private bool ShouldSerializeStateOthers()
        {
            return !_stateOthers.IsDefault;
        }
        #endregion
    }
}
