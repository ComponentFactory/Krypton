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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonForm to act as a floating window within the docking framework.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonFloatingWindow : KryptonForm
    {
        #region Static Fields
        private static readonly Image EMPTY_IMAGE = new Bitmap(1, 1);
        #endregion

        #region Instance Fields
        private KryptonFloatspace _floatspace;
        private IFloatingMessages _floatingMessages;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the window close is requested and provides the set of pages visible.
        /// </summary>
        public event EventHandler<UniqueNamesEventArgs> WindowCloseClicked;

        /// <summary>
        /// Occurs when the window needs to be drag and dropped by its caption.
        /// </summary>
        public event EventHandler<ScreenAndOffsetEventArgs> WindowCaptionDragging;
        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonFloatingWindow class.
        /// </summary>
        /// <param name="owner">Reference to form that will own all the floating window.</param>
        /// <param name="floatspace">Reference to owning floatspace instance.</param>
        public KryptonFloatingWindow(Form owner, KryptonFloatspace floatspace)
        {
            // Set the owner of the window so that minimizing the owner will do the same to this
            Owner = owner;

            // Set correct form settings for a floating window
            TopLevel = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.Manual;
            ButtonSpecMin.ImageStates.ImageDisabled = EMPTY_IMAGE;

            // Hook into floatspace events and add as the content of the floating window
            _floatspace = floatspace;
            _floatspace.CellCountChanged += new EventHandler(OnFloatspaceCellCountChanged);
            _floatspace.CellVisibleCountChanged += new EventHandler(OnFloatspaceCellVisibleCountChanged);
            _floatspace.WorkspaceCellAdding += new EventHandler<WorkspaceCellEventArgs>(OnFloatspaceCellAdding);
            _floatspace.WorkspaceCellRemoved += new EventHandler<WorkspaceCellEventArgs>(OnFloatspaceCellRemoved);
            Controls.Add(_floatspace);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the contained KryptonFloatspace control.
        /// </summary>
        public KryptonFloatspace FloatspaceControl
        {
            get { return _floatspace; }
        }
        #endregion

        #region Protected
		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows Message to process. </param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_NCLBUTTONDOWN:
                    {
                        // Perform a hit test to determine which area the mouse press is over at the moment
                        uint result = PI.SendMessage(this.Handle, (int)PI.WM_NCHITTEST, 0, (uint)m.LParam);

                        // Only want to override the behaviour of moving the window via the caption bar
                        if (result == PI.HITTEST_CAPTION)
                        {
                            // Extract screen position of the mouse from the message LPARAM
                            Point screenPos = new Point((short)((uint)m.LParam & 0x0000FFFFU),
                                                        (short)(uint)(((uint)m.LParam & 0xFFFF0000U) >> 16));

                            // Find the mouse offset relative to the top left of the window
                            Point offset = new Point(screenPos.X - Location.X, screenPos.Y - Location.Y);

                            // Do not intercept message if inside the max/close buttons
                            if (!HitTestMaxButton(offset) && !HitTestCloseButton(offset))
                            {
                                // Capture the mouse until the mouse us is received and gain focus so we look active
                                Capture = true;
                                Activate();

                                // Use event to notify the request to drag the window
                                OnWindowCaptionDragging(new ScreenAndOffsetEventArgs(screenPos, offset));

                                // Eat the message!
                                return;
                            }
                        }
                    }
                    break;
                case PI.WM_KEYDOWN:
                    base.WndProc(ref m);
                    if (_floatingMessages != null)
                        _floatingMessages.OnKEYDOWN(ref m);
                    return;
                case PI.WM_MOUSEMOVE:
                    base.WndProc(ref m);
                    if (_floatingMessages != null)
                        _floatingMessages.OnMOUSEMOVE();
                    return;
                case PI.WM_LBUTTONUP:
                    base.WndProc(ref m);
                    if (_floatingMessages != null)
                        _floatingMessages.OnLBUTTONUP();
                    return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Raises the WindowCloseClicked event.
        /// </summary>
        /// <param name="e">An UniqueNamesEventArgs that contains the event data.</param>
        protected virtual void OnWindowCloseClicked(UniqueNamesEventArgs e)
        {
            if (WindowCloseClicked != null)
                WindowCloseClicked(this, e);
        }

        /// <summary>
        /// Raises the WindowCaptionDragging event.
        /// </summary>
        /// <param name="e">An ScreenAndOffsetEventArgs that contains the event data.</param>
        protected virtual void OnWindowCaptionDragging(ScreenAndOffsetEventArgs e)
        {
            if (WindowCaptionDragging != null)
                WindowCaptionDragging(this, e);
        }

        /// <summary>
        /// Raises the Load event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            BeginInvoke(new EventHandler(OnLayoutWorkspace));
            base.OnLoad(e);
        }

        /// <summary>
        /// Raises the Closing event.
        /// </summary>
        /// <param name="e">An CancelEventArgs that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            // Prevent user from closing by using the close button or close method
            e.Cancel = true;

            // Generate event so handlers to perform appropriate processing
            string[] uniqueNames = VisibleCloseableUniqueNames();
            if (uniqueNames.Length > 0)
                OnWindowCloseClicked(new UniqueNamesEventArgs(uniqueNames));

            base.OnClosing(e);
        }

        /// <summary>
        /// Raises the Activated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            CommonHelper.ActiveFloatingWindow = this;
            base.OnActivated(e);
        }

        /// <summary>
        /// Raises the Deactivate event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnDeactivate(EventArgs e)
        {
            CommonHelper.ActiveFloatingWindow = null;
            base.OnDeactivate(e);
        }
        #endregion

        #region Internal
        /// <summary>
        /// Gets and sets the floating messages interface.
        /// </summary>
        internal IFloatingMessages FloatingMessages
        {
            get { return _floatingMessages; }
            set { _floatingMessages = value; }
        }
        #endregion

        #region Implementation
        private void OnFloatspaceCellCountChanged(object sender, EventArgs e)
        {
            // When all the cells (and so pages) have been removed we kill ourself
            if (FloatspaceControl.CellCount == 0)
                FloatspaceControl.Dispose();
        }

        private void OnFloatspaceCellVisibleCountChanged(object sender, EventArgs e)
        {
            UpdateCellSettings();
        }

        private void OnTabVisibleCountChanged(object sender, EventArgs e)
        {
            UpdateCellSettings();
        }

        private void OnFloatspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            e.Cell.TabVisibleCountChanged += new EventHandler(OnTabVisibleCountChanged);
        }

        private void OnFloatspaceCellRemoved(object sender, WorkspaceCellEventArgs e)
        {
            e.Cell.TabVisibleCountChanged -= new EventHandler(OnTabVisibleCountChanged);
        }

        private void OnLayoutWorkspace(object sender, EventArgs e)
        {
            FloatspaceControl.PerformNeedPaint(true);
        }

        private void UpdateCellSettings()
        {
            KryptonWorkspaceCell cell = FloatspaceControl.FirstVisibleCell();
            if (cell != null)
            {
                // If there is only a single cell inside the floating window
                if (FloatspaceControl.CellVisibleCount <= 1)
                {
                    // Cell display mode depends on the number of tabs in the cell
                    if (cell.Pages.VisibleCount == 1)
                        cell.NavigatorMode = NavigatorMode.HeaderGroup;
                    else
                        cell.NavigatorMode = NavigatorMode.HeaderGroupTab;
                }
                else
                {
                    do
                    {
                        // With multiple cells we always need the tabs showing
                        cell.NavigatorMode = NavigatorMode.HeaderGroupTab;
                        cell = FloatspaceControl.NextVisibleCell(cell);
                    }
                    while (cell != null);
                }
            }

            // Only show the floating window if there is a visible cell
            Visible = (FloatspaceControl.CellVisibleCount > 0);
        }

        private string[] VisibleCloseableUniqueNames()
        {
            List<string> uniqueNames = new List<string>();
            KryptonWorkspaceCell cell = FloatspaceControl.FirstVisibleCell();
            while (cell != null)
            {
                // Create a list of all the visible page names in the floatspace that are allowed to be closed
                foreach (KryptonPage page in cell.Pages)
                    if (page.LastVisibleSet && page.AreFlagsSet(KryptonPageFlags.DockingAllowClose))
                        uniqueNames.Add(page.UniqueName);

                cell = FloatspaceControl.NextVisibleCell(cell);
            }

            return uniqueNames.ToArray();
        }
        #endregion
    }
}
