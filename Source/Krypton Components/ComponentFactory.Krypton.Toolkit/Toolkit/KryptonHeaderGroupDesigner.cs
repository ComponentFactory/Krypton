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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonHeaderGroupDesigner : ParentControlDesigner
    {
        #region Instance Fields
        private bool _lastHitTest;
        private KryptonHeaderGroup _headerGroup;
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private ISelectionService _selectionService;
        #endregion

        #region Protected
        /// <summary>
        /// Releases all resources used by the component. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_headerGroup != null)
            {
                // Unhook from events
                _headerGroup.GetViewManager().MouseUpProcessed -= new MouseEventHandler(OnHeaderGroupMouseUp);
                _headerGroup.GetViewManager().DoubleClickProcessed -= new PointHandler(OnHeaderGroupDoubleClick);
            }

            _changeService.ComponentRemoving -= new ComponentEventHandler(OnComponentRemoving);

            // Must let base class do standard stuff
            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            Debug.Assert(component != null);

            // Validate the parameter reference
            if (component == null) throw new ArgumentNullException("component");

            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _headerGroup = component as KryptonHeaderGroup;

            if (_headerGroup != null)
            {
                // Hook into header event
                _headerGroup.GetViewManager().MouseUpProcessed += new MouseEventHandler(OnHeaderGroupMouseUp);
                _headerGroup.GetViewManager().DoubleClickProcessed += new PointHandler(OnHeaderGroupDoubleClick);
            }

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Acquire service interfaces
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));

            // We need to know when we are being removed
            _changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);

            // Let the internal panel in the container be designable
            if (_headerGroup != null)
                EnableDesignMode(_headerGroup.Panel, "Panel");
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get 
            {
                // Get the set of components from the base class
                ICollection baseComponents = base.AssociatedComponents;

                // If no button specs then nothing more to do
                if ((_headerGroup == null) || (_headerGroup.ButtonSpecs.Count == 0))
                    return baseComponents;
                else
                {
                    // Create a new collection for both values
                    ArrayList compound = new ArrayList(baseComponents);

                    // Add all the button specs to the end
                    compound.AddRange(_headerGroup.ButtonSpecs);

                    return compound;
                }
            }
        }

        /// <summary>
        /// Indicates whether the specified control can be a child of the control managed by a designer.
        /// </summary>
        /// <param name="control">The Control to test.</param>
        /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
        public override bool CanParent(Control control)
        {
            // We never allow anything to be added to the header group
            return false;
        }

        /// <summary>
        /// Returns the internal control designer with the specified index in the ControlDesigner.
        /// </summary>
        /// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
        /// <returns>A ControlDesigner at the specified index.</returns>
        public override ControlDesigner InternalControlDesigner(int internalControlIndex)
        {
            // Get the control designer for the requested indexed child control
            if ((_headerGroup != null) && (internalControlIndex == 0))
                return (ControlDesigner)_designerHost.GetDesigner(_headerGroup.Panel);
            else
                return null;
        }

        /// <summary>
        /// Returns the number of internal control designers in the ControlDesigner.
        /// </summary>
        /// <returns>The number of internal control designers in the ControlDesigner.</returns>
        public override int NumberOfInternalControlDesigners()
        {
            if (_headerGroup != null)
                return 1;
            else
                return 0;
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

                // Add the header group specific list
                actionLists.Add(new KryptonHeaderGroupActionList(this));

                return actionLists;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Indicates whether a mouse click at the specified point should be handled by the control.
        /// </summary>
        /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
        /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
        protected override bool GetHitTest(Point point)
        {
            if (_headerGroup != null)
            {
                // Ask the control if it wants to process the point
                bool ret = _headerGroup.DesignerGetHitTest(_headerGroup.PointToClient(point));

                // If the navigator does not want the mouse point then make sure the 
                // tracking element is informed that the mouse has left the control
                if (!ret && _lastHitTest)
                    _headerGroup.DesignerMouseLeave();

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
            if (_headerGroup != null)
                _headerGroup.DesignerMouseLeave();

            base.OnMouseLeave();
        }        
        #endregion

        #region Implementation
        private void OnHeaderGroupMouseUp(object sender, MouseEventArgs e)
        {
            if ((_headerGroup != null) && (e.Button == MouseButtons.Left))
            {
                // Get any component associated with the current mouse position
                Component component = _headerGroup.DesignerComponentFromPoint(new Point(e.X, e.Y));

                if (component != null)
                {
                    // Force the layout to be update for any change in selection
                    _headerGroup.PerformLayout();

                    // Select the component
                    ArrayList selectionList = new ArrayList();
                    selectionList.Add(component);
                    _selectionService.SetSelectedComponents(selectionList, SelectionTypes.Auto);
                }
            }
        }

        private void OnHeaderGroupDoubleClick(object sender, Point pt)
        {
            // Get any component associated with the current mouse position
            Component component = _headerGroup.DesignerComponentFromPoint(pt);

            if (component != null)
            {
                // Get the designer for the component
                IDesigner designer = _designerHost.GetDesigner(component);

                // Request code for the default event be generated
                designer.DoDefaultAction();
            }
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if (e.Component == _headerGroup)
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _headerGroup.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _headerGroup.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_headerGroup, null);

                    // Perform actual removal of button spec from header
                    _headerGroup.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_headerGroup, null, null, null);
                }
            }
        }
        #endregion
    }
}
