using System.Collections.Generic;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods for returning a strings dictionary from the <see cref="IConfig_Manager.SMTP_Emails"/>
    /// array, and to the methods for sending emails to the designated SMTP server for delivery.
    /// </summary>
    public interface IEmail_Manager
    {
        /// <summary>
        /// Process valid emails in the <see cref="IConfig_Manager.SMTP_Emails"/> string array and return them as a Dictionary.
        /// </summary>
        /// <returns>Email addresses dictionary in format key=name value=address</returns>
        Dictionary<string, string> GetEmailAddresses();
        /// <summary>
        /// Process valid emails in the supplied string array and return them as a Dictionary.
        /// </summary>
        /// <param name="emailsList">Array of Admin Names and Email Addresses for email Alerts (whitespace separator, ex 'name address')</param>
        /// <returns>Email addresses dictionary in format key=name value=address</returns>
        Dictionary<string, string> GetEmailAddresses(string[] emailsList);

        /// <summary>
        /// Send test email(s) using SMTP server &amp; email details in <see cref="IConfig_Manager"/>.
        /// </summary>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        string SendAlertEmails();
        /// <summary>
        /// Send email to the SMTP server for delivery with the supplied subject and body test.
        /// </summary>
        /// <param name="subject">The subject line for the email to send.</param>
        /// <param name="body">The body of text for the email to send.</param>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        string SendAlertEmails(string subject, string body);
        /// <summary>
        /// Send email to the SMTP server for delivery with the supplied subject and body test, optionally
        /// attaching the CSV log file at the supplied file path.
        /// </summary>
        /// <param name="subject">The subject line for the email to send.</param>
        /// <param name="body">The body of text for the email to send.</param>
        /// <param name="filePath">The file path to the CSV log file to be attached.</param>
        /// <param name="attachCSV">A boolean to indicate whether this email should attach the CSV log file.</param>
        /// <returns>A comma-separated string of names/labels which emails were sent to, null if none or otherwise.</returns>
        string SendAlertEmails(string subject, string body, string filePath, bool attachCSV);
    }
}
