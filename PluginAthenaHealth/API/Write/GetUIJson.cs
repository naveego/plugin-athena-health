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
                    "FileStorageMethod",
                    "GoogleCloudStorageCredentialPath",
                }}
            };
            return JsonConvert.SerializeObject(uiJsonObj);
            // var uiJsonObj = $@"{{
            //     ""ui:order"": [
            //         ""FileStorageMethod"",
            //         ""LocalPath"",
            //         ""GoogleCloudStorageCredentialPath"".
            //         ""GoogleCloudStorageBucket"".
            //         ""GoogleCloudStorageDownloadPath""
            //     ]
            // }}";
            //
            // return uiJsonObj;
        }
    }
}