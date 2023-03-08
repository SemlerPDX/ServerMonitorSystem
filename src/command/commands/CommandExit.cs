using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for gracefully exiting the console application.
    /// </summary>
    class CommandExit : ICommands
    {
        #region command_parameters
        public string Name { get; } = "exit";
        public string Usage { get; } = "exit";
        public string Description { get; } = "Stop all monitoring functions and close this application\n";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            Console.WriteLine("\nExiting the application...");
        }

    }
}
