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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implement storage for palette border edge details.
    /// </summary>
    public class PaletteBorderEdgeRedirect : PaletteBack,
                                             IPaletteBorder
    {
        #region Internal Classes
        internal class BackToBorder : IPaletteBack
        {
            #region Instance Fields
            private IPaletteBorder _parent;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the BackToBorder class.
            /// </summary>
            /// <param name="parent">Parent to get border values from.</param>
            public BackToBorder(IPaletteBorder parent)
            {
                Debug.Assert(parent != null);
                _parent = parent;
            }
            #endregion

            #region IPaletteBack
            /// <summary>
            /// Gets the actual background draw value.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>InheritBool value.</returns>
            public InheritBool GetBackDraw(PaletteState state)
            {
                return _parent.GetBorderDraw(state);
            }

            /// <summary>
            /// Gets the actual background graphics hint value.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>PaletteGraphicsHint value.</returns>
            public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
            {
                // We never want the border edge to use anti aliasing
                return PaletteGraphicsHint.None;
            }

            /// <summary>
            /// Gets the first background color.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Color value.</returns>
            public Color GetBackColor1(PaletteState state)
            {
                return _parent.GetBorderColor1(state);
            }

            /// <summary>
            /// Gets the second back color.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Color value.</returns>
            public Color GetBackColor2(PaletteState state)
            {
                return _parent.GetBorderColor2(state);
            }

            /// <summary>
            /// Gets the color drawing style.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Color drawing style.</returns>
            public PaletteColorStyle GetBackColorStyle(PaletteState state)
            {
                return _parent.GetBorderColorStyle(state);
            }

            /// <summary>
            /// Gets the color alignment style.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Color alignment style.</returns>
            public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
            {
                return _parent.GetBorderColorAlign(state);
            }

            /// <summary>
            /// Gets the color background angle.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Angle used for color drawing.</returns>
            public float GetBackColorAngle(PaletteState state)
            {
                return _parent.GetBorderColorAngle(state);
            }

            /// <summary>
            /// Gets a background image.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Image instance.</returns>
            public Image GetBackImage(PaletteState state)
            {
                return _parent.GetBorderImage(state);
            }

            /// <summary>
            /// Gets the background image style.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Image style value.</returns>
            public PaletteImageStyle GetBackImageStyle(PaletteState state)
            {
                return _parent.GetBorderImageStyle(state);
            }

            /// <summary>
            /// Gets the image alignment style.
            /// </summary>
            /// <param name="state">Palette value should be applicable to this state.</param>
            /// <returns>Image alignment style.</returns>
            public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
            {
                return _parent.GetBorderImageAlign(state);
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private IPaletteBorder _inherit;
        private BackToBorder _translate;
        private int _borderWidth;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorderEdge class.
        /// </summary>
        /// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteBorderEdgeRedirect(IPaletteBorder inherit,
                                         NeedPaintHandler needPaint)
            : base(null, needPaint)
        {
            // Remember inheritance
            _inherit = inherit;

            // Default properties
            _borderWidth = -1;

            // Create the helper object that passes background
            // requests from the base clas and converts them into
            // border requests on ourself.
            _translate = new BackToBorder(this);
            SetInherit(_translate);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get { return (_borderWidth == -1) && base.IsDefault; }
        }
        #endregion

        #region Width
        /// <summary>
        /// Gets and sets the border width.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border width.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int Width
        {
            get { return _borderWidth; }

            set
            {
                if (value != _borderWidth)
                {
                    _borderWidth = value;
                    PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region SetPalettes
        /// <summary>
        /// Update the source palettes for drawing.
        /// </summary>
        /// <param name="paletteBorder">Palette source for the border.</param>
        public virtual void SetPalette(IPaletteBorder paletteBorder)
        {
            _inherit = paletteBorder;
        }
        #endregion

        #region IPaletteBorder
        /// <summary>
        /// Gets the actual border draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBorderDraw(PaletteState state)
        {
            return _inherit.GetBorderDraw(state);
        }

        /// <summary>
        /// Gets the actual borders to draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public PaletteDrawBorders GetBorderDrawBorders(PaletteState state)
        {
            return _inherit.GetBorderDrawBorders(state);
        }

        /// <summary>
        /// Gets the actual border graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state)
        {
            return _inherit.GetBorderGraphicsHint(state);
        }

        /// <summary>
        /// Gets the actual first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor1(PaletteState state)
        {
            return _inherit.GetBorderColor1(state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor2(PaletteState state)
        {
            return _inherit.GetBorderColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBorderColorStyle(PaletteState state)
        {
            return _inherit.GetBorderColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBorderColorAlign(PaletteState state)
        {
            return _inherit.GetBorderColorAlign(state);
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBorderColorAngle(PaletteState state)
        {
            return _inherit.GetBorderColorAngle(state);
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border width.</returns>
        public int GetBorderWidth(PaletteState state)
        {
            if (Width != -1)
                return Width;
            else
                return _inherit.GetBorderWidth(state);
        }

        /// <summary>
        /// Gets the border rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border rounding.</returns>
        public int GetBorderRounding(PaletteState state)
        {
            return _inherit.GetBorderRounding(state);
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBorderImage(PaletteState state)
        {
            return _inherit.GetBorderImage(state);
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBorderImageStyle(PaletteState state)
        {
            return _inherit.GetBorderImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBorderImageAlign(PaletteState state)
        {
            return _inherit.GetBorderImageAlign(state);
        }
        #endregion
    }
}
