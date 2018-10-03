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
    internal class KryptonGroupPanelDesigner : KryptonPanelDesigner,
                                               IKryptonDesignerSelect
	{
		#region Instance Fields
		private KryptonGroupPanel _panel;
        private ISelectionService _selectionService;
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

			// Remember references to components involved in design
            _panel = component as KryptonGroupPanel;

            // Acquire service interfaces
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));

			// If inside a Krypton group container then always lock the component from user size/location change
            if (_panel != null)
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)["Locked"];
                if ((descriptor != null) && ((_panel.Parent is KryptonGroup) || (_panel.Parent is KryptonHeaderGroup)))
                    descriptor.SetValue(component, true);
            }
		}

        /// <summary>
        /// Indicates if this designer's control can be parented by the control of the specified designer.
        /// </summary>
        /// <param name="parentDesigner">The IDesigner that manages the control to check.</param>
        /// <returns>true if the control managed by the specified designer can parent the control managed by this designer; otherwise, false.</returns>
        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            // We should only ever exist inside a Krypton group container
            return ((parentDesigner is KryptonGroup) || (parentDesigner is KryptonHeaderGroup));
        }

		/// <summary>
		/// Gets the selection rules that indicate the movement capabilities of a component.
		/// </summary>
		public override SelectionRules SelectionRules
		{
			get
			{
				// If the panel is inside our Krypton group container then prevent 
				// user changing the size or location of the group panel instance
                if ((Control.Parent is KryptonGroup) || 
                    (Control.Parent is KryptonHeaderGroup))
					return (SelectionRules.None | SelectionRules.Locked);
				else
					return SelectionRules.None;
			}
		}

		/// <summary>
		/// Gets a list of SnapLine objects representing significant alignment points for this control.
		/// </summary>
		public override IList SnapLines
		{
			get
			{
				ArrayList snapLines = null;

                // Let the base class generate snap lines
				base.AddPaddingSnapLines(ref snapLines);

				return snapLines;
			}
		}

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // This group panel does not have any smart tag actions
                return new DesignerActionListCollection();
            }
        }

        /// <summary>
        /// Should painting be performed for the selection glyph.
        /// </summary>
        public bool CanPaint 
        {
            get { return true; }
        }

        /// <summary>
        /// Select the control that contains the group panel.
        /// </summary>
        public void SelectParentControl()
        {
            if ((_panel != null) && (_panel.Parent != null))
                _selectionService.SetSelectedComponents(new object[] { _panel.Parent }, SelectionTypes.Primary);
        }
        #endregion

		#region Protected
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
                }
            }
            finally
            {
                // Ensure base class is always disposed
                base.Dispose(disposing);
            }
        }

		/// <summary>
		/// Allows a designer to add to the set of properties that it exposes through a TypeDescriptor.
		/// </summary>
		/// <param name="properties">The properties for the class of the component.</param>
		protected override void PreFilterProperties(IDictionary properties)
		{
			// Let base clas filter properties first
			base.PreFilterProperties(properties);

			// Remove the design time properties we do not want
			properties.Remove("Modifiers");
			properties.Remove("Locked");
			properties.Remove("GenerateMember");

			// Scan for the 'Name' propertty
			foreach (DictionaryEntry entry in properties)
			{
				// Get the property descriptor for the entry
				PropertyDescriptor descriptor = (PropertyDescriptor)entry.Value;

				// Is this the 'Name' we are searching for?
				if (descriptor.Name.Equals("Name") && descriptor.DesignTimeOnly)
				{
					// Hide the 'Name' property so the user cannot modify it
					Attribute[] attributeArray = new Attribute[2] { BrowsableAttribute.No, DesignerSerializationVisibilityAttribute.Hidden };
					properties[entry.Key] = TypeDescriptor.CreateProperty(descriptor.ComponentType, descriptor, attributeArray);

					// Finished
					break;
				}
			}
		}

		/// <summary>
		/// Gets an attribute that indicates the type of inheritance of the associated component.
		/// </summary>
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				// If we have a valid Krypton splitter panel instance
				if ((_panel != null) && (_panel.Parent != null))
				{
					// Then get the attribute associated with the parent of the panel
					return (InheritanceAttribute)TypeDescriptor.GetAttributes(_panel.Parent)[typeof(InheritanceAttribute)];
				}
				else
					return base.InheritanceAttribute;
			}
		}
		#endregion
	}
}
