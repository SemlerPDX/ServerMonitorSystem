using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method for processing command line arguments.
    /// </summary>
    public interface IConfigCommandLine
    {
        /// <summary>
        /// Parse command line arguments and cast values to matching <see cref="IConfigPython"/> class properties.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        /// <returns>A HashSet of strings containing the names of any properties changed.</returns>
        HashSet<string> Parse(string[] args);
    }
}
