using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Hades.Application
{
    public class VotacaoAppService : IVotacaoAppService
    {
        private readonly HttpClient _httpClient;

        public VotacaoAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Post(Votacao votacao)
        {
            return _httpClient.PostAsync("http://localhost:2002/api/voto", votacao, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage Put(Votacao votacao)
        {
            return _httpClient.PutAsync("http://localhost:2002/api/voto", votacao, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetVotos(int? idUsuario, int? idEnquete)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/voto/{idUsuario}/{idEnquete}").Result;
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
