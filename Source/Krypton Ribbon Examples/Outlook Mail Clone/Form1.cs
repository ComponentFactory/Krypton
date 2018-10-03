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
using ComponentFactory.Krypton.Ribbon;

namespace OutlookMailClone
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void formatPlainText_Click(object sender, EventArgs e)
        {
            // If it has become checked, then ensure other options are no
            // longer in the checked state. If we are clicked to make unchecked
            // then override that behavior by forcing outself back to checked.
            if (formatPlainText.Checked)
            {
                formatHTML.Checked = false;
                formatRichText.Checked = false;
            }
            else
                formatPlainText.Checked = true;
        }

        private void formatHTML_Click(object sender, EventArgs e)
        {
            // If it has become checked, then ensure other options are no
            // longer in the checked state. If we are clicked to make unchecked
            // then override that behavior by forcing outself back to checked.
            if (formatHTML.Checked)
            {
                formatPlainText.Checked = false;
                formatRichText.Checked = false;
            }
            else
                formatHTML.Checked = true;
        }

        private void formatRichText_Click(object sender, EventArgs e)
        {
            // If it has become checked, then ensure other options are no
            // longer in the checked state. If we are clicked to make unchecked
            // then override that behavior by forcing outself back to checked.
            if (formatRichText.Checked)
            {
                formatPlainText.Checked = false;
                formatHTML.Checked = false;
            }
            else
                formatRichText.Checked = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
