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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Custom type converter so that PaletteDrawBorders values appear as neat text at design time.
    /// </summary>
    internal class PaletteDrawBordersConverter : EnumConverter
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDrawBordersConverter clas.
        /// </summary>
        public PaletteDrawBordersConverter()
            : base(typeof(PaletteDrawBorders))
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo object. If a null reference the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType">The Type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context,
                                         System.Globalization.CultureInfo culture,
                                         object value,
                                         Type destinationType)
        {
            // We are only interested in adding functionality for converting to strings
            if (destinationType == typeof(string))
            {
                // Convert object to expected style
                PaletteDrawBorders borders = (PaletteDrawBorders)value;

                // If the inherit flag is set that that is the only flag of interest
                if ((borders & PaletteDrawBorders.Inherit) == PaletteDrawBorders.Inherit)
                    return "Inherit";
                else
                {
                    // Append the names of each border we want
                    StringBuilder sb = new StringBuilder();

                    if ((borders & PaletteDrawBorders.Top) == PaletteDrawBorders.Top)
                        sb.Append("Top");

                    if ((borders & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom)
                    {
                        if (sb.Length > 0)
                            sb.Append(",");

                        sb.Append("Bottom");
                    }

                    if ((borders & PaletteDrawBorders.Left) == PaletteDrawBorders.Left)
                    {
                        if (sb.Length > 0)
                            sb.Append(",");

                        sb.Append("Left");
                    }

                    if ((borders & PaletteDrawBorders.Right) == PaletteDrawBorders.Right)
                    {
                        if (sb.Length > 0)
                            sb.Append(",");

                        sb.Append("Right");
                    }

                    // If no border is wanted then return a fixed string
                    if (sb.Length == 0)
                        sb.Append("None");

                    return sb.ToString();
                }
            }

            // Let base class perform default conversion
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">The CultureInfo to use as the current culture.</param>
        /// <param name="value">The Object to convert.</param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context,
                                           System.Globalization.CultureInfo culture,
                                           object value)
        {
            // We are only interested in adding functionality for converting from strings
            if (value is string)
            {
                // Convert incoming value to a string
                string conv = (string)value;

                // Default to returning an empty value
                PaletteDrawBorders ret = PaletteDrawBorders.None;

                // If inherit is in the string, we use only that value
                if (conv.Contains("Inherit"))
                    ret = PaletteDrawBorders.Inherit;
                else
                {
                    // If the word 'none' is found then no value is needed
                    if (!conv.Contains("None"))
                    {
                        // Get the borders actually specified
                        if (conv.Contains("Top"))   ret |= PaletteDrawBorders.Top;
                        if (conv.Contains("Bottom")) ret |= PaletteDrawBorders.Bottom;
                        if (conv.Contains("Left")) ret |= PaletteDrawBorders.Left;
                        if (conv.Contains("Right")) ret |= PaletteDrawBorders.Right;
                    }
                }

                return ret;
            }

            // Let base class perform default conversion
            return base.ConvertFrom(context, culture, value);
        }
        #endregion
    }
}
