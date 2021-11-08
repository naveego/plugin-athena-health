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
        }

        public static readonly Dictionary<string, Endpoint> AppointmentsEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "AllAppointments", new AppointmentsEndpoint
                {
                    Id = "AllAppointments",
                    Name = "All Appointments",
                    BasePath = "/crm/v3/",
                    AllPath = "/objects/Appointments",
                    PropertiesPath = "/crm/v3/properties/Appointments",
                    DetailPath = "/objects/Appointments/{0}",
                    DetailPropertyId = "hs_unique_creation_key",
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