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
using System.Windows.Forms.Design.Behavior;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    internal class KryptonPageDesigner : ScrollableControlDesigner,
                                         IKryptonDesignerSelect
    {
        #region Instance Fields
        private KryptonPage _page;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _verbEditFlags;
        private ISelectionService _selectionService;
        private IComponentChangeService _changeService;
        #endregion

        #region Public
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate with the designer.</param>
        public override void Initialize(IComponent component)
        {
            // Perform common base class initializating
            base.Initialize(component);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Remember references to components involved in design
            _page = (KryptonPage)component;

            // Hook into page events
            _page.FlagsChanged += new KryptonPageFlagsEventHandler(OnPageFlagsChanged);

            // Acquire service interfaces
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed
            _changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);

            // Lock the component from user size/location change
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)["Locked"];
            if ((descriptor != null) && (ParentNavigator != null))
                descriptor.SetValue(component, true);
        }

        /// <summary>
        /// Indicates if this designer's control can be parented by the control of the specified designer. 
        /// </summary>
        /// <param name="parentDesigner">The IDesigner that manages the control to check.</param>
        /// <returns>true if the control managed by the specified designer can parent the control managed by this designer; otherwise, false.</returns>
        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            // Can only place a KrytonPage in the KryptonNavigator
            if (parentDesigner != null)
                return (parentDesigner.Component is KryptonNavigator);
            else
                return false;
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                if (_page != null)
                    return _page.ButtonSpecs;
                else
                    return base.AssociatedComponents;
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
                actionLists.Add(new KryptonPageActionList(this));

                return actionLists;
            }
        }

        /// <summary>
        /// Gets the design-time verbs supported by the component that is associated with the designer.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                // First time around we create the verb collection
                if (_verbs == null)
                {
                    // Cache verb instances so enabled state can be updated in future
                    _verbEditFlags = new DesignerVerb("Edit Flags", new EventHandler(OnEditFlags));
                    _verbs = new DesignerVerbCollection(new DesignerVerb[] { _verbEditFlags });
                }

                return _verbs;
            }
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get 
            { 
                // If inside a navigator then prevent resizing of the page
                if (ParentNavigator != null)
                    return (SelectionRules.None | SelectionRules.Locked);
                else
                    return SelectionRules.None;
            }
        }

        /// <summary>
        /// Should painting be performed for the selection glyph.
        /// </summary>
        public bool CanPaint
        {
            get 
            {
                // Only draw the glyph for the selected page
                if (ParentNavigator != null)
                    return (ParentNavigator.SelectedPage == _page);
                else
                    return false;
            }
        }

        /// <summary>
        /// Select the control that contains the group panel.
        /// </summary>
        public void SelectParentControl()
        {
            if (ParentNavigator != null)
                _selectionService.SetSelectedComponents(new object[] { ParentNavigator }, SelectionTypes.Primary);
            else if (_page.Parent != null)
                _selectionService.SetSelectedComponents(new object[] { _page.Parent }, SelectionTypes.Primary);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Releases the resources used by the KryptonSplitterPanelDesigner. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Remove event hooks
                    _page.FlagsChanged -= new KryptonPageFlagsEventHandler(OnPageFlagsChanged);
                }
            }
            finally
            {
                // Ensure base class is always disposed
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Receives a call when the control that the designer is managing has painted its surface so the designer can paint any additional adornments on top of the control.
        /// </summary>
        /// <param name="pe">A PaintEventArgs the designer can use to draw on the control.</param>
        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            // Let base class paint first
            base.OnPaintAdornments(pe);

            // Always draw a border around the panel
            DrawBorder(pe.Graphics);
        }
        #endregion

        #region Implementation
        private void OnEditFlags(object sender, EventArgs e)
        {
            KryptonPageFormEditFlags editFlags = new KryptonPageFormEditFlags(_page);
            editFlags.ShowDialog();
        }

        private void OnPageFlagsChanged(object sender, KryptonPageFlagsEventArgs e)
        {
            // Get access to the Flags property
            MemberDescriptor propertyFlags = TypeDescriptor.GetProperties(_page)["Flags"];

            // Notify that the flags property has been updated
            RaiseComponentChanging(propertyFlags);
            RaiseComponentChanged(propertyFlags, null, null);
        }

        private KryptonNavigator ParentNavigator
        {
            get
            {
                Control parent = _page.Parent;

                // Search parent chain looking for navigator instance
                while(parent != null)
                {
                    if (parent is KryptonNavigator)
                        return (KryptonNavigator)parent;

                    parent = parent.Parent;
                }

                return null;
            }
        }

        private void DrawBorder(Graphics graphics)
        {
            // Create a pen for drawing
            using (Pen borderPen = new Pen(SystemColors.ControlDarkDark))
            {
                // Always draw the border dashed
                borderPen.DashStyle = DashStyle.Dash;

                // Get the client rectangle
                Rectangle clientRect = Control.ClientRectangle;

                // Reduce by 1 in width and height
                clientRect.Width--;
                clientRect.Height--;

                // Reduce so border is clearly inside the edge of the page
                clientRect.Inflate(-1, -1);

                // Perform actual draw
                graphics.DrawRectangle(borderPen, clientRect);
            }
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_page != null) && (e.Component == _page))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _page.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _page.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_page, null);

                    // Perform actual removal of button spec from page
                    _page.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_page, null, null, null);
                }
            }
        }
        #endregion
    }
}
