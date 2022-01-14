using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        private class BookedAppointmentsEndpoint_Historical : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    
                    //From: BookedAppointments
                    //keys
                    "appointmentid",
                    
                    //bools
                    "coordinatorenterprise",
                    "chargeentrynotrequired",
                    
                    //ints
                    "duration",
                    "hl7providerid",
                    "appointmentcopay.collectedforother",
                    "appointmentcopay.collectedforappointment",
                    "appointmentcopay.insurancecopay",
                    "copay",
                    
                    //strings
                    "date",
                    "starttime",
                    "departmentid",
                    "appointmentstatus",
                    "scheduledby",
                    "patientid",
                    "templateappointmenttypeid",
                    "lastmodifiedby",
                    "appointmenttypeid",
                    "lastmodified",
                    "appointmenttype",
                    "providerid",
                    "scheduleddatetime",
                    "templateappointmentid",
                    "patientappointmenttypename",
                    
                    //From: Patient
                    //strings
                    "patient.racename",
                    "patient.email",
                    "patient.homephone",
                    "patient.guarantorstate",
                    "patient.preferredpronouns",
                    "patient.ethnicitycode",
                    "patient.contactpreference",
                    "patient.guarantordob",
                    "patient.zip",
                    "patient.status",
                    "patient.lastname",
                    "patient.guarantorfirstname",
                    "patient.city",
                    "patient.lastappointment",
                    "patient.genderidentity",
                    "patient.guarantoremail",
                    "patient.guarantorcity",
                    "patient.guarantorzip",
                    "patient.sex",
                    "patient.primarydepartmentid",
                    "patient.firstappointment",
                    "patient.language6392code",
                    "patient.primaryproviderid",
                    "patient.patientphotourl",
                    "patient.mobilephone",
                    "patient.registrationdate",
                    "patient.caresummarydeliverypreference",
                    "patient.guarantorlastname",
                    "patient.firstname",
                    "patient.guarantorcountrycode",
                    "patient.racecode",
                    "patient.state",
                    "patient.dob",
                    "patient.patientid",
                    "patient.guarantorrelationshiptopatient",
                    "patient.address1",
                    "patient.guarantorphone",
                    "patient.driverslicenseurl",
                    "patient.maritalstatus",
                    "patient.countrycode",
                    "patient.guarantoraddress1",
                    "patient.maritalstatusname",
                    "patient.countrycode3166",
                    "patient.guarantorcountrycode3166",
                    "patient.race",
                    
                    //bools
                    "patient.contactpreference_lab_phone",
                    "patient.consenttotext",
                    "patient.contactpreference_billing_sms",
                    "patient.contactpreference_appointment_phone",
                    "patient.hasmobile",
                    "patient.contactpreference_announcement_email",
                    "patient.patientphoto",
                    "patient.consenttocall",
                    "patient.contactpreference_billing_email",
                    "patient.donotcall",
                    "patient.portalaccessgiven",
                    "patient.driverslicense",
                    "patient.contactpreference_appointment_email",
                    "patient.homebound",
                    "patient.contactpreference_appointment_sms",
                    "patient.contactpreference_billing_phone",
                    "patient.contactpreference_announcement_phone",
                    "patient.contactpreference_lab_sms",
                    "patient.guarantoraddresssameaspatient",
                    "patient.portaltermsonfile",
                    "patient.privacyinformationverified",
                    "patient.contactpreference_lab_email",
                    "patient.contactpreference_announcement_sms",
                    "patient.emailexists",
                };
                
                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("appointmentid"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        //bools
                        case ("chargeentrynotrequired"):
                        case ("coordinatorenterprise"):
                        case ("patient.contactpreference_lab_phone"):
                        case ("patient.consenttotext"):
                        case ("patient.contactpreference_billing_sms"):
                        case ("patient.contactpreference_appointment_phone"):
                        case ("patient.hasmobile"):
                        case ("patient.contactpreference_announcement_email"):
                        case ("patient.patientphoto"):
                        case ("patient.consenttocall"):
                        case ("patient.contactpreference_billing_email"):
                        case ("patient.donotcall"):
                        case ("patient.portalaccessgiven"):
                        case ("patient.driverslicense"):
                        case ("patient.contactpreference_appointment_email"):
                        case ("patient.homebound"):
                        case ("patient.contactpreference_appointment_sms"):
                        case ("patient.contactpreference_billing_phone"):
                        case ("patient.contactpreference_announcement_phone"):
                        case ("patient.contactpreference_lab_sms"):
                        case ("patient.guarantoraddresssameaspatient"):
                        case ("patient.portaltermsonfile"):
                        case ("patient.privacyinformationverified"):
                        case ("patient.contactpreference_lab_email"):
                        case ("patient.contactpreference_announcement_sms"):
                        case ("patient.emailexists"): 
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
                            break;
                        //ints
                        case ("duration"):
                        case ("hl7providerid"):
                        case ("appointmentcopay.collectedforother"):
                        case ("appointmentcopay.collectedforappointment"):
                        case ("appointmentcopay.insurancecopay"):
                        case ("copay"):
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

                var startDate = settings.StartDate;
                var endDate = string.IsNullOrWhiteSpace(settings.EndDate) ? DateTime.Today.ToString("MM/dd/yyyy") : settings.EndDate;
                
                foreach (var department in departmentResponse.Departments)
                {
                    var thisDepartmentId = department.DepartmentId ?? "";

                    var bookedApptsPath =
                        $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/appointments/booked?startdate={startDate}&enddate={endDate}&departmentid={thisDepartmentId}";
                    
                    var hasMore = false;

                    do
                    {
                        
                        var bookedApptsResult = await apiClient.GetAsync(bookedApptsPath);

                        if (!bookedApptsResult.IsSuccessStatusCode)
                        {
                            try
                            {
                                var errorResponse =
                                    JsonConvert.DeserializeObject<Error>(
                                        await bookedApptsResult.Content.ReadAsStringAsync());
                                    
                                throw new Exception(string.IsNullOrWhiteSpace(errorResponse.ErrorMessage) ? 
                                    bookedApptsResult.Content.ReadAsStringAsync().ToString() : errorResponse.ErrorMessage);
                            }
                            catch
                            {
                                throw new Exception(bookedApptsResult.Content.ReadAsStringAsync().ToString());
                            }
                            
                        }
                    
                        var bookedApptResponse = JsonConvert.DeserializeObject<BookedAppointmentResponse>(
                            await bookedApptsResult.Content.ReadAsStringAsync());
                        
                        if (bookedApptResponse.Appointments != null)
                        {
                           foreach (var bookedAppointment in bookedApptResponse.Appointments)
                            {
                                var recordMap = new Dictionary<string, object>();

                                //keys
                                recordMap["appointmentid"] = bookedAppointment.AppointmentId ?? "0";
                                
                                //bools
                                recordMap["coordinatorenterprise"] = bookedAppointment.CoordinatorEnterprise;
                                recordMap["chargeentrynotrequired"] = bookedAppointment.ChargeEntryNotRequired;
                                
                                //ints
                                recordMap["duration"] = bookedAppointment.Duration;
                                recordMap["hl7providerid"] = bookedAppointment.Hl7ProviderId;
                                recordMap["copay"] = bookedAppointment.Copay;
                                
                                if (bookedAppointment.AppointmentCopay != null)
                                {
                                    recordMap["appointmentcopay.collectedforother"] = bookedAppointment.AppointmentCopay.CollectedForOther;
                                    recordMap["appointmentcopay.collectedforappointment"] = bookedAppointment.AppointmentCopay.CollectedForAppointment;
                                    recordMap["appointmentcopay.insurancecopay"] = bookedAppointment.AppointmentCopay.InsuranceCopay;
                                }
                                
                                //strings
                                recordMap["date"] = bookedAppointment.Date ?? "";
                                recordMap["starttime"] = bookedAppointment.StartTime ?? "";
                                recordMap["departmentid"] = bookedAppointment.DepartmentId;
                                recordMap["appointmentstatus"] = bookedAppointment.AppointmentStatus ?? "";
                                recordMap["scheduledby"] = bookedAppointment.ScheduledBy ?? "";
                                recordMap["patientid"] = bookedAppointment.PatientId;
                                recordMap["templateappointmenttypeid"] = bookedAppointment.TemplateAppointmentTypeId;
                                recordMap["lastmodifiedby"] = bookedAppointment.LastModifiedBy ?? "";
                                recordMap["appointmenttypeid"] = bookedAppointment.AppointmentTypeId;
                                recordMap["lastmodified"] = bookedAppointment.LastModified ?? "";
                                recordMap["appointmenttype"] = bookedAppointment.AppointmentType ?? "";
                                recordMap["providerid"] = bookedAppointment.ProviderId;
                                recordMap["scheduleddatetime"] = bookedAppointment.ScheduledDateTime ?? "";
                                recordMap["templateappointmentid"] = bookedAppointment.TemplateAppointmentId;
                                recordMap["patientappointmenttypename"] = bookedAppointment.PatientAppointmentTypeName ?? "";
                                    
                                //Patient query
                                
                                var patientPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/patients/{bookedAppointment.PatientId.ToString()}";
                                
                                var patientResult = await apiClient.GetAsync(patientPath);
                                
                                if (!patientResult.IsSuccessStatusCode)
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                                var patientResponses =
                                    JsonConvert.DeserializeObject<List<PatientResponse>>(
                                        await patientResult.Content.ReadAsStringAsync());

                                if (patientResponses[0] == null)
                                {
                                    yield return new Record
                                    {
                                        Action = Record.Types.Action.Upsert,
                                        DataJson = JsonConvert.SerializeObject(recordMap)
                                    };
                                }

                                var patientResponse = patientResponses[0]; //patient received as a list of one patient object
                                
                                //strings
                                recordMap["patient.racename"] = patientResponse.Racename ?? "";
                                recordMap["patient.email"] = patientResponse.Email ?? "";
                                recordMap["patient.homephone"] = patientResponse.Homephone ?? "";
                                recordMap["patient.guarantorstate"] = patientResponse.Guarantorstate ?? "";
                                recordMap["patient.preferredpronouns"] = patientResponse.Preferredpronouns ?? "";
                                recordMap["patient.ethnicitycode"] = patientResponse.Ethnicitycode ?? "";
                                recordMap["patient.contactpreference"] = patientResponse.Contactpreference ?? "";
                                recordMap["patient.guarantordob"] = patientResponse.Guarantordob ?? "";
                                recordMap["patient.zip"] = patientResponse.Zip ?? "";
                                recordMap["patient.status"] = patientResponse.Status ?? "";
                                recordMap["patient.lastname"] = patientResponse.Lastname ?? "";
                                recordMap["patient.guarantorfirstname"] = patientResponse.Guarantorfirstname ?? "";
                                recordMap["patient.city"] = patientResponse.City ?? "";
                                recordMap["patient.lastappointment"] = patientResponse.Lastappointment ?? "";
                                recordMap["patient.genderidentity"] = patientResponse.Genderidentity ?? "";
                                recordMap["patient.guarantoremail"] = patientResponse.Guarantoremail ?? "";
                                recordMap["patient.guarantorcity"] = patientResponse.Guarantorcity ?? "";
                                recordMap["patient.guarantorzip"] = patientResponse.Guarantorzip ?? "";
                                recordMap["patient.sex"] = patientResponse.Sex ?? "";
                                recordMap["patient.primarydepartmentid"] = patientResponse.Primarydepartmentid ?? "";
                                recordMap["patient.firstappointment"] = patientResponse.Firstappointment ?? "";
                                recordMap["patient.language6392code"] = patientResponse.Language6392Code ?? "";
                                recordMap["patient.primaryproviderid"] = patientResponse.Primaryproviderid ?? "";
                                recordMap["patient.patientphotourl"] = patientResponse.Patientphotourl ?? "";
                                recordMap["patient.mobilephone"] = patientResponse.Mobilephone ?? "";
                                recordMap["patient.registrationdate"] = patientResponse.Registrationdate ?? "";
                                recordMap["patient.caresummarydeliverypreference"] = patientResponse.Caresummarydeliverypreference ?? "";
                                recordMap["patient.guarantorlastname"] = patientResponse.Guarantorlastname ?? "";
                                recordMap["patient.firstname"] = patientResponse.Firstname ?? "";
                                recordMap["patient.guarantorcountrycode"] = patientResponse.Guarantorcountrycode ?? "";
                                recordMap["patient.racecode"] = patientResponse.Racecode ?? "";
                                recordMap["patient.state"] = patientResponse.State ?? "";
                                recordMap["patient.dob"] = patientResponse.Dob ?? "";
                                recordMap["patient.patientid"] = patientResponse.Patientid ?? "";
                                recordMap["patient.guarantorrelationshiptopatient"] = patientResponse.Guarantorrelationshiptopatient ?? "";
                                recordMap["patient.address1"] = patientResponse.Address1 ?? "";
                                recordMap["patient.guarantorphone"] = patientResponse.Guarantorphone ?? "";
                                recordMap["patient.driverslicenseurl"] = patientResponse.Driverslicenseurl ?? "";
                                recordMap["patient.maritalstatus"] = patientResponse.Maritalstatus ?? "";
                                recordMap["patient.countrycode"] = patientResponse.Countrycode ?? "";
                                recordMap["patient.guarantoraddress1"] = patientResponse.Guarantoraddress1 ?? "";
                                recordMap["patient.maritalstatusname"] = patientResponse.Maritalstatusname ?? "";
                                recordMap["patient.countrycode3166"] = patientResponse.Countrycode3166 ?? "";
                                recordMap["patient.guarantorcountrycode3166"] = patientResponse.Guarantorcountrycode3166 ?? "";
                                recordMap["patient.race"] = string.Join(',', patientResponse.Race) ?? "";
                                
                                //bools
                                recordMap["patient.contactpreference_lab_phone"] =
                                    patientResponse.ContactpreferenceLabPhone;
                                recordMap["patient.consenttotext"] = patientResponse.Consenttotext;
                                recordMap["patient.contactpreference_billing_sms"] =
                                    patientResponse.ContactpreferenceBillingSms;
                                recordMap["patient.contactpreference_appointment_phone"] =
                                    patientResponse.ContactpreferenceAppointmentPhone;
                                recordMap["patient.hasmobile"] = patientResponse.Hasmobile;
                                recordMap["patient.contactpreference_announcement_email"] =
                                    patientResponse.ContactpreferenceAnnouncementEmail;
                                recordMap["patient.patientphoto"] = patientResponse.Patientphoto;
                                recordMap["patient.consenttocall"] = patientResponse.Consenttocall;
                                recordMap["patient.contactpreference_billing_email"] =
                                    patientResponse.ContactpreferenceBillingEmail;
                                recordMap["patient.donotcall"] = patientResponse.Donotcall;
                                recordMap["patient.portalaccessgiven"] = patientResponse.Portalaccessgiven;
                                recordMap["patient.driverslicense"] = patientResponse.Driverslicense;
                                recordMap["patient.contactpreference_appointment_email"] =
                                    patientResponse.ContactpreferenceAppointmentEmail;
                                recordMap["patient.homebound"] = patientResponse.Homebound;
                                recordMap["patient.contactpreference_appointment_sms"] =
                                    patientResponse.ContactpreferenceAppointmentSms;
                                recordMap["patient.contactpreference_billing_phone"] =
                                    patientResponse.ContactpreferenceBillingPhone;
                                recordMap["patient.contactpreference_announcement_phone"] =
                                    patientResponse.ContactpreferenceAnnouncementPhone;
                                recordMap["patient.contactpreference_lab_sms"] =
                                    patientResponse.ContactpreferenceLabSms;
                                recordMap["patient.guarantoraddresssameaspatient"] =
                                    patientResponse.Guarantoraddresssameaspatient;
                                recordMap["patient.portaltermsonfile"] = patientResponse.Portaltermsonfile;
                                recordMap["patient.privacyinformationverified"] =
                                    patientResponse.Privacyinformationverified;
                                recordMap["patient.contactpreference_lab_email"] = patientResponse.ContactpreferenceLabEmail;
                                recordMap["patient.contactpreference_announcement_sms"] =
                                    patientResponse.ContactpreferenceAnnouncementSms;
                                recordMap["patient.emailexists"] = patientResponse.Emailexists;
                                
                                yield return new Record
                                {
                                    Action = Record.Types.Action.Upsert,
                                    DataJson = JsonConvert.SerializeObject(recordMap)
                                }; 
                            }  
                        }
                        
                        if (string.IsNullOrWhiteSpace(bookedApptResponse.Next))
                        {
                            hasMore = false;
                        }
                        else
                        {
                            var nextUri =
                                new Uri($"{settings.GetBaseUrl(false).TrimEnd('/')}{bookedApptResponse.Next}");

                            var nextOffset = HttpUtility.ParseQueryString(nextUri.Query).Get("offset");

                            if (Int32.Parse(nextOffset) >= Int32.Parse(bookedApptResponse.Totalcount.ToString()))
                            {
                                hasMore = false;
                            }
                            else
                            {
                                hasMore = true;
                                bookedApptsPath = nextUri.ToString();
                            }
                        }
                    } while (hasMore);
                }
            }
        }
        private class BookedAppointmentsEndpoint_Today : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    
                    //From: BookedAppointments
                    //keys
                    "appointmentid",
                    
                    //bools
                    "coordinatorenterprise",
                    "chargeentrynotrequired",
                    
                    //ints
                    "duration",
                    "hl7providerid",
                    "appointmentcopay.collectedforother",
                    "appointmentcopay.collectedforappointment",
                    "appointmentcopay.insurancecopay",
                    "copay",
                    
                    //strings
                    "date",
                    "starttime",
                    "departmentid",
                    "appointmentstatus",
                    "scheduledby",
                    "patientid",
                    "templateappointmenttypeid",
                    "lastmodifiedby",
                    "appointmenttypeid",
                    "lastmodified",
                    "appointmenttype",
                    "providerid",
                    "scheduleddatetime",
                    "templateappointmentid",
                    "patientappointmenttypename",
                    
                    //From: Patient
                    //strings
                    "patient.racename",
                    "patient.email",
                    "patient.homephone",
                    "patient.guarantorstate",
                    "patient.preferredpronouns",
                    "patient.ethnicitycode",
                    "patient.contactpreference",
                    "patient.guarantordob",
                    "patient.zip",
                    "patient.status",
                    "patient.lastname",
                    "patient.guarantorfirstname",
                    "patient.city",
                    "patient.lastappointment",
                    "patient.genderidentity",
                    "patient.guarantoremail",
                    "patient.guarantorcity",
                    "patient.guarantorzip",
                    "patient.sex",
                    "patient.primarydepartmentid",
                    "patient.firstappointment",
                    "patient.language6392code",
                    "patient.primaryproviderid",
                    "patient.patientphotourl",
                    "patient.mobilephone",
                    "patient.registrationdate",
                    "patient.caresummarydeliverypreference",
                    "patient.guarantorlastname",
                    "patient.firstname",
                    "patient.guarantorcountrycode",
                    "patient.racecode",
                    "patient.state",
                    "patient.dob",
                    "patient.patientid",
                    "patient.guarantorrelationshiptopatient",
                    "patient.address1",
                    "patient.guarantorphone",
                    "patient.driverslicenseurl",
                    "patient.maritalstatus",
                    "patient.countrycode",
                    "patient.guarantoraddress1",
                    "patient.maritalstatusname",
                    "patient.countrycode3166",
                    "patient.guarantorcountrycode3166",
                    "patient.race",
                    
                    //bools
                    "patient.contactpreference_lab_phone",
                    "patient.consenttotext",
                    "patient.contactpreference_billing_sms",
                    "patient.contactpreference_appointment_phone",
                    "patient.hasmobile",
                    "patient.contactpreference_announcement_email",
                    "patient.patientphoto",
                    "patient.consenttocall",
                    "patient.contactpreference_billing_email",
                    "patient.donotcall",
                    "patient.portalaccessgiven",
                    "patient.driverslicense",
                    "patient.contactpreference_appointment_email",
                    "patient.homebound",
                    "patient.contactpreference_appointment_sms",
                    "patient.contactpreference_billing_phone",
                    "patient.contactpreference_announcement_phone",
                    "patient.contactpreference_lab_sms",
                    "patient.guarantoraddresssameaspatient",
                    "patient.portaltermsonfile",
                    "patient.privacyinformationverified",
                    "patient.contactpreference_lab_email",
                    "patient.contactpreference_announcement_sms",
                    "patient.emailexists",
                };
                
                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("appointmentid"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        //bools
                        case ("chargeentrynotrequired"):
                        case ("coordinatorenterprise"):
                        case ("patient.contactpreference_lab_phone"):
                        case ("patient.consenttotext"):
                        case ("patient.contactpreference_billing_sms"):
                        case ("patient.contactpreference_appointment_phone"):
                        case ("patient.hasmobile"):
                        case ("patient.contactpreference_announcement_email"):
                        case ("patient.patientphoto"):
                        case ("patient.consenttocall"):
                        case ("patient.contactpreference_billing_email"):
                        case ("patient.donotcall"):
                        case ("patient.portalaccessgiven"):
                        case ("patient.driverslicense"):
                        case ("patient.contactpreference_appointment_email"):
                        case ("patient.homebound"):
                        case ("patient.contactpreference_appointment_sms"):
                        case ("patient.contactpreference_billing_phone"):
                        case ("patient.contactpreference_announcement_phone"):
                        case ("patient.contactpreference_lab_sms"):
                        case ("patient.guarantoraddresssameaspatient"):
                        case ("patient.portaltermsonfile"):
                        case ("patient.privacyinformationverified"):
                        case ("patient.contactpreference_lab_email"):
                        case ("patient.contactpreference_announcement_sms"):
                        case ("patient.emailexists"): 
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
                            break;
                        //ints
                        case ("duration"):
                        case ("hl7providerid"):
                        case ("appointmentcopay.collectedforother"):
                        case ("appointmentcopay.collectedforappointment"):
                        case ("appointmentcopay.insurancecopay"):
                        case ("copay"):
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

                var startDate = DateTime.Today.ToString("MM/dd/yyyy");
                var endDate = DateTime.Today.ToString("MM/dd/yyyy");
                
                foreach (var department in departmentResponse.Departments)
                {
                    var thisDepartmentId = department.DepartmentId ?? "";

                    var bookedApptsPath =
                        $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/appointments/booked?startdate={startDate}&enddate={endDate}&departmentid={thisDepartmentId}";
                    
                    var hasMore = false;

                    do
                    {
                        
                        var bookedApptsResult = await apiClient.GetAsync(bookedApptsPath);

                        if (!bookedApptsResult.IsSuccessStatusCode)
                        {
                            try
                            {
                                var errorResponse =
                                    JsonConvert.DeserializeObject<Error>(
                                        await bookedApptsResult.Content.ReadAsStringAsync());
                                    
                                throw new Exception(string.IsNullOrWhiteSpace(errorResponse.ErrorMessage) ? 
                                    bookedApptsResult.Content.ReadAsStringAsync().ToString() : errorResponse.ErrorMessage);
                            }
                            catch
                            {
                                throw new Exception(bookedApptsResult.Content.ReadAsStringAsync().ToString());
                            }
                            
                        }
                    
                        var bookedApptResponse = JsonConvert.DeserializeObject<BookedAppointmentResponse>(
                            await bookedApptsResult.Content.ReadAsStringAsync());
                        
                        if (bookedApptResponse.Appointments != null)
                        {
                           foreach (var bookedAppointment in bookedApptResponse.Appointments)
                            {
                                var recordMap = new Dictionary<string, object>();

                                //keys
                                recordMap["appointmentid"] = bookedAppointment.AppointmentId ?? "0";
                                
                                //bools
                                recordMap["coordinatorenterprise"] = bookedAppointment.CoordinatorEnterprise;
                                recordMap["chargeentrynotrequired"] = bookedAppointment.ChargeEntryNotRequired;
                                
                                //ints
                                recordMap["duration"] = bookedAppointment.Duration;
                                recordMap["hl7providerid"] = bookedAppointment.Hl7ProviderId;
                                recordMap["copay"] = bookedAppointment.Copay;
                                
                                if (bookedAppointment.AppointmentCopay != null)
                                {
                                    recordMap["appointmentcopay.collectedforother"] = bookedAppointment.AppointmentCopay.CollectedForOther;
                                    recordMap["appointmentcopay.collectedforappointment"] = bookedAppointment.AppointmentCopay.CollectedForAppointment;
                                    recordMap["appointmentcopay.insurancecopay"] = bookedAppointment.AppointmentCopay.InsuranceCopay;
                                }
                                
                                //strings
                                recordMap["date"] = bookedAppointment.Date ?? "";
                                recordMap["starttime"] = bookedAppointment.StartTime ?? "";
                                recordMap["departmentid"] = bookedAppointment.DepartmentId;
                                recordMap["appointmentstatus"] = bookedAppointment.AppointmentStatus ?? "";
                                recordMap["scheduledby"] = bookedAppointment.ScheduledBy ?? "";
                                recordMap["patientid"] = bookedAppointment.PatientId;
                                recordMap["templateappointmenttypeid"] = bookedAppointment.TemplateAppointmentTypeId;
                                recordMap["lastmodifiedby"] = bookedAppointment.LastModifiedBy ?? "";
                                recordMap["appointmenttypeid"] = bookedAppointment.AppointmentTypeId;
                                recordMap["lastmodified"] = bookedAppointment.LastModified ?? "";
                                recordMap["appointmenttype"] = bookedAppointment.AppointmentType ?? "";
                                recordMap["providerid"] = bookedAppointment.ProviderId;
                                recordMap["scheduleddatetime"] = bookedAppointment.ScheduledDateTime ?? "";
                                recordMap["templateappointmentid"] = bookedAppointment.TemplateAppointmentId;
                                recordMap["patientappointmenttypename"] = bookedAppointment.PatientAppointmentTypeName ?? "";
                                    
                                //Patient query
                                
                                var patientPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/patients/{bookedAppointment.PatientId.ToString()}";
                                
                                var patientResult = await apiClient.GetAsync(patientPath);
                                
                                if (!patientResult.IsSuccessStatusCode)
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                                var patientResponses =
                                    JsonConvert.DeserializeObject<List<PatientResponse>>(
                                        await patientResult.Content.ReadAsStringAsync());

                                if (patientResponses[0] == null)
                                {
                                    yield return new Record
                                    {
                                        Action = Record.Types.Action.Upsert,
                                        DataJson = JsonConvert.SerializeObject(recordMap)
                                    };
                                }

                                var patientResponse = patientResponses[0]; //patient received as a list of one patient object
                                
                                //strings
                                recordMap["patient.racename"] = patientResponse.Racename ?? "";
                                recordMap["patient.email"] = patientResponse.Email ?? "";
                                recordMap["patient.homephone"] = patientResponse.Homephone ?? "";
                                recordMap["patient.guarantorstate"] = patientResponse.Guarantorstate ?? "";
                                recordMap["patient.preferredpronouns"] = patientResponse.Preferredpronouns ?? "";
                                recordMap["patient.ethnicitycode"] = patientResponse.Ethnicitycode ?? "";
                                recordMap["patient.contactpreference"] = patientResponse.Contactpreference ?? "";
                                recordMap["patient.guarantordob"] = patientResponse.Guarantordob ?? "";
                                recordMap["patient.zip"] = patientResponse.Zip ?? "";
                                recordMap["patient.status"] = patientResponse.Status ?? "";
                                recordMap["patient.lastname"] = patientResponse.Lastname ?? "";
                                recordMap["patient.guarantorfirstname"] = patientResponse.Guarantorfirstname ?? "";
                                recordMap["patient.city"] = patientResponse.City ?? "";
                                recordMap["patient.lastappointment"] = patientResponse.Lastappointment ?? "";
                                recordMap["patient.genderidentity"] = patientResponse.Genderidentity ?? "";
                                recordMap["patient.guarantoremail"] = patientResponse.Guarantoremail ?? "";
                                recordMap["patient.guarantorcity"] = patientResponse.Guarantorcity ?? "";
                                recordMap["patient.guarantorzip"] = patientResponse.Guarantorzip ?? "";
                                recordMap["patient.sex"] = patientResponse.Sex ?? "";
                                recordMap["patient.primarydepartmentid"] = patientResponse.Primarydepartmentid ?? "";
                                recordMap["patient.firstappointment"] = patientResponse.Firstappointment ?? "";
                                recordMap["patient.language6392code"] = patientResponse.Language6392Code ?? "";
                                recordMap["patient.primaryproviderid"] = patientResponse.Primaryproviderid ?? "";
                                recordMap["patient.patientphotourl"] = patientResponse.Patientphotourl ?? "";
                                recordMap["patient.mobilephone"] = patientResponse.Mobilephone ?? "";
                                recordMap["patient.registrationdate"] = patientResponse.Registrationdate ?? "";
                                recordMap["patient.caresummarydeliverypreference"] = patientResponse.Caresummarydeliverypreference ?? "";
                                recordMap["patient.guarantorlastname"] = patientResponse.Guarantorlastname ?? "";
                                recordMap["patient.firstname"] = patientResponse.Firstname ?? "";
                                recordMap["patient.guarantorcountrycode"] = patientResponse.Guarantorcountrycode ?? "";
                                recordMap["patient.racecode"] = patientResponse.Racecode ?? "";
                                recordMap["patient.state"] = patientResponse.State ?? "";
                                recordMap["patient.dob"] = patientResponse.Dob ?? "";
                                recordMap["patient.patientid"] = patientResponse.Patientid ?? "";
                                recordMap["patient.guarantorrelationshiptopatient"] = patientResponse.Guarantorrelationshiptopatient ?? "";
                                recordMap["patient.address1"] = patientResponse.Address1 ?? "";
                                recordMap["patient.guarantorphone"] = patientResponse.Guarantorphone ?? "";
                                recordMap["patient.driverslicenseurl"] = patientResponse.Driverslicenseurl ?? "";
                                recordMap["patient.maritalstatus"] = patientResponse.Maritalstatus ?? "";
                                recordMap["patient.countrycode"] = patientResponse.Countrycode ?? "";
                                recordMap["patient.guarantoraddress1"] = patientResponse.Guarantoraddress1 ?? "";
                                recordMap["patient.maritalstatusname"] = patientResponse.Maritalstatusname ?? "";
                                recordMap["patient.countrycode3166"] = patientResponse.Countrycode3166 ?? "";
                                recordMap["patient.guarantorcountrycode3166"] = patientResponse.Guarantorcountrycode3166 ?? "";
                                recordMap["patient.race"] = string.Join(',', patientResponse.Race) ?? "";
                                
                                //bools
                                recordMap["patient.contactpreference_lab_phone"] =
                                    patientResponse.ContactpreferenceLabPhone;
                                recordMap["patient.consenttotext"] = patientResponse.Consenttotext;
                                recordMap["patient.contactpreference_billing_sms"] =
                                    patientResponse.ContactpreferenceBillingSms;
                                recordMap["patient.contactpreference_appointment_phone"] =
                                    patientResponse.ContactpreferenceAppointmentPhone;
                                recordMap["patient.hasmobile"] = patientResponse.Hasmobile;
                                recordMap["patient.contactpreference_announcement_email"] =
                                    patientResponse.ContactpreferenceAnnouncementEmail;
                                recordMap["patient.patientphoto"] = patientResponse.Patientphoto;
                                recordMap["patient.consenttocall"] = patientResponse.Consenttocall;
                                recordMap["patient.contactpreference_billing_email"] =
                                    patientResponse.ContactpreferenceBillingEmail;
                                recordMap["patient.donotcall"] = patientResponse.Donotcall;
                                recordMap["patient.portalaccessgiven"] = patientResponse.Portalaccessgiven;
                                recordMap["patient.driverslicense"] = patientResponse.Driverslicense;
                                recordMap["patient.contactpreference_appointment_email"] =
                                    patientResponse.ContactpreferenceAppointmentEmail;
                                recordMap["patient.homebound"] = patientResponse.Homebound;
                                recordMap["patient.contactpreference_appointment_sms"] =
                                    patientResponse.ContactpreferenceAppointmentSms;
                                recordMap["patient.contactpreference_billing_phone"] =
                                    patientResponse.ContactpreferenceBillingPhone;
                                recordMap["patient.contactpreference_announcement_phone"] =
                                    patientResponse.ContactpreferenceAnnouncementPhone;
                                recordMap["patient.contactpreference_lab_sms"] =
                                    patientResponse.ContactpreferenceLabSms;
                                recordMap["patient.guarantoraddresssameaspatient"] =
                                    patientResponse.Guarantoraddresssameaspatient;
                                recordMap["patient.portaltermsonfile"] = patientResponse.Portaltermsonfile;
                                recordMap["patient.privacyinformationverified"] =
                                    patientResponse.Privacyinformationverified;
                                recordMap["patient.contactpreference_lab_email"] = patientResponse.ContactpreferenceLabEmail;
                                recordMap["patient.contactpreference_announcement_sms"] =
                                    patientResponse.ContactpreferenceAnnouncementSms;
                                recordMap["patient.emailexists"] = patientResponse.Emailexists;
                                
                                yield return new Record
                                {
                                    Action = Record.Types.Action.Upsert,
                                    DataJson = JsonConvert.SerializeObject(recordMap)
                                }; 
                            }  
                        }
                        
                        if (string.IsNullOrWhiteSpace(bookedApptResponse.Next))
                        {
                            hasMore = false;
                        }
                        else
                        {
                            var nextUri =
                                new Uri($"{settings.GetBaseUrl(false).TrimEnd('/')}{bookedApptResponse.Next}");

                            var nextOffset = HttpUtility.ParseQueryString(nextUri.Query).Get("offset");

                            if (Int32.Parse(nextOffset) >= Int32.Parse(bookedApptResponse.Totalcount.ToString()))
                            {
                                hasMore = false;
                            }
                            else
                            {
                                hasMore = true;
                                bookedApptsPath = nextUri.ToString();
                            }
                        }
                    } while (hasMore);
                }
            }
        }
        private class BookedAppointmentsEndpoint_Yesterday : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    
                    //From: BookedAppointments
                    //keys
                    "appointmentid",
                    
                    //bools
                    "coordinatorenterprise",
                    "chargeentrynotrequired",
                    
                    //ints
                    "duration",
                    "hl7providerid",
                    "appointmentcopay.collectedforother",
                    "appointmentcopay.collectedforappointment",
                    "appointmentcopay.insurancecopay",
                    "copay",
                    
                    //strings
                    "date",
                    "starttime",
                    "departmentid",
                    "appointmentstatus",
                    "scheduledby",
                    "patientid",
                    "templateappointmenttypeid",
                    "lastmodifiedby",
                    "appointmenttypeid",
                    "lastmodified",
                    "appointmenttype",
                    "providerid",
                    "scheduleddatetime",
                    "templateappointmentid",
                    "patientappointmenttypename",
                    
                    //From: Patient
                    //strings
                    "patient.racename",
                    "patient.email",
                    "patient.homephone",
                    "patient.guarantorstate",
                    "patient.preferredpronouns",
                    "patient.ethnicitycode",
                    "patient.contactpreference",
                    "patient.guarantordob",
                    "patient.zip",
                    "patient.status",
                    "patient.lastname",
                    "patient.guarantorfirstname",
                    "patient.city",
                    "patient.lastappointment",
                    "patient.genderidentity",
                    "patient.guarantoremail",
                    "patient.guarantorcity",
                    "patient.guarantorzip",
                    "patient.sex",
                    "patient.primarydepartmentid",
                    "patient.firstappointment",
                    "patient.language6392code",
                    "patient.primaryproviderid",
                    "patient.patientphotourl",
                    "patient.mobilephone",
                    "patient.registrationdate",
                    "patient.caresummarydeliverypreference",
                    "patient.guarantorlastname",
                    "patient.firstname",
                    "patient.guarantorcountrycode",
                    "patient.racecode",
                    "patient.state",
                    "patient.dob",
                    "patient.patientid",
                    "patient.guarantorrelationshiptopatient",
                    "patient.address1",
                    "patient.guarantorphone",
                    "patient.driverslicenseurl",
                    "patient.maritalstatus",
                    "patient.countrycode",
                    "patient.guarantoraddress1",
                    "patient.maritalstatusname",
                    "patient.countrycode3166",
                    "patient.guarantorcountrycode3166",
                    "patient.race",
                    
                    //bools
                    "patient.contactpreference_lab_phone",
                    "patient.consenttotext",
                    "patient.contactpreference_billing_sms",
                    "patient.contactpreference_appointment_phone",
                    "patient.hasmobile",
                    "patient.contactpreference_announcement_email",
                    "patient.patientphoto",
                    "patient.consenttocall",
                    "patient.contactpreference_billing_email",
                    "patient.donotcall",
                    "patient.portalaccessgiven",
                    "patient.driverslicense",
                    "patient.contactpreference_appointment_email",
                    "patient.homebound",
                    "patient.contactpreference_appointment_sms",
                    "patient.contactpreference_billing_phone",
                    "patient.contactpreference_announcement_phone",
                    "patient.contactpreference_lab_sms",
                    "patient.guarantoraddresssameaspatient",
                    "patient.portaltermsonfile",
                    "patient.privacyinformationverified",
                    "patient.contactpreference_lab_email",
                    "patient.contactpreference_announcement_sms",
                    "patient.emailexists",
                };
                
                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("appointmentid"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        //bools
                        case ("chargeentrynotrequired"):
                        case ("coordinatorenterprise"):
                        case ("patient.contactpreference_lab_phone"):
                        case ("patient.consenttotext"):
                        case ("patient.contactpreference_billing_sms"):
                        case ("patient.contactpreference_appointment_phone"):
                        case ("patient.hasmobile"):
                        case ("patient.contactpreference_announcement_email"):
                        case ("patient.patientphoto"):
                        case ("patient.consenttocall"):
                        case ("patient.contactpreference_billing_email"):
                        case ("patient.donotcall"):
                        case ("patient.portalaccessgiven"):
                        case ("patient.driverslicense"):
                        case ("patient.contactpreference_appointment_email"):
                        case ("patient.homebound"):
                        case ("patient.contactpreference_appointment_sms"):
                        case ("patient.contactpreference_billing_phone"):
                        case ("patient.contactpreference_announcement_phone"):
                        case ("patient.contactpreference_lab_sms"):
                        case ("patient.guarantoraddresssameaspatient"):
                        case ("patient.portaltermsonfile"):
                        case ("patient.privacyinformationverified"):
                        case ("patient.contactpreference_lab_email"):
                        case ("patient.contactpreference_announcement_sms"):
                        case ("patient.emailexists"): 
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
                            break;
                        //ints
                        case ("duration"):
                        case ("hl7providerid"):
                        case ("appointmentcopay.collectedforother"):
                        case ("appointmentcopay.collectedforappointment"):
                        case ("appointmentcopay.insurancecopay"):
                        case ("copay"):
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

                var startDate = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
                var endDate = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
                
                foreach (var department in departmentResponse.Departments)
                {
                    var thisDepartmentId = department.DepartmentId ?? "";

                    var bookedApptsPath =
                        $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/appointments/booked?startdate={startDate}&enddate={endDate}&departmentid={thisDepartmentId}";
                    
                    var hasMore = false;

                    do
                    {
                        
                        var bookedApptsResult = await apiClient.GetAsync(bookedApptsPath);

                        if (!bookedApptsResult.IsSuccessStatusCode)
                        {
                            try
                            {
                                var errorResponse =
                                    JsonConvert.DeserializeObject<Error>(
                                        await bookedApptsResult.Content.ReadAsStringAsync());
                                    
                                throw new Exception(string.IsNullOrWhiteSpace(errorResponse.ErrorMessage) ? 
                                    bookedApptsResult.Content.ReadAsStringAsync().ToString() : errorResponse.ErrorMessage);
                            }
                            catch
                            {
                                throw new Exception(bookedApptsResult.Content.ReadAsStringAsync().ToString());
                            }
                            
                        }
                    
                        var bookedApptResponse = JsonConvert.DeserializeObject<BookedAppointmentResponse>(
                            await bookedApptsResult.Content.ReadAsStringAsync());
                        
                        if (bookedApptResponse.Appointments != null)
                        {
                           foreach (var bookedAppointment in bookedApptResponse.Appointments)
                            {
                                var recordMap = new Dictionary<string, object>();

                                //keys
                                recordMap["appointmentid"] = bookedAppointment.AppointmentId ?? "0";
                                
                                //bools
                                recordMap["coordinatorenterprise"] = bookedAppointment.CoordinatorEnterprise;
                                recordMap["chargeentrynotrequired"] = bookedAppointment.ChargeEntryNotRequired;
                                
                                //ints
                                recordMap["duration"] = bookedAppointment.Duration;
                                recordMap["hl7providerid"] = bookedAppointment.Hl7ProviderId;
                                recordMap["copay"] = bookedAppointment.Copay;
                                
                                if (bookedAppointment.AppointmentCopay != null)
                                {
                                    recordMap["appointmentcopay.collectedforother"] = bookedAppointment.AppointmentCopay.CollectedForOther;
                                    recordMap["appointmentcopay.collectedforappointment"] = bookedAppointment.AppointmentCopay.CollectedForAppointment;
                                    recordMap["appointmentcopay.insurancecopay"] = bookedAppointment.AppointmentCopay.InsuranceCopay;
                                }
                                
                                //strings
                                recordMap["date"] = bookedAppointment.Date ?? "";
                                recordMap["starttime"] = bookedAppointment.StartTime ?? "";
                                recordMap["departmentid"] = bookedAppointment.DepartmentId;
                                recordMap["appointmentstatus"] = bookedAppointment.AppointmentStatus ?? "";
                                recordMap["scheduledby"] = bookedAppointment.ScheduledBy ?? "";
                                recordMap["patientid"] = bookedAppointment.PatientId;
                                recordMap["templateappointmenttypeid"] = bookedAppointment.TemplateAppointmentTypeId;
                                recordMap["lastmodifiedby"] = bookedAppointment.LastModifiedBy ?? "";
                                recordMap["appointmenttypeid"] = bookedAppointment.AppointmentTypeId;
                                recordMap["lastmodified"] = bookedAppointment.LastModified ?? "";
                                recordMap["appointmenttype"] = bookedAppointment.AppointmentType ?? "";
                                recordMap["providerid"] = bookedAppointment.ProviderId;
                                recordMap["scheduleddatetime"] = bookedAppointment.ScheduledDateTime ?? "";
                                recordMap["templateappointmentid"] = bookedAppointment.TemplateAppointmentId;
                                recordMap["patientappointmenttypename"] = bookedAppointment.PatientAppointmentTypeName ?? "";
                                    
                                //Patient query
                                
                                var patientPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/patients/{bookedAppointment.PatientId.ToString()}";
                                
                                var patientResult = await apiClient.GetAsync(patientPath);
                                
                                if (!patientResult.IsSuccessStatusCode)
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                                var patientResponses =
                                    JsonConvert.DeserializeObject<List<PatientResponse>>(
                                        await patientResult.Content.ReadAsStringAsync());

                                if (patientResponses[0] == null)
                                {
                                    yield return new Record
                                    {
                                        Action = Record.Types.Action.Upsert,
                                        DataJson = JsonConvert.SerializeObject(recordMap)
                                    };
                                }

                                var patientResponse = patientResponses[0]; //patient received as a list of one patient object
                                
                                //strings
                                recordMap["patient.racename"] = patientResponse.Racename ?? "";
                                recordMap["patient.email"] = patientResponse.Email ?? "";
                                recordMap["patient.homephone"] = patientResponse.Homephone ?? "";
                                recordMap["patient.guarantorstate"] = patientResponse.Guarantorstate ?? "";
                                recordMap["patient.preferredpronouns"] = patientResponse.Preferredpronouns ?? "";
                                recordMap["patient.ethnicitycode"] = patientResponse.Ethnicitycode ?? "";
                                recordMap["patient.contactpreference"] = patientResponse.Contactpreference ?? "";
                                recordMap["patient.guarantordob"] = patientResponse.Guarantordob ?? "";
                                recordMap["patient.zip"] = patientResponse.Zip ?? "";
                                recordMap["patient.status"] = patientResponse.Status ?? "";
                                recordMap["patient.lastname"] = patientResponse.Lastname ?? "";
                                recordMap["patient.guarantorfirstname"] = patientResponse.Guarantorfirstname ?? "";
                                recordMap["patient.city"] = patientResponse.City ?? "";
                                recordMap["patient.lastappointment"] = patientResponse.Lastappointment ?? "";
                                recordMap["patient.genderidentity"] = patientResponse.Genderidentity ?? "";
                                recordMap["patient.guarantoremail"] = patientResponse.Guarantoremail ?? "";
                                recordMap["patient.guarantorcity"] = patientResponse.Guarantorcity ?? "";
                                recordMap["patient.guarantorzip"] = patientResponse.Guarantorzip ?? "";
                                recordMap["patient.sex"] = patientResponse.Sex ?? "";
                                recordMap["patient.primarydepartmentid"] = patientResponse.Primarydepartmentid ?? "";
                                recordMap["patient.firstappointment"] = patientResponse.Firstappointment ?? "";
                                recordMap["patient.language6392code"] = patientResponse.Language6392Code ?? "";
                                recordMap["patient.primaryproviderid"] = patientResponse.Primaryproviderid ?? "";
                                recordMap["patient.patientphotourl"] = patientResponse.Patientphotourl ?? "";
                                recordMap["patient.mobilephone"] = patientResponse.Mobilephone ?? "";
                                recordMap["patient.registrationdate"] = patientResponse.Registrationdate ?? "";
                                recordMap["patient.caresummarydeliverypreference"] = patientResponse.Caresummarydeliverypreference ?? "";
                                recordMap["patient.guarantorlastname"] = patientResponse.Guarantorlastname ?? "";
                                recordMap["patient.firstname"] = patientResponse.Firstname ?? "";
                                recordMap["patient.guarantorcountrycode"] = patientResponse.Guarantorcountrycode ?? "";
                                recordMap["patient.racecode"] = patientResponse.Racecode ?? "";
                                recordMap["patient.state"] = patientResponse.State ?? "";
                                recordMap["patient.dob"] = patientResponse.Dob ?? "";
                                recordMap["patient.patientid"] = patientResponse.Patientid ?? "";
                                recordMap["patient.guarantorrelationshiptopatient"] = patientResponse.Guarantorrelationshiptopatient ?? "";
                                recordMap["patient.address1"] = patientResponse.Address1 ?? "";
                                recordMap["patient.guarantorphone"] = patientResponse.Guarantorphone ?? "";
                                recordMap["patient.driverslicenseurl"] = patientResponse.Driverslicenseurl ?? "";
                                recordMap["patient.maritalstatus"] = patientResponse.Maritalstatus ?? "";
                                recordMap["patient.countrycode"] = patientResponse.Countrycode ?? "";
                                recordMap["patient.guarantoraddress1"] = patientResponse.Guarantoraddress1 ?? "";
                                recordMap["patient.maritalstatusname"] = patientResponse.Maritalstatusname ?? "";
                                recordMap["patient.countrycode3166"] = patientResponse.Countrycode3166 ?? "";
                                recordMap["patient.guarantorcountrycode3166"] = patientResponse.Guarantorcountrycode3166 ?? "";
                                recordMap["patient.race"] = string.Join(',', patientResponse.Race) ?? "";
                                
                                //bools
                                recordMap["patient.contactpreference_lab_phone"] =
                                    patientResponse.ContactpreferenceLabPhone;
                                recordMap["patient.consenttotext"] = patientResponse.Consenttotext;
                                recordMap["patient.contactpreference_billing_sms"] =
                                    patientResponse.ContactpreferenceBillingSms;
                                recordMap["patient.contactpreference_appointment_phone"] =
                                    patientResponse.ContactpreferenceAppointmentPhone;
                                recordMap["patient.hasmobile"] = patientResponse.Hasmobile;
                                recordMap["patient.contactpreference_announcement_email"] =
                                    patientResponse.ContactpreferenceAnnouncementEmail;
                                recordMap["patient.patientphoto"] = patientResponse.Patientphoto;
                                recordMap["patient.consenttocall"] = patientResponse.Consenttocall;
                                recordMap["patient.contactpreference_billing_email"] =
                                    patientResponse.ContactpreferenceBillingEmail;
                                recordMap["patient.donotcall"] = patientResponse.Donotcall;
                                recordMap["patient.portalaccessgiven"] = patientResponse.Portalaccessgiven;
                                recordMap["patient.driverslicense"] = patientResponse.Driverslicense;
                                recordMap["patient.contactpreference_appointment_email"] =
                                    patientResponse.ContactpreferenceAppointmentEmail;
                                recordMap["patient.homebound"] = patientResponse.Homebound;
                                recordMap["patient.contactpreference_appointment_sms"] =
                                    patientResponse.ContactpreferenceAppointmentSms;
                                recordMap["patient.contactpreference_billing_phone"] =
                                    patientResponse.ContactpreferenceBillingPhone;
                                recordMap["patient.contactpreference_announcement_phone"] =
                                    patientResponse.ContactpreferenceAnnouncementPhone;
                                recordMap["patient.contactpreference_lab_sms"] =
                                    patientResponse.ContactpreferenceLabSms;
                                recordMap["patient.guarantoraddresssameaspatient"] =
                                    patientResponse.Guarantoraddresssameaspatient;
                                recordMap["patient.portaltermsonfile"] = patientResponse.Portaltermsonfile;
                                recordMap["patient.privacyinformationverified"] =
                                    patientResponse.Privacyinformationverified;
                                recordMap["patient.contactpreference_lab_email"] = patientResponse.ContactpreferenceLabEmail;
                                recordMap["patient.contactpreference_announcement_sms"] =
                                    patientResponse.ContactpreferenceAnnouncementSms;
                                recordMap["patient.emailexists"] = patientResponse.Emailexists;
                                
                                yield return new Record
                                {
                                    Action = Record.Types.Action.Upsert,
                                    DataJson = JsonConvert.SerializeObject(recordMap)
                                }; 
                            }  
                        }
                        
                        if (string.IsNullOrWhiteSpace(bookedApptResponse.Next))
                        {
                            hasMore = false;
                        }
                        else
                        {
                            var nextUri =
                                new Uri($"{settings.GetBaseUrl(false).TrimEnd('/')}{bookedApptResponse.Next}");

                            var nextOffset = HttpUtility.ParseQueryString(nextUri.Query).Get("offset");

                            if (Int32.Parse(nextOffset) >= Int32.Parse(bookedApptResponse.Totalcount.ToString()))
                            {
                                hasMore = false;
                            }
                            else
                            {
                                hasMore = true;
                                bookedApptsPath = nextUri.ToString();
                            }
                        }
                    } while (hasMore);
                }
            }
        }
        private class BookedAppointmentsEndpoint_Last7Days : Endpoint
        {
            public override bool ShouldGetStaticSchema { get; set; } = true;

            public async override Task<Schema> GetStaticSchemaAsync(IApiClient apiClient, Schema schema)
            {
                List<string> staticSchemaProperties = new List<string>()
                {
                    
                    //From: BookedAppointments
                    //keys
                    "appointmentid",
                    
                    //bools
                    "coordinatorenterprise",
                    "chargeentrynotrequired",
                    
                    //ints
                    "duration",
                    "hl7providerid",
                    "appointmentcopay.collectedforother",
                    "appointmentcopay.collectedforappointment",
                    "appointmentcopay.insurancecopay",
                    "copay",
                    
                    //strings
                    "date",
                    "starttime",
                    "departmentid",
                    "appointmentstatus",
                    "scheduledby",
                    "patientid",
                    "templateappointmenttypeid",
                    "lastmodifiedby",
                    "appointmenttypeid",
                    "lastmodified",
                    "appointmenttype",
                    "providerid",
                    "scheduleddatetime",
                    "templateappointmentid",
                    "patientappointmenttypename",
                    
                    //From: Patient
                    //strings
                    "patient.racename",
                    "patient.email",
                    "patient.homephone",
                    "patient.guarantorstate",
                    "patient.preferredpronouns",
                    "patient.ethnicitycode",
                    "patient.contactpreference",
                    "patient.guarantordob",
                    "patient.zip",
                    "patient.status",
                    "patient.lastname",
                    "patient.guarantorfirstname",
                    "patient.city",
                    "patient.lastappointment",
                    "patient.genderidentity",
                    "patient.guarantoremail",
                    "patient.guarantorcity",
                    "patient.guarantorzip",
                    "patient.sex",
                    "patient.primarydepartmentid",
                    "patient.firstappointment",
                    "patient.language6392code",
                    "patient.primaryproviderid",
                    "patient.patientphotourl",
                    "patient.mobilephone",
                    "patient.registrationdate",
                    "patient.caresummarydeliverypreference",
                    "patient.guarantorlastname",
                    "patient.firstname",
                    "patient.guarantorcountrycode",
                    "patient.racecode",
                    "patient.state",
                    "patient.dob",
                    "patient.patientid",
                    "patient.guarantorrelationshiptopatient",
                    "patient.address1",
                    "patient.guarantorphone",
                    "patient.driverslicenseurl",
                    "patient.maritalstatus",
                    "patient.countrycode",
                    "patient.guarantoraddress1",
                    "patient.maritalstatusname",
                    "patient.countrycode3166",
                    "patient.guarantorcountrycode3166",
                    "patient.race",
                    
                    //bools
                    "patient.contactpreference_lab_phone",
                    "patient.consenttotext",
                    "patient.contactpreference_billing_sms",
                    "patient.contactpreference_appointment_phone",
                    "patient.hasmobile",
                    "patient.contactpreference_announcement_email",
                    "patient.patientphoto",
                    "patient.consenttocall",
                    "patient.contactpreference_billing_email",
                    "patient.donotcall",
                    "patient.portalaccessgiven",
                    "patient.driverslicense",
                    "patient.contactpreference_appointment_email",
                    "patient.homebound",
                    "patient.contactpreference_appointment_sms",
                    "patient.contactpreference_billing_phone",
                    "patient.contactpreference_announcement_phone",
                    "patient.contactpreference_lab_sms",
                    "patient.guarantoraddresssameaspatient",
                    "patient.portaltermsonfile",
                    "patient.privacyinformationverified",
                    "patient.contactpreference_lab_email",
                    "patient.contactpreference_announcement_sms",
                    "patient.emailexists",
                };
                
                var properties = new List<Property>();

                foreach (var staticProperty in staticSchemaProperties)
                {
                    var property = new Property();

                    property.Id = staticProperty;
                    property.Name = staticProperty;

                    switch (staticProperty)
                    {
                        case ("appointmentid"):
                            property.IsKey = true;
                            property.Type = PropertyType.String;
                            property.TypeAtSource = "string";
                            break;
                        //bools
                        case ("chargeentrynotrequired"):
                        case ("coordinatorenterprise"):
                        case ("patient.contactpreference_lab_phone"):
                        case ("patient.consenttotext"):
                        case ("patient.contactpreference_billing_sms"):
                        case ("patient.contactpreference_appointment_phone"):
                        case ("patient.hasmobile"):
                        case ("patient.contactpreference_announcement_email"):
                        case ("patient.patientphoto"):
                        case ("patient.consenttocall"):
                        case ("patient.contactpreference_billing_email"):
                        case ("patient.donotcall"):
                        case ("patient.portalaccessgiven"):
                        case ("patient.driverslicense"):
                        case ("patient.contactpreference_appointment_email"):
                        case ("patient.homebound"):
                        case ("patient.contactpreference_appointment_sms"):
                        case ("patient.contactpreference_billing_phone"):
                        case ("patient.contactpreference_announcement_phone"):
                        case ("patient.contactpreference_lab_sms"):
                        case ("patient.guarantoraddresssameaspatient"):
                        case ("patient.portaltermsonfile"):
                        case ("patient.privacyinformationverified"):
                        case ("patient.contactpreference_lab_email"):
                        case ("patient.contactpreference_announcement_sms"):
                        case ("patient.emailexists"): 
                            property.IsKey = false;
                            property.Type = PropertyType.Bool;
                            property.TypeAtSource = "boolean";
                            break;
                        //ints
                        case ("duration"):
                        case ("hl7providerid"):
                        case ("appointmentcopay.collectedforother"):
                        case ("appointmentcopay.collectedforappointment"):
                        case ("appointmentcopay.insurancecopay"):
                        case ("copay"):
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

                var startDate = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                var endDate = DateTime.Today.ToString("MM/dd/yyyy");
                
                foreach (var department in departmentResponse.Departments)
                {
                    var thisDepartmentId = department.DepartmentId ?? "";

                    var bookedApptsPath =
                        $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/appointments/booked?startdate={startDate}&enddate={endDate}&departmentid={thisDepartmentId}";
                    
                    var hasMore = false;

                    do
                    {
                        
                        var bookedApptsResult = await apiClient.GetAsync(bookedApptsPath);

                        if (!bookedApptsResult.IsSuccessStatusCode)
                        {
                            try
                            {
                                var errorResponse =
                                    JsonConvert.DeserializeObject<Error>(
                                        await bookedApptsResult.Content.ReadAsStringAsync());
                                    
                                throw new Exception(string.IsNullOrWhiteSpace(errorResponse.ErrorMessage) ? 
                                    bookedApptsResult.Content.ReadAsStringAsync().ToString() : errorResponse.ErrorMessage);
                            }
                            catch
                            {
                                throw new Exception(bookedApptsResult.Content.ReadAsStringAsync().ToString());
                            }
                            
                        }
                    
                        var bookedApptResponse = JsonConvert.DeserializeObject<BookedAppointmentResponse>(
                            await bookedApptsResult.Content.ReadAsStringAsync());
                        
                        if (bookedApptResponse.Appointments != null)
                        {
                           foreach (var bookedAppointment in bookedApptResponse.Appointments)
                            {
                                var recordMap = new Dictionary<string, object>();

                                //keys
                                recordMap["appointmentid"] = bookedAppointment.AppointmentId ?? "0";
                                
                                //bools
                                recordMap["coordinatorenterprise"] = bookedAppointment.CoordinatorEnterprise;
                                recordMap["chargeentrynotrequired"] = bookedAppointment.ChargeEntryNotRequired;
                                
                                //ints
                                recordMap["duration"] = bookedAppointment.Duration;
                                recordMap["hl7providerid"] = bookedAppointment.Hl7ProviderId;
                                recordMap["copay"] = bookedAppointment.Copay;
                                
                                if (bookedAppointment.AppointmentCopay != null)
                                {
                                    recordMap["appointmentcopay.collectedforother"] = bookedAppointment.AppointmentCopay.CollectedForOther;
                                    recordMap["appointmentcopay.collectedforappointment"] = bookedAppointment.AppointmentCopay.CollectedForAppointment;
                                    recordMap["appointmentcopay.insurancecopay"] = bookedAppointment.AppointmentCopay.InsuranceCopay;
                                }
                                
                                //strings
                                recordMap["date"] = bookedAppointment.Date ?? "";
                                recordMap["starttime"] = bookedAppointment.StartTime ?? "";
                                recordMap["departmentid"] = bookedAppointment.DepartmentId;
                                recordMap["appointmentstatus"] = bookedAppointment.AppointmentStatus ?? "";
                                recordMap["scheduledby"] = bookedAppointment.ScheduledBy ?? "";
                                recordMap["patientid"] = bookedAppointment.PatientId;
                                recordMap["templateappointmenttypeid"] = bookedAppointment.TemplateAppointmentTypeId;
                                recordMap["lastmodifiedby"] = bookedAppointment.LastModifiedBy ?? "";
                                recordMap["appointmenttypeid"] = bookedAppointment.AppointmentTypeId;
                                recordMap["lastmodified"] = bookedAppointment.LastModified ?? "";
                                recordMap["appointmenttype"] = bookedAppointment.AppointmentType ?? "";
                                recordMap["providerid"] = bookedAppointment.ProviderId;
                                recordMap["scheduleddatetime"] = bookedAppointment.ScheduledDateTime ?? "";
                                recordMap["templateappointmentid"] = bookedAppointment.TemplateAppointmentId;
                                recordMap["patientappointmenttypename"] = bookedAppointment.PatientAppointmentTypeName ?? "";
                                    
                                //Patient query
                                
                                var patientPath = $"{settings.GetBaseUrl().TrimEnd('/')}/{settings.PracticeId}/patients/{bookedAppointment.PatientId.ToString()}";
                                
                                var patientResult = await apiClient.GetAsync(patientPath);
                                
                                if (!patientResult.IsSuccessStatusCode)
                                {
                                    throw new Exception(patientResult.Content.ReadAsStringAsync().ToString());
                                }
                                var patientResponses =
                                    JsonConvert.DeserializeObject<List<PatientResponse>>(
                                        await patientResult.Content.ReadAsStringAsync());

                                if (patientResponses[0] == null)
                                {
                                    yield return new Record
                                    {
                                        Action = Record.Types.Action.Upsert,
                                        DataJson = JsonConvert.SerializeObject(recordMap)
                                    };
                                }

                                var patientResponse = patientResponses[0]; //patient received as a list of one patient object
                                
                                //strings
                                recordMap["patient.racename"] = patientResponse.Racename ?? "";
                                recordMap["patient.email"] = patientResponse.Email ?? "";
                                recordMap["patient.homephone"] = patientResponse.Homephone ?? "";
                                recordMap["patient.guarantorstate"] = patientResponse.Guarantorstate ?? "";
                                recordMap["patient.preferredpronouns"] = patientResponse.Preferredpronouns ?? "";
                                recordMap["patient.ethnicitycode"] = patientResponse.Ethnicitycode ?? "";
                                recordMap["patient.contactpreference"] = patientResponse.Contactpreference ?? "";
                                recordMap["patient.guarantordob"] = patientResponse.Guarantordob ?? "";
                                recordMap["patient.zip"] = patientResponse.Zip ?? "";
                                recordMap["patient.status"] = patientResponse.Status ?? "";
                                recordMap["patient.lastname"] = patientResponse.Lastname ?? "";
                                recordMap["patient.guarantorfirstname"] = patientResponse.Guarantorfirstname ?? "";
                                recordMap["patient.city"] = patientResponse.City ?? "";
                                recordMap["patient.lastappointment"] = patientResponse.Lastappointment ?? "";
                                recordMap["patient.genderidentity"] = patientResponse.Genderidentity ?? "";
                                recordMap["patient.guarantoremail"] = patientResponse.Guarantoremail ?? "";
                                recordMap["patient.guarantorcity"] = patientResponse.Guarantorcity ?? "";
                                recordMap["patient.guarantorzip"] = patientResponse.Guarantorzip ?? "";
                                recordMap["patient.sex"] = patientResponse.Sex ?? "";
                                recordMap["patient.primarydepartmentid"] = patientResponse.Primarydepartmentid ?? "";
                                recordMap["patient.firstappointment"] = patientResponse.Firstappointment ?? "";
                                recordMap["patient.language6392code"] = patientResponse.Language6392Code ?? "";
                                recordMap["patient.primaryproviderid"] = patientResponse.Primaryproviderid ?? "";
                                recordMap["patient.patientphotourl"] = patientResponse.Patientphotourl ?? "";
                                recordMap["patient.mobilephone"] = patientResponse.Mobilephone ?? "";
                                recordMap["patient.registrationdate"] = patientResponse.Registrationdate ?? "";
                                recordMap["patient.caresummarydeliverypreference"] = patientResponse.Caresummarydeliverypreference ?? "";
                                recordMap["patient.guarantorlastname"] = patientResponse.Guarantorlastname ?? "";
                                recordMap["patient.firstname"] = patientResponse.Firstname ?? "";
                                recordMap["patient.guarantorcountrycode"] = patientResponse.Guarantorcountrycode ?? "";
                                recordMap["patient.racecode"] = patientResponse.Racecode ?? "";
                                recordMap["patient.state"] = patientResponse.State ?? "";
                                recordMap["patient.dob"] = patientResponse.Dob ?? "";
                                recordMap["patient.patientid"] = patientResponse.Patientid ?? "";
                                recordMap["patient.guarantorrelationshiptopatient"] = patientResponse.Guarantorrelationshiptopatient ?? "";
                                recordMap["patient.address1"] = patientResponse.Address1 ?? "";
                                recordMap["patient.guarantorphone"] = patientResponse.Guarantorphone ?? "";
                                recordMap["patient.driverslicenseurl"] = patientResponse.Driverslicenseurl ?? "";
                                recordMap["patient.maritalstatus"] = patientResponse.Maritalstatus ?? "";
                                recordMap["patient.countrycode"] = patientResponse.Countrycode ?? "";
                                recordMap["patient.guarantoraddress1"] = patientResponse.Guarantoraddress1 ?? "";
                                recordMap["patient.maritalstatusname"] = patientResponse.Maritalstatusname ?? "";
                                recordMap["patient.countrycode3166"] = patientResponse.Countrycode3166 ?? "";
                                recordMap["patient.guarantorcountrycode3166"] = patientResponse.Guarantorcountrycode3166 ?? "";
                                recordMap["patient.race"] = string.Join(',', patientResponse.Race) ?? "";
                                
                                //bools
                                recordMap["patient.contactpreference_lab_phone"] =
                                    patientResponse.ContactpreferenceLabPhone;
                                recordMap["patient.consenttotext"] = patientResponse.Consenttotext;
                                recordMap["patient.contactpreference_billing_sms"] =
                                    patientResponse.ContactpreferenceBillingSms;
                                recordMap["patient.contactpreference_appointment_phone"] =
                                    patientResponse.ContactpreferenceAppointmentPhone;
                                recordMap["patient.hasmobile"] = patientResponse.Hasmobile;
                                recordMap["patient.contactpreference_announcement_email"] =
                                    patientResponse.ContactpreferenceAnnouncementEmail;
                                recordMap["patient.patientphoto"] = patientResponse.Patientphoto;
                                recordMap["patient.consenttocall"] = patientResponse.Consenttocall;
                                recordMap["patient.contactpreference_billing_email"] =
                                    patientResponse.ContactpreferenceBillingEmail;
                                recordMap["patient.donotcall"] = patientResponse.Donotcall;
                                recordMap["patient.portalaccessgiven"] = patientResponse.Portalaccessgiven;
                                recordMap["patient.driverslicense"] = patientResponse.Driverslicense;
                                recordMap["patient.contactpreference_appointment_email"] =
                                    patientResponse.ContactpreferenceAppointmentEmail;
                                recordMap["patient.homebound"] = patientResponse.Homebound;
                                recordMap["patient.contactpreference_appointment_sms"] =
                                    patientResponse.ContactpreferenceAppointmentSms;
                                recordMap["patient.contactpreference_billing_phone"] =
                                    patientResponse.ContactpreferenceBillingPhone;
                                recordMap["patient.contactpreference_announcement_phone"] =
                                    patientResponse.ContactpreferenceAnnouncementPhone;
                                recordMap["patient.contactpreference_lab_sms"] =
                                    patientResponse.ContactpreferenceLabSms;
                                recordMap["patient.guarantoraddresssameaspatient"] =
                                    patientResponse.Guarantoraddresssameaspatient;
                                recordMap["patient.portaltermsonfile"] = patientResponse.Portaltermsonfile;
                                recordMap["patient.privacyinformationverified"] =
                                    patientResponse.Privacyinformationverified;
                                recordMap["patient.contactpreference_lab_email"] = patientResponse.ContactpreferenceLabEmail;
                                recordMap["patient.contactpreference_announcement_sms"] =
                                    patientResponse.ContactpreferenceAnnouncementSms;
                                recordMap["patient.emailexists"] = patientResponse.Emailexists;
                                
                                yield return new Record
                                {
                                    Action = Record.Types.Action.Upsert,
                                    DataJson = JsonConvert.SerializeObject(recordMap)
                                }; 
                            }  
                        }
                        
                        if (string.IsNullOrWhiteSpace(bookedApptResponse.Next))
                        {
                            hasMore = false;
                        }
                        else
                        {
                            var nextUri =
                                new Uri($"{settings.GetBaseUrl(false).TrimEnd('/')}{bookedApptResponse.Next}");

                            var nextOffset = HttpUtility.ParseQueryString(nextUri.Query).Get("offset");

                            if (Int32.Parse(nextOffset) >= Int32.Parse(bookedApptResponse.Totalcount.ToString()))
                            {
                                hasMore = false;
                            }
                            else
                            {
                                hasMore = true;
                                bookedApptsPath = nextUri.ToString();
                            }
                        }
                    } while (hasMore);
                }
            }
        }

        public static readonly Dictionary<string, Endpoint> BookedAppointmentsEndpoints = new Dictionary<string, Endpoint>
        {
            {
                "BookedAppointments_Today", new BookedAppointmentsEndpoint_Today
                {
                    Id = "BookedAppointments_Today",
                    Name = "BookedAppointments_Today",
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
            },
            {
                "BookedAppointments_Yesterday", new BookedAppointmentsEndpoint_Yesterday
                {
                    Id = "BookedAppointments_Yesterday",
                    Name = "BookedAppointments_Yesterday",
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
            },
            {
                "BookedAppointments_Historical", new BookedAppointmentsEndpoint_Historical
                {
                    Id = "BookedAppointments_Historical",
                    Name = "BookedAppointments_Historical",
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
            },
            {
                "BookedAppointments_Last7Days", new BookedAppointmentsEndpoint_Last7Days
                {
                    Id = "BookedAppointments_Last7Days",
                    Name = "BookedAppointments_Last7Days",
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