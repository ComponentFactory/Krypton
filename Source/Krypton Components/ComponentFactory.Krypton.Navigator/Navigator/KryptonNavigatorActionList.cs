// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using ComponentFactory.Krypton.Toolkit;
using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Designer time action list for the navigator.
    /// </summary>
    public class KryptonNavigatorActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private KryptonNavigatorDesigner _designer;
        private IComponentChangeService _serviceComponentChange;
        private DesignerActionUIService _serviceDesignerAction;
        private DesignerVerb _headerBarVisible;
        private DesignerVerb _headerPrimaryVisible;
        private DesignerVerb _headerSecondaryVisible;
        private string _headerBarText;
        private string _headerPrimaryText;
        private string _headerSecondaryText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonNavigatorActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonNavigatorActionList(KryptonNavigatorDesigner owner)
            : base(owner.Component)
        {
            // Remember designer and actual component instance being designed
            _designer = owner;
            _navigator = (KryptonNavigator)owner.Component;

            // Cache service used to notify when a property has changed
            _serviceComponentChange = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            _serviceDesignerAction = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the navigator mode.
        /// </summary>
        public NavigatorMode NavigatorMode
        {
            get { return _navigator.NavigatorMode; }

            set
            {
                if (_navigator.NavigatorMode != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.NavigatorMode, value);
                    _navigator.NavigatorMode = value;
                    _serviceDesignerAction.Refresh(_navigator);
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar orientation.
        /// </summary>
        public PaletteBackStyle PageBackStyle
        {
            get { return _navigator.PageBackStyle; }

            set
            {
                if (_navigator.PageBackStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.PageBackStyle, value);
                    _navigator.PageBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar orientation.
        /// </summary>
        public VisualOrientation BarOrientation
        {
            get { return _navigator.Bar.BarOrientation; }

            set
            {
                if (_navigator.Bar.BarOrientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.BarOrientation, value);
                    _navigator.Bar.BarOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar item orientation.
        /// </summary>
        public ButtonOrientation ItemOrientationBar
        {
            get { return _navigator.Bar.ItemOrientation; }

            set
            {
                if (_navigator.Bar.ItemOrientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.ItemOrientation, value);
                    _navigator.Bar.ItemOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the stack item orientation.
        /// </summary>
        public ButtonOrientation ItemOrientationStack
        {
            get { return _navigator.Stack.ItemOrientation; }

            set
            {
                if (_navigator.Stack.ItemOrientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Stack.ItemOrientation, value);
                    _navigator.Stack.ItemOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the stack item orientation.
        /// </summary>
        public ButtonOrientation ItemOrientationOutlook
        {
            get { return _navigator.Outlook.ItemOrientation; }

            set
            {
                if (_navigator.Outlook.ItemOrientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Outlook.ItemOrientation, value);
                    _navigator.Outlook.ItemOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the alignment of items on the bar.
        /// </summary>
        public RelativePositionAlign ItemAlignment
        {
            get { return _navigator.Bar.ItemAlignment; }

            set
            {
                if (_navigator.Bar.ItemAlignment != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.ItemAlignment, value);
                    _navigator.Bar.ItemAlignment = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar item sizing.
        /// </summary>
        public BarItemSizing ItemSizing
        {
            get { return _navigator.Bar.ItemSizing; }

            set
            {
                if (_navigator.Bar.ItemSizing != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.ItemSizing, value);
                    _navigator.Bar.ItemSizing = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets tab border style for the bar modes.
        /// </summary>
        public TabBorderStyle TabBorderStyle
        {
            get { return _navigator.Bar.TabBorderStyle; }

            set
            {
                if (_navigator.Bar.TabBorderStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.TabBorderStyle, value);
                    _navigator.Bar.TabBorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets tab style for the bar modes.
        /// </summary>
        public TabStyle TabStyle
        {
            get { return _navigator.Bar.TabStyle; }

            set
            {
                if (_navigator.Bar.TabStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.TabStyle, value);
                    _navigator.Bar.TabStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets checkbutton style for the bar modes.
        /// </summary>
        public ButtonStyle CheckButtonStyleBar
        {
            get { return _navigator.Bar.CheckButtonStyle; }

            set
            {
                if (_navigator.Bar.CheckButtonStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Bar.CheckButtonStyle, value);
                    _navigator.Bar.CheckButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets checkbutton style for the stack modes.
        /// </summary>
        public ButtonStyle CheckButtonStyleStack
        {
            get { return _navigator.Stack.CheckButtonStyle; }

            set
            {
                if (_navigator.Stack.CheckButtonStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Stack.CheckButtonStyle, value);
                    _navigator.Stack.CheckButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets checkbutton style for the outlook mode.
        /// </summary>
        public ButtonStyle CheckButtonStyleOutlook
        {
            get { return _navigator.Outlook.CheckButtonStyle; }

            set
            {
                if (_navigator.Outlook.CheckButtonStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Outlook.CheckButtonStyle, value);
                    _navigator.Outlook.CheckButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the logic used to display buttons.
        /// </summary>
        public ButtonDisplayLogic ButtonDisplayLogic
        {
            get { return _navigator.Button.ButtonDisplayLogic; }

            set
            {
                if (_navigator.Button.ButtonDisplayLogic != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Button.ButtonDisplayLogic, value);
                    _navigator.Button.ButtonDisplayLogic = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of the close button.
        /// </summary>
        public ButtonDisplay CloseButtonDisplay
        {
            get { return _navigator.Button.CloseButtonDisplay; }

            set
            {
                if (_navigator.Button.CloseButtonDisplay != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Button.CloseButtonDisplay, value);
                    _navigator.Button.CloseButtonDisplay = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the action of the close button.
        /// </summary>
        public CloseButtonAction CloseButtonAction
        {
            get { return _navigator.Button.CloseButtonAction; }

            set
            {
                if (_navigator.Button.CloseButtonAction != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Button.CloseButtonAction, value);
                    _navigator.Button.CloseButtonAction = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group background style.
        /// </summary>
        public PaletteBackStyle GroupBackStyle
        {
            get { return _navigator.Group.GroupBackStyle; }

            set
            {
                if (_navigator.Group.GroupBackStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Group.GroupBackStyle, value);
                    _navigator.Group.GroupBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group border style.
        /// </summary>
        public PaletteBorderStyle GroupBorderStyle
        {
            get { return _navigator.Group.GroupBorderStyle; }

            set
            {
                if (_navigator.Group.GroupBorderStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Group.GroupBorderStyle, value);
                    _navigator.Group.GroupBorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar header style.
        /// </summary>
        public HeaderStyle BarHeaderStyle
        {
            get { return _navigator.Header.HeaderStyleBar; }

            set
            {
                if (_navigator.Header.HeaderStyleBar != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderStyleBar, value);
                    _navigator.Header.HeaderStyleBar = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the bar header position.
        /// </summary>
        public VisualOrientation BarHeaderPosition
        {
            get { return _navigator.Header.HeaderPositionBar; }

            set
            {
                if (_navigator.Header.HeaderPositionBar != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderPositionBar, value);
                    _navigator.Header.HeaderPositionBar = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the primary header style.
        /// </summary>
        public HeaderStyle PrimaryHeaderStyle
        {
            get { return _navigator.Header.HeaderStylePrimary; }

            set
            {
                if (_navigator.Header.HeaderStylePrimary != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderStylePrimary, value);
                    _navigator.Header.HeaderStylePrimary = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the primary header position.
        /// </summary>
        public VisualOrientation PrimaryHeaderPosition
        {
            get { return _navigator.Header.HeaderPositionPrimary; }

            set
            {
                if (_navigator.Header.HeaderPositionPrimary != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderPositionPrimary, value);
                    _navigator.Header.HeaderPositionPrimary = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the secondary header style.
        /// </summary>
        public HeaderStyle SecondaryHeaderStyle
        {
            get { return _navigator.Header.HeaderStyleSecondary; }

            set
            {
                if (_navigator.Header.HeaderStyleSecondary != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderStyleSecondary, value);
                    _navigator.Header.HeaderStyleSecondary = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the secondary header position.
        /// </summary>
        public VisualOrientation SecondaryHeaderPosition
        {
            get { return _navigator.Header.HeaderPositionSecondary; }

            set
            {
                if (_navigator.Header.HeaderPositionSecondary != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Header.HeaderPositionSecondary, value);
                    _navigator.Header.HeaderPositionSecondary = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the secondary header visibility for the outlook mode.
        /// </summary>
        public InheritBool SecondaryHeaderOutlookVisible
        {
            get { return _navigator.Outlook.HeaderSecondaryVisible; }

            set
            {
                if (_navigator.Outlook.HeaderSecondaryVisible != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Outlook.HeaderSecondaryVisible, value);
                    _navigator.Outlook.HeaderSecondaryVisible = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the orientation for the Outlook mode.
        /// </summary>
        public Orientation OutlookOrientation
        {
            get { return _navigator.Outlook.Orientation; }

            set
            {
                if (_navigator.Outlook.Orientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Outlook.Orientation, value);
                    _navigator.Outlook.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the panel background style.
        /// </summary>
        public PaletteBackStyle PanelBackStyle
        {
            get { return _navigator.Panel.PanelBackStyle; }

            set
            {
                if (_navigator.Panel.PanelBackStyle != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Panel.PanelBackStyle, value);
                    _navigator.Panel.PanelBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the stack orientation.
        /// </summary>
        public Orientation StackOrientation
        {
            get { return _navigator.Stack.StackOrientation; }

            set
            {
                if (_navigator.Stack.StackOrientation != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Stack.StackOrientation, value);
                    _navigator.Stack.StackOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the stack alignment.
        /// </summary>
        public RelativePositionAlign StackAlignment
        {
            get { return _navigator.Stack.StackAlignment; }

            set
            {
                if (_navigator.Stack.StackAlignment != value)
                {
                    _serviceComponentChange.OnComponentChanged(_navigator, null, _navigator.Stack.StackAlignment, value);
                    _navigator.Stack.StackAlignment = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_navigator != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionPropertyItem("NavigatorMode", "Mode", "Appearance", "Navigator mode"));
                actions.Add(new DesignerActionPropertyItem("PageBackStyle", "Page Style", "Appearance", "Page background style."));

                // Add the categories appropriate to the mode
                switch (_navigator.NavigatorMode)
                {
                    case NavigatorMode.Panel:
                        AddPanelActions(actions);
                        break;
                    case NavigatorMode.Group:
                        AddGroupActions(actions);
                        break;
                    case NavigatorMode.HeaderGroup:
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, true, true, false);
                        break;
                    case NavigatorMode.BarCheckButtonOnly:
                        AddBarActions(actions, true, true, false);
                        AddButtonsActions(actions);
                        AddPanelActions(actions);
                        break;
                    case NavigatorMode.BarCheckButtonGroupOnly:
                    case NavigatorMode.BarCheckButtonGroupInside:
                        AddBarActions(actions, true, true, false);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        break;
                    case NavigatorMode.BarCheckButtonGroupOutside:
                        AddBarActions(actions, true, true, false);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddPanelActions(actions);
                        break;
                    case NavigatorMode.HeaderBarCheckButtonOnly:
                        AddBarActions(actions, false, true, false);
                        AddButtonsActions(actions);
                        AddHeadersActions(actions, false, false, true);
                        break;
                    case NavigatorMode.HeaderBarCheckButtonGroup:
                        AddBarActions(actions, false, true, false);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, false, false, true);
                        break;
                    case NavigatorMode.HeaderGroupTab:
                        AddBarActions(actions, false, true, false);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, false, false, true);
                        break;
                    case NavigatorMode.HeaderBarCheckButtonHeaderGroup:
                        AddBarActions(actions, false, true, false);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, true, true, true);
                        break;
                    case NavigatorMode.StackCheckButtonGroup:
                        AddStackActions(actions);
                        AddGroupActions(actions);
                        break;
                    case NavigatorMode.StackCheckButtonHeaderGroup:
                        AddStackActions(actions);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, true, true, false);
                        break;
                    case NavigatorMode.OutlookFull:
                    case NavigatorMode.OutlookMini:
                        AddOutlookActions(actions);
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        AddHeadersActions(actions, true, false, false);
                        break;
                    case NavigatorMode.BarTabGroup:
                    case NavigatorMode.BarRibbonTabGroup:
                        AddBarActions(actions, true, false, (_navigator.NavigatorMode == NavigatorMode.BarRibbonTabGroup));
                        AddButtonsActions(actions);
                        AddGroupActions(actions);
                        break;
                    case NavigatorMode.BarTabOnly:
                    case NavigatorMode.BarRibbonTabOnly:
                        AddBarActions(actions, true, false, (_navigator.NavigatorMode == NavigatorMode.BarRibbonTabGroup));
                        AddButtonsActions(actions);
                        AddPanelActions(actions);
                        break;
                }

                AddConstantActions(actions);
            }

            return actions;
        }

        /// <summary>
        /// Add a new page to the navigator.
        /// </summary>
        public void AddPage()
        {
            _designer.AddPage();
            _serviceDesignerAction.Refresh(_navigator);
        }

        /// <summary>
        /// Remove the current page from the navigator.
        /// </summary>
        public void RemovePage()
        {
            _designer.RemovePage();
            _serviceDesignerAction.Refresh(_navigator);
        }

        /// <summary>
        /// Remove all pages from the navigator.
        /// </summary>
        public void ClearPages()
        {
            _designer.ClearPages();
            _serviceDesignerAction.Refresh(_navigator);
        }
        #endregion

        #region Implementation
        private void AddBarActions(DesignerActionItemCollection actions,
                                   bool includeBarOrientation,
                                   bool checkButton,
                                   bool ignore)
        {
            actions.Add(new DesignerActionHeaderItem("Bar"));

            if (includeBarOrientation)
                actions.Add(new DesignerActionPropertyItem("BarOrientation", "Bar Orientation", "Bar", "Orientation of the bar."));

            actions.Add(new DesignerActionPropertyItem("ItemOrientationBar", "Item Orientation", "Bar", "Orientation of the bar items."));
            actions.Add(new DesignerActionPropertyItem("ItemAlignment", "Item Alignment", "Bar", "Alignment of items on the bar."));
            actions.Add(new DesignerActionPropertyItem("ItemSizing", "Item Sizing", "Bar", "Sizing algorithm for bar items."));

            if (!ignore)
            {
                if (checkButton)
                    actions.Add(new DesignerActionPropertyItem("CheckButtonStyleBar", "Item Style", "Bar", "Style of items on the bar."));
                else
                {
                    actions.Add(new DesignerActionPropertyItem("TabBorderStyle", "Tab Border Style", "Bar", "Border style for tabs on the bar."));
                    actions.Add(new DesignerActionPropertyItem("TabStyle", "Tab Style", "Bar", "Style for drawing tab colors and fonts."));
                }
            }
        }

        private void AddStackActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Stack"));
            actions.Add(new DesignerActionPropertyItem("StackOrientation", "Stack Orientation", "Stack", "Orientation of the stack."));
            actions.Add(new DesignerActionPropertyItem("StackAlignment", "Stack Alignment", "Stack", "Alignment of items in the stack."));
            actions.Add(new DesignerActionPropertyItem("ItemOrientationStack", "Item Orientation", "Stack", "Orientation of the stack items."));
            actions.Add(new DesignerActionPropertyItem("CheckButtonStyleStack", "Item Style", "Stack", "Style of items in the stack."));
        }

        private void AddButtonsActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Button"));
            actions.Add(new DesignerActionPropertyItem("ButtonDisplayLogic", "Display Logic", "Button", "Logic used to determine button display."));
            actions.Add(new DesignerActionPropertyItem("CloseButtonDisplay", "Close Display", "Button", "How should the close button be displayed."));
            actions.Add(new DesignerActionPropertyItem("CloseButtonAction", "Close Action", "Button", "Action to take when close button pressed."));
        }

        private void AddGroupActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Group"));
            actions.Add(new DesignerActionPropertyItem("GroupBackStyle", "Back Style", "Group", "Group background style"));
            actions.Add(new DesignerActionPropertyItem("GroupBorderStyle", "Border Style", "Group", "Group border style"));
        }

        private void AddHeadersActions(DesignerActionItemCollection actions,
                                       bool includeHeaderPrimary,
                                       bool includeHeaderSecondary,
                                       bool includeHeaderBar)
        {
            // Get the current visible state of the headers
            bool headerBarVisible = _navigator.Header.HeaderVisibleBar;
            bool headerPrimaryVisible = _navigator.Header.HeaderVisiblePrimary;
            bool headerSecondaryVisible = _navigator.Header.HeaderVisibleSecondary;

            // Decide on the initial text values
            _headerBarText = (headerBarVisible ? "Hide bar header" : "Show bar header");
            _headerPrimaryText = (headerPrimaryVisible ? "Hide primary header" : "Show primary header");
            _headerSecondaryText = (headerSecondaryVisible ? "Hide secondary header" : "Show secondary header");

            // Create the two verbs for toggling the header visibility
            _headerBarVisible = new DesignerVerb(_headerBarText, new EventHandler(OnVisibleClick));
            _headerPrimaryVisible = new DesignerVerb(_headerPrimaryText, new EventHandler(OnVisibleClick));
            _headerSecondaryVisible = new DesignerVerb(_headerSecondaryText, new EventHandler(OnVisibleClick));

            actions.Add(new DesignerActionHeaderItem("Header"));

            if (includeHeaderBar)
            {
                actions.Add(new DesignerActionPropertyItem("BarHeaderStyle", "Bar Style", "Header", "Bar header style."));
                actions.Add(new DesignerActionPropertyItem("BarHeaderPosition", "Bar Position", "Header", "Bar header position."));
                actions.Add(new KryptonDesignerActionItem(_headerBarVisible, "Header"));
            }

            if (includeHeaderPrimary)
            {
                actions.Add(new DesignerActionPropertyItem("PrimaryHeaderStyle", "Primary Style", "Header", "Primary header style."));
                actions.Add(new DesignerActionPropertyItem("PrimaryHeaderPosition", "Primary Position", "Header", "Primary header position."));
                actions.Add(new KryptonDesignerActionItem(_headerPrimaryVisible, "Header"));
            }

            if (includeHeaderSecondary)
            {
                actions.Add(new DesignerActionPropertyItem("SecondaryHeaderStyle", "Secondary Style", "Header", "Secondary header style."));
                actions.Add(new DesignerActionPropertyItem("SecondaryHeaderPosition", "Secondary Position", "Header", "Secondary header position."));
                actions.Add(new KryptonDesignerActionItem(_headerSecondaryVisible, "Header"));
            }
        }

        private void AddPanelActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Panel"));
            actions.Add(new DesignerActionPropertyItem("PanelBackStyle", "Back Style", "Panel", "Panel background style"));
        }

        private void AddOutlookActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Outlook"));
            actions.Add(new DesignerActionPropertyItem("OutlookOrientation", "Outlook Orientation", "Outlook", "Orientation of the view."));
            actions.Add(new DesignerActionPropertyItem("ItemOrientationOutlook", "Item Orientation", "Outlook", "Orientation of the outlook items."));
            actions.Add(new DesignerActionPropertyItem("CheckButtonStyleOutlook", "Item Style", "Outlook", "Style of items in the outlook stack."));
            actions.Add(new DesignerActionPropertyItem("SecondaryHeaderOutlookVisible", "Secondary Header Visible", "Outlook", "How to decide if the secondary header is visible."));
        }

        private void AddConstantActions(DesignerActionItemCollection actions)
        {
            actions.Add(new DesignerActionHeaderItem("Actions"));
            actions.Add(new DesignerActionMethodItem(this, "AddPage", "Add Page", "Actions"));

            // Can only remove a page if one if selected
            if (_navigator.SelectedPage != null)
                actions.Add(new DesignerActionMethodItem(this, "RemovePage", "Remove Page", "Actions"));

            // Are there any pages to be removed
            if (_navigator.Pages.Count > 0)
                actions.Add(new DesignerActionMethodItem(this, "ClearPages", "Clear Pages", "Actions"));
        }

        private void OnVisibleClick(object sender, EventArgs e)
        {
            // Cast to the correct type
            DesignerVerb verb = sender as DesignerVerb;

            // Find out which verb has been used
            bool headerBar = (verb == _headerBarVisible);
            bool headerPrimary = (verb == _headerPrimaryVisible);
            bool headerSecondary = (verb == _headerSecondaryVisible);

            bool newVisible = false;

            // Get the new visible values
            if (headerBar)
            {
                newVisible = _navigator.Header.HeaderVisibleBar;
                _headerBarText = (newVisible ? "Hide bar header" : "Show bar header");
                _navigator.Header.HeaderVisibleBar = !newVisible;
            }
            else if (headerPrimary)
            {
                newVisible = _navigator.Header.HeaderVisiblePrimary;
                _headerPrimaryText = (newVisible ? "Hide primary header" : "Show primary header");
                _navigator.Header.HeaderVisiblePrimary = !newVisible;
            }
            else if (headerSecondary)
            {
                newVisible = _navigator.Header.HeaderVisibleSecondary;
                _headerSecondaryText = (newVisible ? "Hide secondary header" : "Show secondary header");
                _navigator.Header.HeaderVisibleSecondary = !newVisible;
            }

            // Refersh view to reflect the change
            _serviceDesignerAction.Refresh(_navigator);
        }
        #endregion
    }
}
