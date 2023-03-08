using System;
using System.IO;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Log File Rotate class implementing <see cref="ILogFileRotate"/> to provide
    /// access to a method for rotating log files based on a maximum file size.
    /// </summary>
    class LogFileRotate : ILogFileRotate
    {
        /// <summary>
        /// Rotates the log file at the specified file path if its size exceeds the maximum file size in megabytes.
        /// <br>The rotated log file is renamed with a timestamp in its file name and stored in the same directory as the original file.</br>
        /// </summary>
        /// <param name="filePath">The file path of the log file to rotate.</param>
        /// <param name="maxBytes">The maximum file size in megabytes.</param>
        public void RotateLog(string filePath, long maxBytes)
        {
            long fileSize = 0;
            using (FileStream stream = File.OpenRead(filePath))
            {
                if (stream != null)
                {
                    fileSize = stream.Length;
                }
            }

            maxBytes *= 1000 * 1000;

            if (fileSize > maxBytes)
            {
                string timestamp = DateTime.Now.ToString("MMddyyyyHHmmss");
                string fileName = Path.GetFileName(filePath.Replace(".csv", ""));
                string newFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), fileName + "_" + timestamp + ".csv");
                File.Copy(filePath, newFilePath);
                File.Delete(filePath);
            }
        }

    }
}
