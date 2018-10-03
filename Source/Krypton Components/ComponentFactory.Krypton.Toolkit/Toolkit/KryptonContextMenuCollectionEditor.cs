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
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Designer for a collection of context menu items.
    /// </summary>
	public class KryptonContextMenuCollectionEditor : CollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonContextMenuCollection.
        /// </summary>
        protected partial class KryptonContextMenuCollectionForm : CollectionEditor.CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declaractions!
            /// </summary>
            protected class DictItemBase : Dictionary<KryptonContextMenuItemBase, KryptonContextMenuItemBase> { };

            /// <summary>
            /// Tree node that is attached to a context menu item.
            /// </summary>
            protected class MenuTreeNode : TreeNode
            {
                #region Instance Fields
                private KryptonContextMenuItemBase _item;
                private object _propertyObject;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the MenuTreeNode class.
                /// </summary>
                /// <param name="item">Menu item to represent.</param>
                public MenuTreeNode(KryptonContextMenuItemBase item)
                {
                    Debug.Assert(item != null);
                    _item = item;
                    _propertyObject = item;

                    // Setup the initial starting image and description strings
                    ImageIndex = ImageIndexFromItem();
                    SelectedImageIndex = ImageIndex;
                    Text = _item.ToString();

                    // Hook into property changes
                    _item.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets access to the associated item.
                /// </summary>
                public KryptonContextMenuItemBase Item
                {
                    get { return _item; }
                }

                /// <summary>
                /// Gets access to object wrapper for use in the property grid.
                /// </summary>
                public object PropertyObject
                {
                    get { return _propertyObject; }
                }
                #endregion

                #region Implementation
                private int ImageIndexFromItem()
                {
                    if (_item is KryptonContextMenuCheckBox)
                        return 6;
                    else if (_item is KryptonContextMenuCheckButton)
                        return 7;
                    else if (_item is KryptonContextMenuColorColumns)
                        return 0;
                    else if (_item is KryptonContextMenuHeading)
                        return 1;
                    else if (_item is KryptonContextMenuItem)
                        return 2;
                    else if (_item is KryptonContextMenuItems)
                        return 3;
                    else if (_item is KryptonContextMenuLinkLabel)
                        return 8;
                    else if (_item is KryptonContextMenuRadioButton)
                        return 5;
                    else if (_item is KryptonContextMenuSeparator)
                        return 4;
                    else if (_item is KryptonContextMenuImageSelect)
                        return 13;
                    else if (_item is KryptonContextMenuMonthCalendar)
                        return 14;

                    Debug.Assert(false);
                    return -1;
                }

                private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
                {
                    // Update with correct string for new state
                    Text = _item.ToString();
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
            private DictItemBase _beforeItems;
            private KryptonContextMenuCollectionEditor _editor;
            private Button buttonOK;
            private TreeView treeView;
            private Label label1;
            private Label label2;
            private ImageList imageList;
            private Button buttonDelete;
            private Button buttonMoveUp;
            private Button buttonMoveDown;
            private Button buttonAddCheckBox;
            private Button buttonAddCheckButton;
            private Button buttonAddRadioButton;
            private Button buttonAddLinkLabel;
            private Button buttonAddSeparator;
            private Button buttonAddItem;
            private Button buttonAddItems;
            private Button buttonAddHeading;
            private Button buttonAddMonthCalendar;
            private Button buttonAddColorColumns;
            private Button buttonAddImageSelect;
            private PropertyGrid propertyGrid1;
            private IContainer components = null;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonContextMenuCollectionForm class.
            /// </summary>
            public KryptonContextMenuCollectionForm(KryptonContextMenuCollectionEditor editor)
                : base(editor)
            {
                _editor = editor;

                this.components = new System.ComponentModel.Container();
                this.buttonOK = new System.Windows.Forms.Button();
                this.treeView = new System.Windows.Forms.TreeView();
                this.imageList = new System.Windows.Forms.ImageList(this.components);
                this.label1 = new System.Windows.Forms.Label();
                this.buttonDelete = new System.Windows.Forms.Button();
                this.buttonMoveUp = new System.Windows.Forms.Button();
                this.buttonMoveDown = new System.Windows.Forms.Button();
                this.buttonAddCheckBox = new System.Windows.Forms.Button();
                this.buttonAddCheckButton = new System.Windows.Forms.Button();
                this.buttonAddRadioButton = new System.Windows.Forms.Button();
                this.buttonAddLinkLabel = new System.Windows.Forms.Button();
                this.buttonAddSeparator = new System.Windows.Forms.Button();
                this.buttonAddItem = new System.Windows.Forms.Button();
                this.buttonAddItems = new System.Windows.Forms.Button();
                this.buttonAddHeading = new System.Windows.Forms.Button();
                this.buttonAddMonthCalendar = new System.Windows.Forms.Button();
                this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
                this.label2 = new System.Windows.Forms.Label();
                this.buttonAddColorColumns = new System.Windows.Forms.Button();
                this.buttonAddImageSelect = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // buttonOK
                // 
                this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.buttonOK.Location = new System.Drawing.Point(630, 504);
                this.buttonOK.Name = "buttonOK";
                this.buttonOK.Size = new System.Drawing.Size(75, 23);
                this.buttonOK.TabIndex = 16;
                this.buttonOK.Text = "OK";
                this.buttonOK.UseVisualStyleBackColor = true;
                this.buttonOK.Click += new EventHandler(buttonOK_Click);   
                // 
                // treeView
                // 
                this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.treeView.HideSelection = false;
                this.treeView.ImageIndex = 0;
                this.treeView.ImageList = this.imageList;
                this.treeView.Location = new System.Drawing.Point(16, 29);
                this.treeView.Name = "treeView";
                this.treeView.SelectedImageIndex = 0;
                this.treeView.Size = new System.Drawing.Size(251, 466);
                this.treeView.TabIndex = 0;
                this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SelectionChanged);
                // 
                // imageList
                // 
                this.imageList.TransparentColor = System.Drawing.Color.Magenta;
                this.imageList.Images.AddRange(new Image[]{
                    Properties.Resources.KryptonContextMenuColorColumns,
                    Properties.Resources.KryptonContextMenuHeading,
                    Properties.Resources.KryptonContextMenuItem,
                    Properties.Resources.KryptonContextMenuItems,
                    Properties.Resources.KryptonContextMenuSeparator,
                    Properties.Resources.KryptonRadioButton,
                    Properties.Resources.KryptonCheckBox,
                    Properties.Resources.KryptonCheckButton,
                    Properties.Resources.KryptonLinkLabel,
                    Properties.Resources.delete2,
                    Properties.Resources.arrow_up_blue,
                    Properties.Resources.arrow_down_blue,
                    Properties.Resources.KryptonContextMenuColorColumns,
                    Properties.Resources.KryptonContextMenuImageSelect,
                    Properties.Resources.KryptonMonthCalendar});
                this.imageList.Images.SetKeyName(0, "KryptonContextMenuColorColumns.bmp");
                this.imageList.Images.SetKeyName(1, "KryptonContextMenuHeading.bmp");
                this.imageList.Images.SetKeyName(2, "KryptonContextMenuItem.bmp");
                this.imageList.Images.SetKeyName(3, "KryptonContextMenuItems.bmp");
                this.imageList.Images.SetKeyName(4, "KryptonContextMenuSeparator.bmp");
                this.imageList.Images.SetKeyName(5, "KryptonRadioButton.bmp");
                this.imageList.Images.SetKeyName(6, "KryptonCheckBox.bmp");
                this.imageList.Images.SetKeyName(7, "KryptonCheckButton.bmp");
                this.imageList.Images.SetKeyName(8, "KryptonLinkLabel.bmp");
                this.imageList.Images.SetKeyName(9, "delete2.png");
                this.imageList.Images.SetKeyName(10, "arrow_up_blue.png");
                this.imageList.Images.SetKeyName(11, "arrow_down_blue.png");
                this.imageList.Images.SetKeyName(12, "KryptonContextMenuColorColumns.bmp");
                this.imageList.Images.SetKeyName(13, "KryptonContextMenuImageSelect.bmp");
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(13, 11);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(75, 13);
                this.label1.TabIndex = 7;
                this.label1.Text = "Item Hierarchy";
                // 
                // buttonDelete
                // 
                this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonDelete.ImageIndex = 9;
                this.buttonDelete.ImageList = this.imageList;
                this.buttonDelete.Location = new System.Drawing.Point(282, 467);
                this.buttonDelete.Name = "buttonDelete";
                this.buttonDelete.Size = new System.Drawing.Size(144, 28);
                this.buttonDelete.TabIndex = 14;
                this.buttonDelete.Text = "Delete";
                this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonDelete.UseVisualStyleBackColor = true;
                this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
                // 
                // buttonMoveUp
                // 
                this.buttonMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveUp.ImageIndex = 10;
                this.buttonMoveUp.ImageList = this.imageList;
                this.buttonMoveUp.Location = new System.Drawing.Point(282, 29);
                this.buttonMoveUp.Name = "buttonMoveUp";
                this.buttonMoveUp.Size = new System.Drawing.Size(144, 28);
                this.buttonMoveUp.TabIndex = 1;
                this.buttonMoveUp.Text = "Move Up";
                this.buttonMoveUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonMoveUp.UseVisualStyleBackColor = true;
                this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
                // 
                // buttonMoveDown
                // 
                this.buttonMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveDown.ImageIndex = 11;
                this.buttonMoveDown.ImageList = this.imageList;
                this.buttonMoveDown.Location = new System.Drawing.Point(282, 60);
                this.buttonMoveDown.Name = "buttonMoveDown";
                this.buttonMoveDown.Size = new System.Drawing.Size(144, 28);
                this.buttonMoveDown.TabIndex = 2;
                this.buttonMoveDown.Text = "Move Down";
                this.buttonMoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonMoveDown.UseVisualStyleBackColor = true;
                this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
                // 
                // buttonAddCheckBox
                // 
                this.buttonAddCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddCheckBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddCheckBox.ImageIndex = 6;
                this.buttonAddCheckBox.ImageList = this.imageList;
                this.buttonAddCheckBox.Location = new System.Drawing.Point(282, 231);
                this.buttonAddCheckBox.Name = "buttonAddCheckBox";
                this.buttonAddCheckBox.Size = new System.Drawing.Size(144, 28);
                this.buttonAddCheckBox.TabIndex = 7;
                this.buttonAddCheckBox.Text = "Add CheckBox";
                this.buttonAddCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddCheckBox.UseVisualStyleBackColor = true;
                this.buttonAddCheckBox.Click += new System.EventHandler(this.buttonAddCheckBox_Click);
                // 
                // buttonAddCheckButton
                // 
                this.buttonAddCheckButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddCheckButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddCheckButton.ImageIndex = 7;
                this.buttonAddCheckButton.ImageList = this.imageList;
                this.buttonAddCheckButton.Location = new System.Drawing.Point(282, 263);
                this.buttonAddCheckButton.Name = "buttonAddCheckButton";
                this.buttonAddCheckButton.Size = new System.Drawing.Size(144, 28);
                this.buttonAddCheckButton.TabIndex = 8;
                this.buttonAddCheckButton.Text = "Add CheckButton";
                this.buttonAddCheckButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddCheckButton.UseVisualStyleBackColor = true;
                this.buttonAddCheckButton.Click += new System.EventHandler(this.buttonAddCheckButton_Click);
                // 
                // buttonAddRadioButton
                // 
                this.buttonAddRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddRadioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddRadioButton.ImageIndex = 5;
                this.buttonAddRadioButton.ImageList = this.imageList;
                this.buttonAddRadioButton.Location = new System.Drawing.Point(282, 295);
                this.buttonAddRadioButton.Name = "buttonAddRadioButton";
                this.buttonAddRadioButton.Size = new System.Drawing.Size(144, 28);
                this.buttonAddRadioButton.TabIndex = 9;
                this.buttonAddRadioButton.Text = "Add RadioButton";
                this.buttonAddRadioButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddRadioButton.UseVisualStyleBackColor = true;
                this.buttonAddRadioButton.Click += new System.EventHandler(this.buttonAddRadioButton_Click);
                // 
                // buttonAddLinkLabel
                // 
                this.buttonAddLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddLinkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddLinkLabel.ImageIndex = 8;
                this.buttonAddLinkLabel.ImageList = this.imageList;
                this.buttonAddLinkLabel.Location = new System.Drawing.Point(282, 327);
                this.buttonAddLinkLabel.Name = "buttonAddLinkLabel";
                this.buttonAddLinkLabel.Size = new System.Drawing.Size(144, 28);
                this.buttonAddLinkLabel.TabIndex = 10;
                this.buttonAddLinkLabel.Text = "Add LinkLabel";
                this.buttonAddLinkLabel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddLinkLabel.UseVisualStyleBackColor = true;
                this.buttonAddLinkLabel.Click += new System.EventHandler(this.buttonAddLinkLabel_Click);
                // 
                // buttonAddSeparator
                // 
                this.buttonAddSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddSeparator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddSeparator.ImageIndex = 4;
                this.buttonAddSeparator.ImageList = this.imageList;
                this.buttonAddSeparator.Location = new System.Drawing.Point(282, 199);
                this.buttonAddSeparator.Name = "buttonAddSeparator";
                this.buttonAddSeparator.Size = new System.Drawing.Size(144, 28);
                this.buttonAddSeparator.TabIndex = 6;
                this.buttonAddSeparator.Text = "Add Separator";
                this.buttonAddSeparator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddSeparator.UseVisualStyleBackColor = true;
                this.buttonAddSeparator.Click += new System.EventHandler(this.buttonAddSeparator_Click);
                // 
                // buttonAddItem
                // 
                this.buttonAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddItem.ImageIndex = 2;
                this.buttonAddItem.ImageList = this.imageList;
                this.buttonAddItem.Location = new System.Drawing.Point(282, 103);
                this.buttonAddItem.Name = "buttonAddItem";
                this.buttonAddItem.Size = new System.Drawing.Size(144, 28);
                this.buttonAddItem.TabIndex = 3;
                this.buttonAddItem.Text = "Add Item";
                this.buttonAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddItem.UseVisualStyleBackColor = true;
                this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
                // 
                // buttonAddItems
                // 
                this.buttonAddItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddItems.ImageIndex = 3;
                this.buttonAddItems.ImageList = this.imageList;
                this.buttonAddItems.Location = new System.Drawing.Point(282, 135);
                this.buttonAddItems.Name = "buttonAddItems";
                this.buttonAddItems.Size = new System.Drawing.Size(144, 28);
                this.buttonAddItems.TabIndex = 4;
                this.buttonAddItems.Text = "Add Items";
                this.buttonAddItems.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddItems.UseVisualStyleBackColor = true;
                this.buttonAddItems.Click += new System.EventHandler(this.buttonAddItems_Click);
                // 
                // buttonAddHeading
                // 
                this.buttonAddHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddHeading.ImageIndex = 1;
                this.buttonAddHeading.ImageList = this.imageList;
                this.buttonAddHeading.Location = new System.Drawing.Point(282, 167);
                this.buttonAddHeading.Name = "buttonAddHeading";
                this.buttonAddHeading.Size = new System.Drawing.Size(144, 28);
                this.buttonAddHeading.TabIndex = 5;
                this.buttonAddHeading.Text = "Add Heading";
                this.buttonAddHeading.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddHeading.UseVisualStyleBackColor = true;
                this.buttonAddHeading.Click += new System.EventHandler(this.buttonAddHeading_Click);
                // 
                // buttonAddMonthCalendar
                // 
                this.buttonAddMonthCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddMonthCalendar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddMonthCalendar.ImageIndex = 14;
                this.buttonAddMonthCalendar.ImageList = this.imageList;
                this.buttonAddMonthCalendar.Location = new System.Drawing.Point(282, 423);
                this.buttonAddMonthCalendar.Name = "buttonAddMonthCalendar";
                this.buttonAddMonthCalendar.Size = new System.Drawing.Size(144, 28);
                this.buttonAddMonthCalendar.TabIndex = 13;
                this.buttonAddMonthCalendar.Text = "Add Month Calendar";
                this.buttonAddMonthCalendar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddMonthCalendar.UseVisualStyleBackColor = true;
                this.buttonAddMonthCalendar.Click += new System.EventHandler(this.buttonAddMonthCalendar_Click);
                // 
                // propertyGrid1
                // 
                this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.propertyGrid1.HelpVisible = false;
                this.propertyGrid1.Location = new System.Drawing.Point(439, 29);
                this.propertyGrid1.Name = "propertyGrid1";
                this.propertyGrid1.Size = new System.Drawing.Size(266, 466);
                this.propertyGrid1.TabIndex = 15;
                this.propertyGrid1.ToolbarVisible = false;
                // 
                // label2
                // 
                this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(436, 11);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(77, 13);
                this.label2.TabIndex = 16;
                this.label2.Text = "Item Properties";
                // 
                // buttonAddColorColumns
                // 
                this.buttonAddColorColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddColorColumns.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddColorColumns.ImageIndex = 12;
                this.buttonAddColorColumns.ImageList = this.imageList;
                this.buttonAddColorColumns.Location = new System.Drawing.Point(282, 359);
                this.buttonAddColorColumns.Name = "buttonAddColorColumns";
                this.buttonAddColorColumns.Size = new System.Drawing.Size(144, 28);
                this.buttonAddColorColumns.TabIndex = 11;
                this.buttonAddColorColumns.Text = "Add ColorColumns";
                this.buttonAddColorColumns.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddColorColumns.UseVisualStyleBackColor = true;
                this.buttonAddColorColumns.Click += new System.EventHandler(this.buttonAddColorColumns_Click);
                // 
                // buttonAddImageSelect
                // 
                this.buttonAddImageSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddImageSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddImageSelect.ImageIndex = 13;
                this.buttonAddImageSelect.ImageList = this.imageList;
                this.buttonAddImageSelect.Location = new System.Drawing.Point(282, 391);
                this.buttonAddImageSelect.Name = "buttonAddImageSelect";
                this.buttonAddImageSelect.Size = new System.Drawing.Size(144, 28);
                this.buttonAddImageSelect.TabIndex = 12;
                this.buttonAddImageSelect.Text = "Add ImageSelect";
                this.buttonAddImageSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddImageSelect.UseVisualStyleBackColor = true;
                this.buttonAddImageSelect.Click += new System.EventHandler(this.buttonAddImageSelect_Click);
                // 
                // KryptonContextMenuEditorForm
                // 
                this.AcceptButton = this.buttonOK;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.ClientSize = new System.Drawing.Size(717, 557);
                this.ControlBox = false;
                this.Controls.Add(this.buttonAddColorColumns);
                this.Controls.Add(this.buttonAddImageSelect);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.propertyGrid1);
                this.Controls.Add(this.buttonAddMonthCalendar);
                this.Controls.Add(this.buttonAddHeading);
                this.Controls.Add(this.buttonAddItems);
                this.Controls.Add(this.buttonAddItem);
                this.Controls.Add(this.buttonAddSeparator);
                this.Controls.Add(this.buttonAddLinkLabel);
                this.Controls.Add(this.buttonAddRadioButton);
                this.Controls.Add(this.buttonAddCheckButton);
                this.Controls.Add(this.buttonAddCheckBox);
                this.Controls.Add(this.buttonMoveDown);
                this.Controls.Add(this.buttonMoveUp);
                this.Controls.Add(this.buttonDelete);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.treeView);
                this.Controls.Add(this.buttonOK);
                this.MinimumSize = new System.Drawing.Size(733, 593);
                this.Name = "KryptonContextMenuEditorForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "KryptonContextMenu Items Editor";
                this.Load += new System.EventHandler(this.KryptonContextMenuEditorForm_Load);
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
                    // Cache a lookup of all items before changes are made
                    _beforeItems = CreateItemsDictionary(Items);

                    // Need to link the property browser to a site otherwise Image properties cannot be
                    // edited because it cannot navigate to the owning project for its resources
                    propertyGrid1.Site = new PropertyGridSite(Context, propertyGrid1);

                    // Add all the top level clones
                    treeView.Nodes.Clear();
                    foreach (KryptonContextMenuItemBase item in Items)
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
            private void KryptonContextMenuEditorForm_Load(object sender, EventArgs e)
            {
                // Set allowed categories into the property grid filter
                propertyGrid1.BrowsableAttributes = new AttributeCollection(new KryptonPersistAttribute());
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
               
                // Clear down contents of tree as this form can be reused
                treeView.Nodes.Clear();

                // Inform designer of changes in component items
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at the root level then move up in the root items collection
                    if (node.Parent == null)
                    {
                        int index = treeView.Nodes.IndexOf(node);
                        treeView.Nodes.Remove(node);
                        treeView.Nodes.Insert(index - 1, node);
                    }
                    else
                    {
                        int index = node.Parent.Nodes.IndexOf(node);
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        if (treeParentNode.Item is KryptonContextMenuItems)
                        {
                            KryptonContextMenuItems items = treeParentNode.Item as KryptonContextMenuItems;
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index - 1, treeNode.Item);
                        }
                        else if (treeParentNode.Item is KryptonContextMenuItem)
                        {
                            KryptonContextMenuItem items = treeParentNode.Item as KryptonContextMenuItem;
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index - 1, treeNode.Item);
                        }

                        parentNode.Nodes.Remove(node);
                        parentNode.Nodes.Insert(index - 1, node);
                    }

                    treeView.SelectedNode = node;
                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at the root level then move down in the root items collection
                    if (node.Parent == null)
                    {
                        int index = treeView.Nodes.IndexOf(node);
                        treeView.Nodes.Remove(node);
                        treeView.Nodes.Insert(index + 1, node);
                    }
                    else
                    {
                        int index = node.Parent.Nodes.IndexOf(node);
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        if (treeParentNode.Item is KryptonContextMenuItems)
                        {
                            KryptonContextMenuItems items = treeParentNode.Item as KryptonContextMenuItems;
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index + 1, treeNode.Item);
                        }
                        else if (treeParentNode.Item is KryptonContextMenuItem)
                        {
                            KryptonContextMenuItem items = treeParentNode.Item as KryptonContextMenuItem;
                            items.Items.Remove(treeNode.Item);
                            items.Items.Insert(index + 1, treeNode.Item);
                        }

                        parentNode.Nodes.Remove(node);
                        parentNode.Nodes.Insert(index + 1, node);
                    }

                    treeView.SelectedNode = node;
                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddItem_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItem)));
            }

            private void buttonAddItems_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuItems)));
            }

            private void buttonAddHeading_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuHeading)));
            }

            private void buttonAddMonthCalendar_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuMonthCalendar)));
            }

            private void buttonAddSeparator_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuSeparator)));
            }

            private void buttonAddCheckBox_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckBox)));
            }

            private void buttonAddCheckButton_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuCheckButton)));
            }

            private void buttonAddRadioButton_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuRadioButton)));
            }

            private void buttonAddLinkLabel_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuLinkLabel)));
            }

            private void buttonAddColorColumns_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuColorColumns)));
            }

            private void buttonAddImageSelect_Click(object sender, EventArgs e)
            {
                AddNewItem((KryptonContextMenuItemBase)CreateInstance(typeof(KryptonContextMenuImageSelect)));
            }

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at root level then remove from root, otherwise from the parent collection
                    if (node.Parent == null)
                        treeView.Nodes.Remove(node);
                    else
                    {
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        if (treeParentNode.Item is KryptonContextMenuItems)
                        {
                            KryptonContextMenuItems items = treeParentNode.Item as KryptonContextMenuItems;
                            items.Items.Remove(treeNode.Item);
                        }
                        else if (treeParentNode.Item is KryptonContextMenuItem)
                        {
                            KryptonContextMenuItem items = treeParentNode.Item as KryptonContextMenuItem;
                            items.Items.Remove(treeNode.Item);
                        }

                        node.Parent.Nodes.Remove(node);
                    }

                    treeView.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void SelectionChanged(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = treeView.SelectedNode;
                if (node == null)
                    propertyGrid1.SelectedObject = null;
                else
                    propertyGrid1.SelectedObject = ((MenuTreeNode)node).PropertyObject;
            }

            private void AddMenuTreeNode(KryptonContextMenuItemBase item, MenuTreeNode parent)
            {
                // Create a node to match the item
                MenuTreeNode node = new MenuTreeNode(item);

                // Add to either root or parent node
                if (parent != null)
                    parent.Nodes.Add(node);
                else
                    treeView.Nodes.Add(node);

                // Check for items that can contain collections of children
                if (item is KryptonContextMenuItems)
                {
                    KryptonContextMenuItems itemsCollection = (KryptonContextMenuItems)item;
                    foreach (KryptonContextMenuItemBase child in itemsCollection.Items)
                        AddMenuTreeNode(child, node);
                }
                else if (item is KryptonContextMenuItem)
                {
                    KryptonContextMenuItem itemsCollection = (KryptonContextMenuItem)item;
                    foreach (KryptonContextMenuItemBase child in itemsCollection.Items)
                        AddMenuTreeNode(child, node);
                }
            }

            private void AddNewItem(KryptonContextMenuItemBase item)
            {
                TreeNode selectedNode = treeView.SelectedNode;
                TreeNode newNode = new MenuTreeNode(item);

                // If there is no selection then append to root
                if (selectedNode == null)
                    treeView.Nodes.Add(newNode);
                else
                {
                    // If current selection is at the root
                    if (selectedNode.Parent == null)
                    {
                        // If adding a menu item to an items
                        if (item is KryptonContextMenuItem)
                        {
                            MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                            KryptonContextMenuItems items = treeSelectedNode.Item as KryptonContextMenuItems;
                            items.Items.Add(item);
                            selectedNode.Nodes.Add(newNode);
                        }
                        else
                        {
                            int index = treeView.Nodes.IndexOf(selectedNode);
                            treeView.Nodes.Insert(index + 1, newNode);
                        }
                    }
                    else
                    {
                        int index = selectedNode.Parent.Nodes.IndexOf(selectedNode);
                        TreeNode parentNode = selectedNode.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;

                        if (treeParentNode.Item is KryptonContextMenuItems)
                        {
                            if (ValidInItemCollection(item))
                            {
                                KryptonContextMenuItems items = treeParentNode.Item as KryptonContextMenuItems;
                                items.Items.Insert(index + 1, item);
                                selectedNode.Parent.Nodes.Insert(index + 1, newNode);
                            }
                            else
                            {
                                MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                                Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItem);
                                KryptonContextMenuItem items = treeSelectedNode.Item as KryptonContextMenuItem;
                                items.Items.Add(item);
                                selectedNode.Nodes.Add(newNode);
                            }
                        }
                        else if (treeParentNode.Item is KryptonContextMenuItem)
                        {
                            if (ValidInCollection(item))
                            {
                                KryptonContextMenuItem items = treeParentNode.Item as KryptonContextMenuItem;
                                items.Items.Insert(index + 1, item);
                                selectedNode.Parent.Nodes.Insert(index + 1, newNode);
                            }
                            else
                            {
                                MenuTreeNode treeSelectedNode = selectedNode as MenuTreeNode;
                                Debug.Assert(treeSelectedNode.Item is KryptonContextMenuItems);
                                KryptonContextMenuItems items = treeSelectedNode.Item as KryptonContextMenuItems;
                                items.Items.Add(item);
                                selectedNode.Nodes.Add(newNode);
                            }
                        }
                    }
                }

                // Select the newly added node
                if (newNode != null)
                {
                    treeView.SelectedNode = newNode;
                    treeView.Focus();
                }

                UpdateButtons();
            }

            private void UpdateButtons()
            {
                KryptonContextMenuItemBase item = null;
                KryptonContextMenuItemBase parent = null;
                int parentNodeCount = treeView.Nodes.Count;
                int nodeIndex = -1;

                MenuTreeNode node = treeView.SelectedNode as MenuTreeNode;
                if (node != null)
                {
                    item = node.Item;
                    nodeIndex = treeView.Nodes.IndexOf(node);
                    if (node.Parent != null)
                    {
                        parentNodeCount = node.Parent.Nodes.Count;
                        nodeIndex = node.Parent.Nodes.IndexOf(node);
                        node = node.Parent as MenuTreeNode;
                        if (node != null)
                            parent = node.Item;
                    }
                }

                buttonMoveUp.Enabled = ((item != null) && (nodeIndex > 0));
                buttonMoveDown.Enabled = ((item != null) && (nodeIndex < (parentNodeCount - 1)));
                buttonAddItem.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItem));
                buttonAddItems.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuItems));
                buttonAddSeparator.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuSeparator));
                buttonAddHeading.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuHeading));
                buttonAddMonthCalendar.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuMonthCalendar));
                buttonAddCheckBox.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckBox));
                buttonAddCheckButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuCheckButton));
                buttonAddRadioButton.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuRadioButton));
                buttonAddLinkLabel.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuLinkLabel));
                buttonAddColorColumns.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuColorColumns));
                buttonAddImageSelect.Enabled = AllowAddItem(item, parent, typeof(KryptonContextMenuImageSelect));
                buttonDelete.Enabled = (item != null);
            }

            private bool AllowAddItem(KryptonContextMenuItemBase item,
                                      KryptonContextMenuItemBase parent,
                                      Type addType)
            {
                // Special case the you can use add button on an Items collection so it adds an item inside it
                if ((item is KryptonContextMenuItems) && addType.Equals(typeof(KryptonContextMenuItem)))
                    return true;

                if (ItemInsideCollection(item, parent))
                {
                    KryptonContextMenuCollection temp = new KryptonContextMenuCollection();
                    foreach (Type t in temp.RestrictTypes)
                        if (t.Equals(addType))
                            return true;
                }
                else
                {
                    KryptonContextMenuItemCollection temp1 = new KryptonContextMenuItemCollection();
                    foreach (Type t in temp1.RestrictTypes)
                        if (t.Equals(addType))
                            return true;

                    if ((item != null) && (item is KryptonContextMenuItem))
                    {
                        KryptonContextMenuCollection temp2 = new KryptonContextMenuCollection();
                        foreach (Type t in temp2.RestrictTypes)
                            if (t.Equals(addType))
                                return true;
                    }
                }

                return false;
            }

            private bool ValidInCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuCollection temp = new KryptonContextMenuCollection();
                foreach (Type t in temp.RestrictTypes)
                    if (t.Equals(addType))
                        return true;

                return false;
            }

            private bool ValidInItemCollection(KryptonContextMenuItemBase item)
            {
                Type addType = item.GetType();
                KryptonContextMenuItemCollection temp = new KryptonContextMenuItemCollection();
                foreach (Type t in temp.RestrictTypes)
                    if (t.Equals(addType))
                        return true;

                return false;
            }

            private bool ItemInsideCollection(KryptonContextMenuItemBase item,
                                              KryptonContextMenuItemBase parent)
            {
                // If it has no parent the it must be inside a collection
                if (parent == null)
                    return true;
                else
                {
                    // If inside an items then not inside a collection
                    return !(parent is KryptonContextMenuItems);
                }
            }

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new DictItemBase();

                foreach (KryptonContextMenuItemBase item in items)
                    AddItemsToDictionary(dictItems, item);

                return dictItems;
            }

            private void AddItemsToDictionary(DictItemBase dictItems, KryptonContextMenuItemBase baseItem)
            {
                // Add item to the dictionary
                dictItems.Add(baseItem, baseItem);

                // Add children of an items collection
                if (baseItem is KryptonContextMenuItems)
                {
                    KryptonContextMenuItems items = (KryptonContextMenuItems)baseItem;
                    foreach (KryptonContextMenuItemBase childItem in items.Items)
                        AddItemsToDictionary(dictItems, childItem);
                }

                // Add children of an item
                if (baseItem is KryptonContextMenuItem)
                {
                    KryptonContextMenuItem item = (KryptonContextMenuItem)baseItem;
                    foreach (KryptonContextMenuItemBase childItem in item.Items)
                        AddItemsToDictionary(dictItems, childItem);
                }
            }

            private void SynchronizeCollections(DictItemBase before,
                                                DictItemBase after,
                                                ITypeDescriptorContext context)
            {
                // Add all new components (in the 'after' but not the 'before'
                foreach (KryptonContextMenuItemBase item in after.Values)
                    if (!before.ContainsKey(item))
                    {
                        if (context.Container != null)
                            context.Container.Add(item as IComponent);
                    }

                // Delete all old components (in the 'before' but not the 'after'
                foreach (KryptonContextMenuItemBase item in before.Values)
                    if (!after.ContainsKey(item))
                    {
                        DestroyInstance(item);

                        if (context.Container != null)
                            context.Container.Remove(item as IComponent);
                    }

                IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                if (changeService != null)
                {
                    // Mark components as changed when not added or removed
                    foreach (KryptonContextMenuItemBase item in after.Values)
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
        /// Initialize a new instance of the KryptonContextMenuCollectionEditor class.
		/// </summary>
        public KryptonContextMenuCollectionEditor()
            : base(typeof(KryptonContextMenuCollection))
		{
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A CollectionForm to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            return new KryptonContextMenuCollectionForm(this);
        }

		/// <summary>
		/// Gets the data types that this collection editor can contain. 
		/// </summary>
		/// <returns>An array of data types that this collection can contain.</returns>
		protected override Type[] CreateNewItemTypes()
		{
            return new Type[] { typeof(KryptonContextMenuItems),
                                typeof(KryptonContextMenuSeparator),
                                typeof(KryptonContextMenuHeading),
                                typeof(KryptonContextMenuLinkLabel),
                                typeof(KryptonContextMenuCheckBox),
                                typeof(KryptonContextMenuCheckButton),
                                typeof(KryptonContextMenuRadioButton),
                                typeof(KryptonContextMenuColorColumns),
                                typeof(KryptonContextMenuMonthCalendar),
                                typeof(KryptonContextMenuImageSelect)};
		}
        #endregion
	}
}
