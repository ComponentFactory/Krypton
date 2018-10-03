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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Set of calculated values for use when laying out view elements.
    /// </summary>
    internal class CalculatedValues
    {
        #region Static Fields
        private static readonly int FONT_HEIGHT_EXTRA = 2;          // Always need to add 2 pixels for it to draw
        private static readonly int DIALOG_MIN_HEIGHT = 14;         // Button image = 8 + 2 content gap + 2 for button border + 2 for outside gap
        private static readonly int GROUP_LINE_CONTENT_MIN = 18;    // Small image = 16 + 2 content gap
        private static readonly int GROUP_LINE_CONTENT_EXTRA = 4;   // 2 content gap + 2 for button border
        private static readonly int GROUP_INSIDE_BOTTOM_GAP = 1;    // 1 pixel between last group line and the group title
        private static readonly int GROUP_TOP_BORDER = 2;           // 2 pixel border for top edge of a group
        private static readonly int GROUPS_TOP_GAP = 3;             // Space between top of a group and inside edge of borders area
        private static readonly int GROUPS_BOTTOM_GAP = 2;          // Space between bottom of group and bottom of the borders area
        private static readonly int TABS_TOP_GAP = 5;               // 4 padding at top of tab text and 1 extra for the bottom
        private static readonly int KEYTIP_HOFFSET = 16;            // Horizontal distance to offset keytips for group items
        private static readonly int KEYTIP_VOFFSET_LINE2 = 1;       // Vertical distance to offset keytips on group line 2
        private static readonly int KEYTIP_VOFFSET_LINE4 = 8;       // Vertical distance to offset keytips on group line 4
        private static readonly int KEYTIP_VOFFSET_LINE5 = 8;       // Vertical distance to offset keytips on group line 5
        #endregion

        #region Instance Fields
        private PaletteRibbonShape _lastShape;
        private KryptonRibbon _ribbon;
        private int _rawFontHeight;
        private int _drawFontHeight;
        private int _tabHeight;
        private int _groupTitleHeight;
        private int _groupLineContentHeight;
        private int _groupLineHeight;
        private int _groupLineGapHeight;
        private int _groupTripleHeight;
        private int _groupHeight;
        private int _groupsHeight;
        private int _groupHeightModifier;
        private int _groupsHeightModifier;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the CalculatedValues class.
        /// </summary>
        /// <param name="ribbon">Source control instance.</param>
        public CalculatedValues(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
            
            _lastShape = PaletteRibbonShape.Inherit;
        }
        #endregion

        #region Recalc
        /// <summary>
        /// Recalculate all the values.
        /// </summary>
        public void Recalculate()
        {
            // Do we need to update the shape dependant values?
            if (_lastShape != _ribbon.RibbonShape)
            {
                _lastShape = _ribbon.RibbonShape;
                switch (_lastShape)
                {
                    default:
                    case PaletteRibbonShape.Office2007:
                        _groupHeightModifier = 0;
                        _groupsHeightModifier = 0;
                        break;
                    case PaletteRibbonShape.Office2010:
                        _groupHeightModifier = -3;
                        _groupsHeightModifier = -3;
                        break;
                }
            }

            // Get the font used by the ribbon
            Font font = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);

            // Cache common font height related values
            _rawFontHeight = font.Height;

            _drawFontHeight = _rawFontHeight + FONT_HEIGHT_EXTRA;

            // Height of all tabs in the tabs area
            _tabHeight = _drawFontHeight + TABS_TOP_GAP;

            // Find the height of the group title area (must be minimum size to show the dialog launcher button)
            _groupTitleHeight = Math.Max(_drawFontHeight, DIALOG_MIN_HEIGHT);

            // Get the height needed for showing the content of a group line
            _groupLineContentHeight = Math.Max(_drawFontHeight, GROUP_LINE_CONTENT_MIN);

            // Group line height must be the content plus spacing gap and then border
            _groupLineHeight = _groupLineContentHeight + GROUP_LINE_CONTENT_EXTRA;

            // Group inside height is 3 group lines plus space at bottom of the lines
            _groupTripleHeight = (_groupLineHeight * 3);

            // The gap between lines is one of the lines divide by a gap above, between and below lines
            _groupLineGapHeight = (_groupLineHeight / 3);

            // Group height is the inside plus title area at bottom and the top border
            _groupHeight = _groupTripleHeight + GROUP_INSIDE_BOTTOM_GAP + _groupTitleHeight + GROUP_TOP_BORDER;

            // Size of the groups area (not including the top pixel that is placed in the tabs
            // area is the height of a group plus the bottom and top gaps).
            _groupsHeight = _groupHeight + GROUPS_BOTTOM_GAP + GROUPS_TOP_GAP;

            // Apply shape specific modifiers
            _groupHeight += _groupHeightModifier;
            _groupsHeight += _groupsHeightModifier;
        }
        #endregion

        #region RawFontHeight
        /// <summary>
        /// Gets the raw height of the ribbon font.
        /// </summary>
        public int RawFontHeight
        {
            get { return _rawFontHeight; }
        }
        #endregion

        #region DrawFontHeight
        /// <summary>
        /// Gets the drawing height of the ribbon font.
        /// </summary>
        public int DrawFontHeight
        {
            get { return _drawFontHeight; }
        }
        #endregion

        #region TabHeight
        /// <summary>
        /// Gets the drawing height of a tab.
        /// </summary>
        public int TabHeight
        {
            get { return _tabHeight; }
        }
        #endregion

        #region GroupTitleHeight
        /// <summary>
        /// Gets the drawing height of the ribbon font.
        /// </summary>
        public int GroupTitleHeight
        {
            get { return _groupTitleHeight; }
        }
        #endregion

        #region GroupLineContentHeight
        /// <summary>
        /// Gets the drawing height of the content for a group line.
        /// </summary>
        public int GroupLineContentHeight
        {
            get { return _groupLineContentHeight; }
        }
        #endregion

        #region GroupLineHeight
        /// <summary>
        /// Gets the drawing height of one of the three group lines.
        /// </summary>
        public int GroupLineHeight
        {
            get { return _groupLineHeight; }
        }
        #endregion

        #region GroupLineGapHeight
        /// <summary>
        /// Gets the spacing height between two group lines.
        /// </summary>
        public int GroupLineGapHeight
        {
            get { return _groupLineGapHeight; }
        }
        #endregion

        #region GroupTripleHeight
        /// <summary>
        /// Gets the height of the triple height item.
        /// </summary>
        public int GroupTripleHeight
        {
            get { return _groupTripleHeight; }
        }
        #endregion

        #region GroupHeight
        /// <summary>
        /// Gets the drawing height of a group.
        /// </summary>
        public int GroupHeight
        {
            get { return _groupHeight; }
        }
        #endregion

        #region GroupsHeight
        /// <summary>
        /// Gets the drawing height of the entire groups area not including top pixel line.
        /// </summary>
        public int GroupsHeight
        {
            get { return _groupsHeight; }
        }
        #endregion

        #region KeyTipRectToPoint
        /// <summary>
        /// Find the correct screen point for a key tip given a rectangle and its group line.
        /// </summary>
        /// <param name="viewRect">Screen rectangle of the view element.</param>
        /// <param name="groupLine">Group line the view is positioned on.</param>
        /// <returns>Screen point that is the center of the key tip.</returns>
        public Point KeyTipRectToPoint(Rectangle viewRect, int groupLine)
        {
            Point screenPt;

            switch (groupLine)
            {
                case 1:
                    screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Top);
                    break;
                case 2:
                    screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, (viewRect.Top + viewRect.Height / 2) + KEYTIP_VOFFSET_LINE2);
                    break;
                case 3:
                    screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Bottom);
                    break;
                case 4:
                    screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Top - KEYTIP_VOFFSET_LINE4);
                    break;
                case 5:
                    screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Bottom + KEYTIP_VOFFSET_LINE5);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    screenPt = new Point(viewRect.X, viewRect.Y);
                    break;
            }

            return screenPt;
        }
        #endregion
    }
}
