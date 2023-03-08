using System;
using System.Runtime.InteropServices;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Application Initialization Manager class implementing <see cref="IInit_Manager"/>
    /// to provide access to methods for initializing and uninitializing application systems.
    /// <para>
    /// Dependencies: <see cref="IInitProperties"/>, <see cref="IInitSystems"/>, <see cref="IInitServers"/>
    /// </para>
    /// </summary>
    class Init_Manager : IInit_Manager
    {
        /// <summary>
        /// The object responsible for initializing all <see cref="IConfig_Manager"/> properties.
        /// </summary>
        private readonly IInitProperties _initProperties;
        /// <summary>
        /// The object which manages intialization flow and handles startup of key application systems.
        /// </summary>
        private readonly IInitSystems _initSystems;
        /// <summary>
        /// The object responsible for delaying initialization until server processes employing the <see cref="IAlerts"/> monitoring functions are running.
        /// </summary>
        private readonly IInitServers _initServers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Init_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="initProperties">The object responsible for initializing all <see cref="IConfig_Manager"/> properties.</param>
        /// <param name="initSystems">The object which manages intialization flow and handles startup of key application systems.</param>
        /// <param name="initServers">The object responsible for delaying initialization until server processes employing the <see cref="IAlerts"/> monitoring functions are running.</param>
        public Init_Manager(IInitProperties initProperties,
                          IInitSystems initSystems,
                          IInitServers initServers)
        {
            _initProperties = initProperties;
            _initSystems = initSystems;
            _initServers = initServers;
        }

        private const string DEFAULT_INIT_MSG = "[{0}] VG Labs: Server Monitor System is now initializing...";
        private const string DEFAULT_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT = 0x0040;

        #region dllImports
        /// <summary>
        /// Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="nStdHandle">The standard device.</param>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        /// <summary>
        /// Retrieves the current input mode of a console's input buffer or the current output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or the console screen buffer.</param>
        /// <param name="lpMode">A pointer to a variable that receives the current mode of the specified buffer.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        /// <summary>
        /// Sets the input mode of a console's input buffer or the output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or the console screen buffer.</param>
        /// <param name="dwMode">The input or output mode to be set.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        #endregion

        /// <summary>
        /// Disables the console's quick edit mode.
        /// </summary>
        /// <returns>True if quick edit mode was successfully disabled; false, if otherwise.</returns>
        private static bool DisableQuickEdit()
        {
            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // Get current console mode
            if (!GetConsoleMode(consoleHandle, out uint consoleMode))
            {
                // ERROR: Unable to get console mode
                return false;
            }

            // Clear the quick edit bit in the mode flags
            consoleMode &= ~ENABLE_QUICK_EDIT;

            // Set the new mode
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                // ERROR: Unable to set console mode
                return false;
            }

            return true;
        }

        /// <summary>
        /// End all active <see cref="ITimers"/> system timers in preparation for application close.
        /// </summary>
        public void UnInitializeApp()
        {
            _initSystems.StopAllSystems();
        }

        /// <summary>
        /// Initializes information and monitoring systems, loading config file and processing command line arguments.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the program.</param>
        public void InitializeApp(string[] args)
        {
            Console.Clear();

            // Display Help Menu and wait for Any Key when command line blank
            if (args == null || args.Length <= 0)
                _initSystems.PreInitialization();

            string timestamp = DateTime.Now.ToString(DEFAULT_TIME_FORMAT);
            Console.WriteLine(DEFAULT_INIT_MSG, timestamp);

            // Disable mouse clicking
            DisableQuickEdit();

            try
            {
                // Initialize Application Properties from Defaults, File, and finally Commandline Overrides
                _initProperties.InitializeProperties(args);

                // Wait for Servers to come online
                _initServers.Wait();

                // Begin Main Server Monitor Timer
                _initSystems.InitializeSystems();
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at Main Initialization method");
            }
        }

    }
}
