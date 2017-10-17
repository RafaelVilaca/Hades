using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        void Post(Usuario usuario);
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        void Put(Usuario usuario);
        void StatusUsuario(int id, bool status);
        Usuario GetByName(string nome);
    }
}
