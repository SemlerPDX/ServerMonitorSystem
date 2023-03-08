using System.Collections.Generic;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// Provide access to the method for sending SMTP email alerts.
    /// </summary>
    public interface IEmailSend
    {
        /// <summary>
        /// Format HTML email and send to specified SMTP server for delivery.
        /// </summary>
        /// <param name="emailAddresses">Email addresses dictionary in format key=name value=address</param>
        /// <returns>A List(string) of names/labels which emails were sent to, null if none or otherwise</returns>
        /// <exception cref="Exceptions">Thrown when emailAddresses is null or empty or when no valid outgoing address names found or when SMTP client data are invalid. </exception>
        /// <exception cref="Exceptions">Thrown when Email content data is invalid. </exception>
        /// <exception cref="Exceptions">Thrown when email attachment data is invalid. </exception>
        string Send(Dictionary<string, string> emailAddresses);
    }
}
