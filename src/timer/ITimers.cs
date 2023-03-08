namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to Timer classes which implement the <see cref="ITimers"/>
    /// interface, and to their properties &amp; methods used to identify, stop, and
    /// evalute start conditions to enable system timers.
    /// </summary>
    public interface ITimers
    {
        /// <summary>
        /// The name of the system timer.
        /// <para>
        /// <br> -systems: (alerts, logging)</br>
        /// <br> -services: (memory, server, falconbms)</br>
        /// <br> -cooldown: (game, voip, mem)</br>
        /// </para>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type definition of the system timer.
        /// <para>
        ///   -Options: (systems, services, cooldown)
        /// </para>
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Determines whether the specified timer can be started.
        /// </summary>
        /// <param name="timer">The name of the timer.</param>
        /// <returns>true if the timer can be started; otherwise, false.</returns>
        bool CanStartTimer(string timer);

        /// <summary>
        /// Starts the specified timer for the given duration.
        /// </summary>
        /// <param name="timer">The name of the timer.</param>
        /// <param name="minutes">The duration of the timer, in minutes.</param>
        /// <param name="timerFlag">A flag indicating whether the timer is already running.</param>
        void StartTimer(string timer, int minutes, bool timerFlag);

        /// <summary>
        /// Stops the currently running timer belonging to this timer class.
        /// </summary>
        void StopTimer();
    }
}
