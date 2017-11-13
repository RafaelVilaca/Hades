using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Hades.Application
{
    public class CampanhaParticipanteAppService : ICampanhaParticipanteAppService
    {
        private readonly HttpClient _httpClient;

        public CampanhaParticipanteAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Delete(int idUsuario, int idCampanha)
        {
            return _httpClient.DeleteAsync($"http://localhost:2002/api/campanhaParticipante/{idUsuario}/{idCampanha}").Result;
        }

        public HttpResponseMessage Get(int idCampanha)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/campanhaParticipante/{idCampanha}").Result;
        }

        public HttpResponseMessage Post(CampanhaDto campanha)
        {
            return _httpClient.PostAsync("http://localhost:2002/api/campanhaParticipante", campanha, JsonMediaTypeFormatter).Result;
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
