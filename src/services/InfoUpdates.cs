using System;
using System.Reflection;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The App Updates service class implementing <see cref="IInfoUpdates"/> to provide
    /// methods which check for and display updates for the current application.
    /// <para>
    /// Dependencies: <see cref="IAcquisitionInterface"/>
    /// </para>
    /// </summary>
    class InfoUpdates : IInfoUpdates
    {
        /// <summary>
        /// The object that retrieves application version information from the database online.
        /// </summary>
        private readonly IAcquisitionInterface _acquisition;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoUpdates"/> class with the specified interface.
        /// </summary>
        /// <param name="acquisition">The object that retrieves application version information from the database online.</param>
        public InfoUpdates(IAcquisitionInterface acquisition)
        {
            _acquisition = acquisition;
        }


        /// <summary>
        /// Checks for updates for the current application and writes the result to the console.
        /// </summary>
        public void CheckForUpdates() { CheckForUpdates(true); }
        /// <summary>
        /// Checks for updates for the current application and optionally writes the result to the console.
        /// </summary>
        /// <param name="writeLatest">
        /// Specifies whether to write the latest version information to the console
        /// regardless of whether an update has been found.
        /// </param>
        public void CheckForUpdates(bool writeLatest)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string appName = assembly.GetName().Name;
            string appVersion = assembly.GetName().Version.ToString();
            string newVersion = writeLatest ? $"No new updates have been found for this app - [v{appVersion}] is the latest version" : "";
            string featuredChange = "";
            string repositoryLink = "";
            string[] appData = _acquisition.CheckUpdateData(appName, appVersion);

            if (appData != null)
            {
                newVersion = $"An update has been found for {appName}: Version {appData[0]} is now available!";
                featuredChange = $"Featured Changes: {appData[1]}";
                repositoryLink = $"Download link is on the repository: {appData[8]}";
            }

            string[] appUpdate = { newVersion, featuredChange, repositoryLink };
            foreach (string line in appUpdate)
            {
                if (!String.IsNullOrEmpty(line))
                    Console.WriteLine($"{line}");
            }
        }

    }
}
