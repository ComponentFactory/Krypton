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
	/// Implement redirected storage for other navigator appearance states.
	/// </summary>
    public class PaletteNavigatorOtherRedirect : Storage
    {
        #region Instance Fields
        private PaletteTripleRedirect _paletteCheckButton;
        private PaletteTripleRedirect _paletteOverflowButton;
        private PaletteTripleRedirect _paletteMiniButton;
        private PaletteTabTripleRedirect _paletteTab;
        private PaletteRibbonTabContentRedirect _paletteRibbonTab;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOtherRedirect class.
		/// </summary>
        /// <param name="redirectCheckButton">Inheritence redirection instance for the check button.</param>
        /// <param name="redirectOverflowButton">Inheritence redirection instance for the outlook overflow button.</param>
        /// <param name="redirectMiniButton">Inheritence redirection instance for the outlook mini button.</param>
        /// <param name="redirectTab">Inheritence redirection instance for the tab.</param>
        /// <param name="redirectRibbonTab">Inheritence redirection instance for the ribbon tab.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorOtherRedirect(PaletteRedirect redirectCheckButton,
                                             PaletteRedirect redirectOverflowButton,
                                             PaletteRedirect redirectMiniButton,
                                             PaletteRedirect redirectTab,
                                             PaletteRedirect redirectRibbonTab,
                                             NeedPaintHandler needPaint) 
		{
            // Create the palette storage
            _paletteCheckButton = new PaletteTripleRedirect(redirectCheckButton, 
                                                            PaletteBackStyle.ButtonStandalone,
                                                            PaletteBorderStyle.ButtonStandalone,
                                                            PaletteContentStyle.ButtonStandalone,
                                                            needPaint);

            _paletteOverflowButton = new PaletteTripleRedirect(redirectCheckButton,
                                                               PaletteBackStyle.ButtonNavigatorOverflow,
                                                               PaletteBorderStyle.ButtonNavigatorOverflow,
                                                               PaletteContentStyle.ButtonNavigatorOverflow,
                                                               needPaint);

            _paletteMiniButton = new PaletteTripleRedirect(redirectMiniButton,
                                                            PaletteBackStyle.ButtonNavigatorMini,
                                                            PaletteBorderStyle.ButtonNavigatorMini,
                                                            PaletteContentStyle.ButtonNavigatorMini,
                                                            needPaint);

            _paletteTab = new PaletteTabTripleRedirect(redirectTab,
                                                       PaletteBackStyle.TabHighProfile,
                                                       PaletteBorderStyle.TabHighProfile,
                                                       PaletteContentStyle.TabHighProfile,
                                                       needPaint);

            _paletteRibbonTab = new PaletteRibbonTabContentRedirect(redirectRibbonTab, needPaint);
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
                return (_paletteCheckButton.IsDefault &&
                        _paletteOverflowButton.IsDefault &&
                        _paletteMiniButton.IsDefault &&
                        _paletteRibbonTab.IsDefault &&
                        _paletteTab.IsDefault);
            }
		}
		#endregion

        #region CheckButton
        /// <summary>
        /// Gets access to the check button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining check button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect CheckButton
        {
            get { return _paletteCheckButton; }
        }

        private bool ShouldSerializeCheckButton()
        {
            return !_paletteCheckButton.IsDefault;
        }
        #endregion

        #region OverflowButton
        /// <summary>
        /// Gets access to the outlook overflow button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook overflow button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverflowButton
        {
            get { return _paletteOverflowButton; }
        }

        private bool ShouldSerializeOverflowButton()
        {
            return !_paletteOverflowButton.IsDefault;
        }
        #endregion

        #region MiniButton
        /// <summary>
        /// Gets access to the outlook mini button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook mini button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect MiniButton
        {
            get { return _paletteMiniButton; }
        }

        private bool ShouldSerializeMiniButton()
        {
            return !_paletteMiniButton.IsDefault;
        }
        #endregion

        #region Tab
        /// <summary>
        /// Gets access to the tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTripleRedirect Tab
        {
            get { return _paletteTab; }
        }

        private bool ShouldSerializeTab()
        {
            return !_paletteTab.IsDefault;
        }
        #endregion

        #region RibbonTab
        /// <summary>
        /// Gets access to the ribbon tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonTabContentRedirect RibbonTab
        {
            get { return _paletteRibbonTab; }
        }

        private bool ShouldSerializeRibbonTab()
        {
            return !_paletteRibbonTab.IsDefault;
        }
        #endregion
    }
}
