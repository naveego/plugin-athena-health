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
    public class PatientBalancesEndpointHelper
    {
        private class PatientBalancesEndpoint : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    //keys
                    "patientid",
                    "balanceid",
                    
                    //ints
                    "providergroupid",
                    "balance",
                    
                    //strings
                    "departmentlist",
                    
                    //bools
                    "cleanbalance"
                };
                
                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("patientid"):
                        case ("balanceid"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        
                        //bools
                        case ("cleanbalance"):
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
                            break;
                        
                        //ints
                        case ("providergroupid"):
                        case ("balance"):
                            property.IsKey = false;
                            property.Type = PropertyType.Integer;
                            property.TypeAtSource = "integer";
                            break;
                        
                        //strings
                        default:
                            property.IsKey = false;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                    }
                    properties.Add(property);
                }
                schema.Properties.Clear();
                schema.Properties.AddRange(properties);

                schema.DataFlowDirection = GetDataFlowDirection();
                
                return schema;
            }
            
            
            public override async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema, bool isDiscoverRead = false)
            {
                var settings = apiClient.GetSettings();
                
                var departmentPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/departments";
                var departmentResult = await apiClient.GetAsync(departmentPath);
                
                if (departmentResult.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception(departmentResult.ReasonPhrase);
                }
                
                if (!departmentResult.IsSuccessStatusCode)
                {
                    throw new Exception(departmentResult.Content.ReadAsStringAsync().ToString());
                }
                
                var departmentResponse =
                    JsonConvert.DeserializeObject<DepartmentResponse>(
                        await departmentResult.Content.ReadAsStringAsync());

                var providerPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/providers";
                var providerResult = await apiClient.GetAsync(providerPath);
                
                if (providerResult.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception(providerResult.ReasonPhrase);
                }
                
                var providerResponse =
                    JsonConvert.DeserializeObject<ProviderResponse>(
                        await providerResult.Content.ReadAsStringAsync());

                foreach (var provider in providerResponse.Providers)
                {
                    foreach (var department in departmentResponse.Departments)
                    {
                        var recordMap = new Dictionary<string, object>();

                        var patientPath =
                            $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/patients?departmentid={department.DepartmentId.ToString()}&primaryproviderid={provider.ProviderId.ToString()}";
                        var hasMore = false;
                        do
                        {
                            var patientResult = await apiClient.GetAsync(patientPath);
                            if (patientResult.StatusCode == HttpStatusCode.TooManyRequests)
                            {
                                throw new Exception(patientResult.ReasonPhrase);
                            }
                            
                            if (!patientResult.IsSuccessStatusCode)
                            {
                                try
                                {
                                    var errorResponse =
                                        JsonConvert.DeserializeObject<Error>(
                                            await patientResult.Content.ReadAsStringAsync());
                                    
                                    if (errorResponse.ErrorMessage ==
                                        "The given search parameters would produce a total data set larger than 1000 records.  Please refine your search and try again.")
                                    {
                                        Logger.Info("Missing patient records due to 1000 data set limit per query.");
                                        continue;
                                    }

                                    if (errorResponse.ErrorMessage == "The requested ID does not exist.")
                                    {
                                        continue;
                                    }
                                    
                                    throw new Exception(string.IsNullOrWhiteSpace(errorResponse.ErrorMessage)
                                        ? patientResult.Content.ReadAsStringAsync().ToString()
                                        : errorResponse.ErrorMessage);
                                }
                                catch
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                            }
                            
                            var patientResponse =
                                JsonConvert.DeserializeObject<PatientResponseWrapper>(
                                    await patientResult.Content.ReadAsStringAsync());

                            if (patientResponse.Patients.Count > 0)
                            {
                                foreach (var patient in patientResponse.Patients)
                                {
                                    int balance_no = 0;
                                    foreach (var balance in patient.Balances)
                                    {
                                        recordMap["patientid"] = patient.Patientid.ToString();
                                        recordMap["balanceid"] = balance_no.ToString();
                                        recordMap["cleanbalance"] = balance.Cleanbalance;
                                        recordMap["departmentlist"] = balance.Departmentlist.ToString() ?? "";
                                        recordMap["providergroupid"] = balance.Providergroupid;
                                        recordMap["balance"] = balance.BalanceBalance.ToString() ?? "0.0";

                                        balance_no++;

                                        yield return new Record
                                        {
                                            Action = Record.Types.Action.Upsert,
                                            DataJson = JsonConvert.SerializeObject(recordMap)
                                        };
                                    }
                                }
                            }

                            if (string.IsNullOrWhiteSpace(patientResponse.Next))
                            {
                                hasMore = false;
                            }
                            else
                            {
                                hasMore = true;

                                var next = patientResponse.Next.Substring(4); //trim version
                                patientPath = $"{settings.GetBaseUrl().TrimEnd('\\')}{next}";
                            }
                            
                        } while (hasMore);
                    }
                }
            }
        }

        public static readonly Dictionary<string, Endpoint> PatientBalancesEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "PatientBalances", new PatientBalancesEndpoint
                {
                    Id = "PatientBalances",
                    Name = "PatientBalances",
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