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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Snoop incoming mouse messages for an element and inform tooltip manager about them.
    /// </summary>
    public class ToolTipController : GlobalId,
                                     IMouseController
    {
        #region Instance Fields
        private ToolTipManager _manager;
        private ViewBase _targetElement;
        private IMouseController _targetController;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize an instance of the TooltipController class.
        /// </summary>
        /// <param name="manager">Reference to manager of all tooltip functionality.</param>
        /// <param name="targetElement">Target element that controller is for.</param>
        /// <param name="targetController">Target controller that we are snooping.</param>
        public ToolTipController(ToolTipManager manager,
                                 ViewBase targetElement,
                                 IMouseController targetController)
        {
            Debug.Assert(manager != null);
            Debug.Assert(targetElement != null);

            // Remember incoming references
            _manager = manager;
            _targetElement = targetElement;
            _targetController = targetController;
        }
        #endregion

        #region IMouseController
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public void MouseEnter(Control c)
        {
            _manager.MouseEnter(_targetElement, c);

            if (_targetController != null)
                _targetController.MouseEnter(c);
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public void MouseMove(Control c, Point pt)
        {
            _manager.MouseMove(_targetElement, c, pt);

            if (_targetController != null)
                _targetController.MouseMove(c, pt);
        }

        /// <summary>
        /// Mouse button has been pressed in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button pressed down.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public bool MouseDown(Control c, Point pt, MouseButtons button)
        {
            _manager.MouseDown(_targetElement, c, pt, button);

            if (_targetController != null)
                return _targetController.MouseDown(c, pt, button);
            else
                return false;
        }

        /// <summary>
        /// Mouse button has been released in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button released.</param>
        public void MouseUp(Control c, Point pt, MouseButtons button)
        {
            _manager.MouseUp(_targetElement, c, pt, button);

            if (_targetController != null)
                _targetController.MouseUp(c, pt, button);
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public void MouseLeave(Control c, ViewBase next)
        {
            _manager.MouseLeave(_targetElement, c, next);
            
            if (_targetController != null)
                _targetController.MouseLeave(c, next);
        }

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public void DoubleClick(Point pt)
        {
            _manager.DoubleClick(_targetElement, pt);
            
            if (_targetController != null)
                _targetController.DoubleClick(pt);
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public bool IgnoreVisualFormLeftButtonDown
        {
            get
            {
                if (_targetController != null)
                    return _targetController.IgnoreVisualFormLeftButtonDown;
                else
                    return false;
            }
        }
        #endregion
    }
}
