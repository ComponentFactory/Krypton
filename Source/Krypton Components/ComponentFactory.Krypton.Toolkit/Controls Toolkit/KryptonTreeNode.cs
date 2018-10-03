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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Krypton extension of the TreeNode allowing extra information to be drawn.
    /// </summary>
    [ToolboxItem(false)]
    public class KryptonTreeNode : TreeNode
    {
        #region Instance Fields
        private string _longText;
        private Color _longForeColor;
        private Font _longNodeFont;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonTreeNode class.
        /// </summary>
        public KryptonTreeNode()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text.
        /// </summary>
        /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
        public KryptonTreeNode(string text)
            : base(text)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text and child tree nodes.
        /// </summary>
        /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
        /// <param name="children">An array of child System.Windows.Forms.TreeNode objects.</param>
        public KryptonTreeNode(string text, TreeNode[] children)
            : base(text, children)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text and images to display when the tree node is in a selected and unselected state.
        /// </summary>
        /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
        /// <param name="imageIndex">The index value of System.Drawing.Image to display when the tree node is unselected.</param>
        /// <param name="selectedImageIndex">The index value of System.Drawing.Image to display when the tree node is selected.</param>
        public KryptonTreeNode(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the System.Windows.Forms.TreeNode class with the specified label text, child tree nodes, and images to display when the tree node is in a selected and unselected state.
        /// </summary>
        /// <param name="text">The label System.Windows.Forms.TreeNode.Text of the new tree node.</param>
        /// <param name="imageIndex">The index value of System.Drawing.Image to display when the tree node is unselected.</param>
        /// <param name="selectedImageIndex">The index value of System.Drawing.Image to display when the tree node is selected.</param>
        /// <param name="children">An array of child System.Windows.Forms.TreeNode objects.</param>
        public KryptonTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children)
        {
            Init();
        }

        private void Init()
        {
            _longText = string.Empty;
            _longForeColor = Color.Empty;
            _longNodeFont = null;
        }
        #endregion

        #region LongText
        /// <summary>
        /// Gets and sets the long text.
        /// </summary>
        [Category("Appearance")]
        [Description("Supplementary text.")]
        [Localizable(true)]
        public string LongText
        {
            get { return _longText; }

            set 
            {
                if (_longText != value)
                {
                    _longText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LongText"));
                }
            }
        }

        private bool ShouldSerializeLongText()
        {
            return !string.IsNullOrEmpty(_longText);
        }
        #endregion    

        #region LongForeColor
        /// <summary>
        /// Gets or sets the foreground color of the long text.
        /// </summary>
        [Category("Appearance")]
        [Description("Foreground color of the long text")]
        public Color LongForeColor
        {
            get { return _longForeColor; }

            set
            {
                if (_longForeColor != value)
                {
                    _longForeColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LongForeColor"));
                }
            }
        }

        private bool ShouldSerializeLongForeColor()
        {
            return _longForeColor != Color.Empty;
        }
        #endregion    

        #region LongNodeFont
        /// <summary>
        /// Gets or sets the font of the long text.
        /// </summary>
        [Category("Appearance")]
        [Description("Font of the long text")]
        public Font LongNodeFont
        {
            get { return _longNodeFont; }

            set
            {
                if (_longNodeFont != value)
                {
                    _longNodeFont = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LongNodeFont"));
                }
            }
        }

        private bool ShouldSerializeLongNodeFont()
        {
            return _longNodeFont != null;
        }
        #endregion
        
        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion 
    }
}
