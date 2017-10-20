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
        // GET: api/EnqueteApi
        public IHttpActionResult GetAll()
        {
            return Ok(_enqueteService.GetAll());
        }

        [HttpGet]
        [Route("enquete/{id}")]
        // GET: api/EnqueteApi/5
        public IHttpActionResult GetById(int id)
        {
            return Ok(_enqueteService.GetById(id));
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
        [Route("enquete/{id}")]
        public void StatusEnquete(int id)
        {
            _enqueteService.StatusEnquete(id);
        }
    }
}
