using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for clearing the entire display of the console.
    /// </summary>
    class CommandClear : ICommands
    {
        #region command_parameters
        public string Name { get; } = "clear";
        public string Usage { get; } = "clear";
        public string Description { get; } = "Clear the console display of all text";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            Console.Clear();
        }

    }
}
