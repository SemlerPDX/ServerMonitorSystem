using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Server Alerts manager class which implements the <see cref="IAlerts_Manager"/> interface
    /// for iterating through classes implementing <see cref="IAlerts"/> and communicating
    /// alert events based on individual alert trigger conditions.
    /// <para>
    /// Dependencies: <see cref="IAlertsMain"/>, <see cref="IConfig_Manager"/>, <see cref="ITimerPause_Manager"/>
    /// </para>
    /// </summary>
    class Alerts_Manager : IAlerts_Manager
    {
        /// <summary>
        /// A list containing each Alert class implementing <see cref="IAlerts"/>.
        /// </summary>
        private readonly List<IAlerts> _alerts;

        /// <summary>
        /// The main alerts triggered interface providing trigger action(s) for
        /// alert classes implementing <see cref="IAlerts"/>.
        /// </summary>
        private readonly IAlertsMain _alertsMain;
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The timer pause manager for limiting repeated alerts for the same event from firing.
        /// </summary>
        private readonly ITimerPause_Manager _timerPauseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alerts_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="alertsMain">The main alerts triggered interface providing trigger actions for the Alerts classes.</param>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="timerPauseManager">The timer pause manager for limiting repeated alerts for the same event from firing.</param>
        public Alerts_Manager(IAlertsMain alertsMain,
                              IConfig_Manager configManager,
                              ITimerPause_Manager timerPauseManager)
        {
            _alertsMain = alertsMain;
            _configManager = configManager;
            _timerPauseManager = timerPauseManager;


            // All IAlerts classes with their specified interfaces, add new alerts here:
            _alerts = new List<IAlerts>
            {
                new AlertsGame(_configManager, _alertsMain),
                new AlertsVOIP(_configManager, _alertsMain),
                new AlertsMEM(_configManager, _alertsMain)
            };
        }

        /// <summary>
        /// Processes <see cref="IAlerts"/> based on server information, triggering an alert only if their condition(s) are met.
        /// </summary>
        /// <param name="sendEmails">A boolean array indicating whether each alert should send an email.</param>
        public void ProcessAlerts(bool[] sendEmails)
        {
            int sendEmailsIndex = 0;

            foreach (IAlerts alert in _alerts)
            {
                if (alert.CanTriggerAlert())
                {
                    // Execute the alert triggered action(s) for this particular alert
                    alert.AlertTriggered(_configManager.FormerPlayerCount, sendEmails[sendEmailsIndex], _configManager.AutoKill);

                    // Begin an alert timer cooldown for this alert name to prevent redundant repeated alerts for the same event
                    _timerPauseManager.StartTimers(alert.Name, _configManager.MinTime, false);
                    break;
                }
                sendEmailsIndex++;
            }
        }

    }
}
