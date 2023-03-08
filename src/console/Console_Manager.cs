using System;
using System.Collections.Generic;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The main Console Read/Write Manager class implementing <see cref="IConfig_Manager"/> to provide
    /// access to methods which write data to and read data from the console.
    /// </summary>
    class Console_Manager : IConsole_Manager
    {
        /// <summary>
        /// The object that handles writing the user prompt and prompt symbols to the console.
        /// </summary>
        private readonly IConsoleWritePrompts _consoleWritePrompts;
        /// <summary>
        /// The object that handles writing program configuration status information to the console.
        /// </summary>
        private readonly IConsoleWriteStatus _consoleWriteStatus;
        /// <summary>
        /// The object that handles writing server monitoring alerts to the console.
        /// </summary>
        private readonly IConsoleWriteAlerts _consoleWriteAlerts;
        /// <summary>
        /// The object that handles writing help information to the console.
        /// </summary>
        private readonly IConsoleWriteHelpList _consoleWriteHelp;
        /// <summary>
        /// The object that handles list writing to the console.
        /// </summary>
        private readonly IConsoleWriteList _consoleWriteList;
        /// <summary>
        /// The object that handles reading and allowing numeric input only into the console.
        /// </summary>
        private readonly IConsoleReadNumbers _consoleReadNumbers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Console_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleWritePrompts">The object that handles writing the user prompt and prompt symbols to the console.</param>
        /// <param name="consoleWriteStatus">The object that handles writing program configuration status information to the console.</param>
        /// <param name="consoleWriteAlerts">The object that handles writing server monitoring alerts to the console.</param>
        /// <param name="consoleWriteHelp">The object that handles writing help information to the console.</param>
        /// <param name="consoleWriteList">The object that handles list writing to the console.</param>
        /// <param name="consoleReadNumbers">The object that handles reading and allowing numeric input only into the console.</param>
        public Console_Manager(IConsoleWritePrompts consoleWritePrompts,
                        IConsoleWriteStatus consoleWriteStatus,
                        IConsoleWriteAlerts consoleWriteAlerts,
                        IConsoleWriteHelpList consoleWriteHelp,
                        IConsoleWriteList consoleWriteList,
                        IConsoleReadNumbers consoleReadNumbers)
        {
            _consoleWritePrompts = consoleWritePrompts;
            _consoleWriteStatus = consoleWriteStatus;
            _consoleWriteAlerts = consoleWriteAlerts;
            _consoleWriteHelp = consoleWriteHelp;
            _consoleWriteList = consoleWriteList;
            _consoleReadNumbers = consoleReadNumbers;
        }

        public const string PROMPT_NUMBER = "Enter a numeric value";
        public const string PROMPT_CHOICE = "Enter a number choice";
        public const string PROMPT_ANYKEY = "  Press Any Key to Continue - type 'help' anytime to view commands list...";

        /// <summary>
        /// Write a default user input prompt and get general alphanumeric input from the console.
        /// </summary>
        /// <returns>The user input typed into the console.</returns>
        public string GetInputText() { return GetInputText(null); }
        /// <summary>
        /// Write a customized user input prompt and get general alphanumeric input from the console.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        /// <returns>The user input typed into the console.</returns>
        public string GetInputText(string promptText)
        {
            _consoleWritePrompts.WritePrompt(promptText);
            return Console.ReadLine();
        }

        /// <summary>
        /// Write a default user input prompt and get console commands input from the console.
        /// </summary>
        /// <returns>The console command users have typed into the console.</returns>
        public string GetInputCommand()
        {
            _consoleWritePrompts.WritePrompt();
            return Console.ReadLine();
        }

        /// <summary>
        /// Write a default user input prompt to the console directing users to input a numeric value,
        /// allowing only numeric entry into the console, and return that number when users press 'Enter'.
        /// </summary>
        /// <returns>The number(s) entered by the user; -1, if none or otherwise.</returns>
        public int GetInputNumber()
        {
            _consoleWritePrompts.WritePrompt(PROMPT_NUMBER);
            return _consoleReadNumbers.ReadNumberInput();
        }

        /// <summary>
        /// Require a numeric choice selection which allows only numeric entry into the console
        /// up to the maximum number of elements in the supplied choices string array, and
        /// return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choices">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <returns>The number(s) entered by the user pertaining to a non-zero based choice of an item in the supplied list; -1, if none or otherwise.</returns>
        public int GetInputChoice(string[] choices) { return GetInputChoice(choices, null); }
        /// <summary>
        /// Write a custom user input prompt to the console, and present a numeric choice selection
        /// which allows only numeric entry into the console up to the maximum number of elements
        /// in the supplied choices string array, and return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choices">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <param name="promptText">The text to display to the left of the cursor input prompt directing users to input number(s) pertaining to a choice.</param>
        /// <returns>The number(s) entered by the user pertaining to a non-zero based choice of an item in the supplied list; -1, if none or otherwise.</returns>
        public int GetInputChoice(string[] choices, string promptText)
        {
            _consoleWritePrompts.WritePrompt(promptText ?? PROMPT_CHOICE);
            return _consoleReadNumbers.ReadChoiceInput(choices);
        }

        /// <summary>
        /// Writes a default user input prompt directing users to press any key to continue and waits for any keypress to return.
        /// </summary>
        public void GetAnyKeyContinue()
        {
            // Enter spacer below user input prompt
            Console.WriteLine();  // cursor will be here
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(0, (Console.CursorTop - 3));// Move cursor up three lines

            _consoleWritePrompts.WritePrompt(PROMPT_ANYKEY, false);
            Console.ReadKey();
        }

        /// <summary>
        /// A "help list only" console read mode which will clear the console,
        /// rewrite the help list, and read user input for a method waiting for an 'exit' command.
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <param name="commandsHelp">A string array of all command descriptions which will appear in the help list.</param>
        /// <returns>The user input typed into the console.</returns>
        public string GetExitCommand(string[] commands, string[] commandsHelp)
        {
            Console.Clear();
            WriteHelp(commands, commandsHelp, false);
            return Console.ReadLine();
        }

        /// <summary>
        /// Clear the current line in the console by overwriting it with whitespaces up to
        /// the maximum console width, and returning the cursor to the 0 position.
        /// </summary>
        public void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            int consoleWidth = Console.WindowWidth;
            Console.SetCursorPosition(0, currentLineCursor);
            for (int i = 0; i < consoleWidth; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// Writes the current application status to the console, including key states
        /// of integral systems such as Alerts, Alert Emails, game/VOIP proccesses,
        /// online player count, system memory data, active memory logging data, etc.
        /// </summary>
        public void WriteStatus()
        {
            _consoleWriteStatus.WriteProgramStatus();
        }

        /// <summary>
        /// Writes the given list of key-value pairs to the console, with a given title.
        /// </summary>
        /// <param name="title">The title to display above the list of items.</param>
        /// <param name="keyValuePairs">The list of key-value pairs to display.</param>
        public void WriteList(string title, Dictionary<string, string> keyValuePairs)
        {
            _consoleWriteList.WriteDictionaryList(title, keyValuePairs);
        }

        /// <summary>
        /// Write all user commands and descriptions to the console, and then (optionally) writes a new user input prompt.
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandUsages"/>, <seealso cref="IConfig_Manager.CommandDescriptions"/>,
        /// and <seealso cref="ICommand_Manager.LoadCommands()"/>)
        /// </para>
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <param name="commandsHelp">A string array of all command descriptions which will appear in the help list.</param>
        /// <param name="newPrompt">A boolean indicating if a new prompt should be displayed for user input after writing the help list.</param>
        public void WriteHelp(string[] commands, string[] commandsHelp, bool newPrompt)
        {
            _consoleWriteHelp.WriteHelpInfo(commands, commandsHelp);

            if (newPrompt)
                _consoleWritePrompts.WritePrompt();
        }

        /// <summary>
        /// Clear the line, write server monitor alerts to the alerts log and to the console, the reprint the user input prompt.
        /// </summary>
        /// <param name="alert">The subject line pertaining to this alert type.</param>
        /// <param name="alertEmails">A comma-separated string of names/labels which emails for this alert were sent to, null if none or otherwise.</param>
        public void WriteAlert(string alert, string alertEmails)
        {
            // Clear current user input prompt line
            ClearLine();

            // Log to file
            Exceptions.LogEvents(alert);

            // NOTE:  Call the WriteFunction passing inputs
            _consoleWriteAlerts.WriteServerAlerts(alert, alertEmails);

            // Reprint user input prompt
            _consoleWritePrompts.WritePrompt();
        }

    }
}
