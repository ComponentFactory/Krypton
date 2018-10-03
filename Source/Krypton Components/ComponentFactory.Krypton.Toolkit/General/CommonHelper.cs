// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//  The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;

namespace ComponentFactory.Krypton.Toolkit
{
    #region Delegates
    /// <summary>
    /// Signature of a bare method.
    /// </summary>
    public delegate void SimpleCall();

    /// <summary>
    /// Signature of a method that performs an operation.
    /// </summary>
    /// <param name="parameter">Operation parameter.</param>
    /// <returns>Operation result.</returns>
    public delegate object Operation(object parameter);

    /// <summary>
    /// Signature of a method that returns a ToolStripRenderer instance.
    /// </summary>
    public delegate ToolStripRenderer GetToolStripRenderer();
    #endregion
    
    /// <summary>
	/// Set of common helper routines for the Toolkit
	/// </summary>
    public static class CommonHelper
    {
        #region Static Fields
        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12;
        
        private static readonly char[] _singleDateFormat = new char[] { 'd', 'f', 'F', 'g', 'h', 'H', 'K', 'm', 'M', 's', 't', 'y', 'z' };
        private static readonly Padding _inheritPadding = new Padding(-1);
        private static readonly int[] _daysInMonth = new int[12] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        private static readonly ColorMatrix _matrixDisabled = new ColorMatrix(new float[][]{new float[]{0.3f,0.3f,0.3f,0,0},
														                                    new float[]{0.59f,0.59f,0.59f,0,0},
														                                    new float[]{0.11f,0.11f,0.11f,0,0},
														                                    new float[]{0,0,0,0.5f,0},
														                                    new float[]{0,0,0,0,1}});

        private static int _nextId = 1000;
        private static DateTime _baseDate = new DateTime(2000, 1, 1);
        private static PropertyInfo _cachedShortcutPI;
        private static PropertyInfo _cachedDesignModePI;
        private static MethodInfo _cachedShortcutMI;
        private static NullContentValues _nullContentValues;
        private static Point _nullPoint = new Point(Int32.MaxValue, Int32.MaxValue);
        private static Rectangle _nullRectangle = new Rectangle(Int32.MaxValue, Int32.MaxValue, 0, 0);
        private static DoubleConverter _dc = new DoubleConverter();
        private static SizeConverter _sc = new SizeConverter();
        private static PointConverter _pc = new PointConverter();
        private static BooleanConverter _bc = new BooleanConverter();
        private static ColorConverter _cc = new ColorConverter();
        private static Form _activeFloatingWindow;
        #endregion

        #region Public Static
        /// <summary>
        /// Gets access to the global null point value.
        /// </summary>
        public static Point NullPoint
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _nullPoint; }
        }

        /// <summary>
        /// Gets access to the global null rectangle value.
        /// </summary>
        public static Rectangle NullRectangle
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _nullRectangle; }
        }

        /// <summary>
        /// Color matrix used to adjust colors to look disabled.
        /// </summary>
        public static ColorMatrix MatrixDisabled
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _matrixDisabled; }
        }

        /// <summary>
        /// Gets the next global identifier in sequence.
        /// </summary>
        public static int NextId
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _nextId++; }
        }

        /// <summary>
        /// Gets a string that is guaranteed to be unique.
        /// </summary>
        public static string UniqueString
        {
            get
            {
                // Generate a GUID that is guaranteed to be unique
                PI.GUIDSTRUCT newGUID = new PI.GUIDSTRUCT();
                PI.CoCreateGuid(ref newGUID);

                // Return as a hex formatted string.
                return string.Format("{0:X4}{1:X4}{2:X4}{3:X4}{4:X4}{5:X4}{6:X4}{7:X4}",
                                     newGUID.Data1, newGUID.Data2, newGUID.Data3, newGUID.Data4,
                                     newGUID.Data5, newGUID.Data6, newGUID.Data7, newGUID.Data8);
                                     
            }
        }

        /// <summary>
        /// Gets the padding value used when inheritance is needed.
        /// </summary>
        public static Padding InheritPadding
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _inheritPadding; }
        }

        /// <summary>
        /// Check a short cut menu for a matching short and invoke that item if found.
        /// </summary>
        /// <param name="cms">ContextMenuStrip instance to check.</param>
        /// <param name="msg">Windows message that generated check.</param>
        /// <param name="keyData">Keyboard shortcut to check.</param>
        /// <returns>True if shortcut processed; otherwise false.</returns>
        public static bool CheckContextMenuForShortcut(ContextMenuStrip cms, 
                                                       ref Message msg, 
                                                       Keys keyData)
        {
            if (cms != null)
            {
                // Cache the info needed to sneak access to the context menu strip
                if (_cachedShortcutPI == null)
                {
                    _cachedShortcutPI = typeof(ToolStrip).GetProperty("Shortcuts",
                                                                      BindingFlags.Instance |
                                                                      BindingFlags.GetProperty |
                                                                      BindingFlags.NonPublic);

                    _cachedShortcutMI = typeof(ToolStripMenuItem).GetMethod("ProcessCmdKey",
                                                                            BindingFlags.Instance |
                                                                            BindingFlags.NonPublic);
                }

                // Get any menu item from context strip that matches the shortcut key combination
                Hashtable shortcuts = (Hashtable)_cachedShortcutPI.GetValue(cms, null);
                ToolStripMenuItem menuItem = (ToolStripMenuItem)shortcuts[keyData];

                // If we found a match...
                if (menuItem != null)
                {
                    // Get the menu item to process the shortcut
                    object ret = _cachedShortcutMI.Invoke(menuItem, new object[] { msg, keyData });

                    // Return the 'ProcessCmdKey' result
                    if (ret != null)
                        return (bool)ret;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets reference to a null implementation of the IContentValues interface.
        /// </summary>
        public static IContentValues NullContentValues
        {
            get
            {
                // Only create the instance when it is first needed
                if (_nullContentValues == null)
                    _nullContentValues = new NullContentValues();

                return _nullContentValues;
            }
        }

        /// <summary>
        /// Return the provided size with orientation specific padding applied.
        /// </summary>
        /// <param name="orientation">Orientation to apply padding with.</param>
        /// <param name="size">Starting size.</param>
        /// <param name="padding">Padding to be applied.</param>
        /// <returns>Updated size.</returns>
        public static Size ApplyPadding(Orientation orientation, Size size, Padding padding)
        {
            // Ignore an empty padding value
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                // The orientation determines how the border padding is 
                // applied to the preferred size of the children
                switch (orientation)
                {
                    case Orientation.Vertical:
                        size.Width += padding.Vertical;
                        size.Height += padding.Horizontal;
                        break;
                    case Orientation.Horizontal:
                        size.Width += padding.Horizontal;
                        size.Height += padding.Vertical;
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }
            }

            return size;
        }

        /// <summary>
        /// Return the provided size with visual orientation specific padding applied.
        /// </summary>
        /// <param name="orientation">Orientation to apply padding with.</param>
        /// <param name="size">Starting size.</param>
        /// <param name="padding">Padding to be applied.</param>
        /// <returns>Updated size.</returns>
        public static Size ApplyPadding(VisualOrientation orientation,
                                        Size size, 
                                        Padding padding)
        {
            // Ignore an empty padding value
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                // The orientation determines how the border padding is 
                // applied to the preferred size of the children
                switch (orientation)
                {
                    case VisualOrientation.Top:
                    case VisualOrientation.Bottom:
                        size.Width += padding.Horizontal;
                        size.Height += padding.Vertical;
                        break;
                    case VisualOrientation.Left:
                    case VisualOrientation.Right:
                        size.Width += padding.Vertical;
                        size.Height += padding.Horizontal;
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }
            }

            return size;
        }

        /// <summary>
        /// Return the provided rectangle with orientation specific padding applied.
        /// </summary>
        /// <param name="orientation">Orientation to apply padding with.</param>
        /// <param name="rect">Starting rectangle.</param>
        /// <param name="padding">Padding to be applied.</param>
        /// <returns>Updated rectangle.</returns>
        public static Rectangle ApplyPadding(Orientation orientation,
                                             Rectangle rect, 
                                             Padding padding)
        {
            // Ignore an empty padding value
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                // The orientation determines how the border padding is 
                // applied to the preferred size of the children
                switch (orientation)
                {
                    case Orientation.Horizontal:
                        rect.X += padding.Left;
                        rect.Width -= padding.Horizontal;
                        rect.Y += padding.Top;
                        rect.Height -= padding.Vertical;
                        break;
                    case Orientation.Vertical:
                        rect.X += padding.Top;
                        rect.Width -= padding.Vertical;
                        rect.Y += padding.Right;
                        rect.Height -= padding.Horizontal;
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }
            }

            return rect;
        }

        /// <summary>
        /// Return the provided rectangle with visual orientation specific padding applied.
        /// </summary>
        /// <param name="orientation">Orientation to apply padding with.</param>
        /// <param name="rect">Starting rectangle.</param>
        /// <param name="padding">Padding to be applied.</param>
        /// <returns>Updated rectangle.</returns>
        public static Rectangle ApplyPadding(VisualOrientation orientation,
                                             Rectangle rect, 
                                             Padding padding)
        {
            // Ignore an empty padding value
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                // The orientation determines how the border padding is 
                // used to reduce the space available for children
                switch (orientation)
                {
                    case VisualOrientation.Top:
                        rect = new Rectangle(rect.X + padding.Left, rect.Y + padding.Top,
                                             rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                        break;
                    case VisualOrientation.Bottom:
                        rect = new Rectangle(rect.X + padding.Right, rect.Y + padding.Bottom,
                                             rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                        break;
                    case VisualOrientation.Left:
                        rect = new Rectangle(rect.X + padding.Top, rect.Y + padding.Right,
                                             rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                        break;
                    case VisualOrientation.Right:
                        rect = new Rectangle(rect.X + padding.Bottom, rect.Y + padding.Left,
                                             rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }
            }

            return rect;
        }

        /// <summary>
        /// Modify the incoming padding to reflect the visual orientation.
        /// </summary>
        /// <param name="orientation">Orientation to apply to padding.</param>
        /// <param name="padding">Padding to be modified.</param>
        /// <returns>Updated padding.</returns>
        public static Padding OrientatePadding(VisualOrientation orientation,
                                               Padding padding)
        {
            switch (orientation)
            {
                case VisualOrientation.Top:
                    return padding;
                case VisualOrientation.Bottom:
                    return new Padding(padding.Right, padding.Bottom, padding.Left, padding.Top);
                case VisualOrientation.Left:
                    return new Padding(padding.Top, padding.Right, padding.Bottom, padding.Left);
                case VisualOrientation.Right:
                    return new Padding(padding.Bottom, padding.Left, padding.Top, padding.Right);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return padding;
            }
        }

        /// <summary>
        /// Swap the width and height values for the rectangle.
        /// </summary>
        /// <param name="rect">Rectangle to modify.</param>
        [System.Diagnostics.DebuggerStepThrough]
        public static void SwapRectangleSizes(ref Rectangle rect)
        {
            int temp = rect.Width;
            rect.Width = rect.Height;
            rect.Height = temp;
        }

        /// <summary>
        /// Gets the form level right to left setting.
        /// </summary>
        /// <param name="control">Control for which the setting is needed.</param>
        /// <returns>RightToLeft setting.</returns>
        public static bool GetRightToLeftLayout(Control control)
        {
            // Default to left-to-right layout
            bool rtl = false;

            // We need a valid control to find a top level form
            if (control != null)
            {
                // Search for a top level form associated with the control
                Form topForm = control.FindForm();

                // If can find an owning form
                if (topForm != null)
                {
                    // Use the form setting instead
                    rtl = topForm.RightToLeftLayout;
                }
            }

            return rtl;
        }

        /// <summary>
        /// Decide if the context menu strip should be displayed.
        /// </summary>
        /// <param name="cms">Reference to context menu strip.</param>
        /// <returns>True to display; otherwise false.</returns>
        public static bool ValidContextMenuStrip(ContextMenuStrip cms)
        {
            // Must be a valid reference to examine
            return ((cms != null) && (cms.Items.Count > 0));
        }

        /// <summary>
        /// Decide if the KryptonContextMenu should be displayed.
        /// </summary>
        /// <param name="kcm">Reference to context menu strip.</param>
        /// <returns>True to display; otherwise false.</returns>
        public static bool ValidKryptonContextMenu(KryptonContextMenu kcm)
        {
            // Must be a valid reference to examine
            return ((kcm != null) && (kcm.Items.Count > 0));
        }

        /// <summary>
        /// Perform operation in a worker thread with wait dialog in main thread.
        /// </summary>
        /// <param name="op">Delegate of operation to be performed.</param>
        /// <param name="parameter">Parameter to be passed into the operation.</param>
        /// <returns>Result of performing the operation.</returns>
        public static object PerformOperation(Operation op, object parameter)
        {
            // Create a modal window for showing feedback
            using (ModalWaitDialog wait = new ModalWaitDialog())
            {
                // Create the object that runs the operation in a separate thread
                OperationThread opThread = new OperationThread(op, parameter);

                // Create the actual thread and provide thread entry point
                Thread thread = new Thread(new ThreadStart(opThread.Run));

                // Kick off the thread action
                thread.Start();

                // Keep looping until the thread is finished
                while (opThread.State == 0)
                {
                    // Sleep to allow thread to perform more work
                    Thread.Sleep(25);

                    // Give the feedback dialog a chance to update
                    wait.UpdateDialog();
                }

                // Process operation result
                switch (opThread.State)
                {
                    case 1:
                        return opThread.Result;
                    case 2:
                        throw opThread.Exception;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if the provided value is an override state.
        /// </summary>
        /// <param name="state">Specific state.</param>
        /// <returns>True if an override state; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool IsOverrideState(PaletteState state)
        {
            return (state & PaletteState.Override) == PaletteState.Override;
        }

        /// <summary>
        /// Gets a value indicating if the provided value is an override state but excludes one value.
        /// </summary>
        /// <param name="state">Specific state.</param>
        /// <param name="exclude">State that should be excluded from test.</param>
        /// <returns>True if an override state; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool IsOverrideStateExclude(PaletteState state, PaletteState exclude)
        {
            return (state != exclude) && IsOverrideState(state);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration specifies no borders.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if no border specified; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasNoBorders(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.All) == PaletteDrawBorders.None);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration specifies at least one border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if at least one border specified; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasABorder(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.All) != PaletteDrawBorders.None);
        }     

        /// <summary>
        /// Gets a value indicating if the enumeration specifies at least one border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if at least one border specified; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasOneBorder(PaletteDrawBorders borders)
        {
            PaletteDrawBorders justBorders = (borders & PaletteDrawBorders.All);

            // If borders value equals just one of the edges
            return (justBorders == PaletteDrawBorders.Top) ||
                   (justBorders == PaletteDrawBorders.Bottom) ||
                   (justBorders == PaletteDrawBorders.Left) ||
                   (justBorders == PaletteDrawBorders.Right);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration includes the top border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if includes the top border; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasTopBorder(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.Top) == PaletteDrawBorders.Top);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration includes the bottom border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if includes the bottom border; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasBottomBorder(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration includes the left border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if includes the left border; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasLeftBorder(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.Left) == PaletteDrawBorders.Left);
        }

        /// <summary>
        /// Gets a value indicating if the enumeration includes the right border.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if includes the right border; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasRightBorder(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.Right) == PaletteDrawBorders.Right);
        }
        
        /// <summary>
        /// Gets a value indicating if the enumeration specifies all four borders.
        /// </summary>
        /// <param name="borders">Enumeration for borders.</param>
        /// <returns>True if all four borders specified; otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static bool HasAllBorders(PaletteDrawBorders borders)
        {
            return ((borders & PaletteDrawBorders.All) == PaletteDrawBorders.All);
        }

        /// <summary>
        /// Apply an orientation to the draw border edges to get a correct value.
        /// </summary>
        /// <param name="borders">Border edges to be drawn.</param>
        /// <param name="orientation">How to adjsut the border edges.</param>
        /// <returns>Border edges adjusted for orientation.</returns>
        public static PaletteDrawBorders OrientateDrawBorders(PaletteDrawBorders borders,
                                                              VisualOrientation orientation)
        {
            // No need to perform an change for top orientation
            if (orientation == VisualOrientation.Top)
                return borders;

            // No need to change the All or None values
            if ((borders == PaletteDrawBorders.All) || (borders == PaletteDrawBorders.None))
                return borders;

            PaletteDrawBorders ret = PaletteDrawBorders.None;

            // Apply orientation change to each side in turn
            switch (orientation)
            {
                case VisualOrientation.Bottom:
                    // Invert sides
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Top;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Left;
                    break;
                case VisualOrientation.Left:
                    // Rotate one anti-clockwise
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Left;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Top;
                    break;
                case VisualOrientation.Right:
                    // Rotate sides one clockwise
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Left;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Top;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Apply a reversed orientation so that when orientated again it comes out with the original value.
        /// </summary>
        /// <param name="borders">Border edges to be drawn.</param>
        /// <param name="orientation">How to adjsut the border edges.</param>
        /// <returns>Border edges adjusted for orientation.</returns>
        public static PaletteDrawBorders ReverseOrientateDrawBorders(PaletteDrawBorders borders,
                                                                     VisualOrientation orientation)
        {
            // No need to perform an change for top orientation
            if (orientation == VisualOrientation.Top)
                return borders;

            // No need to change the All or None values
            if ((borders == PaletteDrawBorders.All) || (borders == PaletteDrawBorders.None))
                return borders;

            PaletteDrawBorders ret = PaletteDrawBorders.None;

            // Apply orientation change to each side in turn
            switch (orientation)
            {
                case VisualOrientation.Bottom:
                    // Invert sides
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Top;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Left;
                    break;
                case VisualOrientation.Right:
                    // Rotate one anti-clockwise
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Left;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Top;
                    break;
                case VisualOrientation.Left:
                    // Rotate sides one clockwise
                    if (CommonHelper.HasTopBorder(borders)) ret |= PaletteDrawBorders.Right;
                    if (CommonHelper.HasBottomBorder(borders)) ret |= PaletteDrawBorders.Left;
                    if (CommonHelper.HasLeftBorder(borders)) ret |= PaletteDrawBorders.Top;
                    if (CommonHelper.HasRightBorder(borders)) ret |= PaletteDrawBorders.Bottom;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Convert from VisualOrientation to Orientation.
        /// </summary>
        /// <param name="orientation">VisualOrientation value.</param>
        /// <returns>Orientation value.</returns>
        public static Orientation VisualToOrientation(VisualOrientation orientation)
        {
            switch (orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    return Orientation.Vertical;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    return Orientation.Horizontal;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return Orientation.Vertical;
            }
        }

        /// <summary>
        /// Convert from ButtonStyle to PaletteButtonStyle.
        /// </summary>
        /// <param name="style">ButtonStyle to convert.</param>
        /// <returns>PaletteButtonStyle enumeration value.</returns>
        public static PaletteButtonStyle ButtonStyleToPalette(ButtonStyle style)
        {
            switch (style)
            {
                case ButtonStyle.Standalone:
                    return PaletteButtonStyle.Standalone;
                case ButtonStyle.Alternate:
                    return PaletteButtonStyle.Alternate;
                case ButtonStyle.LowProfile:
                    return PaletteButtonStyle.LowProfile;
                case ButtonStyle.ButtonSpec:
                    return PaletteButtonStyle.ButtonSpec;
                case ButtonStyle.BreadCrumb:
                    return PaletteButtonStyle.BreadCrumb;
                case ButtonStyle.Cluster:
                    return PaletteButtonStyle.Cluster;
                case ButtonStyle.NavigatorStack:
                    return PaletteButtonStyle.NavigatorStack;
                case ButtonStyle.NavigatorOverflow:
                    return PaletteButtonStyle.NavigatorOverflow;
                case ButtonStyle.NavigatorMini:
                    return PaletteButtonStyle.NavigatorMini;
                case ButtonStyle.InputControl:
                    return PaletteButtonStyle.InputControl;
                case ButtonStyle.ListItem:
                    return PaletteButtonStyle.ListItem;
                case ButtonStyle.Form:
                    return PaletteButtonStyle.Form;
                case ButtonStyle.FormClose:
                    return PaletteButtonStyle.FormClose;
                case ButtonStyle.Command:
                    return PaletteButtonStyle.Command;
                case ButtonStyle.Custom1:
                    return PaletteButtonStyle.Custom1;
                case ButtonStyle.Custom2:
                    return PaletteButtonStyle.Custom2;
                case ButtonStyle.Custom3:
                    return PaletteButtonStyle.Custom3;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteButtonStyle.Standalone;
            }
        }

        /// <summary>
        /// Create a graphics path that describes a rounded rectangle.
        /// </summary>
        /// <param name="rect">Rectangle to become rounded.</param>
        /// <param name="rounding">The rounding factor to apply.</param>
        /// <returns>GraphicsPath instance.</returns>
        public static GraphicsPath RoundedRectanglePath(Rectangle rect,
                                                        int rounding)
        {
            GraphicsPath roundedPath = new GraphicsPath();

            // Only use a rounding that will fit inside the rect
            rounding = Math.Min(rounding, Math.Min(rect.Width / 2, rect.Height / 2) - rounding);

            // If there is no room for any rounding effect...
            if (rounding <= 0)
            {
                // Just add a simple rectangle as a quick way of adding four lines
                roundedPath.AddRectangle(rect);
            }
            else
            {
                // We create the path using a floating point rectangle
                RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                // The border is made of up a quarter of a circle arc, in each corner
                int arcLength = rounding * 2;
                roundedPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                roundedPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, 270f, 90f);
                roundedPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                roundedPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);

                // Make the last and first arc join up
                roundedPath.CloseFigure();
            }

            return roundedPath;
        }

        /// <summary>
        /// Convert the color to a black and white color.
        /// </summary>
        /// <param name="color">Base color.</param>
        /// <returns>Black and White version of color.</returns>
        public static Color ColorToBlackAndWhite(Color color)
        {
            // Use the standard percentages of RGB for the human eye bias
            int gray = (int)(((float)color.R * 0.3f) +
                             ((float)color.G * 0.59f) +
                             ((float)color.B * 0.11f));

            return Color.FromArgb(gray, gray, gray);
        }

        /// <summary>
        /// Whiten a provided color by applying per channel percentages.
        /// </summary>
        /// <param name="color1">Color.</param>
        /// <param name="percentR">Percentage of red to keep.</param>
        /// <param name="percentG">Percentage of green to keep.</param>
        /// <param name="percentB">Percentage of blue to keep.</param>
        /// <returns>Modified color.</returns>
        public static Color WhitenColor(Color color1, 
                                        float percentR,
                                        float percentG,
                                        float percentB)
        {
            // Find how much to use of each primary color
            int red = (int)(color1.R / percentR);
            int green = (int)(color1.G / percentG);
            int blue = (int)(color1.B / percentB);

            // Limit check against individual component
            if (red < 0) red = 0;
            if (red > 255) red = 255;
            if (green < 0) green = 0;
            if (green > 255) green = 255;
            if (blue < 0) blue = 0;
            if (blue > 255) blue = 255;

            // Return the whitened color
            return Color.FromArgb(color1.A, red, green, blue);
        }

        /// <summary>
        /// Blacken a provided color by applying per channel percentages.
        /// </summary>
        /// <param name="color1">Color.</param>
        /// <param name="percentR">Percentage of red to keep.</param>
        /// <param name="percentG">Percentage of green to keep.</param>
        /// <param name="percentB">Percentage of blue to keep.</param>
        /// <returns>Modified color.</returns>
        public static Color BlackenColor(Color color1,
                                         float percentR,
                                         float percentG,
                                         float percentB)
        {
            // Find how much to use of each primary color
            int red = (int)(color1.R * percentR);
            int green = (int)(color1.G * percentG);
            int blue = (int)(color1.B * percentB);

            // Limit check against individual component
            if (red < 0) red = 0;
            if (red > 255) red = 255;
            if (green < 0) green = 0;
            if (green > 255) green = 255;
            if (blue < 0) blue = 0;
            if (blue > 255) blue = 255;

            // Return the whitened color
            return Color.FromArgb(color1.A, red, green, blue);
        }

        /// <summary>
        /// Merge two colors together using relative percentages.
        /// </summary>
        /// <param name="color1">First color.</param>
        /// <param name="percent1">Percentage of first color to use.</param>
        /// <param name="color2">Second color.</param>
        /// <param name="percent2">Percentage of second color to use.</param>
        /// <returns>Merged color.</returns>
        public static Color MergeColors(Color color1, float percent1,
                                        Color color2, float percent2)
        {
            // Use existing three color merge
            return MergeColors(color1, percent1, color2, percent2, Color.Empty, 0f);
        }

        /// <summary>
        /// Merge three colors together using relative percentages.
        /// </summary>
        /// <param name="color1">First color.</param>
        /// <param name="percent1">Percentage of first color to use.</param>
        /// <param name="color2">Second color.</param>
        /// <param name="percent2">Percentage of second color to use.</param>
        /// <param name="color3">Third color.</param>
        /// <param name="percent3">Percentage of third color to use.</param>
        /// <returns>Merged color.</returns>
        public static Color MergeColors(Color color1, float percent1,
                                        Color color2, float percent2,
                                        Color color3, float percent3)
        {
            // Find how much to use of each primary color
            int red = (int)((color1.R * percent1) + (color2.R * percent2) + (color3.R * percent3));
            int green = (int)((color1.G * percent1) + (color2.G * percent2) + (color3.G * percent3));
            int blue = (int)((color1.B * percent1) + (color2.B * percent2) + (color3.B * percent3));

            // Limit check against individual component
            if (red < 0) red = 0;
            if (red > 255) red = 255;
            if (green < 0) green = 0;
            if (green > 255) green = 255;
            if (blue < 0) blue = 0;
            if (blue > 255) blue = 255;

            // Return the merged color
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Get the number of bits used to define the color depth of the display.
        /// </summary>
        /// <returns>Number of bits in color depth.</returns>
        public static int ColorDepth()
        {
            // Get access to the desktop DC
            IntPtr desktopDC = PI.GetDC(IntPtr.Zero);

            // Find raw values that define the color depth
            int planes = PI.GetDeviceCaps(desktopDC, (int)PI.PLANES);
            int bitsPerPixel = PI.GetDeviceCaps(desktopDC, (int)PI.BITSPIXEL);

            // Must remember to release it!
            PI.ReleaseDC(IntPtr.Zero, desktopDC);

            return planes * bitsPerPixel;
        }

        /// <summary>
        /// Gets a value indicating if the SHIFT key is pressed.
        /// </summary>
        public static bool IsShiftKeyPressed
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return ((int)(PI.GetKeyState(VK_SHIFT) & 0x00008000) != 0); }
        }

        /// <summary>
        /// Gets a value indicating if the CTRL key is pressed.
        /// </summary>
        public static bool IsCtrlKeyPressed
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return ((int)(PI.GetKeyState(VK_CONTROL) & 0x00008000) != 0); }
        }

        /// <summary>
        /// Gets a value indicating if the ALT key is pressed.
        /// </summary>
        public static bool IsAltKeyPressed
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return ((int)(PI.GetKeyState(VK_MENU) & 0x00008000) != 0); }
        }

        /// <summary>
        /// Search the hierarchy of the provided control looking for one that has the focus.
        /// </summary>
        /// <param name="control">Top of the hierarchy to search.</param>
        /// <returns>Control with focus; otherwise null.</returns>
        public static Control GetControlWithFocus(Control control)
        {
            // Does the provided control have the focus?
            if (control.Focused && !(control is IContainedInputControl))
                return control;
            else
            {
                // Check each child hierarchy in turn
                foreach (Control child in control.Controls)
                    if (child.ContainsFocus)
                        return GetControlWithFocus(child);

                return null;
            }
        }

        /// <summary>
        /// Add the provided control to a parent collection.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        /// <param name="c">Control to be added.</param>
        public static void AddControlToParent(Control parent, Control c)
        {
            Debug.Assert(parent != null);
            Debug.Assert(c != null);

            // If the control is already inside a control collection, then remove it
            if (c.Parent != null)
                RemoveControlFromParent(c);

            // If the control collection is one of our internal collections...
            if (parent.Controls is KryptonControlCollection)
            {
                // Then must use the internal method for adding a new instance
                KryptonControlCollection cc = (KryptonControlCollection)parent.Controls;
                cc.AddInternal(c);
            }
            else
            {
                // Inside a standard collection, add it the usual way
                parent.Controls.Add(c);
            }
        }

        /// <summary>
        /// Remove the provided control from its parent collection.
        /// </summary>
        /// <param name="c">Control to be removed.</param>
        public static void RemoveControlFromParent(Control c)
        {
            Debug.Assert(c != null);

            // If the control is inside a parent collection
            if (c.Parent != null)
            {
                // If the control collection is one of our internal collections...
                if (c.Parent.Controls is KryptonControlCollection)
                {
                    // Then must use the internal method for adding a new instance
                    KryptonControlCollection cc = (KryptonControlCollection)c.Parent.Controls;
                    cc.RemoveInternal(c);
                }
                else
                {
                    // Inside a standard collection, remove it the usual way
                    c.Parent.Controls.Remove(c);
                }
            }
        }

        /// <summary>
        /// Gets the size of the borders requested by the real window.
        /// </summary>
        /// <param name="cp">Window style parameters.</param>
        /// <returns>Border sizing.</returns>
        public static Padding GetWindowBorders(CreateParams cp)
        {
            PI.RECT rect = new PI.RECT();

            // Start with a zero sized rectangle
            rect.left = 0;
            rect.right = 0;
            rect.top = 0;
            rect.bottom = 0;

            // Adjust rectangle to add on the borders required
            PI.AdjustWindowRectEx(ref rect, cp.Style, false, cp.ExStyle);

            // Return the per side border values
            return new Padding(-rect.left, -rect.top, rect.right, rect.bottom);
        }

        /// <summary>
        /// Discover if the provided Form is currently minimized.
        /// </summary>
        /// <param name="f">Form reference.</param>
        /// <returns>True if minimized; otherwise false.</returns>
        public static bool IsFormMinimized(Form f)
        {
            // Get the current window style (cannot use the 
            // WindowState property as it can be slightly out of date)
            uint style = PI.GetWindowLong(f.Handle, PI.GWL_STYLE);

            return ((style &= PI.WS_MINIMIZE) != 0);
        }

        /// <summary>
        /// Discover if the provided Form is currently maximized.
        /// </summary>
        /// <param name="f">Form reference.</param>
        /// <returns>True if maximized; otherwise false.</returns>
        public static bool IsFormMaximized(Form f)
        {
            // Get the current window style (cannot use the 
            // WindowState property as it can be slightly out of date)
            uint style = PI.GetWindowLong(f.Handle, PI.GWL_STYLE);

            return ((style &= PI.WS_MAXIMIZE) != 0);
        }


        /// <summary>
        /// Gets the real client rectangle of the list.
        /// </summary>
        /// <param name="handle">Window handle of the control.</param>
        public static Rectangle RealClientRectangle(IntPtr handle)
        {
            // Grab the actual current size of the window, this is more accurate than using
            // the 'this.Size' which is out of date when performing a resize of the window.
            PI.RECT windowRect = new PI.RECT();
            PI.GetWindowRect(handle, ref windowRect);

            // Create rectangle that encloses the entire window
            return new Rectangle(0, 0,
                                 windowRect.right - windowRect.left,
                                 windowRect.bottom - windowRect.top);
        }


        /// <summary>
        /// Find the appropriate content style to match the incoming label style.
        /// </summary>
        /// <param name="style">LabelStyle enumeration.</param>
        /// <returns>Matching PaletteContentStyle enumeration value.</returns>
        public static PaletteContentStyle ContentStyleFromLabelStyle(LabelStyle style)
        {
            switch (style)
            {
                case LabelStyle.NormalControl:
                    return PaletteContentStyle.LabelNormalControl;
                case LabelStyle.BoldControl:
                    return PaletteContentStyle.LabelBoldControl;
                case LabelStyle.ItalicControl:
                    return PaletteContentStyle.LabelItalicControl;
                case LabelStyle.TitleControl:
                    return PaletteContentStyle.LabelTitleControl;
                case LabelStyle.NormalPanel:
                    return PaletteContentStyle.LabelNormalPanel;
                case LabelStyle.BoldPanel:
                    return PaletteContentStyle.LabelBoldPanel;
                case LabelStyle.ItalicPanel:
                    return PaletteContentStyle.LabelItalicPanel;
                case LabelStyle.TitlePanel:
                    return PaletteContentStyle.LabelTitlePanel;
                case LabelStyle.GroupBoxCaption:
                    return PaletteContentStyle.LabelGroupBoxCaption;
                case LabelStyle.ToolTip:
                    return PaletteContentStyle.LabelToolTip;
                case LabelStyle.SuperTip:
                    return PaletteContentStyle.LabelSuperTip;
                case LabelStyle.KeyTip:
                    return PaletteContentStyle.LabelKeyTip;
                case LabelStyle.Custom1:
                    return PaletteContentStyle.LabelCustom1;
                case LabelStyle.Custom2:
                    return PaletteContentStyle.LabelCustom2;
                case LabelStyle.Custom3:
                    return PaletteContentStyle.LabelCustom3;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteContentStyle.LabelNormalPanel;
            }
        }

        /// <summary>
        /// Convert from palette rendering hint to actual rendering hint.
        /// </summary>
        /// <param name="hint">Palette rendering hint.</param>
        /// <returns>Converted value for use with a Graphics instance.</returns>
        public static TextRenderingHint PaletteTextHintToRenderingHint(PaletteTextHint hint)
        {
            switch (hint)
            {
                case PaletteTextHint.AntiAlias:
                    return TextRenderingHint.AntiAlias;
                case PaletteTextHint.AntiAliasGridFit:
                    return TextRenderingHint.AntiAliasGridFit;
                case PaletteTextHint.ClearTypeGridFit:
                    return TextRenderingHint.ClearTypeGridFit;
                case PaletteTextHint.SingleBitPerPixel:
                    return TextRenderingHint.SingleBitPerPixel;
                case PaletteTextHint.SingleBitPerPixelGridFit:
                    return TextRenderingHint.SingleBitPerPixelGridFit;
                case PaletteTextHint.SystemDefault:
                    return TextRenderingHint.SystemDefault;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return TextRenderingHint.SystemDefault;
            }
        }

        /// <summary>
        /// Get the correct metric padding for the provided separator style.
        /// </summary>
        /// <param name="separatorStyle">Separator style.</param>
        /// <returns>Matching metric padding.</returns>
        public static PaletteMetricPadding SeparatorStyleToMetricPadding(SeparatorStyle separatorStyle)
        {
            switch (separatorStyle)
            {
                case SeparatorStyle.LowProfile:
                    return PaletteMetricPadding.SeparatorPaddingLowProfile;
                case SeparatorStyle.HighProfile:
                    return PaletteMetricPadding.SeparatorPaddingHighProfile;
                case SeparatorStyle.HighInternalProfile:
                    return PaletteMetricPadding.SeparatorPaddingHighInternalProfile;
                case SeparatorStyle.Custom1:
                    return PaletteMetricPadding.SeparatorPaddingCustom1;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return PaletteMetricPadding.SeparatorPaddingLowProfile;
            }
        }

        /// <summary>
        /// Ensure that a single character format string is treated as a custom format.
        /// </summary>
        /// <param name="format">Incoming format.</param>
        /// <returns>Corrected format.</returns>
        public static string MakeCustomDateFormat(string format)
        {
            // Is this a single charater format?
            if (format.Length == 1)
            {
                // If the character is one of the predefined entries...
                if (format.IndexOfAny(_singleDateFormat) == 0)
                {
                    // Insert the percentage sign so it is a custom format and not a predefined one
                    format = "%" + format;
                }
            }

            return format;
        }

        /// <summary>
        /// Create new instance of specified type within the designer host, if provided.
        /// </summary>
        /// <param name="itemType">Type of the item to create.</param>
        /// <param name="host">Designer host used if provided.</param>
        /// <returns>Reference to new instance.</returns>
        public static object CreateInstance(Type itemType, IDesignerHost host)
        {
            object retObj = null;

            // Cannot use the designer host to create component unless the type implements IComponent
            if (typeof(IComponent).IsAssignableFrom(itemType) && (host != null))
            {
                // Ask host to create component for us
                retObj = host.CreateComponent(itemType, null);

                // If the new object has an associated designer then use that now to initialize the instance
                IComponentInitializer designer = host.GetDesigner((IComponent)retObj) as IComponentInitializer;
                if (designer != null)
                    designer.InitializeNewComponent(null);
            }
            else
            {
                // Cannot use host for creation, so do it the standard way instead
                retObj = TypeDescriptor.CreateInstance(host, itemType, null, null);
            }

            return retObj;
        }

        /// <summary>
        /// Destroy instance of an object using the provided designer host.
        /// </summary>
        /// <param name="instance">Reference to item for destroying.</param>
        /// <param name="host">Designer host used if provided.</param>
        public static void DestroyInstance(object instance, IDesignerHost host)
        {
            IComponent component = instance as IComponent;
            if (component != null)
            {
                // Use designer to remove component if possible
                if (host != null)
                    host.DestroyComponent(component);
                else
                    component.Dispose();
            }
            else
            {
                // Fallback to calling any exposed dispose method
                IDisposable disposable = instance as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        /// <summary>
        /// Output some debug data to a log file that exists in same directory as the application.
        /// </summary>
        /// <param name="str">String to output.</param>
        public static void LogOutput(string str)
        {
            FileInfo fi = new FileInfo(Application.ExecutablePath);
            using (StreamWriter writer = new StreamWriter(fi.DirectoryName + "LogOutput.txt", true, Encoding.ASCII))
            {
                writer.Write(DateTime.Now.ToLongTimeString() + " :  ");
                writer.WriteLine(str);
                writer.Flush();
            }
        }

        /// <summary>
        /// Discover if the component is in design mode.
        /// </summary>
        /// <param name="c">Component to test.</param>
        /// <returns>True if in design mode; otherwise false.</returns>
        public static bool DesignMode(Component c)
        {
            // Cache the info needed to sneak access to the component protected property
            if (_cachedDesignModePI == null)
            {
                _cachedDesignModePI = typeof(ToolStrip).GetProperty("DesignMode",
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.GetProperty |
                                                                    BindingFlags.NonPublic);
            }

            return (bool)_cachedDesignModePI.GetValue(c, null);
        }

        /// <summary>
        /// Convert a double to a culture invariant string value.
        /// </summary>
        /// <param name="d">Double to convert.</param>
        /// <returns>Culture invariant string representation.</returns>
        public static string DoubleToString(double d)
        {
            return _dc.ConvertToInvariantString(d);
        }

        /// <summary>
        /// Convert a culture invariant string value to a double.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <returns>Double value.</returns>
        public static double StringToDouble(string s)
        {
            return (double)_dc.ConvertFromInvariantString(s);
        }

        /// <summary>
        /// Convert a Size to a culture invariant string value.
        /// </summary>
        /// <param name="s">Size to convert.</param>
        /// <returns>Culture invariant string representation.</returns>
        public static string SizeToString(Size s)
        {
            return _sc.ConvertToInvariantString(s);
        }

        /// <summary>
        /// Convert a culture invariant string value to a Size.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <returns>Size value.</returns>
        public static Size StringToSize(string s)
        {
            return (Size)_sc.ConvertFromInvariantString(s);
        }

        /// <summary>
        /// Convert a Point to a culture invariant string value.
        /// </summary>
        /// <param name="s">Size to convert.</param>
        /// <returns>Culture invariant string representation.</returns>
        public static string PointToString(Point s)
        {
            return _pc.ConvertToInvariantString(s);
        }

        /// <summary>
        /// Convert a culture invariant string value to a Point.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <returns>Point value.</returns>
        public static Point StringToPoint(string s)
        {
            return (Point)_pc.ConvertFromInvariantString(s);
        }

        /// <summary>
        /// Convert a Boolean to a culture invariant string value.
        /// </summary>
        /// <param name="b">Boolean to convert.</param>
        /// <returns>Culture invariant string representation.</returns>
        public static string BoolToString(bool b)
        {
            return _bc.ConvertToInvariantString(b);
        }

        /// <summary>
        /// Convert a culture invariant string value to a Boolean.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <returns>Boolean value.</returns>
        public static bool StringToBool(string s)
        {
            return (bool)_bc.ConvertFromInvariantString(s);
        }

        /// <summary>
        /// Convert a Color to a culture invariant string value.
        /// </summary>
        /// <param name="c">Color to convert.</param>
        /// <returns>Culture invariant string representation.</returns>
        public static string ColorToString(Color c)
        {
            return _cc.ConvertToInvariantString(c);
        }

        /// <summary>
        /// Convert a culture invariant string value to a Color.
        /// </summary>
        /// <param name="s">String to convert.</param>
        /// <returns>Color value.</returns>
        public static Color StringToColor(string s)
        {
            return (Color)_cc.ConvertFromInvariantString(s);
        }

        /// <summary>
        /// Convert a client mouse position inside a windows message into a screen position.
        /// </summary>
        /// <param name="m">Window message.</param>
        /// <returns>Screen point.</returns>
        public static Point ClientMouseMessageToScreenPt(Message m)
        {
            // Extract the x and y mouse position from message
            PI.POINTC clientPt = new PI.POINTC();
            clientPt.x = PI.LOWORD((int)m.LParam);
            clientPt.y = PI.HIWORD((int)m.LParam);

            // Negative positions are in the range 32767 -> 65535, 
            // so convert to actual int values for the negative positions
            if (clientPt.x >= 32767) clientPt.x = (clientPt.x - 65536);
            if (clientPt.y >= 32767) clientPt.y = (clientPt.y - 65536);

            // Convert a 0,0 point from client to screen to find offsetting
            PI.POINTC zeroPIPt = new PI.POINTC();
            zeroPIPt.x = 0;
            zeroPIPt.y = 0;
            PI.MapWindowPoints(m.HWnd, IntPtr.Zero, zeroPIPt, 1);

            // Adjust the client coordinate by the offset to get to screen
            clientPt.x += zeroPIPt.x;
            clientPt.y += zeroPIPt.y;

            // Return as a managed point type
            return new Point(clientPt.x, clientPt.y);
        }

        /// <summary>
        /// Only persist the provided name/value pair as an Xml attribute if the value is not null or empty.
        /// </summary>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        public static void TextToXmlAttribute(XmlWriter xmlWriter,
                                              string name,
                                              string value)
        {
            TextToXmlAttribute(xmlWriter, name, value, string.Empty);
        }

        /// <summary>
        /// Only persist the provided name/value pair as an Xml attribute if the value is not null/empty and not the default.
        /// </summary>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        /// <param name="def">Default value.</param>
        public static void TextToXmlAttribute(XmlWriter xmlWriter,
                                              string name,
                                              string value,
                                              string def)
        {
            if (!string.IsNullOrEmpty(value) && (value != def))
                xmlWriter.WriteAttributeString(name, value);
        }

        /// <summary>
        /// Read the named attribute value but if no attribute is found then an empty string.
        /// </summary>
        /// <param name="xmlReader">Xml reader to load information from.</param>
        /// <param name="name">Attribute name.</param>
        /// <returns></returns>
        public static string XmlAttributeToText(XmlReader xmlReader,
                                                string name)
        {
            return XmlAttributeToText(xmlReader, name, string.Empty);
        }

        /// <summary>
        /// Read the named attribute value but if no attribute is found then return the provided default.
        /// </summary>
        /// <param name="xmlReader">Xml reader to load information from.</param>
        /// <param name="name">Attribute name.</param>
        /// <param name="def">Default value.</param>
        /// <returns></returns>
        public static string XmlAttributeToText(XmlReader xmlReader,
                                                string name,
                                                string def)
        {
            try
            {
                string ret = xmlReader.GetAttribute(name);
                
                if (ret == null)
                    ret = def;
                
                return ret;
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// Convert a Image to a culture invariant string value.
        /// </summary>
        /// <param name="xmlWriter">Xml writer to save information into.</param>
        /// <param name="name">Name of image to save.</param>
        /// <param name="image">Image to persist.</param>
        public static void ImageToXmlCData(XmlWriter xmlWriter, 
                                           string name,
                                           Image image)
        {
            // Only store if we have an actual image to persist
            if (image != null)
            {
                // Convert the Image into base64 so it can be used in xml
                MemoryStream memory = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memory, image);
                string base64 = Convert.ToBase64String(memory.ToArray());

                // Store the base64 Hex as a CDATA inside the element
                xmlWriter.WriteStartElement(name);
                xmlWriter.WriteCData(base64);
                xmlWriter.WriteEndElement();
            }
        }

        /// <summary>
        /// Convert a culture invariant string value into an Image.
        /// </summary>
        /// <param name="xmlReader">Xml reader to load information from.</param>
        /// <returns>Image that was recreated.</returns>
        public static Image XmlCDataToImage(XmlReader xmlReader)
        {
            // Convert the content of the element into base64
            byte[] bytes = Convert.FromBase64String(xmlReader.ReadContentAsString());

            // Convert the bytes back into an Image
            MemoryStream memory = new MemoryStream(bytes);
            BinaryFormatter formatter = new BinaryFormatter();
            return (Image)formatter.Deserialize(memory);
        }

        /// <summary>
        /// Gets a reference to the currently active floating window.
        /// </summary>
        public static Form ActiveFloatingWindow
        {
            get { return _activeFloatingWindow; }
            set { _activeFloatingWindow = value; }
        }
        #endregion
    }
}
