using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonTrackBarExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit the first track bar control
            propertyGrid.SelectedObject = new KryptonTrackBarProxy(kryptonTrackBar1);
        }

        private void trackBar_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this track bar control
            propertyGrid.SelectedObject = new KryptonTrackBarProxy(sender as KryptonTrackBar);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonTrackBarProxy
    {
        private KryptonTrackBar _trackBar;

        public KryptonTrackBarProxy(KryptonTrackBar trackBar)
        {
            _trackBar = trackBar;
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [DefaultValue(typeof(Padding), "0,0,0,0")]
        public Padding Padding
        {
            get { return _trackBar.Padding; }
            set { _trackBar.Padding = value; }
        }

        /// <summary>
        /// Gets and sets the background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Background style.")]
        public PaletteBackStyle BackStyle
        {
            get { return _trackBar.BackStyle; }
            set { _trackBar.BackStyle = value; }
        }

        /// <summary>
        /// Gets access to the track bar appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining track bar appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect OverrideFocus
        {
            get { return _trackBar.OverrideFocus; }
        }

        /// <summary>
        /// Gets access to the common trackbar appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common trackbar appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect StateCommon
        {
            get { return _trackBar.StateCommon; }
        }

        /// <summary>
        /// Gets access to the disabled trackbar appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled trackbar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateDisabled
        {
            get { return _trackBar.StateDisabled; }
        }

        /// <summary>
        /// Gets access to the normal trackbar appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal trackbar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateNormal
        {
            get { return _trackBar.StateNormal; }
        }

        /// <summary>
        /// Gets access to the tracking trackbar appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking trackbar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StateTracking
        {
            get { return _trackBar.StateTracking; }
        }

        /// <summary>
        /// Gets access to the pressed trackbar appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed trackbar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StatePressed
        {
            get { return _trackBar.StatePressed; }
        }

        /// <summary>
        /// Gets and sets the size of the track bar elements.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines size of the track bar elements.")]
        [DefaultValue(typeof(PaletteTrackBarSize), "Medium")]
        public PaletteTrackBarSize TrackBarSize
        {
            get { return _trackBar.TrackBarSize; }
            set { _trackBar.TrackBarSize = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how to display the tick marks on the track bar.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines where tick marks are displayed.")]
        [DefaultValue(typeof(TickStyle), "BottomRight")]
        [RefreshProperties(RefreshProperties.All)]
        public TickStyle TickStyle
        {
            get { return _trackBar.TickStyle; }
            set { _trackBar.TickStyle = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines the frequency of tick marks.")]
        [DefaultValue(1)]
        public int TickFrequency
        {
            get { return _trackBar.TickFrequency; }
            set { _trackBar.TickFrequency = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the horizontal or vertical orientation of the track bar.
        /// </summary>
        [Category("Appearance")]
        [Description("Background style.")]
        [DefaultValue(typeof(Orientation), "Horizontal")]
        public Orientation Orientation
        {
            get { return _trackBar.Orientation; }
            set { _trackBar.Orientation = value; }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range this trackbar is working with.
        /// </summary>
        [Category("Behavior")]
        [Description("Upper limit of the trackbar range.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(10)]
        public int Maximum
        {
            get { return _trackBar.Maximum; }
            set { _trackBar.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this trackbar is working with.
        /// </summary>
        [Category("Behavior")]
        [Description("Lower limit of the trackbar range.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        public int Minimum
        {
            get { return _trackBar.Minimum; }
            set { _trackBar.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
        /// </summary>
        [Category("Behavior")]
        [Description("Current position of the indicator within the trackbar.")]
        [DefaultValue(0)]
        public int Value
        {
            get { return _trackBar.Value; }
            set { _trackBar.Value = value; }
        }

        /// <summary>
        /// Gets or sets the value added to or subtracted from the Value property when the scroll box is moved a small distance.
        /// </summary>
        [Category("Behavior")]
        [Description("Change to apply when a small change occurs.")]
        [DefaultValue(1)]
        public int SmallChange
        {
            get { return _trackBar.SmallChange; }
            set { _trackBar.SmallChange = value; }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the Value property when the scroll box is moved a large distance.
        /// </summary>
        [Category("Behavior")]
        [Description("Change to apply when a large change occurs.")]
        [DefaultValue(5)]
        public int LargeChange
        {
            get { return _trackBar.LargeChange; }
            set { _trackBar.LargeChange = value; }
        }
    }
}
