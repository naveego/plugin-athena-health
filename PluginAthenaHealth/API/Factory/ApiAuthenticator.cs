using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Naveego.Sdk.Logging;
using Newtonsoft.Json;
using PluginAthenaHealth.DataContracts;
using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Factory
{
    public class ApiAuthenticator: IApiAuthenticator
    {
        private HttpClient Client { get; set; }
        private Settings Settings { get; set; }
        private string Token { get; set; }
        private DateTime ExpiresAt { get; set; }
        
        private readonly string AuthUrl = "";
        
        public ApiAuthenticator(HttpClient client, Settings settings)
        {
            Client = client;
            Settings = settings;
            ExpiresAt = DateTime.Now;
            Token = "";
            AuthUrl = string.Format(Settings.GetBaseAuthUrl(), Settings.ClientId, Settings.ClientSecret);
        }

        public async Task<string> GetToken()
        {
            // check if token is expired or will expire in 5 minutes or less
            if (DateTime.Compare(DateTime.Now.AddMinutes(5), ExpiresAt) >= 0)
            {
                return await GetNewToken();
            }
          
            return Token;
        }

        private async Task<string> GetNewToken()
        {
            try
            {
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", "athena/service/Athenanet.MDP.*")
                };

                var body = new FormUrlEncodedContent(formData);
                    
                var client = Client;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
                var response = await Client.PostAsync(AuthUrl, body);
                response.EnsureSuccessStatusCode();
                    
                var content = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
                    
                // update expiration and saved token
                ExpiresAt = DateTime.Now.AddSeconds(content.ExpiresIn);
                Token = content.AccessToken;

                return Token;
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }
    }
}