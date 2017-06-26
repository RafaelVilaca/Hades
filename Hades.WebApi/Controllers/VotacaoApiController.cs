using System.Web.Http;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class VotacaoApiController : ApiController
    {
        private readonly IVotacaoService _votacaoService;

        public VotacaoApiController(IVotacaoService votacaoService)
        {
            _votacaoService = votacaoService;
        }

        [HttpPost]
        [Route("voto")]
        // POST: api/UsuarioApi
        public void Post(Votacao votacao)
        {
            _votacaoService.Post(votacao);
        }

        [HttpPut]
        [Route("voto")]
        // PUT: api/UsuarioApi/5
        public void Put(Votacao votacao)
        {
            _votacaoService.Put(votacao);
        }
    }
}
