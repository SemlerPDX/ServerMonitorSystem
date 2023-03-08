
using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// System 'Cooldown' Timers manager class which implements the <see cref="ITimerPause_Manager"/> interface
    /// to provide access to methods for iterating through all classes implementing <see cref="ITimers"/>
    /// with a <see cref="ITimers.Type"/> of 'cooldown', and evaluating start conditions to
    /// enable system timers, or to stop all 'cooldown' type timers.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class TimerPause_Manager : ITimerPause_Manager
    {
        /// <summary>
        /// A list containing each Alert class implementing <see cref="ITimers"/>
        /// with a <see cref="ITimers.Type"/> of 'cooldown'.
        /// </summary>
        private readonly List<ITimers> _pauseTimers;

        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerPause_Manager"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public TimerPause_Manager(IConfig_Manager configManager)
        {
            _configManager = configManager;

            // All 'cooldown' type ITimers classes with their specified interfaces, add new timers here:
            _pauseTimers = new List<ITimers>
            {
                new TimerAlertPauseGame(_configManager),
                new TimerAlertPauseVOIP(_configManager),
                new TimerAlertPauseMEM(_configManager)
            };
        }

        /// <summary>
        /// Iterate through all timers classes implementing <see cref="ITimers"/>, and enable 'cooldown'
        /// type timers based on parameter information, starting only if their start condition(s) are met.
        /// </summary>
        /// <param name="timer">Name of the 'cooldown' timer(s) to start with <see cref="ITimers.Name"/> equal to this string.</param>
        /// <param name="minutes">The time between elapsed timer events in minutes.</param>
        /// <param name="timerFlag">A boolean flag used as needed to pass a dynamic state from event
        /// handlers back to this <see cref="ITimerPause_Manager.StartTimers"/> method.</param>
        public void StartTimers(string timer, int minutes, bool timerFlag)
        {
            foreach (ITimers functionTimer in _pauseTimers)
            {
                if (functionTimer.CanStartTimer(timer))
                {
                    functionTimer.StartTimer(timer, minutes, timerFlag);
                    break;
                }
            }
        }

        /// <summary>
        /// Stop all system timers in classes implementing <see cref="ITimers"/>
        /// with the <see cref="ITimers.Type"/> of 'cooldown'.
        /// </summary>
        public void StopAllTimers()
        {
            foreach (ITimers functionTimer in _pauseTimers)
            {
                functionTimer.StopTimer();
            }
        }
    }
}
