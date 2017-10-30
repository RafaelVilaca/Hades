using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        string Post(Usuario usuario);
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        string Put(Usuario usuario);
        void StatusUsuario(int id, bool status);
        Usuario GetByName(string nome);
        string SenhaFormatada(string senha);
    }
}
