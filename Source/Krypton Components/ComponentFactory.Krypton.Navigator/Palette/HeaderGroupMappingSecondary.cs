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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Storage and mapping for secondary header.
	/// </summary>
    public class HeaderGroupMappingSecondary : HeaderGroupMappingBase
	{
		#region Static Fields
        private const string _defaultDescription = " ";
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the HeaderGroupMappingSecondary class.
        /// </summary>
        /// <param name="navigator">Reference to owning navogator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public HeaderGroupMappingSecondary(KryptonNavigator navigator,
                                           NeedPaintHandler needPaint)
            : base(navigator, needPaint)
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
            return _defaultDescription;
		}

		/// <summary>
		/// Gets the default description value.
		/// </summary>
		/// <returns>String reference.</returns>
		protected override string GetDescriptionDefault()
		{
            return string.Empty;
		}

        /// <summary>
        /// Gets the default image mapping value.
        /// </summary>
        /// <returns>Image mapping enumeration.</returns>
        protected override MapKryptonPageImage GetMapImageDefault()
        {
            return MapKryptonPageImage.None;
        }

        /// <summary>
        /// Gets the default heading mapping value.
        /// </summary>
        /// <returns>Text mapping enumeration.</returns>
        protected override MapKryptonPageText GetMapHeadingDefault()
        {
            return MapKryptonPageText.Description;
        }

        /// <summary>
        /// Gets the default description mapping value.
        /// </summary>
        /// <returns>Text mapping enumeration.</returns>
        protected override MapKryptonPageText GetMapDescriptionDefault()
        {
            return MapKryptonPageText.None;
        }		
        #endregion

        #region MapImage
        /// <summary>
        /// Gets and sets the mapping used for the Image property.
        /// </summary>
        [DefaultValue(typeof(MapKryptonPageImage), "None (Null image)")]
        public override MapKryptonPageImage MapImage
        {
            get { return base.MapImage; }
            set { base.MapImage = value; }
        }
        #endregion

        #region MapHeading
        /// <summary>
        /// Gets and sets the mapping used for the Heading property.
        /// </summary>
        [DefaultValue(typeof(MapKryptonPageText), "Description")]
        public override MapKryptonPageText MapHeading
        {
            get { return base.MapHeading; }
            set { base.MapHeading = value; }
        }
        #endregion

        #region MapDescription
        /// <summary>
        /// Gets and sets the mapping used for the Description property.
        /// </summary>
        [DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
        public override MapKryptonPageText MapDescription
        {
            get { return base.MapDescription; }
            set { base.MapDescription = value; }
        }
        #endregion
    }
}
