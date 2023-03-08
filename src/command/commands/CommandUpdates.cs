namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for checking the app updates database online
    /// and writing update information to the console.
    /// <para>
    /// Dependencies: <see cref="IInfoUpdates"/>
    /// </para>
    /// </summary>
    class CommandUpdates : ICommands
    {
        /// <summary>
        /// The object that retrieves application update info from the online database.
        /// </summary>
        private readonly IInfoUpdates _infoUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandUpdates"/> class with the specified interface.
        /// </summary>
        /// <param name="infoUpdates">The object that retrieves application update info from the online database.</param>
        public CommandUpdates(IInfoUpdates infoUpdates)
        {
            _infoUpdates = infoUpdates;
        }


        #region command_parameters
        public string Name { get; } = "updates";
        public string Usage { get; } = "updates";
        public string Description { get; } = "Check for updates to this application (console print-out only)";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _infoUpdates.CheckForUpdates();
        }

    }
}
