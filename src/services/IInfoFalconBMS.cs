namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides access to methods for retrieving information about a Falcon BMS server
    /// through <see cref="F4SharedMem"/> &amp; <see cref="F4SharedMem.Headers"/>.
    /// </summary>
    public interface IInfoFalconBMS
    {
        /// <summary>
        /// Gets the name of the current Theater when a mission is online in the BMS server.
        /// </summary>
        /// <returns>A string containing the name of the current theater if server is online, else '(offline)'.</returns>
        string GetTheaterName();

        /// <summary>
        /// Gets the total number of pilots currently online in the BMS server.
        /// </summary>
        /// <returns>The number of pilots currently online.</returns>
        int GetPilotCount();

        /// <summary>
        /// Gets an array of strings that represent the callsign and status of each pilot currently online in the BMS server.
        /// </summary>
        /// <returns>An array of strings that represent the callsign and status of each pilot currently online in the BMS server.</returns>
        string[] GetPilotData();

        /// <summary>
        /// Displays the callsign and status of each pilot currently online in the BMS server on the console.
        /// </summary>
        void PilotStatusReadout();
    }
}
