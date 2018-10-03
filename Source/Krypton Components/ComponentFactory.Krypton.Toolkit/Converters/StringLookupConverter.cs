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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Helper base class used to convert from to/from a table of value,string pairs.
    /// </summary>
    public abstract class StringLookupConverter : EnumConverter
    {
		#region Type Definitions
        /// <summary>
        /// Represents a name/value pair association.
        /// </summary>
		protected struct Pair
        {
            /// <summary>
            /// Enumeration value.
            /// </summary>
            public object Enum;

            /// <summary>
            /// Enumeration value display string.
            /// </summary>
            public string Display;

            /// <summary>
            /// Initialize a new instance of the Pair structure.
            /// </summary>
            /// <param name="obj">Object instance.</param>
            /// <param name="str">String instance.</param>
            public Pair(object obj, string str)
            {
                Enum = obj;
                Display = str;
            }
        }
		#endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the StringLookupConverter clas.
        /// </summary>
        public StringLookupConverter(Type enumType)
            : base(enumType)
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected abstract Pair[] Pairs { get; }
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
                // Search for a matching value
                foreach (Pair p in Pairs)
                    if (p.Enum.Equals(value))
                        return p.Display;
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
                // Search for a matching string
                foreach (Pair p in Pairs)
                    if (p.Display.Equals(value))
                        return p.Enum;
            }

            // Let base class perform default conversion
            return base.ConvertFrom(context, culture, value);
        }
        #endregion
    }
}
