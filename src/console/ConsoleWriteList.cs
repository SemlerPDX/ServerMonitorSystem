using System;
using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Console List Writer class implementing <see cref="IConsoleWriteList"/> to provide
    /// access to a method for writing a list of key-value pairs to the console.
    /// </summary>
    public class ConsoleWriteList : IConsoleWriteList
    {
        /// <summary>
        /// Writes the given list of key-value pairs to the console, with a given title.
        /// </summary>
        /// <param name="title">The title to display above the list of items.</param>
        /// <param name="keyValuePairs">The list of key-value pairs to display.</param>
        public void WriteDictionaryList(string title, Dictionary<string, string> keyValuePairs)
        {
            if (keyValuePairs == null || keyValuePairs.Count == 0)
            {
                Console.WriteLine(" -No items in list!");
                return;
            }

            if (keyValuePairs != null)
            {
                int i = 1;
                Console.WriteLine(title);
                foreach (var keyValuePair in keyValuePairs)
                {
                    Console.WriteLine($"{i}. {keyValuePair.Key}:  {keyValuePair.Value}");
                    i++;
                }
            }
        }

    }
}
