namespace ServerMonitorSystem
{
    /// <summary>
    /// An Alerts class which implements the <see cref="IAlerts"/> interface
    /// providing access to methods for evaluating and executing system memory usage alerts.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IAlertsMain"/>
    /// </para>
    /// </summary>
    class AlertsMEM : IAlerts
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The main alerts triggered interface providing trigger action(s) for
        /// alert classes implementing <see cref="IAlerts"/>.
        /// </summary>
        private readonly IAlertsMain _alertsMain;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsMEM"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="alertsMain">The main alerts triggered interface providing trigger action(s) for
        /// alert classes implementing <see cref="IAlerts"/>.</param>
        public AlertsMEM(IConfig_Manager configManager,
                          IAlertsMain alertsMain)
        {
            _configManager = configManager;
            _alertsMain = alertsMain;
        }


        public string Name { get; } = "mem";

        public bool CanTriggerAlert()
        {
            return !_configManager.MemoryBelowMaxMem && !_configManager.PauseAlertsMEM;
        }

        public void AlertTriggered(int playerCount, bool sendEmail, bool alertFlag)
        {
            string playerDesignation = $"{_configManager.PlayerDesignation}{(playerCount != 1 ? "s" : "")}";
            string wasOrWere = alertFlag ? (playerCount == 1 ? "was" : "were") : "are";
            string killNotice = alertFlag ? $" and the {_configManager.GameServerName} Process has been terminated as instructed." : "";

            string subject = $"A System Memory Alert has been triggered! {playerCount} {playerDesignation} {wasOrWere} online.";
            string message = $"Memory Usage has exceeded alert limits{killNotice}<br> - Please check server or contact additional support staff.";

            _alertsMain.Main(subject, message, sendEmail);
        }

    }
}
