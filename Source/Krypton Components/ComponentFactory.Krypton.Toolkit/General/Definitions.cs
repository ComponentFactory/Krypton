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
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	#region IContentValues
	/// <summary>
	/// Exposes access to content values.
	/// </summary>
	public interface IContentValues
	{
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
		Image GetImage(PaletteState state);

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        Color GetImageTransparentColor(PaletteState state);

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
		string GetShortText();

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
		string GetLongText();
	}
	#endregion

    #region IButtonSpecValues
	/// <summary>
	/// Exposes access to button specification values.
	/// </summary>
    public interface IButtonSpecValues
    {
        /// <summary>
        /// Occurs when a button spec property has changed.
        /// </summary>
        event PropertyChangedEventHandler ButtonSpecPropertyChanged;

        /// <summary>
        /// Gets the button image.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <param name="state">State for which an image is needed.</param>
        /// <returns>Button image.</returns>
        Image GetImage(IPalette palette, PaletteState state);

        /// <summary>
        /// Gets the button image transparent color.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Color value.</returns>
        Color GetImageTransparentColor(IPalette palette);

        /// <summary>
        /// Gets the button short text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Short text string.</returns>
        string GetShortText(IPalette palette);

        /// <summary>
        /// Gets the button long text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Long text string.</returns>
        string GetLongText(IPalette palette);

        /// <summary>
        /// Gets the button tooltip title text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Tooltip title string.</returns>
        string GetToolTipTitle(IPalette palette);

        /// <summary>
        /// Gets and image color to remap to container foreground.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Color value.</returns>
        Color GetColorMap(IPalette palette);

        /// <summary>
        /// Gets the button visibility.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibility value.</returns>
        bool GetVisible(IPalette palette);

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled value.</returns>
        ButtonEnabled GetEnabled(IPalette palette);

        /// <summary>
        /// Sets the current view associated with the button spec.
        /// </summary>
        /// <param name="view">View element reference.</param>
        void SetView(ViewBase view);

        /// <summary>
        /// Get the current view associated with the button spec.
        /// </summary>
        /// <returns>View element reference.</returns>
        ViewBase GetView();

        /// <summary>
        /// Gets a value indicating if the associated view is enabled.
        /// </summary>
        /// <returns>True if enabled; otherwise false.</returns>
        bool GetViewEnabled();

        /// <summary>
        /// Gets the button edge alignment.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button edge value.</returns>
        RelativeEdgeAlign GetEdge(IPalette palette);

        /// <summary>
        /// Gets the button style.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button style value.</returns>
        ButtonStyle GetStyle(IPalette palette);

        /// <summary>
        /// Gets the button location value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button location.</returns>
        HeaderLocation GetLocation(IPalette palette);

        /// <summary>
        /// Gets the button orienation.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Orientation value.</returns>
        ButtonOrientation GetOrientation(IPalette palette);
    }
    #endregion

    #region IContextMenuProvider
    /// <summary>
    /// Interface exposed by a context menu provider.
    /// </summary>
    public interface IContextMenuProvider
    {
        /// <summary>
        /// Raises the Dispose event.
        /// </summary>
        event EventHandler Dispose;

        /// <summary>
        /// Raises the Closing event.
        /// </summary>
        event CancelEventHandler Closing;

        /// <summary>
        /// Raises the Close event.
        /// </summary>
        event EventHandler<CloseReasonEventArgs> Close;

        /// <summary>
        /// Fires the Closing event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        void OnDispose(EventArgs e);

        /// <summary>
        /// Fires the Closing event.
        /// </summary>
        /// <param name="cea">A CancelEventArgs containing the event data.</param>
        void OnClosing(CancelEventArgs cea);

        /// <summary>
        /// Fires the Close event.
        /// </summary>
        /// <param name="e">An CloseReasonMenuArgs containing the event data.</param>
        void OnClose(CloseReasonEventArgs e);

        /// <summary>
        /// Does this provider have a parent provider.
        /// </summary>
        bool HasParentProvider { get; }

        /// <summary>
        /// Is the entire context menu enabled.
        /// </summary>
        bool ProviderEnabled { get; }

        /// <summary>
        /// Is context menu capable of being closed.
        /// </summary>
        bool ProviderCanCloseMenu { get; }

        /// <summary>
        /// Should the sub menu be shown at fixed screen location for this menu item.
        /// </summary>
        /// <param name="menuItem">Menu item that needs to show sub menu.</param>
        /// <returns>True if the sub menu should be a fixed size.</returns>
        bool ProviderShowSubMenuFixed(KryptonContextMenuItem menuItem);

        /// <summary>
        /// Should the sub menu be shown at fixed screen location for this menu item.
        /// </summary>
        /// <param name="menuItem">Menu item that needs to show sub menu.</param>
        /// <returns>Screen rectangle to use as display rectangle.</returns>
        Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem);

        /// <summary>
        /// Sets the reason for the context menu being closed.
        /// </summary>
        Nullable<ToolStripDropDownCloseReason> ProviderCloseReason { get; set; }

        /// <summary>
        /// Gets and sets the horizontal setting used to position the menu.
        /// </summary>
        KryptonContextMenuPositionH ProviderShowHorz { get; set; }

        /// <summary>
        /// Gets and sets the vertical setting used to position the menu.
        /// </summary>
        KryptonContextMenuPositionV ProviderShowVert { get; set; }

        /// <summary>
        /// Gets access to the layout for context menu columns.
        /// </summary>
        ViewLayoutStack ProviderViewColumns { get; }

        /// <summary>
        /// Gets access to the context menu specific view manager.
        /// </summary>
        ViewContextMenuManager ProviderViewManager { get; }

        /// <summary>
        /// Gets access to the context menu common state.
        /// </summary>
        PaletteContextMenuRedirect ProviderStateCommon { get; }

        /// <summary>
        /// Gets access to the context menu disabled state.
        /// </summary>
        PaletteContextMenuItemState ProviderStateDisabled { get; }

        /// <summary>
        /// Gets access to the context menu normal state.
        /// </summary>
        PaletteContextMenuItemState ProviderStateNormal { get; }

        /// <summary>
        /// Gets access to the context menu highlight state.
        /// </summary>
        PaletteContextMenuItemStateHighlight ProviderStateHighlight { get; }

        /// <summary>
        /// Gets access to the context menu checked state.
        /// </summary>
        PaletteContextMenuItemStateChecked ProviderStateChecked { get; }

        /// <summary>
        /// Gets access to the context menu images.
        /// </summary>
        PaletteRedirectContextMenu ProviderImages { get; }

        /// <summary>
        /// Gets access to the custom palette.
        /// </summary>
        IPalette ProviderPalette { get; }

        /// <summary>
        /// Gets access to the palette mode.
        /// </summary>
        PaletteMode ProviderPaletteMode { get; }

        /// <summary>
        /// Gets access to the context menu redirector.
        /// </summary>
        PaletteRedirect ProviderRedirector { get; }

        /// <summary>
        /// Gets a delegate used to indicate a repaint is required.
        /// </summary>
        NeedPaintHandler ProviderNeedPaintDelegate { get; }
    }
    #endregion

    #region IContextMenuItemColumn
    /// <summary>
    /// Interface used to control width of a context menu item column.
    /// </summary>
    public interface IContextMenuItemColumn
    {
        /// <summary>
        /// Gets the index of the column within the menu item.
        /// </summary>
        int ColumnIndex { get; }

        /// <summary>
        /// Gets the last calculated preferred size value.
        /// </summary>
        Size LastPreferredSize { get; }

        /// <summary>
        /// Sets the preferred width value to use until further notice.
        /// </summary>
        int OverridePreferredWidth { set; }
    }
    #endregion

    #region IContextMenuTarget
    /// <summary>
    /// Interface used to control width of a context menu item column.
    /// </summary>
    public interface IContextMenuTarget
    {
        /// <summary>
        /// Returns if the item shows a sub menu when selected.
        /// </summary>
        bool HasSubMenu { get; }

        /// <summary>
        /// This target should display as the active target.
        /// </summary>
        void ShowTarget();

        /// <summary>
        /// This target should clear any active display.
        /// </summary>
        void ClearTarget();

        /// <summary>
        /// This target should show any appropriate sub menu.
        /// </summary>
        void ShowSubMenu();

        /// <summary>
        /// This target should remove any showing sub menu.
        /// </summary>
        void ClearSubMenu();

        /// <summary>
        /// Determine if the keys value matches the mnemonic setting for this target.
        /// </summary>
        /// <param name="charCode">Key code to test against.</param>
        /// <returns>True if a match is found; otherwise false.</returns>
        bool MatchMnemonic(char charCode);

        /// <summary>
        /// Activate the item because of a mnemonic key press.
        /// </summary>
        void MnemonicActivate();
        
        /// <summary>
        /// Gets the view element that should be used when this target is active.
        /// </summary>
        /// <returns>View element to become active.</returns>
        ViewBase GetActiveView();

        /// <summary>
        /// Get the client rectangle for the display of this target.
        /// </summary>
        Rectangle ClientRectangle { get; }

        /// <summary>
        /// Should a mouse down at the provided point cause the currently stacked context menu to become current.
        /// </summary>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        bool DoesStackedClientMouseDownBecomeCurrent(Point pt);
    }
    #endregion

    #region IContainedInputControl
    /// <summary>
    /// Interface allowing access to the contained input control.
    /// </summary>
    public interface IContainedInputControl
    {
        /// <summary>
        /// Gets access to the contained input control.
        /// </summary>
        Control ContainedControl { get; }
    }
    #endregion

    #region IKryptonCommand
    /// <summary>
    /// Interface exposes access to a command definition.
    /// </summary>
    public interface IKryptonCommand
    {
        /// <summary>
        /// Occurs when the command needs executing.
        /// </summary>
        event EventHandler Execute;

        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets and sets the enabled state of the command.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets and sets the checked state of the command.
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Gets and sets the check state of the command.
        /// </summary>
        CheckState CheckState { get; set; }

        /// <summary>
        /// Gets and sets the command text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets and sets the command extra text.
        /// </summary>
        string ExtraText { get; set; }

        /// <summary>
        /// Gets and sets the command text line 1 for use in KryptonRibbon.
        /// </summary>
        string TextLine1 { get; set; }

        /// <summary>
        /// Gets and sets the command text line 2 for use in KryptonRibbon.
        /// </summary>
        string TextLine2 { get; set; }

        /// <summary>
        /// Gets and sets the command small image.
        /// </summary>
        Image ImageSmall { get; set; }

        /// <summary>
        /// Gets and sets the command large image.
        /// </summary>
        Image ImageLarge { get; set; }

        /// <summary>
        /// Gets and sets the command image transparent color.
        /// </summary>
        Color ImageTransparentColor { get; set; }

        /// <summary>
        /// Generates a Execute event for a command.
        /// </summary>
        void PerformExecute();
    }
    #endregion

    #region IKryptonMonthCalendar
    /// <summary>
    /// Provides month calendar information.
    /// </summary>
    public interface IKryptonMonthCalendar
    {
        /// <summary>
        /// Gets access to the owning control
        /// </summary>
        Control CalendarControl { get; }

        /// <summary>
        /// Gets if the control is in design mode.
        /// </summary>
        bool InDesignMode { get; }

        /// <summary>
        /// Get the renderer.
        /// </summary>
        /// <returns>Render instance.</returns>
        IRenderer GetRenderer();

        /// <summary>
        /// Gets a delegate for creating tool strip renderers.
        /// </summary>
        GetToolStripRenderer GetToolStripDelegate { get; }

        /// <summary>
        /// Gets the number of columns and rows of months displayed.
        /// </summary>
        Size CalendarDimensions { get; }

        /// <summary>
        /// First day of the week.
        /// </summary>
        Day FirstDayOfWeek { get; }

        /// <summary>
        /// First date allowed to be drawn/selected.
        /// </summary>
        DateTime MinDate { get; }

        /// <summary>
        /// Last date allowed to be drawn/selected.
        /// </summary>
        DateTime MaxDate { get; }

        /// <summary>
        /// Today's date.
        /// </summary>
        DateTime TodayDate { get; }

        /// <summary>
        /// Today's date format.
        /// </summary>
        string TodayFormat { get; }

        /// <summary>
        /// Gets the focus day.
        /// </summary>
        DateTime? FocusDay { get; set; }

        /// <summary>
        /// Number of days allowed to be selected at a time.
        /// </summary>
        int MaxSelectionCount { get; }

        /// <summary>
        /// Gets the number of months to move for next/prev buttons.
        /// </summary>
        int ScrollChange { get; }
        
        /// <summary>
        /// Start of selected range.
        /// </summary>
        DateTime SelectionStart { get; }

        /// <summary>
        /// End of selected range.
        /// </summary>
        DateTime SelectionEnd { get; }

        /// <summary>
        /// Update usage of bolded overrides.
        /// </summary>
        /// <param name="bolded">Should show bolded.</param>
        void SetBoldedOverride(bool bolded);

        /// <summary>
        /// Update usage of today overrides.
        /// </summary>
        /// <param name="today">New today state.</param>
        void SetTodayOverride(bool today);

        /// <summary>
        /// Update usage of focus overrides.
        /// </summary>
        /// <param name="focus">Should show focus.</param>
        void SetFocusOverride(bool focus);

        /// <summary>
        /// Set the selection range.
        /// </summary>
        /// <param name="start">New starting date.</param>
        /// <param name="end">New ending date.</param>
        void SetSelectionRange(DateTime start, DateTime end);

        /// <summary>
        /// Dates to be bolded.
        /// </summary>
        DateTimeList BoldedDatesList { get; }

        /// <summary>
        /// Monthly days to be bolded.
        /// </summary>
        int MonthlyBoldedDatesMask { get; }

        /// <summary>
        /// Array of annual days per month to be bolded.
        /// </summary>
        int[] AnnuallyBoldedDatesMask { get; }

        /// <summary>
        /// Gets access to the month calendar common appearance entries.
        /// </summary>
        PaletteMonthCalendarRedirect StateCommon { get; }

        /// <summary>
        /// Gets access to the month calendar normal appearance entries.
        /// </summary>
        PaletteMonthCalendarDoubleState StateNormal { get; }

        /// <summary>
        /// Gets access to the month calendar disabled appearance entries.
        /// </summary>
        PaletteMonthCalendarDoubleState StateDisabled { get; }

        /// <summary>
        /// Gets access to the month calendar tracking appearance entries.
        /// </summary>
        PaletteMonthCalendarState StateTracking { get; }

        /// <summary>
        /// Gets access to the month calendar pressed appearance entries.
        /// </summary>
        PaletteMonthCalendarState StatePressed { get; }

        /// <summary>
        /// Gets access to the month calendar checked normal appearance entries.
        /// </summary>
        PaletteMonthCalendarState StateCheckedNormal { get; }

        /// <summary>
        /// Gets access to the month calendar checked tracking appearance entries.
        /// </summary>
        PaletteMonthCalendarState StateCheckedTracking { get; }

        /// <summary>
        /// Gets access to the month calendar checked pressed appearance entries.
        /// </summary>
        PaletteMonthCalendarState StateCheckedPressed { get; }

        /// <summary>
        /// Gets access to the override for disabled day.
        /// </summary>
        PaletteTripleOverride OverrideDisabled { get; }

        /// <summary>
        /// Gets access to the override for disabled day.
        /// </summary>
        PaletteTripleOverride OverrideNormal { get; }

        /// <summary>
        /// Gets access to the override for tracking day.
        /// </summary>
        PaletteTripleOverride OverrideTracking { get; }

        /// <summary>
        /// Gets access to the override for pressed day.
        /// </summary>
        PaletteTripleOverride OverridePressed { get; }

        /// <summary>
        /// Gets access to the override for checked normal day.
        /// </summary>
        PaletteTripleOverride OverrideCheckedNormal { get; }
        
        /// <summary>
        /// Gets access to the override for checked tracking day.
        /// </summary>
        PaletteTripleOverride OverrideCheckedTracking { get; }

        /// <summary>
        /// Gets access to the override for checked pressed day.
        /// </summary>
        PaletteTripleOverride OverrideCheckedPressed { get; }
    }
    #endregion

    #region IKryptonDebug
    /// <summary>
	/// Exposes access to the debugging helpers for krypton controls.
	/// </summary>
    public interface IKryptonDebug
    {
        /// <summary>
        /// Reset the internal counters.
        /// </summary>
        void KryptonResetCounters();

        /// <summary>
        /// Gets the number of layout cycles performed since last reset.
        /// </summary>
        int KryptonLayoutCounter { get; }

        /// <summary>
        /// Gets the number of paint cycles performed since last reset.
        /// </summary>
        int KryptonPaintCounter { get; }
    }
    #endregion

    #region IKryptonDesignerSelect
    /// <summary>
	/// Exposes design time selection of parent control.
	/// </summary>
    public interface IKryptonDesignerSelect
    {
        /// <summary>
        /// Should painting be performed for the selection glyph.
        /// </summary>
        bool CanPaint { get; }

        /// <summary>
        /// Request the parent control be selected.
        /// </summary>
        void SelectParentControl();
    }    
    #endregion

    #region IKryptonComposition
    /// <summary>
    /// Exposes interface for visual form to cooperate with a view for composition.
    /// </summary>
    public interface IKryptonComposition
    {
        /// <summary>
        /// Gets the pixel height of the composition extension into the client area.
        /// </summary>
        int CompHeight { get; }

        /// <summary>
        /// Should painting be performed for the selection glyph.
        /// </summary>
        bool CompVisible { get; set; }

        /// <summary>
        /// Gets and sets the form that owns the composition.
        /// </summary>
        VisualForm CompOwnerForm { get; set; }

        /// <summary>
        /// Request a repaint and optional layout.
        /// </summary>
        /// <param name="needLayout">Is a layout required.</param>
        void CompNeedPaint(bool needLayout);

        /// <summary>
        /// Gets the handle of the composition element control.
        /// </summary>
        IntPtr CompHandle { get; }
    }
    #endregion

    #region IKryptonDesignObject
    /// <summary>
    /// Exposes interface for visual form to cooperate with a view for composition.
    /// </summary>
    public interface IKryptonDesignObject
    {
        /// <summary>
        /// Gets and sets if the object is enabled.
        /// </summary>
        bool DesignEnabled { get; set; }

        /// <summary>
        /// Gets and sets if the object is visible.
        /// </summary>
        bool DesignVisible { get; set; }
    }
    #endregion

    #region Enum VisualOrientation
    /// <summary>
	/// Specifies the orientation of a visual element.
	/// </summary>
	public enum VisualOrientation
	{
		/// <summary>
		/// Specifies the element is orientated in a vertical top down manner.
		/// </summary>
		Top,

		/// <summary>
		/// Specifies the element is orientated in a vertical bottom upwards manner.
		/// </summary>
		Bottom,

		/// <summary>
		/// Specifies the element is orientated in a horizontal left to right manner.
		/// </summary>
		Left,

		/// <summary>
		/// Specifies the element is orientated in a horizontal right to left manner.
		/// </summary>
		Right
	}
	#endregion

    #region Enum TabBorderStyle
    /// <summary>
    /// Specifies the style of tab border to draw.
    /// </summary>
    [TypeConverter(typeof(TabBorderStyleConverter))]
    public enum TabBorderStyle
    {
        /// <summary>
        /// Specifies square tabs of equal size with small spacing gaps.
        /// </summary>
        SquareEqualSmall,

        /// <summary>
        /// Specifies square tabs of equal size with medium spacing gaps.
        /// </summary>
        SquareEqualMedium,

        /// <summary>
        /// Specifies square tabs of equal size with large spacing gaps.
        /// </summary>
        SquareEqualLarge,

        /// <summary>
        /// Specifies square tabs with larger selected entry with small spacing gaps.
        /// </summary>
        SquareOutsizeSmall,

        /// <summary>
        /// Specifies square tabs with larger selected entry with medium spacing gaps.
        /// </summary>
        SquareOutsizeMedium,

        /// <summary>
        /// Specifies square tabs with larger selected entry with large spacing gaps.
        /// </summary>
        SquareOutsizeLarge,

        /// <summary>
        /// Specifies rounded tabs of equal size with small spacing gaps.
        /// </summary>
        RoundedEqualSmall,

        /// <summary>
        /// Specifies rounded tabs of equal size with medium spacing gaps.
        /// </summary>
        RoundedEqualMedium,

        /// <summary>
        /// Specifies rounded tabs of equal size with large spacing gaps.
        /// </summary>
        RoundedEqualLarge,

        /// <summary>
        /// Specifies rounded tabs with larger selected entry with small spacing gaps.
        /// </summary>
        RoundedOutsizeSmall,

        /// <summary>
        /// Specifies rounded tabs with larger selected entry with medium spacing gaps.
        /// </summary>
        RoundedOutsizeMedium,

        /// <summary>
        /// Specifies rounded tabs with larger selected entry with large spacing gaps.
        /// </summary>
        RoundedOutsizeLarge,

        /// <summary>
        /// Specifies near slanted tabs of equal size.
        /// </summary>
        SlantEqualNear,

        /// <summary>
        /// Specifies far slanted tabs of equal size.
        /// </summary>
        SlantEqualFar,

        /// <summary>
        /// Specifies double slanted tabs of equal size.
        /// </summary>
        SlantEqualBoth,

        /// <summary>
        /// Specifies near slanted tabs with larger selected entry.
        /// </summary>
        SlantOutsizeNear,

        /// <summary>
        /// Specifies far slanted tabs with larger selected entry.
        /// </summary>
        SlantOutsizeFar,

        /// <summary>
        /// Specifies double slanted tabs with larger selected entry.
        /// </summary>
        SlantOutsizeBoth,

        /// <summary>
        /// Specifies the OneNote application style tab appearance.
        /// </summary>
        OneNote,

        /// <summary>
        /// Specifies smooth tabs of equal size.
        /// </summary>
        SmoothEqual,

        /// <summary>
        /// Specifies smooth tabs with larger selected entry.
        /// </summary>
        SmoothOutsize,

        /// <summary>
        /// Specifies docking tabs of requal size.
        /// </summary>
        DockEqual,

        /// <summary>
        /// Specifies docking tabs with larger selected entry.
        /// </summary>
        DockOutsize,
    }
    #endregion

    #region Enum ButtonEnabled
    /// <summary>
    /// Specifies the enabled state of a button specification.
    /// </summary>
    public enum ButtonEnabled
    {
        /// <summary>
        /// Specifies button should take enabled state from container control state.
        /// </summary>
        Container,

        /// <summary>
        /// Specifies button should be enabled.
        /// </summary>
        True,

        /// <summary>
        /// Specifies button should be disabled.
        /// </summary>
        False,
    }
    #endregion

    #region Enum ButtonOrientation
    /// <summary>
    /// Specifies the orientation of a button specification.
    /// </summary>
    public enum ButtonOrientation
    {
        /// <summary>
        /// Specifies orientation should automatically match the concept of use.
        /// </summary>
        Auto,

        /// <summary>
        /// Specifies the button is orientated in a vertical top down manner.
        /// </summary>
        FixedTop,

        /// <summary>
        /// Specifies the button is orientated in a vertical bottom upwards manner.
        /// </summary>
        FixedBottom,

        /// <summary>
        /// Specifies the button is orientated in a horizontal left to right manner.
        /// </summary>
        FixedLeft,

        /// <summary>
        /// Specifies the button is orientated in a horizontal right to left manner.
        /// </summary>
        FixedRight
    }
    #endregion

    #region Enum ButtonCheckState
    /// <summary>
    /// Specifies the checked state of a button.
    /// </summary>
    public enum ButtonCheckState
    {
        /// <summary>
        /// Specifies the button is not a checked button.
        /// </summary>
        NotCheckButton,

        /// <summary>
        /// Specifies the check button is currently checked.
        /// </summary>
        Checked,

        /// <summary>
        /// Specifies the check button is not currently checked.
        /// </summary>
        Unchecked
    }
    #endregion

    #region Enum RelativeEdgeAlign
    /// <summary>
    /// Specifies a relative edge alignment position.
    /// </summary>
    public enum RelativeEdgeAlign
    {
        /// <summary>
        /// Specifies a relative alignment of near.
        /// </summary>
        Near,

        /// <summary>
        /// Specifies a relative alignment of far.
        /// </summary>
        Far
    }
    #endregion

    #region Enum RelativePositionAlign
    /// <summary>
    /// Specifies a relative alignment position.
    /// </summary>
    public enum RelativePositionAlign
    {
        /// <summary>
        /// Specifies a relative alignment of near.
        /// </summary>
        Near,

        /// <summary>
        /// Specifies a relative alignment of center.
        /// </summary>
        Center,

        /// <summary>
        /// Specifies a relative alignment of far.
        /// </summary>
        Far
    }
    #endregion

    #region Enum LabelStyle
    /// <summary>
    /// Specifies the label style.
    /// </summary>
    [TypeConverter(typeof(LabelStyleConverter))]
    public enum LabelStyle
    {
        /// <summary>
        /// Specifies a normal label for use on a control style background.
        /// </summary>
        NormalControl,

        /// <summary>
        /// Specifies a bold label for use on a control style background.
        /// </summary>
        BoldControl,

        /// <summary>
        /// Specifies an italic label for use on a control style background.
        /// </summary>
        ItalicControl,
        
        /// <summary>
        /// Specifies a label appropriate for titles for use on a control style background.
        /// </summary>
        TitleControl,

        /// <summary>
        /// Specifies a normal label for use on a panel style background.
        /// </summary>
        NormalPanel,

        /// <summary>
        /// Specifies a bold label for use on a panel style background.
        /// </summary>
        BoldPanel,

        /// <summary>
        /// Specifies an italic label for use on a panel style background.
        /// </summary>
        ItalicPanel,

        /// <summary>
        /// Specifies a label appropriate for titles for use on a panel style background.
        /// </summary>
        TitlePanel,

        /// <summary>
        /// Specifies a label appropriate for captions for use on a group box style background.
        /// </summary>
        GroupBoxCaption,

        /// <summary>
        /// Specifies a label appropriate for use inside a tooltip.
        /// </summary>
        ToolTip,

        /// <summary>
        /// Specifies a label appropriate for use inside a super tooltip.
        /// </summary>
        SuperTip,

        /// <summary>
        /// Specifies a label appropriate for use inside a key tooltip.
        /// </summary>
        KeyTip,

        /// <summary>
        /// Specifies the first custom label style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies the second custom label style.
        /// </summary>
        Custom2,

        /// <summary>
        /// Specifies the third custom label style.
        /// </summary>
        Custom3    
    }
    #endregion

    #region Enum GridStyle
    /// <summary>
    /// Specifies the grid style.
    /// </summary>
    [TypeConverter(typeof(GridStyleConverter))]
    public enum GridStyle
    {
        /// <summary>
        /// Specifies a list grid style.
        /// </summary>
        List,

        /// <summary>
        /// Specifies a worksheet grid style.
        /// </summary>
        Sheet,

        /// <summary>
        /// Specifies the first custom grid style.
        /// </summary>
        Custom1,
    }
    #endregion

    #region Enum DataGridViewStyle
    /// <summary>
    /// Specifies the data grid view style.
    /// </summary>
    [TypeConverter(typeof(DataGridViewStyleConverter))]
    public enum DataGridViewStyle
    {
        /// <summary>
        /// Specifies a list grid style.
        /// </summary>
        List,

        /// <summary>
        /// Specifies a worksheet grid style.
        /// </summary>
        Sheet,

        /// <summary>
        /// Specifies the first custom grid style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies a mixed set of styles.
        /// </summary>
        Mixed,
    }
    #endregion

    #region Enum HeaderStyle
    /// <summary>
    /// Specifies the header style.
    /// </summary>
    [TypeConverter(typeof(HeaderStyleConverter))]
    public enum HeaderStyle
    {
        /// <summary>
        /// Specifies a primary header.
        /// </summary>
        Primary,

        /// <summary>
        /// Specifies a secondary header.
        /// </summary>
        Secondary,

        /// <summary>
        /// Specifies an inactive docking header.
        /// </summary>
        DockInactive,

        /// <summary>
        /// Specifies an active docking header.
        /// </summary>
        DockActive,

        /// <summary>
        /// Specifies a form header.
        /// </summary>
        Form,

        /// <summary>
        /// Specifies a calendar header.
        /// </summary>
        Calendar,

        /// <summary>
        /// Specifies the first custom header style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies the second custom header style.
        /// </summary>
        Custom2,
    }
    #endregion

    #region Enum ButtonStyle
    /// <summary>
    /// Specifies the button style.
    /// </summary>
    [TypeConverter(typeof(ButtonStyleConverter))]
    public enum ButtonStyle
    {
        /// <summary>
        /// Specifies a standalone button style.
        /// </summary>
        Standalone,

        /// <summary>
        /// Specifies an alternate standalone button style.
        /// </summary>
        Alternate,

        /// <summary>
        /// Specifies a low profile button style.
        /// </summary>
        LowProfile,

        /// <summary>
        /// Specifies a button spec usage style.
        /// </summary>
        ButtonSpec,

        /// <summary>
        /// Specifies a button style appropriate for bread crumbs.
        /// </summary>
        BreadCrumb,

        /// <summary>
        /// Specifies a button style appropriate for calendar day.
        /// </summary>
        CalendarDay,

        /// <summary>
        /// Specifies a ribbon cluster button usage style.
        /// </summary>
        Cluster,

        /// <summary>
        /// Specifies a ribbon gallery button usage style.
        /// </summary>
        Gallery,

        /// <summary>
        /// Specifies a navigator stack usage style.
        /// </summary>
        NavigatorStack,

        /// <summary>
        /// Specifies a navigator overflow usage style.
        /// </summary>
        NavigatorOverflow,

        /// <summary>
        /// Specifies a navigator mini usage style.
        /// </summary>
        NavigatorMini,

        /// <summary>
        /// Specifies an input control usage style.
        /// </summary>
        InputControl,

        /// <summary>
        /// Specifies a list item usage style.
        /// </summary>
        ListItem,

        /// <summary>
        /// Specifies a form level style.
        /// </summary>
        Form,

        /// <summary>
        /// Specifies a form close button.
        /// </summary>
        FormClose,

        /// <summary>
        /// Specifies a command button.
        /// </summary>
        Command,

        /// <summary>
        /// Specifies the first custom button style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies the second custom button style.
        /// </summary>
        Custom2,

        /// <summary>
        /// Specifies the third custom button style.
        /// </summary>
        Custom3
    }
    #endregion

    #region Enum InputControlStyle
    /// <summary>
    /// Specifies the input control style.
    /// </summary>
    [TypeConverter(typeof(InputControlStyleConverter))]
    public enum InputControlStyle
    {
        /// <summary>
        /// Specifies a standalone input button style.
        /// </summary>
        Standalone,

        /// <summary>
        /// Specifies a ribbon input button style.
        /// </summary>
        Ribbon,

        /// <summary>
        /// Specifies a custom input button style.
        /// </summary>
        Custom1
    }
    #endregion

    #region Enum SeparatorStyle
    /// <summary>
    /// Specifies the separator style.
    /// </summary>
    [TypeConverter(typeof(SeparatorStyleConverter))]
    public enum SeparatorStyle
    {
        /// <summary>
        /// Specifies a low profile separator.
        /// </summary>
        LowProfile,

        /// <summary>
        /// Specifies a high profile separator.
        /// </summary>
        HighProfile,

        /// <summary>
        /// Specifies a high profile for internal separator.
        /// </summary>
        HighInternalProfile,

        /// <summary>
        /// Specifies a custom separator.
        /// </summary>
        Custom1
    }
    #endregion

    #region Enum TabStyle
    /// <summary>
    /// Specifies the tab style.
    /// </summary>
    [TypeConverter(typeof(TabStyleConverter))]
    public enum TabStyle
    {
        /// <summary>
        /// Specifies the high profile tab style.
        /// </summary>
        HighProfile,

        /// <summary>
        /// Specifies the standard profile style.
        /// </summary>
        StandardProfile,

        /// <summary>
        /// Specifies the low profile tab style.
        /// </summary>
        LowProfile,

        /// <summary>
        /// Specifies the Microsoft OneNote tab style.
        /// </summary>
        OneNote,

        /// <summary>
        /// Specifies the docking tab style.
        /// </summary>
        Dock,

        /// <summary>
        /// Specifies the auto hidden docking tab style.
        /// </summary>
        DockAutoHidden,

        /// <summary>
        /// Specifies the first custom tab style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies the second custom tab style.
        /// </summary>
        Custom2,

        /// <summary>
        /// Specifies the third custom tab style.
        /// </summary>
        Custom3
    }
    #endregion

    #region Enum HeaderLocation
    /// <summary>
    /// Specifies a target header.
    /// </summary>
    public enum HeaderLocation
    {
        /// <summary>
        /// Specifies the primary header.
        /// </summary>
        PrimaryHeader,

        /// <summary>
        /// Specifies the secondary header.
        /// </summary>
        SecondaryHeader
    }
    #endregion

    #region Enum HeaderGroupCollapsedTarget
    /// <summary>
    /// Specifies the target collapsed state of a header group when in the collapsed mode.
    /// </summary>
    [TypeConverter(typeof(HeaderGroupCollapsedTargetConverter))]
    public enum HeaderGroupCollapsedTarget
    {
        /// <summary>
        /// Specifies the appearance is collapsed to just the primary header.
        /// </summary>
        CollapsedToPrimary,

        /// <summary>
        /// Specifies the appearance is collapsed to just the secondary header.
        /// </summary>
        CollapsedToSecondary,

        /// <summary>
        /// Specifies the appearance is collapsed to just the primary and secondary headers.
        /// </summary>
        CollapsedToBoth
    }
    #endregion

    #region Enum KryptonLinkBehavior
    /// <summary>
    /// Specifies the logic for underlining the link label short text.
    /// </summary>
    [TypeConverter(typeof(KryptonLinkBehaviorConverter))]
    public enum KryptonLinkBehavior
    {
        /// <summary>
        /// Specifies the short text is always underlined.
        /// </summary>
        AlwaysUnderline,

        /// <summary>
        /// Specifies the short text is underlined only when mouse hovers over text.
        /// </summary>
        HoverUnderline,

        /// <summary>
        /// Specifies the short text is never underlined.
        /// </summary>
        NeverUnderline
    }
    #endregion

    #region Enum ViewDockStyle
    /// <summary>
    /// Specifies the docking styles for the docking view elements.
    /// </summary>
    public enum ViewDockStyle
    {
        /// <summary>
        /// Specifies the child element should fill the remaining space.
        /// </summary>
        Fill,

        /// <summary>
        /// Specifies the child element should dock against the top edge.
        /// </summary>
        Top,

        /// <summary>
        /// Specifies the child element should dock against the bottom edge.
        /// </summary>
        Bottom,

        /// <summary>
        /// Specifies the child element should dock against the left edge.
        /// </summary>
        Left,

        /// <summary>
        /// Specifies the child element should dock against the right edge.
        /// </summary>
        Right,
    }
    #endregion

    #region Enum GridRowGlyph
    /// <summary>
    /// Specifies the grid row glyph.
    /// </summary>
    public enum GridRowGlyph
    {
        /// <summary>
        /// Specifies no glyph for the row.
        /// </summary>
        None,

        /// <summary>
        /// Specifies a star for showing a dirty row.
        /// </summary>
        Star,

        /// <summary>
        /// Specifies an arrow for the current row.
        /// </summary>
        Arrow,

        /// <summary>
        /// Specifies a star and arrow for a dirty current row.
        /// </summary>
        ArrowStar,

        /// <summary>
        /// Specifies a pencil for the line being edited.
        /// </summary>
        Pencil,
    }
    #endregion

    #region Enum KryptonContextMenuPositionV
    /// <summary>
    /// Specifies the relative vertical position for showing a KryptonContextMenu.
    /// </summary>
    public enum KryptonContextMenuPositionV
    {
        /// <summary>
        /// Specifies bottom of context menu is adjacent to top of rectangle.
        /// </summary>
        Above,

        /// <summary>
        /// Specifies top of context menu is adjacent to bottom of rectangle.
        /// </summary>
        Below,

        /// <summary>
        /// Specifies top of context menu is adjacent to top of rectangle.
        /// </summary>
        Top,

        /// <summary>
        /// Specifies bottom of context menu is adjacent to bottom of rectangle.
        /// </summary>
        Bottom,
    }
    #endregion

    #region Enum KryptonContextMenuPositionH
    /// <summary>
    /// Specifies the relative horizontal position for showing a KryptonContextMenu.
    /// </summary>
    public enum KryptonContextMenuPositionH
    {
        /// <summary>
        /// Specifies right of context menu is adjacent to left of rectangle.
        /// </summary>
        Before,

        /// <summary>
        /// Specifies left of context menu is adjacent to right of rectangle.
        /// </summary>
        After,

        /// <summary>
        /// Specifies left of context menu is adjacent to left of rectangle.
        /// </summary>
        Left,

            /// <summary>
        /// Specifies right of context menu is adjacent to right of rectangle.
        /// </summary>
        Right,
}
    #endregion

    #region Enum ColorScheme
    /// <summary>
    /// Specifies a color scheme.
    /// </summary>
    public enum ColorScheme
    {
        /// <summary>
        /// Specifies no predefined colors.
        /// </summary>
        None,

        /// <summary>
        /// Specifies just white and black.
        /// </summary>
        Mono2,

        /// <summary>
        /// Specifies 8 colors ranging from white to black.
        /// </summary>
        Mono8,

        /// <summary>
        /// Specifies the basic set of 16 colors.
        /// </summary>
        Basic16,

        /// <summary>
        /// Specifies the Office set of standard 10 colors.
        /// </summary>
        OfficeStandard,

        /// <summary>
        /// Specifies the Office set of 10 color themes.
        /// </summary>
        OfficeThemes
    }
    #endregion

    #region Enum TaskDialogButtons
    /// <summary>
    /// Specifies task dialog buttons.
    /// </summary>
    [Flags]
    public enum TaskDialogButtons
    {
        /// <summary>
        /// Specifies no buttons be shown.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Specifies the OK button.
        /// </summary>
        OK = 0x01,

        /// <summary>
        /// Specifies the Cancel button.
        /// </summary>
        Cancel = 0x02,

        /// <summary>
        /// Specifies the Yes button.
        /// </summary>
        Yes = 0x04,

        /// <summary>
        /// Specifies the No button.
        /// </summary>
        No = 0x08,

        /// <summary>
        /// Specifies the Retry button.
        /// </summary>
        Retry = 0x10,

        /// <summary>
        /// Specifies the Close button.
        /// </summary>
        Close = 0x20,
    }
    #endregion

    #region CheckedSelectionMode
    /// <summary>
    /// Specifies selection mode of the KryptonCheckedListBox.
    /// </summary>
    public enum CheckedSelectionMode
    {
        /// <summary>
        /// No items can be selected.
        /// </summary>
        None = 0,

        /// <summary>
        /// Only one item can be selected.
        /// </summary>
        One = 1
    }
    #endregion

    #region Type ViewDockStyleLookup
    internal class ViewDockStyleLookup : Dictionary<ViewBase, ViewDockStyle> { }
    #endregion

    #region Type DateTimeList
    /// <summary>
    /// Manage a list of DateTime instances.
    /// </summary>
    public class DateTimeList : List<DateTime> { };
    #endregion

    #region Type MonthCalendarButtonSpecCollection
    /// <summary>
    /// Collection for managing ButtonSpecAny instances.
    /// </summary>
    public class MonthCalendarButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the MonthCalendarButtonSpecCollection class.
        /// </summary>
        /// <param name="owner">Reference to owning object.</param>
        public MonthCalendarButtonSpecCollection(ViewLayoutMonths owner)
            : base(owner)
        {
        }
        #endregion
    }
    #endregion

    #region Delegates
    /// <summary>
    /// Signature of method that is called when painting needs to occur.
    /// </summary>
    /// <param name="sender">Source of the call.</param>
    /// <param name="e">A NeedLayoutEventArgs containing event information.</param>
    public delegate void NeedPaintHandler(object sender, NeedLayoutEventArgs e);

    /// <summary>
    /// Signature of method that provides a point as the data.
    /// </summary>
    /// <param name="sender">Source of the call.</param>
    /// <param name="pt">A Point related to the event.</param>
    public delegate void PointHandler(object sender, Point pt);
    #endregion
}
