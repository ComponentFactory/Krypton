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
	/// Base class used for implementation of container controls using a Control as the base.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
	public abstract class VisualControlContainment : VisualControl
	{
		#region Identity
		/// <summary>
		/// Initialize a new instance of the VisualContainer class.
		/// </summary>
        protected VisualControlContainment()
		{
			// We act as a container for child controls
			SetStyle(ControlStyles.ContainerControl, true);

			// User cannot select a container control itself
			SetStyle(ControlStyles.Selectable, false);

			// Default TabStop is true in base class, we want it to be false
			TabStop = false;
		}
		#endregion

		#region Public Overrides
		/// <summary>
		/// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(false)]
		public new bool TabStop
		{
			get { return base.TabStop; }
			set { base.TabStop = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}

		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
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

		#region Protected Overrides
		/// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(150, 150); }
		}
		#endregion
	}
}
