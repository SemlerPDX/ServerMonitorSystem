namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides a method to write current application status including configuration states to the console.
    /// </summary>
    public interface IConsoleWriteStatus
    {
        /// <summary>
        /// Writes the current application status to the console, including key states
        /// of integral systems such as Alerts, Alert Emails, game/VOIP proccesses,
        /// online player count, system memory data, active memory logging data, etc.
        /// </summary>
        void WriteProgramStatus();
    }
}
