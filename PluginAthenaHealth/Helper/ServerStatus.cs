using Naveego.Sdk.Plugins;

namespace PluginAthenaHealth.Helper
{
    public static class ServerStatus
    {
        public static ConfigureRequest Config { get; set; }
        public static Settings Settings { get; set; }
        public static bool Connected { get; set; }
        public static WriteSettings WriteSettings { get; set; }
        public static bool WriteConfigured { get; set; }
    }
}