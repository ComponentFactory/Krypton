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

namespace OneNoteTabs
{
    public partial class Form1 : Form
    {
        private int _count = 0;

        // Colors used when hot tracking over tabs
        private Color _hotMain = Color.FromArgb(255, 240, 200);
        private Color _hotEmbedSelected = Color.FromArgb(255, 241, 224);
        private Color _hotEmbedTracking = Color.FromArgb(255, 231, 162);

        // 8 example titles for the tabs
        private string[] _titleMain = new string[] { "Personal",    "Online", 
                                                     "Books",       "Travel", 
                                                     "Movies",      "Music", 
                                                     "Recipes",     "Shopping" };

        private string[] _titleEmbedded = new string[]{ "Financial information", "Credit card accounts",
                                                        "Website logins",        "Medical information",
                                                        "Frequent flyer points", "Activities",
                                                        "Sightseeing",           "Transportation",
                                                        "Hotel information",     "Trip schedule",
                                                        "Searching",             "Take notes",
                                                        "Diary entry",           "Bug reports",
                                                        "Release schedule",      "Shared resources",
                                                        "Screen shots",          "Book list" };


        // 8 colors for when the tab is not selected
        private Color[] _normal = new Color[]{ Color.FromArgb(156, 193, 182), Color.FromArgb(247, 184, 134),
                                               Color.FromArgb(217, 173, 194), Color.FromArgb(165, 194, 215),
                                               Color.FromArgb(179, 166, 190), Color.FromArgb(234, 214, 163),
                                               Color.FromArgb(246, 250, 125), Color.FromArgb(188, 168, 225) };

        // 8 colors for when the tab is selected
        private Color[] _select = new Color[]{ Color.FromArgb(200, 221, 215), Color.FromArgb(251, 216, 188),
                                               Color.FromArgb(234, 210, 221), Color.FromArgb(205, 221, 233),
                                               Color.FromArgb(213, 206, 219), Color.FromArgb(244, 232, 204),
                                               Color.FromArgb(250, 252, 183), Color.FromArgb(218, 207, 239) };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start with four initial pages
            AddTopPage();
            AddTopPage();
            AddTopPage();
            AddTopPage();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Append page to end of list
            AddTopPage();

            // Select the new page
            kryptonNavigator1.SelectedIndex = kryptonNavigator1.Pages.Count - 1;

            // Update button states
            buttonRemove.Enabled = true;
            buttonClear.Enabled = true;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Remove the selected page
            kryptonNavigator1.Pages.Remove(kryptonNavigator1.SelectedPage);

            // Update button states
            buttonRemove.Enabled = (kryptonNavigator1.SelectedPage != null);
            buttonClear.Enabled = (kryptonNavigator1.SelectedPage != null);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Remove all pages
            kryptonNavigator1.Pages.Clear();

            // Update button states
            buttonRemove.Enabled = false;
            buttonClear.Enabled = false;
        }

        private void AddTopPage()
        {
            // Create a new krypton page to be added
            KryptonPage page = new KryptonPage();

            // Set the page title
            page.Text = _titleMain[_count % _titleMain.Length];

            // Remove the default image for the page
            page.ImageSmall = null;

            // Set the padding so contained controls are indented
            page.Padding = new Padding(7);

            // Get the colors to use for this new page
            Color normal = _normal[_count % _normal.Length];
            Color select = _select[_count % _select.Length];

            // Set the page colors
            page.StateNormal.Page.Color1 = select;
            page.StateNormal.Page.Color2 = normal;
            page.StateNormal.Tab.Back.Color2 = normal;
            page.StateSelected.Tab.Back.Color2 = select;
            page.StateTracking.Tab.Back.Color2 = _hotMain;
            page.StatePressed.Tab.Back.Color2 = _hotMain;

            // We want the page drawn as a gradient with colors relative to its own area
            page.StateCommon.Page.ColorAlign = PaletteRectangleAlign.Local;
            page.StateCommon.Page.ColorStyle = PaletteColorStyle.Sigma;

            // We add an embedded navigator with its own pages to mimic OneNote operation
            AddEmbeddedNavigator(page);

            // Add page to end of the navigator collection
            kryptonNavigator1.Pages.Add(page);

            // Bump the page index to use next
            _count++;
        }

        private void AddEmbeddedNavigator(KryptonPage page)
        {
            // Create a navigator to embed inside the page
            KryptonNavigator nav = new KryptonNavigator();

            // We want the navigator to fill the entire page area
            nav.Dock = DockStyle.Fill;

            // Remove the close and context buttons
            nav.Button.CloseButtonDisplay = ButtonDisplay.Hide;
            nav.Button.ButtonDisplayLogic = ButtonDisplayLogic.None;

            // Set the required tab and bar settings
            nav.Bar.BarOrientation = VisualOrientation.Right;
            nav.Bar.ItemOrientation = ButtonOrientation.FixedTop;
            nav.Bar.ItemSizing = BarItemSizing.SameWidthAndHeight;
            nav.Bar.TabBorderStyle = TabBorderStyle.RoundedEqualSmall;
            nav.Bar.TabStyle = TabStyle.StandardProfile;

            // Do not draw the bar area background, let parent page show through
            nav.StateCommon.Panel.Draw = InheritBool.False;

            // Use same font for all tab states and we want text aligned to near
            nav.StateCommon.Tab.Content.ShortText.Font = SystemFonts.IconTitleFont;
            nav.StateCommon.Tab.Content.ShortText.TextH = PaletteRelativeAlign.Near;

            // Set the page colors
            nav.StateCommon.Tab.Content.Padding = new Padding(4);
            nav.StateNormal.Tab.Back.ColorStyle = PaletteColorStyle.Linear;
            nav.StateNormal.Tab.Back.Color1 = _select[_count % _select.Length];
            nav.StateNormal.Tab.Back.Color2 = Color.White;
            nav.StateNormal.Tab.Back.ColorAngle = 270;
            nav.StateSelected.Tab.Back.ColorStyle = PaletteColorStyle.Linear;
            nav.StateSelected.Tab.Back.Color2 = _hotEmbedSelected;
            nav.StateSelected.Tab.Back.ColorAngle = 270;
            nav.StateTracking.Tab.Back.ColorStyle = PaletteColorStyle.Solid;
            nav.StateTracking.Tab.Back.Color1 = _hotEmbedTracking;
            nav.StatePressed.Tab.Back.ColorStyle = PaletteColorStyle.Solid;
            nav.StatePressed.Tab.Back.Color1 = _hotEmbedTracking;

            // Add a random number of pages
            Random rand = new Random();
            int numPages = 3 + rand.Next(5);

            for(int i=0; i<numPages; i++)
                nav.Pages.Add(NewEmbeddedPage(_titleEmbedded[rand.Next(_titleEmbedded.Length - 1)]));

            page.Controls.Add(nav);
        }

        private KryptonPage NewEmbeddedPage(string title)
        {
            KryptonPage page = new KryptonPage();
            page.Text = title;
            page.ImageSmall = null;
            return page;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
