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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Workspace
{
    internal class KryptonWorkspaceDesigner : ParentControlDesigner
    {
        #region Instance Fields
        private KryptonWorkspace _workspace;
        private IComponentChangeService _changeService;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate with the designer.</param>
        public override void Initialize(IComponent component)
        {
            // Validate the parameter reference
            if (component == null) throw new ArgumentNullException("component");

            // Let base class do standard stuff
            base.Initialize(component);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Remember the actual control being designed
            _workspace = (KryptonWorkspace)component;

            // Get access to the services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                ArrayList compound = new ArrayList();

                if (_workspace != null)
                    compound.AddRange(_workspace.Root.Children);

                return compound;
            }
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new DesignerActionListCollection();

                // Add the navigator specific list
                actionLists.Add(new KryptonWorkspaceActionList(this));

                return actionLists;
            }
        }

        /// <summary>
        /// Indicates whether the specified control can be a child of the control managed by a designer.
        /// </summary>
        /// <param name="control">The Control to test.</param>
        /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
        public override bool CanParent(Control control)
        {
            return false;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            _changeService.ComponentRemoving -= new ComponentEventHandler(OnComponentRemoving);

            // Ensure base class is always disposed
            base.Dispose(disposing);
        }

        /// <summary>
        /// Called when a drag-and-drop operation enters the control designer view.
        /// </summary>
        /// <param name="de">A DragEventArgs that provides data for the event.</param>
        protected override void OnDragEnter(DragEventArgs de)
        {
            de.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Called when a drag-and-drop object is dragged over the control designer view.
        /// </summary>
        /// <param name="de">A DragEventArgs that provides data for the event.</param>
        protected override void OnDragOver(DragEventArgs de)
        {
            de.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Called when a drag-and-drop object is dropped onto the control designer view.
        /// </summary>
        /// <param name="de">A DragEventArgs that provides data for the event.</param>
        protected override void OnDragDrop(DragEventArgs de)
        {
            de.Effect = DragDropEffects.None;
        }
        #endregion

        #region Implementation
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our workspace is being removed
            if (e.Component == _workspace)
            {
                // Prevent layout being performed during removal of children otherwise the layout
                // code will cause the controls to be added back before they are actually destroyed
                _workspace.SuspendLayout();

                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
                
                // We need to remove all children from the workspace
                for (int i = _workspace.Root.Children.Count - 1; i >= 0; i--)
                {
                    Component comp = _workspace.Root.Children[i] as Component;

                    // If the component is a control...
                    if (comp is Control)
                    {
                        // We need to manually remove it from the workspace controls collection
                        KryptonReadOnlyControls readOnlyControls = (KryptonReadOnlyControls)_workspace.Controls;
                        readOnlyControls.RemoveInternal(comp as Control);
                    }

                    host.DestroyComponent(comp);

                    // Must remove the child after it has been destroyed otherwise the component destroy method 
                    // will not be able to climb the sequence chain to find the parent workspace instance
                    _workspace.Root.Children.Remove(comp);
                }

                _workspace.ResumeLayout();
            }
        }
        #endregion
    }
}
