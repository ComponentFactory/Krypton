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
	/// Manage a collection of specified reference instances.
	/// </summary>
	public class TypedCollection<T> : IList,
								      IList<T>,
								      ICollection,
                                      ICollection<T>  where T : class
										 
										 
	{
		#region Instance Fields
		private List<T> _list;
		#endregion

		#region Events
        /// <summary>
        /// Occurs when an item is about to be added/inserted to the collection.
        /// </summary>
        public event TypedHandler<T> Inserting;
        
        /// <summary>
        /// Occurs when an item has been added/inserted to the collection.
		/// </summary>
        public event TypedHandler<T> Inserted;

		/// <summary>
        /// Occurs when an item is about to be removed from the collection.
		/// </summary>
        public event TypedHandler<T> Removing;

		/// <summary>
        /// Occurs when an item is removed from the collection.
		/// </summary>
        public event TypedHandler<T> Removed;

		/// <summary>
        /// Occurs when an items are about to be removed from the collection.
		/// </summary>
		public event EventHandler Clearing;

		/// <summary>
        /// Occurs when an items have been removed from the collection.
		/// </summary>
		public event EventHandler Cleared;

        /// <summary>
        /// Occurs when items have been reordered inside the collection.
        /// </summary>
        public event EventHandler Reordered;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the TypedCollection class.
		/// </summary>
        public TypedCollection()
		{
			// Create internal storage
			_list = new List<T>(4);
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
            return Count + " TypedCollection";
		}
		#endregion

        #region AddRange
        /// <summary>
        /// Append an array of items.
        /// </summary>
        /// <param name="itemArray">Array of items to add.</param>
        public virtual void AddRange(T[] itemArray)
        {
            // Just add each item in the array in turn
            foreach (T item in itemArray)
                Add(item);
        }
        #endregion

        #region IList
        /// <summary>
        /// Append an item to the collection.
		/// </summary>
		/// <param name="value">Object reference.</param>
        /// <returns>The position into which the new item was inserted.</returns>
		public virtual int Add(object value)
		{
			// Use strongly typed implementation
			Add(value as T);

            // Index is the last item in the collection
			return (Count - 1);
		}

		/// <summary>
        /// Determines whether the collection contains the item.
		/// </summary>
		/// <param name="value">Object reference.</param>
        /// <returns>True if item found; otherwise false.</returns>
		public bool Contains(object value)
		{
			// Use strongly typed implementation
			return Contains(value as T);
		}

		/// <summary>
        /// Determines the index of the specified item in the collection.
		/// </summary>
		/// <param name="value">Object reference.</param>
		/// <returns>-1 if not found; otherwise index position.</returns>
		public int IndexOf(object value)
		{
			// Use strongly typed implementation
			return IndexOf(value as T);
		}

		/// <summary>
        /// Inserts an item to the collection at the specified index.
		/// </summary>
		/// <param name="index">Insert index.</param>
		/// <param name="value">Object reference.</param>
		public virtual void Insert(int index, object value)
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
		/// Removes first occurance of specified item.
		/// </summary>
		/// <param name="value">Object reference.</param>
		public void Remove(object value)
		{
			// Use strongly typed implementation
			Remove(value as T);
		}

		/// <summary>
        /// Gets or sets the item at the specified index.
		/// </summary>
		/// <param name="index">Object index.</param>
		/// <returns>Object at specified index.</returns>
		object IList.this[int index]
		{
			get { return _list[index]; }
			
			set
			{
				throw new NotImplementedException("Cannot set a collection index with a new value");
			}
		}
		#endregion

		#region IList<T>
		/// <summary>
        /// Determines the index of the specified item in the collection.
		/// </summary>
		/// <param name="item">Item reference.</param>
		/// <returns>-1 if not found; otherwise index position.</returns>
		public int IndexOf(T item)
		{
			Debug.Assert(item != null);
			return _list.IndexOf(item);
		}

		/// <summary>
        /// Inserts an item to the collection at the specified index.
		/// </summary>
		/// <param name="index">Insert index.</param>
		/// <param name="item">Item reference.</param>
		public virtual void Insert(int index, T item)
		{
			Debug.Assert(item != null);

            // We do not allow an empty ribbon tab to be added
			if (item == null)
				throw new ArgumentNullException("item");

            // Not allow to add the same item more than once
			if (_list.Contains(item))
				throw new ArgumentOutOfRangeException("item", "Item already in collection");

            // Generate before insert event
            OnInserting(new TypedCollectionEventArgs<T>(item, index));

			// Add into the internal collection
			_list.Insert(index, item);

			// Generate after insert event
            OnInserted(new TypedCollectionEventArgs<T>(item, index));
		}

		/// <summary>
        /// Removes the item at the specified index.
		/// </summary>
		/// <param name="index">Remove index.</param>
		public void RemoveAt(int index)
		{
            // Cache the item being removed
			T item = this[index];

			// Generate before remove event
            OnRemoving(new TypedCollectionEventArgs<T>(item, index));

            // Remove item from internal collection
			_list.RemoveAt(index);

			// Generate after remove event
            OnRemoved(new TypedCollectionEventArgs<T>(item, index));
		}

		/// <summary>
        /// Gets or sets the item at the specified index.
		/// </summary>
		/// <param name="index">Item index.</param>
		/// <returns>Item at specified index.</returns>
		public T this[int index]
		{
			get { return _list[index]; }
			
			set
			{
				throw new NotImplementedException("Cannot set a collection index with a new value");
			}
		}

        /// <summary>
        /// Gets the item with the provided unique name.
        /// </summary>
        /// <param name="name">Name of the ribbon tab instance.</param>
        /// <returns>Item at specified index.</returns>
        public virtual T this[string name]
        {
            get
            {
                // No match found
                return null;
            }
        }

        /// <summary>
        /// Move the source item to be immediately after the target item.
        /// </summary>
        /// <param name="source">Source item to be moved.</param>
        /// <param name="target">Target item to place the source item after.</param>
        public void MoveAfter(T source, T target)
        {
            _list.Remove(source);
            _list.Insert(_list.IndexOf(target) + 1, source);

            // Generate reorder event.
            OnReordered(EventArgs.Empty);
        }

        /// <summary>
        /// Move the source item to be immediately before the target item.
        /// </summary>
        /// <param name="source">Source item to be moved.</param>
        /// <param name="target">Target item to place the source item before.</param>
        public void MoveBefore(T source, T target)
        {
            _list.Remove(source);
            _list.Insert(_list.IndexOf(target), source);

            // Generate reorder event.
            OnReordered(EventArgs.Empty);
        }
        #endregion

		#region ICollection<T>
		/// <summary>
        /// Append an item to the collection.
		/// </summary>
		/// <param name="item">Item reference.</param>
		public virtual void Add(T item)
		{
			Debug.Assert(item != null);

            // We do not allow an empty item to be added
			if (item == null)
				throw new ArgumentNullException("item");

            // Not allow to add the same item more than once
			if (_list.Contains(item))
				throw new ArgumentOutOfRangeException("item", "Item already in collection");

            // Generate before insert event
            OnInserting(new TypedCollectionEventArgs<T>(item, _list.Count));
            
            // Add to the internal collection
			_list.Add(item);

			// Generate inserted event
			OnInserted(new TypedCollectionEventArgs<T>(item, _list.Count - 1));
		}

		/// <summary>
        /// Remove all items from the collection.
		/// </summary>
		public void Clear()
		{
			// Generate before event
			OnClearing(EventArgs.Empty);

			// Remove all entries from internal collection
			_list.Clear();

			// Generate after event
			OnCleared(EventArgs.Empty);
		}

		/// <summary>
        /// Determines whether the collection contains the item.
		/// </summary>
		/// <param name="item">Item reference.</param>
        /// <returns>True if item found; otherwise false.</returns>
		public bool Contains(T item)
		{
			return _list.Contains(item);
		}

		/// <summary>
        /// Copies items to specified array starting at particular index.
		/// </summary>
		/// <param name="array">Target array.</param>
		/// <param name="arrayIndex">Starting array index.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			Debug.Assert(array != null);
			_list.CopyTo(array, arrayIndex);
		}

		/// <summary>
        /// Gets the number of items in collection.
		/// </summary>
		public int Count
		{
			get { return _list.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
        /// Removes first occurance of specified item.
		/// </summary>
		/// <param name="item">Item reference.</param>
		/// <returns>True if removed; otherwise false.</returns>
		public virtual bool Remove(T item)
		{
			Debug.Assert(item != null);

            // Cache the index of the item
			int index = IndexOf(item);

			// Generate before event
			OnRemoving(new TypedCollectionEventArgs<T>(item, index));

			// Remove from the internal list
			bool ret = _list.Remove(item);

			// Generate after event
            OnRemoved(new TypedCollectionEventArgs<T>(item, index));

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

            // Try and copy each item to the destination array
			foreach (T item in this)
                array.SetValue(item, index++);
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
        /// Shallow enumerate over items in the collection.
		/// </summary>
		/// <returns>Enumerator instance.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		/// <summary>
		/// Enumerate using non-generic interface.
		/// </summary>
		/// <returns>Enumerator instance.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _list.GetEnumerator();
		}
		#endregion

		#region Protected
        /// <summary>
        /// Raises the Inserting event.
        /// </summary>
        /// <param name="e">A KryptonRibbonTabEventArgs instance containing event data.</param>
        protected virtual void OnInserting(TypedCollectionEventArgs<T> e)
        {
            if (Inserting != null)
                Inserting(this, e);
        }

        /// <summary>
		/// Raises the Inserted event.
		/// </summary>
        /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
        protected virtual void OnInserted(TypedCollectionEventArgs<T> e)
		{
			if (Inserted != null)
				Inserted(this, e);
		}

		/// <summary>
		/// Raises the Removing event.
		/// </summary>
        /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
        protected virtual void OnRemoving(TypedCollectionEventArgs<T> e)
		{
			if (Removing != null)
				Removing(this, e);
		}

		/// <summary>
		/// Raises the Removed event.
		/// </summary>
        /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
        protected virtual void OnRemoved(TypedCollectionEventArgs<T> e)
		{
			if (Removed != null)
				Removed(this, e);
		}

		/// <summary>
		/// Raises the Clearing event.
		/// </summary>
		/// <param name="e">An EventArgs instance containing event data.</param>
        protected virtual void OnClearing(EventArgs e)
		{
			if (Clearing != null)
				Clearing(this, e);
		}

		/// <summary>
		/// Raises the Cleared event.
		/// </summary>
		/// <param name="e">An EventArgs instance containing event data.</param>
        protected virtual void OnCleared(EventArgs e)
		{
			if (Cleared != null)
				Cleared(this, e);
		}

        /// <summary>
        /// Raises the Reordered event.
        /// </summary>
        /// <param name="e">An EventArgs instance containing event data.</param>
        protected virtual void OnReordered(EventArgs e)
        {
            if (Reordered != null)
                Reordered(this, e);
        }
        #endregion
	}
}
