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
    /// Colors associated with menus and tool strip.
    /// </summary>
    public class KryptonPaletteTMS : Storage
    {
        #region Instance Fields
        private KryptonInternalKCT _internalKCT;
        private KryptonPaletteTMSButton _paletteButton;
        private KryptonPaletteTMSGrip _paletteGrip;
        private KryptonPaletteTMSMenu _paletteMenu;
        private KryptonPaletteTMSMenuStrip _paletteMenuStrip;
        private KryptonPaletteTMSRafting _paletteRafting;
        private KryptonPaletteTMSSeparator _paletteSeparator;
        private KryptonPaletteTMSStatusStrip _paletteStatusStrip;
        private KryptonPaletteTMSToolStrip _paletteToolStrip;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCT class.
        /// </summary>
        /// <param name="palette">Associated palettte instance.</param>
        /// <param name="baseKCT">Initial base KCT to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMS(IPalette palette,
                                   KryptonColorTable baseKCT,
                                   NeedPaintHandler needPaint)
        {
            Debug.Assert(baseKCT != null);

            // Create actual KCT for storage
            _internalKCT = new KryptonInternalKCT(baseKCT, palette);

            // Create the set of sub objects that expose the palette properties
            _paletteButton = new KryptonPaletteTMSButton(_internalKCT, needPaint);
            _paletteGrip = new KryptonPaletteTMSGrip(_internalKCT, needPaint);
            _paletteMenu = new KryptonPaletteTMSMenu(_internalKCT, needPaint);
            _paletteMenuStrip = new KryptonPaletteTMSMenuStrip(_internalKCT, needPaint);
            _paletteRafting = new KryptonPaletteTMSRafting(_internalKCT, needPaint);
            _paletteSeparator = new KryptonPaletteTMSSeparator(_internalKCT, needPaint);
            _paletteStatusStrip = new KryptonPaletteTMSStatusStrip(_internalKCT, needPaint);
            _paletteToolStrip = new KryptonPaletteTMSToolStrip(_internalKCT, needPaint);
        }

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault
        {
            get
            {
                return _internalKCT.IsDefault &&
                       _paletteButton.IsDefault &&
                       _paletteGrip.IsDefault &&
                       _paletteMenu.IsDefault &&
                       _paletteRafting.IsDefault &&
                       _paletteMenuStrip.IsDefault &&
                       _paletteSeparator.IsDefault &&
                       _paletteStatusStrip.IsDefault &&
                       _paletteToolStrip.IsDefault;
            }
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            Button.PopulateFromBase();
            Grip.PopulateFromBase();
            Menu.PopulateFromBase();
            Rafting.PopulateFromBase();
            MenuStrip.PopulateFromBase();
            Separator.PopulateFromBase();
            StatusStrip.PopulateFromBase();
            ToolStrip.PopulateFromBase();
            UseRoundedEdges = InternalKCT.UseRoundedEdges;
        }
        #endregion

        #region Button
        /// <summary>
        /// Get access to the button colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("Button specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSButton Button
        {
            get { return _paletteButton; }
        }

        private bool ShouldSerializeButton()
        {
            return !_paletteButton.IsDefault;
        }
        #endregion

        #region Grip
        /// <summary>
        /// Get access to the grip colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("Grip specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSGrip Grip
        {
            get { return _paletteGrip; }
        }

        private bool ShouldSerializeGrip()
        {
            return !_paletteGrip.IsDefault;
        }
        #endregion

        #region Menu
        /// <summary>
        /// Get access to the menu colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("Menu specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSMenu Menu
        {
            get { return _paletteMenu; }
        }

        private bool ShouldSerializeMenu()
        {
            return !_paletteMenu.IsDefault;
        }
        #endregion

        #region Rafting
        /// <summary>
        /// Get access to the rafting colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("Rafting specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSRafting Rafting
        {
            get { return _paletteRafting; }
        }

        private bool ShouldSerializeRafting()
        {
            return !_paletteRafting.IsDefault;
        }
        #endregion

        #region MenuStrip
        /// <summary>
        /// Get access to the menu strip colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("MenuStrip specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSMenuStrip MenuStrip
        {
            get { return _paletteMenuStrip; }
        }

        private bool ShouldSerializeMenuStrip()
        {
            return !_paletteMenuStrip.IsDefault;
        }
        #endregion

        #region Separator
        /// <summary>
        /// Get access to the separator colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("Separator specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSSeparator Separator
        {
            get { return _paletteSeparator; }
        }

        private bool ShouldSerializeSeparator()
        {
            return !_paletteSeparator.IsDefault;
        }
        #endregion

        #region StatusStrip
        /// <summary>
        /// Get access to the status strip colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("StatusStrip specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSStatusStrip StatusStrip
        {
            get { return _paletteStatusStrip; }
        }

        private bool ShouldSerializeStatusStrip()
        {
            return !_paletteStatusStrip.IsDefault;
        }
        #endregion

        #region ToolStrip
        /// <summary>
        /// Get access to the tool strip colors.
        /// </summary>
        [KryptonPersist]
        [Category("ToolMenuStatus")]
        [Description("ToolStrip specific colors.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTMSToolStrip ToolStrip
        {
            get { return _paletteToolStrip; }
        }

        private bool ShouldSerializeToolStrip()
        {
            return !_paletteToolStrip.IsDefault;
        }
        #endregion

        #region UseRoundedEdges
        /// <summary>
        /// Gets and sets the use of rounded or square edges when rendering.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Should rendering use rounded or square edges.")]
        [DefaultValue(typeof(InheritBool), "Inherit")]
        public InheritBool UseRoundedEdges
        {
            get { return InternalKCT.InternalUseRoundedEdges; }

            set
            {
                InternalKCT.InternalUseRoundedEdges = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the UseRoundedEdges property to its default value.
        /// </summary>
        public void ResetUseRoundedEdges()
        {
            UseRoundedEdges = InheritBool.Inherit;
        }
        #endregion

        #region Internal
        internal KryptonColorTable BaseKCT
        {
            get { return InternalKCT.BaseKCT; }
            set { InternalKCT.BaseKCT = value; }
        }

        internal KryptonInternalKCT InternalKCT
        {
            get { return _internalKCT; }
        }
        #endregion
    }
}
