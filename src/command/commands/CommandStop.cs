using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface providing
    /// access to a method for stopping the alerts monitoring systems.
    /// <para>
    /// Dependencies: <see cref="ITimer_Manager"/>
    /// </para>
    /// </summary>
    class CommandStop : ICommands
    {
        /// <summary>
        /// The object that manages the various timers for the system.
        /// </summary>
        private readonly ITimer_Manager _timerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStop"/> class with the specified interface.
        /// </summary>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        public CommandStop(ITimer_Manager timerManager)
        {
            _timerManager = timerManager;
        }


        #region command_parameters
        public string Name { get; } = "stop";
        public string Usage { get; } = "stop";
        public string Description { get; } = "Stop Server Alert monitoring systems";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 1 && args[0].ToLower() == Name;
        }

        public void Execute(string[] args)
        {
            _timerManager.StopTimers("alerts");
            Console.WriteLine(" -Server Alerts Monitoring is now disabled.");
        }

    }
}
