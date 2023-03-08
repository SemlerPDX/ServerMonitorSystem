using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface providing
    /// access to a method for reloading configuration settings and restarting application systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="ITimer_Manager"/>
    /// </para>
    /// </summary>
    class CommandReloadApp : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the various timers for the system.
        /// </summary>
        private readonly ITimer_Manager _timerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandReloadApp"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        public CommandReloadApp(IConfig_Manager configManager,
                          ITimer_Manager timerManager)
        {
            _configManager = configManager;
            _timerManager = timerManager;
        }


        #region command_parameters
        public string Name { get; } = "reload";
        public string Usage { get; } = "reload";
        public string Description { get; } = "Stop all Server Monitor functions and Re-Initialize this application";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == "reload";
        }

        public void Execute(string[] args)
        {
            ReloadApp();
            Console.WriteLine("Systems restarted and User Config File{0} been reloaded.", _configManager.CommandLineArgsActive != null ? " and Command Line Overrides have" : " has");
        }

        private void ReloadApp()
        {
            // Stop all systems timers
            _timerManager.StopTimerTypes("systems");

            // Reload config file properties
            _configManager.LoadConfig();

            // Reload command line overrides (if any)
            if (_configManager.CommandLineArgsActive != null)
                _configManager.LoadCommandLineOverrides(_configManager.CommandLineArgsInput);

            //start alert timers
            _timerManager.StartTimers("alerts", _configManager.Interval, false);
            if (_configManager.Logging)
                _timerManager.StartTimers("logging", _configManager.Frequency, false);
        }

    }
}
