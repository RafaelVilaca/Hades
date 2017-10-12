using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Services;
using System.Web.Http;

namespace Hades.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UsuarioApiController : ApiController
    {
        private readonly IUsuarioService _usuarioService;
        
        public UsuarioApiController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("usuarios")]
        // GET: api/UsuarioApi
        public IHttpActionResult GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            if (usuarios != null)
                return Ok(usuarios);
            return BadRequest("Erro ao trazer Lista de Usuarios");
        }

        [HttpGet]
        [Route("usuario/{id}")]
        // GET: api/UsuarioApi/5
        public IHttpActionResult GetById(int id)
        {
            var usuarios = _usuarioService.GetById(id);
            if (usuarios != null)
                return Ok(usuarios);
            return BadRequest("Erro ao trazer Lista de Usuarios");
        }

        [HttpPost]
        [Route("usuario")]
        // POST: api/UsuarioApi
        public void Post(Usuario usuario)
        {
            _usuarioService.Post(usuario);
        }

        [HttpPut]
        [Route("usuario")]
        // PUT: api/UsuarioApi/5
        public void Put(Usuario usuario)
        {
            _usuarioService.Put(usuario);
        }

        [HttpPut]
        [Route("usuario/{id}/{status}")]
        public void StatusUsuario(int id, bool status)
        {
            _usuarioService.StatusUsuario(id, status);
        }
    }
}
