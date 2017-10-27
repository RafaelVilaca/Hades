using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using Hades.Infra.Data.Repositories;
using System.Web.Http;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class SorteioApiController : ApiController
    {
        private readonly ISorteioService _sorteioService;

        public SorteioApiController(ISorteioService sorteioService)
        {
            _sorteioService = sorteioService;
        }

        [HttpGet]
        [Route("sorteios/{idUsuario}")]
        public IHttpActionResult GetAll(int idUsuario)
        {
            var sorteios = _sorteioService.GetAll(idUsuario);
            if (sorteios != null)
                return Ok(sorteios);
            return BadRequest("Erro ao trazer Sorteios");
        }

        [HttpGet]
        [Route("sorteio")]
        public IHttpActionResult GetById(int id)
        {
            var sorteio = _sorteioService.GetById(id);
            if (sorteio != null)
                return Ok(sorteio);
            return BadRequest("Erro ao trazer Sorteios");
        }

        [HttpDelete]
        [Route("sorteio/{id}")]
        public IHttpActionResult Delete(int idSorteio, int idUsuario)
        {
            var sorteioParticipante = new SorteioParticipanteRepository();
            sorteioParticipante.DeletarParticipantesSorteio(idSorteio, idUsuario);

            _sorteioService.Delete(idSorteio);
            return Ok();
        }

        [HttpPost]
        [Route("sorteio")]
        public IHttpActionResult Post(Sorteio sorteio)
        {
            _sorteioService.Post(sorteio);
            return Ok();
        }

        [HttpPut]
        [Route("sorteio")]
        public IHttpActionResult Put(Sorteio sorteio)
        {
            _sorteioService.Put(sorteio);
            return Ok();
        }
    }
}
