using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// VG Labs - Server Monitor System
    /// <br>by SemlerPDX Feb2023 CC BY-NC-ND</br>
    /// <br><see href="https://github.com/SemlerPDX">github.com/SemlerPDX</see></br>
    /// <para>
    /// A program to monitor game and voice server status with several optional features:
    /// <br> -Can report crash alerts through event logs and (optional) SMTP emails.</br>
    /// <br> -Can monitor system memory use and issue alerts if beyond a set level.</br>
    /// <br> -Can enable auto-kill of game server process if memory use exceeds another set level.</br>
    /// <br> -Can log system memory usage to CSV file at user defined frequency/duration.</br>
    /// <br> -Allows remote server status data through public API. (WIP - coming soon)</br>
    /// </para>
    /// </summary>
    class Program
    {
        /// <summary>
        /// Event handler for allowing copy [CTRL+C] of help menu items instead of closing app.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="ConsoleCancelEventArgs"/> that contains the event data.</param>
        private static void Console_HelpMenuCopyAllow(object sender, ConsoleCancelEventArgs e) { e.Cancel = true; }

        /// <summary>
        /// The entry point for the program.
        /// <para>
        ///   <remarks>
        ///     NOTE: verify appropriate service life(s) here during alpha testing: AddScoped AddSingleton AddTransient
        ///     -Sem Feb-22-2023
        ///   </remarks>
        /// </para>
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        static void Main(string[] args)
        {
            // Create ServiceProvider for DependencyInjection
            ServiceProvider serviceProvider = new ServiceCollection()
                // Add Alerts Interfaces
                .AddTransient<IAlertsMain, AlertsMain>()
                .AddSingleton<IAlerts_Manager, Alerts_Manager>()

                // Add Commands Interface
                .AddSingleton<ICommand_Manager, Command_Manager>()

                // Add Configuration Interfaces
                .AddSingleton<IConfig_Manager, Config_Manager>()
                .AddSingleton<IConfigPython, ConfigPython>()
                .AddSingleton<IConfigPythonFileHandler, ConfigPythonFileHandler>()
                .AddScoped<IConfigPythonLoad, ConfigPythonLoad>()
                .AddScoped<IConfigPythonSave, ConfigPythonSave>()
                .AddScoped<IConfigPythonSetScope, ConfigPythonSetScope>()
                .AddTransient<IConfigCommandLine, ConfigCommandLine>()

                // Add Console Interfaces
                .AddSingleton<IConsole_Manager, Console_Manager>()
                .AddScoped<IConsoleReadNumbers, ConsoleReadNumbers>()
                .AddTransient<IConsoleWriteAlerts, ConsoleWriteAlerts>()
                .AddTransient<IConsoleWriteHelpList, ConsoleWriteHelpList>()
                .AddTransient<IConsoleWriteList, ConsoleWriteList>()
                .AddTransient<IConsoleWritePrompts, ConsoleWritePrompts>()
                .AddTransient<IConsoleWriteStatus, ConsoleWriteStatus>()

                // Add Email Interfaces
                .AddSingleton<IEmail_Manager, Email_Manager>()
                .AddScoped<IEmailSend, EmailSend>()
                .AddSingleton<IEmailSMTP, EmailSMTP>()
                .AddSingleton<IEmailContent, EmailContent>()
                .AddSingleton<IEmailAttachment, EmailAttachment>()
                .AddSingleton<IEmailConfiguration, EmailConfiguration>()

                // Add Initialization Interfaces
                .AddScoped<IInit_Manager, Init_Manager>()
                .AddScoped<IInitProperties, InitProperties>()
                .AddScoped<IInitServers, InitServers>()
                .AddScoped<IInitSystems, InitSystems>()

                // Add Memory Logging Interfaces
                .AddScoped<ILogFileHandler, LogFileHandler>()
                .AddTransient<ILogFileCreate, LogFileCreate>()
                .AddTransient<ILogFileRotate, LogFileRotate>()
                .AddTransient<ILogFileSave, LogFileSave>()

                // Add Service Interfaces
                .AddScoped<IInfoMemory, InfoMemory>()
                .AddScoped<IInfoFalconBMS, InfoFalconBMS>()
                .AddScoped<IInfoServer, InfoServer>()
                .AddScoped<IInfoUpdates, InfoUpdates>()

                // Add Timer Interfaces
                .AddSingleton<ITimer_Manager, Timer_Manager>()
                .AddSingleton<ITimerPause_Manager, TimerPause_Manager>()

                // Add VGFoundationsLite.dll Interfaces
                .AddScoped<IApplicationsInterface, Applications>()
                .AddScoped<IAcquisitionInterface, Acquisition>()

                // Add Main Application Class
                .AddSingleton<ServerMonitor>()
                .BuildServiceProvider();

            var serverMonitor = serviceProvider.GetRequiredService<ServerMonitor>();
            string appName = SetTitle();


            // If an instance of this app is already running, just display help menu when args empty
            using Mutex mutex = new(true, appName, out bool createdNew);
            if (!createdNew && args.Length <= 0)
            {
                serverMonitor.RunHelp();
                return;
            }
            else
            {
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_HelpMenuCopyAllow);
                serverMonitor.Run(args);
            }
        }

        /// <summary>
        /// Sets the console title with the application name, version, and company name.
        /// </summary>
        /// <returns>The application name as a string.</returns>
        private static string SetTitle()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string appName = assembly.GetName().Name;
            string appVersion = assembly.GetName().Version.ToString();
            var customAttributesTitle = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            var customAttributesCompany = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (customAttributesTitle.Length > 0 && customAttributesCompany.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)customAttributesTitle[0];
                var companyAttribute = (AssemblyCompanyAttribute)customAttributesCompany[0];
                Console.Title = $"{companyAttribute.Company} - {titleAttribute.Title}   [v{appVersion}]";
            }
            return appName;
        }

    }
}
