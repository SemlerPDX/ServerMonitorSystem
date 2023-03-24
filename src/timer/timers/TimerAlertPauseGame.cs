using System;
using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Cooldown Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for pausing Game server crash alerts until conditions are met.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class TimerAlertPauseGame : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerAlertPauseGame"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public TimerAlertPauseGame(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }

        private System.Timers.Timer _timer;

        private const double DEFAULT_INTERVAL = 60000D; // default 1 minute interval (60000)
        public string Name { get; } = "game";
        public string Type { get; } = "cooldown";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.PauseAlertsGame && timer.ToLower() == Name;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("DEBUG: Game Cooldown Timer ENGAGED!");
#endif
            _configManager.PauseAlertsGame = true;
            _timer = new System.Timers.Timer(DEFAULT_INTERVAL);
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("DEBUG: Game Cooldown Timer completed!");
#endif
            _configManager.PauseAlertsGame = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_configManager.ServerOnlineGame)
                StopTimer();
        }

    }
}
