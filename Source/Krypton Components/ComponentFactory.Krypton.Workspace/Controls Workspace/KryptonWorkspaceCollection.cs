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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// Collection of workspace items.
    /// </summary>
    public class KryptonWorkspaceCollection : TypedRestrictCollection<Component>
    {
        #region Instance Fields
        private KryptonWorkspaceSequence _sequence;
        #endregion

        #region Static Fields
        private static readonly Type[] _types = new Type[] { typeof(KryptonWorkspaceCell),
                                                             typeof(KryptonWorkspaceSequence)};
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
        /// Initialize a new instance of the KryptonWorkspaceCollection class.
        /// </summary>
        /// <param name="sequence">Reference to the owning sequence.</param>
        public KryptonWorkspaceCollection(KryptonWorkspaceSequence sequence)
        {
            _sequence = sequence;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            return Count.ToString() + " Children";
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets an array of types that the collection is restricted to contain.
        /// </summary>
        public override Type[] RestrictTypes
        {
            get { return _types; }
        }

        /// <summary>
        /// Gets a value indicating if the collection or child collections contains a cell instance.
        /// </summary>
        public bool ContainsVisibleCell
        {
            get
            {
                foreach (Component c in this)
                {
                    // If we have a cell and that cell wants to be visible then we are done
                    KryptonWorkspaceCell cell = c as KryptonWorkspaceCell;
                    if ((cell != null) && cell.WorkspaceVisible)
                        return true;
                    else
                    {
                        // If we have a sequence and it is visible and contains a visible cell then we are done
                        KryptonWorkspaceSequence sequence = c as KryptonWorkspaceSequence;
                        if ((sequence != null) && sequence.WorkspaceVisible && sequence.Children.ContainsVisibleCell)
                            return true;
                    }
                }

                return false;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Inserted event.
        /// </summary>
        /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
        protected override void OnInserted(TypedCollectionEventArgs<Component> e)
        {
            base.OnInserted(e);
            
            if (e.Item is IWorkspaceItem)
            {
                IWorkspaceItem workspaceItem = e.Item as IWorkspaceItem;
                workspaceItem.PropertyChanged += new PropertyChangedEventHandler(OnChildPropertyChanged);
                workspaceItem.MaximizeRestoreClicked += new EventHandler(OnChildMaximizeRestoreClicked);
            }

            if (e.Item is KryptonWorkspaceCell)
                ((KryptonWorkspaceCell)e.Item).WorkspaceParent = _sequence;

            if (e.Item is KryptonWorkspaceSequence)
                ((KryptonWorkspaceSequence)e.Item).WorkspaceParent = _sequence;

            OnPropertyChanged("Children");
        }

        /// <summary>
        /// Raises the Removed event.
        /// </summary>
        /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
        protected override void OnRemoved(TypedCollectionEventArgs<Component> e)
        {
            base.OnRemoved(e);

            if (e.Item is IWorkspaceItem)
            {
                IWorkspaceItem workspaceItem = e.Item as IWorkspaceItem;
                workspaceItem.PropertyChanged -= new PropertyChangedEventHandler(OnChildPropertyChanged);
                workspaceItem.MaximizeRestoreClicked -= new EventHandler(OnChildMaximizeRestoreClicked);
            }

            if (e.Item is KryptonWorkspaceCell)
                ((KryptonWorkspaceCell)e.Item).WorkspaceParent = null;

            if (e.Item is KryptonWorkspaceSequence)
                ((KryptonWorkspaceSequence)e.Item).WorkspaceParent = null;

            OnPropertyChanged("Children");
        }

        /// <summary>
        /// Raises the Clearing event.
        /// </summary>
        /// <param name="e">An EventArgs instance containing event data.</param>
        protected override void OnClearing(EventArgs e)
        {
            base.OnClearing(e);

            // Unhook from monitoring the child items
            foreach (Component c in this)
            {
                if (c is IWorkspaceItem)
                {
                    IWorkspaceItem workspaceItem = c as IWorkspaceItem;
                    workspaceItem.PropertyChanged -= new PropertyChangedEventHandler(OnChildPropertyChanged);
                    workspaceItem.MaximizeRestoreClicked -= new EventHandler(OnChildMaximizeRestoreClicked);
                }

                if (c is KryptonWorkspaceCell)
                    ((KryptonWorkspaceCell)c).WorkspaceParent = null;

                if (c is KryptonWorkspaceSequence)
                    ((KryptonWorkspaceSequence)c).WorkspaceParent = null;
            }
        }

        /// <summary>
        /// Raises the Cleared event.
        /// </summary>
        /// <param name="e">An EventArgs instance containing event data.</param>
        protected override void OnCleared(EventArgs e)
        {
            base.OnCleared(e);
            OnPropertyChanged("Children");
        }

        /// <summary>
        /// Handle a change in a child item.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments associated with the event.</param>
        protected void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        /// <summary>
        /// Handle a maximize/restore request from a child item.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event arguments associated with the event.</param>
        protected void OnChildMaximizeRestoreClicked(object sender, EventArgs e)
        {
            if (MaximizeRestoreClicked != null)
                MaximizeRestoreClicked(sender, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="property">Name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments associated with the event.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion
    }
}
