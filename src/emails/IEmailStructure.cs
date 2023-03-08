namespace ServerMonitorSystem
{
    /// <summary>
    /// Represents a container for email SMTP server information.
    /// </summary>
    public interface IEmailSMTP
    {
        /// <summary>
        /// Admin Names and Email Addresses for email Alerts
        /// (whitespace separator, ex 'name address')
        /// </summary>
        string[] SmtpEmails { get; set; }
        /// <summary>
        /// The SMTP protocol of this SMTP server
        /// </summary>
        string SmtpProtocol { get; set; }
        /// <summary>
        /// The address of this SMTP server
        /// </summary>
        string SmtpAddress { get; set; }
        /// <summary>
        /// The password of this SMTP server
        /// </summary>
        string SmtpPassword { get; set; }
        /// <summary>
        /// The port number of this SMTP server
        /// </summary>
        int SmtpPort { get; set; }
        /// <summary>
        /// The SMTP credentials of this SMTP server (2 or 3 elements only)
        /// </summary>
        string[] SmtpCredentials { get; set; }
        /// <summary>
        /// The boolean to enable SSL for this SMTP server (default true)
        /// </summary>
        bool? SmtpEnableSsl { get; set; }
    }

    /// <summary>
    /// Represents a container for email content information.
    /// </summary>
    public interface IEmailContent
    {
        /// <summary>
        /// The subject line for the email
        /// </summary>
        string Subject { get; set; }
        /// <summary>
        /// The text body content for the email
        /// </summary>
        string Body { get; set; }
    }

    /// <summary>
    /// Represents a container for email attachment information.
    /// </summary>
    public interface IEmailAttachment
    {
        /// <summary>
        /// Array containing memory info at last monitoring interval. (available, total, used)
        /// </summary>
        float[] MemoryData { get; set; }
        /// <summary>
        /// Falcon BMS shared memory info of online player count at last monitoring interval.
        /// </summary>
        int PilotCount { get; set; }
        /// <summary>
        /// Memory Logging CSV file path for email attachment
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// The boolean to indicate whether to attach CSV log file to email
        /// </summary>
        bool? AttachFile { get; set; }
    }

    /// <summary>
    /// Represents a container for email custom HTML configuration information.
    /// </summary>
    public interface IEmailConfiguration
    {
        /// <summary>
        /// The displayed 'from' name in alert emails
        /// </summary>
        string ServerName { get; set; }
        /// <summary>
        /// The HTML banner img alt value will display Group Name in emails (when banner image not shown/blocked)
        /// </summary>
        string GroupName { get; set; }
        /// <summary>
        /// The default reply-to address displayed in emails
        /// </summary>
        string ReplyName { get; set; }
        /// <summary>
        /// The HTML link address when clicking on the group banner image in emails
        /// </summary>
        string GroupWebsite { get; set; }
        /// <summary>
        /// The HTML img source direct link to the group banner image file
        /// </summary>
        string GroupBanner { get; set; }
        /// <summary>
        /// An HTML img source direct link to a utility spacer image for tables (1px Square)
        /// </summary>
        string HtmlSpacer { get; set; }
        /// <summary>
        /// An HTML background color style value for the slightly darker area framing the email text body
        /// </summary>
        string BackgroundDark { get; set; }
        /// <summary>
        /// An HTML background color style value for the standard lighter area behind the main email body text
        /// </summary>
        string BackgroundLight { get; set; }
        /// <summary>
        /// An HTML color style value of line separating banner image and email body
        /// </summary>
        string BodyLineColor { get; set; }
        /// <summary>
        /// An HTML font color style value of text in email body
        /// </summary>
        string BodyFontColor { get; set; }
        /// <summary>
        /// An HTML font size style value of slightly larger text in email subject
        /// </summary>
        string SubjectFontSize { get; set; }
        /// <summary>
        /// An HTML font size style value of standard text in email body text
        /// </summary>
        string BodyFontSize { get; set; }

    }

}
