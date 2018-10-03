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

namespace ComponentFactory.Krypton.Toolkit
{
    internal class ShortTextValue : NullContentValues
    {
        #region Instance Fields
        private string _shortText;
        #endregion

        #region Identity
        /// <summary>
        /// Gets and sets the short text value to use.
        /// </summary>
        public string ShortText
        {
            get { return _shortText; }
            set { _shortText = value; }
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public override string GetShortText()
        {
            return _shortText;
        }
        #endregion
    }
}
