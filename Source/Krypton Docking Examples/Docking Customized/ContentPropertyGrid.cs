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
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;

namespace DockingCustomized
{
    public partial class ContentPropertyGrid : UserControl
    {
        public ContentPropertyGrid()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Unhook from events so this control can be garbage collected
            KryptonManager.GlobalPaletteChanged -= new EventHandler(OnGlobalPaletteChanged);

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ContentPropertyGrid_Load(object sender, EventArgs e)
        {
            // Hook into global palette changes
            KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);

            // Set correct initial font for the property grid
            OnGlobalPaletteChanged(null, EventArgs.Empty);
        }

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // Use the current font from the global palette
            IPalette palette = KryptonManager.CurrentGlobalPalette;
            Font font = palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);
            propertyGrid1.Font = font;
        }
    }
}
