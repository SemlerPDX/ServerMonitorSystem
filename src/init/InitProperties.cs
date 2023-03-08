namespace ServerMonitorSystem
{
    /// <summary>
    /// Application Properties Initialization class implementing <see cref="IInitProperties"/> to
    /// provide access to methods for initializing <see cref="IConfig_Manager"/> properties and
    /// any command line argument overrides for those properties.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>
    /// </para>
    /// </summary>
    class InitProperties : IInitProperties
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitProperties"/> class with the specified interface.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public InitProperties(IConfig_Manager configManager)
        {
            _configManager = configManager;
        }

        /// <summary>
        /// Runs <see cref="InitializeConfigProperties()"/> to load all <see cref="IConfigPython"/> properties
        /// from the config python file, which are represented in the <see cref="IConfig_Manager"/> class for the system.
        /// <para>
        /// Next, runs <see cref="InitializePropertiesOverrides(string[])"/> to apply any supplied/stored
        /// command line argument overrides to <see cref="IConfigPython"/> properties.
        /// </para>
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandLineArgsInput"/>, containing command line arguments used this session,
        /// <br>and <seealso cref="IConfig_Manager.CommandLineArgsActive"/>, containing property names of active overrides already applied.)</br>
        /// </para>
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        public void InitializeProperties(string[] args)
        {
            // Load all properties from file, creating file on first use
            InitializeConfigProperties();

            // If command line has args, override defaults or loaded relative values
            InitializePropertiesOverrides(args);
        }

        /// <summary>
        /// Loads all <see cref="IConfigPython"/> properties from the config python file,
        /// which will then be represented in the <see cref="IConfig_Manager"/> class for the app.
        /// </summary>
        public void InitializeConfigProperties()
        {
            // Load all properties from file, creating file on first use
            _configManager.LoadConfig();
        }

        /// <summary>
        /// Apply any stored command line argument overrides to <see cref="IConfigPython"/> properties.
        /// <para>
        ///  -If command line arguments were supplied upon launch, and stored in <see cref="IConfig_Manager.CommandLineArgsInput"/>,
        ///  these will be retrieved and re-applied.
        /// </para>
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandLineArgsActive"/>, containing property names of active overrides)
        /// </para>
        /// </summary>
        public void InitializePropertiesOverrides() { InitializePropertiesOverrides(null); }
        /// <summary>
        /// Apply the supplied command line argument overrides to <see cref="IConfigPython"/> properties.
        /// <para>
        ///  -If null command line arguments were supplied, any stored in <see cref="IConfig_Manager.CommandLineArgsInput"/>,
        ///  will be retrieved and re-applied.
        /// </para>
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandLineArgsActive"/>, containing property names of active overrides)
        /// </para>
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        public void InitializePropertiesOverrides(string[] args)
        {
            // If command line has args, override defaults or loaded relative values
            if (args != null)
                _configManager.CommandLineArgsInput = args;

            if (args == null && _configManager.CommandLineArgsInput != null)
                args = _configManager.CommandLineArgsInput;

            _configManager.LoadCommandLineOverrides(args);
        }

    }
}
