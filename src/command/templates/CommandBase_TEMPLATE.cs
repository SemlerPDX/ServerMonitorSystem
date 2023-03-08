/*
using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for setting a new <see cref="IConfig_Manager.EmailsVOIP"/> value.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// [A general command template example for creating new commands in the ServerMonitorSystem app]
    /// </remarks>
    class CommandBase_TEMPLATE : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager; // Dependencies as needed

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase_TEMPLATE"/> class with the specified interface(s).
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public CommandBase_TEMPLATE(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }


        #region command_parameters
        private const string DEFAULT_PROPERTY_CHANGED = "emailsvoip"; // must match configManager property used below (lowercase) for command line overrides

        public string Name { get; } = "command name";  // ex:  "interval"
        public string Usage { get; } = "command name";  // ex:  "interval <int>"
        public string Description { get; } = "Example Description";  // ex:  "Set Alerts monitoring interval (in minutes, default 5)"
        public bool ConfigSetting { get; } = true; // ex. true when command saves a config property, set as appropriate

        // Cast ConfigValue as appropriate type getting and setting the appropriate configManager property for this command (if needed)
        private bool ConfigValue { get => _configManager.EmailsVOIP; set => _configManager.EmailsVOIP = value; }
        #endregion


        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        // Command Systems - Change methods and expressions below this line as needed
        public bool CanExecute(string[] args)
        {
            // Adjust as needed per command
            return args.Length == 1 && args[0].ToLower() == Name;
            //return args.Length == 2 && $"{args[0].ToLower()} {args[1].ToLower()}" == Name;
        }

        public void Execute(string[] args)
        {
            // Example command which changes and saves a property setting
            bool saveSettings = ExecuteMethodExample(args);
            Console.WriteLine(" -{0} entering example value!", saveSettings ? "Success" : "Failure");
            if (saveSettings)
            {
                // Save the new value to file
                _configManager.SaveConfig();
                OverrideNotice();
            }
        }

        private bool ExecuteMethodExample(string[] args)
        {
            // Evaluate input command further if/as needed
            if (args == null)
                return false;

            // do stuff...

            // Set any new value(s) to the corresponding property as appropriate
            ConfigValue = !ConfigValue; // or some value....

            return true;
        }

    }
}
*/