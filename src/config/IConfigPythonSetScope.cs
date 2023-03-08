using Microsoft.Scripting.Hosting;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method for setting the Python Engine <see cref="ScriptScope"/> to properties being saved to file.
    /// </summary>
    public interface IConfigPythonSetScope
    {
        /// <summary>
        /// Sets the properties of the <see cref="IConfigPython"/> class to the
        /// Python Engine <see cref="ScriptScope"/> for saving to file.
        /// </summary>
        /// <param name="pythonScope">
        /// The scope used to contain the properties of the <see cref="IConfigPython"/> class being
        /// saved to file for the Python Engine.
        /// </param>
        /// <returns>The PythonScope object containing each property of the <see cref="IConfigPython"/> class.</returns>
        ScriptScope SetConfigPythonScope(ScriptScope pythonScope);
    }
}
