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

namespace ExpandingPages
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTopArrow_Click(object sender, EventArgs e)
        {
            // For the top navigator instance we will toggle the showing of 
            // the client area below the check button area. We also toggle 
            // the direction of the button spec arrow.

            if (navigatorTop.NavigatorMode == NavigatorMode.HeaderBarCheckButtonGroup)
            {
                navigatorTop.NavigatorMode = NavigatorMode.HeaderBarCheckButtonOnly;
                buttonTopArrow.TypeRestricted = PaletteNavButtonSpecStyle.ArrowDown;
            }
            else
            {
                navigatorTop.NavigatorMode = NavigatorMode.HeaderBarCheckButtonGroup;
                buttonTopArrow.TypeRestricted = PaletteNavButtonSpecStyle.ArrowUp;
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            // For the left navigator instance we will toggle the showing of 
            // the client area to the right of the check button area. We also 
            // toggle the direction of the button spec arrow.

            if (navigatorLeft.NavigatorMode == NavigatorMode.HeaderBarCheckButtonGroup)
            {
                navigatorLeft.NavigatorMode = NavigatorMode.HeaderBarCheckButtonOnly;
                buttonLeft.TypeRestricted = PaletteNavButtonSpecStyle.ArrowRight;
            }
            else
            {
                navigatorLeft.NavigatorMode = NavigatorMode.HeaderBarCheckButtonGroup;
                buttonLeft.TypeRestricted = PaletteNavButtonSpecStyle.ArrowLeft;
            }
        }

        private void kryptonPaletteButtons_Click(object sender, EventArgs e)
        {
            // When the user presses one of the palette buttons we need to ensure
            // that if the containing page is showing as a popup that the popup
            // is then removed from display. So call DismissPopupPage to remove
            // the page from view. If the page is not showing as a popup then
            // the call does nothing.
            navigatorLeft.DismissPopups();
        }

        private void kryptonPalettes_CheckedButtonChanged(object sender, EventArgs e)
        {
            // Change the palette depending on the selected button
            switch (kryptonPalettes.CheckedIndex)
            {
                case 0:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case 1:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case 2:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case 3:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                    break;
                case 4:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                case 5:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case 6:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                    break;
                case 7:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                    break;
                case 8:
                    kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;

            }
        }
    }
}
