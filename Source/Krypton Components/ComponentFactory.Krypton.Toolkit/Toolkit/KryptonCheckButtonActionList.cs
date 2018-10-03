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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonCheckButtonActionList : KryptonButtonActionList
    {
        #region Instance Fields
        private KryptonCheckButton _checkButton;
        private IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCheckButtonActionList(KryptonCheckButtonDesigner owner) 
            : base(owner)
        {
            // Remember the button instance
            _checkButton = owner.Component as KryptonCheckButton;

            // Assuming we were correctly passed an actual component...
            if (_checkButton != null)
            {
                // Get access to the actual Orientation propertry
                PropertyDescriptor checkedProp = TypeDescriptor.GetProperties(_checkButton)["Checked"];

                // If we succeeded in getting the property
                if (checkedProp != null)
                {
                    // Decide on the next action to take given the current setting
                    if ((bool)checkedProp.GetValue(_checkButton))
                        _action = "Uncheck the button";
                    else
                        _action = "Check the button";
                }
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the checked state.
        /// </summary>
        public bool Checked
        {
            get { return _checkButton.Checked; }

            set
            {
                if (_checkButton.Checked != value)
                {
                    _service.OnComponentChanged(_checkButton, null, _checkButton.Checked, value);
                    _checkButton.Checked = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_checkButton != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, new EventHandler(OnCheckedClick)), "Appearance"));
                actions.Add(new DesignerActionPropertyItem("ButtonStyle", "Style", "Appearance", "Button style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Button orientation"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Button text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Button extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Button image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion

        #region Implementation
        private void OnCheckedClick(object sender, EventArgs e)
        {
            // Cast to the correct type
            DesignerVerb verb = sender as DesignerVerb;

            // Double check the source is the expected type
            if (verb != null)
            {
                // Decide on the new orientation required
                bool isChecked = verb.Text.Equals("Uncheck the button");

                // Decide on the next action to take given the new setting
                if (isChecked)
                    _action = "Uncheck the button";
                else
                    _action = "Check the button";

                // Get access to the actual Orientation propertry
                PropertyDescriptor checkedProp = TypeDescriptor.GetProperties(_checkButton)["Checked"];

                // If we succeeded in getting the property
                if (checkedProp != null)
                {
                    // Update the actual property with the new value
                    checkedProp.SetValue(_checkButton, !isChecked);
                }

                // Get the user interface service associated with actions
                DesignerActionUIService service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

                // If we managed to get it then request it update to reflect new action setting
                if (service != null)
                    service.Refresh(_checkButton);
            }
        }
        #endregion
    }
}
