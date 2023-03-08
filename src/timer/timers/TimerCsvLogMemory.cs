using System.Timers;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Logging Timer class implementing <see cref="ITimers"/> to provide access to
    /// timer controls for the Memory Logging systems.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="ILogFileHandler"/>
    /// </para>
    /// </summary>
    class TimerCsvLogMemory : ITimers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that handles logging memory information to CSV file for the system.
        /// </summary>
        private readonly ILogFileHandler _logFileHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerCsvLogMemory"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="logFileHandler">The object that handles logging memory information to CSV file for the system.</param>
        public TimerCsvLogMemory(IConfig_Manager configManager,
                          ILogFileHandler logFileHandler)
        {
            _configManager = configManager;
            _logFileHandler = logFileHandler;
        }

        private System.Timers.Timer _timer;
        public string Name { get; } = "logging";
        public string Type { get; } = "systems";

        public int LoggingIteration { get; set; }
        public int LoggingMaxIterations { get; set; }


        public bool CanStartTimer(string timer)
        {
            return !_configManager.LoggingCsvTimerActive && timer.ToLower() == Name && _configManager.Logging;
        }

        public void StartTimer(string timer, int minutes, bool timerFlag)
        {
            TimerElapsed(null, null);

            LoggingIteration = 0;
            LoggingMaxIterations = _configManager.Duration * 60;

            if (LoggingMaxIterations > 0)
                LoggingMaxIterations = LoggingMaxIterations >= minutes ? LoggingMaxIterations / minutes : 1;

            _configManager.LoggingCsvTimerActive = true;
#if DEBUG
            _timer = new System.Timers.Timer(15000);
#else
            _timer = new System.Timers.Timer(System.Convert.ToDouble(minutes * 60 * 1000));
#endif
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _configManager.LoggingCsvTimerActive = false;
            _timer?.Stop();
            _timer?.Dispose();
        }
        /// Issues here with null memData??
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logFileHandler.LogDataToFile(_configManager.CurrentMemoryData, _configManager.LogFilePath, _configManager.LogSize);
            if (LoggingMaxIterations > 0)
            {
                LoggingIteration++;
                if (LoggingIteration >= LoggingMaxIterations)
                    StopTimer();
            }
        }

    }
}
