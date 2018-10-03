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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for ribbon background and text values.
	/// </summary>
    public class PaletteRibbonDoubleRedirect : Storage,
                                               IPaletteRibbonBack,
                                               IPaletteRibbonText
    {
        #region Instance Fields
        private Color _backColor1;
        private Color _backColor2;
        private Color _backColor3;
        private Color _backColor4;
        private Color _backColor5;
        private Color _textColor;
        private PaletteRibbonBackInheritRedirect _inheritBack;
        private PaletteRibbonTextInheritRedirect _inheritText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonDoubleRedirect class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="backStyle">Inheritence ribbon back style.</param>
        /// <param name="textStyle">Inheritence ribbon text style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonDoubleRedirect(PaletteRedirect redirect,
                                           PaletteRibbonBackStyle backStyle,
                                           PaletteRibbonTextStyle textStyle,
                                           NeedPaintHandler needPaint) 
		{
            Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Store the inherit instances
            _inheritBack = new PaletteRibbonBackInheritRedirect(redirect, backStyle);
            _inheritText = new PaletteRibbonTextInheritRedirect(redirect, textStyle);

            // Define default values
            _backColor1 = Color.Empty;
            _backColor2 = Color.Empty;
            _backColor3 = Color.Empty;
            _backColor4 = Color.Empty;
            _backColor5 = Color.Empty;
            _textColor = Color.Empty;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _inheritBack.SetRedirector(redirect);
            _inheritText.SetRedirector(redirect);
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
                return (BackColor1 == Color.Empty) &&
                       (BackColor2 == Color.Empty) &&
                       (BackColor3 == Color.Empty) &&
                       (BackColor4 == Color.Empty) &&
                       (BackColor5 == Color.Empty) &&
                       (TextColor == Color.Empty);
            }
		}
		#endregion

        #region BackColorStyle
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            return _inheritBack.GetRibbonBackColorStyle(state);
        }
        #endregion

        #region BackColor1
        /// <summary>
        /// Gets and sets the first background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("First background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color BackColor1
        {
            get { return _backColor1; }

            set
            {
                if (_backColor1 != value)
                {
                    _backColor1 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BackColor1 to the default value.
        /// </summary>
        public void ResetBackColor1()
        {
            BackColor1 = Color.Empty;
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor1(PaletteState state)
        {
            if (BackColor1 != Color.Empty)
                return BackColor1;
            else
                return _inheritBack.GetRibbonBackColor1(state);
        }
        #endregion

        #region BackColor2
        /// <summary>
        /// Gets and sets the second background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Second background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color BackColor2
        {
            get { return _backColor2; }

            set
            {
                if (_backColor2 != value)
                {
                    _backColor2 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BackColor2 to the default value.
        /// </summary>
        public void ResetBackColor2()
        {
            BackColor2 = Color.Empty;
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor2(PaletteState state)
        {
            if (BackColor2 != Color.Empty)
                return BackColor2;
            else
                return _inheritBack.GetRibbonBackColor2(state);
        }
        #endregion

        #region BackColor3
        /// <summary>
        /// Gets and sets the third background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Third background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color BackColor3
        {
            get { return _backColor3; }

            set
            {
                if (_backColor3 != value)
                {
                    _backColor3 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BackColor3 to the default value.
        /// </summary>
        public void ResetBackColor3()
        {
            BackColor3 = Color.Empty;
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor3(PaletteState state)
        {
            if (BackColor3 != Color.Empty)
                return BackColor3;
            else
                return _inheritBack.GetRibbonBackColor3(state);
        }
        #endregion

        #region BackColor4
        /// <summary>
        /// Gets and sets the fourth background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Fourth background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color BackColor4
        {
            get { return _backColor4; }

            set
            {
                if (_backColor4 != value)
                {
                    _backColor4 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BackColor4 to the default value.
        /// </summary>
        public void ResetBackColor4()
        {
            BackColor4 = Color.Empty;
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor4(PaletteState state)
        {
            if (BackColor4 != Color.Empty)
                return BackColor4;
            else
                return _inheritBack.GetRibbonBackColor4(state);
        }
        #endregion

        #region BackColor5
        /// <summary>
        /// Gets and sets the fifth background color for the ribbon item.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Fifth background color for the ribbon item.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color BackColor5
        {
            get { return _backColor5; }

            set
            {
                if (_backColor5 != value)
                {
                    _backColor5 = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the BackColor5 to the default value.
        /// </summary>
        public void ResetBackColor5()
        {
            BackColor5 = Color.Empty;
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor5(PaletteState state)
        {
            if (BackColor5 != Color.Empty)
                return BackColor5;
            else
                return _inheritBack.GetRibbonBackColor5(state);
        }
        #endregion

        #region TextColor
        /// <summary>
        /// Gets and sets the Tab color for the item text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Tab color for the tab text.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color TextColor
        {
            get { return _textColor; }

            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextColor to the default value.
        /// </summary>
        public void ResetTextColor()
        {
            TextColor = Color.Empty;
        }

        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTextColor(PaletteState state)
        {
            if (TextColor != Color.Empty)
                return TextColor;
            else
                return _inheritText.GetRibbonTextColor(state);
        }
        #endregion
    }
}
