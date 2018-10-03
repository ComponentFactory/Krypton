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

namespace NavigatorAndFloatingWindows
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int _count = 1;
        private KryptonPage NewPage(string name, int image, Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name + _count.ToString();
            p.TextTitle = name + _count.ToString();
            p.TextDescription = name + _count.ToString();
            p.UniqueName = p.Text;
            p.ImageSmall = imageListSmall.Images[image];

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private KryptonPage NewDocument()
        {
            KryptonPage page = NewPage("Document ", 0, new ContentDocument());

            // Do not allow the document pages to be closed or made auto hidden/docked
            page.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden |
                            KryptonPageFlags.DockingAllowDocked |
                            KryptonPageFlags.DockingAllowClose);

            return page;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingNavigator n = kryptonDockingManager.ManageNavigator(kryptonDockableNavigator);
            kryptonDockingManager.ManageFloating(this);

            // Add initial floating window and navigator documents
            kryptonDockingManager.AddFloatingWindow("Floating", new KryptonPage[] { NewDocument(), NewDocument() });
            kryptonDockingManager.AddToNavigator("Navigator", new KryptonPage[] { NewDocument(), NewDocument(), NewDocument() });
        }
    }
}
