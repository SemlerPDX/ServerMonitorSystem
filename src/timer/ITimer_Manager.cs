namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods to iterate through timers classes which implement
    /// <see cref="ITimers"/> and evaluate start conditions to enable system timers,
    /// or to start/stop all timers of a given name or type.
    /// </summary>
    public interface ITimer_Manager
    {
        /// <summary>
        /// Iterate through all timers classes implementing <see cref="ITimers"/>, and enable system
        /// timers based on parameter information, starting only if their start condition(s) are met.
        /// </summary>
        /// <param name="timer">Name of the system timer(s) to start with <see cref="ITimers.Name"/> equal to this string.</param>
        /// <param name="minutes">The time between elapsed timer events in minutes.</param>
        /// <param name="timerFlag">A boolean flag used as needed to pass a dynamic state from event
        /// handlers back to this <see cref="ITimer_Manager.StartTimers"/> method.</param>
        void StartTimers(string timer, int minutes, bool timerFlag);

        /// <summary>
        /// Start all timers classes implementing <see cref="ITimers"/> with the <see cref="ITimers.Type"/> of 'services'.
        /// </summary>
        void StartServicesTimers();

        /// <summary>
        /// Stop timer(s) with the <see cref="ITimers.Name"/> equal to this 'timer' string parameter.
        /// </summary>
        /// <param name="timer">Name of the system timer(s) to stop with <see cref="ITimers.Name"/> equal to this string.</param>
        void StopTimers(string timer);

        /// <summary>
        /// Stop timer(s) with the <see cref="ITimers.Type"/> equal to this 'type' string parameter.
        /// </summary>
        /// <param name="type">The type of system timer(s) to stop with <see cref="ITimers.Type"/> equal to this string.</param>
        void StopTimerTypes(string type);

        /// <summary>
        /// Stop all system timers in classes implementing <see cref="ITimers"/>.
        /// </summary>
        void StopAllTimers();
    }

}
