using System;
using System.Threading;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Servers Alert Monitoring initialization class implementing <see cref="IInitServers"/> to
    /// provide access to a method which will wait for game and/or VOIP server process(es) to be
    /// running before intialization proceeds (when alerts are enabled for them), to prevent
    /// immediately issuing game/VOIP process crash alerts.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class InitServers : IInitServers
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitServers"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public InitServers(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }

        private const string DEFAULT_WAIT_TEXT = "[{0}] - Now waiting for {1} Server to start...";
        private const string DEFAULT_FAIL_TEST = "\n[{0}] - Wait limit exceeded! Proceeding without VOIP Server running...";
        private const string DEFAULT_WAIT_DONE = " VOIP Server is now running!";

        private const string DEFAULT_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// When <see cref="IConfig_Manager.AlertsALL"/> is True, waits for game
        /// and/or VOIP server process(es) to be running before intialization proceeds,
        /// to prevent immediately issuing game/VOIP process crash alerts.
        /// <para>
        /// (individual wait methods for processes only wait if <see cref="IConfig_Manager.AlertsGame"/>
        /// or <see cref="IConfig_Manager.AlertsVOIP"/> are true, respectively.)
        /// </para>
        /// </summary>
        public void Wait()
        {
            if (_configManager.AlertsALL)
            {
                Console.WriteLine();

                // Do not proceed until Game Server is running...
                WaitForGame();

                // Try to wait for VOIP Server process, give up after 5 minutes
                WaitForVOIP();
            }
        }

        /// <summary>
        /// Waits for the game server named in <see cref="IConfig_Manager.GameServerName"/> to be running,
        /// but only if <see cref="IConfig_Manager.AlertsGame"/> is True.
        /// </summary>
        private void WaitForGame()
        {
            if (!_configManager.AlertsGame)
                return;

            string timestamp = DateTime.Now.ToString(DEFAULT_TIME_FORMAT);
            Console.Write(DEFAULT_WAIT_TEXT, timestamp, _configManager.GameServerName.Replace("Server", ""));

            while (!_configManager.ServerOnlineGame)
                Thread.Sleep(1000);

            Console.WriteLine();
        }

        /// <summary>
        /// Waits for the VOIP server named in <see cref="IConfig_Manager.VoipServerName"/> to be running,
        /// but only if <see cref="IConfig_Manager.AlertsVOIP"/> is True.
        /// </summary>
        private void WaitForVOIP()
        {
            if (!_configManager.AlertsVOIP)
                return;

            int counter = 0;
            int counterMax = 100; // 100 x 3000ms = 5 min

            string timestamp = DateTime.Now.ToString(DEFAULT_TIME_FORMAT);
            Console.Write(DEFAULT_WAIT_TEXT, timestamp, _configManager.VoipServerName.Replace(" Server", ""));

            bool waitLimitExceeded = false;
            while (!_configManager.ServerOnlineVOIP)
            {
                Thread.Sleep(3000);

                // Increment counter, 5 minutes max
                counter++;
                if (counter >= counterMax)
                {
                    waitLimitExceeded = true;
                    break;
                }
            }

            timestamp = DateTime.Now.ToString(DEFAULT_TIME_FORMAT);
            string proceeding = String.Format(DEFAULT_FAIL_TEST, timestamp);
            Console.Write(waitLimitExceeded ? proceeding : DEFAULT_WAIT_DONE);
            Console.WriteLine("\n");
        }
    }
}
