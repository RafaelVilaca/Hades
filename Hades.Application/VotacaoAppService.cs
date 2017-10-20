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
            return _httpClient.PostAsync("http://localhost:2002/api/votacao", votacao, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetVoto(Votacao votacao)
        {
            return _httpClient.GetAsync("http://localhost:2002/api/votacao/{votacao}").Result;
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
