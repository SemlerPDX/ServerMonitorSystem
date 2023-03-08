using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Console Alerts Writer class implementing <see cref="IConsoleWriteAlerts"/> for providing access to
    /// a method which writes server monitoring alerts to the console.
    /// </summary>
    class ConsoleWriteAlerts : IConsoleWriteAlerts
    {
        private const string HLINE_ALERTS = "======================================================================";

        /// <summary>
        /// Write server monitor alerts to the console including game/VOIP CTD and excessive system memory usage alerts.
        /// </summary>
        /// <param name="alert">The subject line pertaining to this alert type.</param>
        /// <param name="alertEmails">A comma-separated string of names/labels which emails for this alert were sent to, null if none or otherwise.</param>
        public void WriteServerAlerts(string alert, string alertEmails)
        {
            string alertsFormat = "[{0}] - {1}";
            string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string timestamp = DateTime.Now.ToString(DateTimeFormat);

            // Enter spacer from last text/output
            Console.WriteLine();
            Console.WriteLine(HLINE_ALERTS);

            // Print new alert text to console
            Console.WriteLine(alertsFormat, timestamp, alert);

            if (!String.IsNullOrWhiteSpace(alertEmails))
                Console.WriteLine(alertsFormat, timestamp, alertEmails);

            Console.WriteLine(HLINE_ALERTS);
        }

    }
}
