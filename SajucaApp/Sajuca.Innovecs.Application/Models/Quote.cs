using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;


namespace Sajuca.Innovecs.Application.Models
{
    public class Quote
    {
        [JsonProperty(Required = Required.Always)]
        public string ContactAddress { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string WarehouseAddress { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Dictionary<string, object> PackageDimension { get; set; }
    }
}
