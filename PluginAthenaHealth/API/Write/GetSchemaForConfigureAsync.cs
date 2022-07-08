using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility.EndpointHelperEndpoints;
using PluginAthenaHealth.DataContracts;
using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static async Task<Schema> GetSchemaForConfigureAsync(ConfigureWriteFormData configureFormData)
        {
            var endpoint = PatientChartsEndpointHelper.PatientChartsEndpoints.ToList().First(x => x.Value.Id == "PatientCharts");

           var schema = await endpoint.Value.GetStaticSchemaAsync(null, null); //client unneeded

           configureFormData.Id = endpoint.Value.Id;
           schema.PublisherMetaJson = JsonConvert.SerializeObject(configureFormData);

           return schema;
        } 
    }
}