using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using System.Web.Http;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class SorteioParticipanteApiController : ApiController
    {
        private readonly ISorteioParticipanteService _sorteioParticipanteService;

        public SorteioParticipanteApiController(ISorteioParticipanteService sorteioParticipanteService)
        {
            _sorteioParticipanteService = sorteioParticipanteService;
        }

        [HttpGet]
        [Route("sorteioParticipante/id")]
        public IHttpActionResult GetAll(int id)
        {
            var sorteio = _sorteioParticipanteService.GetAll(id);
            if (sorteio != null)
                return Ok(sorteio);
            return BadRequest("Erro ao trazer Lista de Enquetes");
        }

        [HttpPost]
        [Route("sorteioParticipante")]
        public IHttpActionResult Participar(SorteioParticipante sorteioParticipante)
        {
            _sorteioParticipanteService.Participar(sorteioParticipante);
            return Ok();
        }

        [HttpPut]
        [Route("sorteioParticipante/{idSorteio}/{idUsuario}")]
        public IHttpActionResult VencedorParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteService.VencedorParticipantesSorteio(idSorteio, idUsuario);
            return Ok();
        }

        [HttpDelete]
        [Route("sorteioParticipante/{idSorteio}/{idUsuario}")]
        public IHttpActionResult DeletarParticipantesSorteio(int idSorteio, int idUsuario)
        {
            _sorteioParticipanteService.DeletarParticipantesSorteio(idSorteio, idUsuario);
            return Ok();
        }
    }
}
