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
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Manage a collection of ButtonSpec instances.
	/// </summary>
    [ListBindable(false)]
    public abstract class ButtonSpecCollectionBase : GlobalId
    {
        #region Instance Fields
        private object _owner;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a spec is about to be added/inserted to the collection.
        /// </summary>
        public event EventHandler<ButtonSpecEventArgs> Inserting;

        /// <summary>
        /// Occurs when a spec has been added/inserted to the collection.
        /// </summary>
        public event EventHandler<ButtonSpecEventArgs> Inserted;

        /// <summary>
        /// Occurs when a spec is about to be removed from the collection.
        /// </summary>
        public event EventHandler<ButtonSpecEventArgs> Removing;

        /// <summary>
        /// Occurs when a spec is removed from the collection.
        /// </summary>
        public event EventHandler<ButtonSpecEventArgs> Removed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecCollectionBase class.
        /// </summary>
        /// <param name="owner">Reference to owning object instance.</param>
        public ButtonSpecCollectionBase(object owner)
        {
            Debug.Assert(owner != null);
            _owner = owner;
        }
        #endregion

        #region Public
        /// <summary>
        /// Provide access to the derived generic enumerator.
        /// </summary>
        /// <returns>IEnumerable instance.</returns>
        public abstract IEnumerable Enumerate();

        /// <summary>
        /// Gets and sets the owner of the collection.
        /// </summary>
        public object Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Inserting event.
        /// </summary>
        /// <param name="e">A NavButtonSpecEventArgs instance containing event data.</param>
        protected void OnInserting(ButtonSpecEventArgs e)
        {
            e.ButtonSpec.Owner = _owner;
            if (Inserting != null)
                Inserting(this, e);
        }

        /// <summary>
        /// Raises the Inserted event.
        /// </summary>
        /// <param name="e">A NavButtonSpecEventArgs instance containing event data.</param>
        protected void OnInserted(ButtonSpecEventArgs e)
        {
            if (Inserted != null)
                Inserted(this, e);
        }

        /// <summary>
        /// Raises the Removing event.
        /// </summary>
        /// <param name="e">A NavButtonSpecEventArgs instance containing event data.</param>
        protected void OnRemoving(ButtonSpecEventArgs e)
        {
            e.ButtonSpec.Owner = null;
            if (Removing != null)
                Removing(this, e);
        }

        /// <summary>
        /// Raises the Removed event.
        /// </summary>
        /// <param name="e">A NavButtonSpecEventArgs instance containing event data.</param>
        protected void OnRemoved(ButtonSpecEventArgs e)
        {
            if (Removed != null)
                Removed(this, e);
        }
        #endregion
    }

    /// <summary>
	/// Manage a collection of ButtonSpec instances.
	/// </summary>
    public class ButtonSpecCollection<T> : ButtonSpecCollectionBase,
                                           IList,
							               IList<T>,
							               ICollection,
                                           ICollection<T> where T : ButtonSpec
										 
										 
	{
		#region Instance Fields
        private List<T> _specs;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ButtonSpecCollection class.
		/// </summary>
        /// <param name="owner">Reference to owning object instance.</param>
        public ButtonSpecCollection(object owner)
            : base(owner)
		{
			// Create internal storage
            _specs = new List<T>(6);
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
            return Count + " Instances";
		}
		#endregion

		#region IList
		/// <summary>
		/// Append a button spec to the collection.
		/// </summary>
		/// <param name="value">Object reference.</param>
        /// <returns>The position into which the new button spec was inserted.</returns>
		public int Add(object value)
		{
            // Use strongly typed implementation
            Add(value as T);

            // Index is the last button spec in the collection
			return (Count - 1);
		}

        /// <summary>
        /// Append an array of button spec instances.
        /// </summary>
        /// <param name="array">Array of instances.</param>
        public void AddRange(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

		/// <summary>
        /// Determines whether the collection contains the button spec.
		/// </summary>
		/// <param name="value">Object reference.</param>
        /// <returns>True if button spec found; otherwise false.</returns>
		public bool Contains(object value)
		{
			// Use strongly typed implementation
            return Contains(value as T);
		}

		/// <summary>
        /// Determines the index of the specified spec in the collection.
		/// </summary>
		/// <param name="value">Object reference.</param>
		/// <returns>-1 if not found; otherwise index position.</returns>
		public int IndexOf(object value)
		{
			// Use strongly typed implementation
            return IndexOf(value as T);
		}

		/// <summary>
        /// Inserts a button spec to the collection at the specified index.
		/// </summary>
		/// <param name="index">Insert index.</param>
		/// <param name="value">Object reference.</param>
		public void Insert(int index, object value)
		{
            // Use strongly typed implementation
            Insert(index, value as T);
		}

		/// <summary>
		/// Gets a value indicating whether the collection has a fixed size. 
		/// </summary>
		public bool IsFixedSize
		{
			get { return false; }
		}

		/// <summary>
		/// Removes first occurance of specified object.
		/// </summary>
		/// <param name="value">Object reference.</param>
		public void Remove(object value)
		{
            // Use strongly typed implementation
            Remove(value as T);
		}

		/// <summary>
        /// Gets or sets the button spec at the specified index.
		/// </summary>
		/// <param name="index">Object index.</param>
		/// <returns>Object at specified index.</returns>
		object IList.this[int index]
		{
			get { return _specs[index]; }
			
			set
			{
				throw new NotImplementedException("Cannot set a collection index with a new value");
			}
		}
		#endregion

        #region IList<T>
        /// <summary>
        /// Determines the index of the specified spec in the collection.
		/// </summary>
        /// <param name="item">T reference.</param>
		/// <returns>-1 if not found; otherwise index position.</returns>
        public int IndexOf(T item)
		{
			Debug.Assert(item != null);
			return _specs.IndexOf(item);
		}

		/// <summary>
        /// Inserts a button spec to the collection at the specified index.
		/// </summary>
		/// <param name="index">Insert index.</param>
        /// <param name="item">T reference.</param>
        public void Insert(int index, T item)
		{
            Debug.Assert(item != null);

            // We do not allow an empty button spec to be added
			if (item == null)
				throw new ArgumentNullException("item");

            // Not allow to add the same button spec more than once
			if (_specs.Contains(item))
                throw new ArgumentOutOfRangeException("item", "T already in collection");

            // Generate before insert event
            OnInserting(new ButtonSpecEventArgs(item, index));

			// Add into the internal collection
			_specs.Insert(index, item);

			// Generate after insert event
            OnInserted(new ButtonSpecEventArgs(item, index));
		}

		/// <summary>
        /// Removes the button spec at the specified index.
		/// </summary>
		/// <param name="index">Remove index.</param>
		public void RemoveAt(int index)
		{
            // Cache the spec being removed
            T item = this[index];

			// Generate before remove event
            OnRemoving(new ButtonSpecEventArgs(item, index));

            // Remove spec from internal collection
			_specs.RemoveAt(index);

			// Generate after remove event
            OnRemoved(new ButtonSpecEventArgs(item, index));
		}

		/// <summary>
        /// Gets or sets the button spec at the specified index.
		/// </summary>
        /// <param name="index">T index.</param>
        /// <returns>T at specified index.</returns>
        public T this[int index]
		{
			get { return _specs[index]; }
			
			set
			{
				throw new NotImplementedException("Cannot set a collection index with a new value");
			}
		}

        /// <summary>
        /// Gets the button spec with the provided unique name.
        /// </summary>
        /// <param name="uniqueName">Unique name of the ButtonSpec instance.</param>
        /// <returns>T at specified index.</returns>
        public T this[string uniqueName]
        {
            get 
            {
                // First priority is the UniqueName
                foreach (T bs in this)
                    if (bs.UniqueName == uniqueName)
                        return bs;

                // Second priority is the Text
                foreach (T bs in this)
                    if (bs.Text == uniqueName)
                        return bs;

                // No match found
                return null;
            }
        }
        #endregion

        #region ICollection<T>
        /// <summary>
        /// Append a button spec to the collection.
		/// </summary>
        /// <param name="item">T reference.</param>
        public void Add(T item)
		{
			Debug.Assert(item != null);

            // We do not allow an empty button spec to be added
			if (item == null)
				throw new ArgumentNullException("item");

            // Not allow to add the same button spec more than once
			if (_specs.Contains(item))
                throw new ArgumentOutOfRangeException("item", "T already in collection");

            // Generate inserting event
            OnInserting(new ButtonSpecEventArgs(item, _specs.Count));

			// Add to the internal collection
			_specs.Add(item);

			// Generate inserted event
            OnInserted(new ButtonSpecEventArgs(item, _specs.Count - 1));
		}

		/// <summary>
        /// Determines whether the collection contains the button spec.
		/// </summary>
        /// <param name="item">T reference.</param>
        /// <returns>True if spec found; otherwise false.</returns>
        public bool Contains(T item)
		{
			Debug.Assert(item != null);
			return _specs.Contains(item);
		}

		/// <summary>
        /// Copies button specs to specified array starting at particular index.
		/// </summary>
		/// <param name="array">Target array.</param>
		/// <param name="arrayIndex">Starting array index.</param>
        public void CopyTo(T[] array, int arrayIndex)
		{
			Debug.Assert(array != null);
			_specs.CopyTo(array, arrayIndex);
		}

		/// <summary>
        /// Gets the number of button specs in collection.
		/// </summary>
		public int Count
		{
			get { return _specs.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
        /// Removes first occurance of specified spec.
		/// </summary>
        /// <param name="item">T reference.</param>
		/// <returns>True if removed; otherwise false.</returns>
        public bool Remove(T item)
		{
            Debug.Assert(item != null);

            // Cache the index of the button spec
			int index = IndexOf(item);

			// Generate before event
            OnRemoving(new ButtonSpecEventArgs(item, index));

			// Remove from the internal list
			bool ret = _specs.Remove(item);

			// Generate after event
            OnRemoved(new ButtonSpecEventArgs(item, index));

			return ret;
		}
		#endregion

		#region ICollection
		/// <summary>
        /// Copies all the elements of the current collection to the specified Array. 
		/// </summary>
        /// <param name="array">The Array that is the destination of the elements copied from the collection.</param>
		/// <param name="index">The index in array at which copying begins.</param>
		public void CopyTo(Array array, int index)
		{
            Debug.Assert(array != null);

			// Cannot pass a null target array
			if (array == null)
				throw new ArgumentNullException("array");

            // Try and copy each button spec to the destination array
            foreach (T spec in this)
                array.SetValue(spec, index++);
		}

        /// <summary>
        /// Remove all pages from the collection.
        /// </summary>
        public void Clear()
        {
            // Remove all the button specs that are allowed to be removed
            for (int i = Count - 1; i >= 0; i--)
                RemoveAt(i);
        }

        /// <summary>
        /// Gets a value indicating whether access to the collection is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized
		{
			get { return false; }
		}

		/// <summary>
        /// Gets an object that can be used to synchronize access to the collection. 
		/// </summary>
		public object SyncRoot
		{
			get { return this; }
		}
		#endregion

		#region IEnumerable
        /// <summary>
        /// Provide non generic access to the enumerator.
        /// </summary>
        /// <returns>IEnumerable instance.</returns>
        public override IEnumerable Enumerate()
        {
            return this;
        }
        
        /// <summary>
        /// Shallow enumerate over button specs in the collection.
		/// </summary>
		/// <returns>Enumerator instance.</returns>
        public IEnumerator<T> GetEnumerator()
		{
			return _specs.GetEnumerator();
		}

		/// <summary>
		/// Enumerate using non-generic interface.
		/// </summary>
		/// <returns>Enumerator instance.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _specs.GetEnumerator();
		}
        #endregion
	}
}
