using System;
using System.Collections.Generic;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A Commands class which implements the <see cref="ICommands"/> interface
    /// providing access to a method for sending a test email to any valid address in <see cref="IConfig_Manager.SMTP_Emails"/>.
    /// <para>
    /// Dependencies: <see cref="IConsole_Manager"/>, <see cref="IEmail_Manager"/>
    /// </para>
    /// </summary>
    class CommandEmailTest : ICommands
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
        /// Initializes a new instance of the <see cref="CommandEmailTest"/> class with the specified interfaces.
        /// </summary>
        /// <param name="consoleManager">The object that manages the console input/output of the system.</param>
        /// <param name="emailManager">The object that managers SMTP email alerts for the system.</param>
        public CommandEmailTest(IConsole_Manager consoleManager,
                          IEmail_Manager emailManager)
        {
            _consoleManager = consoleManager;
            _emailManager = emailManager;
        }


        #region command_parameters
        private const string DEFAULT_TITLE = " ==Test Emails List==";

        public string Name { get; } = "email test";
        public string Usage { get; } = "email test";
        public string Description { get; } = "Send a test Alert email to every address on the list\n";
        public bool ConfigSetting { get; } = false;
        #endregion


        public bool CanExecute(string[] args)
        {
            return args.Length == 2 && args[0].ToLower() == "email" && args[1].ToLower() == "test";
        }

        public void Execute(string[] args)
        {
            string emailResult = ConfirmTestEmail();
            Console.WriteLine("\n -{0} testing email Alerts!", emailResult != null ? "Success" : "Failure");
            Console.WriteLine(" -{0}", emailResult != null ? "Test " + emailResult : "Please check email addresses and/or SMTP config settings!");
        }

        private bool DisplayEmailsList()
        {
            Dictionary<string, string> emailAddresses = _emailManager.GetEmailAddresses();

            if (emailAddresses == null)
                return false;

            _consoleManager.WriteList(DEFAULT_TITLE, emailAddresses);
            return true;
        }

        private string ConfirmTestEmail()
        {
            try
            {
                if (DisplayEmailsList())
                {
                    Console.WriteLine("Are you sure you want to send a test email to each address on this list?");
                    Console.WriteLine();

                    string[] choice = { "1" };
                    int emailChoice = _consoleManager.GetInputChoice(choice, "Type a 1 to confirm or type 0 to cancel");
                    //Console.WriteLine();
                    if (emailChoice > 0)
                    {
                        return _emailManager.SendAlertEmails();
                    }
                }
                else
                {
                    Console.WriteLine(" -Emails List is empty!");
                }
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at CommandEmailTest in ConfirmTestEmail method");
            }
            return null;
        }
    }
}
