using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static string GetUIJson()
        {
            var uiJsonObj = new Dictionary<string, object>
            {
                {"ui:order", new []
                {
                    "Name",
                    "FileStorageMethod",
                    "GoogleCloudStorageCredentialPath",
                }}
            };
            return JsonConvert.SerializeObject(uiJsonObj);
        }
    }
}