using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Factory
{
    public interface IApiClient
    {
        Task TestConnection();
        Settings GetSettings();
        Task<HttpResponseMessage> GetAsync(string path);
        Task<HttpResponseMessage> PostAsync(string path, Dictionary<string, string> postData);
        // Task<HttpResponseMessage> PostAsync(string path, StringContent json);
        Task<HttpResponseMessage> PutAsync(string path, StringContent json);
        Task<HttpResponseMessage> PatchAsync(string path, StringContent json);
        Task<HttpResponseMessage> DeleteAsync(string path);
    }
}