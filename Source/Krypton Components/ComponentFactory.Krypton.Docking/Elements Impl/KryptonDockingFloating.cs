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
    /// Provides docking functionality for floating windows.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingFloating : DockingElementClosedCollection
    {
        #region Instance Fields
        private Form _ownerForm;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingFloating class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="ownerForm">Reference to form that will own all the floating windows.</param>
        public KryptonDockingFloating(string name, Form ownerForm)
            : base(name)
        {
            if (ownerForm == null)
                throw new ArgumentNullException("owner");

            _ownerForm = ownerForm;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the form the floating windows have as the owner.
        /// </summary>
        public Form OwnerForm
        {
            get { return _ownerForm; }
        }

        /// <summary>
        /// Create and add a new floating window.
        /// </summary>
        /// <returns>Reference to docking element that handles the new workspace.</returns>
        public KryptonDockingFloatingWindow AddFloatingWindow()
        {
            // Generate a unique string by creating a GUID
            return AddFloatingWindow(CommonHelper.UniqueString);
        }

        /// <summary>
        /// Create and add a new floating window.
        /// </summary>
        /// <param name="name">Initial name of the dockspace element.</param>
        /// <returns>Reference to docking element that handles the new workspace.</returns>
        public KryptonDockingFloatingWindow AddFloatingWindow(string name)
        {
            return CreateFloatingWindow(name);
        }

        /// <summary>
        /// Find a floating docking element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable floating element is required.</param>
        /// <returns>KryptonDockingFloating reference if found; otherwise false.</returns>
        public override KryptonDockingFloating FindDockingFloating(string uniqueName)
        {
            return this;
        }

        /// <summary>
        /// Return the floating window element that contains a placeholder for the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name for search.</param>
        /// <returns>Reference to KryptonDockingFloatingWindow if placeholder found; otherwise null.</returns>
        public KryptonDockingFloatingWindow FloatingWindowForStorePage(string uniqueName)
        {
            // Search all the child docking elements
            foreach (IDockingElement child in this)
            {
                // Only interested in floating window elements
                KryptonDockingFloatingWindow floatingWindow = child as KryptonDockingFloatingWindow;
                if (floatingWindow != null)
                {
                    bool? ret = floatingWindow.PropogateBoolState(DockingPropogateBoolState.ContainsStorePage, uniqueName);
                    if (ret.HasValue && ret.Value)
                        return floatingWindow;
                }
            }

            return null;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName
        {
            get { return "DF"; }
        }

        /// <summary>
        /// Perform docking element specific actions for loading a child xml.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages.</param>
        /// <param name="child">Optional reference to existing child docking element.</param>
        protected override void LoadChildDockingElement(XmlReader xmlReader,
                                                        KryptonPageCollection pages,
                                                        IDockingElement child)
        {
            if (child != null)
                child.LoadElementFromXml(xmlReader, pages);
            else
            {
                // Create a new floating window and then reload it
                KryptonDockingFloatingWindow floatingWindow = AddFloatingWindow(xmlReader.GetAttribute("N"));
                floatingWindow.LoadElementFromXml(xmlReader, pages);
            }
        }
        #endregion

        #region Implementation
        private KryptonDockingFloatingWindow CreateFloatingWindow(string name)
        {
            // Create a floatspace and floating window for hosting the floatspace
            KryptonDockingFloatspace floatSpaceElement = new KryptonDockingFloatspace("Floatspace");
            KryptonDockingFloatingWindow floatingWindowElement = new KryptonDockingFloatingWindow(name, OwnerForm, floatSpaceElement);
            floatingWindowElement.Disposed += new EventHandler(OnDockingFloatingWindowDisposed);
            InternalAdd(floatingWindowElement);

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // Generate events so the floating window/dockspace appearance can be customized
                FloatingWindowEventArgs floatingWindowArgs = new FloatingWindowEventArgs(floatingWindowElement.FloatingWindow, floatingWindowElement);
                FloatspaceEventArgs floatSpaceArgs = new FloatspaceEventArgs(floatSpaceElement.FloatspaceControl, floatSpaceElement);
                dockingManager.RaiseFloatingWindowAdding(floatingWindowArgs);
                dockingManager.RaiseFloatspaceAdding(floatSpaceArgs);
            }

            return floatingWindowElement;
        }

        private void OnDockingFloatingWindowDisposed(object sender, EventArgs e)
        {
            // Cast to correct type and unhook event handlers so garbage collection can occur
            KryptonDockingFloatingWindow floatingWindowElement = (KryptonDockingFloatingWindow)sender;
            floatingWindowElement.Disposed -= new EventHandler(OnDockingFloatingWindowDisposed);

            // Remove the elemenet from our child collection as it is no longer valid
            InternalRemove(floatingWindowElement);
        }
        #endregion
    }
}
