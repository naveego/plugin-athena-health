using System.Collections.Generic;
using Newtonsoft.Json;
using PluginAthenaHealth.API.Utility;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static string GetSchemaJson()
        {
            var schemaJsonObj = new Dictionary<string, object>
            {
                {"type", "object"},
                {"properties", new Dictionary<string, object>
                {
                    {"FileStorageMethod", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "File Storage Method"},
                        {"description", "The storage method of the patient charts"},
                        {"enum", new []{Constants.Local, Constants.GoogleCloudStorage}}
                    }},
                    {"LocalPath", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "Local Path"},
                        {"description", "If using local storage, enter the path to the directory containing patient charts"}
                    }},
                    {"GoogleCloudStorageCredentialPath", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "Google Cloud Storage Credential Path"},
                        {"description", "If using GCS, enter the path to the credentials key file"}
                    }},
                    {"GoogleCloudStorageBucket", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "Google Cloud Storage Bucket"},
                        {"description", "If using GCS, enter the name of the bucket being used"}
                    }},
                    {"GoogleCloudStorageDownloadPath", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "Google Cloud Storage Download Path"},
                        {"description", "If using GCS, enter the path to temporarily store uploaded files. Preferably an unused folder"}
                    }}
                }},
                {"required", new []
                {
                    "FileStorageMethod"
                }}
            };

            return JsonConvert.SerializeObject(schemaJsonObj);
            
        //     var schemaJsonObj = $@"{{
        //     ""type"": ""object"",
        //     ""properties"": {{
        //         ""FileStorageMethod"": {{
        //             ""type"": ""string"",
        //             ""title"": ""Name"",
        //             ""description"": ""The storage method of the patient charts"",
        //             ""enum"": [""{Constants.Local}"", ""{Constants.GoogleCloudStorage}""]
        //         }},
        //         ""LocalPath"": {{
        //             ""type"": ""string"",
        //             ""title"": ""Local Path"",
        //             ""description"": ""If using local storage, enter the path to the directory containing patient charts""
        //         }},
        //         ""GoogleCloudStorageCredentialPath"": {{
        //             ""type"": ""string"",
        //             ""title"": ""Google Cloud Storage Credential Path"",
        //             ""description"": ""If using GCS, enter the path to the credentials key file""
        //         }},
        //         ""GoogleCloudStorageBucket"": {{
        //             ""type"": ""string"",
        //             ""title"": ""Google Cloud StorageBucket"",
        //             ""description"": ""If using GCS, enter the name of the bucket being used.""
        //         }},
        //         ""GoogleCloudStorageDownloadPath"": {{
        //             ""type"": ""string"",
        //             ""title"": ""Google Cloud Storage Download Path"",
        //             ""description"": ""If using GCS, enter the path to temporarily store uploaded files. Preferably an unused folder.""
        //         }}
        //     }},
        //     ""required"": [""FileStorageMethod""]
        // }}";
        //
        //     return schemaJsonObj;
        }
    }
}