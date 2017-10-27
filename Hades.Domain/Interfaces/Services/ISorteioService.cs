using Hades.Domain.Entities;
using System.Collections.Generic;

namespace Hades.Domain.Interfaces.Services
{
    public interface ISorteioService
    {
        string Post(Sorteio sorteio);
        Sorteio GetById(int id);
        IEnumerable<Sorteio> GetAll(int idUsuario);
        string Put(Sorteio sorteio);
        void Delete(int id);
    }
}
