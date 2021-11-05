using Newtonsoft.Json;

namespace PluginAthenaHealth.DataContracts
{
    public class ApiError
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}