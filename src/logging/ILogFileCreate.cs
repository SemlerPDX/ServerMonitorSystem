namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method to create a new CSV log file with headers.
    /// </summary>
    public interface ILogFileCreate
    {
        /// <summary>
        /// Creates a new CSV log file at the specified file path and writes the headings to the file.
        /// <br>If a log file already exists at the specified path, its contents will be overwritten.</br>
        /// </summary>
        /// <param name="filePath">The file path where the new CSV log file should be created.</param>
        void CreateLog(string filePath);
    }
}
