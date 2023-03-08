namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods to initialize or uninitialize the application.
    /// </summary>
    public interface IInit_Manager
    {
        /// <summary>
        /// End all active <see cref="ITimers"/> system timers in preparation for application close.
        /// </summary>
        void UnInitializeApp();

        /// <summary>
        /// Initializes information and monitoring systems, loading config file and processing command line arguments.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        void InitializeApp(string[] args);
    }
}
