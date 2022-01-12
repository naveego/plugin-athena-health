namespace PluginAthenaHealth.API.Utility
{
    public static class Constants
    {
        public static string TestConnectionPath = "/ping";
        public static string EmptySchemaDescription = "This schema has no properties. This is likely due to to there being no data.";

        public static string GoogleCloudStorage = "GoogleCloudStorage";
        public static string Local = "Local";
        
        public const string MethodGet = "GET";
        public const string MethodPost = "POST";
        public const string MethodPut = "PUT";
        public const string MethodPatch = "PATCH";
        public const string MethodDelete = "DELETE";
        
        public const string UrlPropertyPrefix = "URL";
        public const string BodyPropertyPrefix = "BODY";
    }
}