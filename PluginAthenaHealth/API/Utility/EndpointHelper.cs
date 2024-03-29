using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Naveego.Sdk.Logging;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility.EndpointHelperEndpoints;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Utility
{
    public static class EndpointHelper
    {
        private static readonly Dictionary<string, Endpoint> Endpoints = new Dictionary<string, Endpoint>();

        static EndpointHelper()
        {
            PatientsEndpointHelper.PatientsEndpoints.ToList().ForEach(x => Endpoints.TryAdd(x.Key, x.Value));
            AppointmentsEndpointHelper.BookedAppointmentsEndpoints.ToList().ForEach(x => Endpoints.TryAdd(x.Key, x.Value));
            PatientChartsEndpointHelper.PatientChartsEndpoints.ToList().ForEach(x => Endpoints.TryAdd(x.Key, x.Value)); //Comment this line for prod releases - noncritical mistake but prevents potential user error
            PatientBalancesEndpointHelper.PatientBalancesEndpoints.ToList().ForEach(x => Endpoints.TryAdd(x.Key, x.Value));
        }

        public static Dictionary<string, Endpoint> GetAllEndpoints()
        {
            return Endpoints;
        }

        public static Endpoint? GetEndpointForId(string id)
        {
            return Endpoints.ContainsKey(id) ? Endpoints[id] : null;
        }

        public static Endpoint? GetEndpointForSchema(Schema schema)
        {
            var endpointMetaJson = JsonConvert.DeserializeObject<dynamic>(schema.PublisherMetaJson);
            string endpointId = endpointMetaJson.Id;
            return GetEndpointForId(endpointId);
        }
    }

    public abstract class Endpoint
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string PropertiesPath { get; set; } = "";
        public string BasePath { get; set; } = "";
        public string AllPath { get; set; } = "";
        public string? DetailPath { get; set; }
        public string? DetailPropertyId { get; set; }
        public List<string> PropertyKeys { get; set; } = new List<string>();

        public virtual bool ShouldGetStaticSchema { get; set; } = false;

        protected virtual string WritePathPropertyId { get; set; } = "";

        protected virtual List<string> RequiredWritePropertyIds { get; set; } = new List<string>
        {
            // "hs_unique_creation_key"
        };

        public List<EndpointActions> SupportedActions { get; set; } = new List<EndpointActions>();

        public async virtual Task<Count> GetCountOfRecords(IApiClient apiClient)
        {
            return await Task.FromResult(new Count
            {
                Kind = Count.Types.Kind.Unavailable
            });
        }

        public virtual async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema, bool isDiscoverRead = false)
        {
            var after = "";
            var hasMore = false;

            do
            {
                var response = await apiClient.GetAsync(
                    $"{BasePath.TrimEnd('/')}/{AllPath.TrimStart('/')}?limit=100&properties={string.Join(",",schema.Properties.Select(p => p.Id))}{(string.IsNullOrWhiteSpace(after) ? "" : $"&after={after}")}");

                response.EnsureSuccessStatusCode();

                var objectResponseWrapper =
                    JsonConvert.DeserializeObject<ObjectResponseWrapper>(await response.Content.ReadAsStringAsync());

                after = objectResponseWrapper?.Paging?.Next?.After ?? "";
                hasMore = !string.IsNullOrWhiteSpace(after);

                if (objectResponseWrapper?.Results.Count == 0)
                {
                    yield break;
                }

                foreach (var objectResponse in objectResponseWrapper?.Results)
                {
                    var recordMap = new Dictionary<string, object>();

                    foreach (var objectProperty in objectResponse.Properties)
                    {
                        try
                        {
                            recordMap[objectProperty.Key] = objectProperty.Value.ToString() ?? "";
                        }
                        catch
                        {
                            recordMap[objectProperty.Key] = "";
                        }
                        
                    }

                    yield return new Record
                    {
                        Action = Record.Types.Action.Upsert,
                        DataJson = JsonConvert.SerializeObject(recordMap)
                    };
                }
            } while (hasMore);
        }

        public virtual async Task<string> WriteRecordAsync(IApiClient apiClient, Schema schema, Record record,
            IServerStreamWriter<RecordAck> responseStream)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> IsCustomProperty(IApiClient apiClient, string propertyId)
        {
            return Task.FromResult(false);
        }

        public Schema.Types.DataFlowDirection GetDataFlowDirection()
        {
            if (CanRead() && CanWrite())
            {
                return Schema.Types.DataFlowDirection.ReadWrite;
            }

            if (CanRead() && !CanWrite())
            {
                return Schema.Types.DataFlowDirection.Read;
            }

            if (!CanRead() && CanWrite())
            {
                return Schema.Types.DataFlowDirection.Write;
            }

            return Schema.Types.DataFlowDirection.Read;
        }


        private bool CanRead()
        {
            return SupportedActions.Contains(EndpointActions.Get);
        }

        private bool CanWrite()
        {
            return SupportedActions.Contains(EndpointActions.Post) ||
                   SupportedActions.Contains(EndpointActions.Put) ||
                   SupportedActions.Contains(EndpointActions.Delete);
        }
    }

    public enum EndpointActions
    {
        Get,
        Post,
        Put,
        Delete
    }
}