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
    /// Implement storage for a combo box state.
	/// </summary>
	public class PaletteComboBoxRedirect : Storage
	{
		#region Instance Fields
        private PaletteDoubleRedirect _dropBackRedirect;
        private PaletteTripleRedirect _itemRedirect;
        private PaletteInputControlTripleRedirect _comboBoxRedirect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteComboBoxRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteComboBoxRedirect(PaletteRedirect redirect,
                                       NeedPaintHandler needPaint)
		{
			Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _itemRedirect = new PaletteTripleRedirect(redirect, 
                                                      PaletteBackStyle.ButtonListItem, 
                                                      PaletteBorderStyle.ButtonListItem, 
                                                      PaletteContentStyle.ButtonListItem, 
                                                      NeedPaint);

            _comboBoxRedirect = new PaletteInputControlTripleRedirect(redirect, 
                                                                      PaletteBackStyle.InputControlStandalone,
                                                                      PaletteBorderStyle.InputControlStandalone,
                                                                      PaletteContentStyle.InputControlStandalone, 
                                                                      NeedPaint);

            _dropBackRedirect = new PaletteDoubleRedirect(redirect, 
                                                          PaletteBackStyle.ControlClient, 
                                                          PaletteBorderStyle.ButtonStandalone,
                                                          NeedPaint);
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
				return (ComboBox.IsDefault &&
						Item.IsDefault &&
                        DropBack.IsDefault);
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
            _itemRedirect.SetRedirector(redirect);
            _comboBoxRedirect.SetRedirector(redirect);
            _dropBackRedirect.SetRedirector(redirect);
        }
        #endregion

        #region SetStyles
        /// <summary>
        /// Update the combo box input control style.
        /// </summary>
        /// <param name="style">New input control style.</param>
        public void SetStyles(InputControlStyle style)
        {
            _comboBoxRedirect.SetStyles(style);
        }

        /// <summary>
        /// Update the combo box item style.
        /// </summary>
        /// <param name="style">New item style.</param>
        public void SetStyles(ButtonStyle style)
        {
            _itemRedirect.SetStyles(style);
        }

        /// <summary>
        /// Update the combo box drop background style.
        /// </summary>
        /// <param name="style">New drop background style.</param>
        public void SetStyles(PaletteBackStyle style)
        {
            _dropBackRedirect.SetStyles(style, PaletteBorderStyle.ButtonStandalone);
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _comboBoxRedirect.PopulateFromBase(state);
            _itemRedirect.PopulateFromBase(state);
            _dropBackRedirect.PopulateFromBase(state);
        }
        #endregion

        #region ComboBox
        /// <summary>
        /// Gets access to the combo box appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining combo box appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleRedirect ComboBox
		{
			get { return _comboBoxRedirect; }
		}

        private bool ShouldSerializeComboBox()
		{
            return !_comboBoxRedirect.IsDefault;
		}
		#endregion

        #region Item
        /// <summary>
        /// Gets access to the item appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Item
        {
            get { return _itemRedirect; }
        }

        private bool ShouldSerializeItem()
        {
            return !_itemRedirect.IsDefault;
        }
        #endregion

        #region DropBack
        /// <summary>
        /// Gets access to the dropdown background appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining dropdown background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack DropBack
        {
            get { return _dropBackRedirect.Back; }
        }

        private bool ShouldSerializeDropBack()
        {
            return !_dropBackRedirect.IsDefault;
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
