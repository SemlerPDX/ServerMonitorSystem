namespace ServerMonitorSystem
{
    /// <summary>
    /// An Alerts class which implements the <see cref="IAlerts"/> interface
    /// providing access to methods for evaluating and executing VOIP server crash alerts.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IAlertsMain"/>
    /// </para>
    /// </summary>
    class AlertsVOIP : IAlerts
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
        /// Initializes a new instance of the <see cref="AlertsVOIP"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="alertsMain">The main alerts triggered interface providing trigger action(s) for
        /// alert classes implementing <see cref="IAlerts"/>.</param>
        public AlertsVOIP(IConfig_Manager configManager,
                          IAlertsMain alertsMain)
        {
            _configManager = configManager;
            _alertsMain = alertsMain;
        }


        public string Name { get; } = "voip";

        public bool CanTriggerAlert()
        {
            return _configManager.ServerOnlineGame && !_configManager.ServerOnlineVOIP && !_configManager.PauseAlertsVOIP;
        }

        public void AlertTriggered(int playerCount, bool sendEmail, bool alertFlag)
        {
            string playerDesignation = $"{_configManager.PlayerDesignation}{(playerCount != 1 ? "s" : "")}";
            string subject = $"{_configManager.VoipOfflineAlertSubject} {playerCount} {playerDesignation} {(playerCount == 1 ? "was" : "were")} online";

            string message = $"{_configManager.VoipOfflineAlertMessage}<br>" +
                                "If you are unable to restart the server, please contact additional support team members.";

            _alertsMain.Main(subject, message, sendEmail);
        }

    }
}
