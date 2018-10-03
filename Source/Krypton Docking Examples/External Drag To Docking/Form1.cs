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

namespace ExternalDragToDocking
{
    public partial class Form1 : KryptonForm
    {
        private int _count = 1;

        public Form1()
        {
            InitializeComponent();
        }

        public KryptonPage NewDocument()
        {
            return NewPage("Document ", 0, new ContentDocument());
        }

        public KryptonPage NewInput()
        {
            return NewPage("Input ", 1, new ContentInput());
        }

        public KryptonPage NewPropertyGrid()
        {
            return NewPage("Properties ", 2, new ContentPropertyGrid());
        }

        public KryptonPage NewTreeView()
        {
            return NewPage("TreeView ", 3, new ContentTreeView(this));
        }

        public KryptonPage NewPage(string name, int image, Control content)
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

        public KryptonDockingManager DockingManager
        {
            get { return kryptonDockingManager; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[] { NewTreeView() });
            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewDocument() });
        }
    }
}
