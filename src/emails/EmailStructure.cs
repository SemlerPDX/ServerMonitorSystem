namespace ServerMonitorSystem
{
    /// <summary>
    /// A class implementing <see cref="IEmailSMTP"/> to provide
    /// a container for email SMTP server information.
    /// </summary>
    class EmailSMTP : IEmailSMTP
    {
        /// <summary>
        /// Admin Names and Email Addresses for email Alerts
        /// (whitespace separator, ex 'name address')
        /// </summary>
        public string[] SmtpEmails { get; set; }
        /// <summary>
        /// The SMTP protocol of this SMTP server
        /// </summary>
        public string SmtpProtocol { get; set; }
        /// <summary>
        /// The address of this SMTP server
        /// </summary>
        public string SmtpAddress { get; set; }
        /// <summary>
        /// The password of this SMTP server
        /// </summary>
        public string SmtpPassword { get; set; }
        /// <summary>
        /// The port number of this SMTP server
        /// </summary>
        public int SmtpPort { get; set; }
        /// <summary>
        /// The SMTP credentials of this SMTP server (2 or 3 elements only)
        /// </summary>
        public string[] SmtpCredentials { get; set; }
        /// <summary>
        /// The boolean to enable SSL for this SMTP server (default true)
        /// </summary>
        public bool? SmtpEnableSsl { get; set; }
    }

    /// <summary>
    /// A class implementing <see cref="IEmailContent"/> to provide
    /// a container for email content information.
    /// </summary>
    class EmailContent : IEmailContent
    {
        /// <summary>
        /// The subject line for the email
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The text body content for the email
        /// </summary>
        public string Body { get; set; }
    }

    /// <summary>
    /// A class implementing <see cref="IEmailAttachment"/> to provide
    /// a container for email attachment information.
    /// </summary>
    class EmailAttachment : IEmailAttachment
    {
        /// <summary>
        /// Array containing memory info at last monitoring interval. (available, total, used)
        /// </summary>
        public float[] MemoryData { get; set; }
        /// <summary>
        /// Falcon BMS shared memory info of online player count at last monitoring interval.
        /// </summary>
        public int PilotCount { get; set; }
        /// <summary>
        /// Memory Logging CSV file path for email attachment
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// The boolean to indicate whether to attach CSV log file to email
        /// </summary>
        public bool? AttachFile { get; set; }
    }

    /// <summary>
    /// A class implementing <see cref="IEmailConfiguration"/> to provide
    /// a container for email custom HTML configuration information.
    /// </summary>
    class EmailConfiguration : IEmailConfiguration
    {
        /// <summary>
        /// The displayed 'from' name in alert emails
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// The HTML banner img alt value will display Group Name in emails (when banner image not shown/blocked)
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// The default reply-to address displayed in emails
        /// </summary>
        public string ReplyName { get; set; }
        /// <summary>
        /// The HTML link address when clicking on the group banner image in emails
        /// </summary>
        public string GroupWebsite { get; set; }
        /// <summary>
        /// The HTML img source direct link to the group banner image file
        /// </summary>
        public string GroupBanner { get; set; }
        /// <summary>
        /// An HTML img source direct link to a utility spacer image for tables (1px Square)
        /// </summary>
        public string HtmlSpacer { get; set; }
        /// <summary>
        /// An HTML background color style value for the slightly darker area framing the email text body
        /// </summary>
        public string BackgroundDark { get; set; }
        /// <summary>
        /// An HTML background color style value for the standard lighter area behind the main email body text
        /// </summary>
        public string BackgroundLight { get; set; }
        /// <summary>
        /// An HTML color style value of line separating banner image and email body
        /// </summary>
        public string BodyLineColor { get; set; }
        /// <summary>
        /// An HTML font color style value of text in email body
        /// </summary>
        public string BodyFontColor { get; set; }
        /// <summary>
        /// An HTML font size style value of slightly larger text in email subject
        /// </summary>
        public string SubjectFontSize { get; set; }
        /// <summary>
        /// An HTML font size style value of standard text in email body text
        /// </summary>
        public string BodyFontSize { get; set; }
    }
}
