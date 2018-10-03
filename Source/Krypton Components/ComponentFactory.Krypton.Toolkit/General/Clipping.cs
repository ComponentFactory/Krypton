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
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Restrict graphics clipping using the provided path/region instance.
	/// </summary>
    public class Clipping : GlobalId,
                            IDisposable
	{
		#region Instance Fields
		private Graphics _graphics;
        private Region _previousRegion;
        private Region _newRegion;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the Clipping class.
		/// </summary>
		/// <param name="graphics">Graphics context.</param>
		/// <param name="path">Path to clip.</param>
        public Clipping(Graphics graphics, GraphicsPath path)
            : this(graphics, path, false)
        {

        }
        
        /// <summary>
        /// Initialize a new instance of the Clipping class.
		/// </summary>
		/// <param name="graphics">Graphics context.</param>
		/// <param name="path">Path to clip.</param>
        /// <param name="exclude">Exclude path from clipping.</param>
        public Clipping(Graphics graphics, GraphicsPath path, bool exclude)
		{
			// Cache graphics instance
			_graphics = graphics;

            // Save the existing clipping region
            _previousRegion = _graphics.Clip;

            // Add clipping of path to existing clipping region
            _newRegion = _previousRegion.Clone();
             
            if (exclude)
                _newRegion.Exclude(path);
            else
                _newRegion.Intersect(path);

            _graphics.Clip = _newRegion;
		}

		/// <summary>
        /// Initialize a new instance of the Clipping class.
		/// </summary>
		/// <param name="graphics">Graphics context.</param>
        /// <param name="region">Clipping region.</param>
        public Clipping(Graphics graphics, Region region)
            : this(graphics, region, false)
        {
        }

		/// <summary>
        /// Initialize a new instance of the Clipping class.
		/// </summary>
		/// <param name="graphics">Graphics context.</param>
        /// <param name="region">Clipping region.</param>
        /// <param name="exclude">Exclude region from clipping.</param>
        public Clipping(Graphics graphics, Region region, bool exclude)
		{
			// Cache graphics instance
			_graphics = graphics;

            // Save the existing clipping region
            _previousRegion = _graphics.Clip;

            // Add clipping of region to existing clipping region
            _newRegion = _previousRegion.Clone();

            if (exclude)
                _newRegion.Exclude(region);
            else
                _newRegion.Intersect(region);

            _graphics.Clip = _newRegion;
		}

        /// <summary>
        /// Initialize a new instance of the Clipping class.
        /// </summary>
        /// <param name="graphics">Graphics context.</param>
        /// <param name="rect">Clipping rectangle.</param>
        public Clipping(Graphics graphics, Rectangle rect)
            : this(graphics, rect, false)
        {
        }

        /// <summary>
        /// Initialize a new instance of the Clipping class.
        /// </summary>
        /// <param name="graphics">Graphics context.</param>
        /// <param name="rect">Clipping rectangle.</param>
        /// <param name="exclude">Exclude rectangle from clipping.</param>
        public Clipping(Graphics graphics, Rectangle rect, bool exclude)
        {
            // Cache graphics instance
            _graphics = graphics;

            // Save the existing clipping region
            _previousRegion = _graphics.Clip;

            // Add clipping of rectangle to existing clipping region
            _newRegion = _previousRegion.Clone();

            if (exclude)
                _newRegion.Exclude(rect);
            else
                _newRegion.Intersect(rect);

            _graphics.Clip = _newRegion;
        }

		/// <summary>
		/// Reverse the smoothing mode change.
		/// </summary>
		public void Dispose()
		{
            if (_graphics != null)
            {
                try
                {
                    // Restore the original clipping region
                    _graphics.Clip = _previousRegion;
                }
                catch { }
            }

            if (_newRegion != null)
            {
                try
                {
                    // Dispose of created resources
                    _newRegion.Dispose();
                    _newRegion = null;
                }
                catch { }
            }
		}
		#endregion
	}
}
