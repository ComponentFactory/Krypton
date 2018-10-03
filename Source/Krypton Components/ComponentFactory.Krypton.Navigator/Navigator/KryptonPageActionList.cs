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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    internal class KryptonPageActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonPage _page;
        private IComponentChangeService _serviceComponentChange;
        private DesignerActionItemCollection _actions;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPageActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonPageActionList(KryptonPageDesigner owner)
            : base(owner.Component)
        {
            // Remember designer and actual component instance being designed
            _page = (KryptonPage)owner.Component;

            // Cache service used to notify when a property has changed
            _serviceComponentChange = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the page text.
        /// </summary>
        public string TextShort
        {
            get { return _page.Text; }

            set
            {
                if (!_page.Text.Equals(value))
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.Text, value);
                    _page.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the page title text.
        /// </summary>
        public string TextTitle
        {
            get { return _page.TextTitle; }

            set
            {
                if (!_page.TextTitle.Equals(value))
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.TextTitle, value);
                    _page.TextTitle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the page description text.
        /// </summary>
        public string TextDescription
        {
            get { return _page.TextDescription; }

            set
            {
                if (!_page.TextDescription.Equals(value))
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.TextDescription, value);
                    _page.TextDescription = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip title text.
        /// </summary>
        public string ToolTipTitle
        {
            get { return _page.ToolTipTitle; }

            set
            {
                if (!_page.ToolTipTitle.Equals(value))
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ToolTipTitle, value);
                    _page.ToolTipTitle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip body text.
        /// </summary>
        public string ToolTipBody
        {
            get { return _page.ToolTipBody; }

            set
            {
                if (!_page.ToolTipBody.Equals(value))
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ToolTipBody, value);
                    _page.ToolTipBody = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the page tooltip image.
        /// </summary>
        public Image ToolTipImage
        {
            get { return _page.ToolTipImage; }

            set
            {
                if (_page.ToolTipImage != value)
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ToolTipImage, value);
                    _page.ToolTipImage = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the small page image.
        /// </summary>
        public Image ImageSmall
        {
            get { return _page.ImageSmall; }

            set
            {
                if (_page.ImageSmall != value)
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ImageSmall, value);
                    _page.ImageSmall = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the medium page image.
        /// </summary>
        public Image ImageMedium
        {
            get { return _page.ImageMedium; }

            set
            {
                if (_page.ImageMedium != value)
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ImageMedium, value);
                    _page.ImageMedium = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the large page image.
        /// </summary>
        public Image ImageLarge
        {
            get { return _page.ImageLarge; }

            set
            {
                if (_page.ImageLarge != value)
                {
                    _serviceComponentChange.OnComponentChanged(_page, null, _page.ImageLarge, value);
                    _page.ImageLarge = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the large page image.
        /// </summary>
        public bool PageInOverflowBarForOutlookMode
        {
            get { return _page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode); }

            set
            {
                _serviceComponentChange.OnComponentChanged(_page, null, _page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode), value);

                if (value)
                    _page.SetFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
                else
                    _page.ClearFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
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
            // Only create the list of items once
            if (_actions == null)
            {
                _actions = new DesignerActionItemCollection();
                _actions.Add(new DesignerActionHeaderItem("Appearance"));
                _actions.Add(new DesignerActionPropertyItem("TextShort", "Text", "Appearance", "The page text."));
                _actions.Add(new DesignerActionPropertyItem("TextTitle", "Text Title", "Appearance", "The title text for the page."));
                _actions.Add(new DesignerActionPropertyItem("TextDescription", "Text Description", "Appearance", "The description text for the page."));
                _actions.Add(new DesignerActionPropertyItem("ImageSmall", "Image Small", "Appearance", "The small image that represents the page."));
                _actions.Add(new DesignerActionPropertyItem("ImageMedium", "Image Medium", "Appearance", "The medium image that represents the page."));
                _actions.Add(new DesignerActionPropertyItem("ImageLarge", "Image Large", "Appearance", "The large image that represents the page."));
                _actions.Add(new DesignerActionPropertyItem("ToolTipTitle", "Tooltip Title", "Appearance", "The tooltip title text for the page."));
                _actions.Add(new DesignerActionPropertyItem("ToolTipBody", "Tooltip Body", "Appearance", "The tooltip body text for the page."));
                _actions.Add(new DesignerActionPropertyItem("ToolTipImage", "Tooltip Image", "Appearance", "The tooltip image that represents the page."));
                _actions.Add(new DesignerActionHeaderItem("Flags"));
                _actions.Add(new DesignerActionPropertyItem("PageInOverflowBarForOutlookMode", "Page in Overflow Bar for Outlook mode", "Flags", "Should the page be shown on the overflow bar for the Outlook mode."));
            }

            return _actions;
        }
        #endregion
    }
}
