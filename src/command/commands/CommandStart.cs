using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface providing
    /// access to a method for starting the alerts monitoring systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="ITimer_Manager"/>
    /// </para>
    /// </summary>
    class CommandStart : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the various timers for the system.
        /// </summary>
        private readonly ITimer_Manager _timerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStart"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        public CommandStart(IConfig_Manager configManager,
                          ITimer_Manager timerManager)
        {
            _configManager = configManager;
            _timerManager = timerManager;
        }


        #region command_parameters
        public string Name { get; } = "start";
        public string Usage { get; } = "start";
        public string Description { get; } = "Start Server Alert monitoring systems";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _timerManager.StartTimers("alerts", _configManager.Interval, false);
            Console.WriteLine(" -Server Alerts Monitoring is now enabled{0}",
                _configManager.AlertsALL ? "." : ", but AlertsALL is currently disabled!");
        }

    }
}
