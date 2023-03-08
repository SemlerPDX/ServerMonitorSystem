using F4SharedMem;
using F4SharedMem.Headers;
using System;
using System.Linq;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Falcon BMS service class implementing <see cref="IInfoFalconBMS"/> to provide methods for retrieving
    /// information about a Falcon BMS server through <see cref="F4SharedMem"/> &amp; <see cref="F4SharedMem.Headers"/>.
    /// </summary>
    class InfoFalconBMS : IInfoFalconBMS
    {
        /// <summary>
        /// Gets the name of the current Theater when a mission is online in the BMS server.
        /// </summary>
        /// <returns>A string containing the name of the current theater if server is online, else '(offline)'.</returns>
        public string GetTheaterName()
        {

            try
            {
                using Reader sharedMemReader = new();
                if (sharedMemReader != null)
                {
                    FlightData currentMemData = sharedMemReader.GetCurrentData();
                    if (currentMemData != null)
                    {
                        var v = currentMemData.StringData.data.ElementAt(13);
                        string theater = v.value;
                        return theater;
                    }
                }
                return "(offline)";
            }
            catch
            {
                return "(offline)";
            }
        }

        /// <summary>
        /// Gets the total number of pilots currently online in the BMS server.
        /// </summary>
        /// <returns>The number of pilots currently online.</returns>
        public int GetPilotCount()
        {
            try
            {
                using (Reader sharedMemReader = new())
                {
                    if (sharedMemReader != null)
                    {
                        FlightData currentMemData = sharedMemReader.GetCurrentData();
                        if (currentMemData != null)
                        {
                            return currentMemData.pilotsOnline;
                        }
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns the status of a pilot based on the given status code.
        /// </summary>
        /// <param name="status">The status code of the pilot.</param>
        /// <returns>The status of the pilot as a string.</returns>
        private string GetPilotStatus(int status)
        {
            return status switch
            {
                (int)FlyStates.IN_UI => "IN_UI",
                (int)FlyStates.LOADING => "LOADING",
                (int)FlyStates.WAITING => "WAITING",
                (int)FlyStates.FLYING => "FLYING",
                (int)FlyStates.DEAD => "DEAD",
                _ => "UNKNOWN"
            };
        }

        /// <summary>
        /// Gets an array of strings that represent the callsign and status of each pilot currently online in the BMS server.
        /// </summary>
        /// <returns>An array of strings that represent the callsign and status of each pilot currently online in the BMS server.</returns>
        public string[] GetPilotData()
        {
            using (Reader sharedMemReader = new())
            {
                if (sharedMemReader != null)
                {
                    FlightData currentMemData = sharedMemReader.GetCurrentData();

                    if (currentMemData != null)
                    {
                        int pilotCount = currentMemData.pilotsOnline;
                        if (pilotCount > 0)
                        {
                            string[] callsigns = new string[pilotCount];
                            string[] statuses = new string[pilotCount];
                            for (int i = 0; i < pilotCount; i++)
                            {
                                callsigns[i] = currentMemData.pilotsCallsign[i];
                                statuses[i] = GetPilotStatus(currentMemData.pilotsStatus[i]);
                            }
                            return callsigns.Zip(statuses, (c, s) => $"{c}    :  {s}").ToArray();
                        }
                    }
                }
            }
            return new string[] { };
        }

        /// <summary>
        /// Displays the callsign and status of each pilot currently online in the BMS server on the console.
        /// </summary>
        public void PilotStatusReadout()
        {
            try
            {
                string[] pilotData = GetPilotData();
                if (pilotData.Length > 0)
                {
                    int counter = 0;
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("---Currently Online Pilot Data---");
                    foreach (string pilot in pilotData)
                    {
                        if (!String.IsNullOrEmpty(pilot))
                        {
                            counter++;
                            Console.WriteLine(counter.ToString() + ". " + pilot);
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------------------");
                }
                else
                {
                    Console.WriteLine("---NO Currently Online Pilot Data---");
                }
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at PrintPlayerData method");
            }
        }

    }
}
