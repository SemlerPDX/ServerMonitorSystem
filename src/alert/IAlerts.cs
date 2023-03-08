namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to Alert classes which implement the <see cref="IAlerts"/>
    /// interface, and to their methods used to evalute and handle server alert triggers.
    /// </summary>
    public interface IAlerts
    {
        /// <summary>
        /// The name of the alert object.
        /// <para>
        ///   -Options: (game, voip, mem)
        /// </para>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Evaluates if this alert should be triggered.
        /// </summary>
        /// <returns>True if alert conditions are met for this alert; false, if otherwise.</returns>
        bool CanTriggerAlert();

        /// <summary>
        /// Execute the alert triggered action(s) method for this alert using <see cref="IAlertsMain"/>.
        /// </summary>
        /// <param name="playerCount">The former online player count before this alert triggered.</param>
        /// <param name="sendEmail">A boolean flag to indicate whether this alert should send SMTP email(s).</param>
        /// <param name="alertFlag">A boolean flag used as needed to pass a dynamic state from event
        /// handlers back to this <see cref="IAlerts.AlertTriggered"/> method.</param>
        void AlertTriggered(int playerCount, bool sendEmail, bool alertFlag);
    }
}
