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
    internal class KryptonLinkLabelActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonLinkLabel _linkLabel;
        private IComponentChangeService _service;
        private string _action;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLinkLabelActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonLinkLabelActionList(KryptonLinkLabelDesigner owner)
            : base(owner.Component)
        {
            // Remember the link label instance
            _linkLabel = owner.Component as KryptonLinkLabel;

            // Assuming we were correctly passed an actual component...
            if (_linkLabel != null)
            {
                // Decide on the next action to take given the current setting
                if (_linkLabel.LinkVisited)
                    _action = "Link has not been visited";
                else
                    _action = "Link has been visited";
            }

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the link label style.
        /// </summary>
        public LabelStyle LabelStyle
        {
            get { return _linkLabel.LabelStyle; }
            
            set 
            {
                if (_linkLabel.LabelStyle != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.LabelStyle, value);
                    _linkLabel.LabelStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _linkLabel.Orientation; }
            
            set
            {
                if (_linkLabel.Orientation != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.Orientation, value);
                    _linkLabel.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the link behavior.
        /// </summary>
        public KryptonLinkBehavior LinkBehavior
        {
            get { return _linkLabel.LinkBehavior; }

            set
            {
                if (_linkLabel.LinkBehavior != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.LinkBehavior, value);
                    _linkLabel.LinkBehavior = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the link visited.
        /// </summary>
        public bool LinkVisited
        {
            get { return _linkLabel.LinkVisited; }

            set
            {
                if (_linkLabel.LinkVisited != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.LinkVisited, value);
                    _linkLabel.LinkVisited = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label text.
        /// </summary>
        public string Text
        {
            get { return _linkLabel.Values.Text; }
            
            set 
            {
                if (_linkLabel.Values.Text != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.Values.Text, value);
                    _linkLabel.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra link label text.
        /// </summary>
        public string ExtraText
        {
            get { return _linkLabel.Values.ExtraText; }
            
            set 
            {
                if (_linkLabel.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.Values.ExtraText, value);
                    _linkLabel.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the link label image.
        /// </summary>
        public Image Image
        {
            get { return _linkLabel.Values.Image; }
            
            set 
            {
                if (_linkLabel.Values.Image != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.Values.Image, value);
                    _linkLabel.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _linkLabel.PaletteMode; }
            
            set 
            {
                if (_linkLabel.PaletteMode != value)
                {
                    _service.OnComponentChanged(_linkLabel, null, _linkLabel.PaletteMode, value);
                    _linkLabel.PaletteMode = value;
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
            if (_linkLabel != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("LabelStyle", "Style", "Appearance", "Label style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Visual orientation"));
                actions.Add(new DesignerActionPropertyItem("LinkBehavior", "Link Behavior", "Appearance", "Underline behavior"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, new EventHandler(OnLinkVisitedClick)), "Appearance"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Label text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Label extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Label image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion

        #region Implementation
        private void OnLinkVisitedClick(object sender, EventArgs e)
        {
            // Cast to the correct type
            DesignerVerb verb = sender as DesignerVerb;
            
            // Double check the source is the expected type
            if (verb != null)
            {
                // Invert the visited setting
                _linkLabel.LinkVisited = !_linkLabel.LinkVisited;

                // Decide on the next action to take given the new setting
                if (_linkLabel.LinkVisited)
                    _action = "Link has not been visited";
                else
                    _action = "Link has been visited";

                // Get the user interface service associated with actions
                DesignerActionUIService service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

                // If we managed to get it then request it update to reflect new action setting
                if (service != null)
                    service.Refresh(_linkLabel);
            }
        }
        #endregion   
    }
}
