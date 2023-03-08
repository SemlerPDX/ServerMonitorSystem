using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for toggling the Memory Leak Game Server Auto-kill system ON/OFF.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class CommandAutoKill : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAutoKill"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public CommandAutoKill(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }


        #region command_parameters
        // Change only these parameters to customize this boolean property toggle 
        private const string DEFAULT_PROPERTY_DESIGNATION = "Game Server Autokill systems";
        private const string DEFAULT_PROPERTY_CHANGED = "autokill";

        public string Name { get; } = "autokill";
        public string Usage { get; } = "autokill";
        public string Description { get; } = "Toggle Memory Leak BMS Server Auto-Kill system ON/OFF";
        public bool ConfigSetting { get; } = true;
        private bool ConfigValue { get => _configManager.AutoKill; set => _configManager.AutoKill = value; }
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
            string enabledNote = String.Format("\n -Game Server will be force closed if total system memory usage exceeds {0} megabytes", _configManager.KillMem);
            Console.WriteLine(" -{0} are now: {1}{2}", DEFAULT_PROPERTY_DESIGNATION, savedSetting ? "ON" : "OFF", ConfigValue ? enabledNote : "");

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
