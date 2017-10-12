using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

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
            return _httpClient.PostAsync("http://localhost:5002/api/enquete", enquete, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetById(int id)
        {
            return _httpClient.GetAsync("http://localhost:5002/api/enquete?id=" + id).Result;
        }

        public HttpResponseMessage GetAll()
        {
            return _httpClient.GetAsync("http://localhost:5002/api/enquete").Result;
        }

        public HttpResponseMessage Put(Enquete enquete)
        {
            return _httpClient.PutAsync("http://localhost:5002/api/enquete", enquete, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage StatusEnquete(int id)
        {
            var url = $"http://localhost:5002/api/enquete/{id}";
            return _httpClient.PutAsync(url, new {}, JsonMediaTypeFormatter).Result;
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
