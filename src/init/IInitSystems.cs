namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods which control initialization flow &amp; check for app updates,
    /// and which start/restart system timers to manage alerts, log performance data, and update
    /// <see cref="IConfig_Manager"/> properties using the service classes.
    /// </summary>
    public interface IInitSystems
    {
        /// <summary>
        /// Enables system timers in classes implementing <see cref="ITimers"/> of the 'services' type, writes help list to the console,
        /// and a note about any updates for this app - then waits for user to press any key to continue initialization.
        /// <para>
        /// (see also <seealso cref="IConfig_Manager.CommandUsages"/> which holds all console command usage examples,
        /// and <seealso cref="IConfig_Manager.CommandDescriptions"/> which holds all command descriptions,
        /// dictating the help list display format.)
        /// </para>
        /// </summary>
        void PreInitialization();

        /// <summary>
        /// Enables all system timers in classes managed by <see cref="ITimer_Manager"/> which implement <see cref="ITimers"/>
        /// in accordance with their individual 'can start' conditions, writes a note about any updates for this app, and
        /// finally, prints current application status to the console.
        /// </summary>
        void InitializeSystems();

        /// <summary>
        /// Stops system timers in classes implementing <see cref="ITimers"/> with the names 'alerts' and 'logging',
        /// and then starts them again with any updated properties including <see cref="IConfig_Manager.Interval"/>
        /// and <see cref="IConfig_Manager.Frequency"/>, then writes a note about any updates for this app,
        /// and finally, prints current application status to the console.
        /// </summary>
        void ReInitializeSystems();

        /// <summary>
        /// End all system timers in classes implementing <see cref="ITimers"/> used througout the system.
        /// </summary>
        void StopAllSystems();
    }
}
