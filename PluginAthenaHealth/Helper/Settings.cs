using System;

namespace PluginAthenaHealth.Helper
{
    public class Settings
    {
        public string PracticeId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
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
        }

        public string GetBaseUrl()
        {
            return ProductionPractice
                ? "https://api.platform.athenahealth.com/v1/"
                : "https://api.preview.platform.athenahealth.com/v1/";
        }


        public string GetBaseAuthUrl()
        {
            // return ProductionPractice
            //     ? "https://{0}:{1}@api.platform.athenahealth.com/oauth2/v1/token"
            //     : "https://{0}:{1}@api.preview.platform.athenahealth.com/oauth2/v1/token";
            //
            return ProductionPractice
                ? "https://api.platform.athenahealth.com/oauth2/v1/token"
                : "https://api.preview.platform.athenahealth.com/oauth2/v1/token";
        }
    }
}