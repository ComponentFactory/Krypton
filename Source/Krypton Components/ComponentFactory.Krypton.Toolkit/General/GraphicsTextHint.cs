// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.Drawing.Text;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Apply a requested text rendering hint to a graphics instance.
	/// </summary>
    public class GraphicsTextHint : GlobalId,
                                    IDisposable
	{
		#region Instance Fields
		private Graphics _graphics;
		private TextRenderingHint _textHint;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the GraphicsSmooth class.
		/// </summary>
		/// <param name="graphics">Graphics context.</param>
		/// <param name="textHint">Temporary text rendering hint to apply.</param>
		public GraphicsTextHint(Graphics graphics, TextRenderingHint textHint)
		{
			// Cache graphics instance
			_graphics = graphics;

			// Remember current text hint
			_textHint = _graphics.TextRenderingHint;

			// Apply new text hint
			_graphics.TextRenderingHint = textHint;
		}

		/// <summary>
		/// Reverse the text hint change.
		/// </summary>
		public void Dispose()
		{
            if (_graphics != null)
            {
                try
                {
                    // Put back to the original text hint
                    _graphics.TextRenderingHint = _textHint;
                }
                catch { }
            }
		}
		#endregion
	}
}
