using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Factory
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(Settings settings);
    }
}