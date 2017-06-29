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
    }
}
