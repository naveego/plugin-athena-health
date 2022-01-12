namespace PluginAthenaHealth.DataContracts
{
    public class ConfigureWriteFormData
    {
        public string StorageType { get; set; }
        
        public string LocalPath { get; set; }
        public string GoogleCloudStorageCredentialPath { get; set; }
        public string GoogleCloudStorageDownloadPath { get; set; }
        public string GoogleCloudStorageBucket { get; set; }
    }
}