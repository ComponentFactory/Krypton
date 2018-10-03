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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Draw the date time picker text.
    /// </summary>
    public class ViewDrawDateTimeText : ViewLeaf
    {
        #region Type Declarations
        private class FormatHandler
        {
            #region Instance Fields
            private bool _hasFocus;
            private bool _rightToLeftLayout;
            private int _activeFragment;
            private FormatFragmentList _fragments;
            private String _inputDigits;
            private DateTime _dt;
            private KryptonDateTimePicker _dateTimePicker;
            private NeedPaintHandler _needPaint;
            private ViewDrawDateTimeText _timeText;
            #endregion

            #region Identity
            /// <summary>
            /// Initalize a new instance of the FormatHandler class.
            /// </summary>
            /// <param name="dateTimePicker">Reference to owning date time picker.</param>
            /// <param name="timeText">Reference to owning time text element.</param>
            /// <param name="needPaint">Delegate for invoking repainting.</param>
            public FormatHandler(KryptonDateTimePicker dateTimePicker,
                                 ViewDrawDateTimeText timeText,
                                 NeedPaintHandler needPaint)
            {
                _dateTimePicker = dateTimePicker;
                _timeText = timeText;
                _needPaint = needPaint;
                _fragments = new FormatFragmentList();
                _activeFragment = -1;
                _inputDigits = null;
                _rightToLeftLayout = false;
            }

            /// <summary>
            /// Obtains the String representation of this instance.
            /// </summary>
            /// <returns>User readable name of the instance.</returns>
            public override string ToString()
            {
                StringBuilder ret = new StringBuilder();
                for(int i=0; i<_fragments.Count; i++)
                    ret.Append(_fragments[i].GetDisplay(_dt));
                return ret.ToString();
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets and sets the need to show focus.
            /// </summary>
            public bool HasFocus
            {
                get { return _hasFocus; }
                set { _hasFocus = value; }
            }

            /// <summary>
            /// Gets and sets the right to left layout of text.
            /// </summary>
            public bool RightToLeftLayout
            {
                get { return _rightToLeftLayout; }
                set { _rightToLeftLayout = value; }
            }

            /// <summary>
            /// Gets a value indicating if there is an active char fragment.
            /// </summary>
            public bool HasActiveFragment
            {
                get { return (_activeFragment >= 0); }
            }

            /// <summary>
            /// Gets and sets the active fragment based on the fragment string.
            /// </summary>
            public string ActiveFragment
            {
                get
                {
                    if (!HasActiveFragment)
                        return String.Empty;
                    else
                        return _fragments[_activeFragment].FragFormat;
                }

                set
                {
                    _activeFragment = -1;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            if (_fragments[i].FragFormat.Equals(value))
                            {
                                _activeFragment = i;
                                return;
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Clear the active fragment.
            /// </summary>
            public void ClearActiveFragment()
            {
                _activeFragment = -1;
            }

            /// <summary>
            /// Gets and sets the date time currently used by the handler.
            /// </summary>
            public DateTime DateTime
            {
                get { return _dt; }
                set { _dt = value; }
            }

            /// <summary>
            /// Moves to the first char fragment.
            /// </summary>
            public void MoveFirst()
            {
                if (ImplRightToLeft)
                {
                    _activeFragment = -1;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                            _activeFragment = i;
                    }
                }
                else
                {
                    _activeFragment = -1;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }
                }
            }

            /// <summary>
            /// Moves to the last char fragment.
            /// </summary>
            public void MoveLast()
            {
                if (ImplRightToLeft)
                {
                    _activeFragment = -1;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }
                }
                else
                {
                    _activeFragment = -1;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                            _activeFragment = i;
                    }
                }
            }

            /// <summary>
            /// Moves left one char fragment.
            /// </summary>
            public void MoveLeft()
            {
                if (ImplRightToLeft)
                {
                    for (int i = _activeFragment + 1; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }

                    _activeFragment = -1;
                }
                else
                {
                    for (int i = _activeFragment - 1; i >= 0; i--)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }

                    _activeFragment = -1;
                }
            }

            /// <summary>
            /// Move to the right one char fragment.
            /// </summary>
            public void MoveRight()
            {
                if (ImplRightToLeft)
                {
                    for (int i = _activeFragment - 1; i >= 0; i--)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }

                    _activeFragment = -1;
                }
                else
                {
                    for (int i = _activeFragment + 1; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            _activeFragment = i;
                            return;
                        }
                    }

                    _activeFragment = -1;
                }
            }

            /// <summary>
            /// Move to the next fragment.
            /// </summary>
            public void MoveNext()
            {
                MoveRight();

                // Rotate around the end of the fragments
                if (!HasActiveFragment)
                    MoveFirst();
            }

            /// <summary>
            /// Move to the next fragment.
            /// </summary>
            public void MovePrevious()
            {
                MoveLeft();

                // Rotate around the start of the fragments
                if (!HasActiveFragment)
                    MoveLast();
            }

            /// <summary>
            /// Select the nearest fragment to the mouse point.
            /// </summary>
            /// <param name="pt">Mouse point.</param>
            public void SelectFragment(Point pt)
            {
                if (ImplRightToLeft)
                {
                    int totalWidth = 0;
                    for (int i = _fragments.Count - 1; i >= 0; i--)
                    {
                        totalWidth += (i == 0 ? _fragments[i].TotalWidth : _fragments[i].TotalWidth - _fragments[i - 1].TotalWidth);
                        if (_fragments[i].AllowActive)
                        {

                            // If the point is after the current fragment, make it the active fragment
                            if (pt.X > (_timeText.ClientRectangle.Right - totalWidth))
                            {
                                EndInputDigits();
                                _activeFragment = i;
                                return;
                            }
                        }
                    }

                    MoveLast();
                }
                else
                {
                    // Adjust point for the location of the text
                    pt.X -= _timeText.ClientLocation.X;

                    for (int i = 0; i < _fragments.Count; i++)
                    {
                        if (_fragments[i].AllowActive)
                        {
                            // If the point is before the current fragment, make it the active fragment
                            if (pt.X < _fragments[i].TotalWidth)
                            {
                                EndInputDigits();
                                _activeFragment = i;
                                return;
                            }
                        }
                    }

                    MoveLast();
                }
            }

            /// <summary>
            /// Increment the current fragment value.
            /// </summary>
            /// <param name="forward">Forward to add; otherwise subtract.</param>
            /// <returns>Modified date/time.</returns>
            public DateTime Increment(bool forward)
            {
                // Pass request onto the fragment itself
                if (_activeFragment >= 0)
                    return _fragments[_activeFragment].Increment(_dt, forward);
                else
                    return _dt;
            }

            /// <summary>
            /// Invert the AM/PM indicator for the date.
            /// </summary>
            /// <param name="am">Am requested.</param>
            /// <returns>Modified date/time.</returns>
            public DateTime AMPM(bool am)
            {
                // Pass request onto the fragment itself
                if (_activeFragment >= 0)
                    return _fragments[_activeFragment].AMPM(_dt, am);
                else
                    return _dt;
            }

            /// <summary>
            /// Gets a value indicating if input digits are being processed.
            /// </summary>
            public bool IsInputDigits
            {
                get { return (_inputDigits != null); }
            }

            /// <summary>
            /// Process the input of numeric digit.
            /// </summary>
            /// <param name="digit">Input digit.</param>
            public void InputDigit(char digit)
            {
                // We clear the cache if the active fragment says no digits are allowed
                if ((_activeFragment == -1) || (_fragments[_activeFragment].InputDigits == 0))
                    _inputDigits = null;
                else
                {
                    if (_inputDigits == null)
                        _inputDigits = "";

                    // Append the latest digit
                    _inputDigits += digit;

                    // We need to special case the digit entry for descriptive months
                    if (_fragments[_activeFragment].FragFormat.Contains("MMM"))
                    {
                        // Get the actual month number entered
                        int monthNumber = int.Parse(_inputDigits);

                        // If two digits is not valid then use just the last digit
                        if (monthNumber > 12)
                            monthNumber -= 10;
                        else if (monthNumber == 0)
                            monthNumber = 10;

                        // Set the new date using the month number
                        DateTime dt = _dt.AddMonths(monthNumber - _dt.Month);
                        if (!dt.Equals(_dt))
                        {
                            _dateTimePicker.Value = _timeText.ValidateDate(dt);
                            _needPaint(this, new NeedLayoutEventArgs(true));
                        }

                        // Keep the digit if there is just a single '1' or '0' digit that might be start of two digit month
                        if ((_inputDigits.Length > 1) || ((_inputDigits.Length == 1) && (monthNumber > 1) && (monthNumber < 10)))
                        {
                            // Do we need to shift to the next field?
                            if ((_inputDigits.Length == 2) && _dateTimePicker.AutoShift)
                            {
                                MoveRight();

                                // Rotate around the end of the fragments
                                if (!HasActiveFragment)
                                {
                                    // Use evnet to show that we are overflowing
                                    CancelEventArgs cea = new CancelEventArgs();
                                    _timeText.OnAutoShiftOverflow(cea);

                                    // Event might be cancelled so check we want to overflow
                                    if (!cea.Cancel)
                                    {
                                        if (_dateTimePicker.ShowCheckBox)
                                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                                        else
                                            MoveFirst();
                                    }
                                }
                            }

                            _inputDigits = null;
                        }
                    }
                    else
                    {
                        // If we hit the maximum number of valid digits
                        if (_inputDigits.Length == _fragments[_activeFragment].InputDigits)
                        {
                            // Ask the fragment to process the digits
                            DateTime dt = _fragments[_activeFragment].EndDigits(_dt, _inputDigits);
                            if (!dt.Equals(_dt))
                            {
                                _dateTimePicker.Value = _timeText.ValidateDate(dt);
                                _needPaint(this, new NeedLayoutEventArgs(true));
                            }

                            _inputDigits = null;


                            // Do we need to shift to the next field?
                            if (_dateTimePicker.AutoShift)
                            {
                                MoveRight();

                                // Rotate around the end of the fragments
                                if (!HasActiveFragment)
                                {
                                    // Use evnet to show that we are overflowing
                                    CancelEventArgs cea = new CancelEventArgs();
                                    _timeText.OnAutoShiftOverflow(cea);

                                    // Event might be cancelled so check we want to overflow
                                    if (!cea.Cancel)
                                    {
                                        if (_dateTimePicker.ShowCheckBox)
                                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                                        else
                                            MoveFirst();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Process the end of inputting digits.
            /// </summary>
            public void EndInputDigits()
            {
                // Do we have input waiting to be processed and a matching active fragment
                if ((_inputDigits != null) && (_activeFragment >= 0))
                {
                    DateTime dt = _fragments[_activeFragment].EndDigits(_dt, _inputDigits);
                    if (!dt.Equals(_dt))
                    {
                        _dateTimePicker.Value = _timeText.ValidateDate(dt);
                        _needPaint(this, new NeedLayoutEventArgs(true));
                    }
                    _inputDigits = null;
                }
            }

            /// <summary>
            /// Parse a new format into fragments.
            /// </summary>
            /// <param name="format">Format string to parse.</param>
            /// <param name="g">Graphics instance used to measure text.</param>
            /// <param name="font">Font used to measure text.</param>
            public void ParseFormat(string format, Graphics g, Font font)
            {
                // Split the format into a set of fragments
                _fragments = ParseFormatToFragments(format);

                // If there anything to measure?
                if (_fragments.Count > 0)
                {
                    // Measure the pixel width of each fragment
                    MeasureFragments(g, font, _dt);
                }

                // If we have an active fragment make sure it is still valid
                ValidateActiveFragment();
            }

            /// <summary>
            /// Render the text.
            /// </summary>
            /// <param name="context">Render context.</param>
            /// <param name="font">Text font.</param>
            /// <param name="rect">Client rectangle area.</param>
            /// <param name="textColor">Text color.</param>
            /// <param name="backColor">Back color.</param>
            /// <param name="enabled">If text enabled.</param>
            public void Render(RenderContext context, Font font, Rectangle rect, 
                               Color textColor, Color backColor, bool enabled)
            {
                if (enabled || string.IsNullOrEmpty(_dateTimePicker.CustomNullText))
                {
                    // If there anything to draw?
                    if (_fragments.Count > 0)
                    {
                        if (ImplRightToLeft)
                        {
                            // Right align by updating the drawing rectangle
                            int textWidth = _fragments[_fragments.Count - 1].TotalWidth;
                            rect.X = rect.Right - textWidth - 1;
                            rect.Width = textWidth;
                        }

                        int lastTotalWidth = 0;
                        for (int i = 0; i < _fragments.Count; i++)
                        {
                            Color foreColor = textColor;
                            int totalWidth = _fragments[i].TotalWidth;
                            if (totalWidth > rect.Width)
                                totalWidth = rect.Width;

                            Rectangle drawText = new Rectangle(rect.X + lastTotalWidth, rect.Y, totalWidth - lastTotalWidth, rect.Height);
                            if (drawText.Width > 0)
                            {
                                // If we need to draw a focus indication?
                                if (HasFocus)
                                {
                                    // Do we have an active fragment?
                                    if (_activeFragment == i)
                                    {
                                        // Draw background in the highlight color
                                        if (enabled)
                                        {
                                            context.Graphics.FillRectangle(SystemBrushes.Highlight, new Rectangle(drawText.X - 1, drawText.Y, drawText.Width + 2, drawText.Height));
                                            foreColor = SystemColors.HighlightText;
                                        }
                                        else
                                        {
                                            using (SolidBrush fillBrush = new SolidBrush(foreColor))
                                                context.Graphics.FillRectangle(fillBrush, drawText);

                                            foreColor = backColor;
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(_inputDigits) &&
                                    (_activeFragment == i) &&
                                    !_fragments[_activeFragment].FragFormat.Contains("MMM"))
                                {
                                    // Draw input digits for this fragment
                                    TextRenderer.DrawText(context.Graphics, _inputDigits, font, drawText, foreColor, _drawLeftFlags);
                                }
                                else
                                {
                                    // Draw text for this fragment only
                                    TextRenderer.DrawText(context.Graphics, _fragments[i].GetDisplay(_dt), font, drawText, foreColor, _drawLeftFlags);
                                }

                                lastTotalWidth = totalWidth;
                            }
                        }
                    }
                }
                else
                {
                    // Draw input digits for this fragment
                    TextRenderer.DrawText(context.Graphics, _dateTimePicker.CustomNullText, font, rect, textColor, _drawLeftFlags);
                }
            }
            #endregion

            #region Private
            private void ValidateActiveFragment()
            {
                int firstFocus = -1;
                for (int i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        if (firstFocus == -1)
                        {
                            firstFocus = i;
                            break;
                        }
                    }
                }

                if (_activeFragment >= 0)
                {
                    if (_activeFragment >= _fragments.Count)
                    {
                        EndInputDigits();
                        _activeFragment = firstFocus;
                    }
                    else if (!(_fragments[_activeFragment].AllowActive))
                    {
                        EndInputDigits();
                        _activeFragment = firstFocus;
                    }
                }
            }

            private bool ImplRightToLeft
            {
                get { return (RightToLeftLayout && (_dateTimePicker.RightToLeft == RightToLeft.Yes)); }
            }

            private void MeasureFragments(Graphics g, Font font, DateTime dt)
            {
                // Create a character range/characger region for each of the fragments
                CharacterRange[] charRanges = new CharacterRange[_fragments.Count];
                Region[] charRegion = new Region[_fragments.Count];

                // Generate the output for each fragment and measure the length of that fragment output
                for (int i = 0; i < _fragments.Count; i++)
                    charRanges[i] = new CharacterRange(0, _fragments[i].GenerateOutput(dt).Length);

                // Update format with details of the ranges to measure
                StringFormat measureFormat = new StringFormat(StringFormatFlags.FitBlackBox);
                measureFormat.SetMeasurableCharacterRanges(charRanges);

                // Perform measuring using the output of the last fragmet (last frag must be the whole output string)
                charRegion = g.MeasureCharacterRanges(_fragments[_fragments.Count - 1].Output, font, _measureRect, measureFormat);

                // Push return values into the individual fragment entries
                for (int i = 0; i < _fragments.Count; i++)
                    _fragments[i].TotalWidth = (int)Math.Ceiling((double)charRegion[i].GetBounds(g).Width);
            }

            private FormatFragmentList ParseFormatToFragments(string format)
            {
                FormatFragmentList fragList = new FormatFragmentList();

                // Grab the string used for formatting
                int length = format.Length;
                int current = 0;
                int literal = 0;

                // Use state machine to parse the format one character at a time
                while (current < length)
                {
                    // Processing depends on the character
                    switch (format[current])
                    {
                        case 'h':
                        case 'H':
                        case 'm':
                        case 's':
                        case 't':
                        case 'd':
                        case 'y':
                            ParseCharacter(format[current], fragList, ref literal, ref current, ref format);
                            break;
                        case 'f':
                            ParseCharacter('f', 7, fragList, ref literal, ref current, ref format);
                            break;
                        case 'F':
                            ParseCharacter('F', 7, fragList, ref literal, ref current, ref format);
                            break;
                        case 'M':
                            ParseCharacter('M', 4, fragList, ref literal, ref current, ref format);
                            break;
                        // ' = everything inside the single quotes is a literal
                        case '\'':
                            do
                            {
                                current++;
                                literal++;
                            }
                            while ((current < length) && (format[current] != '\''));

                            if ((current < length) && (format[current] == '\''))
                            {
                                current++;
                                literal++;
                            }
                            break;
                        // Prefix before a single format character
                        case '%':
                            current++;
                            break;
                        // All other characters are literals
                        default:
                            current++;
                            literal++;
                            break;
                    }
                }

                // Add any trailing literal
                if (literal > 0)
                    fragList.Add(new FormatFragment(current, format, format.Substring(current - literal, literal)));

                return fragList;
            }

            private void ParseCharacter(char charater, FormatFragmentList fragList,
                                        ref int literal, ref int current, ref string format)
            {
                ParseCharacter(charater, int.MaxValue, fragList, ref literal, ref current, ref format);
            }

            private void ParseCharacter(char charater, int max, FormatFragmentList fragList,
                                        ref int literal, ref int current, ref string format)
            {
                if (literal > 0)
                    fragList.Add(new FormatFragment(current, format, format.Substring(current - literal, literal)));
                int count = CountUptoMaxCharacters(charater, max, ref current, ref format);
                fragList.Add(new FormatFragmentChar(current, format, charater, count));
                literal = 0;
            }

            private int CountUptoMaxCharacters(char character, int max, ref int current, ref string format)
            {
                int count = 0;
                int length = format.Length;

                // Keep consuming until we run out of characters
                while ((current < length) && (count < max))
                {
                    // Only interested in the specified characters
                    if (format[current] == character)
                    {
                        count++;
                        current++;
                    }
                    else
                        break;
                }

                return count;
            }
            #endregion
        }

        private class FormatFragment
        {
            #region Instance Fields
            private string _fragment;
            private string _fragFormat;
            private string _output;
            private int _totalWidth;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the FormatFragment class.
            /// </summary>
            /// <param name="length">Length of the format string to extract.</param>
            /// <param name="format">Source string to extra fragment from.</param>
            /// <param name="literal">String literal.</param>
            public FormatFragment(int length, string format, string literal)
            {
                _fragFormat = literal;

                if (length == 0)
                    _fragment = string.Empty;
                else
                    _fragment = format.Substring(0, length);
            }

            /// <summary>
            /// Output a text representation of the fragment.
            /// </summary>
            /// <returns>String instance.</returns>
            public override string ToString()
            {
                return _fragment;
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the fragment string.
            /// </summary>
            public string Fragment
            {
                get { return _fragment; }
            }

            /// <summary>
            /// Gets access to the fragment format string.
            /// </summary>
            public virtual string FragFormat
            {
                get { return _fragFormat; }
            }

            /// <summary>
            /// Gets access to the generate output.
            /// </summary>
            public string Output
            {
                get { return _output; }
            }

            /// <summary>
            /// Gets and sets the total pixel width of this fragments output.
            /// </summary>
            public int TotalWidth
            {
                set { _totalWidth = value; }
                get { return _totalWidth; }
            }

            /// <summary>
            /// Generate the output string from the provided date and the format fragment.
            /// </summary>
            /// <param name="dt">DateTime used to generate output.</param>
            /// <returns>Generated output string.</returns>
            public string GenerateOutput(DateTime dt)
            {
                // Use helper to ensure single character formats are handled correctly
                _output = dt.ToString(CommonHelper.MakeCustomDateFormat(Fragment));
                return _output;
            }

            /// <summary>
            /// Can this field be edited and active.
            /// </summary>
            public virtual bool AllowActive
            {
                get { return false; }
            }

            /// <summary>
            /// Gets the number of digits allowed to be entered for this fragment.
            /// </summary>
            public virtual int InputDigits
            {
                get { return 0; }
            }

            /// <summary>
            /// Process the input digits to modify the incoming date time.
            /// </summary>
            /// <param name="dt">Date time to modify.</param>
            /// <param name="digits">Set of digits to process.</param>
            /// <returns>Modified date time.</returns>
            public virtual DateTime EndDigits(DateTime dt, string digits)
            {
                return dt;
            }

            /// <summary>
            /// Gets the display string for display using the provided date time.
            /// </summary>
            /// <param name="dt">DateTime to format.</param>
            /// <returns>Display string.</returns>
            public virtual string GetDisplay(DateTime dt)
            {
                if (FragFormat.Length == 1)
                    return dt.ToString("\\" + FragFormat);
                else
                    return dt.ToString(FragFormat);
            }

            /// <summary>
            /// Increment the current fragment value.
            /// </summary>
            /// <param name="dt">DateTime to be modified.</param>
            /// <param name="forward">Forward to add; otherwise subtract.</param>
            /// <returns>Modified date/time.</returns>
            public virtual DateTime Increment(DateTime dt, bool forward)
            {
                // Cannot change a literal, so do nothing
                Debug.Assert(false);
                return dt;
            }

            /// <summary>
            /// Invert the AM/PM indicator for the date.
            /// </summary>
            /// <param name="dt">DateTime to be modified.</param>
            /// <param name="am">Am requested.</param>
            /// <returns>Modified date/time.</returns>
            public virtual DateTime AMPM(DateTime dt, bool am)
            {
                // Cannot change a literal, so do nothing
                Debug.Assert(false);
                return dt;
            }
            #endregion
        }

        private class FormatFragmentChar : FormatFragment
        {
            #region Instance Fields
            private string _fragFormat;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the FormatFragmentChar class.
            /// </summary>
            /// <param name="index">Index after the string we want.</param>
            /// <param name="format">Source string to extra fragment from.</param>
            /// <param name="character">Character that represents the format fragment.</param>
            /// <param name="count">Number characters in the fragment.</param>
            public FormatFragmentChar(int index, string format, char character, int count)
                : base(index, format, string.Empty)
            {
                _fragFormat = new string(character, count);
            }

            /// <summary>
            /// Output a text representation of the fragment.
            /// </summary>
            /// <returns>String instance.</returns>
            public override string ToString()
            {
                return base.ToString() + " (" + _fragFormat + ")";
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets access to the fragment format string.
            /// </summary>
            public override string FragFormat
            {
                get { return _fragFormat; }
            }

            /// <summary>
            /// Can this field be edited and active.
            /// </summary>
            public override bool AllowActive
            {
                get 
                {
                    // Only certain formats can be edited
                    switch (FragFormat)
                    {
                        case "d":
                        case "dd":
                        case "M":
                        case "MM":
                        case "MMM":
                        case "MMMM":
                            return true;
                    }

                    if (FragFormat.StartsWith("h") || 
                        FragFormat.StartsWith("H") ||
                        FragFormat.StartsWith("m") ||
                        FragFormat.StartsWith("s") ||
                        FragFormat.StartsWith("t") ||
                        FragFormat.StartsWith("f") ||
                        FragFormat.StartsWith("F") ||
                        FragFormat.StartsWith("y"))
                        return true;

                    return false; 
                }
            }

            /// <summary>
            /// Gets the number of digits allowed to be entered for this fragment.
            /// </summary>
            public override int InputDigits
            {
                get
                {
                    // Only certain formats can be edited
                    switch (FragFormat)
                    {
                        case "f":
                        case "F":
                            return 1;
                        case "d":
                        case "dd":
                        case "M":
                        case "MM":
                        case "MMM":
                        case "MMMM":
                        case "ff":
                        case "FF":
                            return 2;
                        case "fff":
                        case "FFF":
                            return 3;
                        case "ffff":
                        case "FFFF":
                            return 4;
                        case "fffff":
                        case "FFFFF":
                            return 5;
                        case "ffffff":
                        case "FFFFFF":
                            return 6;
                        case "fffffff":
                        case "FFFFFFF":
                            return 7;
                    }

                    if (FragFormat.StartsWith("h") ||
                        FragFormat.StartsWith("H") ||
                        FragFormat.StartsWith("m") ||
                        FragFormat.StartsWith("s"))
                        return 2;

                    if (FragFormat.StartsWith("y"))
                        return 4;

                    return base.InputDigits;
                }
            }

            /// <summary>
            /// Process the input digits to modify the incoming date time.
            /// </summary>
            /// <param name="dt">Date time to modify.</param>
            /// <param name="digits">Set of digits to process.</param>
            /// <returns>Modified date time.</returns>
            public override DateTime EndDigits(DateTime dt, string digits)
            {
                switch (FragFormat)
                {
                    case "d":
                    case "dd":
                        int dayNumber = int.Parse(digits);
                        if ((dayNumber <= LastDayOfMonth(dt).Day) && (dayNumber > 0))
                            dt = dt.AddDays(dayNumber - dt.Day);
                        break;
                    case "M":
                    case "MM":
                        int monthNumber = int.Parse(digits);
                        if ((monthNumber <= 12) && (monthNumber > 0))
                            dt = dt.AddMonths(monthNumber - dt.Month);
                        break;
                    case "f":
                    case "F":
                    case "ff":
                    case "FF":
                    case "fff":
                    case "FFF":
                    case "ffff":
                    case "FFFF":
                    case "fffff":
                    case "FFFFF":
                    case "ffffff":
                    case "FFFFFF":
                    case "fffffff":
                    case "FFFFFFF":
                        dt = dt.AddMilliseconds(int.Parse(digits) - dt.Millisecond);
                        break;
                }

                if (FragFormat.StartsWith("h") || FragFormat.StartsWith("H"))
                {
                    int hoursNumber = int.Parse(digits);
                    if ((hoursNumber < 24) && (hoursNumber >= 0))
                        dt = dt.AddHours(hoursNumber - dt.Hour);
                } 
                else if (FragFormat.StartsWith("m"))
                {
                    int minutesNumber = int.Parse(digits);
                    if ((minutesNumber < 60) && (minutesNumber >= 0))
                        dt = dt.AddMinutes(minutesNumber - dt.Minute);
                }
                else if (FragFormat.StartsWith("s"))
                {
                    int secondsNumber = int.Parse(digits);
                    if ((secondsNumber < 60) && (secondsNumber >= 0))
                        dt = dt.AddSeconds(secondsNumber - dt.Second);
                }
                else if (FragFormat.StartsWith("y"))
                {
                    int yearNumber = int.Parse(digits);

                    // A zero year makes to change to the date
                    if (yearNumber != 0)
                    {
                        if (digits.Length == 2)
                        {
                            // Two digits causes the century/millenium to be auto added
                            if (yearNumber >= 30)
                                yearNumber += 1900;
                            else
                                yearNumber += 2000;

                            dt = dt.AddYears(yearNumber - dt.Year);
                        }
                        else if (digits.Length == 4)
                        {
                            // Four digits causes us to attempt to use that date
                            dt = dt.AddYears(yearNumber - dt.Year);
                        }
                    }
                }

                return dt;
            }

            /// <summary>
            /// Gets the display string for display using the provided date time.
            /// </summary>
            /// <param name="dt">DateTime to format.</param>
            /// <returns>Display string.</returns>
            public override string GetDisplay(DateTime dt)
            {
                return dt.ToString(CommonHelper.MakeCustomDateFormat(FragFormat));
            }

            /// <summary>
            /// Increment the current fragment value.
            /// </summary>
            /// <param name="dt">DateTime to be modified.</param>
            /// <param name="forward">Forward to add; otherwise subtract.</param>
            /// <returns>Modified date/time.</returns>
            public override DateTime Increment(DateTime dt, bool forward)
            {
                // Action depends on the format string
                switch (FragFormat)
                {
                    case "d":
                    case "dd":
                        int currentMonth = dt.Month;
                        if (forward)
                        {
                            if (dt.Day == LastDayOfMonth(dt).Day)
                                dt = dt.AddDays(-(dt.Day - 1));
                            else
                                dt = dt.AddDays(1);
                        }
                        else
                        {
                            if (dt.Day == 1)
                                dt = dt.AddDays(LastDayOfMonth(dt).Day - 1);
                            else
                                dt = dt.AddDays(-1);
                        }
                        break;
                    case "M":
                    case "MM":
                    case "MMM":
                    case "MMMM":
                        if (forward)
                        {
                            if (dt.Month == 12)
                                dt = dt.AddMonths(-(dt.Month - 1));
                            else
                                dt = dt.AddMonths(1);
                        }
                        else
                        {
                            if (dt.Month == 1)
                                dt = dt.AddMonths(11);
                            else
                                dt = dt.AddMonths(-1);
                        } 
                        break;
                }

                // Any number of 'h' or 'H' are allowed
                if (FragFormat.StartsWith("h") || FragFormat.StartsWith("H"))
                {
                    if (forward)
                    {
                        if (dt.Hour == 23)
                        {
                            dt = dt.AddHours(1);
                            dt = dt.AddDays(-1);
                        }
                        else
                            dt = dt.AddHours(1);
                    }
                    else
                    {
                        if (dt.Hour == 1)
                        {
                            dt = dt.AddHours(-1);
                            dt = dt.AddDays(1);
                        }
                        else
                            dt = dt.AddHours(-1);
                    }
                }

                // Any number of 'm'
                if (FragFormat.StartsWith("m"))
                {
                    if (forward)
                    {
                        if (dt.Minute == 59)
                            dt = dt.AddMinutes(-dt.Minute);
                        else
                            dt = dt.AddMinutes(1);
                    }
                    else
                    {
                        if (dt.Minute == 0)
                            dt = dt.AddMinutes(59 - dt.Minute);
                        else
                            dt = dt.AddMinutes(-1);
                    }
                }

                // Any number of 's'
                if (FragFormat.StartsWith("s"))
                {
                    if (forward)
                    {
                        if (dt.Second == 59)
                            dt = dt.AddSeconds(-dt.Second);
                        else
                            dt = dt.AddSeconds(1);
                    }
                    else
                    {
                        if (dt.Second == 0)
                            dt = dt.AddSeconds(59 - dt.Second);
                        else
                            dt = dt.AddSeconds(-1);
                    }
                }

                // Any number of 't'
                if (FragFormat.StartsWith("t"))
                {
                    if (dt.Hour > 11)
                        dt = dt.AddHours(-12);
                    else
                        dt = dt.AddHours(12);
                }

                // Any number of 'y'
                if (FragFormat.StartsWith("y"))
                {
                    if (forward)
                        dt = dt.AddYears(1);
                    else
                        dt = dt.AddYears(-1);
                }

                // Any number of 'f' or 'F'
                if (FragFormat.StartsWith("f") || FragFormat.StartsWith("F"))
                {
                    // We increment by the last digit (upto a maximum of 3 digits)
                    int digits = Math.Min(FragFormat.Length, 3);

                    // Convert to correct increment size
                    double increment = 1000;
                    for (int i = 0; i < digits; i++)
                        increment /= 10;

                    if (forward)
                        dt = dt.AddMilliseconds(increment);
                    else
                        dt = dt.AddMilliseconds(-increment);
                }

                return dt;
            }

            /// <summary>
            /// Invert the AM/PM indicator for the date.
            /// </summary>
            /// <param name="dt">DateTime to be modified.</param>
            /// <param name="am">Am requested.</param>
            /// <returns>Modified date/time.</returns>
            public override DateTime AMPM(DateTime dt, bool am)
            {
                if (FragFormat.StartsWith("t"))
                {
                    if ((dt.Hour > 11) && am)
                        dt = dt.AddHours(-12);
                    else if ((dt.Hour < 12) && !am)
                        dt = dt.AddHours(12);
                }

                return dt;
            }
            #endregion

            #region Implementation
            private DateTime LastDayOfMonth(DateTime dt)
            {
                dt = dt.AddMonths(1);
                dt = dt.AddDays(-dt.Day);
                return new DateTime(dt.Year, dt.Month, dt.Day);
            }
            #endregion
        }

        private class FormatFragmentList : List<FormatFragment> { };
        #endregion

        #region Static Fields
        private static readonly RectangleF _measureRect = new RectangleF(0, 0, 1000, 1000);
        private static readonly TextFormatFlags _measureFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;
        private static readonly TextFormatFlags _drawLeftFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;
        #endregion

        #region Instance Fields
        private KryptonDateTimePicker _dateTimePicker;
        private FormatHandler _formatHandler;
        private NeedPaintHandler _needPaint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawDateTimeText class.
		/// </summary>
        /// <param name="dateTimePicker">Color to fill drawing area.</param>
        /// <param name="needPaint">Delegate to allow repainting.</param>
        public ViewDrawDateTimeText(KryptonDateTimePicker dateTimePicker,
                                    NeedPaintHandler needPaint)
		{
            _dateTimePicker= dateTimePicker;
            _needPaint = needPaint;
            _formatHandler = new FormatHandler(dateTimePicker, this, needPaint);
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
            // Return the display string.
            _formatHandler.DateTime = _dateTimePicker.Value;
            return _formatHandler.ToString();
		}
		#endregion

        #region RightToLeftLayout
        /// <summary>
        /// Gets and sets the right to left layout of text.
        /// </summary>
        public bool RightToLeftLayout
        {
            get { return _formatHandler.RightToLeftLayout; }
            set { _formatHandler.RightToLeftLayout = value; }
        }
        #endregion

        #region AutoShiftOverflow
        /// <summary>
        /// Raises the AutoShiftOverflow event.
        /// </summary>
        /// <param name="e">An CancelEventArgs the contains the event data.</param>
        public void OnAutoShiftOverflow(CancelEventArgs e)
        {
            _dateTimePicker.OnAutoShiftOverflow(e);
        }
        #endregion

        #region HasFocus
        /// <summary>
        /// Gets and sets the need to show focus.
        /// </summary>
        public bool HasFocus
        {
            get { return _formatHandler.HasFocus; }
            set { _formatHandler.HasFocus = value; }
        }
        #endregion

        #region HasActiveFragment
        /// <summary>
        /// Gets a value indicating if there is an active char fragment.
        /// </summary>
        public bool HasActiveFragment
        {
            get { return _formatHandler.HasActiveFragment; }
        }
        #endregion

        #region ClearActiveFragment
        /// <summary>
        /// Remove active fragment.
        /// </summary>
        public void ClearActiveFragment()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();

            _formatHandler.ClearActiveFragment();
        }
        #endregion

        #region EndInputDigits
        /// <summary>
        /// End the input of input digits.
        /// </summary>
        public void EndInputDigits()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();
        }
        #endregion

        #region MoveFirstFragment
        /// <summary>
        /// Make the first fragment the active fragment.
        /// </summary>
        public void MoveFirstFragment()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();

            _formatHandler.MoveFirst();
        }
        #endregion

        #region ActiveFragment
        /// <summary>
        /// Gets and sets the active fragment based on the fragment string.
        /// </summary>
        public string ActiveFragment
        {
            get { return _formatHandler.ActiveFragment; }
            set { _formatHandler.ActiveFragment = value; }
        }
        #endregion

        #region MoveNextFragment
        /// <summary>
        /// Make the next fragment the active fragment.
        /// </summary>
        public void MoveNextFragment()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();

            _formatHandler.MoveNext();
        }
        #endregion

        #region MovePreviousFragment
        /// <summary>
        /// Make the previous fragment the active fragment.
        /// </summary>
        public void MovePreviousFragment()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();

            _formatHandler.MovePrevious();
        }
        #endregion

        #region MoveLastFragment
        /// <summary>
        /// Make the last fragment the active fragment.
        /// </summary>
        public void MoveLastFragment()
        {
            if (_formatHandler.IsInputDigits)
                _formatHandler.EndInputDigits();

            _formatHandler.MoveLast();
        }
        #endregion

        #region SelectFragment
        /// <summary>
        /// Select the fragment that is nearest the provided point.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button pressed down.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public void SelectFragment(Point pt, MouseButtons button)
        {
            // Tell the format handler to select the nearest fragment
            _formatHandler.SelectFragment(pt);
            PerformNeedPaint(true);
        }
        #endregion

        #region PerformKeyDown
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public void PerformKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (_formatHandler.IsInputDigits)
                        _formatHandler.EndInputDigits();

                    if (_dateTimePicker.ShowCheckBox && _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking)
                    {
                        if (_dateTimePicker.Checked)
                        {
                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = false;
                            _formatHandler.MoveLast();
                        }
                    }
                    else
                    {
                        _formatHandler.MoveLeft();

                        // Rotate around the start of the fragments
                        if (!_formatHandler.HasActiveFragment)
                        {
                            if (_dateTimePicker.ShowCheckBox)
                                _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                            else
                                _formatHandler.MoveLast();
                        }
                    }

                    PerformNeedPaint(false);
                    break;
                case Keys.Subtract:
                case Keys.Divide:
                case Keys.Decimal:
                case Keys.Right:
                    if (_formatHandler.IsInputDigits)
                        _formatHandler.EndInputDigits();

                    if (_dateTimePicker.ShowCheckBox && _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking)
                    {
                        if (_dateTimePicker.Checked)
                        {
                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = false;
                            _formatHandler.MoveFirst();
                        }
                    }
                    else
                    {
                        _formatHandler.MoveRight();

                        // Rotate around the end of the fragments
                        if (!_formatHandler.HasActiveFragment)
                        {
                            if (_dateTimePicker.ShowCheckBox)
                                _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                            else
                                _formatHandler.MoveFirst();
                        }
                    }

                    PerformNeedPaint(false);
                    break;
                case Keys.Up:
                    if (_dateTimePicker.Checked)
                    {
                        if (_formatHandler.IsInputDigits)
                            _formatHandler.EndInputDigits();
                        else
                            _dateTimePicker.Value = ValidateDate(_formatHandler.Increment(true));
    
                        PerformNeedPaint(false);
                    }
                    break;
                case Keys.Down:
                    if (_dateTimePicker.Checked)
                    {
                        if (_formatHandler.IsInputDigits)
                            _formatHandler.EndInputDigits();
                        else
                            _dateTimePicker.Value = ValidateDate(_formatHandler.Increment(false));
                        
                        PerformNeedPaint(false);
                    }
                    break;
                case Keys.A:
                case Keys.P:
                    if (_dateTimePicker.Checked)
                    {
                        _dateTimePicker.Value = ValidateDate(_formatHandler.AMPM((e.KeyCode == Keys.A)));
                        PerformNeedPaint(false);
                    }
                    break;
            }
        }
        #endregion

        #region PerformKeyPress
        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public void PerformKeyPress(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) && _formatHandler.HasActiveFragment)
            {
                _formatHandler.InputDigit(e.KeyChar);
                PerformNeedPaint(true);
            }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Inform handler of the date time currently used
            _formatHandler.DateTime = _dateTimePicker.Value;

            // Grab the font used to draw the text
            Font font = GetFont();

            // Find the width of the text we are drawing
            Size retSize = TextRenderer.MeasureText(GetFullDisplayText(), font, Size.Empty, _measureFlags);

            // The line height gives better appearance as it includes space for overhanging glyphs
            retSize.Height = Math.Max(font.Height, retSize.Height);

            // Add constant extra sizing to add padding around area
            retSize.Height += 3;

            return retSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Take all the provide space
            ClientRectangle = context.DisplayRectangle;

            // Inform handler of the date time currently used
            _formatHandler.DateTime = _dateTimePicker.Value;

            // Refresh the format handler with the latest format string
            _formatHandler.ParseFormat(GetFormat(), context.Graphics, GetFont());
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            // Inform handler of the date time currently used
            _formatHandler.DateTime = _dateTimePicker.Value;

            // Ask the format handler to perform actual rendering of the text
            using (Clipping clipped = new Clipping(context.Graphics, ClientRectangle))
                _formatHandler.Render(context, GetFont(), ClientRectangle,
                                      GetTextColor(), GetBackColor(),
                                      _dateTimePicker.Checked);
        }
        #endregion

        #region Implementation
        internal DateTime ValidateDate(DateTime dt)
        {
            if (dt < _dateTimePicker.EffectiveMinDate(_dateTimePicker.MinDate))
                return _dateTimePicker.EffectiveMinDate(_dateTimePicker.MinDate);
            else if (dt > _dateTimePicker.EffectiveMaxDate(_dateTimePicker.MaxDate))
                return _dateTimePicker.EffectiveMaxDate(_dateTimePicker.MaxDate);
            else
                return dt;
        }

        private void PerformNeedPaint(bool needLayout)
        {
            _needPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        private Font GetFont()
        {
            if (!Enabled || _dateTimePicker.InternalDateTimeNull())
                return _dateTimePicker.StateDisabled.PaletteContent.GetContentShortTextFont(PaletteState.Disabled);
            else
            {
                if (_dateTimePicker.IsActive)
                    return _dateTimePicker.StateActive.PaletteContent.GetContentShortTextFont(PaletteState.Normal);
                else
                    return _dateTimePicker.StateNormal.PaletteContent.GetContentShortTextFont(PaletteState.Normal);
            }
        }

        private Color GetTextColor()
        {
            if (!Enabled || _dateTimePicker.InternalDateTimeNull())
                return _dateTimePicker.StateDisabled.PaletteContent.GetContentShortTextColor1(PaletteState.Disabled);
            else
            {
                if (_dateTimePicker.IsActive)
                    return _dateTimePicker.StateActive.PaletteContent.GetContentShortTextColor1(PaletteState.Normal);
                else
                    return _dateTimePicker.StateNormal.PaletteContent.GetContentShortTextColor1(PaletteState.Normal);
            }
        }

        private Color GetBackColor()
        {
            if (!Enabled || _dateTimePicker.InternalDateTimeNull())
                return _dateTimePicker.StateDisabled.PaletteBack.GetBackColor1(PaletteState.Disabled);
            else
            {
                if (_dateTimePicker.IsActive)
                    return _dateTimePicker.StateActive.PaletteBack.GetBackColor1(PaletteState.Normal);
                else
                    return _dateTimePicker.StateNormal.PaletteBack.GetBackColor1(PaletteState.Normal);
            }
        }

        private string GetFormat()
        {
            string format = string.Empty;

            switch (_dateTimePicker.Format)
            {
                case DateTimePickerFormat.Long:
                    format = CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;
                    break;
                case DateTimePickerFormat.Short:
                    format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    break;
                case DateTimePickerFormat.Time:
                    format = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                    break;
                case DateTimePickerFormat.Custom:
                    // Use helper to ensure single character formats are handled correctly
                    format = CommonHelper.MakeCustomDateFormat(_dateTimePicker.CustomFormat);
                    break;
            }

            return format;
        }

        private string GetFullDisplayText()
        {
            try
            {
                return _dateTimePicker.Value.ToString(GetFormat());
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
