namespace ServerMonitorSystem
{
    /// <summary>
    /// Console commands interface providing access to command properties and exection methods.
    /// <para>
    /// (see also <seealso cref="ICommand_Manager.LoadCommands()"/> &amp; <seealso cref="ICommand_Manager.ProcessCommand(string)"/>)
    /// </para>
    /// </summary>
    public interface ICommands
    {
        /// <summary>
        /// The user input text corresponding to this command.
        /// </summary>
        public string Name { get; } // ex:  "interval"

        /// <summary>
        /// The usage format of this command for the help menu list.
        /// </summary>
        public string Usage { get; } // ex:  "interval <int>"

        /// <summary>
        /// The description of this command for the help menu list.
        /// </summary>
        public string Description { get; } // ex:  "Set Alerts monitoring interval (in minutes, default 5)"

        /// <summary>
        /// A boolean flag indicating whether this command saves a <see cref="IConfigPython"/> property to file; false when it does not.
        /// </summary>
        bool ConfigSetting { get; } // ex. true when command saves a config property

        /// <summary>
        /// Evaluate if this command based on the supplied user input.
        /// </summary>
        /// <param name="args">The user input from console split at whitespace into a string array.</param>
        bool CanExecute(string[] args);

        /// <summary>
        /// Run this command with the supplied user input.
        /// </summary>
        /// <param name="args">The user input from console split at whitespace into a string array.</param>
        void Execute(string[] args);
    }
}
