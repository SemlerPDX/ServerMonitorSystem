using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The managing class for global application properties implementing <see cref="IConfig_Manager"/> to provide access to methods for
    /// <br>saving/loading them from file, and for processing command line arguments as overrides for <see cref="IConfigPython"/> properties.</br>
    /// <para>
    /// Dependencies: <see cref="IConfigCommandLine"/>, <see cref="IConfigPython"/>, <see cref="IConfigPythonFileHandler"/>
    /// </para>
    /// </summary>
    class Config_Manager : IConfig_Manager
    {
        /// <summary>
        /// The object that processes command line arguments provided to the program.
        /// </summary>
        private readonly IConfigCommandLine _configCommandLine;
        /// <summary>
        /// The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.
        /// </summary>
        private readonly IConfigPython _configPython;
        /// <summary>
        /// The object that provides read and write methods for the config python file.
        /// </summary>
        private readonly IConfigPythonFileHandler _configPythonFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="Config_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configCommandLine">The object that processes command line arguments provided to the program.</param>
        /// <param name="configPython">The object that provides all <see cref="IConfigPython"/> properties as numbered attributes.</param>
        /// <param name="configPythonFile">The object that provides read and write methods for the config python file.</param>
        public Config_Manager(IConfigCommandLine configCommandLine,
                          IConfigPython configPython,
                          IConfigPythonFileHandler configPythonFile)
        {
            _configCommandLine = configCommandLine;
            _configPython = configPython;
            _configPythonFile = configPythonFile;
        }


        // Command line argument properties and default override notice
        #region commandline_properties
        public string[] CommandLineArgsInput { get; set; }
        public HashSet<string> CommandLineArgsActive { get; set; }
        public string DEFAULT_OVERRIDE_NOTICE { get; } = " -NOTE: This property is currently being overridden by a command line argument!";
        #endregion

        // Console application user command properties
        #region command_properties
        public string[] CommandNames { get; set; }
        public string[] CommandUsages { get; set; }
        public string[] CommandDescriptions { get; set; }
        #endregion


        // Service class properties holding retrieved information.
        #region service_properties
        public float[] CurrentMemoryInfo { get; set; }
        public float[] FormerMemoryInfo { get; set; }
        public string[] CurrentMemoryData { get; set; }
        public string[] FormerMemoryData { get; set; }
        public bool MemoryBelowMaxMem { get; set; } = true;
        public bool MemoryBelowKillMem { get; set; } = true;

        public string CurrentTheaterName { get; set; }

        public int CurrentPlayerCount { get; set; } = 0;
        public int FormerPlayerCount { get; set; } = 0;

        public string[] CurrentPlayerStatus { get; set; }
        public string[] FormerPlayerStatus { get; set; }

        public bool ServerOnlineGame { get; set; } = false;
        public bool ServerOnlineVOIP { get; set; } = false;
        #endregion


        // Timer boolean properties to represent current state(s) of timers.
        #region timer_properties
        public bool MonitorTimerActive { get; set; }

        public bool LoggingCsvTimerActive { get; set; }

        public bool ServerInfoTimerActive { get; set; }
        public bool FalconBMSInfoTimerActive { get; set; }
        public bool MemoryInfoTimerActive { get; set; }

        public bool PauseAlertsGame { get; set; }
        public bool PauseAlertsVOIP { get; set; }
        public bool PauseAlertsMEM { get; set; }
        #endregion


        // Config Properties are those which are present in the
        // config python file, and can be overridden by matching
        // command line (case insensitive) arguments.
        #region config_properties
        public int Interval { get => _configPython.Interval; set => _configPython.Interval = value; }
        public float MaxMem { get => _configPython.MaxMem; set => _configPython.MaxMem = value; }
        public bool AutoKill { get => _configPython.AutoKill; set => _configPython.AutoKill = value; }
        public float KillMem { get => _configPython.KillMem; set => _configPython.KillMem = value; }

        public bool AlertsALL { get => _configPython.AlertsALL; set => _configPython.AlertsALL = value; }
        public bool AlertsGame { get => _configPython.AlertsGame; set => _configPython.AlertsGame = value; }
        public bool AlertsVOIP { get => _configPython.AlertsVOIP; set => _configPython.AlertsVOIP = value; }
        public bool AlertsMEM { get => _configPython.AlertsMEM; set => _configPython.AlertsMEM = value; }

        public bool EmailsALL { get => _configPython.EmailsALL; set => _configPython.EmailsALL = value; }
        public bool EmailsGame { get => _configPython.EmailsGame; set => _configPython.EmailsGame = value; }
        public bool EmailsVOIP { get => _configPython.EmailsVOIP; set => _configPython.EmailsVOIP = value; }
        public bool EmailsMEM { get => _configPython.EmailsMEM; set => _configPython.EmailsMEM = value; }

        public string PlayerDesignation { get => _configPython.PlayerDesignation; set => _configPython.PlayerDesignation = value; }
        public string GameServerName { get => _configPython.GameServerName; set => _configPython.GameServerName = value; }
        public string GameOfflineAlertSubject { get => _configPython.GameOfflineAlertSubject; set => _configPython.GameOfflineAlertSubject = value; }
        public string GameOfflineAlertMessage { get => _configPython.GameOfflineAlertMessage; set => _configPython.GameOfflineAlertMessage = value; }
        public string VoipServerName { get => _configPython.VoipServerName; set => _configPython.VoipServerName = value; }
        public string VoipOfflineAlertSubject { get => _configPython.VoipOfflineAlertSubject; set => _configPython.VoipOfflineAlertSubject = value; }
        public string VoipOfflineAlertMessage { get => _configPython.VoipOfflineAlertMessage; set => _configPython.VoipOfflineAlertMessage = value; }

        public bool Logging { get => _configPython.Logging; set => _configPython.Logging = value; }
        public int MinTime { get => _configPython.MinTime; set => _configPython.MinTime = value; }
        public int Frequency { get => _configPython.Frequency; set => _configPython.Frequency = value; }
        public int Duration { get => _configPython.Duration; set => _configPython.Duration = value; }
        public string LogFilePath { get => _configPython.LogFilePath; set => _configPython.LogFilePath = value; }
        public long LogSize { get => _configPython.LogSize; set => _configPython.LogSize = value; }


        public string[] SMTP_Emails { get => _configPython.SMTP_Emails; set => _configPython.SMTP_Emails = value; }
        public string SMTP_Protocol { get => _configPython.SMTP_Protocol; set => _configPython.SMTP_Protocol = value; }
        public string SMTP_Address { get => _configPython.SMTP_Address; set => _configPython.SMTP_Address = value; }
        public string SMTP_Password { get => _configPython.SMTP_Password; set => _configPython.SMTP_Password = value; }
        public int SMTP_Port { get => _configPython.SMTP_Port; set => _configPython.SMTP_Port = value; }
        public string[] SMTP_Credentials { get => _configPython.SMTP_Credentials; set => _configPython.SMTP_Credentials = value; }
        public bool SMTP_EnableSsl { get => _configPython.SMTP_EnableSsl; set => _configPython.SMTP_EnableSsl = value; }

        public string EmailServerName { get => _configPython.EmailServerName; set => _configPython.EmailServerName = value; }
        public string EmailGroupName { get => _configPython.EmailGroupName; set => _configPython.EmailGroupName = value; }
        public string EmailReplyName { get => _configPython.EmailReplyName; set => _configPython.EmailReplyName = value; }

        public string EmailGroupWebsite { get => _configPython.EmailGroupWebsite; set => _configPython.EmailGroupWebsite = value; }
        public string EmailGroupBanner { get => _configPython.EmailGroupBanner; set => _configPython.EmailGroupBanner = value; }
        public string EmailSpacer { get => _configPython.EmailSpacer; set => _configPython.EmailSpacer = value; }

        public string EmailBodyBackground_Dark { get => _configPython.EmailBodyBackground_Dark; set => _configPython.EmailBodyBackground_Dark = value; }
        public string EmailBodyBackground_Light { get => _configPython.EmailBodyBackground_Light; set => _configPython.EmailBodyBackground_Light = value; }
        public string EmailBodyLineColor { get => _configPython.EmailBodyLineColor; set => _configPython.EmailBodyLineColor = value; }
        public string EmailBodyFontColor { get => _configPython.EmailBodyFontColor; set => _configPython.EmailBodyFontColor = value; }
        public string EmailSubjectFontSize { get => _configPython.EmailSubjectFontSize; set => _configPython.EmailSubjectFontSize = value; }
        public string EmailBodyFontSize { get => _configPython.EmailBodyFontSize; set => _configPython.EmailBodyFontSize = value; }
        #endregion


        /// <summary>
        /// Load all <see cref="IConfigPython"/> properties from the config python file.
        /// </summary>
        public void LoadConfig()
        {
            _configPythonFile.LoadPythonConfig();
        }

        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file.
        /// </summary>
        public void SaveConfig()
        {
            _configPythonFile.SavePythonConfig();
        }

        /// <summary>
        /// Clear all active command line arguments overriding <see cref="IConfigPython"/> properties,
        /// including the <see cref="IConfig_Manager.CommandLineArgsInput"/> storage property.
        /// <para>
        /// Runs <see cref="LoadConfig"/> afterwards to completely clear overridden values.
        /// </para>
        /// </summary>
        public void ClearCommandLineOverrides()
        {
            // Providing null arguments will clear storage properties for current arguments
            LoadCommandLineOverrides(null);

            // Reloading the config file will clear any property values overriden by command line arguments
            _configPythonFile.LoadPythonConfig();
        }

        /// <summary>
        /// Load command line arguments from the supplied string array.
        /// </summary>
        /// <param name="args">An array of command-line arguments to pass to the program.</param>
        public void LoadCommandLineOverrides(string[] args)
        {
            CommandLineArgsActive = _configCommandLine.Parse(args);
        }

        /// <summary>
        /// Reapply command line arguments which were saved to the
        /// <see cref="IConfig_Manager.CommandLineArgsInput"/> property on first initialization.
        /// </summary>
        public void ReLoadOverrides() { ReLoadOverrides(null); }
        /// <summary>
        /// Reapply command line arguments based on provided string array input,
        /// and store them in the <see cref="IConfig_Manager.CommandLineArgsInput"/> &amp; <see cref="IConfig_Manager.CommandLineArgsActive"/> properties.
        /// </summary>
        /// <param name="args">An array of command-line arguments to pass to the program.</param>
        public void ReLoadOverrides(string[] args)
        {
            if (args == null && CommandLineArgsActive.Count > 0)
                args = CommandLineArgsInput;

            _configCommandLine.Parse(args);
        }

    }
}
