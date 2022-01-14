using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using PluginAthenaHealth.DataContracts;

namespace PluginAthenaHealth.DataContracts
{
    public class DepartmentResponse
    {
        [JsonProperty("totalcount")]
        public Int32 TotalCount { get; set; }
        
        [JsonProperty("departments")]
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public Department(string departmentId)
        {
            DepartmentId = departmentId;
        }
        
        [JsonProperty("departmentid")]
        public string DepartmentId { get; set; }
    }

    public class BookedAppointmentResponse
    {
        [JsonProperty("next")]
        public string Next { get; set; }
        
        [JsonProperty("totalcount")]
        public long Totalcount { get; set; }

        [JsonProperty("appointments")]
        public List<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("appointmentid")]
        public string AppointmentId { get; set; }

        [JsonProperty("starttime")]
        public string StartTime { get; set; }

        [JsonProperty("departmentid")]
        public string DepartmentId { get; set; }

        [JsonProperty("appointmentstatus")]
        public string AppointmentStatus { get; set; }

        [JsonProperty("scheduledby")]
        public string ScheduledBy { get; set; }

        [JsonProperty("patientid")]
        public string PatientId { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("templateappointmenttypeid")]
        public string TemplateAppointmentTypeId { get; set; }

        [JsonProperty("hl7providerid")]
        public string Hl7ProviderId { get; set; }

        [JsonProperty("lastmodifiedby")]
        public string LastModifiedBy { get; set; }

        [JsonProperty("appointmentcopay")]
        public AppointmentCopay AppointmentCopay { get; set; }

        [JsonProperty("copay")]
        public string Copay { get; set; }

        [JsonProperty("appointmenttypeid")]
        public string AppointmentTypeId { get; set; }

        [JsonProperty("lastmodified")]
        public string LastModified { get; set; }

        [JsonProperty("appointmenttype")]
        public string AppointmentType { get; set; }

        [JsonProperty("providerid")]
        public string ProviderId { get; set; }

        [JsonProperty("chargeentrynotrequired")]
        public bool ChargeEntryNotRequired { get; set; }

        [JsonProperty("scheduleddatetime")]
        public string ScheduledDateTime { get; set; }

        [JsonProperty("coordinatorenterprise")]
        public bool CoordinatorEnterprise { get; set; }

        [JsonProperty("templateappointmentid")]
        public string TemplateAppointmentId { get; set; }

        [JsonProperty("patientappointmenttypename")]
        public string PatientAppointmentTypeName { get; set; }
    }

    public class AppointmentCopay
    {
        [JsonProperty("collectedforother")]
        public string CollectedForOther { get; set; }
        
        [JsonProperty("collectedforappointment")]
        public string CollectedForAppointment { get; set; }
        
        [JsonProperty("InsuranceCopay")]
        public string InsuranceCopay { get; set; }
    }
    public class ProviderResponse
    {
        [JsonProperty("providers")]
        public List<Provider> Providers { get; set; }
    }

    public class Provider
    {
        [JsonProperty("providerid")]
        public long ProviderId { get; set; }
    }

    public class PatientAppointmentReasons
    {
        [JsonProperty("totalcount")]
        public int TotalCount { get; set; }

    }

    public class WritePatientChart
    {
        [JsonProperty("practiceid")]
        public int PracticeId { get; set; }
        
        [JsonProperty("patientid")]
        public int PatientId { get; set; }
        
        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
        
        [JsonProperty("attachmentcontents")]
        public string AttachmentContents { get; set; }
        
        [JsonProperty("attachmenttype")]
        public string AttachmentType { get; set; }
        
        [JsonProperty("autoclose")]
        public bool AutoClose { get; set; }
        
        [JsonProperty("clinicalproviderid")]
        public int ClinicalProviderId { get; set; }
        
        [JsonProperty("departmentid")]
        public int DepartmentId { get; set; }
        
        [JsonProperty("documentdata")]
        public string DocumentData { get; set; }
        
        [JsonProperty("documentsubclass")]
        public string DocumentSubclass { get; set; }
        
        [JsonProperty("documenttypeid")]
        public int DocumentTypeId { get; set; }
        
        [JsonProperty("entityid")]
        public string EntityId { get; set; }
        
        [JsonProperty("entitytype")]
        public string EntityType { get; set; }
        
        [JsonProperty("internalnote")]
        public string InternalNote { get; set; }
        
        [JsonProperty("observationdate")]
        public string ObservationDate { get; set; }
        
        [JsonProperty("observationtime")]
        public string ObservationTime { get; set; }
        
        [JsonProperty("originalfilename")]
        public string OriginalFileName { get; set; }
        
        [JsonProperty("priority")]
        public string Priority { get; set; }
        
        [JsonProperty("providerid")]
        public int ProviderId { get; set; }
    }

    public class Error
    {
        [JsonProperty("error")]
        public string ErrorMessage { get; set; }
    }
    public class WritePatientChartResponse
    {
        [JsonProperty("clinicaldocumentid")]
        public int ClinicalDocumentId { get; set; }
        
        [JsonProperty("errormessage")]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("success")]
        public string Success { get; set; }
    }

    public class PatientResponseWrapper
    {
        [JsonProperty("next")]
        public string Next { get; set; }
        
        [JsonProperty("patients")]
        public List<PatientResponse> Patients { get; set; }
        
        [JsonProperty("totalcount")]
        public int TotalCount { get; set; }
    }
    public class PatientResponse
    {
        [JsonProperty("racename")]
        public string Racename { get; set; }

        [JsonProperty("donotcall")]
        public bool Donotcall { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("departmentid")]
        public long Departmentid { get; set; }

        [JsonProperty("homephone")]
        public string Homephone { get; set; }

        [JsonProperty("guarantorstate")]
        public string Guarantorstate { get; set; }

        [JsonProperty("portalaccessgiven")]
        public bool Portalaccessgiven { get; set; }

        [JsonProperty("driverslicense")]
        public bool Driverslicense { get; set; }

        [JsonProperty("contactpreference_appointment_email")]
        public bool ContactpreferenceAppointmentEmail { get; set; }

        [JsonProperty("homebound")]
        public bool Homebound { get; set; }

        [JsonProperty("contactpreference_appointment_sms")]
        public bool ContactpreferenceAppointmentSms { get; set; }

        [JsonProperty("preferredpronouns")]
        public string Preferredpronouns { get; set; }

        [JsonProperty("contactpreference_billing_phone")]
        public bool ContactpreferenceBillingPhone { get; set; }

        [JsonProperty("ethnicitycode")]
        public string Ethnicitycode { get; set; }

        [JsonProperty("contactpreference_announcement_phone")]
        public bool ContactpreferenceAnnouncementPhone { get; set; }

        [JsonProperty("contactpreference")]
        public string Contactpreference { get; set; }

        [JsonProperty("contactpreference_lab_sms")]
        public bool ContactpreferenceLabSms { get; set; }

        [JsonProperty("guarantordob")]
        public string Guarantordob { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("guarantoraddresssameaspatient")]
        public bool Guarantoraddresssameaspatient { get; set; }

        [JsonProperty("portaltermsonfile")]
        public bool Portaltermsonfile { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("guarantorfirstname")]
        public string Guarantorfirstname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("lastappointment")]
        public string Lastappointment { get; set; }

        [JsonProperty("genderidentity")]
        public string Genderidentity { get; set; }

        [JsonProperty("guarantoremail")]
        public string Guarantoremail { get; set; }

        [JsonProperty("guarantorcity")]
        public string Guarantorcity { get; set; }

        [JsonProperty("guarantorzip")]
        public string Guarantorzip { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("privacyinformationverified")]
        public bool Privacyinformationverified { get; set; }

        [JsonProperty("primarydepartmentid")]
        public string Primarydepartmentid { get; set; }

        [JsonProperty("contactpreference_lab_email")]
        public bool ContactpreferenceLabEmail { get; set; }

        [JsonProperty("balances")]
        public List<Balance> Balances { get; set; }

        [JsonProperty("contactpreference_announcement_sms")]
        public bool ContactpreferenceAnnouncementSms { get; set; }

        [JsonProperty("emailexists")]
        public bool Emailexists { get; set; }

        [JsonProperty("race")]
        public List<string> Race { get; set; }

        [JsonProperty("firstappointment")]
        public string Firstappointment { get; set; }

        [JsonProperty("language6392code")]
        public string Language6392Code { get; set; }

        [JsonProperty("primaryproviderid")]
        public string Primaryproviderid { get; set; }

        [JsonProperty("patientphoto")]
        public bool Patientphoto { get; set; }

        [JsonProperty("consenttocall")]
        public bool Consenttocall { get; set; }

        [JsonProperty("contactpreference_billing_email")]
        public bool ContactpreferenceBillingEmail { get; set; }

        [JsonProperty("patientphotourl")]
        public string Patientphotourl { get; set; }

        [JsonProperty("mobilephone")]
        public string Mobilephone { get; set; }

        [JsonProperty("contactpreference_announcement_email")]
        public bool ContactpreferenceAnnouncementEmail { get; set; }

        [JsonProperty("hasmobile")]
        public bool Hasmobile { get; set; }

        [JsonProperty("registrationdate")]
        public string Registrationdate { get; set; }

        [JsonProperty("caresummarydeliverypreference")]
        public string Caresummarydeliverypreference { get; set; }

        [JsonProperty("guarantorlastname")]
        public string Guarantorlastname { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("guarantorcountrycode")]
        public string Guarantorcountrycode { get; set; }

        [JsonProperty("racecode")]
        public string Racecode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("contactpreference_appointment_phone")]
        public bool ContactpreferenceAppointmentPhone { get; set; }

        [JsonProperty("patientid")]
        public string Patientid { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("guarantorrelationshiptopatient")]
        public string Guarantorrelationshiptopatient { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("contactpreference_billing_sms")]
        public bool ContactpreferenceBillingSms { get; set; }

        [JsonProperty("guarantorphone")]
        public string Guarantorphone { get; set; }

        [JsonProperty("driverslicenseurl")]
        public string Driverslicenseurl { get; set; }

        [JsonProperty("maritalstatus")]
        public string Maritalstatus { get; set; }

        [JsonProperty("countrycode")]
        public string Countrycode { get; set; }

        [JsonProperty("guarantoraddress1")]
        public string Guarantoraddress1 { get; set; }

        [JsonProperty("maritalstatusname")]
        public string Maritalstatusname { get; set; }

        [JsonProperty("consenttotext")]
        public bool Consenttotext { get; set; }

        [JsonProperty("countrycode3166")]
        public string Countrycode3166 { get; set; }

        [JsonProperty("contactpreference_lab_phone")]
        public bool ContactpreferenceLabPhone { get; set; }

        [JsonProperty("guarantorcountrycode3166")]
        public string Guarantorcountrycode3166 { get; set; }
    }
    public class Balance
    {
        [JsonProperty("balance")]
        public string BalanceBalance { get; set; }

        [JsonProperty("departmentlist")]
        public string Departmentlist { get; set; }

        [JsonProperty("providergroupid")]
        public string Providergroupid { get; set; }

        [JsonProperty("cleanbalance")]
        public bool Cleanbalance { get; set; }
    }

    //old below
    public class ObjectResponseWrapper
    {
        [JsonProperty("results")]
        public List<ObjectResponse> Results { get; set; }
        
        [JsonProperty("paging")]
        public PagingResponse Paging { get; set; }
    }

    public class ObjectResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; }
    }


    public class PropertyResponseWrapper
    {
        [JsonProperty("results")]
        public List<PropertyResponse> Results { get; set; }
        
        [JsonProperty("paging")]
        public PagingResponse Paging { get; set; }
    }

    public class PropertyResponse
    {
        [JsonProperty("name")]
        public string Id { get; set; }
        
        [JsonProperty("label")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("hasUniqueValue")]
        public bool IsKey { get; set; }
        
        [JsonProperty("calculated")]
        public bool Calculated { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("modificationMetadata")]
        public ModificationMetaData ModificationMetaData { get; set; }
    }

    public class PagingResponse
    {
        [JsonProperty("next")]
        public NextResponse Next { get; set; }
    }

    public class NextResponse
    {
        [JsonProperty("after")]
        public string After { get; set; }
        
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}