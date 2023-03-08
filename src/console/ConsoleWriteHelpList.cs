using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Console Help List Writer class implementing <see cref="IConsoleWriteHelpList"/> to provide access
    /// to the method for writing a user commands help list to the console.
    /// </summary>
    class ConsoleWriteHelpList : IConsoleWriteHelpList
    {
        private const string HEADER_LEFT = " -- COMMAND";
        private const string HEADER_RIGHT = "-      DESCRIPTION --";
        private const string HLINE_OUTER = "----------------------------------------------------------";
        private const string HLINE_INNER = "  ------------------------------------------------------";
        private const string FOOTER_NOTE = "  --( review log files regularly! )--";

        /// <summary>
        /// Method to determine the maximum whitespace count to use for separating
        /// command usages on the left from command descriptions on the right.
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <returns>The maximum whitespace count determined by length of longest command usage.</returns>
        private int CalculateWhitespaceLength(string[] commands)
        {
            int maxSpaces = 0;
            int checkSpaces;
            foreach (string command in commands)
            {
                checkSpaces = command.Length + 4;
                if (checkSpaces > maxSpaces)
                    maxSpaces = checkSpaces;
            }
            return maxSpaces;
        }

        /// <summary>
        /// Method to determine the whitespace count to use for dividing this
        /// command usage on the left from its counterpart command descriptions on the right.
        /// </summary>
        /// <param name="maxSpaces">The maximum number of whitespaces which can be used to separate text.</param>
        /// <returns>A string of whitespaces to place after this command and before its description.</returns>
        private string GetWhiteSpaces(int maxSpaces)
        {
            string whiteSpaces = "";

            for (int s = 0; s < maxSpaces; s++)
                whiteSpaces += " ";

            return whiteSpaces;
        }

        /// <summary>
        /// Write all user commands and descriptions to the console with a common whitespace gap between each column of text.
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandUsages"/>, <seealso cref="IConfig_Manager.CommandDescriptions"/>,
        /// and <seealso cref="ICommand_Manager.LoadCommands()"/>)
        /// </para>
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <param name="commandsHelp">A string array of all command descriptions which will appear in the help list.</param>
        public void WriteHelpInfo(string[] commands, string[] commandsHelp)
        {
            int whiteSpaceCount;
            int maxSpaces = CalculateWhitespaceLength(commands);
            string whiteSpaces = GetWhiteSpaces(maxSpaces);

            Console.WriteLine(HLINE_OUTER);
            Console.WriteLine(HEADER_LEFT + whiteSpaces + HEADER_RIGHT);
            Console.WriteLine(HLINE_INNER);

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] != null && commandsHelp[i] != null)
                {
                    whiteSpaceCount = maxSpaces - commands[i].Length;
                    whiteSpaces = GetWhiteSpaces(whiteSpaceCount);

                    Console.WriteLine(" {0}{1}-  {2}", commands[i], whiteSpaces, commandsHelp[i]);
                }
            }

            Console.WriteLine(HLINE_INNER);
            Console.WriteLine(FOOTER_NOTE);
            Console.WriteLine(HLINE_OUTER);
        }

    }
}
