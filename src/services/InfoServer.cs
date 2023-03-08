using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Server Process service class implementing <see cref="IInfoServer"/> to provide
    /// methods which check for game and VOIP server process running state info.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IApplicationsInterface"/>
    /// </para>
    /// </summary>
    /// <remarks>
    /// NOTE: I would like to expand functionality for multiple servers
    /// of the same name, by a list of process target paths or somehow
    /// provide a means to differentiate and monitor multiple instances.
    /// -Sem Feb2023
    /// </remarks>
    class InfoServer : IInfoServer
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that provides application information and methods to the system.
        /// </summary>
        private readonly IApplicationsInterface _applications;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoServer"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="applications">The object that provides application information and methods to the system.</param>
        public InfoServer(IConfig_Manager configManager, IApplicationsInterface applications)
        {
            _configManager = configManager;
            _applications = applications;
        }


        /// <summary>
        /// Checks if the game server set in the <see cref="IConfig_Manager.GameServerName"/> property is running.
        /// </summary>
        /// <returns>True if the game server process is running; otherwise, false.</returns>
        public bool ServerOnlineGame()
        {
            try
            {
                return _applications.ProcessIsRunning(_configManager.GameServerName);
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at InfoServer in ServerOnlineGame method");
            }
            return false;
        }

        /// <summary>
        /// Checks if the VOIP server set in the <see cref="IConfig_Manager.VoipServerName"/> property is running.
        /// </summary>
        /// <returns>True if the VOIP server process is running; otherwise, false.</returns>
        public bool ServerOnlineVOIP()
        {
            try
            {
                return _applications.ProcessIsRunning(_configManager.VoipServerName);
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at InfoServer in ServerOnlineVOIP method");
            }
            return false;
        }

    }
}
