using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public class PatientsEndpointHelper
    {
        private class PatientsEndpoint : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

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
                
                if (!providerResult.IsSuccessStatusCode)
                {
                    throw new Exception(providerResult.Content.ReadAsStringAsync().ToString());
                }
                
                var providerResponse =
                    JsonConvert.DeserializeObject<ProviderResponse>(
                        await providerResult.Content.ReadAsStringAsync());

                foreach (var provider in providerResponse.Providers)
                {
                    foreach (var department in departmentResponse.Departments)
                    {
                        var patientsPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId.TrimEnd('/')}{BasePath}?departmentid={department.DepartmentId}&primaryproviderid={provider.ProviderId}";

                        var hasMore = false;
                        do
                        {
                            
                            var patientResult = await apiClient.GetAsync(patientsPath);
                            
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
                                        throw new Exception("Patient Err: The given search parameters would produce a total data set larger than 1000 records.  Please refine your search and try again.");
                                    }
                                }
                                catch
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                                
                            }
                        
                            var patientResponse =
                                JsonConvert.DeserializeObject<PatientResponseWrapper>(await patientResult.Content.ReadAsStringAsync());

                            foreach (var patient in patientResponse.Patients)
                            {
                                
                                var recordMap = new Dictionary<string, object>{};
                                //key
                                recordMap["patientid"] = patient.Patientid ?? "";
                                
                                //strings
                                recordMap["racename"] = patient.Racename ?? "";
                                recordMap["email"] = patient.Email ?? "";
                                recordMap["homephone"] = patient.Homephone ?? "";
                                recordMap["guarantorstate"] = patient.Guarantorstate ?? "";
                                recordMap["preferredpronouns"] = patient.Preferredpronouns ?? "";
                                recordMap["ethnicitycode"] = patient.Ethnicitycode ?? "";
                                recordMap["contactpreference"] = patient.Contactpreference ?? "";
                                recordMap["guarantordob"] = patient.Guarantordob ?? "";
                                recordMap["zip"] = patient.Zip ?? "";
                                recordMap["status"] = patient.Status ?? "";
                                recordMap["lastname"] = patient.Lastname ?? "";
                                recordMap["guarantorfirstname"] = patient.Guarantorfirstname ?? "";
                                recordMap["city"] = patient.City ?? "";
                                recordMap["lastappointment"] = patient.Lastappointment ?? "";
                                recordMap["genderidentity"] = patient.Genderidentity ?? "";
                                recordMap["guarantoremail"] = patient.Guarantoremail ?? "";
                                recordMap["guarantorcity"] = patient.Guarantorcity ?? "";
                                recordMap["guarantorzip"] = patient.Guarantorzip ?? "";
                                recordMap["sex"] = patient.Sex ?? "";
                                recordMap["primarydepartmentid"] = patient.Primarydepartmentid ?? "";
                                recordMap["firstappointment"] = patient.Firstappointment ?? "";
                                recordMap["language6392code"] = patient.Language6392Code ?? "";
                                recordMap["primaryproviderid"] = patient.Primaryproviderid ?? "";
                                recordMap["patientphotourl"] = patient.Patientphotourl ?? "";
                                recordMap["mobilephone"] = patient.Mobilephone ?? "";
                                recordMap["registrationdate"] = patient.Registrationdate ?? "";
                                recordMap["caresummarydeliverypreference"] = patient.Caresummarydeliverypreference ?? "";
                                recordMap["guarantorlastname"] = patient.Guarantorlastname ?? "";
                                recordMap["firstname"] = patient.Firstname ?? "";
                                recordMap["guarantorcountrycode"] = patient.Guarantorcountrycode ?? "";
                                recordMap["racecode"] = patient.Racecode ?? "";
                                recordMap["state"] = patient.State ?? "";
                                recordMap["dob"] = patient.Dob ?? "";
                                recordMap["guarantorrelationshiptopatient"] = patient.Guarantorrelationshiptopatient ?? "";
                                recordMap["address1"] = patient.Address1 ?? "";
                                recordMap["guarantorphone"] = patient.Guarantorphone ?? "";
                                recordMap["driverslicenseurl"] = patient.Driverslicenseurl ?? "";
                                recordMap["maritalstatus"] = patient.Maritalstatus ?? "";
                                recordMap["countrycode"] = patient.Countrycode ?? "";
                                recordMap["guarantoraddress1"] = patient.Guarantoraddress1 ?? "";
                                recordMap["maritalstatusname"] = patient.Maritalstatusname ?? "";
                                recordMap["countrycode3166"] = patient.Countrycode3166 ?? "";
                                recordMap["guarantorcountrycode3166"] = patient.Guarantorcountrycode3166 ?? "";
                                recordMap["race"] = string.Join(',', patient.Race) ?? "";
                                
                                //bools
                                recordMap["contactpreference_lab_phone"] =
                                    patient.ContactpreferenceLabPhone;
                                recordMap["consenttotext"] = patient.Consenttotext;
                                recordMap["contactpreference_billing_sms"] =
                                    patient.ContactpreferenceBillingSms;
                                recordMap["contactpreference_appointment_phone"] =
                                    patient.ContactpreferenceAppointmentPhone;
                                recordMap["hasmobile"] = patient.Hasmobile;
                                recordMap["contactpreference_announcement_email"] =
                                    patient.ContactpreferenceAnnouncementEmail;
                                recordMap["patientphoto"] = patient.Patientphoto;
                                recordMap["consenttocall"] = patient.Consenttocall;
                                recordMap["contactpreference_billing_email"] =
                                    patient.ContactpreferenceBillingEmail;
                                recordMap["donotcall"] = patient.Donotcall;
                                recordMap["portalaccessgiven"] = patient.Portalaccessgiven;
                                recordMap["driverslicense"] = patient.Driverslicense;
                                recordMap["contactpreference_appointment_email"] =
                                    patient.ContactpreferenceAppointmentEmail;
                                recordMap["homebound"] = patient.Homebound;
                                recordMap["contactpreference_appointment_sms"] =
                                    patient.ContactpreferenceAppointmentSms;
                                recordMap["contactpreference_billing_phone"] =
                                    patient.ContactpreferenceBillingPhone;
                                recordMap["contactpreference_announcement_phone"] =
                                    patient.ContactpreferenceAnnouncementPhone;
                                recordMap["contactpreference_lab_sms"] =
                                    patient.ContactpreferenceLabSms;
                                recordMap["guarantoraddresssameaspatient"] =
                                    patient.Guarantoraddresssameaspatient;
                                recordMap["portaltermsonfile"] = patient.Portaltermsonfile;
                                recordMap["privacyinformationverified"] =
                                    patient.Privacyinformationverified;
                                recordMap["contactpreference_lab_email"] = patient.ContactpreferenceLabEmail;
                                recordMap["contactpreference_announcement_sms"] =
                                    patient.ContactpreferenceAnnouncementSms;
                                recordMap["emailexists"] = patient.Emailexists;
                                
                                yield return new Record
                                {
                                    Action = Record.Types.Action.Upsert,
                                    DataJson = JsonConvert.SerializeObject(recordMap)
                                };
                            }
                        } while (hasMore);
                    }
                }
            }

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    //keys
                    "patientid",
                    
                    //strings
                    "racename",
                    "email",
                    "homephone",
                    "guarantorstate",
                    "preferredpronouns",
                    "ethnicitycode",
                    "contactpreference",
                    "guarantordob",
                    "zip",
                    "status",
                    "lastname",
                    "guarantorfirstname",
                    "city",
                    "lastappointment",
                    "genderidentity",
                    "guarantoremail",
                    "guarantorcity",
                    "guarantorzip",
                    "sex",
                    "primarydepartmentid",
                    "firstappointment",
                    "language6392code",
                    "primaryproviderid",
                    "patientphotourl",
                    "mobilephone",
                    "registrationdate",
                    "caresummarydeliverypreference",
                    "guarantorlastname",
                    "firstname",
                    "guarantorcountrycode",
                    "racecode",
                    "state",
                    "dob",
                    "guarantorrelationshiptopatient",
                    "address1",
                    "guarantorphone",
                    "driverslicenseurl",
                    "maritalstatus",
                    "countrycode",
                    "guarantoraddress1",
                    "maritalstatusname",
                    "countrycode3166",
                    "guarantorcountrycode3166",
                    "race",
                    
                    //bools
                    "contactpreference_lab_phone",
                    "consenttotext",
                    "contactpreference_billing_sms",
                    "contactpreference_appointment_phone",
                    "hasmobile",
                    "contactpreference_announcement_email",
                    "patientphoto",
                    "consenttocall",
                    "contactpreference_billing_email",
                    "donotcall",
                    "portalaccessgiven",
                    "driverslicense",
                    "contactpreference_appointment_email",
                    "homebound",
                    "contactpreference_appointment_sms",
                    "contactpreference_billing_phone",
                    "contactpreference_announcement_phone",
                    "contactpreference_lab_sms",
                    "guarantoraddresssameaspatient",
                    "portaltermsonfile",
                    "privacyinformationverified",
                    "contactpreference_lab_email",
                    "contactpreference_announcement_sms",
                    "emailexists",
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
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        
                        //bools
                        case ("contactpreference_lab_phone"):
                        case ("consenttotext"):
                        case ("contactpreference_billing_sms"):
                        case ("contactpreference_appointment_phone"):
                        case ("hasmobile"):
                        case ("contactpreference_announcement_email"):
                        case ("patientphoto"):
                        case ("consenttocall"):
                        case ("contactpreference_billing_email"):
                        case ("donotcall"):
                        case ("portalaccessgiven"):
                        case ("driverslicense"):
                        case ("contactpreference_appointment_email"):
                        case ("homebound"):
                        case ("contactpreference_appointment_sms"):
                        case ("contactpreference_billing_phone"):
                        case ("contactpreference_announcement_phone"):
                        case ("contactpreference_lab_sms"):
                        case ("guarantoraddresssameaspatient"):
                        case ("portaltermsonfile"):
                        case ("privacyinformationverified"):
                        case ("contactpreference_lab_email"):
                        case ("contactpreference_announcement_sms"):
                        case ("emailexists"):
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
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
        }

        public static readonly Dictionary<string, Endpoint> PatientsEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "AllPatients", new PatientsEndpoint
                {
                    Id = "AllPatients",
                    Name = "All Patients",
                    BasePath = "/patients",
                    AllPath = "/patients",
                    PropertiesPath = "/patients",
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