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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteBorderStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteBorderStyleConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(PaletteBorderStyle.ButtonStandalone,          "Button - Standalone"),
                                             new Pair(PaletteBorderStyle.ButtonAlternate,           "Button - Alternate"),
                                             new Pair(PaletteBorderStyle.ButtonLowProfile,          "Button - Low Profile"),
                                             new Pair(PaletteBorderStyle.ButtonButtonSpec,          "Button - ButtonSpec"),
                                             new Pair(PaletteBorderStyle.ButtonBreadCrumb,          "Button - BreadCrumb"),
                                             new Pair(PaletteBorderStyle.ButtonCalendarDay,         "Button - Calendar Day"),
                                             new Pair(PaletteBorderStyle.ButtonCluster,             "Button - Cluster"),
                                             new Pair(PaletteBorderStyle.ButtonGallery,             "Button - Gallery"),
                                             new Pair(PaletteBorderStyle.ButtonNavigatorStack,      "Button - Navigator Stack"),
                                             new Pair(PaletteBorderStyle.ButtonNavigatorOverflow,   "Button - Navigator Overflow"),
                                             new Pair(PaletteBorderStyle.ButtonNavigatorMini,       "Button - Navigator Mini"),
                                             new Pair(PaletteBorderStyle.ButtonInputControl,        "Button - Input Control"),
                                             new Pair(PaletteBorderStyle.ButtonListItem,            "Button - List Item"),
                                             new Pair(PaletteBorderStyle.ButtonForm,                "Button - Form"),
                                             new Pair(PaletteBorderStyle.ButtonFormClose,           "Button - Form Close"),
                                             new Pair(PaletteBorderStyle.ButtonCommand,             "Button - Command"),
                                             new Pair(PaletteBorderStyle.ButtonCustom1,             "Button - Custom1"),
                                             new Pair(PaletteBorderStyle.ButtonCustom2,             "Button - Custom2"),
                                             new Pair(PaletteBorderStyle.ButtonCustom3,             "Button - Custom3"),
                                             new Pair(PaletteBorderStyle.ControlClient,             "Control - Client"),
                                             new Pair(PaletteBorderStyle.ControlAlternate,          "Control - Alternate"),
                                             new Pair(PaletteBorderStyle.ControlGroupBox,           "Control - GroupBox"),
                                             new Pair(PaletteBorderStyle.ControlToolTip,            "Control - ToolTip"),
                                             new Pair(PaletteBorderStyle.ControlRibbon,             "Control - Ribbon"),
                                             new Pair(PaletteBorderStyle.ControlRibbonAppMenu,      "Control - RibbonAppMenu"),
                                             new Pair(PaletteBorderStyle.ControlCustom1,            "Control - Custom1"),
                                             new Pair(PaletteBorderStyle.ContextMenuOuter,          "ContextMenu - Outer"),
                                             new Pair(PaletteBorderStyle.ContextMenuInner,          "ContextMenu - Inner"),
                                             new Pair(PaletteBorderStyle.ContextMenuHeading,        "ContextMenu - Heading"),
                                             new Pair(PaletteBorderStyle.ContextMenuSeparator,      "ContextMenu - Separator"),
                                             new Pair(PaletteBorderStyle.ContextMenuItemSplit,      "ContextMenu - Item Split"),
                                             new Pair(PaletteBorderStyle.ContextMenuItemImage,      "ContextMenu - Item Image"),
                                             new Pair(PaletteBorderStyle.ContextMenuItemImageColumn,"ContextMenu - Item ImageColumn"),
                                             new Pair(PaletteBorderStyle.ContextMenuItemHighlight,  "ContextMenu - Item Highlight"),
                                             new Pair(PaletteBorderStyle.InputControlStandalone,    "InputControl - Standalone"),
                                             new Pair(PaletteBorderStyle.InputControlRibbon,        "InputControl - Ribbon"),
                                             new Pair(PaletteBorderStyle.InputControlCustom1,       "InputControl - Custom1"),
                                             new Pair(PaletteBorderStyle.FormMain,                  "Form - Main"),
                                             new Pair(PaletteBorderStyle.FormCustom1,               "Form - Custom1"),
                                             new Pair(PaletteBorderStyle.GridHeaderColumnList,      "Grid - HeaderColumn - List"),
                                             new Pair(PaletteBorderStyle.GridHeaderRowList,         "Grid - HeaderRow - List"),
                                             new Pair(PaletteBorderStyle.GridDataCellList,          "Grid - DataCell - List"),
                                             new Pair(PaletteBorderStyle.GridHeaderColumnSheet,     "Grid - HeaderColumn - Sheet"),
                                             new Pair(PaletteBorderStyle.GridHeaderRowSheet,        "Grid - HeaderRow - Sheet"),
                                             new Pair(PaletteBorderStyle.GridDataCellSheet,         "Grid - DataCell - Sheet"),
                                             new Pair(PaletteBorderStyle.GridHeaderColumnCustom1,   "Grid - HeaderColumn - Custom1"),
                                             new Pair(PaletteBorderStyle.GridHeaderRowCustom1,      "Grid - HeaderRow - Custom1"),
                                             new Pair(PaletteBorderStyle.GridDataCellCustom1,       "Grid - DataCell - Custom1"),
                                             new Pair(PaletteBorderStyle.HeaderPrimary,             "Header - Primary"),
                                             new Pair(PaletteBorderStyle.HeaderSecondary,           "Header - Secondary"),
                                             new Pair(PaletteBorderStyle.HeaderDockActive,          "Header - Dock - Active"),
                                             new Pair(PaletteBorderStyle.HeaderDockInactive,        "Header - Dock - Inactive"),
                                             new Pair(PaletteBorderStyle.HeaderForm,                "Header - Form"),
                                             new Pair(PaletteBorderStyle.HeaderCalendar,            "Header - Calendar"),
                                             new Pair(PaletteBorderStyle.HeaderCustom1,             "Header - Custom1"),
                                             new Pair(PaletteBorderStyle.HeaderCustom2,             "Header - Custom2"),
                                             new Pair(PaletteBorderStyle.SeparatorLowProfile,       "Separator - Low Profile"),
                                             new Pair(PaletteBorderStyle.SeparatorHighProfile,      "Separator - High Profile"),
                                             new Pair(PaletteBorderStyle.SeparatorHighInternalProfile,"Separator - High Internal Profile"),
                                             new Pair(PaletteBorderStyle.TabHighProfile,            "Tab - High Profile"),
                                             new Pair(PaletteBorderStyle.TabStandardProfile,        "Tab - Standard Profile"),
                                             new Pair(PaletteBorderStyle.TabLowProfile,             "Tab - Low Profile"),
                                             new Pair(PaletteBorderStyle.TabOneNote,                "Tab - OneNote"),
                                             new Pair(PaletteBorderStyle.TabDock,                   "Tab - Dock"),
                                             new Pair(PaletteBorderStyle.TabDockAutoHidden,         "Tab - Dock AutoHidden"),
                                             new Pair(PaletteBorderStyle.TabCustom1,                "Tab - Custom1"),
                                             new Pair(PaletteBorderStyle.TabCustom2,                "Tab - Custom2"),
                                             new Pair(PaletteBorderStyle.TabCustom3,                "Tab - Custom3") };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorderStyleConverter clas.
        /// </summary>
        public PaletteBorderStyleConverter()
            : base(typeof(PaletteBorderStyle))
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override Pair[] Pairs 
        {
            get { return _pairs; }
        }
        #endregion
    }
}
