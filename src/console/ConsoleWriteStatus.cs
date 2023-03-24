using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Application Status class implementing <see cref="IConsoleWriteStatus"/> to provide access to 
    /// a method for writing current application status including configuration states to the console.
    /// </summary>
    class ConsoleWriteStatus : IConsoleWriteStatus
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleWriteStatus"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public ConsoleWriteStatus(IConfig_Manager configManager)
        {
            _configManager = configManager;

        }
        private const string LINE0_HEADER = "========Program Status Report========\n";
        private const string HLINE_SEPARATOR = "-------------------------------------";
        private const string HLINE_FOOTER = "=====================================";

        private const string LINE1_TIME = " Report Time: {0}";
        private const string LINE2_PLAYERCOUNT = " Online Pilot Count:   {0}";
        private const string LINE3_AVAILABLEMEM = " Available memory:   {0} (MB)";
        private const string LINE4_TOTALMEM = " Total memory:       {0} (MB)";
        private const string LINE5_USEDMEM = " Used memory:        {0} (MB)";
        private const string LINE6_ISRUNNING_GAME = " Running:   {0}";
        private const string LINE7_ISRUNNING_VOIP = " Running:   {0}";

        /// <summary>
        /// Writes the current application status to the console, including key states
        /// of integral systems such as Alerts, Alert Emails, game/VOIP proccesses,
        /// online player count, system memory data, active memory logging data, etc.
        /// </summary>
        public void WriteProgramStatus()
        {
            // Print current app status data to console
            Console.Write(LINE0_HEADER);

            Console.WriteLine(LINE1_TIME, DateTime.Now);
            Console.WriteLine(LINE2_PLAYERCOUNT, _configManager.CurrentPlayerCount >= 0 ? _configManager.CurrentPlayerCount : "(offline)");
            Console.WriteLine(LINE3_AVAILABLEMEM, _configManager.CurrentMemoryInfo[0] >= 0 ? _configManager.CurrentMemoryInfo[0] : "0");
            Console.WriteLine(LINE4_TOTALMEM, _configManager.CurrentMemoryInfo[1] >= 0 ? _configManager.CurrentMemoryInfo[1] : "0");
            Console.WriteLine(LINE5_USEDMEM, _configManager.CurrentMemoryInfo[2] >= 0 ? _configManager.CurrentMemoryInfo[2] : "0");
            Console.WriteLine(" " + _configManager.GameServerName + LINE6_ISRUNNING_GAME, _configManager.ServerOnlineGame.ToString().ToUpper());
            Console.WriteLine(" " + _configManager.VoipServerName + LINE7_ISRUNNING_VOIP, _configManager.ServerOnlineVOIP.ToString().ToUpper());
            Console.WriteLine(HLINE_SEPARATOR);
            if (_configManager.AlertsMEM && _configManager.AlertsALL)
            {
                string autoKillMB = "\n  -Autokill limit: " + _configManager.KillMem.ToString() + " (MB)";
                Console.WriteLine(" Game Autokill is:      {0}", _configManager.AutoKill ? "ON" + autoKillMB : "OFF");
            }
            Console.WriteLine(" ALL Alerts are:       {0}", _configManager.AlertsALL ? "ON" : "OFF");
            Console.WriteLine("  -GAME Alerts are:    {0}", _configManager.AlertsGame ? "ON" : "OFF");
            Console.WriteLine("  -VOIP Alerts are:    {0}", _configManager.AlertsVOIP ? "ON" : "OFF");
            Console.WriteLine("  -MEM Alerts are:     {0}", _configManager.AlertsMEM ? "ON" : "OFF");

            if (_configManager.AlertsALL)
            {
                Console.WriteLine(" Email Alerts are:     {0}", _configManager.EmailsALL ? "ON" : "OFF");
                Console.WriteLine("  -GAME Emails are:    {0}", _configManager.EmailsGame ? "ON" : "OFF");
                Console.WriteLine("  -VOIP Emails are:    {0}", _configManager.EmailsVOIP ? "ON" : "OFF");
                Console.WriteLine("  -MEM Emails are:     {0}", _configManager.EmailsMEM ? "ON" : "OFF");
            }

            Console.WriteLine(HLINE_SEPARATOR);
            Console.WriteLine(" Memory Logging is:    {0}", _configManager.LoggingCsvTimerActive ? "ON" : "OFF");
            if (_configManager.LoggingCsvTimerActive)
            {
                string hours = String.Format("{0} (hour{1})", _configManager.Duration, _configManager.Duration > 1 ? "s" : "");
                Console.WriteLine("  -Max duration: {0}", _configManager.Duration == 0 ? " (endless)" : hours);
                Console.WriteLine("  -Log frequency: {0} (minute{1})", _configManager.Frequency, _configManager.Frequency > 1 ? "s" : "");
                Console.WriteLine("  -Max log size:  {0} (MB)", _configManager.LogSize);
            }

            Console.WriteLine(HLINE_FOOTER);
        }

    }
}
