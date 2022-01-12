using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static string GetUIJson()
        {
            var uiJsonObj = $@"{{
                ""ui:order"": [
                    ""FileStorageMethod"",
                    ""LocalPath"",
                    ""GoogleCloudStorageCredentialPath"".
                    ""GoogleCloudStorageBucket"".
                    ""GoogleCloudStorageDownloadPath""
                ]
            }}";
            
            return uiJsonObj;
        }
    }
}