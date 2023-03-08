using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for writing the application configuration status to the console.
    /// <para>
    /// Dependencies: <see cref="IConsole_Manager"/>
    /// </para>
    /// </summary>
    class CommandReport : ICommands
    {
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandReport"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        public CommandReport(IConsole_Manager consoleManager)
        {
            _consoleManager = consoleManager;
        }


        #region command_parameters
        public string Name { get; } = "report";
        public string Usage { get; } = "report";
        public string Description { get; } = "Check Server Monitor application current status";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            Console.Clear();
            _consoleManager.WriteStatus();
        }

    }
}
