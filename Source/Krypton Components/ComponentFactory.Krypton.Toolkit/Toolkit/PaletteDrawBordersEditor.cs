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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class PaletteDrawBordersEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>UITypeEditorEditStyle value.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            // We show a drop down for editing the PaletteDrawBorders value.
            if ((context != null) && (context.Instance != null))
                return UITypeEditorEditStyle.DropDown;
            else
                return base.GetEditStyle(context);
        }

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by the GetEditStyle method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <param name="provider">An IServiceProvider that this editor can use to obtain services.</param>
        /// <param name="value">The object to edit.</param>
        /// <returns>The new value of the object.</returns>
        public override object EditValue(ITypeDescriptorContext context, 
                                         IServiceProvider provider, 
                                         object value)
        {
            if ((context != null) && (provider != null) && (value != null))
            {
                // Grab the service needed to show the drop down
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (service != null)
                {
                    // Create the custom control used to edit value
                    PaletteDrawBordersSelector selector = new PaletteDrawBordersSelector();

                    // Populate selector with starting value
                    selector.Value = (PaletteDrawBorders)value;

                    // Show as a drop down control
                    service.DropDownControl(selector);

                    // Return the updated value
                    return selector.Value;
                }
            }

            return base.EditValue(context, provider, value);
        }
    }
}
