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
	/// Storage for palette radio button images.
	/// </summary>
    public class KryptonPaletteImagesRadioButton : Storage
    {
        #region Instance Fields
        private PaletteRedirect _redirect;
        private Image _common;
        private Image _uncheckedDisabled;
        private Image _uncheckedNormal;
        private Image _uncheckedTracking;
        private Image _uncheckedPressed;
        private Image _checkedDisabled;
        private Image _checkedNormal;
        private Image _checkedTracking;
        private Image _checkedPressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteImagesRadioButton class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteImagesRadioButton(PaletteRedirect redirect,
                                               NeedPaintHandler needPaint) 
		{
            // Store the redirector
            _redirect = redirect;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create the storage
            _common = null;
            _uncheckedDisabled = null;
            _uncheckedNormal = null;
            _uncheckedTracking = null;
            _uncheckedPressed = null;
            _checkedDisabled = null;
            _checkedNormal = null;
            _checkedTracking = null;
            _checkedPressed = null;
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
                       (_uncheckedDisabled == null) &&
                       (_uncheckedNormal == null) &&
                       (_uncheckedTracking == null) &&
                       (_uncheckedPressed == null) &&
                       (_checkedDisabled == null) &&
                       (_checkedNormal == null) &&
                       (_checkedTracking == null) &&
                       (_checkedPressed == null);
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            _checkedDisabled = _redirect.GetRadioButtonImage(false, true, false, false);
            _checkedNormal = _redirect.GetRadioButtonImage(true, true, false, false);
            _checkedTracking = _redirect.GetRadioButtonImage(true, true, true, false);
            _checkedPressed = _redirect.GetRadioButtonImage(true, true, false, true);
            _uncheckedDisabled = _redirect.GetRadioButtonImage(false, false, false, false);
            _uncheckedNormal = _redirect.GetRadioButtonImage(true, false, false, false);
            _uncheckedTracking = _redirect.GetRadioButtonImage(true, false, true, false);
            _uncheckedPressed = _redirect.GetRadioButtonImage(true, false, false, true);
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
        /// Gets and sets the common image that other check box images inherit from.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Common image that other check box images inherit from.")]
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

        #region UncheckedDisabled
        /// <summary>
        /// Gets and sets the image for use when the check box is not checked and disabled.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is not checked and disabled.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image UncheckedDisabled
        {
            get { return _uncheckedDisabled; }

            set
            {
                if (_uncheckedDisabled != value)
                {
                    _uncheckedDisabled = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the UncheckedDisabled property to its default value.
        /// </summary>
        public void ResetUncheckedDisabled()
        {
            UncheckedDisabled = null;
        }
        #endregion

        #region UncheckedNormal
        /// <summary>
        /// Gets and sets the image for use when the check box is unchecked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is unchecked.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image UncheckedNormal
        {
            get { return _uncheckedNormal; }

            set
            {
                if (_uncheckedNormal != value)
                {
                    _uncheckedNormal = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the UncheckedNormal property to its default value.
        /// </summary>
        public void ResetUncheckedNormal()
        {
            UncheckedNormal = null;
        }
        #endregion

        #region UncheckedTracking
        /// <summary>
        /// Gets and sets the image for use when the check box is unchecked and hot tracking.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is unchecked and hot tracking.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image UncheckedTracking
        {
            get { return _uncheckedTracking; }

            set
            {
                if (_uncheckedTracking != value)
                {
                    _uncheckedTracking = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the UncheckedTracking property to its default value.
        /// </summary>
        public void ResetUncheckedTracking()
        {
            UncheckedTracking = null;
        }
        #endregion

        #region UncheckedPressed
        /// <summary>
        /// Gets and sets the image for use when the check box is unchecked and pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is unchecked and pressed.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image UncheckedPressed
        {
            get { return _uncheckedPressed; }

            set
            {
                if (_uncheckedPressed != value)
                {
                    _uncheckedPressed = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the UncheckedPressed property to its default value.
        /// </summary>
        public void ResetUncheckedPressed()
        {
            UncheckedPressed = null;
        }
        #endregion

        #region CheckedDisabled
        /// <summary>
        /// Gets and sets the image for use when the check box is checked but disabled.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is checked but disabled.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image CheckedDisabled
        {
            get { return _checkedDisabled; }

            set
            {
                if (_checkedDisabled != value)
                {
                    _checkedDisabled = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the CheckedDisabled property to its default value.
        /// </summary>
        public void ResetCheckedDisabled()
        {
            CheckedDisabled = null;
        }
        #endregion

        #region CheckedNormal
        /// <summary>
        /// Gets and sets the image for use when the check box is checked.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is checked.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image CheckedNormal
        {
            get { return _checkedNormal; }

            set
            {
                if (_checkedNormal != value)
                {
                    _checkedNormal = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the CheckedNormal property to its default value.
        /// </summary>
        public void ResetCheckedNormal()
        {
            CheckedNormal = null;
        }
        #endregion

        #region CheckedTracking
        /// <summary>
        /// Gets and sets the image for use when the check box is checked and hot tracking.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is checked and hot tracking.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image CheckedTracking
        {
            get { return _checkedTracking; }

            set
            {
                if (_checkedTracking != value)
                {
                    _checkedTracking = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the CheckedTracking property to its default value.
        /// </summary>
        public void ResetCheckedTracking()
        {
            CheckedTracking = null;
        }
        #endregion

        #region CheckedPressed
        /// <summary>
        /// Gets and sets the image for use when the check box is checked and pressed.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Image for use when the check box is checked and pressed.")]
        [DefaultValue(null)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image CheckedPressed
        {
            get { return _checkedPressed; }

            set
            {
                if (_checkedPressed != value)
                {
                    _checkedPressed = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the CheckedPressed property to its default value.
        /// </summary>
        public void ResetCheckedPressed()
        {
            CheckedPressed = null;
        }
        #endregion
    }
}
