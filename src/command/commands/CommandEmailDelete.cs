using System;
using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for deleting an email address from <see cref="IConfig_Manager.SMTP_Emails"/>.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>, <see cref="IEmail_Manager"/>
    /// </para>
    /// </summary>
    class CommandEmailDelete : ICommands
    {
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;
        /// <summary>
        /// The object that manages the console input/output of the system.
        /// </summary>
        private readonly IConsole_Manager _consoleManager;
        /// <summary>
        /// The object that managers SMTP email alerts for the system.
        /// </summary>
        private readonly IEmail_Manager _emailManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandEmailDelete"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="emailManager">The object that managers SMTP email alerts for the system.</param>
        public CommandEmailDelete(IConfig_Manager configManager,
                                   IEmail_Manager emailManager,
                                   IConsole_Manager consoleManager)
        {
            _configManager = configManager;
            _emailManager = emailManager;
            _consoleManager = consoleManager;
        }


        #region command_parameters
        private const string DEFAULT_ADDRESS = "No address";
        private const string DEFAULT_ERROR = "Email list empty! ";
        private const string DEFAULT_TEXT = " has been removed from the emails list";
        private const string DEFAULT_TITLE = " ==Choose an Email Address to Delete==";

        private const string DEFAULT_PROPERTY_CHANGED = "smtp_emails";

        public string Name { get; } = "email delete";
        public string Usage { get; } = "email delete";
        public string Description { get; } = "Remove an email address from the list";
        public bool ConfigSetting { get; } = true;
        #endregion


        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        public bool CanExecute(string[] args)
        {
            return args.Length == 2 && args[0].ToLower() == "email" && args[1].ToLower() == "delete";
        }

        public void Execute(string[] args)
        {
            Dictionary<string, string> emailAddresses = _emailManager.GetEmailAddresses();
            string address = emailAddresses != null ? RemoveEmailAddress(emailAddresses) : DEFAULT_ERROR + DEFAULT_ADDRESS;
            Console.WriteLine(" -{0}{1}", address, DEFAULT_TEXT);
        }

        private string RemoveEmailAddress(Dictionary<string, string> emailAddresses)
        {
            // Write list of email addresses to choose from
            _consoleManager.WriteList(DEFAULT_TITLE, emailAddresses);

            // Get numeric user input (up to max number of addresses)
            int emailChoice = _consoleManager.GetInputChoice(_configManager.SMTP_Emails);

            if (emailChoice > 0)
            {
                string address = _configManager.SMTP_Emails[emailChoice - 1];
                List<string> emailList = new(_configManager.SMTP_Emails);
                emailList.Remove(address);

                // Set modified emails list to config property and save to config file
                _configManager.SMTP_Emails = emailList.ToArray();
                _configManager.SaveConfig();
                OverrideNotice();

                // Isolate name of email address listing
                string[] addressData = address.Split();
                address = addressData[0];

                return address;
            }
            else
            {
                return DEFAULT_ADDRESS;
            }
        }
    }
}
