using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using VGLabsFoundationLite;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Emails class implementing <see cref="IEmailSend"/> to provide
    /// access to the method for sending SMTP email alerts.
    /// <para>
    /// Dependencies: <see cref="IEmailSMTP"/>, <see cref="IEmailContent"/>, <see cref="IEmailAttachment"/>,
    /// <br><see cref="IEmailConfiguration"/></br>
    /// </para>
    /// </summary>
    class EmailSend : IEmailSend
    {
        /// <summary>
        /// The object containing email details.
        /// </summary>
        private readonly EmailDetails _newMail;

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
        /// Initializes a new instance of the <see cref="EmailSend"/> class with the specified interfaces.
        /// </summary>
        /// <param name="emailSMTP">The object containing email SMTP server information.</param>
        /// <param name="emailContent">The object containing email content information.</param>
        /// <param name="emailAttachment">The object containing email attachment information.</param>
        /// <param name="emailConfiguration">The object containing email custom HTML configuration information.</param>
        public EmailSend(IEmailSMTP emailSMTP,
                        IEmailContent emailContent,
                        IEmailAttachment emailAttachment,
                        IEmailConfiguration emailConfiguration)
        {
            _emailSMTP = emailSMTP;
            _emailContent = emailContent;
            _emailAttachment = emailAttachment;
            _emailConfiguration = emailConfiguration;

            _newMail = new EmailDetails();
        }

        private static readonly string DEFAULT_EMAIL_SUBJECT = "Test Email Successful!";
        private static readonly string DEFAULT_EMAIL_BODY = "(This is only a test of the email system) - If you have received this message, it means SMTP mail is working.";
        private static readonly float[] DEFAULT_MEMORY_DATA = { 0f, 0f, 0f };

        /// <summary>
        /// A class providing a container for email details.
        /// </summary>
        public class EmailDetails
        {
            public string[] SmtpCredentials;
            public string SmtpProtocol;
            public string SmtpAddress;
            public string SmtpPassword;
            public int SmtpPort;
            public bool SmtpEnableSsl;
            public string Subject;
            public string Body;
            public string ServerName;
            public string GroupName;
            public string ReplyName;
            public string GroupWebsite;
            public string GroupBanner;
            public string HtmlSpacer;
            public string BackgroundDark;
            public string BackgroundLight;
            public string BodyLineColor;
            public string BodyFontColor;
            public string SubjectFontSize;
            public string BodyFontSize;
            public float[] MemoryData;
            public int PilotCount;
            public string FilePath;
            public bool AttachFile;
        }

        /// <summary>
        /// A method to format the HTML display for memory metrics in GB within the email.
        /// </summary>
        /// <param name="memoryInMB">The memory data in megabytes.</param>
        /// <param name="metricName">The metric name for this memory data type.</param>
        /// <returns>An HTML formatted string with supplied megabytes of memory displayed in GB.</returns>
        private string FormatMemoryMetric(float memoryInMB, string metricName)
        {
            // MB to GB string format conversion method for email HTML table
            float memoryInGB = memoryInMB / 1024f;
            return string.Format(@"{0}{1:F2} GB</td></tr>", metricName, memoryInGB);
        }

        /// <summary>
        /// Sets the email SMTP <see cref="EmailDetails"/> values, and creates a list
        /// of names/labels corresponding to valid email addresses which emails will be sent to.
        /// </summary>
        /// <param name="emailAddresses">A dictionary containing names/labels as keys and their corresponding email addresses as values.</param>
        /// <returns>The list of names/labels corresponding to valid email addresses which emails will be sent to.</returns>
        private List<string> SetSmtpServer(Dictionary<string, string> emailAddresses)
        {
            List<string> names = new();

            try
            {
                bool emailHasSMTP = _emailSMTP != null;
                bool emailHasCredentials = _emailSMTP.SmtpCredentials != null && _emailSMTP.SmtpCredentials.Length > 1;

                if (!emailHasCredentials)
                    throw new Exceptions("emailHasCredentials null or invalid - cannot configure SMTP server! Check config!");

                // Gather Email SMTP properties
                _newMail.SmtpCredentials = _emailSMTP.SmtpCredentials;
                _newMail.SmtpProtocol = emailHasSMTP && !String.IsNullOrWhiteSpace(_emailSMTP.SmtpProtocol) ? _emailSMTP.SmtpProtocol.Replace("'", "") : "";
                _newMail.SmtpAddress = emailHasSMTP && !String.IsNullOrWhiteSpace(_emailSMTP.SmtpAddress) ? _emailSMTP.SmtpAddress.Replace("'", "") : "example";
                _newMail.SmtpPassword = emailHasSMTP && !String.IsNullOrWhiteSpace(_emailSMTP.SmtpPassword) ? _emailSMTP.SmtpPassword.Replace("'", "") : "example";
                _newMail.SmtpPort = emailHasSMTP && _emailSMTP.SmtpPort > 0 ? _emailSMTP.SmtpPort : 0;
                _newMail.SmtpEnableSsl = _emailSMTP.SmtpEnableSsl ?? true;

                if (emailAddresses == null)
                    throw new Exceptions("emailAddresses null - no valid outgoing addresses found! Check config!");

                // Create a list of valid names to return upon success
                names = emailAddresses
                    .Where(ea => !ea.Value.Contains("example"))
                    .Select(ea => ea.Key)
                    .ToList();

                if (names.Count == 0)
                    throw new Exceptions("No valid outgoing address names found! Check config!");

                // Cannot proceed without protocol or proper address
                if (String.IsNullOrWhiteSpace(_newMail.SmtpProtocol))
                    throw new Exceptions("Invalid SMTP Protocol data! Check config!");

                if (_newMail.SmtpAddress.Contains("example"))
                    throw new Exceptions("Invalid SMTP Address data! Check config!");

                if (_newMail.SmtpPassword.Contains("example"))
                    throw new Exceptions("Invalid SMTP Password data! Check config!");

                if (_newMail.SmtpPort == 0)
                    throw new Exceptions("Invalid SMTP Port data (0)! Check config!");

            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at EmailSMTP in Send method");
                return null;
            }

            return names;
        }

        /// <summary>
        /// Sets the email content <see cref="EmailDetails"/> values.
        /// </summary>
        /// <returns>True when email content details have been set to <see cref="EmailDetails"/> values; false, if otherwise.</returns>
        private bool SetContent()
        {
            try
            {
                bool emailHasContent = _emailContent != null;

                // If the subject and body are empty, this is a test email attempt
                _newMail.Subject = emailHasContent && !String.IsNullOrWhiteSpace(_emailContent.Subject) ? _emailContent.Subject.Replace("'", "") : DEFAULT_EMAIL_SUBJECT;
                _newMail.Body = emailHasContent && !String.IsNullOrWhiteSpace(_emailContent.Body) ? _emailContent.Body.Replace("'", "") : DEFAULT_EMAIL_BODY;
                return true;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at EmailContent in Send method");
                return false;
            }
        }

        /// <summary>
        /// Sets the email attachment <see cref="EmailDetails"/> values.
        /// </summary>
        /// <returns>True when email has attachment properties which have been set to <see cref="EmailDetails"/> values; false, if otherwise.</returns>
        private bool SetAttachments()
        {
            try
            {
                bool emailHasAttachment = _emailAttachment != null;
                bool emailHasData = _emailAttachment.MemoryData != null && _emailAttachment.MemoryData.Length > 0;
                bool emailHasFile = !String.IsNullOrWhiteSpace(_emailAttachment.FilePath) && File.Exists(_newMail.FilePath.Replace("'", ""));

                // Gather Email Attachment properties
                _newMail.MemoryData = emailHasAttachment && emailHasData ? _emailAttachment.MemoryData : DEFAULT_MEMORY_DATA;
                _newMail.PilotCount = emailHasAttachment && _emailAttachment.PilotCount > 0 ? _emailAttachment.PilotCount : 0;
                _newMail.FilePath = emailHasAttachment && emailHasFile ? _emailAttachment.FilePath.Replace("'", "") : null;
                _newMail.AttachFile = _emailAttachment.AttachFile.HasValue && _emailAttachment.AttachFile.Value && emailHasFile;
                return true;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at EmailAttachment in Send method");
                return false;
            }
        }

        /// <summary>
        /// Sets the email custom HTML style configuration to <see cref="EmailDetails"/> values.
        /// </summary>
        /// <returns>True when email has email configuration properties which have been set to <see cref="EmailDetails"/> values; false, if otherwise.</returns>
        private bool SetEmailStyling()
        {
            try
            {
                bool emailHasConfig = _emailConfiguration != null;

                if (emailHasConfig)
                {
                    foreach (var property in typeof(IEmailConfiguration).GetProperties())
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            string value = (string)property.GetValue(_emailConfiguration);
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                string propertyName = property.Name;
                                switch (propertyName)
                                {
                                    case nameof(_emailConfiguration.ServerName):
                                        _newMail.ServerName = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.GroupName):
                                        _newMail.GroupName = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.ReplyName):
                                        _newMail.ReplyName = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.GroupWebsite):
                                        _newMail.GroupWebsite = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.GroupBanner):
                                        _newMail.GroupBanner = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.HtmlSpacer):
                                        _newMail.HtmlSpacer = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.BackgroundDark):
                                        _newMail.BackgroundDark = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.BackgroundLight):
                                        _newMail.BackgroundLight = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.BodyLineColor):
                                        _newMail.BodyLineColor = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.BodyFontColor):
                                        _newMail.BodyFontColor = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.SubjectFontSize):
                                        _newMail.SubjectFontSize = value.Replace("'", "");
                                        break;
                                    case nameof(_emailConfiguration.BodyFontSize):
                                        _newMail.BodyFontSize = value.Replace("'", "");
                                        break;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at EmailConfiguration in Send method");
                return false;
            }
        }

        /// <summary>
        /// Assemble final email HTML body and set to the <see cref="EmailDetails.Body"/> value.
        /// </summary>
        /// <returns>True when email HTML body has been properly assembled and set to the <see cref="EmailDetails.Body"/> value; false, if otherwise.</returns>
        private bool AssembleEmailBody()
        {
            try
            {
                // Add memory data and player count info box to email body
                _newMail.Body += string.Format(@"<br><br><br><div><table style=""min-width:190px;width:auto;max-width:500px;border:3px;padding:4px;border-color:{0};border-style:inset;"">", _newMail.BodyLineColor);
                _newMail.Body += FormatMemoryMetric(_newMail.MemoryData[1], @"<tr><th align=""left"">RAM Maximum:</th><td align=""center"" style=""min-width:100%""></td><td align=""right"">");
                _newMail.Body += FormatMemoryMetric(_newMail.MemoryData[0], @"<tr><th align=""left"">RAM Available:</th><td align=""center"" style=""min-width:100%""></td><td align=""right"">");
                _newMail.Body += FormatMemoryMetric(_newMail.MemoryData[2], @"<tr><th align=""left"">RAM Used:</th><td align=""center"" style=""min-width:100%""></td><td align=""right"">");
                _newMail.Body += string.Format(@"<tr><th align=""left"">Online Pilots:</th><td align=""center"" style=""min-width:100%""></td><td align=""right"">{0}</td></tr></table></div>", _newMail.PilotCount);

                if (_newMail.AttachFile)
                    _newMail.Body += "<br><br>(see attached file for more information)";

                // Add custom styling and HTML wrapping to existing email body
                _newMail.Body = String.Format(@"<div bgcolor=""{4}"" style=""direction:ltr""><table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" bgcolor=""{4}""><tbody><tr height=""25""><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td></tr><tr><td dir=""ltr"" valign=""top""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr"" valign=""middle"" style=""font-family:'Helvetica Neue',helvetica,sans-serif;font-size:30px;font-weight:300;line-height:48px;padding-left:10px""><a href=""{1}"" style=""text-decoration:none;border:0"" target=""_blank""><img src=""{2}"" alt=""{0}""></a></td><td dir=""ltr"" valign=""top""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td></tr><tr height=""25""><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td></tr><tr><td dir=""ltr""><img src=""{3}"" width=""1"" height=""1"" alt=""""></td><td dir=""ltr"" width=""800"" id=""m_-3224917696149400074main"" bgcolor=""{5}"" style=""border-top:10px solid {6};line-height:1.5""><table width=""100%"" cellpadding=""20"" style=""table-layout:fixed""><tbody><tr><td dir=""ltr"" style=""font-family:'Helvetica Neue',helvetica,sans-serif;{9};color:{7};line-height:21px""><strong style=""{8}"">{10}</strong><br><br>{11}<br>&nbsp;<br>&nbsp;</td></tr></tbody></table></div><div style=""text-align:center;font-style:italic;color:#b1b1b1;background-color:{4};padding:20px;""><br>this email was sent via Server Monitor System<br>Copyright © 2023  CC BY-NC-ND  by SemlerPDX Feb2023</div></tr></tbody></table></div>",
                    _newMail.GroupName,
                    _newMail.GroupWebsite,
                    _newMail.GroupBanner,
                    _newMail.HtmlSpacer,
                    _newMail.BackgroundDark,
                    _newMail.BackgroundLight,
                    _newMail.BodyLineColor,
                    _newMail.BodyFontColor,
                    _newMail.SubjectFontSize,
                    _newMail.BodyFontSize,
                    _newMail.Subject,
                    _newMail.Body
                );
                return true;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at Body final assembly in Send method");
                return false;
            }
        }

        /// <summary>
        /// Format HTML email and send to specified SMTP server for delivery.
        /// </summary>
        /// <param name="emailAddresses">Email addresses dictionary in format key=name value=address</param>
        /// <returns>A List(string) of names/labels which emails were sent to, null if none or otherwise</returns>
        /// <exception cref="Exceptions">Thrown when emailAddresses is null or empty or when no valid outgoing address names found or when SMTP client data are invalid. </exception>
        /// <exception cref="Exceptions">Thrown when Email content data is invalid. </exception>
        /// <exception cref="Exceptions">Thrown when email attachment data is invalid. </exception>
        public string Send(Dictionary<string, string> emailAddresses)
        {
            try
            {
                List<string> names = new();
                names = SetSmtpServer(emailAddresses);

                if (names == null)
                    throw new Exceptions("Error setting SMTP server values");

                if (!SetContent())
                    throw new Exceptions("Error setting email content");

                if (!SetAttachments())
                    throw new Exceptions("Error setting email attachments");

                if (!SetEmailStyling())
                    throw new Exceptions("Error setting custom email styles");

                if (!AssembleEmailBody())
                    throw new Exceptions("Error assembling email body");

                // A string of names/labels to return which emails have been sent to
                string returnNames = $"{(names.Count != 1 ? "Emails have" : "An email has")} been sent to:  {string.Join(", ", names)}";

                SmtpClient SmtpServer = new(_newMail.SmtpProtocol);
                SmtpServer.Port = _newMail.SmtpPort;
                SmtpServer.EnableSsl = _newMail.SmtpEnableSsl;

                try
                {
                    // Try setting proper smtp client credentials
                    SmtpServer.Credentials = _newMail.SmtpCredentials.Length switch
                    {
                        2 => new System.Net.NetworkCredential(_newMail.SmtpCredentials[0].Replace("'", ""), _newMail.SmtpCredentials[1].Replace("'", "")),
                        3 => new System.Net.NetworkCredential(_newMail.SmtpCredentials[0].Replace("'", ""), _newMail.SmtpCredentials[1].Replace("'", ""), _newMail.SmtpCredentials[2].Replace("'", "")),
                        _ => throw new Exceptions("SMTP credentials are invalid!")
                    };
                }
                catch (Exceptions ex)
                {
                    ex.LogError("Error applying SMTP credentials in Send");
                    return null;
                }

                MailMessage mail = new()
                {
                    From = new MailAddress(_newMail.ReplyName, _newMail.ServerName)
                };

                foreach (var emailAddress in emailAddresses.Where(ea => !ea.Value.Contains("example")))
                {
                    mail.To.Add(emailAddress.Value);
                }

                mail.Subject = _newMail.Subject;
                mail.Body = _newMail.Body;
                mail.IsBodyHtml = true;

                try
                {
                    // Attach file to the email (if requested)
                    if (_newMail.AttachFile)
                    {
                        using (var file = new FileStream(_newMail.FilePath, FileMode.Open, FileAccess.Read))
                        {
                            // Send email with attachment
                            mail.Attachments.Add(new Attachment(file, Path.GetFileName(_newMail.FilePath), MediaTypeNames.Application.Octet));
                            SmtpServer.Send(mail);
                        }
                        return returnNames;
                    }
                }
                catch (Exceptions ex)
                {
                    ex.LogError("Error attaching file to email in Send method");
                }

                // Send email without attachment
                SmtpServer.Send(mail);

                return returnNames;
            }
            catch (Exceptions ex)
            {
                ex.LogError("Error at Send method");
                return null;
            }
        }

    }
}
