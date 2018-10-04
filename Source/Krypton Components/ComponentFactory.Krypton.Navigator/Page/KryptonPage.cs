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
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Page class used inside visual containers.
	/// </summary>
	[ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonPage), "ToolboxBitmaps.KryptonPage.bmp")]
    [DefaultEvent("Click")]
	[DefaultProperty("Text")]
    [Designer("ComponentFactory.Krypton.Navigator.KryptonPageDesigner, ComponentFactory.Krypton.Navigator, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
	[DesignTimeVisible(false)]
    public class KryptonPage : VisualPanel
    {
        #region Instance Fields
        private ViewDrawPanel _drawPanel;
        private PaletteRedirectDoubleMetric _redirectNavigator;
        private PaletteRedirectDoubleMetric _redirectNavigatorHeaderGroup;
        private PaletteRedirectTripleMetric _redirectNavigatorHeaderPrimary;
        private PaletteRedirectTripleMetric _redirectNavigatorHeaderSecondary;
        private PaletteRedirectTripleMetric _redirectNavigatorHeaderBar;
        private PaletteRedirectTripleMetric _redirectNavigatorHeaderOverflow;
        private PaletteRedirectTriple _redirectNavigatorCheckButton;
        private PaletteRedirectTriple _redirectNavigatorOverflowButton;
        private PaletteRedirectTriple _redirectNavigatorMiniButton;
        private PaletteRedirectTriple _redirectNavigatorTab;
        private PaletteRedirectRibbonTabContent _redirectNavigatorRibbonTab;
        private PaletteRedirectMetric _redirectNavigatorBar;
        private PaletteRedirectDouble _redirectNavigatorPage;
        private PaletteRedirectDoubleMetric _redirectNavigatorSeparator;
        private PaletteNavigatorRedirect _stateCommon;
        private PaletteNavigator _stateDisabled;
        private PaletteNavigator _stateNormal;
        private PaletteNavigatorOtherEx _stateTracking;
        private PaletteNavigatorOtherEx _statePressed;
        private PaletteNavigatorOther _stateSelected;
        private PaletteNavigatorOtherRedirect _stateFocus;
        private NeedPaintHandler _needDisabledPaint;
        private NeedPaintHandler _needNormalPaint;
        private PageButtonSpecCollection _buttonSpecs;
        private BoolFlags31 _flags;
        private string _textTitle;
        private string _textDescription;
        private string _toolTipTitle;
        private string _toolTipBody;
        private string _uniqueName;
        private Image _imageSmall;
        private Image _imageMedium;
        private Image _imageLarge;
        private Image _toolTipImage;
        private Color _toolTipImageTransparentColor;
        private bool _setVisible;
        private LabelStyle _toolTipStyle;
        private KryptonContextMenu _kcm;
        private Size _autoHiddenSlideSize;
        #endregion

		#region Events
        /// <summary>
        /// Occurs when the control is loaded.
        /// </summary>
        [Category("Page")]
        [Description("Occurs when the control is loaded.")]
        public event EventHandler Load;

        /// <summary>
        /// Occurs when an appearance specific page property has changed.
        /// </summary>
        [Category("Page")]
        [Description("Occurs when an appearance specific page property has changed.")]
        public event PropertyChangedEventHandler AppearancePropertyChanged;

        /// <summary>
        /// Occurs when the flags have changed.
        /// </summary>
        [Category("Page")]
        [Description("Occurs when the flags have changed.")]
        public event KryptonPageFlagsEventHandler FlagsChanged;

        /// <summary>
        /// Occurs when the AutoHiddenSlideSize property has changed.
        /// </summary>
        [Category("Page")]
        [Description("Occurs when the auto hidden slide size have changed.")]
        public event EventHandler AutoHiddenSlideSizeChanged;

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
		#endregion

		#region Identity
        /// <summary>
		/// Initialize a new instance of the KryptonPage class.
		/// </summary>
        public KryptonPage()
            : this("Page", null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonPage class.
        /// </summary>
        /// <param name="text">Initial text.</param>
        public KryptonPage(string text)
            : this(text, null, null)
        {
        }


        /// <summary>
        /// Initialize a new instance of the KryptonPage class.
        /// </summary>
        /// <param name="text">Initial text.</param>
        /// <param name="uniqueName">Initial unique name.</param>
        public KryptonPage(string text, string uniqueName)
            : this(text, null, uniqueName)
        {
        }

        /// <summary>
		/// Initialize a new instance of the KryptonPage class.
		/// </summary>
        /// <param name="text">Initial text.</param>
        /// <param name="imageSmall">Initial small image.</param>
        /// <param name="uniqueName">Initial unique name.</param>
        public KryptonPage(string text, Image imageSmall, string uniqueName)
		{
            // Default properties
            Text = text;
            MinimumSize = new Size(50, 50);
            _textTitle = "Page Title";
            _textDescription = "Page Description";
            _toolTipTitle = "Page ToolTip";
            _toolTipBody = string.Empty;
            _toolTipImage = null;
            _toolTipStyle = LabelStyle.ToolTip;
            _toolTipImageTransparentColor = Color.Empty;
            _imageSmall = imageSmall;
            _setVisible = true;
            _autoHiddenSlideSize = new Size(200, 200);
            _uniqueName = (string.IsNullOrEmpty(uniqueName) ? CommonHelper.UniqueString : uniqueName);
            _flags.Flags = (int)(KryptonPageFlags.All);
            _flags.ClearFlags((int)KryptonPageFlags.PageInOverflowBarForOutlookMode);

            // Create delegates
            _needDisabledPaint = new NeedPaintHandler(OnNeedDisabledPaint);
            _needNormalPaint = new NeedPaintHandler(OnNeedNormalPaint);

            // Create redirector for inheriting from owning navigator
            _redirectNavigator = new PaletteRedirectDoubleMetric(Redirector);
            _redirectNavigatorPage = new PaletteRedirectDouble(Redirector);
            _redirectNavigatorHeaderGroup = new PaletteRedirectDoubleMetric(Redirector);
            _redirectNavigatorHeaderPrimary = new PaletteRedirectTripleMetric(Redirector);
            _redirectNavigatorHeaderSecondary = new PaletteRedirectTripleMetric(Redirector);
            _redirectNavigatorHeaderBar = new PaletteRedirectTripleMetric(Redirector);
            _redirectNavigatorHeaderOverflow = new PaletteRedirectTripleMetric(Redirector);
            _redirectNavigatorCheckButton = new PaletteRedirectTriple(Redirector);
            _redirectNavigatorOverflowButton = new PaletteRedirectTriple(Redirector);
            _redirectNavigatorMiniButton = new PaletteRedirectTriple(Redirector);
            _redirectNavigatorBar = new PaletteRedirectMetric(Redirector);
            _redirectNavigatorSeparator = new PaletteRedirectDoubleMetric(Redirector);
            _redirectNavigatorTab = new PaletteRedirectTriple(Redirector);
            _redirectNavigatorRibbonTab = new PaletteRedirectRibbonTabContent(Redirector);
            
            // Create the palette storage
            _stateCommon = new PaletteNavigatorRedirect(null, 
                                                        _redirectNavigator, 
                                                        _redirectNavigatorPage, 
                                                        _redirectNavigatorHeaderGroup,
                                                        _redirectNavigatorHeaderPrimary,
                                                        _redirectNavigatorHeaderSecondary,
                                                        _redirectNavigatorHeaderBar,
                                                        _redirectNavigatorHeaderOverflow,
                                                        _redirectNavigatorCheckButton,
                                                        _redirectNavigatorOverflowButton,
                                                        _redirectNavigatorMiniButton,
                                                        _redirectNavigatorBar,
                                                        new PaletteRedirectBorder(Redirector),
                                                        _redirectNavigatorSeparator,
                                                        _redirectNavigatorTab,
                                                        _redirectNavigatorRibbonTab,
                                                        new PaletteRedirectRibbonGeneral(Redirector),
                                                        NeedPaintDelegate);

            _stateDisabled = new PaletteNavigator(_stateCommon, _needDisabledPaint);
            _stateNormal = new PaletteNavigator(_stateCommon, _needNormalPaint);
            _stateTracking = new PaletteNavigatorOtherEx(_stateCommon, _needNormalPaint);
            _statePressed = new PaletteNavigatorOtherEx(_stateCommon, _needNormalPaint);
            _stateSelected = new PaletteNavigatorOther(_stateCommon, _needNormalPaint);
            
            _stateFocus = new PaletteNavigatorOtherRedirect(_redirectNavigatorCheckButton,
                                                            _redirectNavigatorOverflowButton,
                                                            _redirectNavigatorMiniButton, 
                                                            _redirectNavigatorTab, 
                                                            _redirectNavigatorRibbonTab, _needNormalPaint);

            // Our view contains just a simple canvas that covers entire client area
            _drawPanel = new ViewDrawPanel(_stateNormal.Page);

            // Create page specific button spec storage
            _buttonSpecs = new PageButtonSpecCollection(this);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawPanel);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			return "KryptonPage " + Text;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteMode PaletteMode
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return base.PaletteMode; }
            set { throw new OperationCanceledException("Cannot change PaletteMode property"); }
        }

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IPalette Palette
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return base.Palette; }
            set { throw new OperationCanceledException("Cannot change PaletteMode property"); }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PageButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets access to the common page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigatorRedirect StateCommon
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigator StateDisabled
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !StateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigator StateNormal
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigatorOtherEx StateTracking
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !StateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigatorOtherEx StatePressed
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !StatePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the selected page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining selected page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigatorOther StateSelected
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateSelected; }
        }

        private bool ShouldSerializeStateSelected()
        {
            return !StateSelected.IsDefault;
        }

        /// <summary>
        /// Gets access to the focus page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining focus page appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteNavigatorOtherRedirect OverrideFocus
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !OverrideFocus.IsDefault;
        }

        /// <summary>
        /// Gets and sets the page text.
        /// </summary>
        [Bindable(true)]
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The page text.")]
        [DefaultValue("Page")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return base.Text; }
            
            set 
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    OnAppearancePropertyChanged("Text");
                }
            }
        }
 
        /// <summary>
        /// Gets and sets the title text for the page.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("The title text for the page.")]
        [DefaultValue("Page Title")]
        public virtual string TextTitle
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _textTitle; }

            set
            {
                if (_textTitle != value)
                {
                    _textTitle = value;
                    OnAppearancePropertyChanged("TextTitle");
                }
            }
        }

        /// <summary>
        /// Resets the TextTitle property to its default value.
        /// </summary>
        public void ResetTextTitle()
        {
            TextTitle = null;
        }

        /// <summary>
        /// Gets and sets the description text for the page.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("The description text for the page.")]
        [DefaultValue("Page Description")]
        public virtual string TextDescription
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _textDescription; }

            set
            {
                if (_textDescription != value)
                {
                    _textDescription = value;
                    OnAppearancePropertyChanged("TextDescription");
                }
            }
        }

        /// <summary>
        /// Resets the TextDescription property to its default value.
        /// </summary>
        public void ResetTextDescription()
        {
            TextDescription = null;
        }

        /// <summary>
        /// Gets and sets the small image for the page.
        /// </summary>
        [Category("Appearance")]
        [Description("The small image that represents the page.")]
        [Localizable(true)]
        [DefaultValue(null)]
        public virtual Image ImageSmall
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _imageSmall; }

            set
            {
                if (_imageSmall != value)
                {
                    _imageSmall = value;
                    OnAppearancePropertyChanged("ImageSmall");
                }
            }
        }

        /// <summary>
        /// Resets the ImageSmall property to its default value.
        /// </summary>
        public void ResetImageSmall()
        {
            ImageSmall = null;
        }

        /// <summary>
        /// Gets and sets the medium image for the page.
        /// </summary>
        [Category("Appearance")]
        [Description("The medium image that represents the page.")]
        [Localizable(true)]
        [DefaultValue(null)]
        public virtual Image ImageMedium
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _imageMedium; }

            set
            {
                if (_imageMedium != value)
                {
                    _imageMedium = value;
                    OnAppearancePropertyChanged("ImageMedium");
                }
            }
        }

        /// <summary>
        /// Resets the ImageMedium property to its default value.
        /// </summary>
        public void ResetImageMedium()
        {
            ImageMedium = null;
        }

        /// <summary>
        /// Gets and sets the large image for the page.
        /// </summary>
        [Category("Appearance")]
        [Description("The large image that represents the page.")]
        [Localizable(true)]
        [DefaultValue(null)]
        public virtual Image ImageLarge
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _imageLarge; }

            set
            {
                if (_imageLarge != value)
                {
                    _imageLarge = value;
                    OnAppearancePropertyChanged("ImageLarge");
                }
            }
        }

        /// <summary>
        /// Resets the ImageLarge property to its default value.
        /// </summary>
        public void ResetImageLarge()
        {
            ImageLarge = null;
        }

        /// <summary>
        /// Gets and sets the page tooltip image.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Page tooltip image.")]
        [DefaultValue(null)]
        public virtual Image ToolTipImage
        {
            get { return _toolTipImage; }
            
            set 
            {
                if (_toolTipImage != value)
                {
                    _toolTipImage = value;
                    OnAppearancePropertyChanged("ToolTipImage");
                }
            }
        }

        private bool ShouldSerializeToolTipImage()
        {
            return ToolTipImage != null;
        }

        /// <summary>
        /// Resets the ToolTipImage property to its default value.
        /// </summary>
        public void ResetToolTipImage()
        {
            ToolTipImage = null;
        }

        /// <summary>
        /// Gets and sets the tooltip image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Page tooltip image transparent color.")]
        [KryptonDefaultColorAttribute()]
        public virtual Color ToolTipImageTransparentColor
        {
            get { return _toolTipImageTransparentColor; }

            set
            {
                if (_toolTipImageTransparentColor != value)
                {
                    _toolTipImageTransparentColor = value;
                    OnAppearancePropertyChanged("ToolTipImageTransparentColor");
                }
            }
        }

        private bool ShouldSerializeToolTipImageTransparentColor()
        {
            return ToolTipImageTransparentColor != Color.Empty;
        }

        /// <summary>
        /// Resets the ToolTipImageTransparentColor property to its default value.
        /// </summary>
        public void ResetToolTipImageTransparentColor()
        {
            ToolTipImageTransparentColor = Color.Empty;
        }

        /// <summary>
        /// Gets and sets the page tooltip title text.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Page tooltip title text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string ToolTipTitle
        {
            get { return _toolTipTitle; }
            
            set 
            {
                if (_toolTipTitle != value)
                {
                    _toolTipTitle = value;
                    OnAppearancePropertyChanged("ToolTipTitle");
                }
            }
        }

        private bool ShouldSerializeToolTipTitle()
        {
            return ToolTipTitle != string.Empty;
        }

        /// <summary>
        /// Resets the ToolTipTitle property to its default value.
        /// </summary>
        public void ResetToolTipTitle()
        {
            ToolTipTitle = string.Empty;
        }

        /// <summary>
        /// Gets and sets the page tooltip body text.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Page tooltip body text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public virtual string ToolTipBody
        {
            get { return _toolTipBody; }

            set 
            {
                if (_toolTipBody != value)
                {
                    _toolTipBody = value;
                    OnAppearancePropertyChanged("ToolTipBody");
                }
            }
        }

        private bool ShouldSerializeToolTipBody()
        {
            return ToolTipBody != string.Empty;
        }

        /// <summary>
        /// Resets the ToolTipBody property to its default value.
        /// </summary>
        public void ResetToolTipBody()
        {
            ToolTipBody = string.Empty;
        }

        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        [Category("Appearance")]
        [Description("Page tooltip label style.")]
        [DefaultValue(typeof(LabelStyle), "ToolTip")]
        public virtual LabelStyle ToolTipStyle
        {
            get { return _toolTipStyle; }
            
            set 
            {
                if (_toolTipStyle != value)
                {
                    _toolTipStyle = value;
                    OnAppearancePropertyChanged("ToolTipStyle");
                }
            }
        }

        private bool ShouldSerializeToolTipStyle()
        {
            return ToolTipStyle != LabelStyle.ToolTip;
        }

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipStyle()
        {
            ToolTipStyle = LabelStyle.ToolTip;
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut menu to show when the user right-clicks the page.")]
        [DefaultValue(null)]
        public virtual KryptonContextMenu KryptonContextMenu
        {
            get { return _kcm; }
            
            set 
            {
                if (_kcm != value)
                {
                    if (_kcm != null)
                        _kcm.Disposed += new EventHandler(OnKryptonContextMenuDisposed);

                    _kcm = value;

                    if (_kcm != null)
                        _kcm.Disposed -= new EventHandler(OnKryptonContextMenuDisposed);
                }
            }
        }

        /// <summary>
        /// Gets and sets the unique name of the page.
        /// </summary>
        [Category("Appearance")]
        [Description("The unique name of the page.")]
        public virtual string UniqueName
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _uniqueName; }

            [System.Diagnostics.DebuggerStepThrough]
            set { _uniqueName = value; }
        }

        /// <summary>
        /// Resets the UniqueName property to its default value.
        /// </summary>
        public void ResetUniqueName()
        {
            UniqueName = CommonHelper.UniqueString;
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawPanel.FixedState = state;
        }

        /// <summary>
        /// Gets and sets the preferred size for the page when inside an auto hidden slide panel.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual Size AutoHiddenSlideSize
        {
            get { return _autoHiddenSlideSize; }

            set
            {
                if (_autoHiddenSlideSize != value)
                {
                    _autoHiddenSlideSize = value;
                    OnAutoHiddenSlideSizeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Define the state to use when inheriting state values.
        /// </summary>
        /// <param name="alignControl">Control to use when aligning rectangles.</param>
        /// <param name="common">State palette for inheriting common values.</param>
        /// <param name="disabled">State palette for inheriting disabled values.</param>
        /// <param name="normal">State palette for inheriting normal values.</param>
        /// <param name="tracking">State palette for inheriting tracking values.</param>
        /// <param name="pressed">State palette for inheriting pressed values.</param>
        /// <param name="selected">State palette for inheriting selected values.</param>
        /// <param name="focus">State palette for inheriting focus values.</param>
        public virtual void SetInherit(Control alignControl,
                                       PaletteNavigatorRedirect common,
                                       PaletteNavigator disabled,
                                       PaletteNavigator normal,
                                       PaletteNavigatorOtherEx tracking,
                                       PaletteNavigatorOtherEx pressed,
                                       PaletteNavigatorOther selected,
                                       PaletteNavigatorOtherRedirect focus)
        {
            ViewManager.AlignControl = alignControl;

            // Setup the redirection states
            _redirectNavigator.SetRedirectStates(disabled, disabled, normal, normal);
            _redirectNavigatorPage.SetRedirectStates(disabled.PalettePage, normal.PalettePage);
            _redirectNavigatorHeaderGroup.SetRedirectStates(disabled.HeaderGroup, disabled.HeaderGroup, normal.HeaderGroup, normal.HeaderGroup);
            _redirectNavigatorHeaderPrimary.SetRedirectStates(disabled.HeaderGroup.HeaderPrimary, disabled.HeaderGroup.HeaderPrimary, normal.HeaderGroup.HeaderPrimary, normal.HeaderGroup.HeaderPrimary);
            _redirectNavigatorHeaderSecondary.SetRedirectStates(disabled.HeaderGroup.HeaderSecondary, disabled.HeaderGroup.HeaderSecondary,  normal.HeaderGroup.HeaderSecondary, normal.HeaderGroup.HeaderSecondary);
            _redirectNavigatorHeaderBar.SetRedirectStates(disabled.HeaderGroup.HeaderBar, disabled.HeaderGroup.HeaderBar, normal.HeaderGroup.HeaderBar, normal.HeaderGroup.HeaderBar);
            _redirectNavigatorHeaderOverflow.SetRedirectStates(disabled.HeaderGroup.HeaderOverflow, disabled.HeaderGroup.HeaderOverflow, normal.HeaderGroup.HeaderOverflow, normal.HeaderGroup.HeaderOverflow);
            _redirectNavigatorCheckButton.SetRedirectStates(disabled.CheckButton, normal.CheckButton, pressed.CheckButton, tracking.CheckButton, selected.CheckButton, selected.CheckButton, selected.CheckButton, focus.CheckButton, null);
            _redirectNavigatorOverflowButton.SetRedirectStates(disabled.OverflowButton, normal.OverflowButton, pressed.OverflowButton, tracking.OverflowButton, selected.OverflowButton, selected.OverflowButton, selected.OverflowButton, focus.OverflowButton, null);
            _redirectNavigatorMiniButton.SetRedirectStates(disabled.MiniButton, normal.MiniButton, pressed.MiniButton, tracking.MiniButton, selected.MiniButton, selected.MiniButton, selected.MiniButton, focus.MiniButton, null);
            _redirectNavigatorBar.SetRedirectStates(common.Bar, common.Bar);
            _redirectNavigatorSeparator.SetRedirectStates(disabled.Separator, disabled.Separator, normal.Separator, normal.Separator, pressed.Separator, pressed.Separator, tracking.Separator, tracking.Separator);
            _redirectNavigatorTab.SetRedirectStates(disabled.Tab, normal.Tab, pressed.Tab, tracking.Tab, selected.Tab, selected.Tab, selected.Tab, focus.Tab, null);
            _redirectNavigatorRibbonTab.SetRedirectStates(disabled.RibbonTab, normal.RibbonTab, pressed.RibbonTab, tracking.RibbonTab, selected.RibbonTab, focus.RibbonTab);
            _stateCommon.RedirectBorderEdge = new PaletteRedirectBorderEdge(Redirector, disabled.BorderEdge, normal.BorderEdge);
            _stateCommon.RedirectRibbonGeneral = new PaletteRedirectRibbonGeneral(Redirector);
        }

        /// <summary>
        /// Reset the state palettes so they no longer inherit from external source.
        /// </summary>
        /// <param name="alignControl">Only if inherited values are still the same as when the aligned control was set are they reset.</param>
        public virtual void ResetInherit(Control alignControl)
        {
            if (alignControl == ViewManager.AlignControl)
            {
                ViewManager.AlignControl = this;

                // Clear down the redirection states
                _redirectNavigator.ResetRedirectStates();
                _redirectNavigatorPage.ResetRedirectStates();
                _redirectNavigatorHeaderGroup.ResetRedirectStates();
                _redirectNavigatorHeaderPrimary.ResetRedirectStates();
                _redirectNavigatorHeaderSecondary.ResetRedirectStates();
                _redirectNavigatorHeaderBar.ResetRedirectStates();
                _redirectNavigatorHeaderOverflow.ResetRedirectStates();
                _redirectNavigatorCheckButton.ResetRedirectStates();
                _redirectNavigatorOverflowButton.ResetRedirectStates();
                _redirectNavigatorMiniButton.ResetRedirectStates();
                _redirectNavigatorBar.ResetRedirectStates();
                _redirectNavigatorSeparator.ResetRedirectStates();
                _redirectNavigatorTab.ResetRedirectStates();
                _redirectNavigatorRibbonTab.ResetRedirectStates();
                _stateCommon.RedirectBorderEdge = new PaletteRedirectBorder(Redirector);
                _stateCommon.RedirectRibbonGeneral = new PaletteRedirectRibbonGeneral(Redirector);
            }
        }

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		/// <summary>
		/// Gets or sets which edges of the control are anchored to the edges of its container.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor
		{
			get { return base.Anchor; }
			set { base.Anchor = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}

        /// <summary>
        /// Gets or sets the size of the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
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
			set { base.AutoSizeMode = value; }
		}

		/// <summary>
		/// Gets or sets which edge of the parent container a control is docked to.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}

		/// <summary>
		/// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
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
		/// Gets or sets the tab order of the control within its container.
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
		/// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
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
        /// Gets the string that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Text mapping.</param>
        /// <returns>Matching string.</returns>
        public virtual string GetTextMapping(MapKryptonPageText mapping)
        {
            string ret = string.Empty;

            // Recover the first preference value
            switch (mapping)
            {
                case MapKryptonPageText.Text:
                case MapKryptonPageText.TextTitle:
                case MapKryptonPageText.TextTitleDescription:
                case MapKryptonPageText.TextDescription:
                    ret = Text;
                    break;
                case MapKryptonPageText.Title:
                case MapKryptonPageText.TitleDescription:
                case MapKryptonPageText.TitleText:
                    ret = TextTitle;
                    break;
                case MapKryptonPageText.Description:
                case MapKryptonPageText.DescriptionText:
                case MapKryptonPageText.DescriptionTitle:
                case MapKryptonPageText.DescriptionTitleText:
                    ret = TextDescription;
                    break;
                case MapKryptonPageText.ToolTipTitle:
                    ret = ToolTipTitle;
                    break;
                case MapKryptonPageText.ToolTipBody:
                    ret = ToolTipBody;
                    break;
            }

            // If nothing found then...
            if (string.IsNullOrEmpty(ret))
            {
                // Recover the second preference value
                switch (mapping)
                {
                    case MapKryptonPageText.TitleText:
                    case MapKryptonPageText.DescriptionText:
                        ret = Text;
                        break;
                    case MapKryptonPageText.TextTitle:
                    case MapKryptonPageText.TextTitleDescription:
                    case MapKryptonPageText.DescriptionTitle:
                    case MapKryptonPageText.DescriptionTitleText:
                        ret = TextTitle;
                        break;
                    case MapKryptonPageText.TextDescription:
                    case MapKryptonPageText.TitleDescription:
                        ret = TextDescription;
                        break;
                }
            }

            // If nothing found then...
            if (string.IsNullOrEmpty(ret))
            {
                // Recover the third preference value
                switch (mapping)
                {
                    case MapKryptonPageText.DescriptionTitleText:
                        ret = Text;
                        break;
                    case MapKryptonPageText.TextTitleDescription:
                        ret = TextDescription;
                        break;
                }
            }

            // We do not want to return a null
            if (ret == null)
                ret = string.Empty;

            return ret;
        }

        /// <summary>
        /// Gets the image that matches the mapping request.
        /// </summary>
        /// <param name="mapping">Image mapping.</param>
        /// <returns>Image reference.</returns>
        public virtual Image GetImageMapping(MapKryptonPageImage mapping)
        {
            Image ret = null;

            // Recover the first preference value
            switch (mapping)
            {
                case MapKryptonPageImage.Small:
                case MapKryptonPageImage.SmallMedium:
                case MapKryptonPageImage.SmallMediumLarge:
                    ret = ImageSmall;
                    break;
                case MapKryptonPageImage.Medium:
                case MapKryptonPageImage.MediumLarge:
                case MapKryptonPageImage.MediumSmall:
                    ret = ImageMedium;
                    break;
                case MapKryptonPageImage.Large:
                case MapKryptonPageImage.LargeMedium:
                case MapKryptonPageImage.LargeMediumSmall:
                    ret = ImageLarge;
                    break;
                case MapKryptonPageImage.ToolTip:
                    ret = ToolTipImage;
                    break;
            }

            // If nothing found then...
            if (ret == null)
            {
                // Recover the second preference value
                switch (mapping)
                {
                    case MapKryptonPageImage.MediumSmall:
                        ret = ImageSmall;
                        break;
                    case MapKryptonPageImage.SmallMedium:
                    case MapKryptonPageImage.SmallMediumLarge:
                    case MapKryptonPageImage.LargeMedium:
                    case MapKryptonPageImage.LargeMediumSmall:
                        ret = ImageMedium;
                        break;
                    case MapKryptonPageImage.MediumLarge:
                        ret = ImageLarge;
                        break;
                }
            }

            // If nothing found then...
            if (ret == null)
            {
                // Recover the third preference value
                switch (mapping)
                {
                    case MapKryptonPageImage.LargeMediumSmall:
                        ret = ImageSmall;
                        break;
                    case MapKryptonPageImage.SmallMediumLarge:
                        ret = ImageLarge;
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Gets the Krypton control that is acting as the parent.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Control KryptonParentContainer
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return ViewManager.AlignControl; }
        }

        /// <summary>
        /// Gets and sets the set of page flags.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(0)]
        public virtual int Flags
        {
            get { return _flags.Flags; }
            
            set
            {
                if (_flags.Flags != value)
                {
                    int changed = _flags.Flags ^ value;
                    _flags.Flags = value;
                    OnFlagsChanged((KryptonPageFlags)changed);
                }
            }
        }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public virtual void SetFlags(KryptonPageFlags flags)
        {
            int changed = _flags.SetFlags((int)flags);

            if (changed != 0)
                OnFlagsChanged((KryptonPageFlags)changed);
        }

        /// <summary>
        /// Sets all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        public virtual void ClearFlags(KryptonPageFlags flags)
        {
            int changed = _flags.ClearFlags((int)flags);

            if (changed != 0)
                OnFlagsChanged((KryptonPageFlags)changed);
        }

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all provided flags are defined as true; otherwise false.</returns>
        public virtual bool AreFlagsSet(KryptonPageFlags flags)
        {
            return _flags.AreFlagsSet((int)flags);
        }

        /// <summary>
        /// Gets the last value set to the Visible property.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual bool LastVisibleSet
        {
            get { return _setVisible; }
            
            set 
            {
                if (value != _setVisible)
                {
                    _setVisible = value;

                    // Must generate event manually because if we are set to false and the parent
                    // chain is also false then an event will not be generated automatically.
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// The OnCreateControl method is called when the control is first created.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnLoad(EventArgs.Empty);
        }

        /// <summary>
        /// Sets the control to the specified visible state. 
        /// </summary>
        /// <param name="value">true to make the control visible; otherwise, false.</param>
        protected override void SetVisibleCore(bool value)
        {
            LastVisibleSet = value;
            base.SetVisibleCore(value);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // We need to snoop the need to show a context menu
            if (m.Msg == PI.WM_CONTEXTMENU)
            {
                // Only interested in overriding the behavior when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)((long)m.LParam)) == -1)
                        mousePt = new Point(Width / 2, Height / 2);
                    else
                        mousePt = PointToClient(mousePt);

                    // If the mouse posiiton is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        if (!DesignMode)
                        {
                            // Show the context menu
                            KryptonContextMenu.Show(this, PointToScreen(mousePt));

                            // We eat the message!
                            return;
                        }
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Push correct palettes into the view
            _drawPanel.SetPalettes(Enabled ? _stateNormal.Page : _stateDisabled.Page);

            // Update state of view panel to reflect page state
            _drawPanel.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }


        /// <summary>
		/// Raises the DockChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnDockChanged(EventArgs e)
		{
			if (DockChanged != null)
				DockChanged(this, e);
		}

		/// <summary>
		/// Raises the LocationChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnLocationChanged(EventArgs e)
		{
			if (LocationChanged != null)
				LocationChanged(this, e);
		}

		/// <summary>
		/// Raises the TabIndexChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnTabIndexChanged(EventArgs e)
		{
			if (TabIndexChanged != null)
				TabIndexChanged(this, e);
		}

		/// <summary>
		/// Raises the TabStopChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnTabStopChanged(EventArgs e)
		{
			if (TabStopChanged != null)
				TabStopChanged(this, e);
		}

        /// <summary>
        /// Raises the AppearancePropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the appearance property that has changed.</param>
        protected virtual void OnAppearancePropertyChanged(string propertyName)
        {
            if (AppearancePropertyChanged != null)
                AppearancePropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the FlagsChanged event.
        /// </summary>
        /// <param name="changed">Set of flags that have changed.</param>
        protected virtual void OnFlagsChanged(KryptonPageFlags changed)
        {
            if (FlagsChanged != null)
                FlagsChanged(this, new KryptonPageFlagsEventArgs(changed));
        }

        /// <summary>
        /// Raises the AutoHiddenSlideSizeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnAutoHiddenSlideSizeChanged(EventArgs e)
        {
            if (AutoHiddenSlideSizeChanged != null)
                AutoHiddenSlideSizeChanged(this, e);
        }

        /// <summary>
        /// Raises the Load event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLoad(EventArgs e)
        {
            if (Load != null)
                Load(this, e);
        }

        /// <summary>
		/// Processes the need for a repaint for the disabled palette values.
		/// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedDisabledPaint(object sender, NeedLayoutEventArgs e)
		{
            if (!Enabled)
            {
                OnAppearancePropertyChanged("Palette");
                OnNeedPaint(this, e);
            }
		}

        /// <summary>
        /// Processes the need for a repaint for the enabled palette values.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedNormalPaint(object sender, NeedLayoutEventArgs e)
        {
            if (Enabled)
            {
                OnAppearancePropertyChanged("Palette");
                OnNeedPaint(this, e);
            }
        }
        #endregion

        #region Implementation
        private void OnKryptonContextMenuDisposed(object sender, EventArgs e)
        {
            // When the current krypton context menu is disposed, we should remove 
            // it to prevent it being used again, as that would just throw an exception 
            // because it has been disposed.
            KryptonContextMenu = null;
        }
        #endregion
    }
}
