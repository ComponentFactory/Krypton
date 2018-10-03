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
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Manage the items that can be added to a ribbon group lines container.
    /// </summary>
    public class KryptonRibbonGroupLinesCollection : TypedRestrictCollection<KryptonRibbonGroupItem>
    {
        #region Static Fields
        private static readonly Type[] _types = new Type[] { typeof(KryptonRibbonGroupButton),
                                                             typeof(KryptonRibbonGroupColorButton),
                                                             typeof(KryptonRibbonGroupCheckBox),
                                                             typeof(KryptonRibbonGroupComboBox),
                                                             typeof(KryptonRibbonGroupCluster),
                                                             typeof(KryptonRibbonGroupCustomControl),
                                                             typeof(KryptonRibbonGroupDateTimePicker),
                                                             typeof(KryptonRibbonGroupDomainUpDown),
                                                             typeof(KryptonRibbonGroupLabel),
                                                             typeof(KryptonRibbonGroupNumericUpDown),
                                                             typeof(KryptonRibbonGroupRadioButton),
                                                             typeof(KryptonRibbonGroupRichTextBox),
                                                             typeof(KryptonRibbonGroupTextBox),
                                                             typeof(KryptonRibbonGroupTrackBar),
                                                             typeof(KryptonRibbonGroupMaskedTextBox),
                                                           };

        #endregion

        #region Restrict
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes 
        {
            get { return _types; }
        }
        #endregion
    }
}
