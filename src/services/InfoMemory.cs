using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// System Memory service class which implements <see cref="IInfoMemory"/>
    /// to provide methods for retrieving system memory information.
    /// </summary>
    class InfoMemory : IInfoMemory
    {
        /// <summary>
        /// Gets current system memory information in megabytes.
        /// </summary>
        /// <returns>An array of three floats containing available memory, maximum memory, and used memory - in MB.</returns>
        public float[] GetMemoryInfo()
        {
            try
            {
                PerfomanceInfoData perfData = PsApiWrapper.GetPerformanceInfo();
                float availableBytes = perfData.PhysicalAvailableBytes / (1024 * 1024);
                float maximumBytes = perfData.PhysicalTotalBytes / (1024 * 1024);
                float usedBytes = (maximumBytes - availableBytes);

                // Assemble & Return current memory data
                float[] memInfo = { availableBytes, maximumBytes, usedBytes };
                return memInfo;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at InfoMemory in GetMemoryInfo method");
            }
            return null;
        }

        /// <summary>
        /// Gets detailed system performance &amp; memory data.
        /// </summary>
        /// <returns>An array of strings containing various system performance data &amp; memory usage statistics.</returns>
        public string[] GetMemoryData()
        {
            try
            {
                PerfomanceInfoData perfData = PsApiWrapper.GetPerformanceInfo();
                string[] memData = {
                    perfData.CommitTotalPages.ToString(),
                    perfData.CommitLimitPages.ToString(),
                    perfData.CommitPeakPages.ToString(),
                    perfData.PhysicalTotalBytes.ToString(),
                    perfData.PhysicalAvailableBytes.ToString(),
                    perfData.SystemCacheBytes.ToString(),
                    perfData.KernelTotalBytes.ToString(),
                    perfData.KernelPagedBytes.ToString(),
                    perfData.KernelNonPagedBytes.ToString(),
                    perfData.PageSizeBytes.ToString(),
                    perfData.HandlesCount.ToString(),
                    perfData.ProcessCount.ToString(),
                    perfData.ThreadCount.ToString()
                };
                return memData;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at InfoMemory in GetMemoryData method");
            }
            return null;
        }

    }
}
