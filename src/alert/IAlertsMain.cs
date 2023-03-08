namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to the main <see cref="IAlerts"/> alert triggered action(s) method.
    /// </summary>
    public interface IAlertsMain
    {
        /// <summary>
        /// Process <see cref="IAlerts"/> triggered alert action(s) using the provided subject and message, sending email(s) if requested.
        /// </summary>
        /// <param name="subject">The subject text of this alert.</param>
        /// <param name="message">The message text of this alert.</param>
        /// <param name="sendEmail">A boolean flag to indicate whether this alert should send email(s).</param>
        void Main(string subject, string message, bool sendEmail);
    }
}
