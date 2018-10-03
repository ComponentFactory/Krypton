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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonGroupDesigner : ParentControlDesigner
	{
        #region Instance Fields
        private KryptonGroup _group;
        private IDesignerHost _designerHost;
        #endregion

        #region Public Overrides
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
            _group = component as KryptonGroup;

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;

            // Acquire service interfaces
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

            // Let the internal panel in the container be designable
            if (_group != null)
                EnableDesignMode(_group.Panel, "Panel");
        }

        /// <summary>
        /// Indicates whether the specified control can be a child of the control managed by a designer.
        /// </summary>
        /// <param name="control">The Control to test.</param>
        /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
        public override bool CanParent(Control control)
        {
            // We never allow anything to be added to the group
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
            if ((internalControlIndex == 0) && (_group != null))
                return (ControlDesigner)_designerHost.GetDesigner(_group.Panel);
            else
                return null;
        }

        /// <summary>
        /// Returns the number of internal control designers in the ControlDesigner.
        /// </summary>
        /// <returns>The number of internal control designers in the ControlDesigner.</returns>
        public override int NumberOfInternalControlDesigners()
        {
            if (_group != null)
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

                // Add the group specific list
                actionLists.Add(new KryptonGroupActionList(this));

                return actionLists;
            }
        }
        #endregion
    }
}
