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
using System.Diagnostics;
using System.ComponentModel;


namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Expose a global set of strings used within Krypton and that are localizable.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
    public class GlobalStrings : GlobalId
    {
        #region Static Fields
        private static readonly string DEFAULT_OK = "OK";
        private static readonly string DEFAULT_CANCEL = "Cancel";
        private static readonly string DEFAULT_YES = "Yes";
        private static readonly string DEFAULT_NO = "No";
        private static readonly string DEFAULT_ABORT = "Abort";
        private static readonly string DEFAULT_RETRY = "Retry";
        private static readonly string DEFAULT_IGNORE = "Ignore";
        private static readonly string DEFAULT_CLOSE = "Close";
        private static readonly string DEFAULT_TODAY = "Today";
        #endregion

        #region Instance Fields
        private string _ok;
        private string _cancel;
        private string _yes;
        private string _no;
        private string _abort;
        private string _retry;
        private string _ignore;
        private string _close;
        private string _today;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the Clipping class.
		/// </summary>
        public GlobalStrings()
        {
            Reset();
        }

        /// <summary>
        /// Returns a string that represents the current defaulted state.
        /// </summary>
        /// <returns>A string that represents the current defaulted state.</returns>
        public override string ToString()
        {
            if (!IsDefault)
                return "Modified";

            return string.Empty;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets a value indicating if all the strings are default values.
        /// </summary>
        /// <returns>True if all values are defaulted; otherwise false.</returns>
        [Browsable(false)]
        public bool IsDefault
        {
            get
            {
                return _ok.Equals(DEFAULT_OK) &&
                       _cancel.Equals(DEFAULT_CANCEL) &&
                       _yes.Equals(DEFAULT_YES) &&
                       _no.Equals(DEFAULT_NO) &&
                       _abort.Equals(DEFAULT_ABORT) &&
                       _retry.Equals(DEFAULT_RETRY) &&
                       _ignore.Equals(DEFAULT_IGNORE) &&
                       _close.Equals(DEFAULT_CLOSE) &&
                       _today.Equals(DEFAULT_CLOSE);
            }
        }

        /// <summary>
        /// Reset all strings to default values.
        /// </summary>
        public void Reset()
        {
            _ok = DEFAULT_OK;
            _cancel = DEFAULT_CANCEL;
            _yes = DEFAULT_YES;
            _no = DEFAULT_NO;
            _abort = DEFAULT_ABORT;
            _retry = DEFAULT_RETRY;
            _ignore = DEFAULT_IGNORE;
            _close = DEFAULT_CLOSE;
            _today = DEFAULT_TODAY;
        }

        /// <summary>
        /// Gets and sets the OK string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("OK string used for message box buttons.")]
        [DefaultValue("OK")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string OK
        {
            get { return _ok; }
            set { _ok = value; }
        }

        /// <summary>
        /// Gets and sets the Cancel string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Cancel string used for message box buttons.")]
        [DefaultValue("Cancel")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        /// Gets and sets the Yes string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Yes string used for message box buttons.")]
        [DefaultValue("Yes")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Yes
        {
            get { return _yes; }
            set { _yes = value; }
        }

        /// <summary>
        /// Gets and sets the No string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("No string used for message box buttons.")]
        [DefaultValue("No")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string No
        {
            get { return _no; }
            set { _no = value; }
        }

        /// <summary>
        /// Gets and sets the Abort string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Abort string used for message box buttons.")]
        [DefaultValue("Abort")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Abort
        {
            get { return _abort; }
            set { _abort = value; }
        }
        
        /// <summary>
        /// Gets and sets the Retry string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Retry string used for message box buttons.")]
        [DefaultValue("Retry")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Retry
        {
            get { return _retry; }
            set { _retry = value; }
        }

        /// <summary>
        /// Gets and sets the Ignore string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Ignore string used for message box buttons.")]
        [DefaultValue("Ignore")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Ignore
        {
            get { return _ignore; }
            set { _ignore = value; }
        }

        /// <summary>
        /// Gets and sets the Close string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Close string used for message box buttons.")]
        [DefaultValue("Close")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Close
        {
            get { return _close; }
            set { _close = value; }
        }

        /// <summary>
        /// Gets and sets the Close string used in calendars.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Today string used for calendars.")]
        [DefaultValue("Today")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public string Today
        {
            get { return _today; }
            set { _today = value; }
        }
        #endregion
    }
}
