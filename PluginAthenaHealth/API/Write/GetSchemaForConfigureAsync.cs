﻿using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility.EndpointHelperEndpoints;
using PluginAthenaHealth.Helper;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        public static async Task<Schema> GetSchemaForConfigureAsync()
        {
            var endpoint = PatientChartsEndpointHelper.PatientChartsEndpoints.ToList().First(x => x.Value.Id == "PatientCharts");

            return await endpoint.Value.GetStaticSchemaAsync(null, null); //client unneeded
        } 
    }
}