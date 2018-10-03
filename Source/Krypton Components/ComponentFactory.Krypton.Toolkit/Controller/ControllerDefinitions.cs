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
using System.Text;

namespace ComponentFactory.Krypton.Toolkit
{
    #region IMouseController
    /// <summary>
	/// Interface for processing mouse notifications.
	/// </summary>
	public interface IMouseController
	{
		/// <summary>
		/// Mouse has entered the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
		void MouseEnter(Control c);

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
		/// <param name="pt">Mouse position relative to control.</param>
		void MouseMove(Control c, Point pt);

		/// <summary>
		/// Mouse button has been pressed in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
		/// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button pressed down.</param>
		/// <returns>True if capturing input; otherwise false.</returns>
		bool MouseDown(Control c, Point pt, MouseButtons button);

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
		/// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
		void MouseUp(Control c, Point pt, MouseButtons button);

		/// <summary>
		/// Mouse has left the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        void MouseLeave(Control c, ViewBase next);

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        void DoubleClick(Point pt);

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        bool IgnoreVisualFormLeftButtonDown { get; }
    }
    #endregion

    #region IKeyController
    /// <summary>
	/// Interface for processing keyboard notifications.
	/// </summary>
    public interface IKeyController
    {
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        void KeyDown(Control c, KeyEventArgs e);

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        void KeyPress(Control c, KeyPressEventArgs e);

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        bool KeyUp(Control c, KeyEventArgs e);
    }
    #endregion

    #region ISourceController
    /// <summary>
	/// Interface for processing source notifications.
	/// </summary>
    public interface ISourceController
    {
        /// <summary>
        /// Source control has got the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        void GotFocus(Control c);

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        void LostFocus(Control c);
    }
    #endregion
}
