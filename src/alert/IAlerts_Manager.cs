namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method to iterate through alerts classes which implement
    /// <see cref="IAlerts"/> and evaluate trigger conditions to execute <see cref="IAlertsMain"/> actions.
    /// </summary>
    public interface IAlerts_Manager
    {
        /// <summary>
        /// Processes <see cref="IAlerts"/> based on server information, triggering an alert only if their condition(s) are met.
        /// </summary>
        /// <param name="sendEmails">A boolean array indicating whether each alert should send an email.</param>
        void ProcessAlerts(bool[] sendEmails);
    }
}
