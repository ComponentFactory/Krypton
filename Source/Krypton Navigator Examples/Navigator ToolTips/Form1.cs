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
using System.Diagnostics;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;

namespace NavigatorToolTips
{
    public partial class Form1 : Form
    {
        private MapKryptonPageTextConverter _textConv = new MapKryptonPageTextConverter();
        private MapKryptonPageImageConverter _imageConv = new MapKryptonPageImageConverter();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new MapKryptonPageImageConverter();

            // Populate the text/extra text mapping combo boxes
            foreach (MapKryptonPageText value in Enum.GetValues(typeof(MapKryptonPageText)))
            {
                comboMapText.Items.Add(_textConv.ConvertToInvariantString(value));
                comboMapExtraText.Items.Add(_textConv.ConvertToInvariantString(value));
            }

            // Populate the image mapping combo box
            foreach (MapKryptonPageImage value in Enum.GetValues(typeof(MapKryptonPageImage)))
                comboMapImage.Items.Add(_imageConv.ConvertToInvariantString(value));

            // Update with current navigator settings
            checkAllowPageTooltips.Checked = kryptonNavigator.ToolTips.AllowPageToolTips;
            checkAllowButtonSpecTooltips.Checked = kryptonNavigator.ToolTips.AllowButtonSpecToolTips;
            comboMapText.Text = _textConv.ConvertToInvariantString(kryptonNavigator.ToolTips.MapText);
            comboMapExtraText.Text = _textConv.ConvertToInvariantString(kryptonNavigator.ToolTips.MapExtraText);
            comboMapImage.Text = _imageConv.ConvertToInvariantString(kryptonNavigator.ToolTips.MapImage);
        }

        private void checkAllowPageTooltips_CheckedChanged(object sender, EventArgs e)
        {
            kryptonNavigator.ToolTips.AllowPageToolTips = checkAllowPageTooltips.Checked;
        }

        private void checkAllowButtonSpecTooltips_CheckedChanged(object sender, EventArgs e)
        {
            kryptonNavigator.ToolTips.AllowButtonSpecToolTips = checkAllowButtonSpecTooltips.Checked;
        }

        private void comboMapText_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonNavigator.ToolTips.MapText = (MapKryptonPageText)_textConv.ConvertFromInvariantString(comboMapText.Text);
        }

        private void comboMapExtraText_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonNavigator.ToolTips.MapExtraText = (MapKryptonPageText)_textConv.ConvertFromInvariantString(comboMapExtraText.Text);
        }

        private void comboMapImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonNavigator.ToolTips.MapImage = (MapKryptonPageImage)_imageConv.ConvertFromInvariantString(comboMapImage.Text);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
