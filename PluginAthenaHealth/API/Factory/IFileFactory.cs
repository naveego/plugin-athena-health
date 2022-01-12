using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Factory
{
    public interface IFileFactory
    {
        IFile CreateFile(string fileName, string filePath, string GCSBucket, string GCSTempStoragePath);
        void DeleteTemporaryFile(string fileName);
    }
}