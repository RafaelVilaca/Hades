using Hades.Application.Interface;
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

        public HttpResponseMessage Participar(int idSorteio, int idUsuario)
        {
            return _httpClient.PostAsync($"http://localhost:2002/api/sorteioParticipante/{idSorteio}/{idUsuario}", new {}, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetAll(int id)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/sorteioParticipante/{id}").Result;
        }

        public HttpResponseMessage DeletarParticipantesSorteio(int idSorteio, int idUsuario)
        {
            return _httpClient.DeleteAsync($"http://localhost:2002/api/sorteioParticipante/{idSorteio}/{idUsuario}").Result;
        }

        public HttpResponseMessage VencedorParticipantesSorteio(int idSorteio, int idUsuario)
        {
            return _httpClient.PutAsync($"http://localhost:2002/api/sorteioParticipante/{idSorteio}/{idUsuario}", new {}, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage SortearNovamente(int idSorteio)
        {
            return _httpClient.PutAsync($"http://localhost:2002/api/sorteioParticipante/SortearNovamente/{idSorteio}", new { }, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetVencedores(int idSorteio)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/sorteioParticipante/Vencedores/{idSorteio}").Result;
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
