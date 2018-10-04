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
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Provides an identifiable area for containing other controls.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonPanel), "ToolboxBitmaps.KryptonPanel.bmp")]
    [DefaultEvent("Paint")]
	[DefaultProperty("PanelStyle")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonPanelDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Enables you to group collections of controls.")]
    [Docking(DockingBehavior.Ask)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonPanel : VisualPanel
	{
		#region Instance Fields
		private ViewDrawPanel _drawPanel;
        private PaletteDoubleRedirect _stateCommon;
        private PaletteDouble _stateDisabled;
		private PaletteDouble _stateNormal;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the KryptonPanel class.
		/// </summary>
		public KryptonPanel()
		{
			// Create the palette storage
            _stateCommon = new PaletteDoubleRedirect(Redirector, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
            _stateDisabled = new PaletteDouble(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteDouble(_stateCommon, NeedPaintDelegate);

            Construct();
		}

        /// <summary>
        /// Initialize a new instance of the KryptonPanel class.
        /// </summary>
        /// <param name="stateCommon">Common appearance state to inherit from.</param>
        /// <param name="stateDisabled">Disabled appearance state.</param>
        /// <param name="stateNormal">Normal appearance state.</param>
        public KryptonPanel(PaletteDoubleRedirect stateCommon,
                            PaletteDouble stateDisabled,
                            PaletteDouble stateNormal)
        {
            Debug.Assert(stateCommon != null);
            Debug.Assert(stateDisabled != null);
            Debug.Assert(stateNormal != null);

            // Remember the palette storage
            _stateCommon = stateCommon;
            _stateDisabled = stateDisabled;
            _stateNormal = stateNormal;

            Construct();
        }
        #endregion

		#region Public
		/// <summary>
		/// Gets and sets the panel style.
		/// </summary>
		[Category("Visuals")]
		[Description("Panel style.")]
		public PaletteBackStyle PanelBackStyle
		{
            get { return _stateCommon.BackStyle; }
			
			set
			{
				if (_stateCommon.BackStyle != value)
				{
                    _stateCommon.BackStyle = value;
					PerformNeedPaint(true);
				}
			}
		}

        private bool ShouldSerializePanelBackStyle()
        {
            return (PanelBackStyle != PaletteBackStyle.PanelClient);
        }

        private void ResetPanelBackStyle()
        {
            PanelBackStyle = PaletteBackStyle.PanelClient;
        }

        /// <summary>
        /// Gets access to the common panel appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common panel appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack StateCommon
        {
            get { return _stateCommon.Back; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.Back.IsDefault;
        }
        
        /// <summary>
		/// Gets access to the disabled panel appearance.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled panel appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteBack StateDisabled
		{
			get { return _stateDisabled.Back; }
		}

		private bool ShouldSerializeStateDisabled()
		{
            return !_stateDisabled.Back.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal panel appearance.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal panel appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack StateNormal
		{
            get { return _stateNormal.Back; }
		}

		private bool ShouldSerializeStateNormal()
		{
            return !_stateNormal.Back.IsDefault;
		}

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawPanel.FixedState = state;
        }
        #endregion

		#region Protected
        /// <summary>
        /// Gets access to the view element used to draw the KryptonPanel.
        /// </summary>
        protected ViewDrawPanel ViewDrawPanel
        {
            get { return _drawPanel; }
        }

		/// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Push correct palettes into the view
            _drawPanel.SetPalettes(Enabled ? _stateNormal.Back : _stateDisabled.Back);

            // Update with latest enabled state
            _drawPanel.Enabled = Enabled;

			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);

			// Let base class fire standard event
			base.OnEnabledChanged(e);
		}
		#endregion

        #region Implementation
        private void Construct()
        {
            // Our view contains just a simple canvas that covers entire client area
            _drawPanel = new ViewDrawPanel(_stateNormal.Back);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawPanel);
        }
        #endregion
    }
}
