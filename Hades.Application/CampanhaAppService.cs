using Hades.Application.Interface;
using System;
using Hades.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class CampanhaAppService : ICampanhaAppService
    {
        private readonly HttpClient _httpClient;

        public CampanhaAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Delete(int idCampanha)
        {
            return _httpClient.DeleteAsync($"http://localhost:2002/api/campanha/{idCampanha}").Result;
        }

        public HttpResponseMessage GetCampanha(int idCampanha)
        {
            return _httpClient.GetAsync($"http://localhost:2002/api/campanha/{idCampanha}").Result;
        }

        public HttpResponseMessage GetCampanhas()
        {
            return _httpClient.GetAsync("http://localhost:2002/api/campanhas").Result;
        }

        public HttpResponseMessage Post(CampanhaDto campanha)
        {
            return _httpClient.PostAsync("http://localhost:2002/api/campanha", campanha, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage Put(CampanhaDto campanha)
        {
            return _httpClient.PutAsync("http://localhost:2002/api/campanha", campanha, JsonMediaTypeFormatter).Result;
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
