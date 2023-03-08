using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for toggling the Game Crash Alerts Monitoring system ON/OFF.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class CommandAlertsGame : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAlertsGame"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public CommandAlertsGame(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }


        #region command_parameters
        // Change only these parameters to customize this boolean property toggle 
        private const string DEFAULT_PROPERTY_DESIGNATION = "Game Server Crash Alerts";
        private const string DEFAULT_PROPERTY_CHANGED = "alertsgame";

        public string Name { get; } = "alerts game";
        public string Usage { get; } = "alerts game";
        public string Description { get; } = "Toggle only Game Server Crash Alerts monitoring system ON/OFF";
        public bool ConfigSetting { get; } = true;
        private bool ConfigValue { get => _configManager.AlertsGame; set => _configManager.AlertsGame = value; }
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
            bool savedSetting = ToggleConfigValue();
            Console.WriteLine(" -{0} are now: {1}", DEFAULT_PROPERTY_DESIGNATION, savedSetting ? "ON" : "OFF");

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
