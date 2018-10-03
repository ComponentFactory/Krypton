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
using System.Collections.Generic;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    #region PageButtonSpecCollection
    /// <summary>
    /// Collection for managing ButtonSpecAny instances for a KryptonPage.
    /// </summary>
    public class PageButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public PageButtonSpecCollection(KryptonPage owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region NavigatorButtonSpecCollection
    /// <summary>
    /// Collection for managing NavigatorButtonSpec instances for a KryptonNavigator.
    /// </summary>
    public class NavigatorButtonSpecCollection : ButtonSpecCollection<ButtonSpecNavigator> 
    { 
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public NavigatorButtonSpecCollection(KryptonNavigator owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region NavFixedButtonSpecCollection
    /// <summary>
    /// Collection for managing NavigatorButtonSpec instances.
    /// </summary>
    public class NavFixedButtonSpecCollection : ButtonSpecCollection<ButtonSpecNavFixed> 
    { 
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavFixedButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public NavFixedButtonSpecCollection(KryptonNavigator owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Enum KryptonPageFlags
    /// <summary>
    /// Specifies flags that can be applied to a KryptonPage
    /// </summary>
    [Flags()]
    public enum KryptonPageFlags : int
    {
        /// <summary>Specifies that in the Outlook mode the page is shown on the overflow bar.</summary>
        PageInOverflowBarForOutlookMode = 0x0001,

        /// <summary>Specifies that the page is allowed to be saved to configuration.</summary>
        AllowConfigSave = 0x0002,

        /// <summary>Specifies that the user is allowed to close a docking page.</summary>
        DockingAllowClose = 0x0004,

        /// <summary>Specifies that the user is allowed to select from a drop down button.</summary>
        DockingAllowDropDown = 0x008,

        /// <summary>Specifies that the user is allowed to make a page auto hidden.</summary>
        DockingAllowAutoHidden = 0x0010,

        /// <summary>Specifies that the user is allowed to make a page docked.</summary>
        DockingAllowDocked = 0x0020,

        /// <summary>Specifies that the user is allowed to make a page floating.</summary>
        DockingAllowFloating = 0x0040,

        /// <summary>Specifies that the user is allowed to make a page tabbed in a workspace.</summary>
        DockingAllowWorkspace = 0x0080,

        /// <summary>Specifies that the user is allowed to make a page tabbed in a navigator.</summary>
        DockingAllowNavigator = 0x0100,

        /// <summary>Specifies that the page is allowed to be drag reordered.</summary>
        AllowPageReorder = 0x0200,

        /// <summary>Specifies that the page is allowed to be dragged from the navigator.</summary>
        AllowPageDrag = 0x0400,

        /// <summary>Specifies that all flags are set.</summary>
        All = 0xFFFF,
    }
    #endregion

    #region Enum NavigatorMode
    /// <summary>
    /// Specifies the display mode of the Navigator control.
    /// </summary>
    [TypeConverter(typeof(NavigatorModeConverter))]
    public enum NavigatorMode
    {
        /// <summary>Specifies that tabs are placed on a bar outside a group.</summary>
        BarTabGroup,

        /// <summary>Specifies that tabs are placed on a bar without showing pages.</summary>
        BarTabOnly,

        /// <summary>Specifies that ribbons tabs are placed on a bar outside a group.</summary>
        BarRibbonTabGroup,

        /// <summary>Specifies that ribbon tabs are placed on a bar without showing pages.</summary>
        BarRibbonTabOnly,

        /// <summary>Specifies that check buttons are placed on a bar outside a group.</summary>
        BarCheckButtonGroupOutside,

        /// <summary>Specifies that check buttons are placed on a bar inside a group.</summary>
        BarCheckButtonGroupInside,

        /// <summary>Specifies that check buttons are placed on a bar inside a group without showing pages.</summary>
        BarCheckButtonGroupOnly,

        /// <summary>Specifies that check buttons are placed on a bar without showing pages.</summary>
        BarCheckButtonOnly,

        /// <summary>Specifies that check buttons are placed on a bar inside a header in a group.</summary>
        HeaderBarCheckButtonGroup,

        /// <summary>Specifies that check buttons are placed on a bar inside a header in a header group.</summary>
        HeaderBarCheckButtonHeaderGroup,

        /// <summary>Specifies that check buttons are placed on a bar inside a header without showing pages.</summary>
        HeaderBarCheckButtonOnly,

        /// <summary>Specifies that check buttons are stacked inside a group.</summary>
        StackCheckButtonGroup,

        /// <summary>Specifies that check buttons are stacked inside a header group.</summary>
        StackCheckButtonHeaderGroup,

        /// <summary>Specifies a navigation mode similar to the expanded Microsoft Outlook Navigator.</summary>
        OutlookFull,

        /// <summary>Specifies a navigation mode similar to the collapsed Microsoft Outlook Navigator.</summary>
        OutlookMini,

        /// <summary>Specifies a KryptonHeaderGroup style of appearance.</summary>
        HeaderGroup,

        /// <summary>Specifies a KryptonHeaderGroup style of appearance combined with a set of tabs.</summary>
        HeaderGroupTab,

        /// <summary>Specifies a KryptonGroup style of appearance.</summary>
        Group,

        /// <summary>Specifies a KryptonPanel style of appearance.</summary>
        Panel,
    }
    #endregion

    #region Enum MapKryptonPageText
    /// <summary>
    /// Specifies the mapping from KryptonPage text property.
    /// </summary>
    [TypeConverter(typeof(MapKryptonPageTextConverter))]
    public enum MapKryptonPageText
    {
        /// <summary>
        /// Specifies no mapping take place.
        /// </summary>
        None,

        /// <summary>
        /// Specifies use of the KryptonPage.Text property.
        /// </summary>
        Text,

        /// <summary>
        /// Specifies using the text property in preference in the title property.
        /// </summary>
        TextTitle,

        /// <summary>
        /// Specifies use of the text/title and description properties in that order of preference.
        /// </summary>
        TextTitleDescription,

        /// <summary>
        /// Specifies using the text property in preference in the description property.
        /// </summary>
        TextDescription,

        /// <summary>
        /// Specifies use of the KryptonPage.TextTitle property.
        /// </summary>
        Title,

        /// <summary>
        /// Specifies using the title property in preference in the text property.
        /// </summary>
        TitleText,

        /// <summary>
        /// Specifies using the title property in preference in the description property.
        /// </summary>
        TitleDescription,

        /// <summary>
        /// Specifies use of the KryptonPage.TextDescription property.
        /// </summary>
        Description,

        /// <summary>
        /// Specifies using the description property in preference in the text property.
        /// </summary>
        DescriptionText,

        /// <summary>
        /// Specifies using the description property in preference in the title property.
        /// </summary>
        DescriptionTitle,

        /// <summary>
        /// Specifies use of the description/title and text properties in that order of preference.
        /// </summary>
        DescriptionTitleText,

        /// <summary>
        /// Specifies use of the KryptonPage.ToolTipTitle property.
        /// </summary>
        ToolTipTitle,

        /// <summary>
        /// Specifies use of the KryptonPage.ToolTipBody property.
        /// </summary>
        ToolTipBody,
    }
    #endregion

    #region Enum MapKryptonPageImage
    /// <summary>
    /// Specifies the mapping from KryptonPage image property.
    /// </summary>
    [TypeConverter(typeof(MapKryptonPageImageConverter))]
    public enum MapKryptonPageImage
    {
        /// <summary>
        /// Specifies no mapping take place.
        /// </summary>
        None,

        /// <summary>
        /// Specifies use of the KryptonPage.ImageSmall property.
        /// </summary>
        Small,

        /// <summary>
        /// Specifies use of small and medium in that preference order.
        /// </summary>
        SmallMedium,

        /// <summary>
        /// Specifies use of small/medium and large in that preference order.
        /// </summary>
        SmallMediumLarge,

        /// <summary>
        /// Specifies use of the KryptonPage.ImageMedium property.
        /// </summary>
        Medium,

        /// <summary>
        /// Specifies use of medium and small in that preference order.
        /// </summary>
        MediumSmall,

        /// <summary>
        /// Specifies use of medium and large in that preference order.
        /// </summary>
        MediumLarge,

        /// <summary>
        /// Specifies use of the KryptonPage.ImageLarge property.
        /// </summary>
        Large,

        /// <summary>
        /// Specifies use of large and medium in that preference order.
        /// </summary>
        LargeMedium,

        /// <summary>
        /// Specifies use of large/medium and small in that preference order.
        /// </summary>
        LargeMediumSmall,

        /// <summary>
        /// Specifies use of the KryptonPage.ToolTipImage property.
        /// </summary>
        ToolTip,
    }
    #endregion

    #region Enum ButtonDisplay
    /// <summary>
    /// Specifies the display logic for the a button on the navigator.
    /// </summary>
    [TypeConverter(typeof(ButtonDisplayConverter))]
    public enum ButtonDisplay
    {
        /// <summary>Specifies the button is never shown.</summary>
        Hide,

        /// <summary>Specifies the button is always shown but always disabled.</summary>
        ShowDisabled,

        /// <summary>Specifies the button is always shown but always enabled.</summary>
        ShowEnabled,

        /// <summary>Specifies the button is is enabled and shown depending on state logic.</summary>
        Logic
    }
    #endregion

    #region Enum ButtonDisplayLogic
    /// <summary>
    /// Specifies how buttons using logic should be presented.
    /// </summary>
    [TypeConverter(typeof(ButtonDisplayLogicConverter))]
    public enum ButtonDisplayLogic
    {
        /// <summary>Specifies that no selection actions are presented.</summary>
        None,

        /// <summary>Specifies that next and previous buttons are presented.</summary>
        NextPrevious,

        /// <summary>Specifies that the context button is presented.</summary>
        Context,

        /// <summary>Specifies that context, next and previous buttons are presented.</summary>
        ContextNextPrevious,
    }
    #endregion

    #region Enum DirectionButtonAction
    /// <summary>
    /// Specifies the action to take when previous or next button is fired.
    /// </summary>
    [TypeConverter(typeof(DirectionButtonActionConverter))]
    public enum DirectionButtonAction
    {
        /// <summary>Specifies no action be taken.</summary>
        None,

        /// <summary>Specifies a page is selected.</summary>
        SelectPage,

        /// <summary>Specifies the item bar be moved to show more items.</summary>
        MoveBar,

        /// <summary>Specifies the appropriate action for the mode be applied.</summary>
        ModeAppropriateAction
    }
    #endregion

    #region Enum DirectionButtonAction
    /// <summary>
    /// Specifies the action to take when context button is fired.
    /// </summary>
    [TypeConverter(typeof(ContextButtonActionConverter))]
    public enum ContextButtonAction
    {
        /// <summary>Specifies no action be taken.</summary>
        None,

        /// <summary>Specifies a page is selected.</summary>
        SelectPage
    }
    #endregion

    #region Enum CloseButtonAction
    /// <summary>
    /// Specifies the action to take when close button is fired.
    /// </summary>
    [TypeConverter(typeof(CloseButtonActionConverter))]
    public enum CloseButtonAction
    {
        /// <summary>Specifies no action be taken.</summary>
        None,

        /// <summary>Specifies the current page be removed from the pages collection.</summary>
        RemovePage,

        /// <summary>Specifies the current page be removed from the pages collection and then disposed.</summary>
        RemovePageAndDispose,

        /// <summary>Specifies the current page be hidden.</summary>
        HidePage,
    }
    #endregion

    #region Enum BarItemSize
    /// <summary>
    /// Specifies the how the size of each bar item is calculated.
    /// </summary>
    [TypeConverter(typeof(BarItemSizingConverter))]
    public enum BarItemSizing
    {
        /// <summary>Specifies each item has its own calculated size.</summary>
        Individual,

        /// <summary>Specifies all items have the same height but individual width.</summary>
        SameHeight,

        /// <summary>Specifies all items have the same width but individual height.</summary>
        SameWidth,

        /// <summary>Specifies all items have the same with and height.</summary>
        SameWidthAndHeight,
    }
    #endregion

    #region Enum BarMultiline
    /// <summary>
    /// Specifies how items are placed within lines for display in bar.
    /// </summary>
    public enum BarMultiline
    {
        /// <summary>Specifies items are placed on single line but may not all be visible.</summary>
        Singleline,

        /// <summary>Specifies items are split over number of lines required to fully show all items.</summary>
        Multiline,

        /// <summary>Specifies items are placed on single line and shrunk/expanded to fit the line exactly.</summary>
        Exactline,

        /// <summary>Specifies items are placed on single line and shrunk to try and make all visible.</summary>
        Shrinkline,

        /// <summary>Specifies items are placed on single line and expanded to try and fill the entire line.</summary>
        Expandline,
    }
    #endregion

    #region Enum PaletteButtonSpecStyle
    /// <summary>
    /// Specifies the style of button spec for the Navigator.
    /// </summary>
    [TypeConverter(typeof(PaletteNavButtonSpecStyleConverter))]
    public enum PaletteNavButtonSpecStyle
    {
        /// <summary>
        /// Specifies a general purpose button specification.
        /// </summary>
        Generic,

        /// <summary>
        /// Specifies a left pointing arrow button specification.
        /// </summary>
        ArrowLeft,

        /// <summary>
        /// Specifies a right pointing arrow button specification.
        /// </summary>
        ArrowRight,

        /// <summary>
        /// Specifies an upwards pointing arrow button specification.
        /// </summary>
        ArrowUp,

        /// <summary>
        /// Specifies a downwards pointing arrow button specification.
        /// </summary>
        ArrowDown,

        /// <summary>
        /// Specifies a drop down button specification.
        /// </summary>
        DropDown,

        /// <summary>
        /// Specifies a vertical pin specification.
        /// </summary>
        PinVertical,

        /// <summary>
        /// Specifies a horizontal pin specification.
        /// </summary>
        PinHorizontal,

        /// <summary>
        /// Specifies a form close button specification.
        /// </summary>
        FormClose,

        /// <summary>
        /// Specifies a form minimize button specification.
        /// </summary>
        FormMin,

        /// <summary>
        /// Specifies a form maximize button specification.
        /// </summary>
        FormMax,

        /// <summary>
        /// Specifies a form restore button specification.
        /// </summary>
        FormRestore,

        /// <summary>
        /// Specifies a pendant close button specification.
        /// </summary>
        PendantClose,

        /// <summary>
        /// Specifies a pendant minimize button specification.
        /// </summary>
        PendantMin,

        /// <summary>
        /// Specifies a pendant restore button specification.
        /// </summary>
        PendantRestore,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceMaximize,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceRestore,

        /// <summary>
        /// Specifies a ribbon minimize button specification.
        /// </summary>
        RibbonMinimize,

        /// <summary>
        /// Specifies a ribbon expand button specification.
        /// </summary>
        RibbonExpand,
    }
    #endregion

    #region Enum PopupPageAllow
    /// <summary>
    /// Specifies whe popup pages are allowed to be used.
    /// </summary>
    [TypeConverter(typeof(PopupPageAllowConverter))]
    public enum PopupPageAllow
    {
        /// <summary>Specifies that popup pages are never used.</summary>
        Never,

        /// <summary>Specifies that popup pages are used in compatible modes.</summary>
        OnlyCompatibleModes,

        /// <summary>Specifies that popup pages are used in Outlook Mini mode only.</summary>
        OnlyOutlookMiniMode
    }
    #endregion

    #region Enum PopupPageElement
    /// <summary>
    /// Specifies the relative element to use when deciding on screen size and position.
    /// </summary>
    public enum PopupPageElement
    {
        /// <summary>Specifies the popup is relative to the page item.</summary>
        Item,

        /// <summary>Specifies the popup is relative to the entire navigator.</summary>
        Navigator,
    }
    #endregion

    #region Enum PopupPagePosition
    /// <summary>
    /// Specifies how to auto calculate the popup page position.
    /// </summary>
    [TypeConverter(typeof(PopupPagePositionConverter))]
    public enum PopupPagePosition
    {
        /// <summary>Specifies the popup is positioned appropriately for the mode and mode settings.</summary>
        ModeAppropriate,

        /// <summary>Specifies the popup is above the relative item and aligned to near edge.</summary>
        AboveNear,

        /// <summary>Specifies the popup is above the relative item and aligned to far edge.</summary>
        AboveFar,

        /// <summary>Specifies the popup is above the relative item and same width as the relative item.</summary>
        AboveMatch,

        /// <summary>Specifies the popup is below the relative item and aligned to near edge.</summary>
        BelowNear,

        /// <summary>Specifies the popup is below the relative item and aligned to far edge.</summary>
        BelowFar,

        /// <summary>Specifies the popup is below the relative item and same width as the relative item.</summary>
        BelowMatch,

        /// <summary>Specifies the popup is to the far side of the relative item and aligned to the top edge.</summary>
        FarTop,

        /// <summary>Specifies the popup is to the far side of the relative item and aligned to the bottom edge.</summary>
        FarBottom,

        /// <summary>Specifies the popup is to the far side of the relative item and same height as the relative item.</summary>
        FarMatch,

        /// <summary>Specifies the popup is to the near side of the relative item and aligned to the top edge.</summary>
        NearTop,

        /// <summary>Specifies the popup is to the near side of the relative item and aligned to the bottom edge.</summary>
        NearBottom,

        /// <summary>Specifies the popup is to the near side of the relative item and same height as the relative item.</summary>
        NearMatch,
    }
    #endregion

    #region Emum DragTargetHint
    /// <summary>
    /// Specifies a hint about the action that occurs on drop.
    /// </summary>
    public enum DragTargetHint
    {
        /// <summary>Specifies the target has no hint information.</summary>
        None = 0x0000,

        /// <summary>Specifies the target will position drop at the left edge.</summary>
        EdgeLeft = 0x0001,

        /// <summary>Specifies the target will position drop at the right edge.</summary>
        EdgeRight = 0x0002,

        /// <summary>Specifies the target will position drop at the top edge.</summary>
        EdgeTop = 0x0003,

        /// <summary>Specifies the target will position drop at the bottom edge.</summary>
        EdgeBottom = 0x0004,

        /// <summary>Specifies the target will transfer content into the target.</summary>
        Transfer = 0x0005,

        /// <summary>Specifies the target will transfer content into the target.</summary>
        ExcludeFlags = 0x00FF,

        /// <summary>Specifies the target does not allow itself to be combined with others for cluster docking.</summary>
        ExcludeCluster = 0x0100,
    }
    #endregion

    #region Interface IDragTargetProvider
    /// <summary>
    /// Interface for providing drag targets.
    /// </summary>
    public interface IDragTargetProvider
    {
        /// <summary>
        /// Generate a list of drag targets that are relevant to the provided end data.
        /// </summary>
        /// <param name="dragEndData">Pages data being dragged.</param>
        /// <returns>List of drag targets.</returns>
        DragTargetList GenerateDragTargets(PageDragEndData dragEndData);
    }
    #endregion

    #region Interface IDropDockingIndicator
    /// <summary>
    /// Interface for allowing generic access to drop docking indicator implementations.
    /// </summary>
    public interface IDropDockingIndicator
    {
        /// <summary>
		/// Show the window relative to provided screen rectangle.
		/// </summary>
		/// <param name="screenRect">Screen rectangle.</param>
        void ShowRelative(Rectangle screenRect);

		/// <summary>
		/// Perofrm mouse hit testing against a screen point.
		/// </summary>
		/// <param name="screenPoint">Screen point.</param>
		/// <returns>Area that is active.</returns>
        int ScreenMouseMove(Point screenPoint);

		/// <summary>
		/// Ensure the state is updated to reflect the mouse not being over the control.
		/// </summary>
        void MouseReset();

        /// <summary>
        /// Hide the window from display.
        /// </summary>
        void Hide();
    }
    #endregion

    #region Interface IDragPageNotify
    /// <summary>
    /// Interface for receiving page notifications.
    /// </summary>
    public interface IDragPageNotify
    {
        /// <summary>
        /// Occurs when a page drag is about to begin and allows it to be cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; should never be null.</param>
        /// <param name="navigator">Navigator instance associated with source; can be null.</param>
        /// <param name="e">Event arguments indicating list of pages being dragged.</param>
        void PageDragStart(object sender, KryptonNavigator navigator, PageDragCancelEventArgs e);

        /// <summary>
        /// Occurs when the mouse moves during the drag operation.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        void PageDragMove(object sender, PointEventArgs e);

        /// <summary>
        /// Occurs when drag operation completes with pages being dropped.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        bool PageDragEnd(object sender, PointEventArgs e);

        /// <summary>
        /// Occurs when dragging pages has been cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        void PageDragQuit(object sender);
    }
    #endregion

    #region Interface INavCheckItem
    /// <summary>
    /// Interface for an individual bar check item.
    /// </summary>
    internal interface INavCheckItem
    {
        /// <summary>
        /// Occurs when the dragging start rectangle is needed.
        /// </summary>
        event EventHandler<ButtonDragRectangleEventArgs> ButtonDragRectangle;

        /// <summary>
        /// Occurs when the dragging offset has changed.
        /// </summary>
        event EventHandler<ButtonDragOffsetEventArgs> ButtonDragOffset;

        /// <summary>
        /// Gets the view associated with the check item.
        /// </summary>
        ViewBase View { get; }

        /// <summary>
        /// Gets the page this check item represents.
        /// </summary>
        KryptonPage Page { get; }

        /// <summary>
        /// Gets the navigator this check item is inside.
        /// </summary>
        KryptonNavigator Navigator { get; }

        /// <summary>
        /// Gets and sets the checked state of the check item.
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Gets and sets if the check item has the focus.
        /// </summary>
        bool HasFocus { get; set; }

        /// <summary>
        /// Gets and sets the paint delegate to use for refresh requests.
        /// </summary>
        NeedPaintHandler NeedPaint { get; set; }
        
        /// <summary>
        /// Gets the ButtonSpec associated with the provided item.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        ButtonSpec ButtonSpecFromView(ViewBase element);

        /// <summary>
        /// Raises the Click event for the button.
        /// </summary>
        void PerformClick();

        /// <summary>
        /// Set the orientation of the background/border and content.
        /// </summary>
        /// <param name="borderBackOrient">Orientation of the button border and background..</param>
        /// <param name="contentOrient">Orientation of the button contents.</param>
        void SetOrientation(VisualOrientation borderBackOrient,
                            VisualOrientation contentOrient);
    }
    #endregion

    #region Delegates
    /// <summary>
    /// Signature of method that provides a KryptonPageFlags enumeration value.
    /// </summary>
    /// <param name="sender">Source of the call.</param>
    /// <param name="e">A KryptonPageFlagsEventArgs containing event information.</param>
    public delegate void KryptonPageFlagsEventHandler(object sender, KryptonPageFlagsEventArgs e);
    #endregion
}
