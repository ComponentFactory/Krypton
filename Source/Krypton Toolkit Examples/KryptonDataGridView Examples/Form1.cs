// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonDataGridViewExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create some simple test data for display
            DateTime dt = DateTime.Now.Date;
            dtTestData.Rows.Add(dt, "Mr",   "Mark",     "(55) 5555-5555", "Single",     36, "Press!", true);
            dtTestData.Rows.Add(dt, "Mrs",  "Mary",     "(01) 2345-6789", "Married",    21, "Press!", false);
            dtTestData.Rows.Add(dt, "Miss", "Mandy",    "(03) 5555-1111", "Single",     44, "Press!", false);
            dtTestData.Rows.Add(dt, "Ms",   "Mercy",    "(99) 2211-2211", "Single",     25, "Press!", true);
            dtTestData.Rows.Add(dt, "Mr",   "Micheal",  "(07) 0070-0700", "Divorced",   35, "Press!", false);
            dtTestData.Rows.Add(dt, "Mrs",  "Marge",    "(10) 2311-2311", "Married",    80, "Press!", true);

            // Show selected data grid properties in the property grid
            propertyGrid.SelectedObject = new KryptonDataGridViewProxy(kryptonDataGridView1);
        }

        private void rbOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Blue;
        }

        private void rbOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Blue;
        }

        private void rbOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Silver;
        }

        private void rbOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Black;
        }

        private void rbOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalOffice2003;
        }

        private void rbSystem_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalSystem;
        }

        private void rbSparkle_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparkleBlue;
        }

        private void rbStyleList_CheckedChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.GridStyles.Style = DataGridViewStyle.List;
        }

        private void rbStyleSheet_CheckedChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.GridStyles.Style = DataGridViewStyle.Sheet;
        }

        private void rbStyleCustom_CheckedChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.GridStyles.Style = DataGridViewStyle.Custom1;
        }

        private void buttonRandomCellColors_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < kryptonDataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < kryptonDataGridView1.ColumnCount; j++)
                {
                    Color foreColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    Color backColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

                    kryptonDataGridView1.Rows[i].Cells[j].Style.BackColor = foreColor;
                    kryptonDataGridView1.Rows[i].Cells[j].Style.ForeColor = backColor;
                }
            }
        }

        private void buttonClearCellColors_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < kryptonDataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < kryptonDataGridView1.ColumnCount; j++)
                {
                    kryptonDataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Empty;
                    kryptonDataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Empty;
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
          var old = new Form1().kryptonDataGridView1;
          var changes = GetChangedProperties<DataGridView>(old, kryptonDataGridView1);
          lbPropChanges.Items.Clear();
          foreach (var s in changes)
            lbPropChanges.Items.Add(s);
        }

        public List<string> GetChangedProperties<T>(object a, object b)
        {
          if (a == null || b == null)
            throw new ArgumentNullException("You need to provide 2 non-null objects");

          var type = typeof(T);
          var allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
          var allSimpleProperties = allProperties.Where(pi => pi.CanWrite && IsSimpleType(pi.PropertyType));
          var unequalProperties =
            from   pi in allSimpleProperties
            let    AValue = type.GetProperty(pi.Name).GetValue(a, null)
            let    BValue = type.GetProperty(pi.Name).GetValue(b, null)
            where  AValue != BValue && (AValue == null || !AValue.Equals(BValue))
            select pi.Name;
          return unequalProperties.ToList();
        }

        public bool IsSimpleType(Type type)
        {
          if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
          {
            // nullable type, check if the nested type is simple.
            return IsSimpleType(type.GetGenericArguments()[0]);
          }
          return type.IsPrimitive
                 || type.IsEnum
                 || type == typeof(string)
                 || type == typeof(decimal);
        }
    }

    public class KryptonDataGridViewProxy
    {
        private KryptonDataGridView _grid;

        public KryptonDataGridViewProxy(KryptonDataGridView grid)
        {
            _grid = grid;
        }

        [Category("Appearance")]
        [Description("The height, in pixels, of the column headers row.")]
        public int ColumnHeadersHeight
        {
            get { return _grid.ColumnHeadersHeight; }
            set { _grid.ColumnHeadersHeight = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether the column headers row is displayed.")]
        public bool ColumnHeadersVisible
        {
            get { return _grid.ColumnHeadersVisible; }
            set { _grid.ColumnHeadersVisible = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether the column that contains row headers is displayed.")]
        public bool RowHeadersVisible
        {
            get { return _grid.RowHeadersVisible; }
            set { _grid.RowHeadersVisible = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether to show cell errors.")]
        public bool ShowCellErrors
        {
            get { return _grid.ShowCellErrors; }
            set { _grid.ShowCellErrors = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether or not ToolTips will show when the mouse pointer pauses on a cell.")]
        public bool ShowCellToolTips
        {
            get { return _grid.ShowCellToolTips; }
            set { _grid.ShowCellToolTips = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether or not the editing glyph is visible in the row header of the cell being edited.")]
        public bool ShowEditingIcon
        {
            get { return _grid.ShowEditingIcon; }
            set { _grid.ShowEditingIcon = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether row headers will display error glyphs for each row that contains a data entry error.")]
        public bool ShowRowErrors
        {
            get { return _grid.ShowRowErrors; }
            set { _grid.ShowRowErrors = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the option to add rows is displayed to the user.")]
        public bool AllowUserToAddRows
        {
            get { return _grid.AllowUserToAddRows; }
            set { _grid.AllowUserToAddRows = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the user is allowed to delete rows from the DataGridView.")]
        public bool AllowUserToDeleteRows
        {
            get { return _grid.AllowUserToDeleteRows; }
            set { _grid.AllowUserToDeleteRows = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether manual column repositioning is enabled.")]
        public bool AllowUserToOrderColumns
        {
            get { return _grid.AllowUserToOrderColumns; }
            set { _grid.AllowUserToOrderColumns = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether users can resize columns.")]
        public bool AllowUserToResizeColumns
        {
            get { return _grid.AllowUserToResizeColumns; }
            set { _grid.AllowUserToResizeColumns = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether users can resize rows.")]
        public bool AllowUserToResizeRows
        {
            get { return _grid.AllowUserToResizeRows; }
            set { _grid.AllowUserToResizeRows = value; }
        }

        [Category("Behavior")]
        [Description("Determines the behavior for adjusting the column headers height.")]
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get { return _grid.ColumnHeadersHeightSizeMode; }
            set { _grid.ColumnHeadersHeightSizeMode = value; }
        }

        [Category("Behavior")]
        [Description("Identifies the mode that determines the cell editing is started.")]
        public DataGridViewEditMode EditMode
        {
            get { return _grid.EditMode; }
            set { _grid.EditMode = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the user is allowed to selected more than one cell, row or column of hte DataGridView at a time.")]
        public bool MultiSelect
        {
            get { return _grid.MultiSelect; }
            set { _grid.MultiSelect = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the user can edit the cells of the DataGridView control.")]
        public bool ReadOnly
        {
            get { return _grid.ReadOnly; }
            set { _grid.ReadOnly = value; }
        }

        [Category("Behavior")]
        [Description("Determine the behavior for adjusting the row headers width.")]
        public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
        {
            get { return _grid.RowHeadersWidthSizeMode; }
            set { _grid.RowHeadersWidthSizeMode = value; }
        }

        [Category("Behavior")]
        [Description("Indicates how the cells of the DataGridView can be selected.")]
        public DataGridViewSelectionMode SelectionMode
        {
            get { return _grid.SelectionMode; }
            set { _grid.SelectionMode = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the TAB key moves the focus to the next control in the tab order rather than moving focus to the next cell in the control.")]
        public bool StandardTab
        {
            get { return _grid.StandardTab; }
            set { _grid.StandardTab = value; }
        }

        [Category("Layout")]
        [Description("Determines the auto size mode for the visible columns.")]
        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
        {
            get { return _grid.AutoSizeColumnsMode; }
            set { _grid.AutoSizeColumnsMode = value; }
        }

        [Category("Layout")]
        [Description("Determines the auto size mode for the visible rows.")]
        public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
        {
            get { return _grid.AutoSizeRowsMode; }
            set { _grid.AutoSizeRowsMode = value; }
        }

        [Category("Layout")]
        [Description("The width, in pixels, of the column that contains the row headers.")]
        public int RowHeadersWidth
        {
            get { return _grid.RowHeadersWidth; }
            set { _grid.RowHeadersWidth = value; }
        }

        [Category("Layout")]
        [Description("The type of scrollbars to display.")]
        public ScrollBars ScrollBars
        {
            get { return _grid.ScrollBars; }
            set { _grid.ScrollBars = value; }
        }

        [Category("Visuals")]
        [Description("Determine if the outer borders of the grid cells are drawn.")]
        public bool HideOuterBorders
        {
            get { return _grid.HideOuterBorders; }
            set { _grid.HideOuterBorders = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common data grid view appearance that other states can override.")]
        public PaletteDataGridViewRedirect StateCommon
        {
            get { return _grid.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled data grid view appearance.")]
        public PaletteDataGridViewAll StateDisabled
        {
            get { return _grid.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal data grid view appearance.")]
        public PaletteDataGridViewAll StateNormal
        {
            get { return _grid.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining tracking data grid view appearance.")]
        public PaletteDataGridViewHeaders StateTracking
        {
            get { return _grid.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed data grid view appearance.")]
        public PaletteDataGridViewHeaders StatePressed
        {
            get { return _grid.StatePressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining selected data grid view appearance.")]
        public PaletteDataGridViewCells StateSelected
        {
            get { return _grid.StateSelected; }
        }

        [Category("Visuals")]
        [Description("Set of grid styles.")]
        public DataGridViewStyles GridStyles
        {
            get { return _grid.GridStyles; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            get { return _grid.PaletteMode; }
            set { _grid.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _grid.Size; }
            set { _grid.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _grid.Location; }
            set { _grid.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _grid.Enabled; }
            set { _grid.Enabled = value; }
        }

  }
}
