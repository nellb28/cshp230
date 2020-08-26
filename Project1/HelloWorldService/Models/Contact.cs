using Newtonsoft.Json;
using System;

namespace HelloWorldService.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }
        
        public Phone[] Phones { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Number { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("phone_type", DefaultValueHandling = DefaultValueHandling.Ignore)] 
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Nil = 0,
        Home =1,
        Mobile =2,
    }
}