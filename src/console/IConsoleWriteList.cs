using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method for writing a list of key-value pairs to the console.
    /// </summary>
    public interface IConsoleWriteList
    {
        /// <summary>
        /// Writes the given list of key-value pairs to the console, with a given title.
        /// </summary>
        /// <param name="title">The title to display above the list of items.</param>
        /// <param name="keyValuePairs">The list of key-value pairs to display.</param>
        void WriteDictionaryList(string title, Dictionary<string, string> keyValuePairs);
    }
}
