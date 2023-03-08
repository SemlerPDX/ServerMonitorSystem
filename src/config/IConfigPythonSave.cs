using Microsoft.Scripting.Hosting;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method for saving or creating the config properties file.
    /// </summary>
    public interface IConfigPythonSave
    {
        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file,
        /// or create new file if it does not exist.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the <see cref="IConfigPython"/> properties being
        /// saved to file for the Python Engine.
        /// </param>
        /// <param name="configFilePath">The path to the config file to save.</param>
        void WriteConfigPythonFile(ScriptScope pythonScope, string configFilePath);
    }
}
