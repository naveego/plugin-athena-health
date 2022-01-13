using System.Linq;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility.EndpointHelperEndpoints;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static async Task<Schema> GetSchemaForConfigureAsync()
        {
            var endpoint = PatientChartsEndpointHelper.PatientChartsEndpoints.ToList().First(x => x.Value.Id == "PatientCharts");

            var schema = await endpoint.Value.GetStaticSchemaAsync(null, null); //unused params

            return schema;
        } 
    }
}