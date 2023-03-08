using System;
using System.Collections.Generic;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The main Emails Manager class implementing <see cref="IEmail_Manager"/> to provide access to
    /// methods for returning a strings dictionary from the <see cref="IConfig_Manager.SMTP_Emails"/>
    /// array, and to the methods for sending emails to the designated SMTP server for delivery.
    /// <para>
    /// Dependencies: <see cref="IEmailSend"/>, <see cref="IEmailSMTP"/>, <see cref="IEmailContent"/>,
    /// <br><see cref="IEmailAttachment"/>, <see cref="IEmailConfiguration"/>, <see cref="IConfig_Manager"/></br>
    /// </para>
    /// </summary>
    class Email_Manager : IEmail_Manager
    {
        /// <summary>
        /// The object responsible for sending emails to the designated SMTP server for delivery.
        /// </summary>
        private readonly IEmailSend _emailSend;
        /// <summary>
        /// The object containing email SMTP server information.
        /// </summary>
        private readonly IEmailSMTP _emailSMTP;
        /// <summary>
        /// The object containing email content information.
        /// </summary>
        private readonly IEmailContent _emailContent;
        /// <summary>
        /// The object containing email attachment information.
        /// </summary>
        private readonly IEmailAttachment _emailAttachment;
        /// <summary>
        /// The object containing email custom HTML configuration information.
        /// </summary>
        private readonly IEmailConfiguration _emailConfiguration;
        /// <summary>
        /// The object that manages the configuration settings of the system.
        /// </summary>
        private readonly IConfig_Manager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Email_Manager"/> class with the specified interfaces.
        /// </summary>
        /// <param name="emailSend">The object responsible for sending emails to the designated SMTP server for delivery.</param>
        /// <param name="emailSMTP">The object containing email SMTP server information.</param>
        /// <param name="emailContent">The object containing email content information.</param>
        /// <param name="emailAttachment">The object containing email attachment information.</param>
        /// <param name="emailConfiguration">The object containing email custom HTML configuration information.</param>
        /// <param name="configManager">The object that manages the configuration settings of the system.</param>
        public Email_Manager(IEmailSend emailSend,
                        IEmailSMTP emailSMTP,
                        IEmailContent emailContent,
                        IEmailAttachment emailAttachment,
                        IEmailConfiguration emailConfiguration,
                        IConfig_Manager configManager)
        {
            _emailSend = emailSend;
            _emailSMTP = emailSMTP;
            _emailContent = emailContent;
            _emailAttachment = emailAttachment;
            _emailConfiguration = emailConfiguration;
            _configManager = configManager;
        }

        /// <summary>
        /// Process valid emails in the <see cref="IConfig_Manager.SMTP_Emails"/> string array and return them as a Dictionary.
        /// </summary>
        /// <returns>Email addresses dictionary in format key=name value=address</returns>
        public Dictionary<string, string> GetEmailAddresses() { return GetEmailAddresses(null); }
        /// <summary>
        /// Process valid emails in the supplied string array and return them as a Dictionary.
        /// </summary>
        /// <param name="emailsList">Array of Admin Names and Email Addresses for email Alerts (whitespace separator, ex 'name address')</param>
        /// <returns>Email addresses dictionary in format key=name value=address</returns>
        public Dictionary<string, string> GetEmailAddresses(string[] emailsList)
        {
            Dictionary<string, string> emailAddresses = new();

            if (emailsList == null)
                emailsList = _configManager.SMTP_Emails;

            try
            {
                if (emailsList == null && emailsList.Length <= 0)
                    throw new Exceptions("Invalid email address list");

                if (emailsList != null && emailsList.Length != 0)
                {
                    foreach (string address in emailsList)
                    {
                        string[] addressData = address.Replace("'", "").Split();

                        if (addressData.Length == 2 && !string.IsNullOrWhiteSpace(addressData[1]) && addressData[1].Contains("@"))
                            emailAddresses.Add(addressData[0].Replace("'", ""), addressData[1].Replace("'", "").Trim());
                    }
                }
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at GetEmailAddresses method");
            }

            if (emailAddresses.Count == 0)
            {
                return null;
            }

            return emailAddresses;
        }

        /// <summary>
        /// Send test email(s) using SMTP server &amp; email details in <see cref="IConfig_Manager"/>.
        /// </summary>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        public string SendAlertEmails() { return SendAlertEmails(null, null); }
        /// <summary>
        /// Send email to the SMTP server for delivery with the supplied subject and body test.
        /// </summary>
        /// <param name="subject">The subject line for the email to send.</param>
        /// <param name="body">The body of text for the email to send.</param>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        public string SendAlertEmails(string subject, string body) { return SendAlertEmails(subject, body, null, false); }
        /// <summary>
        /// Send email to the SMTP server for delivery with the supplied subject and body test, optionally
        /// attaching the CSV log file at the supplied file path.
        /// </summary>
        /// <param name="subject">The subject line for the email to send.</param>
        /// <param name="body">The body of text for the email to send.</param>
        /// <param name="filePath">The file path to the CSV log file to be attached.</param>
        /// <param name="attachCSV">A boolean to indicate whether this email should attach the CSV log file.</param>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        public string SendAlertEmails(string subject, string body, string filePath, bool attachCSV)
        {
            try
            {
                // Prepare all parameters for EmailConfiguration, EmailContent, and EmailAttachment
                _emailSMTP.SmtpEmails = _configManager.SMTP_Emails;
                _emailSMTP.SmtpCredentials = _configManager.SMTP_Credentials;
                _emailSMTP.SmtpProtocol = _configManager.SMTP_Protocol;
                _emailSMTP.SmtpAddress = _configManager.SMTP_Address;
                _emailSMTP.SmtpPassword = _configManager.SMTP_Password;
                _emailSMTP.SmtpPort = _configManager.SMTP_Port;
                _emailSMTP.SmtpEnableSsl = _configManager.SMTP_EnableSsl;

                _emailContent.Subject = subject;
                _emailContent.Body = body;

                _emailAttachment.MemoryData = _configManager.FormerMemoryInfo;
                _emailAttachment.PilotCount = _configManager.FormerPlayerCount;
                _emailAttachment.FilePath = filePath;
                _emailAttachment.AttachFile = attachCSV;

                _emailConfiguration.ServerName = _configManager.EmailServerName;
                _emailConfiguration.GroupName = _configManager.EmailGroupName;
                _emailConfiguration.ReplyName = _configManager.EmailReplyName;
                _emailConfiguration.GroupWebsite = _configManager.EmailGroupWebsite;
                _emailConfiguration.GroupBanner = _configManager.EmailGroupBanner;
                _emailConfiguration.HtmlSpacer = _configManager.EmailSpacer;
                _emailConfiguration.BackgroundDark = _configManager.EmailBodyBackground_Dark;
                _emailConfiguration.BackgroundLight = _configManager.EmailBodyBackground_Light;
                _emailConfiguration.BodyLineColor = _configManager.EmailBodyLineColor;
                _emailConfiguration.BodyFontColor = _configManager.EmailBodyFontColor;
                _emailConfiguration.SubjectFontSize = _configManager.EmailSubjectFontSize;
                _emailConfiguration.BodyFontSize = _configManager.EmailBodyFontSize;

                // Send Alert Email(s) - return will be names/labels of emails successfully sent
                Dictionary<string, string> emailAddresses = GetEmailAddresses(_configManager.SMTP_Emails);

                Dictionary<string, string> filteredEmailAddresses = new();
                foreach (var kvp in emailAddresses)
                {
                    if (!kvp.Key.Contains("example") && !kvp.Value.Contains("example"))
                    {
                        filteredEmailAddresses[kvp.Key] = kvp.Value;
                    }
                }

                if (filteredEmailAddresses.Count == 0)
                    throw new Exception("No valid email addresses found");

                return _emailSend.Send(filteredEmailAddresses);
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at SendAlertEmails method");
            }
            return null;
        }
    }
}
