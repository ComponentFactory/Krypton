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
    /// Contains a global identifier that is unique among objects.
    /// </summary>
    public class GlobalId
    {
        #region Instance Fields
        private int _id;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GlobalId class.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public GlobalId()
        {
            // Assign the next global identifier
            _id = CommonHelper.NextId;
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets the unique identifier of the object.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id
        {
            get { return _id; }
        }
        #endregion
    }
}
