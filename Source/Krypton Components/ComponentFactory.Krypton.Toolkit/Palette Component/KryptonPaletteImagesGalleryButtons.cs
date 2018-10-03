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
	/// Storage for gallery button images.
	/// </summary>
    public class KryptonPaletteImagesGalleryButtons : Storage
    {
        #region Instance Fields
        private KryptonPaletteImagesGalleryButton _up;
        private KryptonPaletteImagesGalleryButton _down;
        private KryptonPaletteImagesGalleryButton _dropDown;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteImagesGalleryButtons class.
		/// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteImagesGalleryButtons(PaletteRedirect redirector,
                                                  NeedPaintHandler needPaint) 
		{
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create the storage
            _up = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.Up, redirector, needPaint);
            _down = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.Down, redirector, needPaint);
            _dropDown = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.DropDown, redirector, needPaint);
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
                return _up.IsDefault &&
                       _down.IsDefault &&
                       _dropDown.IsDefault;
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            _up.PopulateFromBase();
            _down.PopulateFromBase();
            _dropDown.PopulateFromBase();
        }
        #endregion

        #region Up
        /// <summary>
        /// Gallery up button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery up button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesGalleryButton Up
        {
            get { return _up; }
        }
        #endregion

        #region Down
        /// <summary>
        /// Gallery down button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesGalleryButton Down
        {
            get { return _down; }
        }
        #endregion

        #region DropDown
        /// <summary>
        /// Gallery drop down button images.
        /// </summary>
        [KryptonPersist(true)]
        [Category("Visuals")]
        [Description("Gallery drop down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesGalleryButton DropDown
        {
            get { return _dropDown; }
        }
        #endregion
    }
}
