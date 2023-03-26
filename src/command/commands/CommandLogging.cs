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
    class CommandLogging : ICommands
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
        /// Initializes a new instance of the <see cref="CommandLogging"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        public CommandLogging(IConfig_Manager configManager,
                          ITimer_Manager timerManager)
        {
            _configManager = configManager;
            _timerManager = timerManager;
        }


        #region command_parameters
        private const string DEFAULT_PROPERTY_DESIGNATION = "Memory Logging Systems";
        private const string DEFAULT_PROPERTY_CHANGED = "logging"; // must match configManager property used below (lowercase) for command line overrides

        public string Name { get; } = "logging";
        public string Usage { get; } = "logging";
        public string Description { get; } = "Toggle Memory Data Logging to CSV file ON/OFF\n";
        public bool ConfigSetting { get; } = true;
        private bool ConfigValue { get => _configManager.Logging; set => _configManager.Logging = value; }
        #endregion


        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            bool savedSetting = ToggleConfigValue();
            if (savedSetting)
            {
                _timerManager.StartTimers("logging", _configManager.Frequency, false);
            }
            else
            {
                _timerManager.StopTimers("logging");
            }

            Console.WriteLine(" -{0} are now: {1}", DEFAULT_PROPERTY_DESIGNATION, savedSetting ? "ON" : "OFF");

            _configManager.Logging = savedSetting;
            _configManager.SaveConfig();
            OverrideNotice();
        }

        private bool ToggleConfigValue()
        {
            ConfigValue = !ConfigValue;
            return ConfigValue;
        }

    }
}
