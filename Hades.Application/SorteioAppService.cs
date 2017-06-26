using System.Net.Http;
using System.Net.Http.Formatting;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class SorteioAppService : ISorteioAppService
    {
        private readonly HttpClient _httpClient;

        public SorteioAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Post(Sorteio sorteio)
        {
            return _httpClient.PostAsync("http://localhost:10000/api/sorteio", sorteio, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetById(int id)
        {
            return _httpClient.GetAsync("http://localhost:10000/api/sorteio?id=" + id).Result;
        }

        public HttpResponseMessage GetAll()
        {
            return _httpClient.GetAsync("http://localhost:10000/api/sorteio").Result;
        }

        public HttpResponseMessage Put(Sorteio sorteio)
        {
            return _httpClient.PutAsync("http://localhost:10000/api/sorteio", sorteio, JsonMediaTypeFormatter).Result;
        }

        public void Delete(int id)
        {
            _httpClient.DeleteAsync($"http://localhost:10000/api/sorteio/{id}");
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
