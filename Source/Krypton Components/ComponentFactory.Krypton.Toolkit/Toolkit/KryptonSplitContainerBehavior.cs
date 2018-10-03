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
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonSplitContainerBehavior : Behavior
    {
        #region Instance Fields
        private KryptonSplitContainer _splitContainer;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonSplitContainerBehavior class.
        /// </summary>
        /// <param name="relatedDesigner">Reference to the containing designer.</param>
        public KryptonSplitContainerBehavior(IDesigner relatedDesigner)
        {
            _splitContainer = relatedDesigner.Component as KryptonSplitContainer;
        }
        #endregion

        #region Public Overrides
        /// <summary>
        ///  Called when any mouse-enter message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseEnter(Glyph g)
        {
            // Notify the split container so it can track mouse message
            if (_splitContainer != null)
                _splitContainer.DesignMouseEnter();

            return base.OnMouseEnter(g);
        }

        /// <summary>
        ///  Called when any mouse-down message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <param name="pt">The location at which the click occurred.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseDown(Glyph g, MouseButtons button, Point pt)
        {
            if (_splitContainer != null)
            {
                // Convert the adorner coordinate to the split container client coordinate
                Point splitPt = PointToSplitContainer(g, pt);

                // Notify the split container so it can track mouse message
                if (_splitContainer.DesignMouseDown(splitPt, button))
                {
                    // Splitter is starting to be moved, we need to capture mouse input
                    _splitContainer.Capture = true;
                }
            }

            return base.OnMouseDown(g, button, pt);
        }

        /// <summary>
        ///  Called when any mouse-move message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <param name="pt">The location at which the move occurred.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseMove(Glyph g, MouseButtons button, Point pt)
        {
            if (_splitContainer != null)
            {
                // Convert the adorner coordinate to the split container client coordinate
                Point splitPt = PointToSplitContainer(g, pt);

                // Notify the split container so it can track mouse message
                _splitContainer.DesignMouseMove(splitPt);
            }

            return base.OnMouseMove(g, button, pt);
        }

        /// <summary>
        ///  Called when any mouse-up message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <param name="button">A MouseButtons value indicating which button was clicked.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseUp(Glyph g, MouseButtons button)
        {
            // Notify the split container so it can track mouse message
            if (_splitContainer != null)
                _splitContainer.DesignMouseUp(button);

            return base.OnMouseUp(g, button);
        }

        /// <summary>
        ///  Called when any mouse-leave message enters the adorner window of the BehaviorService.
        /// </summary>
        /// <param name="g">A Glyph.</param>
        /// <returns>true if the message was handled; otherwise, false.</returns>
        public override bool OnMouseLeave(Glyph g)
        {
            // Notify the split container so it can track mouse message
            if (_splitContainer != null)
                _splitContainer.DesignMouseLeave();
            
            return base.OnMouseLeave(g);
        }
        #endregion

        #region Implementation Static
        private static Point PointToSplitContainer(Glyph g, Point pt)
        {
            // Cast the correct type
            KryptonSplitContainerGlyph splitGlyph = (KryptonSplitContainerGlyph)g;

            // Gets the bounds of the glyph in adorner coordinates
            Rectangle bounds = splitGlyph.Bounds;

            // Convert from adorner coordinates to the control client coordinates
            return new Point(pt.X - bounds.X, pt.Y - bounds.Y);
        }
        #endregion
    }
}
