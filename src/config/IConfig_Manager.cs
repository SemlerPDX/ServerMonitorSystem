using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to all global properties used throughout the application,
    /// as well as methods to save/load config file, and apply/reapply/clear command line argument
    /// overrides of the application properties listed in <see cref="IConfigPython"/>.
    /// </summary>
    public interface IConfig_Manager
    {
        /// <summary>
        /// Load all <see cref="IConfigPython"/> properties from the config python file.
        /// </summary>
        void LoadConfig();
        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file, creating file if missing.
        /// </summary>
        void SaveConfig();

        /// <summary>
        /// Clear all active command line arguments overriding <see cref="IConfigPython"/> properties,
        /// including the <see cref="IConfig_Manager.CommandLineArgsInput"/> storage property.
        /// <para>
        /// Runs <see cref="LoadConfig"/> afterwards to completely clear overridden values.
        /// </para>
        /// </summary>
        void ClearCommandLineOverrides();

        /// <summary>
        /// Load command line arguments from the supplied string array.
        /// </summary>
        /// <param name="args">An array of command-line arguments to pass to the program.</param>
        void LoadCommandLineOverrides(string[] args);

        /// <summary>
        /// Reapply command line arguments which were saved to the <see cref="IConfig_Manager.CommandLineArgsInput"/> property on first initialization.
        /// </summary>
        void ReLoadOverrides();
        /// <summary>
        /// Reapply command line arguments based on provided string array input,
        /// and saving them to <see cref="IConfig_Manager.CommandLineArgsInput"/> property.
        /// </summary>
        /// <param name="args">An array of command-line arguments to pass to the program.</param>
        void ReLoadOverrides(string[] args);

        // Command line argument properties and default override notice
        #region commandline_properties
        /// <summary>
        /// Command line arguments this instance was launched with.
        /// </summary>
        string[] CommandLineArgsInput { get; set; }
        /// <summary>
        /// Command line argument names which are currently overriding <see cref="IConfigPython"/> properties.
        /// </summary>
        HashSet<string> CommandLineArgsActive { get; set; }
        /// <summary>
        /// The default message presented when a command is used which sets and saves to file
        /// an <see cref="IConfigPython"/> property that is currently being overridden by a command line argument.
        /// </summary>
        string DEFAULT_OVERRIDE_NOTICE { get; }
        #endregion


        // Console application user command properties
        #region command_properties
        /// <summary>
        /// Array of all <see cref="ICommands"/> console commands which can be used.
        /// </summary>
        string[] CommandNames { get; set; }
        /// <summary>
        /// Array of all <see cref="ICommands"/> console commands showing usage example. (ex. 'interval &lt;int&gt;')
        /// </summary>
        string[] CommandUsages { get; set; }
        /// <summary>
        /// Array of all <see cref="ICommands"/> console command descriptions for the help list.
        /// </summary>
        string[] CommandDescriptions { get; set; }
        #endregion


        // Service class properties holding retrieved information.
        #region service_properties
        /// <summary>
        /// Array containing current memory info. (available, total, used)
        /// </summary>
        float[] CurrentMemoryInfo { get; set; }
        /// <summary>
        /// Array containing memory info at last monitoring interval. (available, total, used)
        /// </summary>
        float[] FormerMemoryInfo { get; set; }
        /// <summary>
        /// Array containing complete current memory info.
        /// </summary>
        string[] CurrentMemoryData { get; set; }
        /// <summary>
        /// Array containing complete memory info at last monitoring interval.
        /// </summary>
        string[] FormerMemoryData { get; set; }
        /// <summary>
        /// A boolean indicating whether total memory usage is currently below
        /// user defined limit in <see cref="MaxMem"/>; false, if not.
        /// </summary>
        bool MemoryBelowMaxMem { get; set; }
        /// <summary>
        /// A boolean indicating whether total memory usage is currently below
        /// user defined Auto-Kill limit in <see cref="KillMem"/>; false, if not.
        /// </summary>
        bool MemoryBelowKillMem { get; set; }

        /// <summary>
        /// Falcon BMS shared memory info of current server Theater Name.
        /// </summary>
        string CurrentTheaterName { get; set; }

        /// <summary>
        /// Falcon BMS shared memory info of current online player count.
        /// </summary>
        int CurrentPlayerCount { get; set; }
        /// <summary>
        /// Falcon BMS shared memory info of online player count at last monitoring interval.
        /// </summary>
        int FormerPlayerCount { get; set; }

        /// <summary>
        /// Falcon BMS shared memory info array of current online player callsigns and status.
        /// </summary>
        string[] CurrentPlayerStatus { get; set; }
        /// <summary>
        /// Falcon BMS shared memory info array of online player callsigns and status at last monitoring interval.
        /// </summary>
        string[] FormerPlayerStatus { get; set; }

        /// <summary>
        /// True when the <see cref="GameServerName"/> process is currently running; false, if offline.
        /// </summary>
        bool ServerOnlineGame { get; set; }
        /// <summary>
        /// True when the <see cref="VoipServerName"/> process is currently running; false, if offline.
        /// </summary>
        bool ServerOnlineVOIP { get; set; }
        #endregion


        // Timer boolean properties to represent current state(s) of timers.
        #region timer_properties
        /// <summary>
        /// True when the memory logging to csv file timer is active; false, if offline.
        /// </summary>
        bool LoggingCsvTimerActive { get; set; }
        /// <summary>
        /// True when the alerts monitoring timer is active; false, if offline.
        /// </summary>
        bool MonitorTimerActive { get; set; }
        /// <summary>
        /// True when the server process running state info timer is active; false, if offline.
        /// </summary>
        bool ServerInfoTimerActive { get; set; }
        /// <summary>
        /// True when the Falcon BMS shared memory info timer is active; false, if offline.
        /// </summary>
        bool FalconBMSInfoTimerActive { get; set; }
        /// <summary>
        /// True when the system memory info timer is active; false, if offline.
        /// </summary>
        bool MemoryInfoTimerActive { get; set; }
        /// <summary>
        /// True when the game server crash alerts cooldown pause timer is active; false, if offline.
        /// </summary>
        bool PauseAlertsGame { get; set; }
        /// <summary>
        /// True when the VOIP server crash alerts cooldown pause timer is active; false, if offline.
        /// </summary>
        bool PauseAlertsVOIP { get; set; }
        /// <summary>
        /// True when the memory leak alerts cooldown pause timer is active; false, if offline.
        /// </summary>
        bool PauseAlertsMEM { get; set; }
        #endregion


        // Config Properties are those which are present in the
        // config python file and ConfigPython class, and can be overridden
        // by matching command line (case insensitive) arguments.
        #region config_properties
        /// <summary>
        /// Server info monitoring interval (in minutes) [default:1 minimum:1 maximum:1440]
        /// </summary>
        int Interval { get; set; }
        /// <summary>
        /// Maximum used RAM after which to issue a Leak Alert (in megabytes) [default:8000 minimum:1]
        /// </summary>
        float MaxMem { get; set; }
        /// <summary>
        /// Toggle Flag for Memory Leak Game Server Auto-Kill system (true/false == ON/OFF)
        /// </summary>
        bool AutoKill { get; set; }
        /// <summary>
        /// Maximum used RAM after which to Auto-Kill Game Server (if ON) (in megabytes) [default:11500 minimum:1]
        /// </summary>
        float KillMem { get; set; }

        /// <summary>
        /// Toggle Flag for ALL Alerts monitoring (console, file, and emails) (true/false == ON/OFF)
        /// </summary>
        bool AlertsALL { get; set; }
        /// <summary>
        /// Toggle Flag for Game Server Crash Alerts monitoring system (true/false == ON/OFF)
        /// </summary>
        bool AlertsGame { get; set; }
        /// <summary>
        /// Toggle Flag for VOIP Server Crash Alerts monitoring system (true/false == ON/OFF)
        /// </summary>
        bool AlertsVOIP { get; set; }
        /// <summary>
        /// Toggle Flag for Memory Leak Alerts monitoring system (true/false == ON/OFF)
        /// </summary>
        bool AlertsMEM { get; set; }

        /// <summary>
        /// Toggle Flag for ALL Email Alerts functions (true/false == ON/OFF)
        /// </summary>
        bool EmailsALL { get; set; }
        /// <summary>
        /// Toggle Flag for Game Server Crash Alerts emails function (true/false == ON/OFF)
        /// </summary>
        bool EmailsGame { get; set; }
        /// <summary>
        /// Toggle Flag for VOIP Server Crash Alerts emails function (true/false == ON/OFF)
        /// </summary>
        bool EmailsVOIP { get; set; }
        /// <summary>
        /// Toggle Flag for Memory Leak Alerts email function (true/false == ON/OFF)
        /// </summary>
        bool EmailsMEM { get; set; }

        /// <summary>
        /// Singular designation for a player in this game (ex. 'Pilot')
        /// </summary>
        string PlayerDesignation { get; }
        /// <summary>
        /// Name of the game server process to monitor, no extension (ex. 'Falcon BMS')
        /// </summary>
        string GameServerName { get; }
        /// <summary>
        /// The default subject text line for a game server crash alert
        /// </summary>
        string GameOfflineAlertSubject { get; }
        /// <summary>
        /// The default message text line for a game server crash alert
        /// </summary>
        string GameOfflineAlertMessage { get; }
        /// <summary>
        /// Name of the VOIP server process to monitor, no extension (ex. 'IVC Server')
        /// </summary>
        string VoipServerName { get; }
        /// <summary>
        /// The default subject text line for a VOIP server crash alert
        /// </summary>
        string VoipOfflineAlertSubject { get; }
        /// <summary>
        /// The default message text line for a VOIP server crash alert
        /// </summary>
        string VoipOfflineAlertMessage { get; }

        // System memory logging systems config properties
        /// <summary>
        /// Toggle Flag for Memory Data Logging to CSV file (true/false == ON/OFF)
        /// </summary>
        bool Logging { get; set; }
        /// <summary>
        /// Minimum time between repeated unresolved Memory Leak Alerts (in minutes) [default:60 minimum:15 maximum:11000]
        /// </summary>
        int MinTime { get; set; }
        /// <summary>
        /// Memory Data Logging to CSV file frequency (in minutes) [default:1 minimum:1 maximum:11000]
        /// </summary>
        int Frequency { get; set; }
        /// <summary>
        /// Memory Data Logging duration (in hours, 0 = endless) [default:0 maximum:11000]
        /// </summary>
        int Duration { get; set; }
        /// <summary>
        /// Memory Logging CSV file path (use literal or escape '\' with doubles, ex. '\\')
        /// </summary>
        string LogFilePath { get; set; }
        /// <summary>
        /// Memory Data Logging max file size per log (in megabytes) [default:1000 minimum:1 maximum:4000]
        /// </summary>
        long LogSize { get; set; }

        // Email Alerts SMTP and Server Group detail properties
        /// <summary>
        /// Admin Names and Email Addresses for email Alerts
        /// (whitespace separator, ex 'name address')
        /// </summary>
        string[] SMTP_Emails { get; set; }
        /// <summary>
        /// The SMTP protocol of this SMTP server
        /// </summary>
        string SMTP_Protocol { get; set; }
        /// <summary>
        /// The address of this SMTP server
        /// </summary>
        string SMTP_Address { get; set; }
        /// <summary>
        /// The password of this SMTP server
        /// </summary>
        string SMTP_Password { get; set; }
        /// <summary>
        /// The port number of this SMTP server
        /// </summary>
        int SMTP_Port { get; set; }
        /// <summary>
        /// The SMTP credentials of this SMTP server (2 or 3 elements only)
        /// </summary>
        string[] SMTP_Credentials { get; set; }
        /// <summary>
        /// The boolean to enable SSL for this SMTP server (default true)
        /// </summary>
        bool SMTP_EnableSsl { get; set; }

        /// <summary>
        /// The displayed 'from' name in alert emails
        /// </summary>
        string EmailServerName { get; set; }
        /// <summary>
        /// The HTML banner img alt value will display Group Name in emails (when banner image not shown/blocked)
        /// </summary>
        string EmailGroupName { get; set; }
        /// <summary>
        /// The default reply-to address displayed in emails (ex. 'noreply@yourgroupname.com')
        /// </summary>
        string EmailReplyName { get; set; }

        /// <summary>
        /// The HTML link address when clicking on the group banner image in emails
        /// </summary>
        string EmailGroupWebsite { get; set; }
        /// <summary>
        /// The HTML img source direct link to the group banner image file
        /// </summary>
        string EmailGroupBanner { get; set; }

        /// <summary>
        /// An HTML img source direct link to a utility spacer image for tables (1px Square)
        /// </summary>
        string EmailSpacer { get; set; }
        /// <summary>
        /// An HTML background color style value for the slightly darker area framing the email text body (default '#f1f1f1')
        /// </summary>
        string EmailBodyBackground_Dark { get; set; }
        /// <summary>
        /// An HTML background color style value for the standard lighter area behind the main email body text (default '#ffffff')
        /// </summary>
        string EmailBodyBackground_Light { get; set; }
        /// <summary>
        /// An HTML color style value of line separating banner image and email body (default '#db2c00')
        /// </summary>
        string EmailBodyLineColor { get; set; }
        /// <summary>
        /// An HTML font color style value of text in email body (default '#333333')
        /// </summary>
        string EmailBodyFontColor { get; set; }
        /// <summary>
        /// An HTML font size style value of slightly larger text in email subject (default 'font-size:17px')
        /// </summary>
        string EmailSubjectFontSize { get; set; }
        /// <summary>
        /// An HTML font size style value of standard text in email body text (default 'font-size:15px')
        /// </summary>
        string EmailBodyFontSize { get; set; }
        #endregion

    }

}
