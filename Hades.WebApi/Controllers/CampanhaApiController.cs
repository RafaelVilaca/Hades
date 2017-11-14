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
        [Route("campanhas/{idUsuario}")]
        public IHttpActionResult GetCampanhas(int idUsuario)
        {
            return Ok(_campanhaService.GetCampanhas(idUsuario));
        }

        [HttpGet]
        [Route("todasCampanhas/{idUsuario}")]
        public IHttpActionResult GetTodasCampanhas(int idUsuario)
        {
            return Ok(_campanhaService.GetTodasCampanhas(idUsuario));
        }

        [HttpGet]
        [Route("campanha/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_campanhaService.GetCampanha(id));
        }

        [HttpPost]
        [Route("campanha")]
        public IHttpActionResult Post(CampanhaDto campanha)
        {
            _campanhaService.Post(campanha);
            return Ok();
        }

        [HttpPut]
        [Route("campanha")]
        public IHttpActionResult Put(CampanhaDto campanha)
        {
            _campanhaService.Put(campanha);
            return Ok();
        }

        [HttpPut]
        [Route("campanha/{id}")]
        public IHttpActionResult StatusCampanha(int id)
        {
            _campanhaService.Delete(id);
            return Ok();
        }

    }
}