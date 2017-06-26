﻿using System.Net.Http;
using System.Net.Http.Formatting;
using Hades.Application.Interface;
using Hades.Domain.Entities;
using Newtonsoft.Json;

namespace Hades.Application
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly HttpClient _httpClient;

        public UsuarioAppService()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Post(Usuario usuario)
        {
            return _httpClient.PostAsync("http://localhost:10000/api/usuario", usuario, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage GetById(int id)
        {
            return _httpClient.GetAsync("http://localhost:10000/api/usuario?id=" + id).Result;
        }

        public HttpResponseMessage GetAll()
        {
            return _httpClient.GetAsync("http://localhost:10000/api/usuario").Result;
        }

        public HttpResponseMessage Put(Usuario usuario)
        {
            return _httpClient.PutAsync("http://localhost:10000/api/usuario", usuario, JsonMediaTypeFormatter).Result;
        }

        public HttpResponseMessage StatusUsuario(int id, bool status)
        {
            var url = $"http://localhost:10000/api/usuario/{id}/{status}";

            return _httpClient.PutAsync(url, new {}, JsonMediaTypeFormatter).Result;
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