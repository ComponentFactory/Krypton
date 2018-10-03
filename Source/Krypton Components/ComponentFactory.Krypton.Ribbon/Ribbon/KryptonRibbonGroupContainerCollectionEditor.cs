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
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	internal class KryptonRibbonGroupContainerCollectionEditor : CollectionEditor
	{
		/// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupTopCollectionEditor class.
		/// </summary>
        public KryptonRibbonGroupContainerCollectionEditor()
            : base(typeof(KryptonRibbonGroupContainerCollection))
		{
		}

		/// <summary>
		/// Gets the data types that this collection editor can contain. 
		/// </summary>
		/// <returns>An array of data types that this collection can contain.</returns>
		protected override Type[] CreateNewItemTypes()
		{
            return new Type[] { typeof(KryptonRibbonGroupLines),
                                typeof(KryptonRibbonGroupTriple),
                                typeof(KryptonRibbonGroupSeparator) };
		}
	}
}
