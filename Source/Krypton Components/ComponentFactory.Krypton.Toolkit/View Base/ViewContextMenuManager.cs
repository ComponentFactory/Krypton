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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// ViewMananger for context menu handling.
    /// </summary>
    public class ViewContextMenuManager : ViewManager
    {
        #region Type Definitions
        private class TargetList : List<IContextMenuTarget> { };
        #endregion

        #region Instance Fields
        private IContextMenuTarget _target;
        private IContextMenuTarget _targetSubMenu;
        private Timer _itemDelayTimer;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewContextMenuManager class.
		/// </summary>
        /// <param name="control">Owning control.</param>
        /// <param name="root">Root of the view hierarchy.</param>
        public ViewContextMenuManager(Control control, ViewBase root)
            : base(control, root)
		{
            // Create timer to notify targets when the standard delay expires
            _itemDelayTimer = new Timer();
            _itemDelayTimer.Interval = Math.Max(1, SystemInformation.MenuShowDelay);
            _itemDelayTimer.Tick += new EventHandler(OnDelayTimerExpire);
		}

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        public override void Dispose()
        {
            if (_itemDelayTimer != null)
            {
                _itemDelayTimer.Stop();
                _itemDelayTimer.Dispose();
                _itemDelayTimer = null;
            }

            if (_targetSubMenu != null)
            {
                _targetSubMenu.ClearSubMenu();
                _targetSubMenu = null;
            }

            if (_target != null)
            {
                _target.ClearTarget();
                _target = null;
            }

            base.Dispose();
        }
        #endregion

        #region Target Handling
        /// <summary>
        /// Set the provided target as the current target.
        /// </summary>
        /// <param name="target">Reference to the new target.</param>
        /// <param name="startTimer">Should a timer be started for handling sub menu showing.</param>
        public void SetTarget(IContextMenuTarget target, bool startTimer)
        {
            // Only interested in a change of target
            if (_target != target)
            {
                // Tell current target to reset drawing
                if (_target != null)
                {
                    _itemDelayTimer.Stop();
                    _target.ClearTarget();
                    _target = null;
                }

                // Shift the active view to the new target
                if (target != null)
                    ActiveView = target.GetActiveView();
                else
                    ActiveView = null;

                _target = target;

                // Tell new target to draw as highlighted and start delay timer
                if (_target != null)
                {
                    _itemDelayTimer.Stop();

                    if (startTimer)
                        _itemDelayTimer.Start();

                    _target.ShowTarget();
                }
            }
        }

        /// <summary>
        /// Set the provided target as the current target and it is already showing a sub menu
        /// </summary>
        public void SetTargetSubMenu(IContextMenuTarget target)
        {
            // Kill any running timer
            if (_itemDelayTimer != null)
                _itemDelayTimer.Stop();

            // If the currently showing sub menu is not for the new target
            if (_targetSubMenu != target)
            {
                // Do we know a target that was instructed to show any sub menu?
                if (_targetSubMenu != null)
                {
                    // Inform the same target to remove any showing sub menu
                    _targetSubMenu.ClearSubMenu();
                    _targetSubMenu = null;
                }

                // Remember the new sub menu target
                _targetSubMenu = target;
            }

            // Tell current target to reset drawing
            if (_target != target)
            {
                _target.ClearTarget();
                _target = null;
            }

            _target = target;

            // Tell new target to draw as highlighted and start delay timer
            if (_target != null)
                _target.ShowTarget();
        }

        /// <summary>
        /// Clear the provided target from being the current target.
        /// </summary>
        /// <param name="target"></param>
        public void ClearTarget(IContextMenuTarget target)
        {
            // Is there a current target to clear down?
            if (_target != null)
            {
                _itemDelayTimer.Stop();
                _target.ClearTarget();
                _target = null;
            }

            // With no target, we might still be showing a sub menu...
            if (_targetSubMenu != null)
            {
                // We need to show the sub menu target as active
                _target = _targetSubMenu;
                _target.ShowTarget();
            }
        }

        /// <summary>
        /// Clear the provided target as no longer showing a sub menu.
        /// </summary>
        /// <param name="target">Target that used to be showing a sub menu.</param>
        public void ClearTargetSubMenu(IContextMenuTarget target)
        {
            // Do we know a target that was instructed to show any sub menu?
            if (_targetSubMenu != null)
            {
                // Inform the same target to remove any showing sub menu
                _targetSubMenu.ClearSubMenu();
                _targetSubMenu = null;
            }
        }
        #endregion

        #region Keyboard Handling
        /// <summary>
        /// Handle up key being pressed.
        /// </summary>
        public void KeyUp()
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Find the next appropriate target
            IContextMenuTarget newTarget = null;
            if (_target == null)
                newTarget = FindBottomLeftTarget(targets);
            else
                newTarget = FindUpTarget(targets, _target);

            // If we found a new target, then make it the current target
            if ((newTarget != null) && (newTarget != _target))
                SetTarget(newTarget, false);
        }

        /// <summary>
        /// Handle down key being pressed.
        /// </summary>
        public void KeyDown()
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Find the next appropriate target
            IContextMenuTarget newTarget = null;
            if (_target == null)
                newTarget = FindTopLeftTarget(targets);
            else
                newTarget = FindDownTarget(targets, _target);

            // If we found a new target, then make it the current target
            if ((newTarget != null) && (newTarget != _target))
                SetTarget(newTarget, false);
        }

        /// <summary>
        /// Handle left key being pressed.
        /// </summary>
        /// <param name="wrap">Should calculation wrap around the left edge.</param>
        /// <returns>Did the calculation hit the left edge.</returns>
        public bool KeyLeft(bool wrap)
        {
            bool hitEdge = false;
            TargetList targets = ConstructKeyboardTargets(Root);

            // Find the next appropriate target
            IContextMenuTarget newTarget = null;
            if (_target == null)
                newTarget = FindTopRightTarget(targets);
            else
                newTarget = FindLeftTarget(targets, _target, wrap, ref hitEdge);

            // If we found a new target, then make it the current target
            if ((newTarget != null) && (newTarget != _target))
                SetTarget(newTarget, false);

            return hitEdge;
        }

        /// <summary>
        /// Handle right key being pressed.
        /// </summary>
        public void KeyRight()
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Find the next appropriate target
            IContextMenuTarget newTarget = null;
            if (_target == null)
                newTarget = FindTopLeftTarget(targets);
            else
                newTarget = FindRightTarget(targets, _target);

            // If we found a new target, then make it the current target
            if ((newTarget != null) && (newTarget != _target))
                SetTarget(newTarget, false);
        }

        /// <summary>
        /// Handle tab key being pressed.
        /// </summary>
        /// <param name="shift">Was shift key pressed for the tab.</param>
        public void KeyTab(bool shift)
        {
            if (shift)
            {
                // If nothing currently selected, then start at end
                if (_target == null)
                    KeyEnd();
                else
                {
                    // Find the currently selected target
                    TargetList targets = ConstructKeyboardTargets(Root);
                    for (int i = targets.Count - 1; i >= 0; i--)
                        if (targets[i] == _target)
                        {
                            // If at the first item then wrap to the last
                            if (i == 0)
                                KeyEnd();
                            else
                                SetTarget(targets[i - 1], false);
                            return;
                        }

                    // Should never happen!
                    KeyEnd();
                }
            }
            else
            {
                // If nothing currently selected, then start at home
                if (_target == null)
                    KeyHome();
                else
                {
                    // Find the currently selected target
                    TargetList targets = ConstructKeyboardTargets(Root);
                    for(int i=0; i<targets.Count; i++)
                        if (targets[i] == _target)
                        {
                            // If at the last item then wrap to the first
                            if (i == (targets.Count - 1))
                                KeyHome();
                            else
                                SetTarget(targets[i + 1], false);
                            return;
                        }

                    // Should never happen!
                    KeyHome();
                }
            }
        }

        /// <summary>
        /// Handle home key being pressed.
        /// </summary>
        public void KeyHome()
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Move to the first target found
            if (targets.Count > 0)
                SetTarget(targets[0], false);
        }

        /// <summary>
        /// Handle end key being pressed.
        /// </summary>
        public void KeyEnd()
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Move to the last target found
            if (targets.Count > 0)
                SetTarget(targets[targets.Count - 1], false);
        }

        /// <summary>
        /// Handle key that could be interpreted as a mnemonic.
        /// </summary>
        /// <param name="charCode">Key code to test against.</param>
        public void KeyMnemonic(char charCode)
        {
            TargetList targets = ConstructKeyboardTargets(Root);

            // Scan the targets after ourself and until the end
            bool found = false;
            for (int i = 0; i < targets.Count; i++)
            {
                if (!found)
                    found = (_target == targets[i]);
                else
                {
                    if (targets[i].MatchMnemonic(charCode))
                    {
                        SetTarget(targets[i], false);
                        targets[i].MnemonicActivate();
                        return;
                    }
                }
            }

            // Scan the targets from the start to the current entry
            for (int i = 0; i < targets.Count; i++)
            {
                if (_target == targets[i])
                    break;
                else
                {
                    if (targets[i].MatchMnemonic(charCode))
                    {
                        SetTarget(targets[i], false);
                        targets[i].MnemonicActivate();
                        return;
                    }
                }
            }
        }
        #endregion

        #region DoesStackedClientMouseDownBecomeCurrent
        /// <summary>
        /// Should a mouse down at the provided point cause it to become the current tracking popup.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        public bool DoesStackedClientMouseDownBecomeCurrent(Message m, Point pt)
        {
            // Do we have a current target we can ask?
            if (_target != null)
                return _target.DoesStackedClientMouseDownBecomeCurrent(pt);
            else
                return true;
        }
        #endregion

        #region Implementation
        private TargetList ConstructKeyboardTargets(ViewBase root)
        {
            TargetList targets = new TargetList();
            FindKeyboardTargets(root, targets);
            return targets;
        }

        private void FindKeyboardTargets(ViewBase parent, TargetList targets)
        {
            IContextMenuTarget target = null;

            // Any target interface will be implemented on a controller instance
            if (parent.KeyController != null)
                target = parent.KeyController as IContextMenuTarget;
            else if (parent.MouseController != null)
                target = parent.MouseController as IContextMenuTarget;

            // Did we find a target associated with the view element?
            if (target != null)
                targets.Add(target);

            // Recurse into each of the child elements
            foreach (ViewBase child in parent)
                FindKeyboardTargets(child, targets);
        }

        private IContextMenuTarget FindTopLeftTarget(TargetList targets)
        {
            // Best match found so far
            IContextMenuTarget topLeftTarget = null;
            Rectangle topLeftRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                // It nothing found so far, it automatically becomes the best match
                if (topLeftTarget == null)
                {
                    topLeftTarget = target;
                    topLeftRect = target.ClientRectangle;
                }
                else
                {
                    Rectangle targetRect = target.ClientRectangle;

                    // If above the current best match, or if at
                    // same vertical position but further left...
                    if ((targetRect.Y < topLeftRect.Y) ||
                        ((targetRect.Y == topLeftRect.Y) && (targetRect.X < topLeftRect.X)))
                    {
                        //...then it becomes the new best match
                        topLeftTarget = target;
                        topLeftRect = targetRect;
                    }
                }
            }

            return topLeftTarget;
        }

        private IContextMenuTarget FindTopRightTarget(TargetList targets)
        {
            // Best match found so far
            IContextMenuTarget topRightTarget = null;
            Rectangle topRightRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                // It nothing found so far, it automatically becomes the best match
                if (topRightTarget == null)
                {
                    topRightTarget = target;
                    topRightRect = target.ClientRectangle;
                }
                else
                {
                    Rectangle targetRect = target.ClientRectangle;

                    // If above the current best match, or if at
                    // same vertical position but further right...
                    if ((targetRect.Y < topRightRect.Y) ||
                        ((targetRect.Y == topRightRect.Y) && (targetRect.X > topRightRect.X)))
                    {
                        //...then it becomes the new best match
                        topRightTarget = target;
                        topRightRect = targetRect;
                    }
                }
            }

            return topRightTarget;
        }

        private IContextMenuTarget FindBottomLeftTarget(TargetList targets)
        {
            // Best match found so far
            IContextMenuTarget bottomLeftTarget = null;
            Rectangle bottomLeftRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                // It nothing found so far, it automatically becomes the best match
                if (bottomLeftTarget == null)
                {
                    bottomLeftTarget = target;
                    bottomLeftRect = target.ClientRectangle;
                }
                else
                {
                    Rectangle targetRect = target.ClientRectangle;

                    // If below the current best match, or if at
                    // same vertical position but further left...
                    if ((targetRect.Bottom > bottomLeftRect.Bottom) ||
                        ((targetRect.Bottom == bottomLeftRect.Bottom) && (targetRect.X < bottomLeftRect.X)))
                    {
                        //...then it becomes the new best match
                        bottomLeftTarget = target;
                        bottomLeftRect = targetRect;
                    }
                }
            }

            return bottomLeftTarget;
        }

        private IContextMenuTarget FindDownTarget(TargetList targets, IContextMenuTarget current)
        {
            // Find the next item below the current one
            IContextMenuTarget newTarget = FindDownTarget(targets, current.ClientRectangle);

            // If nothing found, then we must be at the bottom of the display
            if (newTarget == null)
            {
                // Convert item rectangle to be above the client area
                Rectangle currentRect = current.ClientRectangle;
                currentRect.Y = 0;
                currentRect.Height = 0;

                // Now find the next item below it, which must be the top most matching entry
                newTarget = FindDownTarget(targets, currentRect);
            }

            return newTarget;
        }

        private IContextMenuTarget FindDownTarget(TargetList targets, Rectangle currentRect)
        {
            // We compare from the bottom edge always
            currentRect.Y = currentRect.Bottom;
            currentRect.Height = 0;

            // Best match found so far
            IContextMenuTarget nextTarget = null;
            Rectangle nextRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                Rectangle targetRect = target.ClientRectangle;

                // Only interested in targets that are below the current one
                if (targetRect.Top >= currentRect.Bottom)
                {
                    // It nothing found so far, it automatically becomes the best match
                    if (nextTarget == null)
                    {
                        nextTarget = target;
                        nextRect = targetRect;
                    }
                    else
                    {
                        double currentDistance = CenterDistance(currentRect, new Rectangle(nextRect.X, nextRect.Y, nextRect.Width, 0));
                        double nextDistance = CenterDistance(currentRect, new Rectangle(targetRect.X, targetRect.Y, targetRect.Width, 0));

                        // If next target is nearer than the current best...
                        if (nextDistance < currentDistance)
                        {
                            //...then it becomes the new best match
                            nextTarget = target;
                            nextRect = targetRect;
                        }
                    }
                }
            }

            return nextTarget;
        }
        
        private IContextMenuTarget FindUpTarget(TargetList targets, IContextMenuTarget current)
        {
            // Find the next item above the current one
            IContextMenuTarget newTarget = FindUpTarget(targets, current.ClientRectangle);

            // If nothing found, then we must be at the top of the display
            if (newTarget == null)
            {
                // Convert item rectangle to be below the client area
                Rectangle currentRect = current.ClientRectangle;
                currentRect.Y = AlignControl.Height + 1;
                currentRect.Height = 0;

                // Now find the next item above it, which must be the bottom most matching entry
                newTarget = FindUpTarget(targets, currentRect);
            }

            return newTarget;
        }

        private IContextMenuTarget FindUpTarget(TargetList targets, Rectangle currentRect)
        {
            // We compare from the top edge always
            currentRect.Height = 0;

            // Best match found so far
            IContextMenuTarget nextTarget = null;
            Rectangle nextRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                Rectangle targetRect = target.ClientRectangle;

                // Only interested in targets that are above the current one
                if (targetRect.Bottom <= currentRect.Top)
                {
                    // It nothing found so far, it automatically becomes the best match
                    if (nextTarget == null)
                    {
                        nextTarget = target;
                        nextRect = targetRect;
                    }
                    else
                    {
                        double currentDistance = CenterDistance(currentRect, new Rectangle(nextRect.X, nextRect.Bottom, nextRect.Width, 0));
                        double nextDistance = CenterDistance(currentRect, new Rectangle(targetRect.X, targetRect.Bottom, targetRect.Width, 0));

                        // If next target is nearer than the current best...
                        if (nextDistance < currentDistance)
                        {
                            //...then it becomes the new best match
                            nextTarget = target;
                            nextRect = targetRect;
                        }
                    }
                }
            }

            return nextTarget;
        }

        private IContextMenuTarget FindRightTarget(TargetList targets, IContextMenuTarget current)
        {
            // Find the next item after the current one
            IContextMenuTarget newTarget = FindRightTarget(targets, current.ClientRectangle);

            // If nothing found, then we must be at the right side of the display
            if (newTarget == null)
            {
                // Convert item rectangle to be left of the client area
                Rectangle currentRect = current.ClientRectangle;
                currentRect.X = 0;
                currentRect.Width = 0;

                // Now find the next item after it, which must be the left most matching entry
                newTarget = FindRightTarget(targets, currentRect);
            }

            return newTarget;
        }

        private IContextMenuTarget FindRightTarget(TargetList targets, Rectangle currentRect)
        {
            // We compare from the right edge always
            currentRect.X = currentRect.Right;
            currentRect.Width = 0;

            // Best match found so far
            IContextMenuTarget nextTarget = null;
            Rectangle nextRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                Rectangle targetRect = target.ClientRectangle;

                // Only interested in targets that are after the current one
                if (targetRect.Left >= currentRect.Right)
                {
                    // It nothing found so far, it automatically becomes the best match
                    if (nextTarget == null)
                    {
                        nextTarget = target;
                        nextRect = targetRect;
                    }
                    else
                    {
                        double currentDistance = CenterDistance(currentRect, new Rectangle(nextRect.X, nextRect.Y, 0, nextRect.Height));
                        double nextDistance = CenterDistance(currentRect, new Rectangle(targetRect.X, targetRect.Y, 0, targetRect.Height));

                        // If next target is nearer than the current best...
                        if (nextDistance < currentDistance)
                        {
                            //...then it becomes the new best match
                            nextTarget = target;
                            nextRect = targetRect;
                        }
                    }
                }
            }

            return nextTarget;
        }

        private IContextMenuTarget FindLeftTarget(TargetList targets, 
                                                  IContextMenuTarget current,
                                                  bool wrap,
                                                  ref bool hitEdge)
        {
            // Find the item left of the current one
            IContextMenuTarget newTarget = FindLeftTarget(targets, current.ClientRectangle);

            // If nothing found, then we must be at the left edge of the display
            if (newTarget == null)
            {
                if (wrap)
                {
                    // Convert item rectangle to be right of the client area
                    Rectangle currentRect = current.ClientRectangle;
                    currentRect.X = AlignControl.Width + 1;
                    currentRect.Width = 0;

                    // Now find the next item left ot it, which must be the right most matching entry
                    newTarget = FindLeftTarget(targets, currentRect);
                }
                else
                    hitEdge = true;
            }

            return newTarget;
        }

        private IContextMenuTarget FindLeftTarget(TargetList targets, Rectangle currentRect)
        {
            // We compare from the left edge always
            currentRect.Width = 0;

            // Best match found so far
            IContextMenuTarget nextTarget = null;
            Rectangle nextRect = Rectangle.Empty;

            // Search all targets, looking for a better match than the current best
            foreach (IContextMenuTarget target in targets)
            {
                Rectangle targetRect = target.ClientRectangle;

                // Only interested in targets that are before the current one
                if (targetRect.Right <= currentRect.Left)
                {
                    // It nothing found so far, it automatically becomes the best match
                    if (nextTarget == null)
                    {
                        nextTarget = target;
                        nextRect = targetRect;
                    }
                    else
                    {
                        // Compare the right edge only
                        double currentDistance = CenterDistance(currentRect, new Rectangle(nextRect.Right, nextRect.Y, 0, nextRect.Height));
                        double nextDistance = CenterDistance(currentRect, new Rectangle(targetRect.Right, targetRect.Y, 0, targetRect.Height));

                        // If next target is nearer than the current best...
                        if (nextDistance < currentDistance)
                        {
                            //...then it becomes the new best match
                            nextTarget = target;
                            nextRect = targetRect;
                        }
                    }
                }
            }

            return nextTarget;
        }

        private double CenterDistance(Rectangle source, Rectangle compare)
        {
            double horzDistance = Math.Abs(((source.Left + source.Right) / 2) - ((compare.Left + compare.Right) / 2));
            double vertDistance = Math.Abs(((source.Top + source.Bottom) / 2) - ((compare.Top + compare.Bottom) / 2));
            double distance = Math.Sqrt((horzDistance * horzDistance) + (vertDistance * vertDistance));
            return distance;
        }

        private void OnDelayTimerExpire(object sender, EventArgs e)
        {
            if (_itemDelayTimer != null)
            {
                _itemDelayTimer.Stop();

                // If the target and current sub menu target are the same, then there is nothing
                // to do as we would only tell the sub menu to be removed and shown again.
                if (_target != _targetSubMenu)
                {
                    // Do we know a target that was instructed to show any sub menu?
                    if (_targetSubMenu != null)
                    {
                        // Inform the same target to remove any showing sub menu
                        _targetSubMenu.ClearSubMenu();
                        _targetSubMenu = null;
                    }

                    // If we still have a target and it has a sub menu
                    if ((_target != null) && _target.HasSubMenu)
                    {
                        // Remember we told the target to show any sub menu
                        _targetSubMenu = _target;
                        _target.ShowSubMenu();
                    }
                }
            }
        }

        #endregion
    }
}
