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

namespace UserPageCreation
{
    public partial class Form1 : Form
    {
        private int _count = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add the initial document page
            InsertNewPage();
        }

        private void kryptonNavigator1_SelectedPageChanged(object sender, EventArgs e)
        {
            // If user selects the 'new page' which is the last page in the pages 
            // collection then we want to insert a new page just before the 'new page'.
            if (kryptonNavigator1.SelectedIndex == (kryptonNavigator1.Pages.Count - 1))
                InsertNewPage();
        }

        private void kryptonNavigator1_CloseAction(object sender, CloseActionEventArgs e)
        {
            // If deleting the last page before the 'new page' then need to switch the 
            // selection before the close actually gets processed. Otherwise removing the 
            // second to last page will causing the last 'new page' page to be selected. 
            // But that would cause a new page to be created! So manually set selection 
            // to the previous page to prevent this behavior.
            if (e.Index == (kryptonNavigator1.Pages.Count - 2))
                kryptonNavigator1.SelectedIndex = kryptonNavigator1.Pages.Count - 3;

            // You cannot delete the last document page. As the 'new page' page at the end of
            // the collection should always be there, we insist that 2 pages must be present. As
            // we are now removing a page that means that if there are 3 pages then after the
            // remove has completed we should not allow any more removes.
            if (kryptonNavigator1.Pages.Count == 3)
                kryptonNavigator1.Button.CloseButtonDisplay = ButtonDisplay.ShowDisabled;
        }

        private void InsertNewPage()
        {
            // Then create a new page
            KryptonPage newPage = new KryptonPage();

            // Define the name and image of the page
            newPage.Text = "Page" + (_count++).ToString();
            newPage.ImageSmall = global::UserPageCreation.Properties.Resources.document;

            // Insert just at second to last index, just before the 'new page' page
            kryptonNavigator1.Pages.Insert(kryptonNavigator1.Pages.Count - 1, newPage);

            // Select the new page
            kryptonNavigator1.SelectedPage = newPage;

            // We insist that two pages are always present. As we have just added one we
            // need to check if we now have 3 and so the close button should be allowed again.
            if (kryptonNavigator1.Pages.Count == 3)
                kryptonNavigator1.Button.CloseButtonDisplay = ButtonDisplay.ShowEnabled;
        }

        private void kryptonNavigator1_ContextAction(object sender, ContextActionEventArgs e)
        {
            // Because the 'new page' item does not have any text we need to manually set the
            // displayed text for 'new page' in the context menu strip, otherwise it will be blank
            KryptonContextMenuItems items = (KryptonContextMenuItems)e.KryptonContextMenu.Items[0];
            KryptonContextMenuItem item = (KryptonContextMenuItem)items.Items[items.Items.Count - 1];
            item.Text = "New Page";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            kryptonNavigator1.NavigatorMode = NavigatorMode.BarRibbonTabGroup;
            kryptonNavigator1.NavigatorMode = NavigatorMode.BarTabGroup;

            //Close();
        }
    }
}
