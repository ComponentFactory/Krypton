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

namespace MultiControlDocking
{
    public partial class Form1 : Form
    {
        private int _count = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private KryptonPage NewInput()
        {
            return NewPage("Input ", 1, new ContentInput());
        }

        private KryptonPage NewPage(string name, int image, Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name + _count.ToString();
            p.TextTitle = name + _count.ToString();
            p.TextDescription = name + _count.ToString();
            p.ImageSmall = imageListSmall.Images[image];
            
            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add docking to the two panels and allow floating windows
            kryptonDockingManager.ManageControl("Control1", kryptonPanel2);
            kryptonDockingManager.ManageControl("Control2", kryptonPanel3);
            kryptonDockingManager.ManageFloating(this);


            // Add docking pages
            kryptonDockingManager.AddDockspace("Control1", DockingEdge.Left, new KryptonPage[] { NewInput(), NewInput() });
            kryptonDockingManager.AddDockspace("Control1", DockingEdge.Bottom, new KryptonPage[] { NewInput(), NewInput() });
            kryptonDockingManager.AddDockspace("Control2", DockingEdge.Bottom, new KryptonPage[] { NewInput(), NewInput() });
            kryptonDockingManager.AddAutoHiddenGroup("Control2", DockingEdge.Right, new KryptonPage[] { NewInput(), NewInput() });
        }
    }
}
