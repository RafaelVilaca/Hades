using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        string Post(Usuario usuario);
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        string Put(Usuario usuario);
        void StatusUsuario(int id, bool status);
    }
}
