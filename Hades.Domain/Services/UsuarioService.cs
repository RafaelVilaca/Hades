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
            if (usuario.IsValid() != 0)
                switch (usuario.IsValid())
                {
                    case 1:
                        return "Email inválido, corrija por favor!";
                    case 2:
                        return "Senha deve conter de 5 à 20 caracteres";
                    case 3:
                        return "Nome deve conter de 3 à 100 caracteres";
                }
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
            if (usuario.IsValid() != 0)
                switch (usuario.IsValid())
                {
                    case 1:
                        return "Email inválido, corrija por favor!";
                    case 2:
                        return "Senha deve conter de 5 à 20 caracteres";
                    case 3:
                        return "Nome deve conter de 3 à 100 caracteres";
                }
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
