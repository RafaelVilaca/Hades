using Hades.Domain.Interfaces.Services;
using System.Web.Http;
using Hades.Domain.Entities;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class CampanhaParticipantesApiController : ApiController
    {
        private readonly ICampanhaParticipanteService _campanhaParticipanteService;

        public CampanhaParticipantesApiController(ICampanhaParticipanteService campanhaParticipanteService)
        {
            _campanhaParticipanteService = campanhaParticipanteService;
        }

        [HttpGet]
        [Route("campanhaParticipante/{idCampanha}")]
        public IHttpActionResult Get(int idCampanha)
        {
            return Ok(_campanhaParticipanteService.Get(idCampanha));
        }

        [HttpPost]
        [Route("campanhaParticipante")]
        public void Post(CampanhaDto campanha) => _campanhaParticipanteService.Post(campanha.IdCriador, campanha.IdCampanha);

        [HttpDelete]
        [Route("campanhaParticipante/{idUsuario}/{idCampanha}")]
        public void Delete(int idUsuario, int idCampanha) => _campanhaParticipanteService.Delete(idUsuario, idCampanha);
    }
}