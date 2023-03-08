using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface providing
    /// access to a method for stopping the memory logging system.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="ITimer_Manager"/>
    /// </para>
    /// </summary>
    class CommandLoggingStop : ICommands
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
        /// Initializes a new instance of the <see cref="CommandLoggingStop"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        public CommandLoggingStop(IConfig_Manager configManager,
                          ITimer_Manager timerManager)
        {
            _configManager = configManager;
            _timerManager = timerManager;
        }


        #region command_parameters
        public string Name { get; } = "logging stop";
        public string Usage { get; } = "logging stop";
        public string Description { get; } = "Stop Memory Data Logging to CSV file";
        public bool ConfigSetting { get; } = true;

        private const string DEFAULT_PROPERTY_CHANGED = "logging"; // must match configManager property used below (lowercase) for command line overrides
        #endregion


        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        public bool CanExecute(string[] args)
        {
            return args.Length == 2 && args[0].ToLower() + " " + args[1].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _timerManager.StopTimers("logging");
            Console.WriteLine(" -Memory Logging System is now disabled!");
            _configManager.Logging = false;
            _configManager.SaveConfig();
            OverrideNotice();
        }

    }
}
