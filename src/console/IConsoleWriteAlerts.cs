namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method which writes server monitoring alerts to the console.
    /// </summary>
    public interface IConsoleWriteAlerts
    {
        /// <summary>
        /// Write server monitor alerts to the console including game/VOIP CTD and excessive system memory usage alerts.
        /// </summary>
        /// <param name="alert">The subject line pertaining to this alert type.</param>
        /// <param name="alertEmails">A comma-separated string of names/labels which emails for this alert were sent to, null if none or otherwise.</param>
        void WriteServerAlerts(string alert, string alertEmails);
    }
}
