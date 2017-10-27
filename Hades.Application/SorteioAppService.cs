using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

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
            return _httpClient.PostAsync("http://localhost:2002/api/sorteio", sorteio, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetById(int id)
        {
            return _httpClient.GetAsync("http://localhost:2002/api/sorteio?id=" + id).Result;
        }

        public HttpResponseMessage GetAll(int idUsuario)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/sorteios/{idUsuario}").Result;
        }

        public HttpResponseMessage Put(Sorteio sorteio)
        {
            return _httpClient.PutAsync("http://localhost:2002/api/sorteio", sorteio, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage Delete(int id)
        {
            return _httpClient.DeleteAsync($"http://localhost:2002/api/sorteio/{id}").Result;
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
