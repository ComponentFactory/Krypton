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
	/// Storage for drag and drop values.
	/// </summary>
    public class PaletteDragDrop : Storage,
                                   IPaletteDragDrop
    {
        #region Instance Fields
        private IPalette _inherit;
        private PaletteDragFeedback _feedback;
        private Color _solidBack;
        private Color _solidBorder;
        private float _solidOpacity;
        private Color _dropDockBack;
        private Color _dropDockBorder;
        private Color _dropDockActive;
        private Color _dropDockInactive;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDragDrop class.
        /// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDragDrop(IPalette inherit,
                               NeedPaintHandler needPaint)
        {
            // Remember inheritance
            _inherit = inherit;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Set default values
            _feedback = PaletteDragFeedback.Inherit;
            _solidBack = Color.Empty;
            _solidBorder = Color.Empty;
            _solidOpacity = -1.0f;
            _dropDockBack = Color.Empty;
            _dropDockBorder = Color.Empty;
            _dropDockActive = Color.Empty;
            _dropDockInactive = Color.Empty;
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
                return ((Feedback == PaletteDragFeedback.Inherit) &&
                        (SolidBack == Color.Empty) &&
                        (SolidBorder == Color.Empty) &&
                        (SolidOpacity == -1.0f) &&
                        (DropDockBack == Color.Empty) &&
                        (DropDockBorder == Color.Empty) &&
                        (DropDockActive == Color.Empty) &&
                        (DropDockInactive == Color.Empty));
            }
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPalette inherit)
        {
            _inherit = inherit;
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            Feedback = GetDragDropFeedback();
            SolidBack = GetDragDropSolidBack();
            SolidBorder = GetDragDropSolidBorder();
            SolidOpacity = GetDragDropSolidOpacity();
            DropDockBack = GetDragDropDockBack();
            DropDockBorder = GetDragDropDockBorder();
            DropDockActive = GetDragDropDockBorder();
            DropDockInactive = GetDragDropDockInactive();
        }
        #endregion

        #region Feedback
        /// <summary>
        /// Gets and sets the feedback drawing method used.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Feedback drawing method used.")]
        [DefaultValue(typeof(PaletteDragFeedback), "Inherit")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteDragFeedback Feedback
        {
            get { return _feedback; }

            set
            {
                if (_feedback != value)
                {
                    _feedback = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the Feedback property to its default value.
        /// </summary>
        public void ResetFeedback()
        {
            Feedback = PaletteDragFeedback.Inherit;
        }

        /// <summary>
        /// Gets the feedback drawing method used.
        /// </summary>
        /// <returns>Color value.</returns>
        public PaletteDragFeedback GetDragDropFeedback()
        {
            if (Feedback != PaletteDragFeedback.Inherit)
                return Feedback;
            else
                return _inherit.GetDragDropFeedback();
        }
        #endregion

        #region SolidBack
        /// <summary>
        /// Gets and sets the background color for a solid drag drop area.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Background color for a solid drag drop area.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color SolidBack
        {
            get { return _solidBack; }

            set
            {
                if (_solidBack != value)
                {
                    _solidBack = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the SolidBack property to its default value.
        /// </summary>
        public void ResetSolidBack()
        {
            SolidBack = Color.Empty;
        }

        /// <summary>
        /// Gets the background color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropSolidBack()
        {
            if (SolidBack != Color.Empty)
                return SolidBack;
            else
                return _inherit.GetDragDropSolidBack();
        }
        #endregion

        #region SolidBorder
        /// <summary>
        ///  Gets and sets the border color for a solid drag drop area.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border color for a solid drag drop area.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color SolidBorder
        {
            get { return _solidBorder; }

            set
            {
                if (_solidBorder != value)
                {
                    _solidBorder = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the SolidBorder property to its default value.
        /// </summary>
        public void ResetSolidBorder()
        {
            SolidBorder = Color.Empty;
        }

        /// <summary>
        /// Gets the border color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropSolidBorder()
        {
            if (SolidBorder != Color.Empty)
                return SolidBorder;
            else
                return _inherit.GetDragDropSolidBorder();
        }
        #endregion

        #region SolidOpacity
        /// <summary>
        /// Gets and sets the opacity of the solid area.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Opacity for the solid drag drop area.")]
        [DefaultValue(-1.0f)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public float SolidOpacity
        {
            get { return _solidOpacity; }

            set
            {
                if (_solidOpacity != value)
                {
                    _solidOpacity = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the SolidOpacity property to its default value.
        /// </summary>
        public void ResetSolidOpacity()
        {
            SolidOpacity = -1.0f;
        }

        /// <summary>
        /// Gets the opacity of the solid area.
        /// </summary>
        /// <returns>Opacity ranging from 0 to 1.</returns>
        public virtual float GetDragDropSolidOpacity()
        {
            if (SolidOpacity >= 0.0f)
                return SolidOpacity;
            else
                return _inherit.GetDragDropSolidOpacity();
        }
        #endregion

        #region DropDockBack
        /// <summary>
        ///  Gets and sets the background color for the docking indicators area.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Background color for the docking indicators area.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color DropDockBack
        {
            get { return _dropDockBack; }

            set
            {
                if (_dropDockBack != value)
                {
                    _dropDockBack = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropDockBack property to its default value.
        /// </summary>
        public void ResetDropDockBack()
        {
            DropDockBack = Color.Empty;
        }


        /// <summary>
        /// Gets the background color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockBack()
        {
            if (DropDockBack != Color.Empty)
                return DropDockBack;
            else
                return _inherit.GetDragDropDockBack();
        }
        #endregion

        #region DropDockBorder
        /// <summary>
        /// Gets and sets the border color for the docking indicators area.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Border color for the docking indicators area.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color DropDockBorder
        {
            get { return _dropDockBorder; }

            set
            {
                if (_dropDockBorder != value)
                {
                    _dropDockBorder = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropDockBorder property to its default value.
        /// </summary>
        public void ResetDropDockBorder()
        {
            DropDockBorder = Color.Empty;
        }

        /// <summary>
        /// Gets the border color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockBorder()
        {
            if (DropDockBorder != Color.Empty)
                return DropDockBorder;
            else
                return _inherit.GetDragDropDockBorder();
        }
        #endregion

        #region DropDockActive
        /// <summary>
        /// Gets and sets the active color for docking indicators.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Sctive color for docking indicators..")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color DropDockActive
        {
            get { return _dropDockActive; }

            set
            {
                if (_dropDockActive != value)
                {
                    _dropDockActive = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropDockActive property to its default value.
        /// </summary>
        public void ResetDropDockActive()
        {
            DropDockActive = Color.Empty;
        }

        /// <summary>
        /// Gets the active color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockActive()
        {
            if (DropDockActive != Color.Empty)
                return DropDockActive;
            else
                return _inherit.GetDragDropDockActive();
        }
        #endregion

        #region DropDockInactive
        /// <summary>
        /// Gets and sets the inactive color for docking indicators.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Inactive color for docking indicators.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color DropDockInactive
        {
            get { return _dropDockInactive; }

            set
            {
                if (_dropDockInactive != value)
                {
                    _dropDockInactive = value;
                    PerformNeedPaint();
                }
            }
        }

        /// <summary>
        /// Resets the DropDockInactive property to its default value.
        /// </summary>
        public void ResetDropDockInactive()
        {
            DropDockInactive = Color.Empty;
        }

        /// <summary>
        /// Gets the inactive color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        public Color GetDragDropDockInactive()
        {
            if (DropDockInactive != Color.Empty)
                return DropDockInactive;
            else
                return _inherit.GetDragDropDockInactive();
        }
        #endregion
    }
}
