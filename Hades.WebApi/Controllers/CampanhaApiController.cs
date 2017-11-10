using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using System.Web.Http;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class CampanhaApiController : ApiController
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaApiController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [HttpGet]
        [Route("campanha")]
        public IHttpActionResult Get()
        {
            return Ok(_campanhaService.GetCampanhas());
        }

        [HttpGet]
        [Route("campanha/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_campanhaService.GetCampanha(id));
        }

        [HttpPost]
        [Route("campanha")]
        public void Post(CampanhaDto campanha) => _campanhaService.Post(campanha);

        [HttpPut]
        [Route("campanha")]
        public void Put(CampanhaDto campanha) => _campanhaService.Put(campanha);

        [HttpPut]
        [Route("campanha/{id}")]
        public void StatusCampanha(int id) => _campanhaService.Delete(id);

    }
}