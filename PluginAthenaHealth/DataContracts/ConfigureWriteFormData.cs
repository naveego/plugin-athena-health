﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using PluginAthenaHealth.API.Utility;

namespace PluginAthenaHealth.DataContracts
{
    public class ConfigureWriteFormData
    {
        public string FileStorageMethod { get; set; }
        
        public string GoogleCloudStorageCredentialPath { get; set; }
        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public List<string> Validate()
        {
            var errors = new List<string>();
            
            if (String.IsNullOrEmpty(FileStorageMethod))
            {
                errors.Add("the File Storage Method property must be set");
            }

            if (FileStorageMethod == Constants.GoogleCloudStorage)
            {
                if (String.IsNullOrEmpty(GoogleCloudStorageCredentialPath))
                {
                    errors.Add("the Google Cloud Storage Credentials property must be set");
                }
                
                if (!File.Exists(GoogleCloudStorageCredentialPath))
                {
                    errors.Add("No file found at given path to credentials");
                }
            }

            return errors;
        }
    }
}