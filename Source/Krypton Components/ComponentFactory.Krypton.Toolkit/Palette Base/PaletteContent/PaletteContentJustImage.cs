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
    /// Implement storage but remove accesss to the non image properties.
    /// </summary>
    public class PaletteContentJustImage : PaletteContent
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentJustImage class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteContentJustImage(IPaletteContent inherit,
                                       NeedPaintHandler needPaint)
            : base(inherit, needPaint)
        {
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Draw = GetContentDraw(state);
            Image.ImageH = GetContentImageH(state);
            Image.ImageV = GetContentImageV(state);
            Image.Effect = GetContentImageEffect(state);
            Image.ImageColorMap = GetContentImageColorMap(state);
            Image.ImageColorTo = GetContentImageColorTo(state);
            Padding = GetContentPadding(state);
        }
        #endregion

        #region DrawFocus
        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        [KryptonPersist(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override InheritBool DrawFocus
        {
            get { return base.DrawFocus; }
            set { base.DrawFocus = value; }
        }
        #endregion

        #region ShortText
        /// <summary>
        /// Gets access to the short text palette details.
        /// </summary>
        [KryptonPersist]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PaletteContentText ShortText
        {
            get { return base.ShortText; }
        }
        #endregion

        #region LongText
        /// <summary>
        /// Gets access to the long text palette details.
        /// </summary>
        [KryptonPersist]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PaletteContentText LongText
        {
            get { return base.LongText; }
        }
        #endregion

        #region AdjacentGap
        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        [KryptonPersist(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int AdjacentGap
        {
            get { return base.AdjacentGap;  }
            set { base.AdjacentGap = value; }
        }
        #endregion
    }
}
