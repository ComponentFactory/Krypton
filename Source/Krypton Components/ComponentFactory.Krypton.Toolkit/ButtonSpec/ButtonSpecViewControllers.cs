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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Stores a triple of controller references.
    /// </summary>
    public class ButtonSpecViewControllers
    {
        #region Instance Fields
        private IMouseController _mouseController;
        private ISourceController _sourceController;
        private IKeyController _keyController;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecViewControllers class.
        /// </summary>
        /// <param name="mouseController">Mouse controller.</param>
        /// <param name="sourceController">Source controller.</param>
        /// <param name="keyController">Key controller.</param>
        public ButtonSpecViewControllers(IMouseController mouseController,
                                         ISourceController sourceController,
                                         IKeyController keyController)
        {
            Debug.Assert(mouseController != null);
            Debug.Assert(sourceController != null);
            Debug.Assert(keyController != null);

            _mouseController = mouseController;
            _sourceController = sourceController;
            _keyController = keyController;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the mouse controller reference.
        /// </summary>
        public IMouseController MouseController
        {
            get { return _mouseController; }
        }

        /// <summary>
        /// Gets the mouse controller reference.
        /// </summary>
        public ISourceController SourceController
        {
            get { return _sourceController; }
        }

        /// <summary>
        /// Gets the mouse controller reference.
        /// </summary>
        public IKeyController KeyController
        {
            get { return _keyController; }
        }
        #endregion
    }
}
