using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Hades.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public string Post(Usuario usuario)
        {
            _usuarioRepository.Post(usuario);
            return string.Empty;
        }

        public Usuario GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        public Usuario GetByName(string nome)
        {
            return _usuarioRepository.GetByName(nome);
        }

        public string Put(Usuario usuario)
        {
            _usuarioRepository.Put(usuario);
            return string.Empty;
        }

        public void StatusUsuario(int id, bool status)
        {
            _usuarioRepository.StatusUsuario(id, status);
        }

        public string SenhaFormatada(string senha)
        {
            return _usuarioRepository.SenhaFormatada(senha);
        }
    }
}
