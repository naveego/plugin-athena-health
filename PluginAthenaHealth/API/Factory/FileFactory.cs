using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using PluginAthenaHealth.API.Utility;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Factory
{
    public class FileFactory : IFileFactory
    {
        private ConfigureWriteFormData ConfigureWriteFormData { get; set; }


        public ConfigureWriteFormData GetConfigureWriteFormData()
        {
            return ConfigureWriteFormData;
        }

        public FileFactory(ConfigureWriteFormData configureWriteFormData)
        {
            ConfigureWriteFormData = configureWriteFormData;
        }
        
        public IFile CreateFile(string fileName, string filePath = "", string GCSBucket = "")
        {
            if (ConfigureWriteFormData.StorageType == Constants.Local)
            {
                var fullFilePath = $"{filePath}\\{fileName}"; 
        
                if (!File.Exists(fullFilePath))
                {
                    throw new Exception($"Failed to discover file expected at {fullFilePath}");
                }
                        
                return new FileByteArray(File.ReadAllBytes(fullFilePath));
            }
            else //configureWrite == gcs
            {
        
                var storage = StorageClient.Create(GoogleCredential.FromFile(ConfigureWriteFormData.GoogleCloudStorageCredentialPath));
                var outputFile = File.OpenWrite(ConfigureWriteFormData.GoogleCloudStorageDownloadPath);
                storage.DownloadObjectAsync(GCSBucket,
                    fileName,
                    outputFile);
                        
                var byteArray = File.ReadAllBytes($"{ConfigureWriteFormData.GoogleCloudStorageDownloadPath.TrimEnd('\\')}\\{fileName}");
                
                return new FileByteArray(byteArray);
            }
        }

        public void DeleteTemporaryFile(string fileName)
        {
            var filePath = "";
            
            if (ConfigureWriteFormData.StorageType == Constants.GoogleCloudStorage)
            {
                filePath = $"{ConfigureWriteFormData.GoogleCloudStorageDownloadPath.TrimEnd('\\')}\\{fileName}";
                
                if (!File.Exists(filePath))
                {
                    throw new Exception($"Failed to discover file expected at {filePath}");
                }
            }
            
            File.Delete(filePath);
        }
    }
}