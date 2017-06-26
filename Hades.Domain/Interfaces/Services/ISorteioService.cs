using System.Collections.Generic;
using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioService
    {
        string Post(Sorteio sorteio);
        Sorteio GetById(int id);
        IEnumerable<Sorteio> GetAll();
        string Put(Sorteio sorteio);
        void Delete(int id);
    }
}
