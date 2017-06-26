using System.Net.Http;
using System.Net.Http.Formatting;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class SorteioParticipanteAppService : ISorteioParticipanteAppService
    {
        private readonly HttpClient _httpClient;

        public SorteioParticipanteAppService()
        {
            _httpClient = new HttpClient();
        }

        public void Participar(SorteioParticipante sorteioParticipante)
        {
            _httpClient.PostAsync("http://localhost:10000/api/sorteioParticipante", sorteioParticipante, JsonMediaTypeFormatter);
        }

        public HttpResponseMessage GetAll(int id)
        {
            return _httpClient.GetAsync("http://localhost:10000/api/sorteioParticipante").Result;
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
