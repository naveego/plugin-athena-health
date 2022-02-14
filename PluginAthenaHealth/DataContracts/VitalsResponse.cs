using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluginAthenaHealth.DataContracts
{
    public class VitalsResponse
    {
        [JsonProperty("vitals")]
        public List<Vital> Vitals { get; set; }
    }

    public class Vital
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        
        [JsonProperty("readings")]
        public List<List<Reading>> Readings { get; set; }
    }

    public class Reading
    {
        [JsonProperty("clinicalelementid")]
        public string Key { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
        
        [JsonProperty("unit")]
        public string Unit { get; set; }
        
        [JsonProperty("readingtaken")]
        public DateTime Date { get; set; }
    }
}