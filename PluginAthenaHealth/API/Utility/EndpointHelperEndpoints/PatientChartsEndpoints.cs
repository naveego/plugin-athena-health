using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Grpc.Core;
using Naveego.Sdk.Logging;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PluginAthenaHealth.API.Factory;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.API.Utility.EndpointHelperEndpoints
{
    public class PatientChartsEndpointHelper
    {
        private class PatientChartsEndpoint : Endpoint
        {
            private static readonly SemaphoreSlim WriteSemaphoreSlim = new SemaphoreSlim(1, 1);
            public override bool ShouldGetStaticSchema { get; set; } = true;


            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                var outSchema = new Schema
                {
                    Id = "Patient Chart Upload",
                    Name = "Patient Chart Upload",
                    Description = "",
                    DataFlowDirection = GetDataFlowDirection(),
                };
                
                List<string> staticSchemaProperties = new List<string>()
                {
                    //strings
                    "fileName",
                    "gcsBucket",
                    "localFilePath",
                    "documentSubclass",
                    "departmentid",
                    "patientid",
                    "appointmentid"
                };

                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        //keys
                        //int keys
                        case("patientid"):
                            property.IsKey = true;
                            property.TypeAtSource = "integer";
                            property.Type = PropertyType.Integer;
                            break;

                        //strings
                        case("fileName"):
                        case("gcsBucket"):
                        case("documentSubclass"):
                        case("localFilePath"):
                        case("appointmentid"):
                        case("departmentid"):
                            property.IsKey = false;
                            property.TypeAtSource = "string";
                            property.Type = PropertyType.String;
                            break;
                        default:
                            property.IsKey = false;
                            property.TypeAtSource = "string";
                            property.Type = PropertyType.String;
                            break;
                    }
                    properties.Add(property);
                }

                outSchema.Properties.Clear();
                outSchema.Properties.AddRange(properties);

                return outSchema;
            }

            public async override Task<string> WriteRecordAsync(IApiClient apiClient, Schema schema, Record record,
                IServerStreamWriter<RecordAck> responseStream)
            {
                // debug
                Logger.Debug($"Starting timer for {record.RecordId}");
                
                try
                {
                    // debug
                    Logger.Debug(JsonConvert.SerializeObject(record, Formatting.Indented));

                    // semaphore
                    await WriteSemaphoreSlim.WaitAsync();
                    
                    // get settings
                    var settings = apiClient.GetSettings();

                    // get record map
                    var recordMap = JsonConvert.DeserializeObject<Dictionary<string, object>>(record.DataJson);

                    // write records

                    var postObject = new Dictionary<string, string>();

                    if (!recordMap.TryGetValue("patientid", out var patientId))
                    {
                        patientId = "";
                    }
                    if (!recordMap.TryGetValue("departmentid", out var departmentId))
                    {
                        departmentId = "";
                    }
                    if (!recordMap.TryGetValue("localFilePath", out var localFilePath))
                    {
                        localFilePath = "";
                    }
                    if (!recordMap.TryGetValue("gcsBucket", out var GCSBucket))
                    {
                        GCSBucket = "";
                    }
                    if (!recordMap.TryGetValue("fileName", out var fileName))
                    {
                        fileName = "";
                    }
                    if (!recordMap.TryGetValue("documentSubclass", out var documentSubclass))
                    {
                        documentSubclass = "";
                    }

                    if (string.IsNullOrWhiteSpace(patientId.ToString()))
                    {
                        throw new Exception($"Missing required patientId to upload patient chart");
                    }
                    if (string.IsNullOrWhiteSpace(documentSubclass.ToString()))
                    {
                        throw new Exception($"Missing required documentSubclass to upload patient chart");
                    }
                    if (string.IsNullOrWhiteSpace(fileName.ToString()))
                    {
                        throw new Exception($"Missing required fileName to upload patient chart");
                    }
                    if (string.IsNullOrWhiteSpace(departmentId.ToString()))
                    {
                        throw new Exception($"Missing required departmentId to upload patient chart");
                    }
                    if (string.IsNullOrWhiteSpace(localFilePath.ToString()) && string.IsNullOrWhiteSpace(GCSBucket.ToString()))
                    {
                        throw new Exception($"Missing required localFilePath or gcsBucket to upload patient chart");
                    }
                    
                    var postPath = $"{settings.PracticeId}/patients/{patientId}/documents/clinicaldocument?practiceid={settings.PracticeId}&Content-Type=application/pdf";
                    var configureWriteSettings = JsonConvert.DeserializeObject<ConfigureWriteFormData>(schema.PublisherMetaJson);
                    
                    var fileFactory = new FileFactory(configureWriteSettings);
                    var file = fileFactory.CreateFile(fileName.ToString(), localFilePath.ToString(), GCSBucket.ToString());
                    var fileBase64 = file.GetBase64String();
                    
                    postObject.TryAdd("attachmentcontents", fileBase64);
                    postObject.TryAdd("documentsubclass", documentSubclass.ToString());
                    postObject.TryAdd("departmentid", departmentId.ToString());
                    postObject.TryAdd("Content-Type", $"application/pdf");
                    postObject.TryAdd("originalfilename", new string(fileName.ToString().Take(200).ToArray())); //maximum length of 200 permitted by API

                    HttpResponseMessage response = await apiClient.PostAsync(postPath, postObject);
                    
                    fileFactory.DeleteTemporaryFile(fileName.ToString());
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        var errorAck = new RecordAck
                        {
                            CorrelationId = record.CorrelationId,
                            Error = errorMessage
                        };
                        await responseStream.WriteAsync(errorAck);
                        
                        return errorMessage;
                    }
                    
                    var ack = new RecordAck
                    {
                        CorrelationId = record.CorrelationId,
                        Error = ""
                    };
                    
                    await responseStream.WriteAsync(ack);

                    return ack.Error;
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        public static readonly Dictionary<string, Endpoint> PatientChartsEndpoints =
                new Dictionary<string, Endpoint>
                {
                    {
                        "PatientCharts", new PatientChartsEndpoint
                        {
                            Id = "PatientCharts",
                            Name = "PatientCharts",
                            BasePath = "/patients",
                            AllPath = "/patients",
                            PropertiesPath = "/patients",
                            SupportedActions = new List<EndpointActions>
                            {
                                EndpointActions.Put,
                                EndpointActions.Post
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