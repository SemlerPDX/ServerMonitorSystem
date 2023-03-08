using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Server Process Info Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for the game and voip server process running status information systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IInfoServer"/>
    /// </para>
    /// </summary>
    class TimerInfoServer : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that retrieves server process info for the system.
        /// </summary>
        private readonly IInfoServer _infoServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerInfoServer"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="infoServer">The object that retrieves server process info for the system.</param>
        public TimerInfoServer(IConfig_Manager configManager,
                          IInfoServer infoServer)
        {
            _configManager = configManager;
            _infoServer = infoServer;
        }

        private const double DEFAULT_INTERVAL = 5000D;

        private System.Timers.Timer _timer;
        public string Name { get; } = "server";
        public string Type { get; } = "services";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.ServerInfoTimerActive && timer.ToLower() == Name;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            TimerElapsed(null, null);
            _configManager.ServerInfoTimerActive = true;
            _timer = new System.Timers.Timer(DEFAULT_INTERVAL);
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.ServerInfoTimerActive = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _configManager.ServerOnlineGame = _infoServer.ServerOnlineGame();
            _configManager.ServerOnlineVOIP = _infoServer.ServerOnlineVOIP();
        }

    }
}
