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
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base class for storage implementations.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class Storage : GlobalId
    {
        #region Instance Fields
        private NeedPaintHandler _needPaint;
        private NeedPaintHandler _needPaintDelegate;
        #endregion

        #region Identity
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

        #region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public abstract bool IsDefault { get; }
		#endregion

        #region NeedPaint
        /// <summary>
        /// Gets and sets the need paint delegate for notifying paint requests.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual NeedPaintHandler NeedPaint
        {
            get { return _needPaint; }
            set { _needPaint = value; }
        }
        #endregion

        #region NeedPaintDelegate
        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            get 
            {
                // Only create the delegate when it is first needed
                if (_needPaintDelegate == null)
                    _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);

                return _needPaintDelegate; 
            }
        }
        #endregion

        #region PerformNeedPaint
        /// <summary>
		/// Fires the NeedPaint event.
		/// </summary>
		public void PerformNeedPaint()
		{
			OnNeedPaint(this, new NeedLayoutEventArgs(false));
		}

		/// <summary>
		/// Fires the NeedPaint event.
		/// </summary>
		/// <param name="needLayout">Does the palette change require a layout.</param>
		public void PerformNeedPaint(bool needLayout)
		{
			OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
		}
		#endregion

		#region OnNeedPaint
		/// <summary>
		/// Raises the NeedPaint event.
		/// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedPaint(object sender, NeedLayoutEventArgs e)
		{
            if (_needPaint != null)
                _needPaint(this, e);
		}
		#endregion
	}
}
