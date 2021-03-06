using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Naveego.Sdk.Logging;
using PluginAthenaHealth.API.Utility;
using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Factory
{
    public class ApiClient: IApiClient
    {
        private IApiAuthenticator Authenticator { get; set; }
        private static HttpClient Client { get; set; }
        private Settings Settings { get; set; }

        public Settings GetSettings()
        {
            return Settings;
        }
        public ApiClient(HttpClient client, Settings settings)
        {
            Authenticator = new ApiAuthenticator(client, settings);
            Client = client;
            Settings = settings;
            
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task TestConnection()
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder($"{Settings.GetBaseUrl().TrimEnd('/')}/{Settings.PracticeId}/{Constants.TestConnectionPath.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await Client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    //Pause until next midnight, API call reset at this moment
                    throw new Exception("API reached maximum amount of calls per day. More calls available at midnight GMT.");
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder(path);
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        // public async Task<HttpResponseMessage> PostAsync(string path, StringContent json)
        public async Task<HttpResponseMessage> PostAsync(string path, Dictionary<string, string> postData)
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder($"{Settings.GetBaseUrl().TrimEnd('/')}/{path.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var encodedItems = postData.Select(i => WebUtility.UrlEncode(i.Key) + "=" + WebUtility.UrlEncode(i.Value));
                var encodedContent = new StringContent(String.Join("&", encodedItems), null, "application/x-www-form-urlencoded");
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = encodedContent
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string path, StringContent json)
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder($"{Settings.GetBaseUrl().TrimEnd('/')}/{path.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = uri,
                    Content = json
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PatchAsync(string path, StringContent json)
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder($"{Settings.GetBaseUrl().TrimEnd('/')}/{path.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Patch,
                    RequestUri = uri,
                    Content = json
                };
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            try
            {
                var token = await Authenticator.GetToken();
                var uriBuilder = new UriBuilder($"{Settings.GetBaseUrl().TrimEnd('/')}/{path.TrimStart('/')}");
                var uri = new Uri(uriBuilder.ToString());
                
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = uri
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await Client.SendAsync(request);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                throw;
            }
        }
    }
}