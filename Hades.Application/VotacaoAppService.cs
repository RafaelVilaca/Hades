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
            return _httpClient.PostAsync("http://localhost:5002/api/votacao", votacao, JsonMediaTypeFormatter).Result;
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
