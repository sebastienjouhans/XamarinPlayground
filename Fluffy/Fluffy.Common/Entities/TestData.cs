namespace Fluffy.Common.Entities
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class TestData
    {

        [JsonProperty(PropertyName = "users")]
        public List<User> Users { get; set; }

        [JsonProperty(PropertyName = "countries")]
        public List<Country> Countries { get; set; }
    }
}