using System.Web.Http;
using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class EnqueteApiController : ApiController
    {
        private readonly IEnqueteService _enqueteService;

        public EnqueteApiController(IEnqueteService enqueteService)
        {
            _enqueteService = enqueteService;
        }

        [HttpGet]
        [Route("enquete")]
        // GET: api/EnqueteApi
        public IHttpActionResult GetAll()
        {
            var enquetes = _enqueteService.GetAll();
            if (enquetes != null)
                return Ok(enquetes);
            return BadRequest("Erro ao trazer Lista de Enquetes");
        }

        [HttpGet]
        [Route("enquete/{id}")]
        // GET: api/EnqueteApi/5
        public IHttpActionResult GetById(int id)
        {
            var enquete = _enqueteService.GetById(id);
            if (enquete != null)
                return Ok(enquete);
            return BadRequest("Erro ao trazer Lista de Enquetes");
        }

        [HttpPost]
        [Route("enquete")]
        // POST: api/EnqueteApi
        public void Post(Enquete enquete)
        {
            _enqueteService.Post(enquete);
        }

        [HttpPut]
        [Route("enquete")]
        // PUT: api/EnqueteApi/5
        public void Put(Enquete enquete)
        {
            _enqueteService.Put(enquete);
        }

        [HttpPut]
        [Route("enquete/{id}/{status}")]
        public void StatusEnquete(int id, bool status)
        {
            _enqueteService.StatusEnquete(id, status);
        }
    }
}
