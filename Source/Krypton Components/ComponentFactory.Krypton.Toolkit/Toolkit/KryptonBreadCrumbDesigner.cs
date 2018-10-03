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
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonBreadCrumbDesigner : ControlDesigner
    {
        #region Instance Fields
        private bool _lastHitTest;
        private KryptonBreadCrumb _breadCrumb;
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private ISelectionService _selectionService;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
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

            // Cast to correct type
            _breadCrumb = component as KryptonBreadCrumb;

            if (_breadCrumb != null)
            {
                // Hook into bread crumb events
                _breadCrumb.GetViewManager().MouseUpProcessed += new MouseEventHandler(OnBreadCrumbMouseUp);
                _breadCrumb.GetViewManager().DoubleClickProcessed += new PointHandler(OnBreadCrumbDoubleClick);
            }

            // Get access to the design services
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));

            // We need to know when we are being removed
            _changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents 
        {
            get 
            {
                ArrayList compound = new ArrayList(base.AssociatedComponents);

                if (_breadCrumb != null)
                {
                    compound.AddRange(_breadCrumb.ButtonSpecs);
                    compound.AddRange(_breadCrumb.RootItem.Items);
                }

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

                // Add the bread crumb specific list
                actionLists.Add(new KryptonBreadCrumbActionList(this));

                return actionLists;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Releases all resources used by the component. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            // Unhook from events
            if (_breadCrumb != null)
            {
                _breadCrumb.GetViewManager().MouseUpProcessed -= new MouseEventHandler(OnBreadCrumbMouseUp);
                _breadCrumb.GetViewManager().DoubleClickProcessed -= new PointHandler(OnBreadCrumbDoubleClick);
            }

            _changeService.ComponentRemoving -= new ComponentEventHandler(OnComponentRemoving);

            // Must let base class do standard stuff
            base.Dispose(disposing);
        }

        /// <summary>
        /// Indicates whether a mouse click at the specified point should be handled by the control.
        /// </summary>
        /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
        /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
        protected override bool GetHitTest(Point point)
        {
            if (_breadCrumb != null)
            {
                // Ask the control if it wants to process the point
                bool ret = _breadCrumb.DesignerGetHitTest(_breadCrumb.PointToClient(point));

                // If the navigator does not want the mouse point then make sure the 
                // tracking element is informed that the mouse has left the control
                if (!ret && _lastHitTest)
                    _breadCrumb.DesignerMouseLeave();

                // Cache the last answer recovered
                _lastHitTest = ret;

                return ret;
            }
            else
                return false;
        }

        /// <summary>
        /// Receives a call when the mouse leaves the control. 
        /// </summary>
        protected override void OnMouseLeave()
        {
            if (_breadCrumb != null)
                _breadCrumb.DesignerMouseLeave();

            base.OnMouseLeave();
        }
        #endregion

        #region Implementation
        private void OnBreadCrumbMouseUp(object sender, MouseEventArgs e)
        {
            if ((_breadCrumb != null) && (e.Button == MouseButtons.Left))
            {
                // Get any component associated with the current mouse position
                Component component = _breadCrumb.DesignerComponentFromPoint(new Point(e.X, e.Y));

                if (component != null)
                {
                    // Force the layout to be update for any change in selection
                    _breadCrumb.PerformLayout();

                    // Select the component
                    ArrayList selectionList = new ArrayList();
                    selectionList.Add(component);
                    _selectionService.SetSelectedComponents(selectionList, SelectionTypes.Auto);
                }
            }
        }

        private void OnBreadCrumbDoubleClick(object sender, Point pt)
        {
            if (_breadCrumb != null)
            {
                // Get any component associated with the current mouse position
                Component component = _breadCrumb.DesignerComponentFromPoint(pt);

                if (component != null)
                {
                    // Get the designer for the component
                    IDesigner designer = _designerHost.GetDesigner(component);

                    // Request code for the default event be generated
                    designer.DoDefaultAction();
                }
            }
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_breadCrumb != null) && (e.Component == _breadCrumb))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _breadCrumb.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _breadCrumb.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_breadCrumb, null);

                    // Perform actual removal of button spec from bread crumb
                    _breadCrumb.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_breadCrumb, null, null, null);
                }
            }
        }
        #endregion
    }
}
