namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides the method for writing a user commands help list to the console.
    /// </summary>
    public interface IConsoleWriteHelpList
    {
        /// <summary>
        /// Write all user commands and descriptions to the console with a common whitespace gap between each column of text.
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandUsages"/>, <seealso cref="IConfig_Manager.CommandDescriptions"/>,
        /// and <seealso cref="ICommand_Manager.LoadCommands()"/>)
        /// </para>
        /// </summary>
        /// <param name="commands">A string array of all command usages which will appear in the help list.</param>
        /// <param name="commandsHelp">A string array of all command descriptions which will appear in the help list.</param>
        void WriteHelpInfo(string[] commands, string[] commandsHelp);
    }
}
