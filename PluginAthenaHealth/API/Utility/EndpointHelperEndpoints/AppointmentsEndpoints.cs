using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Naveego.Sdk.Logging;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Utility.EndpointHelperEndpoints
{
    public class AppointmentsEndpointHelper
    {
        private class AppointmentsEndpoint : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                return base.GetStaticSchemaAsync(apiClient, schema);
            }
        }

        public static readonly Dictionary<string, Endpoint> AppointmentsEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "AllAppointments", new AppointmentsEndpoint
                {
                    Id = "AllAppointments",
                    Name = "All Appointments",
                    BasePath = "/appointments/report",
                    AllPath = "/appointments/report",
                    PropertiesPath = "/appointments/report",
                    SupportedActions = new List<EndpointActions>
                    {
                        EndpointActions.Get
                    },
                    PropertyKeys = new List<string>
                    {
                        "hs_unique_creation_key"
                    }
                }
            }
        };
    }
}