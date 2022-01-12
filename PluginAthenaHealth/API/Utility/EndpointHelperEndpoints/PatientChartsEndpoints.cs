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
                List<string> staticSchemaProperties = new List<string>()
                {
                    //strings
                    "attachmentcontents",
                    "actionnote",
                    "attachmenttype",
                    "documentsubclass",
                    "entityid",
                    "entitytype",
                    "fileName",
                    "internalnote",
                    "originalfilename",
                    
                    //ints
                    "practiceid",
                    "patientid",
                    "appointmentid",
                    "providerid",
                    
                    //bools
                    "autoclose",
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
                        case("practiceid"):
                        case("patientid"):
                            property.IsKey = true;
                            property.TypeAtSource = "integer";
                            property.Type = PropertyType.Integer;
                            break;
                        //string keys
                        case("departmentid"):
                        case("documentsubclass"):
                            property.IsKey = true;
                            property.TypeAtSource = "string";
                            property.Type = PropertyType.String;
                            break;

                        //strings
                        case("entityid"):
                        case("fileName"):
                        case("attachmentcontents"):
                        case("actionnote"):
                        case("attachmenttype"):
                        case("entitytype"):
                        case("internalnote"):
                        case("originalfilename"):
                            property.IsKey = false;
                            property.TypeAtSource = "string";
                            property.Type = PropertyType.String;
                            break;
                        
                        //ints
                        case("appointmentid"):
                        case("providerid"):
                            property.IsKey = false;
                            property.TypeAtSource = "integer";
                            property.Type = PropertyType.Integer;
                            break;
                            
                        //bools
                        case("autoclose"):
                            property.IsKey = false;
                            property.TypeAtSource = "boolean";
                            property.Type = PropertyType.Bool;
                            break;
                        
                        default:
                            property.IsKey = false;
                            property.TypeAtSource = "string";
                            property.Type = PropertyType.String;
                            break;
                    }
                    properties.Add(property);
                }

                schema.Properties.Clear();
                schema.Properties.AddRange(properties);

                schema.DataFlowDirection = GetDataFlowDirection();
                
                return schema;
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

                    var postObject = new Dictionary<string, object>();

                    foreach (var property in schema.Properties)
                    {
                        object value = "";
                        
                        if (recordMap.ContainsKey(property.Id))
                        {
                            value = recordMap[property.Id];
                        }

                        postObject.TryAdd(property.Id, value);
                    }
                    var patientId = recordMap["patientid"] ?? "";
                    var fileName = recordMap["fileName"] ?? "";
                    
                    var appointmentId = recordMap["appointmentid"] ?? "";
                    
                    //could be needed in future 
                    // var documentSubclass = recordMap["documentsubclass"] ?? ""; 
                    // var departmentId = recordMap["departmentid"] ?? "";
                    // var fileName = $"{patientId}_{appointmentId}_{departmentId}.pdf";

                    if (string.IsNullOrWhiteSpace(patientId.ToString()) ||
                        string.IsNullOrWhiteSpace(fileName.ToString()))
                    {
                        throw new Exception($"Missing required patientId or fileName to upload patient chart for appointment {appointmentId.ToString()}");
                    }
                    
                    var postPath = $"{BasePath.TrimEnd('/')}/{settings.PracticeId}/{patientId}/clinicaldocument?practiceid={settings.PracticeId}&Content-Type=application/pdf";

                    
                    var configureWriteSettings = JsonConvert.DeserializeObject<ConfigureWriteFormData>(schema.PublisherMetaJson);

                    var fileFactory = new FileFactory(configureWriteSettings);

                    var file = fileFactory.CreateFile(fileName.ToString());

                    var fileBase64 = file.GetBase64String();
                    
                    postObject.TryAdd("attachmentcontents", fileBase64);
                    postObject.TryAdd("Content-Type", $"application/pdf");
                    
                    postObject["originalfilename"] = new string(fileName.ToString().Take(200).ToArray()); //maximum length of 200 permitted by API

                    //add to json
                    var postObjectWrapper = new UpsertObjectWrapper
                    {
                        Properties = postObject
                    };

                    var json = new StringContent(
                        JsonConvert.SerializeObject(postObjectWrapper),
                        Encoding.UTF8,
                        "application/json"
                    );
                    
                    HttpResponseMessage response = await apiClient.PostAsync(postPath, json);
                    
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