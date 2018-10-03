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
using System.Diagnostics;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Image select event data.
	/// </summary>
	public class ImageSelectEventArgs : EventArgs
	{
		#region Instance Fields
        private ImageList _imageList;
        private int _imageIndex;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ImageSelectEventArgs class.
		/// </summary>
        /// <param name="imageList">Defined image list.</param>
        /// <param name="imageIndex">Index within the image list.</param>
        public ImageSelectEventArgs(ImageList imageList, int imageIndex)
		{
            _imageList = imageList;
            _imageIndex = imageIndex;
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets the image list.
		/// </summary>
        public ImageList ImageList
		{
            get { return _imageList; }
		}

        /// <summary>
        /// Gets the image index.
        /// </summary>
        public int ImageIndex
        {
            get { return _imageIndex; }
        }
        #endregion
	}
}
