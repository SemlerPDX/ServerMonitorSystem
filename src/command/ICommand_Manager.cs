namespace ServerMonitorSystem
{
    /// <summary>
    /// Console command manager interface providing access
    /// to <see cref="ICommands"/> processing and properties loading methods.
    /// </summary>
    public interface ICommand_Manager
    {
        /// <summary>
        /// Evaluate user command input against all <see cref="ICommands"/>, execute only if allowed.
        /// </summary>
        /// <param name="input">The user input text from the console.</param>
        void ProcessCommand(string input);

        /// <summary>
        /// Load all <see cref="ICommands"/> name, usage, and description properties
        /// to <see cref="IConfig_Manager"/> command properties.
        /// </summary>
        void LoadCommands();
    }
}
