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
    /// Storage for palette image settings.
    /// </summary>
    public class KryptonPaletteImages : Storage
    {
        #region Instance Fields
        private KryptonPaletteImagesCheckBox _imagesCheckBox;
        private KryptonPaletteImagesContextMenu _imagesContextMenu;
        private KryptonPaletteImagesDropDownButton _imagesDropDownButton;
        private KryptonPaletteImagesGalleryButtons _imagesGalleryButtons;
        private KryptonPaletteImagesRadioButton _imagesRadioButton;
        private KryptonPaletteImagesTreeView _imagesTreeView;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteImages class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteImages(PaletteRedirect redirector,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the different image sets
            _imagesCheckBox = new KryptonPaletteImagesCheckBox(redirector, needPaint);
            _imagesContextMenu = new KryptonPaletteImagesContextMenu(redirector, needPaint);
            _imagesDropDownButton = new KryptonPaletteImagesDropDownButton(redirector, needPaint);
            _imagesGalleryButtons = new KryptonPaletteImagesGalleryButtons(redirector, needPaint);
            _imagesRadioButton = new KryptonPaletteImagesRadioButton(redirector, needPaint);
            _imagesTreeView = new KryptonPaletteImagesTreeView(redirector, needPaint);
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
                return _imagesCheckBox.IsDefault &&
                       _imagesContextMenu.IsDefault &&
                       _imagesDropDownButton.IsDefault &&
                       _imagesGalleryButtons.IsDefault &&
                       _imagesRadioButton.IsDefault &&
                       _imagesTreeView.IsDefault;
            }
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            _imagesCheckBox.PopulateFromBase();
            _imagesContextMenu.PopulateFromBase();
            _imagesDropDownButton.PopulateFromBase();
            _imagesGalleryButtons.PopulateFromBase();
            _imagesRadioButton.PopulateFromBase();
            _imagesTreeView.PopulateFromBase();
        }
        #endregion

        #region CheckBox
        /// <summary>
        /// Gets access to the check box set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining check box images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesCheckBox CheckBox
        {
            get { return _imagesCheckBox; }
        }

        private bool ShouldSerializeCheckBox()
        {
            return !_imagesCheckBox.IsDefault;
        }
        #endregion

        #region ContextMenu
        /// <summary>
        /// Gets access to the context menu set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context menu images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesContextMenu ContextMenu
        {
            get { return _imagesContextMenu; }
        }

        private bool ShouldSerializeContextMenu()
        {
            return !_imagesContextMenu.IsDefault;
        }
        #endregion

        #region DropDownButton
        /// <summary>
        /// Gets access to the drop down button set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining drop down button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesDropDownButton DropDownButton
        {
            get { return _imagesDropDownButton; }
        }

        private bool ShouldSerializeDropDownButton()
        {
            return !_imagesDropDownButton.IsDefault;
        }
        #endregion

        #region CheckBox
        /// <summary>
        /// Gets access to the gallery button images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining gallery button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesGalleryButtons GalleryButtons
        {
            get { return _imagesGalleryButtons; }
        }

        private bool ShouldSerializeGalleryButtons()
        {
            return !_imagesGalleryButtons.IsDefault;
        }
        #endregion

        #region RadioButton
        /// <summary>
        /// Gets access to the radio button set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining radio button images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesRadioButton RadioButton
        {
            get { return _imagesRadioButton; }
        }

        private bool ShouldSerializeRadioButton()
        {
            return !_imagesRadioButton.IsDefault;
        }
        #endregion

        #region TreeView
        /// <summary>
        /// Gets access to the tree view set of images.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tree view images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteImagesTreeView TreeView
        {
            get { return _imagesTreeView; }
        }

        private bool ShouldSerializeTreeView()
        {
            return !_imagesTreeView.IsDefault;
        }
        #endregion
    }
}
