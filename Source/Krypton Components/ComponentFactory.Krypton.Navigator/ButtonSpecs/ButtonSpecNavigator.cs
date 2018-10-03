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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// ButtonSpecNavigator specific implementation of a button specification.
    /// </summary>
    public class ButtonSpecNavigator : ButtonSpecHeaderGroup
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavigator class.
		/// </summary>
        public ButtonSpecNavigator()
		{
            ProtectedType = NavigatorToPaletteType(PaletteNavButtonSpecStyle.Generic);
		}
        #endregion

        #region Type
        /// <summary>
        /// Gets and sets the button type.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new PaletteButtonSpecStyle Type
        {
            get { return ProtectedType; }
            set { ProtectedType = value; }
        }
        #endregion

        #region TypeRestricted
        /// <summary>
        /// Gets and sets the button type.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines a restricted type for a navigator button spec.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(PaletteNavButtonSpecStyle), "Generic")]
        public PaletteNavButtonSpecStyle TypeRestricted
        {
            get { return PaletteTypeToNavigator(ProtectedType); }

            set
            {
                ProtectedType = NavigatorToPaletteType(value);
                OnButtonSpecPropertyChanged("Type");
            }
        }

        /// <summary>
        /// Resets the TypeRestricted property to its default value.
        /// </summary>
        public void ResetTypeRestricted()
        {
            TypeRestricted = PaletteNavButtonSpecStyle.Generic;
        }
        #endregion

        #region Implementation
        private PaletteButtonSpecStyle NavigatorToPaletteType(PaletteNavButtonSpecStyle type)
        {
            switch (type)
            {
                case PaletteNavButtonSpecStyle.Generic:
                    return PaletteButtonSpecStyle.Generic;
                case PaletteNavButtonSpecStyle.ArrowUp:
                    return PaletteButtonSpecStyle.ArrowUp;
                case PaletteNavButtonSpecStyle.ArrowRight:
                    return PaletteButtonSpecStyle.ArrowRight;
                case PaletteNavButtonSpecStyle.ArrowLeft:
                    return PaletteButtonSpecStyle.ArrowLeft;
                case PaletteNavButtonSpecStyle.ArrowDown:
                    return PaletteButtonSpecStyle.ArrowDown;
                case PaletteNavButtonSpecStyle.DropDown:
                    return PaletteButtonSpecStyle.DropDown;
                case PaletteNavButtonSpecStyle.PinVertical:
                    return PaletteButtonSpecStyle.PinVertical;
                case PaletteNavButtonSpecStyle.PinHorizontal:
                    return PaletteButtonSpecStyle.PinHorizontal;
                case PaletteNavButtonSpecStyle.FormClose:
                    return PaletteButtonSpecStyle.FormClose;
                case PaletteNavButtonSpecStyle.FormMax:
                    return PaletteButtonSpecStyle.FormMax;
                case PaletteNavButtonSpecStyle.FormMin:
                    return PaletteButtonSpecStyle.FormMin;
                case PaletteNavButtonSpecStyle.FormRestore:
                    return PaletteButtonSpecStyle.FormRestore;
                case PaletteNavButtonSpecStyle.PendantClose:
                    return PaletteButtonSpecStyle.PendantClose;
                case PaletteNavButtonSpecStyle.PendantMin:
                    return PaletteButtonSpecStyle.PendantMin;
                case PaletteNavButtonSpecStyle.PendantRestore:
                    return PaletteButtonSpecStyle.PendantRestore;
                case PaletteNavButtonSpecStyle.WorkspaceMaximize:
                    return PaletteButtonSpecStyle.WorkspaceMaximize;
                case PaletteNavButtonSpecStyle.WorkspaceRestore:
                    return PaletteButtonSpecStyle.WorkspaceRestore;
                case PaletteNavButtonSpecStyle.RibbonMinimize:
                    return PaletteButtonSpecStyle.RibbonMinimize;
                case PaletteNavButtonSpecStyle.RibbonExpand:
                    return PaletteButtonSpecStyle.RibbonExpand;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonSpecStyle.Generic;
            }
        }

        private PaletteNavButtonSpecStyle PaletteTypeToNavigator(PaletteButtonSpecStyle type)
        {
            switch (type)
            {
                case PaletteButtonSpecStyle.Generic:
                    return PaletteNavButtonSpecStyle.Generic;
                case PaletteButtonSpecStyle.ArrowUp:
                    return PaletteNavButtonSpecStyle.ArrowUp;
                case PaletteButtonSpecStyle.ArrowRight:
                    return PaletteNavButtonSpecStyle.ArrowRight;
                case PaletteButtonSpecStyle.ArrowLeft:
                    return PaletteNavButtonSpecStyle.ArrowLeft;
                case PaletteButtonSpecStyle.ArrowDown:
                    return PaletteNavButtonSpecStyle.ArrowDown;
                case PaletteButtonSpecStyle.DropDown:
                    return PaletteNavButtonSpecStyle.DropDown;
                case PaletteButtonSpecStyle.PinVertical:
                    return PaletteNavButtonSpecStyle.PinVertical;
                case PaletteButtonSpecStyle.PinHorizontal:
                    return PaletteNavButtonSpecStyle.PinHorizontal;
                case PaletteButtonSpecStyle.FormClose:
                    return PaletteNavButtonSpecStyle.FormClose;
                case PaletteButtonSpecStyle.FormMax:
                    return PaletteNavButtonSpecStyle.FormMax;
                case PaletteButtonSpecStyle.FormMin:
                    return PaletteNavButtonSpecStyle.FormMin;
                case PaletteButtonSpecStyle.FormRestore:
                    return PaletteNavButtonSpecStyle.FormRestore;
                case PaletteButtonSpecStyle.PendantClose:
                    return PaletteNavButtonSpecStyle.PendantClose;
                case PaletteButtonSpecStyle.PendantMin:
                    return PaletteNavButtonSpecStyle.PendantMin;
                case PaletteButtonSpecStyle.PendantRestore:
                    return PaletteNavButtonSpecStyle.PendantRestore;
                case PaletteButtonSpecStyle.WorkspaceMaximize:
                    return PaletteNavButtonSpecStyle.WorkspaceMaximize;
                case PaletteButtonSpecStyle.WorkspaceRestore:
                    return PaletteNavButtonSpecStyle.WorkspaceRestore;
                case PaletteButtonSpecStyle.RibbonMinimize:
                    return PaletteNavButtonSpecStyle.RibbonMinimize;
                case PaletteButtonSpecStyle.RibbonExpand:
                    return PaletteNavButtonSpecStyle.RibbonExpand;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteNavButtonSpecStyle.Generic;
            }
        }
        #endregion
    }
}
