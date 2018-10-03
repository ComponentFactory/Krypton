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

namespace NavigatorModes
{
    public partial class Form1 : Form
    {
        private class DescriptionLookup : Dictionary<NavigatorMode, string> { }
        private DescriptionLookup _lookup;

        public Form1()
        {
            // Create descriptions for each of the modes
            _lookup = new DescriptionLookup();

            _lookup.Add(NavigatorMode.BarTabGroup,
                "BarTabGroup shows a set of tabs on the outside of a Group container.\n\n" +
                "The contents of the selected page are displayed inside the Group container whilst the tab bar has a Panel style background.\n\n" +
                "Buttons are provided on the right hand side of the bar so the user can change the selected page or remove a page from the control. " +
                "As with all bar modes, you can change the edge the bar is orientated against.");

            _lookup.Add(NavigatorMode.BarTabOnly,
                "BarTabOnly is a tab strip style mode that does not display the contents of the selected page.\n\n" +
                "It shows a set of tabs that allow the user to select different pages at random.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for displaying the selected item. " +
                "By setting the AutoSize=True property on the navigator you can get the control to size appropriately.");

            _lookup.Add(NavigatorMode.BarRibbonTabGroup,
                "BarRibbonTabGroup shows a set of ribbon styled tabs on the outside of a Group container.\n\n" +
                "The contents of the selected page are displayed inside the Group container whilst the tab bar has a Panel style background.\n\n" +
                "Buttons are provided on the right hand side of the bar so the user can change the selected page or remove a page from the control. " +
                "As with all bar modes, you can change the edge the bar is orientated against.");

            _lookup.Add(NavigatorMode.BarRibbonTabOnly,
                "BarRibbonTabOnly is a tab strip style mode that does not display the contents of the selected page.\n\n" +
                "It shows a set of ribbon styled tabs that allow the user to select different pages at random.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for displaying the selected item. " +
                "By setting the AutoSize=True property on the navigator you can get the control to size appropriately.");

            _lookup.Add(NavigatorMode.BarCheckButtonGroupOutside,
                "BarCheckButtonGroupOutside shows a bar of CheckButton controls on the outside of a Group container.\n\n" +
                "The contents of the selected page are displayed inside the Group container whilst the bar has a Panel style background.\n\n" +
                "Buttons are provided on the right hand side of the bar so the user can change the selected page or remove a page from the control. " +
                "As with all modes that show buttons, you can customize the buttons that appear and how they function.");

            _lookup.Add(NavigatorMode.BarCheckButtonGroupInside,
                "BarCheckButtonGroupInside shows a Group container that has two items inside.\n\n" +
                "Against the top edge is a bar of CheckButton controls and the rest of the space is filled with the contents of the selected page. " +
                "Buttons are provided on the right hand side of the bar so the user can change the selected page or remove a page from the control.\n\n" +
                "As with all bar modes, you can change the edge the bar is orientated against.");
            
            _lookup.Add(NavigatorMode.BarCheckButtonGroupOnly,
                "BarCheckButtonGroupOnly is a tab strip style mode that does not display the contents of the selected page.\n\n" +
                "It shows a Group style container that has a bar of CheckButton controls inside.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for displaying the selected item. " +
                "By setting the AutoSize=True property on the navigator you can get the control to size appropriately.");
            
            _lookup.Add(NavigatorMode.BarCheckButtonOnly, 
                "BarCheckButtonOnly is a tab strip style mode that does not display the contents of the selected page.\n\n" +
                "It shows a bar of CheckButton controls that allow the user to select different pages at random. " +
                "The background of the bar uses a Panel style to control the appearance.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for displaying the selected item. " +
                "By setting the AutoSize=True property on the navigator you can get the control to size appropriately.");

            _lookup.Add(NavigatorMode.HeaderBarCheckButtonGroup,
                "HeaderBarCheckButtonGroup shows a Header bar containing CheckButton controls within a Group container.\n\n" +
                "The contents of the selected page are displayed inside the Group container.\n\n" +
                "Buttons are provided on the right hand side of the bar so the user can change the selected page or remove a page from the control. " +
                "As with all modes that show buttons, you can customize the buttons that appear and how they function.");

            _lookup.Add(NavigatorMode.HeaderBarCheckButtonHeaderGroup,
                "HeaderBarCheckButtonHeaderGroup mode provides a Header containing CheckButton controls and two additional headers for displaying information about the currently selected page.\n\n" +
                "Buttons are provided on the primary header so the user can page the selected page or remove a page from the control.\n\n" +
                "You can customize how page details are mapped to the headers in order to show only the exact details you need in the location of your choice.");

            _lookup.Add(NavigatorMode.HeaderBarCheckButtonOnly,
                "HeaderBarCheckButtonOnly is a tab strip style mode that does not display the contents of the selected page.\n\n" +
                "It shows a Header bar of CheckButton controls that allow the user to select different pages at random.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for displaying the selected item. " +
                "By setting the AutoSize=True property on the navigator you can get the control to size appropriately.");

            _lookup.Add(NavigatorMode.StackCheckButtonGroup,
                "StackCheckButtonGroup mode shows a set of CheckButton controls stacked within a Group container. " +
                "You can choose to stack in either a vertical or horizontal orientation.\n\n" +
                "The selected page is shown immediately below the matching CheckButton but you can choose to have all the CheckButtons placed at the top or bottom.");

            _lookup.Add(NavigatorMode.StackCheckButtonHeaderGroup,
                "StackCheckButtonGroup mode shows a set of CheckButton controls in conjunction with two headers for displaying information about the currently selected page.\n\n" +
                "You can choose to stack in either a vertical or horizontal orientation. " +
                "The selected page is shown immediately below the matching CheckButton but you can choose to have all the CheckButtons placed at the top or bottom.");

            _lookup.Add(NavigatorMode.OutlookFull,
                "OutlookFull mode mimics the expanded operation of the Microsoft Outlook 2007 selection control.\n\n" +
                "A set of CheckButton controls are stacked vertically along with an overflow bar at the bottom of the control. " +
                "If there is not enough room to show all the stacking items then they are automatically placed on the overflow bar.\n\n" + 
                "The user can drag the separator to manually force CheckButton items to be removed from the stack and placed in the overflow bar and vica versa.");

            _lookup.Add(NavigatorMode.OutlookMini,
                "OutlookMini mode mimics the collapsed operation of the Microsoft Outlook 2007 selection control.\n\n" +
                "A set of CheckButton controls are stacked vertically to allow selection of different pages. " +
                "Clicking the selection button for the current page causes a popup to show with the page contents.");

            _lookup.Add(NavigatorMode.HeaderGroup,
                "HeaderGroup mode provides two headers that are used to display information about the currently selected page.\n\n" +
                "Buttons are provided on the primary header so the user can page the selected page or remove a page from the control.\n\n" +
                "You can customize how page details are mapped to the headers in order to show only the exact details you need in the location of your choice.");

            _lookup.Add(NavigatorMode.HeaderGroupTab,
                "HeaderGroupTab mode provides two headers that are used to display information about the currently selected page along with a set of tabs for page selection.\n\n" +
                "Buttons are provided on the primary header so the user can page the selected page or remove a page from the control.\n\n" +
                "You can customize how page details are mapped to the headers in order to show only the exact details you need in the location of your choice.");

            _lookup.Add(NavigatorMode.Group, 
                "The Group mode does not provide any user interface for changing the selection, it just displays the selected page inside a Group container. " +
                "If you need to change the selection then you must do so programmatically.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for changing the selection. " +
                "At design time you can use a mode that allows pages to be selected so you can quickly and easily design each of the pages, then at runtime switch to the Panel mode for the sparse appearance you need.");

            _lookup.Add(NavigatorMode.Panel, 
                "Panel mode does not provide any user interface other than filling the entire client area with the selected page. " +
                "If you need to change the selection then you must do so programmatically.\n\n" +
                "This mode is useful when you want to provide your own separate mechanism for changing the selection. " +
                "At design time you can use a mode that allows pages to be selected so you can quickly and easily design each of the pages, then at runtime switch to the Panel mode for the sparse appearance you need.");

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateRadioButtonsFromMode();
        }

        private void radioMode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateModeFromRadioButtons();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateModeFromRadioButtons()
        {
            NavigatorMode mode = NavigatorMode.BarTabGroup;

            // Work out the new mode we want
            if (radioBarTabGroup.Checked)
                mode = NavigatorMode.BarTabGroup;
            else if (radioBarTabOnly.Checked)
                mode = NavigatorMode.BarTabOnly;
            else if (radioBarRibbonTabGroup.Checked)
                mode = NavigatorMode.BarRibbonTabGroup;
            else if (radioBarRibbonTabOnly.Checked)
                mode = NavigatorMode.BarRibbonTabOnly;
            else if (radioBarCheckButtonGroupInside.Checked)
                mode = NavigatorMode.BarCheckButtonGroupInside;
            else if (radioBarCheckButtonGroupOutside.Checked)
                mode = NavigatorMode.BarCheckButtonGroupOutside;
            else if (radioBarCheckButtonGroupOnly.Checked)
                mode = NavigatorMode.BarCheckButtonGroupOnly;
            else if (radioBarCheckButtonOnly.Checked)
                mode = NavigatorMode.BarCheckButtonOnly;
            else if (radioHeaderBarCheckButtonGroup.Checked)
                mode = NavigatorMode.HeaderBarCheckButtonGroup;
            else if (radioHeaderBarCheckButtonHeaderGroup.Checked)
                mode = NavigatorMode.HeaderBarCheckButtonHeaderGroup;
            else if (radioHeaderBarCheckButtonOnly.Checked)
                mode = NavigatorMode.HeaderBarCheckButtonOnly;
            else if (radioStackCheckButtonGroup.Checked)
                mode = NavigatorMode.StackCheckButtonGroup;
            else if (radioStackCheckButtonHeaderGroup.Checked)
                mode = NavigatorMode.StackCheckButtonHeaderGroup;
            else if (radioOutlookFull.Checked)
                mode = NavigatorMode.OutlookFull;
            else if (radioOutlookMini.Checked)
                mode = NavigatorMode.OutlookMini;
            else if (radioHeaderGroup.Checked)
                mode = NavigatorMode.HeaderGroup;
            else if (radioHeaderGroupTab.Checked)
                mode = NavigatorMode.HeaderGroupTab;
            else if (radioGroup.Checked)
                mode = NavigatorMode.Group;
            else if (radioPanel.Checked)
                mode = NavigatorMode.Panel;

            // Set the mode into the navigator itself
            kryptonNavigator1.NavigatorMode = mode;

            // Set the AutoSize to show that tabstrip functionality works
            switch (mode)
            {
                case NavigatorMode.BarCheckButtonGroupOnly:
                case NavigatorMode.BarCheckButtonOnly:
                case NavigatorMode.BarTabOnly:
                case NavigatorMode.HeaderBarCheckButtonOnly:
                case NavigatorMode.OutlookMini:
                    kryptonNavigator1.AutoSize = true;
                    break;
                default:
                    kryptonNavigator1.AutoSize = false;
                    break;
            }

            // Set mode specific properties
            switch (mode)
            {
                case NavigatorMode.BarRibbonTabGroup:
                case NavigatorMode.BarRibbonTabOnly:
                    kryptonNavigator1.PageBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator1.Group.GroupBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator1.Group.GroupBorderStyle = PaletteBorderStyle.ControlRibbon;
                    break;
                default:
                    kryptonNavigator1.PageBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator1.Group.GroupBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator1.Group.GroupBorderStyle = PaletteBorderStyle.ControlClient;
                    break;
            }

            kryptonNavigator1.Dock = (mode == NavigatorMode.OutlookMini ? DockStyle.Left : DockStyle.Top);

            // Update the radio buttons to reflect the new mode
            UpdateRadioButtonsFromMode();
        }

        private void UpdateRadioButtonsFromMode()
        {
            // Grab the current mode of the navigator control
            NavigatorMode mode = kryptonNavigator1.NavigatorMode;

            // Update buttons to reflect the mode
            radioBarTabGroup.Checked = (mode == NavigatorMode.BarTabGroup);
            radioBarTabOnly.Checked = (mode == NavigatorMode.BarTabOnly);
            radioBarRibbonTabGroup.Checked = (mode == NavigatorMode.BarRibbonTabGroup);
            radioBarRibbonTabOnly.Checked = (mode == NavigatorMode.BarRibbonTabOnly);
            radioBarCheckButtonGroupInside.Checked = (mode == NavigatorMode.BarCheckButtonGroupInside);
            radioBarCheckButtonGroupOutside.Checked = (mode == NavigatorMode.BarCheckButtonGroupOutside);
            radioBarCheckButtonGroupOnly.Checked = (mode == NavigatorMode.BarCheckButtonGroupOnly);
            radioBarCheckButtonOnly.Checked = (mode == NavigatorMode.BarCheckButtonOnly);
            radioHeaderBarCheckButtonGroup.Checked = (mode == NavigatorMode.HeaderBarCheckButtonGroup);
            radioHeaderBarCheckButtonHeaderGroup.Checked = (mode == NavigatorMode.HeaderBarCheckButtonHeaderGroup);
            radioHeaderBarCheckButtonOnly.Checked = (mode == NavigatorMode.HeaderBarCheckButtonOnly);
            radioStackCheckButtonGroup.Checked = (mode == NavigatorMode.StackCheckButtonGroup);
            radioStackCheckButtonHeaderGroup.Checked = (mode == NavigatorMode.StackCheckButtonHeaderGroup);
            radioOutlookFull.Checked = (mode == NavigatorMode.OutlookFull);
            radioOutlookMini.Checked = (mode == NavigatorMode.OutlookMini);
            radioHeaderGroup.Checked = (mode == NavigatorMode.HeaderGroup);
            radioGroup.Checked = (mode == NavigatorMode.Group);
            radioPanel.Checked = (mode == NavigatorMode.Panel);

            // Update the description with text for the mode
            textBoxDescription.Text = _lookup[mode];
        }
    }
}
