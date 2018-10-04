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
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Special panel used in the KryptonSplitContainer.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
    [ToolboxBitmap(typeof(KryptonSplitterPanel), "ToolboxBitmaps.KryptonGroupPanel.bmp")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonSplitterPanelDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [Description("Enables you to group collections of controls.")]
    [Docking(DockingBehavior.Never)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public sealed class KryptonSplitterPanel : KryptonPanel
	{
		#region Instance Fields
		private bool _collapsed;
		private KryptonSplitContainer _container;
		#endregion

		#region Events
		/// <summary>
		/// Occurs when the value of the AutoSize property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler AutoSizeChanged;

		/// <summary>
		/// Occurs when the value of the Dock property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler DockChanged;

		/// <summary>
		/// Occurs when the value of the Location property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler LocationChanged;

		/// <summary>
		/// Occurs when the value of the TabIndex property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler TabIndexChanged;

		/// <summary>
		/// Occurs when the value of the TabStop property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler TabStopChanged;

		/// <summary>
		/// Occurs when the value of the Visible property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler VisibleChanged;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the KryptonSplitterPanel class.
		/// </summary>
		/// <param name="container">Reference to owning container.</param>
		public KryptonSplitterPanel(KryptonSplitContainer container)
		{
			Debug.Assert(container != null);

			_container = container;
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets or sets how a KryptonSplitterPanel attaches to the edges of the KryptonSplitContainer.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor
		{
			get { return base.Anchor; }
            set { /* Ignore request */ }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the KryptonSplitterPanel is automatically resized to display its entire contents.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
            set { /* Ignore request */ }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AutoSizeMode AutoSizeMode
		{
			get { return base.AutoSizeMode; }
			set { /* Ignore request */ }
		}

		/// <summary>
		/// Gets or sets the border style for the KryptonSplitterPanel. 
		/// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
            set { /* Ignore request */ }
		}

		/// <summary>
		/// Gets or sets which edge of the KryptonSplitContainer that the KryptonSplitterPanel is docked to. 
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock
		{
			get { return base.Dock; }
            set { /* Ignore request */ }
		}

		/// <summary>
		/// Gets the internal spacing between the KryptonSplitterPanel and its edges.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DockPaddingEdges DockPadding
		{
			get { return base.DockPadding; }
		}

		/// <summary>
		/// Gets or sets the height of the KryptonSplitterPanel. 
		/// </summary>
        [Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int Height
		{
			get
			{
				if (Collapsed)
					return 0;
				else
					return base.Height;
			}

			set
			{
				throw new NotSupportedException("Cannot set the Height of a KryptonSplitterPanel");
			}
		}

		/// <summary>
		/// Gets or sets the coordinates of the upper-left corner of the KryptonSplitterPanel relative to the upper-left corner of its KryptonSplitContainer. 
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get { return base.Location; }
			set { base.Location = value; }
		}

		/// <summary>
		/// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}

		/// <summary>
		/// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}

		/// <summary>
		/// The name of this KryptonSplitterPanel.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string Name
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		/// <summary>
		/// The name of this KryptonSplitterPanel.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control Parent
		{
			get { return base.Parent; }
			set { base.Parent = value; }
		}

		/// <summary>
		/// Gets or sets the height and width of the KryptonSplitterPanel.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size Size
		{
			get 
			{
				if (Collapsed)
					return Size.Empty;
				else
					return base.Size;
			}

			set { base.Size = value; }
		}

		/// <summary>
		/// Gets or sets the tab order of the KryptonSplitterPanel within its KryptonSplitContainer.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get { return base.TabIndex; }
			set { base.TabIndex = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the user can give the focus to this KryptonSplitterPanel using the TAB key.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool TabStop
		{
			get { return base.TabStop; }
			set { base.TabStop = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the KryptonSplitterPanel is displayed.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}

		/// <summary>
		/// Gets or sets the width of the KryptonSplitterPanel. 
		/// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int Width
		{
			get
			{
				if (Collapsed)
					return 0;
				else
					return base.Width;
			}

			set
			{
				throw new NotSupportedException("Cannot set the Width of a KryptonSplitterPanel");
			}
		}

		#endregion

		#region Protected Overrides
		/// <summary>
		/// Gets the space, in pixels, that is specified by default between controls.
		/// </summary>
		protected override Padding DefaultMargin
		{
			get { return new Padding(0, 0, 0, 0); }
		}

		/// <summary>
		/// Raises the AutoSizeChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			if (AutoSizeChanged != null)
				AutoSizeChanged(this, e);

            base.OnAutoSizeChanged(e);
        }

		/// <summary>
		/// Raises the DockChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnDockChanged(EventArgs e)
		{
			if (DockChanged != null)
				DockChanged(this, e);

            base.OnDockChanged(e);
        }

		/// <summary>
		/// Raises the LocationChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnLocationChanged(EventArgs e)
		{
			if (LocationChanged != null)
				LocationChanged(this, e);

            base.OnLocationChanged(e);
        }

		/// <summary>
		/// Raises the TabIndexChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnTabIndexChanged(EventArgs e)
		{
			if (TabIndexChanged != null)
				TabIndexChanged(this, e);

            base.OnTabIndexChanged(e);
        }

		/// <summary>
		/// Raises the TabStopChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnTabStopChanged(EventArgs e)
		{
			if (TabStopChanged != null)
				TabStopChanged(this, e);

            base.OnTabStopChanged(e);
        }

		/// <summary>
		/// Raises the VisibleChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (VisibleChanged != null)
				VisibleChanged(this, e);

            base.OnVisibleChanged(e);
		}
        #endregion

		#region Internal
		internal KryptonSplitContainer Owner
		{
			get { return _container; }
		}

		internal bool Collapsed
		{
			get { return _collapsed; }
			set { _collapsed = value; }
		}
		#endregion
	}
}
