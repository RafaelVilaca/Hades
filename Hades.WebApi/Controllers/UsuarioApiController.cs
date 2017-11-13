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
        public IHttpActionResult GetAll()
        {
            var usuarios = _usuarioService.GetAll();
            if (usuarios != null)
                return Ok(usuarios);
            return BadRequest("Erro ao trazer Lista de Usuarios");
        }

        [HttpGet]
        [Route("usuario/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var usuarios = _usuarioService.GetById(id);
            if (usuarios != null)
                return Ok(usuarios);
            return BadRequest("Erro ao trazer Lista de Usuarios");
        }

        public IHttpActionResult GetByName(string nome)
        {
            return Ok(_usuarioService.GetByName(nome));
        }

        [HttpGet]
        [Route("usuarioSenha/{senha}")]
        public IHttpActionResult SenhaFormatada(string senha)
        {
            return Ok(_usuarioService.SenhaFormatada(senha));
        }

        [HttpPost]
        [Route("usuario")]
        public void Post(Usuario usuario) => _usuarioService.Post(usuario);

        [HttpPut]
        [Route("usuario")]
        // PUT: api/UsuarioApi/5
        public void Put(Usuario usuario) => _usuarioService.Put(usuario);

        [HttpPut]
        [Route("usuario/{id}/{status}")]
        public void StatusUsuario(int id, bool status) => _usuarioService.StatusUsuario(id, status);
    }
}
