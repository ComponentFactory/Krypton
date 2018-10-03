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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    #region IQuickAccessToolbarButton
    /// <summary>
	/// Information needed for a quick access toolbar entry.
	/// </summary>
    public interface IQuickAccessToolbarButton
	{
        /// <summary>
        /// Occurs when the quick access toolbar button has been clicked.
        /// </summary>
        event EventHandler Click;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Provides a back reference to the owning ribbon control instance.
        /// </summary>
        /// <param name="ribbon">Reference to owning instance.</param>
        void SetRibbon(KryptonRibbon ribbon);

        /// <summary>
        /// Gets the entry image.
        /// </summary>
        /// <returns>Image value.</returns>
		Image GetImage();

        /// <summary>
        /// Gets the entry text.
        /// </summary>
        /// <returns>Text value.</returns>
        string GetText();

        /// <summary>
        /// Gets the entry enabled state.
        /// </summary>
        /// <returns>Enabled value.</returns>
        bool GetEnabled();

        /// <summary>
        /// Gets the entry shortcut keys state.
        /// </summary>
        /// <returns>ShortcutKeys value.</returns>
        Keys GetShortcutKeys();

        /// <summary>
        /// Gets the entry visible state.
        /// </summary>
        /// <returns>Visible value.</returns>
        bool GetVisible();

        /// <summary>
        /// Sets a new value for the visible state.
        /// </summary>
        /// <param name="visible"></param>
        void SetVisible(bool visible);

        /// <summary>
        /// Gets the tooltip label style.
        /// </summary>
        LabelStyle GetToolTipStyle();

        /// <summary>
        /// Gets the image for the item ToolTip.
        /// </summary>
        Image GetToolTipImage();

        /// <summary>
        /// Gets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        Color GetToolTipImageTransparentColor();

        /// <summary>
        /// Gets the title text for the item ToolTip.
        /// </summary>
        string GetToolTipTitle();

        /// <summary>
        /// Gets the body text for the item ToolTip.
        /// </summary>
        string GetToolTipBody();

        /// <summary>
        /// Generates a Click event for a button.
        /// </summary>
        void PerformClick();
    }
	#endregion

    #region IRibbonGroupItem
    /// <summary>
    /// Information for a ribbon group item.
    /// </summary>
    public interface IRibbonGroupItem
    {
        /// <summary>
        /// Gets and sets the owning ribbon control instance.
        /// </summary>
        KryptonRibbon Ribbon { get; set; }

        /// <summary>
        /// Gets and sets the owning ribbon tab instance.
        /// </summary>
        KryptonRibbonTab RibbonTab { get; set; }

        /// <summary>
        /// Gets and sets the owning ribbon container instance.
        /// </summary>
        KryptonRibbonGroupContainer RibbonContainer { get; set; }

        /// <summary>
        /// Gets the visible state of the item.
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        GroupItemSize ItemSizeMaximum { get; set; }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        GroupItemSize ItemSizeMinimum { get; set; }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        GroupItemSize ItemSizeCurrent { get; set; }

        /// <summary>
        /// Return the spacing gap between the provided previous item and this item.
        /// </summary>
        /// <param name="previousItem">Previous item.</param>
        /// <returns>Pixel gap between previous item and this item.</returns>
        int ItemGap(IRibbonGroupItem previousItem);

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        ViewBase CreateView(KryptonRibbon ribbon, NeedPaintHandler needPaint);
    }
    #endregion

    #region IRibbonGroupContainer
    /// <summary>
    /// Information for a ribbon group container.
    /// </summary>
    public interface IRibbonGroupContainer
    {
        /// <summary>
        /// Gets an array of all the child components.
        /// </summary>
        /// <returns></returns>
        Component[] GetChildComponents();
    }
    #endregion

    #region IRibbonViewGroupItemView
    internal interface IRibbonViewGroupItemView
    {
        /// <summary>
        /// Override the group item size if possible.
        /// </summary>
        /// <param name="size">New size to use.</param>
        void SetGroupItemSize(GroupItemSize size);

        /// <summary>
        /// Reset the group item size to the item definition.
        /// </summary>
        void ResetGroupItemSize();

        /// <summary>
        /// Gets the first focus item from the item.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetFirstFocusItem();

        /// <summary>
        /// Gets the last focus item from the item.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetLastFocusItem();

        /// <summary>
        /// Gets the next focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetNextFocusItem(ViewBase current, ref bool matched);

        /// <summary>
        /// Gets the previous focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched);

        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <param name="keyTipList">List to add new entries into.</param>
        /// <param name="lineHint">Provide hint to item about its location.</param>
        void GetGroupKeyTips(KeyTipInfoList keyTipList, int lineHint);
    }
    #endregion

    #region IRibbonViewGroupContainerView
    internal interface IRibbonViewGroupContainerView
    {
        /// <summary>
        /// Gets an array of the allowed possible sizes of the container.
        /// </summary>
        /// <param name="context">Context used to calculate the sizes.</param>
        /// <returns>Array of size values.</returns>
        ItemSizeWidth[] GetPossibleSizes(ViewLayoutContext context);

        /// <summary>
        /// Update the group with the provided sizing solution.
        /// </summary>
        /// <param name="size">Value for the container.</param>
        void SetSolutionSize(ItemSizeWidth size);

        /// <summary>
        /// Reset the container back to its requested size.
        /// </summary>
        void ResetSolutionSize();

        /// <summary>
        /// Gets the first focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetFirstFocusItem();

        /// <summary>
        /// Gets the last focus item from the container.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetLastFocusItem();

        /// <summary>
        /// Gets the next focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetNextFocusItem(ViewBase current, ref bool matched);

        /// <summary>
        /// Gets the previous focus item based on the current item as provided.
        /// </summary>
        /// <param name="current">The view that is currently focused.</param>
        /// <param name="matched">Has the current focus item been matched yet.</param>
        /// <returns>ViewBase of item; otherwise false.</returns>
        ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched);

        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <param name="keyTipList">List to add new entries into.</param>
        void GetGroupKeyTips(KeyTipInfoList keyTipList);
    }
    #endregion

    #region IRibbonViewGroupContainerSize
    internal interface IRibbonViewGroupSize
    {
        /// <summary>
        /// Get an array of available widths for the group with associated sizing values.
        /// </summary>
        /// <param name="context">Context used to calculate the sizes.</param>
        /// <returns>Array of size values.</returns>
        GroupSizeWidth[] GetPossibleSizes(ViewLayoutContext context);

        /// <summary>
        /// Update the group with the provided sizing solution.
        /// </summary>
        /// <param name="size">Array of values for the group containers.</param>
        void SetSolutionSize(ItemSizeWidth[] size);
    }
    #endregion

    #region IRibbonKeyTipTarget
    internal interface IRibbonKeyTipTarget
    {
        /// <summary>
        /// Perform actual selection of the item.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        void KeyTipSelect(KryptonRibbon ribbon);
    }
    #endregion

    #region Enum GroupItemSize
    /// <summary>
    /// Specifies the size of a group item or container.
    /// </summary>
    public enum GroupItemSize
    {
        /// <summary>
        /// Specifies a group item display in its smallest display state.
        /// </summary>
        Small = 0,

        /// <summary>
        /// Specifies a group item display in its medium display state.
        /// </summary>
        Medium = 1,

        /// <summary>
        /// Specifies a group item display in its largest display state.
        /// </summary>
        Large = 2,
    }
    #endregion

    #region Enum GroupItemSizeSM
    /// <summary>
    /// Specifies the size of a group item or container but restricted to just small or medium.
    /// </summary>
    public enum GroupItemSizeSM
    {
        /// <summary>
        /// Specifies a group item display in its smallest display state.
        /// </summary>
        Small = 0,

        /// <summary>
        /// Specifies a group item display in its medium display state.
        /// </summary>
        Medium = 1
    }
    #endregion

    #region Enum GroupButtonType
    /// <summary>
    /// Specifies the type of operation for a group button.
    /// </summary>
    public enum GroupButtonType
    {
        /// <summary>
        /// Specifies a simple push button operation.
        /// </summary>
        Push,

        /// <summary>
        /// Specifies a check button that toggles between checked and not checked.
        /// </summary>
        Check,

        /// <summary>
        /// Specifies a button that when pressed shows a drop down.
        /// </summary>
        DropDown,

        /// <summary>
        /// Specifies a button that is split between push area and drop down area.
        /// </summary>
        Split,
    }
    #endregion

    #region Enum QATLocation
    /// <summary>
    /// Specifies the location of the quick access toolbar.
    /// </summary>
    public enum QATLocation
    {
        /// <summary>
        /// Specifies the quick access toolbar is above the ribbon.
        /// </summary>
        Above,

        /// <summary>
        /// Specifies the quick access toolbar is below the ribbon.
        /// </summary>
        Below,

        /// <summary>
        /// Specifies the quick access toolbar is hidden from view.
        /// </summary>
        Hidden,

    }
    #endregion

    #region Enum KeyTipMode
    internal enum KeyTipMode
    {
        /// <summary>
        /// Specifies the key tips are for root items.
        /// </summary>
        Root,

        /// <summary>
        /// Specifies the key tips are for the selected tabs groups.
        /// </summary>
        SelectedGroups,

        /// <summary>
        /// Specifies the key tips are for a popup group.
        /// </summary>
        PopupGroup,

        /// <summary>
        /// Specifies the key tips are for a popup QAT overflow.
        /// </summary>
        PopupQATOverflow,

        /// <summary>
        /// Specifies the key tips are for a minimized mode popup.
        /// </summary>
        PopupMinimized
    }
    #endregion

    #region Enum RibbonItemAlignment
    /// <summary>
    /// Specifies how items are aligned within a ribbon container.
    /// </summary>
    public enum RibbonItemAlignment
    {
        /// <summary>
        /// Specifies all items are aligned to the near edge.
        /// </summary>
        Near,

        /// <summary>
        /// Specifies all items are centered.
        /// </summary>
        Center,

        /// <summary>
        /// Specifies all items are aligned to the far edge.
        /// </summary>
        Far,
    }
    #endregion

    #region Delegates
    /// <summary>
    /// Signature of a click event that expects the provided finish delegate to be called when associated actions are completed.
    /// </summary>
    /// <param name="sender">Event source.</param>
    /// <param name="clickFinished">Delegate for finish notication.</param>
    public delegate void ClickAndFinishHandler(object sender, EventHandler clickFinished);
    #endregion
}
