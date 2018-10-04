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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Exposes global settings that affect all the Krypton controls.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonManager), "ToolboxBitmaps.KryptonManager.bmp")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonManagerDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DefaultProperty("GlobalPaletteMode")]
    [Description("Access global Krypton settings.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public sealed class KryptonManager : Component
    {
        #region Static Fields
        // Initialize the global state
        private static bool _globalApplyToolstrips = true;
        private static bool _globalAllowFormChrome = true;
        private static GlobalStrings _globalStrings = new GlobalStrings();

        // Initialize the default modes
        private static PaletteModeManager _globalPaletteMode = PaletteModeManager.Office2010Blue;

        // Initialize instances to match the default modes
        private static IPalette _globalPalette = CurrentGlobalPalette;

        // Singleton instances are created on demand
        private static PaletteProfessionalOffice2003 _paletteProfessionalOffice2003;
        private static PaletteProfessionalSystem _paletteProfessionalSystem;
        private static PaletteOffice2007Blue _paletteOffice2007Blue;
        private static PaletteOffice2007Silver _paletteOffice2007Silver;
        private static PaletteOffice2007Black _paletteOffice2007Black;
        private static PaletteOffice2010Blue _paletteOffice2010Blue;
        private static PaletteOffice2010Silver _paletteOffice2010Silver;
        private static PaletteOffice2010Black _paletteOffice2010Black;
        private static PaletteSparkleBlue _paletteSparkleBlue;
        private static PaletteSparkleOrange _paletteSparkleOrange;
        private static PaletteSparklePurple _paletteSparklePurple;
        private static RenderStandard _renderStandard;
        private static RenderProfessional _renderProfessional;
        private static RenderOffice2007 _renderOffice2007;
        private static RenderOffice2010 _renderOffice2010;
        private static RenderSparkle _renderSparkle;
        #endregion

        #region Static Events
        /// <summary>
        /// Occurs when the palette changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the GlobalPalette property is changed.")]
        public static event EventHandler GlobalPaletteChanged;

        /// <summary>
        /// Occurs when the AllowFormChrome property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the GlobalAllowFormChrome property is changed.")]
        public static event EventHandler GlobalAllowFormChromeChanged;
        #endregion

        #region Identity
        static KryptonManager()
        {
            // We need to notice when system color settings change
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);

            // Update the tool strip global renderer with the default setting
            UpdateToolStripManager();
        }

        /// <summary>
        /// Initialize a new instance of the KryptonManager class.
        /// </summary>
        public KryptonManager()
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonManager class.
        /// </summary>
        /// <param name="container">Container that owns the component.</param>
        public KryptonManager(IContainer container)
            : this()
        {
            Debug.Assert(container != null);
            
            // Validate reference parameter
            if (container == null) throw new ArgumentNullException("container");

            container.Add(this);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the global palette used for drawing.
        /// </summary>
        [Category("Visuals")]
        [Description("Global palette applied to drawing.")]
        [DefaultValue(typeof(PaletteModeManager), "Office2010Blue")]
        public PaletteModeManager GlobalPaletteMode
        {
            get { return _globalPaletteMode; }
            
            set 
            {
                // Only interested in changes of value
                if (_globalPaletteMode != value)
                {
                    // Action depends on the value
                    switch (value)
                    {
                        case PaletteModeManager.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'GlobalPalette' property in order to get the custom mode
                            break;
                        default:
                            // Cache the new valus
                            PaletteModeManager tempMode = _globalPaletteMode;
                            IPalette tempPalette = _globalPalette;

                            // Use the new value
                            _globalPaletteMode = value;
                            _globalPalette = null;

                            // If the new value creates a circular reference
                            if (HasCircularReference())
                            {
                                // Restore the original values before throwing
                                _globalPaletteMode = tempMode;
                                _globalPalette = tempPalette;

                                throw new ArgumentOutOfRangeException("value", "Cannot use palette that would create a circular reference");
                            }
                            else
                            {
                                // Restore the original global pallete as 'SetPalette' will not 
                                // work correctly unles it still has the old value in place
                                _globalPalette = tempPalette;
                            }

                            // Get a reference to the standard palette from its name
                            SetPalette(CurrentGlobalPalette);

                            // Raise the palette changed event
                            OnGlobalPaletteChanged(EventArgs.Empty);
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializeGlobalPaletteMode()
        {
            return (GlobalPaletteMode != PaletteModeManager.Office2010Blue);
        }

        /// <summary>
        /// Resets the GlobalPaletteMode property to its default value.
        /// </summary>
        public void ResetGlobalPaletteMode()
        {
            GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        /// <summary>
        /// Gets and sets the global custom applied to drawing.
        /// </summary>
        [Category("Visuals")]
        [Description("Global custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette GlobalPalette
        {
            get { return _globalPalette; }

            set
            {
                // Only interested in changes of value
                if (_globalPalette != value)
                {
                    // Cache the current values
                    PaletteModeManager tempMode = _globalPaletteMode;
                    IPalette tempPalette = _globalPalette;

                    // Use the new values
                    _globalPaletteMode = (value == null) ? PaletteModeManager.Office2010Blue : PaletteModeManager.Custom;
                    _globalPalette = value;

                    // If the new value creates a circular reference
                    if (HasCircularReference())
                    {
                        // Restore the original values
                        _globalPaletteMode = tempMode;
                        _globalPalette = tempPalette;

                        throw new ArgumentOutOfRangeException("value", "Cannot use palette that would create a circular reference");
                    }
                    else
                    {
                        // Restore the original global pallete as 'SetPalette' will not 
                        // work correctly unles it still has the old value in place
                        _globalPalette = tempPalette;
                    }

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // Get a reference to current global palette defined by the mode
                        SetPalette(CurrentGlobalPalette);
                    }
                    else
                    {
                        // No longer using a standard palette
                        _globalPaletteMode = PaletteModeManager.Custom;
                    }

                    // Raise the palette changed event
                    OnGlobalPaletteChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the GlobalPalette property to its default value.
        /// </summary>
        public void ResetGlobalPalette()
        {
            GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }
        
        /// <summary>
        /// Gets or sets a value indicating if the palette colors are applied to the toolstrips.
        /// </summary>
        [Category("Visuals")]
        [Description("Should the palette colors be applied to the toolstrips.")]
        [DefaultValue(true)]
        public bool GlobalApplyToolstrips
        {
            get { return KryptonManager.ApplyToolstrips; }
            set { KryptonManager.ApplyToolstrips = value; }
        }

        private bool ShouldSerializeGlobalApplyToolstrips()
        {
            return !GlobalApplyToolstrips;
        }

        /// <summary>
        /// Resets the GlobalApplyToolstrips property to its default value.
        /// </summary>
        public void ResetGlobalApplyToolstrips()
        {
            GlobalApplyToolstrips = true;
        }

        /// <summary>
        /// Gets or sets a value indicating if KryptonForm instances are allowed to show custom chrome.
        /// </summary>
        [Category("Visuals")]
        [Description("Should KryptonForm instances be allowed to show custom chrome.")]
        [DefaultValue(true)]
        public bool GlobalAllowFormChrome
        {
            get { return KryptonManager.AllowFormChrome; }
            set { KryptonManager.AllowFormChrome = value; }
        }

        private bool ShouldSerializeGlobalAllowFormChrome()
        {
            return !GlobalAllowFormChrome;
        }

        /// <summary>
        /// Resets the GlobalAllowFormChrome property to its default value.
        /// </summary>
        public void ResetGlobalAllowFormChrome()
        {
            GlobalAllowFormChrome = true;
        }

        /// <summary>
        /// Gets a set of global strings used by Krypton that can be localized.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of global strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public GlobalStrings GlobalStrings
        {
            get { return _globalStrings; }
        }

        private bool ShouldSerializeGlobalStrings()
        {
            return !_globalStrings.IsDefault;
        }

        /// <summary>
        /// Resets the GlobalStrings property to its default value.
        /// </summary>
        public void ResetGlobalStrings()
        {
            _globalStrings.Reset();
        }
        #endregion

        #region Static ApplyToolstrips
        /// <summary>
        /// Gets and sets the global flag that decides if palette colors are applied to toolstrips.
        /// </summary>
        public static bool ApplyToolstrips
        {
            get { return _globalApplyToolstrips; }

            set
            {
                // Only interested if the value changes
                if (_globalApplyToolstrips != value)
                {
                    // Use new value
                    _globalApplyToolstrips = value;

                    // Change the toolstrip manager renderer as required
                    if (_globalApplyToolstrips)
                        UpdateToolStripManager();
                    else
                        ResetToolStripManager();
                }
            }
        }
        #endregion

        #region Static AllowFormChrome
        /// <summary>
        /// Gets and sets the global flag that decides if form chrom should be customized.
        /// </summary>
        public static bool AllowFormChrome
        {
            get { return _globalAllowFormChrome; }

            set
            {
                // Only interested if the value changes
                if (_globalAllowFormChrome != value)
                {
                    // Use new value
                    _globalAllowFormChrome = value;
             
                    // Fire change event
                    OnGlobalAllowFormChromeChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Static Strings
        /// <summary>
        /// Gets access to the set of global strings.
        /// </summary>
        public static GlobalStrings Strings
        {
            get { return _globalStrings; }
        }
        #endregion

        #region Static Palette
        /// <summary>
        /// Gets the current global palette instance given the manager settings.
        /// </summary>
        public static IPalette CurrentGlobalPalette
        {
            get 
            {
                switch (_globalPaletteMode)
                {
                    case PaletteModeManager.ProfessionalSystem:
                        return PaletteProfessionalSystem;
                    case PaletteModeManager.ProfessionalOffice2003:
                        return PaletteProfessionalOffice2003;
                    case PaletteModeManager.Office2007Blue:
                        return PaletteOffice2007Blue;
                    case PaletteModeManager.Office2007Silver:
                        return PaletteOffice2007Silver;
                    case PaletteModeManager.Office2007Black:
                        return PaletteOffice2007Black;
                    case PaletteModeManager.Office2010Blue:
                        return PaletteOffice2010Blue;
                    case PaletteModeManager.Office2010Silver:
                        return PaletteOffice2010Silver;
                    case PaletteModeManager.Office2010Black:
                        return PaletteOffice2010Black;
                    case PaletteModeManager.SparkleBlue:
                        return PaletteSparkleBlue;
                    case PaletteModeManager.SparkleOrange:
                        return PaletteSparkleOrange;
                    case PaletteModeManager.SparklePurple:
                        return PaletteSparklePurple;
                    case PaletteModeManager.Custom:
                        return _globalPalette;
                    default:
                        Debug.Assert(false);
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets the implementation for the requested palette mode.
        /// </summary>
        /// <param name="mode">Requested palette mode.</param>
        /// <returns>IPalette reference is available; otherwise false.</returns>
        public static IPalette GetPaletteForMode(PaletteMode mode)
        {
            switch (mode)
            {
                case PaletteMode.ProfessionalSystem:
                    return PaletteProfessionalSystem;
                case PaletteMode.ProfessionalOffice2003:
                    return PaletteProfessionalOffice2003;
                case PaletteMode.Office2007Blue:
                    return PaletteOffice2007Blue;
                case PaletteMode.Office2007Silver:
                    return PaletteOffice2007Silver;
                case PaletteMode.Office2007Black:
                    return PaletteOffice2007Black;
                case PaletteMode.Office2010Blue:
                    return PaletteOffice2010Blue;
                case PaletteMode.Office2010Silver:
                    return PaletteOffice2010Silver;
                case PaletteMode.Office2010Black:
                    return PaletteOffice2010Black;
                case PaletteMode.SparkleBlue:
                    return PaletteSparkleBlue;
                case PaletteMode.SparkleOrange:
                    return PaletteSparkleOrange;
                case PaletteMode.SparklePurple:
                    return PaletteSparklePurple;
                case PaletteMode.Global:
                    return CurrentGlobalPalette;
                case PaletteMode.Custom:
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the single instance of the professional system palette.
        /// </summary>
        public static PaletteProfessionalSystem PaletteProfessionalSystem
        {
            get
            {
                if (_paletteProfessionalSystem == null)
                    _paletteProfessionalSystem = new PaletteProfessionalSystem();

                return _paletteProfessionalSystem;
            }
        }

        /// <summary>
        /// Gets the single instance of the professional office palette.
        /// </summary>
        public static PaletteProfessionalOffice2003 PaletteProfessionalOffice2003
        {
            get
            {
                if (_paletteProfessionalOffice2003 == null)
                    _paletteProfessionalOffice2003 = new PaletteProfessionalOffice2003();

                return _paletteProfessionalOffice2003;
            }
        }

        /// <summary>
        /// Gets the single instance of the Blue variant Office 2007 palette.
        /// </summary>
        public static PaletteOffice2007Blue PaletteOffice2007Blue
        {
            get
            {
                if (_paletteOffice2007Blue == null)
                    _paletteOffice2007Blue = new PaletteOffice2007Blue();

                return _paletteOffice2007Blue;
            }
        }

        /// <summary>
        /// Gets the single instance of the Silver variant Office 2007 palette.
        /// </summary>
        public static PaletteOffice2007Silver PaletteOffice2007Silver
        {
            get
            {
                if (_paletteOffice2007Silver == null)
                    _paletteOffice2007Silver = new PaletteOffice2007Silver();

                return _paletteOffice2007Silver;
            }
        }

        /// <summary>
        /// Gets the single instance of the Black variant Office 2007 palette.
        /// </summary>
        public static PaletteOffice2007Black PaletteOffice2007Black
        {
            get
            {
                if (_paletteOffice2007Black == null)
                    _paletteOffice2007Black = new PaletteOffice2007Black();

                return _paletteOffice2007Black;
            }
        }

        /// <summary>
        /// Gets the single instance of the Blue variant Office 2010 palette.
        /// </summary>
        public static PaletteOffice2010Blue PaletteOffice2010Blue
        {
            get
            {
                if (_paletteOffice2010Blue == null)
                    _paletteOffice2010Blue = new PaletteOffice2010Blue();

                return _paletteOffice2010Blue;
            }
        }

        /// <summary>
        /// Gets the single instance of the Silver variant Office 2010 palette.
        /// </summary>
        public static PaletteOffice2010Silver PaletteOffice2010Silver
        {
            get
            {
                if (_paletteOffice2010Silver == null)
                    _paletteOffice2010Silver = new PaletteOffice2010Silver();

                return _paletteOffice2010Silver;
            }
        }

        /// <summary>
        /// Gets the single instance of the Black variant Office 2010 palette.
        /// </summary>
        public static PaletteOffice2010Black PaletteOffice2010Black
        {
            get
            {
                if (_paletteOffice2010Black == null)
                    _paletteOffice2010Black = new PaletteOffice2010Black();

                return _paletteOffice2010Black;
            }
        }

        /// <summary>
        /// Gets the single instance of the Blue variant sparkle palette.
        /// </summary>
        public static PaletteSparkleBlue PaletteSparkleBlue
        {
            get
            {
                if (_paletteSparkleBlue == null)
                    _paletteSparkleBlue = new PaletteSparkleBlue();

                return _paletteSparkleBlue;
            }
        }

        /// <summary>
        /// Gets the single instance of the Orange variant sparkle palette.
        /// </summary>
        public static PaletteSparkleOrange PaletteSparkleOrange
        {
            get
            {
                if (_paletteSparkleOrange == null)
                    _paletteSparkleOrange = new PaletteSparkleOrange();

                return _paletteSparkleOrange;
            }
        }

        /// <summary>
        /// Gets the single instance of the Purple variant sparkle palette.
        /// </summary>
        public static PaletteSparklePurple PaletteSparklePurple
        {
            get
            {
                if (_paletteSparklePurple == null)
                    _paletteSparklePurple = new PaletteSparklePurple();

                return _paletteSparklePurple;
            }
        }

        /// <summary>
        /// Gets the implementation for the requested renderer mode.
        /// </summary>
        /// <param name="mode">Requested renderer mode.</param>
        /// <returns>IRenderer reference is available; otherwise false.</returns>
        public static IRenderer GetRendererForMode(RendererMode mode)
        {
            switch (mode)
            {
                case RendererMode.Sparkle:
                    return RenderSparkle;
                case RendererMode.Office2007:
                    return RenderOffice2007;
                case RendererMode.Professional:
                    return RenderProfessional;
                case RendererMode.Standard:
                    return RenderStandard;
                case RendererMode.Inherit:
                case RendererMode.Custom:
                default:
                    // Should never be passed
                    Debug.Assert(false);
                    return null;
            }
        }

        /// <summary>
        /// Gets the single instance of the Sparkle renderer.
        /// </summary>
        public static RenderSparkle RenderSparkle
        {
            get
            {
                if (_renderSparkle == null)
                    _renderSparkle = new RenderSparkle();

                return _renderSparkle;
            }
        }

        /// <summary>
        /// Gets the single instance of the Office 2007 renderer.
        /// </summary>
        public static RenderOffice2007 RenderOffice2007
        {
            get
            {
                if (_renderOffice2007 == null)
                    _renderOffice2007 = new RenderOffice2007();

                return _renderOffice2007;
            }
        }

        /// <summary>
        /// Gets the single instance of the Office 2010 renderer.
        /// </summary>
        public static RenderOffice2010 RenderOffice2010
        {
            get
            {
                if (_renderOffice2010 == null)
                    _renderOffice2010 = new RenderOffice2010();

                return _renderOffice2010;
            }
        }

        /// <summary>
        /// Gets the single instance of the professional renderer.
        /// </summary>
        public static RenderProfessional RenderProfessional
        {
            get
            {
                if (_renderProfessional == null)
                    _renderProfessional = new RenderProfessional();

                return _renderProfessional;
            }
        }

        /// <summary>
        /// Gets the single instance of the standard renderer.
        /// </summary>
        public static RenderStandard RenderStandard
        {
            get
            {
                if (_renderStandard == null)
                    _renderStandard = new RenderStandard();

                return _renderProfessional;
            }
        }
        #endregion

        #region Static Internal
        internal static PaletteModeManager InternalGlobalPaletteMode
        {
            get { return _globalPaletteMode; }
        }

        internal static IPalette InternalGlobalPalette
        {
            get { return _globalPalette; }
        }

        internal static bool HasCircularReference()
        {
            // Use a dictionary as a set to check for existence
            Dictionary<IPalette, bool> paletteSet = new Dictionary<IPalette, bool>();

            IPalette palette = null;

            // Get the next palette up in hierarchy
            if (KryptonManager.InternalGlobalPaletteMode == PaletteModeManager.Custom)
                palette = KryptonManager.InternalGlobalPalette;

            // Keep searching until no more palettes found
            while (palette != null)
            {
                // If the palette has already been encountered then it is a circular reference
                if (paletteSet.ContainsKey(palette))
                    return true;
                else
                {
                    // Otherwise, add to the set
                    paletteSet.Add(palette, true);

                    // If this is a KryptonPalette instance
                    if (palette is KryptonPalette)
                    {
                        // Cast to correct type
                        KryptonPalette owner = (KryptonPalette)palette;

                        // Get the next palette up in hierarchy
                        if (owner.BasePaletteMode == PaletteMode.Custom)
                            palette = owner.BasePalette;
                        else if (owner.BasePaletteMode == PaletteMode.Global)
                            palette = KryptonManager.InternalGlobalPalette;
                        else
                            palette = null;
                    }
                    else
                        palette = null;
                }
            }

            // No circular reference encountered
            return false;
        }
        #endregion

        #region Static Implementation
        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Because we are static this event is fired before any instance controls are updated, so we need to
            // tell the palette instances to update now so that when the instance controls are updated the new fonts
            // and other resources are recreated as needed.

            if (_paletteProfessionalOffice2003 != null)
                _paletteProfessionalOffice2003.UserPreferenceChanged();

            if (_paletteProfessionalSystem != null)
                _paletteProfessionalSystem.UserPreferenceChanged();

            if (_paletteOffice2007Blue != null)
                _paletteOffice2007Blue.UserPreferenceChanged();

            if (_paletteOffice2007Silver != null)
                _paletteOffice2007Silver.UserPreferenceChanged();

            if (_paletteOffice2007Black != null)
                _paletteOffice2007Black.UserPreferenceChanged();

            if (_paletteOffice2010Blue != null)
                _paletteOffice2010Blue.UserPreferenceChanged();

            if (_paletteOffice2010Silver != null)
                _paletteOffice2010Silver.UserPreferenceChanged();

            if (_paletteOffice2010Black != null)
                _paletteOffice2010Black.UserPreferenceChanged();

            if (_paletteSparkleBlue != null)
                _paletteSparkleBlue.UserPreferenceChanged();
            
            if (_paletteSparkleOrange != null)
                _paletteSparkleOrange.UserPreferenceChanged();

            if (_paletteSparklePurple != null)
                _paletteSparklePurple.UserPreferenceChanged();

            UpdateToolStripManager();
        }

        private static void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // If the color table has changed then need to update tool strip immediately
            if (e.NeedColorTable)
                UpdateToolStripManager();
        }

        private static void SetPalette(IPalette globalPalette)
        {
            if (globalPalette != _globalPalette)
            {
                // Unhook from current palette events
                if (_globalPalette != null)
                    _globalPalette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

                // Remember the new palette
                _globalPalette = globalPalette;

                // Hook to new palette events
                if (_globalPalette != null)
                    _globalPalette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);
            }
        }

        private static void OnGlobalAllowFormChromeChanged(EventArgs e)
        {
            if (GlobalAllowFormChromeChanged != null)
                GlobalAllowFormChromeChanged(null, e);
        }

        private static void OnGlobalPaletteChanged(EventArgs e)
        {
            UpdateToolStripManager();

            if (GlobalPaletteChanged != null)
                GlobalPaletteChanged(null, e);
        }

        private static void UpdateToolStripManager()
        {
            if (_globalApplyToolstrips)
                ToolStripManager.Renderer = _globalPalette.GetRenderer().RenderToolStrip(_globalPalette);
        }

        private static void ResetToolStripManager()
        {
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;
        }
        #endregion
    }
}
