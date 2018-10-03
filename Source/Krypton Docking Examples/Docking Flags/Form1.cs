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
using System.IO;
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

namespace DockingFlags
{
    public partial class Form1 : KryptonForm
    {
        private int _count = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private KryptonPage NewDocument()
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = "Document " + _count.ToString();
            p.TextTitle = p.Text;
            p.TextDescription = p.Text;
            p.UniqueName = p.Text;
            p.ImageSmall = imageListSmall.Images[0];

            // Add the control for display inside the page
            ContentDocument contentDoc = new ContentDocument();
            contentDoc.Dock = DockStyle.Fill;
            p.Controls.Add(contentDoc);

            _count++;
            return p;
        }

        private KryptonPage NewFlags()
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = "Flags " + _count.ToString();
            p.TextTitle = p.Text;
            p.TextDescription = p.Text;
            p.UniqueName = p.Text;
            p.ImageSmall = imageListSmall.Images[1];

            // Add the control for display inside the page
            ContentFlags contentFlags = new ContentFlags(p);
            contentFlags.Dock = DockStyle.Fill;
            p.Controls.Add(contentFlags);

            _count++;
            return p;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add docking pages
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[] { NewFlags(), NewFlags() });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewDocument() });
            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewFlags(), NewFlags() });
        }
    }
}
