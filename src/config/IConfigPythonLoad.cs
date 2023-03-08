using Microsoft.Scripting.Hosting;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method for loading <see cref="IConfigPython"/> properties from file.
    /// </summary>
    public interface IConfigPythonLoad
    {
        /// <summary>
        /// Load all <see cref="IConfigPython"/> class properties from the config python file.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the properties of the <see cref="IConfigPython"/> class being
        /// loaded from file for the Python Engine.
        /// </param>
        void LoadPythonConfigFile(ScriptScope pythonScope);
    }
}
