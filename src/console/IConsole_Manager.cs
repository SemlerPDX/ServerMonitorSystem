using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods which write data to and read data from the console.
    /// </summary>
    public interface IConsole_Manager
    {
        /// <summary>
        /// Write a default user input prompt and get general alphanumeric input from the console.
        /// </summary>
        /// <returns>The user input typed into the console.</returns>
        string GetInputText();
        /// <summary>
        /// Write a customized user input prompt and get general alphanumeric input from the console.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        /// <returns>The user input typed into the console.</returns>
        string GetInputText(string promptText);

        /// <summary>
        /// Write a default user input prompt and get console commands input from the console.
        /// </summary>
        /// <returns>The console command users have typed into the console.</returns>
        string GetInputCommand();

        /// <summary>
        /// Write a default user input prompt to the console directing users to input a numeric value,
        /// allowing only numeric entry into the console, and return that number when users press 'Enter'.
        /// </summary>
        /// <returns>The number(s) entered by the user; -1, if none or otherwise.</returns>
        int GetInputNumber();

        /// <summary>
        /// Require a numeric choice selection which allows only numeric entry into the console
        /// up to the maximum number of elements in the supplied choices string array, and
        /// return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choices">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <returns>The number(s) entered by the user pertaining to a non-zero based choice of an item in the supplied list; -1, if none or otherwise.</returns>
        int GetInputChoice(string[] choices);
        /// <summary>
        /// Write a custom user input prompt to the console, and present a numeric choice selection
        /// which allows only numeric entry into the console up to the maximum number of elements
        /// in the supplied choices string array, and return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choices">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <param name="promptText">The text to display to the left of the cursor input prompt directing users to input number(s) pertaining to a choice.</param>
        /// <returns>The number(s) entered by the user pertaining to a non-zero based choice of an item in the supplied list; -1, if none or otherwise.</returns>
        int GetInputChoice(string[] choices, string promptText);

        /// <summary>
        /// Writes a default user input prompt directing users to press any key to continue and waits for any keypress to return.
        /// </summary>
        void GetAnyKeyContinue();

        /// <summary>
        /// A "help list only" console read mode which will clear the console,
        /// rewrite the help list, and read user input for a method waiting for an 'exit' command.
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <param name="commandsHelp">A string array of all command descriptions which will appear in the help list.</param>
        /// <returns>The user input typed into the console.</returns>
        string GetExitCommand(string[] commands, string[] commandsHelp);

        /// <summary>
        /// Clear the current line in the console by overwriting it with whitespaces up to
        /// the maximum console width, and returning the cursor to the 0 position.
        /// </summary>
        void ClearLine();

        /// <summary>
        /// Writes the current application status to the console, including key states
        /// of integral systems such as Alerts, Alert Emails, game/VOIP proccesses,
        /// online player count, system memory data, active memory logging data, etc.
        /// </summary>
        void WriteStatus();

        /// <summary>
        /// Writes the given list of key-value pairs to the console, with a given title.
        /// </summary>
        /// <param name="title">The title to display above the list of items.</param>
        /// <param name="keyValuePairs">The list of key-value pairs to display.</param>
        void WriteList(string title, Dictionary<string, string> keyValuePairs);

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
        void WriteHelp(string[] commands, string[] commandsHelp, bool newPrompt);

        /// <summary>
        /// Clear the line, write server monitor alerts to the alerts log and to the console, the reprint the user input prompt.
        /// </summary>
        /// <param name="alert">The subject line pertaining to this alert type.</param>
        /// <param name="alertEmails">A comma-separated string of names/labels which emails for this alert were sent to, null if none or otherwise.</param>
        void WriteAlert(string alert, string alertEmails);
    }
}
