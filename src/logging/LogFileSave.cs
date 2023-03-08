using System.Text;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Logging Save File class implementing <see cref="ILogFileSave"/> to provide
    /// a method for saving performance &amp; memory data to a specified log file path.
    /// </summary>
    class LogFileSave : ILogFileSave
    {
        /// <summary>
        /// Saves the given performance &amp; memory data to the specified log file path.
        /// </summary>
        /// <param name="filePath">The log file path to save the data to.</param>
        /// <param name="memData">An array containing the performance &amp; memory data to be saved.</param>
        public void SaveLog(string filePath, string[] memData)
        {
            using System.IO.StreamWriter writer = new(filePath, true);
            string separator = ",";
            StringBuilder newLine = new();
            newLine.AppendLine(string.Join(separator, memData));
            writer.Write(newLine.ToString());
        }

    }
}
