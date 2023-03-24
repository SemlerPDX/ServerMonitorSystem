using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for setting a new <see cref="IConfig_Manager.LogSize"/> value.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>
    /// </para>
    /// </summary>
    class CommandLogSize : ICommands
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
        /// Initializes a new instance of the <see cref="CommandLogSize"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        public CommandLogSize(IConfig_Manager configManager,
                          IConsole_Manager consoleManager)
        {
            _configManager = configManager;
            _consoleManager = consoleManager;
        }


        #region command_parameters
        // Change only these parameters to customize this number property command
        public string Name { get; } = "logsize";
        public string Usage { get; } = "logsize <int>";
        public string Description { get; } = "Set Memory Data Logging max file size per log (in megabytes, default 1024)\n\n";
        public bool ConfigSetting { get; } = true; // required true for commands changing any on-file config setting

        private const string DEFAULT_PROPERTY_DESIGNATION = "Maximum log file size";
        private const string DEFAULT_PROPERTY_DESCRIPTOR = "megabytes";
        private const string DEFAULT_PROPERTY_CHANGED = "logsize"; // must match configManager property used below (lowercase) for command line overrides

        // Set the number range default values in the appropriate number type ex. private const float ...
        private const long DEFAULT_NUMBER_MIN = 1L;
        private const long DEFAULT_NUMBER_MAX = 4096L;

        // Cast ConfigValue and NewValue as appropriate type referring to configManager property and an out of range default value
        private long ConfigValue { get => _configManager.LogSize; set => _configManager.LogSize = value; }
        private long NewValue { get; set; } = -1L;

        // Alter this method TryParse as appropriate for number type ex. float.TryParse(...)
        private void ParseInputValue(string inputValue)
        {
            if (long.TryParse(inputValue, out long value))
                NewValue = value;
        }
        #endregion


        // Command Systems - Do not change anything below this line
        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        public bool CanExecute(string[] args)
        {
            return args.Length >= 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            bool saveSettings = GetNewValue(args);
            EvaluateResults(saveSettings);
        }

        private void EvaluateResults(bool saveSettings)
        {
            string savedReadout = String.Format("\n -{0} is now: {1} {2}",
                DEFAULT_PROPERTY_DESIGNATION,
                ConfigValue,
                DEFAULT_PROPERTY_DESCRIPTOR);

            Console.WriteLine(" -{0} setting new {1}!{2}",
                saveSettings ? "Success" : "Failure",
                DEFAULT_PROPERTY_DESIGNATION,
                saveSettings ? savedReadout : "");
        }

        private bool SetNewValue()
        {
            // Verify valid number input with defined range
            if (NewValue >= DEFAULT_NUMBER_MIN && NewValue <= DEFAULT_NUMBER_MAX)
            {
                // Set the value to the corresponding property and save
                ConfigValue = NewValue;
                _configManager.SaveConfig();
                OverrideNotice();
                return true;
            }
            else
            {
                Console.WriteLine(" -invalid number out of range (min:{0}  max:{1})", DEFAULT_NUMBER_MIN, DEFAULT_NUMBER_MAX);
                return false;
            }
        }

        private bool GetNewValue(string[] args)
        {
            string inputValue = NewValue.ToString(); // default 'out of range' number value
            var currentValue = ConfigValue;

            if (args.Length == 1)
            {
                // Get number from console input
                Console.WriteLine("Enter a new {0} (in {1}) (min:{2}  max:{3} now:{4})",
                    DEFAULT_PROPERTY_DESIGNATION, DEFAULT_PROPERTY_DESCRIPTOR,
                    DEFAULT_NUMBER_MIN, DEFAULT_NUMBER_MAX, currentValue);
                inputValue = _consoleManager.GetInputNumber().ToString();
            }
            else
            {
                // Check existing input for number value
                if (int.TryParse(args[1], out int value))
                    inputValue = value.ToString();
            }

            if (String.IsNullOrWhiteSpace(inputValue))
                return false;

            // Transform input value based on command parameters method
            ParseInputValue(inputValue);

            // Set New Value to corresponding property (if changed)
            return SetNewValue();
        }

    }
}
