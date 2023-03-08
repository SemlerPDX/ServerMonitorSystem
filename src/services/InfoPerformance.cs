using System;
using System.Runtime.InteropServices;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Represents a container for performance information data.
    /// </summary>
    public class PerfomanceInfoData
    {
        /// <summary>
        /// System Commit Total Pages.
        /// </summary>
        public Int64 CommitTotalPages;
        /// <summary>
        /// System Commit Limit Pages.
        /// </summary>
        public Int64 CommitLimitPages;
        /// <summary>
        /// System Commit Peak Pages.
        /// </summary>
        public Int64 CommitPeakPages;
        /// <summary>
        /// System Physical Total Bytes.
        /// </summary>
        public Int64 PhysicalTotalBytes;
        /// <summary>
        /// System Physical Available Bytes.
        /// </summary>
        public Int64 PhysicalAvailableBytes;
        /// <summary>
        /// System System Cache Bytes.
        /// </summary>
        public Int64 SystemCacheBytes;
        /// <summary>
        /// System Kernel Total Bytes.
        /// </summary>
        public Int64 KernelTotalBytes;
        /// <summary>
        /// System Kernel Paged Bytes.
        /// </summary>
        public Int64 KernelPagedBytes;
        /// <summary>
        /// System Kernel Non-Paged Bytes.
        /// </summary>
        public Int64 KernelNonPagedBytes;
        /// <summary>
        /// System Page Size Bytes.
        /// </summary>
        public Int64 PageSizeBytes;
        /// <summary>
        /// System Handles Count.
        /// </summary>
        public int HandlesCount;
        /// <summary>
        /// System Process Count.
        /// </summary>
        public int ProcessCount;
        /// <summary>
        /// System Thread Count.
        /// </summary>
        public int ThreadCount;
    }

    /// <summary>
    /// Provides a wrapper class to retrieve performance information data using the psapi.dll.
    /// </summary>
    public static class PsApiWrapper
    {
        /// <summary>
        /// Retrieves performance information data using the psapi.dll.
        /// </summary>
        /// <param name="PerformanceInformation">A <see cref="PsApiPerformanceInformation"/> structure
        /// that receives the performance information data.</param>
        /// <param name="Size">The size of the <see cref="PsApiPerformanceInformation"/> structure, in bytes.</param>
        /// <returns>True if the function succeeds, false otherwise.</returns>
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPerformanceInfo([Out] out PsApiPerformanceInformation PerformanceInformation, [In] int Size);

        /// <summary>
        /// Contains performance information data returned by <see cref="GetPerformanceInfo()"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PsApiPerformanceInformation
        {
            /// <summary>
            /// The size of the <see cref="PsApiPerformanceInformation"/> structure, in bytes.
            /// </summary>
            public int Size;
            /// <summary>
            /// System Commit Total Pages.
            /// </summary>
            public IntPtr CommitTotal;
            /// <summary>
            /// System Commit Limit Pages.
            /// </summary>
            public IntPtr CommitLimit;
            /// <summary>
            /// System Commit Peak Pages.
            /// </summary>
            public IntPtr CommitPeak;
            /// <summary>
            /// System Physical Total Bytes.
            /// </summary>
            public IntPtr PhysicalTotal;
            /// <summary>
            /// System Physical Available Bytes.
            /// </summary>
            public IntPtr PhysicalAvailable;
            /// <summary>
            /// System System Cache Bytes.
            /// </summary>
            public IntPtr SystemCache;
            /// <summary>
            /// System Kernel Total Bytes.
            /// </summary>
            public IntPtr KernelTotal;
            /// <summary>
            /// System Kernel Paged Bytes.
            /// </summary>
            public IntPtr KernelPaged;
            /// <summary>
            /// System Kernel Non-Paged Bytes.
            /// </summary>
            public IntPtr KernelNonPaged;
            /// <summary>
            /// System Page Size Bytes.
            /// </summary>
            public IntPtr PageSize;
            /// <summary>
            /// System Handles Count.
            /// </summary>
            public int HandlesCount;
            /// <summary>
            /// System Process Count.
            /// </summary>
            public int ProcessCount;
            /// <summary>
            /// System Thread Count.
            /// </summary>
            public int ThreadCount;
        }

        /// <summary>
        /// Retrieves performance information data and returns it as a <see cref="PerfomanceInfoData"/> object.
        /// </summary>
        /// <returns>A <see cref="PerfomanceInfoData"/> object containing performance information data.</returns>
        public static PerfomanceInfoData GetPerformanceInfo()
        {
            PerfomanceInfoData data = new();
            PsApiPerformanceInformation perfInfo = new();
            if (GetPerformanceInfo(out perfInfo, Marshal.SizeOf(perfInfo)))
            {
                // Data in pages
                data.CommitTotalPages = perfInfo.CommitTotal.ToInt64();
                data.CommitLimitPages = perfInfo.CommitLimit.ToInt64();
                data.CommitPeakPages = perfInfo.CommitPeak.ToInt64();

                // Data in bytes
                Int64 pageSize = perfInfo.PageSize.ToInt64();
                data.PhysicalTotalBytes = perfInfo.PhysicalTotal.ToInt64() * pageSize;
                data.PhysicalAvailableBytes = perfInfo.PhysicalAvailable.ToInt64() * pageSize;
                data.SystemCacheBytes = perfInfo.SystemCache.ToInt64() * pageSize;
                data.KernelTotalBytes = perfInfo.KernelTotal.ToInt64() * pageSize;
                data.KernelPagedBytes = perfInfo.KernelPaged.ToInt64() * pageSize;
                data.KernelNonPagedBytes = perfInfo.KernelNonPaged.ToInt64() * pageSize;
                data.PageSizeBytes = pageSize;

                // Counters
                data.HandlesCount = perfInfo.HandlesCount;
                data.ProcessCount = perfInfo.ProcessCount;
                data.ThreadCount = perfInfo.ThreadCount;
            }
            return data;
        }

    }
}
