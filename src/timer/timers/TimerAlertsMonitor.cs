using System;
using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Alerts Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for the Alerts Monitoring systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IAlerts_Manager"/>
    /// </para>
    /// </summary>
    class TimerAlertsMonitor : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the server alerts for the system.
        /// </summary>
        private readonly IAlerts_Manager _alertsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerAlertsMonitor"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="alertsManager">The object that manages the server alerts for the system.</param>
        public TimerAlertsMonitor(IConfig_Manager configManager,
                          IAlerts_Manager alertsManager)
        {
            _configManager = configManager;
            _alertsManager = alertsManager;
        }

        private System.Timers.Timer _timer;
        public string Name { get; } = "alerts";
        public string Type { get; } = "systems";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.MonitorTimerActive && timer.ToLower() == Name && _configManager.AlertsALL;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            _configManager.MonitorTimerActive = true;
            _timer = new System.Timers.Timer(Convert.ToDouble(minutes * 60 * 1000));
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.MonitorTimerActive = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            bool[] sendEmails =
            {
                _configManager.EmailsALL && _configManager.EmailsGame && !_configManager.PauseAlertsGame,
                _configManager.EmailsALL && _configManager.EmailsVOIP && !_configManager.PauseAlertsVOIP,
                _configManager.EmailsALL && _configManager.EmailsMEM && !_configManager.PauseAlertsMEM
            };
            _alertsManager.ProcessAlerts(sendEmails);
        }

    }
}
