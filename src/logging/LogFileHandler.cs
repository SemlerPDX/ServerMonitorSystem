using System;
using System.IO;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Log File Handler class implementing <see cref="ILogFileHandler"/> to provide access to
    /// a method for handling the CSV log file and saving system performance &amp; memory data to it.
    /// <para>
    /// Dependencies: <see cref="ILogFileCreate"/>, <see cref="ILogFileRotate"/>, <see cref="ILogFileSave"/>
    /// </para>
    /// </summary>
    class LogFileHandler : ILogFileHandler
    {
        /// <summary>
        /// The object responsible for creating new log files.
        /// </summary>
        private readonly ILogFileCreate _logFileCreate;
        /// <summary>
        /// The object that manages rotation of log files that exceed a certain size.
        /// </summary>
        private readonly ILogFileRotate _logFileRotate;
        /// <summary>
        /// The object responsible for saving system performance &amp; memory data to a log file.
        /// </summary>
        private readonly ILogFileSave _logFileSave;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFileHandler"/> class with the specified interfaces.
        /// </summary>
        /// <param name="logFileCreate">The object responsible for creating new log files.</param>
        /// <param name="logFileRotate">The object that manages rotation of log files that exceed a certain size.</param>
        /// <param name="logFileSave">The object responsible for saving system performance &amp; memory data to a log file.</param>
        public LogFileHandler(ILogFileCreate logFileCreate,
                              ILogFileRotate logFileRotate,
                              ILogFileSave logFileSave)
        {
            _logFileCreate = logFileCreate;
            _logFileRotate = logFileRotate;
            _logFileSave = logFileSave;
        }

        private const string LOG_FILE_NAME = "memory_monitor"; // default "memory_monitor"

        /// <summary>
        /// Logs the provided system performance &amp; memory data to a CSV file at the specified path.
        /// </summary>
        /// <param name="memData">The system performance &amp; memory data to log.</param>
        /// <param name="filePath">The path to the CSV log file. If null or empty, a default file path is used.</param>
        /// <param name="maxBytes">The maximum file size, in bytes, before log file rotation is triggered.
        /// <br>If the log file exceeds this size, it is renamed with a timestamp and a new file is created.</br></param>
        public void LogDataToFile(string[] memData, string filePath, long maxBytes)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath) || filePath.Contains("example"))
                    filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FILE_NAME + ".csv");

                if (File.Exists(filePath))
                    _logFileRotate.RotateLog(filePath, maxBytes);

                if (!File.Exists(filePath))
                    _logFileCreate.CreateLog(filePath);

                if (File.Exists(filePath))
                    _logFileSave.SaveLog(filePath, memData);
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at LogFileHandler in LogDataToFile method");
            }
        }

    }
}
