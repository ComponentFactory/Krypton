// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonNavigator to work as a docking auto hidden group control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonAutoHiddenGroup : KryptonNavigator
    {
        #region Events
        /// <summary>
        /// Occurs when a page is becoming stored.
        /// </summary>
        public event EventHandler<UniqueNameEventArgs> StoringPage;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonAutoHiddenGroup class.
        /// </summary>
        public KryptonAutoHiddenGroup(DockingEdge edge)
        {
            // Define appropriate appearance/behavior for an auto hidden group
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AllowTabFocus = false;
            AllowTabSelect = false;
            Bar.TabBorderStyle = TabBorderStyle.DockEqual;
            Bar.TabStyle = TabStyle.DockAutoHidden;
            Bar.BarFirstItemInset = 3;
            Bar.BarLastItemInset = 12;
            Bar.BarMinimumHeight = 0;
            Button.ButtonDisplayLogic = ButtonDisplayLogic.None;
            Button.CloseButtonDisplay = ButtonDisplay.Hide;
            NavigatorMode = NavigatorMode.BarTabOnly;

            // Edge dependant values
            switch (edge)
            {
                case DockingEdge.Left:
                    Bar.BarOrientation = VisualOrientation.Right;
                    Bar.ItemOrientation = ButtonOrientation.FixedRight;
                    Dock = DockStyle.Top;
                    break;
                case DockingEdge.Right:
                    Bar.BarOrientation = VisualOrientation.Left;
                    Bar.ItemOrientation = ButtonOrientation.FixedRight;
                    Dock = DockStyle.Top;
                    break;
                case DockingEdge.Top:
                    Bar.BarOrientation = VisualOrientation.Bottom;
                    Bar.ItemOrientation = ButtonOrientation.FixedTop;
                    Dock = DockStyle.Left;
                    break;
                case DockingEdge.Bottom:
                    Bar.BarOrientation = VisualOrientation.Top;
                    Bar.ItemOrientation = ButtonOrientation.FixedTop;
                    Dock = DockStyle.Left;
                    break;
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Convert all pages into store placeholders.
        /// </summary>
        public void StoreAllPages()
        {
            List<string> uniqueNames = new List<string>();

            // Create a list of pages that have not yet store placeholders
            foreach(KryptonPage page in Pages)
                if (!(page is KryptonStorePage))
                    uniqueNames.Add(page.UniqueName);

            StorePages(uniqueNames.ToArray());
        }

        /// <summary>
        /// Convert the named pages into store placeholders.
        /// </summary>
        /// <param name="uniqueNames">Array of page names.</param>
        public void StorePages(string[] uniqueNames)
        {
            foreach (string uniqueName in uniqueNames)
            {
                // If a matching page exists and it is not a store placeholder already
                KryptonPage page = Pages[uniqueName];
                if ((page != null) && !(page is KryptonStorePage))
                {
                    // Notify that we are storing a page, so handlers can ensure it will be unique to the auto hidden location
                    OnStoringPage(new UniqueNameEventArgs(page.UniqueName));

                    // Replace the existing page with a placeholder that has the same unique name
                    KryptonStorePage placeholder = new KryptonStorePage(uniqueName, "AutoHiddenGroup");
                    Pages.Insert(Pages.IndexOf(page), placeholder);
                    Pages.Remove(page);
                }
            }
        }

        /// <summary>
        /// Convert matching placeholders into actual pages.
        /// </summary>
        /// <param name="pages">Array of pages to restore.</param>
        public void RestorePages(KryptonPage[] pages)
        {
            foreach (KryptonPage page in pages)
            {
                // If a matching page exists and it is not a store placeholder already
                KryptonPage storePage = Pages[page.UniqueName];
                if ((storePage != null) && (storePage is KryptonStorePage))
                {
                    // Replace the existing placeholder with the actual page
                    Pages.Insert(Pages.IndexOf(storePage), page);
                    Pages.Remove(storePage);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the StoringPage event.
        /// </summary>
        /// <param name="e">An StorePageEventArgs containing the event data.</param>
        protected virtual void OnStoringPage(UniqueNameEventArgs e)
        {
            if (StoringPage != null)
                StoringPage(this, e);
        }

        /// <summary>
        /// Raises the TabCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        protected override void OnTabCountChanged(EventArgs e)
        {
            // When all the pages have been removed we kill ourself
            if (Pages.Count == 0)
                Dispose();
        }
        #endregion
    }
}
