using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for adding a new email address to <see cref="IConfig_Manager.SMTP_Emails"/>.
    /// <para>
    /// Dependencies: <see cref="IConfig_Manager"/>, <see cref="IConsole_Manager"/>
    /// </para>
    /// </summary>
    class CommandEmailAdd : ICommands
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
        /// Initializes a new instance of the <see cref="CommandEmailAdd"/> class with the specified interfaces.
        /// </summary>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        public CommandEmailAdd(IConfig_Manager configManager,
                          IConsole_Manager consoleManager)
        {
            _configManager = configManager;
            _consoleManager = consoleManager;
        }


        #region command_parameters
        private const string DEFAULT_PROMPT_ADDRESS = "Enter a new email address";
        private const string DEFAULT_PROMPT_LABEL = "Enter a name/label for this address";

        private const string DEFAULT_PROPERTY_CHANGED = "smtp_emails";

        public string Name { get; } = "email add";
        public string Usage { get; } = "email add";
        public string Description { get; } = "Add an email address to the list";
        public bool ConfigSetting { get; } = true;
        #endregion


        private void OverrideNotice()
        {
            if (_configManager.CommandLineArgsActive != null && _configManager.CommandLineArgsActive.Contains(DEFAULT_PROPERTY_CHANGED))
                Console.WriteLine(_configManager.DEFAULT_OVERRIDE_NOTICE);
        }

        public bool CanExecute(string[] args)
        {
            return args.Length == 2 && args[0].ToLower() == "email" && args[1].ToLower() == "add";
        }

        public void Execute(string[] args)
        {
            // Prompt user to enter email address
            string[] newEmailsList = AddEmail();
            bool saveSettings = newEmailsList != null && newEmailsList.Length > 0;
            Console.WriteLine(" -{0} entering email address!", saveSettings ? "Success" : "Failure");
            if (saveSettings)
            {
                _configManager.SMTP_Emails = newEmailsList;
                _configManager.SaveConfig();
                OverrideNotice();
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    IdnMapping idn = new();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string[] AddEmail()
        {
            string address = _consoleManager.GetInputText(DEFAULT_PROMPT_ADDRESS);
            if (!IsValidEmail(address))
                return null;

            string name = _consoleManager.GetInputText(DEFAULT_PROMPT_LABEL);
            if (String.IsNullOrWhiteSpace(name))
                return null;

            string emailAddress = name + " " + address;
            List<string> emailAddresses = new(_configManager.SMTP_Emails ?? new string[0]);
            emailAddresses.Add(emailAddress);

            return emailAddresses.ToArray();
        }

    }
}
