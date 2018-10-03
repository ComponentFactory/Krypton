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
    /// Implement storage for a track bar state.
	/// </summary>
	public class PaletteTrackBarRedirect : Storage
	{
		#region Instance Fields
        private PaletteDoubleRedirect _backRedirect;
        private PaletteElementColorRedirect _tickRedirect;
        private PaletteElementColorRedirect _trackRedirect;
        private PaletteElementColorRedirect _positionRedirect;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTrackBarRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTrackBarRedirect(PaletteRedirect redirect,
                                       NeedPaintHandler needPaint)
		{
			Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _backRedirect = new PaletteDoubleRedirect(redirect, PaletteBackStyle.PanelClient, PaletteBorderStyle.ControlClient, NeedPaint);
            _tickRedirect = new PaletteElementColorRedirect(redirect, PaletteElement.TrackBarTick, NeedPaint);
            _trackRedirect = new PaletteElementColorRedirect(redirect, PaletteElement.TrackBarTrack, NeedPaint);
            _positionRedirect = new PaletteElementColorRedirect(redirect, PaletteElement.TrackBarPosition, NeedPaint);
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
                return (Back.IsDefault &&
                        Tick.IsDefault &&
                        Track.IsDefault &&
                        Position.IsDefault);
			}
		}
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public virtual void SetRedirector(PaletteRedirect redirect)
        {
            _backRedirect.SetRedirector(redirect);
            _tickRedirect.SetRedirector(redirect);
            _trackRedirect.SetRedirector(redirect);
            _positionRedirect.SetRedirector(redirect);
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _backRedirect.PopulateFromBase(state);
            _tickRedirect.PopulateFromBase(state);
            _trackRedirect.PopulateFromBase(state);
            _positionRedirect.PopulateFromBase(state);
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
        public PaletteElementColorRedirect Tick
        {
            get { return _tickRedirect; }
        }

        private bool ShouldSerializeTick()
        {
            return !_tickRedirect.IsDefault;
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
        public PaletteElementColorRedirect Track
        {
            get { return _trackRedirect; }
        }

        private bool ShouldSerializeTrack()
        {
            return !_trackRedirect.IsDefault;
        }
        #endregion

        #region Position
        /// <summary>
        /// Gets access to the position marker appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining position marker appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteElementColorRedirect Position
        {
            get { return _positionRedirect; }
        }

        private bool ShouldSerializePosition()
        {
            return !_positionRedirect.IsDefault;
        }
        #endregion

        #region Internal
        /// <summary>
        /// Gets access to the background appearance.
        /// </summary>
        internal PaletteBack Back
        {
            get { return _backRedirect.Back; }
        }

        internal PaletteBackStyle BackStyle
        {
            get { return _backRedirect.BackStyle; }
            set { _backRedirect.BackStyle = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Handle a change event from palette source.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="needLayout">True if a layout is also needed.</param>
        protected void OnNeedPaint(object sender, bool needLayout)
        {
            // Pass request from child to our own handler
            PerformNeedPaint(needLayout);
        }
        #endregion
    }
}
