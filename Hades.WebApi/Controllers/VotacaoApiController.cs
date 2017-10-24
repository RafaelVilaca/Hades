using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using System.Web.Http;

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
        // POST: api/UsuarioApi
        public void Put(Votacao votacao)
        {
            _votacaoService.Put(votacao);
        }

        [HttpGet]
        [Route("voto/{IdUsuario}/{IdEnquete}")]
        public IHttpActionResult GetVotos(int idUsuario, int idEnquete)
        {
            return Ok(_votacaoService.GetVotos(idUsuario, idEnquete));
        }
    }
}
