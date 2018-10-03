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
	/// Storage for button related properties.
	/// </summary>
    public class NavigatorButton : Storage
    {
        #region Static Fields
        private static readonly Keys _defaultShortcutPrevious = (Keys.Control | Keys.Shift | Keys.F6);
        private static readonly Keys _defaultShortcutNext = (Keys.Control | Keys.F6);
        private static readonly Keys _defaultShortcutContext = (Keys.Control | Keys.Alt | Keys.Down);
        private static readonly Keys _defaultShortcutClose = (Keys.Control | Keys.F4);
        #endregion

        #region Instance Fields
        private KryptonNavigator _navigator;
        private NavFixedButtonSpecCollection _fixedSpecs;
        private NavigatorButtonSpecCollection _buttonSpecs;
        private ButtonSpecNavPrevious _fixedPrevious;
        private DirectionButtonAction _actionPrevious;
        private Keys _shortcutPrevious;
        private ButtonDisplay _displayPrevious;
        private ButtonSpecNavNext _fixedNext;
        private DirectionButtonAction _actionNext;
        private Keys _shortcutNext;
        private ButtonDisplay _displayNext;
        private ButtonSpecNavContext _fixedContext;
        private ContextButtonAction _actionContext;
        private Keys _shortcutContext;
        private ButtonDisplay _displayContext;
        private MapKryptonPageText _mapTextContext;
        private MapKryptonPageImage _mapImageContext;
        private ButtonSpecNavClose _fixedClose;
        private CloseButtonAction _actionClosed;
        private Keys _shortcutClose;
        private ButtonDisplay _displayClosed;
        private ButtonDisplayLogic _displayLogic;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorButton class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorButton(KryptonNavigator navigator,
                               NeedPaintHandler needPaint)
		{
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create collection for use defined and fixed buttons
            _buttonSpecs = new NavigatorButtonSpecCollection(navigator);
            _fixedSpecs = new NavFixedButtonSpecCollection(navigator);

            // Create the fixed buttons
            _fixedPrevious = new ButtonSpecNavPrevious(_navigator);
            _fixedNext = new ButtonSpecNavNext(_navigator);
            _fixedContext = new ButtonSpecNavContext(_navigator);
            _fixedClose = new ButtonSpecNavClose(_navigator);

            // Hook into the click events for the buttons
            _fixedPrevious.Click += new EventHandler(OnPreviousClick);
            _fixedNext.Click += new EventHandler(OnNextClick);
            _fixedContext.Click += new EventHandler(OnContextClick);
            _fixedClose.Click += new EventHandler(OnCloseClick);

            // Add fixed buttons into the display collection
            _fixedSpecs.AddRange(new ButtonSpecNavFixed[] { _fixedPrevious, _fixedNext, _fixedContext, _fixedClose });

            // Default fields
            _displayLogic = ButtonDisplayLogic.Context;
            _mapTextContext = MapKryptonPageText.TextTitle;
            _mapImageContext = MapKryptonPageImage.Small;
            _actionClosed = CloseButtonAction.RemovePageAndDispose;
            _actionContext = ContextButtonAction.SelectPage;
            _actionPrevious = _actionNext = DirectionButtonAction.ModeAppropriateAction;
            _displayPrevious = _displayNext = _displayContext = _displayClosed = ButtonDisplay.Logic;
            _shortcutClose = _defaultShortcutClose;
            _shortcutContext = _defaultShortcutContext;
            _shortcutNext = _defaultShortcutNext;
            _shortcutPrevious = _defaultShortcutPrevious;
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return ((ButtonSpecs.Count == 0) &&
                        PreviousButton.IsDefault &&
                        (PreviousButtonAction == DirectionButtonAction.ModeAppropriateAction) &&
                        (PreviousButtonDisplay == ButtonDisplay.Logic) &&
                        (PreviousButtonShortcut == _defaultShortcutPrevious) &&
                        NextButton.IsDefault &&
                        (NextButtonAction == DirectionButtonAction.ModeAppropriateAction) &&
                        (NextButtonDisplay == ButtonDisplay.Logic) &&
                        (NextButtonShortcut == _defaultShortcutNext) &&
                        ContextButton.IsDefault &&
                        (ContextButtonDisplay == ButtonDisplay.Logic) &&
                        (ContextButtonShortcut == _defaultShortcutContext) &&
                        (ContextMenuMapText == MapKryptonPageText.TextTitle) &&
                        (ContextMenuMapImage == MapKryptonPageImage.Small) &&
                        CloseButton.IsDefault &&
                        (CloseButtonAction == CloseButtonAction.RemovePageAndDispose) &&
                        (CloseButtonDisplay == ButtonDisplay.Logic) &&
                        (CloseButtonShortcut == _defaultShortcutClose) &&
                        (ButtonDisplayLogic == ButtonDisplayLogic.Context));
            }
        }
        #endregion

        #region ButtonSpecs
        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }
        #endregion

        #region PreviousButton
        /// <summary>
        /// Gets access to the previous button specification.
        /// </summary>
        [Category("Visuals")]
        [Description("Previous button specification.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonSpecNavPrevious PreviousButton
        {
            get { return _fixedPrevious; }
        }

        private bool ShouldSerializePreviousButton()
        {
            return !_fixedPrevious.IsDefault;
        }
        #endregion

        #region PreviousButtonAction
        /// <summary>
        /// Gets and sets the action to take when the previous button is clicked.
        /// </summary>
        [Category("Visuals")]
        [Description("Action to take when the previous button is clicked.")]
        [DefaultValue(typeof(DirectionButtonAction), "Mode Appropriate Action")]
        public DirectionButtonAction PreviousButtonAction
        {
            get { return _actionPrevious; }

            set
            {
                if (_actionPrevious != value)
                {
                    _actionPrevious = value;
                    _navigator.OnViewBuilderPropertyChanged("PreviousButtonAction");
                }
            }
        }
        #endregion

        #region PreviousButtonDisplay
        /// <summary>
        /// Gets and set the logic used to decide how to show the previous button.
        /// </summary>
        [Category("Visuals")]
        [Description("Logic used to decide if previous button is displayed.")]
        [DefaultValue(typeof(ButtonDisplay), "Logic")]
        public ButtonDisplay PreviousButtonDisplay
        {
            get { return _displayPrevious; }

            set
            {
                if (_displayPrevious != value)
                {
                    _displayPrevious = value;
                    _navigator.OnViewBuilderPropertyChanged("PreviousButtonDisplay");
                }
            }
        }
        #endregion

        #region PreviousButtonShortcut
        /// <summary>
        /// Gets access to the shortcut for invoking the previous action.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut for invoking the previous action.")]
        [DefaultValue(typeof(Keys), "F6, Shift, Control")]
        public Keys PreviousButtonShortcut
        {
            get { return _shortcutPrevious; }
            set { _shortcutPrevious = value; }
        }

        private bool ShouldSerializePreviousButtonShortcut()
        {
            return (PreviousButtonShortcut != _defaultShortcutPrevious);
        }

        /// <summary>
        /// Resets the PreviousButtonShortcut property to its default value.
        /// </summary>
        public void ResetPreviousButtonShortcut()
        {
            PreviousButtonShortcut = _defaultShortcutPrevious;
        }
        #endregion

        #region NextButton
        /// <summary>
        /// Gets access to the next button specification.
        /// </summary>
        [Category("Visuals")]
        [Description("Next button specification.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonSpecNavNext NextButton
        {
            get { return _fixedNext; }
        }

        private bool ShouldSerializeNextButton()
        {
            return !_fixedNext.IsDefault;
        }
        #endregion

        #region NextButtonAction
        /// <summary>
        /// Gets and sets the action to take when the next button is clicked.
        /// </summary>
        [Category("Visuals")]
        [Description("Action to take when the next button is clicked.")]
        [DefaultValue(typeof(DirectionButtonAction), "Mode Appropriate Action")]
        public DirectionButtonAction NextButtonAction
        {
            get { return _actionNext; }

            set
            {
                if (_actionNext != value)
                {
                    _actionNext = value;
                    _navigator.OnViewBuilderPropertyChanged("NextButtonAction");
                }
            }
        }
        #endregion

        #region NextButtonDisplay
        /// <summary>
        /// Gets and set the logic used to decide how to show the next button.
        /// </summary>
        [Category("Visuals")]
        [Description("Logic used to decide if next button is displayed.")]
        [DefaultValue(typeof(ButtonDisplay), "Logic")]
        public ButtonDisplay NextButtonDisplay
        {
            get { return _displayNext; }

            set
            {
                if (_displayNext != value)
                {
                    _displayNext = value;
                    _navigator.OnViewBuilderPropertyChanged("NextButtonDisplay");
                }
            }
        }
        #endregion

        #region NextButtonShortcut
        /// <summary>
        /// Gets access to the shortcut for invoking the next action.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut for invoking the next action.")]
        [DefaultValue(typeof(Keys), "F6, Control")]
        public Keys NextButtonShortcut
        {
            get { return _shortcutNext; }
            set { _shortcutNext = value; }
        }

        private bool ShouldSerializeNextButtonShortcut()
        {
            return (NextButtonShortcut != _defaultShortcutNext);
        }

        /// <summary>
        /// Resets the NextButtonShortcut property to its default value.
        /// </summary>
        public void ResetNextButtonShortcut()
        {
            NextButtonShortcut = _defaultShortcutNext;
        }
        #endregion

        #region ContextButton
        /// <summary>
        /// Gets access to the context button specification.
        /// </summary>
        [Category("Visuals")]
        [Description("Context button specification.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonSpecNavContext ContextButton
        {
            get { return _fixedContext; }
        }

        private bool ShouldSerializeContextButton()
        {
            return !_fixedContext.IsDefault;
        }
        #endregion

        #region ContextButtonAction
        /// <summary>
        /// Gets and sets the action to take when the context button is clicked.
        /// </summary>
        [Category("Visuals")]
        [Description("Action to take when the context button is clicked.")]
        [DefaultValue(typeof(ContextButtonAction), "Select Page")]
        public ContextButtonAction ContextButtonAction
        {
            get { return _actionContext; }

            set
            {
                if (_actionContext != value)
                {
                    _actionContext = value;
                    _navigator.OnViewBuilderPropertyChanged("ContextButtonAction");
                }
            }
        }
        #endregion

        #region ContextButtonDisplay
        /// <summary>
        /// Gets and set the logic used to decide how to show the context button.
        /// </summary>
        [Category("Visuals")]
        [Description("Logic used to decide if context button is displayed.")]
        [DefaultValue(typeof(ButtonDisplay), "Logic")]
        public ButtonDisplay ContextButtonDisplay
        {
            get { return _displayContext; }

            set
            {
                if (_displayContext != value)
                {
                    _displayContext = value;
                    _navigator.OnViewBuilderPropertyChanged("ContextButtonDisplay");
                }
            }
        }
        #endregion

        #region ContextButtonShortcut
        /// <summary>
        /// Gets access to the shortcut for invoking the context action.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut for invoking the context action.")]
        [DefaultValue(typeof(Keys), "Down, Alt, Control")]
        public Keys ContextButtonShortcut
        {
            get { return _shortcutContext; }
            set { _shortcutContext = value; }
        }

        private bool ShouldSerializeContextButtonShortcut()
        {
            return (ContextButtonShortcut != _defaultShortcutContext);
        }

        /// <summary>
        /// Resets the ContextButtonShortcut property to its default value.
        /// </summary>
        public void ResetContextButtonShortcut()
        {
            ContextButtonShortcut = _defaultShortcutContext;
        }
        #endregion

        #region ContextMenuMapText
        /// <summary>
        /// Gets and set the mapping used to generate context menu item image.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used to generate context menu item image.")]
        [DefaultValue(typeof(MapKryptonPageText), "Text - Title")]
        public MapKryptonPageText ContextMenuMapText
        {
            get { return _mapTextContext; }
            set { _mapTextContext = value; }
        }
        #endregion

        #region ContextMenuMapImage
        /// <summary>
        /// Gets and set the mapping used to generate context menu item text.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used to generate context menu item text.")]
        [DefaultValue(typeof(MapKryptonPageImage), "Small")]
        public MapKryptonPageImage ContextMenuMapImage
        {
            get { return _mapImageContext; }
            set { _mapImageContext = value; }
        }
        #endregion

        #region CloseButton
        /// <summary>
        /// Gets access to the close button specification.
        /// </summary>
        [Category("Visuals")]
        [Description("Close button specification.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonSpecNavClose CloseButton
        {
            get { return _fixedClose; }
        }

        private bool ShouldSerializeCloseButton()
        {
            return !_fixedClose.IsDefault;
        }
        #endregion

        #region CloseButtonAction
        /// <summary>
        /// Gets and sets the action to take when the close button is clicked.
        /// </summary>
        [Category("Visuals")]
        [Description("Action to take when the close button is clicked.")]
        [DefaultValue(typeof(CloseButtonAction), "RemovePage & Dispose")]
        public CloseButtonAction CloseButtonAction
        {
            get { return _actionClosed; }

            set
            {
                if (_actionClosed != value)
                {
                    _actionClosed = value;
                    _navigator.OnViewBuilderPropertyChanged("CloseButtonAction");
                }
            }
        }
        #endregion

        #region CloseButtonDisplay
        /// <summary>
        /// Gets and set the logic used to decide how to show the close button.
        /// </summary>
        [Category("Visuals")]
        [Description("Logic used to decide if close button is displayed.")]
        [DefaultValue(typeof(ButtonDisplay), "Logic")]
        public ButtonDisplay CloseButtonDisplay
        {
            get { return _displayClosed; }

            set
            {
                if (_displayClosed != value)
                {
                    _displayClosed = value;
                    _navigator.OnViewBuilderPropertyChanged("CloseButtonDisplay");
                }
            }
        }
        #endregion

        #region CloseButtonShortcut
        /// <summary>
        /// Gets access to the shortcut for invoking the close action.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Shortcut for invoking the close action.")]
        [DefaultValue(typeof(Keys), "F4, Control")]
        public Keys CloseButtonShortcut
        {
            get { return _shortcutClose; }
            set { _shortcutClose = value; }
        }

        private bool ShouldSerializeCloseButtonShortcut()
        {
            return (CloseButtonShortcut != _defaultShortcutClose);
        }

        /// <summary>
        /// Resets the CloseButtonShortcut property to its default value.
        /// </summary>
        public void ResetCloseButtonShortcut()
        {
            CloseButtonShortcut = _defaultShortcutClose;
        }
        #endregion

        #region ButtonDisplayLogic
        /// <summary>
        /// Gets and sets the logic used to control button display.
        /// </summary>
        [Category("Visuals")]
        [Description("Define the logic used to control button display.")]
        [DefaultValue(typeof(ButtonDisplayLogic), "Context")]
        public ButtonDisplayLogic ButtonDisplayLogic
        {
            get { return _displayLogic; }

            set
            {
                if (_displayLogic != value)
                {
                    _displayLogic = value;
                    _navigator.OnViewBuilderPropertyChanged("ButtonDisplayLogic");
                }
            }
        }

        /// <summary>
        /// Resets the ButtonDisplayLogic property to its default value.
        /// </summary>
        public void ResetButtonDisplayLogic()
        {
            ButtonDisplayLogic = ButtonDisplayLogic.Context;
        }
        #endregion

        #region Internal
        internal NavFixedButtonSpecCollection FixedSpecs
        {
            get { return _fixedSpecs; }
        }
        #endregion

        #region Implementation
        private void OnPreviousClick(object sender, EventArgs e)
        {
            _navigator.PerformPreviousAction();
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            _navigator.PerformNextAction();
        }

        private void OnContextClick(object sender, EventArgs e)
        {
            _navigator.PerformContextAction();
        }

        private void OnCloseClick(object sender, EventArgs e)
        {
            _navigator.PerformCloseAction();
        }
        #endregion
    }
}
