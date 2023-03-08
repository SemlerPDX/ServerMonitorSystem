using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Memory Info Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for the performance &amp; memory information systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IInfoMemory"/>
    /// </para>
    /// </summary>
    class TimerInfoMemory : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that retrieves memory info for the system.
        /// </summary>
        private readonly IInfoMemory _infoMemory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerInfoMemory"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="infoMemory">The object that retrieves memory info for the system.</param>
        public TimerInfoMemory(IConfig_Manager configManager,
                          IInfoMemory infoMemory)
        {
            _configManager = configManager;
            _infoMemory = infoMemory;
        }

        private const double DEFAULT_INTERVAL = 10000D;

        private System.Timers.Timer _timer;
        public string Name { get; } = "memory";
        public string Type { get; } = "services";


        public bool CanStartTimer(string timer)
        {
            return !_configManager.MemoryInfoTimerActive && timer.ToLower() == Name;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            TimerElapsed(null, null);
            _configManager.MemoryInfoTimerActive = true;
            _timer = new System.Timers.Timer(DEFAULT_INTERVAL);
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.MemoryInfoTimerActive = false;
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _configManager.FormerMemoryInfo = _configManager.CurrentMemoryInfo != null && _configManager.CurrentMemoryInfo.Length > 0
                ? _configManager.CurrentMemoryInfo
                : _infoMemory.GetMemoryInfo();

            _configManager.FormerMemoryData = _configManager.CurrentMemoryData != null && _configManager.CurrentMemoryData.Length > 0
                ? _configManager.CurrentMemoryData
                : _infoMemory.GetMemoryData();

            _configManager.CurrentMemoryInfo = _infoMemory.GetMemoryInfo();
            _configManager.CurrentMemoryData = _infoMemory.GetMemoryData();
        }

    }
}
