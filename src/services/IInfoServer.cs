namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods which check for game and VOIP server process running state info.
    /// </summary>
    public interface IInfoServer
    {
        /// <summary>
        /// Checks if the game server set in the <see cref="IConfig_Manager.GameServerName"/> property is running.
        /// </summary>
        /// <returns>True if the game server process is running; otherwise, false.</returns>
        bool ServerOnlineGame();

        /// <summary>
        /// Checks if the VOIP server set in the <see cref="IConfig_Manager.VoipServerName"/> property is running.
        /// </summary>
        /// <returns>True if the VOIP server process is running; otherwise, false.</returns>
        bool ServerOnlineVOIP();
    }
}
