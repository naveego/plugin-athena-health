using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Factory
{
    public interface IFileFactory
    {
        IFile CreateFile(string fileName);
        void DeleteTemporaryFile(string fileName);
    }
}