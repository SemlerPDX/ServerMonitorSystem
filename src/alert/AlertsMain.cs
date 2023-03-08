namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IAlertsMain"/> interface to provide
    /// access to the main <see cref="IAlerts"/> alert triggered action(s) method.
    /// <para>
    /// Dependencies: <see cref="IConsole_Manager"/>, <see cref="IEmail_Manager"/>
    /// </para>
    /// </summary>
    class AlertsMain : IAlertsMain
    {
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that managers SMTP email alerts for the system.
        /// </summary>
        private readonly IEmail_Manager _emailManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsMain"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="emailManager">The object that managers SMTP email alerts for the system.</param>
        public AlertsMain(IConsole_Manager consoleManager,
                          IEmail_Manager emailManager)
        {
            _consoleManager = consoleManager;
            _emailManager = emailManager;
        }

        /// <summary>
        /// Process <see cref="IAlerts"/> triggered alert action(s) using the provided subject &amp; message, and sending email(s) if requested.
        /// </summary>
        /// <param name="subject">The subject text of this alert.</param>
        /// <param name="message">The message text of this alert.</param>
        /// <param name="sendEmail">A boolean flag to indicate whether this alert should send email(s).</param>
        public void Main(string subject, string message, bool sendEmail)
        {
            // A string of names/labels belonging to email addresses which alert emails were sent to
            string emailResult = "";

            if (sendEmail)
                emailResult = _emailManager.SendAlertEmails(subject, message);

            _consoleManager.WriteAlert(subject, emailResult);
        }
    }
}
