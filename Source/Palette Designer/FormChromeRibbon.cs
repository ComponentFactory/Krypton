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
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Ribbon;

namespace PaletteDesigner
{
    public partial class FormChromeRibbon : KryptonForm
    {
        #region Identity
        public FormChromeRibbon()
        {
            InitializeComponent();
        }
        #endregion

        #region Public
        public KryptonPalette OverridePalette
        {
            set
            {
                Palette = value;
                kryptonPanel1.Palette = value;
                kryptonRibbon1.Palette = value;
            }
        }
        #endregion
    }
}
