namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to config properties loaded from and saved to file
    /// </summary>
    public interface IConfigPython
    {
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
        string PlayerDesignation { get; set; }
        /// <summary>
        /// Name of the game server process to monitor, no extension (ex. 'Falcon BMS')
        /// </summary>
        string GameServerName { get; set; }
        /// <summary>
        /// The default subject text line for a game server crash alert
        /// </summary>
        string GameOfflineAlertSubject { get; set; }
        /// <summary>
        /// The default message text line for a game server crash alert
        /// </summary>
        string GameOfflineAlertMessage { get; set; }
        /// <summary>
        /// Name of the VOIP server process to monitor, no extension (ex. 'IVC Server')
        /// </summary>
        string VoipServerName { get; set; }
        /// <summary>
        /// The default subject text line for a VOIP server crash alert
        /// </summary>
        string VoipOfflineAlertSubject { get; set; }
        /// <summary>
        /// The default message text line for a VOIP server crash alert
        /// </summary>
        string VoipOfflineAlertMessage { get; set; }

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
    }

}
