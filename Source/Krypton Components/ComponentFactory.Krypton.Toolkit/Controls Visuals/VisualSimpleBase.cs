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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the control base with some common changes relevant to krypton simple controls.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public abstract class VisualSimpleBase : VisualControlBase
	{
		#region Identity
		/// <summary>
        /// Initialize a new instance of the VisualSimpleBase class.
		/// </summary>
        protected VisualSimpleBase()
		{
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets and sets the auto size mode.
		/// </summary>
		[Category("Layout")]
		[Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
		[DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
		public virtual AutoSizeMode AutoSizeMode
		{
			get { return base.GetAutoSizeMode(); }

			set
			{
				if (value != base.GetAutoSizeMode())
				{
					base.SetAutoSizeMode(value);

					// Only perform an immediate layout if
					// currently performing auto size operations
					if (AutoSize)
						PerformNeedPaint(true);
				}
			}
		}

		/// <summary>
		/// Get the preferred size of the control based on a proposed size.
		/// </summary>
		/// <param name="proposedSize">Starting size proposed by the caller.</param>
		/// <returns>Calculated preferred size.</returns>
		public override Size GetPreferredSize(Size proposedSize)
		{
			// Do we have a manager to ask for a preferred size?
			if (ViewManager != null)
			{
                // Ask the view to peform a layout
                Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

                // Apply the maximum sizing
                if (MaximumSize.Width > 0) retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
                if (MaximumSize.Height > 0) retSize.Height = Math.Min(MaximumSize.Height, retSize.Width);

                // Apply the minimum sizing
                if (MinimumSize.Width > 0) retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
                if (MinimumSize.Height > 0) retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);

                return retSize;
			}
			else
			{
				// Fall back on default control processing
				return base.GetPreferredSize(proposedSize);
			}
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		public override Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		/// <summary>
		/// Gets or sets the foreground color for the control.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}
		#endregion
	}
}
