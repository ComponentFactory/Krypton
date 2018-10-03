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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// View element that draws nothing and just takes up the metric provided size.
	/// </summary>
	public class ViewLayoutMetricSpacer : ViewLeaf
	{
		#region Instance Fields
        private IPaletteMetric _paletteMetric;
        private PaletteMetricInt _metricInt;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutMetricSpacer class.
		/// </summary>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="metricInt">Metric used to get spacer size.</param>
        public ViewLayoutMetricSpacer(IPaletteMetric paletteMetric,
                                      PaletteMetricInt metricInt)
		{
            Debug.Assert(paletteMetric != null);

            // Remember the source information
            _paletteMetric = paletteMetric;
            _metricInt = metricInt;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutMetricSpacer:" + Id;
		}
		#endregion

        #region SetMetrics
        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Source for aquiring metrics.</param>
        public void SetMetrics(IPaletteMetric paletteMetric)
        {
            _paletteMetric = paletteMetric;
        }

        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Source for aquiring metrics.</param>
        /// <param name="metricInt">Actual integer metric to use.</param>
        public void SetMetrics(IPaletteMetric paletteMetric,
                               PaletteMetricInt metricInt)
        {
            _paletteMetric = paletteMetric;
            _metricInt = metricInt;
        }
        #endregion

        #region Layout
		/// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
            Debug.Assert(context != null);

            // Get the sizing metric
            int length = _paletteMetric.GetMetricInt(ElementState, _metricInt);

            // Use the same size for vertical and horizontal
            return new Size(length, length);
		}

		/// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
            Debug.Assert(context != null);

            // Get the sizing metric
            int length = _paletteMetric.GetMetricInt(ElementState, _metricInt);

            // Always use the metric and ignore given space
            ClientRectangle = new Rectangle(context.DisplayRectangle.Location, new Size(length, length));
        }
		#endregion
    }
}
