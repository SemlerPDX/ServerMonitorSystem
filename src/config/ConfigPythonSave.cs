using Microsoft.Scripting.Hosting;
using System.Collections.Generic;
using System.IO;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IConfigPythonSave"/> interface to provide access to a method
    /// for saving or creating the config properties file.
    /// </summary>
    class ConfigPythonSave : IConfigPythonSave
    {
        #region default_comments
        /// <summary>
        /// The header section of the config python file
        /// including program details, editing notes, and links.
        /// </summary>
        private static readonly string[] ConfigPythonHeader =
        {
                "# ############################################# ",
                "#  VG Labs - Server Monitor System - Config File",
                "#    by SemlerPDX  Feb2023   GNU GPL v2.0",
                "# ",
                "# https://github.com/SemlerPDX",
                "# ",
                "#   -IMPORTANT NOTES:",
                "# Change settings in app and run 'save' command",
                "# Most issues can be resolved by deleting this file",
                "# This config will be recreated if missing",
                "# ",
                "# Keep data structures intact if editing manually",
                "# Restart app or run 'reload' command to apply changes",
                "# All can be used as command line argument overrides",
                "#    (case insensitive - see guide on github)",
                "# ",
                "# ############################################# ",
                " "
        };
        /// <summary>
        /// The comments which will appear above the corresponding property
        /// in the config file.
        /// <br>Order of elements in this array must match the <see cref="IConfigPython"/> class.</br>
        /// </summary>
        private static readonly string[] ConfigPythonComments =
        {
            "<int> Server info monitoring interval (in minutes) [default:1 minimum:1 maximum:15]",
            "<float> Maximum used RAM after which to issue a Leak Alert (in megabytes) [default:8000 minimum:1]",
            "<bool> Toggle Memory Leak Game Server Auto-Kill system ON/OFF",
            "<float> Maximum used RAM after which to Auto-Kill Game Server (if ON) (in megabytes) [default:11500 minimum:1]",

            "<bool> Toggles ALL Alerts monitoring (console, file, and emails) ON/OFF",
            "<bool> Toggles only Game Server Crash Alerts monitoring system ON/OFF",
            "<bool> Toggles only VOIP Server Crash Alerts monitoring system ON/OFF",
            "<bool> Toggles only Memory Leak Alerts monitoring system ON/OFF",

            "<bool> Toggles ALL Email Alerts functions ON/OFF",
            "<bool> Toggles only Game Server Crash Alerts emails function ON/OFF",
            "<bool> Toggles only VOIP Server Crash Alerts emails function ON/OFF",
            "<bool> Toggles only Memory Leak Alerts email function ON/OFF",

            "<string> Singular designation for players (surround in single-quotes) -example:  PlayerDesignation = 'Pilot'",
            "<string> Name of the game server process to monitor, no extension  (surround in single-quotes) -example:  GameServerName = 'Falcon BMS'",
            "<string> Game Server crash alert subject line  (surround in single-quotes)  -example:  GameOfflineAlertSubject = 'A Falcon BMS Server CTD Alert has been triggered!'",
            "<string> Game Server crash alert message body  (surround in single-quotes)  -example:  GameOfflineAlertMessage = 'The Falcon BMS Server is not responding, is closed, or has crashed to desktop ... etc...'",
            "<string> Name of the voice server process to monitor, no extension  (surround in single-quotes) -example:  VoipServerName = 'IVC Server'",
            "<string> VOIP Server crash alert subject line  (surround in single-quotes)  -example:  GameOfflineAlertSubject = 'An IVC Server CTD Alert has been triggered!'",
            "<string> VOIP Server crash alert message body  (surround in single-quotes)  -example:  VoipOfflineAlertMessage = 'The Falcon BMS IVC Server is not responding, is closed, or has crashed to desktop ... etc...'",


            "<bool> Toggles Memory Data Logging to CSV file ON/OFF",
            "<int> Minimum time between repeated unresolved Memory Leak Alerts (in minutes) [default:60 minimum:15 maximum:11000])",
            "<int> Set Memory Data Logging to CSV file frequency (in minutes) [default:1 minimum:1 maximum:11000]",
            "<int> Set Memory Data Logging duration (in hours, 0 = endless) [default:0 maximum:11000]",

            @"<string> Memory Logging CSV file path (surround in single-quotes, escape '\' with doubles) -example:  LogFilePath = 'C:\\folder\\filename.csv'",
            "<long> Memory Data Logging max file size per log (in megabytes) [default:1024 minimum:1 maximum:4096]",

            "<string[]> Admin Addresses for email Alerts (comma separated array) -example:  Emails = ('Jack example@gmail.com','Jill otherExample@gmail.com')",
            "<string> Protocol of the outgoing SMTP server (surround in single-quotes) -example:  SMTP_Protocol = 'smtp.gmail.com'",
            "<string> Outgoing SMTP email address to use (surround in single-quotes) -example:  SMTP_Address = 'example@gmail.com'",
            "<string> Outgoing SMTP password (surround in single-quotes) -example:  SMTP_Password = 'example-smtp-password'",
            "<int> SMTP server Port Number -example:  SMTP_Port = 587)",
            "<string[]> SMTP server Credentials (comma separated array of 2 or 3 elements) -example:  SMTP_Credentials = ('example@gmail.com','example-smtp-password')",
            "<bool> Enable/disable SSL for SMTP server (must be Title Case, either True or False) -example:  SMTP_EnableSsl = True",

            "<string> Outgoing email Server Name (surround in single-quotes) -example:  EmailServerName = 'VG Labs Server Bot'",
            "<string> Outgoing email Group Name (surround in single-quotes) -example:  EmailGroupName = 'VETERANS-GAMING'",
            "<string> Outgoing email Reply Address (surround in single-quotes) -example:  EmailReplyName = 'noreply@example.com'",

            "<string> Group website homepage link (surround in single-quotes) -example:  EmailGroupWebsite = 'https://example.com/'",
            "<string> Group banner 750px by 130px is best (surround in single-quotes) -example:  EmailGroupBanner = 'https://example.com/banner.png'",
            "<string> 1px Square HTML spacer image (surround in single-quotes) -example:  EmailSpacer = 'https://example.com/spacer.png'",

            "<string> Grey outer box in email body (surround in single-quotes) -example:  EmailBodyBackground_Dark = '#f1f1f1'",
            "<string> Light inner box in email body (surround in single-quotes) -example:  EmailBodyBackground_Light = '#ffffff'",
            "<string> Color of line separating banner image and email body (surround in single-quotes) -example:  EmailBodyLineColor = '#db2c00'",

            "<string> Font color of text in email body (surround in single-quotes) -example:  EmailBodyFontColor = '#333333'",
            "<string> Font size of text in email subject (surround in single-quotes) -example:  EmailSubjectFontSize = 'font-size:17px'",
            "<string> Font size of text in email body (surround in single-quotes) -example:  EmailBodyFontSize = 'font-size:15px'"

        };
        #endregion

        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file,
        /// or create new file if it does not exist.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the <see cref="IConfigPython"/> properties being
        /// saved to file for the Python Engine.
        /// </param>
        /// <param name="configFilePath">The path to the config file to save.</param>
        public void WriteConfigPythonFile(ScriptScope pythonScope, string configFilePath)
        {
            using var stream = new StreamWriter(configFilePath);
            foreach (string line in ConfigPythonHeader)
            {
                stream.WriteLine(line);
            }

            // Set starting position for comments array
            int i = 0;

            // Write each line to file, adding custom spacing and special comments for manual file editing
            foreach (KeyValuePair<string, object> entry in pythonScope.GetItems())
            {
                if (entry.Key == "SMTP_Emails")
                    stream.WriteLine("#  Max file size per log (in megabytes) should not exceed max attachment size of SMTP host if emails used\r\n");

                if (entry.Key == "EmailServerName")
                    stream.WriteLine("# ==========================================================================================================");

                if (entry.Key == "AlertsALL" ||
                    entry.Key == "EmailsALL" ||
                    entry.Key == "PlayerDesignation" ||
                    entry.Key == "Logging" ||
                    entry.Key == "SMTP_Emails" ||
                    entry.Key == "EmailServerName" ||
                    entry.Key == "EmailGroupBanner" ||
                    entry.Key == "EmailServerName")
                    stream.WriteLine("");

                stream.WriteLine("");
                if (entry.Key == "Logging")
                    stream.WriteLine("#          ( Memory Data Logging is independant of the above systems )");

                if (entry.Key == "EmailServerName")
                    stream.WriteLine("");

                if (entry.Key == "SMTP_Emails")
                    stream.WriteLine("# =============== SMTP Settings - You must enter your own smtp server data here for the email alerts system to work! ===============");

                stream.WriteLine(@"# {0} {1}", entry.Key, ConfigPythonComments[i]);
                stream.WriteLine(@"{0} = {1}", entry.Key, entry.Value);
                i++;
            }
            stream.WriteLine("");
        }

    }
}
