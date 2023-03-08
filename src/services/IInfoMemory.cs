namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods for retrieving system memory information.
    /// </summary>
    public interface IInfoMemory
    {
        /// <summary>
        /// Gets current system memory information in megabytes.
        /// </summary>
        /// <returns>An array of three floats containing available memory, maximum memory, and used memory - in MB.</returns>
        float[] GetMemoryInfo();

        /// <summary>
        /// Gets detailed system performance &amp; memory data.
        /// </summary>
        /// <returns>An array of strings containing various system performance data &amp; memory usage statistics.</returns>
        string[] GetMemoryData();
    }
}
