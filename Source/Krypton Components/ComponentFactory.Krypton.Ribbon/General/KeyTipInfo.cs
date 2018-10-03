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
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class KeyTipInfoList : List<KeyTipInfo> {};

    internal class KeyTipInfo
    {
        #region Instance Fields
        private bool _enabled;
        private bool _visible;
        private string _keyString;
        private Point _screenPt;
        private Rectangle _clientRect;
        private IRibbonKeyTipTarget _target;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KeyTipInfo class.
        /// </summary>
        /// <param name="enabled">Enabled state of the item.</param>
        /// <param name="keyString">String of characters used to activate item.</param>
        /// <param name="screenPt">Screen coordinate for center of keytip.</param>
        /// <param name="clientRect">Client rectangle for keytip.</param>
        /// <param name="target">Target to invoke when item is selected.</param>
        public KeyTipInfo(bool enabled,
                          string keyString,
                          Point screenPt,
                          Rectangle clientRect,
                          IRibbonKeyTipTarget target)
        {
            _enabled = enabled;
            _keyString = keyString;
            _screenPt = screenPt;
            _clientRect = clientRect;
            _target = target;
            _visible = true;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the enabled state of the source item.
        /// </summary>
        public bool Enabled 
        {
            get { return _enabled; }
        }

        /// <summary>
        /// Gets and sets the visible state of the key tip.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// Gets the string used to select the item.
        /// </summary>
        public string KeyString 
        {
            get { return _keyString; }
        }

        /// <summary>
        /// Gets the center screen location for showing the keytip.
        /// </summary>
        public Point ScreenPt 
        {
            get { return _screenPt; }
        }

        /// <summary>
        /// Gets the client rectangle for showing the keytip.
        /// </summary>
        public Rectangle ClientRect
        {
            get { return _clientRect; }
        }

        /// <summary>
        /// Perform actual selection of the item.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public void KeyTipSelect(KryptonRibbon ribbon)
        {
            if (_target != null)
                _target.KeyTipSelect(ribbon);
        }
        #endregion
    }
}
