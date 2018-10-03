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
    internal class KryptonTextBoxDesigner : ControlDesigner
	{
        #region Instance Fields
        private bool _lastHitTest;
        private KryptonTextBox _textBox;
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
            // Let base class do standard stuff
            base.Initialize(component);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Cast to correct type
            _textBox = component as KryptonTextBox;

            if (_textBox != null)
            {
                // Hook into textbox events
                _textBox.GetViewManager().MouseUpProcessed += new MouseEventHandler(OnTextBoxMouseUp);
                _textBox.GetViewManager().DoubleClickProcessed += new PointHandler(OnTextBoxDoubleClick);
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
                if (_textBox != null)
                    return _textBox.ButtonSpecs;
                else
                    return base.AssociatedComponents;
            }
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                // Start with all edges being sizeable
                SelectionRules rules = base.SelectionRules;

                // Get access to the actual control instance
                KryptonTextBox textBox = (KryptonTextBox)Component;

                // With multiline and autosize we prevent the user changing the height
                if (!textBox.Multiline && textBox.AutoSize)
                    rules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);

                return rules;
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

                // Add the label specific list
                actionLists.Add(new KryptonTextBoxActionList(this));

                return actionLists;
            }
        }

        /// <summary>
        /// Indicates whether a mouse click at the specified point should be handled by the control.
        /// </summary>
        /// <param name="point">A Point indicating the position at which the mouse was clicked, in screen coordinates.</param>
        /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
        protected override bool GetHitTest(Point point)
        {
            if (_textBox != null)
            {
                // Ask the control if it wants to process the point
                bool ret = _textBox.DesignerGetHitTest(_textBox.PointToClient(point));

                // If the navigator does not want the mouse point then make sure the 
                // tracking element is informed that the mouse has left the control
                if (!ret && _lastHitTest)
                    _textBox.DesignerMouseLeave();

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
            if (_textBox != null)
                _textBox.DesignerMouseLeave();

            base.OnMouseLeave();
        }
        #endregion

        #region Implementation
        private void OnTextBoxMouseUp(object sender, MouseEventArgs e)
        {
            if ((_textBox != null) && (e.Button == MouseButtons.Left))
            {
                // Get any component associated with the current mouse position
                Component component = _textBox.DesignerComponentFromPoint(new Point(e.X, e.Y));

                if (component != null)
                {
                    // Force the layout to be update for any change in selection
                    _textBox.PerformLayout();

                    // Select the component
                    ArrayList selectionList = new ArrayList();
                    selectionList.Add(component);
                    _selectionService.SetSelectedComponents(selectionList, SelectionTypes.Auto);
                }
            }
        }

        private void OnTextBoxDoubleClick(object sender, Point pt)
        {
            if (_textBox != null)
            {
                // Get any component associated with the current mouse position
                Component component = _textBox.DesignerComponentFromPoint(pt);

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
            if ((_textBox != null) && (e.Component == _textBox))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _textBox.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _textBox.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_textBox, null);

                    // Perform actual removal of button spec from textbox
                    _textBox.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_textBox, null, null, null);
                }
            }
        }
        #endregion
    }
}
