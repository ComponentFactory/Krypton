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
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Acts as a proxy for a KryptonPage inside a auto hidden group.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonAutoHiddenProxyPage : KryptonPage
    {
        #region Instance Fields
        private KryptonPage _page;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonAutoHiddenProxyPage class.
        /// </summary>
        public KryptonAutoHiddenProxyPage(KryptonPage page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            // We are a proxy for this cached page reference
            _page = page;

            // Text property was updated by the base class constructor, so now we update the actual referenced class
            _page.Text = Text;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (_page != null)
                _page.Dispose();

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a reference to the page for which this is a proxy.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }

        /// <summary>
        /// Gets and sets the page text.
        /// </summary>
        public override string Text
        {
            get 
            {
                if (Page != null)
                    return Page.Text;
                else
                    return base.Text;
            }
            
            set 
            {
                base.Text = value;
                if (Page != null)
                    Page.Text = value; 
            }
        }

        /// <summary>
        /// Gets and sets the title text for the page.
        /// </summary>
        public override string TextTitle
        {
            get { return Page.TextTitle; }
            set { Page.TextTitle = value; }
        }

        /// <summary>
        /// Gets and sets the description text for the page.
        /// </summary>
        public override string TextDescription
        {
            get { return Page.TextDescription; }
            set { Page.TextDescription = value; }
        }

        /// <summary>
        /// Gets and sets the small image for the page.
        /// </summary>
        public override Image ImageSmall
        {
            get { return Page.ImageSmall; }
            set { Page.ImageSmall = value; }
        }

        /// <summary>
        /// Gets and sets the medium image for the page.
        /// </summary>
        public override Image ImageMedium
        {
            get { return Page.ImageMedium; }
            set { Page.ImageMedium = value; }
        }

        /// <summary>
        /// Gets and sets the large image for the page.
        /// </summary>
        public override Image ImageLarge
        {
            get { return Page.ImageLarge; }
            set { Page.ImageLarge = value; }
        }

        /// <summary>
        /// Gets and sets the page tooltip image.
        /// </summary>
        public override Image ToolTipImage
        {
            get { return Page.ToolTipImage; }
            set { Page.ToolTipImage = value; }
        }

        /// <summary>
        /// Gets and sets the tooltip image transparent color.
        /// </summary>
        public override Color ToolTipImageTransparentColor
        {
            get { return Page.ToolTipImageTransparentColor; }
            set { Page.ToolTipImageTransparentColor = value; }
        }

        /// <summary>
        /// Gets and sets the page tooltip title text.
        /// </summary>
        public override string ToolTipTitle
        {
            get { return Page.ToolTipTitle; }
            set { Page.ToolTipTitle = value; }
        }

        /// <summary>
        /// Gets and sets the page tooltip body text.
        /// </summary>
        public override string ToolTipBody
        {
            get { return Page.ToolTipBody; }
            set { Page.ToolTipBody = value; }
        }

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        public override LabelStyle ToolTipStyle
        {
            get { return Page.ToolTipStyle; }
            set { Page.ToolTipStyle = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        public override KryptonContextMenu KryptonContextMenu
        {
            get { return Page.KryptonContextMenu; }
            set { Page.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets and sets the unique name of the page.
        /// </summary>
        public override string UniqueName
        {
            get { return Page.UniqueName; }
            set { Page.UniqueName = value; }
        }

        /// <summary>
        /// Gets the string that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Text mapping.</param>
        /// <returns>Matching string.</returns>
        public override string GetTextMapping(MapKryptonPageText mapping)
        {
            return Page.GetTextMapping(mapping);
        }

        /// <summary>
        /// Gets the image that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Image mapping.</param>
        /// <returns>Image reference.</returns>
        public override Image GetImageMapping(MapKryptonPageImage mapping)
        {
            return Page.GetImageMapping(mapping);
        }

        /// <summary>
        /// Gets and sets the set of page flags.
        /// </summary>
        public override int Flags
        {
            get { return Page.Flags; }
            set { Page.Flags = value; }
        }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void SetFlags(KryptonPageFlags flags)
        {
            Page.SetFlags(flags);
        }

        /// <summary>
        /// Sets all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public override void ClearFlags(KryptonPageFlags flags)
        {
            Page.ClearFlags(flags);
        }

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all provided flags are defined as true; otherwise false.</returns>
        public override bool AreFlagsSet(KryptonPageFlags flags)
        {
            return Page.AreFlagsSet(flags);
        }

        /// <summary>
        /// Gets the last value set to the Visible property.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override bool LastVisibleSet
        {
            get { return Page.LastVisibleSet; }
            set { Page.LastVisibleSet = value; }
        }
        #endregion
    }
}
