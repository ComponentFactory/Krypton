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
	/// Implement storage for other navigator appearance states.
	/// </summary>
    public class PaletteNavigatorOther : Storage
    {
        #region Instance Fields
        private PaletteTriple _paletteCheckButton;
        private PaletteTriple _paletteOverflowButton;
        private PaletteTriple _paletteMiniButton;
        private PaletteTabTriple _paletteTab;
        private PaletteRibbonTabContent _paletteRibbonTab;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOther class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorOther(PaletteNavigatorRedirect redirect,
                                     NeedPaintHandler needPaint) 
		{
            // Create the palette storage
            _paletteCheckButton = new PaletteTriple(redirect.CheckButton, needPaint);
            _paletteOverflowButton = new PaletteTriple(redirect.OverflowButton, needPaint);
            _paletteMiniButton = new PaletteTriple(redirect.MiniButton, needPaint);
            _paletteTab = new PaletteTabTriple(redirect.Tab, needPaint);
            _paletteRibbonTab = new PaletteRibbonTabContent(redirect.RibbonTab.TabDraw, redirect.RibbonTab.TabDraw, redirect.RibbonTab.Content, needPaint);
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
                        _paletteTab.IsDefault &&
                        _paletteRibbonTab.IsDefault);
            }
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritNavigator">Source for inheriting.</param>
        public virtual void SetInherit(PaletteNavigator inheritNavigator)
        {
            _paletteCheckButton.SetInherit(inheritNavigator.CheckButton);
            _paletteOverflowButton.SetInherit(inheritNavigator.OverflowButton);
            _paletteMiniButton.SetInherit(inheritNavigator.MiniButton);
            _paletteTab.SetInherit(inheritNavigator.Tab);
            _paletteRibbonTab.SetInherit(inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.Content);
        }
        #endregion

        #region CheckButton
        /// <summary>
        /// Gets access to the check button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining check button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple CheckButton
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
        public PaletteTriple OverflowButton
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
        public PaletteTriple MiniButton
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
        public PaletteTabTriple Tab
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
        public PaletteRibbonTabContent RibbonTab
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
