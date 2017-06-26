using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface ISorteioRepository
    {
        void Post(Sorteio sorteio);
        Sorteio GetById(int id);
        IEnumerable<Sorteio> GetAll();
        void Put(Sorteio sorteio);
        void Delete(int id);
    }
}
