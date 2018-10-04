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
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// Represents an individual workspace cell.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonWorkspaceCell), "ToolboxBitmaps.KryptonWorkspaceCell.bmp")]
    [Designer("ComponentFactory.Krypton.Workspace.KryptonWorkspaceCellDesigner, ComponentFactory.Krypton.Workspace, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Pages")]
    public class KryptonWorkspaceCell : KryptonNavigator,
                                        IWorkspaceItem
    {
        #region Instance Fields
        private string _uniqueName;
        private StarSize _starSize;
        private IWorkspaceItem _parent;
        private ButtonSpecNavigator _maxamizeRestoreButton;
        private bool _disposeOnRemove;
        private bool _setVisible;
        private bool _allowResizing;
        private bool _events;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after a change has occured to the collection.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the user clicks the maximize/restore button.
        /// </summary>
        public event EventHandler MaximizeRestoreClicked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonWorkspaceCell class.
        /// </summary>
        public KryptonWorkspaceCell()
            : this("50*,50*")
        {
        }

        /// <summary>
        /// Initialise a new instance of the KryptonWorkspaceCell class.
        /// </summary>
        /// <param name="starSize">Initial star sizing value.</param>
        public KryptonWorkspaceCell(string starSize)
        {
            // Change Navigator defaults to workspace requirements
            AllowPageDrag = true;
            AllowTabFocus = false;

            // Initialize internal fields
            _disposeOnRemove = true;
            _setVisible = true;
            _starSize = new StarSize(starSize);
            _allowResizing = true;
            _uniqueName = CommonHelper.UniqueString;

            // We need to know when the set of pages has changed
            Pages.Cleared += new EventHandler(OnPagesChanged);
            Pages.Removed += new TypedHandler<KryptonPage>(OnPagesChanged);
            Pages.Inserted += new TypedHandler<KryptonPage>(OnPagesChanged);
            _events = true;

            // Add a button spec used to handle maximize/restore functionality
            _maxamizeRestoreButton = new ButtonSpecNavigator();
            _maxamizeRestoreButton.Type = PaletteButtonSpecStyle.WorkspaceMaximize;
            _maxamizeRestoreButton.Click += new EventHandler(OnMaximizeRestoreButtonClicked);
            Button.ButtonSpecs.Add(_maxamizeRestoreButton);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                // Must unhook to prevent memory leak
                _events = false;
                Pages.Cleared -= new EventHandler(OnPagesChanged);
                Pages.Removed -= new TypedHandler<KryptonPage>(OnPagesChanged);
                Pages.Inserted -= new TypedHandler<KryptonPage>(OnPagesChanged);

                // Must remove from parent workspace manually because the control collection is readonly
                if (Parent != null)
                {
                    KryptonReadOnlyControls controls = (KryptonReadOnlyControls)Parent.Controls;
                    controls.RemoveInternal(this);
                }

                base.Dispose(disposing);
            }
            catch { }
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the docking value of the cell.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get { return DockStyle.None; }
            
            set
            {
                // The cell must never have dock defined because that would interfere with 
                // layout that using the control Bounds to define its runtime location
            }
        }

        /// <summary>
        /// Gets and sets the control text.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the workspace item relative to the upper-left corner of its KryptonWorkspace. 
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        /// <summary>
        /// Gets or sets the height and width of the workspace item.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        /// <summary>
        /// Gets or sets the tab order of the workspace item within its KryptonWorkspace.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        /// <summary>
        /// Perform any compacting actions allowed by the flags.
        /// </summary>
        /// <param name="flags">Set of compacting actions allowed.</param>
        public void Compact(CompactFlags flags)
        {
            if (!DesignMode)
            {
            }
        }

        /// <summary>
        /// Should the item be displayed in the workspace.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WorkspaceVisible
        {
            get { return _setVisible; }
        }

        /// <summary>
        /// Gets and sets if the user can a separator to resize this workspace cell.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WorkspaceAllowResizing 
        {
            get { return _allowResizing; }
        }

        /// <summary>
        /// Current pixel size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspaceActualSize 
        {
            get { return Size; }
        }

        /// <summary>
        /// Current preferred size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspacePreferredSize
        {
            get 
            {
                if (IsDisposed)
                    return Size.Empty;
                else
                    return GetPreferredSize(Size.Empty);
            }
        }

        /// <summary>
        /// Get the required size in star notation.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public StarSize WorkspaceStarSize 
        {
            get { return _starSize; }
        }

        /// <summary>
        /// Get the defined minimum size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspaceMinSize 
        {
            get { return MinimumSize; }
        }

        /// <summary>
        /// Get the defined maximum size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size WorkspaceMaxSize
        {
            get { return MaximumSize; }
        }

        /// <summary>
        /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
        /// </summary>
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            
            set
            {
                if (!base.MinimumSize.Equals(value))
                {
                    base.MinimumSize = value;
                    OnPropertyChanged("MinimumSize");
                }
            }
        }

        /// <summary>
        /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
        /// </summary>
        public override Size MaximumSize
        {
            get { return base.MaximumSize; }

            set
            {
                if (!base.MaximumSize.Equals(value))
                {
                    base.MaximumSize = value;
                    OnPropertyChanged("MaximumSize");
                }
            }
        }

        /// <summary>
        /// Gets access to the parent workspace item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IWorkspaceItem WorkspaceParent
        {
            get { return _parent; }
            
            internal set 
            {
                if (_parent != value)
                {
                    _parent = value;
                    if (_parent != null)
                        AttachGlobalEvents();
                    else
                        UnattachGlobalEvents();
                }
            }
        }

        /// <summary>
        /// Gets and sets if the user can a separator to resize this workspace cell.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the user can a separator to resize this workspace cell.")]
        [DefaultValue(true)]
        public bool AllowResizing
        {
            get { return _allowResizing; }

            set
            {
                if (_allowResizing != value)
                {
                    _allowResizing = value;
                    OnPropertyChanged("AllowResizing");
                }
            }
        }

        /// <summary>
        /// Star notation the describes the sizing of the workspace item.
        /// </summary>
        [Category("Workspace")]
        [Description("Star notation for specifying the size of the item.")]
        [DefaultValue("50*,50*")]
        public string StarSize
        {
            get { return _starSize.Value; }

            set
            {
                _starSize.Value = value;
                OnPropertyChanged("StarSize");
            }
        }

        /// <summary>
        /// Should the item be disposed when it is removed from the workspace.
        /// </summary>
        [Category("Workspace")]
        [Description("Should the KryptonNavigator be Disposed when removed from KryptonWorkspace.")]
        [DefaultValue(true)]
        public virtual bool DisposeOnRemove
        {
            get { return _disposeOnRemove; }
            set { _disposeOnRemove = value; }
        }

        /// <summary>
        /// Gets and sets the unique name of the workspace cell.
        /// </summary>
        [Category("Appearance")]
        [Description("The unique name of the workspace cell.")]
        public string UniqueName
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _uniqueName; }

            [System.Diagnostics.DebuggerStepThrough]
            set { _uniqueName = value; }
        }


        /// <summary>
        /// Gets access to the maxmize/restore button spec.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonSpecNavigator MaximizeRestoreButton
        {
            get { return _maxamizeRestoreButton; }
        }

		/// <summary>
		/// Request this cell save its information.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace instance..</param>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        public void SaveToXml(KryptonWorkspace workspace, XmlWriter xmlWriter)
        {
            // Output cell values but not the actual customization of appearance
            xmlWriter.WriteStartElement("WC");
            workspace.WriteCellElement(xmlWriter, this);

            // Persist each child page in turn
            foreach (KryptonPage page in Pages)
            {
                // Are we allowed to save the page?
                if (page.AreFlagsSet(KryptonPageFlags.AllowConfigSave))
                {
                    xmlWriter.WriteStartElement("KP");
                    workspace.WritePageElement(xmlWriter, page);

                    // Give event handlers a chance to save custom data with the page
                    xmlWriter.WriteStartElement("CPD");
                    workspace.OnPageSaving(new PageSavingEventArgs(workspace, page, xmlWriter));
                    xmlWriter.WriteEndElement();

                    // Terminate the page element        
                    xmlWriter.WriteEndElement();
                }
            }

            // Terminate the cell element        
            xmlWriter.WriteEndElement();
        }

		/// <summary>
		/// Request this cell load and update state.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace instance.</param>
        /// <param name="xmlReader">Xml reader for loading information.</param>
        /// <param name="existingPages">Dictionary on existing pages before load.</param>
        public void LoadFromXml(KryptonWorkspace workspace, 
                                XmlReader xmlReader,
                                UniqueNameToPage existingPages)
        {
            // Load the cell details and return the unique name of the selected page for the cell
            string selectedPageUniqueName = workspace.ReadCellElement(xmlReader, this);
            KryptonPage selectedPage = null;

            // If the cell contains nothing then exit immediately
            if (!xmlReader.IsEmptyElement)
            {
                do
                {
                    // Read the next Element
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");

                    // Is this the end of the cell
                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                        break;

                    if (xmlReader.Name == "KP")
                    {
                        // Load the page details and optionally recreate the page
                        string uniqueName = CommonHelper.XmlAttributeToText(xmlReader, "UN");
                        KryptonPage page = workspace.ReadPageElement(xmlReader, uniqueName, existingPages);

                        if (xmlReader.Name != "CPD")
                            throw new ArgumentException("Expected 'CPD' element was not found");

                        bool finished = xmlReader.IsEmptyElement;

                        // Generate event so custom data can be loaded and/or the page to be added can be modified
                        PageLoadingEventArgs plea = new PageLoadingEventArgs(workspace, page, xmlReader);
                        workspace.OnPageLoading(plea);
                        page = plea.Page;

                        // Read everything until we get the end of custom data marker
                        while (!finished)
                        {
                            // Check it has the expected name
                            if (xmlReader.NodeType == XmlNodeType.EndElement)
                                finished = (xmlReader.Name == "CPD");

                            if (!finished)
                            {
                                if (!xmlReader.Read())
                                    throw new ArgumentException("An element was expected but could not be read in.");
                            }
                        }

                        // Read past the end of page element                    
                        if (!xmlReader.Read())
                            throw new ArgumentException("An element was expected but could not be read in.");

                        // Check it has the expected name
                        if (xmlReader.NodeType != XmlNodeType.EndElement)
                            throw new ArgumentException("End of 'KP' element expected but missing.");

                        // PageLoading event might have nulled the page value to prevent it being added
                        if (page != null)
                        {
                            // Remember the page that should become selected
                            if (!string.IsNullOrEmpty(page.UniqueName) && (page.UniqueName == selectedPageUniqueName))
                            {
                                // Can only selected a visible page
                                if (page.LastVisibleSet)
                                    selectedPage = page;
                            }

                            Pages.Add(page);
                        }
                    }
                    else
                        throw new ArgumentException("Unknown element was encountered.");
                }
                while (true);
            }

            // Did we find a matching page that should become selected?
            // (and we are allowed to have selected tabs)
            if ((selectedPage != null) && AllowTabSelect)
                SelectedPage = selectedPage;
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool LastVisibleSet
        {
            get { return _setVisible; }
            set { _setVisible = value; }
        }

        /// <summary>
        /// Output debug information about the workspace hierarchy.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DebugOutput(int indent)
        {
            Console.WriteLine("{0}Cell Count:{1} Visible:{1}", new string(' ', indent++ * 2), Pages.Count, LastVisibleSet);

            string prefix = new string(' ', indent * 2);
            foreach (KryptonPage page in Pages)
                Console.WriteLine("{0}Page Text:{1} Visible:{2} Type:{3}", prefix, page.Text, page.LastVisibleSet, page.GetType().Name);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Should the OnInitialized call perform layout.
        /// </summary>
        protected override bool LayoutOnInitialized
        {
            get { return false; }
        }

        /// <summary>
        /// Sets the control to the specified visible state. 
        /// </summary>
        /// <param name="value">true to make the control visible; otherwise, false.</param>
        protected override void SetVisibleCore(bool value)
        {
            if (_setVisible != value)
            {
                _setVisible = value;
                OnPropertyChanged("Visible");
            }

            base.SetVisibleCore(value);
        }

        /// <summary>
        /// Gets the child panel used for displaying actual pages.
        /// </summary>
        protected internal KryptonGroupPanel CellChildPanel
        {
            get { return ChildPanel; }
        }

        /// <summary>
        /// Called by the designer to hit test a point.
        /// </summary>
        /// <param name="pt">Point to be tested.</param>
        /// <returns>True if a hit otherwise false.</returns>
        protected internal bool CellDesignerGetHitTest(Point pt)
        {
            return DesignerGetHitTest(pt);
        }

        /// <summary>
        /// Called by the designer to get the component associated with the point.
        /// </summary>
        /// <param name="pt">Point to be tested.</param>
        /// <returns>Component associated with point or null.</returns>
        protected internal Component CellDesignerComponentFromPoint(Point pt)
        {
            return DesignerComponentFromPoint(pt);
        }

        /// <summary>
        /// Called by the designer to indicate that the mouse has left the control.
        /// </summary>
        protected internal void CellDesignerMouseLeave()
        {
            base.DesignerMouseLeave();
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion

        #region Implementation
        private void OnPagesChanged(object sender, EventArgs e)
        {
            // Need to raise property changed so that the owning workspace will layout as 
            // a change in pages might cause compacting to perform extra actions.
            if (_events)
                OnPropertyChanged("Pages");
        }

        private void OnMaximizeRestoreButtonClicked(object sender, EventArgs e)
        {
            if (MaximizeRestoreClicked != null)
                MaximizeRestoreClicked(this, EventArgs.Empty);
        }
        #endregion
    }

    /// <summary>
    /// Manages a list of KryptonWorkspaceCell instances.
    /// </summary>
    public class KryptonWorkspaceCellList : List<KryptonWorkspaceCell> {}
}
