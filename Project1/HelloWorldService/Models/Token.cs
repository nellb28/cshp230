using Newtonsoft.Json;

namespace HelloWorldService.Models
{
    public class Token
    {
        [JsonProperty("token")]
        public string TokenString { get; set; }
    }
}
