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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonSplitContainerGlyph : Glyph
    {
        #region Instance Fields
        private KryptonSplitContainer _splitContainer;
        private ISelectionService _selectionService;
        private BehaviorService _behaviorService; 
        private Adorner _adorner;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonSplitContainerGlyph class.
        /// </summary>
        /// <param name="selectionService">Reference to the selection service.</param>
        /// <param name="behaviorService">Reference to the behavior service.</param>
        /// <param name="adorner">Reference to the containing adorner.</param>
        /// <param name="relatedDesigner">Reference to the containing designer.</param>
        public KryptonSplitContainerGlyph(ISelectionService selectionService,
                                          BehaviorService behaviorService,
                                          Adorner adorner,
                                          IDesigner relatedDesigner)
            : base(new KryptonSplitContainerBehavior(relatedDesigner))
        {
            Debug.Assert(selectionService != null);
            Debug.Assert(behaviorService != null);
            Debug.Assert(adorner != null);
            Debug.Assert(relatedDesigner != null);

            // Remember incoming references
            _selectionService = selectionService;
            _behaviorService = behaviorService;
            _adorner = adorner;

            // Find the related control
            _splitContainer = relatedDesigner.Component as KryptonSplitContainer;

            // We want to know whenever the selection has changed or a property has changed
            _selectionService.SelectionChanged += new EventHandler(OnSelectionChanged);
        }
        #endregion

        #region Public Overrides
        /// <summary>
        ///  Provides hit test logic.
        /// </summary>
        /// <param name="pt">A point to hit-test.</param>
        /// <returns> A Cursor if the Glyph is associated with p; otherwise, a null reference.</returns>
        public override Cursor GetHitTest(Point pt)
        {
            if (_splitContainer != null)
            {
                Rectangle bounds = Bounds;

                // Is the point inside the bounds of the split container?
                if (bounds.Contains(pt))
                {
                    // Convert from adorner coordinates to the control client coordinates
                    Point splitPt = new Point(pt.X - bounds.X, pt.Y - bounds.Y);

                    // Ask the split container for the correct cursor to use
                    return _splitContainer.DesignGetHitTest(splitPt);
                }
            }

            return null;
        }

        /// <summary>
        ///  Provides paint logic.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        public override void Paint(PaintEventArgs e)
        {
            // We never need to paint over the control itself
        }

        /// <summary>
        ///  Gets the bounds of the Glyph.
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                if (_splitContainer != null)
                {
                    // Find the location of the control inside the adnorner window
                    Point location = _behaviorService.ControlToAdornerWindow(_splitContainer);

                    // The bounds of the glyph match the control exactly, the returned rectangle
                    // is specified in adorner window coordinates, hence the need to use the
                    // behavior service to find the control location in adorner coordinates.
                    return new Rectangle(location, _splitContainer.Size);

                }
                else
                    return Rectangle.Empty;
            }
        }
        #endregion

        #region Implementation
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (_splitContainer != null)
            {
                // Make sure there is no splitter movement occuring
                _splitContainer.DesignAbortMoving();

                // Is this the primary selection now?
                if (ReferenceEquals(_selectionService.PrimarySelection, _splitContainer))
                    _adorner.Enabled = true;
                else
                    _adorner.Enabled = false;
            }
        }
        #endregion
    }
}
