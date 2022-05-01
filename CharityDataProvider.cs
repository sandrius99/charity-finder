using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Charity
{
    public class CharityDataProvider : ICharityDataProvider
    {
        private IRestClient _client = new RestClient($"http://data.orghunter.com/v1/");
        private string _key = ""; // add key here to be able to make requests
        public List<Category> GetCharityCategories()
        {
            var request = new RestRequest($"categories?user_key={_key}");
            IRestResponse response = _client.Execute(request);
            string convertedResponse = ConvertResponse(response.Content);
            return JsonConvert.DeserializeObject<List<Category>>(convertedResponse, settings());
            
        }

        public IEnumerable<Organization> GetOrganizations(string category, string city = "")
        {
            
            var request = new RestRequest($"charitysearch?user_key={_key}&category={category}&city={city}&rows=80");
            IRestResponse response = _client.Execute(request);
            string convertedResponse = ConvertResponse(response.Content).Substring(2);
            Console.WriteLine(convertedResponse);
            return JsonConvert.DeserializeObject<IEnumerable<Organization>>(convertedResponse, settings());

        }

        public OrganizationDetails GetOrganizationDetails(string ein)
        {
            var request = new RestRequest($"charitybasic?user_key={_key}&ein={ein}");
            IRestResponse response = _client.Execute(request);
            string convertedResponse = ConvertResponse(response.Content);

            return JsonConvert.DeserializeObject<OrganizationDetails>(convertedResponse, settings());

        }

        private string ConvertResponse(string response)
        {
            string response1 = response.Substring(49);
           return   response1.Remove(response1.LastIndexOf('}'));
        }

        private JsonSerializerSettings settings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            return settings;
        }
    }
}
