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
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement storage for GroupBox states.
	/// </summary>
	public class PaletteGroupBox : PaletteDouble
	{
		#region Instance Fields
        private PaletteContent _content;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteGroupBox class.
		/// </summary>
        /// <param name="inherit">Source for inheriting palette defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteGroupBox(PaletteGroupBoxRedirect inherit,
                               NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
            _content = new PaletteContent(inherit.PaletteContent, needPaint);
        }
		#endregion

        #region Content
        /// <summary>
        /// Gets access to the content palette details.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining content appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent Content
        {
            get { return _content; }
        }

        private bool ShouldSerializeContent()
        {
            return !_content.IsDefault;
        }

        /// <summary>
        /// Gets the content palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPaletteContent PaletteContent
        {
            get { return Content; }
        }
        #endregion
    }
}
