using System.Net.Http;
using System.Net.Http.Formatting;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class EnqueteAppService : IEnqueteAppService
    {
        private readonly HttpClient _httpClient;

        public EnqueteAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Post(Enquete enquete)
        {
            return _httpClient.PostAsync("http://localhost:10000/api/enquete", enquete, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetById(int id)
        {
            return _httpClient.GetAsync("http://localhost:10000/api/enquete?id=" + id).Result;
        }

        public HttpResponseMessage GetAll()
        {
            return _httpClient.GetAsync("http://localhost:10000/api/enquete").Result;
        }

        public HttpResponseMessage Put(Enquete enquete)
        {
            return _httpClient.PutAsync("http://localhost:10000/api/enquete", enquete, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage StatusEnquete(int id, bool status)
        {
            var url = $"http://localhost:10000/api/enquete/{id}/{status}";

            return _httpClient.PutAsync(url, new { }, JsonMediaTypeFormatter).Result;
        }

        private static readonly JsonMediaTypeFormatter JsonMediaTypeFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Include
            }
        };
    }
}
