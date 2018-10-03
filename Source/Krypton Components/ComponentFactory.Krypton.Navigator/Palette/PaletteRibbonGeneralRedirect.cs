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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Storage for general ribbon values.
	/// </summary>
    public class PaletteRibbonGeneralNavRedirect : Storage,
                                                   IPaletteRibbonGeneral
    {
        #region Instance Fields
        private Font _textFont;
        private PaletteTextHint _textHint;
        private PaletteRibbonGeneralInheritRedirect _inherit;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonGeneralNavRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonGeneralNavRedirect(PaletteRedirect redirect,
                                               NeedPaintHandler needPaint)
        {
            Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Store the inherit instances
            _inherit = new PaletteRibbonGeneralInheritRedirect(redirect);

            // Set default values
            _textFont = null;
            _textHint = PaletteTextHint.Inherit;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _inherit.SetRedirector(redirect);
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
                return (TextFont == null) && (TextHint == PaletteTextHint.Inherit);
            }                        
        }
        #endregion

        #region ContextTextAlign
        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state)
        {
            return _inherit.GetRibbonContextTextAlign(state);
        }
        #endregion

        #region ContextTextFont
        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonContextTextFont(PaletteState state)
        {
            return _inherit.GetRibbonContextTextFont(state);
        }
        #endregion

        #region ContextTextColor
        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Color GetRibbonContextTextColor(PaletteState state)
        {
            return _inherit.GetRibbonContextTextColor(state);
        }
        #endregion

        #region DisabledDark
        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledDark(PaletteState state)
        {
            return _inherit.GetRibbonDisabledDark(state);
        }
        #endregion

        #region DisabledLight
        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDisabledLight(PaletteState state)
        {
            return _inherit.GetRibbonDisabledLight(state);
        }
        #endregion

        #region DropArrowLight
        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowLight(PaletteState state)
        {
            return _inherit.GetRibbonDropArrowLight(state);
        }
        #endregion

        #region DropArrowDark
        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonDropArrowDark(PaletteState state)
        {
            return _inherit.GetRibbonDropArrowDark(state);
        }
        #endregion

        #region GroupDialogDark
        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogDark(PaletteState state)
        {
            return _inherit.GetRibbonGroupDialogDark(state);
        }
        #endregion

        #region GroupDialogLight
        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupDialogLight(PaletteState state)
        {
            return _inherit.GetRibbonGroupDialogLight(state);
        }
        #endregion

        #region GroupSeparatorDark
        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorDark(PaletteState state)
        {
            return _inherit.GetRibbonGroupSeparatorDark(state);
        }
        #endregion

        #region GroupSeparatorLight
        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonGroupSeparatorLight(PaletteState state)
        {
            return _inherit.GetRibbonGroupSeparatorLight(state);
        }
        #endregion

        #region MinimizeBarDarkColor
        /// <summary>
        /// Gets the color for the ribbon minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarDark(PaletteState state)
        {
            return _inherit.GetRibbonMinimizeBarDark(state);
        }
        #endregion

        #region MinimizeBarLightColor
        /// <summary>
        /// Gets the color for the ribbon minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonMinimizeBarLight(PaletteState state)
        {
            return _inherit.GetRibbonMinimizeBarLight(state);
        }
        #endregion

        #region GetRibbonShape
        /// <summary>
        /// Gets the ribbon shape.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        public PaletteRibbonShape GetRibbonShape()
        {
            return _inherit.GetRibbonShape();
        }
        #endregion

        #region TabSeparatorColor
        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorColor(PaletteState state)
        {
            return _inherit.GetRibbonTabSeparatorColor(state);
        }
        #endregion

        #region TabSeparatorContextColor
        /// <summary>
        /// Gets the color for the tab context separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTabSeparatorContextColor(PaletteState state)
        {
            return _inherit.GetRibbonTabSeparatorContextColor(state);
        }
        #endregion

        #region TextFont
        /// <summary>
        /// Gets and sets the font for the ribbon text.
        /// </summary>
        [Category("Visuals")]
        [Description("Font for the ribbon text.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Font TextFont
        {
            get { return _textFont; }

            set
            {
                if (_textFont != value)
                {
                    _textFont = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextFont to the default value.
        /// </summary>
        public void ResetTextFont()
        {
            TextFont = null;
        }

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetRibbonTextFont(PaletteState state)
        {
            if (TextFont != null)
                return TextFont;
            else
                return _inherit.GetRibbonTextFont(state);
        }
        #endregion

        #region TextHint
        /// <summary>
        /// Gets and sets the rendering hint for the text font.
        /// </summary>
        [Category("Visuals")]
        [Description("Rendering hint for the text font.")]
        [DefaultValue(typeof(PaletteTextHint), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteTextHint TextHint
        {
            get { return _textHint; }

            set
            {
                if (_textHint != value)
                {
                    _textHint = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextHint to the default value.
        /// </summary>
        public void ResetTextHint()
        {
            TextHint = PaletteTextHint.Inherit;
        }

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public PaletteTextHint GetRibbonTextHint(PaletteState state)
        {
            if (TextHint != PaletteTextHint.Inherit)
                return TextHint;
            else
                return _inherit.GetRibbonTextHint(state);
        }
        #endregion

        #region QATButtonDarkColor
        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonDark(PaletteState state)
        {
            return _inherit.GetRibbonQATButtonDark(state);
        }
        #endregion

        #region QATButtonLightColor
        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonQATButtonLight(PaletteState state)
        {
            return _inherit.GetRibbonQATButtonLight(state);
        }
        #endregion
    }
}
