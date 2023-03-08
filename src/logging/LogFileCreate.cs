using System.IO;
using System.Text;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Log File Create class implementing <see cref="ILogFileCreate"/> to provide
    /// access to a method for creating a new CSV log file with headers.
    /// </summary>
    class LogFileCreate : ILogFileCreate
    {
        /// <summary>
        /// The server info, system performance, and memory data headings to be written at the top of the CSV log file.
        /// </summary>
        private readonly string[] LOG_FILE_HEADINGS = {
            "Date/Time",
            "Online PlayerCount",
            "Commit Total Pages",
            "Commit Limit Pages",
            "Commit Peak Pages",
            "Physical Total Bytes",
            "Physical Available Bytes",
            "System Cache Bytes",
            "Kernel Total Bytes",
            "Kernel Paged Bytes",
            "Kernel Non-Paged Bytes",
            "Page Size Bytes",
            "Handles Count",
            "Process Count",
            "Thread Count"
        };

        /// <summary>
        /// Creates a new CSV log file at the specified file path and writes the headings to the file.
        /// <br>If a log file already exists at the specified path, its contents will be overwritten.</br>
        /// </summary>
        /// <param name="filePath">The file path where the new CSV log file should be created.</param>
        public void CreateLog(string filePath)
        {
            using StreamWriter csvWriter = new(filePath, false);
            string separator = ",";
            StringBuilder fileContents = new();
            fileContents.AppendLine(string.Join(separator, LOG_FILE_HEADINGS));
            csvWriter.Write(fileContents.ToString());
        }
    }
}
