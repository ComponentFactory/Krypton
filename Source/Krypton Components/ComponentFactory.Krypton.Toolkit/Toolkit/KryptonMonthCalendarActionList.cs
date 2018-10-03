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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonMonthCalendarActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonMonthCalendar _monthCalendar;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMonthCalendarActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonMonthCalendarActionList(KryptonMonthCalendarDesigner owner)
            : base(owner.Component)
        {
            // Remember the bread crumb control instance
            _monthCalendar = owner.Component as KryptonMonthCalendar;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _monthCalendar.PaletteMode; }
            
            set 
            {
                if (_monthCalendar.PaletteMode != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.PaletteMode, value);
                    _monthCalendar.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum selection count.
        /// </summary>
        public int MaxSelectionCount
        {
            get { return _monthCalendar.MaxSelectionCount; }
            
            set 
            {
                if (_monthCalendar.MaxSelectionCount != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.MaxSelectionCount, value);
                    _monthCalendar.MaxSelectionCount = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the today button.
        /// </summary>
        public bool ShowToday
        {
            get { return _monthCalendar.ShowToday; }

            set
            {
                if (_monthCalendar.ShowToday != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowToday, value);
                    _monthCalendar.ShowToday = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the today entry circled.
        /// </summary>
        public bool ShowTodayCircle
        {
            get { return _monthCalendar.ShowTodayCircle; }

            set
            {
                if (_monthCalendar.ShowTodayCircle != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowTodayCircle, value);
                    _monthCalendar.ShowTodayCircle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of week numbers.
        /// </summary>
        public bool ShowWeekNumbers
        {
            get { return _monthCalendar.ShowWeekNumbers; }

            set
            {
                if (_monthCalendar.ShowWeekNumbers != value)
                {
                    _service.OnComponentChanged(_monthCalendar, null, _monthCalendar.ShowWeekNumbers, value);
                    _monthCalendar.ShowWeekNumbers = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_monthCalendar != null)
            {
                // Add the list of bread crumb specific actions
                actions.Add(new DesignerActionHeaderItem("Behavior"));
                actions.Add(new DesignerActionPropertyItem("MaxSelectionCount", "MaxSelectionCount", "Behavior", "Maximum number of selected days"));
                actions.Add(new DesignerActionPropertyItem("ShowToday", "ShowToday", "Behavior", "Show the today button"));
                actions.Add(new DesignerActionPropertyItem("ShowTodayCircle", "ShowTodayCircle", "Behavior", "Show a circle around the today entry"));
                actions.Add(new DesignerActionPropertyItem("ShowWeekNumbers", "ShowWeekNumbers", "Behavior", "Show the week numbers"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
