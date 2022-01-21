using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PluginAthenaHealth.Helper
{
    public class Settings
    {
        public string PracticeId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Departments { get; set; }
        public string? AppointmentTypes { get; set; }
        public bool ProductionPractice { get; set; }

        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (String.IsNullOrEmpty(ClientId))
            {
                throw new Exception("the ClientId property must be set");
            }

            if (String.IsNullOrEmpty(ClientSecret))
            {
                throw new Exception("the ClientSecret property must be set");
            }

            if (String.IsNullOrEmpty(PracticeId))
            {
                throw new Exception("the PracticeId property must be set");
            }

            var date_regex = new Regex(@"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$");
            
            if (String.IsNullOrEmpty(StartDate))
            {
                throw new Exception("the StartDate property must be set");
            }
            if (!date_regex.IsMatch(StartDate))
            {
                throw new Exception("the StartDate property must be MM/dd/yyyy");
            }
        }

        public string GetBaseUrl(bool includeVersion = true)
        {
            if(includeVersion)
            {
                return ProductionPractice
                    ? "https://api.platform.athenahealth.com/v1/"
                    : "https://api.preview.platform.athenahealth.com/v1/";
            }
            else
            {
                return ProductionPractice
                    ? "https://api.platform.athenahealth.com/"
                    : "https://api.preview.platform.athenahealth.com/";
            }
        }


        public string GetBaseAuthUrl()
        {
            return ProductionPractice
                ? "https://api.platform.athenahealth.com/oauth2/v1/token"
                : "https://api.preview.platform.athenahealth.com/oauth2/v1/token";
        }
    }
}