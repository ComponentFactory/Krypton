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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a radio button centered in the correct location.
	/// </summary>
    internal class ViewDrawRibbonGroupRadioButtonImage : ViewComposite                                              
    {
        #region Static Fields
        private static readonly Size _smallSize = new Size(16, 16);
        private static readonly Size _largeSize = new Size(32, 32);
        #endregion

        #region Instance Fields
        private KryptonRibbonGroupRadioButton _ribbonRadioButton;
        private ViewDrawRadioButton _drawRadioButton;
        private bool _large;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupRadioButtonImage class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonRadioButton">Reference to ribbon group radio button definition.</param>
        /// <param name="large">Show the large image.</param>
        public ViewDrawRibbonGroupRadioButtonImage(KryptonRibbon ribbon,
                                                   KryptonRibbonGroupRadioButton ribbonRadioButton,
                                                   bool large)
        {
            Debug.Assert(ribbonRadioButton != null);

            // Remember incoming parameters
            _ribbonRadioButton = ribbonRadioButton;
            _large = large;

            // Use redirector to get the radio button images and redirect to parent palette
            PaletteRedirectRadioButton redirectImages = new PaletteRedirectRadioButton(ribbon.GetRedirector(), ribbon.StateCommon.RibbonImages.RadioButton);
            
            // Create drawing element
            _drawRadioButton = new ViewDrawRadioButton(redirectImages);

            // Add as only child
            Add(_drawRadioButton);
        }        

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupRadioButtonImage:" + Id;
		}
        #endregion

        #region Enabled
        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get { return _drawRadioButton.Enabled; }
            set { _drawRadioButton.Enabled = value; }
        }
        #endregion

        #region Checked
        /// <summary>
        /// Gets and sets the checked state of the radio button.
        /// </summary>
        public bool Checked
        {
            get { return _drawRadioButton.CheckState; }
            set { _drawRadioButton.CheckState = value; }
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the radio button.
        /// </summary>
        public bool Tracking
        {
            get { return _drawRadioButton.Tracking; }
            set { _drawRadioButton.Tracking = value; }
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the radio button.
        /// </summary>
        public bool Pressed
        {
            get { return _drawRadioButton.Pressed; }
            set { _drawRadioButton.Pressed = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            if (_large)
                return _largeSize;
            else
                return _smallSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Take on all the provided area
            ClientRectangle = context.DisplayRectangle;

            // Get the size of the radio button when it is drawn
            Rectangle radioButtonRect = new Rectangle(Point.Empty, _drawRadioButton.GetPreferredSize(context));
            
            // Decide on correct position within our rectangle
            if (_large)
            {
                // Place horizontal centered at the bottom of area
                radioButtonRect.X = ClientLocation.X + (ClientWidth - radioButtonRect.Width) / 2;
                radioButtonRect.Y = ClientRectangle.Bottom - radioButtonRect.Height;
            }
            else
            {
                // Place vertically centered at the right of area
                radioButtonRect.X = ClientRectangle.Right - radioButtonRect.Width;
                radioButtonRect.Y = ClientLocation.Y + (ClientHeight - radioButtonRect.Height) / 2;
            }

            // Layout the radio button draw element
            context.DisplayRectangle = radioButtonRect;
            _drawRadioButton.Layout(context);
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
    }
}
