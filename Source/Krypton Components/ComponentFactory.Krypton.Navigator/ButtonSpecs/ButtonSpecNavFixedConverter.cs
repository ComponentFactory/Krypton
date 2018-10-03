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
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Add conversion to a string for display in properties window at design time.
    /// </summary>
    public class ButtonSpecNavFixedConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            // Can always convert to a string representation
            if (destinationType == typeof(string))
                return true;

            // Let base class do standard processing
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType">The Type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, 
                                         System.Globalization.CultureInfo culture, 
                                         object value, 
                                         Type destinationType)
        {
            // Can always convert to a string representation
            if (destinationType == typeof(string))
            {
                // Cast to correct type
                ButtonSpecNavFixed buttonSpec = (ButtonSpecNavFixed)value;

                // Ask the button spec for the correct string
                return buttonSpec.ToString();
            }
            
            // Let base class attempt other conversions
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
