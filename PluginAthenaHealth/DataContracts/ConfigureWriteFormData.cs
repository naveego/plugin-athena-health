using System;
using System.IO;
using System.Net;
using PluginAthenaHealth.API.Utility;

namespace PluginAthenaHealth.DataContracts
{
    public class ConfigureWriteFormData
    {
        public string StorageType { get; set; }
        
        public string GoogleCloudStorageCredentialPath { get; set; }
        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (String.IsNullOrEmpty(StorageType))
            {
                throw new Exception("the ClientId property must be set");
            }

            if (StorageType == Constants.GoogleCloudStorage)
            {
                if (String.IsNullOrEmpty(GoogleCloudStorageCredentialPath))
                {
                    throw new Exception("the Google Cloud Storage Credentials property must be set");
                }
                
                if (!File.Exists(GoogleCloudStorageCredentialPath))
                {
                    throw new Exception("No file found at given path to credentials");
                }
            }
        }
    }
}