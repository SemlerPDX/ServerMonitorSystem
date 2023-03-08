using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// A custom attribute class used to annotate properties in a configuration class.
    /// </summary>
    class ConfigPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets the position of the annotated property within the configuration class.
        /// </summary>
        public int Position { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigPropertyAttribute"/> class with the specified position.
        /// </summary>
        /// <param name="position">The position of the annotated property within the configuration class.</param>
        public ConfigPropertyAttribute(int position)
        {
            Position = position;
        }
    }


    /// <summary>
    /// Represents the configuration settings for this application which are
    /// present in the config python file, and which can be overridden
    /// by command line arguments, and defines their default values.
    /// <para>
    /// These properties are provided to the application through matching
    /// reference properties in the <see cref="ICommand_Manager"/> interface.
    /// </para>
    /// </summary>
    class ConfigPython : IConfigPython
    {
        [ConfigProperty(0)]
        public int Interval { get; set; } = 1;
        [ConfigProperty(1)]
        public float MaxMem { get; set; } = 7168F;
        [ConfigProperty(2)]
        public bool AutoKill { get; set; }
        [ConfigProperty(3)]
        public float KillMem { get; set; } = 11766F;

        [ConfigProperty(4)]
        public bool AlertsALL { get; set; }
        [ConfigProperty(5)]
        public bool AlertsGame { get; set; }
        [ConfigProperty(6)]
        public bool AlertsVOIP { get; set; }
        [ConfigProperty(7)]
        public bool AlertsMEM { get; set; }

        [ConfigProperty(8)]
        public bool EmailsALL { get; set; }
        [ConfigProperty(9)]
        public bool EmailsGame { get; set; }
        [ConfigProperty(10)]
        public bool EmailsVOIP { get; set; }
        [ConfigProperty(11)]
        public bool EmailsMEM { get; set; }

        [ConfigProperty(12)]
        public string PlayerDesignation { get; set; } = "Pilot";
        [ConfigProperty(13)]
        public string GameServerName { get; set; } = "Falcon BMS";
        [ConfigProperty(14)]
        public string GameOfflineAlertSubject { get; set; } = "A Falcon BMS Server CTD Alert has been triggered!";
        [ConfigProperty(15)]
        public string GameOfflineAlertMessage { get; set; } = "The Falcon BMS is not responding, is closed, " +
                     "or has crashed to desktop and must be manually started.";
        [ConfigProperty(16)]
        public string VoipServerName { get; set; } = "IVC Server";
        [ConfigProperty(17)]
        public string VoipOfflineAlertSubject { get; set; } = "An IVC Server CTD Alert has been triggered!";
        [ConfigProperty(18)]
        public string VoipOfflineAlertMessage { get; set; } = "The Falcon BMS IVC Server is not responding, is closed, " +
                     "or has crashed to desktop and must be manually started.<br>" +
                     "The IVC Server also may have simply not been started or started properly.";

        [ConfigProperty(19)]
        public bool Logging { get; set; }
        [ConfigProperty(20)]
        public int MinTime { get; set; } = 60;
        [ConfigProperty(21)]
        public int Frequency { get; set; } = 1;
        [ConfigProperty(22)]
        public int Duration { get; set; } = 0;
        [ConfigProperty(23)]
        public string LogFilePath { get; set; } = @"C:\folder example\filename_example.csv";
        [ConfigProperty(24)]
        public long LogSize { get; set; } = 1024L;

        [ConfigProperty(25)]
        public string[] SMTP_Emails { get; set; } = { "Jack example@gmail.com", "Jill example@hotmail.com" };
        [ConfigProperty(26)]
        public string SMTP_Protocol { get; set; } = "smtp.gmail.com";
        [ConfigProperty(27)]
        public string SMTP_Address { get; set; } = "example@gmail.com";
        [ConfigProperty(28)]
        public string SMTP_Password { get; set; } = "example-smtp-password";
        [ConfigProperty(29)]
        public int SMTP_Port { get; set; } = 0;
        [ConfigProperty(30)]
        public string[] SMTP_Credentials { get; set; } = { "example@gmail.com", "example-smtp-password" };
        [ConfigProperty(31)]
        public bool SMTP_EnableSsl { get; set; } = true;

        [ConfigProperty(32)]
        public string EmailServerName { get; set; } = "\"VG Labs Server Bot\"";
        [ConfigProperty(33)]
        public string EmailGroupName { get; set; } = "VETERANS-GAMING";
        [ConfigProperty(34)]
        public string EmailReplyName { get; set; } = "noreply@veterans-gaming.com";
        [ConfigProperty(35)]
        public string EmailGroupWebsite { get; set; } = "https://veterans-gaming.com/";

        [ConfigProperty(36)]
        public string EmailGroupBanner { get; set; } = "https://veterans-gaming.com/uploads/monthly_2021_01/veterans-gaming_com.png.a6a0567842dada066e04bf753106ffb4.png"; //750px by 150px or so is best
        [ConfigProperty(37)]
        public string EmailSpacer { get; set; } = "https://veterans-gaming.com/applications/core/interface/email/spacer.png"; // Any 1px by 1px custom spacer img
        [ConfigProperty(38)]
        public string EmailBodyBackground_Dark { get; set; } = "#f1f1f1";
        [ConfigProperty(39)]
        public string EmailBodyBackground_Light { get; set; } = "#ffffff";
        [ConfigProperty(40)]
        public string EmailBodyLineColor { get; set; } = "#db2c00";
        [ConfigProperty(41)]
        public string EmailBodyFontColor { get; set; } = "#333333";
        [ConfigProperty(42)]
        public string EmailSubjectFontSize { get; set; } = "font-size:17px";
        [ConfigProperty(43)]
        public string EmailBodyFontSize { get; set; } = "font-size:15px";
    }

}
