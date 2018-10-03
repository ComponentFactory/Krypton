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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Storage for panel related properties.
	/// </summary>
    public class NavigatorPanel : Storage
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private PaletteBackStyle _panelBackStyle;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorPanel class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorPanel(KryptonNavigator navigator,
                              NeedPaintHandler needPaint)
		{
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            _panelBackStyle = PaletteBackStyle.PanelClient;
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get { return (PanelBackStyle == PaletteBackStyle.PanelClient); }
        }
        #endregion

        #region PanelBackStyle
        /// <summary>
        /// Gets and sets the panel back style.
        /// </summary>
        [Category("Visuals")]
        [Description("Panel back style.")]
        [DefaultValue(typeof(PaletteBackStyle), "PanelClient")]
        public PaletteBackStyle PanelBackStyle
        {
            get { return _panelBackStyle; }

            set
            {
                if (_panelBackStyle != value)
                {
                    _panelBackStyle = value;
                    _navigator.OnViewBuilderPropertyChanged("PanelBackStyle");
                }
            }
        }
        #endregion
    }
}
