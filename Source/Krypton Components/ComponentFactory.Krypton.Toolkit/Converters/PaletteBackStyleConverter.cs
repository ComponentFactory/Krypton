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
    /// Custom type converter so that PaletteBackStyle values appear as neat text at design time.
    /// </summary>
    internal class PaletteBackStyleConverter : StringLookupConverter
    {
        #region Static Fields
        private Pair[] _pairs = new Pair[] { new Pair(PaletteBackStyle.ButtonStandalone,            "Button - Standalone"),
                                             new Pair(PaletteBackStyle.ButtonAlternate,             "Button - Alternate"),
                                             new Pair(PaletteBackStyle.ButtonLowProfile,            "Button - Low Profile"),
                                             new Pair(PaletteBackStyle.ButtonButtonSpec,            "Button - ButtonSpec"),
                                             new Pair(PaletteBackStyle.ButtonBreadCrumb,            "Button - BreadCrumb"),
                                             new Pair(PaletteBackStyle.ButtonCalendarDay,           "Button - Calendar Day"),
                                             new Pair(PaletteBackStyle.ButtonCluster,               "Button - Cluster"),
                                             new Pair(PaletteBackStyle.ButtonGallery,               "Button - Gallery"),
                                             new Pair(PaletteBackStyle.ButtonNavigatorStack,        "Button - Navigator Stack"),
                                             new Pair(PaletteBackStyle.ButtonNavigatorOverflow,     "Button - Navigator Overflow"),
                                             new Pair(PaletteBackStyle.ButtonNavigatorMini,         "Button - Navigator Mini"),
                                             new Pair(PaletteBackStyle.ButtonInputControl,          "Button - Input Control"),
                                             new Pair(PaletteBackStyle.ButtonListItem,              "Button - List Item"),
                                             new Pair(PaletteBackStyle.ButtonForm,                  "Button - Form"),
                                             new Pair(PaletteBackStyle.ButtonFormClose,             "Button - Form Close"),
                                             new Pair(PaletteBackStyle.ButtonCommand,               "Button - Command"),
                                             new Pair(PaletteBackStyle.ButtonCustom1,               "Button - Custom1"),
                                             new Pair(PaletteBackStyle.ButtonCustom2,               "Button - Custom2"),
                                             new Pair(PaletteBackStyle.ButtonCustom3,               "Button - Custom3"),
                                             new Pair(PaletteBackStyle.ControlClient,               "Control - Client"),
                                             new Pair(PaletteBackStyle.ControlAlternate,            "Control - Alternate"),
                                             new Pair(PaletteBackStyle.ControlGroupBox,             "Control - GroupBox"),
                                             new Pair(PaletteBackStyle.ControlToolTip,              "Control - ToolTip"),
                                             new Pair(PaletteBackStyle.ControlRibbon,               "Control - Ribbon"),
                                             new Pair(PaletteBackStyle.ControlRibbonAppMenu,        "Control - RibbonAppMenu"),
                                             new Pair(PaletteBackStyle.ControlCustom1,              "Control - Custom1"),
                                             new Pair(PaletteBackStyle.ContextMenuOuter,            "ContextMenu - Outer"),
                                             new Pair(PaletteBackStyle.ContextMenuInner,            "ContextMenu - Inner"),
                                             new Pair(PaletteBackStyle.ContextMenuHeading,          "ContextMenu - Heading"),
                                             new Pair(PaletteBackStyle.ContextMenuSeparator,        "ContextMenu - Separator"),
                                             new Pair(PaletteBackStyle.ContextMenuItemSplit,        "ContextMenu - Item Split"),
                                             new Pair(PaletteBackStyle.ContextMenuItemImage,        "ContextMenu - Item Image"),
                                             new Pair(PaletteBackStyle.ContextMenuItemImageColumn,  "ContextMenu - Item ImageColumn"),
                                             new Pair(PaletteBackStyle.ContextMenuItemHighlight,    "ContextMenu - Item Highlight"),
                                             new Pair(PaletteBackStyle.InputControlStandalone,      "InputControl - Standalone"),
                                             new Pair(PaletteBackStyle.InputControlRibbon,          "InputControl - Ribbon"),
                                             new Pair(PaletteBackStyle.InputControlCustom1,         "InputControl - Custom1"),
                                             new Pair(PaletteBackStyle.FormMain,                    "Form - Main"),
                                             new Pair(PaletteBackStyle.FormCustom1,                 "Form - Custom1"),
                                             new Pair(PaletteBackStyle.GridHeaderColumnList,        "Grid - HeaderColumn - List"),
                                             new Pair(PaletteBackStyle.GridHeaderRowList,           "Grid - HeaderRow - List"),
                                             new Pair(PaletteBackStyle.GridDataCellList,            "Grid - DataCell - List"),
                                             new Pair(PaletteBackStyle.GridBackgroundList,          "Grid - Background - List"),
                                             new Pair(PaletteBackStyle.GridHeaderColumnSheet,       "Grid - HeaderColumn - Sheet"),
                                             new Pair(PaletteBackStyle.GridHeaderRowSheet,          "Grid - HeaderRow - Sheet"),
                                             new Pair(PaletteBackStyle.GridDataCellSheet,           "Grid - DataCell - Sheet"),
                                             new Pair(PaletteBackStyle.GridBackgroundSheet,         "Grid - Background - Sheet"),
                                             new Pair(PaletteBackStyle.GridHeaderColumnCustom1,     "Grid - HeaderColumn - Custom1"),
                                             new Pair(PaletteBackStyle.GridHeaderRowCustom1,        "Grid - HeaderRow - Custom1"),
                                             new Pair(PaletteBackStyle.GridDataCellCustom1,         "Grid - DataCell - Custom1"),
                                             new Pair(PaletteBackStyle.GridBackgroundCustom1,       "Grid - Background - Custom1"),
                                             new Pair(PaletteBackStyle.HeaderPrimary,               "Header - Primary"),
                                             new Pair(PaletteBackStyle.HeaderSecondary,             "Header - Secondary"),
                                             new Pair(PaletteBackStyle.HeaderDockActive,            "Header - Dock - Active"),
                                             new Pair(PaletteBackStyle.HeaderDockInactive,          "Header - Dock - Inactive"),
                                             new Pair(PaletteBackStyle.HeaderForm,                  "Header - Form"),
                                             new Pair(PaletteBackStyle.HeaderCalendar,              "Header - Calendar"),
                                             new Pair(PaletteBackStyle.HeaderCustom1,               "Header - Custom1"),
                                             new Pair(PaletteBackStyle.HeaderCustom2,               "Header - Custom2"),
                                             new Pair(PaletteBackStyle.PanelClient,                 "Panel - Client"),
                                             new Pair(PaletteBackStyle.PanelAlternate,              "Panel - Alternate"),
                                             new Pair(PaletteBackStyle.PanelRibbonInactive,         "Panel - Ribbon Inactive"),
                                             new Pair(PaletteBackStyle.PanelCustom1,                "Panel - Custom1"),
                                             new Pair(PaletteBackStyle.SeparatorLowProfile,         "Separator - Low Profile"),
                                             new Pair(PaletteBackStyle.SeparatorHighProfile,        "Separator - High Profile"),
                                             new Pair(PaletteBackStyle.SeparatorHighInternalProfile,"Separator - High Internal Profile"),
                                             new Pair(PaletteBackStyle.TabHighProfile,              "Tab - High Profile"),
                                             new Pair(PaletteBackStyle.TabStandardProfile,          "Tab - Standard Profile"),
                                             new Pair(PaletteBackStyle.TabLowProfile,               "Tab - Low Profile"),
                                             new Pair(PaletteBackStyle.TabOneNote,                  "Tab - OneNote"),
                                             new Pair(PaletteBackStyle.TabDock,                     "Tab - Dock"),
                                             new Pair(PaletteBackStyle.TabDockAutoHidden,           "Tab - Dock AutoHidden"),
                                             new Pair(PaletteBackStyle.TabCustom1,                  "Tab - Custom1"),
                                             new Pair(PaletteBackStyle.TabCustom2,                  "Tab - Custom2"),
                                             new Pair(PaletteBackStyle.TabCustom3,                  "Tab - Custom3") };
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBackStyleConverter clas.
        /// </summary>
        public PaletteBackStyleConverter()
            : base(typeof(PaletteBackStyle))
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
