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
	/// Implement storage and redirection for a KryptonDataGridView state.
	/// </summary>
	public class PaletteDataGridViewRedirect : Storage
	{
		#region Instance Fields
        private PaletteDoubleRedirect _background;
        private PaletteDataGridViewTripleRedirect _dataCell;
        private PaletteDataGridViewTripleRedirect _headerColumn;
        private PaletteDataGridViewTripleRedirect _headerRow;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteDataGridViewRedirect class.
		/// </summary>
        /// <param name="redirect">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDataGridViewRedirect(PaletteRedirect redirect,
                                           NeedPaintHandler needPaint)
		{
            Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Create storage that maps onto the inherit instances
            _background = new PaletteDoubleRedirect(redirect, PaletteBackStyle.GridBackgroundList, PaletteBorderStyle.GridDataCellList, needPaint);
            _dataCell = new PaletteDataGridViewTripleRedirect(redirect, PaletteBackStyle.GridDataCellList, PaletteBorderStyle.GridDataCellList, PaletteContentStyle.GridDataCellList, needPaint);
            _headerColumn = new PaletteDataGridViewTripleRedirect(redirect, PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList, needPaint);
            _headerRow = new PaletteDataGridViewTripleRedirect(redirect, PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList, needPaint);
        }
		#endregion

        #region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
            get { 
                    return (Background.IsDefault &&
                            DataCell.IsDefault &&
                            HeaderColumn.IsDefault &&
                            HeaderRow.IsDefault);
                }
		}
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _background.SetRedirector(redirect);
            _dataCell.SetRedirector(redirect);
            _headerColumn.SetRedirector(redirect);
            _headerRow.SetRedirector(redirect);
        }
        #endregion

        #region BackStyle
        /// <summary>
        /// Gets and sets the back style.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteBackStyle BackStyle
        {
            get { return _background.BackStyle; }
            set { _background.BackStyle = value; }
        }
        #endregion

        #region GridStyle
        /// <summary>
        /// Update the styles of the headers and data cells.
        /// </summary>
        /// <param name="headerColumn">Style for the column headers.</param>
        /// <param name="headerRow">Style for the row headers.</param>
        /// <param name="dataCell">Style for the data cells.</param>
        public void SetGridStyles(GridStyle headerColumn,
                                  GridStyle headerRow,
                                  GridStyle dataCell)   
        {
            switch (headerColumn)
            {
                case GridStyle.List:
                    _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnList, PaletteBorderStyle.GridHeaderColumnList, PaletteContentStyle.GridHeaderColumnList);
                    break;
                case GridStyle.Sheet:
                    _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnSheet, PaletteBorderStyle.GridHeaderColumnSheet, PaletteContentStyle.GridHeaderColumnSheet);
                    break;
                case GridStyle.Custom1:
                    _headerColumn.SetStyles(PaletteBackStyle.GridHeaderColumnCustom1, PaletteBorderStyle.GridHeaderColumnCustom1, PaletteContentStyle.GridHeaderColumnCustom1);
                    break;
            }

            switch (headerRow)
            {
                case GridStyle.List:
                    _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowList, PaletteBorderStyle.GridHeaderRowList, PaletteContentStyle.GridHeaderRowList);
                    break;
                case GridStyle.Sheet:
                    _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowSheet, PaletteBorderStyle.GridHeaderRowSheet, PaletteContentStyle.GridHeaderRowSheet);
                    break;
                case GridStyle.Custom1:
                    _headerRow.SetStyles(PaletteBackStyle.GridHeaderRowCustom1, PaletteBorderStyle.GridHeaderRowCustom1, PaletteContentStyle.GridHeaderRowCustom1);
                    break;
            }

            switch (dataCell)
            {
                case GridStyle.List:
                    _dataCell.SetStyles(PaletteBackStyle.GridDataCellList, PaletteBorderStyle.GridDataCellList, PaletteContentStyle.GridDataCellList);
                    break;
                case GridStyle.Sheet:
                    _dataCell.SetStyles(PaletteBackStyle.GridDataCellSheet, PaletteBorderStyle.GridDataCellSheet, PaletteContentStyle.GridDataCellSheet);
                    break;
                case GridStyle.Custom1:
                    _dataCell.SetStyles(PaletteBackStyle.GridDataCellCustom1, PaletteBorderStyle.GridDataCellCustom1, PaletteContentStyle.GridDataCellCustom1);
                    break;
            }
        }
        #endregion

        #region Background
        /// <summary>
        /// Gets access to the data grid view background palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining data grid view background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteBack Background
        {
            get { return _background.Back; }
        }

        private bool ShouldSerializeBackground()
        {
            return !_background.IsDefault;
        }

        internal IPaletteDouble BackgroundDouble
        {
            get { return _background; }
        }
        #endregion

        #region DataCell
        /// <summary>
        /// Gets access to the data cell palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining data cell appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteDataGridViewTripleRedirect DataCell
        {
            get { return _dataCell; }
        }

        private bool ShouldSerializeDataCell()
        {
            return !_dataCell.IsDefault;
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
        public virtual PaletteDataGridViewTripleRedirect HeaderColumn
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
        public virtual PaletteDataGridViewTripleRedirect HeaderRow
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
