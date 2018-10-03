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

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Storage for workspace context menu for pages.
	/// </summary>
    public class WorkspaceMenus : Storage
    {
        #region Static Fields
        private static readonly string _defaultTextClose = "&Close";
        private static readonly string _defaultTextCloseAllButThis = "Close &All But This";
        private static readonly string _defaultTextMoveNext = "Move &Next";
        private static readonly string _defaultTextMovePrevious = "Move &Previous";
        private static readonly string _defaultTextSplitVertical = "Split &Vertical";
        private static readonly string _defaultTextSplitHorizontal = "Split &Horizontal";
        private static readonly string _defaultTextRebalance = "&Rebalance";
        private static readonly string _defaultTextMaximize = "&Maximize";
        private static readonly string _defaultTextRestore = "Res&tore";
        private static readonly Keys _defaultShortcutClose = Keys.Control | Keys.Shift | Keys.C;
        private static readonly Keys _defaultShortcutCloseAllButThis = Keys.Control | Keys.Shift | Keys.A;
        private static readonly Keys _defaultShortcutMoveNext = Keys.Control | Keys.Shift | Keys.N;
        private static readonly Keys _defaultShortcutMovePrevious = Keys.Control | Keys.Shift | Keys.P;
        private static readonly Keys _defaultShortcutSplitVertical = Keys.Control | Keys.Shift | Keys.V;
        private static readonly Keys _defaultShortcutSplitHorizontal = Keys.Control | Keys.Shift | Keys.H;
        private static readonly Keys _defaultShortcutRebalance = Keys.Control | Keys.Shift | Keys.R;
        private static readonly Keys _defaultShortcutMaximizeRestore = Keys.Control | Keys.Shift | Keys.M;
        #endregion

        #region Instance Fields
        private string _textClose;
        private string _textCloseAllButThis;
        private string _textMoveNext;
        private string _textMovePrevious;
        private string _textSplitVertical;
        private string _textSplitHorizontal;
        private string _textRebalance;
        private string _textMaximize;
        private string _textRestore;
        private Keys _shortcutClose;
        private Keys _shortcutCloseAllButThis;
        private Keys _shortcutMoveNext;
        private Keys _shortcutMovePrevious;
        private Keys _shortcutSplitVertical;
        private Keys _shortcutSplitHorizontal;
        private Keys _shortcutRebalance;
        private Keys _shortcutMaximizeRestore;
        private bool _showContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the WorkspaceMenus class.
		/// </summary>
        public WorkspaceMenus(KryptonWorkspace workspace)
            : base()
		{
            // Default values
            _textClose = _defaultTextClose;
            _textCloseAllButThis = _defaultTextCloseAllButThis;
            _textMoveNext = _defaultTextMoveNext;
            _textMovePrevious = _defaultTextMovePrevious;
            _textSplitVertical = _defaultTextSplitVertical;
            _textSplitHorizontal = _defaultTextSplitHorizontal;
            _textRebalance = _defaultTextRebalance;
            _textMaximize = _defaultTextMaximize;
            _textRestore = _defaultTextRestore;
            _shortcutClose = _defaultShortcutClose;
            _shortcutCloseAllButThis = _defaultShortcutCloseAllButThis;
            _shortcutMoveNext = _defaultShortcutMoveNext;
            _shortcutMovePrevious = _defaultShortcutMovePrevious;
            _shortcutSplitVertical = _defaultShortcutSplitVertical;
            _shortcutSplitHorizontal = _defaultShortcutSplitHorizontal;
            _shortcutRebalance = _defaultShortcutRebalance;
            _shortcutMaximizeRestore = _defaultShortcutMaximizeRestore;
            _showContextMenu = true;
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
                return (TextClose.Equals(_defaultTextClose) &&
                        TextCloseAllButThis.Equals(_defaultTextCloseAllButThis) &&
                        TextMoveNext.Equals(_defaultTextMoveNext) &&
                        TextMovePrevious.Equals(_defaultTextMovePrevious) &&
                        TextSplitVertical.Equals(_defaultTextSplitVertical) &&
                        TextSplitHorizontal.Equals(_defaultTextSplitHorizontal) &&
                        TextRebalance.Equals(_defaultTextRebalance) &&
                        TextMaximize.Equals(_defaultTextMaximize) &&
                        TextRestore.Equals(_defaultTextRestore) &&
                        ShortcutClose.Equals(_defaultShortcutClose) &&
                        ShortcutCloseAllButThis.Equals(_defaultShortcutCloseAllButThis) &&
                        ShortcutMoveNext.Equals(_defaultShortcutMoveNext) &&
                        ShortcutMovePrevious.Equals(_defaultShortcutMovePrevious) &&
                        ShortcutSplitVertical.Equals(_defaultShortcutSplitVertical) &&
                        ShortcutSplitHorizontal.Equals(_defaultShortcutSplitHorizontal) &&
                        ShortcutRebalance.Equals(_defaultShortcutRebalance) &&
                        ShortcutMaximizeRestore.Equals(_defaultShortcutMaximizeRestore) &&
                        (ShowContextMenu == true));
            }
        }
        #endregion

        #region TextClose
        /// <summary>
        /// Gets and sets the text to use for the close context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the close context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("&Close")]
        [Localizable(true)]
        public string TextClose
        {
            get { return _textClose; }
            set { _textClose = value; }
        }

        /// <summary>
        /// Resets the TextClose property to its default value.
        /// </summary>
        public void ResetTextClose()
        {
            TextClose = _defaultTextClose;
        }
        #endregion

        #region TextCloseAllButThis
        /// <summary>
        /// Gets and sets the text to use for the 'close all but this' context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the 'close all but this' context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Close &All But This")]
        [Localizable(true)]
        public string TextCloseAllButThis
        {
            get { return _textCloseAllButThis; }
            set { _textCloseAllButThis = value; }
        }

        /// <summary>
        /// Resets the TextCloseAllButThis property to its default value.
        /// </summary>
        public void ResetTextCloseAllButThis()
        {
            TextCloseAllButThis = _defaultTextCloseAllButThis;
        }
        #endregion

        #region TextMoveNext
        /// <summary>
        /// Gets and sets the text to use for the move next context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the move next context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Move &Next")]
        [Localizable(true)]
        public string TextMoveNext
        {
            get { return _textMoveNext; }
            set { _textMoveNext = value; }
        }

        /// <summary>
        /// Resets the TextMoveNext property to its default value.
        /// </summary>
        public void ResetTextMoveNext()
        {
            TextMoveNext = _defaultTextMoveNext;
        }
        #endregion

        #region TextMovePrevious
        /// <summary>
        /// Gets and sets the text to use for the move previous context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the move previous context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Move &Previous")]
        [Localizable(true)]
        public string TextMovePrevious
        {
            get { return _textMovePrevious; }
            set { _textMovePrevious = value; }
        }

        /// <summary>
        /// Resets the TextMovePrevious property to its default value.
        /// </summary>
        public void ResetTextMovePrevious()
        {
            TextMovePrevious = _defaultTextMovePrevious;
        }
        #endregion

        #region TextSplitVertical
        /// <summary>
        /// Gets and sets the text to use for the split vertical context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the split vertical context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Split &Vertical")]
        [Localizable(true)]
        public string TextSplitVertical
        {
            get { return _textSplitVertical; }
            set { _textSplitVertical = value; }
        }

        /// <summary>
        /// Resets the TextSplitVertical property to its default value.
        /// </summary>
        public void ResetTextSplitVertical()
        {
            TextSplitVertical = _defaultTextSplitVertical;
        }
        #endregion

        #region TextSplitHorizontal
        /// <summary>
        /// Gets and sets the text to use for the split horizontal context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the split horizontal context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Split &Horizontal")]
        [Localizable(true)]
        public string TextSplitHorizontal
        {
            get { return _textSplitHorizontal; }
            set { _textSplitHorizontal = value; }
        }

        /// <summary>
        /// Resets the TextSplitHorizontal property to its default value.
        /// </summary>
        public void ResetTextSplitHorizontal()
        {
            TextSplitHorizontal = _defaultTextSplitHorizontal;
        }
        #endregion

        #region TextRebalance
        /// <summary>
        /// Gets and sets the text to use for the rebalance context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the rebalance context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("&Rebalance")]
        [Localizable(true)]
        public string TextRebalance
        {
            get { return _textRebalance; }
            set { _textRebalance = value; }
        }

        /// <summary>
        /// Resets the TextRebalance property to its default value.
        /// </summary>
        public void ResetTextRebalance()
        {
            TextRebalance = _defaultTextRebalance;
        }
        #endregion

        #region TextMaximize
        /// <summary>
        /// Gets and sets the text to use for the maximize context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the maximize context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("&Maximize")]
        [Localizable(true)]
        public string TextMaximize
        {
            get { return _textMaximize; }
            set { _textMaximize = value; }
        }

        /// <summary>
        /// Resets the TextMaximize property to its default value.
        /// </summary>
        public void ResetTextMaximize()
        {
            TextMaximize = _defaultTextMaximize;
        }
        #endregion

        #region TextRestore
        /// <summary>
        /// Gets and sets the text to use for the restore context menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the restore context menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Res&tore")]
        [Localizable(true)]
        public string TextRestore
        {
            get { return _textRestore; }
            set { _textRestore = value; }
        }

        /// <summary>
        /// Resets the TextRestore property to its default value.
        /// </summary>
        public void ResetTextRestore()
        {
            TextRestore = _defaultTextRestore;
        }
        #endregion

        #region ShortcutClose
        /// <summary>
        /// Gets and sets the shortcut for closing the current page.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for closing the current page.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutClose
        {
            get { return _shortcutClose; }
            set { _shortcutClose = value; }
        }

        /// <summary>
        /// Decide if the shortcut for closing the current page.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutClose()
        {
            return !_shortcutClose.Equals(_defaultShortcutClose);
        }

        /// <summary>
        /// Resets the ShortcutClose property to its default value.
        /// </summary>
        public void ResetShortcutClose()
        {
            ShortcutClose = _defaultShortcutClose;
        }
        #endregion

        #region ShortcutCloseAllButThis
        /// <summary>
        /// Gets and sets the shortcut for 'close all but this' page.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for 'close all but this' page.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutCloseAllButThis
        {
            get { return _shortcutCloseAllButThis; }
            set { _shortcutCloseAllButThis = value; }
        }

        /// <summary>
        /// Decide if the shortcut for 'close all but this' page.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutCloseAllButThis()
        {
            return !_shortcutCloseAllButThis.Equals(_defaultShortcutCloseAllButThis);
        }

        /// <summary>
        /// Resets the ShortcutCloseAllButThis property to its default value.
        /// </summary>
        public void ResetShortcutCloseAllButThis()
        {
            ShortcutCloseAllButThis = _defaultShortcutCloseAllButThis;
        }
        #endregion

        #region ShortcutMoveNext
        /// <summary>
        /// Gets and sets the shortcut for moving the current page to the next cell.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for moving the current page to the next cell.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutMoveNext
        {
            get { return _shortcutMoveNext; }
            set { _shortcutMoveNext = value; }
        }

        /// <summary>
        /// Decide if the shortcut for moving the current page to the next cell.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutMoveNext()
        {
            return !_shortcutMoveNext.Equals(_defaultShortcutMoveNext);
        }

        /// <summary>
        /// Resets the ShortcutMoveNext property to its default value.
        /// </summary>
        public void ResetShortcutMoveNext()
        {
            ShortcutMoveNext = _defaultShortcutMoveNext;
        }
        #endregion

        #region ShortcutMovePrevious
        /// <summary>
        /// Gets and sets the shortcut for moving the current page to the previous cell.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for moving the current page to the previous cell.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutMovePrevious
        {
            get { return _shortcutMovePrevious; }
            set { _shortcutMovePrevious = value; }
        }

        /// <summary>
        /// Decide if the shortcut for moving the current page to the previous cell.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutMovePrevious()
        {
            return !_shortcutMovePrevious.Equals(_defaultShortcutMovePrevious);
        }

        /// <summary>
        /// Resets the ShortcutMovePrevious property to its default value.
        /// </summary>
        public void ResetShortcutMovePrevious()
        {
            ShortcutMovePrevious = _defaultShortcutMovePrevious;
        }
        #endregion

        #region ShortcutSplitVertical
        /// <summary>
        /// Gets and sets the shortcut for splitting the current page into a vertical aligned page.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for splitting the current page into a vertical aligned page.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutSplitVertical
        {
            get { return _shortcutSplitVertical; }
            set { _shortcutSplitVertical = value; }
        }

        /// <summary>
        /// Decide if the shortcut for splitting the current page into a vertical aligned page.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutSplitVertical()
        {
            return !_shortcutSplitVertical.Equals(_defaultShortcutSplitVertical);
        }

        /// <summary>
        /// Resets the ShortcutSplitVertical property to its default value.
        /// </summary>
        public void ResetShortcutSplitVertical()
        {
            ShortcutSplitVertical = _defaultShortcutSplitVertical;
        }
        #endregion

        #region ShortcutSplitHorizontal
        /// <summary>
        /// Gets and sets the shortcut for splitting the current page into a horizontal aligned page.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for splitting the current page into a horizontal aligned page.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutSplitHorizontal
        {
            get { return _shortcutSplitHorizontal; }
            set { _shortcutSplitHorizontal = value; }
        }

        /// <summary>
        /// Decide if the shortcut for splitting the current page into a horizontal aligned page.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutSplitHorizontal()
        {
            return !_shortcutSplitHorizontal.Equals(_defaultShortcutSplitHorizontal);
        }

        /// <summary>
        /// Resets the ShortcutSplitHorizontal property to its default value.
        /// </summary>
        public void ResetShortcutSplitHorizontal()
        {
            ShortcutSplitHorizontal = _defaultShortcutSplitHorizontal;
        }
        #endregion

        #region ShortcutRebalance
        /// <summary>
        /// Gets and sets the shortcut for rebalancing the layout.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for rebalancing the layout.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutRebalance
        {
            get { return _shortcutRebalance; }
            set { _shortcutRebalance = value; }
        }

        /// <summary>
        /// Decide if the shortcut for rebalancing the layout.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutRebalance()
        {
            return !_shortcutRebalance.Equals(_defaultShortcutRebalance);
        }

        /// <summary>
        /// Resets the ShortcutRebalance property to its default value.
        /// </summary>
        public void ResetShortcutRebalance()
        {
            ShortcutRebalance = _defaultShortcutRebalance;
        }
        #endregion

        #region ShortcutMaximizeRestore
        /// <summary>
        /// Gets and sets the shortcut for maximizing/restoring the layout.
        /// </summary>
        [Category("Visuals")]
        [Description("Shortcut for maximizing/restoring the layout.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Keys ShortcutMaximizeRestore
        {
            get { return _shortcutMaximizeRestore; }
            set { _shortcutMaximizeRestore = value; }
        }

        /// <summary>
        /// Decide if the shortcut for maximizing/restoring the layout.
        /// </summary>
        /// <returns>True if value should be serialized.</returns>
        protected bool ShouldSerializeShortcutMaximizeRestore()
        {
            return !_shortcutMaximizeRestore.Equals(_defaultShortcutMaximizeRestore);
        }

        /// <summary>
        /// Resets the ShortcutMaximizeRestore property to its default value.
        /// </summary>
        public void ResetShortcutMaximizeRestore()
        {
            ShortcutMaximizeRestore = _defaultShortcutMaximizeRestore;
        }
        #endregion

        #region ShowContextMenu
        /// <summary>
        /// Gets and sets if a workspace context menu is shown on tab right clicking.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if a workspace context menu is added on tab right clicking.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(true)]
        public bool ShowContextMenu
        {
            get { return _showContextMenu; }
            set { _showContextMenu = value; }
        }
        #endregion
    }
}
