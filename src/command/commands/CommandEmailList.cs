using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for writing every email label &amp; address
    /// in <see cref="IConfig_Manager.SMTP_Emails"/> onto the console in a numbered list.
    /// <para>
    /// Dependencies: <see cref="IConsole_Manager"/>, <see cref="IEmail_Manager"/>
    /// </para>
    /// </summary>
    class CommandEmailList : ICommands
    {
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that managers SMTP email alerts for the system.
        /// </summary>
        private readonly IEmail_Manager _emailManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandEmailList"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="emailManager">The object that managers SMTP email alerts for the system.</param>
        public CommandEmailList(IConsole_Manager consoleManager,
                          IEmail_Manager emailManager)
        {
            _consoleManager = consoleManager;
            _emailManager = emailManager;
        }


        #region command_parameters
        private const string DEFAULT_TITLE = " ==Emails List==";

        public string Name { get; } = "email list";
        public string Usage { get; } = "email list";
        public string Description { get; } = "Display email addresses list";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 2 && args[0].ToLower() == "email" && args[1].ToLower() == "list";
        }

        public void Execute(string[] args)
        {
            Dictionary<string, string> emailAddresses = _emailManager.GetEmailAddresses();

            if (emailAddresses != null)
                _consoleManager.WriteList(DEFAULT_TITLE, emailAddresses);
        }

    }
}
