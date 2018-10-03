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
    internal class KryptonSplitterPanelDesigner : KryptonPanelDesigner,
                                                  IKryptonDesignerSelect
	{
		#region Instance Fields
		private KryptonSplitterPanel _panel;
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
            _panel = component as KryptonSplitterPanel;

            // Acquire service interfaces
            _selectionService = (ISelectionService)GetService(typeof(ISelectionService));

			// Hook into changes in selected component
			IComponentChangeService service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
			if (service != null)
				service.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);

			// If inside a Krypton split container then always lock the component from user size/location change
            if (_panel != null)
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)["Locked"];
                if ((descriptor != null) && (_panel.Parent is KryptonSplitContainer))
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
            // We should only ever exist inside a Krypton split container
            return (parentDesigner is KryptonSplitContainerDesigner);
        }

		/// <summary>
		/// Gets the selection rules that indicate the movement capabilities of a component.
		/// </summary>
		public override SelectionRules SelectionRules
		{
			get
			{
				// If the panel is inside our Krypton split container then prevent 
				// user changing the size or location of the split panel instance
				if (Control.Parent is KryptonSplitContainer)
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
		/// Releases the resources used by the KryptonSplitterPanelDesigner. 
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
            try
            {
                if (disposing)
                {
                    // Get access to the component change service
                    IComponentChangeService service = (IComponentChangeService)GetService(typeof(IComponentChangeService));

                    // Must unhook our event from the service so we can be garbage collected
                    if (service != null)
                        service.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
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

			// If the panel has no children, then draw the watermark
			if ((_panel != null) && (_panel.Controls.Count == 0))
				DrawWaterMark(pe.Graphics);
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

		#region Implementation
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			// Assuming the panel has a parent
			if ((_panel != null) && (_panel.Parent != null))
			{
				// And the panel does not have any children yet
				if (_panel.Controls.Count == 0)
				{
					// Then we need to draw a watermark to indicate no children
					using(Graphics g = _panel.CreateGraphics())
						DrawWaterMark(g);
				}
				else
				{
					// Panel has children, so just redraw it
					_panel.Invalidate();
				}
			}
		}

		private void DrawWaterMark(Graphics g)
		{
			// Get the rectangle available for drawing
			Rectangle clientRect = Control.ClientRectangle;

			// Get the name of the control to draw
			string drawText = Control.Name;

			// Use a fixed font for the drawing
			using (Font f = new Font("Arial", 8f))
			{
                try
                {
                    // Measure the size of the text
                    SizeF sizeF = g.MeasureString(drawText, f);

                    // Find the drawing position to centre the text
                    int middleX = (clientRect.Width / 2) - (((int)sizeF.Width) / 2);
                    int middleY = (clientRect.Height / 2) - (((int)sizeF.Height) / 2);

                    // Draw the name of the panel in the centre
                    TextRenderer.DrawText(g, drawText, f,
                                          new Point(middleX, middleY),
                                          Color.Black,
                                          TextFormatFlags.GlyphOverhangPadding);
                }
                catch { }
			}
		}
		#endregion
	}
}
