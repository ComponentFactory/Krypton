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

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonBreadCrumbItemsEditor : CollectionEditor
    {
        #region Classes
        /// <summary>
        /// Form used for editing the KryptonBreadCrumbItems.
        /// </summary>
        protected partial class KryptonBreadCrumbItemsForm : CollectionEditor.CollectionForm
        {
            #region Types
            /// <summary>
            /// Simple class to reduce the length of declaractions!
            /// </summary>
            protected class DictItemBase : Dictionary<KryptonBreadCrumbItem, KryptonBreadCrumbItem> { };

            /// <summary>
            /// Act as proxy for a crumb item to control the exposed properties to the property grid.
            /// </summary>
            protected class CrumbProxy
            {
                #region Instance Fields
                private KryptonBreadCrumbItem _item;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the KryptonBreadCrumbItem class.
                /// </summary>
                /// <param name="item">Item to act as proxy for.</param>
                public CrumbProxy(KryptonBreadCrumbItem item)
                {
                    _item = item;
                }
                #endregion

                #region ShortText
                /// <summary>
                /// Gets and sets the short text.
                /// </summary>
                [Category("Appearance")]
                public string ShortText
                {
                    get { return _item.ShortText; }
                    set { _item.ShortText = value; }
                }
                #endregion

                #region LongText
                /// <summary>
                /// Gets and sets the long text.
                /// </summary>
                [Category("Appearance")]
                public string LongText
                {
                    get { return _item.LongText; }
                    set { _item.LongText = value; }
                }
                #endregion

                #region Image
                /// <summary>
                /// Gets and sets the image.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(null)]
                public Image Image
                {
                    get { return _item.Image; }
                    set { _item.Image = value; }
                }
                #endregion

                #region ImageTransparentColor
                /// <summary>
                /// Gets and sets the image transparent color.
                /// </summary>
                [Category("Appearance")]
                [DefaultValue(typeof(Color), "")]
                public Color ImageTransparentColor
                {
                    get { return _item.ImageTransparentColor; }
                    set { _item.ImageTransparentColor = value; }
                }
                #endregion

                #region Tag
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
            /// Tree node that is attached to a context menu item.
            /// </summary>
            protected class MenuTreeNode : TreeNode
            {
                #region Instance Fields
                private KryptonBreadCrumbItem _item;
                private object _propertyObject;
                #endregion

                #region Identity
                /// <summary>
                /// Initialize a new instance of the MenuTreeNode class.
                /// </summary>
                /// <param name="item">Menu item to represent.</param>
                public MenuTreeNode(KryptonBreadCrumbItem item)
                {
                    _item = item;
                    _propertyObject = item;

                    Text = _item.ToString();

                    // Hook into property changes
                    _item.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
                }
                #endregion

                #region Public
                /// <summary>
                /// Gets access to the associated item.
                /// </summary>
                public KryptonBreadCrumbItem Item
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
            private KryptonBreadCrumbItemsEditor _editor;
            private DictItemBase _beforeItems;
            private Button buttonOK;
            private TreeView treeView1;
            private Button buttonMoveUp;
            private Button buttonMoveDown;
            private Button buttonAddItem;
            private Button buttonDelete;
            private PropertyGrid propertyGrid1;
            private Label label1;
            private Label label2;
            private Button buttonAddChild;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the KryptonBreadCrumbItemsForm class.
            /// </summary>
            public KryptonBreadCrumbItemsForm(KryptonBreadCrumbItemsEditor editor)
                : base(editor)
            {
                _editor = editor;

                this.buttonOK = new System.Windows.Forms.Button();
                this.treeView1 = new System.Windows.Forms.TreeView();
                this.buttonMoveUp = new System.Windows.Forms.Button();
                this.buttonMoveDown = new System.Windows.Forms.Button();
                this.buttonAddItem = new System.Windows.Forms.Button();
                this.buttonDelete = new System.Windows.Forms.Button();
                this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
                this.label1 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.buttonAddChild = new System.Windows.Forms.Button();
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
                // treeView1
                // 
                this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.treeView1.Location = new System.Drawing.Point(12, 32);
                this.treeView1.Name = "treeView1";
                this.treeView1.Size = new System.Drawing.Size(254, 339);
                this.treeView1.TabIndex = 1;
                this.treeView1.HideSelection = false;
                this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
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
                this.buttonMoveUp.Text = "Move Up";
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
                this.buttonMoveDown.Text = "Move Down";
                this.buttonMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonMoveDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonMoveDown.UseVisualStyleBackColor = true;
                this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
                // 
                // buttonAddItem
                // 
                this.buttonAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddItem.Image = Properties.Resources.add;
                this.buttonAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddItem.Location = new System.Drawing.Point(272, 112);
                this.buttonAddItem.Name = "buttonAddItem";
                this.buttonAddItem.Size = new System.Drawing.Size(95, 28);
                this.buttonAddItem.TabIndex = 4;
                this.buttonAddItem.Text = "Add Sibling";
                this.buttonAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddItem.UseVisualStyleBackColor = true;
                this.buttonAddItem.Click += new System.EventHandler(this.buttonAddSibling_Click);
                // 
                // buttonDelete
                // 
                this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonDelete.Image = Properties.Resources.delete2;
                this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonDelete.Location = new System.Drawing.Point(272, 190);
                this.buttonDelete.Name = "buttonDelete";
                this.buttonDelete.Size = new System.Drawing.Size(95, 28);
                this.buttonDelete.TabIndex = 5;
                this.buttonDelete.Text = "Delete Item";
                this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonDelete.UseVisualStyleBackColor = true;
                this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
                // 
                // propertyGrid1
                // 
                this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.propertyGrid1.HelpVisible = false;
                this.propertyGrid1.Location = new System.Drawing.Point(373, 32);
                this.propertyGrid1.Name = "propertyGrid1";
                this.propertyGrid1.Size = new System.Drawing.Size(249, 339);
                this.propertyGrid1.TabIndex = 7;
                this.propertyGrid1.ToolbarVisible = false;
                // 
                // label1
                // 
                this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(370, 13);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(81, 13);
                this.label1.TabIndex = 6;
                this.label1.Text = "Item Properties";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(12, 13);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(142, 13);
                this.label2.TabIndex = 0;
                this.label2.Text = "BreadCrumbItems Collection";
                // 
                // buttonAddChild
                // 
                this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.buttonAddChild.Image = Properties.Resources.add;
                this.buttonAddChild.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddChild.Location = new System.Drawing.Point(272, 146);
                this.buttonAddChild.Name = "buttonAddChild";
                this.buttonAddChild.Size = new System.Drawing.Size(95, 28);
                this.buttonAddChild.TabIndex = 9;
                this.buttonAddChild.Text = "Add Child";
                this.buttonAddChild.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.buttonAddChild.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                this.buttonAddChild.UseVisualStyleBackColor = true;
                this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
                // 
                // KryptonBreadCrumbCollectionForm
                // 
                this.AcceptButton = this.buttonOK;
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(634, 414);
                this.ControlBox = false;
                this.Controls.Add(this.buttonAddChild);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.propertyGrid1);
                this.Controls.Add(this.buttonDelete);
                this.Controls.Add(this.buttonAddItem);
                this.Controls.Add(this.buttonMoveDown);
                this.Controls.Add(this.buttonMoveUp);
                this.Controls.Add(this.treeView1);
                this.Controls.Add(this.buttonOK);
                this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.MinimumSize = new System.Drawing.Size(501, 296);
                this.Name = "KryptonBreadCrumbCollectionForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "BreadCrumbItem Collection Editor";
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
                    treeView1.Nodes.Clear();
                    foreach (KryptonBreadCrumbItem item in Items)
                        AddMenuTreeNode(item, null);

                    // Expand to show all entries
                    treeView1.ExpandAll();

                    // Select the first node
                    if (treeView1.Nodes.Count > 0)
                        treeView1.SelectedNode = treeView1.Nodes[0];

                    UpdateButtons();
                    UpdatePropertyGrid();
                }
            }
            #endregion

            #region Implementation
            private void buttonOK_Click(object sender, EventArgs e)
            {
                // Create an array with all the root items
                object[] rootItems = new object[treeView1.Nodes.Count];
                for (int i = 0; i < rootItems.Length; i++)
                    rootItems[i] = ((MenuTreeNode)treeView1.Nodes[i]).Item;

                // Cache a lookup of all items after changes are made
                DictItemBase afterItems = CreateItemsDictionary(rootItems);

                // Update collection with new set of items
                Items = rootItems;

                // Clear down contents of tree as this form can be reused
                treeView1.Nodes.Clear();

                // Inform designer of changes in component items
                SynchronizeCollections(_beforeItems, afterItems, Context);

                // Notify container that the value has been changed
                Context.OnComponentChanged();
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

                bool found = false;
                return RecursiveFind(treeView1.Nodes, currentNode, ref found, true);
            }

            private TreeNode PreviousNode(TreeNode currentNode)
            {
                if (currentNode == null)
                    return null;

                bool found = false;
                return RecursiveFind(treeView1.Nodes, currentNode, ref found, false);
            }

            private TreeNode RecursiveFind(TreeNodeCollection nodes,
                                           TreeNode target,
                                           ref bool found,
                                           bool forward)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    TreeNode node = nodes[forward ? i : nodes.Count - 1 - i];

                    // Searching forward we check the node before any child collection
                    if (forward)
                    {
                        if (!found)
                            found |= (node == target);
                        else 
                            return node;
                    }

                    // Do not recurse into the children if looking forwards and at the target
                    if (!(found && forward))
                    {
                        // Searching the child collection of nodes
                        TreeNode findNode = RecursiveFind(node.Nodes, target, ref found, forward); 
                        
                        // If we found a node to return then return it now
                        if (findNode != null)
                            return findNode;
                        else if (found && (target != node))
                            return node;

                        // Searching backwards we check the child collection after checking the node
                        if (!forward)
                        {
                            if (!found)
                                found |= (node == target);
                            else 
                                return node;
                        }
                    }
                }

                return null;
            }

            private void buttonMoveUp_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView1.SelectedNode;
                if (node != null)
                {
                    // Find the previous node using the currently selected node
                    MenuTreeNode previousNode = (MenuTreeNode)PreviousNode(node);
                    if (previousNode != null)
                    {
                        // Is the current node contained inside the next node
                        bool contained = ContainsNode(previousNode, node);

                        // Remove cell from parent collection
                        MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                        TreeNodeCollection parentCollection = (node.Parent == null ? treeView1.Nodes : node.Parent.Nodes);
                        if (parentNode != null)
                            parentNode.Item.Items.Remove(node.Item);
                        parentCollection.Remove(node);

                        if (contained)
                        {
                            // Add cell to the parent of target node
                            MenuTreeNode previousParent = (MenuTreeNode)previousNode.Parent;
                            parentCollection = (previousNode.Parent == null ? treeView1.Nodes : previousNode.Parent.Nodes);
                            int pageIndex = parentCollection.IndexOf(previousNode);

                            // If the current and previous nodes are inside the same common node
                            if (!contained && ((previousParent != null) && (previousParent != parentNode)))
                            {
                                // If the page is the last one in the collection then we need to insert afterwards
                                if (pageIndex == (previousParent.Nodes.Count - 1))
                                    pageIndex++;
                            }

                            if (previousParent != null)
                                previousParent.Item.Items.Insert(pageIndex, node.Item);
                            parentCollection.Insert(pageIndex, node);
                        }
                        else
                        {
                            parentNode = (MenuTreeNode)previousNode;
                            parentNode.Item.Items.Insert(parentNode.Nodes.Count, node.Item);
                            parentNode.Nodes.Insert(parentNode.Nodes.Count, node);
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView1.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonMoveDown_Click(object sender, EventArgs e)
            {
                // If we have a selected node
                MenuTreeNode node = (MenuTreeNode)treeView1.SelectedNode;
                if (node != null)
                {
                    // Find the next node using the currently selected node
                    MenuTreeNode nextNode = (MenuTreeNode)NextNode(node);
                    if (nextNode != null)
                    {
                        // Is the current node contained inside the next node
                        bool contained = ContainsNode(nextNode, node);

                        // Remove cell from parent collection
                        MenuTreeNode parentNode = (MenuTreeNode)node.Parent;
                        TreeNodeCollection parentCollection = (node.Parent == null ? treeView1.Nodes : node.Parent.Nodes);
                        if (parentNode != null)
                            parentNode.Item.Items.Remove(node.Item);
                        parentCollection.Remove(node);

                        if (contained)
                        {
                            // Add cell to the parent sequence of target cell
                            MenuTreeNode previousParent = (MenuTreeNode)nextNode.Parent;
                            parentCollection = (nextNode.Parent == null ? treeView1.Nodes : nextNode.Parent.Nodes);
                            int pageIndex = parentCollection.IndexOf(nextNode);
                            if (previousParent != null)
                                previousParent.Item.Items.Insert(pageIndex + 1, node.Item);
                            parentCollection.Insert(pageIndex + 1, node);
                        }
                        else
                        {
                            parentNode = (MenuTreeNode)nextNode;
                            parentNode.Item.Items.Insert(0, node.Item);
                            parentNode.Nodes.Insert(0, node);
                        }
                    }
                }

                // Ensure the target node is still selected
                treeView1.SelectedNode = node;

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddSibling_Click(object sender, EventArgs e)
            {
                KryptonBreadCrumbItem item = (KryptonBreadCrumbItem)CreateInstance(typeof(KryptonBreadCrumbItem));
                TreeNode newNode = new MenuTreeNode(item);
                TreeNode selectedNode = treeView1.SelectedNode;

                // If there is no selection then append to root
                if (selectedNode == null)
                    treeView1.Nodes.Add(newNode);
                else
                {
                    // If current selection is at the root
                    TreeNode parentNode = selectedNode.Parent;
                    if (parentNode == null)
                        treeView1.Nodes.Insert(treeView1.Nodes.IndexOf(selectedNode) + 1, newNode);
                    else
                    {
                        MenuTreeNode parentMenu = (MenuTreeNode)parentNode;
                        parentMenu.Item.Items.Insert(parentNode.Nodes.IndexOf(selectedNode) + 1, item);
                        parentNode.Nodes.Insert(parentNode.Nodes.IndexOf(selectedNode) + 1, newNode);
                    }
                }

                // Select the newly added node
                if (newNode != null)
                {
                    treeView1.SelectedNode = newNode;
                    treeView1.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonAddChild_Click(object sender, EventArgs e)
            {
                KryptonBreadCrumbItem item = (KryptonBreadCrumbItem)CreateInstance(typeof(KryptonBreadCrumbItem));
                TreeNode newNode = new MenuTreeNode(item);
                TreeNode selectedNode = treeView1.SelectedNode;

                // If there is no selection then append to root
                if (selectedNode == null)
                    treeView1.Nodes.Add(newNode);
                else
                {
                    MenuTreeNode selectedMenu = (MenuTreeNode)selectedNode;
                    selectedMenu.Item.Items.Add(item);
                    selectedNode.Nodes.Add(newNode);
                }

                // Select the newly added node
                if (newNode != null)
                {
                    treeView1.SelectedNode = newNode;
                    treeView1.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void buttonDelete_Click(object sender, EventArgs e)
            {
                TreeNode node = treeView1.SelectedNode;

                // We should have a selected node!
                if (node != null)
                {
                    MenuTreeNode treeNode = node as MenuTreeNode;

                    // If at root level then remove from root, otherwise from the parent collection
                    if (node.Parent == null)
                        treeView1.Nodes.Remove(node);
                    else
                    {
                        TreeNode parentNode = node.Parent;
                        MenuTreeNode treeParentNode = parentNode as MenuTreeNode;
                        treeParentNode.Item.Items.Remove(treeNode.Item);
                        node.Parent.Nodes.Remove(node);
                    }

                    treeView1.Focus();
                }

                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
            {
                UpdateButtons();
                UpdatePropertyGrid();
            }

            private void UpdateButtons()
            {
                MenuTreeNode node = treeView1.SelectedNode as MenuTreeNode;
                buttonMoveUp.Enabled = (node != null) && (PreviousNode(node) != null);
                buttonMoveDown.Enabled = (node != null) && (NextNode(node) != null);
                buttonDelete.Enabled = (node != null);
            }

            private void UpdatePropertyGrid()
            {
                TreeNode node = treeView1.SelectedNode;
                if (node == null)
                    propertyGrid1.SelectedObject = null;
                else
                    propertyGrid1.SelectedObject = new CrumbProxy((KryptonBreadCrumbItem)((MenuTreeNode)node).PropertyObject);
            }

            private DictItemBase CreateItemsDictionary(object[] items)
            {
                DictItemBase dictItems = new DictItemBase();

                foreach (KryptonBreadCrumbItem item in items)
                    AddItemsToDictionary(dictItems, item);

                return dictItems;
            }

            private void AddItemsToDictionary(DictItemBase dictItems, KryptonBreadCrumbItem baseItem)
            {
                // Add item to the dictionary
                dictItems.Add(baseItem, baseItem);

                // Add children of an items collection
                foreach (KryptonBreadCrumbItem item in baseItem.Items)
                    AddItemsToDictionary(dictItems, item);
            }

            private void AddMenuTreeNode(KryptonBreadCrumbItem item, MenuTreeNode parent)
            {
                // Create a node to match the item
                MenuTreeNode node = new MenuTreeNode(item);

                // Add to either root or parent node
                if (parent != null)
                    parent.Nodes.Add(node);
                else
                    treeView1.Nodes.Add(node);

                // Add children of an items collection
                foreach (KryptonBreadCrumbItem child in item.Items)
                    AddMenuTreeNode(child, node);
            }

            private void SynchronizeCollections(DictItemBase before,
                                                DictItemBase after,
                                                ITypeDescriptorContext context)
            {
                // Add all new components (in the 'after' but not the 'before'
                foreach (KryptonBreadCrumbItem item in after.Values)
                    if (!before.ContainsKey(item))
                    {
                        if (context.Container != null)
                            context.Container.Add(item as IComponent);
                    }

                // Delete all old components (in the 'before' but not the 'after'
                foreach (KryptonBreadCrumbItem item in before.Values)
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
                    foreach (KryptonBreadCrumbItem item in after.Values)
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
        /// Initialize a new instance of the KryptonBreadCrumbItemsEditor class.
		/// </summary>
        public KryptonBreadCrumbItemsEditor()
            : base(typeof(KryptonBreadCrumbItem.BreadCrumbItems))
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
            return new KryptonBreadCrumbItemsForm(this);
        }
        #endregion
    }
}
