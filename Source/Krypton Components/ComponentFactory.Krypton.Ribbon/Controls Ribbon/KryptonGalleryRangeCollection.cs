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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for gallery range item accessor.
    /// </summary>
    public class KryptonGalleryRangeCollection : TypedCollection<KryptonGalleryRange>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="heading">Heading of the gallery range instance.</param>
        /// <returns>Item at specified index.</returns>
        public override KryptonGalleryRange this[string heading]
        {
            get
            {
                // Search for a gallery range with the same heading as that requested.
                foreach (KryptonGalleryRange range in this)
                    if (range.Heading == heading)
                        return range;

                // Let base class perform standard processing
                return base[heading];
            }
        }
        #endregion
    }
}
