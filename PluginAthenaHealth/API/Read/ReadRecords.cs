using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility;

namespace PluginAthenaHealth.API.Read
{
    public static partial class Read
    {
        public static async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema)
        {
            var endpoint = EndpointHelper.GetEndpointForSchema(schema);

            var records = endpoint?.ReadRecordsAsync(apiClient, schema);

            if (records != null)
            {
                await foreach (var record in records)
                {
                    yield return record;
                }
            }
        }
    }
}