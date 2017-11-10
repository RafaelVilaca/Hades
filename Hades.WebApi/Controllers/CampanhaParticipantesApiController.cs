using Hades.Domain.Interfaces.Services;
using System.Web.Http;

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
        // GET: api/EnqueteApi
        public IHttpActionResult Get(int idCampanha)
        {
            return Ok(_campanhaParticipanteService.Get(idCampanha));
        }

        [HttpPost]
        [Route("campanhaParticipante")]
        // POST: api/EnqueteApi
        public void Post(int idUsuario, int idCampanha) => _campanhaParticipanteService.Post(idUsuario, idCampanha);

        [HttpDelete]
        [Route("campanhaParticipante/{idUsuario}/{idCampanha}")]
        // PUT: api/EnqueteApi/5
        public void Delete(int idUsuario, int idCampanha) => _campanhaParticipanteService.Delete(idUsuario, idCampanha);
    }
}