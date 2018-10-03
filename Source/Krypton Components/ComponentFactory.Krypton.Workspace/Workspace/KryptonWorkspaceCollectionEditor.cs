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
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
    internal class KryptonWorkspaceCollectionEditor : CollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonWorkspaceCollection.
        /// </summary>
        protected partial class KryptonWorkspaceCollectionForm : CollectionEditor.CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declaractions!
            /// </summary>
            protected class DictItemBase : Dictionary<Component, Component> { };

            /// <summary>
            /// Act as proxy for krypton page item to control the exposed properties to the property grid.
            /// </summary>
            protected class PageProxy
            {
                #region Instance Fields
                private KryptonPage _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the PageProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public PageProxy(KryptonPage item)
                {
                    _item = item;
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets access to the common page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorRedirect StateCommon
                {
                    get { return _item.StateCommon; }
                }

                /// <summary>
                /// Gets access to the disabled page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigator StateDisabled
                {
                    get { return _item.StateDisabled; }
                }

                /// <summary>
                /// Gets access to the normal page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigator StateNormal
                {
                    get { return _item.StateNormal; }
                }

                /// <summary>
                /// Gets access to the tracking page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherEx StateTracking
                {
                    get { return _item.StateTracking; }
                }

                /// <summary>
                /// Gets access to the pressed page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherEx StatePressed
                {
                    get { return _item.StatePressed; }
                }

                /// <summary>
                /// Gets access to the selected page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOther StateSelected
                {
                    get { return _item.StateSelected; }
                }

                /// <summary>
                /// Gets access to the focus page appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherRedirect OverrideFocus
                {
                    get { return _item.OverrideFocus; }
                }

                /// <summary>
                /// Gets and sets the page text.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue("Page")]
                public string Text
                {
                    get { return _item.Text; }
                    set { _item.Text = value; }
                }

                /// <summary>
                /// Gets and sets the title text for the page.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue("Page Title")]
                public string TextTitle
                {
                    get { return _item.TextTitle; }
                    set { _item.TextTitle = value; }
                }

                /// <summary>
                /// Gets and sets the description text for the page.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue("Page Description")]
                public string TextDescription
                {
                    get { return _item.TextDescription; }
                    set { _item.TextDescription = value; }
                }

                /// <summary>
                /// Gets and sets the small image for the page.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(null)]
                public Image ImageSmall
                {
                    get { return _item.ImageSmall; }
                    set { _item.ImageSmall = value; }
                }

                /// <summary>
                /// Gets and sets the medium image for the page.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(null)]
                public Image ImageMedium
                {
                    get { return _item.ImageMedium; }
                    set { _item.ImageMedium = value; }
                }

                /// <summary>
                /// Gets and sets the large image for the page.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(null)]
                public Image ImageLarge
                {
                    get { return _item.ImageLarge; }
                    set { _item.ImageLarge = value; }
                }

                /// <summary>
                /// Gets and sets the page tooltip image.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(null)]
                public Image ToolTipImage
                {
                    get { return _item.ToolTipImage; }
                    set { _item.ToolTipImage = value; }
                }

                /// <summary>
                /// Gets and sets the tooltip image transparent color.
                /// </summary>
                [Category("Appearance")]
                [Description("Page tooltip image transparent color.")]
                public Color ToolTipImageTransparentColor
                {
                    get { return _item.ToolTipImageTransparentColor; }
                    set { _item.ToolTipImageTransparentColor = value; }
                }

                /// <summary>
                /// Gets and sets the page tooltip title text.
                /// </summary>
                [Category("Appearance")]
                [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
                [DefaultValue("")]
                public string ToolTipTitle
                {
                    get { return _item.ToolTipTitle; }
                    set { _item.ToolTipTitle = value; }
                }

                /// <summary>
                /// Gets and sets the page tooltip body text.
                /// </summary>
                [Category("Appearance")]
                [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
                [DefaultValue("")]
                public string ToolTipBody
                {
                    get { return _item.ToolTipBody; }
                    set { _item.ToolTipBody = value; }
                }

                /// <summary>
                /// Gets and sets the tooltip label style.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(typeof(LabelStyle), "ToolTip")]
                public LabelStyle ToolTipStyle
                {
                    get { return _item.ToolTipStyle; }
                    set { _item.ToolTipStyle = value; }
                }

                /// <summary>
                /// Gets and sets the unique name of the page.
                /// </summary>
                [Category("Appearance")]
                public string UniqueName
                {
                    get { return _item.UniqueName; }
                    set { _item.UniqueName = value; }
                }

                /// <summary>
                /// Gets and sets if the page should be shown.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get { return _item.LastVisibleSet; }
                    set { _item.LastVisibleSet = value; }
                }

                /// <summary>
                /// Gets and sets if the page should be enabled.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public bool Enabled
                {
                    get { return _item.Enabled; }
                    set { _item.Enabled = value; }
                }

                /// <summary>
                /// Gets and sets the KryptonContextMenu to show when right clicked.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(null)]
                public KryptonContextMenu KryptonContextMenu
                {
                    get { return _item.KryptonContextMenu; }
                    set { _item.KryptonContextMenu = value; }
                }

                /// <summary>
                /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
                /// </summary>
                [Category("Layout")]
                [DefaultValue(typeof(Size), "50,50")]
                public Size MinimumSize
                {
                    get { return _item.MinimumSize; }
                    set { _item.MinimumSize = value; }
                }

                /// <summary>
                /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
                /// </summary>
                [Category("Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MaximumSize
                {
                    get { return _item.MaximumSize; }
                    set { _item.MaximumSize = value; }
                }

                /// <summary>
                /// Gets or sets the page padding.
                /// </summary>
                [Category("Layout")]
                [DefaultValue(typeof(Padding), "0,0,0,0")]
                public Padding Padding
                {
                    get { return _item.Padding; }
                    set { _item.Padding = value; }
                }

                /// <summary>
                /// Gets and sets user-defined data associated with the object.
                /// </summary>
                [Category("Data")]
                [TypeConverter(typeof(StringConverter))]
                [DefaultValue(null)]
                public object Tag
                {
                    get { return _item.Tag; }
                    set { _item.Tag = value; }
                }
                #endregion
            }

            /// <summary>
            /// Act as proxy for workspace cell item to control the exposed properties to the property grid.
            /// </summary>
            protected class CellProxy
            {
                #region Instance Fields
                private KryptonWorkspaceCell _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the CellProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public CellProxy(KryptonWorkspaceCell item)
                {
                    _item = item;
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
                /// </summary>
                [Category("Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MinimumSize
                {
                    get { return _item.MinimumSize; }
                    set { _item.MinimumSize = value; }
                }

                /// <summary>
                /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
                /// </summary>
                [Category("Layout")]
                [DefaultValue(typeof(Size), "0,0")]
                public Size MaximumSize
                {
                    get { return _item.MaximumSize; }
                    set { _item.MaximumSize = value; }
                }

                /// <summary>
                /// Gets and sets if the user can a separator to resize this workspace cell.
                /// </summary>
                [Category("Visuals")]
                [DefaultValue(true)]
                public bool AllowResizing
                {
                    get { return _item.AllowResizing; }
                    set { _item.AllowResizing = value; }
                }

                /// <summary>
                /// Star notation the describes the sizing of the workspace item.
                /// </summary>
                [Category("Workspace")]
                [DefaultValue("50*,50*")]
                public string StarSize
                {
                    get { return _item.StarSize; }
                    set { _item.StarSize = value; }
                }

                /// <summary>
                /// Should the item be disposed when it is removed from the workspace.
                /// </summary>
                [Category("Workspace")]
                [DefaultValue(true)]
                public bool DisposeOnRemove
                {
                    get { return _item.DisposeOnRemove; }
                    set { _item.DisposeOnRemove = value; }
                }

                /// <summary>
                /// Gets access to the bar specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorBar Bar
                {
                    get { return _item.Bar; }
                }

                /// <summary>
                /// Gets access to the stack specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorStack Stack
                {
                    get { return _item.Stack; }
                }

                /// <summary>
                /// Gets access to the outlook mode specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorOutlook Outlook
                {
                    get { return _item.Outlook; }
                }

                /// <summary>
                /// Gets access to button specifications and fixed button logic.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorButton Button
                {
                    get { return _item.Button; }
                }

                /// <summary>
                /// Gets access to the group specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorGroup Group
                {
                    get { return _item.Group; }
                }

                /// <summary>
                /// Gets access to the header specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorHeader Header
                {
                    get { return _item.Header; }
                }

                /// <summary>
                /// Gets access to the panels specific settings.
                /// </summary>
                [Category("Visuals (Modes)")]
                public NavigatorPanel Panel
                {
                    get { return _item.Panel; }
                }

                /// <summary>
                /// Gets access to the popup page specific settings.
                /// </summary>
                [Category("Visuals")]
                public NavigatorPopupPages PopupPages
                {
                    get { return _item.PopupPages; }
                }

                /// <summary>
                /// Gets access to the tooltip specific settings.
                /// </summary>
                [Category("Visuals")]
                public NavigatorToolTips ToolTips
                {
                    get { return _item.ToolTips; }
                }

                /// <summary>
                /// Gets access to the common navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorRedirect StateCommon
                {
                    get { return _item.StateCommon; }
                }

                /// <summary>
                /// Gets access to the disabled navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigator StateDisabled
                {
                    get { return _item.StateDisabled; }
                }

                /// <summary>
                /// Gets access to the normal navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigator StateNormal
                {
                    get { return _item.StateNormal; }
                }

                /// <summary>
                /// Gets access to the tracking navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherEx StateTracking
                {
                    get { return _item.StateTracking; }
                }

                /// <summary>
                /// Gets access to the pressed navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherEx StatePressed
                {
                    get { return _item.StatePressed; }
                }

                /// <summary>
                /// Gets access to the selected navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOther StateSelected
                {
                    get { return _item.StateSelected; }
                }

                /// <summary>
                /// Gets access to the focus navigator appearance entries.
                /// </summary>
                [Category("Visuals")]
                public PaletteNavigatorOtherRedirect OverrideFocus
                {
                    get { return _item.OverrideFocus; }
                }

                /// <summary>
                /// Gets and sets the display mode.
                /// </summary>
                [Category("Visuals")]
                [DefaultValue(typeof(NavigatorMode), "BarTabGroup")]
                public NavigatorMode NavigatorMode
                {
                    get { return _item.NavigatorMode; }
                    set { _item.NavigatorMode = value; }
                }

                /// <summary>
                /// Gets and sets the page background style.
                /// </summary>
                [Category("Visuals")]
                [DefaultValue(typeof(PaletteBackStyle), "ControlClient")]
                public PaletteBackStyle PageBackStyle
                {
                    get { return _item.PageBackStyle; }
                    set { _item.PageBackStyle = value; }
                }

                /// <summary>
                /// Gets or sets the default setting for allowing the page dragging from of the navigator.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public bool AllowPageDrag
                {
                    get { return _item.AllowPageDrag; }
                    set { _item.AllowPageDrag = value; }
                }

                /// <summary>
                /// Gets or sets if the tab headers are allowed to take the focus.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(false)]
                public bool AllowTabFocus
                {
                    get { return _item.AllowTabFocus; }
                    set { _item.AllowTabFocus = value; }
                }

                /// <summary>
                /// Gets and sets if the cell should be shown.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get { return _item.LastVisibleSet; }
                    set { _item.LastVisibleSet = value; }
                }

                /// <summary>
                /// Gets and sets if the cell should be enabled.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public bool Enabled
                {
                    get { return _item.Enabled; }
                    set { _item.Enabled = value; }
                }

                /// <summary>
                /// Gets and sets if the cell selected page.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(true)]
                public KryptonPage SelectedPage
                {
                    get { return _item.SelectedPage; }
                    
                    set 
                    { 
                        // Check that the target cell allows selected tabs
                        if (_item.AllowTabSelect)
                            _item.SelectedPage = value; 
                    }
                }

                /// <summary>
                /// Gets and sets the KryptonContextMenu to show when right clicked.
                /// </summary>
                [Category("Behavior")]
                [DefaultValue(null)]
                public KryptonContextMenu KryptonContextMenu
                {
                    get { return _item.KryptonContextMenu; }
                    set { _item.KryptonContextMenu = value; }
                }

                /// <summary>
                /// Gets or sets a value indicating whether mnemonics select pages and button specs.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(true)]
                public bool UseMnemonic
                {
                    get { return _item.UseMnemonic; }
                    set { _item.UseMnemonic = value; }
                }

                /// <summary>
                /// Gets and sets user-defined data associated with the object.
                /// </summary>
                [Category("Data")]
                [TypeConverter(typeof(StringConverter))]
                [DefaultValue(null)]
                public object Tag
                {
                    get { return _item.Tag; }
                    set { _item.Tag = value; }
                }
                #endregion
            }

            /// <summary>
            /// Act as proxy for workspace sequence item to control the exposed properties to the property grid.
            /// </summary>
            protected class SequenceProxy
            {
                #region Instance Fields
                private KryptonWorkspaceSequence _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the SequenceProxy class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public SequenceProxy(KryptonWorkspaceSequence item)
                {
                    _item = item;
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets or sets a value indicating whether the sequence is displayed.
                /// </summary>
                [Category("Visuals")]
                [DefaultValue(true)]
                public bool Visible
                {
                    get { return _item.Visible; }
                    set { _item.Visible = value; }
                }

                /// <summary>
                /// Gets and sets the orientation for laying out the child entries.
                /// </summary>
                [Category("Workspace")]
                [DefaultValue(typeof(Orientation), "Horizontal")]
                public Orientation Orientation
                {
                    get { return _item.Orientation; }
                    set { _item.Orientation = value; }
                }

                /// <summary>
                /// Star notation the describes the sizing of the workspace item.
                /// </summary>
                [Category("Workspace")]
                [DefaultValue("50*,50*")]
                public string StarSize
                {
                    get { return _item.StarSize; }
                    set { _item.StarSize = value; }
                }
                #endregion
            }

            /// <summary>
            /// Tree node that is attached to a context menu item.
            /// </summary>
            protected class MenuTreeNode : TreeNode
            {
                #region Static Fields
                private static int _id = 1;
                #endregion

                #region Instance Fields
                private int _instanceId;
                private KryptonPage _page;
                private KryptonWorkspaceCell _cell;
                private KryptonWorkspaceSequence _sequence;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the MenuTreeNode class.
                /// </summary>
                /// <param name="item">Item to represent.</param>
                public MenuTreeNode(Component item)
                {
                    _instanceId = _id++;

                    _page = item as KryptonPage;
                    if (_page != null)
                    {
                        _page.TextChanged += new EventHandler(OnPageTextChanged);
                        Text = "Page (" + _page.Text.ToString() + ")";
                    }

                    _cell = item as KryptonWorkspaceCell;
                    if (_cell != null)
                    {
                        _cell.PropertyChanged += new PropertyChangedEventHandler(OnCellPropertyChanged);
                        Text = "Cell (" + _cell.StarSize.ToString() + ")";
                    }

                    _sequence = item as KryptonWorkspaceSequence;
                    if (_sequence != null)
                    {
                        _sequence.PropertyChanged += new PropertyChangedEventHandler(OnSequencePropertyChanged);
                        Text = _sequence.Orientation + " (" + _sequence.StarSize.ToString() + ")";
                    }
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public Component Item
                {
                    get { return (PageItem != null ? (Component)PageItem : (CellItem != null ? (Component)CellItem : (Component)SequenceItem)); }
                }

                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public KryptonPage PageItem
                {
                    get { return _page; }
                }

                /// <summary>
                /// Gets access to the associated workspace cell item.
                /// </summary>
                public KryptonWorkspaceCell CellItem
                {
                    get { return _cell; }
                }

                /// <summary>
                /// Gets access to the associated workspace sequence item.
                /// </summary>
                public KryptonWorkspaceSequence SequenceItem
                {
                    get { return _sequence; }
                }

                /// <summary>
                /// Gets the instance identifier.
                /// </summary>
                public int InstanceId
                {
                    get { return _instanceId; }
                }
                #endregion

                #region Implementation
                private void OnPageTextChanged(object sender, EventArgs e)
                {
                    Text = "Page (" + _page.Text.ToString() + ")";
                }

                private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    Text = "Cell (" + _cell.StarSize.ToString() + ")";
                }

                private void OnSequencePropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    Text = _sequence.Orientation + " (" + _sequence.StarSize.ToString() + ")";
                }
                #endregion
            }

            /// <summary>
            /// Site that allows the property grid to discover Visual Studio services.
            /// </summary>
            protected class PropertyGridSite : ISite, IServiceProvider
            {
                #region Instance Fields
                private IComponent _component;
                private IServiceProvider _serviceProvider;
                private bool _inGetService;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the PropertyGridSite.
                /// </summary>
                /// <param name="servicePovider">Reference to service container.</param>
                /// <param name="component">Reference to component.</param>
                public PropertyGridSite(IServiceProvider servicePovider,
                                        IComponent component)
                {
                    _serviceProvider = servicePovider;
                    _component = component;
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets the service object of the specified type. 
                /// </summary>
                /// <param name="t">An object that specifies the type of service object to get. </param>
                /// <returns>A service object of type serviceType; or null reference if there is no service object of type serviceType.</returns>
                public object GetService(Type t)
                {
                    if (!_inGetService && (_serviceProvider != null))
                    {
                        try
                        {
                            _inGetService = true;
                            return _serviceProvider.GetService(t);
                        }
                        finally
                        {
                            _inGetService = false;
                        }
                    }

                    return null;
                }

                /// <summary>
                /// Gets the component associated with the ISite when implemented by a class.
                /// </summary>
                public IComponent Component
                {
                    get { return _component; }
                }

                /// <summary>
                /// Gets the IContainer associated with the ISite when implemented by a class.
                /// </summary>
                public IContainer Container
                {
                    get { return null; }
                }

                /// <summary>
                /// Determines whether the component is in design mode when implemented by a class.
                /// </summary>
                public bool DesignMode
                {
                    get { return false; }
                }

                /// <summary>
                /// Gets or sets the name of the component associated with the ISite when implemented by a class.
                /// </summary>
                public string Name
                {
                    get { return null; }
                    set { }
                }
                #endregion
            }
            #endregion

            #region Instance Fields
            private KryptonWorkspaceCollectionEditor _editor;
            private DictItemBase _beforeItems;
            private TreeView treeView;
            private PropertyGrid propertyGrid;
            private Button buttonMoveUp;
            private Button buttonMoveDown;
            private Button buttonAddPage;
            private Button buttonAddCell;
            private Button buttonAddSequence;
            private Button buttonOK;
            private Button buttonDelete;
            private Label labelItemProperties;
            private Label labelWorkspaceCollection;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonWorkspaceCollectionForm class.
            /// </summary>
            public KryptonWorkspaceCollectionForm(KryptonWorkspaceCollectionEditor editor)
                : base(editor)
            {
                _editor = editor;

                this.buttonOK = new System.Windows.Forms.Button();
                this.treeView = new System.Windows.Forms.TreeView();
                this.buttonMoveUp = new System.Windows.Forms.Button();
                this.buttonMoveDown = new System.Windows.Forms.Button();
                this.buttonAddPage = new System.Windows.Forms.Button();
                this.buttonAddCell = new System.Windows.Forms.Button();
                this.buttonAddSequence = new System.Windows.Forms.Button();
                this.buttonDelete = new System.Windows.Forms.Button();
                this.propertyGrid = new System.Windows.Forms.PropertyGrid();
                this.labelItemProperties = new System.Windows.Forms.Label();
                this.labelWorkspaceCollection = new System.Windows.Forms.Label();
                this.SuspendLayout();
                // 
                // buttonOK
                // 
                this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.buttonOK.Location = new System.Drawing.Point(547, 382);
                this.buttonOK.Name = "buttonOK";
                this.buttonOK.Size = new System.Drawing.Size(75, 23);
                this.buttonOK.TabIndex = 8;
                this.buttonOK.Text = "OK";
                this.buttonOK.UseVisualStyleBackColor = true;
                this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
                // 
                // treeView
                // 
                this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.treeView.Location = new System.Drawing.Point(12, 32);
                this.treeView.Name = "treeView";
                this.treeView.Size = new System.Drawing.Size(251, 339);
                this.treeView.TabIndex = 1;
                this.treeView.HideSelection = false;
                this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
                // 
                // buttonMoveUp
                // 
                this.buttonMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonMoveUp.Image = Properties.Resources.arrow_up_blue;
                this.buttonMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveUp.Location = new System.Drawing.Point(272, 32);
                this.buttonMoveUp.Name = "buttonMoveUp";
                this.buttonMoveUp.Size = new System.Drawing.Size(95, 28);
                this.buttonMoveUp.TabIndex = 2;
                this.buttonMoveUp.Text = " Move Up";
                this.buttonMoveUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonMoveUp.UseVisualStyleBackColor = true;
                this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
                // 
                // buttonMoveDown
                // 
                this.buttonMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonMoveDown.Image = Properties.Resources.arrow_down_blue;
                this.buttonMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveDown.Location = new System.Drawing.Point(272, 66);
                this.buttonMoveDown.Name = "buttonMoveDown";
                this.buttonMoveDown.Size = new System.Drawing.Size(95, 28);
                this.buttonMoveDown.TabIndex = 3;
                this.buttonMoveDown.Text = " Move Down";
                this.buttonMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonMoveDown.UseVisualStyleBackColor = true;
                this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
                // 
                // buttonDelete
                // 
                this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonDelete.Image = Properties.Resources.delete2;
                this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonDelete.Location = new System.Drawing.Point(272, 234);
                this.buttonDelete.Name = "buttonDelete";
                this.buttonDelete.Size = new System.Drawing.Size(95, 28);
                this.buttonDelete.TabIndex = 5;
                this.buttonDelete.Text = " Delete Item";
                this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonDelete.UseVisualStyleBackColor = true;
                this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
                // 
                // propertyGrid
                // 
                this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.propertyGrid.HelpVisible = false;
                this.propertyGrid.Location = new System.Drawing.Point(376, 32);
                this.propertyGrid.Name = "propertyGrid";
                this.propertyGrid.Size = new System.Drawing.Size(246, 339);
                this.propertyGrid.TabIndex = 7;
                this.propertyGrid.ToolbarVisible = false;
                // 
                // labelItemProperties
                // 
                this.labelItemProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.labelItemProperties.AutoSize = true;
                this.labelItemProperties.Location = new System.Drawing.Point(370, 13);
                this.labelItemProperties.Name = "labelItemProperties";
                this.labelItemProperties.Size = new System.Drawing.Size(81, 13);
                this.labelItemProperties.TabIndex = 6;
                this.labelItemProperties.Text = "Item Properties";
                // 
                // labelWorkspaceCollection
                // 
                this.labelWorkspaceCollection.AutoSize = true;
                this.labelWorkspaceCollection.Location = new System.Drawing.Point(12, 13);
                this.labelWorkspaceCollection.Name = "labelWorkspaceCollection";
                this.labelWorkspaceCollection.Size = new System.Drawing.Size(142, 13);
                this.labelWorkspaceCollection.TabIndex = 0;
                this.labelWorkspaceCollection.Text = "Workspace Collection";
                // 
                // buttonAddPage
                // 
                this.buttonAddPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddPage.Image = Properties.Resources.add;
                this.buttonAddPage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddPage.Location = new System.Drawing.Point(272, 114);
                this.buttonAddPage.Name = "buttonAddPage";
                this.buttonAddPage.Size = new System.Drawing.Size(95, 28);
                this.buttonAddPage.TabIndex = 4;
                this.buttonAddPage.Text = " Page";
                this.buttonAddPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddPage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddPage.UseVisualStyleBackColor = true;
                this.buttonAddPage.Click += new System.EventHandler(this.buttonAddPage_Click);
                // 
                // buttonAddCell
                // 
                this.buttonAddCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddCell.Image = Properties.Resources.add;
                this.buttonAddCell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddCell.Location = new System.Drawing.Point(272, 148);
                this.buttonAddCell.Name = "buttonAddCell";
                this.buttonAddCell.Size = new System.Drawing.Size(95, 28);
                this.buttonAddCell.TabIndex = 9;
                this.buttonAddCell.Text = " Cell";
                this.buttonAddCell.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddCell.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddCell.UseVisualStyleBackColor = true;
                this.buttonAddCell.Click += new System.EventHandler(this.buttonAddCell_Click);
                // 
                // buttonAddSequence
                // 
                this.buttonAddSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddSequence.Image = Properties.Resources.add;
                this.buttonAddSequence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddSequence.Location = new System.Drawing.Point(272, 182);
                this.buttonAddSequence.Name = "buttonAddSequence";
                this.buttonAddSequence.Size = new System.Drawing.Size(95, 28);
                this.buttonAddSequence.TabIndex = 9;
                this.buttonAddSequence.Text = " Sequence";
                this.buttonAddSequence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddSequence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddSequence.UseVisualStyleBackColor = true;
                this.buttonAddSequence.Click += new System.EventHandler(this.buttonAddSequence_Click);

                this.AcceptButton = this.buttonOK;
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(634, 414);
                this.ControlBox = false;
                this.Controls.Add(this.treeView);
                this.Controls.Add(this.buttonMoveUp);
                this.Controls.Add(this.buttonMoveDown);
                this.Controls.Add(this.buttonAddPage);
                this.Controls.Add(this.buttonAddCell);
                this.Controls.Add(this.buttonAddSequence);
                this.Controls.Add(this.propertyGrid);
                this.Controls.Add(this.buttonDelete);
                this.Controls.Add(this.buttonOK);
                this.Controls.Add(this.labelWorkspaceCollection);
                this.Controls.Add(this.labelItemProperties);
                this.VisibleChanged += new EventHandler(OnVisibleChanged);
                this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.MinimumSize = new System.Drawing.Size(501, 344);
                this.Name = "KryptonWorkspaceCollectionForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Workspace Collection Editor";
                this.ResumeLayout(false);
                this.PerformLayout();
            }
            #endregion

            #region Protected Overrides
            /// <summary>
            /// Provides an opportunity to perform processing when a collection value has changed.
            /// </summary>
            protected override void OnEditValueChanged()
            {
                if (EditValue != null)
                {                    
                    // Need to link the property browser to a site otherwise Image properties cannot be
                    // edited because it cannot navigate to the owning project for its resources
                    propertyGrid.Site = new PropertyGridSite(Context, propertyGrid);

                    // Cache a lookup of all items before changes are made
                    _beforeItems = CreateItemsDictionary(Items);

                    // Add all the top level clones
                    treeView.Nodes.Clear();
                    foreach (Component item in Items)
                        AddMenuTreeNode(item, null);

                    // Expand to show all entries
                    treeView.ExpandAll();

                    // Select the first node
                    if (treeView.Nodes.Count > 0)
                        treeView.SelectedNode = treeView.Nodes[0];

                    UpdateButtons();
                    UpdatePropertyGrid();
                }
            }
            #endregion

            #region Implementation
            private void OnVisibleChanged(object sender, EventArgs e)
            {
                if (Visible)
                    _editor.Workspace.SuspendWorkspaceLayout();
                else
                    _editor.Workspace.ResumeWorkspaceLayout();
            }

            private void buttonOK_Click(object sender, EventArgs e)
            {
                // Create an array with all the root items
                object[] rootItems = new object[treeView.Nodes.Count];
                for (int i = 0; i < rootItems.Length; i++)
                    rootItems[i] = ((MenuTreeNode)treeView.Nodes[i]).Item;

                // Cache a lookup of all items after changes are made
                DictItemBase afterItems = CreateItemsDictionary(rootItems);

                // Update collection with new set of items
                Items = rootItems;

                // Inform designer of changes in component items
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();

                // Clear down contents of tree as this form can be reused
                treeView.Nodes.Clear();
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                if (node != null)
                {
                    // Find out the type of item represented by the selected node
                    bool isNodePage, isNodeCell, isNodeSequence;
                    NodeToType(node, out isNodePage, out isNodeCell, out isNodeSequence);

                    // Find the previous node compatible as target for the selected node
                    MenuTreeNode previousNode = (MenuTreeNode)PreviousNode(node);
                    if (previousNode != null)
                    {
                        // Find out the type of previous node
                        bool isPreviousPage, isPreviousCell, isPreviousSequence;
                        NodeToType(previousNode, out isPreviousPage, out isPreviousCell, out isPreviousSequence);

                        // If moving a page...
                        if (isNodePage)
                        {
                            // Remove page from parent cell
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            parentNode.CellItem.Pages.Remove(node.PageItem);
                            parentNode.Nodes.Remove(node);

                            // If the previous node is also a page
                            if (isPreviousPage)
                            {
                                // Add page to the parent cell of target page
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                int pageIndex = previousParent.CellItem.Pages.IndexOf(previousNode.PageItem);

                                // If the current and previous nodes are inside different cells
                                if (previousParent != parentNode)
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.CellItem.Pages.Count - 1))
                                        pageIndex++;
                                }

                                previousParent.CellItem.Pages.Insert(pageIndex, node.PageItem);
                                previousParent.Nodes.Insert(pageIndex, node);
                            }
                            else if (isPreviousCell)
                            {
                                // Add page as last item of target cell
                                parentNode = (MenuTreeNode)previousNode;
                                parentNode.CellItem.Pages.Insert(parentNode.CellItem.Pages.Count, node.PageItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                        else if (isNodeCell)
                        {
                            // Is the current node contained inside the next node
                            bool contained = ContainsNode(previousNode, node);

                            // Remove cell from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            if (parentNode != null)
                                parentNode.SequenceItem.Children.Remove(node.CellItem);
                            parentCollection.Remove(node);

                            // If the previous node is also a cell
                            if (isPreviousCell || contained)
                            {
                                // Add cell to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                parentCollection = (previousNode.Parent == null ? treeView.Nodes : previousNode.Parent.Nodes);
                                int pageIndex = parentCollection.IndexOf(previousNode);

                                // If the current and previous nodes are inside different cells
                                if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.SequenceItem.Children.Count - 1))
                                        pageIndex++;
                                }

                                if (previousParent != null)
                                    previousParent.SequenceItem.Children.Insert(pageIndex, node.CellItem);
                                parentCollection.Insert(pageIndex, node);
                            }
                            else if (isPreviousSequence)
                            {
                                // Add cell as last item of target sequence
                                parentNode = (MenuTreeNode)previousNode;
                                parentNode.SequenceItem.Children.Insert(parentNode.SequenceItem.Children.Count, node.CellItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                        else if (isNodeSequence)
                        {
                            // Is the current node contained inside the next node
                            bool contained = ContainsNode(previousNode, node);

                            // Remove sequence from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            if (parentNode != null)
                                parentNode.SequenceItem.Children.Remove(node.SequenceItem);
                            parentCollection.Remove(node);

                            // If the previous node is also a sequence
                            if (isPreviousCell || contained)
                            {
                                // Add sequence to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                                parentCollection = (previousNode.Parent == null ? treeView.Nodes : previousNode.Parent.Nodes);
                                int pageIndex = parentCollection.IndexOf(previousNode);

                                // If the current and previous nodes are inside different cells
                                if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                                {
                                    // If the page is the last one in the collection then we need to insert afterwards
                                    if (pageIndex == (previousParent.SequenceItem.Children.Count - 1))
                                        pageIndex++;
                                }

                                if (previousParent != null)
                                    previousParent.SequenceItem.Children.Insert(pageIndex, node.SequenceItem);
                                parentCollection.Insert(pageIndex, node);
                            }
                            else if (isPreviousSequence)
                            {
                                // Add sequence as last item of target sequence
                                parentNode = (MenuTreeNode)previousNode;
                                parentNode.SequenceItem.Children.Insert(parentNode.SequenceItem.Children.Count, node.SequenceItem);
                                parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                            }
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                if (node != null)
                {
                    // Find out the type of item represented by the selected node
                    bool isNodePage, isNodeCell, isNodeSequence;
                    NodeToType(node, out isNodePage, out isNodeCell, out isNodeSequence);

                    // Find the next node compatible as target for the selected node
                    MenuTreeNode nextNode = (MenuTreeNode)NextNode(node);
                    if (nextNode != null)
                    {
                        // Find out the type of next node
                        bool isNextPage, isNextCell, isNextSequence;
                        NodeToType(nextNode, out isNextPage, out isNextCell, out isNextSequence);

                        // If moving a page...
                        if (isNodePage)
                        {
                            // Remove page from parent cell
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            parentNode.CellItem.Pages.Remove(node.PageItem);
                            parentNode.Nodes.Remove(node);

                            // If the next node is also a page
                            if (isNextPage)
                            {
                                // Add page to the parent cell of target page
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                int pageIndex = previousParent.CellItem.Pages.IndexOf(nextNode.PageItem);
                                previousParent.CellItem.Pages.Insert(pageIndex + 1, node.PageItem);
                                previousParent.Nodes.Insert(pageIndex + 1, node);
                            }
                            else if (isNextCell)
                            {
                                // Add page as first item of target cell
                                parentNode = (MenuTreeNode)nextNode;
                                parentNode.CellItem.Pages.Insert(0, node.PageItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                        else if (isNodeCell)
                        {
                            // Is the current node contained inside the next node
                            bool contained = ContainsNode(nextNode, node);

                            // Remove cell from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            if (parentNode != null)
                                parentNode.SequenceItem.Children.Remove(node.CellItem);
                            parentCollection.Remove(node);

                            // If the next node is also a cell
                            if (isNextCell || contained)
                            {
                                // Add cell to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                parentCollection = (nextNode.Parent == null ? treeView.Nodes : nextNode.Parent.Nodes);
                                int pageIndex = parentCollection.IndexOf(nextNode);
                                if (previousParent != null)
                                    previousParent.SequenceItem.Children.Insert(pageIndex + 1, node.CellItem);
                                parentCollection.Insert(pageIndex + 1, node);
                            }
                            else if (isNextSequence)
                            {
                                // Add cell as first item of target sequence
                                parentNode = (MenuTreeNode)nextNode;
                                parentNode.SequenceItem.Children.Insert(0, node.CellItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                        else if (isNodeSequence)
                        {
                            // Is the current node contained inside the next node
                            bool contained = ContainsNode(nextNode, node);

                            // Remove sequence from parent collection
                            MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                            TreeNodeCollection parentCollection = (node.Parent == null ? treeView.Nodes : node.Parent.Nodes);
                            if (parentNode != null)
                                parentNode.SequenceItem.Children.Remove(node.SequenceItem);
                            parentCollection.Remove(node);

                            // If the next node is a cell
                            if (isNextCell || contained)
                            {
                                // Add sequence to the parent sequence of target cell
                                MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                                parentCollection = (nextNode.Parent == null ? treeView.Nodes : nextNode.Parent.Nodes);
                                int pageIndex = parentCollection.IndexOf(nextNode);
                                if (previousParent != null)
                                    previousParent.SequenceItem.Children.Insert(pageIndex + 1, node.SequenceItem);
                                parentCollection.Insert(pageIndex + 1, node);
                            }
                            else if (isNextSequence)
                            {
                                // Add sequence as first item of target sequence
                                parentNode = (MenuTreeNode)nextNode;
                                parentNode.SequenceItem.Children.Insert(0, node.SequenceItem);
                                parentNode.Nodes.Insert(0, node);
                            }
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddPage_Click(object sender, EventArgs e)
            {
                // Create new page and menu node for the page
                KryptonPage page = (KryptonPage)CreateInstance(typeof(KryptonPage));
                TreeNode newNode = new MenuTreeNode(page);

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode.CellItem != null)
                {
                    // Selected node is a cell, so append page to end of cells page collection
                    selectedNode.CellItem.Pages.Add(page);
                    selectedNode.Nodes.Add(newNode);
                }
                else if (selectedNode.PageItem != null)
                {
                    // Selected node is a page, so insert after this page
                    MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                    int selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                    selectedParentNode.CellItem.Pages.Insert(selectedIndex + 1, page);
                    selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddCell_Click(object sender, EventArgs e)
            {
                // Create new cell and menu node for the cell and two nodes for the child pages
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)CreateInstance(typeof(KryptonWorkspaceCell));
                TreeNode newNode = new MenuTreeNode(cell);

                // Add each page inside the new cell as a child of the new node
                foreach (KryptonPage page in cell.Pages)
                    newNode.Nodes.Add(new MenuTreeNode(page));
                newNode.Expand();

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode == null)
                {
                    // Nothing is selected, so add to the root
                    treeView.Nodes.Add(newNode);
                }
                else if (selectedNode.SequenceItem != null)
                {
                    // Selected node is a sequence, so append cell to end of sequence collection
                    selectedNode.SequenceItem.Children.Add(cell);
                    selectedNode.Nodes.Add(newNode);
                }
                else if (selectedNode.CellItem != null)
                {
                    if (selectedNode.Parent == null)
                    {
                        // Selected node is cell in root, so insert after it in the root
                        treeView.Nodes.Insert(treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                    }
                    else
                    {
                        // Selected node is a cell, so insert after this cell
                        MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                        int selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                        selectedParentNode.SequenceItem.Children.Insert(selectedIndex + 1, cell);
                        selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                    }
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddSequence_Click(object sender, EventArgs e)
            {
                // Create new sequence and menu node for the sequence
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)CreateInstance(typeof(KryptonWorkspaceSequence));
                TreeNode newNode = new MenuTreeNode(sequence);

                MenuTreeNode selectedNode = (MenuTreeNode)treeView.SelectedNode;
                if (selectedNode == null)
                {
                    // Nothing is selected, so add to the root
                    treeView.Nodes.Add(newNode);
                }
                else if (selectedNode.CellItem != null)
                {
                    if (selectedNode.Parent == null)
                    {
                        // Selected node is cell in root, so insert after it in the root
                        treeView.Nodes.Insert(treeView.Nodes.IndexOf(selectedNode) + 1, newNode);
                    }
                    else
                    {
                        // Selected node is a cell, so insert after this cell
                        MenuTreeNode selectedParentNode = (MenuTreeNode)selectedNode.Parent;
                        int selectedIndex = selectedParentNode.Nodes.IndexOf(selectedNode);
                        selectedParentNode.SequenceItem.Children.Insert(selectedIndex + 1, sequence);
                        selectedParentNode.Nodes.Insert(selectedIndex + 1, newNode);
                    }
                }
                else if (selectedNode.SequenceItem != null)
                {
                    // Selected node is a sequence, so append sequence to end of child collection
                    selectedNode.SequenceItem.Children.Add(sequence);
                    selectedNode.Nodes.Add(newNode);
                }

                // Selected the newly added node
                treeView.SelectedNode = newNode;
                treeView.Focus();

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                if (treeView.SelectedNode != null)
                {
                    MenuTreeNode treeNode = (MenuTreeNode)treeView.SelectedNode;

                    if (treeNode.Parent == null)
                    {
                        // Remove from the root collection
                        treeView.Nodes.Remove(treeNode);
                    }
                    else
                    {
                        // Remove from parent node collection
                        MenuTreeNode parentNode = (MenuTreeNode)treeNode.Parent;
                        treeNode.Parent.Nodes.Remove(treeNode);

                        // Remove item from parent container
                        if (parentNode.CellItem != null)
                            parentNode.CellItem.Pages.Remove(treeNode.Item);
                        else if (parentNode.SequenceItem != null)
                            parentNode.SequenceItem.Children.Remove(treeNode.Item);
                    }

                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void NodeToType(TreeNode node, out bool isPage, out bool isCell, out bool isSequence)
            {
                NodeToType((MenuTreeNode)node, out isPage, out isCell, out isSequence);
            }

            private void NodeToType(MenuTreeNode node, out bool isPage, out bool isCell, out bool isSequence)
            {
                isPage = (node != null) && (node.PageItem != null);
                isCell = (node != null) && (node.CellItem != null);
                isSequence = (node != null) && (node.SequenceItem != null);
            }

            private bool ContainsNode(TreeNode node, TreeNode find)
            {
                if (node.Nodes.Contains(find))
                    return true;
                else
                {
                    foreach (TreeNode child in node.Nodes)
                        if (ContainsNode(child, find))
                            return true;
                }

                return false;
            }

            private TreeNode NextNode(TreeNode currentNode)
            {
                if (currentNode == null)
                    return null;

                bool isPage, isCell, isSequence, found = false;
                NodeToType(currentNode, out isPage, out isCell, out isSequence);
                TreeNode returnNode = currentNode;

                do
                {
                    // Find the previous node
                    returnNode = RecursiveFind(treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, true);

                    // If we actually found a next node
                    if (returnNode != null)
                    {
                        // Cannot move a sequence inside itself
                        if (isSequence && ContainsNode(currentNode, returnNode))
                        {
                            found = false;
                            continue;
                        }
                    }

                    // Found no reason not the accept the found node
                    break;
                
                } while (returnNode != null);

                return returnNode;
            }

            private TreeNode PreviousNode(TreeNode currentNode)
            {
                if (currentNode == null)
                    return null;

                bool isPage, isCell, isSequence, found = false;
                NodeToType(currentNode, out isPage, out isCell, out isSequence);
                TreeNode returnNode = currentNode;

                do
                {
                    // Find the previous node
                    returnNode = RecursiveFind(treeView.Nodes, returnNode, ref found, isPage, isCell, isSequence, false);

                    // If we actually found a previous node
                    if (returnNode != null)
                    {
                        // If searching from a page that is the first page in the owning cell and the previous
                        // node is actually the owning cell of the page then we need to continue with searching
                        if (isPage && (currentNode.Index == 0) && (returnNode == currentNode.Parent))
                        {
                            found = false;
                            continue;
                        }
                    }

                    // Found no reason not the accept the found node
                    break;
                
                } while (returnNode != null);

                return returnNode;
            }

            private TreeNode RecursiveFind(TreeNodeCollection nodes,
                                           TreeNode target,
                                           ref bool found,
                                           bool findPage,
                                           bool findCell, 
                                           bool findSequence,
                                           bool forward)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    TreeNode node = nodes[forward ? i : nodes.Count - 1 - i];
                    bool isPage, isCell, isSequence;
                    NodeToType(node, out isPage, out isCell, out isSequence);

                    // Searching forward we check the node before any child collection
                    if (forward)
                    {
                        if (!found)
                            found |= (node == target);
                        else if ((isPage && findPage) || (isCell && (findPage || findCell)) || (isSequence && (findCell || findSequence)))
                                return node;
                    }

                    // Do not recurse into the children if looking forwards and at the target sequence
                    if (!(isSequence && findSequence && found && forward))
                    {
                        // Searching the child collection of nodes
                        TreeNode findNode = RecursiveFind(node.Nodes, target, ref found, findPage, findCell, findSequence, forward);

                        // If we found a node to return then return it now
                        if (findNode != null)
                            return findNode;
                        else if (found && (target != node))
                        {
                            if ((findCell && (isCell || isSequence)) ||
                                (findSequence && (isCell || isSequence)))
                                return node;
                        }

                        // Searching backwards we check the child collection after checking the node
                        if (!forward)
                        {
                            if (!found)
                                found |= (node == target);
                            else if ((isPage && findPage) || (isCell && (findPage || findCell)) || (isSequence && (findCell || findSequence)))
                                return node;
                        }
                    }
                }

                return null;
            }

            private void SeparatorToItems(ViewDrawWorkspaceSeparator separator,
                                          out IWorkspaceItem after,
                                          out IWorkspaceItem before)
            {
                // Workspace item after the separator (to the right or below)
                after = separator.WorkspaceItem;

                // Workspace item before the separator (to the left or above)
                KryptonWorkspaceSequence beforeSequence = (KryptonWorkspaceSequence)after.WorkspaceParent;

                // Previous items might be invisible and so search till we find the visible one we expect
                before = null;
                for (int i = beforeSequence.Children.IndexOf(after) - 1; i >= 0; i--)
                {
                    IWorkspaceItem item = beforeSequence.Children[i] as IWorkspaceItem;
                    if ((item != null) && item.WorkspaceVisible)
                    {
                        before = item;
                        break;
                    }
                }
            }

            private void UpdateButtons()
            {
                MenuTreeNode node = (MenuTreeNode)treeView.SelectedNode;
                bool isNone = (node == null);
                bool isPage = (node != null) && (node.PageItem != null);
                bool isCell = (node != null) && (node.CellItem != null);
                bool isSequence = (node != null) && (node.SequenceItem != null);

                buttonMoveUp.Enabled = !isNone && (PreviousNode(node) != null);
                buttonMoveDown.Enabled = !isNone && (NextNode(node) != null);
                buttonAddPage.Enabled = isPage || isCell;
                buttonAddCell.Enabled = isNone || isCell || isSequence;
                buttonAddSequence.Enabled = isNone || isCell || isSequence;
                buttonDelete.Enabled = (node != null);
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = treeView.SelectedNode;
                if (node == null)
                    propertyGrid.SelectedObject = null;
                else
                {
                    MenuTreeNode menuNode = (MenuTreeNode)node;

                    if (menuNode.PageItem != null)
                        propertyGrid.SelectedObject = new PageProxy(menuNode.PageItem);
                    else if (menuNode.CellItem != null)
                        propertyGrid.SelectedObject = new CellProxy(menuNode.CellItem);
                    else
                        propertyGrid.SelectedObject = new SequenceProxy(menuNode.SequenceItem);
                }
            }

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new DictItemBase();

                foreach (Component item in items)
                    AddItemsToDictionary(dictItems, item);

                return dictItems;
            }

            private void AddItemsToDictionary(DictItemBase dictItems, Component baseItem)
            {
                // Add item to the dictionary
                dictItems.Add(baseItem, baseItem);

                // Add pages from a cell
                if (baseItem is KryptonWorkspaceCell)
                {
                    KryptonWorkspaceCell cell = (KryptonWorkspaceCell)baseItem;
                    foreach (Component item in cell.Pages)
                        AddItemsToDictionary(dictItems, item);
                }

                // Add children from a sequence
                if (baseItem is KryptonWorkspaceSequence)
                {
                    KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)baseItem;
                    foreach (Component item in sequence.Children)
                        AddItemsToDictionary(dictItems, item);
                }
            }

            private void AddMenuTreeNode(Component item, MenuTreeNode parent)
            {
                // Create a node to match the item
                MenuTreeNode node = new MenuTreeNode(item);

                // Add to either root or parent node
                if (parent != null)
                    parent.Nodes.Add(node);
                else
                    treeView.Nodes.Add(node);

                // Add pages from a cell
                if (item is KryptonWorkspaceCell)
                {
                    KryptonWorkspaceCell cell = (KryptonWorkspaceCell)item;
                    foreach (Component page in cell.Pages)
                        AddMenuTreeNode(page, node);
                }

                // Add children from a sequence
                if (item is KryptonWorkspaceSequence)
                {
                    KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)item;
                    foreach (Component child in sequence.Children)
                        AddMenuTreeNode(child, node);
                }
            }

            private void SynchronizeCollections(DictItemBase before,
                                                DictItemBase after,
                                                ITypeDescriptorContext context)
            {
                // Add all new components (in the 'after' but not the 'before'
                foreach (Component item in after.Values)
                    if (!before.ContainsKey(item))
                    {
                        if (context.Container != null)
                            context.Container.Add(item);
                    }

                // Delete all old components (in the 'before' but not the 'after'
                foreach (Component item in before.Values)
                    if (!after.ContainsKey(item))
                    {
                        DestroyInstance(item);
                        
                        if (context.Container != null)
                            context.Container.Remove(item);
                    }

                IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (changeService != null)
                {
                    // Mark components as changed when not added or removed
                    foreach (Component item in after.Values)
                        if (before.ContainsKey(item))
                        {
                            changeService.OnComponentChanging(item, null);
                            changeService.OnComponentChanged(item, null, null, null);
                        }
                }
            }
            #endregion
        }
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonWorkspaceCollectionEditor class.
		/// </summary>
        public KryptonWorkspaceCollectionEditor()
            : base(typeof(KryptonWorkspaceCollection))
		{
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the owning workspace instance.
        /// </summary>
        public KryptonWorkspace Workspace
        {
            get
            {
                KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)Context.Instance;
                return sequence.WorkspaceControl;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            return new KryptonWorkspaceCollectionForm(this);
        }
        #endregion
    }
}
