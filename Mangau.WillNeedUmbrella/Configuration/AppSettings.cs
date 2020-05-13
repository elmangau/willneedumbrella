using System;
using System.Collections.Generic;
using System.Text;

namespace Mangau.WillNeedUmbrella.Configuration
{
    public class AppSettings
    {
        public ConnectionStringsSettings ConnectionStrings { get; set; }

        public AuthenticationSettings Authentication { get; set; }

        public int LogoutExpiredInterval { get; set; }

        public int WeatherUpdateInterval { get; set; }

        public string WeatherUrl { get; set; }

        public string WeatherKey { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }
    }
}
