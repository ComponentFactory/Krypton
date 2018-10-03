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
	/// Implement storage for a KryptonDataGridView states with header cells.
	/// </summary>
	public class PaletteDataGridViewHeaders : Storage
	{
		#region Instance Fields
        private PaletteDataGridViewTripleStates _headerColumn;
        private PaletteDataGridViewTripleStates _headerRow;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteDataGridViewHeaders class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDataGridViewHeaders(PaletteDataGridViewRedirect inherit,
                                          NeedPaintHandler needPaint)
		{
            Debug.Assert(inherit != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Create storage that maps onto the inherit instances
            _headerColumn = new PaletteDataGridViewTripleStates(inherit.HeaderColumn, needPaint);
            _headerRow = new PaletteDataGridViewTripleStates(inherit.HeaderRow, needPaint);
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
                return (HeaderColumn.IsDefault &&
                        HeaderRow.IsDefault);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        /// <param name="common">Reference to common settings.</param>
        /// <param name="gridStyle">Grid style to use for populating.</param>
        public virtual void PopulateFromBase(KryptonPaletteCommon common,
                                             PaletteState state,
                                             GridStyle gridStyle)
        {
            if (gridStyle == GridStyle.List)
                common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList);
            else
                common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderColumnSheet, PaletteBorderStyle.GridHeaderColumnSheet, PaletteContentStyle.GridHeaderColumnSheet);
            _headerColumn.PopulateFromBase(state);

            if (gridStyle == GridStyle.List)
                common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList);
            else
                common.StateCommon.SetStyles(PaletteBackStyle.GridHeaderRowSheet, PaletteBorderStyle.GridHeaderRowSheet, PaletteContentStyle.GridHeaderRowSheet);
            _headerRow.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public virtual void SetInherit(PaletteDataGridViewRedirect inherit)
        {
            _headerColumn.SetInherit(inherit.HeaderColumn);
            _headerRow.SetInherit(inherit.HeaderRow);
        }
        #endregion

        #region HeaderColumn
        /// <summary>
        /// Gets access to the header column cell palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining header column cell appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteDataGridViewTripleStates HeaderColumn
        {
            get { return _headerColumn; }
        }

        private bool ShouldSerializeHeaderColumn()
        {
            return !_headerColumn.IsDefault;
        }
        #endregion

        #region HeaderRow
        /// <summary>
        /// Gets access to the header row cell palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining header row cell appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteDataGridViewTripleStates HeaderRow
        {
            get { return _headerRow; }
        }

        private bool ShouldSerializeHeaderRow()
        {
            return !_headerRow.IsDefault;
        }
        #endregion

        #region Implementation
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
