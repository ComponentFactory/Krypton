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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// Handle a star number which consists of a number with optional asterisk at the end.
    /// </summary>
    public class StarNumber
    {
        #region Internal Fields
        private string _value;
        private bool _usingStar;
        private int _fixedSize;
        private double _starSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the StarNumber class.
        /// </summary>
        public StarNumber()
        {
            _value = "*";
            _usingStar = true;
            _fixedSize = 0;
            _starSize = 1;
        }

        /// <summary>
        /// Initialize a new instance of the StarNumber class.
        /// </summary>
        /// <param name="value">Initial value to process.</param>
        public StarNumber(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets a string representing the value of the instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the star notation and breaks it apart.
        /// </summary>
        public string Value
        {
            get { return _value; }

            set
            {
                // Validate the incoming value
                if (value == null)
                    throw new ArgumentNullException("Cannot be assigned a null value.");

                // If it ends with an asterisk...
                if (value.EndsWith("*"))
                {
                    // If there is only an asterisk in the string
                    if (value.Length == 1)
                        _starSize = 1;
                    else
                    {
                        // The star number can have decimal places
                        _starSize = double.Parse(value.Substring(0, value.Length - 1));
                    }

                    _usingStar = true;
                }
                else
                {
                    // No asterisk, so it should be just be an integer number
                    _fixedSize = int.Parse(value);
                    _usingStar = false;
                }

                _value = value;
            }
        }

        /// <summary>
        /// Gets a value indicating if stars are being specified.
        /// </summary>
        public bool UsingStar
        {
            get { return _usingStar; }
        }

        /// <summary>
        /// Gets the fixed size value.
        /// </summary>
        public int FixedSize
        {
            get { return _fixedSize; }
        }

        /// <summary>
        /// Gets the star size value.
        /// </summary>
        public double StarSize
        {
            get { return _starSize; }
        }
        #endregion

        #region Internal
        internal string PersistString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(_usingStar ? "T," : "F,");
                builder.Append(_fixedSize.ToString() + ",");
                builder.Append(CommonHelper.DoubleToString(_starSize));
                return builder.ToString();
            }

            set
            {
                string[] parts = value.Split(',');
                _usingStar = (parts[0] == "T");
                _fixedSize = int.Parse(parts[1]);
                _starSize = CommonHelper.StringToDouble(parts[2]);
            }
        }
        #endregion
    }
}
