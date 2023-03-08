namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to methods for initializing <see cref="IConfig_Manager"/> properties and
    /// any command line argument overrides for those properties.
    /// </summary>
    public interface IInitProperties
    {
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
        void InitializeProperties(string[] args);

        /// <summary>
        /// Loads all <see cref="IConfigPython"/> properties from the config python file,
        /// which will then be represented in the <see cref="IConfig_Manager"/> class for the app.
        /// </summary>
        void InitializeConfigProperties();

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
        void InitializePropertiesOverrides();
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
        void InitializePropertiesOverrides(string[] args);
    }
}
