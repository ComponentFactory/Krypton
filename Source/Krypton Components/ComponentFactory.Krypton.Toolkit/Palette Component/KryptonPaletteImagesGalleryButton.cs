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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage for gallery button state specific images.
	/// </summary>
    public class KryptonPaletteImagesGalleryButton : Storage
    {
        #region Instance Fields
        private PaletteRibbonGalleryButton _button;
        private PaletteRedirect _redirect;
        private Image _common;
        private Image _disabled;
        private Image _normal;
        private Image _tracking;
        private Image _pressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteImagesGalleryButton class.
		/// </summary>
        /// <param name="button">Which button this instance represents.</param>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton button,
                                                 PaletteRedirect redirect,
                                                 NeedPaintHandler needPaint) 
		{
            _button = button;
            _redirect = redirect;
            NeedPaint = needPaint;

            // Create the storage
            _common = null;
            _disabled = null;
            _normal = null;
            _tracking = null;
            _pressed = null;
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
                return (_common == null) &&
                       (_disabled == null) &&
                       (_normal == null) &&
                       (_tracking == null) &&
                       (_pressed == null);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            _disabled = _redirect.GetGalleryButtonImage(_button, PaletteState.Disabled);
            _normal = _redirect.GetGalleryButtonImage(_button, PaletteState.Normal);
            _tracking = _redirect.GetGalleryButtonImage(_button, PaletteState.Tracking);
            _pressed = _redirect.GetGalleryButtonImage(_button, PaletteState.Pressed);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            // Update our cached reference
            _redirect = redirect;
        }
        #endregion

        #region Common
        /// <summary>
        /// Gets and sets the common image that other gallery button images inherit from.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Common image that other gallery button images inherit from.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Common
        {
            get { return _common; }

            set
            {
                if (_common != value)
                {
                    _common = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Common property to its default value.
        /// </summary>
        public void ResetCommon()
        {
            Common = null;
        }
        #endregion

        #region Disabled
        /// <summary>
        /// Gets and sets the image for use when the gallery button is disabled.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the gallery button is disabled.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Disabled
        {
            get { return _disabled; }

            set
            {
                if (_disabled != value)
                {
                    _disabled = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Disabled property to its default value.
        /// </summary>
        public void ResetDisabled()
        {
            Disabled = null;
        }
        #endregion

        #region Normal
        /// <summary>
        /// Gets and sets the image for use when the gallery button is normal.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the gallery button is normal.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Normal
        {
            get { return _normal; }

            set
            {
                if (_normal != value)
                {
                    _normal = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Normal property to its default value.
        /// </summary>
        public void ResetNormal()
        {
            Normal = null;
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the image for use when the gallery button is hot tracking.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the gallery button is hot tracking.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Tracking
        {
            get { return _tracking; }

            set
            {
                if (_tracking != value)
                {
                    _tracking = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Tracking property to its default value.
        /// </summary>
        public void ResetTracking()
        {
            Tracking = null;
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the image for use when the gallery button is pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the gallery button is pressed.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Pressed
        {
            get { return _pressed; }

            set
            {
                if (_pressed != value)
                {
                    _pressed = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the Pressed property to its default value.
        /// </summary>
        public void ResetPressed()
        {
            Pressed = null;
        }
        #endregion
    }
}
