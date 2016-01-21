namespace Fluffy.Common.Entities
{
    using Newtonsoft.Json;

    public class Country
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "continent")]
        public string Continent { get; set; }
    }
}