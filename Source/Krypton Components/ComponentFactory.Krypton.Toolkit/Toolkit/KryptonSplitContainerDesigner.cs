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
using System.Windows.Forms.Design.Behavior;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	internal class KryptonSplitContainerDesigner : ParentControlDesigner
    {
        #region Instance Fields
        private KryptonSplitContainer _splitContainer;
        private IDesignerHost _designerHost;
        private ISelectionService _selectionService;
        private BehaviorService _behaviorService;
        private Adorner _adorner;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate with the designer.</param>
        public override void Initialize(IComponent component)
        {
            Debug.Assert(component != null);

            // Validate the parameter reference
            if (component == null) throw new ArgumentNullException("component");

            // Let base class do standard stuff
            base.Initialize(component);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Acquire service interfaces
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
            _behaviorService = (BehaviorService)GetService(typeof(BehaviorService));

            // Remember the actual control being designed
            _splitContainer = component as KryptonSplitContainer;

            // Create a new adorner and add our splitter glyph
            _adorner = new Adorner();
            _adorner.Glyphs.Add(new KryptonSplitContainerGlyph(_selectionService, _behaviorService, _adorner, this));
            _behaviorService.Adorners.Add(_adorner);

            // Let the two panels in the container be designable
            if (_splitContainer != null)
            {
                EnableDesignMode(_splitContainer.Panel1, "Panel1");
                EnableDesignMode(_splitContainer.Panel2, "Panel2");
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Remove adorners
                    if (_behaviorService != null)
                        _behaviorService.Adorners.Remove(_adorner);
                }
            }
            finally
            {
                // Ensure base class is always disposed
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Indicates whether the specified control can be a child of the control managed by a designer.
        /// </summary>
        /// <param name="control">The Control to test.</param>
        /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
        public override bool CanParent(Control control)
        {
            // We never allow anything to be added to the split container
            return false;
        }

        /// <summary>
        /// Returns the internal control designer with the specified index in the ControlDesigner.
        /// </summary>
        /// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
        /// <returns>A ControlDesigner at the specified index.</returns>
        public override ControlDesigner InternalControlDesigner(int internalControlIndex)
        {
            if (_splitContainer != null)
            {
                // Get the control designer for the requested indexed child control
                if (internalControlIndex == 0)
                    return (ControlDesigner)_designerHost.GetDesigner(_splitContainer.Panel1);
                else if (internalControlIndex == 1)
                    return (ControlDesigner)_designerHost.GetDesigner(_splitContainer.Panel2);
            }

            return null;
        }

        /// <summary>
        /// Returns the number of internal control designers in the ControlDesigner.
        /// </summary>
        /// <returns>The number of internal control designers in the ControlDesigner.</returns>
        public override int NumberOfInternalControlDesigners()
        {
            if (_splitContainer != null)
                return 2;
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

                // Add the orientation list
                actionLists.Add(new KryptonSplitContainerActionList(this));

                return actionLists;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the DragEnter event.
        /// </summary>
        /// <param name="de">A DragEventArgs that contains the event data.</param>
        protected override void OnDragEnter(DragEventArgs de)
        {
            // Prevent user dragging a toolbox control onto the control
            de.Effect = DragDropEffects.None;
        }
        #endregion
    }
}
