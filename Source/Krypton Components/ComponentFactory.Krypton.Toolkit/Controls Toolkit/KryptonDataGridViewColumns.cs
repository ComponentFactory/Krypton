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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;
using System.Threading;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class DataGridViewColumnSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewColumnSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public DataGridViewColumnSpecCollection(object owner)
            : base(owner)
        {
        }
        #endregion
    }

    /// <summary>
    /// Event argument data for a data grid view buttons spec.
    /// </summary>
    public class DataGridViewButtonSpecClickEventArgs : EventArgs
    {
        #region Instance Fields
        private DataGridViewColumn _column;
        private DataGridViewCell _cell;
        private ButtonSpecAny _buttonSpec;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DataGridViewButtonSpecClickEventArgs class.
        /// </summary>
        /// <param name="column">Reference to data grid view column.</param>
        /// <param name="cell">Reference to data grid view cell.</param>
        /// <param name="buttonSpec">Reference to button spec.</param>
        public DataGridViewButtonSpecClickEventArgs(DataGridViewColumn column,
                                                    DataGridViewCell cell,
                                                    ButtonSpecAny buttonSpec)
        {
            _column = column;
            _cell = cell;
            _buttonSpec = buttonSpec;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the column associated with the button spec.
        /// </summary>
        public DataGridViewColumn Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets a reference to the cell that generated the click event.
        /// </summary>
        public DataGridViewCell Cell
        {
            get { return _cell; }
        }

        /// <summary>
        /// Gets a reference to the button spec that is performing the click.
        /// </summary>
        public ButtonSpecAny ButtonSpec
        {
            get { return _buttonSpec; }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewTextBoxCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonTextBoxColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewTextBoxColumn), "ToolboxBitmaps.KryptonTextBox.bmp")]
    public class KryptonDataGridViewTextBoxColumn : DataGridViewColumn
    {
        #region Instance Fields
        private DataGridViewColumnSpecCollection _buttonSpecs;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewTextBoxColumn class.
        /// </summary>
        public KryptonDataGridViewTextBoxColumn()
            : base(new KryptonDataGridViewTextBoxCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
            SortMode = DataGridViewColumnSortMode.Automatic;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewTextBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewTextBoxColumn cloned = base.Clone() as KryptonDataGridViewTextBoxColumn;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
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
        /// Gets or sets the maximum number of characters that can be entered into the text box.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(int), "32767")]
        public int MaxInputLength
        {
            get
            {
                if (TextBoxCellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewTextBoxColumn cell template required");

                return TextBoxCellTemplate.MaxInputLength;
            }

            set
            {
                if (MaxInputLength != value)
                {
                    TextBoxCellTemplate.MaxInputLength = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewTextBoxCell cell = rows.SharedRow(i).Cells[Index] as DataGridViewTextBoxCell;
                            if (cell != null)
                                cell.MaxInputLength = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the sort mode for the column.
        /// </summary>
        [DefaultValue(typeof(DataGridViewColumnSortMode), "Automatic")]
        public new DataGridViewColumnSortMode SortMode
        {
            get { return base.SortMode; }
            set { base.SortMode = value; }
        }

        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewTextBoxCell))
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewTextBoxCell");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }
        #endregion

        #region Private
        private KryptonDataGridViewTextBoxCell TextBoxCellTemplate
        {
            get { return (KryptonDataGridViewTextBoxCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Displays editable text information in a KryptonDataGridView control.
    /// </summary>
    public class KryptonDataGridViewTextBoxCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonTextBox _paintingTextBox;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewTextBoxEditingControl);
        private static readonly Type _defaultValueType = typeof(System.String);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewTextBoxCell cell type
        /// </summary>
        public KryptonDataGridViewTextBoxCell()
        {
            // Create a thread specific KryptonTextBox control used for the painting of the non-edited cells
            if (_paintingTextBox == null)
            {
                _paintingTextBox = new KryptonTextBox();
                _paintingTextBox.StateCommon.Border.Width = 0;
                _paintingTextBox.StateCommon.Border.Draw = InheritBool.False;
                _paintingTextBox.StateCommon.Back.Color1 = Color.Empty;
            }
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "KryptonDataGridViewTextBoxCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
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
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;

                return _defaultValueType;
            }
        }

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonTextBox kryptonTextBox = dataGridView.EditingControl as KryptonTextBox;
            if (kryptonTextBox != null)
            {
                KryptonDataGridViewTextBoxColumn textBoxColumn = OwningColumn as KryptonDataGridViewTextBoxColumn;
                if (textBoxColumn != null)
                {
                    foreach (ButtonSpec bs in kryptonTextBox.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    
                    kryptonTextBox.ButtonSpecs.Clear();

                    TextBox textBox = kryptonTextBox.Controls[0] as TextBox;
                    if (textBox != null)
                        textBox.ClearUndo();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonTextBox textBox = DataGridView.EditingControl as KryptonTextBox;
            if (textBox != null)
            {
                KryptonDataGridViewTextBoxColumn textBoxColumn = OwningColumn as KryptonDataGridViewTextBoxColumn;
                if (textBoxColumn != null)
                {
                    // Set this cell as the owner of the buttonspecs
                    textBox.ButtonSpecs.Clear();
                    textBox.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in textBoxColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        textBox.ButtonSpecs.Add(bs);
                    }
                }

                string initialFormattedValueStr = initialFormattedValue as string;
                if (initialFormattedValueStr == null)
                    textBox.Text = string.Empty;
                else
                    textBox.Text = initialFormattedValueStr;

                DataGridViewTriState wrapMode = this.Style.WrapMode;
                if (wrapMode == DataGridViewTriState.NotSet)
                    wrapMode = this.OwningColumn.DefaultCellStyle.WrapMode;

                textBox.WordWrap = textBox.Multiline = (wrapMode == DataGridViewTriState.True);
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewTextBoxColumn textColumn = OwningColumn as KryptonDataGridViewTextBoxColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(textColumn, this, (ButtonSpecAny)sender);
            textColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewTextBoxEditingControl EditingTextBox
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewTextBoxEditingControl; }
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = DataGridView.EditingControl.GetPreferredSize(new Size(editingControlBounds.Width, 10000)).Height;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingTextBox(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewTextBoxEditingControl control = DataGridView.EditingControl as KryptonDataGridViewTextBoxEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewTextBoxCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewTextBoxEditingControl : KryptonTextBox,
                                                            IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewTextBoxEditingControl class.
        /// </summary>
        public KryptonDataGridViewTextBoxEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            StateCommon.Content.Padding = new Padding(0);
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
            TextAlign = KryptonDataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    {
                        TextBox textBox = Controls[0] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        TextBox textBox = Controls[0] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the begining of the string or if the entire text is selected 
                            // and we did not start editing, send this character to the dataGridView, else process the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Down:
                case Keys.Up:
                    return true;
                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        TextBox textBox = Controls[0] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength != textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Delete:
                    {
                        // Let the grid handle the key if the carret is at the end of the text.
                        TextBox textBox = Controls[0] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength > 0 ||
                                textBox.SelectionStart < textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            TextBox textBox = Controls[0] as TextBox;
            if (textBox != null)
            {
                if (selectAll)
                    textBox.SelectAll();
                else
                    textBox.SelectionStart = textBox.Text.Length;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            TextBox textBox = Controls[0] as TextBox;
            if (textBox != null)
            {
                PI.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewCheckBoxCell cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewCheckBoxColumn), "ToolboxBitmaps.KryptonCheckBox.bmp")]
    public class KryptonDataGridViewCheckBoxColumn : DataGridViewColumn
    {
        #region Implementation
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxColumn class.
        /// </summary>
        public KryptonDataGridViewCheckBoxColumn()
            : this(false)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxColumn class.
        /// </summary>
        /// <param name="threeState">true to display check boxes with three states; false to display check boxes with two states.</param>
        public KryptonDataGridViewCheckBoxColumn(bool threeState)
            : base(new KryptonDataGridViewCheckBoxCell(threeState))
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (threeState)
                style.NullValue = CheckState.Indeterminate;
            else
                style.NullValue = false;
            DefaultCellStyle = style;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewCheckBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewCheckBoxCell))
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewCheckBoxCell");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the underlying value corresponding to a cell value of false, which appears as an unchecked box.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object FalseValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");

                return CheckBoxCellTemplate.FalseValue;
            }
            set
            {
                if (FalseValue != value)
                {
                    CheckBoxCellTemplate.FalseValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                                cell.FalseValue = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the underlying value corresponding to an indeterminate or a null reference (Nothing in Visual Basic) cell value, which appears as a disabled checkbox.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object IndeterminateValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                
                return CheckBoxCellTemplate.IndeterminateValue;
            }
            set
            {
                if (IndeterminateValue != value)
                {
                    CheckBoxCellTemplate.IndeterminateValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                                cell.IndeterminateValue = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }        
        
        /// <summary>
        /// Gets or sets the underlying value corresponding to a cell value of true, which appears as a checked box.
        /// </summary>
        [Category("Data")]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        public object TrueValue
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");
                
                return CheckBoxCellTemplate.TrueValue;
            }
            set
            {
                if (TrueValue != value)
                {
                    CheckBoxCellTemplate.TrueValue = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                                cell.TrueValue = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get
            {
                if (CheckBoxCellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewCheckBoxColumn cell template required");

                return CheckBoxCellTemplate.ThreeState;
            }
            set
            {
                if (ThreeState != value)
                {
                    CheckBoxCellTemplate.ThreeState = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewCheckBoxCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewCheckBoxCell;
                            if (cell != null)
                                cell.ThreeState = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }

                    if ((value && (DefaultCellStyle.NullValue is bool)) && !((bool)DefaultCellStyle.NullValue))
                        DefaultCellStyle.NullValue = CheckState.Indeterminate;
                    else if ((!value && (DefaultCellStyle.NullValue is CheckState)) && (((CheckState)DefaultCellStyle.NullValue) == CheckState.Indeterminate))
                        DefaultCellStyle.NullValue = false;
                }
            }
        }
        #endregion

        #region Private
        private KryptonDataGridViewCheckBoxCell CheckBoxCellTemplate
        {
            get { return (KryptonDataGridViewCheckBoxCell)CellTemplate; }
        }

        private bool ShouldSerializeDefaultCellStyle()
        {
            KryptonDataGridViewCheckBoxCell cellTemplate = CheckBoxCellTemplate;
            if (cellTemplate != null)
            {
                object indeterminate;
                if (cellTemplate.ThreeState)
                    indeterminate = CheckState.Indeterminate;
                else
                    indeterminate = false;

                if (!base.HasDefaultCellStyle)
                    return false;

                DataGridViewCellStyle defaultCellStyle = DefaultCellStyle;
                if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && defaultCellStyle.NullValue.Equals(indeterminate)) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)) && ((defaultCellStyle.WrapMode == DataGridViewTriState.NotSet) && (defaultCellStyle.Tag == null))))
                    return !defaultCellStyle.Padding.Equals(Padding.Empty);
            }
            return true;
        }

        #endregion
    }

    /// <summary>
    /// Displays a check box user interface (UI) to use in a DataGridView control.
    /// </summary>
    public class KryptonDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
    {
        #region Static Fields
        private static PropertyInfo _piButtonState;
        private static PropertyInfo _piMouseEnteredCellAddress;
        private static FieldInfo _fiMouseInContentBounds;
        #endregion

        #region Instance Fields
        private Rectangle _contentBounds;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
        /// </summary>
        public KryptonDataGridViewCheckBoxCell()
            : this(false)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
        /// </summary>
        /// <param name="threeState">Enable binary or ternary operation.</param>
        public KryptonDataGridViewCheckBoxCell(bool threeState)
            : base(threeState)
        {
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's content area.
        /// </summary>
        /// <param name="graphics">Graphics instance for calculations.</param>
        /// <param name="cellStyle">Cell style to use in calculations.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <returns></returns>
        protected override Rectangle GetContentBounds(Graphics graphics, 
                                                      DataGridViewCellStyle cellStyle, 
                                                      int rowIndex)
        {
            // Return the cached bounds from last drawing cycle
            return _contentBounds;
        }

        /// <summary>
        /// This member overrides DataGridViewCell.GetPreferredSize. 
        /// </summary>
        /// <param name="graphics">Graphics instance used for calculations.</param>
        /// <param name="cellStyle">Individual cell style to apply.</param>
        /// <param name="rowIndex">Row of column being processed.</param>
        /// <param name="constraintSize">Maximum allowed size.</param>
        /// <returns>Requested ideal size for the cell.</returns>
        protected override Size GetPreferredSize(Graphics graphics,
                                                 DataGridViewCellStyle cellStyle,
                                                 int rowIndex,
                                                 Size constraintSize)
        {
            try
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Is this cell the currently active cell
                bool currentCell = (rowIndex == DataGridView.CurrentCellAddress.Y) &&
                                   (ColumnIndex == DataGridView.CurrentCellAddress.X);

                // Is this cell the same as the one with the mouse inside it
                Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
                bool mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                                 (ColumnIndex == mouseEnteredCellAddress.X);

                // Snoop tracking and pressed status from the base class implementation
                bool tracking = mouseCell && MouseInContentBoundsInternal;
                bool pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

                // Find out the requested size of the check box drawing
                using (ViewLayoutContext viewContent = new ViewLayoutContext(kDGV, kDGV.Renderer))
                {
                    Size checkBoxSize = kDGV.Renderer.RenderGlyph.GetCheckBoxPreferredSize(viewContent,
                                                                                           kDGV.Redirector,
                                                                                           kDGV.Enabled,
                                                                                           CheckState.Unchecked,
                                                                                           tracking,
                                                                                           pressed);

                    // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
                    // in the painting routine to prevent drawing over the bottom right border)
                    checkBoxSize.Width += cellStyle.Padding.Horizontal + 1;
                    checkBoxSize.Height += cellStyle.Padding.Vertical + 1;

                    return checkBoxSize;
                }
            }
            catch
            {
                return Size.Empty;
            }
        }

        /// <summary>
        /// This member overrides DataGridViewCell.Paint.
        /// </summary>
        /// <param name="graphics">The Graphics used to paint the DataGridViewCell.</param>
        /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be repainted.</param>
        /// <param name="cellBounds">A Rectangle that contains the bounds of the DataGridViewCell that is being painted.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="cellState">A bitwise combination of DataGridViewElementStates values that specifies the state of the cell.</param>
        /// <param name="value">The data of the DataGridViewCell that is being painted.</param>
        /// <param name="formattedValue">The formatted data of the DataGridViewCell that is being painted.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="cellStyle">A DataGridViewCellStyle that contains formatting and style information about the cell.</param>
        /// <param name="advancedBorderStyle">A DataGridViewAdvancedBorderStyle that contains border styles for the cell that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of the DataGridViewPaintParts values that specifies which parts of the cell need to be painted.</param>
        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates cellState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if ((DataGridView != null) && (DataGridView is KryptonDataGridView))
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Should we draw the content foreground?
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
                {
                    CheckState checkState = CheckState.Unchecked;

                    if ((formattedValue != null) && (formattedValue is CheckState))
                        checkState = (CheckState)formattedValue;
                    else if ((formattedValue != null) && (formattedValue is bool))
                    {
                        if ((bool)formattedValue)
                            checkState = CheckState.Checked;
                        else
                            checkState = CheckState.Unchecked;
                    }

                    // Is this cell the currently active cell
                    bool currentCell = (rowIndex == DataGridView.CurrentCellAddress.Y) &&
                                       (ColumnIndex == DataGridView.CurrentCellAddress.X);

                    // Is this cell the same as the one with the mouse inside it
                    Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
                    bool mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                                     (ColumnIndex == mouseEnteredCellAddress.X);

                    // Snoop tracking and pressed status from the base class implementation
                    bool tracking = mouseCell && MouseInContentBoundsInternal;
                    bool pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

                    using (RenderContext renderContext = new RenderContext(kDGV, graphics, cellBounds, kDGV.Renderer))
                    {
                        Size checkBoxSize = Size.Empty;

                        // Find out the requested size of the check box drawing
                        using (ViewLayoutContext viewContent = new ViewLayoutContext(kDGV, kDGV.Renderer))
                            checkBoxSize = renderContext.Renderer.RenderGlyph.GetCheckBoxPreferredSize(viewContent, 
                                                                                                       kDGV.Redirector,
                                                                                                       kDGV.Enabled,
                                                                                                       checkState,
                                                                                                       tracking,
                                                                                                       pressed);
                        // Remember the original cell bounds
                        Rectangle startBounds = cellBounds;

                        // Prevent check box overlapping the bottom/right border
                        cellBounds.Width--;
                        cellBounds.Height--;

                        // Adjust the horizontal alignment
                        switch (cellStyle.Alignment)
                        {
                            case DataGridViewContentAlignment.NotSet:
                            case DataGridViewContentAlignment.TopCenter:
                            case DataGridViewContentAlignment.MiddleCenter:
                            case DataGridViewContentAlignment.BottomCenter:
                                cellBounds.X += (cellBounds.Width - checkBoxSize.Width) / 2;
                                break;
                            case DataGridViewContentAlignment.TopRight:
                            case DataGridViewContentAlignment.MiddleRight:
                            case DataGridViewContentAlignment.BottomRight:
                                cellBounds.X = cellBounds.Right - checkBoxSize.Width;
                                break;
                        }

                        // Adjust the vertical alignment
                        switch (cellStyle.Alignment)
                        {
                            case DataGridViewContentAlignment.NotSet:
                            case DataGridViewContentAlignment.MiddleLeft:
                            case DataGridViewContentAlignment.MiddleCenter:
                            case DataGridViewContentAlignment.MiddleRight:
                                cellBounds.Y += (cellBounds.Height - checkBoxSize.Height) / 2;
                                break;
                            case DataGridViewContentAlignment.BottomLeft:
                            case DataGridViewContentAlignment.BottomCenter:
                            case DataGridViewContentAlignment.BottomRight:
                                cellBounds.Y = cellBounds.Bottom - checkBoxSize.Height;
                                break;
                        }

                        // Make the cell the same size as the check box itself
                        cellBounds.Width = checkBoxSize.Width;
                        cellBounds.Height = checkBoxSize.Height;

                        // Remember the current drawing bounds
                        _contentBounds = new Rectangle(cellBounds.X - startBounds.X,
                                                       cellBounds.Y - startBounds.Y,
                                                       cellBounds.Width, cellBounds.Height);

                        // Perform actual drawing of the check box
                        renderContext.Renderer.RenderGlyph.DrawCheckBox(renderContext,
                                                                        cellBounds,
                                                                        kDGV.Redirector,
                                                                        kDGV.Enabled,
                                                                        checkState,
                                                                        tracking,
                                                                        pressed);
                    }
                }
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                           cellState, value, formattedValue, errorText,
                           cellStyle, advancedBorderStyle, paintParts);
            }
        }
        #endregion

        #region Private
        private ButtonState ButtonStateInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piButtonState == null)
                {
                    // Cache access to the internal get property 'ButtonState'
                    _piButtonState = typeof(DataGridViewCheckBoxCell).GetProperty("ButtonState", BindingFlags.Instance |
                                                                                                 BindingFlags.NonPublic |
                                                                                                 BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (ButtonState)_piButtonState.GetValue(this, null);
            }
        }

        private bool MouseInContentBoundsInternal
        {
            get
            {
                // Only need to cache reflection info the first time it is needed
                if (_fiMouseInContentBounds == null)
                {
                    // Cache field info about the internal 'mouseInContentBounds' instance
                    _fiMouseInContentBounds = typeof(DataGridViewCheckBoxCell).GetField("mouseInContentBounds", BindingFlags.Static |
                                                                                                                BindingFlags.NonPublic |
                                                                                                                BindingFlags.GetField);
                }

                // Grab the internal property implemented by base class
                return (bool)_fiMouseInContentBounds.GetValue(this);
            }
        }

        private Point MouseEnteredCellAddressInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piMouseEnteredCellAddress == null)
                {
                    // Cache access to the internal get property 'MouseEnteredCellAddress'
                    _piMouseEnteredCellAddress = typeof(DataGridView).GetProperty("MouseEnteredCellAddress", BindingFlags.Instance |
                                                                                                             BindingFlags.NonPublic |
                                                                                                             BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (Point)_piMouseEnteredCellAddress.GetValue(base.DataGridView, null);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewButtonCell cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewButtonColumn), "ToolboxBitmaps.KryptonButton.bmp")]
    public class KryptonDataGridViewButtonColumn : DataGridViewColumn
    {
        #region Static Fields
        private MethodInfo _miColumnCommonChange;
        private PropertyInfo _piUseColumnTextForButtonValueInternal;
        #endregion

        #region Instance Fields
        private string _text;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewButtonColumn class.
        /// </summary>
        public KryptonDataGridViewButtonColumn()
            : base(new KryptonDataGridViewButtonCell())
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefaultCellStyle = style;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewButtonColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// This member overrides DataGridViewButtonColumn.Clone.
        /// </summary>
        /// <returns>New object instance.</returns>
        public override object Clone()
        {
            // Create a new instance
            KryptonDataGridViewButtonColumn clone = base.Clone() as KryptonDataGridViewButtonColumn;
            clone.Text = Text;
            return clone;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewButtonCell))
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewButtonCell");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the column's default cell style.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get { return base.DefaultCellStyle; }
            set { base.DefaultCellStyle = value; }
        }
        
        /// <summary>
        /// Gets or sets the default text displayed on the button cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue((string)null)]
        public string Text
        {
            get { return _text; }
            set
            {
                if (!string.Equals(value, _text, StringComparison.Ordinal))
                {
                    _text = value;
                    if (DataGridView != null)
                    {
                        if (UseColumnTextForButtonValue)
                            ColumnCommonChange(Index);
                        else
                        {
                            DataGridViewRowCollection rows = DataGridView.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                KryptonDataGridViewButtonCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewButtonCell;
                                if ((cell != null) && cell.UseColumnTextForButtonValue)
                                {
                                    ColumnCommonChange(Index);
                                    return;
                                }
                            }
                            DataGridView.InvalidateColumn(Index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the button text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool UseColumnTextForButtonValue
        {
            get
            {
                if (CellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewButtonColumn cell template required");

                return ((KryptonDataGridViewButtonCell)CellTemplate).UseColumnTextForButtonValue;
            }

            set
            {
                if (UseColumnTextForButtonValue != value)
                {
                    SetUseColumnTextForButtonValueInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewButtonCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewButtonCell;
                            if (cell != null)
                                SetUseColumnTextForButtonValueInternal(cell, value);
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the button text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get
            {
                if (CellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewButtonColumn cell template required");

                return ((KryptonDataGridViewButtonCell)CellTemplate).ButtonStyle;
            }

            set
            {
                if (ButtonStyle != value)
                {
                    ((KryptonDataGridViewButtonCell)CellTemplate).ButtonStyleInternal = value;
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            KryptonDataGridViewButtonCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewButtonCell;
                            if (cell != null)
                                cell.ButtonStyleInternal = value;
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }
        #endregion

        #region Private
        private bool ShouldSerializeDefaultCellStyle()
        {
            if (!HasDefaultCellStyle)
                return false;

            DataGridViewCellStyle defaultCellStyle = DefaultCellStyle;
            if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && 
                  (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && 
                  (((defaultCellStyle.Font == null) && defaultCellStyle.IsNullValueDefault) && 
                    (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && 
                   ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)) && 
                   ((defaultCellStyle.WrapMode == DataGridViewTriState.NotSet) && (defaultCellStyle.Tag == null))))
            {
                return !defaultCellStyle.Padding.Equals(Padding.Empty);
            }
            
            return true;
        }

        private void ColumnCommonChange(int columnIndex)
        {
            // Only need to cache reflection info the first time around
            if (_miColumnCommonChange == null)
            {
                // Cache access to the internal method 'OnColumnCommonChange'
                _miColumnCommonChange = typeof(DataGridView).GetMethod("OnColumnCommonChange", BindingFlags.Instance |
                                                                                               BindingFlags.NonPublic |
                                                                                               BindingFlags.GetField);

            }

            _miColumnCommonChange.Invoke(DataGridView, new object[] { columnIndex });
        }

        private void SetUseColumnTextForButtonValueInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piUseColumnTextForButtonValueInternal == null)
            {
                // Cache access to the internal property sette 'UseColumnTextForButtonValueInternal'
                _piUseColumnTextForButtonValueInternal = typeof(DataGridViewButtonCell).GetProperty("UseColumnTextForButtonValueInternal", BindingFlags.Instance |
                                                                                                                                           BindingFlags.NonPublic |
                                                                                                                                           BindingFlags.SetProperty);

            }

            _piUseColumnTextForButtonValueInternal.SetValue(instance, value, null);
        }
        #endregion
    }

    /// <summary>
    /// Displays a button-like user interface (UI) for use in a DataGridView control.
    /// </summary>
    public class KryptonDataGridViewButtonCell : DataGridViewButtonCell
    {
        #region Static Fields
        private static PropertyInfo _piButtonState;
        private static PropertyInfo _piMouseEnteredCellAddress;
        private static FieldInfo _fiMouseInContentBounds;
        #endregion

        #region Instance Fields
        private bool _styleSet;
        private ButtonStyle _buttonStyle;
        private PaletteTripleToPalette _palette;
        private ShortTextValue _shortTextValue;
        private ViewDrawButton _viewButton;
        private Rectangle _contentBounds;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewCheckBoxCell.
        /// </summary>
        public KryptonDataGridViewButtonCell()
        {
            _buttonStyle = ButtonStyle.Standalone;
        }
        #endregion

        #region Public
        /// <summary>
        /// This member overrides KryptonDataGridViewButtonCell.Clone.
        /// </summary>
        /// <returns>New object instance.</returns>
        public override object Clone()
        {
            KryptonDataGridViewButtonCell dataGridViewCell = base.Clone() as KryptonDataGridViewButtonCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell._styleSet = _styleSet;
                dataGridViewCell._shortTextValue = _shortTextValue;
                dataGridViewCell._buttonStyle = _buttonStyle;
            }
            return dataGridViewCell;
        }

        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get { return _buttonStyle; }

            set
            {
                _buttonStyle = value;
                _styleSet = true;
                DataGridView.InvalidateCell(this);
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's content area.
        /// </summary>
        /// <param name="graphics">Graphics instance for calculations.</param>
        /// <param name="cellStyle">Cell style to use in calculations.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <returns></returns>
        protected override Rectangle GetContentBounds(Graphics graphics,
                                                      DataGridViewCellStyle cellStyle,
                                                      int rowIndex)
        {
            // Return the cached bounds from last drawing cycle
            return _contentBounds;
        }

        /// <summary>
        /// This member overrides DataGridViewCell.GetPreferredSize. 
        /// </summary>
        /// <param name="graphics">Graphics instance used for calculations.</param>
        /// <param name="cellStyle">Individual cell style to apply.</param>
        /// <param name="rowIndex">Row of column being processed.</param>
        /// <param name="constraintSize">Maximum allowed size.</param>
        /// <returns>Requested ideal size for the cell.</returns>
        protected override Size GetPreferredSize(Graphics graphics, 
                                                 DataGridViewCellStyle cellStyle, 
                                                 int rowIndex, 
                                                 Size constraintSize)
        {
            try
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Create the view elements and palette structure
                CreateViewAndPalettes(kDGV);

                // Is this cell the currently active cell
                bool currentCell = (rowIndex == DataGridView.CurrentCellAddress.Y) &&
                                   (ColumnIndex == DataGridView.CurrentCellAddress.X);

                // Is this cell the same as the one with the mouse inside it
                Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
                bool mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                                 (ColumnIndex == mouseEnteredCellAddress.X);

                // Snoop tracking and pressed status from the base class implementation
                bool tracking = mouseCell && MouseInContentBoundsInternal;
                bool pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

                // Update the button state to reflect the tracking/pressed values
                if (pressed)
                    _viewButton.ElementState = PaletteState.Pressed;
                else if (tracking)
                    _viewButton.ElementState = PaletteState.Tracking;
                else
                    _viewButton.ElementState = PaletteState.Normal;

                // Update the display text
                KryptonDataGridViewButtonColumn col = kDGV.Columns[ColumnIndex] as KryptonDataGridViewButtonColumn;
                if ((col != null) && col.UseColumnTextForButtonValue && !kDGV.Rows[rowIndex].IsNewRow)
                    _shortTextValue.ShortText = col.Text;
                else if ((FormattedValue != null) && !string.IsNullOrEmpty(FormattedValue.ToString()))
                    _shortTextValue.ShortText = FormattedValue.ToString();
                else
                    _shortTextValue.ShortText = string.Empty;

                // Position the button element inside the available cell area
                using (ViewLayoutContext layoutContext = new ViewLayoutContext(kDGV, kDGV.Renderer))
                {
                    // Define the available area for layout
                    layoutContext.DisplayRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

                    // Get the ideal size of the button
                    Size buttonSize = _viewButton.GetPreferredSize(layoutContext);

                    // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
                    // in the painting routine to prevent drawing over the bottom right border)
                    buttonSize.Width += cellStyle.Padding.Horizontal + 1;
                    buttonSize.Height += cellStyle.Padding.Vertical + 1;

                    return buttonSize;
                }
            }
            catch
            {
                return Size.Empty;
            }
        }

        /// <summary>
        /// This member overrides DataGridViewCell.Paint.
        /// </summary>
        /// <param name="graphics">The Graphics used to paint the DataGridViewCell.</param>
        /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be repainted.</param>
        /// <param name="cellBounds">A Rectangle that contains the bounds of the DataGridViewCell that is being painted.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="cellState">A bitwise combination of DataGridViewElementStates values that specifies the state of the cell.</param>
        /// <param name="value">The data of the DataGridViewCell that is being painted.</param>
        /// <param name="formattedValue">The formatted data of the DataGridViewCell that is being painted.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="cellStyle">A DataGridViewCellStyle that contains formatting and style information about the cell.</param>
        /// <param name="advancedBorderStyle">A DataGridViewAdvancedBorderStyle that contains border styles for the cell that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of the DataGridViewPaintParts values that specifies which parts of the cell need to be painted.</param>
        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates cellState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if ((DataGridView != null) && (DataGridView is KryptonDataGridView))
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Should we draw the content foreground?
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
                {
                    using (RenderContext renderContext = new RenderContext(kDGV, graphics, cellBounds, kDGV.Renderer))
                    {
                        // Create the view elements and palette structure
                        CreateViewAndPalettes(kDGV);

                        // Cache the starting cell bounds
                        Rectangle startBounds = cellBounds;

                        // Is this cell the currently active cell
                        bool currentCell = (rowIndex == DataGridView.CurrentCellAddress.Y) &&
                                           (ColumnIndex == DataGridView.CurrentCellAddress.X);

                        // Is this cell the same as the one with the mouse inside it
                        Point mouseEnteredCellAddress = MouseEnteredCellAddressInternal;
                        bool mouseCell = (rowIndex == mouseEnteredCellAddress.Y) &&
                                         (ColumnIndex == mouseEnteredCellAddress.X);

                        // Snoop tracking and pressed status from the base class implementation
                        bool tracking = mouseCell && MouseInContentBoundsInternal;
                        bool pressed = currentCell && ((ButtonStateInternal & ButtonState.Pushed) == ButtonState.Pushed);

                        // Update the button state to reflect the tracking/pressed values
                        if (pressed)
                            _viewButton.ElementState = PaletteState.Pressed;
                        else if (tracking)
                            _viewButton.ElementState = PaletteState.Tracking;
                        else
                            _viewButton.ElementState = PaletteState.Normal;

                        // Update the display text
                        KryptonDataGridViewButtonColumn col = kDGV.Columns[ColumnIndex] as KryptonDataGridViewButtonColumn;
                        if ((col != null) && col.UseColumnTextForButtonValue && !kDGV.Rows[rowIndex].IsNewRow)
                            _shortTextValue.ShortText = col.Text;
                        else if ((FormattedValue != null) && !string.IsNullOrEmpty(FormattedValue.ToString()))
                            _shortTextValue.ShortText = FormattedValue.ToString();
                        else
                            _shortTextValue.ShortText = string.Empty;

                        // Prevent button overlapping the bottom/right border
                        cellBounds.Width--;
                        cellBounds.Height--;

                        // Apply the padding
                        if (kDGV.RightToLeftInternal)
                            cellBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Bottom);
                        else
                            cellBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);

                        cellBounds.Width -= cellStyle.Padding.Horizontal;
                        cellBounds.Height -= cellStyle.Padding.Vertical;

                        // Position the button element inside the available cell area
                        using (ViewLayoutContext layoutContext = new ViewLayoutContext(kDGV, kDGV.Renderer))
                        {
                            // Define the available area for layout
                            layoutContext.DisplayRectangle = cellBounds;

                            // Perform actual layout inside that area
                            _viewButton.Layout(layoutContext);
                        }
                            
                        // Ask the element to draw now
                        _viewButton.Render(renderContext);

                        // Remember the current drawing bounds
                        _contentBounds = new Rectangle(cellBounds.X - startBounds.X,
                                                       cellBounds.Y - startBounds.Y,
                                                       cellBounds.Width, cellBounds.Height);
                    }
                }
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                           cellState, value, formattedValue, errorText,
                           cellStyle, advancedBorderStyle, paintParts);
            }
        }
        #endregion

        #region Private
        private void CreateViewAndPalettes(KryptonDataGridView kDGV)
        {
            // Create the view element when first needed
            if (_viewButton == null)
            {
                // Create helper object to get all values from the DGV redirector
                _palette = new PaletteTripleToPalette(kDGV.Redirector,
                                                      PaletteBackStyle.ButtonStandalone,
                                                      PaletteBorderStyle.ButtonStandalone,
                                                      PaletteContentStyle.ButtonStandalone);

                // Provider of values for the button element
                _shortTextValue = new ShortTextValue();

                // Create view element for drawing the actual button
                _viewButton = new ViewDrawButton(_palette, _palette, _palette, 
                                                 _palette, _palette, _palette, _palette,
                                                 new PaletteMetricRedirect(kDGV.Redirector),
                                                 _shortTextValue, VisualOrientation.Top, false);
            }

            // Update with latest defined style
            _palette.SetStyles(_buttonStyle);
        }

        internal ButtonStyle ButtonStyleInternal
        {
            set
            {
                if (!_styleSet)
                    _buttonStyle = value;
            }
        }

        private ButtonState ButtonStateInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piButtonState == null)
                {
                    // Cache access to the internal get property 'ButtonState'
                    _piButtonState = typeof(DataGridViewButtonCell).GetProperty("ButtonState", BindingFlags.Instance |
                                                                                               BindingFlags.NonPublic |
                                                                                               BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (ButtonState)_piButtonState.GetValue(this, null);
            }
        }

        private bool MouseInContentBoundsInternal
        {
            get
            {
                // Only need to cache reflection info the first time it is needed
                if (_fiMouseInContentBounds == null)
                {
                    // Cache field info about the internal 'mouseInContentBounds' instance
                    _fiMouseInContentBounds = typeof(DataGridViewButtonCell).GetField("mouseInContentBounds", BindingFlags.Static |
                                                                                                              BindingFlags.NonPublic |
                                                                                                              BindingFlags.GetField);
                }

                // Grab the internal property implemented by base class
                return (bool)_fiMouseInContentBounds.GetValue(this);
            }
        }

        private Point MouseEnteredCellAddressInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piMouseEnteredCellAddress == null)
                {
                    // Cache access to the internal get property 'MouseEnteredCellAddress'
                    _piMouseEnteredCellAddress = typeof(DataGridView).GetProperty("MouseEnteredCellAddress", BindingFlags.Instance |
                                                                                                             BindingFlags.NonPublic |
                                                                                                             BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (Point)_piMouseEnteredCellAddress.GetValue(base.DataGridView, null);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewLinkColumn cells.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDataGridViewLinkColumn), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
    public class KryptonDataGridViewLinkColumn : DataGridViewColumn
    {
        #region Static Fields
        private MethodInfo _miColumnCommonChange;
        private PropertyInfo _piUseColumnTextForLinkValueInternal;
        private PropertyInfo _piTrackVisitedStateInternal;
        #endregion

        #region Instance Fields
        private string _text;
        private LabelStyle _labelStyle;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewLinkColumn class.
        /// </summary>
        public KryptonDataGridViewLinkColumn()
            : base(new KryptonDataGridViewLinkCell())
        {
            // Define defaults
            _labelStyle = LabelStyle.NormalControl;
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewLinkColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// This member overrides DataGridViewButtonColumn.Clone.
        /// </summary>
        /// <returns>New object instance.</returns>
        public override object Clone()
        {
            // Create a new instance
            KryptonDataGridViewLinkColumn clone = base.Clone() as KryptonDataGridViewLinkColumn;
            clone.Text = Text;
            clone.LabelStyle = LabelStyle;
            return clone;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets the template used to model cell appearance.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                if ((value != null) && !(value is KryptonDataGridViewLinkCell))
                    throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewLinkCell");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the default text displayed on the link cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue((string)null)]
        public string Text
        {
            get { return _text; }
            set
            {
                if (!string.Equals(value, _text, StringComparison.Ordinal))
                {
                    _text = value;
                    if (DataGridView != null)
                    {
                        if (UseColumnTextForLinkValue)
                            ColumnCommonChange(Index);
                        else
                        {
                            DataGridViewRowCollection rows = DataGridView.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                KryptonDataGridViewLinkCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewLinkCell;
                                if ((cell != null) && cell.UseColumnTextForLinkValue)
                                {
                                    ColumnCommonChange(Index);
                                    return;
                                }
                            }
                            DataGridView.InvalidateColumn(Index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the default label style of link cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(LabelStyle), "NormalControl")]
        public LabelStyle LabelStyle
        {
            get { return _labelStyle; }
            set
            {
                if (_labelStyle != value)
                {
                    _labelStyle = value;
                    ((KryptonDataGridViewLinkCell)CellTemplate).LabelStyleInternal = value;
                    if (base.DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            KryptonDataGridViewLinkCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewLinkCell;
                            if (cell != null)
                                cell.LabelStyleInternal = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the behavior of links within cells in the column.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(LinkBehavior), "AlwaysUnderline")]
        public LinkBehavior LinkBehavior
        {
            get
            {
                if (CellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");

                return ((KryptonDataGridViewLinkCell)CellTemplate).LinkBehavior;
            }
            set
            {
                if (!LinkBehavior.Equals(value))
                {
                    ((KryptonDataGridViewLinkCell)CellTemplate).LinkBehaviorInternal = value;
                    if (base.DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            KryptonDataGridViewLinkCell cell = rows.SharedRow(i).Cells[Index] as KryptonDataGridViewLinkCell;
                            if (cell != null)
                                cell.LinkBehaviorInternal = value;
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the link changes color when it is visited.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool TrackVisitedState
        {
            get
            {
                if (CellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");

                return ((KryptonDataGridViewLinkCell)CellTemplate).TrackVisitedState;
            }
            set
            {
                if (TrackVisitedState != value)
                {
                    TrackVisitedStateInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewLinkCell cell = rows.SharedRow(i).Cells[Index] as DataGridViewLinkCell;
                            if (cell != null)
                                TrackVisitedStateInternal(cell, value);
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Text property value is displayed as the link text for cells in this column.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool UseColumnTextForLinkValue
        {
            get
            {
                if (CellTemplate == null)
                    throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");

                return ((KryptonDataGridViewLinkCell)CellTemplate).UseColumnTextForLinkValue;
            }

            set
            {
                if (UseColumnTextForLinkValue != value)
                {
                    SetUseColumnTextForLinkValueInternal(CellTemplate, value);
                    if (DataGridView != null)
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataGridViewLinkCell cell = rows.SharedRow(i).Cells[Index] as DataGridViewLinkCell;
                            if (cell != null)
                                SetUseColumnTextForLinkValueInternal(cell, value);
                        }
                        ColumnCommonChange(Index);
                    }
                }
            }
        }
        #endregion

        #region Private
        private void ColumnCommonChange(int columnIndex)
        {
            // Only need to cache reflection info the first time around
            if (_miColumnCommonChange == null)
            {
                // Cache access to the internal method 'OnColumnCommonChange'
                _miColumnCommonChange = typeof(DataGridView).GetMethod("OnColumnCommonChange", BindingFlags.Instance |
                                                                                               BindingFlags.NonPublic |
                                                                                               BindingFlags.GetField);

            }

            _miColumnCommonChange.Invoke(DataGridView, new object[] { columnIndex });
        }

        private void SetUseColumnTextForLinkValueInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piUseColumnTextForLinkValueInternal == null)
            {
                // Cache access to the internal property sette 'UseColumnTextForLinkValueInternal'
                _piUseColumnTextForLinkValueInternal = typeof(DataGridViewLinkCell).GetProperty("UseColumnTextForLinkValueInternal", BindingFlags.Instance |
                                                                                                                                     BindingFlags.NonPublic |
                                                                                                                                     BindingFlags.SetProperty);

            }

            _piUseColumnTextForLinkValueInternal.SetValue(instance, value, null);
        }

        private void TrackVisitedStateInternal(object instance, bool value)
        {
            // Only need to cache reflection info the first time around
            if (_piTrackVisitedStateInternal == null)
            {
                // Cache access to the internal property sette 'TrackVisitedStateInternal'
                _piTrackVisitedStateInternal = typeof(DataGridViewLinkCell).GetProperty("TrackVisitedStateInternal", BindingFlags.Instance |
                                                                                                                     BindingFlags.NonPublic |
                                                                                                                     BindingFlags.SetProperty);

            }

            _piTrackVisitedStateInternal.SetValue(instance, value, null);
        }
        #endregion
    }

    /// <summary>
    /// Displays a link label-like user interface (UI) for use in a DataGridView control.
    /// </summary>
    public class KryptonDataGridViewLinkCell : DataGridViewLinkCell
    {
        #region Static Fields
        private static PropertyInfo _piLinkState;
        #endregion

        #region Instance Fields
        private bool _linkDefined;
        private bool _labelStyleDefined;
        private LabelStyle _labelStyle;
        private PaletteContentToPalette _palette;
        private LinkLabelBehaviorInherit _inheritBehavior;
        private PaletteContentInheritOverride _overrideVisited;
        private PaletteContentInheritOverride _overridePressed;
        private ShortTextValue _shortTextValue;
        private ViewDrawContent _viewLabel;
        private Rectangle _contentBounds;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewLinkCell.
        /// </summary>
        public KryptonDataGridViewLinkCell()
        {
            _labelStyle = LabelStyle.NormalControl;
            base.LinkBehavior = LinkBehavior.AlwaysUnderline;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets a value that represents the behavior of links.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(typeof(LinkBehavior), "AlwaysUnderline")]
        public new LinkBehavior LinkBehavior
        {
            get { return base.LinkBehavior; }

            set
            {
                if (value != base.LinkBehavior)
                {
                    base.LinkBehavior = value;
                    _linkDefined = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a display style for drawing link cell.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(LabelStyle), "NormalControl")]
        public LabelStyle LabelStyle
        {
            get { return _labelStyle; }

            set
            {
                if (value != _labelStyle)
                {
                    _labelStyle = value;
                    _labelStyleDefined = true;
                    DataGridView.InvalidateCell(this);
                }
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's content area.
        /// </summary>
        /// <param name="graphics">Graphics instance for calculations.</param>
        /// <param name="cellStyle">Cell style to use in calculations.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <returns></returns>
        protected override Rectangle GetContentBounds(Graphics graphics,
                                                      DataGridViewCellStyle cellStyle,
                                                      int rowIndex)
        {
            // Return the cached bounds from last drawing cycle
            return _contentBounds;
        }

        /// <summary>
        /// This member overrides DataGridViewCell.GetPreferredSize. 
        /// </summary>
        /// <param name="graphics">Graphics instance used for calculations.</param>
        /// <param name="cellStyle">Individual cell style to apply.</param>
        /// <param name="rowIndex">Row of column being processed.</param>
        /// <param name="constraintSize">Maximum allowed size.</param>
        /// <returns>Requested ideal size for the cell.</returns>
        protected override Size GetPreferredSize(Graphics graphics,
                                                 DataGridViewCellStyle cellStyle,
                                                 int rowIndex,
                                                 Size constraintSize)
        {
            try
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Ensure the view classes are created and hooked up
                CreateViewAndPalettes(kDGV);

                // Update the element with the correct state and used palette
                SetElementStateAndPalette();

                // Update the display text
                if ((rowIndex >= 0) && (FormattedValue != null) && !string.IsNullOrEmpty(FormattedValue.ToString()))
                    _shortTextValue.ShortText = FormattedValue.ToString();
                else
                {
                    KryptonDataGridViewButtonColumn col = kDGV.Columns[ColumnIndex] as KryptonDataGridViewButtonColumn;
                    if ((col != null) && col.UseColumnTextForButtonValue && !kDGV.Rows[rowIndex].IsNewRow)
                        _shortTextValue.ShortText = col.Text;
                    else
                        _shortTextValue.ShortText = string.Empty;
                }

                // Position the button element inside the available cell area
                using (ViewLayoutContext layoutContext = new ViewLayoutContext(kDGV, kDGV.Renderer))
                {
                    // Define the available area for layout
                    layoutContext.DisplayRectangle = new Rectangle(0, 0, int.MaxValue, int.MaxValue);

                    // Get the ideal size of the label
                    Size labelSize = _viewLabel.GetPreferredSize(layoutContext);

                    // Add on the requested cell padding (plus add 1 to counter the -1 that occurs
                    // in the painting routine to prevent drawing over the bottom right border)
                    labelSize.Width += cellStyle.Padding.Horizontal + 1;
                    labelSize.Height += cellStyle.Padding.Vertical + 1;

                    return labelSize;
                }
            }
            catch
            {
                return Size.Empty;
            }
        }

        /// <summary>
        /// This member overrides DataGridViewCell.Paint.
        /// </summary>
        /// <param name="graphics">The Graphics used to paint the DataGridViewCell.</param>
        /// <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be repainted.</param>
        /// <param name="cellBounds">A Rectangle that contains the bounds of the DataGridViewCell that is being painted.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="cellState">A bitwise combination of DataGridViewElementStates values that specifies the state of the cell.</param>
        /// <param name="value">The data of the DataGridViewCell that is being painted.</param>
        /// <param name="formattedValue">The formatted data of the DataGridViewCell that is being painted.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="cellStyle">A DataGridViewCellStyle that contains formatting and style information about the cell.</param>
        /// <param name="advancedBorderStyle">A DataGridViewAdvancedBorderStyle that contains border styles for the cell that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of the DataGridViewPaintParts values that specifies which parts of the cell need to be painted.</param>
        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates cellState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if ((DataGridView != null) && (DataGridView is KryptonDataGridView))
            {
                KryptonDataGridView kDGV = (KryptonDataGridView)DataGridView;

                // Should we draw the content foreground?
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
                {
                    using (RenderContext renderContext = new RenderContext(kDGV, graphics, cellBounds, kDGV.Renderer))
                    {
                        // Cache the starting cell bounds
                        Rectangle startBounds = cellBounds;

                        // Ensure the view classes are created and hooked up
                        CreateViewAndPalettes(kDGV);

                        // Update the element with the correct state and used palette
                        SetElementStateAndPalette();

                        // Update the display text
                        if ((formattedValue != null) && !string.IsNullOrEmpty(formattedValue.ToString()))
                            _shortTextValue.ShortText = formattedValue.ToString();
                        else
                        {
                            KryptonDataGridViewButtonColumn col = kDGV.Columns[ColumnIndex] as KryptonDataGridViewButtonColumn;
                            if ((col != null) && col.UseColumnTextForButtonValue && !kDGV.Rows[rowIndex].IsNewRow)
                                _shortTextValue.ShortText = col.Text;
                            else
                                _shortTextValue.ShortText = string.Empty;
                        }

                        // Prevent button overlapping the bottom/right border
                        cellBounds.Width--;
                        cellBounds.Height--;

                        // Apply the padding
                        if (kDGV.RightToLeftInternal)
                            cellBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Bottom);
                        else
                            cellBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);

                        cellBounds.Width -= cellStyle.Padding.Horizontal;
                        cellBounds.Height -= cellStyle.Padding.Vertical;

                        // Position the button element inside the available cell area
                        using (ViewLayoutContext layoutContext = new ViewLayoutContext(kDGV, kDGV.Renderer))
                        {
                            // Define the available area for calculating layout
                            layoutContext.DisplayRectangle = cellBounds;

                            // Get the requests size for the label
                            Size contentSize = _viewLabel.GetPreferredSize(layoutContext);

                            // Adjust the horizontal alignment
                            switch (cellStyle.Alignment)
                            {
                                case DataGridViewContentAlignment.NotSet:
                                case DataGridViewContentAlignment.TopCenter:
                                case DataGridViewContentAlignment.MiddleCenter:
                                case DataGridViewContentAlignment.BottomCenter:
                                    cellBounds.X += (cellBounds.Width - contentSize.Width) / 2;
                                    break;
                                case DataGridViewContentAlignment.TopRight:
                                case DataGridViewContentAlignment.MiddleRight:
                                case DataGridViewContentAlignment.BottomRight:
                                    cellBounds.X = cellBounds.Right - contentSize.Width;
                                    break;
                            }

                            // Adjust the vertical alignment
                            switch (cellStyle.Alignment)
                            {
                                case DataGridViewContentAlignment.NotSet:
                                case DataGridViewContentAlignment.MiddleLeft:
                                case DataGridViewContentAlignment.MiddleCenter:
                                case DataGridViewContentAlignment.MiddleRight:
                                    cellBounds.Y += (cellBounds.Height - contentSize.Height) / 2;
                                    break;
                                case DataGridViewContentAlignment.BottomLeft:
                                case DataGridViewContentAlignment.BottomCenter:
                                case DataGridViewContentAlignment.BottomRight:
                                    cellBounds.Y = cellBounds.Bottom - contentSize.Height;
                                    break;
                            }

                            // Make the cell the same size as the check box itself
                            cellBounds.Width = Math.Min(contentSize.Width, cellBounds.Width);
                            cellBounds.Height = Math.Min(contentSize.Height, cellBounds.Height);

                            // Perform actual layout inside that area
                            layoutContext.DisplayRectangle = cellBounds;
                            _viewLabel.Layout(layoutContext);
                        }

                        // Ask the element to draw now
                        _viewLabel.Render(renderContext);

                        // Remember the current drawing bounds
                        _contentBounds = new Rectangle(cellBounds.X - startBounds.X,
                                                       cellBounds.Y - startBounds.Y,
                                                       cellBounds.Width, cellBounds.Height);
                    }
                }
            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                           cellState, value, formattedValue, errorText,
                           cellStyle, advancedBorderStyle, paintParts);
            }
        }
        #endregion

        #region Internal
        internal LinkBehavior LinkBehaviorInternal
        {
            set
            {
                if (!_linkDefined)
                    base.LinkBehavior = value;
            }
        }

        internal LabelStyle LabelStyleInternal
        {
            set
            {
                if (!_labelStyleDefined)
                    _labelStyle = value;
            }
        }
        #endregion

        #region Private
        private void CreateViewAndPalettes(KryptonDataGridView kDGV)
        {
            // Create the view element when first needed
            if (_viewLabel == null)
            {
                // Create helper object to get all values from the DGV redirector
                _palette = new PaletteContentToPalette(kDGV.Redirector, PaletteContentStyle.LabelNormalControl);
                _inheritBehavior = new LinkLabelBehaviorInherit(_palette, KryptonLinkBehavior.AlwaysUnderline);
                _overrideVisited = new PaletteContentInheritOverride(_palette, _inheritBehavior, PaletteState.LinkNotVisitedOverride, true);
                _overridePressed = new PaletteContentInheritOverride(_palette, _overrideVisited, PaletteState.LinkPressedOverride, false);

                // Provider of values for the button element
                _shortTextValue = new ShortTextValue();

                // Create view element for drawing the actual button
                _viewLabel = new ViewDrawContent(_overridePressed, _shortTextValue, VisualOrientation.Top);
            }
        }

        private void SetElementStateAndPalette()
        {
            LinkState linkState = LinkStateInternal;

            // Has the item been visited
            if (LinkVisited)
                _overrideVisited.OverrideState = PaletteState.LinkVisitedOverride;
            else
                _overrideVisited.OverrideState = PaletteState.LinkNotVisitedOverride;

            // Is the item being pressed?
            _overridePressed.Apply = ((linkState & LinkState.Active) == LinkState.Active);

            if ((linkState & LinkState.Hover) == LinkState.Hover)
                _viewLabel.ElementState = PaletteState.Tracking;
            else
                _viewLabel.ElementState = PaletteState.Normal;

            // Update with latest cell setting for the link behavior
            switch (base.LinkBehavior)
            {
                default:
                case LinkBehavior.AlwaysUnderline:
                    _inheritBehavior.LinkBehavior = KryptonLinkBehavior.AlwaysUnderline;
                    break;
                case LinkBehavior.HoverUnderline:
                    _inheritBehavior.LinkBehavior = KryptonLinkBehavior.HoverUnderline;
                    break;
                case LinkBehavior.NeverUnderline:
                    _inheritBehavior.LinkBehavior = KryptonLinkBehavior.NeverUnderline;
                    break;
            }

            // Use the latest defined label style
            _palette.ContentStyle = CommonHelper.ContentStyleFromLabelStyle(_labelStyle);
        }

        private LinkState LinkStateInternal
        {
            get
            {
                // Only need to cache reflection info the first time around
                if (_piLinkState == null)
                {
                    // Cache access to the internal get property 'LinkState'
                    _piLinkState = typeof(DataGridViewLinkCell).GetProperty("LinkState", BindingFlags.Instance |
                                                                                         BindingFlags.NonPublic |
                                                                                         BindingFlags.GetField);

                }

                // Grab the internal property implemented by base class
                return (LinkState)_piLinkState.GetValue(this, null);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewNumericUpDownCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonNumericUpDownColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewNumericUpDownColumn), "ToolboxBitmaps.KryptonNumericUpDown.bmp")]
    public class KryptonDataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        #region Instance Fields
        private DataGridViewColumnSpecCollection _buttonSpecs;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewNumericUpDownColumn class.
        /// </summary>
        public KryptonDataGridViewNumericUpDownColumn()
            : base(new KryptonDataGridViewNumericUpDownCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewNumericUpDownColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewNumericUpDownColumn cloned = base.Clone() as KryptonDataGridViewNumericUpDownColumn;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate;}
            set
            {
                KryptonDataGridViewNumericUpDownCell cell = value as KryptonDataGridViewNumericUpDownCell;
                if ((value != null) && (cell == null))
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewNumericUpDownCell or derive from it.");
                
                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Replicates the DecimalPlaces property of the KryptonDataGridViewNumericUpDownCell cell type.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("Indicates the number of decimal places to display.")]
        public int DecimalPlaces
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.DecimalPlaces;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                NumericUpDownCellTemplate.DecimalPlaces = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetDecimalPlaces(rowIndex, value);
                    }
                    
                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets wheather the numeric up-down should display its value in hexadecimal.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Indicates wheather the numeric up-down should display its value in hexadecimal.")]
        public bool Hexadecimal
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.Hexadecimal;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                NumericUpDownCellTemplate.Hexadecimal = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetHexadecimal(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the Increment property of the DataGridViewNumericUpDownCell cell type.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the amount to increment or decrement on each button click.")]
        public Decimal Increment
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.Increment;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                NumericUpDownCellTemplate.Increment = value;
                if (DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetIncrement(rowIndex, value);
                    }
                }
            }
        }

        /// Indicates whether the Increment property should be persisted.
        private bool ShouldSerializeIncrement()
        {
            return !Increment.Equals(Decimal.One);
        }

        /// <summary>
        /// Replicates the Maximum property of the KryptonDataGridViewNumericUpDownCell cell type.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the maximum value for the numeric up-down cells.")]
        [RefreshProperties(RefreshProperties.All)]
        public Decimal Maximum
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.Maximum;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                NumericUpDownCellTemplate.Maximum = value;
                if (DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMaximum(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// Indicates whether the Maximum property should be persisted.
        private bool ShouldSerializeMaximum()
        {
            return !Maximum.Equals((Decimal)100.0);
        }

        /// <summary>
        /// Replicates the Minimum property of the KryptonDataGridViewNumericUpDownCell cell type.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the minimum value for the numeric up-down cells.")]
        [RefreshProperties(RefreshProperties.All)]
        public Decimal Minimum
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.Minimum;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                NumericUpDownCellTemplate.Minimum = value;
                if (DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMinimum(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// Indicates whether the Maximum property should be persisted.
        private bool ShouldSerializeMinimum()
        {
            return !Minimum.Equals(Decimal.Zero);
        }

        /// <summary>
        /// Replicates the ThousandsSeparator property of the KryptonDataGridViewNumericUpDownCell cell type.
        /// </summary>
        [Category("Data")]
        [DefaultValue(false)]
        [Description("Indicates whether the thousands separator will be inserted between every three decimal digits.")]
        public bool ThousandsSeparator
        {
            get
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return NumericUpDownCellTemplate.ThousandsSeparator;
            }
            set
            {
                if (NumericUpDownCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                NumericUpDownCellTemplate.ThousandsSeparator = value;
                if (DataGridView != null)
                {
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewNumericUpDownCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewNumericUpDownCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetThousandsSeparator(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewNumericUpDownCell
        /// </summary>
        private KryptonDataGridViewNumericUpDownCell NumericUpDownCellTemplate
        {
            get { return (KryptonDataGridViewNumericUpDownCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Defines a KryptonNumericUpDown cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonNumericUpDown _paintingNumericUpDown;
        private static readonly DataGridViewContentAlignment _anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment _anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewNumericUpDownEditingControl);
        private static readonly Type _defaultValueType = typeof(System.Decimal);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Instance Fields
        private int _decimalPlaces;
        private Decimal _increment;
        private Decimal _minimum;
        private Decimal _maximum;
        private bool _thousandsSeparator;
        private bool _hexadecimal;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the DataGridViewNumericUpDownCell cell type
        /// </summary>
        public KryptonDataGridViewNumericUpDownCell()
        {
            // Create a thread specific KryptonNumericUpDown control used for the painting of the non-edited cells
            if (_paintingNumericUpDown == null)
            {
                _paintingNumericUpDown = new KryptonNumericUpDown();
                _paintingNumericUpDown.SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
                _paintingNumericUpDown.Maximum = Decimal.MaxValue / 10;
                _paintingNumericUpDown.Minimum = Decimal.MinValue / 10;
                _paintingNumericUpDown.StateCommon.Border.Width = 0;
                _paintingNumericUpDown.StateCommon.Border.Draw = InheritBool.False;
            }

            // Set the default values of the properties:
            _decimalPlaces = 0;
            _increment = Decimal.One;
            _minimum = Decimal.Zero;
            _maximum = (Decimal)100.0;
            _thousandsSeparator = false;
            _hexadecimal = false;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "DataGridViewNumericUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + 
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// The DecimalPlaces property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }

            set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.");

                if (_decimalPlaces != value)
                {
                    SetDecimalPlaces(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Indicates wheather the numeric up-down should display its value in hexadecimal.
        /// </summary>
        public bool Hexadecimal
        {
            get { return _hexadecimal; }

            set
            {
                if (_hexadecimal != value)
                {
                    SetHexadecimal(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Increment property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public Decimal Increment
        {
            get { return _increment; }

            set
            {
                if (value < (Decimal)0.0)
                    throw new ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.");

                SetIncrement(RowIndex, value);
            }
        }

        /// <summary>
        /// The Maximum property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public Decimal Maximum
        {
            get { return _maximum; }

            set
            {
                if (_maximum != value)
                {
                    SetMaximum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Minimum property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        public Decimal Minimum
        {
            get { return _minimum; }

            set
            {
                if (_minimum != value)
                {
                    SetMinimum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ThousandsSeparator property replicates the one from the KryptonNumericUpDown control
        /// </summary>
        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get { return _thousandsSeparator; }

            set
            {
                if (_thousandsSeparator != value)
                {
                    SetThousandsSeparator(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;
                
                return _defaultValueType;
            }
        }

        /// <summary>
        /// Clones a DataGridViewNumericUpDownCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewNumericUpDownCell dataGridViewCell = base.Clone() as KryptonDataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.DecimalPlaces = DecimalPlaces;
                dataGridViewCell.Increment = Increment;
                dataGridViewCell.Maximum = Maximum;
                dataGridViewCell.Minimum = Minimum;
                dataGridViewCell.ThousandsSeparator = ThousandsSeparator;
                dataGridViewCell.Hexadecimal = Hexadecimal;
            }
            return dataGridViewCell;
        }

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonNumericUpDown numericUpDown = dataGridView.EditingControl as KryptonNumericUpDown;
            if (numericUpDown != null)
            {
                KryptonDataGridViewNumericUpDownColumn numericColumn = OwningColumn as KryptonDataGridViewNumericUpDownColumn;
                if (numericColumn != null)
                {
                    foreach (ButtonSpec bs in numericUpDown.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    numericUpDown.ButtonSpecs.Clear();

                    TextBox textBox = numericUpDown.Controls[0].Controls[1] as TextBox;
                    if (textBox != null)
                        textBox.ClearUndo();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex, 
                                                      object initialFormattedValue, 
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonNumericUpDown numericUpDown = DataGridView.EditingControl as KryptonNumericUpDown;
            if (numericUpDown != null)
            {
                numericUpDown.DecimalPlaces = DecimalPlaces;
                numericUpDown.Increment = Increment;
                numericUpDown.Maximum = Maximum;
                numericUpDown.Minimum = Minimum;
                numericUpDown.ThousandsSeparator = ThousandsSeparator;
                numericUpDown.Hexadecimal = Hexadecimal;

                KryptonDataGridViewNumericUpDownColumn numericColumn = OwningColumn as KryptonDataGridViewNumericUpDownColumn;
                if (numericColumn != null)
                {
                    // Set this cell as the owner of the buttonspecs
                    numericUpDown.ButtonSpecs.Clear();
                    numericUpDown.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in numericColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        numericUpDown.ButtonSpecs.Add(bs);
                    }
                }

                string initialFormattedValueStr = initialFormattedValue as string;
                if (initialFormattedValueStr == null)
                    numericUpDown.Text = string.Empty;
                else
                    numericUpDown.Text = initialFormattedValueStr;
            }
        }

        /// <summary>
        /// Custom implementation of the KeyEntersEditMode function. This function is called by the DataGridView control
        /// to decide whether a keystroke must start an editing session or not. In this case, a new session is started when
        /// a digit or negative sign key is hit.
        /// </summary>
        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            Keys negativeSignKey = Keys.None;
            string negativeSignStr = numberFormatInfo.NegativeSign;
            if (!string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
                negativeSignKey = (Keys)(PI.VkKeyScan(negativeSignStr[0]));

            if ((char.IsDigit((char)e.KeyCode) ||
                 (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||
                 negativeSignKey == e.KeyCode ||
                 Keys.Subtract == e.KeyCode) &&
                !e.Shift && !e.Alt && !e.Control)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Customized implementation of the GetFormattedValue function in order to include the decimal and thousand separator
        /// characters in the formatted representation of the cell value.
        /// </summary>
        protected override object GetFormattedValue(object value,
                                                    int rowIndex,
                                                    ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter,
                                                    TypeConverter formattedValueTypeConverter,
                                                    DataGridViewDataErrorContexts context)
        {
            // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
            object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            string formattedNumber = formattedValue as string;
            if (!string.IsNullOrEmpty(formattedNumber) && value != null)
            {
                Decimal unformattedDecimal = System.Convert.ToDecimal(value);
                Decimal formattedDecimal = System.Convert.ToDecimal(formattedNumber);
                if (unformattedDecimal == formattedDecimal)
                {
                    // The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
                    // the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
                    // this may not be the actual string displayed. The real formatted value may be "1,234.500"
                    return formattedDecimal.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
                }
            }
            return formattedValue;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewNumericUpDownColumn numericColumn = OwningColumn as KryptonDataGridViewNumericUpDownColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(numericColumn, this, (ButtonSpecAny)sender);
            numericColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewNumericUpDownEditingControl EditingNumericUpDown
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewNumericUpDownEditingControl; }
        }

        private Decimal Constrain(Decimal value)
        {
            if (value < _minimum)
                value = _minimum;

            if (value > _maximum)
                value = _maximum;

            return value;
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, 
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingNumericUpDown.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingNumericUpDown(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewNumericUpDownEditingControl control = DataGridView.EditingControl as KryptonDataGridViewNumericUpDownEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal void SetDecimalPlaces(int rowIndex, int value)
        {
            _decimalPlaces = value;
            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.DecimalPlaces = value;
        }

        internal void SetHexadecimal(int rowIndex, bool value)
        {
            _hexadecimal = value;
            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.Hexadecimal = value;
        }

        internal void SetIncrement(int rowIndex, Decimal value)
        {
            _increment = value;
            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.Increment = value;
        }

        internal void SetMaximum(int rowIndex, Decimal value)
        {
            _maximum = value;
            if (_minimum > _maximum)
                _minimum = _maximum;

            object cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                Decimal currentValue = System.Convert.ToDecimal(cellValue);
                Decimal constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                    SetValue(rowIndex, constrainedValue);
            }

            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.Maximum = value;
        }

        internal void SetMinimum(int rowIndex, Decimal value)
        {
            _minimum = value;
            if (_minimum > _maximum)
                _maximum = value;

            object cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                Decimal currentValue = System.Convert.ToDecimal(cellValue);
                Decimal constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                    SetValue(rowIndex, constrainedValue);
            }

            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.Minimum = value;
        }

        internal void SetThousandsSeparator(int rowIndex, bool value)
        {
            _thousandsSeparator = value;
            if (OwnsEditingNumericUpDown(rowIndex))
                EditingNumericUpDown.ThousandsSeparator = value;
        }

        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & _anyRight) != 0)
                return HorizontalAlignment.Right;
            else if ((align & _anyCenter) != 0)
                return HorizontalAlignment.Center;
            else
                return HorizontalAlignment.Left;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewNumericUpDownCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewNumericUpDownEditingControl : KryptonNumericUpDown, 
                                                                  IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewNumericUpDownEditingControl class.
        /// </summary>
        public KryptonDataGridViewNumericUpDownEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
            TextAlign = KryptonDataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    {
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the begining of the string or if the entire text is selected 
                            // and we did not start editing, send this character to the dataGridView, else process the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Down:
                    if (Value > Minimum)
                        return true;
                    break;
                case Keys.Up:
                    if (Value < Maximum)
                        return true;
                    break;
                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength != textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Delete:
                    {
                        // Let the grid handle the key if the carret is at the end of the text.
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength > 0 ||
                                textBox.SelectionStart < textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            bool userEdit = UserEdit;
            try
            {
                // Prevent the Value from being set to Maximum or Minimum when the cell is being painted.
                UserEdit = (context & DataGridViewDataErrorContexts.Display) == 0;
                return Value.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
            }
            finally
            {
                UserEdit = userEdit;
            }
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            TextBox textBox = Controls[0].Controls[1] as TextBox;
            if (textBox != null)
            {
                if (selectAll)
                    textBox.SelectAll();
                else
                    textBox.SelectionStart = textBox.Text.Length;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the KeyPress notification to know when the value changed, and notify the grid of the change.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            bool notifyValueChange = false;
            if (char.IsDigit(e.KeyChar))
                notifyValueChange = true;
            else
            {
                NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
                string decimalSeparatorStr = numberFormatInfo.NumberDecimalSeparator;
                string groupSeparatorStr = numberFormatInfo.NumberGroupSeparator;
                string negativeSignStr = numberFormatInfo.NegativeSign;

                if (!string.IsNullOrEmpty(decimalSeparatorStr) && decimalSeparatorStr.Length == 1)
                    notifyValueChange = decimalSeparatorStr[0] == e.KeyChar;

                if (!notifyValueChange && !string.IsNullOrEmpty(groupSeparatorStr) && groupSeparatorStr.Length == 1)
                    notifyValueChange = groupSeparatorStr[0] == e.KeyChar;

                if (!notifyValueChange && !string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
                    notifyValueChange = negativeSignStr[0] == e.KeyChar;
            }

            if (notifyValueChange)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// Listen to the ValueChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            TextBox textBox = Controls[0].Controls[1] as TextBox;
            if (textBox != null)
            {
                PI.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewDomainUpDownCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonDomainUpDownColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewDomainUpDownColumn), "ToolboxBitmaps.KryptonDomainUpDown.bmp")]
    public class KryptonDataGridViewDomainUpDownColumn : DataGridViewColumn
    {
        #region Instance Fields
        private DataGridViewColumnSpecCollection _buttonSpecs;
        private StringCollection _items;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewDomainUpDownColumn class.
        /// </summary>
        public KryptonDataGridViewDomainUpDownColumn()
            : base(new KryptonDataGridViewDomainUpDownCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
            _items = new StringCollection();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewDomainUpDownColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewDomainUpDownColumn cloned = base.Clone() as KryptonDataGridViewDomainUpDownColumn;

            // Convert collection of strings to an array
            string[] strings = new string[Items.Count];
            for (int i = 0; i < strings.Length; i++)
                strings[i] = Items[i];

            cloned.Items.AddRange(strings);

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                KryptonDataGridViewDomainUpDownCell cell = value as KryptonDataGridViewDomainUpDownCell;
                if ((value != null) && (cell == null))
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewDomainUpDownCell or derive from it.");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets the collection of allowable items of the domain up down.
        /// </summary>
        [Category("Data")]
        [Description("The allowable items of the domain up down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public StringCollection Items
        {
            get { return _items; }
        }
        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewDomainUpDownCell
        /// </summary>
        private KryptonDataGridViewDomainUpDownCell DomainUpDownCellTemplate
        {
            get { return (KryptonDataGridViewDomainUpDownCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Defines a KryptonDomainUpDown cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewDomainUpDownCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonDomainUpDown _paintingDomainUpDown;
        private static readonly DataGridViewContentAlignment _anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment _anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDomainUpDownEditingControl);
        private static readonly Type _defaultValueType = typeof(System.String);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewDomainUpDownCell cell type
        /// </summary>
        public KryptonDataGridViewDomainUpDownCell()
        {
            // Create a thread specific KryptonNumericUpDown control used for the painting of the non-edited cells
            if (_paintingDomainUpDown == null)
            {
                _paintingDomainUpDown = new KryptonDomainUpDown();
                _paintingDomainUpDown.SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
                _paintingDomainUpDown.StateCommon.Border.Width = 0;
                _paintingDomainUpDown.StateCommon.Border.Draw = InheritBool.False;
            }
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "DataGridViewDomainUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;

                return _defaultValueType;
            }
        }

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonDomainUpDown domainUpDown = dataGridView.EditingControl as KryptonDomainUpDown;
            if (domainUpDown != null)
            {
                KryptonDataGridViewDomainUpDownColumn domainColumn = OwningColumn as KryptonDataGridViewDomainUpDownColumn;
                if (domainColumn != null)
                {
                    domainUpDown.Items.Clear();

                    foreach (ButtonSpec bs in domainUpDown.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    domainUpDown.ButtonSpecs.Clear();

                    TextBox textBox = domainUpDown.Controls[0].Controls[1] as TextBox;
                    if (textBox != null)
                        textBox.ClearUndo();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonDomainUpDown domainUpDown = DataGridView.EditingControl as KryptonDomainUpDown;
            if (domainUpDown != null)
            {
                domainUpDown.Items.Clear();
                domainUpDown.ButtonSpecs.Clear();

                KryptonDataGridViewDomainUpDownColumn domainColumn = OwningColumn as KryptonDataGridViewDomainUpDownColumn;
                if (domainColumn != null)
                {
                    domainUpDown.Items.InsertRange(0, domainColumn.Items);

                    // Set this cell as the owner of the buttonspecs
                    domainUpDown.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in domainColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        domainUpDown.ButtonSpecs.Add(bs);
                    }
                }

                string initialFormattedValueStr = initialFormattedValue as string;
                if (initialFormattedValueStr == null)
                    domainUpDown.Text = string.Empty;
                else
                    domainUpDown.Text = initialFormattedValueStr;
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewDomainUpDownColumn domainColumn = OwningColumn as KryptonDataGridViewDomainUpDownColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(domainColumn, this, (ButtonSpecAny)sender);
            domainColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewDomainUpDownEditingControl EditingDomainUpDown
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewDomainUpDownEditingControl; }
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingDomainUpDown.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingDomainUpDown(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewDomainUpDownEditingControl control = DataGridView.EditingControl as KryptonDataGridViewDomainUpDownEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & _anyRight) != 0)
                return HorizontalAlignment.Right;
            else if ((align & _anyCenter) != 0)
                return HorizontalAlignment.Center;
            else
                return HorizontalAlignment.Left;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewDomainUpDownCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewDomainUpDownEditingControl : KryptonDomainUpDown,
                                                                 IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewDomainUpDownEditingControl class.
        /// </summary>
        public KryptonDataGridViewDomainUpDownEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 0, 0, -1));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
            TextAlign = KryptonDataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    {
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the begining of the string or if the entire text is selected 
                            // and we did not start editing, send this character to the dataGridView, else process the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Down:
                case Keys.Up:
                    return true;
                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength != textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Delete:
                    {
                        // Let the grid handle the key if the carret is at the end of the text.
                        TextBox textBox = Controls[0].Controls[1] as TextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength > 0 ||
                                textBox.SelectionStart < textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            TextBox textBox = Controls[0].Controls[1] as TextBox;
            if (textBox != null)
            {
                if (selectAll)
                    textBox.SelectAll();
                else
                    textBox.SelectionStart = textBox.Text.Length;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            TextBox textBox = Controls[0].Controls[1] as TextBox;
            if (textBox != null)
            {
                PI.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewComboBoxCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonComboBoxColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewComboBoxColumn), "ToolboxBitmaps.KryptonComboBox.bmp")]
    public class KryptonDataGridViewComboBoxColumn : DataGridViewColumn
    {
        #region Instance Fields
        private StringCollection _items;
        private AutoCompleteStringCollection _autoCompleteCustom;
        private DataGridViewColumnSpecCollection _buttonSpecs;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewComboBoxColumn class.
        /// </summary>
        public KryptonDataGridViewComboBoxColumn()
            : base(new KryptonDataGridViewComboBoxCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
            _items = new StringCollection();
            _autoCompleteCustom = new AutoCompleteStringCollection();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewComboBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewComboBoxColumn cloned = base.Clone() as KryptonDataGridViewComboBoxColumn;

            // Convert collection of strings to an array
            string[] strings = new string[Items.Count];
            for (int i = 0; i < strings.Length; i++)
                strings[i] = Items[i];

            cloned.Items.AddRange(strings);

            // Convert collection of strings to an array
            strings = new string[AutoCompleteCustomSource.Count];
            for (int i = 0; i < strings.Length; i++)
                strings[i] = AutoCompleteCustomSource[i];

            cloned.AutoCompleteCustomSource.AddRange(strings);


            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                KryptonDataGridViewComboBoxCell cell = value as KryptonDataGridViewComboBoxCell;
                if ((value != null) && (cell == null))
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewComboBoxCell or derive from it.");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets the collection of allowable items of the domain up down.
        /// </summary>
        [Category("Data")]
        [Description("The allowable items of the domain up down.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        public StringCollection Items
        {
            get { return _items; }
        }

        private bool ShouldSerializeItems
        {
            get { return true; }
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Controls the appearance and functionality of the KryptonComboBox.")]
        [DefaultValue(typeof(ComboBoxStyle), "DropDown")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get 
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.DropDownStyle;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownStyle = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetDropDownStyle(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of entries to display in the drop-down list.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum number of entries to display in the drop-down list.")]
        [Localizable(true)]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get 
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.MaxDropDownItems;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.MaxDropDownItems = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The height, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(200)]
        [Browsable(true)]
        public int DropDownHeight
        {
            get 
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.DropDownHeight;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownHeight = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMaxDropDownItems(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the width, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The width, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int DropDownWidth
        {
            get 
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.DropDownWidth;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DropDownWidth = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetDropDownWidth(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return _autoCompleteCustom; }
        }

        private bool ShouldSerializeAutoCompleteCustomSource
        {
            get { return true; }
        }

        /// <summary>
        /// Gets or sets the text completion behavior of the combobox.
        /// </summary>
        [Description("Indicates the text completion behavior of the combobox.")]
        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.AutoCompleteMode;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.AutoCompleteMode = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetAutoCompleteMode(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
        /// </summary>
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [DefaultValue(typeof(AutoCompleteSource), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.AutoCompleteSource;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.AutoCompleteSource = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetAutoCompleteSource(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string DisplayMember
        {
            get
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return ComboBoxCellTemplate.DisplayMember;
            }

            set
            {
                if (ComboBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                ComboBoxCellTemplate.DisplayMember = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewComboBoxCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewComboBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewComboBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetDisplayMember(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewDomainUpDownCell
        /// </summary>
        private KryptonDataGridViewComboBoxCell ComboBoxCellTemplate
        {
            get { return (KryptonDataGridViewComboBoxCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Defines a KryptonComboBox cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewComboBoxCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonComboBox _paintingComboBox;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewComboBoxEditingControl);
        private static readonly Type _defaultValueType = typeof(System.String);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Instance Fields
        private ComboBoxStyle _dropDownStyle;
        private int _maxDropDownItems;
        private int _dropDownHeight;
        private int _dropDownWidth;
        private AutoCompleteMode _autoCompleteMode;
        private AutoCompleteSource _autoCompleteSource;
        private string _displayMember;
        private string _valueMember;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewComboBoxCell cell type
        /// </summary>
        public KryptonDataGridViewComboBoxCell()
        {
            // Create a thread specific KryptonComboBox control used for the painting of the non-edited cells
            if (_paintingComboBox == null)
            {
                _paintingComboBox = new KryptonComboBox();
                _paintingComboBox.SetLayoutDisplayPadding(new Padding(0, 1, 1, 0));
                _paintingComboBox.StateCommon.ComboBox.Border.Width = 0;
                _paintingComboBox.StateCommon.ComboBox.Border.Draw = InheritBool.False;
            }

            _dropDownStyle = ComboBoxStyle.DropDown;
            _maxDropDownItems = 8;
            _dropDownHeight = 200;
            _dropDownWidth = 121;
            _autoCompleteMode = AutoCompleteMode.None;
            _autoCompleteSource = AutoCompleteSource.None;
            _displayMember = string.Empty;
            _valueMember = string.Empty;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "KryptonDataGridViewComboBoxCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;

                return _defaultValueType;
            }
        }

        /// <summary>
        /// Clones a DataGridViewComboBoxCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewComboBoxCell dataGridViewCell = base.Clone() as KryptonDataGridViewComboBoxCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.DropDownStyle = DropDownStyle;
                dataGridViewCell.DropDownHeight = DropDownHeight;
                dataGridViewCell.DropDownWidth = DropDownWidth;
                dataGridViewCell.MaxDropDownItems = MaxDropDownItems;
                dataGridViewCell.AutoCompleteMode = AutoCompleteMode;
                dataGridViewCell.AutoCompleteSource = AutoCompleteSource;
                dataGridViewCell.DisplayMember = DisplayMember;
                dataGridViewCell.ValueMember = ValueMember;
            }
            return dataGridViewCell;
        }
        /// <summary>
        /// The DropDownStyle property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(0)]
        public ComboBoxStyle DropDownStyle
        {
            get { return _dropDownStyle; }

            set
            {
                if (value == ComboBoxStyle.Simple)
                    throw new ArgumentOutOfRangeException("The DropDownStyle property does not support the Simple style.");

                if (_dropDownStyle != value)
                {
                    SetDropDownStyle(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The MaxDropDownItems property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get { return _maxDropDownItems; }

            set
            {
                if (_maxDropDownItems != value)
                {
                    SetMaxDropDownItems(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The DropDownHeight property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(200)]
        public int DropDownHeight
        {
            get { return _dropDownHeight; }

            set
            {
                if (_dropDownHeight != value)
                {
                    SetDropDownHeight(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The DropDownWidth property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(121)]
        public int DropDownWidth
        {
            get { return _dropDownWidth; }

            set
            {
                if (DropDownWidth != value)
                {
                    SetDropDownWidth(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AutoCompleteMode property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(121)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _autoCompleteMode; }

            set
            {
                if (AutoCompleteMode != value)
                {
                    SetAutoCompleteMode(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AutoCompleteSource property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue(121)]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return _autoCompleteSource; }

            set
            {
                if (AutoCompleteSource != value)
                {
                    SetAutoCompleteSource(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The DisplayMember property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue("")]
        public string DisplayMember
        {
            get { return _displayMember; }

            set
            {
                if (_displayMember != value)
                {
                    SetDisplayMember(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ValueMember property replicates the one from the KryptonComboBox control
        /// </summary>
        [DefaultValue("")]
        public string ValueMember
        {
            get { return _valueMember; }

            set
            {
                if (_valueMember != value)
                {
                    SetValueMember(RowIndex, value);
                    OnCommonChange();
                }
            }
        }
        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonComboBox comboBox = dataGridView.EditingControl as KryptonComboBox;
            if (comboBox != null)
            {
                KryptonDataGridViewComboBoxColumn comboColumn = OwningColumn as KryptonDataGridViewComboBoxColumn;
                if (comboColumn != null)
                {
                    foreach (ButtonSpec bs in comboBox.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    comboBox.ButtonSpecs.Clear();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonComboBox comboBox = DataGridView.EditingControl as KryptonComboBox;
            if (comboBox != null)
            {
                KryptonDataGridViewComboBoxColumn comboColumn = OwningColumn as KryptonDataGridViewComboBoxColumn;
                if (comboColumn != null)
                {
                    // Convert collection of strings to an array
                    object[] strings = new object[comboColumn.Items.Count];
                    for (int i = 0; i < strings.Length; i++)
                        strings[i] = comboColumn.Items[i];

                    comboBox.Items.Clear();
                    comboBox.Items.AddRange(strings);

                    string[] autoAppend = new string[comboColumn.AutoCompleteCustomSource.Count];
                    for (int j = 0; j < autoAppend.Length; j++)
                        autoAppend[j] = comboColumn.AutoCompleteCustomSource[j];

                    comboBox.AutoCompleteCustomSource.Clear();
                    comboBox.AutoCompleteCustomSource.AddRange(autoAppend);

                    // Set this cell as the owner of the buttonspecs
                    comboBox.ButtonSpecs.Clear();
                    comboBox.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in comboColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        comboBox.ButtonSpecs.Add(bs);
                    }
                }

                comboBox.DropDownStyle = DropDownStyle;
                comboBox.DropDownHeight = DropDownHeight;
                comboBox.DropDownWidth = DropDownWidth;
                comboBox.MaxDropDownItems = MaxDropDownItems;
                comboBox.AutoCompleteSource = AutoCompleteSource;
                comboBox.AutoCompleteMode = AutoCompleteMode;
                comboBox.DisplayMember = DisplayMember;
                comboBox.ValueMember = ValueMember;

                string initialFormattedValueStr = initialFormattedValue as string;
                if (initialFormattedValueStr == null)
                    comboBox.Text = string.Empty;
                else
                    comboBox.Text = initialFormattedValueStr;
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewComboBoxColumn comboColumn = OwningColumn as KryptonDataGridViewComboBoxColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(comboColumn, this, (ButtonSpecAny)sender);
            comboColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewComboBoxEditingControl EditingComboBox
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewComboBoxEditingControl; }
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingComboBox.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingComboBox(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewComboBoxEditingControl control = DataGridView.EditingControl as KryptonDataGridViewComboBoxEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal void SetDropDownStyle(int rowIndex, ComboBoxStyle value)
        {
            _dropDownStyle = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.DropDownStyle = value;
        }

        internal void SetMaxDropDownItems(int rowIndex, int value)
        {
            _maxDropDownItems = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.MaxDropDownItems = value;
        }

        internal void SetDropDownHeight(int rowIndex, int value)
        {
            _dropDownHeight = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.DropDownHeight = value;
        }

        internal void SetDropDownWidth(int rowIndex, int value)
        {
            _dropDownWidth = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.DropDownWidth = value;
        }

        internal void SetAutoCompleteMode(int rowIndex, AutoCompleteMode value)
        {
            _autoCompleteMode = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.AutoCompleteMode = value;
        }

        internal void SetAutoCompleteSource(int rowIndex, AutoCompleteSource value)
        {
            _autoCompleteSource = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.AutoCompleteSource = value;
        }

        internal void SetDisplayMember(int rowIndex, string value)
        {
            _displayMember = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.DisplayMember = value;
        }

        internal void SetValueMember(int rowIndex, string value)
        {
            _valueMember = value;
            if (OwnsEditingComboBox(rowIndex))
                EditingComboBox.ValueMember = value;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewComboBoxCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewComboBoxEditingControl : KryptonComboBox,
                                                             IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewComboBoxEditingControl class.
        /// </summary>
        public KryptonDataGridViewComboBoxEditingControl()
        {
            TabStop = false;
            StateCommon.ComboBox.Border.Width = 0;
            StateCommon.ComboBox.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 1, 1, 0));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.ComboBox.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.ComboBox.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.ComboBox.Back.Color1 = dataGridViewCellStyle.BackColor;
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.Delete:
                    return true;
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// Listen to the SelectedIndexChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (SelectedIndex != -1)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonComboBox control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewDateTimePickerCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonDateTimePickerColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewDateTimePickerColumn), "ToolboxBitmaps.KryptonDateTimePicker.bmp")]
    public class KryptonDataGridViewDateTimePickerColumn : DataGridViewColumn
    {
        #region Instance Fields
        private DataGridViewColumnSpecCollection _buttonSpecs;
        private DateTimeList _annualDates;
        private DateTimeList _monthlyDates;
        private DateTimeList _dates;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewDateTimePickerColumn class.
        /// </summary>
        public KryptonDataGridViewDateTimePickerColumn()
            : base(new KryptonDataGridViewDateTimePickerCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
            _annualDates = new DateTimeList();
            _monthlyDates = new DateTimeList();
            _dates = new DateTimeList();
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewDateTimePickerColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewDateTimePickerColumn cloned = base.Clone() as KryptonDataGridViewDateTimePickerColumn;
            
            cloned.CalendarAnnuallyBoldedDates = CalendarAnnuallyBoldedDates;
            cloned.CalendarMonthlyBoldedDates = CalendarMonthlyBoldedDates;
            cloned.CalendarBoldedDates = CalendarBoldedDates;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }

            set
            {
                KryptonDataGridViewDateTimePickerCell cell = value as KryptonDataGridViewDateTimePickerCell;
                if ((value != null) && (cell == null))
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewDateTimePickerCell or derive from it.");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Replicates the ShowCheckBox property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether a check box is displayed in the control. When the box is unchecked, no value is selected.")]
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.ShowCheckBox;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.ShowCheckBox = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetShowCheckBox(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the ShowUpDown property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether a spin box rather than a drop-down calendar is displayed for modifying the control value.")]
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.ShowUpDown;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.ShowUpDown = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetShowUpDown(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the Format property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether dates and times are displayed using standard or custom formatting.")]
        [DefaultValue(typeof(DateTimePickerFormat), "Long")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public DateTimePickerFormat Format
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.Format;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.Format = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetFormat(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Replicates the AutoShift property of the KryptonDataGridViewDateTimePickerCell cell type.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if keyboard input will automatically shift to the next input field.")]
        [DefaultValue(false)]
        public bool AutoShift
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.AutoShift;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.AutoShift = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetAutoShift(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is checked and if the ValueNullable is DBNull or a DateTime value.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
        [DefaultValue(true)]
        public bool Checked
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.Checked;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.Checked = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetChecked(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom date/time format string.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom format string used to format the date and/or time displayed in the control.")]
        [DefaultValue("")]
        public string CustomFormat
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CustomFormat;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CustomFormat = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCustomFormat(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom text to show when control is not checked.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom text to draw when the control is not checked. Provide an empty string for default action of showing the defined date.")]
        [DefaultValue(" ")]
        public string CustomNullText
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CustomNullText;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CustomNullText = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCustomNullText(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        public DateTime MaxDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.MaxDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.MaxDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMaxDate(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Should the MaxDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMaxDate()
        {
            return (MaxDate != DateTimePicker.MaximumDateTime) && (MaxDate != DateTime.MaxValue);
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTime.MaxValue;
        }

        /// <summary>
        /// Gets or sets the minimum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        public DateTime MinDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.MinDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.MinDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMinDate(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Should the MinDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMinDate()
        {
            return (MinDate != DateTimePicker.MinimumDateTime) && (MinDate != DateTime.MinValue);
        }

        private void ResetMinDate()
        {
            MinDate = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Specifies the number of rows and columns of months displayed.")]
        [DefaultValue(typeof(Size), "1,1")]
        public Size CalendarDimensions
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarDimensions;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarDimensions = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarDimensions(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the label text for todays text. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Text used as label for todays date.")]
        [DefaultValue("Today:")]
        public string CalendarTodayText
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarTodayText;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarTodayText = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarTodayText(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Reset the value of the CalendarTodayText property.
        /// </summary>
        public void ResetCalendarTodayText()
        {
            CalendarTodayText = "Today:";
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("First day of the week.")]
        [DefaultValue(typeof(Day), "Default")]
        public Day CalendarFirstDayOfWeek
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarFirstDayOfWeek;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarFirstDayOfWeek = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarFirstDayOfWeek(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarShowToday;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowToday = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarShowToday(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if clicking the Today button closes the drop down menu.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates if clicking the Today button closes the drop down menu.")]
        [DefaultValue(false)]
        public bool CalendarCloseOnTodayClick
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarCloseOnTodayClick;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarCloseOnTodayClick = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarCloseOnTodayClick(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarShowTodayCircle;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowTodayCircle = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarShowTodayCircle(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool CalendarShowWeekNumbers
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarShowWeekNumbers;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarShowWeekNumbers = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarShowWeekNumbers(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Today's date.")]
        public DateTime CalendarTodayDate
        {
            get
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return DateTimePickerCellTemplate.CalendarTodayDate;
            }
            set
            {
                if (DateTimePickerCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                DateTimePickerCellTemplate.CalendarTodayDate = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewDateTimePickerCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewDateTimePickerCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewDateTimePickerCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCalendarTodayDate(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        private void ResetCalendarTodayDate()
        {
            CalendarTodayDate = DateTime.Now.Date;
        }

        private bool ShouldSerializeCalendarTodayDate()
        {
            return (CalendarTodayDate != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which annual dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarAnnuallyBoldedDates
        {
            get { return _annualDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _annualDates.Clear();
                _annualDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarAnnuallyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarAnnuallyBoldedDates()
        {
            return (_annualDates.Count > 0);
        }

        private void ResetCalendarAnnuallyBoldedDates()
        {
            CalendarAnnuallyBoldedDates = null;
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which monthly dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarMonthlyBoldedDates
        {
            get { return _monthlyDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _monthlyDates.Clear();
                _monthlyDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarMonthlyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarMonthlyBoldedDates()
        {
            return (_monthlyDates.Count > 0);
        }

        private void ResetCalendarMonthlyBoldedDates()
        {
            CalendarMonthlyBoldedDates = null;
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarBoldedDates
        {
            get { return _dates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _dates.Clear();
                _dates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarBoldedDates()
        {
            return (_dates.Count > 0);
        }

        private void ResetCalendarBoldedDates()
        {
            CalendarBoldedDates = null;
        }
        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewDateTimePickerCell
        /// </summary>
        private KryptonDataGridViewDateTimePickerCell DateTimePickerCellTemplate
        {
            get { return (KryptonDataGridViewDateTimePickerCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Defines a KryptonDateTimePicker cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewDateTimePickerCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonDateTimePicker _paintingDateTime;
        private static DateTimeConverter _dtc = new DateTimeConverter();
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewDateTimePickerEditingControl);
        private static readonly Type _defaultValueType = typeof(System.DateTime);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Instance Fields
        private bool _showCheckBox;
        private bool _showUpDown;
        private bool _autoShift;
        private bool _checked;
        private string _customFormat;
        private string _customNullText;
        private DateTime _maxDate;
        private DateTime _minDate;
        private DateTimePickerFormat _format;
        private Size _calendarDimensions;
        private string _calendarTodayText;
        private Day _calendarFirstDayOfWeek;
        private bool _calendarShowToday;
        private bool _calendarCloseOnTodayClick;
        private bool _calendarShowTodayCircle;
        private bool _calendarShowWeekNumbers;
        private DateTime _calendarTodayDate;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewDateTimePickerCell cell type
        /// </summary>
        public KryptonDataGridViewDateTimePickerCell()
        {
            // Create a thread specific KryptonDateTimePicker control used for the painting of the non-edited cells
            if (_paintingDateTime == null)
            {
                _paintingDateTime = new KryptonDateTimePicker();
                _paintingDateTime.ShowBorder = false;
                _paintingDateTime.StateCommon.Border.Width = 0;
                _paintingDateTime.StateCommon.Border.Draw = InheritBool.False;
            }

            // Set the default values of the properties:
            _showCheckBox = false;
            _showUpDown = false;
            _autoShift = false;
            _checked = false;
            _customFormat = string.Empty;
            _customNullText = " ";
            _maxDate = DateTime.MaxValue;
            _minDate = DateTime.MinValue;
            _format = DateTimePickerFormat.Long;
            _calendarDimensions = new Size(1,1);
            _calendarTodayText = "Today:";
            _calendarFirstDayOfWeek = Day.Default;
            _calendarShowToday = true;
            _calendarCloseOnTodayClick = false;
            _calendarShowTodayCircle = true;
            _calendarShowWeekNumbers = false;
            _calendarTodayDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "KryptonDataGridViewDateTimePickerCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;

                return _defaultValueType;
            }
        }

        /// <summary>
        /// Clones a DataGridViewDateTimePickerCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewDateTimePickerCell dateTimeCell = base.Clone() as KryptonDataGridViewDateTimePickerCell;
            if (dateTimeCell != null)
            {
                dateTimeCell.AutoShift = AutoShift;
                dateTimeCell.Checked = Checked;
                dateTimeCell.ShowCheckBox = ShowCheckBox;
                dateTimeCell.ShowUpDown = ShowUpDown;
                dateTimeCell.CustomFormat = CustomFormat;
                dateTimeCell.CustomNullText = CustomNullText;
                dateTimeCell.MaxDate = MaxDate;
                dateTimeCell.MinDate = MinDate;
                dateTimeCell.Format = Format;
                dateTimeCell.CalendarDimensions = CalendarDimensions;
                dateTimeCell.CalendarTodayText = CalendarTodayText;
                dateTimeCell.CalendarFirstDayOfWeek = CalendarFirstDayOfWeek;
                dateTimeCell.CalendarShowToday = CalendarShowToday;
                dateTimeCell.CalendarCloseOnTodayClick = CalendarCloseOnTodayClick;
                dateTimeCell.CalendarShowTodayCircle = CalendarShowTodayCircle;
                dateTimeCell.CalendarShowWeekNumbers = CalendarShowWeekNumbers;
                dateTimeCell.CalendarTodayDate = CalendarTodayDate;
            }
            return dateTimeCell;
        }

        /// <summary>
        /// The ShowCheckBox property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get { return _showCheckBox; }

            set
            {
                if (_showCheckBox != value)
                {
                    SetShowCheckBox(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ShowUpDown property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get { return _showUpDown; }

            set
            {
                if (_showUpDown != value)
                {
                    SetShowUpDown(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AutoShift property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool AutoShift
        {
            get { return _autoShift; }

            set
            {
                if (_autoShift != value)
                {
                    SetAutoShift(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Checked property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _checked; }

            set
            {
                if (_checked != value)
                {
                    SetChecked(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CustomFormat property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue("")]
        public string CustomFormat
        {
            get { return _customFormat; }

            set
            {
                if (_customFormat != value)
                {
                    SetCustomFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CustomNullText property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(" ")]
        public string CustomNullText
        {
            get { return _customNullText; }

            set
            {
                if (_customNullText != value)
                {
                    SetCustomNullText(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The MaxDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        public DateTime MaxDate
        {
            get { return _maxDate; }

            set
            {
                if (_maxDate != value)
                {
                    SetMaxDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Should the MaxDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMaxDate()
        {
            return (MaxDate != DateTimePicker.MaximumDateTime) && (MaxDate != DateTime.MaxValue);
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTime.MaxValue;
        }

        /// <summary>
        /// The MaxDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        public DateTime MinDate
        {
            get { return _minDate; }

            set
            {
                if (_minDate != value)
                {
                    SetMinDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Should the MinDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMinDate()
        {
            return (MinDate != DateTimePicker.MinimumDateTime) && (MinDate != DateTime.MinValue);
        }

        private void ResetMinDate()
        {
            MinDate = DateTime.MinValue;
        }

        /// <summary>
        /// The Format property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(DateTimePickerFormat), "Long")]
        public DateTimePickerFormat Format
        {
            get { return _format; }

            set
            {
                if (_format != value)
                {
                    SetFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarDimensions property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(Size), "1,1")]
        public Size CalendarDimensions
        {
            get { return _calendarDimensions; }

            set
            {
                if (_calendarDimensions != value)
                {
                    SetCalendarDimensions(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarTodayText property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue("Today:")]
        public string CalendarTodayText
        {
            get { return _calendarTodayText; }

            set
            {
                if (_calendarTodayText != value)
                {
                    SetCalendarTodayText(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarFirstDayOfWeek property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(typeof(Day), "Default")]
        public Day CalendarFirstDayOfWeek
        {
            get { return _calendarFirstDayOfWeek; }

            set
            {
                if (_calendarFirstDayOfWeek != value)
                {
                    SetCalendarFirstDayOfWeek(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarShowToday property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get { return _calendarShowToday; }

            set
            {
                if (_calendarShowToday != value)
                {
                    SetCalendarShowToday(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarCloseOnTodayClick property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarCloseOnTodayClick
        {
            get { return _calendarCloseOnTodayClick; }

            set
            {
                if (_calendarCloseOnTodayClick != value)
                {
                    SetCalendarCloseOnTodayClick(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        
        /// <summary>
        /// The CalendarShowTodayCircle property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get { return _calendarShowTodayCircle; }

            set
            {
                if (_calendarShowTodayCircle != value)
                {
                    SetCalendarShowTodayCircle(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarShowWeekNumbers property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public bool CalendarShowWeekNumbers
        {
            get { return _calendarShowWeekNumbers; }

            set
            {
                if (_calendarShowWeekNumbers != value)
                {
                    SetCalendarShowWeekNumbers(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CalendarTodayDate property replicates the one from the KryptonDateTimePicker control
        /// </summary>
        [DefaultValue(true)]
        public DateTime CalendarTodayDate
        {
            get { return _calendarTodayDate; }

            set
            {
                if (_calendarTodayDate != value)
                {
                    SetCalendarTodayDate(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        private void ResetCalendarTodayDate()
        {
            CalendarTodayDate = DateTime.Now.Date;
        }

        private bool ShouldSerializeCalendarTodayDate()
        {
            return (CalendarTodayDate != DateTime.Now.Date);
        }
        
        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonDateTimePicker dateTimePicker = dataGridView.EditingControl as KryptonDateTimePicker;
            if (dateTimePicker != null)
            {
                KryptonDataGridViewDateTimePickerColumn dateTimeColumn = OwningColumn as KryptonDataGridViewDateTimePickerColumn;
                if (dateTimeColumn != null)
                {
                    foreach (ButtonSpec bs in dateTimePicker.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    dateTimePicker.ButtonSpecs.Clear();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonDateTimePicker editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonDateTimePicker dateTime = DataGridView.EditingControl as KryptonDateTimePicker;
            if (dateTime != null)
            {
                KryptonDataGridViewDateTimePickerColumn dateTimeColumn = OwningColumn as KryptonDataGridViewDateTimePickerColumn;
                if (dateTimeColumn != null)
                {
                    dateTime.ShowCheckBox = ShowCheckBox;
                    dateTime.ShowUpDown = ShowUpDown;
                    dateTime.AutoShift = AutoShift;
                    dateTime.Checked = Checked;
                    dateTime.CustomFormat = CustomFormat;
                    dateTime.CustomNullText = CustomNullText;
                    dateTime.MaxDate = MaxDate;
                    dateTime.MinDate = MinDate;
                    dateTime.Format = Format;
                    dateTime.CalendarDimensions = CalendarDimensions;
                    dateTime.CalendarTodayText = CalendarTodayText;
                    dateTime.CalendarFirstDayOfWeek = CalendarFirstDayOfWeek;
                    dateTime.CalendarShowToday = CalendarShowToday;
                    dateTime.CalendarCloseOnTodayClick = CalendarCloseOnTodayClick;
                    dateTime.CalendarShowTodayCircle = CalendarShowTodayCircle;
                    dateTime.CalendarShowWeekNumbers = CalendarShowWeekNumbers;
                    dateTime.CalendarTodayDate = CalendarTodayDate;
                    dateTime.CalendarAnnuallyBoldedDates = dateTimeColumn.CalendarAnnuallyBoldedDates;
                    dateTime.CalendarMonthlyBoldedDates = dateTimeColumn.CalendarMonthlyBoldedDates;
                    dateTime.CalendarBoldedDates = dateTimeColumn.CalendarBoldedDates;

                    // Set this cell as the owner of the buttonspecs
                    dateTime.ButtonSpecs.Clear();
                    dateTime.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in dateTimeColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        dateTime.ButtonSpecs.Add(bs);
                    }
                }

                string initialFormattedValueStr = initialFormattedValue as string;
                if ((initialFormattedValueStr == null) || string.IsNullOrEmpty(initialFormattedValueStr))
                    dateTime.ValueNullable = null;
                else
                {
                    DateTime dt = (DateTime)_dtc.ConvertFromInvariantString(initialFormattedValueStr);
                    if (dt != null)
                        dateTime.Value = dt;
                    else
                        dateTime.Text = initialFormattedValueStr;
                }
            }
        }

        /// <summary>
        /// Gets the value of the cell as formatted for display. 
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="rowIndex">The index of the cell's parent row.</param>
        /// <param name="cellStyle">The DataGridViewCellStyle in effect for the cell.</param>
        /// <param name="valueTypeConverter">A TypeConverter associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed</param>
        /// <param name="formattedValueTypeConverter">A TypeConverter associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
        /// <param name="context">A bitwise combination of DataGridViewDataErrorContexts values describing the context in which the formatted value is needed.</param>
        /// <returns></returns>
        protected override object GetFormattedValue(object value, int rowIndex, 
                                                    ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter, 
                                                    TypeConverter formattedValueTypeConverter, 
                                                    DataGridViewDataErrorContexts context)
        {
            if ((value == null) || (value == DBNull.Value))
                return string.Empty;
            else
            {
                DateTime dt = (DateTime)value;
                if (dt != null)
                    return _dtc.ConvertToInvariantString(dt);
            }

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        /// <summary>
        /// Converts a value formatted for display to an actual cell value.
        /// </summary>
        /// <param name="formattedValue">The display value of the cell.</param>
        /// <param name="cellStyle">The DataGridViewCellStyle in effect for the cell.</param>
        /// <param name="formattedValueTypeConverter">A TypeConverter for the display value type, or null to use the default converter.</param>
        /// <param name="valueTypeConverter">A TypeConverter for the cell value type, or null to use the default converter.</param>
        /// <returns></returns>
        public override object ParseFormattedValue(object formattedValue, 
                                                   DataGridViewCellStyle cellStyle, 
                                                   TypeConverter formattedValueTypeConverter, 
                                                   TypeConverter valueTypeConverter)
        {
            if (formattedValue == null)
                return DBNull.Value;
            else
            {
                string stringValue = (string)formattedValue;
                if (string.IsNullOrEmpty(stringValue))
                    return DBNull.Value;
                else
                    return _dtc.ConvertFromInvariantString(stringValue);
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }

        /// <summary>
        /// Custom paints the cell. The base implementation of the DataGridViewTextBoxCell type is called first,
        /// dropping the icon error and content foreground parts. Those two parts are painted by this custom implementation.
        /// In this sample, the non-edited KryptonDateTimePicker control is painted by using a call to Control.DrawToBitmap. This is
        /// an easy solution for painting controls but it's not necessarily the most performant. An alternative would be to paint
        /// the KryptonDateTimePicker control piece by piece (text and up/down buttons).
        /// </summary>
        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates cellState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if (DataGridView == null)
                return;

            _paintingDateTime.RightToLeft = DataGridView.RightToLeft;
            _paintingDateTime.Format = Format;
            _paintingDateTime.CustomFormat = CustomFormat;
            _paintingDateTime.CustomNullText = CustomNullText;
            _paintingDateTime.MaxDate = MaxDate;
            _paintingDateTime.MinDate = MinDate;

            string drawText = CustomNullText;
            if ((value == null) || (value == DBNull.Value))
            {
                _paintingDateTime.ValueNullable = value;
                _paintingDateTime.PerformLayout();
            }
            else
            {
                _paintingDateTime.Value = (DateTime)value;
                _paintingDateTime.PerformLayout();
                drawText = _paintingDateTime.Text;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                       cellState, value, drawText, errorText,
                       cellStyle, advancedBorderStyle, paintParts);
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewDateTimePickerColumn dateTimeColumn = OwningColumn as KryptonDataGridViewDateTimePickerColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(dateTimeColumn, this, (ButtonSpecAny)sender);
            dateTimeColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewDateTimePickerEditingControl EditingDateTimePicker
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewDateTimePickerEditingControl; }
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingDateTime.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingDateTimePicker(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewDateTimePickerEditingControl control = DataGridView.EditingControl as KryptonDataGridViewDateTimePickerEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal void SetShowCheckBox(int rowIndex, bool value)
        {
            _showCheckBox = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.ShowCheckBox = value;
        }

        internal void SetShowUpDown(int rowIndex, bool value)
        {
            _showUpDown = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.ShowUpDown = value;
        }

        internal void SetAutoShift(int rowIndex, bool value)
        {
            _autoShift = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.AutoShift = value;
        }

        internal void SetChecked(int rowIndex, bool value)
        {
            _checked = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.Checked = value;
        }

        internal void SetCustomFormat(int rowIndex, string value)
        {
            _customFormat = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CustomFormat = value;
        }

        internal void SetCustomNullText(int rowIndex, string value)
        {
            _customNullText = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CustomNullText = value;
        }

        internal void SetMaxDate(int rowIndex, DateTime value)
        {
            _maxDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.MaxDate = value;
        }

        internal void SetMinDate(int rowIndex, DateTime value)
        {
            _minDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.MinDate = value;
        }

        internal void SetFormat(int rowIndex, DateTimePickerFormat value)
        {
            _format = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.Format = value;
        }

        internal void SetCalendarCloseOnTodayClick(int rowIndex, bool value)
        {
            _calendarCloseOnTodayClick = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarCloseOnTodayClick = value;
        }

        internal void SetCalendarDimensions(int rowIndex, Size value)
        {
            _calendarDimensions = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarDimensions = value;
        }

        internal void SetCalendarFirstDayOfWeek(int rowIndex, Day value)
        {
            _calendarFirstDayOfWeek = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarFirstDayOfWeek = value;
        }

        internal void SetCalendarShowToday(int rowIndex, bool value)
        {
            _calendarShowToday = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarShowToday = value;
        }

        internal void SetCalendarShowTodayCircle(int rowIndex, bool value)
        {
            _calendarShowTodayCircle = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarShowTodayCircle = value;
        }

        internal void SetCalendarShowWeekNumbers(int rowIndex, bool value)
        {
            _calendarShowWeekNumbers = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarShowWeekNumbers = value;
        }

        internal void SetCalendarTodayText(int rowIndex, string value)
        {
            _calendarTodayText = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarTodayText = value;
        }

        internal void SetCalendarTodayDate(int rowIndex, DateTime value)
        {
            _calendarTodayDate = value;
            if (OwnsEditingDateTimePicker(rowIndex))
                EditingDateTimePicker.CalendarTodayDate = value;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewKryptonDateTimePickerCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewDateTimePickerEditingControl : KryptonDateTimePicker,
                                                                   IDataGridViewEditingControl
    {
        #region Static Fields
        private static DateTimeConverter _dtc = new DateTimeConverter();
        #endregion

        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewDateTimePickerEditingControl class.
        /// </summary>
        public KryptonDataGridViewDateTimePickerEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            ShowBorder = false;
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            
            set 
            {
                if ((value == null) || (value == DBNull.Value))
                    ValueNullable = value;
                else
                {
                    string formattedValue = value as string;
                    if (string.IsNullOrEmpty(formattedValue))
                        ValueNullable = (formattedValue == string.Empty) ? null : value;
                    else
                        Value = (DateTime)_dtc.ConvertFromInvariantString(formattedValue);
                }
            }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.Delete:
                    return true;
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            if ((ValueNullable == null) || (ValueNullable == DBNull.Value))
                return String.Empty;
            else
                return _dtc.ConvertToInvariantString(Value);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the ValueNullableChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnValueNullableChanged(EventArgs e)
        {
            base.OnValueNullableChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Hosts a collection of KryptonDataGridViewMaskedTextBoxCell cells.
    /// </summary>
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonMaskedTextBoxColumnDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [ToolboxBitmap(typeof(KryptonDataGridViewMaskedTextBoxColumn), "ToolboxBitmaps.KryptonMaskedTextBox.bmp")]
    public class KryptonDataGridViewMaskedTextBoxColumn : DataGridViewColumn
    {
        #region Instance Fields
        private DataGridViewColumnSpecCollection _buttonSpecs;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a button spec.
        /// </summary>
        public event EventHandler<DataGridViewButtonSpecClickEventArgs> ButtonSpecClick;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDataGridViewMaskedTextBoxColumn class.
        /// </summary>
        public KryptonDataGridViewMaskedTextBoxColumn()
            : base(new KryptonDataGridViewMaskedTextBoxCell())
        {
            _buttonSpecs = new DataGridViewColumnSpecCollection(this);
        }

        /// <summary>
        /// Returns a standard compact string representation of the column.
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("KryptonDataGridViewMaskedTextBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>
        /// Create a cloned copy of the column.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            KryptonDataGridViewMaskedTextBoxColumn cloned = base.Clone() as KryptonDataGridViewMaskedTextBoxColumn;

            // Move the button specs over to the new clone
            foreach (ButtonSpec bs in ButtonSpecs)
                cloned.ButtonSpecs.Add(bs.Clone());

            return cloned;
        }
        #endregion

        #region Public
        /// <summary>
        /// Represents the implicit cell that gets cloned when adding rows to the grid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                KryptonDataGridViewMaskedTextBoxCell cell = value as KryptonDataGridViewMaskedTextBoxCell;
                if ((value != null) && (cell == null))
                    throw new InvalidCastException("Value provided for CellTemplate must be of type KryptonDataGridViewMaskedTextBoxCell or derive from it.");

                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// Gets the collection of the button specifications.
        /// </summary>
        [Category("Data")]
        [Description("Set of extra button specs to appear with control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Replicates the PromptChar property of the KryptonDataGridViewMaskedTextBoxCell cell type.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the character used as the placeholder.")]
        [DefaultValue('_')]
        public char PromptChar
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.PromptChar;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.PromptChar = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetPromptChar(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PromptChar can be entered as valid data by the user.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the prompt character is valid as input.")]
        [DefaultValue(true)]
        public bool AllowPromptAsInput
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.AllowPromptAsInput;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.AllowPromptAsInput = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetAllowPromptAsInput(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the MaskedTextBox control accepts characters outside of the ASCII character set.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether only Ascii characters are valid as input.")]
        [DefaultValue(false)]
        public bool AsciiOnly
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.AsciiOnly;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.AsciiOnly = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetAsciiOnly(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the control will beep when an invalid character is typed.")]
        [DefaultValue(false)]
        public bool BeepOnError
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.BeepOnError;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.BeepOnError = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetBeepOnError(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat CutCopyMaskFormat
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.CutCopyMaskFormat;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.CutCopyMaskFormat = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetCutCopyMaskFormat(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether prompt characters are displayed when the control does not have focus.")]
        [DefaultValue(false)]
        public bool HidePromptOnLeave
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.HidePromptOnLeave;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.HidePromptOnLeave = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetHidePromptOnLeave(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        [DefaultValue(true)]
        public bool HideSelection
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.HideSelection;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.HideSelection = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetHideSelection(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the text insertion mode of the masked text box control.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the masked text box input character typing mode.")]
        [DefaultValue(typeof(InsertKeyMode), "Default")]
        public InsertKeyMode InsertKeyMode
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.InsertKeyMode;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.InsertKeyMode = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetInsertKeyMode(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the input mask to use at run time. 
        /// </summary>
        [Category("Behavior")]
        [Description("Sets the string governing the input allowed for the control.")]
        [DefaultValue("")]
        public string Mask
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.Mask;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.Mask = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetMask(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a the character to display for password input for single-line edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the character to display for password input for single-line edit controls.")]
        [DefaultValue('\0')]
        public char PasswordChar
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.PasswordChar;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.PasswordChar = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetPasswordChar(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.
        /// </summary>
        [Category("Behavior")]
        [Description("If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
        [DefaultValue(false)]
        public bool RejectInputOnFirstFailure
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.RejectInputOnFirstFailure;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.RejectInputOnFirstFailure = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetRejectInputOnFirstFailure(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines how an input character that matches the prompt character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
        [DefaultValue(true)]
        public bool ResetOnPrompt
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.ResetOnPrompt;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.ResetOnPrompt = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetResetOnPrompt(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines how a space input character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
        [DefaultValue(true)]
        public bool ResetOnSpace
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.ResetOnSpace;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.ResetOnSpace = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetResetOnSpace(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
        [DefaultValue(true)]
        public bool SkipLiterals
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.SkipLiterals;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.SkipLiterals = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetSkipLiterals(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat TextMaskFormat
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.TextMaskFormat;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.TextMaskFormat = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetTextMaskFormat(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                return MaskedTextBoxCellTemplate.UseSystemPasswordChar;
            }
            set
            {
                if (MaskedTextBoxCellTemplate == null)
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");

                // Update the template cell so that subsequent cloned cells use the new value.
                MaskedTextBoxCellTemplate.UseSystemPasswordChar = value;
                if (DataGridView != null)
                {
                    // Update all the existing KryptonDataGridViewNumericUpDownCell cells in the column accordingly.
                    DataGridViewRowCollection dataGridViewRows = DataGridView.Rows;
                    int rowCount = dataGridViewRows.Count;
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        // Be careful not to unshare rows unnecessarily. 
                        // This could have severe performance repercussions.
                        DataGridViewRow dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                        KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = dataGridViewRow.Cells[Index] as KryptonDataGridViewMaskedTextBoxCell;
                        if (dataGridViewCell != null)
                            dataGridViewCell.SetUseSystemPasswordChar(rowIndex, value);
                    }

                    DataGridView.InvalidateColumn(Index);
                }
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// Small utility function that returns the template cell as a KryptonDataGridViewMaskedTextBoxCell
        /// </summary>
        private KryptonDataGridViewMaskedTextBoxCell MaskedTextBoxCellTemplate
        {
            get { return (KryptonDataGridViewMaskedTextBoxCell)CellTemplate; }
        }
        #endregion

        #region Internal
        internal void PerfomButtonSpecClick(DataGridViewButtonSpecClickEventArgs args)
        {
            if (ButtonSpecClick != null)
                ButtonSpecClick(this, args);
        }
        #endregion
    }

    /// <summary>
    /// Defines a KryptonMaskedTextBox cell type for the KryptonDataGridView control
    /// </summary>
    public class KryptonDataGridViewMaskedTextBoxCell : DataGridViewTextBoxCell
    {
        #region Static Fields
        [ThreadStatic]
        private static KryptonMaskedTextBox _paintingMaskedTextBox;
        private static readonly DataGridViewContentAlignment _anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment _anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
        private static readonly Type _defaultEditType = typeof(KryptonDataGridViewMaskedTextBoxEditingControl);
        private static readonly Type _defaultValueType = typeof(System.String);
        private static readonly Size _sizeLarge = new Size(10000, 10000);
        #endregion

        #region Instance Fields
        private char _promptChar;
        private bool _allowPromptAsInput;
        private bool _asciiOnly;
        private bool _beepOnError;
        private MaskFormat _cutCopyMaskFormat;
        private bool _hidePromptOnLeave;
        private bool _hideSelection;
        private InsertKeyMode _insertKeyMode;
        private string _mask;
        private char _passwordChar;
        private bool _rejectInputOnFirstFailure;
        private bool _resetOnPrompt;
        private bool _resetOnSpace;
        private bool _skipLiterals;
        private MaskFormat _textMaskFormat;
        private bool _useSystemPasswordChar;
        #endregion

        #region Identity
        /// <summary>
        /// Constructor for the KryptonDataGridViewMaskedTextBoxCell cell type
        /// </summary>
        public KryptonDataGridViewMaskedTextBoxCell()
        {
            // Create a thread specific KryptonMaskedTextBox control used for the painting of the non-edited cells
            if (_paintingMaskedTextBox == null)
            {
                _paintingMaskedTextBox = new KryptonMaskedTextBox();
                _paintingMaskedTextBox.SetLayoutDisplayPadding(new Padding(0, 0, 1, -1));
                _paintingMaskedTextBox.StateCommon.Border.Width = 0;
                _paintingMaskedTextBox.StateCommon.Border.Draw = InheritBool.False;
                _paintingMaskedTextBox.StateCommon.Back.Color1 = Color.Empty;
            }

            // Set the default values of the properties:
            _promptChar = '_';
            _allowPromptAsInput = true;
            _asciiOnly = false;
            _beepOnError = false;
            _cutCopyMaskFormat = MaskFormat.IncludeLiterals;
            _hidePromptOnLeave = false;
            _hideSelection = true;
            _insertKeyMode = InsertKeyMode.Default;
            _mask = string.Empty;
            _passwordChar = '\0';
            _rejectInputOnFirstFailure = false;
            _resetOnPrompt = true;
            _resetOnSpace = true;
            _skipLiterals = true;
            _textMaskFormat = MaskFormat.IncludeLiterals;
            _useSystemPasswordChar = false;
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "DataGridViewMaskedTextBoxCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) +
                   ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get { return _defaultEditType; }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;

                if (valueType != null)
                    return valueType;

                return _defaultValueType;
            }
        }

        /// <summary>
        /// The PromptChar property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue('_')]
        public char PromptChar
        {
            get { return _promptChar; }

            set
            {
                if (_promptChar != value)
                {
                    SetPromptChar(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AllowPromptAsInput property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool AllowPromptAsInput
        {
            get { return _allowPromptAsInput; }

            set
            {
                if (_allowPromptAsInput != value)
                {
                    SetAllowPromptAsInput(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The AsciiOnly property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool AsciiOnly
        {
            get { return _asciiOnly; }

            set
            {
                if (_asciiOnly != value)
                {
                    SetAsciiOnly(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The BeepOnError property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool BeepOnError
        {
            get { return _beepOnError; }

            set
            {
                if (_beepOnError != value)
                {
                    SetBeepOnError(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The CutCopyMaskFormat property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat CutCopyMaskFormat
        {
            get { return _cutCopyMaskFormat; }

            set
            {
                if (_cutCopyMaskFormat != value)
                {
                    SetCutCopyMaskFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The HidePromptOnLeave property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool HidePromptOnLeave
        {
            get { return _hidePromptOnLeave; }

            set
            {
                if (_hidePromptOnLeave != value)
                {
                    SetHidePromptOnLeave(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The HideSelection property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool HideSelection
        {
            get { return _hideSelection; }

            set
            {
                if (_hideSelection != value)
                {
                    SetHideSelection(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The InsertKeyMode property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(typeof(InsertKeyMode), "Default")]
        public InsertKeyMode InsertKeyMode
        {
            get { return _insertKeyMode; }

            set
            {
                if (_insertKeyMode != value)
                {
                    SetInsertKeyMode(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Mask property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue("")]
        public string Mask
        {
            get { return _mask; }

            set
            {
                if (_mask != value)
                {
                    SetMask(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The PasswordChar property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue('\0')]
        public char PasswordChar
        {
            get { return _passwordChar; }

            set
            {
                if (_passwordChar != value)
                {
                    SetPasswordChar(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The RejectInputOnFirstFailure property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool RejectInputOnFirstFailure
        {
            get { return _rejectInputOnFirstFailure; }

            set
            {
                if (_rejectInputOnFirstFailure != value)
                {
                    SetRejectInputOnFirstFailure(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ResetOnPrompt property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool ResetOnPrompt
        {
            get { return _resetOnPrompt; }

            set
            {
                if (_resetOnPrompt != value)
                {
                    SetResetOnPrompt(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ResetOnSpace property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool ResetOnSpace
        {
            get { return _resetOnSpace; }

            set
            {
                if (_resetOnSpace != value)
                {
                    SetResetOnSpace(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The SkipLiterals property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(true)]
        public bool SkipLiterals
        {
            get { return _skipLiterals; }

            set
            {
                if (_skipLiterals != value)
                {
                    SetSkipLiterals(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The TextMaskFormat property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat TextMaskFormat
        {
            get { return _textMaskFormat; }

            set
            {
                if (_textMaskFormat != value)
                {
                    SetTextMaskFormat(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The UseSystemPasswordChar property replicates the one from the KryptonMaskedTextBox control
        /// </summary>
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return _useSystemPasswordChar; }

            set
            {
                if (_useSystemPasswordChar != value)
                {
                    SetUseSystemPasswordChar(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Clones a DataGridViewMaskedTextBoxCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            KryptonDataGridViewMaskedTextBoxCell dataGridViewCell = base.Clone() as KryptonDataGridViewMaskedTextBoxCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.PromptChar = PromptChar;
                dataGridViewCell.AllowPromptAsInput = AllowPromptAsInput;
                dataGridViewCell.AsciiOnly = AsciiOnly;
                dataGridViewCell.BeepOnError = BeepOnError;
                dataGridViewCell.CutCopyMaskFormat = CutCopyMaskFormat;
                dataGridViewCell.HidePromptOnLeave = HidePromptOnLeave;
                dataGridViewCell.HideSelection = HideSelection;
                dataGridViewCell.InsertKeyMode = InsertKeyMode;
                dataGridViewCell.Mask = Mask;
                dataGridViewCell.PasswordChar = PasswordChar;
                dataGridViewCell.RejectInputOnFirstFailure = RejectInputOnFirstFailure;
                dataGridViewCell.ResetOnPrompt = ResetOnPrompt;
                dataGridViewCell.ResetOnSpace = ResetOnSpace;
                dataGridViewCell.SkipLiterals = SkipLiterals;
                dataGridViewCell.TextMaskFormat = TextMaskFormat;
                dataGridViewCell.UseSystemPasswordChar = UseSystemPasswordChar;
            }
            return dataGridViewCell;
        }

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");

            KryptonMaskedTextBox maskedTextBox = dataGridView.EditingControl as KryptonMaskedTextBox;
            if (maskedTextBox != null)
            {
                KryptonDataGridViewMaskedTextBoxColumn maskedTextBoxColumn = OwningColumn as KryptonDataGridViewMaskedTextBoxColumn;
                if (maskedTextBoxColumn != null)
                {
                    foreach (ButtonSpec bs in maskedTextBox.ButtonSpecs)
                        bs.Click -= new EventHandler(OnButtonClick);
                    maskedTextBox.ButtonSpecs.Clear();

                    TextBox textBox = maskedTextBox.Controls[0] as TextBox;
                    if (textBox != null)
                        textBox.ClearUndo();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the KryptonNumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            KryptonMaskedTextBox maskedTextBox = DataGridView.EditingControl as KryptonMaskedTextBox;
            if (maskedTextBox != null)
            {
                maskedTextBox.PromptChar = PromptChar;
                maskedTextBox.AllowPromptAsInput = AllowPromptAsInput;
                maskedTextBox.AsciiOnly = AsciiOnly;
                maskedTextBox.BeepOnError = BeepOnError;
                maskedTextBox.CutCopyMaskFormat = CutCopyMaskFormat;
                maskedTextBox.HidePromptOnLeave = HidePromptOnLeave;
                maskedTextBox.HideSelection = HideSelection;
                maskedTextBox.InsertKeyMode = InsertKeyMode;
                maskedTextBox.Mask = Mask;
                maskedTextBox.PasswordChar = PasswordChar;
                maskedTextBox.RejectInputOnFirstFailure = RejectInputOnFirstFailure;
                maskedTextBox.ResetOnPrompt = ResetOnPrompt;
                maskedTextBox.ResetOnSpace = ResetOnSpace;
                maskedTextBox.SkipLiterals = SkipLiterals;
                maskedTextBox.TextMaskFormat = TextMaskFormat;
                maskedTextBox.UseSystemPasswordChar = UseSystemPasswordChar;

                KryptonDataGridViewMaskedTextBoxColumn maskedTextBoxColumn = OwningColumn as KryptonDataGridViewMaskedTextBoxColumn;
                if (maskedTextBoxColumn != null)
                {
                    // Set this cell as the owner of the buttonspecs
                    maskedTextBox.ButtonSpecs.Clear();
                    maskedTextBox.ButtonSpecs.Owner = DataGridView.Rows[rowIndex].Cells[ColumnIndex];
                    foreach (ButtonSpec bs in maskedTextBoxColumn.ButtonSpecs)
                    {
                        bs.Click += new EventHandler(OnButtonClick);
                        maskedTextBox.ButtonSpecs.Add(bs);
                    }
                }

                string initialFormattedValueStr = initialFormattedValue as string;
                if (initialFormattedValueStr == null)
                    maskedTextBox.Text = string.Empty;
                else
                    maskedTextBox.Text = initialFormattedValueStr;
            }
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                                    bool setSize,
                                                    Rectangle cellBounds,
                                                    Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle,
                                                    bool singleVerticalBorderAdded,
                                                    bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn,
                                                    bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = PositionEditingPanel(cellBounds, cellClip, cellStyle,
                                                                  singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                                                  isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            const int ButtonsWidth = 16;

            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
                errorIconBounds.X = errorIconBounds.Left + ButtonsWidth;
            else
                errorIconBounds.X = errorIconBounds.Left - ButtonsWidth;

            return errorIconBounds;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(-1, -1);

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                const int ButtonsWidth = 16; // Account for the width of the up/down buttons.
                const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += ButtonsWidth + ButtonMargin;
            }

            return preferredSize;
        }
        #endregion

        #region Private
        private void OnButtonClick(object sender, EventArgs e)
        {
            KryptonDataGridViewMaskedTextBoxColumn maskedColumn = OwningColumn as KryptonDataGridViewMaskedTextBoxColumn;
            DataGridViewButtonSpecClickEventArgs args = new DataGridViewButtonSpecClickEventArgs(maskedColumn, this, (ButtonSpecAny)sender);
            maskedColumn.PerfomButtonSpecClick(args);
        }

        private KryptonDataGridViewMaskedTextBoxEditingControl EditingMaskedTextBox
        {
            get { return DataGridView.EditingControl as KryptonDataGridViewMaskedTextBoxEditingControl; }
        }

        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds,
                                                          DataGridViewCellStyle cellStyle)
        {
            // Adjust the vertical location of the editing control:
            int preferredHeight = _paintingMaskedTextBox.GetPreferredSize(_sizeLarge).Height + 2;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                    DataGridView.InvalidateColumn(ColumnIndex);
                else
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
            }
        }

        private bool OwnsEditingMaskedTextBox(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
                return false;

            KryptonDataGridViewMaskedTextBoxEditingControl control = DataGridView.EditingControl as KryptonDataGridViewMaskedTextBoxEditingControl;
            return (control != null) && (rowIndex == ((IDataGridViewEditingControl)control).EditingControlRowIndex);
        }

        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }
        #endregion

        #region Internal
        internal void SetPromptChar(int rowIndex, char value)
        {
            _promptChar = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.PromptChar = value;
        }

        internal void SetAllowPromptAsInput(int rowIndex, bool value)
        {
            _allowPromptAsInput = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.AllowPromptAsInput = value;
        }

        internal void SetAsciiOnly(int rowIndex, bool value)
        {
            _asciiOnly = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.AsciiOnly = value;
        }

        internal void SetBeepOnError(int rowIndex, bool value)
        {
            _beepOnError = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.BeepOnError = value;
        }

        internal void SetCutCopyMaskFormat(int rowIndex, MaskFormat value)
        {
            _cutCopyMaskFormat = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.CutCopyMaskFormat = value;
        }

        internal void SetHidePromptOnLeave(int rowIndex, bool value)
        {
            _hidePromptOnLeave = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.HidePromptOnLeave = value;
        }

        internal void SetHideSelection(int rowIndex, bool value)
        {
            _hideSelection = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.HideSelection = value;
        }

        internal void SetInsertKeyMode(int rowIndex, InsertKeyMode value)
        {
            _insertKeyMode = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.InsertKeyMode = value;
        }

        internal void SetMask(int rowIndex, string value)
        {
            _mask = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.Mask = value;
        }

        internal void SetPasswordChar(int rowIndex, char value)
        {
            _passwordChar = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.PasswordChar = value;
        }

        internal void SetRejectInputOnFirstFailure(int rowIndex, bool value)
        {
            _rejectInputOnFirstFailure = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.RejectInputOnFirstFailure = value;
        }

        internal void SetResetOnPrompt(int rowIndex, bool value)
        {
            _resetOnPrompt = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.ResetOnPrompt = value;
        }

        internal void SetResetOnSpace(int rowIndex, bool value)
        {
            _resetOnSpace = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.ResetOnSpace = value;
        }

        internal void SetSkipLiterals(int rowIndex, bool value)
        {
            _skipLiterals = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.SkipLiterals = value;
        }

        internal void SetTextMaskFormat(int rowIndex, MaskFormat value)
        {
            _textMaskFormat = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.TextMaskFormat = value;
        }

        internal void SetUseSystemPasswordChar(int rowIndex, bool value)
        {
            _useSystemPasswordChar = value;
            if (OwnsEditingMaskedTextBox(rowIndex))
                EditingMaskedTextBox.UseSystemPasswordChar = value;
        }

        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & _anyRight) != 0)
                return HorizontalAlignment.Right;
            else if ((align & _anyCenter) != 0)
                return HorizontalAlignment.Center;
            else
                return HorizontalAlignment.Left;
        }
        #endregion
    }

    /// <summary>
    /// Defines the editing control for the DataGridViewMaskedTextBoxCell custom cell type.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonDataGridViewMaskedTextBoxEditingControl : KryptonMaskedTextBox,
                                                                  IDataGridViewEditingControl
    {
        #region Instance Fields
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the KryptonDataGridViewMaskedTextBoxEditingControl class.
        /// </summary>
        public KryptonDataGridViewMaskedTextBoxEditingControl()
        {
            TabStop = false;
            StateCommon.Border.Width = 0;
            StateCommon.Border.Draw = InheritBool.False;
            SetLayoutDisplayPadding(new Padding(0, 0, 1, -1));
        }
        #endregion

        #region Public
        /// <summary>
        /// Property which caches the grid that uses this editing control
        /// </summary>
        public virtual DataGridView EditingControlDataGridView
        {
            get { return _dataGridView; }
            set { _dataGridView = value; }
        }

        /// <summary>
        /// Property which represents the current formatted value of the editing control
        /// </summary>
        public virtual object EditingControlFormattedValue
        {
            get { return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting); }
            set { Text = (string)value; }
        }

        /// <summary>
        /// Property which represents the row in which the editing control resides
        /// </summary>
        public virtual int EditingControlRowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }

        /// <summary>
        /// Property which indicates whether the value of the editing control has changed or not
        /// </summary>
        public virtual bool EditingControlValueChanged
        {
            get { return _valueChanged; }
            set { _valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel, i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            StateCommon.Content.Font = dataGridViewCellStyle.Font;
            StateCommon.Content.Color1 = dataGridViewCellStyle.ForeColor;
            StateCommon.Back.Color1 = dataGridViewCellStyle.BackColor;
            TextAlign = KryptonDataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    {
                        MaskedTextBox textBox = Controls[0] as MaskedTextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the end of the string, let the DataGridView treat the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        MaskedTextBox textBox = Controls[0] as MaskedTextBox;
                        if (textBox != null)
                        {
                            // If the end of the selection is at the begining of the string or if the entire text is selected 
                            // and we did not start editing, send this character to the dataGridView, else process the key message
                            if ((RightToLeft == RightToLeft.No && !(textBox.SelectionLength == 0 && textBox.SelectionStart == 0)) ||
                                (RightToLeft == RightToLeft.Yes && !(textBox.SelectionLength == 0 && textBox.SelectionStart == textBox.Text.Length)))
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Down:
                case Keys.Up:
                    return true;
                case Keys.Home:
                case Keys.End:
                    {
                        // Let the grid handle the key if the entire text is selected.
                        MaskedTextBox textBox = Controls[0] as MaskedTextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength != textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case Keys.Delete:
                    {
                        // Let the grid handle the key if the carret is at the end of the text.
                        MaskedTextBox textBox = Controls[0] as MaskedTextBox;
                        if (textBox != null)
                        {
                            if (textBox.SelectionLength > 0 ||
                                textBox.SelectionStart < textBox.Text.Length)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }

            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            MaskedTextBox textBox = Controls[0] as MaskedTextBox;
            if (textBox != null)
            {
                if (selectAll)
                    textBox.SelectAll();
                else
                    textBox.SelectionStart = textBox.Text.Length;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Listen to the TextChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Focused)
                NotifyDataGridViewOfValueChange();
        }

        /// <summary>
        /// A few keyboard messages need to be forwarded to the inner textbox of the
        /// KryptonNumericUpDown control so that the first character pressed appears in it.
        /// </summary>
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            MaskedTextBox textBox = Controls[0] as MaskedTextBox;
            if (textBox != null)
            {
                PI.SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        #endregion

        #region Private
        private void NotifyDataGridViewOfValueChange()
        {
            if (!_valueChanged)
            {
                _valueChanged = true;
                _dataGridView.NotifyCurrentCellDirty(true);
            }
        }
        #endregion
    }
}
