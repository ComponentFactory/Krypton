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
    internal class KryptonRichTextBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonRichTextBox _richTextBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRichTextBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonRichTextBoxActionList(KryptonRichTextBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _richTextBox = owner.Component as KryptonRichTextBox;

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
            get { return _richTextBox.PaletteMode; }
            
            set 
            {
                if (_richTextBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_richTextBox, null, _richTextBox.PaletteMode, value);
                    _richTextBox.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get { return _richTextBox.InputControlStyle; }

            set
            {
                if (_richTextBox.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_richTextBox, null, _richTextBox.InputControlStyle, value);
                    _richTextBox.InputControlStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the Multiline mode.
        /// </summary>
        public bool Multiline
        {
            get { return _richTextBox.Multiline; }

            set
            {
                if (_richTextBox.Multiline != value)
                {
                    _service.OnComponentChanged(_richTextBox, null, _richTextBox.Multiline, value);
                    _richTextBox.Multiline = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the WordWrap mode.
        /// </summary>
        public bool WordWrap
        {
            get { return _richTextBox.WordWrap; }

            set
            {
                if (_richTextBox.WordWrap != value)
                {
                    _service.OnComponentChanged(_richTextBox, null, _richTextBox.WordWrap, value);
                    _richTextBox.WordWrap = value;
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
            if (_richTextBox != null)
            {
                // Add the list of rich text box specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("InputControlStyle", "Style", "Appearance", "TextBox display style."));
                actions.Add(new DesignerActionHeaderItem("TextBox"));
                actions.Add(new DesignerActionPropertyItem("Multiline", "Multiline", "TextBox", "Should text span multiple lines."));
                actions.Add(new DesignerActionPropertyItem("WordWrap", "WordWrap", "TextBox", "Should words be wrapped over multiple lines."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
