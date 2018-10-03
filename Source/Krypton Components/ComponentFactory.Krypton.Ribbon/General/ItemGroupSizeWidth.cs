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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class ItemSizeWidth
    {
        #region Instance Fields
        private GroupItemSize _groupItemSize;
        private int _width;
        private int _tag;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        public ItemSizeWidth(GroupItemSize itemSize, int width)
            : this(itemSize, width, -1)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ItemSizeWidth class.
        /// </summary>
        /// <param name="itemSize">Group item size.</param>
        /// <param name="width">Width associated with the item size.</param>
        /// <param name="tag">Source specific tag information.</param>
        public ItemSizeWidth(GroupItemSize itemSize, 
                             int width,
                             int tag)
        {
            _groupItemSize = itemSize;
            _width = width;
            _tag = tag;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item size.
        /// </summary>
        public GroupItemSize GroupItemSize
        {
            get { return _groupItemSize; }
            set { _groupItemSize = value; }
        }

        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets and sets the item tag.
        /// </summary>
        public int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        #endregion
    }

    internal class GroupSizeWidth
    {
        #region Instance Fields
        private int _width;
        private ItemSizeWidth[] _sizing;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the GroupSizeWidth class.
        /// </summary>
        /// <param name="width">Width available for sizing a group.</param>
        /// <param name="sizing">Sizing information for applying to group.</param>
        public GroupSizeWidth(int width, ItemSizeWidth[] sizing)
        {
            _width = width;
            _sizing = sizing;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the item width.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Gets and sets the array of sizing information for group.
        /// </summary>
        public ItemSizeWidth[] Sizing
        {
            get { return _sizing; }
            set { _sizing = value; }
        }
        #endregion
    }
}
