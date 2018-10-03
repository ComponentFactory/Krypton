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
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Restrict a controls collection of child controls.
    /// </summary>
    public class KryptonReadOnlyControls : KryptonControlCollection
    {
        #region Instance Fields
        private bool _allowRemove;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonReadOnlyControls class.
        /// </summary>
        /// <param name="owner">Owning control.</param>
        public KryptonReadOnlyControls(Control owner)
            : base(owner)
        {
            _allowRemove = false;
        }
        #endregion

        #region Public
        /// <summary>
        /// Clear out all the entries in the collection.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AllowRemoveInternal
        {
            get { return _allowRemove; }
            set { _allowRemove = value; }
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Adds the specified control to the control collection.
        /// </summary>
        /// <param name="value">The Control to add to the control collection.</param>
        public override void Add(Control value)
        {
            if (AllowRemoveInternal)
                base.Add(value);
            else
                throw new NotSupportedException("ReadOnly controls collection");
        }

        /// <summary>
        /// Adds an array of control objects to the collection.
        /// </summary>
        /// <param name="controls">An array of Control objects to add to the collection.</param>
        public override void AddRange(Control[] controls)
        {
            if (AllowRemoveInternal)
                base.AddRange(controls);
            else
                throw new NotSupportedException("ReadOnly controls collection");
        }

        /// <summary>
        /// Removes the specified control from the control collection.
        /// </summary>
        /// <param name="value">The Control to remove from the Control.ControlCollection.</param>
        public override void Remove(Control value)
        {
            if (AllowRemoveInternal)
                base.Remove(value);
            else
            {
                if (Contains(value))
                    throw new NotSupportedException("ReadOnly controls collection");
            }
        }

        /// <summary>
        /// Removes the child control with the specified key.
        /// </summary>
        /// <param name="key">The name of the child control to remove.</param>
        public override void RemoveByKey(string key)
        {
            if (AllowRemoveInternal)
                base.RemoveByKey(key);
            else
            {
                if (ContainsKey(key))
                    throw new NotSupportedException("ReadOnly controls collection");
            }
        }

        /// <summary>
        /// Removes all controls from the collection.
        /// </summary>
        public override void Clear()
        {
            if (AllowRemoveInternal)
                base.Clear();
            else
            {
                if (Count > 0)
                    throw new NotSupportedException("ReadOnly controls collection");
            }
        }
        #endregion
    }
}
