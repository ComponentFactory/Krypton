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
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for the group box caption values.
	/// </summary>
	public class CaptionValues : HeaderValuesBase
	{
		#region Static Fields
        private const string _defaultText = "Caption";
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the CaptionValues class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public CaptionValues(NeedPaintHandler needPaint)
            : base(needPaint)
		{
		}
		#endregion

		#region Default Values
		/// <summary>
		/// Gets the default image value.
		/// </summary>
		/// <returns>Image reference.</returns>
		protected override Image GetImageDefault()
		{
			return null;
		}

		/// <summary>
		/// Gets the default heading value.
		/// </summary>
		/// <returns>String reference.</returns>
		protected override string GetHeadingDefault()
		{
            return _defaultText;
		}

		/// <summary>
		/// Gets the default description value.
		/// </summary>
		/// <returns>String reference.</returns>
		protected override string GetDescriptionDefault()
		{
            return string.Empty;
		}
		#endregion

        #region Description
        /// <summary>
        /// Gets and sets the header description text.
        /// </summary>
        [DefaultValue("")]
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        #endregion
    }
}
