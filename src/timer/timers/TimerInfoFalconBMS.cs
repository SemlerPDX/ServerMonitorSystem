using System;
using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Falcon BMS Info Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for the Falcon BMS server shared memory information systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IInfoServer"/>
    /// </para>
    /// </summary>
    class TimerInfoFalconBMS : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that retrieves Falcon BMS server info from shared memory.
        /// </summary>
        private readonly IInfoFalconBMS _infoFalconBMS;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerInfoFalconBMS"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="infoFalconBMS">The object that retrieves Falcon BMS server info from shared memory.</param>
        public TimerInfoFalconBMS(IConfig_Manager configManager,
                          IInfoFalconBMS infoFalconBMS)
        {
            _configManager = configManager;
            _infoFalconBMS = infoFalconBMS;
        }


        private System.Timers.Timer _timer;
        public string Name { get; } = "falconbms";
        public string Type { get; } = "services";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.FalconBMSInfoTimerActive && timer.ToLower() == Name;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            TimerElapsed(null, null);

            // Add a 1 minute delay to BMS info timer to allow alerts to reflect former server state
            double sharedMemInterval = Convert.ToDouble((_configManager.Interval + 1) * 60 * 1000);

            _configManager.FalconBMSInfoTimerActive = true;
            _timer = new System.Timers.Timer(sharedMemInterval);
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.FalconBMSInfoTimerActive = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            int playerCount = _infoFalconBMS.GetPilotCount();

            // Set current player count to former player count property before updating CurrentPlayerCount
            _configManager.FormerPlayerCount = _configManager.CurrentPlayerCount;
            _configManager.CurrentPlayerCount = playerCount >= 1 ? playerCount - 1 : 0; // subtract 1 for server plane

            // Set current player data to former player data property before updating CurrentPlayerStatus
            _configManager.FormerPlayerStatus = _configManager.CurrentPlayerStatus;
            _configManager.CurrentPlayerStatus = _infoFalconBMS.GetPilotData();

            // Set the current theater name property for the active BMS server
            _configManager.CurrentTheaterName = _infoFalconBMS.GetTheaterName();
        }

    }
}
