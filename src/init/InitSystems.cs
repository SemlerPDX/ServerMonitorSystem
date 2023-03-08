using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Main Application Systems Initialization class implementing <see cref="IInitSystems"/> for
    /// providing access to methods which control initialization flow &amp; check for app updates,
    /// and which start/restart system timers to manage alerts, log performance data, and update
    /// <see cref="IConfig_Manager"/> properties using the service classes.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>, <see cref="ITimer_Manager"/>,
    /// <br><see cref="ITimerPause_Manager"/>, <see cref="IInfoUpdates"/></br>
    /// </para>
    /// </summary>
    class InitSystems : IInitSystems
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that manages the various timers for the system.
        /// </summary>
        private readonly ITimer_Manager _timerManager;
        /// <summary>
        /// The object that manages the alert 'cooldown' pause timers for the system.
        /// </summary>
        private readonly ITimerPause_Manager _timerPauseManager;
        /// <summary>
        /// The object that retrieves application update info from the online database.
        /// </summary>
        private readonly IInfoUpdates _infoUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitSystems"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        /// <param name="timerPauseManager">The object that manages the alert 'cooldown' pause timers for the system.</param>
        /// <param name="infoUpdates">The object that retrieves application update info from the online database.</param>
        public InitSystems(IConfig_Manager configManager,
                          IConsole_Manager consoleManager,
                          ITimer_Manager timerManager,
                          ITimerPause_Manager timerPauseManager,
                          IInfoUpdates infoUpdates)
        {
            _configManager = configManager;
            _consoleManager = consoleManager;
            _timerManager = timerManager;
            _timerPauseManager = timerPauseManager;
            _infoUpdates = infoUpdates;
        }

        /// <summary>
        /// Enables system timers in classes implementing <see cref="ITimers"/> of the 'services' type, writes help list to the console,
        /// and a note about any updates for this app - then waits for user to press any key to continue initialization.
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandUsages"/> which holds all console command usage examples,
        /// and <seealso cref="IConfig_Manager.CommandDescriptions"/> which holds all command descriptions,
        /// dictating the help list display format.)
        /// </para>
        /// </summary>
        public void PreInitialization()
        {
            _timerManager.StartServicesTimers();

            _consoleManager.WriteHelp(_configManager.CommandUsages, _configManager.CommandDescriptions, false);

            _infoUpdates.CheckForUpdates(false);
            Console.WriteLine();

            _consoleManager.GetAnyKeyContinue();

            Console.Clear();
        }

        /// <summary>
        /// Enables all system timers in classes managed by <see cref="ITimer_Manager"/> which implement <see cref="ITimers"/>
        /// in accordance with their individual 'can start' conditions, writes a note about any updates for this app, and
        /// finally, prints current application status to the console.
        /// </summary>
        public void InitializeSystems()
        {
            // service info:
            _timerManager.StartServicesTimers();
            // Begin Main Server Monitor Timers
            _timerManager.StartTimers("alerts", _configManager.Interval, false);
            // optional logging:
            _timerManager.StartTimers("logging", _configManager.Frequency, false);
            // check updates and write to console only if update is found
            _infoUpdates.CheckForUpdates(false);
            // Print current status and default prompt
            _consoleManager.WriteStatus();
        }

        /// <summary>
        /// Stops system timers in classes implementing <see cref="ITimers"/> with the names 'alerts' and 'logging',
        /// and then starts them again with any updated properties including <see cref="IConfig_Manager.Interval"/>
        /// and <see cref="IConfig_Manager.Frequency"/>, then writes a note about any updates for this app,
        /// and finally, prints current application status to the console.
        /// </summary>
        public void ReInitializeSystems()
        {
            // Begin Main Server Monitor Timers
            _timerManager.StopTimers("alerts");
            // optional logging:
            _timerManager.StopTimers("logging");
            // Begin Main Server Monitor Timers
            _timerManager.StartTimers("alerts", _configManager.Interval, false);
            // optional logging:
            _timerManager.StartTimers("logging", _configManager.Frequency, false);
            // check updates and write to console only if update is found
            _infoUpdates.CheckForUpdates(false);
            // Print current status and default prompt
            _consoleManager.WriteStatus();
        }

        /// <summary>
        /// End all system timers in classes implementing <see cref="ITimers"/> used througout the system.
        /// </summary>
        public void StopAllSystems()
        {
            _timerManager.StopAllTimers();
            _timerPauseManager.StopAllTimers();
        }

    }
}
