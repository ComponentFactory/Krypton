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

namespace PaletteDesigner
{
    public partial class FormChromeTMS : KryptonForm
    {
        #region Identity
        public FormChromeTMS()
        {
            InitializeComponent();
        }
        #endregion

        #region Public
        public ToolStripRenderer OverrideToolStripRenderer
        {
            set
            {
                // Apply the new toolstrip renderer to the design page controls
                tmsMenuStrip.Renderer = value;
                tmsStatusStrip.Renderer = value;
                tmsToolStrip.Renderer = value;
                tmsToolStripContainer.TopToolStripPanel.Renderer = value;
                tmsToolStripContainer.BottomToolStripPanel.Renderer = value;
                tmsToolStripContainer.LeftToolStripPanel.Renderer = value;
                tmsToolStripContainer.RightToolStripPanel.Renderer = value;
                tmsToolStripContainer.ContentPanel.Renderer = value;
            }
        }
        #endregion
    }
}
