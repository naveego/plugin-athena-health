namespace PluginAthenaHealth.API.Utility
{
    public static class Constants
    {
        public static string BaseApiUrl = "https://api.platform.athenahealth.com/v1/";
        public static string TestConnectionPath = "/appointments/report?enddate=11-07-2021&startdate=11-01-2021&showexpectedprocedurecodes=false";
        public static string CustomProperty = "CustomProperty";
        public static string EmptySchemaDescription = "This schema has no properties. This is likely due to to there being no data.";
    }
}