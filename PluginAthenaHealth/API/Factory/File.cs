using System;

namespace PluginAthenaHealth.API.Factory
{
    public class FileByteArray : IFile
    {
        private Byte[] FileBytes { get; set; }
        
        public FileByteArray(Byte[] fileBytes)
        {
            FileBytes = fileBytes;
        }
        
        public string GetBase64String()
        {
            return Convert.ToBase64String(FileBytes);
        }
    }
}