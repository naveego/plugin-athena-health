using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Grpc.Core;
using Naveego.Sdk.Logging;
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
            
            if (ConfigureWriteFormData.FileStorageMethod == Constants.Local)
            {
                var fullFilePath = Path.Join(filePath, fileName); 
        
                if (!File.Exists(fullFilePath))
                {
                    throw new Exception($"Failed to discover file expected at {fullFilePath}");
                }
                        
                return new FileByteArray(File.ReadAllBytes(fullFilePath));
            }
            else //configureWrite == gcs
            {
                var storage = StorageClient.Create(GoogleCredential.FromFile(ConfigureWriteFormData.GoogleCloudStorageCredentialPath));

                var tempPath = Path.Join(ServerStatus.Config.TemporaryDirectory, fileName);
                
                Logger.Info($"Using {tempPath} as temporary file");
                using (var outputFile = File.OpenWrite(tempPath))
                {
                    storage.DownloadObject(GCSBucket,
                        fileName,
                        outputFile);
                }
                Logger.Info($"Downloaded to {tempPath} successfully");
                var byteArray = File.ReadAllBytes($"{tempPath}");
                return new FileByteArray(byteArray);
            }
        }

        public void DeleteTemporaryFile(string fileName)
        {
            var filePath = "";
            
            if (ConfigureWriteFormData.FileStorageMethod == Constants.GoogleCloudStorage)
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