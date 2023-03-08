namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for checking any online pilots callsigns and
    /// their status, and writing that information to the console.
    /// <para>
    /// Dependencies: <see cref="IInfoFalconBMS"/>
    /// </para>
    /// </summary>
    class CommandPlayers : ICommands
    {
        /// <summary>
        /// The object that retrieves Falcon BMS server info from shared memory.
        /// </summary>
        private readonly IInfoFalconBMS _infoFalconBMS;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandPlayers"/> class with the specified interface.
        /// </summary>
        /// <param name="infoFalconBMS">The object that retrieves Falcon BMS server info from shared memory.</param>
        public CommandPlayers(IInfoFalconBMS infoFalconBMS)
        {
            _infoFalconBMS = infoFalconBMS;
        }


        #region command_parameters
        public string Name { get; } = "pilots";
        public string Usage { get; } = "pilots";
        public string Description { get; } = "Display list of online Pilot callsigns & their status (if any)";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _infoFalconBMS.PilotStatusReadout();
        }

    }
}
