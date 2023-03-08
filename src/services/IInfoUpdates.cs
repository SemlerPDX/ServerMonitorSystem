namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods which check for and display updates for the current application.
    /// </summary>
    public interface IInfoUpdates
    {
        /// <summary>
        /// Checks for updates for the current application and writes the result to the console.
        /// </summary>
        void CheckForUpdates();
        /// <summary>
        /// Checks for updates for the current application and optionally writes the result to the console.
        /// </summary>
        /// <param name="writeLatest">
        /// Specifies whether to write the latest version information to the console
        /// regardless of whether an update has been found.
        /// </param>
        void CheckForUpdates(bool writeLatest);
    }
}
