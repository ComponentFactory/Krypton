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
