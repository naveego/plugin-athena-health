using System.Threading.Tasks;

namespace PluginAthenaHealth.API.Factory
{
    public interface IApiAuthenticator
    {
        Task<string> GetToken();
    }
}