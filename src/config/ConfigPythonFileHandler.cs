using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Implements the <see cref="IConfigPythonFileHandler"/> interface and provides methods to save/load config properties to/from file.
    /// <para>
    /// Dependencies: <see cref="IConfigPythonSetScope"/>, <see cref="IConfigPythonSave"/>, <see cref="IConfigPythonLoad"/>
    /// </para>
    /// </summary>
    class ConfigPythonFileHandler : IConfigPythonFileHandler
    {
        /// <summary>
        /// The default path to the monitor_config.py file in the root folder of this application.
        /// </summary>
        private readonly string ConfigFilePath;

        /// <summary>
        /// The object used to set the <see cref="ScriptScope"/> PythonScope of properties to save to file.
        /// </summary>
        private readonly IConfigPythonSetScope _configPythonSetScope;
        /// <summary>
        /// The object used to save <see cref="IConfigPython"/> properties to file.
        /// </summary>
        private readonly IConfigPythonSave _configPythonSave;
        /// <summary>
        /// The object used to load <see cref="IConfigPython"/> properties from file.
        /// </summary>
        private readonly IConfigPythonLoad _configPythonLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigPythonFileHandler"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configPythonSetScope">The object used to set the PythonScope of properties to save to file.</param>
        /// <param name="configPythonSave">The object used to save <see cref="IConfigPython"/> properties to file.</param>
        /// <param name="configPythonLoad">The object used to load <see cref="IConfigPython"/> properties from file.</param>
        public ConfigPythonFileHandler(IConfigPythonSetScope configPythonSetScope,
                          IConfigPythonSave configPythonSave,
                          IConfigPythonLoad configPythonLoad)
        {
            _configPythonSetScope = configPythonSetScope;
            _configPythonSave = configPythonSave;
            _configPythonLoad = configPythonLoad;

            ConfigFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PYTHON_FILE_NAME + ".py");
        }

        private static readonly string PYTHON_FILE_NAME = "monitor_config"; // default "monitor_config"

        /// <summary>
        /// Save all <see cref="IConfigPython"/> properties to the config python file.
        /// </summary>
        public void SavePythonConfig()
        {
            ScriptEngine pythonEngine = Python.CreateEngine();
            ScriptScope pythonScope = pythonEngine.CreateScope();
            pythonScope = _configPythonSetScope.SetConfigPythonScope(pythonScope);
            _configPythonSave.WriteConfigPythonFile(pythonScope, ConfigFilePath);
        }

        /// <summary>
        /// Load all <see cref="IConfigPython"/> properties from the config python file.
        /// </summary>
        public void LoadPythonConfig()
        {
            // Create save file from defaults if nonexistent
            if (!File.Exists(ConfigFilePath))
                SavePythonConfig();

            // Initialize IronPython engine & scope to load file
            ScriptEngine pythonEngine = Python.CreateEngine();
            ScriptScope pythonScope = pythonEngine.CreateScope();
            pythonScope = pythonEngine.ExecuteFile(ConfigFilePath, pythonScope);

            _configPythonLoad.LoadPythonConfigFile(pythonScope);
        }

    }
}
