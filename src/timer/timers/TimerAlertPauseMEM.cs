using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Cooldown Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for pausing Memory Leak alerts until conditions are met.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class TimerAlertPauseMEM : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerAlertPauseMEM"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public TimerAlertPauseMEM(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }

        private bool MemoryAlertsCooldownHastened { get; set; }

        private System.Timers.Timer _timer;
        public string Name { get; } = "mem";
        public string Type { get; } = "cooldown";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.PauseAlertsMEM && timer.ToLower() == Name;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            if (_timer.Enabled)
                StopTimer();

            _configManager.PauseAlertsMEM = true;
            MemoryAlertsCooldownHastened = timerFlag;
#if DEBUG
            _timer = new System.Timers.Timer(15000D); // NOTE: 15 second development and testing interval
#else
            _timer = new System.Timers.Timer(System.Convert.ToDouble(minutes * 60 * 1000));
#endif
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.PauseAlertsMEM = false;
            MemoryAlertsCooldownHastened = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_configManager.MemoryBelowMaxMem)
            {
                StopTimer();
            }
            else
            {
                if (!MemoryAlertsCooldownHastened)
                    StartTimer(null, 1, true);
            }
        }
    }
}
