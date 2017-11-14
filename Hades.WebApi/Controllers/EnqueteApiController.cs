using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using System.Web.Http;

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
        public IHttpActionResult GetAll()
        {
            return Ok(_enqueteService.GetAll());
        }

        [HttpGet]
        [Route("todasEnquetes")]
        public IHttpActionResult GetTodasEnquetes()
        {
            return Ok(_enqueteService.GetTodasEnquetes());
        }

        [HttpGet]
        [Route("enquete/{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_enqueteService.GetById(id));
        }

        [HttpPost]
        [Route("enquete")]
        public void Post(EnqueteDto enquete)
        {
            _enqueteService.Post(enquete);
        }

        [HttpPut]
        [Route("enquete")]
        public void Put(EnqueteDto enquete)
        {
            _enqueteService.Put(enquete);
        }

        [HttpPut]
        [Route("enquete/{id}")]
        public void StatusEnquete(int id)
        {
            _enqueteService.StatusEnquete(id);
        }
    }
}
