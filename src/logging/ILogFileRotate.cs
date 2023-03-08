namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method to rotate log files based on a maximum file size.
    /// </summary>
    public interface ILogFileRotate
    {
        /// <summary>
        /// Rotates the log file at the specified file path if its size exceeds the maximum file size in megabytes.
        /// <br>The rotated log file is renamed with a timestamp in its file name and stored in the same directory as the original file.</br>
        /// </summary>
        /// <param name="filePath">The file path of the log file to rotate.</param>
        /// <param name="maxBytes">The maximum file size in megabytes.</param>
        void RotateLog(string filePath, long maxBytes);
    }
}
