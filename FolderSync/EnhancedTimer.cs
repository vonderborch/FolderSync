// ***********************************************************************
// Assembly         : FolderSync
// Component        : EnhancedTimer.cs
// Created          : 05-31-2016
// 
// Version          : 1.0.0
// Last Modified On : 05-31-2016
// ***********************************************************************
// <copyright file="EnhancedTimer.cs" company="">
//		Copyright ©  2016
// </copyright>
// <summary>
//      A timer with the ability to output the remaining time on it.
// </summary>
//
// Changelog: 
//            - 1.0.0 (05-31-2016) - Initial version created.
// ***********************************************************************

using System;
using System.Timers;

/// <summary>
/// Class EnhancedTimer.
/// </summary>
public class EnhancedTimer : Timer
{
    #region Private Fields

    /// <summary>
    /// The time the timer is due.
    /// </summary>
    private DateTime dueTime;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="EnhancedTimer"/> class.
    /// </summary>
    public EnhancedTimer() : base()
    {
        Elapsed += ElapsedAction;
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets the time left (in Milliseconds). Returns 0 if the time has passed.
    /// </summary>
    /// <value>The time left.</value>
    public double TimeLeft
    {
        get
        {
            return dueTime < DateTime.Now
                ? 0
                : (dueTime - DateTime.Now).TotalMilliseconds;
        }
    }

    #endregion Public Properties

    #region Public Methods

    /// <summary>
    /// Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to true.
    /// </summary>
    ///  Changelog:
    ///             - 1.0.0 (05-31-2016) - Initial version.
    public new void Start()
    {
        dueTime = DateTime.Now.AddMilliseconds(Interval);
        base.Start();
    }

    #endregion Public Methods

    #region Protected Methods

    /// <summary>
    /// Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.
    /// </summary>
    ///  Changelog:
    ///             - 1.0.0 (05-31-2016) - Initial version.
    protected new void Dispose()
    {
        Elapsed -= ElapsedAction;
        base.Dispose();
    }

    #endregion Protected Methods

    #region Private Methods

    /// <summary>
    /// Runs when the timer has elapsed.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
    ///  Changelog:
    ///             - 1.0.0 (05-31-2016) - Initial version.
    private void ElapsedAction(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (AutoReset)
            dueTime = DateTime.Now.AddMilliseconds(Interval);
    }

    #endregion Private Methods
}