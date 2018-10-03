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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implementation for internal calendar buttons.
    /// </summary>
    public class ButtonSpecCalendar : ButtonSpec
    {
        #region Instance Fields
        private ViewDrawMonth _month;
        private RelativeEdgeAlign _edge;
        private bool _visible;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecCalendar class.
        /// </summary>
        /// <param name="month">Reference to owning view.</param>
        /// <param name="fixedStyle">Fixed style to use.</param>
        /// <param name="edge">Alignment edge.</param>
        public ButtonSpecCalendar(ViewDrawMonth month,
                                  PaletteButtonSpecStyle fixedStyle,
                                  RelativeEdgeAlign edge)
        {
            Debug.Assert(month != null);

            // Remember back reference to owning navigator.
            _month = month;
            _edge = edge;
            _enabled = true;
            _visible = true;

            // Fix the type
            ProtectedType = fixedStyle;
        }      
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the visible state.
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// Gets and sets the enabled state.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Can a component be associated with the view.
        /// </summary>
        public override bool AllowComponent
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            return Visible;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            return (Enabled ? ButtonEnabled.Container : ButtonEnabled.False);
        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette)
        {
            return ButtonCheckState.Unchecked;
        }

        /// <summary>
        /// Gets the button edge to position against.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Edge position.</returns>
        public override RelativeEdgeAlign GetEdge(IPalette palette)
        {
            return _edge;
        }
        #endregion
    }
}
