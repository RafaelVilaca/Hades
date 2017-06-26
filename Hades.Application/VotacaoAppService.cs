using System.Net.Http;
using System.Net.Http.Formatting;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class VotacaoAppService : IVotacaoAppService
    {
        private readonly HttpClient _httpClient;

        public VotacaoAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Put(Votacao votacao)
        {
            return _httpClient.PutAsync("http://localhost:10000/api/votacao", votacao, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage Post(Votacao votacao)
        {
            return _httpClient.PostAsync("http://localhost:10000/api/votacao", votacao, JsonMediaTypeFormatter).Result;
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
