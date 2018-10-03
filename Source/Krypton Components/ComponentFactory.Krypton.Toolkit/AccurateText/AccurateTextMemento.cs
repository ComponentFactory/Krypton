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
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Encapsulate the information needed to draw text using the AccurateText class.
	/// </summary>
    public class AccurateTextMemento : GlobalId,
                                       IDisposable
	{
		#region Static Fields
		private static AccurateTextMemento _empty;
		#endregion

		#region Instance Fields
        private bool _disposeFont;
        private string _text;
        private Size _size;
        private Font _font;
        private StringFormat _format;
        private TextRenderingHint _hint;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the TextMemento class.
		/// </summary>
        /// <param name="text">Text to draw.</param>
        /// <param name="font">Drawing font.</param>
        /// <param name="sizeF">Size of measured text.</param>
        /// <param name="format">String formatting.</param>
        /// <param name="hint">Drawing hint.</param>
        /// <param name="disposeFont">Should the font be disposed.</param>
        internal AccurateTextMemento(string text,
                                     Font font,
                                     SizeF sizeF,
                                     StringFormat format,
                                     TextRenderingHint hint,
                                     bool disposeFont)
		{
            _text = text;
            _size = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
            _font = font;
            _format = format;
            _hint = hint;
            _disposeFont = disposeFont;
		}

        /// <summary>
        /// Dispose of the memento resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposeFont && (_font != null))
            {
                _font.Dispose();
                _font = null;
            }
        }
		#endregion

		#region Public Properties
        /// <summary>
        /// Gets the text to draw.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }
        
        /// <summary>
        /// Gets the drawing font.
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// Gets the pixel size of the text area.
        /// </summary>
        public Size Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the pixel size of the text area.
        /// </summary>
        public StringFormat Format
        {
            get { return _format; }
        }

        /// <summary>
        /// Gets a value indicating if the memento represents nothing that can be drawn.
        /// </summary>
        public bool IsEmpty
        {
            get { return (_size == Size.Empty); }
        }
        #endregion

		#region Internal Static Properties
		/// <summary>
		/// Get access to an empty TextMemento instance.
		/// </summary>
		internal static AccurateTextMemento Empty
		{
			get
			{
				// Only create the single instance when first requested
				if (_empty == null)
					_empty = new AccurateTextMemento(string.Empty,
                                                     null,
                                                     Size.Empty,
                                                     StringFormat.GenericDefault,
                                                     TextRenderingHint.SystemDefault,
                                                     false);

				return _empty;
			}
		}
		#endregion
    }
}
