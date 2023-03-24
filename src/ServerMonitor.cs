using System;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The main operations class for the Server Monitor System.
    /// <para>
    /// Dependencies: <see cref="IInit_Manager"/>, <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>,
    /// <br><see cref="ICommand_Manager"/>, <see cref="ITimerPause_Manager"/></br>
    /// </para>
    /// </summary>
    class ServerMonitor
    {
        /// <summary>
        /// The object that manages the initialization of the system.
        /// </summary>
        private readonly IInit_Manager _initManager;
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that manages the user commands of the system.
        /// </summary>
        private readonly ICommand_Manager _commandManager;
        /// <summary>
        /// The object that manages the alert timers "cooldown" pausing for the timers system.
        /// </summary>
        private readonly ITimerPause_Manager _timerPauseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerMonitor"/> class with the specified interfaces.
        /// </summary>
        /// <param name="initManager">The object that manages the initialization of the system.</param>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="commandManager">The object that manages the user commands of the system.</param>
        /// <param name="timerPauseManager">The object that manages the alert timers "cooldown" pausing for the timers system.</param>
        public ServerMonitor(IInit_Manager initManager,
                          IConfig_Manager configManager,
                          IConsole_Manager consoleManager,
                          ICommand_Manager commandManager,
                          ITimerPause_Manager timerPauseManager)
        {
            _initManager = initManager;
            _configManager = configManager;
            _consoleManager = consoleManager;
            _commandManager = commandManager;
            _timerPauseManager = timerPauseManager;
        }

        /// <summary>
        /// Initialize all console command names, usages, and descriptions
        /// </summary>
        private void InitializeCommandProperties()
        {
            _commandManager.LoadCommands();
        }

        /// <summary>
        /// Run the console application in a perpetual help list display mode using <see cref="IConsoleWriteHelpList.WriteHelpInfo"/>
        /// <para>
        /// Mouse selection and copy will be allowed, CTRL+C will not exit the console app.
        /// </para>
        /// <para>
        /// Pressing 'Enter' will clear console and reprint help list, 'exit' will close the help list app.
        /// </para>
        /// </summary>
        public void RunHelp()
        {
            InitializeCommandProperties();

            string userInput;
            do
            {
                userInput = _consoleManager.GetExitCommand(_configManager.CommandUsages, _configManager.CommandDescriptions);
            }
            while (userInput != "exit");

            return;
        }

        /// <summary>
        /// Initialize application commands, systems and configuration, and run main loop.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        public void Run(string[] args)
        {
            // Start the application
            try
            {
                InitializeCommandProperties();
                _initManager.InitializeApp(args);

                string userInput;
                do
                {
                    userInput = _consoleManager.GetInputCommand();
                    _commandManager.ProcessCommand(userInput ?? "");
                }
                while (userInput != "exit") ;

                CloseApp();
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error in Program Main");
                Environment.Exit(ex.HResult);
            }
        }

        /// <summary>
        /// Close this application with exit code 0.
        /// </summary>
        public void CloseApp() { CloseApp(0); }
        /// <summary>
        /// Close this application with the supplied exit code.
        /// </summary>
        /// <param name="exitCode">The exit code to register when closing the application.</param>
        public void CloseApp(int exitCode)
        {
            _initManager.UnInitializeApp();
            _timerPauseManager.StopAllTimers();
            Environment.Exit(exitCode);
        }

    }
}
