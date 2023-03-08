namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods to iterate through timers classes which implement
    /// <see cref="ITimers"/> with a <see cref="ITimers.Type"/> of 'cooldown',
    /// and evaluate start conditions to enable their system timers, or to stop them all.
    /// </summary>
    public interface ITimerPause_Manager
    {
        /// <summary>
        /// Iterate through all timers classes implementing <see cref="ITimers"/>, and enable 'cooldown'
        /// type timers based on parameter information, starting only if their start condition(s) are met.
        /// </summary>
        /// <param name="timer">Name of the 'cooldown' timer(s) to start with <see cref="ITimers.Name"/> equal to this string.</param>
        /// <param name="minutes">The time between elapsed timer events in minutes.</param>
        /// <param name="timerFlag">A boolean flag used as needed to pass a dynamic state from event
        /// handlers back to this <see cref="ITimerPause_Manager.StartTimers"/> method.</param>
        void StartTimers(string timer, int minutes, bool timerFlag);

        /// <summary>
        /// Stop all system timers in classes implementing <see cref="ITimers"/>
        /// with the <see cref="ITimers.Type"/> of 'cooldown'.
        /// </summary>
        void StopAllTimers();
    }
}
