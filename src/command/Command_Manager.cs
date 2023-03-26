using System;
using System.Collections.Generic;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The main command manager class which processes user input console commands which match listed <see cref="ICommands"/>.
    /// <para>
    /// Dependencies: <see cref="IConsole_Manager"/>, <see cref="IConfig_Manager"/>, <see cref="IEmail_Manager"/>,
    /// <br><see cref="ITimer_Manager"/>, <see cref="IInfoFalconBMS"/>, <see cref="IInfoUpdates"/></br>
    /// </para>
    /// </summary>
    class Command_Manager : ICommand_Manager
    {
        /// <summary>
        /// A list containing each command class implementating <see cref="ICommands"/>
        /// </summary>
        private readonly List<ICommands> _commands;

        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that managers SMTP email alerts for the system.
        /// </summary>
        private readonly IEmail_Manager _emailManager;
        /// <summary>
        /// The object that manages the various timers for the system.
        /// </summary>
        private readonly ITimer_Manager _timerManager;
        /// <summary>
        /// The object that retrieves Falcon BMS server info from shared memory.
        /// </summary>
        private readonly IInfoFalconBMS _infoFalconBMS;
        /// <summary>
        /// The object that retrieves application update info from the online database.
        /// </summary>
        private readonly IInfoUpdates _infoUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="emailManager">The object that managers SMTP email alerts for the system.</param>
        /// <param name="timerManager">The object that manages the various timers for the system.</param>
        /// <param name="infoFalconBMS">The object that retrieves Falcon BMS server info from shared memory.</param>
        /// <param name="infoUpdates">The object that retrieves application update info from the online database.</param>
        public Command_Manager(IConsole_Manager consoleManager,
                          IConfig_Manager configManager,
                          IEmail_Manager emailManager,
                          ITimer_Manager timerManager,
                          IInfoFalconBMS infoFalconBMS,
                          IInfoUpdates infoUpdates)
        {
            _consoleManager = consoleManager;
            _configManager = configManager;
            _emailManager = emailManager;
            _timerManager = timerManager;
            _infoFalconBMS = infoFalconBMS;
            _infoUpdates = infoUpdates;

            // All ICommand classes with their specified interfaces, add new commands here:
            _commands = new List<ICommands>
            {
                new CommandReloadApp(_configManager, _timerManager),
                new CommandReport(_consoleManager),
                new CommandStop(_timerManager),
                new CommandStart(_configManager, _timerManager),
                //new CommandInterval(_configManager, _consoleManager),

                //new CommandAlerts(_configManager),
                //new CommandAlertsGame(_configManager),
                //new CommandAlertsVOIP(_configManager),
                //new CommandAlertsMEM(_configManager),

                //new CommandEmails(_configManager),
                //new CommandEmailsGame(_configManager),
                //new CommandEmailsVOIP(_configManager),
                //new CommandEmailsMEM(_configManager),

                new CommandEmailList(_consoleManager, _emailManager),
                new CommandEmailAdd(_configManager, _consoleManager),
                new CommandEmailDelete(_configManager, _emailManager, _consoleManager),
                new CommandEmailTest(_consoleManager, _emailManager),

                //new CommandMaxMem(_configManager, _consoleManager),
                //new CommandMinTime(_configManager, _consoleManager),

                //new CommandAutoKill(_configManager),
                //new CommandAutoKillMaxMem(_configManager, _consoleManager),

                new CommandLogging(_configManager, _timerManager),
                //new CommandLoggingStart(_configManager, _timerManager),
                //new CommandLoggingStop(_configManager, _timerManager),

                //new CommandFrequency(_configManager, _consoleManager),
                //new CommandDuration(_configManager, _consoleManager),
                //new CommandLogSize(_configManager, _consoleManager),

                //new CommandClear(),
                //new CommandUpdates(_infoUpdates),
                new CommandPlayers(_infoFalconBMS),
                new CommandHelp(_configManager, _consoleManager),
                //new CommandHelpList(_configManager, _consoleManager)
                new CommandExit(),
            };

        }

        /// <summary>
        /// The default message displayed when user enters a command not represented in <see cref="ICommands"/>.
        /// </summary>
        private const string DEFAULT_ERROR = " -Error: the above input is not valid";


        /// <summary>
        /// Reapply command line overrides (if any) to <see cref="IConfigPython"/> properties.
        /// </summary>
        private void ReApplyOverrides() { _configManager.ReLoadOverrides(); }

        /// <summary>
        /// Reload all <see cref="IConfigPython"/> properties from the config file.
        /// </summary>
        private void ReLoadConfig() { _configManager.LoadConfig(); }


        /// <summary>
        /// Execute the provided command with the given user input as a string array.
        /// </summary>
        /// <param name="command">The command object to execute.</param>
        /// <param name="args">The user input from console split at whitespace into a string array.</param>
        private void ExecuteCommand(ICommands command, string[] args)
        {
            bool hasOverrides = (command.ConfigSetting && _configManager.CommandLineArgsActive != null);

            // Clear Command Line Overrides in anticipation of new saved property
            if (hasOverrides)
                ReLoadConfig();

            // Run this command
            command.Execute(args);

            // Reload Command Line Overrides (if any)
            if (hasOverrides)
                ReApplyOverrides();
        }

        /// <summary>
        /// Post process the user input to present invalid command text in console if not 'exit' or whitespace.
        /// </summary>
        /// <param name="processedInput">The user input text from the console.</param>
        private void PostProcessCommand(string processedInput)
        {
            processedInput = processedInput.ToLower();
            if (processedInput != "exit" && !String.IsNullOrEmpty(processedInput))
                Console.WriteLine(DEFAULT_ERROR);
        }

        /// <summary>
        /// Evaluate user command input against all <see cref="ICommands"/>, execute only if allowed.
        /// </summary>
        /// <param name="input">The user input text from the console.</param>
        public void ProcessCommand(string input)
        {
            string[] args = input.Split();
            bool validCommand = false;

            foreach (ICommands command in _commands)
            {
                try
                {
                    if (command.CanExecute(args))
                    {
                        validCommand = true;
                        ExecuteCommand(command, args);
                        break;
                    }
                }
                catch (Exceptions ex)
                {
                    ex.LogError("Error at ProcessCommand: " + command.Name);
                }
            }

            if (!validCommand)
                PostProcessCommand(input);
        }


        /// <summary>
        /// Load all <see cref="ICommands"/> name, usage, and description properties
        /// to <see cref="IConfig_Manager"/> command properties.
        /// </summary>
        public void LoadCommands()
        {
            List<string> commandNames = new();
            List<string> commandUsages = new();
            List<string> commandDescriptions = new();
            foreach (ICommands command in _commands)
            {
                commandNames.Add(command.Name);
                commandUsages.Add(command.Usage);
                commandDescriptions.Add(command.Description);
            }
            _configManager.CommandNames = commandNames.ToArray();
            _configManager.CommandUsages = commandUsages.ToArray();
            _configManager.CommandDescriptions = commandDescriptions.ToArray();
        }

    }
}
