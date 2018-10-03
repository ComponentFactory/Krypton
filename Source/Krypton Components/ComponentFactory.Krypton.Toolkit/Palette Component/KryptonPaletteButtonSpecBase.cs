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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Base class for defining button specifications.
    /// </summary>
    public class KryptonPaletteButtonSpecBase : Storage,
                                                IPaletteButtonSpec
    {
        #region Instance Fields
        private PaletteRedirect _redirector;
        private PaletteButtonStyle _style;
        private PaletteButtonOrientation _orientation;
        private PaletteRelativeEdgeAlign _edge;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a button spec change occurs.
        /// </summary>
        public event EventHandler ButtonSpecChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteButtonSpecBase class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        internal KryptonPaletteButtonSpecBase(PaletteRedirect redirector)
        {
            Debug.Assert(redirector != null);

            // Remember reference to redirector
            _redirector = redirector;

            // Default the generic overridable values
            _style = PaletteButtonStyle.Inherit;
            _orientation = PaletteButtonOrientation.Inherit;
            _edge = PaletteRelativeEdgeAlign.Inherit;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault
        {
            get
            {
                return ((Style == PaletteButtonStyle.Inherit) &&
                        (Orientation == PaletteButtonOrientation.Inherit) &&
                        (Edge == PaletteRelativeEdgeAlign.Inherit));

            }
        }
        #endregion

        #region Redirector
        /// <summary>
        /// Gets access to the redirector.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaletteRedirect Redirector
        {
            get { return _redirector; }
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _redirector = redirect;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="style">The style of the button spec instance.</param>
        public virtual void PopulateFromBase(PaletteButtonSpecStyle style)
        {
            Style = _redirector.GetButtonSpecStyle(style);
            Orientation = _redirector.GetButtonSpecOrientation(style);
            Edge = _redirector.GetButtonSpecEdge(style);
        }
        #endregion

        #region Style
        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button style.")]
        [DefaultValue(typeof(PaletteButtonStyle), "Inherit")]
        public PaletteButtonStyle Style
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the Style property to its default value.
        /// </summary>
        public void ResetStyle()
        {
            Style = PaletteButtonStyle.Inherit;
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the button orientation.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Defines the button orientation.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(PaletteButtonOrientation), "Inherit")]
        public PaletteButtonOrientation Orientation
        {
            get { return _orientation; }

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the Orientation property to its default value.
        /// </summary>
        public void ResetOrientation()
        {
            Orientation = PaletteButtonOrientation.Inherit;
        }
        #endregion

        #region Edge
        /// <summary>
        /// Gets and sets the header edge to display the button against.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("The header edge to display the button against.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(PaletteRelativeEdgeAlign), "Inherit")]
        public PaletteRelativeEdgeAlign Edge
        {
            get { return _edge; }

            set
            {
                if (_edge != value)
                {
                    _edge = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the Edge property to its default value.
        /// </summary>
        public void ResetEdge()
        {
            Edge = PaletteRelativeEdgeAlign.Inherit;
        }
        #endregion

        #region IPaletteButtonSpec
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        public virtual Icon GetButtonSpecIcon(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecIcon(style);
        }

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public virtual Image GetButtonSpecImage(PaletteButtonSpecStyle style,
                                                PaletteState state)
        {
            return _redirector.GetButtonSpecImage(style, state);
        }

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecImageTransparentColor(style);
        }

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public virtual string GetButtonSpecShortText(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecShortText(style);
        }

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public virtual string GetButtonSpecLongText(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecLongText(style);
        }

        /// <summary>
        /// Gets the tooltip title text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public virtual string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecToolTipTitle(style);
        }

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecColorMap(style);
        }

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        public virtual PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style)
        {
            if (Style != PaletteButtonStyle.Inherit)
                return Style;
            else
                return _redirector.GetButtonSpecStyle(style);
        }

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        public virtual HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style)
        {
            return _redirector.GetButtonSpecLocation(style);
        }

        /// <summary>
        /// Gets the edge to positon the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        public virtual PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
        {
            if (Edge != PaletteRelativeEdgeAlign.Inherit)
                return Edge;
            else
                return _redirector.GetButtonSpecEdge(style);
        }

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        public virtual PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style)
        {
            if (Orientation != PaletteButtonOrientation.Inherit)
                return Orientation;
            else
                return _redirector.GetButtonSpecOrientation(style);
        }
        #endregion

        #region OnButtonSpecChanged
        /// <summary>
        /// Raises the ButtonSpecChanged event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnButtonSpecChanged(object sender, EventArgs e)
        {
            if (ButtonSpecChanged != null)
                ButtonSpecChanged(this, e);
        }
        #endregion
    }
}
