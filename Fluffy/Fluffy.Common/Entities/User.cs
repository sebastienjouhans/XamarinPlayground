namespace Fluffy.Common.Entities
{
    using Newtonsoft.Json;

    public class User
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "thumbImage")]
        public string ThumbImage { get; set; }
    }
}