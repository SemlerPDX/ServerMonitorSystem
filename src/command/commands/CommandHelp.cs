using System;
using System.Reflection;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for opening a new instance of this application in
    /// a 'help list' only mode, and writing all user commands and descriptions in this
    /// new console in an endless loop until closed.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>
    /// </para>
    /// <para>
    /// (see also <seealso cref="IConfig_Manager.CommandUsages"/>, <seealso cref="IConfig_Manager.CommandDescriptions"/>,
    /// and <seealso cref="ICommand_Manager.LoadCommands()"/>)
    /// </para>
    /// </summary>
    class CommandHelp : ICommands
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
        /// Initializes a new instance of the <see cref="CommandHelp"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        public CommandHelp(IConfig_Manager configManager,
                          IConsole_Manager consoleManager)
        {
            _configManager = configManager;
            _consoleManager = consoleManager;
        }


        #region command_parameters
        public string Name { get; } = "help";
        public string Usage { get; } = "help";
        public string Description { get; } = "Display this Help List in a new console window";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == "help";
        }

        public void Execute(string[] args)
        {
            try
            {
                // Launching second instance with no arguments will display help in scrollable list with copy/paste
                string appPath = Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(appPath);
            }
            catch (Exceptions ex)
            {
                // If that fails, just list the help in this window and log the exception to file
                _consoleManager.WriteHelp(_configManager.CommandUsages, _configManager.CommandDescriptions, false);
                Console.WriteLine();
                ex.LogError("Error launching this application as help info display mode");
            }
        }

    }
}
