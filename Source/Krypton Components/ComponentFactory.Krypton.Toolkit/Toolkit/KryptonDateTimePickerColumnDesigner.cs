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
    internal class KryptonDateTimePickerColumnDesigner : ComponentDesigner
	{
        #region Instance Fields
        private KryptonDataGridViewDateTimePickerColumn _dateTimePicker;
        private IComponentChangeService _changeService;
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

            // Cast to correct type
            _dateTimePicker = component as KryptonDataGridViewDateTimePickerColumn;

            // Get access to the design services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                if (_dateTimePicker != null)
                    return _dateTimePicker.ButtonSpecs;
                else
                    return base.AssociatedComponents;
            }
        }
        #endregion

        #region Private
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_dateTimePicker != null) && (e.Component == _dateTimePicker))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _dateTimePicker.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _dateTimePicker.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_dateTimePicker, null);

                    // Perform actual removal of button spec from textbox
                    _dateTimePicker.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_dateTimePicker, null, null, null);
                }
            }
        }
        #endregion
    }
}
