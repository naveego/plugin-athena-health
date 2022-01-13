using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Grpc.Core;
using PluginAthenaHealth.API.Utility;
using PluginAthenaHealth.Helper;
using PluginAthenaHealth.Plugin;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Factory
{
    public class FileFactory : IFileFactory
    {
        private ConfigureWriteFormData ConfigureWriteFormData { get; set; }

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
                var outputFile = File.OpenWrite(ServerStatus.Config.TemporaryDirectory);
                storage.DownloadObjectAsync(GCSBucket,
                    fileName,
                    outputFile);
                        
                var byteArray = File.ReadAllBytes($"{ServerStatus.Config.TemporaryDirectory.TrimEnd('\\')}\\{fileName}");
                
                return new FileByteArray(byteArray);
            }
        }

        public void DeleteTemporaryFile(string fileName)
        {
            var filePath = "";
            
            if (ConfigureWriteFormData.StorageType == Constants.GoogleCloudStorage)
            {
                filePath = $"{ServerStatus.Config.TemporaryDirectory.TrimEnd('\\')}\\{fileName}";
                
                if (!File.Exists(filePath))
                {
                    throw new Exception($"Failed to discover file expected at {filePath}");
                }
            }
            
            File.Delete(filePath);
        }
    }
}