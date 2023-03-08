using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// System Timers manager class which implements the <see cref="ITimer_Manager"/> interface to
    /// provide access to methods for iterating through classes implementing <see cref="ITimers"/>
    /// and evaluating start conditions to enable system timers, or to start/stop all system timers of a given name or type.
    /// <para>
    /// Dependencies: <see cref="IAlerts_Manager"/>, <see cref="IInfoFalconBMS"/>, <see cref="IInfoServer"/>,
    /// <br><see cref="IInfoMemory"/>, <see cref="ILogFileHandler"/>, <see cref="IConfig_Manager"/></br>
    /// </para>
    /// </summary>
    class Timer_Manager : ITimer_Manager
    {
        /// <summary>
        /// A list containing each Alert class implementing <see cref="ITimers"/>.
        /// </summary>
        private readonly List<ITimers> _functionTimers;

        /// <summary>
        /// The object that manages the server alerts for the system.
        /// </summary>
        private readonly IAlerts_Manager _alertsManager;
        /// <summary>
        /// The object that retrieves Falcon BMS server info from shared memory.
        /// </summary>
        private readonly IInfoFalconBMS _infoFalconBMS;
        /// <summary>
        /// The object that retrieves server process info for the system.
        /// </summary>
        private readonly IInfoServer _infoServer;
        /// <summary>
        /// The object that retrieves memory info for the system.
        /// </summary>
        private readonly IInfoMemory _infoMemory;
        /// <summary>
        /// The object that handles logging memory information to CSV file for the system.
        /// </summary>
        private readonly ILogFileHandler _logFileHandler;
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="alertsManager">The object that manages the server alerts for the system.</param>
        /// <param name="infoFalconBMS">The object that retrieves Falcon BMS server info from shared memory.</param>
        /// <param name="infoServer">The object that retrieves server process info for the system.</param>
        /// <param name="infoMemory">The object that retrieves memory info for the system.</param>
        /// <param name="logFileHandler">The object that handles logging memory information to CSV file for the system.</param>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public Timer_Manager(IAlerts_Manager alertsManager,
                          IInfoFalconBMS infoFalconBMS,
                          IInfoServer infoServer,
                          IInfoMemory infoMemory,
                          ILogFileHandler logFileHandler,
                          IConfig_Manager configManager)
        {
            _alertsManager = alertsManager;
            _infoFalconBMS = infoFalconBMS;
            _infoServer = infoServer;
            _infoMemory = infoMemory;
            _logFileHandler = logFileHandler;
            _configManager = configManager;

            // All ITimers classes with their specified interfaces, add new timers here:
            _functionTimers = new List<ITimers>
            {
                new TimerAlertPauseGame(_configManager),
                new TimerAlertPauseVOIP(_configManager),
                new TimerAlertPauseMEM(_configManager),
                new TimerAlertsMonitor(_configManager, _alertsManager),
                new TimerCsvLogMemory(_configManager, _logFileHandler),
                new TimerInfoFalconBMS(_configManager, _infoFalconBMS),
                new TimerInfoServer(_configManager, _infoServer),
                new TimerInfoMemory(_configManager, _infoMemory)
            };
        }

        /// <summary>
        /// Iterate through all timers classes implementing <see cref="ITimers"/>, and enable system
        /// timers based on parameter information, starting only if their start condition(s) are met.
        /// </summary>
        /// <param name="timer">Name of the system timer(s) to start with <see cref="ITimers.Name"/> equal to this string.</param>
        /// <param name="minutes">The time between elapsed timer events in minutes.</param>
        /// <param name="timerFlag">A boolean flag used as needed to pass a dynamic state from event
        /// handlers back to this <see cref="ITimer_Manager.StartTimers"/> method.</param>
        public void StartTimers(string timer, int minutes, bool timerFlag)
        {
            foreach (ITimers functionTimer in _functionTimers)
            {
                if (functionTimer.CanStartTimer(timer))
                {
                    functionTimer.StartTimer(timer, minutes, timerFlag);
                    break;
                }
            }
        }

        /// <summary>
        /// Start all timers classes implementing <see cref="ITimers"/> with the <see cref="ITimers.Type"/> of 'services'.
        /// </summary>
        public void StartServicesTimers()
        {
            foreach (ITimers functionTimer in _functionTimers)
            {
                if (functionTimer.Type == "services")
                    functionTimer.StartTimer(functionTimer.Name, 0, false);
            }
        }

        /// <summary>
        /// Stop timer(s) with the <see cref="ITimers.Name"/> equal to this 'timer' string parameter.
        /// </summary>
        /// <param name="timer">Name of the system timer(s) to stop with <see cref="ITimers.Name"/> equal to this string.</param>
        public void StopTimers(string timer)
        {
            foreach (ITimers functionTimer in _functionTimers)
            {
                if (functionTimer.Name == timer)
                {
                    functionTimer.StopTimer();
                    break;
                }
            }
        }

        /// <summary>
        /// Stop timer(s) with the <see cref="ITimers.Type"/> equal to this 'type' string parameter.
        /// </summary>
        /// <param name="type">The type of system timer(s) to stop with <see cref="ITimers.Type"/> equal to this string.</param>
        public void StopTimerTypes(string type)
        {
            foreach (ITimers functionTimer in _functionTimers)
            {
                if (functionTimer.Type == type)
                    functionTimer.StopTimer();
            }
        }

        /// <summary>
        /// Stop all system timers in classes implementing <see cref="ITimers"/>.
        /// </summary>
        public void StopAllTimers()
        {
            foreach (ITimers functionTimer in _functionTimers)
            {
                functionTimer.StopTimer();
            }
        }
    }
}
