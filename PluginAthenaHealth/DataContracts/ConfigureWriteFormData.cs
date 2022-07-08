using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Naveego.Sdk.Logging;
using PluginAthenaHealth.API.Utility;

namespace PluginAthenaHealth.DataContracts
{
    public class ConfigureWriteFormData
    {
        public string FileStorageMethod { get; set; }
        public string GoogleCloudStorageCredentialPath { get; set; }
        public string Id { get; set; }
        
        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            Logger.Info("Starting validate");

            if (String.IsNullOrEmpty(FileStorageMethod))
            {
                throw new Exception("the File Storage Method property must be set");
            }
            
            Logger.Info("After FileStorageMethod");

            if (FileStorageMethod == Constants.GoogleCloudStorage)
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
            
            Logger.Info("Ending validate");
        }
    }
}