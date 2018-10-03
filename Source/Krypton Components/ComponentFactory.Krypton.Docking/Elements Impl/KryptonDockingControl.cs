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
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Provides docking functionality for a control instance.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingControl : DockingElementOpenCollection
    {
        #region Static Fields
        private static readonly Size INNER_MINIMUM = new Size(100, 100);
        #endregion

        #region Instance Fields
        private Control _control;
        private ObscureControl _obscure;
        private IDockingElement _innerElement;
        private Size _innerMinimum;
        private int _updateCount;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingControl class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="control">Reference to control derived instance.</param>
        public KryptonDockingControl(string name, Control control)
            : base(name)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            Construct(control, null);
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDockingControl class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="control">Reference to control derived instance.</param>
        /// <param name="navigator">Inner space occupied by a KryptonDockingNavigator.</param>
        public KryptonDockingControl(string name, Control control, KryptonDockingNavigator navigator)
            : base(name)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            if (navigator == null)
                throw new ArgumentNullException("navigator");

            Construct(control, navigator);
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDockingControl class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="control">Reference to control derived instance.</param>
        /// <param name="workspace">Inner space occupied by a KryptonDockingNavigator.</param>
        public KryptonDockingControl(string name, Control control, KryptonDockingWorkspace workspace)
            : base(name)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            if (workspace == null)
                throw new ArgumentNullException("workspace");

            Construct(control, workspace);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the control this element is managing.
        /// </summary>
        public Control Control
        {
            get { return _control; }
        }

        /// <summary>
        /// Gets and sets the minimum size for the inner area of the control that docking should not overlap.
        /// </summary>
        public Size InnerMinimum
        {
            get { return _innerMinimum; }

            set
            {
                if (_innerMinimum != value)
                {
                    _innerMinimum = value;
                    EnforceInnerMinimum();
                }
            }
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
        public override void PropogateAction(DockingPropogateAction action, string[] uniqueNames)
        {
            switch (action)
            {
                case DockingPropogateAction.StartUpdate:
                    // Only the first of several 'StartUpdate' actions needs actioning
                    if (_updateCount++ == 0)
                    {
                        // Place the obscuring control at the top of the z-order
                        Control.Controls.SetChildIndex(_obscure, 0);

                        // Set obscuring control to take up entire client area and be made visible, this prevents
                        // the drawing of any control underneath it and so prevents any drawing artifacts being seen
                        // until the end of all operations resulting from the request action.
                        _obscure.SetBounds(0, 0, Control.Width, Control.Height);
                        _obscure.Visible = true;
                    }
                    break;
                case DockingPropogateAction.EndUpdate:
                    // Only final matching 'EndUpdate' needs to reverse start action
                    if ((_updateCount > 0) && (_updateCount-- == 1))
                    {
                        // Multi operation might have caused a change in the inner minimum
                        EnforceInnerMinimum();

                        _obscure.Visible = false;
                    }
                    break;
                case DockingPropogateAction.ShowPages:
                case DockingPropogateAction.ShowAllPages:
                    // Let base class perform actual requested actions
                    base.PropogateAction(action, uniqueNames);

                    // Ensure that showing extra pages does not trespass on the inner minimum
                    if ((action == DockingPropogateAction.ShowPages) ||
                        (action == DockingPropogateAction.ShowAllPages))
                        EnforceInnerMinimum();
                    break;
                default:
                    // Let base class perform actual requested actions
                    base.PropogateAction(action, uniqueNames);
                    break;
            }
        }

        /// <summary>
        /// Propogates a request for drag targets down the hierarchy of docking elements.
        /// </summary>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="dragData">Set of pages being dragged.</param>
        /// <param name="targets">Collection of drag targets.</param>
        public override void PropogateDragTargets(KryptonFloatingWindow floatingWindow,
                                                  PageDragEndData dragData,
                                                  DragTargetList targets)
        {
            // Create a list of pages that are allowed to be transferred into a dockspace
            List<KryptonPage> transferPages = new List<KryptonPage>();
            foreach (KryptonPage page in dragData.Pages)
                if (page.AreFlagsSet(KryptonPageFlags.DockingAllowDocked))
                    transferPages.Add(page);

            // Only generate targets if we have some valid pages to transfer
            if (transferPages.Count > 0)
            {
                // Generate targets for the four control edges
                Rectangle screenRect = Control.RectangleToScreen(Control.ClientRectangle);
                Rectangle[] rectsDraw = SubdivideRectangle(screenRect, 3, int.MaxValue);
                Rectangle[] rectsHot = SubdivideRectangle(screenRect, 10, 20);

                // Must insert at start of target list as they are higher priority than cell targets
                targets.Add(new DragTargetControlEdge(screenRect, rectsHot[0], rectsDraw[0], DragTargetHint.EdgeLeft | DragTargetHint.ExcludeCluster, this, KryptonPageFlags.DockingAllowDocked, true));
                targets.Add(new DragTargetControlEdge(screenRect, rectsHot[1], rectsDraw[1], DragTargetHint.EdgeRight | DragTargetHint.ExcludeCluster, this, KryptonPageFlags.DockingAllowDocked, true));
                targets.Add(new DragTargetControlEdge(screenRect, rectsHot[2], rectsDraw[2], DragTargetHint.EdgeTop | DragTargetHint.ExcludeCluster, this, KryptonPageFlags.DockingAllowDocked, true));
                targets.Add(new DragTargetControlEdge(screenRect, rectsHot[3], rectsDraw[3], DragTargetHint.EdgeBottom | DragTargetHint.ExcludeCluster, this, KryptonPageFlags.DockingAllowDocked, true));

                // If we have no designated inner element when we have to decide if we can place edge drag drop targets based on the
                // available space at the center of the control after taking into account any edge docked controls already in place.
                if (_innerElement == null)
                {
                    // Find the inner rectangle after taking docked controls into account 
                    Size tl = Size.Empty;
                    Size br = Control.ClientSize;
                    foreach (Control c in Control.Controls)
                        if (c.Visible)
                        {
                            switch (c.Dock)
                            {
                                case DockStyle.Left:
                                    tl.Width = Math.Max(tl.Width, c.Right);
                                    break;
                                case DockStyle.Right:
                                    br.Width = Math.Min(br.Width, c.Left);
                                    break;
                                case DockStyle.Top:
                                    tl.Height = Math.Max(tl.Height, c.Bottom);
                                    break;
                                case DockStyle.Bottom:
                                    br.Height = Math.Min(br.Height, c.Top);
                                    break;
                            }
                        }

                    // If there is inner space available
                    Rectangle innerRect = new Rectangle(tl.Width, tl.Height, br.Width - tl.Width, br.Height - tl.Height);
                    if ((innerRect.Width > 0) && (innerRect.Height > 0))
                    {
                        Rectangle innerScreenRect = Control.RectangleToScreen(innerRect);
                        Rectangle[] innerRectsDraw = SubdivideRectangle(innerScreenRect, 3, int.MaxValue);
                        Rectangle[] innerRectsHot = SubdivideRectangle(innerScreenRect, 10, 20);
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[0], innerRectsDraw[0], DragTargetHint.EdgeLeft, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[1], innerRectsDraw[1], DragTargetHint.EdgeRight, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[2], innerRectsDraw[2], DragTargetHint.EdgeTop, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[3], innerRectsDraw[3], DragTargetHint.EdgeBottom, this, KryptonPageFlags.DockingAllowDocked, false));
                    }
                }
                else if (_innerElement is KryptonDockingNavigator)
                {
                    KryptonDockingNavigator dockingNavigator = (KryptonDockingNavigator)_innerElement;

                    // If there is inner space available
                    Rectangle innerScreenRect = dockingNavigator.DockableNavigatorControl.RectangleToScreen(dockingNavigator.DockableNavigatorControl.ClientRectangle);
                    if ((innerScreenRect.Width > 0) && (innerScreenRect.Height > 0))
                    {
                        Rectangle[] innerRectsDraw = SubdivideRectangle(innerScreenRect, 3, int.MaxValue);
                        Rectangle[] innerRectsHot = SubdivideRectangle(innerScreenRect, 10, 20);
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[0], innerRectsDraw[0], DragTargetHint.EdgeLeft, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[1], innerRectsDraw[1], DragTargetHint.EdgeRight, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[2], innerRectsDraw[2], DragTargetHint.EdgeTop, this, KryptonPageFlags.DockingAllowDocked, false));
                        targets.Add(new DragTargetControlEdge(innerScreenRect, innerRectsHot[3], innerRectsDraw[3], DragTargetHint.EdgeBottom, this, KryptonPageFlags.DockingAllowDocked, false));
                    }
                }

                // Let base class generate targets for contained elements
                base.PropogateDragTargets(floatingWindow, dragData, targets);
            }
        }        
        #endregion

        #region Protected
        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName
        {
            get { return "DC"; }
        }

        /// <summary>
        /// Loads docking configuration information using a provider xml reader.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages for adding.</param>
        public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
        {
            // Let base class perform loading of all docking elements
            base.LoadElementFromXml(xmlReader, pages);

            // Find the largest ordering value for all dockspace controls
            int largestOrder = -1;
            PropogateIntState(DockingPropogateIntState.DockspaceOrder, ref largestOrder);

            if (largestOrder > 0)
            {
                // Use upper limit to prevent crazy values causing long delays
                largestOrder = Math.Min(largestOrder, 30);
            
                // Request each dockspace in ordering sequence reposition itself
                for (int i = 0; i <= largestOrder; i++)
                    PropogateAction(DockingPropogateAction.RepositionDockspace, i);
            }
        }
        #endregion

        #region Implementation
        private void Construct(Control control, IDockingElement innerElement)
        {
            _innerElement = innerElement;
            _innerMinimum = INNER_MINIMUM;

            // Hook into events on the target control
            _control = control;
            _control.SizeChanged += new EventHandler(OnControlSizeChanged);
            _control.Disposed += new EventHandler(OnControlDisposed);

            // Create and add a control we use to obscure the client area during multi-part operations
            _obscure = new ObscureControl();
            _obscure.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            _obscure.Visible = false;
            _control.Controls.Add(_obscure);

            // Create docking elements for managing each of the four control edges
            Add(new KryptonDockingEdge("Top", control, DockingEdge.Top));
            Add(new KryptonDockingEdge("Bottom", control, DockingEdge.Bottom));
            Add(new KryptonDockingEdge("Left", control, DockingEdge.Left));
            Add(new KryptonDockingEdge("Right", control, DockingEdge.Right));
        }
        
        private void OnControlDisposed(object sender, EventArgs e)
        {
            // Unhook events to allow garbage collection
            _control.SizeChanged -= new EventHandler(OnControlSizeChanged);
            _control.Disposed -= new EventHandler(OnControlDisposed);
        }

        private void OnControlSizeChanged(object sender, EventArgs e)
        {
            if (!Control.Size.IsEmpty)
            {
                Form ownerForm = Control.FindForm();

                // When the control is inside a minimized form we do not enforce the minimum
                if ((ownerForm != null) && (ownerForm.WindowState != FormWindowState.Minimized))
                    EnforceInnerMinimum();
            }
        }

        private void EnforceInnerMinimum()
        {
            // Find the available inner rectangle of our containing control
            Rectangle innerRect = DockingHelper.InnerRectangle(Control);

            // Do we need to adjust the left/right edge controls?
            if (innerRect.Width < InnerMinimum.Width)
                EnforceInnerMinimum(InnerMinimum.Width - innerRect.Width, Orientation.Horizontal);

            // Do we need to adjust the top/bottom edge controls?
            if (innerRect.Height < InnerMinimum.Height)
                EnforceInnerMinimum(InnerMinimum.Height - innerRect.Height, Orientation.Vertical);
        }

        private void EnforceInnerMinimum(int remove, Orientation orientation)
        {
            // Create a list of all the dockspace controls in our orientation and a matching array 
            // of lengths that will be used in the final phase of updating the control sizes
            List<KryptonDockspace> controls = new List<KryptonDockspace>();
            foreach (Control c in Control.Controls)
            {
                KryptonDockspace dockspace = c as KryptonDockspace;
                if ((dockspace != null) && c.Visible)
                {
                    switch (c.Dock)
                    {
                        case DockStyle.Left:
                        case DockStyle.Right:
                            if (orientation == Orientation.Horizontal)
                                controls.Add(dockspace);
                            break;
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            if (orientation == Orientation.Vertical)
                                controls.Add(dockspace);
                            break;
                    }
                }
            }

            // Keep trying to remove size from controls until we have removed all 
            // needed space or have no more controls that can be reduced in size
            while ((remove > 0) && (controls.Count > 0))
            {
                // How much space to remove from each control
                int delta = Math.Max(1, remove / controls.Count);

                // Try and remove delta from each control
                int dockDelta;
                for (int i = controls.Count - 1; i >= 0; i--)
                {
                    KryptonDockspace dockspace = controls[i];

                    // Find how much we can subtract from the dockspace without violating the minimum size
                    if (orientation == Orientation.Horizontal)
                        dockDelta = dockspace.Width - Math.Max(dockspace.MinimumSize.Width, dockspace.Width - delta);
                    else
                        dockDelta = dockspace.Height - Math.Max(dockspace.MinimumSize.Height, dockspace.Height - delta);

                    // We cannot remove any more from the dockspace control, then we are done with that control
                    if (dockDelta == 0)
                        controls.Remove(dockspace);
                    else
                    {
                        // Reduce the dockspace size
                        if (orientation == Orientation.Horizontal)
                            dockspace.Width -= dockDelta;
                        else
                            dockspace.Height -= dockDelta;

                        // Update total amount to be removed
                        remove -= dockDelta;
                    }
                }
            }
        }

        private Rectangle[] SubdivideRectangle(Rectangle area,
                                               int divisor,
                                               int maxLength)
        {
            int length = Math.Min(area.Width / divisor, Math.Min(area.Height / divisor, maxLength));

            // Find the left, right, top, bottom, center rectangles
            return new Rectangle[]{ new Rectangle(area.X, area.Y, length, area.Height),
                                    new Rectangle(area.Right - length, area.Y, length, area.Height),
                                    new Rectangle(area.X, area.Y, area.Width, length),
                                    new Rectangle(area.X, area.Bottom - length, area.Width, length),
                                    new Rectangle(area.X + length, area.Y + length, 
                                                  area.Width - length * 2, area.Height - length * 2)};
        }

        private void DebugOutput(string title)
        {
            Console.WriteLine("\n{0}", title);
            foreach (Control c in Control.Controls)
                Console.WriteLine("    {0} {1} {2} {3}", c.GetType().Name, c.Visible, c.Size, c.Dock);
        }
        #endregion
    }
}
