﻿using System.Collections.Generic;
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
                    {"AutoClose", new Dictionary<string, object>
                    {
                        {"type", "boolean"},
                        {"title", "Auto Close Uploads"},
                        {"description", "Automatically set the uploaded documents to closed"},
                    }},
                    {"FileStorageMethod", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "File Storage Method"},
                        {"description", "The storage method of the patient charts"},
                        {"enum", new []{Constants.Local, Constants.GoogleCloudStorage}}
                    }},
                    {"GoogleCloudStorageCredentialPath", new Dictionary<string, object>
                    {
                        {"type", "string"},
                        {"title", "Google Cloud Storage Credential Path"},
                        {"description", "If using GCS, enter the path to the credentials key file"}
                    }}
                }},
                {"required", new []
                {
                    "FileStorageMethod"
                }}
            };

            return JsonConvert.SerializeObject(schemaJsonObj);
           
        }
    }
}