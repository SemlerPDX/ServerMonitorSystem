namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to the python config file save and load methods.
    /// </summary>
    public interface IConfigPythonFileHandler
    {
        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file.
        /// </summary>
        void SavePythonConfig();

        /// <summary>
        /// Load all <see cref="IConfigPython"/> properties from the config python file.
        /// </summary>
        void LoadPythonConfig();
    }
}
