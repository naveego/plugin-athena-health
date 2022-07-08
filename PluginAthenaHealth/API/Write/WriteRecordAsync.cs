using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Naveego.Sdk.Logging;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.API.Utility;
using PluginAthenaHealth.API.Utility.EndpointHelperEndpoints;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Write
{
    public static partial class Write
    {
        private static readonly SemaphoreSlim WriteSemaphoreSlim = new SemaphoreSlim(1, 1);

        public static async Task<string> WriteRecordAsync(IApiClient apiClient, Schema schema, Record record,
            IServerStreamWriter<RecordAck> responseStream)
        {
            // debug
            Logger.Debug($"Starting timer for {record.RecordId}");
            var timer = Stopwatch.StartNew();

            try
            {
                // var endpoint = EndpointHelper.GetEndpointForSchema(schema);
                var endpoint = PatientChartsEndpointHelper.PatientChartsEndpoints.ToList()
                    .First(x => x.Value.Id == "PatientCharts").Value;

                if (endpoint == null)
                {
                    throw new Exception($"Endpoint {schema.Id} does not exist");
                }

                // debug
                Logger.Debug(JsonConvert.SerializeObject(record, Formatting.Indented));

                // semaphore
                await WriteSemaphoreSlim.WaitAsync();

                // write records
                var errorMessage = await endpoint.WriteRecordAsync(apiClient, schema, record, responseStream);
                
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    Logger.Error(new Exception(errorMessage), errorMessage);
                }
                
                timer.Stop();
                Logger.Debug($"Acknowledged Record {record.RecordId} time: {timer.ElapsedMilliseconds}");

                return "";
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Error writing record {e.Message}");
                // send ack
                var ack = new RecordAck
                {
                    CorrelationId = record.CorrelationId,
                    Error = e.Message
                };
                await responseStream.WriteAsync(ack);

                timer.Stop();
                Logger.Debug($"Failed Record {record.RecordId} time: {timer.ElapsedMilliseconds}");

                return e.Message;
            }
            finally
            {
                WriteSemaphoreSlim.Release();
            }
        }
    }
}