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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for tree view images.
	/// </summary>
    public class TreeViewImages : Storage
    {
        #region Instance Fields
        private Image _plus;
        private Image _minus;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TreeViewImages class.
		/// </summary>
        public TreeViewImages()
            : this(null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the TreeViewImages class.
		/// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public TreeViewImages(NeedPaintHandler needPaint) 
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create the storage
            _plus = null;
            _minus = null;
        }
        #endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get
			{
                return (_plus == null) &&
                       (_minus == null);
            }
		}
		#endregion

        #region Plus
        /// <summary>
        /// Gets and sets the image for use to expand a tree node.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image used to expand a tree node.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Plus
        {
            get { return _plus; }

            set
            {
                if (_plus != value)
                {
                    _plus = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the collapse property to its default value.
        /// </summary>
        public void ResetPlus()
        {
            Plus = null;
        }
        #endregion

        #region Minus
        /// <summary>
        /// Gets and sets the image for use to collapse a tree node.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image used to collapse a tree node.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Minus
        {
            get { return _minus; }

            set
            {
                if (_minus != value)
                {
                    _minus = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Minus property to its default value.
        /// </summary>
        public void ResetMinus()
        {
            Minus = null;
        }
        #endregion
    }
}
