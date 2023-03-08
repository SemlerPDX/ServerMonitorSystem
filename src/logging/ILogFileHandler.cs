namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method for handling the CSV log file and saving system performance &amp; memory data to it.
    /// </summary>
    public interface ILogFileHandler
    {
        /// <summary>
        /// Logs the provided system performance &amp; memory data to a CSV file at the specified path.
        /// </summary>
        /// <param name="memData">The system performance &amp; memory data to log.</param>
        /// <param name="filePath">The path to the CSV log file. If null or empty, a default file path is used.</param>
        /// <param name="maxBytes">The maximum file size, in bytes, before log file rotation is triggered.
        /// <br>If the log file exceeds this size, it is renamed with a timestamp and a new file is created.</br></param>
        void LogDataToFile(string[] memData, string filePath, long maxBytes);
    }
}
