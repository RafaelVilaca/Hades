using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioRepository
    {
        void Post(Sorteio sorteio);
        Sorteio GetById(int id);
        IEnumerable<Sorteio> GetAll(int idUsuario);
        void Put(Sorteio sorteio);
        void Delete(int id);
    }
}
