using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

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
            _httpClient.PostAsync("http://localhost:5002/api/sorteioParticipante", sorteioParticipante, JsonMediaTypeFormatter);
        }

        public HttpResponseMessage GetAll(int id)
        {
            return _httpClient.GetAsync("http://localhost:5002/api/sorteioParticipante?id=" + id).Result;
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
