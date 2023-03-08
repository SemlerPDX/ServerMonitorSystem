using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for writing all user commands and descriptions in the console.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>
    /// </para>
    /// <para>
    /// (see also <seealso cref="IConfig_Manager.CommandUsages"/>, <seealso cref="IConfig_Manager.CommandDescriptions"/>,
    /// and <seealso cref="ICommand_Manager.LoadCommands()"/>)
    /// </para>
    /// </summary>
    class CommandHelpList : ICommands
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
        /// Initializes a new instance of the <see cref="CommandHelpList"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        public CommandHelpList(IConfig_Manager configManager,
                          IConsole_Manager consoleManager)
        {
            _configManager = configManager;
            _consoleManager = consoleManager;
        }


        #region command_parameters
        public string Name { get; } = "helplist";
        public string Usage { get; } = "helplist";
        public string Description { get; } = "Display this Help List of console commands & descriptions";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _consoleManager.WriteHelp(_configManager.CommandUsages, _configManager.CommandDescriptions, false);
            Console.WriteLine();
        }

    }
}
