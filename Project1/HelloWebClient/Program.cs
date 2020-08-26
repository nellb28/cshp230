using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HelloWorldClient
{
    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("phones")]
        public Phone[] Phones { get; set; }
    }

    public class Phone
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("phone_type")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PhoneType PhoneType { get; set; }
    }

    public enum PhoneType
    {
        Home,
        Mobile,
    }

    public class Token
    {
        [JsonProperty("token")]
        public string TokenString { get; set; }
    }

    class Program
    {
        static async void DoStuff()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost.fiddler:58835/api/");

            var tokenResult = await client.GetAsync("token?userName=dave&password=pass");
            var tokenjson = await tokenResult.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<Token>(tokenjson);

            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.TokenString);
            // OR
            var authHeader = new AuthenticationHeaderValue("Bearer", token.TokenString);
            client.DefaultRequestHeaders.Authorization = authHeader;

            var newContact = new Contact
            {
                Name = "New Name",
                Phones = new[] {
                    new Phone {
                        Number = "425-111-2222",
                        PhoneType = PhoneType.Mobile
                    }
                }
            };

            var newJson = JsonConvert.SerializeObject(newContact);

            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("contacts", postContent);

            Console.WriteLine(postResult.StatusCode);

            var postjson = await postResult.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Contact>(postjson);

            var result = await client.GetAsync("contacts");

            var json = await result.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<Contact[]>(json);

            var id = list[0].Id;
            var deleteResult = await client.DeleteAsync("contacts/" + id);

            Console.WriteLine(json);
        }

        static void Main(string[] args)
        {
            DoStuff();

            Console.ReadLine();
        }
    }
}
