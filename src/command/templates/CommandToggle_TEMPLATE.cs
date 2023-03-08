/*
using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for toggling <see cref="IConfig_Manager.EmailsVOIP"/> ON/OFF.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// [A command template example for creating new toggle commands in the ServerMonitorSystem app]
    /// </remarks>
    class CommandToggle_TEMPLATE : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandToggle_TEMPLATE"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public CommandToggle_TEMPLATE(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }


        #region command_parameters
        // Change only these parameters to customize this boolean property toggle 
        private const string DEFAULT_PROPERTY_DESIGNATION = "VOIP Server Crash Email Alerts";
        private const string DEFAULT_PROPERTY_CHANGED = "emailsvoip";

        public string Name { get; } = "emails voip";
        public string Usage { get; } = "emails voip";
        public string Description { get; } = "Toggle only VOIP Server Crash Alerts emails function ON/OFF";
        public bool ConfigSetting { get; } = true;
        private bool ConfigValue { get => _configManager.EmailsVOIP; set => _configManager.EmailsVOIP = value; }
        #endregion


        // Command Systems - Do not change anything below this line
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
*/