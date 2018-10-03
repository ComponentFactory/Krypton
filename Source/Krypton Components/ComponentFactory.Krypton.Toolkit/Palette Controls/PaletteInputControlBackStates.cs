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
	/// Implement storage for input control palette background details.
	/// </summary>
	public class PaletteInputControlBackStates : Storage,
								                 IPaletteBack
	{
        #region Instance Fields
        private IPaletteBack _inherit;
        private Color _color1;
        #endregion

        #region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteInputControlBackStates class.
		/// </summary>
		/// <param name="inherit">Source for inheriting defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteInputControlBackStates(IPaletteBack inherit,
                                             NeedPaintHandler needPaint)
		{
			Debug.Assert(inherit != null);

			// Remember inheritance
			_inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default the initial values
            _color1 = Color.Empty;
   		}
		#endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get 
			{
                return (Color1 == Color.Empty);
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteBack inherit)
        {
            _inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            // Get the values and set into storage
            Color1 = GetBackColor1(state);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Gets the actual background draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBackDraw(PaletteState state)
        {
            return _inherit.GetBackDraw(state);
        }
        #endregion

        #region GraphicsHint
        /// <summary>
        /// Gets the actual background graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
        {
            return _inherit.GetBackGraphicsHint(state);
        }
        #endregion

        #region Color1
        /// <summary>
        /// Gets and sets the first background color.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Main background color.")]
        [KryptonDefaultColorAttribute()]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color Color1
        {
            get { return _color1; }

            set
            {
                if (value != _color1)
                {
                    _color1 = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor1(PaletteState state)
        {
            if (Color1 != Color.Empty)
                return Color1;
            else
                return _inherit.GetBackColor1(state);
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor2(PaletteState state)
        {
            return _inherit.GetBackColor2(state);
        }
        #endregion

        #region ColorStyle
        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBackColorStyle(PaletteState state)
        {
            return _inherit.GetBackColorStyle(state);
        }
        #endregion

        #region ColorAlign
        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
        {
            return _inherit.GetBackColorAlign(state);
        }
        #endregion

        #region ColorAngle
        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBackColorAngle(PaletteState state)
        {
            return _inherit.GetBackColorAngle(state);
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBackImage(PaletteState state)
        {
            return _inherit.GetBackImage(state);
        }
        #endregion

        #region ImageStyle
        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBackImageStyle(PaletteState state)
        {
            return _inherit.GetBackImageStyle(state);
        }
        #endregion

        #region ImageAlign
        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
        {
            return _inherit.GetBackImageAlign(state);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the inheritence parent.
        /// </summary>
        protected IPaletteBack Inherit
        {
            get { return _inherit; }
        }
        #endregion
    }
}
