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
    /// Extends the KryptonWorkspace to work within the docking floating window.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonFloatspace : KryptonSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonFloatspace class.
        /// </summary>
        public KryptonFloatspace()
            : base("Floating")
        {
        }

        /// <summary>
        /// Gets a string representation of the class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "KryptonFloatspace " + Dock.ToString();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets a value indicating if docking specific pin actions should be applied.
        /// </summary>
        protected override bool ApplyDockingPinAction
        {
            get { return false; }
        }      
        #endregion
    }
}
