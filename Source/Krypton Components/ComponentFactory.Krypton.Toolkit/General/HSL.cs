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
using System.Collections.Generic;
using System.Text;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Represents a hue, saturation, luminance triple.
	/// </summary>
    internal class ColorHSL : GlobalId
	{
		#region Instance Fields
		private double _hue;
		private double _saturation;
		private double _luminance;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the ColorHSL class.
		/// </summary>
		public ColorHSL()
		{
		}

		/// <summary>
		/// Initialize a new instance of the ColorHSL class.
		/// </summary>
		/// <param name="c">Initialize from an existing Color.</param>
		public ColorHSL(Color c)
		{
			// Initialize from the color instance
			_hue = c.GetHue() / 360f;
			_saturation = c.GetBrightness();
			_luminance = c.GetSaturation();
		}
		#endregion

		#region Color
		/// <summary>
		/// Get a Color instance from the HSL triple.
		/// </summary>
		public Color Color
		{
			get
			{
				double red = 0;
				double green = 0;
				double blue = 0;

				if (Luminance > 0)
				{
					if (Saturation == 0)
						red = green = blue = Luminance;
					else
					{
						double temp2;

						if (Luminance <= 0.5)
							temp2 = Luminance * (1.0 + Saturation);
						else
							temp2 = Luminance + Saturation - (Luminance * Saturation);

						double temp1 = 2.0 * Luminance - temp2;

						double[] t3 = new double[] { Hue + 1.0 / 3.0, Hue, Hue - 1.0 / 3.0 };
						double[] clr = new double[] { 0, 0, 0 };

						for (int i = 0; i < 3; i++)
						{
							if (t3[i] < 0)
								t3[i] += 1.0;
							if (t3[i] > 1)
								t3[i] -= 1.0;

							if (6.0 * t3[i] < 1.0)
								clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
							else if (2.0 * t3[i] < 1.0)
								clr[i] = temp2;
							else if (3.0 * t3[i] < 2.0)
								clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
							else
								clr[i] = temp1;
						}

						red = clr[0];
						green = clr[1];
						blue = clr[2];
					}
				}

				return Color.FromArgb((int)(255 * red), 
									  (int)(255 * green), 
									  (int)(255 * blue));
			}
		}
		#endregion

		#region Hue
		/// <summary>
		/// Gets and sets the hue.
		/// </summary>
		public double Hue
		{
			get { return _hue; }
			
			set
			{
				// Store new value
				_hue = value;

				// Limit check inside range of 0 -> 1
				if (_hue > 1)
					_hue = 1;
				else if (_hue < 0)
					_hue = 0;
			}
		}
		#endregion

		#region Saturation
		/// <summary>
		/// Gets and sets the saturation.
		/// </summary>
		public double Saturation
		{
			get { return _saturation; }

			set
			{
				// Store new value
				_saturation = value;

				// Limit check inside range of 0 -> 1
				if (_saturation > 1)
					_saturation = 1;
				else if (_saturation < 0)
					_saturation = 0;
			}
		}
		#endregion

		#region Luminance
		/// <summary>
		/// Gets and sets the luminance.
		/// </summary>
		public double Luminance
		{
			get { return _luminance; }

			set
			{
				// Store new value
				_luminance = value;

				// Limit check inside range of 0 -> 1
				if (_luminance > 1)
					_luminance = 1;
				else if (_luminance < 0)
					_luminance = 0;
			}
		}
		#endregion
	}
}
