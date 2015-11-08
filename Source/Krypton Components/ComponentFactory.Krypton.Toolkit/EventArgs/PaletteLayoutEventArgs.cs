// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Details for palette layout events.
	/// </summary>
    public class PaletteLayoutEventArgs : NeedLayoutEventArgs
	{
		#region Instance Fields
        private bool _needColorTable;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteLayoutEventArgs class.
		/// </summary>
        /// <param name="needLayout">Does the layout need regenerating.</param>
        /// <param name="needColorTable">Have the color table values changed?</param>
        public PaletteLayoutEventArgs(bool needLayout,
                                      bool needColorTable)
            : base(needLayout)
		{
            _needColorTable = needColorTable;
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets a value indicating if the color table needs to be reprocessed.
		/// </summary>
        public bool NeedColorTable
		{
            get { return _needColorTable; }
		}
		#endregion
	}
}
