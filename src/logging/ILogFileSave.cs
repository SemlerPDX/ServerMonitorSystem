namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to a method for saving performance &amp; memory data to a specified log file path.
    /// </summary>
    public interface ILogFileSave
    {
        /// <summary>
        /// Saves the given performance &amp; memory data to the specified log file path.
        /// </summary>
        /// <param name="filePath">The log file path to save the data to.</param>
        /// <param name="memData">An array containing the performance &amp; memory data to be saved.</param>
        void SaveLog(string filePath, string[] memData);
    }
}
