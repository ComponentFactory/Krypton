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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonSeparatorExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonSeparatorProxy(kryptonSeparator1);
        }

        private void separator_MouseDown(object sender, MouseEventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonSeparatorProxy(sender as KryptonSeparator);
        }

        private void separator_MoveRect(object sender, SplitterMoveRectMenuArgs e)
        {
            // Allow the splitter to move 50 pixels in each direction
            e.MoveRect = new Rectangle(e.MoveRect.X - 50,
                                       e.MoveRect.Y - 50,
                                       e.MoveRect.Width + 100,
                                       e.MoveRect.Height + 100);

            Output("MoveRect " + e.MoveRect.ToString());
        }

        private void separator_Moving(object sender, SplitterCancelEventArgs e)
        {
            Output("Moving" +
                   " SplitX:" + e.SplitX.ToString() +
                   " SplitY:" + e.SplitY.ToString() +
                   " MouseX:" + e.MouseCursorX.ToString() +
                   " MouseY:" + e.MouseCursorY.ToString());
        }

        private void separator_Moved(object sender, SplitterEventArgs e)
        {
            Output("Moved" +
                   " SplitX:" + e.SplitX.ToString() +
                   " SplitY:" + e.SplitY.ToString());
        }

        private void separator_NotMoved(object sender, EventArgs e)
        {
            Output("Not Moved");
        }

        private void Output(string str)
        {
            string newText = richTextBox1.Text;
            
            if (newText.Length > 10000) 
                newText = string.Empty;
            
            richTextBox1.Text = str + "\n" + newText;
        }

        private void office2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void office2010Silver_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void office2010Black_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void office2007Blue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void office2007Silver_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void office2007Black_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void sparkleBlue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }

        private void office2003_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
        }

        private void system_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonSeparatorProxy
    {
        private KryptonSeparator _separator;

        public KryptonSeparatorProxy(KryptonSeparator separator)
        {
            _separator = separator;
        }

        #region Public
        [Category("Visuals")]
        [Description("Separator background style.")]
        public PaletteBackStyle ContainerBackStyle
        {
            get { return _separator.ContainerBackStyle; }
            set { _separator.ContainerBackStyle = value; }
        }

        [Category("Visuals")]
        [Description("Separator style.")]
        public SeparatorStyle SeparatorStyle
        {
            get { return _separator.SeparatorStyle; }
            set { _separator.SeparatorStyle = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common separator appearance that other states can override.")]
        public PaletteSplitContainerRedirect StateCommon
        {
            get { return _separator.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled separator appearance.")]
        public PaletteSplitContainer StateDisabled
        {
            get { return _separator.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal separator appearance.")]
        public PaletteSplitContainer StateNormal
        {
            get { return _separator.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking separator appearance.")]
        public PaletteSeparatorPadding StateTracking
        {
            get { return _separator.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed separator appearance.")]
        public PaletteSeparatorPadding StatePressed
        {
            get { return _separator.StatePressed; }
        }

        [Category("Layout")]
        [Description("Determines the increment used for moving.")]
        public int SplitterIncrements
        {
            get { return _separator.SplitterIncrements; }
            set { _separator.SplitterIncrements = value; }
        }

        [Category("Layout")]
        [Description("Determines if the separator is vertical or horizontal.")]
        public Orientation Orientation
        {
            get { return _separator.Orientation; }
            set { _separator.Orientation = value; }
        }

        [Category("Behavior")]
        [Description("Determines if the separator is allowed to notify a move.")]
        public bool AllowMove
        {
            get { return _separator.AllowMove; }
            set { _separator.AllowMove = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _separator.PaletteMode; }
            set { _separator.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _separator.Size; }
            set { _separator.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _separator.Location; }
            set { _separator.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _separator.Enabled; }
            set { _separator.Enabled = value; }
        }
        #endregion
    }
}
