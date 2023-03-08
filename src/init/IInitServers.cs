namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method which will wait for game and/or VOIP server process(es) to be
    /// running before intialization proceeds (when alerts are enabled for them), to prevent
    /// immediately issuing game/VOIP process crash alerts.
    /// </summary>
    public interface IInitServers
    {
        /// <summary>
        /// When <see cref="IConfig_Manager.AlertsALL"/> is True, waits for game
        /// and/or VOIP server process(es) to be running before intialization proceeds,
        /// to prevent immediately issuing game/VOIP process crash alerts.
        /// <para>
        /// (individual wait methods for processes only wait if <see cref="IConfig_Manager.AlertsGame"/>
        /// or <see cref="IConfig_Manager.AlertsVOIP"/> are true, respectively.)
        /// </para>
        /// </summary>
        void Wait();
    }
}
